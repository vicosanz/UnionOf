using System;
using System.Threading.Tasks;
using static UnionOf.ErrOr;

namespace UnionOf
{
    public interface IErrOr : IUnionOf
    {
        Exception ValueException { get; }
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

        /// <summary>
        /// Create a new ErrOr with value wrapped
        /// </summary>
        /// <typeparam name="T">Type of valid value</typeparam>
        /// <param name="value">value to be wrapped</param>
        /// <returns>A new ErrOr object with value wrapped</returns>
        public static ErrOr<T> Of<T>(T value) => new(value);

        /// <summary>
        /// Create a new ErrOr with an exception wrapped
        /// </summary>
        /// <typeparam name="T">Type of valid value</typeparam>
        /// <param name="error">Exception value</param>
        /// <returns>A new ErrOr object with an exception wrapped</returns>
        public static ErrOr<T> Fail<T>(Exception error) => new(error);


        public delegate ErrOr<T> MapDelegate<T>(T request);

        public static ErrOr<T> Map<T>(this ErrOr<T> errOr, MapDelegate<T> map) =>
            errOr.Is(out T value) ? map(value) : errOr;


        public delegate Task<ErrOr<T>> MapDelegateAsync<T>(T request);
        public static async Task<ErrOr<T>> MapAsync<T>(this ErrOr<T> errOr, MapDelegateAsync<T> map) =>
            errOr.Is(out T value) ? await map(value) : errOr;


        public delegate void TapDelegate<T>(T request);

        public static ErrOr<T> Tap<T>(this ErrOr<T> errOr, TapDelegate<T> tap)
        {
            if (errOr.Is(out T value))
            {
                tap(value);
            }
            return errOr;
        }

        public delegate Task TapDelegateAsync<T>(T request);
        public static async Task<ErrOr<T>> TapAsync<T>(this ErrOr<T> errOr, TapDelegateAsync<T> map)
        {
            if (errOr.Is(out T value))
            {
                await map(value);
            }
            return errOr;
        }

        public delegate ErrOr<TResponse> BindDelegate<TRequest, TResponse>(TRequest request);
        public static ErrOr<T2> Bind<T1, T2>(this ErrOr<T1> errOr, BindDelegate<T1, T2> bind) =>
            errOr.Is(out T1 value) ? bind(value) : errOr.ValueException;


        public delegate ErrOr<TResponse> BindDefaultDelegate<TResponse>();
        public static ErrOr<T2> BindOrDefault<T1, T2>(this ErrOr<T1> errOr, BindDelegate<T1, T2> bind, BindDefaultDelegate<T2> @default = null)
        {
            if (errOr.Is(out T1 value))
            {
                try
                {
                    var result = bind(value);
                    return result;
                }
                catch (Exception ex)
                {
                    if (@default == null)
                        return ex;
                    return @default();
                }
            }
            return errOr.ValueException;
        }

        public delegate Task<ErrOr<TResponse>> BindDelegateAsync<TRequest, TResponse>(TRequest request);
        public static async Task<ErrOr<T2>> BindAsync<T1, T2>(this ErrOr<T1> errOr, BindDelegateAsync<T1, T2> bind) =>
            errOr.Is(out T1 value) ? await bind(value) : errOr.ValueException;


        public delegate Task<ErrOr<TResponse>> BindDefaultDelegateAsync<TResponse>();
        public static async Task<ErrOr<T2>> BindOrDefaultAsync<T1, T2>(this ErrOr<T1> errOr, BindDelegateAsync<T1, T2> bind, BindDefaultDelegateAsync<T2> @default)
        {
            if (errOr.Is(out T1 value))
            {
                try
                {
                    var result = await bind(value);
                    return result;
                }
                catch (Exception ex)
                {
                    if (@default == null)
                        return ex;
                    return await @default();
                }
            }
            return errOr.ValueException;
        }


        public static async Task<ErrOr<T2>> BindOrDefaultAsync<T1, T2>(this ErrOr<T1> errOr, BindDelegateAsync<T1, T2> bind, BindDefaultDelegate<T2> @default)
        {
            if (errOr.Is(out T1 value))
            {
                try
                {
                    var result = await bind(value);
                    return result;
                }
                catch (Exception ex)
                {
                    if (@default == null)
                        return ex;
                    return @default();
                }
            }
            return errOr.ValueException;
        }


        public delegate TResult MatchValid<T0, TResult>(T0 value);
        public delegate TResult MatchInvalid<TResult>(Exception value);

        /// <summary>
        /// Get Inner type using a predicate
        /// </summary>
        /// <typeparam name="TResult">Type of Mapped object</typeparam>
        /// <param name="mapT0">Predicate to map valid type</param>
        /// <param name="mapError">Predicate to map Exception type</param>
        /// <returns>Mapped object</returns>
        /// <exception cref="InvalidCastException"></exception>
        public static TResult Match<T0, TResult>(this ErrOr<T0> errOr, 
            MatchValid<T0, TResult> mapT0, MatchInvalid<TResult> mapError) => 
            errOr.Value switch
            {
                Exception error => mapError(error),
                T0 valueT0 => mapT0(valueT0),
                _ => throw new InvalidCastException()
            };

        public delegate Task<TResult> MatchValidTask<T0, TResult>(T0 value);
        public delegate Task<TResult> MatchInvalidTask<TResult>(Exception value);

        /// <summary>
        /// Get Inner type using a predicate
        /// </summary>
        /// <typeparam name="TResult">Type of Mapped object</typeparam>
        /// <param name="mapT0"></param>
        /// <param name="mapError"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public static async Task<TResult> MatchAsync<T0, TResult>(this ErrOr<T0> errOr, 
            MatchValidTask<T0, TResult> mapT0, MatchInvalidTask<TResult> mapError) =>
            errOr.Value switch
            {
                Exception error => await mapError(error),
                T0 valueT0 => await mapT0(valueT0),
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
        public static async Task<TResult> MatchAsync<T0, TResult>(this ErrOr<T0> errOr, 
            MatchValidTask<T0, TResult> mapT0, MatchInvalid<TResult> mapError) =>
            errOr.Value switch
            {
                Exception error => mapError(error),
                T0 valueT0 => await mapT0(valueT0),
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
        public static async Task<TResult> MatchAsync<T0, TResult>(this ErrOr<T0> errOr,
            MatchValid<T0, TResult> mapT0, MatchInvalidTask<TResult> mapError) =>
            errOr.Value switch
            {
                Exception error => await mapError(error),
                T0 valueT0 => mapT0(valueT0),
                _ => throw new InvalidCastException()
            };
    }
}
