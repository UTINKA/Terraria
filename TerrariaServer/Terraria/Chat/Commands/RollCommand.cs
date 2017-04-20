// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.RollCommand
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
  [ChatCommand("Roll")]
  public class RollCommand : IChatCommand
  {
    private static readonly Color RESPONSE_COLOR = new Color((int) byte.MaxValue, 240, 20);

    public string InternalName
    {
      get
      {
        return "roll";
      }
    }

    public void ProcessMessage(string text, byte clientId)
    {
      int num = Main.rand.Next(1, 101);
      NetMessage.BroadcastChatMessage(NetworkText.FromFormattable("*{0} {1} {2}", (object) Main.player[(int) clientId].name, (object) Lang.mp[9].ToNetworkText(), (object) num), RollCommand.RESPONSE_COLOR, -1);
    }
  }
}
