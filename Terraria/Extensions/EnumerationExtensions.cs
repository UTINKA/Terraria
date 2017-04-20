// Decompiled with JetBrains decompiler
// Type: Extensions.EnumerationExtensions
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using System;

namespace Extensions
{
  public static class EnumerationExtensions
  {
    public static T Include<T>(this Enum value, T append)
    {
      Type type = value.GetType();
      object obj1 = (object) value;
      EnumerationExtensions._Value obj2 = new EnumerationExtensions._Value((object) append, type);
      if (obj2.Signed.HasValue)
        obj1 = (object) (Convert.ToInt64((object) value) | obj2.Signed.Value);
      else if (obj2.Unsigned.HasValue)
        obj1 = (object) (ulong) ((long) Convert.ToUInt64((object) value) | (long) obj2.Unsigned.Value);
      return (T) Enum.Parse(type, obj1.ToString());
    }

    public static T Remove<T>(this Enum value, T remove)
    {
      Type type = value.GetType();
      object obj1 = (object) value;
      EnumerationExtensions._Value obj2 = new EnumerationExtensions._Value((object) remove, type);
      if (obj2.Signed.HasValue)
        obj1 = (object) (Convert.ToInt64((object) value) & ~obj2.Signed.Value);
      else if (obj2.Unsigned.HasValue)
        obj1 = (object) (ulong) ((long) Convert.ToUInt64((object) value) & ~(long) obj2.Unsigned.Value);
      return (T) Enum.Parse(type, obj1.ToString());
    }

    public static bool Has<T>(this Enum value, T check)
    {
      Type type = value.GetType();
      EnumerationExtensions._Value obj = new EnumerationExtensions._Value((object) check, type);
      if (obj.Signed.HasValue)
        return (Convert.ToInt64((object) value) & obj.Signed.Value) == obj.Signed.Value;
      if (obj.Unsigned.HasValue)
        return ((long) Convert.ToUInt64((object) value) & (long) obj.Unsigned.Value) == (long) obj.Unsigned.Value;
      return false;
    }

    public static bool Missing<T>(this Enum obj, T value)
    {
      return !obj.Has<T>(value);
    }

    private class _Value
    {
      private static Type _UInt64 = typeof (ulong);
      private static Type _UInt32 = typeof (long);
      public long? Signed;
      public ulong? Unsigned;

      public _Value(object value, Type type)
      {
        if (!type.IsEnum)
          throw new ArgumentException("Value provided is not an enumerated type!");
        Type underlyingType = Enum.GetUnderlyingType(type);
        if (underlyingType.Equals(EnumerationExtensions._Value._UInt32) || underlyingType.Equals(EnumerationExtensions._Value._UInt64))
          this.Unsigned = new ulong?(Convert.ToUInt64(value));
        else
          this.Signed = new long?(Convert.ToInt64(value));
      }
    }
  }
}
