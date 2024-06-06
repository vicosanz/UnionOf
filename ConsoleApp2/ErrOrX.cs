using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UnionOf;

namespace ConsoleApp2
{
    public readonly partial struct ErrOrX<T0> 
    {
        public T0 ValueT0 
        { 
            get => Value is T0 value ? value : default;
            init {
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

        public void Is(out T0 value) => value = ValueT0;
        public void Is(out Exception value) => value = Error;
    }

    public readonly partial struct ErrOrX<T0> : IEquatable<ErrOrX<T0>>
    {
        private readonly object _value;
        [JsonIgnore]
        public object Value
        {
            get => _value;
            init => _value = value ?? ParseNull();
        }

        [Obsolete("Only for serializer", true)]
        public ErrOrX() { }
        //public ErrOrX() => Value = null;

        public object ParseNull() => throw new InvalidCastException("Type not allowed");



        public ErrOrX(T0 value) => Value = value;

        public static ErrOrX<T0> Create(T0 value) => new(value);

        public static implicit operator ErrOrX<T0>(T0 value) => Create(value);

        public static explicit operator T0(ErrOrX<T0> source) => source.Value is T0 value ? value : throw new InvalidCastException();




        public ErrOrX(Exception value) => Value = value;

        public static ErrOrX<T0> Create(Exception value) => new(value);

        public static implicit operator ErrOrX<T0>(Exception value) => Create(value);

        public static explicit operator Exception(ErrOrX<T0> source) => source.Value is Exception value ? value : throw new InvalidCastException();


        public static bool operator ==(ErrOrX<T0> left, ErrOrX<T0> right) => left.Equals(right);
        public static bool operator !=(ErrOrX<T0> left, ErrOrX<T0> right) => !left.Equals(right);

        public bool Equals(ErrOrX<T0> other)
        {
            return _value is not null ? other.Value is not null && _value.Equals(other.Value) : other.Value is null;
        }

        public override bool Equals(object obj) => obj is not null && obj is ErrOrX<T0> o && Equals(o);

        public override int GetHashCode() => _value.GetHashCode();
        public override string ToString() => _value.ToString();
    }

}
