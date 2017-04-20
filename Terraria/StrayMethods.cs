// Decompiled with JetBrains decompiler
// Type: Terraria.StrayMethods
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria
{
  public class StrayMethods
  {
    public static bool CountSandHorizontally(int i, int j, bool[] fittingTypes, int requiredTotalSpread = 4, int spreadInEachAxis = 5)
    {
      if (!WorldGen.InWorld(i, j, 2))
        return false;
      int num1 = 0;
      int num2 = 0;
      for (int i1 = i - 1; num1 < spreadInEachAxis && i1 > 0; --i1)
      {
        Tile tile = Main.tile[i1, j];
        if (tile.active() && fittingTypes[(int) tile.type] && !WorldGen.SolidTileAllowBottomSlope(i1, j - 1))
          ++num1;
        else if (!tile.active())
          break;
      }
      for (int i1 = i + 1; num2 < spreadInEachAxis && i1 < Main.maxTilesX - 1; ++i1)
      {
        Tile tile = Main.tile[i1, j];
        if (tile.active() && fittingTypes[(int) tile.type] && !WorldGen.SolidTileAllowBottomSlope(i1, j - 1))
          ++num2;
        else if (!tile.active())
          break;
      }
      return num1 + num2 + 1 >= requiredTotalSpread;
    }

    public static bool CanSpawnSandstormHostile(Vector2 position, int expandUp, int expandDown)
    {
      bool flag = true;
      Point tileCoordinates = position.ToTileCoordinates();
      for (int index = -1; index <= 1; ++index)
      {
        int topY;
        int bottomY;
        Collision.ExpandVertically(tileCoordinates.X + index, (int) tileCoordinates.Y, out topY, out bottomY, expandUp, expandDown);
        ++topY;
        --bottomY;
        if (bottomY - topY < 20)
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    public static bool CanSpawnSandstormFriendly(Vector2 position, int expandUp, int expandDown)
    {
      bool flag = true;
      Point tileCoordinates = position.ToTileCoordinates();
      for (int index = -1; index <= 1; ++index)
      {
        int topY;
        int bottomY;
        Collision.ExpandVertically(tileCoordinates.X + index, (int) tileCoordinates.Y, out topY, out bottomY, expandUp, expandDown);
        ++topY;
        --bottomY;
        if (bottomY - topY < 10)
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    public static void CheckArenaScore(Vector2 arenaCenter, out Point xLeftEnd, out Point xRightEnd, int walkerWidthInTiles = 5, int walkerHeightInTiles = 10)
    {
      bool showDebug = false;
      Point tileCoordinates = arenaCenter.ToTileCoordinates();
      xLeftEnd = xRightEnd = tileCoordinates;
      int topY;
      int bottomY;
      Collision.ExpandVertically((int) tileCoordinates.X, (int) tileCoordinates.Y, out topY, out bottomY, 0, 4);
      tileCoordinates.Y = (__Null) bottomY;
      if (showDebug)
        Dust.QuickDust(tileCoordinates, Color.get_Blue()).scale = 5f;
      int distanceCoveredInTiles1;
      Point lastIteratedFloorSpot1;
      StrayMethods.SendWalker(tileCoordinates, walkerHeightInTiles, -1, out distanceCoveredInTiles1, out lastIteratedFloorSpot1, 120, showDebug);
      int distanceCoveredInTiles2;
      Point lastIteratedFloorSpot2;
      StrayMethods.SendWalker(tileCoordinates, walkerHeightInTiles, 1, out distanceCoveredInTiles2, out lastIteratedFloorSpot2, 120, showDebug);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Point& local1 = @lastIteratedFloorSpot1;
      // ISSUE: explicit reference operation
      int num1 = (^local1).X + 1;
      // ISSUE: explicit reference operation
      (^local1).X = (__Null) num1;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Point& local2 = @lastIteratedFloorSpot2;
      // ISSUE: explicit reference operation
      int num2 = (^local2).X - 1;
      // ISSUE: explicit reference operation
      (^local2).X = (__Null) num2;
      if (showDebug)
        Dust.QuickDustLine(lastIteratedFloorSpot1.ToWorldCoordinates(8f, 8f), lastIteratedFloorSpot2.ToWorldCoordinates(8f, 8f), 50f, Color.get_Pink());
      xLeftEnd = lastIteratedFloorSpot1;
      xRightEnd = lastIteratedFloorSpot2;
    }

    public static void SendWalker(Point startFloorPosition, int height, int direction, out int distanceCoveredInTiles, out Point lastIteratedFloorSpot, int maxDistance = 100, bool showDebug = false)
    {
      distanceCoveredInTiles = 0;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Point& local1 = @startFloorPosition;
      // ISSUE: explicit reference operation
      int num1 = (^local1).Y - 1;
      // ISSUE: explicit reference operation
      (^local1).Y = (__Null) num1;
      lastIteratedFloorSpot = startFloorPosition;
      for (int index1 = 0; index1 < maxDistance; ++index1)
      {
        for (int index2 = 0; index2 < 3 && WorldGen.SolidTile3((int) startFloorPosition.X, (int) startFloorPosition.Y); ++index2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Point& local2 = @startFloorPosition;
          // ISSUE: explicit reference operation
          int num2 = (^local2).Y - 1;
          // ISSUE: explicit reference operation
          (^local2).Y = (__Null) num2;
        }
        int topY1;
        int bottomY1;
        Collision.ExpandVertically((int) startFloorPosition.X, (int) startFloorPosition.Y, out topY1, out bottomY1, height, 2);
        ++topY1;
        --bottomY1;
        if (!WorldGen.SolidTile3((int) startFloorPosition.X, bottomY1 + 1))
        {
          int topY2;
          int bottomY2;
          Collision.ExpandVertically((int) startFloorPosition.X, bottomY1, out topY2, out bottomY2, 0, 6);
          if (showDebug)
            Dust.QuickBox(new Vector2((float) (startFloorPosition.X * 16 + 8), (float) (topY2 * 16)), new Vector2((float) (startFloorPosition.X * 16 + 8), (float) (bottomY2 * 16)), 1, Color.get_Blue(), (Action<Dust>) null);
          if (!WorldGen.SolidTile3((int) startFloorPosition.X, bottomY2))
            break;
        }
        if (bottomY1 - topY1 >= height - 1)
        {
          if (showDebug)
          {
            Dust.QuickDust(startFloorPosition, Color.get_Green()).scale = 1f;
            Dust.QuickBox(new Vector2((float) (startFloorPosition.X * 16 + 8), (float) (topY1 * 16)), new Vector2((float) (startFloorPosition.X * 16 + 8), (float) (bottomY1 * 16 + 16)), 1, Color.get_Red(), (Action<Dust>) null);
          }
          distanceCoveredInTiles += direction;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Point& local2 = @startFloorPosition;
          // ISSUE: explicit reference operation
          int num2 = (^local2).X + direction;
          // ISSUE: explicit reference operation
          (^local2).X = (__Null) num2;
          startFloorPosition.Y = (__Null) bottomY1;
          lastIteratedFloorSpot = startFloorPosition;
          if (Math.Abs(distanceCoveredInTiles) >= maxDistance)
            break;
        }
        else
          break;
      }
      distanceCoveredInTiles = Math.Abs(distanceCoveredInTiles);
    }
  }
}
