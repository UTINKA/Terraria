// Decompiled with JetBrains decompiler
// Type: Terraria.Localization.LocalizedText
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Terraria.Localization
{
  public class LocalizedText
  {
    public static readonly LocalizedText Empty = new LocalizedText("", "");
    private static Regex _substitutionRegex = new Regex("{(\\?(?:!)?)?([a-zA-Z][\\w\\.]*)}", RegexOptions.Compiled);
    public readonly string Key;

    public string Value { get; private set; }

    internal LocalizedText(string key, string text)
    {
      this.Key = key;
      this.Value = text;
    }

    public static explicit operator string(LocalizedText text)
    {
      return text.Value;
    }

    internal void SetValue(string text)
    {
      this.Value = text;
    }

    public string FormatWith(object obj)
    {
      string input = this.Value;
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
      foreach (Match match in LocalizedText._substitutionRegex.Matches(this.Value))
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

    public NetworkText ToNetworkText()
    {
      return NetworkText.FromKey(this.Key);
    }

    public NetworkText ToNetworkText(params object[] substitutions)
    {
      return NetworkText.FromKey(this.Key, substitutions);
    }

    public string Format(object arg0)
    {
      return string.Format(this.Value, arg0);
    }

    public string Format(object arg0, object arg1)
    {
      return string.Format(this.Value, arg0, arg1);
    }

    public string Format(object arg0, object arg1, object arg2)
    {
      return string.Format(this.Value, arg0, arg1, arg2);
    }

    public string Format(params object[] args)
    {
      return string.Format(this.Value, args);
    }

    public override string ToString()
    {
      return this.Value;
    }
  }
}
