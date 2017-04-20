// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.TileObjectCoordinatesModule
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System;
using Terraria.DataStructures;

namespace Terraria.Modules
{
  public class TileObjectCoordinatesModule
  {
    public int width;
    public int[] heights;
    public int padding;
    public Point16 paddingFix;
    public int styleWidth;
    public int styleHeight;
    public bool calculated;

    public TileObjectCoordinatesModule(TileObjectCoordinatesModule copyFrom = null, int[] drawHeight = null)
    {
      if (copyFrom == null)
      {
        this.width = 0;
        this.padding = 0;
        this.paddingFix = Point16.Zero;
        this.styleWidth = 0;
        this.styleHeight = 0;
        this.calculated = false;
        this.heights = drawHeight;
      }
      else
      {
        this.width = copyFrom.width;
        this.padding = copyFrom.padding;
        this.paddingFix = copyFrom.paddingFix;
        this.styleWidth = copyFrom.styleWidth;
        this.styleHeight = copyFrom.styleHeight;
        this.calculated = copyFrom.calculated;
        if (drawHeight == null)
        {
          if (copyFrom.heights == null)
          {
            this.heights = (int[]) null;
          }
          else
          {
            this.heights = new int[copyFrom.heights.Length];
            Array.Copy((Array) copyFrom.heights, (Array) this.heights, this.heights.Length);
          }
        }
        else
          this.heights = drawHeight;
      }
    }
  }
}
