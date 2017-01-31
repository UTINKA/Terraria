// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Chat.PlainTagHandler
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
  public class PlainTagHandler : ITagHandler
  {
    TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
    {
      return (TextSnippet) new PlainTagHandler.PlainSnippet(text);
    }

    public class PlainSnippet : TextSnippet
    {
      public PlainSnippet(string text = "")
        : base(text)
      {
      }

      public PlainSnippet(string text, Color color, float scale = 1f)
        : base(text, color, scale)
      {
      }

      public override Color GetVisibleColor()
      {
        return this.Color;
      }
    }
  }
}
