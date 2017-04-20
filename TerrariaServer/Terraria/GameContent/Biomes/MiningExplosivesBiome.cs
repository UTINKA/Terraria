// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.MiningExplosivesBiome
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.World.Generation;

namespace Terraria.GameContent.Biomes
{
  public class MiningExplosivesBiome : MicroBiome
  {
    public override bool Place(Point origin, StructureMap structures)
    {
      if (WorldGen.SolidTile((int) origin.X, (int) origin.Y))
        return false;
      ushort type = Utils.SelectRandom<ushort>(GenBase._random, new ushort[4]
      {
        WorldGen.goldBar == 19 ? (ushort) 8 : (ushort) 169,
        WorldGen.silverBar == 21 ? (ushort) 9 : (ushort) 168,
        WorldGen.ironBar == 22 ? (ushort) 6 : (ushort) 167,
        WorldGen.copperBar == 20 ? (ushort) 7 : (ushort) 166
      });
      double num1 = GenBase._random.NextDouble() * 2.0 - 1.0;
      if (!WorldUtils.Find(origin, Searches.Chain(num1 > 0.0 ? (GenSearch) new Searches.Right(40) : (GenSearch) new Searches.Left(40), (GenCondition) new Conditions.IsSolid()), out origin))
        return false;
      if (!WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Down(80), (GenCondition) new Conditions.IsSolid()), out origin))
        return false;
      ShapeData shapeData = new ShapeData();
      Ref<int> count1 = new Ref<int>(0);
      Ref<int> count2 = new Ref<int>(0);
      WorldUtils.Gen(origin, new ShapeRunner(10f, 20, new Vector2((float) num1, 1f)).Output(shapeData), Actions.Chain((GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Actions.Scanner(count1), (GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.Scanner(count2)));
      if (count2.Value < count1.Value / 2)
        return false;
      Rectangle area;
      // ISSUE: explicit reference operation
      ((Rectangle) @area).\u002Ector(origin.X - 15, origin.Y - 10, 30, 20);
      if (!structures.CanPlace(area, 0))
        return false;
      WorldUtils.Gen(origin, (GenShape) new ModShapes.All(shapeData), (GenAction) new Actions.SetTile(type, true, true));
      WorldUtils.Gen(new Point(origin.X - (int) (num1 * -5.0), origin.Y - 5), (GenShape) new Shapes.Circle(5), Actions.Chain((GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Actions.ClearTile(true)));
      Point result1;
      bool flag = ((true ? 1 : 0) & (WorldUtils.Find(new Point(origin.X - (num1 > 0.0 ? 3 : -3), origin.Y - 3), Searches.Chain((GenSearch) new Searches.Down(10), (GenCondition) new Conditions.IsSolid()), out result1) ? 1 : 0)) != 0;
      int num2 = GenBase._random.Next(4) == 0 ? 3 : 7;
      Point result2;
      if (((flag ? 1 : 0) & (WorldUtils.Find(new Point(origin.X - (num1 > 0.0 ? -num2 : num2), origin.Y - 3), Searches.Chain((GenSearch) new Searches.Down(10), (GenCondition) new Conditions.IsSolid()), out result2) ? 1 : 0)) == 0)
        return false;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Point& local1 = @result1;
      // ISSUE: explicit reference operation
      int num3 = (^local1).Y - 1;
      // ISSUE: explicit reference operation
      (^local1).Y = (__Null) num3;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Point& local2 = @result2;
      // ISSUE: explicit reference operation
      int num4 = (^local2).Y - 1;
      // ISSUE: explicit reference operation
      (^local2).Y = (__Null) num4;
      Tile tile1 = GenBase._tiles[(int) result1.X, result1.Y + 1];
      tile1.slope((byte) 0);
      tile1.halfBrick(false);
      for (int index = -1; index <= 1; ++index)
      {
        WorldUtils.ClearTile(result2.X + index, (int) result2.Y, false);
        Tile tile2 = GenBase._tiles[result2.X + index, result2.Y + 1];
        if (!WorldGen.SolidOrSlopedTile(tile2))
        {
          tile2.ResetToType((ushort) 1);
          tile2.active(true);
        }
        tile2.slope((byte) 0);
        tile2.halfBrick(false);
        WorldUtils.TileFrame(result2.X + index, result2.Y + 1, true);
      }
      WorldGen.PlaceTile((int) result1.X, (int) result1.Y, 141, false, false, -1, 0);
      WorldGen.PlaceTile((int) result2.X, (int) result2.Y, 411, true, true, -1, 0);
      WorldUtils.WireLine(result1, result2);
      structures.AddStructure(area, 5);
      return true;
    }
  }
}
