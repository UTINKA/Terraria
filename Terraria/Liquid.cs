// Decompiled with JetBrains decompiler
// Type: Terraria.Liquid
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Net;
using Terraria.ObjectData;

namespace Terraria
{
  public class Liquid
  {
    public static int skipCount = 0;
    public static int stuckCount = 0;
    public static int stuckAmount = 0;
    public static int cycles = 10;
    public static int resLiquid = 5000;
    public static int maxLiquid = 5000;
    public static bool stuck = false;
    public static bool quickFall = false;
    public static bool quickSettle = false;
    public static int panicCounter = 0;
    public static bool panicMode = false;
    public static int panicY = 0;
    private static HashSet<int> _netChangeSet = new HashSet<int>();
    private static HashSet<int> _swapNetChangeSet = new HashSet<int>();
    public static int numLiquid;
    private static int wetCounter;
    public int x;
    public int y;
    public int kill;
    public int delay;

    public static void NetSendLiquid(int x, int y)
    {
      if (WorldGen.gen)
        return;
      lock (Liquid._netChangeSet)
        Liquid._netChangeSet.Add((x & (int) ushort.MaxValue) << 16 | y & (int) ushort.MaxValue);
    }

    public static void ReInit()
    {
      Liquid.skipCount = 0;
      Liquid.stuckCount = 0;
      Liquid.stuckAmount = 0;
      Liquid.cycles = 10;
      Liquid.resLiquid = 5000;
      Liquid.maxLiquid = 5000;
      Liquid.numLiquid = 0;
      Liquid.stuck = false;
      Liquid.quickFall = false;
      Liquid.quickSettle = false;
      Liquid.wetCounter = 0;
      Liquid.panicCounter = 0;
      Liquid.panicMode = false;
      Liquid.panicY = 0;
    }

    public static double QuickWater(int verbose = 0, int minY = -1, int maxY = -1)
    {
      Main.tileSolid[379] = true;
      int num1 = 0;
      if (minY == -1)
        minY = 3;
      if (maxY == -1)
        maxY = Main.maxTilesY - 3;
      for (int index1 = maxY; index1 >= minY; --index1)
      {
        if (verbose > 0)
        {
          float num2 = (float) (maxY - index1) / (float) (maxY - minY + 1) / (float) verbose;
          Main.statusText = Lang.gen[27].Value + " " + (object) (int) ((double) num2 * 100.0 + 1.0) + "%";
        }
        else if (verbose < 0)
        {
          float num2 = (float) (maxY - index1) / (float) (maxY - minY + 1) / (float) -verbose;
          Main.statusText = Lang.gen[18].Value + " " + (object) (int) ((double) num2 * 100.0 + 1.0) + "%";
        }
        for (int index2 = 0; index2 < 2; ++index2)
        {
          int num2 = 2;
          int num3 = Main.maxTilesX - 2;
          int num4 = 1;
          if (index2 == 1)
          {
            num2 = Main.maxTilesX - 2;
            num3 = 2;
            num4 = -1;
          }
          int index3 = num2;
          while (index3 != num3)
          {
            Tile tile = Main.tile[index3, index1];
            if ((int) tile.liquid > 0)
            {
              int num5 = -num4;
              bool flag1 = false;
              int x = index3;
              int y = index1;
              byte num6 = tile.liquidType();
              bool flag2 = tile.lava();
              bool flag3 = tile.honey();
              byte liquid = tile.liquid;
              tile.liquid = (byte) 0;
              bool flag4 = true;
              int num7 = 0;
              while (flag4 && x > 3 && (x < Main.maxTilesX - 3 && y < Main.maxTilesY - 3))
              {
                flag4 = false;
                while ((int) Main.tile[x, y + 1].liquid == 0 && y < Main.maxTilesY - 5 && (!Main.tile[x, y + 1].nactive() || !Main.tileSolid[(int) Main.tile[x, y + 1].type] || Main.tileSolidTop[(int) Main.tile[x, y + 1].type]))
                {
                  flag1 = true;
                  num5 = num4;
                  num7 = 0;
                  flag4 = true;
                  ++y;
                  if (y > WorldGen.waterLine && WorldGen.gen && !flag3)
                    num6 = (byte) 1;
                }
                if ((int) Main.tile[x, y + 1].liquid > 0 && (int) Main.tile[x, y + 1].liquid < (int) byte.MaxValue && (int) Main.tile[x, y + 1].liquidType() == (int) num6)
                {
                  int num8 = (int) byte.MaxValue - (int) Main.tile[x, y + 1].liquid;
                  if (num8 > (int) liquid)
                    num8 = (int) liquid;
                  Main.tile[x, y + 1].liquid += (byte) num8;
                  liquid -= (byte) num8;
                  if ((int) liquid == 0)
                  {
                    ++num1;
                    break;
                  }
                }
                if (num7 == 0)
                {
                  if ((int) Main.tile[x + num5, y].liquid == 0 && (!Main.tile[x + num5, y].nactive() || !Main.tileSolid[(int) Main.tile[x + num5, y].type] || Main.tileSolidTop[(int) Main.tile[x + num5, y].type]))
                    num7 = num5;
                  else if ((int) Main.tile[x - num5, y].liquid == 0 && (!Main.tile[x - num5, y].nactive() || !Main.tileSolid[(int) Main.tile[x - num5, y].type] || Main.tileSolidTop[(int) Main.tile[x - num5, y].type]))
                    num7 = -num5;
                }
                if (num7 != 0 && (int) Main.tile[x + num7, y].liquid == 0 && (!Main.tile[x + num7, y].nactive() || !Main.tileSolid[(int) Main.tile[x + num7, y].type] || Main.tileSolidTop[(int) Main.tile[x + num7, y].type]))
                {
                  flag4 = true;
                  x += num7;
                }
                if (flag1 && !flag4)
                {
                  flag1 = false;
                  flag4 = true;
                  num5 = -num4;
                  num7 = 0;
                }
              }
              if (index3 != x && index1 != y)
                ++num1;
              Main.tile[x, y].liquid = liquid;
              Main.tile[x, y].liquidType((int) num6);
              if ((int) Main.tile[x - 1, y].liquid > 0 && Main.tile[x - 1, y].lava() != flag2)
              {
                if (flag2)
                  Liquid.LavaCheck(x, y);
                else
                  Liquid.LavaCheck(x - 1, y);
              }
              else if ((int) Main.tile[x + 1, y].liquid > 0 && Main.tile[x + 1, y].lava() != flag2)
              {
                if (flag2)
                  Liquid.LavaCheck(x, y);
                else
                  Liquid.LavaCheck(x + 1, y);
              }
              else if ((int) Main.tile[x, y - 1].liquid > 0 && Main.tile[x, y - 1].lava() != flag2)
              {
                if (flag2)
                  Liquid.LavaCheck(x, y);
                else
                  Liquid.LavaCheck(x, y - 1);
              }
              else if ((int) Main.tile[x, y + 1].liquid > 0 && Main.tile[x, y + 1].lava() != flag2)
              {
                if (flag2)
                  Liquid.LavaCheck(x, y);
                else
                  Liquid.LavaCheck(x, y + 1);
              }
              if ((int) Main.tile[x, y].liquid > 0)
              {
                if ((int) Main.tile[x - 1, y].liquid > 0 && Main.tile[x - 1, y].honey() != flag3)
                {
                  if (flag3)
                    Liquid.HoneyCheck(x, y);
                  else
                    Liquid.HoneyCheck(x - 1, y);
                }
                else if ((int) Main.tile[x + 1, y].liquid > 0 && Main.tile[x + 1, y].honey() != flag3)
                {
                  if (flag3)
                    Liquid.HoneyCheck(x, y);
                  else
                    Liquid.HoneyCheck(x + 1, y);
                }
                else if ((int) Main.tile[x, y - 1].liquid > 0 && Main.tile[x, y - 1].honey() != flag3)
                {
                  if (flag3)
                    Liquid.HoneyCheck(x, y);
                  else
                    Liquid.HoneyCheck(x, y - 1);
                }
                else if ((int) Main.tile[x, y + 1].liquid > 0 && Main.tile[x, y + 1].honey() != flag3)
                {
                  if (flag3)
                    Liquid.HoneyCheck(x, y);
                  else
                    Liquid.HoneyCheck(x, y + 1);
                }
              }
            }
            index3 += num4;
          }
        }
      }
      return (double) num1;
    }

