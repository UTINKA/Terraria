// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.SayChatCommand
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using System;
using Terraria.GameContent.NetModules;
using Terraria.Localization;
using Terraria.Net;

namespace Terraria.Chat.Commands
{
  [ChatCommand("Say")]
  public class SayChatCommand : IChatCommand
  {
    public void ProcessMessage(string text, byte clientId)
    {
      NetPacket packet = NetTextModule.SerializeServerMessage(NetworkText.FromLiteral(text), Main.player[(int) clientId].ChatColor(), clientId);
      NetManager.Instance.Broadcast(packet, -1);
      Console.WriteLine("<{0}> {1}", (object) Main.player[(int) clientId].name, (object) text);
    }
  }
}
