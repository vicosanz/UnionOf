using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UnionOf;

namespace UnionOf
{
	public interface IErrOr : IUnionOf
	{
		Exception Error { get; }
	}

	//Predefined ErrOr type
	/// <summary>
	/// UnionOf a type with an exception
	/// </summary>
	/// <typeparam name="T0">Type of valid value</typeparam>
	[UnionOf]
	public readonly partial struct ErrOr<T0> : IUnionOf<T0, Exception>, IErrOr
	{
		/// <summary>
		/// Get Inner type using a predicate
		/// </summary>
		/// <typeparam name="TResult">Type of Mapped object</typeparam>
		/// <param name="mapT0">Predicate to map valid type</param>
		/// <param name="mapError">Predicate to map Exception type</param>
		/// <returns>Mapped object</returns>
		/// <exception cref="InvalidCastException"></exception>
		public TResult Match<TResult>(Func<T0, TResult> mapT0, Func<Exception, TResult> mapError) => Value switch
		{
			Exception error => mapError(error),
			T0 valueT0 => mapT0(valueT0),
			_ => throw new InvalidCastException()
		};

		/// <summary>
		/// Get Inner type using a predicate
		/// </summary>
		/// <typeparam name="TResult">Type of Mapped object</typeparam>
		/// <param name="mapT0"></param>
		/// <param name="mapError"></param>
		/// <returns></returns>
		/// <exception cref="InvalidCastException"></exception>
		public async Task<TResult> MatchAsync<TResult>(Func<T0, Task<TResult>> mapT0, Func<Exception, TResult> mapError) => Value switch
		{
			Exception error => mapError(error),
			T0 valueT0 => await mapT0(valueT0),
			_ => throw new InvalidCastException()
		};
        public T0 ValueT0
        {
            get => Value is T0 value ? value : default;
            init
            {
                if (value != null)
                {
                    Value = value;
                }
            }
        }
        public Exception Error
        {
            get => Value is Exception ex ? ex : default;
            init
            {
                if (value != null)
                {
                    Value = value;
                }
            }
        }
	}

	/// <summary>
	/// UnionOf valid types with an exception
	/// </summary>
	/// <typeparam name="T0">Type of valid value</typeparam>
	/// <typeparam name="T1">Type of valid value</typeparam>
	[UnionOf]
	public readonly partial struct ErrOr<T0, T1> : IUnionOf<T0, T1, Exception>, IErrOr
	{
		/// <summary>
		/// Get Inner type using a predicate
		/// </summary>
		/// <typeparam name="TResult">Type of Mapped object</typeparam>
		/// <param name="mapT0">Predicate to map valid type</param>
		/// <param name="mapT1">Predicate to map valid type</param>
		/// <param name="mapError">Predicate to map Exception type</param>
		/// <returns>Mapped object</returns>
		/// <exception cref="InvalidCastException"></exception>
		public TResult Match<TResult>(Func<T0, TResult> mapT0, Func<T1, TResult> mapT1, Func<Exception, TResult> mapError) => Value switch
		{
			Exception error => mapError(error),
			T0 valueT0 => mapT0(valueT0),
			T1 valueT1 => mapT1(valueT1),
			_ => throw new InvalidCastException()
		};

		public T0 ValueT0 => Value is T0 value ? value : default;
		public T1 ValueT1 => Value is T1 value ? value : default;
		public Exception Error => Value is Exception ex ? ex : default;
	}

	/// <summary>
	/// UnionOf valid types with an exception
	/// </summary>
	/// <typeparam name="T0">Type of valid value</typeparam>
	/// <typeparam name="T1">Type of valid value</typeparam>
	/// <typeparam name="T2">Type of valid value</typeparam>
	[UnionOf]
	public readonly partial struct ErrOr<T0, T1, T2> : IUnionOf<T0, T1, T2, Exception>, IErrOr
	{
		/// <summary>
		/// Get Inner type using a predicate
		/// </summary>
		/// <typeparam name="TResult">Type of Mapped object</typeparam>
		/// <param name="mapT0">Predicate to map valid type</param>
		/// <param name="mapT1">Predicate to map valid type</param>
		/// <param name="mapT2">Predicate to map valid type</param>
		/// <param name="mapError">Predicate to map Exception type</param>
		/// <returns>Mapped object</returns>
		/// <exception cref="InvalidCastException"></exception>
		public TResult Match<TResult>(Func<T0, TResult> mapT0, Func<T1, TResult> mapT1, Func<T2, TResult> mapT2,
			Func<Exception, TResult> mapError) => Value switch
			{
				Exception error => mapError(error),
				T0 valueT0 => mapT0(valueT0),
				T1 valueT1 => mapT1(valueT1),
				T2 valueT2 => mapT2(valueT2),
				_ => throw new InvalidCastException()
			};

		public T0 ValueT0 => Value is T0 value ? value : default;
		public T1 ValueT1 => Value is T1 value ? value : default;
		public T2 ValueT2 => Value is T2 value ? value : default;
		public Exception Error => Value is Exception ex ? ex : default;
	}

