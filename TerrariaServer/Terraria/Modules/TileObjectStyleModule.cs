// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.TileObjectStyleModule
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

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
