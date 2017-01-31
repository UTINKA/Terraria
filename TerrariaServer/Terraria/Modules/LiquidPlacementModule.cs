// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.LiquidPlacementModule
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Terraria.Enums;

namespace Terraria.Modules
{
  public class LiquidPlacementModule
  {
    public LiquidPlacement water;
    public LiquidPlacement lava;

    public LiquidPlacementModule(LiquidPlacementModule copyFrom = null)
    {
      if (copyFrom == null)
      {
        this.water = LiquidPlacement.Allowed;
        this.lava = LiquidPlacement.Allowed;
      }
      else
      {
        this.water = copyFrom.water;
        this.lava = copyFrom.lava;
      }
    }
  }
}
