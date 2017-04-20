// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.EmoteCommand
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

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
