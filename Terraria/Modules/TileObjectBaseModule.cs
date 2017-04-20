// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.TileObjectBaseModule
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Terraria.DataStructures;
using Terraria.Enums;

namespace Terraria.Modules
{
  public class TileObjectBaseModule
  {
    public int width;
    public int height;
    public Point16 origin;
    public TileObjectDirection direction;
    public int randomRange;
    public bool flattenAnchors;

    public TileObjectBaseModule(TileObjectBaseModule copyFrom = null)
    {
      if (copyFrom == null)
      {
        this.width = 1;
        this.height = 1;
        this.origin = Point16.Zero;
        this.direction = TileObjectDirection.None;
        this.randomRange = 0;
        this.flattenAnchors = false;
      }
      else
      {
        this.width = copyFrom.width;
        this.height = copyFrom.height;
        this.origin = copyFrom.origin;
        this.direction = copyFrom.direction;
        this.randomRange = copyFrom.randomRange;
        this.flattenAnchors = copyFrom.flattenAnchors;
      }
    }
  }
}
