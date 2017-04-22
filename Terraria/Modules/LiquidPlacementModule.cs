// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.LiquidPlacementModule
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

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
