// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.GraniteBiome
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.World.Generation;

namespace Terraria.GameContent.Biomes
{
  public class GraniteBiome : MicroBiome
  {
    private static GraniteBiome.Magma[,] _sourceMagmaMap = new GraniteBiome.Magma[200, 200];
    private static GraniteBiome.Magma[,] _targetMagmaMap = new GraniteBiome.Magma[200, 200];
    private const int MAX_MAGMA_ITERATIONS = 300;

    public override bool Place(Point origin, StructureMap structures)
    {
      if (GenBase._tiles[(int) origin.X, (int) origin.Y].active())
        return false;
      int length1 = GraniteBiome._sourceMagmaMap.GetLength(0);
      int length2 = GraniteBiome._sourceMagmaMap.GetLength(1);
      int index1 = length1 / 2;
      int index2 = length2 / 2;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Point& local1 = @origin;
      // ISSUE: explicit reference operation
      int num1 = (^local1).X - index1;
      // ISSUE: explicit reference operation
      (^local1).X = (__Null) num1;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Point& local2 = @origin;
      // ISSUE: explicit reference operation
      int num2 = (^local2).Y - index2;
      // ISSUE: explicit reference operation
      (^local2).Y = (__Null) num2;
      for (int index3 = 0; index3 < length1; ++index3)
      {
        for (int index4 = 0; index4 < length2; ++index4)
        {
          int i = index3 + origin.X;
          int j = index4 + origin.Y;
          GraniteBiome._sourceMagmaMap[index3, index4] = GraniteBiome.Magma.CreateEmpty(WorldGen.SolidTile(i, j) ? 4f : 1f);
          GraniteBiome._targetMagmaMap[index3, index4] = GraniteBiome._sourceMagmaMap[index3, index4];
        }
      }
      int max1 = index1;
      int min1 = index1;
      int max2 = index2;
      int min2 = index2;
      for (int index3 = 0; index3 < 300; ++index3)
      {
        for (int index4 = max1; index4 <= min1; ++index4)
        {
          for (int index5 = max2; index5 <= min2; ++index5)
          {
            GraniteBiome.Magma sourceMagma1 = GraniteBiome._sourceMagmaMap[index4, index5];
            if (sourceMagma1.IsActive)
            {
              float num3 = 0.0f;
              Vector2 vector2_1 = Vector2.get_Zero();
              for (int index6 = -1; index6 <= 1; ++index6)
              {
                for (int index7 = -1; index7 <= 1; ++index7)
                {
                  if (index6 != 0 || index7 != 0)
                  {
                    Vector2 vector2_2;
                    // ISSUE: explicit reference operation
                    ((Vector2) @vector2_2).\u002Ector((float) index6, (float) index7);
                    // ISSUE: explicit reference operation
                    ((Vector2) @vector2_2).Normalize();
                    GraniteBiome.Magma sourceMagma2 = GraniteBiome._sourceMagmaMap[index4 + index6, index5 + index7];
                    if ((double) sourceMagma1.Pressure > 0.00999999977648258 && !sourceMagma2.IsActive)
                    {
                      if (index6 == -1)
                        max1 = Utils.Clamp<int>(index4 + index6, 1, max1);
                      else
                        min1 = Utils.Clamp<int>(index4 + index6, min1, length1 - 2);
                      if (index7 == -1)
                        max2 = Utils.Clamp<int>(index5 + index7, 1, max2);
                      else
                        min2 = Utils.Clamp<int>(index5 + index7, min2, length2 - 2);
                      GraniteBiome._targetMagmaMap[index4 + index6, index5 + index7] = sourceMagma2.ToFlow();
                    }
                    float pressure = sourceMagma2.Pressure;
                    num3 += pressure;
                    vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(pressure, vector2_2));
                  }
                }
              }
              float num4 = num3 / 8f;
              if ((double) num4 > (double) sourceMagma1.Resistance)
              {
                // ISSUE: explicit reference operation
                float num5 = ((Vector2) @vector2_1).Length() / 8f;
                float pressure = Math.Max(0.0f, (float) ((double) Math.Max(num4 - num5 - sourceMagma1.Pressure, 0.0f) + (double) num5 + (double) sourceMagma1.Pressure * 0.875) - sourceMagma1.Resistance);
                GraniteBiome._targetMagmaMap[index4, index5] = GraniteBiome.Magma.CreateFlow(pressure, Math.Max(0.0f, sourceMagma1.Resistance - pressure * 0.02f));
              }
            }
          }
        }
        if (index3 < 2)
          GraniteBiome._targetMagmaMap[index1, index2] = GraniteBiome.Magma.CreateFlow(25f, 0.0f);
        Utils.Swap<GraniteBiome.Magma[,]>(ref GraniteBiome._sourceMagmaMap, ref GraniteBiome._targetMagmaMap);
      }
      bool flag1 = origin.Y + index2 > WorldGen.lavaLine - 30;
      bool flag2 = false;
      for (int index3 = -50; index3 < 50 && !flag2; ++index3)
      {
        for (int index4 = -50; index4 < 50 && !flag2; ++index4)
        {
          if (GenBase._tiles[origin.X + index1 + index3, origin.Y + index2 + index4].active())
          {
            switch (GenBase._tiles[origin.X + index1 + index3, origin.Y + index2 + index4].type)
            {
              case 147:
              case 161:
              case 162:
              case 163:
              case 200:
                flag1 = false;
                flag2 = true;
                continue;
              default:
                continue;
            }
          }
        }
      }
      for (int index3 = max1; index3 <= min1; ++index3)
      {
        for (int index4 = max2; index4 <= min2; ++index4)
        {
          GraniteBiome.Magma sourceMagma = GraniteBiome._sourceMagmaMap[index3, index4];
          if (sourceMagma.IsActive)
          {
            Tile tile = GenBase._tiles[origin.X + index3, origin.Y + index4];
            if ((double) Math.Max(1f - Math.Max(0.0f, (float) (Math.Sin((double) (origin.Y + index4) * 0.400000005960464) * 0.699999988079071 + 1.20000004768372) * (float) (0.200000002980232 + 0.5 / Math.Sqrt((double) Math.Max(0.0f, sourceMagma.Pressure - sourceMagma.Resistance)))), sourceMagma.Pressure / 15f) > 0.349999994039536 + (WorldGen.SolidTile(origin.X + index3, origin.Y + index4) ? 0.0 : 0.5))
            {
              if (TileID.Sets.Ore[(int) tile.type])
                tile.ResetToType(tile.type);
              else
                tile.ResetToType((ushort) 368);
              tile.wall = (byte) 180;
            }
            else if ((double) sourceMagma.Resistance < 0.00999999977648258)
            {
              WorldUtils.ClearTile(origin.X + index3, origin.Y + index4, false);
              tile.wall = (byte) 180;
            }
            if ((int) tile.liquid > 0 && flag1)
              tile.liquidType(1);
          }
        }
      }
      List<Point16> point16List = new List<Point16>();
      for (int index3 = max1; index3 <= min1; ++index3)
      {
        for (int index4 = max2; index4 <= min2; ++index4)
        {
          if (GraniteBiome._sourceMagmaMap[index3, index4].IsActive)
          {
            int num3 = 0;
            int num4 = index3 + origin.X;
            int num5 = index4 + origin.Y;
            if (WorldGen.SolidTile(num4, num5))
            {
              for (int index5 = -1; index5 <= 1; ++index5)
              {
                for (int index6 = -1; index6 <= 1; ++index6)
                {
                  if (WorldGen.SolidTile(num4 + index5, num5 + index6))
                    ++num3;
                }
              }
              if (num3 < 3)
                point16List.Add(new Point16(num4, num5));
            }
          }
        }
      }
      foreach (Point16 point16 in point16List)
      {
        int x = (int) point16.X;
        int y = (int) point16.Y;
        WorldUtils.ClearTile(x, y, true);
        GenBase._tiles[x, y].wall = (byte) 180;
      }
      point16List.Clear();
      for (int index3 = max1; index3 <= min1; ++index3)
      {
        for (int index4 = max2; index4 <= min2; ++index4)
        {
          GraniteBiome.Magma sourceMagma = GraniteBiome._sourceMagmaMap[index3, index4];
          int index5 = index3 + origin.X;
          int index6 = index4 + origin.Y;
          if (sourceMagma.IsActive)
          {
            WorldUtils.TileFrame(index5, index6, false);
            WorldGen.SquareWallFrame(index5, index6, true);
            if (GenBase._random.Next(8) == 0 && GenBase._tiles[index5, index6].active())
            {
              if (!GenBase._tiles[index5, index6 + 1].active())
                WorldGen.PlaceTight(index5, index6 + 1, (ushort) 165, false);
              if (!GenBase._tiles[index5, index6 - 1].active())
                WorldGen.PlaceTight(index5, index6 - 1, (ushort) 165, false);
            }
            if (GenBase._random.Next(2) == 0)
              Tile.SmoothSlope(index5, index6, true);
          }
        }
      }
      return true;
    }

    private struct Magma
    {
      public readonly float Pressure;
      public readonly float Resistance;
      public readonly bool IsActive;

      private Magma(float pressure, float resistance, bool active)
      {
        this.Pressure = pressure;
        this.Resistance = resistance;
        this.IsActive = active;
      }

      public GraniteBiome.Magma ToFlow()
      {
        return new GraniteBiome.Magma(this.Pressure, this.Resistance, true);
      }

      public static GraniteBiome.Magma CreateFlow(float pressure, float resistance = 0.0f)
      {
        return new GraniteBiome.Magma(pressure, resistance, true);
      }

      public static GraniteBiome.Magma CreateEmpty(float resistance = 0.0f)
      {
        return new GraniteBiome.Magma(0.0f, resistance, false);
      }
    }
  }
}
