// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.CaveHouseBiome
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.World.Generation;

namespace Terraria.GameContent.Biomes
{
  public class CaveHouseBiome : MicroBiome
  {
    private static readonly bool[] _blacklistedTiles = TileID.Sets.Factory.CreateBoolSet(true, 225, 41, 43, 44, 226, 203, 112, 25, 151);
    private const int VERTICAL_EXIT_WIDTH = 3;
    private int _sharpenerCount;
    private int _extractinatorCount;

    private Microsoft.Xna.Framework.Rectangle GetRoom(Point origin)
    {
      Point result1;
      bool flag1 = WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Left(25), (GenCondition) new Conditions.IsSolid()), out result1);
      Point result2;
      bool flag2 = WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Right(25), (GenCondition) new Conditions.IsSolid()), out result2);
      if (!flag1)
        result1 = new Point(origin.X - 25, origin.Y);
      if (!flag2)
        result2 = new Point(origin.X + 25, origin.Y);
      Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(origin.X, origin.Y, 0, 0);
      if (origin.X - result1.X > result2.X - origin.X)
      {
        rectangle.X = result1.X;
        rectangle.Width = Utils.Clamp<int>(result2.X - result1.X, 15, 30);
      }
      else
      {
        rectangle.Width = Utils.Clamp<int>(result2.X - result1.X, 15, 30);
        rectangle.X = result2.X - rectangle.Width;
      }
      Point result3;
      bool flag3 = WorldUtils.Find(result1, Searches.Chain((GenSearch) new Searches.Up(10), (GenCondition) new Conditions.IsSolid()), out result3);
      Point result4;
      bool flag4 = WorldUtils.Find(result2, Searches.Chain((GenSearch) new Searches.Up(10), (GenCondition) new Conditions.IsSolid()), out result4);
      if (!flag3)
        result3 = new Point(origin.X, origin.Y - 10);
      if (!flag4)
        result4 = new Point(origin.X, origin.Y - 10);
      rectangle.Height = Utils.Clamp<int>(Math.Max(origin.Y - result3.Y, origin.Y - result4.Y), 8, 12);
      rectangle.Y -= rectangle.Height;
      return rectangle;
    }

    private float RoomSolidPrecentage(Microsoft.Xna.Framework.Rectangle room)
    {
      float num = (float) (room.Width * room.Height);
      Ref<int> count = new Ref<int>(0);
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.Count(count)));
      return (float) count.Value / num;
    }

    private bool FindVerticalExit(Microsoft.Xna.Framework.Rectangle wall, bool isUp, out int exitX)
    {
      Point result;
      bool flag = WorldUtils.Find(new Point(wall.X + wall.Width - 3, wall.Y + (isUp ? -5 : 0)), Searches.Chain((GenSearch) new Searches.Left(wall.Width - 3), new Conditions.IsSolid().Not().AreaOr(3, 5)), out result);
      exitX = result.X;
      return flag;
    }

    private bool FindSideExit(Microsoft.Xna.Framework.Rectangle wall, bool isLeft, out int exitY)
    {
      Point result;
      bool flag = WorldUtils.Find(new Point(wall.X + (isLeft ? -4 : 0), wall.Y + wall.Height - 3), Searches.Chain((GenSearch) new Searches.Up(wall.Height - 3), new Conditions.IsSolid().Not().AreaOr(4, 3)), out result);
      exitY = result.Y;
      return flag;
    }

    private int SortBiomeResults(Tuple<CaveHouseBiome.BuildData, int> item1, Tuple<CaveHouseBiome.BuildData, int> item2)
    {
      return item2.Item2.CompareTo(item1.Item2);
    }

    public override bool Place(Point origin, StructureMap structures)
    {
      Point result1;
      if (!WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Down(200), (GenCondition) new Conditions.IsSolid()), out result1) || result1 == origin)
        return false;
      Microsoft.Xna.Framework.Rectangle room1 = this.GetRoom(result1);
      Microsoft.Xna.Framework.Rectangle room2 = this.GetRoom(new Point(room1.Center.X, room1.Y + 1));
      Microsoft.Xna.Framework.Rectangle room3 = this.GetRoom(new Point(room1.Center.X, room1.Y + room1.Height + 10));
      room3.Y = room1.Y + room1.Height - 1;
      float num1 = this.RoomSolidPrecentage(room2);
      float num2 = this.RoomSolidPrecentage(room3);
      room1.Y += 3;
      room2.Y += 3;
      room3.Y += 3;
      List<Microsoft.Xna.Framework.Rectangle> rectangleList1 = new List<Microsoft.Xna.Framework.Rectangle>();
      if ((double) GenBase._random.NextFloat() > (double) num1 + 0.200000002980232)
        rectangleList1.Add(room2);
      else
        room2 = room1;
      rectangleList1.Add(room1);
      if ((double) GenBase._random.NextFloat() > (double) num2 + 0.200000002980232)
        rectangleList1.Add(room3);
      else
        room3 = room1;
      foreach (Microsoft.Xna.Framework.Rectangle rectangle in rectangleList1)
      {
        if (rectangle.Y + rectangle.Height > Main.maxTilesY - 220)
          return false;
      }
      Dictionary<ushort, int> resultsOutput = new Dictionary<ushort, int>();
      foreach (Microsoft.Xna.Framework.Rectangle rectangle in rectangleList1)
        WorldUtils.Gen(new Point(rectangle.X - 10, rectangle.Y - 10), (GenShape) new Shapes.Rectangle(rectangle.Width + 20, rectangle.Height + 20), (GenAction) new Actions.TileScanner(new ushort[12]
        {
          (ushort) 0,
          (ushort) 59,
          (ushort) 147,
          (ushort) 1,
          (ushort) 161,
          (ushort) 53,
          (ushort) 396,
          (ushort) 397,
          (ushort) 368,
          (ushort) 367,
          (ushort) 60,
          (ushort) 70
        }).Output(resultsOutput));
      List<Tuple<CaveHouseBiome.BuildData, int>> tupleList1 = new List<Tuple<CaveHouseBiome.BuildData, int>>();
      tupleList1.Add(Tuple.Create<CaveHouseBiome.BuildData, int>(CaveHouseBiome.BuildData.Default, resultsOutput[(ushort) 0] + resultsOutput[(ushort) 1]));
      tupleList1.Add(Tuple.Create<CaveHouseBiome.BuildData, int>(CaveHouseBiome.BuildData.Jungle, resultsOutput[(ushort) 59] + resultsOutput[(ushort) 60] * 10));
      tupleList1.Add(Tuple.Create<CaveHouseBiome.BuildData, int>(CaveHouseBiome.BuildData.Mushroom, resultsOutput[(ushort) 59] + resultsOutput[(ushort) 70] * 10));
      tupleList1.Add(Tuple.Create<CaveHouseBiome.BuildData, int>(CaveHouseBiome.BuildData.Snow, resultsOutput[(ushort) 147] + resultsOutput[(ushort) 161]));
      tupleList1.Add(Tuple.Create<CaveHouseBiome.BuildData, int>(CaveHouseBiome.BuildData.Desert, resultsOutput[(ushort) 397] + resultsOutput[(ushort) 396] + resultsOutput[(ushort) 53]));
      tupleList1.Add(Tuple.Create<CaveHouseBiome.BuildData, int>(CaveHouseBiome.BuildData.Granite, resultsOutput[(ushort) 368]));
      tupleList1.Add(Tuple.Create<CaveHouseBiome.BuildData, int>(CaveHouseBiome.BuildData.Marble, resultsOutput[(ushort) 367]));
      tupleList1.Sort(new Comparison<Tuple<CaveHouseBiome.BuildData, int>>(this.SortBiomeResults));
      CaveHouseBiome.BuildData buildData = tupleList1[0].Item1;
      foreach (Microsoft.Xna.Framework.Rectangle area in rectangleList1)
      {
        if (buildData != CaveHouseBiome.BuildData.Granite)
        {
          Point result2;
          if (WorldUtils.Find(new Point(area.X - 2, area.Y - 2), Searches.Chain(new Searches.Rectangle(area.Width + 4, area.Height + 4).RequireAll(false), (GenCondition) new Conditions.HasLava()), out result2))
            return false;
        }
        if (!structures.CanPlace(area, CaveHouseBiome._blacklistedTiles, 5))
          return false;
      }
      int val1_1 = room1.X;
      int val1_2 = room1.X + room1.Width - 1;
      List<Microsoft.Xna.Framework.Rectangle> rectangleList2 = new List<Microsoft.Xna.Framework.Rectangle>();
      foreach (Microsoft.Xna.Framework.Rectangle rectangle in rectangleList1)
      {
        val1_1 = Math.Min(val1_1, rectangle.X);
        val1_2 = Math.Max(val1_2, rectangle.X + rectangle.Width - 1);
      }
      int num3 = 6;
      while (num3 > 4 && (val1_2 - val1_1) % num3 != 0)
        --num3;
      int x1 = val1_1;
      while (x1 <= val1_2)
      {
        for (int index1 = 0; index1 < rectangleList1.Count; ++index1)
        {
          Microsoft.Xna.Framework.Rectangle rectangle = rectangleList1[index1];
          if (x1 >= rectangle.X && x1 < rectangle.X + rectangle.Width)
          {
            int y = rectangle.Y + rectangle.Height;
            int num4 = 50;
            for (int index2 = index1 + 1; index2 < rectangleList1.Count; ++index2)
            {
              if (x1 >= rectangleList1[index2].X && x1 < rectangleList1[index2].X + rectangleList1[index2].Width)
                num4 = Math.Min(num4, rectangleList1[index2].Y - y);
            }
            if (num4 > 0)
            {
              Point result2;
              bool flag = WorldUtils.Find(new Point(x1, y), Searches.Chain((GenSearch) new Searches.Down(num4), (GenCondition) new Conditions.IsSolid()), out result2);
              if (num4 < 50)
              {
                flag = true;
                result2 = new Point(x1, y + num4);
              }
              if (flag)
                rectangleList2.Add(new Microsoft.Xna.Framework.Rectangle(x1, y, 1, result2.Y - y));
            }
          }
        }
        x1 += num3;
      }
      List<Point> pointList1 = new List<Point>();
      foreach (Microsoft.Xna.Framework.Rectangle rectangle in rectangleList1)
      {
        int exitY;
        if (this.FindSideExit(new Microsoft.Xna.Framework.Rectangle(rectangle.X + rectangle.Width, rectangle.Y + 1, 1, rectangle.Height - 2), false, out exitY))
          pointList1.Add(new Point(rectangle.X + rectangle.Width - 1, exitY));
        if (this.FindSideExit(new Microsoft.Xna.Framework.Rectangle(rectangle.X, rectangle.Y + 1, 1, rectangle.Height - 2), true, out exitY))
          pointList1.Add(new Point(rectangle.X, exitY));
      }
      List<Tuple<Point, Point>> tupleList2 = new List<Tuple<Point, Point>>();
      for (int index = 1; index < rectangleList1.Count; ++index)
      {
        Microsoft.Xna.Framework.Rectangle rectangle1 = rectangleList1[index];
        Microsoft.Xna.Framework.Rectangle rectangle2 = rectangleList1[index - 1];
        if (rectangle2.X - rectangle1.X > rectangle1.X + rectangle1.Width - (rectangle2.X + rectangle2.Width))
          tupleList2.Add(new Tuple<Point, Point>(new Point(rectangle1.X + rectangle1.Width - 1, rectangle1.Y + 1), new Point(rectangle1.X + rectangle1.Width - rectangle1.Height + 1, rectangle1.Y + rectangle1.Height - 1)));
        else
          tupleList2.Add(new Tuple<Point, Point>(new Point(rectangle1.X, rectangle1.Y + 1), new Point(rectangle1.X + rectangle1.Height - 1, rectangle1.Y + rectangle1.Height - 1)));
      }
      List<Point> pointList2 = new List<Point>();
      int exitX;
      if (this.FindVerticalExit(new Microsoft.Xna.Framework.Rectangle(room2.X + 2, room2.Y, room2.Width - 4, 1), true, out exitX))
        pointList2.Add(new Point(exitX, room2.Y));
      if (this.FindVerticalExit(new Microsoft.Xna.Framework.Rectangle(room3.X + 2, room3.Y + room3.Height - 1, room3.Width - 4, 1), false, out exitX))
        pointList2.Add(new Point(exitX, room3.Y + room3.Height - 1));
      foreach (Microsoft.Xna.Framework.Rectangle area in rectangleList1)
      {
        WorldUtils.Gen(new Point(area.X, area.Y), (GenShape) new Shapes.Rectangle(area.Width, area.Height), Actions.Chain((GenAction) new Actions.SetTile(buildData.Tile, false, true), (GenAction) new Actions.SetFrames(true)));
        WorldUtils.Gen(new Point(area.X + 1, area.Y + 1), (GenShape) new Shapes.Rectangle(area.Width - 2, area.Height - 2), Actions.Chain((GenAction) new Actions.ClearTile(true), (GenAction) new Actions.PlaceWall(buildData.Wall, true)));
        structures.AddStructure(area, 8);
      }
      foreach (Tuple<Point, Point> tuple in tupleList2)
      {
        Point origin1 = tuple.Item1;
        Point point = tuple.Item2;
        int num4 = point.X > origin1.X ? 1 : -1;
        ShapeData data = new ShapeData();
        for (int y = 0; y < point.Y - origin1.Y; ++y)
          data.Add(num4 * (y + 1), y);
        WorldUtils.Gen(origin1, (GenShape) new ModShapes.All(data), Actions.Chain((GenAction) new Actions.PlaceTile((ushort) 19, buildData.PlatformStyle), (GenAction) new Actions.SetSlope(num4 == 1 ? 1 : 2), (GenAction) new Actions.SetFrames(true)));
        WorldUtils.Gen(new Point(origin1.X + (num4 == 1 ? 1 : -4), origin1.Y - 1), (GenShape) new Shapes.Rectangle(4, 1), Actions.Chain((GenAction) new Actions.Clear(), (GenAction) new Actions.PlaceWall(buildData.Wall, true), (GenAction) new Actions.PlaceTile((ushort) 19, buildData.PlatformStyle), (GenAction) new Actions.SetFrames(true)));
      }
      foreach (Point origin1 in pointList1)
      {
        WorldUtils.Gen(origin1, (GenShape) new Shapes.Rectangle(1, 3), (GenAction) new Actions.ClearTile(true));
        WorldGen.PlaceTile(origin1.X, origin1.Y, 10, true, true, -1, buildData.DoorStyle);
      }
      foreach (Point origin1 in pointList2)
      {
        Shapes.Rectangle rectangle = new Shapes.Rectangle(3, 1);
        GenAction action = Actions.Chain((GenAction) new Actions.ClearMetadata(), (GenAction) new Actions.PlaceTile((ushort) 19, buildData.PlatformStyle), (GenAction) new Actions.SetFrames(true));
        WorldUtils.Gen(origin1, (GenShape) rectangle, action);
      }
      foreach (Microsoft.Xna.Framework.Rectangle rectangle in rectangleList2)
      {
        if (rectangle.Height > 1 && (int) GenBase._tiles[rectangle.X, rectangle.Y - 1].type != 19)
        {
          WorldUtils.Gen(new Point(rectangle.X, rectangle.Y), (GenShape) new Shapes.Rectangle(rectangle.Width, rectangle.Height), Actions.Chain((GenAction) new Actions.SetTile((ushort) 124, false, true), (GenAction) new Actions.SetFrames(true)));
          Tile tile = GenBase._tiles[rectangle.X, rectangle.Y + rectangle.Height];
          tile.slope((byte) 0);
          tile.halfBrick(false);
        }
      }
      Point[] pointArray = new Point[7]
      {
        new Point(14, buildData.TableStyle),
        new Point(16, 0),
        new Point(18, buildData.WorkbenchStyle),
        new Point(86, 0),
        new Point(87, buildData.PianoStyle),
        new Point(94, 0),
        new Point(101, buildData.BookcaseStyle)
      };
      foreach (Microsoft.Xna.Framework.Rectangle rectangle in rectangleList1)
      {
        int num4 = rectangle.Width / 8;
        int num5 = rectangle.Width / (num4 + 1);
        int num6 = GenBase._random.Next(2);
        for (int index1 = 0; index1 < num4; ++index1)
        {
          int num7 = (index1 + 1) * num5 + rectangle.X;
          switch (index1 + num6 % 2)
          {
            case 0:
              int num8 = rectangle.Y + Math.Min(rectangle.Height / 2, rectangle.Height - 5);
              Vector2 vector2 = WorldGen.randHousePicture();
              int x2 = (int) vector2.X;
              int y = (int) vector2.Y;
              if (!WorldGen.nearPicture(num7, num8))
              {
                WorldGen.PlaceTile(num7, num8, x2, true, false, -1, y);
                break;
              }
              break;
            case 1:
              int j = rectangle.Y + 1;
              WorldGen.PlaceTile(num7, j, 34, true, false, -1, GenBase._random.Next(6));
              for (int index2 = -1; index2 < 2; ++index2)
              {
                for (int index3 = 0; index3 < 3; ++index3)
                  GenBase._tiles[index2 + num7, index3 + j].frameX += (short) 54;
              }
              break;
          }
        }
        int num9 = rectangle.Width / 8 + 3;
        WorldGen.SetupStatueList();
        for (; num9 > 0; --num9)
        {
          int num7 = GenBase._random.Next(rectangle.Width - 3) + 1 + rectangle.X;
          int num8 = rectangle.Y + rectangle.Height - 2;
          switch (GenBase._random.Next(4))
          {
            case 0:
              WorldGen.PlaceSmallPile(num7, num8, GenBase._random.Next(31, 34), 1, (ushort) 185);
              break;
            case 1:
              WorldGen.PlaceTile(num7, num8, 186, true, false, -1, GenBase._random.Next(22, 26));
              break;
            case 2:
              int index = GenBase._random.Next(2, WorldGen.statueList.Length);
              WorldGen.PlaceTile(num7, num8, (int) WorldGen.statueList[index].X, true, false, -1, (int) WorldGen.statueList[index].Y);
              if (WorldGen.StatuesWithTraps.Contains(index))
              {
                WorldGen.PlaceStatueTrap(num7, num8);
                break;
              }
              break;
            case 3:
              Point point = Utils.SelectRandom<Point>(GenBase._random, pointArray);
              WorldGen.PlaceTile(num7, num8, point.X, true, false, -1, point.Y);
              break;
          }
        }
      }
      foreach (Microsoft.Xna.Framework.Rectangle room4 in rectangleList1)
        buildData.ProcessRoom(room4);
      bool flag1 = false;
      foreach (Microsoft.Xna.Framework.Rectangle rectangle in rectangleList1)
      {
        int j = rectangle.Height - 1 + rectangle.Y;
        int Style = j > (int) Main.worldSurface ? buildData.ChestStyle : 0;
        int num4 = 0;
        while (num4 < 10 && !(flag1 = WorldGen.AddBuriedChest(GenBase._random.Next(2, rectangle.Width - 2) + rectangle.X, j, 0, false, Style)))
          ++num4;
        if (!flag1)
        {
          int i = rectangle.X + 2;
          while (i <= rectangle.X + rectangle.Width - 2 && !(flag1 = WorldGen.AddBuriedChest(i, j, 0, false, Style)))
            ++i;
          if (flag1)
            break;
        }
        else
          break;
      }
      if (!flag1)
      {
        foreach (Microsoft.Xna.Framework.Rectangle rectangle in rectangleList1)
        {
          int j = rectangle.Y - 1;
          int Style = j > (int) Main.worldSurface ? buildData.ChestStyle : 0;
          int num4 = 0;
          while (num4 < 10 && !(flag1 = WorldGen.AddBuriedChest(GenBase._random.Next(2, rectangle.Width - 2) + rectangle.X, j, 0, false, Style)))
            ++num4;
          if (!flag1)
          {
            int i = rectangle.X + 2;
            while (i <= rectangle.X + rectangle.Width - 2 && !(flag1 = WorldGen.AddBuriedChest(i, j, 0, false, Style)))
              ++i;
            if (flag1)
              break;
          }
          else
            break;
        }
      }
      if (!flag1)
      {
        for (int index = 0; index < 1000; ++index)
        {
          int i = GenBase._random.Next(rectangleList1[0].X - 30, rectangleList1[0].X + 30);
          int j = GenBase._random.Next(rectangleList1[0].Y - 30, rectangleList1[0].Y + 30);
          int Style = j > (int) Main.worldSurface ? buildData.ChestStyle : 0;
          if (WorldGen.AddBuriedChest(i, j, 0, false, Style))
            break;
        }
      }
      if (buildData == CaveHouseBiome.BuildData.Jungle && this._sharpenerCount < GenBase._random.Next(2, 5))
      {
        bool flag2 = false;
        foreach (Microsoft.Xna.Framework.Rectangle rectangle in rectangleList1)
        {
          int j = rectangle.Height - 2 + rectangle.Y;
          for (int index = 0; index < 10; ++index)
          {
            int i = GenBase._random.Next(2, rectangle.Width - 2) + rectangle.X;
            WorldGen.PlaceTile(i, j, 377, true, true, -1, 0);
            if (flag2 = GenBase._tiles[i, j].active() && (int) GenBase._tiles[i, j].type == 377)
              break;
          }
          if (!flag2)
          {
            int i = rectangle.X + 2;
            while (i <= rectangle.X + rectangle.Width - 2 && !(flag2 = WorldGen.PlaceTile(i, j, 377, true, true, -1, 0)))
              ++i;
            if (flag2)
              break;
          }
          else
            break;
        }
        if (flag2)
          ++this._sharpenerCount;
      }
      if (buildData == CaveHouseBiome.BuildData.Desert && this._extractinatorCount < GenBase._random.Next(2, 5))
      {
        bool flag2 = false;
        foreach (Microsoft.Xna.Framework.Rectangle rectangle in rectangleList1)
        {
          int j = rectangle.Height - 2 + rectangle.Y;
          for (int index = 0; index < 10; ++index)
          {
            int i = GenBase._random.Next(2, rectangle.Width - 2) + rectangle.X;
            WorldGen.PlaceTile(i, j, 219, true, true, -1, 0);
            if (flag2 = GenBase._tiles[i, j].active() && (int) GenBase._tiles[i, j].type == 219)
              break;
          }
          if (!flag2)
          {
            int i = rectangle.X + 2;
            while (i <= rectangle.X + rectangle.Width - 2 && !(flag2 = WorldGen.PlaceTile(i, j, 219, true, true, -1, 0)))
              ++i;
            if (flag2)
              break;
          }
          else
            break;
        }
        if (flag2)
          ++this._extractinatorCount;
      }
      return true;
    }

    public override void Reset()
    {
      this._sharpenerCount = 0;
      this._extractinatorCount = 0;
    }

    internal static void AgeDefaultRoom(Microsoft.Xna.Framework.Rectangle room)
    {
      for (int index = 0; index < room.Width * room.Height / 16; ++index)
        WorldUtils.Gen(new Point(GenBase._random.Next(1, room.Width - 1) + room.X, GenBase._random.Next(1, room.Height - 1) + room.Y), (GenShape) new Shapes.Rectangle(2, 2), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.Blotches(2, 2.0), (GenAction) new Modifiers.IsEmpty(), (GenAction) new Actions.SetTile((ushort) 51, true, true)));
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Modifiers.OnlyWalls(new byte[1]
      {
        CaveHouseBiome.BuildData.Default.Wall
      }), (double) room.Y > Main.worldSurface ? (GenAction) new Actions.ClearWall(true) : (GenAction) new Actions.PlaceWall((byte) 2, true)));
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.949999988079071), (GenAction) new Modifiers.OnlyTiles(new ushort[3]
      {
        (ushort) 30,
        (ushort) 321,
        (ushort) 158
      }), (GenAction) new Actions.ClearTile(true)));
    }

    internal static void AgeSnowRoom(Microsoft.Xna.Framework.Rectangle room)
    {
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.Blotches(2, 0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        CaveHouseBiome.BuildData.Snow.Tile
      }), (GenAction) new Actions.SetTile((ushort) 161, true, true), (GenAction) new Modifiers.Dither(0.8), (GenAction) new Actions.SetTile((ushort) 147, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 161
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 161
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.8), (double) room.Y > Main.worldSurface ? (GenAction) new Actions.ClearWall(true) : (GenAction) new Actions.PlaceWall((byte) 40, true)));
    }

    internal static void AgeDesertRoom(Microsoft.Xna.Framework.Rectangle room)
    {
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Modifiers.Blotches(2, 0.200000002980232), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        CaveHouseBiome.BuildData.Desert.Tile
      }), (GenAction) new Actions.SetTile((ushort) 396, true, true), (GenAction) new Modifiers.Dither(0.5), (GenAction) new Actions.SetTile((ushort) 397, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[2]
      {
        (ushort) 397,
        (ushort) 396
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[2]
      {
        (ushort) 397,
        (ushort) 396
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Modifiers.OnlyWalls(new byte[1]
      {
        CaveHouseBiome.BuildData.Desert.Wall
      }), (GenAction) new Actions.PlaceWall((byte) 216, true)));
    }

    internal static void AgeGraniteRoom(Microsoft.Xna.Framework.Rectangle room)
    {
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.Blotches(2, 0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        CaveHouseBiome.BuildData.Granite.Tile
      }), (GenAction) new Actions.SetTile((ushort) 368, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 368
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 368
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Actions.PlaceWall((byte) 180, true)));
    }

    internal static void AgeMarbleRoom(Microsoft.Xna.Framework.Rectangle room)
    {
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.Blotches(2, 0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        CaveHouseBiome.BuildData.Marble.Tile
      }), (GenAction) new Actions.SetTile((ushort) 367, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 367
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 367
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Actions.PlaceWall((byte) 178, true)));
    }

    internal static void AgeMushroomRoom(Microsoft.Xna.Framework.Rectangle room)
    {
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.699999988079071), (GenAction) new Modifiers.Blotches(2, 0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        CaveHouseBiome.BuildData.Mushroom.Tile
      }), (GenAction) new Actions.SetTile((ushort) 70, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 70
      }), (GenAction) new Modifiers.Offset(0, -1), (GenAction) new Actions.SetTile((ushort) 71, false, true)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 70
      }), (GenAction) new Modifiers.Offset(0, -1), (GenAction) new Actions.SetTile((ushort) 71, false, true)));
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Actions.ClearWall(false)));
    }

    internal static void AgeJungleRoom(Microsoft.Xna.Framework.Rectangle room)
    {
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.Blotches(2, 0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        CaveHouseBiome.BuildData.Jungle.Tile
      }), (GenAction) new Actions.SetTile((ushort) 60, true, true), (GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Actions.SetTile((ushort) 59, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 60
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionVines(3, room.Height, 62)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 60
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionVines(3, room.Height, 62)));
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Actions.PlaceWall((byte) 64, true)));
    }

    private class BuildData
    {
      public static CaveHouseBiome.BuildData Snow = CaveHouseBiome.BuildData.CreateSnowData();
      public static CaveHouseBiome.BuildData Jungle = CaveHouseBiome.BuildData.CreateJungleData();
      public static CaveHouseBiome.BuildData Default = CaveHouseBiome.BuildData.CreateDefaultData();
      public static CaveHouseBiome.BuildData Granite = CaveHouseBiome.BuildData.CreateGraniteData();
      public static CaveHouseBiome.BuildData Marble = CaveHouseBiome.BuildData.CreateMarbleData();
      public static CaveHouseBiome.BuildData Mushroom = CaveHouseBiome.BuildData.CreateMushroomData();
      public static CaveHouseBiome.BuildData Desert = CaveHouseBiome.BuildData.CreateDesertData();
      public ushort Tile;
      public byte Wall;
      public int PlatformStyle;
      public int DoorStyle;
      public int TableStyle;
      public int WorkbenchStyle;
      public int PianoStyle;
      public int BookcaseStyle;
      public int ChairStyle;
      public int ChestStyle;
      public CaveHouseBiome.BuildData.ProcessRoomMethod ProcessRoom;

      public static CaveHouseBiome.BuildData CreateSnowData()
      {
        return new CaveHouseBiome.BuildData()
        {
          Tile = 321,
          Wall = 149,
          DoorStyle = 30,
          PlatformStyle = 19,
          TableStyle = 28,
          WorkbenchStyle = 23,
          PianoStyle = 23,
          BookcaseStyle = 25,
          ChairStyle = 30,
          ChestStyle = 11,
          ProcessRoom = new CaveHouseBiome.BuildData.ProcessRoomMethod(CaveHouseBiome.AgeSnowRoom)
        };
      }

      public static CaveHouseBiome.BuildData CreateDesertData()
      {
        return new CaveHouseBiome.BuildData()
        {
          Tile = 396,
          Wall = 187,
          PlatformStyle = 0,
          DoorStyle = 0,
          TableStyle = 0,
          WorkbenchStyle = 0,
          PianoStyle = 0,
          BookcaseStyle = 0,
          ChairStyle = 0,
          ChestStyle = 1,
          ProcessRoom = new CaveHouseBiome.BuildData.ProcessRoomMethod(CaveHouseBiome.AgeDesertRoom)
        };
      }

      public static CaveHouseBiome.BuildData CreateJungleData()
      {
        return new CaveHouseBiome.BuildData()
        {
          Tile = 158,
          Wall = 42,
          PlatformStyle = 2,
          DoorStyle = 2,
          TableStyle = 2,
          WorkbenchStyle = 2,
          PianoStyle = 2,
          BookcaseStyle = 12,
          ChairStyle = 3,
          ChestStyle = 8,
          ProcessRoom = new CaveHouseBiome.BuildData.ProcessRoomMethod(CaveHouseBiome.AgeJungleRoom)
        };
      }

      public static CaveHouseBiome.BuildData CreateGraniteData()
      {
        return new CaveHouseBiome.BuildData()
        {
          Tile = 369,
          Wall = 181,
          PlatformStyle = 28,
          DoorStyle = 34,
          TableStyle = 33,
          WorkbenchStyle = 29,
          PianoStyle = 28,
          BookcaseStyle = 30,
          ChairStyle = 34,
          ChestStyle = 50,
          ProcessRoom = new CaveHouseBiome.BuildData.ProcessRoomMethod(CaveHouseBiome.AgeGraniteRoom)
        };
      }

      public static CaveHouseBiome.BuildData CreateMarbleData()
      {
        return new CaveHouseBiome.BuildData()
        {
          Tile = 357,
          Wall = 179,
          PlatformStyle = 29,
          DoorStyle = 35,
          TableStyle = 34,
          WorkbenchStyle = 30,
          PianoStyle = 29,
          BookcaseStyle = 31,
          ChairStyle = 35,
          ChestStyle = 51,
          ProcessRoom = new CaveHouseBiome.BuildData.ProcessRoomMethod(CaveHouseBiome.AgeMarbleRoom)
        };
      }

      public static CaveHouseBiome.BuildData CreateMushroomData()
      {
        return new CaveHouseBiome.BuildData()
        {
          Tile = 190,
          Wall = 74,
          PlatformStyle = 18,
          DoorStyle = 6,
          TableStyle = 27,
          WorkbenchStyle = 7,
          PianoStyle = 22,
          BookcaseStyle = 24,
          ChairStyle = 9,
          ChestStyle = 32,
          ProcessRoom = new CaveHouseBiome.BuildData.ProcessRoomMethod(CaveHouseBiome.AgeMushroomRoom)
        };
      }

      public static CaveHouseBiome.BuildData CreateDefaultData()
      {
        return new CaveHouseBiome.BuildData()
        {
          Tile = 30,
          Wall = 27,
          PlatformStyle = 0,
          DoorStyle = 0,
          TableStyle = 0,
          WorkbenchStyle = 0,
          PianoStyle = 0,
          BookcaseStyle = 0,
          ChairStyle = 0,
          ChestStyle = 1,
          ProcessRoom = new CaveHouseBiome.BuildData.ProcessRoomMethod(CaveHouseBiome.AgeDefaultRoom)
        };
      }

      public delegate void ProcessRoomMethod(Microsoft.Xna.Framework.Rectangle room);
    }
  }
}
