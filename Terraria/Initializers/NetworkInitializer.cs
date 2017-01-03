// Decompiled with JetBrains decompiler
// Type: Terraria.Initializers.NetworkInitializer
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Terraria.GameContent.NetModules;
using Terraria.Net;

namespace Terraria.Initializers
{
  public static class NetworkInitializer
  {
    public static void Load()
    {
      NetManager.Instance.Register<NetLiquidModule>();
    }
  }
}
