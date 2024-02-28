using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnionOf;

namespace UnionOf
{	public interface IErrOr
	{
	}

	//Predefined ErrOr type
	[UnionOf]
	public readonly partial struct ErrOr<T0> : IUnionOf<T0, Exception>, IErrOr
	{
	}

	[UnionOf]
	public readonly partial struct ErrOr<T0, T1> : IUnionOf<T0, T1, Exception>, IErrOr
	{
	}

	[UnionOf]
	public readonly partial struct ErrOr<T0, T1, T2> : IUnionOf<T0, T1, T2, Exception>, IErrOr
	{
	}

	[UnionOf]
	public readonly partial struct ErrOr<T0, T1, T2, T3> : IUnionOf<T0, T1, T2, T3, Exception>, IErrOr
	{
	}

}
