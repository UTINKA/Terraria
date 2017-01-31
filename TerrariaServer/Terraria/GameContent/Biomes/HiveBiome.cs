// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.HiveBiome
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;
using Terraria.World.Generation;

namespace Terraria.GameContent.Biomes
{
  public class HiveBiome : MicroBiome
  {
    public override bool Place(Point origin, StructureMap structures)
    {
      Ref<int> count1 = new Ref<int>(0);
      Ref<int> count2 = new Ref<int>(0);
      Ref<int> count3 = new Ref<int>(0);
      Ref<int> count4 = new Ref<int>(0);
      WorldUtils.Gen(origin, (GenShape) new Shapes.Circle(15), Actions.Chain((GenAction) new Actions.Scanner(count3), (GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.Scanner(count1), (GenAction) new Modifiers.OnlyTiles(new ushort[2]
      {
        (ushort) 60,
        (ushort) 59
      }), (GenAction) new Actions.Scanner(count2), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 60
      }), (GenAction) new Actions.Scanner(count4)));
      if ((double) count2.Value / (double) count1.Value < 0.75 || count4.Value < 2 || !structures.CanPlace(new Microsoft.Xna.Framework.Rectangle(origin.X - 50, origin.Y - 50, 100, 100), 0))
        return false;
      int x1 = origin.X;
      int y1 = origin.Y;
      int num1 = 150;
      int index1 = x1 - num1;
      while (index1 < x1 + num1)
      {
        if (index1 > 0 && index1 <= Main.maxTilesX - 1)
        {
          int index2 = y1 - num1;
          while (index2 < y1 + num1)
          {
            if (index2 > 0 && index2 <= Main.maxTilesY - 1 && (Main.tile[index1, index2].active() && (int) Main.tile[index1, index2].type == 226 || ((int) Main.tile[index1, index2].wall == 87 || (int) Main.tile[index1, index2].wall == 3) || (int) Main.tile[index1, index2].wall == 83))
              return false;
            index2 += 10;
          }
        }
        index1 += 10;
      }
      int x2 = origin.X;
      int y2 = origin.Y;
      int index3 = 0;
      int[] numArray1 = new int[10];
      int[] numArray2 = new int[10];
      Vector2 vector2_1 = new Vector2((float) x2, (float) y2);
      Vector2 vector2_2 = vector2_1;
      int num2 = WorldGen.genRand.Next(2, 5);
      for (int index2 = 0; index2 < num2; ++index2)
      {
        int num3 = WorldGen.genRand.Next(2, 5);
        for (int index4 = 0; index4 < num3; ++index4)
          vector2_2 = WorldGen.Hive((int) vector2_1.X, (int) vector2_1.Y);
        vector2_1 = vector2_2;
        numArray1[index3] = (int) vector2_1.X;
        numArray2[index3] = (int) vector2_1.Y;
        ++index3;
      }
      for (int index2 = 0; index2 < index3; ++index2)
      {
        int index4 = numArray1[index2];
        int index5 = numArray2[index2];
        bool flag = false;
        int num3 = 1;
        if (WorldGen.genRand.Next(2) == 0)
          num3 = -1;
        while (index4 > 10 && index4 < Main.maxTilesX - 10 && (index5 > 10 && index5 < Main.maxTilesY - 10) && (!Main.tile[index4, index5].active() || !Main.tile[index4, index5 + 1].active() || (!Main.tile[index4 + 1, index5].active() || !Main.tile[index4 + 1, index5 + 1].active())))
        {
          index4 += num3;
          if (Math.Abs(index4 - numArray1[index2]) > 50)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          int i = index4 + num3;
          for (int index6 = i - 1; index6 <= i + 2; ++index6)
          {
            for (int index7 = index5 - 1; index7 <= index5 + 2; ++index7)
            {
              if (index6 < 10 || index6 > Main.maxTilesX - 10)
                flag = true;
              else if (Main.tile[index6, index7].active() && (int) Main.tile[index6, index7].type != 225)
              {
                flag = true;
                break;
              }
            }
          }
          if (!flag)
          {
            for (int index6 = i - 1; index6 <= i + 2; ++index6)
            {
              for (int index7 = index5 - 1; index7 <= index5 + 2; ++index7)
              {
                if (index6 >= i && index6 <= i + 1 && (index7 >= index5 && index7 <= index5 + 1))
                {
                  Main.tile[index6, index7].active(false);
                  Main.tile[index6, index7].liquid = byte.MaxValue;
                  Main.tile[index6, index7].honey(true);
                }
                else
                {
                  Main.tile[index6, index7].active(true);
                  Main.tile[index6, index7].type = (ushort) 225;
                }
              }
            }
            int num4 = num3 * -1;
            int j = index5 + 1;
            int num5 = 0;
            while ((num5 < 4 || WorldGen.SolidTile(i, j)) && (i > 10 && i < Main.maxTilesX - 10))
            {
              ++num5;
              i += num4;
              if (WorldGen.SolidTile(i, j))
              {
                WorldGen.PoundTile(i, j);
                if (!Main.tile[i, j + 1].active())
                {
                  Main.tile[i, j + 1].active(true);
                  Main.tile[i, j + 1].type = (ushort) 225;
                }
              }
            }
          }
        }
      }
      WorldGen.larvaX[WorldGen.numLarva] = Utils.Clamp<int>((int) vector2_1.X, 5, Main.maxTilesX - 5);
      WorldGen.larvaY[WorldGen.numLarva] = Utils.Clamp<int>((int) vector2_1.Y, 5, Main.maxTilesY - 5);
      ++WorldGen.numLarva;
      int x3 = (int) vector2_1.X;
      int y3 = (int) vector2_1.Y;
      for (int index2 = x3 - 1; index2 <= x3 + 1 && (index2 > 0 && index2 < Main.maxTilesX); ++index2)
      {
        for (int index4 = y3 - 2; index4 <= y3 + 1 && (index4 > 0 && index4 < Main.maxTilesY); ++index4)
        {
          if (index4 != y3 + 1)
          {
            Main.tile[index2, index4].active(false);
          }
          else
          {
            Main.tile[index2, index4].active(true);
            Main.tile[index2, index4].type = (ushort) 225;
            Main.tile[index2, index4].slope((byte) 0);
            Main.tile[index2, index4].halfBrick(false);
          }
        }
      }
      structures.AddStructure(new Microsoft.Xna.Framework.Rectangle(origin.X - 50, origin.Y - 50, 100, 100), 5);
      return true;
    }
  }
}