	/// <summary>
	/// UnionOf valid types with an exception
	/// </summary>
	/// <typeparam name="T0">Type of valid value</typeparam>
	/// <typeparam name="T1">Type of valid value</typeparam>
	/// <typeparam name="T2">Type of valid value</typeparam>
	/// <typeparam name="T3">Type of valid value</typeparam>
	[UnionOf]
	public readonly partial struct ErrOr<T0, T1, T2, T3> : IUnionOf<T0, T1, T2, T3, Exception>, IErrOr
	{
		/// <summary>
		/// Get Inner type using a predicate
		/// </summary>
		/// <typeparam name="TResult">Type of Mapped object</typeparam>
		/// <param name="mapT0">Predicate to map valid type</param>
		/// <param name="mapT1">Predicate to map valid type</param>
		/// <param name="mapT2">Predicate to map valid type</param>
		/// <param name="mapT3">Predicate to map valid type</param>
		/// <param name="mapError">Predicate to map Exception type</param>
		/// <returns>Mapped object</returns>
		/// <exception cref="InvalidCastException"></exception>
		public TResult Match<TResult>(Func<T0, TResult> mapT0, Func<T1, TResult> mapT1, Func<T2, TResult> mapT2,
			Func<T3, TResult> mapT3, Func<Exception, TResult> mapError) => Value switch
			{
				Exception error => mapError(error),
				T0 valueT0 => mapT0(valueT0),
				T1 valueT1 => mapT1(valueT1),
				T2 valueT2 => mapT2(valueT2),
				T3 valueT3 => mapT3(valueT3),
				_ => throw new InvalidCastException()
			};

		public T0 ValueT0 => Value is T0 value ? value : default;
		public T1 ValueT1 => Value is T1 value ? value : default;
		public T2 ValueT2 => Value is T2 value ? value : default;
		public T3 ValueT3 => Value is T3 value ? value : default;
		public Exception Error => Value is Exception ex ? ex : default;
	}

	/// <summary>
	/// UnionOf valid types with an exception
	/// </summary>
	/// <typeparam name="T0">Type of valid value</typeparam>
	/// <typeparam name="T1">Type of valid value</typeparam>
	/// <typeparam name="T2">Type of valid value</typeparam>
	/// <typeparam name="T3">Type of valid value</typeparam>
	/// <typeparam name="T4">Type of valid value</typeparam>
	[UnionOf]
	public readonly partial struct ErrOr<T0, T1, T2, T3, T4> : IUnionOf<T0, T1, T2, T3, T4, Exception>, IErrOr
	{
		/// <summary>
		/// Get Inner type using a predicate
		/// </summary>
		/// <typeparam name="TResult">Type of Mapped object</typeparam>
		/// <param name="mapT0">Predicate to map valid type</param>
		/// <param name="mapT1">Predicate to map valid type</param>
		/// <param name="mapT2">Predicate to map valid type</param>
		/// <param name="mapT3">Predicate to map valid type</param>
		/// <param name="mapT4">Predicate to map valid type</param>
		/// <param name="mapError">Predicate to map Exception type</param>
		/// <returns>Mapped object</returns>
		/// <exception cref="InvalidCastException"></exception>
		public TResult Match<TResult>(Func<T0, TResult> mapT0, Func<T1, TResult> mapT1, Func<T2, TResult> mapT2,
			Func<T3, TResult> mapT3, Func<T4, TResult> mapT4, Func<Exception, TResult> mapError) => Value switch
			{
				Exception error => mapError(error),
				T0 valueT0 => mapT0(valueT0),
				T1 valueT1 => mapT1(valueT1),
				T2 valueT2 => mapT2(valueT2),
				T3 valueT3 => mapT3(valueT3),
				T4 valueT4 => mapT4(valueT4),
				_ => throw new InvalidCastException()
			};
		public T0 ValueT0 => Value is T0 value ? value : default;
		public T1 ValueT1 => Value is T1 value ? value : default;
		public T2 ValueT2 => Value is T2 value ? value : default;
		public T3 ValueT3 => Value is T3 value ? value : default;
		public T4 ValueT4 => Value is T4 value ? value : default;
		public Exception Error => Value is Exception ex ? ex : default;
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
		/// True if this UnionOf has an exception type as value
		/// </summary>
		/// <param name="value">A UnionOf ErrOr</param>
		/// <param name="exception">The inner Exception</param>
		/// <returns>true if inner value is an Exception otherwise false</returns>
		public static bool IsFail(this IErrOr value, out Exception exception)
		{
			exception = default;
			if (value is not Exception ex) return false;
			exception = ex;
			return true;
		}

		/// <summary>
		/// True if this UnionOf has not an exception type as value
		/// </summary>
		/// <param name="value">A UnionOf ErrOr</param>
		/// <returns>true if inner value is not an Exception otherwise false</returns>
		public static bool IsValid(this IErrOr value) => value.Value is not Exception;
	}
}
