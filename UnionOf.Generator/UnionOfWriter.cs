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

			if (!union.AllowNulls)
			{
				WriteLine("#nullable disable");
			}
			else
			{
				WriteLine("#nullable enable");
			}
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
			WriteLine($"private readonly object{Nullable(union)} _value;");
			WriteLine("[System.Text.Json.Serialization.JsonIgnore]");
			WriteBrace($"public object{Nullable(union)} Value", () =>
			{
				WriteLine("get => _value;");
				WriteLine($"init => _value = value ?? ParseNull();");
			});
		}

		private static string Nullable(UnionOfMetadata union) => union.AllowNulls ? "?" : "";

		private void WriteConstructor()
		{
			if (!union.AllowNulls)
			{
				WriteLine("[Obsolete(\"Only for serializer\", true)]");
                WriteLine($"public {union.Name}() {{ }}");
            }
			else
			{
                WriteLine($"public {union.Name}() => Value = null;");
            }

            if (!union.AllowNulls)
			{
				WriteLine();
				WriteLine("public object ParseNull() => throw new InvalidCastException(\"Null not allowed\");");
				WriteLine();
			}

			foreach (var type in union.Types)
			{
                WriteLine();
                WriteLine();
                WriteBrace($"public {type}{Nullable(union)} Value{type.Replace(".", "_")}", () =>
				{
					WriteLine($"get => Value is {type} value ? value : default;");
					WriteBrace($"init", () =>
					{
						WriteLine($"if (value != null) Value = value;");
					});
				});
				WriteLine();
				WriteLine($"public {union.Name}({type}{Nullable(union)} value) => Value = value;");
				WriteLine();
				WriteLine($"public static {union.NameTyped} Create({type}{Nullable(union)} value) => new(value);");
				WriteLine();
				WriteLine($"public static implicit operator {union.NameTyped}({type}{Nullable(union)} value) => Create(value);");
				WriteLine();
				WriteLine($"public static explicit operator {type}({union.NameTyped} source) => source.Value is {type} value ? value : throw new InvalidCastException();");
				WriteLine();
				WriteBrace($"public bool Is(out {type}{Nullable(union)} valueOut)", () =>
				{
					WriteBrace($"if (Value is {type} result)", () =>
					{
						WriteLine($"valueOut = result;");
						WriteLine("return true;");
					});
                    WriteLine($"valueOut = default;");
                    WriteLine("return false;");
                });
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
				WriteLine("return _value is not null ? other.Value is not null && _value.Equals(other.Value) : other.Value is null;");
			});
			WriteLine();
			WriteLine($"public override bool Equals(object{Nullable(union)} obj) => obj is not null && obj is {union.NameTyped} o && Equals(o);");
			WriteLine();
			WriteLine($"public override int GetHashCode() => {(union.AllowNulls ? "_value?.GetHashCode() ?? 0;" : "_value.GetHashCode();")}");
		}

		private void WriteToString()
		{
			// object.ToString()
			WriteLine($"public override string ToString() => {(union.AllowNulls ? "_value?.ToString() ?? \"\"" : "_value.ToString()")};");
		}
	}
}