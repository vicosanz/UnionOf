using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace UnionOf.Generator
{
	/// <param name="Namespace"> The namespace found in the base struct </param>
	/// <param name="Usings"> Usings of the base struct </param>
	/// <param name="AllowNulls"> Struct allow nulls </param>
	/// <param name="Name"> The short name of the base struct </param>
	/// <param name="NameTyped"> The shot name with typed parameters e.g. <T0, T1>.
	/// <param name="FullName"> The full name of the base struct </param>
	/// <param name="Modifiers"> All modifiers of the base struct e.g. public readonly </param>
	/// <param name="Types"> All types of the unionof configured using [UnionOf(typeof(type1), typeof(type2)...] or [UnionOf] + inherits IUnionOf<type1, type2...></type1> </param>
	public record UnionOfMetadata(string Namespace, IReadOnlyList<string> Usings, 
		bool AllowNulls, string Name, string NameTyped, string FullName, string Modifiers, 
		IReadOnlyList<string> Types)
	{
		/// <summary>
		/// The namespace found in the base struct
		/// </summary>
		public string Namespace { get; internal set; } = Namespace;

		/// <summary>
		/// Usings of the base struct
		/// </summary>
		public IReadOnlyList<string> Usings { get; internal set; } = Usings;

		/// <summary>
		/// Struct allow nulls
		/// </summary>
		public bool AllowNulls { get; internal set; } = AllowNulls;

		/// <summary>
		/// The short name of the base struct
		/// </summary>
		public string Name { get; internal set; } = Name;

		/// <summary>
		/// The shot name with typed parameters e.g. <T0, T1>.
		/// </summary>
		public string NameTyped { get; internal set; } = NameTyped;

		/// <summary>
		/// The full name of the base struct
		/// </summary>
		public string FullName { get; internal set; } = FullName;

		/// <summary>
		/// All modifiers of the base struct e.g. public readonly
		/// </summary>
		public string Modifiers { get; internal set; } = Modifiers;

		/// <summary>
		/// All types of the unionof configured using [UnionOf(typeof(type1), typeof(type2)...] or [UnionOf] + inherits IUnionOf<type1, type2...></type1>
		/// </summary>
		public IReadOnlyList<string> Types { get; internal set; } = Types;
	}

}
