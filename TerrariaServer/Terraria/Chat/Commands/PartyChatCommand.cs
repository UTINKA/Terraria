// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.PartyChatCommand
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Terraria.GameContent.NetModules;
using Terraria.Localization;
using Terraria.Net;

namespace Terraria.Chat.Commands
{
  [ChatCommand("Party")]
  public class PartyChatCommand : IChatCommand
  {
    private static readonly Color ERROR_COLOR = new Color((int) byte.MaxValue, 240, 20);

    public void ProcessMessage(string text, byte clientId)
    {
      int team = Main.player[(int) clientId].team;
      Color color = Main.teamColor[team];
      if (team == 0)
      {
        this.SendNoTeamError(clientId);
      }
      else
      {
        if (text == "")
          return;
        for (int playerId = 0; playerId < (int) byte.MaxValue; ++playerId)
        {
          if (Main.player[playerId].team == team)
          {
            NetPacket packet = NetTextModule.SerializeServerMessage(NetworkText.FromLiteral(text), color, clientId);
            NetManager.Instance.SendToClient(packet, playerId);
          }
        }
      }
    }

    private void SendNoTeamError(byte clientId)
    {
      NetPacket packet = NetTextModule.SerializeServerMessage(Lang.mp[10].ToNetworkText(), PartyChatCommand.ERROR_COLOR);
      NetManager.Instance.SendToClient(packet, (int) clientId);
    }
  }
}
