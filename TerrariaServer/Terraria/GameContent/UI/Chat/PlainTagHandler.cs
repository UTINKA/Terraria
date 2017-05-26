// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Chat.PlainTagHandler
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
