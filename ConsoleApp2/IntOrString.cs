using UnionOf;

namespace ConsoleApp2
{
    [UnionOf]
    public readonly partial struct IntOrString : IUnionOf<int, string>, IHandleDefaultValue
    {
        // User logic
        public int GetInt() => Value switch
        {
            int number => number,
            string text => ParseString(text),
            _ => throw new InvalidOperationException()
        };

        private static int ParseString(string s) => int.TryParse(s, out int num) ? num : 0;

        public object ParseNull() => "no number";
    }

}
