using System;

namespace UnionOf;

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnionOfAttribute : Attribute
{
	public UnionOfAttribute()
	{
		UnionTypes = [];
	}

	public UnionOfAttribute(params Type[] types)
	{
		UnionTypes = types;
	}

	public Type[] UnionTypes { get; }
}
