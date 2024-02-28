using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnionOf;

namespace ConsoleApp2
{
    [UnionOf(typeof(bool), typeof(Exception))] 
    public readonly partial struct Result 
    {
    }

    [UnionOf]
    public readonly partial struct ResultOperation : IUnionOf<ResultOperation.Ok, ResultOperation.Err>
    {
		public record Ok { }
        public static ResultOperation IsOk() => Create(new Ok());


		public record Err(string Message);
		public static ResultOperation IsErr(string message) => Create(new Err(message));

	}
}
