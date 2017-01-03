// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.TileObjectStyleModule
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

namespace Terraria.Modules
{
  public class TileObjectStyleModule
  {
    public int style;
    public bool horizontal;
    public int styleWrapLimit;
    public int styleMultiplier;

    public TileObjectStyleModule(TileObjectStyleModule copyFrom = null)
    {
      if (copyFrom == null)
      {
        this.style = 0;
        this.horizontal = false;
        this.styleWrapLimit = 0;
        this.styleMultiplier = 1;
      }
      else
      {
        this.style = copyFrom.style;
        this.horizontal = copyFrom.horizontal;
        this.styleWrapLimit = copyFrom.styleWrapLimit;
        this.styleMultiplier = copyFrom.styleMultiplier;
      }
    }
  }
}
