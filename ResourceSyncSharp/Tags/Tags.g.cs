// NB: generated file do not modify!
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using JetBrains.Annotations;
namespace ResourceSyncSharp.Tags
{
	// ReSharper disable once ClassCanBeSealed.Global
    // ReSharper disable once PartialTypeWithSinglePart
	[Serializable]  
    [TypeConverter(typeof(JsonStringConverter))]     [ExcludeFromCodeCoverage]  
	public partial class JsonString : IEquatable<JsonString>, IComparable<JsonString>, IComparable
	{ 
        [PublicAPI] public static implicit operator JsonString (string val) { return new JsonString(val); }
		[PublicAPI] public string Value { [Pure] get; private set; }
		[PublicAPI, DebuggerStepThrough] public JsonString (string val) 
        {
                        Value = val; 
        }
		[PublicAPI, Pure] public static implicit operator string(JsonString container) {  return container == null ? null : container.Value;  }
		[PublicAPI, Pure] public override string ToString() { return Convert.ToString(Value); }
		[PublicAPI, Pure] public bool Equals(JsonString other)
	    {
	        if (ReferenceEquals(null, other))
	            return false;
	        if (ReferenceEquals(this, other))
	            return true;
	        var result = string.Equals(Value, other.Value);
            return result;
	    }
	    [PublicAPI, Pure] public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj))
	            return false;
	        if (ReferenceEquals(this, obj))
	            return true;
	        var result = obj is JsonString && Equals((JsonString) obj);
            return result;
	    }
	    [PublicAPI, Pure] public override int GetHashCode()
	    {
	        return  (Value != null ? Value.GetHashCode() : 0);
 	    }
	    [PublicAPI, Pure] public static bool operator ==(JsonString left, JsonString right)
	    {
	        return Equals(left, right);
	    }
	    [PublicAPI, Pure] public static bool operator !=(JsonString left, JsonString right)
	    {
	        return !Equals(left, right);
	    }  
	    [PublicAPI] public static JsonString operator +(JsonString left, string right)
	    {
			return new JsonString(left.Value + right);
	    }
        [PublicAPI] public int CompareTo(JsonString other)
        {
            // ReSharper disable once StringCompareToIsCultureSpecific
            return Value.CompareTo(other.Value);
        }
        [PublicAPI] public int CompareTo(object obj)
        {
            var other = obj as JsonString;
            // ReSharper disable once StringCompareToIsCultureSpecific
            return other == null ? 0 : Value.CompareTo(other.Value);
        }
	}

  
    // type converter for ease of webapi routing
    [ExcludeFromCodeCoverage]  
    class JsonStringConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, 
            CultureInfo culture, object value)
        {
            if (value is string)
            {
                return new JsonString(value as string);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
	// ReSharper disable once ClassCanBeSealed.Global
    // ReSharper disable once PartialTypeWithSinglePart
	[Serializable]  
    [TypeConverter(typeof(XmlStringConverter))]     [ExcludeFromCodeCoverage]  
	public partial class XmlString : IEquatable<XmlString>, IComparable<XmlString>, IComparable
	{ 
        [PublicAPI] public static implicit operator XmlString (string val) { return new XmlString(val); }
		[PublicAPI] public string Value { [Pure] get; private set; }
		[PublicAPI, DebuggerStepThrough] public XmlString (string val) 
        {
                        Value = val; 
        }
		[PublicAPI, Pure] public static implicit operator string(XmlString container) {  return container == null ? null : container.Value;  }
		[PublicAPI, Pure] public override string ToString() { return Convert.ToString(Value); }
		[PublicAPI, Pure] public bool Equals(XmlString other)
	    {
	        if (ReferenceEquals(null, other))
	            return false;
	        if (ReferenceEquals(this, other))
	            return true;
	        var result = string.Equals(Value, other.Value);
            return result;
	    }
	    [PublicAPI, Pure] public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj))
	            return false;
	        if (ReferenceEquals(this, obj))
	            return true;
	        var result = obj is XmlString && Equals((XmlString) obj);
            return result;
	    }
	    [PublicAPI, Pure] public override int GetHashCode()
	    {
	        return  (Value != null ? Value.GetHashCode() : 0);
 	    }
	    [PublicAPI, Pure] public static bool operator ==(XmlString left, XmlString right)
	    {
	        return Equals(left, right);
	    }
	    [PublicAPI, Pure] public static bool operator !=(XmlString left, XmlString right)
	    {
	        return !Equals(left, right);
	    }  
	    [PublicAPI] public static XmlString operator +(XmlString left, string right)
	    {
			return new XmlString(left.Value + right);
	    }
        [PublicAPI] public int CompareTo(XmlString other)
        {
            // ReSharper disable once StringCompareToIsCultureSpecific
            return Value.CompareTo(other.Value);
        }
        [PublicAPI] public int CompareTo(object obj)
        {
            var other = obj as XmlString;
            // ReSharper disable once StringCompareToIsCultureSpecific
            return other == null ? 0 : Value.CompareTo(other.Value);
        }
	}

  
    // type converter for ease of webapi routing
    [ExcludeFromCodeCoverage]  
    class XmlStringConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, 
            CultureInfo culture, object value)
        {
            if (value is string)
            {
                return new XmlString(value as string);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
	// ReSharper disable once ClassCanBeSealed.Global
    // ReSharper disable once PartialTypeWithSinglePart
	[Serializable]  
    [TypeConverter(typeof(ResourceSetConverter))]     [ExcludeFromCodeCoverage]  
	public partial class ResourceSet : IEquatable<ResourceSet>, IComparable<ResourceSet>, IComparable
	{ 
        [PublicAPI] public static implicit operator ResourceSet (string val) { return new ResourceSet(val); }
		[PublicAPI] public string Value { [Pure] get; private set; }
		[PublicAPI, DebuggerStepThrough] public ResourceSet (string val) 
        {
            if (string.IsNullOrWhiteSpace(val)) 
                throw new ArgumentOutOfRangeException();
            Value = val; 
        }
		[PublicAPI, DebuggerStepThrough] public ResourceSet () { }
		[PublicAPI, Pure] public static implicit operator string(ResourceSet container) {  return container == null ? null : container.Value;  }
		[PublicAPI, Pure] public override string ToString() { return Convert.ToString(Value); }
		[PublicAPI, Pure] public bool Equals(ResourceSet other)
	    {
	        if (ReferenceEquals(null, other))
	            return false;
	        if (ReferenceEquals(this, other))
	            return true;
	        var result = string.Equals(Value, other.Value);
            return result;
	    }
	    [PublicAPI, Pure] public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj))
	            return false;
	        if (ReferenceEquals(this, obj))
	            return true;
	        var result = obj is ResourceSet && Equals((ResourceSet) obj);
            return result;
	    }
	    [PublicAPI, Pure] public override int GetHashCode()
	    {
	        return  (Value != null ? Value.GetHashCode() : 0);
 	    }
	    [PublicAPI, Pure] public static bool operator ==(ResourceSet left, ResourceSet right)
	    {
	        return Equals(left, right);
	    }
	    [PublicAPI, Pure] public static bool operator !=(ResourceSet left, ResourceSet right)
	    {
	        return !Equals(left, right);
	    }  
	    [PublicAPI] public static ResourceSet operator +(ResourceSet left, string right)
	    {
			return new ResourceSet(left.Value + right);
	    }
        [PublicAPI] public int CompareTo(ResourceSet other)
        {
            // ReSharper disable once StringCompareToIsCultureSpecific
            return Value.CompareTo(other.Value);
        }
        [PublicAPI] public int CompareTo(object obj)
        {
            var other = obj as ResourceSet;
            // ReSharper disable once StringCompareToIsCultureSpecific
            return other == null ? 0 : Value.CompareTo(other.Value);
        }
	}

  
    // type converter for ease of webapi routing
    [ExcludeFromCodeCoverage]  
    class ResourceSetConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, 
            CultureInfo culture, object value)
        {
            if (value is string)
            {
                return new ResourceSet(value as string);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
	// ReSharper disable once ClassCanBeSealed.Global
    // ReSharper disable once PartialTypeWithSinglePart
	[Serializable]  
    [TypeConverter(typeof(ResourceNameConverter))]     [ExcludeFromCodeCoverage]  
	public partial class ResourceName : IEquatable<ResourceName>, IComparable<ResourceName>, IComparable
	{ 
        [PublicAPI] public static implicit operator ResourceName (string val) { return new ResourceName(val); }
		[PublicAPI] public string Value { [Pure] get; private set; }
		[PublicAPI, DebuggerStepThrough] public ResourceName (string val) 
        {
            if (string.IsNullOrWhiteSpace(val)) 
                throw new ArgumentOutOfRangeException();
            Value = val; 
        }
		[PublicAPI, DebuggerStepThrough] public ResourceName () { }
		[PublicAPI, Pure] public static implicit operator string(ResourceName container) {  return container == null ? null : container.Value;  }
		[PublicAPI, Pure] public override string ToString() { return Convert.ToString(Value); }
		[PublicAPI, Pure] public bool Equals(ResourceName other)
	    {
	        if (ReferenceEquals(null, other))
	            return false;
	        if (ReferenceEquals(this, other))
	            return true;
	        var result = string.Equals(Value, other.Value);
            return result;
	    }
	    [PublicAPI, Pure] public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj))
	            return false;
	        if (ReferenceEquals(this, obj))
	            return true;
	        var result = obj is ResourceName && Equals((ResourceName) obj);
            return result;
	    }
	    [PublicAPI, Pure] public override int GetHashCode()
	    {
	        return  (Value != null ? Value.GetHashCode() : 0);
 	    }
	    [PublicAPI, Pure] public static bool operator ==(ResourceName left, ResourceName right)
	    {
	        return Equals(left, right);
	    }
	    [PublicAPI, Pure] public static bool operator !=(ResourceName left, ResourceName right)
	    {
	        return !Equals(left, right);
	    }  
	    [PublicAPI] public static ResourceName operator +(ResourceName left, string right)
	    {
			return new ResourceName(left.Value + right);
	    }
        [PublicAPI] public int CompareTo(ResourceName other)
        {
            // ReSharper disable once StringCompareToIsCultureSpecific
            return Value.CompareTo(other.Value);
        }
        [PublicAPI] public int CompareTo(object obj)
        {
            var other = obj as ResourceName;
            // ReSharper disable once StringCompareToIsCultureSpecific
            return other == null ? 0 : Value.CompareTo(other.Value);
        }
	}

  
    // type converter for ease of webapi routing
    [ExcludeFromCodeCoverage]  
    class ResourceNameConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, 
            CultureInfo culture, object value)
        {
            if (value is string)
            {
                return new ResourceName(value as string);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
	// ReSharper disable once ClassCanBeSealed.Global
    // ReSharper disable once PartialTypeWithSinglePart
	[Serializable]  
    [TypeConverter(typeof(ResourceValueConverter))]     [ExcludeFromCodeCoverage]  
	public partial class ResourceValue : IEquatable<ResourceValue>, IComparable<ResourceValue>, IComparable
	{ 
        [PublicAPI] public static implicit operator ResourceValue (string val) { return new ResourceValue(val); }
		[PublicAPI] public string Value { [Pure] get; private set; }
		[PublicAPI, DebuggerStepThrough] public ResourceValue (string val) 
        {
                        Value = val; 
        }
		[PublicAPI, Pure] public static implicit operator string(ResourceValue container) {  return container == null ? null : container.Value;  }
		[PublicAPI, Pure] public override string ToString() { return Convert.ToString(Value); }
		[PublicAPI, Pure] public bool Equals(ResourceValue other)
	    {
	        if (ReferenceEquals(null, other))
	            return false;
	        if (ReferenceEquals(this, other))
	            return true;
	        var result = string.Equals(Value, other.Value);
            return result;
	    }
	    [PublicAPI, Pure] public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj))
	            return false;
	        if (ReferenceEquals(this, obj))
	            return true;
	        var result = obj is ResourceValue && Equals((ResourceValue) obj);
            return result;
	    }
	    [PublicAPI, Pure] public override int GetHashCode()
	    {
	        return  (Value != null ? Value.GetHashCode() : 0);
 	    }
	    [PublicAPI, Pure] public static bool operator ==(ResourceValue left, ResourceValue right)
	    {
	        return Equals(left, right);
	    }
	    [PublicAPI, Pure] public static bool operator !=(ResourceValue left, ResourceValue right)
	    {
	        return !Equals(left, right);
	    }  
	    [PublicAPI] public static ResourceValue operator +(ResourceValue left, string right)
	    {
			return new ResourceValue(left.Value + right);
	    }
        [PublicAPI] public int CompareTo(ResourceValue other)
        {
            // ReSharper disable once StringCompareToIsCultureSpecific
            return Value.CompareTo(other.Value);
        }
        [PublicAPI] public int CompareTo(object obj)
        {
            var other = obj as ResourceValue;
            // ReSharper disable once StringCompareToIsCultureSpecific
            return other == null ? 0 : Value.CompareTo(other.Value);
        }
	}

  
    // type converter for ease of webapi routing
    [ExcludeFromCodeCoverage]  
    class ResourceValueConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, 
            CultureInfo culture, object value)
        {
            if (value is string)
            {
                return new ResourceValue(value as string);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
	// ReSharper disable once ClassCanBeSealed.Global
    // ReSharper disable once PartialTypeWithSinglePart
	[Serializable]  
    [TypeConverter(typeof(HRefConverter))]     [ExcludeFromCodeCoverage]  
	public partial class HRef : IEquatable<HRef>, IComparable<HRef>, IComparable
	{ 
        [PublicAPI] public static implicit operator HRef (string val) { return new HRef(val); }
		[PublicAPI] public string Value { [Pure] get; private set; }
		[PublicAPI, DebuggerStepThrough] public HRef (string val) 
        {
                        Value = val; 
        }
		[PublicAPI, Pure] public static implicit operator string(HRef container) {  return container == null ? null : container.Value;  }
		[PublicAPI, Pure] public override string ToString() { return Convert.ToString(Value); }
		[PublicAPI, Pure] public bool Equals(HRef other)
	    {
	        if (ReferenceEquals(null, other))
	            return false;
	        if (ReferenceEquals(this, other))
	            return true;
	        var result = string.Equals(Value, other.Value);
            return result;
	    }
	    [PublicAPI, Pure] public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj))
	            return false;
	        if (ReferenceEquals(this, obj))
	            return true;
	        var result = obj is HRef && Equals((HRef) obj);
            return result;
	    }
	    [PublicAPI, Pure] public override int GetHashCode()
	    {
	        return  (Value != null ? Value.GetHashCode() : 0);
 	    }
	    [PublicAPI, Pure] public static bool operator ==(HRef left, HRef right)
	    {
	        return Equals(left, right);
	    }
	    [PublicAPI, Pure] public static bool operator !=(HRef left, HRef right)
	    {
	        return !Equals(left, right);
	    }  
	    [PublicAPI] public static HRef operator +(HRef left, string right)
	    {
			return new HRef(left.Value + right);
	    }
        [PublicAPI] public int CompareTo(HRef other)
        {
            // ReSharper disable once StringCompareToIsCultureSpecific
            return Value.CompareTo(other.Value);
        }
        [PublicAPI] public int CompareTo(object obj)
        {
            var other = obj as HRef;
            // ReSharper disable once StringCompareToIsCultureSpecific
            return other == null ? 0 : Value.CompareTo(other.Value);
        }
	}

  
    // type converter for ease of webapi routing
    [ExcludeFromCodeCoverage]  
    class HRefConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, 
            CultureInfo culture, object value)
        {
            if (value is string)
            {
                return new HRef(value as string);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
	// ReSharper disable once ClassCanBeSealed.Global
    // ReSharper disable once PartialTypeWithSinglePart
	[Serializable]    [ExcludeFromCodeCoverage]  
	public partial class IsOfInterest : IEquatable<IsOfInterest>, IComparable<IsOfInterest>, IComparable
	{ 
        [PublicAPI] public static implicit operator IsOfInterest (bool val) { return new IsOfInterest(val); }
		[PublicAPI] public bool Value { [Pure] get; private set; }
		[PublicAPI, DebuggerStepThrough] public IsOfInterest (bool val) 
        {
                        Value = val; 
        }
		[PublicAPI, Pure] public static implicit operator bool(IsOfInterest container) {  return container.Value;  }
		[PublicAPI, Pure] public override string ToString() { return Convert.ToString(Value); }
		[PublicAPI, Pure] public bool Equals(IsOfInterest other)
	    {
	        if (ReferenceEquals(null, other))
	            return false;
	        if (ReferenceEquals(this, other))
	            return true;
	        var result = bool.Equals(Value, other.Value);
            return result;
	    }
	    [PublicAPI, Pure] public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj))
	            return false;
	        if (ReferenceEquals(this, obj))
	            return true;
	        var result = obj is IsOfInterest && Equals((IsOfInterest) obj);
            return result;
	    }
	    [PublicAPI, Pure] public override int GetHashCode()
	    {
	        return  Value.GetHashCode(); 	    }
	    [PublicAPI, Pure] public static bool operator ==(IsOfInterest left, IsOfInterest right)
	    {
	        return Equals(left, right);
	    }
	    [PublicAPI, Pure] public static bool operator !=(IsOfInterest left, IsOfInterest right)
	    {
	        return !Equals(left, right);
	    }        [PublicAPI] public int CompareTo(IsOfInterest other)
        {
            // ReSharper disable once StringCompareToIsCultureSpecific
            return Value.CompareTo(other.Value);
        }
        [PublicAPI] public int CompareTo(object obj)
        {
            var other = obj as IsOfInterest;
            // ReSharper disable once StringCompareToIsCultureSpecific
            return other == null ? 0 : Value.CompareTo(other.Value);
        }
	}

	// ReSharper disable once ClassCanBeSealed.Global
    // ReSharper disable once PartialTypeWithSinglePart
	[Serializable]  
    [TypeConverter(typeof(ConnectionStringConverter))]     [ExcludeFromCodeCoverage]  
	public partial class ConnectionString : IEquatable<ConnectionString>, IComparable<ConnectionString>, IComparable
	{ 
        [PublicAPI] public static implicit operator ConnectionString (string val) { return new ConnectionString(val); }
		[PublicAPI] public string Value { [Pure] get; private set; }
		[PublicAPI, DebuggerStepThrough] public ConnectionString (string val) 
        {
            if (string.IsNullOrWhiteSpace(val)) 
                throw new ArgumentOutOfRangeException();
            Value = val; 
        }
		[PublicAPI, DebuggerStepThrough] public ConnectionString () { }
		[PublicAPI, Pure] public static implicit operator string(ConnectionString container) {  return container == null ? null : container.Value;  }
		[PublicAPI, Pure] public override string ToString() { return Convert.ToString(Value); }
		[PublicAPI, Pure] public bool Equals(ConnectionString other)
	    {
	        if (ReferenceEquals(null, other))
	            return false;
	        if (ReferenceEquals(this, other))
	            return true;
	        var result = string.Equals(Value, other.Value);
            return result;
	    }
	    [PublicAPI, Pure] public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj))
	            return false;
	        if (ReferenceEquals(this, obj))
	            return true;
	        var result = obj is ConnectionString && Equals((ConnectionString) obj);
            return result;
	    }
	    [PublicAPI, Pure] public override int GetHashCode()
	    {
	        return  (Value != null ? Value.GetHashCode() : 0);
 	    }
	    [PublicAPI, Pure] public static bool operator ==(ConnectionString left, ConnectionString right)
	    {
	        return Equals(left, right);
	    }
	    [PublicAPI, Pure] public static bool operator !=(ConnectionString left, ConnectionString right)
	    {
	        return !Equals(left, right);
	    }  
	    [PublicAPI] public static ConnectionString operator +(ConnectionString left, string right)
	    {
			return new ConnectionString(left.Value + right);
	    }
        [PublicAPI] public int CompareTo(ConnectionString other)
        {
            // ReSharper disable once StringCompareToIsCultureSpecific
            return Value.CompareTo(other.Value);
        }
        [PublicAPI] public int CompareTo(object obj)
        {
            var other = obj as ConnectionString;
            // ReSharper disable once StringCompareToIsCultureSpecific
            return other == null ? 0 : Value.CompareTo(other.Value);
        }
	}

  
    // type converter for ease of webapi routing
    [ExcludeFromCodeCoverage]  
    class ConnectionStringConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, 
            CultureInfo culture, object value)
        {
            if (value is string)
            {
                return new ConnectionString(value as string);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}