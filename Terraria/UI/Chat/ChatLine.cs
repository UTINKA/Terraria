// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Chat.ChatLine
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

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
