// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.MarbleBiome
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Terraria.World.Generation;

namespace Terraria.GameContent.Biomes
{
  public class MarbleBiome : MicroBiome
  {
    private const int SCALE = 3;
    private MarbleBiome.Slab[,] _slabs;

    private void SmoothSlope(int x, int y)
    {
      MarbleBiome.Slab slab = this._slabs[x, y];
      if (!slab.IsSolid)
        return;
      switch ((this._slabs[x, y - 1].IsSolid ? 1 : 0) << 3 | (this._slabs[x, y + 1].IsSolid ? 1 : 0) << 2 | (this._slabs[x - 1, y].IsSolid ? 1 : 0) << 1 | (this._slabs[x + 1, y].IsSolid ? 1 : 0))
      {
        case 4:
          this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.HalfBrick));
          break;
        case 5:
          this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.BottomRightFilled));
          break;
        case 6:
          this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.BottomLeftFilled));
          break;
        case 9:
          this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.TopRightFilled));
          break;
        case 10:
          this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.TopLeftFilled));
          break;
        default:
          this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.Solid));
          break;
      }
    }

    private void PlaceSlab(MarbleBiome.Slab slab, int originX, int originY, int scale)
    {
      for (int x = 0; x < scale; ++x)
      {
        for (int y = 0; y < scale; ++y)
        {
          Tile tile = GenBase._tiles[originX + x, originY + y];
          if (TileID.Sets.Ore[(int) tile.type])
            tile.ResetToType(tile.type);
          else
            tile.ResetToType((ushort) 367);
          bool active = slab.State(x, y, scale);
          tile.active(active);
          if (slab.HasWall)
            tile.wall = (byte) 178;
          WorldUtils.TileFrame(originX + x, originY + y, true);
          WorldGen.SquareWallFrame(originX + x, originY + y, true);
          Tile.SmoothSlope(originX + x, originY + y, true);
          if (WorldGen.SolidTile(originX + x, originY + y - 1) && GenBase._random.Next(4) == 0)
            WorldGen.PlaceTight(originX + x, originY + y, (ushort) 165, false);
          if (WorldGen.SolidTile(originX + x, originY + y) && GenBase._random.Next(4) == 0)
            WorldGen.PlaceTight(originX + x, originY + y - 1, (ushort) 165, false);
        }
      }
    }

    private bool IsGroupSolid(int x, int y, int scale)
    {
      int num = 0;
      for (int index1 = 0; index1 < scale; ++index1)
      {
        for (int index2 = 0; index2 < scale; ++index2)
        {
          if (WorldGen.SolidOrSlopedTile(x + index1, y + index2))
            ++num;
        }
      }
      return num > scale / 4 * 3;
    }

    public override bool Place(Point origin, StructureMap structures)
    {
      if (this._slabs == null)
        this._slabs = new MarbleBiome.Slab[56, 26];
      int num1 = GenBase._random.Next(80, 150) / 3;
      int num2 = GenBase._random.Next(40, 60) / 3;
      int num3 = (num2 * 3 - GenBase._random.Next(20, 30)) / 3;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Point& local1 = @origin;
      // ISSUE: explicit reference operation
      int num4 = (^local1).X - num1 * 3 / 2;
      // ISSUE: explicit reference operation
      (^local1).X = (__Null) num4;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Point& local2 = @origin;
      // ISSUE: explicit reference operation
      int num5 = (^local2).Y - num2 * 3 / 2;
      // ISSUE: explicit reference operation
      (^local2).Y = (__Null) num5;
      for (int index1 = -1; index1 < num1 + 1; ++index1)
      {
        float num6 = (float) ((double) (index1 - num1 / 2) / (double) num1 + 0.5);
        int num7 = (int) ((0.5 - (double) Math.Abs(num6 - 0.5f)) * 5.0) - 2;
        for (int index2 = -1; index2 < num2 + 1; ++index2)
        {
          bool hasWall = true;
          bool flag1 = false;
          bool flag2 = this.IsGroupSolid(index1 * 3 + origin.X, index2 * 3 + origin.Y, 3);
          int num8 = Math.Abs(index2 - num2 / 2) - num3 / 4 + num7;
          if (num8 > 3)
          {
            flag1 = flag2;
            hasWall = false;
          }
          else if (num8 > 0)
          {
            flag1 = index2 - num2 / 2 > 0 || flag2;
            hasWall = index2 - num2 / 2 < 0 || num8 <= 2;
          }
          else if (num8 == 0)
            flag1 = GenBase._random.Next(2) == 0 && (index2 - num2 / 2 > 0 || flag2);
          if ((double) Math.Abs(num6 - 0.5f) > 0.349999994039536 + (double) GenBase._random.NextFloat() * 0.100000001490116 && !flag2)
          {
            hasWall = false;
            flag1 = false;
          }
          this._slabs[index1 + 1, index2 + 1] = MarbleBiome.Slab.Create(flag1 ? new MarbleBiome.SlabState(MarbleBiome.SlabStates.Solid) : new MarbleBiome.SlabState(MarbleBiome.SlabStates.Empty), hasWall);
        }
      }
      for (int index1 = 0; index1 < num1; ++index1)
      {
        for (int index2 = 0; index2 < num2; ++index2)
          this.SmoothSlope(index1 + 1, index2 + 1);
      }
      int num9 = num1 / 2;
      int val1 = num2 / 2;
      int num10 = (val1 + 1) * (val1 + 1);
      float num11 = (float) ((double) GenBase._random.NextFloat() * 2.0 - 1.0);
      float num12 = (float) ((double) GenBase._random.NextFloat() * 2.0 - 1.0);
      float num13 = (float) ((double) GenBase._random.NextFloat() * 2.0 - 1.0);
      float num14 = 0.0f;
      for (int index1 = 0; index1 <= num1; ++index1)
      {
        float num6 = (float) val1 / (float) num9 * (float) (index1 - num9);
        int num7 = Math.Min(val1, (int) Math.Sqrt((double) Math.Max(0.0f, (float) num10 - num6 * num6)));
        if (index1 < num1 / 2)
          num14 += MathHelper.Lerp(num11, num12, (float) index1 / (float) (num1 / 2));
        else
          num14 += MathHelper.Lerp(num12, num13, (float) ((double) index1 / (double) (num1 / 2) - 1.0));
        for (int index2 = val1 - num7; index2 <= val1 + num7; ++index2)
          this.PlaceSlab(this._slabs[index1 + 1, index2 + 1], index1 * 3 + origin.X, index2 * 3 + origin.Y + (int) num14, 3);
      }
      return true;
    }

    private delegate bool SlabState(int x, int y, int scale);

    private class SlabStates
    {
      public static bool Empty(int x, int y, int scale)
      {
        return false;
      }

      public static bool Solid(int x, int y, int scale)
      {
        return true;
      }

      public static bool HalfBrick(int x, int y, int scale)
      {
        return y >= scale / 2;
      }

      public static bool BottomRightFilled(int x, int y, int scale)
      {
        return x >= scale - y;
      }

      public static bool BottomLeftFilled(int x, int y, int scale)
      {
        return x < y;
      }

      public static bool TopRightFilled(int x, int y, int scale)
      {
        return x > y;
      }

      public static bool TopLeftFilled(int x, int y, int scale)
      {
        return x < scale - y;
      }
    }

    private struct Slab
    {
      public readonly MarbleBiome.SlabState State;
      public readonly bool HasWall;

      public bool IsSolid
      {
        get
        {
          return this.State != new MarbleBiome.SlabState(MarbleBiome.SlabStates.Empty);
        }
      }

      private Slab(MarbleBiome.SlabState state, bool hasWall)
      {
        this.State = state;
        this.HasWall = hasWall;
      }

      public MarbleBiome.Slab WithState(MarbleBiome.SlabState state)
      {
        return new MarbleBiome.Slab(state, this.HasWall);
      }

      public static MarbleBiome.Slab Create(MarbleBiome.SlabState state, bool hasWall)
      {
        return new MarbleBiome.Slab(state, hasWall);
      }
    }
  }
}
