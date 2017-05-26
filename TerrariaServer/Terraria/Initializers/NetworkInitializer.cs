// Decompiled with JetBrains decompiler
// Type: Terraria.Initializers.NetworkInitializer
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Terraria.GameContent.NetModules;
using Terraria.Net;

namespace Terraria.Initializers
{
  public static class NetworkInitializer
  {
    public static void Load()
    {
      NetManager.Instance.Register<NetLiquidModule>();
      NetManager.Instance.Register<NetTextModule>();
    }
  }
}
