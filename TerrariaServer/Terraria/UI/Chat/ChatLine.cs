// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Chat.ChatLine
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;

namespace Terraria.UI.Chat
{
  public class ChatLine
  {
    public Color color = Color.White;
    public string text = "";
    public TextSnippet[] parsedText = new TextSnippet[0];
    public int showTime;
  }
}
