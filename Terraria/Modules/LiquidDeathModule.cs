// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.LiquidDeathModule
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
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
