// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.LiquidDeathModule
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

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
