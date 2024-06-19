using Microsoft.CodeAnalysis;

namespace UnionOf.Generator
{
    public static class DiagnosticDescriptors
    {
        public static readonly DiagnosticDescriptor StructNotPartial =
            new(
                "UNI001",
                "Struct must be declared as 'public readonly partial struct'",
                "Struct {0} must be declared as 'public readonly partial struct'",
                DiagnosticCategories.UnionOf,
                DiagnosticSeverity.Warning,
                true
            );
        public static readonly DiagnosticDescriptor TypesNotDefined =
            new(
                "UNI002",
                "Struct must to declare types in [UnionOf(typeof(A),...)] or inherits from IUnionOf<A, B...>",
                "Struct {0} must to declare types in [UnionOf(typeof(A),...)] or inherits from IUnionOf<A, B...>",
                DiagnosticCategories.UnionOf,
                DiagnosticSeverity.Warning,
                true
            );

    }
}
