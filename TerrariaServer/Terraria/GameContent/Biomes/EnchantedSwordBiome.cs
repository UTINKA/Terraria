// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.EnchantedSwordBiome
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.World.Generation;

namespace Terraria.GameContent.Biomes
{
  public class EnchantedSwordBiome : MicroBiome
  {
    public override bool Place(Point origin, StructureMap structures)
    {
      Dictionary<ushort, int> resultsOutput = new Dictionary<ushort, int>();
      WorldUtils.Gen(new Point(origin.X - 25, origin.Y - 25), (GenShape) new Shapes.Rectangle(50, 50), (GenAction) new Actions.TileScanner(new ushort[2]
      {
        (ushort) 0,
        (ushort) 1
      }).Output(resultsOutput));
      if (resultsOutput[(ushort) 0] + resultsOutput[(ushort) 1] < 1250)
        return false;
      Point result1;
      bool flag = WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Up(1000), new Conditions.IsSolid().AreaOr(1, 50).Not()), out result1);
      Point result2;
      if (WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Up(origin.Y - result1.Y), (GenCondition) new Conditions.IsTile(new ushort[1]
      {
        (ushort) 53
      })), out result2) || !flag)
        return false;
      result1.Y += 50;
      ShapeData data1 = new ShapeData();
      ShapeData shapeData = new ShapeData();
      Point point1 = new Point(origin.X, origin.Y + 20);
      Point point2 = new Point(origin.X, origin.Y + 30);
      float xScale = (float) (0.800000011920929 + (double) GenBase._random.NextFloat() * 0.5);
      if (!structures.CanPlace(new Microsoft.Xna.Framework.Rectangle(point1.X - (int) (20.0 * (double) xScale), point1.Y - 20, (int) (40.0 * (double) xScale), 40), 0) || !structures.CanPlace(new Microsoft.Xna.Framework.Rectangle(origin.X, result1.Y + 10, 1, origin.Y - result1.Y - 9), 2))
        return false;
      WorldUtils.Gen(point1, (GenShape) new Shapes.Slime(20, xScale, 1f), Actions.Chain((GenAction) new Modifiers.Blotches(2, 0.4), new Actions.ClearTile(true).Output(data1)));
      WorldUtils.Gen(point2, (GenShape) new Shapes.Mound(14, 14), Actions.Chain((GenAction) new Modifiers.Blotches(2, 1, 0.8), (GenAction) new Actions.SetTile((ushort) 0, false, true), new Actions.SetFrames(true).Output(shapeData)));
      data1.Subtract(shapeData, point1, point2);
      WorldUtils.Gen(point1, (GenShape) new ModShapes.InnerOutline(data1, true), Actions.Chain((GenAction) new Actions.SetTile((ushort) 2, false, true), (GenAction) new Actions.SetFrames(true)));
      WorldUtils.Gen(point1, (GenShape) new ModShapes.All(data1), Actions.Chain((GenAction) new Modifiers.RectangleMask(-40, 40, 0, 40), (GenAction) new Modifiers.IsEmpty(), (GenAction) new Actions.SetLiquid(0, byte.MaxValue)));
      WorldUtils.Gen(point1, (GenShape) new ModShapes.All(data1), Actions.Chain((GenAction) new Actions.PlaceWall((byte) 68, true), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 2
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionVines(3, 5, 52)));
      ShapeData data2 = new ShapeData();
      WorldUtils.Gen(new Point(origin.X, result1.Y + 10), (GenShape) new Shapes.Rectangle(1, origin.Y - result1.Y - 9), Actions.Chain((GenAction) new Modifiers.Blotches(2, 0.2), new Actions.ClearTile(false).Output(data2), (GenAction) new Modifiers.Expand(1), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 53
      }), new Actions.SetTile((ushort) 397, false, true).Output(data2)));
      WorldUtils.Gen(new Point(origin.X, result1.Y + 10), (GenShape) new ModShapes.All(data2), (GenAction) new Actions.SetFrames(true));
      if (GenBase._random.Next(3) == 0)
        WorldGen.PlaceTile(point2.X, point2.Y - 15, 187, true, false, -1, 17);
      else
        WorldGen.PlaceTile(point2.X, point2.Y - 15, 186, true, false, -1, 15);
      WorldUtils.Gen(point2, (GenShape) new ModShapes.All(shapeData), Actions.Chain((GenAction) new Modifiers.Offset(0, -1), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 2
      }), (GenAction) new Modifiers.Offset(0, -1), (GenAction) new ActionGrass()));
      structures.AddStructure(new Microsoft.Xna.Framework.Rectangle(point1.X - (int) (20.0 * (double) xScale), point1.Y - 20, (int) (40.0 * (double) xScale), 40), 4);
      return true;
    }
  }
}
