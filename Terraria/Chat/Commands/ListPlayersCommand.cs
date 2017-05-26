// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.ListPlayersCommand
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
  [ChatCommand("Playing")]
  public class ListPlayersCommand : IChatCommand
  {
    private static readonly Color RESPONSE_COLOR = new Color((int) byte.MaxValue, 240, 20);

    public void ProcessMessage(string text, byte clientId)
    {
      NetMessage.SendChatMessageToClient(NetworkText.FromLiteral(string.Join(", ", ((IEnumerable<Player>) Main.player).Where<Player>((Func<Player, bool>) (player => player.active)).Select<Player, string>((Func<Player, string>) (player => player.name)))), ListPlayersCommand.RESPONSE_COLOR, (int) clientId);
    }
  }
}
