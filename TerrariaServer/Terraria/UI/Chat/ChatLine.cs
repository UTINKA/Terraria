// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Chat.ChatLine
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;

namespace Terraria.UI.Chat
{
  public class ChatLine
  {
    public Color color = Color.get_White();
    public string text = "";
    public TextSnippet[] parsedText = new TextSnippet[0];
    public int showTime;
  }
}
