// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Chat.NameTagHandler
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

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
