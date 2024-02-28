using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnionOf;

namespace UnionOf
{	public interface IErrOr : IUnionOf
	{
	}

	//Predefined ErrOr type
	/// <summary>
	/// UnionOf a type with an exception
	/// </summary>
	/// <typeparam name="T0">Type of valid value</typeparam>
	[UnionOf]
	public readonly partial struct ErrOr<T0> : IUnionOf<T0, Exception>, IErrOr
	{
	}

	/// <summary>
	/// UnionOf valid types with an exception
	/// </summary>
	/// <typeparam name="T0"></typeparam>
	/// <typeparam name="T1"></typeparam>
	[UnionOf]
	public readonly partial struct ErrOr<T0, T1> : IUnionOf<T0, T1, Exception>, IErrOr
	{
	}

	/// <summary>
	/// UnionOf valid types with an exception
	/// </summary>
	/// <typeparam name="T0"></typeparam>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	[UnionOf]
	public readonly partial struct ErrOr<T0, T1, T2> : IUnionOf<T0, T1, T2, Exception>, IErrOr
	{
	}

	/// <summary>
	/// UnionOf valid types with an exception
	/// </summary>
	/// <typeparam name="T0"></typeparam>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	/// <typeparam name="T3"></typeparam>
	[UnionOf]
	public readonly partial struct ErrOr<T0, T1, T2, T3> : IUnionOf<T0, T1, T2, T3, Exception>, IErrOr
	{
	}

	/// <summary>
	/// UnionOf valid types with an exception
	/// </summary>
	/// <typeparam name="T0"></typeparam>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	/// <typeparam name="T3"></typeparam>
	/// <typeparam name="T4"></typeparam>
	[UnionOf]
	public readonly partial struct ErrOr<T0, T1, T2, T3, T4> : IUnionOf<T0, T1, T2, T3, T4, Exception>, IErrOr
	{
	}

	public static class ErrOr
	{
		/// <summary>
		/// True if this UnionOf has an exception type as value
		/// </summary>
		/// <param name="value">A UnionOf ErrOr</param>
		/// <returns>true if inner value is an Exception otherwise false</returns>
		public static bool IsFail(this IErrOr value) => value.Value is Exception;

		/// <summary>
		/// True if this UnionOf has not an exception type as value
		/// </summary>
		/// <param name="value">A UnionOf ErrOr</param>
		/// <returns>true if inner value is not an Exception otherwise false</returns>
		public static bool IsValid(this IErrOr value) => value.Value is not Exception;
	}
}
