// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.CaveHouseBiome
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

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

    private Rectangle GetRoom(Point origin)
    {
      Point result1;
      bool flag1 = WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Left(25), (GenCondition) new Conditions.IsSolid()), out result1);
      Point result2;
      bool flag2 = WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Right(25), (GenCondition) new Conditions.IsSolid()), out result2);
      if (!flag1)
        result1 = new Point(origin.X - 25, (int) origin.Y);
      if (!flag2)
        result2 = new Point(origin.X + 25, (int) origin.Y);
      Rectangle rectangle;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle).\u002Ector((int) origin.X, (int) origin.Y, 0, 0);
      if (origin.X - result1.X > result2.X - origin.X)
      {
        rectangle.X = result1.X;
        rectangle.Width = (__Null) Utils.Clamp<int>((int) (result2.X - result1.X), 15, 30);
      }
      else
      {
        rectangle.Width = (__Null) Utils.Clamp<int>((int) (result2.X - result1.X), 15, 30);
        rectangle.X = result2.X - rectangle.Width;
      }
      Point result3;
      bool flag3 = WorldUtils.Find(result1, Searches.Chain((GenSearch) new Searches.Up(10), (GenCondition) new Conditions.IsSolid()), out result3);
      Point result4;
      bool flag4 = WorldUtils.Find(result2, Searches.Chain((GenSearch) new Searches.Up(10), (GenCondition) new Conditions.IsSolid()), out result4);
      if (!flag3)
        result3 = new Point((int) origin.X, origin.Y - 10);
      if (!flag4)
        result4 = new Point((int) origin.X, origin.Y - 10);
      rectangle.Height = (__Null) Utils.Clamp<int>(Math.Max((int) (origin.Y - result3.Y), (int) (origin.Y - result4.Y)), 8, 12);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Rectangle& local1 = @rectangle;
      // ISSUE: explicit reference operation
      // ISSUE: variable of the null type
      __Null local2 = (^local1).Y - rectangle.Height;
      // ISSUE: explicit reference operation
      (^local1).Y = local2;
      return rectangle;
    }

    private float RoomSolidPrecentage(Rectangle room)
    {
      float num = (float) (room.Width * room.Height);
      Ref<int> count = new Ref<int>(0);
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.Count(count)));
      return (float) count.Value / num;
    }

    private bool FindVerticalExit(Rectangle wall, bool isUp, out int exitX)
    {
      Point result;
      bool flag = WorldUtils.Find(new Point(wall.X + wall.Width - 3, wall.Y + (isUp ? -5 : 0)), Searches.Chain((GenSearch) new Searches.Left(wall.Width - 3), new Conditions.IsSolid().Not().AreaOr(3, 5)), out result);
      exitX = (int) result.X;
      return flag;
    }

    private bool FindSideExit(Rectangle wall, bool isLeft, out int exitY)
    {
      Point result;
      bool flag = WorldUtils.Find(new Point(wall.X + (isLeft ? -4 : 0), wall.Y + wall.Height - 3), Searches.Chain((GenSearch) new Searches.Up(wall.Height - 3), new Conditions.IsSolid().Not().AreaOr(4, 3)), out result);
      exitY = (int) result.Y;
      return flag;
    }

    private int SortBiomeResults(Tuple<CaveHouseBiome.BuildData, int> item1, Tuple<CaveHouseBiome.BuildData, int> item2)
    {
      return item2.Item2.CompareTo(item1.Item2);
    }

    public override bool Place(Point origin, StructureMap structures)
    {
      Point result1;
      if (!WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Down(200), (GenCondition) new Conditions.IsSolid()), out result1) || Point.op_Equality(result1, origin))
        return false;
      Rectangle room1 = this.GetRoom(result1);
      // ISSUE: explicit reference operation
      Rectangle room2 = this.GetRoom(new Point((int) ((Rectangle) @room1).get_Center().X, room1.Y + 1));
      // ISSUE: explicit reference operation
      Rectangle room3 = this.GetRoom(new Point((int) ((Rectangle) @room1).get_Center().X, room1.Y + room1.Height + 10));
      room3.Y = (__Null) (room1.Y + room1.Height - 1);
      float num1 = this.RoomSolidPrecentage(room2);
      float num2 = this.RoomSolidPrecentage(room3);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Rectangle& local1 = @room1;
      // ISSUE: explicit reference operation
      int num3 = (^local1).Y + 3;
      // ISSUE: explicit reference operation
      (^local1).Y = (__Null) num3;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Rectangle& local2 = @room2;
      // ISSUE: explicit reference operation
      int num4 = (^local2).Y + 3;
      // ISSUE: explicit reference operation
      (^local2).Y = (__Null) num4;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Rectangle& local3 = @room3;
      // ISSUE: explicit reference operation
      int num5 = (^local3).Y + 3;
      // ISSUE: explicit reference operation
      (^local3).Y = (__Null) num5;
      List<Rectangle> rectangleList1 = new List<Rectangle>();
      if ((double) GenBase._random.NextFloat() > (double) num1 + 0.200000002980232)
        rectangleList1.Add(room2);
      else
        room2 = room1;
      rectangleList1.Add(room1);
      if ((double) GenBase._random.NextFloat() > (double) num2 + 0.200000002980232)
        rectangleList1.Add(room3);
      else
        room3 = room1;
      using (List<Rectangle>.Enumerator enumerator = rectangleList1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Rectangle current = enumerator.Current;
          if (current.Y + current.Height > Main.maxTilesY - 220)
            return false;
        }
      }
      Dictionary<ushort, int> resultsOutput = new Dictionary<ushort, int>();
      using (List<Rectangle>.Enumerator enumerator = rectangleList1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Rectangle current = enumerator.Current;
          WorldUtils.Gen(new Point(current.X - 10, current.Y - 10), (GenShape) new Shapes.Rectangle(current.Width + 20, current.Height + 20), (GenAction) new Actions.TileScanner(new ushort[12]
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
        }
      }
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
      using (List<Rectangle>.Enumerator enumerator = rectangleList1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Rectangle current = enumerator.Current;
          if (buildData != CaveHouseBiome.BuildData.Granite)
          {
            Point result2;
            if (WorldUtils.Find(new Point(current.X - 2, current.Y - 2), Searches.Chain(new Searches.Rectangle(current.Width + 4, current.Height + 4).RequireAll(false), (GenCondition) new Conditions.HasLava()), out result2))
              return false;
          }
          if (!structures.CanPlace(current, CaveHouseBiome._blacklistedTiles, 5))
            return false;
        }
      }
      int val1_1 = (int) room1.X;
      int val1_2 = room1.X + room1.Width - 1;
      List<Rectangle> rectangleList2 = new List<Rectangle>();
      using (List<Rectangle>.Enumerator enumerator = rectangleList1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Rectangle current = enumerator.Current;
          val1_1 = Math.Min(val1_1, (int) current.X);
          val1_2 = Math.Max(val1_2, current.X + current.Width - 1);
        }
      }
      int num6 = 6;
      while (num6 > 4 && (val1_2 - val1_1) % num6 != 0)
        --num6;
      int num7 = val1_1;
      while (num7 <= val1_2)
      {
        for (int index1 = 0; index1 < rectangleList1.Count; ++index1)
        {
          Rectangle rectangle = rectangleList1[index1];
          if (num7 >= rectangle.X && num7 < rectangle.X + rectangle.Width)
          {
            int num8 = (int) (rectangle.Y + rectangle.Height);
            int num9 = 50;
            for (int index2 = index1 + 1; index2 < rectangleList1.Count; ++index2)
            {
              if (num7 >= rectangleList1[index2].X && num7 < rectangleList1[index2].X + rectangleList1[index2].Width)
                num9 = Math.Min(num9, rectangleList1[index2].Y - num8);
            }
            if (num9 > 0)
            {
              Point result2;
              bool flag = WorldUtils.Find(new Point(num7, num8), Searches.Chain((GenSearch) new Searches.Down(num9), (GenCondition) new Conditions.IsSolid()), out result2);
              if (num9 < 50)
              {
                flag = true;
                result2 = new Point(num7, num8 + num9);
              }
              if (flag)
                rectangleList2.Add(new Rectangle(num7, num8, 1, result2.Y - num8));
            }
          }
        }
        num7 += num6;
      }
      List<Point> pointList1 = new List<Point>();
      using (List<Rectangle>.Enumerator enumerator = rectangleList1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Rectangle current = enumerator.Current;
          int exitY;
          if (this.FindSideExit(new Rectangle((int) (current.X + current.Width), current.Y + 1, 1, current.Height - 2), false, out exitY))
            pointList1.Add(new Point(current.X + current.Width - 1, exitY));
          if (this.FindSideExit(new Rectangle((int) current.X, current.Y + 1, 1, current.Height - 2), true, out exitY))
            pointList1.Add(new Point((int) current.X, exitY));
        }
      }
      List<Tuple<Point, Point>> tupleList2 = new List<Tuple<Point, Point>>();
      for (int index = 1; index < rectangleList1.Count; ++index)
      {
        Rectangle rectangle1 = rectangleList1[index];
        Rectangle rectangle2 = rectangleList1[index - 1];
        if ((int) (rectangle2.X - rectangle1.X) > (int) (rectangle1.X + rectangle1.Width - (rectangle2.X + rectangle2.Width)))
          tupleList2.Add(new Tuple<Point, Point>(new Point(rectangle1.X + rectangle1.Width - 1, rectangle1.Y + 1), new Point(rectangle1.X + rectangle1.Width - rectangle1.Height + 1, rectangle1.Y + rectangle1.Height - 1)));
        else
          tupleList2.Add(new Tuple<Point, Point>(new Point((int) rectangle1.X, rectangle1.Y + 1), new Point(rectangle1.X + rectangle1.Height - 1, rectangle1.Y + rectangle1.Height - 1)));
      }
      List<Point> pointList2 = new List<Point>();
      int exitX;
      if (this.FindVerticalExit(new Rectangle(room2.X + 2, (int) room2.Y, room2.Width - 4, 1), true, out exitX))
        pointList2.Add(new Point(exitX, (int) room2.Y));
      if (this.FindVerticalExit(new Rectangle(room3.X + 2, room3.Y + room3.Height - 1, room3.Width - 4, 1), false, out exitX))
        pointList2.Add(new Point(exitX, room3.Y + room3.Height - 1));
      using (List<Rectangle>.Enumerator enumerator = rectangleList1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Rectangle current = enumerator.Current;
          WorldUtils.Gen(new Point((int) current.X, (int) current.Y), (GenShape) new Shapes.Rectangle((int) current.Width, (int) current.Height), Actions.Chain((GenAction) new Actions.SetTile(buildData.Tile, false, true), (GenAction) new Actions.SetFrames(true)));
          WorldUtils.Gen(new Point(current.X + 1, current.Y + 1), (GenShape) new Shapes.Rectangle(current.Width - 2, current.Height - 2), Actions.Chain((GenAction) new Actions.ClearTile(true), (GenAction) new Actions.PlaceWall(buildData.Wall, true)));
          structures.AddStructure(current, 8);
        }
      }
      using (List<Tuple<Point, Point>>.Enumerator enumerator = tupleList2.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Tuple<Point, Point> current = enumerator.Current;
          Point origin1 = current.Item1;
          Point point = current.Item2;
          int num8 = point.X > origin1.X ? 1 : -1;
          ShapeData data = new ShapeData();
          for (int y = 0; y < point.Y - origin1.Y; ++y)
            data.Add(num8 * (y + 1), y);
          WorldUtils.Gen(origin1, (GenShape) new ModShapes.All(data), Actions.Chain((GenAction) new Actions.PlaceTile((ushort) 19, buildData.PlatformStyle), (GenAction) new Actions.SetSlope(num8 == 1 ? 1 : 2), (GenAction) new Actions.SetFrames(true)));
          WorldUtils.Gen(new Point(origin1.X + (num8 == 1 ? 1 : -4), origin1.Y - 1), (GenShape) new Shapes.Rectangle(4, 1), Actions.Chain((GenAction) new Actions.Clear(), (GenAction) new Actions.PlaceWall(buildData.Wall, true), (GenAction) new Actions.PlaceTile((ushort) 19, buildData.PlatformStyle), (GenAction) new Actions.SetFrames(true)));
        }
      }
      using (List<Point>.Enumerator enumerator = pointList1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Point current = enumerator.Current;
          WorldUtils.Gen(current, (GenShape) new Shapes.Rectangle(1, 3), (GenAction) new Actions.ClearTile(true));
          WorldGen.PlaceTile((int) current.X, (int) current.Y, 10, true, true, -1, buildData.DoorStyle);
        }
      }
      using (List<Point>.Enumerator enumerator = pointList2.GetEnumerator())
      {
        while (enumerator.MoveNext())
          WorldUtils.Gen(enumerator.Current, (GenShape) new Shapes.Rectangle(3, 1), Actions.Chain((GenAction) new Actions.ClearMetadata(), (GenAction) new Actions.PlaceTile((ushort) 19, buildData.PlatformStyle), (GenAction) new Actions.SetFrames(true)));
      }
      using (List<Rectangle>.Enumerator enumerator = rectangleList2.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Rectangle current = enumerator.Current;
          if (current.Height > 1 && (int) GenBase._tiles[(int) current.X, current.Y - 1].type != 19)
          {
            WorldUtils.Gen(new Point((int) current.X, (int) current.Y), (GenShape) new Shapes.Rectangle((int) current.Width, (int) current.Height), Actions.Chain((GenAction) new Actions.SetTile((ushort) 124, false, true), (GenAction) new Actions.SetFrames(true)));
            Tile tile = GenBase._tiles[(int) current.X, (int) (current.Y + current.Height)];
            tile.slope((byte) 0);
            tile.halfBrick(false);
          }
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
      using (List<Rectangle>.Enumerator enumerator = rectangleList1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Rectangle current = enumerator.Current;
          int num8 = current.Width / 8;
          int num9 = current.Width / (num8 + 1);
          int num10 = GenBase._random.Next(2);
          for (int index1 = 0; index1 < num8; ++index1)
          {
            int num11 = (index1 + 1) * num9 + current.X;
            switch (index1 + num10 % 2)
            {
              case 0:
                int num12 = current.Y + Math.Min(current.Height / 2, current.Height - 5);
                Vector2 vector2 = WorldGen.randHousePicture();
                int x = (int) vector2.X;
                int y = (int) vector2.Y;
                if (!WorldGen.nearPicture(num11, num12))
                {
                  WorldGen.PlaceTile(num11, num12, x, true, false, -1, y);
                  break;
                }
                break;
              case 1:
                int j = current.Y + 1;
                WorldGen.PlaceTile(num11, j, 34, true, false, -1, GenBase._random.Next(6));
                for (int index2 = -1; index2 < 2; ++index2)
                {
                  for (int index3 = 0; index3 < 3; ++index3)
                    GenBase._tiles[index2 + num11, index3 + j].frameX += (short) 54;
                }
                break;
            }
          }
          int num13 = current.Width / 8 + 3;
          WorldGen.SetupStatueList();
          for (; num13 > 0; --num13)
          {
            int num11 = GenBase._random.Next(current.Width - 3) + 1 + current.X;
            int num12 = current.Y + current.Height - 2;
            switch (GenBase._random.Next(4))
            {
              case 0:
                WorldGen.PlaceSmallPile(num11, num12, GenBase._random.Next(31, 34), 1, (ushort) 185);
                break;
              case 1:
                WorldGen.PlaceTile(num11, num12, 186, true, false, -1, GenBase._random.Next(22, 26));
                break;
              case 2:
                int index = GenBase._random.Next(2, WorldGen.statueList.Length);
                WorldGen.PlaceTile(num11, num12, (int) WorldGen.statueList[index].X, true, false, -1, (int) WorldGen.statueList[index].Y);
                if (WorldGen.StatuesWithTraps.Contains(index))
                {
                  WorldGen.PlaceStatueTrap(num11, num12);
                  break;
                }
                break;
              case 3:
                Point point = Utils.SelectRandom<Point>(GenBase._random, pointArray);
                WorldGen.PlaceTile(num11, num12, (int) point.X, true, false, -1, (int) point.Y);
                break;
            }
          }
        }
      }
      using (List<Rectangle>.Enumerator enumerator = rectangleList1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Rectangle current = enumerator.Current;
          buildData.ProcessRoom(current);
        }
      }
      bool flag1 = false;
      using (List<Rectangle>.Enumerator enumerator = rectangleList1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Rectangle current = enumerator.Current;
          int j = current.Height - 1 + current.Y;
          int Style = j > (int) Main.worldSurface ? buildData.ChestStyle : 0;
          int num8 = 0;
          while (num8 < 10 && !(flag1 = WorldGen.AddBuriedChest(GenBase._random.Next(2, current.Width - 2) + current.X, j, 0, false, Style)))
            ++num8;
          if (!flag1)
          {
            int i = current.X + 2;
            while (i <= current.X + current.Width - 2 && !(flag1 = WorldGen.AddBuriedChest(i, j, 0, false, Style)))
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
        using (List<Rectangle>.Enumerator enumerator = rectangleList1.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Rectangle current = enumerator.Current;
            int j = current.Y - 1;
            int Style = j > (int) Main.worldSurface ? buildData.ChestStyle : 0;
            int num8 = 0;
            while (num8 < 10 && !(flag1 = WorldGen.AddBuriedChest(GenBase._random.Next(2, current.Width - 2) + current.X, j, 0, false, Style)))
              ++num8;
            if (!flag1)
            {
              int i = current.X + 2;
              while (i <= current.X + current.Width - 2 && !(flag1 = WorldGen.AddBuriedChest(i, j, 0, false, Style)))
                ++i;
              if (flag1)
                break;
            }
            else
              break;
          }
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
        using (List<Rectangle>.Enumerator enumerator = rectangleList1.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Rectangle current = enumerator.Current;
            int j = current.Height - 2 + current.Y;
            for (int index = 0; index < 10; ++index)
            {
              int i = GenBase._random.Next(2, current.Width - 2) + current.X;
              WorldGen.PlaceTile(i, j, 377, true, true, -1, 0);
              if (flag2 = GenBase._tiles[i, j].active() && (int) GenBase._tiles[i, j].type == 377)
                break;
            }
            if (!flag2)
            {
              int i = current.X + 2;
              while (i <= current.X + current.Width - 2 && !(flag2 = WorldGen.PlaceTile(i, j, 377, true, true, -1, 0)))
                ++i;
              if (flag2)
                break;
            }
            else
              break;
          }
        }
        if (flag2)
          ++this._sharpenerCount;
      }
      if (buildData == CaveHouseBiome.BuildData.Desert && this._extractinatorCount < GenBase._random.Next(2, 5))
      {
        bool flag2 = false;
        using (List<Rectangle>.Enumerator enumerator = rectangleList1.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Rectangle current = enumerator.Current;
            int j = current.Height - 2 + current.Y;
            for (int index = 0; index < 10; ++index)
            {
              int i = GenBase._random.Next(2, current.Width - 2) + current.X;
              WorldGen.PlaceTile(i, j, 219, true, true, -1, 0);
              if (flag2 = GenBase._tiles[i, j].active() && (int) GenBase._tiles[i, j].type == 219)
                break;
            }
            if (!flag2)
            {
              int i = current.X + 2;
              while (i <= current.X + current.Width - 2 && !(flag2 = WorldGen.PlaceTile(i, j, 219, true, true, -1, 0)))
                ++i;
              if (flag2)
                break;
            }
            else
              break;
          }
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

    internal static void AgeDefaultRoom(Rectangle room)
    {
      for (int index = 0; index < room.Width * room.Height / 16; ++index)
        WorldUtils.Gen(new Point(GenBase._random.Next(1, room.Width - 1) + room.X, GenBase._random.Next(1, room.Height - 1) + room.Y), (GenShape) new Shapes.Rectangle(2, 2), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.Blotches(2, 2.0), (GenAction) new Modifiers.IsEmpty(), (GenAction) new Actions.SetTile((ushort) 51, true, true)));
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Modifiers.OnlyWalls(new byte[1]
      {
        CaveHouseBiome.BuildData.Default.Wall
      }), (double) room.Y > Main.worldSurface ? (GenAction) new Actions.ClearWall(true) : (GenAction) new Actions.PlaceWall((byte) 2, true)));
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.949999988079071), (GenAction) new Modifiers.OnlyTiles(new ushort[3]
      {
        (ushort) 30,
        (ushort) 321,
        (ushort) 158
      }), (GenAction) new Actions.ClearTile(true)));
    }

    internal static void AgeSnowRoom(Rectangle room)
    {
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.Blotches(2, 0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        CaveHouseBiome.BuildData.Snow.Tile
      }), (GenAction) new Actions.SetTile((ushort) 161, true, true), (GenAction) new Modifiers.Dither(0.8), (GenAction) new Actions.SetTile((ushort) 147, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, (int) room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 161
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 161
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.8), (double) room.Y > Main.worldSurface ? (GenAction) new Actions.ClearWall(true) : (GenAction) new Actions.PlaceWall((byte) 40, true)));
    }

    internal static void AgeDesertRoom(Rectangle room)
    {
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Modifiers.Blotches(2, 0.200000002980232), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        CaveHouseBiome.BuildData.Desert.Tile
      }), (GenAction) new Actions.SetTile((ushort) 396, true, true), (GenAction) new Modifiers.Dither(0.5), (GenAction) new Actions.SetTile((ushort) 397, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, (int) room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[2]
      {
        (ushort) 397,
        (ushort) 396
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[2]
      {
        (ushort) 397,
        (ushort) 396
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Modifiers.OnlyWalls(new byte[1]
      {
        CaveHouseBiome.BuildData.Desert.Wall
      }), (GenAction) new Actions.PlaceWall((byte) 216, true)));
    }

    internal static void AgeGraniteRoom(Rectangle room)
    {
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.Blotches(2, 0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        CaveHouseBiome.BuildData.Granite.Tile
      }), (GenAction) new Actions.SetTile((ushort) 368, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, (int) room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 368
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 368
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Actions.PlaceWall((byte) 180, true)));
    }

    internal static void AgeMarbleRoom(Rectangle room)
    {
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.Blotches(2, 0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        CaveHouseBiome.BuildData.Marble.Tile
      }), (GenAction) new Actions.SetTile((ushort) 367, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, (int) room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 367
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 367
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Actions.PlaceWall((byte) 178, true)));
    }

    internal static void AgeMushroomRoom(Rectangle room)
    {
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.699999988079071), (GenAction) new Modifiers.Blotches(2, 0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        CaveHouseBiome.BuildData.Mushroom.Tile
      }), (GenAction) new Actions.SetTile((ushort) 70, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, (int) room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 70
      }), (GenAction) new Modifiers.Offset(0, -1), (GenAction) new Actions.SetTile((ushort) 71, false, true)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 70
      }), (GenAction) new Modifiers.Offset(0, -1), (GenAction) new Actions.SetTile((ushort) 71, false, true)));
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Actions.ClearWall(false)));
    }

    internal static void AgeJungleRoom(Rectangle room)
    {
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.Blotches(2, 0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        CaveHouseBiome.BuildData.Jungle.Tile
      }), (GenAction) new Actions.SetTile((ushort) 60, true, true), (GenAction) new Modifiers.Dither(0.800000011920929), (GenAction) new Actions.SetTile((ushort) 59, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, (int) room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 60
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionVines(3, (int) room.Height, 62)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 60
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionVines(3, (int) room.Height, 62)));
      WorldUtils.Gen(new Point((int) room.X, (int) room.Y), (GenShape) new Shapes.Rectangle((int) room.Width, (int) room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Actions.PlaceWall((byte) 64, true)));
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

      public delegate void ProcessRoomMethod(Rectangle room);
    }
  }
}
