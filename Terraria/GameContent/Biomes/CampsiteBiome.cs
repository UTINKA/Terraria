// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.CampsiteBiome
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.World.Generation;

namespace Terraria.GameContent.Biomes
{
  public class CampsiteBiome : MicroBiome
  {
    public override bool Place(Point origin, StructureMap structures)
    {
      Ref<int> count1 = new Ref<int>(0);
      Ref<int> count2 = new Ref<int>(0);
      WorldUtils.Gen(origin, (GenShape) new Shapes.Circle(10), Actions.Chain((GenAction) new Actions.Scanner(count2), (GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.Scanner(count1)));
      if (count1.Value < count2.Value - 5)
        return false;
      int radius = GenBase._random.Next(6, 10);
      int num1 = GenBase._random.Next(5);
      if (!structures.CanPlace(new Microsoft.Xna.Framework.Rectangle(origin.X - radius, origin.Y - radius, radius * 2, radius * 2), 0))
        return false;
      ShapeData data = new ShapeData();
      WorldUtils.Gen(origin, (GenShape) new Shapes.Slime(radius), Actions.Chain(new Modifiers.Blotches(num1, num1, num1, 1, 0.3).Output(data), (GenAction) new Modifiers.Offset(0, -2), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 53
      }), (GenAction) new Actions.SetTile((ushort) 397, true, true), (GenAction) new Modifiers.OnlyWalls(new byte[1]), (GenAction) new Actions.PlaceWall((byte) 16, true)));
      WorldUtils.Gen(origin, (GenShape) new ModShapes.All(data), Actions.Chain((GenAction) new Actions.ClearTile(false), (GenAction) new Actions.SetLiquid(0, (byte) 0), (GenAction) new Actions.SetFrames(true), (GenAction) new Modifiers.OnlyWalls(new byte[1]), (GenAction) new Actions.PlaceWall((byte) 16, true)));
      Point result;
      if (!WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Down(10), (GenCondition) new Conditions.IsSolid()), out result))
        return false;
      int j = result.Y - 1;
      bool flag = GenBase._random.Next() % 2 == 0;
      if (GenBase._random.Next() % 10 != 0)
      {
        int num2 = GenBase._random.Next(1, 4);
        int num3 = flag ? 4 : -(radius >> 1);
        for (int index1 = 0; index1 < num2; ++index1)
        {
          int num4 = GenBase._random.Next(1, 3);
          for (int index2 = 0; index2 < num4; ++index2)
            WorldGen.PlaceTile(origin.X + num3 - index1, j - index2, 331, false, false, -1, 0);
        }
      }
      int num5 = (radius - 3) * (flag ? -1 : 1);
      if (GenBase._random.Next() % 10 != 0)
        WorldGen.PlaceTile(origin.X + num5, j, 186, false, false, -1, 0);
      if (GenBase._random.Next() % 10 != 0)
      {
        WorldGen.PlaceTile(origin.X, j, 215, true, false, -1, 0);
        if (GenBase._tiles[origin.X, j].active() && (int) GenBase._tiles[origin.X, j].type == 215)
        {
          GenBase._tiles[origin.X, j].frameY += (short) 36;
          GenBase._tiles[origin.X - 1, j].frameY += (short) 36;
          GenBase._tiles[origin.X + 1, j].frameY += (short) 36;
          GenBase._tiles[origin.X, j - 1].frameY += (short) 36;
          GenBase._tiles[origin.X - 1, j - 1].frameY += (short) 36;
          GenBase._tiles[origin.X + 1, j - 1].frameY += (short) 36;
        }
      }
      structures.AddStructure(new Microsoft.Xna.Framework.Rectangle(origin.X - radius, origin.Y - radius, radius * 2, radius * 2), 4);
      return true;
    }
  }
}
