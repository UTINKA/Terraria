// Decompiled with JetBrains decompiler
// Type: Terraria.Initializers.NetworkInitializer
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

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
