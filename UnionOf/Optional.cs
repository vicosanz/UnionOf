using System;

namespace UnionOf
{
	public interface IOptional
	{
	}

	//Predefined Optional type
	[UnionOf]
	public readonly partial struct Optional<T0> : IUnionOf<T0, Empty>, IOptional
	{
		public Optional<T0> When(Func<T0, bool> predicate) =>
			Value is T0 value && predicate(value) ? this : new(Optional.Empty());

		public Optional<T0> WhenNot(Func<T0, bool> predicate) =>
			Value is T0 value && !predicate(value) ? this : new(Optional.Empty());

		public Optional<TResult> Map<TResult>(Func<T0, TResult> map) =>
			Value is T0 value ? Optional<TResult>.Create(map(value)) : Optional<TResult>.Create(Optional.Empty());

		public T0 Reduce(T0 @default) => Value is T0 value ? value : @default;
		public T0 Reduce(Func<T0> @default) => Value is T0 value ? value : @default();
	}

	public record Empty();

	public static class Optional
	{
		public static Empty Empty() => new();

		public static Optional<T0> Of<T0>(T0 value) => value == null ? new(Empty()) : new(value);
	}

}
