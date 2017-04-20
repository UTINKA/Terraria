// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.StructureMap
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;

namespace Terraria.World.Generation
{
  public class StructureMap
  {
    private List<Rectangle> _structures = new List<Rectangle>(2048);

    public bool CanPlace(Rectangle area, int padding = 0)
    {
      return this.CanPlace(area, TileID.Sets.GeneralPlacementTiles, padding);
    }

    public bool CanPlace(Rectangle area, bool[] validTiles, int padding = 0)
    {
      if (area.X < 0 || area.Y < 0 || (area.X + area.Width > Main.maxTilesX - 1 || area.Y + area.Height > Main.maxTilesY - 1))
        return false;
      Rectangle rectangle;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle).\u002Ector(area.X - padding, area.Y - padding, area.Width + padding * 2, area.Height + padding * 2);
      for (int index = 0; index < this._structures.Count; ++index)
      {
        // ISSUE: explicit reference operation
        if (((Rectangle) @rectangle).Intersects(this._structures[index]))
          return false;
      }
      for (int x = (int) rectangle.X; x < rectangle.X + rectangle.Width; ++x)
      {
        for (int y = (int) rectangle.Y; y < rectangle.Y + rectangle.Height; ++y)
        {
          if (Main.tile[x, y].active())
          {
            ushort type = Main.tile[x, y].type;
            if (!validTiles[(int) type])
              return false;
          }
        }
      }
      return true;
    }

    public void AddStructure(Rectangle area, int padding = 0)
    {
      Rectangle rectangle;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle).\u002Ector(area.X - padding, area.Y - padding, area.Width + padding * 2, area.Height + padding * 2);
      this._structures.Add(rectangle);
    }

    public void Reset()
    {
      this._structures.Clear();
    }
  }
}
