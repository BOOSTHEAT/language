using System;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Model;

public class Urn : IEquatable<Urn>
{
    public const char Separator = ':';
    private readonly string _value;
    private readonly string[] _components;
    private readonly int _hashCode;

    protected Urn(string value)
    {
        _value = value;
        _components = Deconstruct(value);
        _hashCode = _value.ToLower().GetHashCode();
    }

    public bool IsPartOf(Urn other)
    {
        if (other._components.Length <= _components.Length) return false;
        for (var i = 0; i < _components.Length; i++)
        {
            if (other._components[i] != _components[i]) return false;
        }

        return true;
    }
    
    public bool TryRemoveRoot(Urn other, out Urn newUrn)
    {
        if (!other.IsPartOf(this))
        {
            newUrn = null;
            return false;
        }

        var newComponents = _components[other._components.Length..];
        newUrn = BuildUrn(newComponents);
        return true;        
    }
    
    public Urn RemoveRoot(Urn other)
    {
        if(!TryRemoveRoot(other, out var newUrn))
            throw new ArgumentException($"Cannot remove {other} from {this}");
        
        return newUrn;
    }


    public string Value => _value;

    public static implicit operator String(Urn urn) => urn._value;
    public static implicit operator Urn(string value) => new (value);

    public static Urn BuildUrn(params string[] components)
    {
        return new Urn(Format(components));
    }

    public static string[] Deconstruct(string urnStr) =>
        urnStr?.Split(Separator);

    internal static string Format(params string[] components) =>
        string.Join(Separator, components);


    public override string ToString()
    {
        return _value;
    }

    public bool Equals(Urn other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ValueEquals(other);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return Equals((Urn) obj);
    }

    public override int GetHashCode()
    {
        return _hashCode;
    }

    public static bool operator ==(Urn left, Urn right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Urn left, Urn right)
    {
        return !Equals(left, right);
    }

    public bool ValueEquals(Urn other)
    {
        return _hashCode == other._hashCode;
    }


}