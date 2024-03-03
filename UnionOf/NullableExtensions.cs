using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace UnionOf
{
	public static class NullableExtensions
	{
		/// <summary>
		/// Evaluate a predicate and returns this Nullable object or default
		/// </summary>
		/// <typeparam name="T0">Nullable type</typeparam>
		/// <param name="source">Source nullable to evaluate</param>
		/// <param name="predicate">Predicate to evaluate</param>
		/// <returns>if true return this object otherwise default</returns>
		public static T0? When<T0>(this T0? source, Func<T0?, bool> predicate) => 
			source != null && predicate(source) ? source : default;

		/// <summary>
		/// Evaluate a negative predicate and returns this Nullable object or default
		/// </summary>
		/// <typeparam name="T0">Nullable type</typeparam>
		/// <param name="source">Source nullable to evaluate</param>
		/// <param name="predicate">Negative predicate to evaluate</param>
		/// <returns>if false return this object otherwise default</returns>
		public static T0? WhenNot<T0>(this T0? source, Func<T0?, bool> predicate) =>
			source != null && !predicate(source) ? source : default;

		/// <summary>
		/// Return inner value or default based in a function
		/// </summary>
		/// <typeparam name="T0">Nullable type</typeparam>
		/// <param name="source">Source nullable to evaluate</param>
		/// <param name="default">Function to handle null</param>
		/// <returns>Inner value or function result</returns>
		public static T0 Reduce<T0>(this T0? source, Func<T0> @default) =>
			source != null ? source : @default();

		/// <summary>
		/// Return inner value or default based in a function
		/// </summary>
		/// <typeparam name="T0">Nullable type</typeparam>
		/// <param name="source">Source nullable to evaluate</param>
		/// <param name="default">Default value to return if source is null</param>
		/// <returns>Inner value or default result</returns>
		public static T0? Reduce<T0>(this T0? source, T0? @default = default) =>
			source != null ? source : @default;

		/// <summary>
		/// Map nullable using a function
		/// </summary>
		/// <typeparam name="T0">Nullable type</typeparam>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="source">Source nullable to evaluate</param>
		/// <param name="map">Map function</param>
		/// <returns>Nullable</returns>
		public static TResult? Map<T0, TResult>(this T0? source, Func<T0?, TResult?> map) =>
			source == null ? default : map(source);

		/// <summary>
		/// Map nullable using a function
		/// </summary>
		/// <typeparam name="T0">Nullable type</typeparam>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="source">Source nullable to evaluate</param>
		/// <param name="map">Map function</param>
		/// <returns>Nullable</returns>
		public static async Task<TResult?> MapAsync<T0, TResult>(this T0? source, Func<T0?, Task<TResult?>> map) =>
			source == null ? default : await map(source);
	}
}
