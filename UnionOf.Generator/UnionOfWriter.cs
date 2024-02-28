namespace UnionOf.Generator
{
	public class UnionOfWriter(UnionOfMetadata union) : AbstractWriter
	{
		public string GetCode()
		{
			WriteFile();
			return GeneratedText();
		}

		private void WriteFile()
		{
			if (union.Usings.Any())
			{
				foreach (var @using in union.Usings)
				{
					Write(@using);
				}
			}

			WriteLine("#nullable disable");
			WriteLine();

			if (!string.IsNullOrEmpty(union.Namespace))
			{
				WriteLine($"namespace {union.Namespace};");
			}
			WriteUnionOf();
		}

		private void WriteUnionOf()
		{
			WriteLine($"{union.Modifiers} struct {union.NameTyped} : IEquatable<{union.NameTyped}>");
			WriteBrace(() =>
			{
				WriteInternalValue();
				WriteConstructor();
				WriteEquatables();
				WriteToString();
			});
		}

		private void WriteInternalValue()
		{
			WriteLine("private readonly object _value;");
			WriteLine("");
			WriteBrace("public object Value", () =>
			{
				WriteLine("get => _value;");
				WriteBrace("init", () =>
				{
					WriteLine("ArgumentNullException.ThrowIfNull(value);");
					WriteBrace($"if (value is {string.Join(" || value is ", union.Types)})", () =>
					{
						WriteLine("_value = value;");
					});
					WriteBrace("else", () =>
					{
						WriteLine("throw new InvalidCastException(\"Type not allowed\");");
					});
				});
			});
		}

		private void WriteConstructor()
		{
			WriteLine($"public {union.Name}() => throw new InvalidCastException(\"Value must be specified\");");

			foreach (var type in union.Types)
			{
				WriteLine();
				WriteLine();
				WriteLine($"public {union.Name}({type} value) => _value = value;");
				WriteLine();
				WriteLine($"public static {union.NameTyped} Create({type} value) => new(value);");
				WriteLine();
				WriteLine($"public static implicit operator {union.NameTyped}({type} value) => Create(value);");
				WriteLine();
			}
		}

		private void WriteEquatables()
		{
			WriteLine($"public static bool operator ==({union.NameTyped} left, {union.NameTyped} right) => left.Equals(right);");
			WriteLine($"public static bool operator !=({union.NameTyped} left, {union.NameTyped} right) => !left.Equals(right);");
			WriteLine();
			WriteBrace($"public bool Equals({union.NameTyped} other)", () =>
			{
				WriteLine($"if (ReferenceEquals(null, other)) return false;");
				WriteLine($"return _value.GetType() == other.Value.GetType() && Equals(_value, other.Value);");
			});
			WriteLine();
			WriteLine($"public override bool Equals(object obj) => ReferenceEquals(null, obj) ? false : obj is {union.NameTyped} o && Equals(o);");
			WriteLine();
			WriteLine($"public override int GetHashCode() => _value.GetHashCode();");
		}

		private void WriteToString()
		{
			// object.ToString()
			WriteLine("public override string ToString() => _value.ToString()!;");
		}
	}
}