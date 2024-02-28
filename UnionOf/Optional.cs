using System;

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
	public readonly partial struct Optional<T0> : IUnionOf<T0, Empty>, IOptional
	{
		/// <summary>
		/// Evaluate a predicate and returns this Optiomal object or new Optional empty
		/// </summary>
		/// <param name="predicate">Predicate to evaluate</param>
		/// <returns>if true return this object otherwise a new Optional empty</returns>
		public Optional<T0> When(Func<T0, bool> predicate) =>
			Value is T0 value && predicate(value) ? this : new(Optional.Empty);

		/// <summary>
		/// Evaluate a negative predicate and returns this Optiomal object or new Optional empty
		/// </summary>
		/// <param name="predicate">Negative predicate to evaluate</param>
		/// <returns>if false return this object otherwise a new Optional empty</returns>
		public Optional<T0> WhenNot(Func<T0, bool> predicate) =>
			Value is T0 value && !predicate(value) ? this : new(Optional.Empty);

		/// <summary>
		/// Create an Optional of different type based in this Optiomal object
		/// </summary>
		/// <typeparam name="TResult">Type of a result Optional</typeparam>
		/// <param name="map">Map predicate</param>
		/// <returns>A new Optional of <see cref="TResult"/> after map execution</returns>
		public Optional<TResult> Map<TResult>(Func<T0, TResult> map) =>
			Value is T0 value ? Optional<TResult>.Create(map(value)) : Optional<TResult>.Create(Optional.Empty);

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

	public readonly struct Empty { }

	public static class Optional
	{
		public static readonly Empty Empty;

		/// <summary>
		/// Create an Optional using a value
		/// </summary>
		/// <typeparam name="T0">Type of Optional result</typeparam>
		/// <param name="value">Source value</param>
		/// <returns>A new Optional object, if value is null returns Optional empty</returns>
		public static Optional<T0> Of<T0>(T0 value) => value == null ? new(Empty) : new(value);
	}

}
