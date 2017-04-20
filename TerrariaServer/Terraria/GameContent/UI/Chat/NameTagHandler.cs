// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Chat.NameTagHandler
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
  public class NameTagHandler : ITagHandler
  {
    TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
    {
      return new TextSnippet("<" + text.Replace("\\[", "[").Replace("\\]", "]") + ">", baseColor, 1f);
    }

    public static string GenerateTag(string name)
    {
      return "[n:" + name.Replace("[", "\\[").Replace("]", "\\]") + "]";
    }
  }
}
