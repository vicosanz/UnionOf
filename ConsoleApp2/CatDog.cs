using UnionOf;

namespace ConsoleApp2
{
    [UnionOf]
    public readonly partial struct CatDog : IUnionOf<Cat, Dog>
    {
    }

    public class Cat
    {
        public string Type => "Cat";
    }

    public class Dog
    {
        public string Type => "Dog";

    }
}
