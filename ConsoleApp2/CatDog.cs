using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
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