    public void Update()
    {
      Main.tileSolid[379] = true;
      Tile tile1 = Main.tile[this.x - 1, this.y];
      Tile tile2 = Main.tile[this.x + 1, this.y];
      Tile tile3 = Main.tile[this.x, this.y - 1];
      Tile tile4 = Main.tile[this.x, this.y + 1];
      Tile tile5 = Main.tile[this.x, this.y];
      if (tile5.nactive() && Main.tileSolid[(int) tile5.type] && !Main.tileSolidTop[(int) tile5.type])
      {
        int type = (int) tile5.type;
        this.kill = 9;
      }
      else
      {
        byte liquid = tile5.liquid;
        if (this.y > Main.maxTilesY - 200 && (int) tile5.liquidType() == 0 && (int) tile5.liquid > 0)
        {
          byte num = 2;
          if ((int) tile5.liquid < (int) num)
            num = tile5.liquid;
          tile5.liquid -= num;
        }
        if ((int) tile5.liquid == 0)
        {
          this.kill = 9;
        }
        else
        {
          if (tile5.lava())
          {
            Liquid.LavaCheck(this.x, this.y);
            if (!Liquid.quickFall)
            {
              if (this.delay < 5)
              {
                this.delay = this.delay + 1;
                return;
              }
              this.delay = 0;
            }
          }
          else
          {
            if (tile1.lava())
              Liquid.AddWater(this.x - 1, this.y);
            if (tile2.lava())
              Liquid.AddWater(this.x + 1, this.y);
            if (tile3.lava())
              Liquid.AddWater(this.x, this.y - 1);
            if (tile4.lava())
              Liquid.AddWater(this.x, this.y + 1);
            if (tile5.honey())
            {
              Liquid.HoneyCheck(this.x, this.y);
              if (!Liquid.quickFall)
              {
                if (this.delay < 10)
                {
                  this.delay = this.delay + 1;
                  return;
                }
                this.delay = 0;
              }
            }
            else
            {
              if (tile1.honey())
                Liquid.AddWater(this.x - 1, this.y);
              if (tile2.honey())
                Liquid.AddWater(this.x + 1, this.y);
              if (tile3.honey())
                Liquid.AddWater(this.x, this.y - 1);
              if (tile4.honey())
                Liquid.AddWater(this.x, this.y + 1);
            }
          }
          if ((!tile4.nactive() || !Main.tileSolid[(int) tile4.type] || Main.tileSolidTop[(int) tile4.type]) && (((int) tile4.liquid <= 0 || (int) tile4.liquidType() == (int) tile5.liquidType()) && (int) tile4.liquid < (int) byte.MaxValue))
          {
            float num = (float) ((int) byte.MaxValue - (int) tile4.liquid);
            if ((double) num > (double) tile5.liquid)
              num = (float) tile5.liquid;
            tile5.liquid -= (byte) num;
            tile4.liquid += (byte) num;
            tile4.liquidType((int) tile5.liquidType());
            Liquid.AddWater(this.x, this.y + 1);
            tile4.skipLiquid(true);
            tile5.skipLiquid(true);
            if ((int) tile5.liquid > 250)
            {
              tile5.liquid = byte.MaxValue;
            }
            else
            {
              Liquid.AddWater(this.x - 1, this.y);
              Liquid.AddWater(this.x + 1, this.y);
            }
          }
          if ((int) tile5.liquid > 0)
          {
            bool flag1 = true;
            bool flag2 = true;
            bool flag3 = true;
            bool flag4 = true;
            if (tile1.nactive() && Main.tileSolid[(int) tile1.type] && !Main.tileSolidTop[(int) tile1.type])
              flag1 = false;
            else if ((int) tile1.liquid > 0 && (int) tile1.liquidType() != (int) tile5.liquidType())
              flag1 = false;
            else if (Main.tile[this.x - 2, this.y].nactive() && Main.tileSolid[(int) Main.tile[this.x - 2, this.y].type] && !Main.tileSolidTop[(int) Main.tile[this.x - 2, this.y].type])
              flag3 = false;
            else if ((int) Main.tile[this.x - 2, this.y].liquid == 0)
              flag3 = false;
            else if ((int) Main.tile[this.x - 2, this.y].liquid > 0 && (int) Main.tile[this.x - 2, this.y].liquidType() != (int) tile5.liquidType())
              flag3 = false;
            if (tile2.nactive() && Main.tileSolid[(int) tile2.type] && !Main.tileSolidTop[(int) tile2.type])
              flag2 = false;
            else if ((int) tile2.liquid > 0 && (int) tile2.liquidType() != (int) tile5.liquidType())
              flag2 = false;
            else if (Main.tile[this.x + 2, this.y].nactive() && Main.tileSolid[(int) Main.tile[this.x + 2, this.y].type] && !Main.tileSolidTop[(int) Main.tile[this.x + 2, this.y].type])
              flag4 = false;
            else if ((int) Main.tile[this.x + 2, this.y].liquid == 0)
              flag4 = false;
            else if ((int) Main.tile[this.x + 2, this.y].liquid > 0 && (int) Main.tile[this.x + 2, this.y].liquidType() != (int) tile5.liquidType())
              flag4 = false;
            int num1 = 0;
            if ((int) tile5.liquid < 3)
              num1 = -1;
            if (flag1 & flag2)
            {
              if (flag3 & flag4)
              {
                bool flag5 = true;
                bool flag6 = true;
                if (Main.tile[this.x - 3, this.y].nactive() && Main.tileSolid[(int) Main.tile[this.x - 3, this.y].type] && !Main.tileSolidTop[(int) Main.tile[this.x - 3, this.y].type])
                  flag5 = false;
                else if ((int) Main.tile[this.x - 3, this.y].liquid == 0)
                  flag5 = false;
                else if ((int) Main.tile[this.x - 3, this.y].liquidType() != (int) tile5.liquidType())
                  flag5 = false;
                if (Main.tile[this.x + 3, this.y].nactive() && Main.tileSolid[(int) Main.tile[this.x + 3, this.y].type] && !Main.tileSolidTop[(int) Main.tile[this.x + 3, this.y].type])
                  flag6 = false;
                else if ((int) Main.tile[this.x + 3, this.y].liquid == 0)
                  flag6 = false;
                else if ((int) Main.tile[this.x + 3, this.y].liquidType() != (int) tile5.liquidType())
                  flag6 = false;
                if (flag5 & flag6)
                {
                  float num2 = (float) Math.Round((double) ((int) tile1.liquid + (int) tile2.liquid + (int) Main.tile[this.x - 2, this.y].liquid + (int) Main.tile[this.x + 2, this.y].liquid + (int) Main.tile[this.x - 3, this.y].liquid + (int) Main.tile[this.x + 3, this.y].liquid + (int) tile5.liquid + num1) / 7.0);
                  int num3 = 0;
                  tile1.liquidType((int) tile5.liquidType());
                  if ((int) tile1.liquid != (int) (byte) num2)
                  {
                    tile1.liquid = (byte) num2;
                    Liquid.AddWater(this.x - 1, this.y);
                  }
                  else
                    ++num3;
                  tile2.liquidType((int) tile5.liquidType());
                  if ((int) tile2.liquid != (int) (byte) num2)
                  {
                    tile2.liquid = (byte) num2;
                    Liquid.AddWater(this.x + 1, this.y);
                  }
                  else
                    ++num3;
                  Main.tile[this.x - 2, this.y].liquidType((int) tile5.liquidType());
                  if ((int) Main.tile[this.x - 2, this.y].liquid != (int) (byte) num2)
                  {
                    Main.tile[this.x - 2, this.y].liquid = (byte) num2;
                    Liquid.AddWater(this.x - 2, this.y);
                  }
                  else
                    ++num3;
                  Main.tile[this.x + 2, this.y].liquidType((int) tile5.liquidType());
                  if ((int) Main.tile[this.x + 2, this.y].liquid != (int) (byte) num2)
                  {
                    Main.tile[this.x + 2, this.y].liquid = (byte) num2;
                    Liquid.AddWater(this.x + 2, this.y);
                  }
                  else
                    ++num3;
                  Main.tile[this.x - 3, this.y].liquidType((int) tile5.liquidType());
                  if ((int) Main.tile[this.x - 3, this.y].liquid != (int) (byte) num2)
                  {
                    Main.tile[this.x - 3, this.y].liquid = (byte) num2;
                    Liquid.AddWater(this.x - 3, this.y);
                  }
                  else
                    ++num3;
                  Main.tile[this.x + 3, this.y].liquidType((int) tile5.liquidType());
                  if ((int) Main.tile[this.x + 3, this.y].liquid != (int) (byte) num2)
                  {
                    Main.tile[this.x + 3, this.y].liquid = (byte) num2;
                    Liquid.AddWater(this.x + 3, this.y);
                  }
                  else
                    ++num3;
                  if ((int) tile1.liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                    Liquid.AddWater(this.x - 1, this.y);
                  if ((int) tile2.liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                    Liquid.AddWater(this.x + 1, this.y);
                  if ((int) Main.tile[this.x - 2, this.y].liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                    Liquid.AddWater(this.x - 2, this.y);
                  if ((int) Main.tile[this.x + 2, this.y].liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                    Liquid.AddWater(this.x + 2, this.y);
                  if ((int) Main.tile[this.x - 3, this.y].liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                    Liquid.AddWater(this.x - 3, this.y);
                  if ((int) Main.tile[this.x + 3, this.y].liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                    Liquid.AddWater(this.x + 3, this.y);
                  if (num3 != 6 || (int) tile3.liquid <= 0)
                    tile5.liquid = (byte) num2;
                }
                else
                {
                  int num2 = 0;
                  float num3 = (float) Math.Round((double) ((int) tile1.liquid + (int) tile2.liquid + (int) Main.tile[this.x - 2, this.y].liquid + (int) Main.tile[this.x + 2, this.y].liquid + (int) tile5.liquid + num1) / 5.0);
                  tile1.liquidType((int) tile5.liquidType());
                  if ((int) tile1.liquid != (int) (byte) num3)
                  {
                    tile1.liquid = (byte) num3;
                    Liquid.AddWater(this.x - 1, this.y);
                  }
                  else
                    ++num2;
                  tile2.liquidType((int) tile5.liquidType());
                  if ((int) tile2.liquid != (int) (byte) num3)
                  {
                    tile2.liquid = (byte) num3;
                    Liquid.AddWater(this.x + 1, this.y);
                  }
                  else
                    ++num2;
                  Main.tile[this.x - 2, this.y].liquidType((int) tile5.liquidType());
                  if ((int) Main.tile[this.x - 2, this.y].liquid != (int) (byte) num3)
                  {
                    Main.tile[this.x - 2, this.y].liquid = (byte) num3;
                    Liquid.AddWater(this.x - 2, this.y);
                  }
                  else
                    ++num2;
                  Main.tile[this.x + 2, this.y].liquidType((int) tile5.liquidType());
                  if ((int) Main.tile[this.x + 2, this.y].liquid != (int) (byte) num3)
                  {
                    Main.tile[this.x + 2, this.y].liquid = (byte) num3;
                    Liquid.AddWater(this.x + 2, this.y);
                  }
                  else
                    ++num2;
                  if ((int) tile1.liquid != (int) (byte) num3 || (int) tile5.liquid != (int) (byte) num3)
                    Liquid.AddWater(this.x - 1, this.y);
                  if ((int) tile2.liquid != (int) (byte) num3 || (int) tile5.liquid != (int) (byte) num3)
                    Liquid.AddWater(this.x + 1, this.y);
                  if ((int) Main.tile[this.x - 2, this.y].liquid != (int) (byte) num3 || (int) tile5.liquid != (int) (byte) num3)
                    Liquid.AddWater(this.x - 2, this.y);
                  if ((int) Main.tile[this.x + 2, this.y].liquid != (int) (byte) num3 || (int) tile5.liquid != (int) (byte) num3)
                    Liquid.AddWater(this.x + 2, this.y);
                  if (num2 != 4 || (int) tile3.liquid <= 0)
                    tile5.liquid = (byte) num3;
                }
              }
              else if (flag3)
              {
                float num2 = (float) Math.Round((double) ((int) tile1.liquid + (int) tile2.liquid + (int) Main.tile[this.x - 2, this.y].liquid + (int) tile5.liquid + num1) / 4.0 + 0.001);
                tile1.liquidType((int) tile5.liquidType());
                if ((int) tile1.liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                {
                  tile1.liquid = (byte) num2;
                  Liquid.AddWater(this.x - 1, this.y);
                }
                tile2.liquidType((int) tile5.liquidType());
                if ((int) tile2.liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                {
                  tile2.liquid = (byte) num2;
                  Liquid.AddWater(this.x + 1, this.y);
                }
                Main.tile[this.x - 2, this.y].liquidType((int) tile5.liquidType());
                if ((int) Main.tile[this.x - 2, this.y].liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                {
                  Main.tile[this.x - 2, this.y].liquid = (byte) num2;
                  Liquid.AddWater(this.x - 2, this.y);
                }
                tile5.liquid = (byte) num2;
              }
              else if (flag4)
              {
                float num2 = (float) Math.Round((double) ((int) tile1.liquid + (int) tile2.liquid + (int) Main.tile[this.x + 2, this.y].liquid + (int) tile5.liquid + num1) / 4.0 + 0.001);
                tile1.liquidType((int) tile5.liquidType());
                if ((int) tile1.liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                {
                  tile1.liquid = (byte) num2;
                  Liquid.AddWater(this.x - 1, this.y);
                }
                tile2.liquidType((int) tile5.liquidType());
                if ((int) tile2.liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                {
                  tile2.liquid = (byte) num2;
                  Liquid.AddWater(this.x + 1, this.y);
                }
                Main.tile[this.x + 2, this.y].liquidType((int) tile5.liquidType());
                if ((int) Main.tile[this.x + 2, this.y].liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                {
                  Main.tile[this.x + 2, this.y].liquid = (byte) num2;
                  Liquid.AddWater(this.x + 2, this.y);
                }
                tile5.liquid = (byte) num2;
              }
              else
              {
                float num2 = (float) Math.Round((double) ((int) tile1.liquid + (int) tile2.liquid + (int) tile5.liquid + num1) / 3.0 + 0.001);
                tile1.liquidType((int) tile5.liquidType());
                if ((int) tile1.liquid != (int) (byte) num2)
                  tile1.liquid = (byte) num2;
                if ((int) tile5.liquid != (int) (byte) num2 || (int) tile1.liquid != (int) (byte) num2)
                  Liquid.AddWater(this.x - 1, this.y);
                tile2.liquidType((int) tile5.liquidType());
                if ((int) tile2.liquid != (int) (byte) num2)
                  tile2.liquid = (byte) num2;
                if ((int) tile5.liquid != (int) (byte) num2 || (int) tile2.liquid != (int) (byte) num2)
                  Liquid.AddWater(this.x + 1, this.y);
                tile5.liquid = (byte) num2;
              }
            }
            else if (flag1)
            {
              float num2 = (float) Math.Round((double) ((int) tile1.liquid + (int) tile5.liquid + num1) / 2.0 + 0.001);
              if ((int) tile1.liquid != (int) (byte) num2)
                tile1.liquid = (byte) num2;
              tile1.liquidType((int) tile5.liquidType());
              if ((int) tile5.liquid != (int) (byte) num2 || (int) tile1.liquid != (int) (byte) num2)
                Liquid.AddWater(this.x - 1, this.y);
              tile5.liquid = (byte) num2;
            }
            else if (flag2)
            {
              float num2 = (float) Math.Round((double) ((int) tile2.liquid + (int) tile5.liquid + num1) / 2.0 + 0.001);
              if ((int) tile2.liquid != (int) (byte) num2)
                tile2.liquid = (byte) num2;
              tile2.liquidType((int) tile5.liquidType());
              if ((int) tile5.liquid != (int) (byte) num2 || (int) tile2.liquid != (int) (byte) num2)
                Liquid.AddWater(this.x + 1, this.y);
              tile5.liquid = (byte) num2;
            }
          }
          if ((int) tile5.liquid != (int) liquid)
          {
            if ((int) tile5.liquid == 254 && (int) liquid == (int) byte.MaxValue)
            {
              tile5.liquid = byte.MaxValue;
              this.kill = this.kill + 1;
            }
            else
            {
              Liquid.AddWater(this.x, this.y - 1);
              this.kill = 0;
            }
          }
          else
            this.kill = this.kill + 1;
        }
      }
    }

    public static void StartPanic()
    {
      if (Liquid.panicMode)
        return;
      WorldGen.waterLine = Main.maxTilesY;
      Liquid.numLiquid = 0;
      LiquidBuffer.numLiquidBuffer = 0;
      Liquid.panicCounter = 0;
      Liquid.panicMode = true;
      Liquid.panicY = Main.maxTilesY - 3;
      if (!Main.dedServ)
        return;
      Console.WriteLine(Language.GetTextValue("Misc.ForceWaterSettling"));
    }

    public static void UpdateLiquid()
    {
      int netMode1 = Main.netMode;
      if (!WorldGen.gen)
      {
        if (!Liquid.panicMode)
        {
          if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > 4000)
          {
            ++Liquid.panicCounter;
            if (Liquid.panicCounter > 1800 || Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > 13500)
              Liquid.StartPanic();
          }
          else
            Liquid.panicCounter = 0;
        }
        if (Liquid.panicMode)
        {
          int num = 0;
          while (Liquid.panicY >= 3 && num < 5)
          {
            ++num;
            Liquid.QuickWater(0, Liquid.panicY, Liquid.panicY);
            --Liquid.panicY;
            if (Liquid.panicY < 3)
            {
              Console.WriteLine(Language.GetTextValue("Misc.WaterSettled"));
              Liquid.panicCounter = 0;
              Liquid.panicMode = false;
              WorldGen.WaterCheck();
              if (Main.netMode == 2)
              {
                for (int index1 = 0; index1 < (int) byte.MaxValue; ++index1)
                {
                  for (int index2 = 0; index2 < Main.maxSectionsX; ++index2)
                  {
                    for (int index3 = 0; index3 < Main.maxSectionsY; ++index3)
                      Netplay.Clients[index1].TileSections[index2, index3] = false;
                  }
                }
              }
            }
          }
          return;
        }
      }
      Liquid.quickFall = Liquid.quickSettle || Liquid.numLiquid > 2000;
      ++Liquid.wetCounter;
      int num1 = Liquid.maxLiquid / Liquid.cycles;
      int num2 = Liquid.wetCounter - 1;
      int num3 = num1 * num2;
      int wetCounter = Liquid.wetCounter;
      int num4 = num1 * wetCounter;
      if (Liquid.wetCounter == Liquid.cycles)
        num4 = Liquid.numLiquid;
      if (num4 > Liquid.numLiquid)
      {
        num4 = Liquid.numLiquid;
        int netMode2 = Main.netMode;
        Liquid.wetCounter = Liquid.cycles;
      }
      if (Liquid.quickFall)
      {
        for (int index = num3; index < num4; ++index)
        {
          Main.liquid[index].delay = 10;
          Main.liquid[index].Update();
          Main.tile[Main.liquid[index].x, Main.liquid[index].y].skipLiquid(false);
        }
      }
      else
      {
        for (int index = num3; index < num4; ++index)
        {
          if (!Main.tile[Main.liquid[index].x, Main.liquid[index].y].skipLiquid())
            Main.liquid[index].Update();
          else
            Main.tile[Main.liquid[index].x, Main.liquid[index].y].skipLiquid(false);
        }
      }
      if (Liquid.wetCounter >= Liquid.cycles)
      {
        Liquid.wetCounter = 0;
        for (int l = Liquid.numLiquid - 1; l >= 0; --l)
        {
          if (Main.liquid[l].kill > 4)
            Liquid.DelWater(l);
        }
        int num5 = Liquid.maxLiquid - (Liquid.maxLiquid - Liquid.numLiquid);
        if (num5 > LiquidBuffer.numLiquidBuffer)
          num5 = LiquidBuffer.numLiquidBuffer;
        for (int index = 0; index < num5; ++index)
        {
          Main.tile[Main.liquidBuffer[0].x, Main.liquidBuffer[0].y].checkingLiquid(false);
          Liquid.AddWater(Main.liquidBuffer[0].x, Main.liquidBuffer[0].y);
          LiquidBuffer.DelBuffer(0);
        }
        if (Liquid.numLiquid > 0 && Liquid.numLiquid > Liquid.stuckAmount - 50 && Liquid.numLiquid < Liquid.stuckAmount + 50)
        {
          ++Liquid.stuckCount;
          if (Liquid.stuckCount >= 10000)
          {
            Liquid.stuck = true;
            for (int l = Liquid.numLiquid - 1; l >= 0; --l)
              Liquid.DelWater(l);
            Liquid.stuck = false;
            Liquid.stuckCount = 0;
          }
        }
        else
        {
          Liquid.stuckCount = 0;
          Liquid.stuckAmount = Liquid.numLiquid;
        }
      }
      if (WorldGen.gen || Main.netMode != 2 || Liquid._netChangeSet.Count <= 0)
        return;
      Utils.Swap<HashSet<int>>(ref Liquid._netChangeSet, ref Liquid._swapNetChangeSet);
      NetManager.Instance.Broadcast(NetLiquidModule.Serialize(Liquid._swapNetChangeSet), -1);
      Liquid._swapNetChangeSet.Clear();
    }

    public static void AddWater(int x, int y)
    {
      Tile checkTile = Main.tile[x, y];
      if (Main.tile[x, y] == null || checkTile.checkingLiquid() || (x >= Main.maxTilesX - 5 || y >= Main.maxTilesY - 5) || (x < 5 || y < 5 || (int) checkTile.liquid == 0))
        return;
      if (Liquid.numLiquid >= Liquid.maxLiquid - 1)
      {
        LiquidBuffer.AddBuffer(x, y);
      }
      else
      {
        checkTile.checkingLiquid(true);
        Main.liquid[Liquid.numLiquid].kill = 0;
        Main.liquid[Liquid.numLiquid].x = x;
        Main.liquid[Liquid.numLiquid].y = y;
        Main.liquid[Liquid.numLiquid].delay = 0;
        checkTile.skipLiquid(false);
        ++Liquid.numLiquid;
        if (Main.netMode == 2)
          Liquid.NetSendLiquid(x, y);
        if (!checkTile.active() || WorldGen.gen)
          return;
        bool flag = false;
        if (checkTile.lava())
        {
          if (TileObjectData.CheckLavaDeath(checkTile))
            flag = true;
        }
        else if (TileObjectData.CheckWaterDeath(checkTile))
          flag = true;
        if (!flag)
          return;
        WorldGen.KillTile(x, y, false, false, false);
        if (Main.netMode != 2)
          return;
        NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) y, 0.0f, 0, 0, 0);
      }
    }

    public static void LavaCheck(int x, int y)
    {
      Tile tile1 = Main.tile[x - 1, y];
      Tile tile2 = Main.tile[x + 1, y];
      Tile tile3 = Main.tile[x, y - 1];
      Tile tile4 = Main.tile[x, y + 1];
      Tile tile5 = Main.tile[x, y];
      if ((int) tile1.liquid > 0 && !tile1.lava() || (int) tile2.liquid > 0 && !tile2.lava() || (int) tile3.liquid > 0 && !tile3.lava())
      {
        int num = 0;
        int type = 56;
        if (!tile1.lava())
        {
          num += (int) tile1.liquid;
          tile1.liquid = (byte) 0;
        }
        if (!tile2.lava())
        {
          num += (int) tile2.liquid;
          tile2.liquid = (byte) 0;
        }
        if (!tile3.lava())
        {
          num += (int) tile3.liquid;
          tile3.liquid = (byte) 0;
        }
        if (tile1.honey() || tile2.honey() || tile3.honey())
          type = 230;
        if (num < 24)
          return;
        if (tile5.active() && Main.tileObsidianKill[(int) tile5.type])
        {
          WorldGen.KillTile(x, y, false, false, false);
          if (Main.netMode == 2)
            NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) y, 0.0f, 0, 0, 0);
        }
        if (tile5.active())
          return;
        tile5.liquid = (byte) 0;
        tile5.lava(false);
        if (type == 56)
          Main.PlaySound(SoundID.LiquidsWaterLava, new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)));
        else
          Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)));
        WorldGen.PlaceTile(x, y, type, true, true, -1, 0);
        WorldGen.SquareTileFrame(x, y, true);
        if (Main.netMode != 2)
          return;
        NetMessage.SendTileSquare(-1, x - 1, y - 1, 3, type == 56 ? TileChangeType.LavaWater : TileChangeType.HoneyLava);
      }
      else
      {
        if ((int) tile4.liquid <= 0 || tile4.lava())
          return;
        bool flag = false;
        if (tile5.active() && TileID.Sets.ForceObsidianKill[(int) tile5.type] && !TileID.Sets.ForceObsidianKill[(int) tile4.type])
          flag = true;
        if (Main.tileCut[(int) tile4.type])
        {
          WorldGen.KillTile(x, y + 1, false, false, false);
          if (Main.netMode == 2)
            NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) (y + 1), 0.0f, 0, 0, 0);
        }
        else if (tile4.active() && Main.tileObsidianKill[(int) tile4.type])
        {
          WorldGen.KillTile(x, y + 1, false, false, false);
          if (Main.netMode == 2)
            NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) (y + 1), 0.0f, 0, 0, 0);
        }
        if (!(!tile4.active() | flag))
          return;
        if ((int) tile5.liquid < 24)
        {
          tile5.liquid = (byte) 0;
          tile5.liquidType(0);
          if (Main.netMode != 2)
            return;
          NetMessage.SendTileSquare(-1, x - 1, y, 3, TileChangeType.None);
        }
        else
        {
          int type = 56;
          if (tile4.honey())
            type = 230;
          tile5.liquid = (byte) 0;
          tile5.lava(false);
          tile4.liquid = (byte) 0;
          if (type == 56)
            Main.PlaySound(SoundID.LiquidsWaterLava, new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)));
          else
            Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)));
          WorldGen.PlaceTile(x, y + 1, type, true, true, -1, 0);
          WorldGen.SquareTileFrame(x, y + 1, true);
          if (Main.netMode != 2)
            return;
          NetMessage.SendTileSquare(-1, x - 1, y, 3, type == 56 ? TileChangeType.LavaWater : TileChangeType.HoneyLava);
        }
      }
    }

    public static void HoneyCheck(int x, int y)
    {
      Tile tile1 = Main.tile[x - 1, y];
      Tile tile2 = Main.tile[x + 1, y];
      Tile tile3 = Main.tile[x, y - 1];
      Tile tile4 = Main.tile[x, y + 1];
      Tile tile5 = Main.tile[x, y];
      bool flag = false;
      if ((int) tile1.liquid > 0 && (int) tile1.liquidType() == 0 || (int) tile2.liquid > 0 && (int) tile2.liquidType() == 0 || (int) tile3.liquid > 0 && (int) tile3.liquidType() == 0)
      {
        int num = 0;
        if ((int) tile1.liquidType() == 0)
        {
          num += (int) tile1.liquid;
          tile1.liquid = (byte) 0;
        }
        if ((int) tile2.liquidType() == 0)
        {
          num += (int) tile2.liquid;
          tile2.liquid = (byte) 0;
        }
        if ((int) tile3.liquidType() == 0)
        {
          num += (int) tile3.liquid;
          tile3.liquid = (byte) 0;
        }
        if (tile1.lava() || tile2.lava() || tile3.lava())
          flag = true;
        if (num < 32)
          return;
        if (tile5.active() && Main.tileObsidianKill[(int) tile5.type])
        {
          WorldGen.KillTile(x, y, false, false, false);
          if (Main.netMode == 2)
            NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) y, 0.0f, 0, 0, 0);
        }
        if (tile5.active())
          return;
        tile5.liquid = (byte) 0;
        tile5.liquidType(0);
        WorldGen.PlaceTile(x, y, 229, true, true, -1, 0);
        if (flag)
          Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)));
        else
          Main.PlaySound(SoundID.LiquidsHoneyWater, new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)));
        WorldGen.SquareTileFrame(x, y, true);
        if (Main.netMode != 2)
          return;
        NetMessage.SendTileSquare(-1, x - 1, y - 1, 3, flag ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
      }
      else
      {
        if ((int) tile4.liquid <= 0 || (int) tile4.liquidType() != 0)
          return;
        if (Main.tileCut[(int) tile4.type])
        {
          WorldGen.KillTile(x, y + 1, false, false, false);
          if (Main.netMode == 2)
            NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) (y + 1), 0.0f, 0, 0, 0);
        }
        else if (tile4.active() && Main.tileObsidianKill[(int) tile4.type])
        {
          WorldGen.KillTile(x, y + 1, false, false, false);
          if (Main.netMode == 2)
            NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) (y + 1), 0.0f, 0, 0, 0);
        }
        if (tile4.active())
          return;
        if ((int) tile5.liquid < 32)
        {
          tile5.liquid = (byte) 0;
          tile5.liquidType(0);
          if (Main.netMode != 2)
            return;
          NetMessage.SendTileSquare(-1, x - 1, y, 3, TileChangeType.None);
        }
        else
        {
          if (tile4.lava())
            flag = true;
          tile5.liquid = (byte) 0;
          tile5.liquidType(0);
          tile4.liquid = (byte) 0;
          tile4.liquidType(0);
          if (flag)
            Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)));
          else
            Main.PlaySound(SoundID.LiquidsHoneyWater, new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)));
          WorldGen.PlaceTile(x, y + 1, 229, true, true, -1, 0);
          WorldGen.SquareTileFrame(x, y + 1, true);
          if (Main.netMode != 2)
            return;
          NetMessage.SendTileSquare(-1, x - 1, y, 3, flag ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
        }
      }
    }

    public static void DelWater(int l)
    {
      int x = Main.liquid[l].x;
      int y = Main.liquid[l].y;
      Tile tile1 = Main.tile[x - 1, y];
      Tile tile2 = Main.tile[x + 1, y];
      Tile tile3 = Main.tile[x, y + 1];
      Tile tile4 = Main.tile[x, y];
      byte num = 2;
      if ((int) tile4.liquid < (int) num)
      {
        tile4.liquid = (byte) 0;
        if ((int) tile1.liquid < (int) num)
          tile1.liquid = (byte) 0;
        else
          Liquid.AddWater(x - 1, y);
        if ((int) tile2.liquid < (int) num)
          tile2.liquid = (byte) 0;
        else
          Liquid.AddWater(x + 1, y);
      }
      else if ((int) tile4.liquid < 20)
      {
        if ((int) tile1.liquid < (int) tile4.liquid && (!tile1.nactive() || !Main.tileSolid[(int) tile1.type] || Main.tileSolidTop[(int) tile1.type]) || (int) tile2.liquid < (int) tile4.liquid && (!tile2.nactive() || !Main.tileSolid[(int) tile2.type] || Main.tileSolidTop[(int) tile2.type]) || (int) tile3.liquid < (int) byte.MaxValue && (!tile3.nactive() || !Main.tileSolid[(int) tile3.type] || Main.tileSolidTop[(int) tile3.type]))
          tile4.liquid = (byte) 0;
      }
      else if ((int) tile3.liquid < (int) byte.MaxValue && (!tile3.nactive() || !Main.tileSolid[(int) tile3.type] || Main.tileSolidTop[(int) tile3.type]) && !Liquid.stuck)
      {
        Main.liquid[l].kill = 0;
        return;
      }
      if ((int) tile4.liquid < 250 && (int) Main.tile[x, y - 1].liquid > 0)
        Liquid.AddWater(x, y - 1);
      if ((int) tile4.liquid == 0)
      {
        tile4.liquidType(0);
      }
      else
      {
        if ((int) tile2.liquid > 0 && (int) Main.tile[x + 1, y + 1].liquid < 250 && !Main.tile[x + 1, y + 1].active() || (int) tile1.liquid > 0 && (int) Main.tile[x - 1, y + 1].liquid < 250 && !Main.tile[x - 1, y + 1].active())
        {
          Liquid.AddWater(x - 1, y);
          Liquid.AddWater(x + 1, y);
        }
        if (tile4.lava())
        {
          Liquid.LavaCheck(x, y);
          for (int i = x - 1; i <= x + 1; ++i)
          {
            for (int j = y - 1; j <= y + 1; ++j)
            {
              Tile tile5 = Main.tile[i, j];
              if (tile5.active())
              {
                if ((int) tile5.type == 2 || (int) tile5.type == 23 || ((int) tile5.type == 109 || (int) tile5.type == 199))
                {
                  tile5.type = (ushort) 0;
                  WorldGen.SquareTileFrame(i, j, true);
                  if (Main.netMode == 2)
                    NetMessage.SendTileSquare(-1, x, y, 3, TileChangeType.None);
                }
                else if ((int) tile5.type == 60 || (int) tile5.type == 70)
                {
                  tile5.type = (ushort) 59;
                  WorldGen.SquareTileFrame(i, j, true);
                  if (Main.netMode == 2)
                    NetMessage.SendTileSquare(-1, x, y, 3, TileChangeType.None);
                }
              }
            }
          }
        }
        else if (tile4.honey())
          Liquid.HoneyCheck(x, y);
      }
      if (Main.netMode == 2)
        Liquid.NetSendLiquid(x, y);
      --Liquid.numLiquid;
      Main.tile[Main.liquid[l].x, Main.liquid[l].y].checkingLiquid(false);
      Main.liquid[l].x = Main.liquid[Liquid.numLiquid].x;
      Main.liquid[l].y = Main.liquid[Liquid.numLiquid].y;
      Main.liquid[l].kill = Main.liquid[Liquid.numLiquid].kill;
      if (!Main.tileAlch[(int) tile4.type])
        return;
      WorldGen.CheckAlch(x, y);
    }
  }
}
