// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.LiquidDeathModule
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

namespace Terraria.Modules
{
  public class LiquidDeathModule
  {
    public bool water;
    public bool lava;

    public LiquidDeathModule(LiquidDeathModule copyFrom = null)
    {
      if (copyFrom == null)
      {
        this.water = false;
        this.lava = false;
      }
      else
      {
        this.water = copyFrom.water;
        this.lava = copyFrom.lava;
      }
    }
  }
}
