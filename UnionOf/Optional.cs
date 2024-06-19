using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace UnionOf
{
    public interface IOptional
    {
    }

    //Predefined Optional type

    /// <summary>
    /// Optional pattern implementation
    /// </summary>
    /// <typeparam name="T0">Type of valid value otherwise Empty</typeparam>
    [UnionOf]
    public readonly partial struct Optional<T0> : IUnionOf<T0, Empty>, IOptional, IHandleDefaultValue
    {
        public static readonly Optional<T0> Default = Empty.Default;
        public object ParseNull() => Default;

        /// <summary>
        /// Evaluate a predicate and returns this Optional object or new Optional empty
        /// </summary>
        /// <param name="predicate">Predicate to evaluate</param>
        /// <returns>if true return this object otherwise a new Optional empty</returns>
        public Optional<T0> When(Predicate<T0> predicate) =>
            Value is T0 value && predicate(value) ? this : Default;

        /// <summary>
        /// Evaluate a negative predicate and returns this Optional object or new Optional empty
        /// </summary>
        /// <param name="predicate">Negative predicate to evaluate</param>
        /// <returns>if false return this object otherwise a new Optional empty</returns>
        public Optional<T0> WhenNot(Predicate<T0> predicate) =>
            Value is T0 value && !predicate(value) ? this : Default;

        /// <summary>
        /// Create an Optional of different type based in this Optional object
        /// </summary>
        /// <typeparam name="TResult">Type of a result Optional</typeparam>
        /// <param name="map">Map predicate</param>
        /// <returns>A new Optional of <see cref="TResult"/> after map execution</returns>
        public Optional<TResult> Map<TResult>(Func<T0, TResult> map) =>
            Value is T0 value ? new Optional<TResult>(map(value)) : new Optional<TResult>();

        /// <summary>
        /// Create an Optional of different type based in this Optional object
        /// </summary>
        /// <typeparam name="TResult">Type of a result Optional</typeparam>
        /// <param name="map">Map predicate</param>
        /// <returns>A new Optional of <see cref="TResult"/> after map execution</returns>
        public async Task<Optional<TResult>> MapAsync<TResult>(Func<T0, Task<TResult>> map) =>
            Value is T0 value ? new Optional<TResult>(await map(value)) : new Optional<TResult>();

        /// <summary>
        /// Return inner value if is valid otherwise return default value
        /// </summary>
        /// <param name="default">Value returned if inner value is Empty</param>
        /// <returns>Inner value or default</returns>
        public T0 Reduce(T0 @default) => Value is T0 value ? value : @default;

        /// <summary>
        /// Return inner value if is valid otherwise return default value
        /// </summary>
        /// <param name="default">Value returned if inner value is Empty</param>
        /// <returns>Inner value or default</returns>
        public T0 Reduce(Func<T0> @default) => Value is T0 value ? value : @default();
    }

    public readonly struct Empty : IEquatable<Empty>, IComparable<Empty>
    {
        public static readonly Empty Default;

        public static bool operator true(Empty _) => false;
        public static bool operator false(Empty _) => true;

        public override int GetHashCode() => 0;
        public int CompareTo(Empty other) => 0;

        public override string ToString() => "<Empty>";

        public bool Equals(Empty other) => true;
        public override bool Equals(object obj) => obj is Empty o && Equals(o);

        public static bool operator ==(Empty left, Empty right) => left.Equals(right);

        public static bool operator !=(Empty left, Empty right) => !(left == right);

        public static bool operator <(Empty left, Empty right) => left.CompareTo(right) < 0;

        public static bool operator <=(Empty left, Empty right) => left.CompareTo(right) <= 0;

        public static bool operator >(Empty left, Empty right) => left.CompareTo(right) > 0;

        public static bool operator >=(Empty left, Empty right) => left.CompareTo(right) >= 0;
    }

    public static class Optional
    {
        public static Optional<T0> ToEmpty<T0>() => Optional<T0>.Default;

        /// <summary>
        /// Create an Optional using a value
        /// </summary>
        /// <typeparam name="T0">Type of Optional result</typeparam>
        /// <param name="value">Source value</param>
        /// <returns>A new Optional object, if value is null returns Optional empty</returns>
        public static Optional<T0> Of<T0>(T0 value) => value == null ? new Optional<T0>() : new(value);

        public static Optional<T0> Of<T0, T1>(Optional<T1> value)
            => value.Map((value) => (T0)Convert.ChangeType(value, typeof(T0)));

        public static Optional<TEnum> ParseEnum<TEnum>(string value) where TEnum : struct =>
            Parse<TEnum>(Enum.TryParse, value);

        public static Optional<TEnum> ParseEnumIgnoreCase<TEnum>(string value) where TEnum : struct =>
            ParseIgnoreCase<TEnum>(Enum.TryParse, value);

        public static Optional<T> Parse<T>(string value)
        {
            if (typeof(T) == typeof(Guid))
                return Of<T, Guid>(Parse<Guid>(Guid.TryParse, value));

            if (typeof(T) == typeof(DateTimeOffset))
                return Of<T, DateTimeOffset>(Parse<DateTimeOffset>(DateTimeOffset.TryParse, value));

            if (typeof(T) == typeof(TimeSpan))
                return Of<T, TimeSpan>(Parse<TimeSpan>(TimeSpan.TryParse, value));

            return Type.GetTypeCode(typeof(T)) switch
            {
                TypeCode.Empty => ToEmpty<T>(),
                TypeCode.Boolean => Of<T, bool>(Parse<bool>(bool.TryParse, value)),
                TypeCode.Char => Of<T, char>(Parse<char>(char.TryParse, value)),
                TypeCode.SByte => Of<T, sbyte>(Parse<sbyte>(sbyte.TryParse, value)),
                TypeCode.Byte => Of<T, byte>(Parse<byte>(byte.TryParse, value)),
                TypeCode.Int16 => Of<T, short>(Parse<short>(short.TryParse, value)),
                TypeCode.Int32 => Of<T, int>(Parse<int>(int.TryParse, value)),
                TypeCode.UInt16 => Of<T, ushort>(Parse<ushort>(ushort.TryParse, value)),
                TypeCode.UInt32 => Of<T, uint>(Parse<uint>(uint.TryParse, value)),
                TypeCode.Int64 => Of<T, long>(Parse<long>(long.TryParse, value)),
                TypeCode.UInt64 => Of<T, ulong>(Parse<ulong>(ulong.TryParse, value)),
                TypeCode.Single => Of<T, float>(Parse<float>(float.TryParse, value)),
                TypeCode.Double => Of<T, double>(Parse<double>(double.TryParse, value)),
                TypeCode.Decimal => Of<T, decimal>(Parse<decimal>(decimal.TryParse, value)),
                TypeCode.DateTime => Of<T, DateTime>(Parse<DateTime>(DateTime.TryParse, value)),
                TypeCode.String => Of<T, string>(value),
                _ => throw new InvalidCastException()
            };
        }

        private delegate bool TryParse<T>(string value, out T result);

        private static Optional<T> Parse<T>(TryParse<T> tryParse, string value) =>
            !tryParse(value, out T result) ? new Optional<T>() : Of(result);

        private delegate bool TryParseIgnoreCase<T>(string value, bool IgnoreCase, out T result);

        private static Optional<T> ParseIgnoreCase<T>(TryParseIgnoreCase<T> tryParse, string value) =>
            !tryParse(value, true, out T result) ? new Optional<T>() : Of(result);

        public static Type GetUnderlyingType(Type optionalType)
        {
            ArgumentNullException.ThrowIfNull(optionalType);
            Contract.EndContractBlock();
            Type result = null;
            if (optionalType.IsGenericType && !optionalType.IsGenericTypeDefinition)
            {
                // instantiated generic type only                
                Type genericType = optionalType.GetGenericTypeDefinition();
                if (ReferenceEquals(genericType, typeof(Optional<>)))
                {
                    result = optionalType.GetGenericArguments()[0];
                }
            }
            return result;
        }

    }

}
