using System.Text;

namespace UnionOf.Generator
{
	public abstract class AbstractWriter
	{
		private StringBuilder _builder = new();
		private string _lineStartIndentation = "";
		private const string _indent = "    ";
		private bool _isLineStart = true;

		public string GeneratedText() => _builder.ToString();

		protected void Write(string text)
		{
			if (_isLineStart)
			{
				_builder.Append(_lineStartIndentation);
				_isLineStart = false;
			}

			_builder.Append(text);
		}

		protected void WriteLine(string? text = null)
		{
			if (text != null) Write(text);
			_builder.AppendLine();
			_isLineStart = true;
		}

		protected void WriteNested(Action action)
		{
			var oldLineStartIndentation = _lineStartIndentation;
			_lineStartIndentation += _indent;
			action();
			_lineStartIndentation = oldLineStartIndentation;
		}

		protected void WriteNested(string open, string close, Action action)
		{
			if (!_isLineStart)
				WriteLine();
			WriteLine(open);
			WriteNested(action);
			WriteLine(close);
		}

		protected void WriteBrace(Action action)
		{
			WriteNested("{", "}", action);
		}

		protected void WriteBrace(string? text, Action action)
		{
			WriteLine(text);
			WriteNested("{", "}", action);
		}
	}
}