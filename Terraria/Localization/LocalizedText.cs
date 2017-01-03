// Decompiled with JetBrains decompiler
// Type: Terraria.Localization.LocalizedText
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Terraria.Localization
{
  public class LocalizedText
  {
    private static Regex _substitutionRegex = new Regex("{(\\?(?:!)?)?([a-zA-Z][\\w\\.]*)}", RegexOptions.Compiled);
    private string _value = "";

    public string Value
    {
      get
      {
        return this._value;
      }
    }

    internal LocalizedText(string text)
    {
      this._value = text;
    }

    internal void SetValue(string text)
    {
      this._value = text;
    }

    public string FormatWith(object obj)
    {
      string input = this._value;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
      return LocalizedText._substitutionRegex.Replace(input, (MatchEvaluator) (match =>
      {
        if (match.Groups[1].Length != 0)
          return "";
        PropertyDescriptor propertyDescriptor = properties.Find(match.Groups[2].ToString(), false);
        if (propertyDescriptor == null)
          return "";
        return (propertyDescriptor.GetValue(obj) ?? (object) "").ToString();
      }));
    }

    public bool CanFormatWith(object obj)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
      foreach (Match match in LocalizedText._substitutionRegex.Matches(this._value))
      {
        string name = match.Groups[2].ToString();
        PropertyDescriptor propertyDescriptor = properties.Find(name, false);
        if (propertyDescriptor == null)
          return false;
        object obj1 = propertyDescriptor.GetValue(obj);
        if (obj1 == null)
          return false;
        if (match.Groups[1].Length != 0)
        {
          bool? nullable = obj1 as bool?;
          if (((nullable.HasValue ? (nullable.GetValueOrDefault() ? 1 : 0) : 0) ^ (match.Groups[1].Length == 1 ? 1 : 0)) != 0)
            return false;
        }
      }
      return true;
    }

    public string Format(object arg0)
    {
      return string.Format(this._value, arg0);
    }

    public string Format(object arg0, object arg1)
    {
      return string.Format(this._value, arg0, arg1);
    }

    public string Format(object arg0, object arg1, object arg2)
    {
      return string.Format(this._value, arg0, arg1, arg2);
    }

    public string Format(params object[] args)
    {
      return string.Format(this._value, args);
    }

    public override string ToString()
    {
      return this._value;
    }
  }
}
