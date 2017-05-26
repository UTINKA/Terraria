// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Chat.ITagHandler
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.UI.Chat
{
  public interface ITagHandler
  {
    TextSnippet Parse(string text, Color baseColor = null, string options = null);
  }
}
