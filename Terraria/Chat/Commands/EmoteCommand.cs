// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.EmoteCommand
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
  [ChatCommand("Emote")]
  public class EmoteCommand : IChatCommand
  {
    private static readonly Color RESPONSE_COLOR = new Color(200, 100, 0);

    public void ProcessMessage(string text, byte clientId)
    {
      if (!(text != ""))
        return;
      text = string.Format("*{0} {1}", (object) Main.player[(int) clientId].name, (object) text);
      NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), EmoteCommand.RESPONSE_COLOR, -1);
    }
  }
}
