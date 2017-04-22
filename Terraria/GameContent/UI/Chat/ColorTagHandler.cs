// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Chat.ColorTagHandler
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Globalization;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
  public class ColorTagHandler : ITagHandler
  {
    TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
    {
      TextSnippet textSnippet = new TextSnippet(text);
      int result;
      if (!int.TryParse(options, NumberStyles.AllowHexSpecifier, (IFormatProvider) CultureInfo.InvariantCulture, out result))
        return textSnippet;
      textSnippet.Color = new Color(result >> 16 & (int) byte.MaxValue, result >> 8 & (int) byte.MaxValue, result & (int) byte.MaxValue);
      return textSnippet;
    }
  }
}
