using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Text;

namespace UnionOf.Generator
{
    [Generator]
    public class UnionOfGenerator : IIncrementalGenerator
    {
        private static readonly string iUnionOf = "IUnionOf<";
        private static readonly string unionOfAttribute = "UnionOf.UnionOfAttribute";
        private static readonly string iHandleDefaultValue = "IHandleDefaultValue";

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            //#if DEBUG
            //			if (!Debugger.IsAttached) Debugger.Launch();
            //#endif
            IncrementalValuesProvider<TypeDeclarationSyntax> typeDeclarations = context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => s.IsSyntaxTargetForGeneration(),
                    transform: static (ctx, _) => ctx.GetSemanticTargetForGeneration(unionOfAttribute))
                .Where(static m => m is not null)!;

            IncrementalValueProvider<(Compilation, ImmutableArray<TypeDeclarationSyntax>)> compilationAndEnums
                = context.CompilationProvider.Combine(typeDeclarations.Collect());

            context.RegisterSourceOutput(compilationAndEnums,
                static (spc, source) => Execute(source.Item1, source.Item2, spc));
        }

        private static void Execute(Compilation compilation, ImmutableArray<TypeDeclarationSyntax> type, SourceProductionContext context)
        {
            if (type.IsDefaultOrEmpty) return;

            var unionOfs = GetUnionOfs(compilation, type.Distinct(), context);

            if (unionOfs.Any())
            {
                foreach (var unionOf in unionOfs)
                {
                    var generator = new UnionOfWriter(unionOf);
                    context.AddSource(unionOf.GetFileNameGenerated(),
                                      SourceText.From(generator.GetCode(), Encoding.UTF8));
                }
            }
        }

        protected static List<UnionOfMetadata> GetUnionOfs(Compilation compilation,
            IEnumerable<TypeDeclarationSyntax> types, SourceProductionContext context)
        {
            var unionOfs = new List<UnionOfMetadata>();
            foreach (var type in types)
            {
                context.CancellationToken.ThrowIfCancellationRequested();

                SemanticModel semanticModel = compilation.GetSemanticModel(type.SyntaxTree);
                if (semanticModel.GetDeclaredSymbol(type) is not INamedTypeSymbol typeSymbol)
                {
                    // report diagnostic, something went wrong
                    continue;
                }

                var typelist = new List<string>();
                bool allowNulls = false;
                string modifiers = type.GetModifiers();

                if (!modifiers.Contains("partial"))
                {
                    context.ReportDiagnostic(
                        Diagnostic.Create(DiagnosticDescriptors.StructNotPartial, null, typeSymbol.ToString())
                    );
                    continue;
                }

                if (type.BaseList != null)
                {
                    foreach (var baseType in type.BaseList!.Types)
                    {
                        if (baseType.ToFullString().Contains(iUnionOf))
                        {
                            var argumentsType = (GenericNameSyntax)baseType.Type;
                            typelist.AddRange(argumentsType.TypeArgumentList.Arguments.ToList().ConvertAll(x => x.ToFullString()));
                        }
                        else if (baseType.ToFullString().Contains(iHandleDefaultValue))
                        {
                            allowNulls = true;
                        }
                    }
                }
                if (!typelist.Any())
                {
                    foreach (var attribute in typeSymbol.GetAttributes())
                    {
                        if (attribute.AttributeClass!.ToDisplayString().Equals(unionOfAttribute, StringComparison.OrdinalIgnoreCase))
                        {
                            if (attribute.ConstructorArguments.Any())
                            {
                                var argument = attribute.ConstructorArguments.First();
                                typelist.AddRange(argument.Values.ToList().ConvertAll(x => x.Value!.ToString()));
                            }
                        }
                    }
                }
                if (!typelist.Any())
                {
                    context.ReportDiagnostic(
                        Diagnostic.Create(DiagnosticDescriptors.TypesNotDefined, null, typeSymbol.ToString())
                    );
                    continue;
                }
                unionOfs.Add(
                    new UnionOfMetadata(type.GetNamespace(),
                                        type.GetUsings(),
                                        allowNulls,
                                        typeSymbol.Name,
                                        typeSymbol.GetNameTyped(),
                                        typeSymbol.ToString(),
                                        modifiers,
                                        typelist));
            }
            return unionOfs;
        }

    }
}
