// Decompiled with JetBrains decompiler
// Type: Terraria.Wiring
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria
{
  public static class Wiring
  {
    private static int CurrentUser = 254;
    private const int MaxPump = 20;
    private const int MaxMech = 1000;
    public static bool blockPlayerTeleportationForOneIteration;
    public static bool running;
    private static Dictionary<Point16, bool> _wireSkip;
    private static DoubleStack<Point16> _wireList;
    private static DoubleStack<byte> _wireDirectionList;
    private static Dictionary<Point16, byte> _toProcess;
    private static Queue<Point16> _GatesCurrent;
    private static Queue<Point16> _LampsToCheck;
    private static Queue<Point16> _GatesNext;
    private static Dictionary<Point16, bool> _GatesDone;
    private static Dictionary<Point16, byte> _PixelBoxTriggers;
    private static Vector2[] _teleport;
    private static int[] _inPumpX;
    private static int[] _inPumpY;
    private static int _numInPump;
    private static int[] _outPumpX;
    private static int[] _outPumpY;
    private static int _numOutPump;
    private static int[] _mechX;
    private static int[] _mechY;
    private static int _numMechs;
    private static int[] _mechTime;
    private static int _currentWireColor;

    public static void SetCurrentUser(int plr = -1)
    {
      if (plr < 0 || plr >= (int) byte.MaxValue)
        plr = 254;
      if (Main.netMode == 0)
        plr = Main.myPlayer;
      Wiring.CurrentUser = plr;
    }

    public static void Initialize()
    {
      Wiring._wireSkip = new Dictionary<Point16, bool>();
      Wiring._wireList = new DoubleStack<Point16>(1024, 0);
      Wiring._wireDirectionList = new DoubleStack<byte>(1024, 0);
      Wiring._toProcess = new Dictionary<Point16, byte>();
      Wiring._GatesCurrent = new Queue<Point16>();
      Wiring._GatesNext = new Queue<Point16>();
      Wiring._GatesDone = new Dictionary<Point16, bool>();
      Wiring._LampsToCheck = new Queue<Point16>();
      Wiring._PixelBoxTriggers = new Dictionary<Point16, byte>();
      Wiring._inPumpX = new int[20];
      Wiring._inPumpY = new int[20];
      Wiring._outPumpX = new int[20];
      Wiring._outPumpY = new int[20];
      Wiring._teleport = new Vector2[2];
      Wiring._mechX = new int[1000];
      Wiring._mechY = new int[1000];
      Wiring._mechTime = new int[1000];
    }

    public static void SkipWire(int x, int y)
    {
      Wiring._wireSkip[new Point16(x, y)] = true;
    }

    public static void SkipWire(Point16 point)
    {
      Wiring._wireSkip[point] = true;
    }

    public static void UpdateMech()
    {
      Wiring.SetCurrentUser(-1);
      for (int index1 = Wiring._numMechs - 1; index1 >= 0; --index1)
      {
        --Wiring._mechTime[index1];
        if (Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].active() && (int) Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].type == 144)
        {
          if ((int) Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].frameY == 0)
          {
            Wiring._mechTime[index1] = 0;
          }
          else
          {
            int num = (int) Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].frameX / 18;
            switch (num)
            {
              case 0:
                num = 60;
                break;
              case 1:
                num = 180;
                break;
              case 2:
                num = 300;
                break;
            }
            if (Math.IEEERemainder((double) Wiring._mechTime[index1], (double) num) == 0.0)
            {
              Wiring._mechTime[index1] = 18000;
              Wiring.TripWire(Wiring._mechX[index1], Wiring._mechY[index1], 1, 1);
            }
          }
        }
        if (Wiring._mechTime[index1] <= 0)
        {
          if (Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].active() && (int) Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].type == 144)
          {
            Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].frameY = (short) 0;
            NetMessage.SendTileSquare(-1, Wiring._mechX[index1], Wiring._mechY[index1], 1, TileChangeType.None);
          }
          if (Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].active() && (int) Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].type == 411)
          {
            Tile tile = Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]];
            int num1 = (int) tile.frameX % 36 / 18;
            int num2 = (int) tile.frameY % 36 / 18;
            int tileX = Wiring._mechX[index1] - num1;
            int tileY = Wiring._mechY[index1] - num2;
            int num3 = 36;
            if ((int) Main.tile[tileX, tileY].frameX >= 36)
              num3 = -36;
            for (int index2 = tileX; index2 < tileX + 2; ++index2)
            {
              for (int index3 = tileY; index3 < tileY + 2; ++index3)
                Main.tile[index2, index3].frameX = (short) ((int) Main.tile[index2, index3].frameX + num3);
            }
            NetMessage.SendTileSquare(-1, tileX, tileY, 2, TileChangeType.None);
          }
          for (int index2 = index1; index2 < Wiring._numMechs; ++index2)
          {
            Wiring._mechX[index2] = Wiring._mechX[index2 + 1];
            Wiring._mechY[index2] = Wiring._mechY[index2 + 1];
            Wiring._mechTime[index2] = Wiring._mechTime[index2 + 1];
          }
          --Wiring._numMechs;
        }
      }
    }

    public static void HitSwitch(int i, int j)
    {
      if (!WorldGen.InWorld(i, j, 0) || Main.tile[i, j] == null)
        return;
      if ((int) Main.tile[i, j].type == 135 || (int) Main.tile[i, j].type == 314 || ((int) Main.tile[i, j].type == 423 || (int) Main.tile[i, j].type == 428) || (int) Main.tile[i, j].type == 442)
      {
        Main.PlaySound(28, i * 16, j * 16, 0, 1f, 0.0f);
        Wiring.TripWire(i, j, 1, 1);
      }
      else if ((int) Main.tile[i, j].type == 440)
      {
        Main.PlaySound(28, i * 16 + 16, j * 16 + 16, 0, 1f, 0.0f);
        Wiring.TripWire(i, j, 3, 3);
      }
      else if ((int) Main.tile[i, j].type == 136)
      {
        Main.tile[i, j].frameY = (int) Main.tile[i, j].frameY != 0 ? (short) 0 : (short) 18;
        Main.PlaySound(28, i * 16, j * 16, 0, 1f, 0.0f);
        Wiring.TripWire(i, j, 1, 1);
      }
      else if ((int) Main.tile[i, j].type == 144)
      {
        if ((int) Main.tile[i, j].frameY == 0)
        {
          Main.tile[i, j].frameY = (short) 18;
          if (Main.netMode != 1)
            Wiring.CheckMech(i, j, 18000);
        }
        else
          Main.tile[i, j].frameY = (short) 0;
        Main.PlaySound(28, i * 16, j * 16, 0, 1f, 0.0f);
      }
      else if ((int) Main.tile[i, j].type == 441 || (int) Main.tile[i, j].type == 468)
      {
        int num1 = (int) Main.tile[i, j].frameX / 18 * -1;
        int num2 = (int) Main.tile[i, j].frameY / 18 * -1;
        int num3 = num1 % 4;
        if (num3 < -1)
          num3 += 2;
        int left = num3 + i;
        int top = num2 + j;
        Main.PlaySound(28, i * 16, j * 16, 0, 1f, 0.0f);
        Wiring.TripWire(left, top, 2, 2);
      }
      else
      {
        if ((int) Main.tile[i, j].type != 132 && (int) Main.tile[i, j].type != 411)
          return;
        short num1 = 36;
        int num2 = (int) Main.tile[i, j].frameX / 18 * -1;
        int num3 = (int) Main.tile[i, j].frameY / 18 * -1;
        int num4 = num2 % 4;
        if (num4 < -1)
        {
          num4 += 2;
          num1 = (short) -36;
        }
        int index1 = num4 + i;
        int index2 = num3 + j;
        if (Main.netMode != 1 && (int) Main.tile[index1, index2].type == 411)
          Wiring.CheckMech(index1, index2, 60);
        for (int index3 = index1; index3 < index1 + 2; ++index3)
        {
          for (int index4 = index2; index4 < index2 + 2; ++index4)
          {
            if ((int) Main.tile[index3, index4].type == 132 || (int) Main.tile[index3, index4].type == 411)
              Main.tile[index3, index4].frameX += num1;
          }
        }
        WorldGen.TileFrame(index1, index2, false, false);
        Main.PlaySound(28, i * 16, j * 16, 0, 1f, 0.0f);
        Wiring.TripWire(index1, index2, 2, 2);
      }
    }

    public static void PokeLogicGate(int lampX, int lampY)
    {
      if (Main.netMode == 1)
        return;
      Wiring._LampsToCheck.Enqueue(new Point16(lampX, lampY));
      Wiring.LogicGatePass();
    }

    public static bool Actuate(int i, int j)
    {
      Tile tile = Main.tile[i, j];
      if (!tile.actuator())
        return false;
      if (((int) tile.type != 226 || (double) j <= Main.worldSurface || NPC.downedPlantBoss) && ((double) j <= Main.worldSurface || NPC.downedGolemBoss || (int) Main.tile[i, j - 1].type != 237))
      {
        if (tile.inActive())
          Wiring.ReActive(i, j);
        else
          Wiring.DeActive(i, j);
      }
      return true;
    }

    public static void ActuateForced(int i, int j)
    {
      Tile tile = Main.tile[i, j];
      if ((int) tile.type == 226 && (double) j > Main.worldSurface && !NPC.downedPlantBoss)
        return;
      if (tile.inActive())
        Wiring.ReActive(i, j);
      else
        Wiring.DeActive(i, j);
    }

    public static void MassWireOperation(Point ps, Point pe, Player master)
    {
      int wireCount = 0;
      int actuatorCount = 0;
      for (int index = 0; index < 58; ++index)
      {
        if (master.inventory[index].type == 530)
          wireCount += master.inventory[index].stack;
        if (master.inventory[index].type == 849)
          actuatorCount += master.inventory[index].stack;
      }
      int num1 = wireCount;
      int num2 = actuatorCount;
      Wiring.MassWireOperationInner(ps, pe, master.Center, master.direction == 1, ref wireCount, ref actuatorCount);
      int num3 = num1 - wireCount;
      int num4 = num2 - actuatorCount;
      if (Main.netMode == 2)
      {
        NetMessage.SendData(110, master.whoAmI, -1, (NetworkText) null, 530, (float) num3, (float) master.whoAmI, 0.0f, 0, 0, 0);
        NetMessage.SendData(110, master.whoAmI, -1, (NetworkText) null, 849, (float) num4, (float) master.whoAmI, 0.0f, 0, 0, 0);
      }
      else
      {
        for (int index = 0; index < num3; ++index)
          master.ConsumeItem(530, false);
        for (int index = 0; index < num4; ++index)
          master.ConsumeItem(849, false);
      }
    }

    private static bool CheckMech(int i, int j, int time)
    {
      for (int index = 0; index < Wiring._numMechs; ++index)
      {
        if (Wiring._mechX[index] == i && Wiring._mechY[index] == j)
          return false;
      }
      if (Wiring._numMechs >= 999)
        return false;
      Wiring._mechX[Wiring._numMechs] = i;
      Wiring._mechY[Wiring._numMechs] = j;
      Wiring._mechTime[Wiring._numMechs] = time;
      ++Wiring._numMechs;
      return true;
    }

    private static void XferWater()
    {
      for (int index1 = 0; index1 < Wiring._numInPump; ++index1)
      {
        int i1 = Wiring._inPumpX[index1];
        int j1 = Wiring._inPumpY[index1];
        int liquid1 = (int) Main.tile[i1, j1].liquid;
        if (liquid1 > 0)
        {
          bool lava = Main.tile[i1, j1].lava();
          bool honey = Main.tile[i1, j1].honey();
          for (int index2 = 0; index2 < Wiring._numOutPump; ++index2)
          {
            int i2 = Wiring._outPumpX[index2];
            int j2 = Wiring._outPumpY[index2];
            int liquid2 = (int) Main.tile[i2, j2].liquid;
            if (liquid2 < (int) byte.MaxValue)
            {
              bool flag1 = Main.tile[i2, j2].lava();
              bool flag2 = Main.tile[i2, j2].honey();
              if (liquid2 == 0)
              {
                flag1 = lava;
                flag2 = honey;
              }
              if (lava == flag1 && honey == flag2)
              {
                int num = liquid1;
                if (num + liquid2 > (int) byte.MaxValue)
                  num = (int) byte.MaxValue - liquid2;
                Main.tile[i2, j2].liquid += (byte) num;
                Main.tile[i1, j1].liquid -= (byte) num;
                liquid1 = (int) Main.tile[i1, j1].liquid;
                Main.tile[i2, j2].lava(lava);
                Main.tile[i2, j2].honey(honey);
                WorldGen.SquareTileFrame(i2, j2, true);
                if ((int) Main.tile[i1, j1].liquid == 0)
                {
                  Main.tile[i1, j1].lava(false);
                  WorldGen.SquareTileFrame(i1, j1, true);
                  break;
                }
              }
            }
          }
          WorldGen.SquareTileFrame(i1, j1, true);
        }
      }
    }

    private static void TripWire(int left, int top, int width, int height)
    {
      if (Main.netMode == 1)
        return;
      Wiring.running = true;
      if (Wiring._wireList.Count != 0)
        Wiring._wireList.Clear(true);
      if (Wiring._wireDirectionList.Count != 0)
        Wiring._wireDirectionList.Clear(true);
      Vector2[] vector2Array1 = new Vector2[8];
      int num1 = 0;
      Point16 back;
      for (int X = left; X < left + width; ++X)
      {
        for (int Y = top; Y < top + height; ++Y)
        {
          back = new Point16(X, Y);
          Tile tile = Main.tile[X, Y];
          if (tile != null && tile.wire())
            Wiring._wireList.PushBack(back);
        }
      }
      Wiring._teleport[0].X = (__Null) -1.0;
      Wiring._teleport[0].Y = (__Null) -1.0;
      Wiring._teleport[1].X = (__Null) -1.0;
      Wiring._teleport[1].Y = (__Null) -1.0;
      if (Wiring._wireList.Count > 0)
      {
        Wiring._numInPump = 0;
        Wiring._numOutPump = 0;
        Wiring.HitWire(Wiring._wireList, 1);
        if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
          Wiring.XferWater();
      }
      Vector2[] vector2Array2 = vector2Array1;
      int index1 = num1;
      int num2 = 1;
      int num3 = index1 + num2;
      vector2Array2[index1] = Wiring._teleport[0];
      Vector2[] vector2Array3 = vector2Array1;
      int index2 = num3;
      int num4 = 1;
      int num5 = index2 + num4;
      vector2Array3[index2] = Wiring._teleport[1];
      for (int X = left; X < left + width; ++X)
      {
        for (int Y = top; Y < top + height; ++Y)
        {
          back = new Point16(X, Y);
          Tile tile = Main.tile[X, Y];
          if (tile != null && tile.wire2())
            Wiring._wireList.PushBack(back);
        }
      }
      Wiring._teleport[0].X = (__Null) -1.0;
      Wiring._teleport[0].Y = (__Null) -1.0;
      Wiring._teleport[1].X = (__Null) -1.0;
      Wiring._teleport[1].Y = (__Null) -1.0;
      if (Wiring._wireList.Count > 0)
      {
        Wiring._numInPump = 0;
        Wiring._numOutPump = 0;
        Wiring.HitWire(Wiring._wireList, 2);
        if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
          Wiring.XferWater();
      }
      Vector2[] vector2Array4 = vector2Array1;
      int index3 = num5;
      int num6 = 1;
      int num7 = index3 + num6;
      vector2Array4[index3] = Wiring._teleport[0];
      Vector2[] vector2Array5 = vector2Array1;
      int index4 = num7;
      int num8 = 1;
      int num9 = index4 + num8;
      vector2Array5[index4] = Wiring._teleport[1];
      Wiring._teleport[0].X = (__Null) -1.0;
      Wiring._teleport[0].Y = (__Null) -1.0;
      Wiring._teleport[1].X = (__Null) -1.0;
      Wiring._teleport[1].Y = (__Null) -1.0;
      for (int X = left; X < left + width; ++X)
      {
        for (int Y = top; Y < top + height; ++Y)
        {
          back = new Point16(X, Y);
          Tile tile = Main.tile[X, Y];
          if (tile != null && tile.wire3())
            Wiring._wireList.PushBack(back);
        }
      }
      if (Wiring._wireList.Count > 0)
      {
        Wiring._numInPump = 0;
        Wiring._numOutPump = 0;
        Wiring.HitWire(Wiring._wireList, 3);
        if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
          Wiring.XferWater();
      }
      Vector2[] vector2Array6 = vector2Array1;
      int index5 = num9;
      int num10 = 1;
      int num11 = index5 + num10;
      vector2Array6[index5] = Wiring._teleport[0];
      Vector2[] vector2Array7 = vector2Array1;
      int index6 = num11;
      int num12 = 1;
      int num13 = index6 + num12;
      vector2Array7[index6] = Wiring._teleport[1];
      Wiring._teleport[0].X = (__Null) -1.0;
      Wiring._teleport[0].Y = (__Null) -1.0;
      Wiring._teleport[1].X = (__Null) -1.0;
      Wiring._teleport[1].Y = (__Null) -1.0;
      for (int X = left; X < left + width; ++X)
      {
        for (int Y = top; Y < top + height; ++Y)
        {
          back = new Point16(X, Y);
          Tile tile = Main.tile[X, Y];
          if (tile != null && tile.wire4())
            Wiring._wireList.PushBack(back);
        }
      }
      if (Wiring._wireList.Count > 0)
      {
        Wiring._numInPump = 0;
        Wiring._numOutPump = 0;
        Wiring.HitWire(Wiring._wireList, 4);
        if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
          Wiring.XferWater();
      }
      Vector2[] vector2Array8 = vector2Array1;
      int index7 = num13;
      int num14 = 1;
      int num15 = index7 + num14;
      vector2Array8[index7] = Wiring._teleport[0];
      Vector2[] vector2Array9 = vector2Array1;
      int index8 = num15;
      int num16 = 1;
      int num17 = index8 + num16;
      vector2Array9[index8] = Wiring._teleport[1];
      int index9 = 0;
      while (index9 < 8)
      {
        Wiring._teleport[0] = vector2Array1[index9];
        Wiring._teleport[1] = vector2Array1[index9 + 1];
        if (Wiring._teleport[0].X >= 0.0 && Wiring._teleport[1].X >= 0.0)
          Wiring.Teleport();
        index9 += 2;
      }
      Wiring.PixelBoxPass();
      Wiring.LogicGatePass();
    }

    private static void PixelBoxPass()
    {
      foreach (KeyValuePair<Point16, byte> pixelBoxTrigger in Wiring._PixelBoxTriggers)
      {
        if ((int) pixelBoxTrigger.Value != 2)
        {
          if ((int) pixelBoxTrigger.Value == 1)
          {
            if ((int) Main.tile[(int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y].frameX != 0)
            {
              Main.tile[(int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y].frameX = (short) 0;
              NetMessage.SendTileSquare(-1, (int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y, 1, TileChangeType.None);
            }
          }
          else if ((int) pixelBoxTrigger.Value == 3 && (int) Main.tile[(int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y].frameX != 18)
          {
            Main.tile[(int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y].frameX = (short) 18;
            NetMessage.SendTileSquare(-1, (int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y, 1, TileChangeType.None);
          }
        }
      }
      Wiring._PixelBoxTriggers.Clear();
    }

    private static void LogicGatePass()
    {
      if (Wiring._GatesCurrent.Count != 0)
        return;
      Wiring._GatesDone.Clear();
      while (Wiring._LampsToCheck.Count > 0)
      {
        while (Wiring._LampsToCheck.Count > 0)
        {
          Point16 point16 = Wiring._LampsToCheck.Dequeue();
          Wiring.CheckLogicGate((int) point16.X, (int) point16.Y);
        }
        while (Wiring._GatesNext.Count > 0)
        {
          Utils.Swap<Queue<Point16>>(ref Wiring._GatesCurrent, ref Wiring._GatesNext);
          while (Wiring._GatesCurrent.Count > 0)
          {
            Point16 key = Wiring._GatesCurrent.Peek();
            bool flag;
            if (Wiring._GatesDone.TryGetValue(key, out flag) && flag)
            {
              Wiring._GatesCurrent.Dequeue();
            }
            else
            {
              Wiring._GatesDone.Add(key, true);
              Wiring.TripWire((int) key.X, (int) key.Y, 1, 1);
              Wiring._GatesCurrent.Dequeue();
            }
          }
        }
      }
      Wiring._GatesDone.Clear();
      if (!Wiring.blockPlayerTeleportationForOneIteration)
        return;
      Wiring.blockPlayerTeleportationForOneIteration = false;
    }

    private static void CheckLogicGate(int lampX, int lampY)
    {
      if (!WorldGen.InWorld(lampX, lampY, 1))
        return;
      for (int index1 = lampY; index1 < Main.maxTilesY; ++index1)
      {
        Tile tile1 = Main.tile[lampX, index1];
        if (!tile1.active())
          break;
        if ((int) tile1.type == 420)
        {
          bool flag1;
          Wiring._GatesDone.TryGetValue(new Point16(lampX, index1), out flag1);
          int num1 = (int) tile1.frameY / 18;
          bool flag2 = (int) tile1.frameX == 18;
          bool flag3 = (int) tile1.frameX == 36;
          if (num1 < 0)
            break;
          int num2 = 0;
          int num3 = 0;
          bool flag4 = false;
          for (int index2 = index1 - 1; index2 > 0; --index2)
          {
            Tile tile2 = Main.tile[lampX, index2];
            if (tile2.active() && (int) tile2.type == 419)
            {
              if ((int) tile2.frameX == 36)
              {
                flag4 = true;
                break;
              }
              ++num2;
              num3 += ((int) tile2.frameX == 18).ToInt();
            }
            else
              break;
          }
          bool flag5;
          switch (num1)
          {
            case 0:
              flag5 = num2 == num3;
              break;
            case 1:
              flag5 = num3 > 0;
              break;
            case 2:
              flag5 = num2 != num3;
              break;
            case 3:
              flag5 = num3 == 0;
              break;
            case 4:
              flag5 = num3 == 1;
              break;
            case 5:
              flag5 = num3 != 1;
              break;
            default:
              return;
          }
          bool flag6 = !flag4 && flag3;
          bool flag7 = false;
          if (flag4 && (int) Framing.GetTileSafely(lampX, lampY).frameX == 36)
            flag7 = true;
          if (flag5 == flag2 && !flag6 && !flag7)
            break;
          int num4 = (int) tile1.frameX % 18 / 18;
          tile1.frameX = (short) (18 * flag5.ToInt());
          if (flag4)
            tile1.frameX = (short) 36;
          Wiring.SkipWire(lampX, index1);
          WorldGen.SquareTileFrame(lampX, index1, true);
          NetMessage.SendTileSquare(-1, lampX, index1, 1, TileChangeType.None);
          bool flag8 = !flag4 || flag7;
          if (flag7)
          {
            if (num3 == 0 || num2 == 0)
              ;
            flag8 = (double) Main.rand.NextFloat() < (double) num3 / (double) num2;
          }
          if (flag6)
            flag8 = false;
          if (!flag8)
            break;
          if (!flag1)
          {
            Wiring._GatesNext.Enqueue(new Point16(lampX, index1));
            break;
          }
          Vector2 position = Vector2.op_Subtraction(Vector2.op_Multiply(new Vector2((float) lampX, (float) index1), 16f), new Vector2(10f));
          Utils.PoofOfSmoke(position);
          NetMessage.SendData(106, -1, -1, (NetworkText) null, (int) position.X, (float) position.Y, 0.0f, 0.0f, 0, 0, 0);
          break;
        }
        if ((int) tile1.type != 419)
          break;
      }
    }

    private static void HitWire(DoubleStack<Point16> next, int wireType)
    {
      Wiring._wireDirectionList.Clear(true);
      for (int index = 0; index < next.Count; ++index)
      {
        Point16 point16 = next.PopFront();
        Wiring.SkipWire(point16);
        Wiring._toProcess.Add(point16, (byte) 4);
        next.PushBack(point16);
        Wiring._wireDirectionList.PushBack((byte) 0);
      }
      Wiring._currentWireColor = wireType;
      while (next.Count > 0)
      {
        Point16 key = next.PopFront();
        int num1 = (int) Wiring._wireDirectionList.PopFront();
        int x = (int) key.X;
        int y = (int) key.Y;
        if (!Wiring._wireSkip.ContainsKey(key))
          Wiring.HitWireSingle(x, y);
        for (int index1 = 0; index1 < 4; ++index1)
        {
          int X;
          int Y;
          switch (index1)
          {
            case 0:
              X = x;
              Y = y + 1;
              break;
            case 1:
              X = x;
              Y = y - 1;
              break;
            case 2:
              X = x + 1;
              Y = y;
              break;
            case 3:
              X = x - 1;
              Y = y;
              break;
            default:
              X = x;
              Y = y + 1;
              break;
          }
          if (X >= 2 && X < Main.maxTilesX - 2 && (Y >= 2 && Y < Main.maxTilesY - 2))
          {
            Tile tile1 = Main.tile[X, Y];
            if (tile1 != null)
            {
              Tile tile2 = Main.tile[x, y];
              if (tile2 != null)
              {
                byte num2 = 3;
                if ((int) tile1.type == 424 || (int) tile1.type == 445)
                  num2 = (byte) 0;
                if ((int) tile2.type == 424)
                {
                  switch ((int) tile2.frameX / 18)
                  {
                    case 0:
                      if (index1 == num1)
                        break;
                      continue;
                    case 1:
                      if (num1 == 0 && index1 == 3 || num1 == 3 && index1 == 0 || (num1 == 1 && index1 == 2 || num1 == 2 && index1 == 1))
                        break;
                      continue;
                    case 2:
                      if (num1 == 0 && index1 == 2 || num1 == 2 && index1 == 0 || (num1 == 1 && index1 == 3 || num1 == 3 && index1 == 1))
                        break;
                      continue;
                  }
                }
                if ((int) tile2.type == 445)
                {
                  if (index1 == num1)
                  {
                    if (Wiring._PixelBoxTriggers.ContainsKey(key))
                    {
                      Dictionary<Point16, byte> pixelBoxTriggers;
                      Point16 index2;
                      int num3 = (int) (byte) ((int) (pixelBoxTriggers = Wiring._PixelBoxTriggers)[index2 = key] | (index1 == 0 | index1 == 1 ? 2 : 1));
                      pixelBoxTriggers[index2] = (byte) num3;
                    }
                    else
                      Wiring._PixelBoxTriggers[key] = index1 == 0 | index1 == 1 ? (byte) 2 : (byte) 1;
                  }
                  else
                    continue;
                }
                bool flag;
                switch (wireType)
                {
                  case 1:
                    flag = tile1.wire();
                    break;
                  case 2:
                    flag = tile1.wire2();
                    break;
                  case 3:
                    flag = tile1.wire3();
                    break;
                  case 4:
                    flag = tile1.wire4();
                    break;
                  default:
                    flag = false;
                    break;
                }
                if (flag)
                {
                  Point16 index2 = new Point16(X, Y);
                  byte num3;
                  if (Wiring._toProcess.TryGetValue(index2, out num3))
                  {
                    --num3;
                    if ((int) num3 == 0)
                      Wiring._toProcess.Remove(index2);
                    else
                      Wiring._toProcess[index2] = num3;
                  }
                  else
                  {
                    next.PushBack(index2);
                    Wiring._wireDirectionList.PushBack((byte) index1);
                    if ((int) num2 > 0)
                      Wiring._toProcess.Add(index2, num2);
                  }
                }
              }
            }
          }
        }
      }
      Wiring._wireSkip.Clear();
      Wiring._toProcess.Clear();
      Wiring.running = false;
    }

    private static void HitWireSingle(int i, int j)
    {
      Tile tile1 = Main.tile[i, j];
      int type = (int) tile1.type;
      if (tile1.actuator())
        Wiring.ActuateForced(i, j);
      if (!tile1.active())
        return;
      if (type == 144)
      {
        Wiring.HitSwitch(i, j);
        WorldGen.SquareTileFrame(i, j, true);
        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
      }
      else if (type == 421)
      {
        if (!tile1.actuator())
        {
          tile1.type = (ushort) 422;
          WorldGen.SquareTileFrame(i, j, true);
          NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
        }
      }
      else if (type == 422 && !tile1.actuator())
      {
        tile1.type = (ushort) 421;
        WorldGen.SquareTileFrame(i, j, true);
        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
      }
      if (type >= (int) byte.MaxValue && type <= 268)
      {
        if (tile1.actuator())
          return;
        if (type >= 262)
          tile1.type -= (ushort) 7;
        else
          tile1.type += (ushort) 7;
        WorldGen.SquareTileFrame(i, j, true);
        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
      }
      else if (type == 419)
      {
        int num = 18;
        if ((int) tile1.frameX >= num)
          num = -num;
        if ((int) tile1.frameX == 36)
          num = 0;
        Wiring.SkipWire(i, j);
        tile1.frameX = (short) ((int) tile1.frameX + num);
        WorldGen.SquareTileFrame(i, j, true);
        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
        Wiring._LampsToCheck.Enqueue(new Point16(i, j));
      }
      else if (type == 406)
      {
        int num1 = (int) tile1.frameX % 54 / 18;
        int num2 = (int) tile1.frameY % 54 / 18;
        int index1 = i - num1;
        int index2 = j - num2;
        int num3 = 54;
        if ((int) Main.tile[index1, index2].frameY >= 108)
          num3 = -108;
        for (int x = index1; x < index1 + 3; ++x)
        {
          for (int y = index2; y < index2 + 3; ++y)
          {
            Wiring.SkipWire(x, y);
            Main.tile[x, y].frameY = (short) ((int) Main.tile[x, y].frameY + num3);
          }
        }
        NetMessage.SendTileSquare(-1, index1 + 1, index2 + 1, 3, TileChangeType.None);
      }
      else if (type == 452)
      {
        int num1 = (int) tile1.frameX % 54 / 18;
        int num2 = (int) tile1.frameY % 54 / 18;
        int index1 = i - num1;
        int index2 = j - num2;
        int num3 = 54;
        if ((int) Main.tile[index1, index2].frameX >= 54)
          num3 = -54;
        for (int x = index1; x < index1 + 3; ++x)
        {
          for (int y = index2; y < index2 + 3; ++y)
          {
            Wiring.SkipWire(x, y);
            Main.tile[x, y].frameX = (short) ((int) Main.tile[x, y].frameX + num3);
          }
        }
        NetMessage.SendTileSquare(-1, index1 + 1, index2 + 1, 3, TileChangeType.None);
      }
      else if (type == 411)
      {
        int num1 = (int) tile1.frameX % 36 / 18;
        int num2 = (int) tile1.frameY % 36 / 18;
        int tileX = i - num1;
        int tileY = j - num2;
        int num3 = 36;
        if ((int) Main.tile[tileX, tileY].frameX >= 36)
          num3 = -36;
        for (int x = tileX; x < tileX + 2; ++x)
        {
          for (int y = tileY; y < tileY + 2; ++y)
          {
            Wiring.SkipWire(x, y);
            Main.tile[x, y].frameX = (short) ((int) Main.tile[x, y].frameX + num3);
          }
        }
        NetMessage.SendTileSquare(-1, tileX, tileY, 2, TileChangeType.None);
      }
      else if (type == 425)
      {
        int num1 = (int) tile1.frameX % 36 / 18;
        int num2 = (int) tile1.frameY % 36 / 18;
        int i1 = i - num1;
        int j1 = j - num2;
        for (int x = i1; x < i1 + 2; ++x)
        {
          for (int y = j1; y < j1 + 2; ++y)
            Wiring.SkipWire(x, y);
        }
        if (Main.AnnouncementBoxDisabled)
          return;
        Color pink = Color.get_Pink();
        int index = Sign.ReadSign(i1, j1, false);
        if (index == -1 || Main.sign[index] == null || string.IsNullOrWhiteSpace(Main.sign[index].text))
          return;
        if (Main.AnnouncementBoxRange == -1)
        {
          if (Main.netMode == 0)
          {
            Main.NewTextMultiline(Main.sign[index].text, false, pink, 460);
          }
          else
          {
            if (Main.netMode != 2)
              return;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            NetMessage.SendData(107, -1, -1, NetworkText.FromLiteral(Main.sign[index].text), (int) byte.MaxValue, (float) ((Color) @pink).get_R(), (float) ((Color) @pink).get_G(), (float) ((Color) @pink).get_B(), 460, 0, 0);
          }
        }
        else if (Main.netMode == 0)
        {
          if ((double) Main.player[Main.myPlayer].Distance(new Vector2((float) (i1 * 16 + 16), (float) (j1 * 16 + 16))) > (double) Main.AnnouncementBoxRange)
            return;
          Main.NewTextMultiline(Main.sign[index].text, false, pink, 460);
        }
        else
        {
          if (Main.netMode != 2)
            return;
          for (int remoteClient = 0; remoteClient < (int) byte.MaxValue; ++remoteClient)
          {
            if (Main.player[remoteClient].active && (double) Main.player[remoteClient].Distance(new Vector2((float) (i1 * 16 + 16), (float) (j1 * 16 + 16))) <= (double) Main.AnnouncementBoxRange)
            {
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              NetMessage.SendData(107, remoteClient, -1, NetworkText.FromLiteral(Main.sign[index].text), (int) byte.MaxValue, (float) ((Color) @pink).get_R(), (float) ((Color) @pink).get_G(), (float) ((Color) @pink).get_B(), 460, 0, 0);
            }
          }
        }
      }
      else if (type == 405)
      {
        int num1 = (int) tile1.frameX % 54 / 18;
        int num2 = (int) tile1.frameY % 36 / 18;
        int index1 = i - num1;
        int index2 = j - num2;
        int num3 = 54;
        if ((int) Main.tile[index1, index2].frameX >= 54)
          num3 = -54;
        for (int x = index1; x < index1 + 3; ++x)
        {
          for (int y = index2; y < index2 + 2; ++y)
          {
            Wiring.SkipWire(x, y);
            Main.tile[x, y].frameX = (short) ((int) Main.tile[x, y].frameX + num3);
          }
        }
        NetMessage.SendTileSquare(-1, index1 + 1, index2 + 1, 3, TileChangeType.None);
      }
      else if (type == 209)
      {
        int num1 = (int) tile1.frameX % 72 / 18;
        int num2 = (int) tile1.frameY % 54 / 18;
        int num3 = i - num1;
        int num4 = j - num2;
        int angle = (int) tile1.frameY / 54;
        int num5 = (int) tile1.frameX / 72;
        int num6 = -1;
        if (num1 == 1 || num1 == 2)
          num6 = num2;
        int num7 = 0;
        if (num1 == 3)
          num7 = -54;
        if (num1 == 0)
          num7 = 54;
        if (angle >= 8 && num7 > 0)
          num7 = 0;
        if (angle == 0 && num7 < 0)
          num7 = 0;
        bool flag1 = false;
        if (num7 != 0)
        {
          for (int x = num3; x < num3 + 4; ++x)
          {
            for (int y = num4; y < num4 + 3; ++y)
            {
              Wiring.SkipWire(x, y);
              Main.tile[x, y].frameY = (short) ((int) Main.tile[x, y].frameY + num7);
            }
          }
          flag1 = true;
        }
        if ((num5 == 3 || num5 == 4) && (num6 == 0 || num6 == 1))
        {
          int num8 = num5 == 3 ? 72 : -72;
          for (int x = num3; x < num3 + 4; ++x)
          {
            for (int y = num4; y < num4 + 3; ++y)
            {
              Wiring.SkipWire(x, y);
              Main.tile[x, y].frameX = (short) ((int) Main.tile[x, y].frameX + num8);
            }
          }
          flag1 = true;
        }
        if (flag1)
          NetMessage.SendTileSquare(-1, num3 + 1, num4 + 1, 4, TileChangeType.None);
        if (num6 == -1)
          return;
        bool flag2 = true;
        if ((num5 == 3 || num5 == 4) && num6 < 2)
          flag2 = false;
        if (!Wiring.CheckMech(num3, num4, 30) || !flag2)
          return;
        WorldGen.ShootFromCannon(num3, num4, angle, num5 + 1, 0, 0.0f, Wiring.CurrentUser);
      }
      else if (type == 212)
      {
        int num1 = (int) tile1.frameX % 54 / 18;
        int num2 = (int) tile1.frameY % 54 / 18;
        int i1 = i - num1;
        int j1 = j - num2;
        int num3 = (int) tile1.frameX / 54;
        int num4 = -1;
        if (num1 == 1)
          num4 = num2;
        int num5 = 0;
        if (num1 == 0)
          num5 = -54;
        if (num1 == 2)
          num5 = 54;
        if (num3 >= 1 && num5 > 0)
          num5 = 0;
        if (num3 == 0 && num5 < 0)
          num5 = 0;
        bool flag = false;
        if (num5 != 0)
        {
          for (int x = i1; x < i1 + 3; ++x)
          {
            for (int y = j1; y < j1 + 3; ++y)
            {
              Wiring.SkipWire(x, y);
              Main.tile[x, y].frameX = (short) ((int) Main.tile[x, y].frameX + num5);
            }
          }
          flag = true;
        }
        if (flag)
          NetMessage.SendTileSquare(-1, i1 + 1, j1 + 1, 4, TileChangeType.None);
        if (num4 == -1 || !Wiring.CheckMech(i1, j1, 10))
          return;
        float num6 = (float) (12.0 + (double) Main.rand.Next(450) * 0.00999999977648258);
        float num7 = (float) Main.rand.Next(85, 105);
        float num8 = (float) Main.rand.Next(-35, 11);
        int Type = 166;
        int Damage = 0;
        float KnockBack = 0.0f;
        Vector2 vector2;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2).\u002Ector((float) ((i1 + 2) * 16 - 8), (float) ((j1 + 2) * 16 - 8));
        if ((int) tile1.frameX / 54 == 0)
        {
          num7 *= -1f;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local = @vector2;
          // ISSUE: explicit reference operation
          double num9 = (^local).X - 12.0;
          // ISSUE: explicit reference operation
          (^local).X = (__Null) num9;
        }
        else
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local = @vector2;
          // ISSUE: explicit reference operation
          double num9 = (^local).X + 12.0;
          // ISSUE: explicit reference operation
          (^local).X = (__Null) num9;
        }
        float num10 = num7;
        float num11 = num8;
        float num12 = (float) Math.Sqrt((double) num10 * (double) num10 + (double) num11 * (double) num11);
        float num13 = num6 / num12;
        float SpeedX = num10 * num13;
        float SpeedY = num11 * num13;
        Projectile.NewProjectile((float) vector2.X, (float) vector2.Y, SpeedX, SpeedY, Type, Damage, KnockBack, Wiring.CurrentUser, 0.0f, 0.0f);
      }
      else if (type == 215)
      {
        int num1 = (int) tile1.frameX % 54 / 18;
        int num2 = (int) tile1.frameY % 36 / 18;
        int index1 = i - num1;
        int index2 = j - num2;
        int num3 = 36;
        if ((int) Main.tile[index1, index2].frameY >= 36)
          num3 = -36;
        for (int x = index1; x < index1 + 3; ++x)
        {
          for (int y = index2; y < index2 + 2; ++y)
          {
            Wiring.SkipWire(x, y);
            Main.tile[x, y].frameY = (short) ((int) Main.tile[x, y].frameY + num3);
          }
        }
        NetMessage.SendTileSquare(-1, index1 + 1, index2 + 1, 3, TileChangeType.None);
      }
      else if (type == 130)
      {
        if (Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active() && (TileID.Sets.BasicChest[(int) Main.tile[i, j - 1].type] || TileID.Sets.BasicChestFake[(int) Main.tile[i, j - 1].type] || (int) Main.tile[i, j - 1].type == 88))
          return;
        tile1.type = (ushort) 131;
        WorldGen.SquareTileFrame(i, j, true);
        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
      }
      else if (type == 131)
      {
        tile1.type = (ushort) 130;
        WorldGen.SquareTileFrame(i, j, true);
        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
      }
      else if (type == 387 || type == 386)
      {
        bool flag = type == 387;
        int num = WorldGen.ShiftTrapdoor(i, j, true, -1).ToInt();
        if (num == 0)
          num = -WorldGen.ShiftTrapdoor(i, j, false, -1).ToInt();
        if (num == 0)
          return;
        NetMessage.SendData(19, -1, -1, (NetworkText) null, 3 - flag.ToInt(), (float) i, (float) j, (float) num, 0, 0, 0);
      }
      else if (type == 389 || type == 388)
      {
        bool closing = type == 389;
        WorldGen.ShiftTallGate(i, j, closing);
        NetMessage.SendData(19, -1, -1, (NetworkText) null, 4 + closing.ToInt(), (float) i, (float) j, 0.0f, 0, 0, 0);
      }
      else if (type == 11)
      {
        if (!WorldGen.CloseDoor(i, j, true))
          return;
        NetMessage.SendData(19, -1, -1, (NetworkText) null, 1, (float) i, (float) j, 0.0f, 0, 0, 0);
      }
      else if (type == 10)
      {
        int direction = 1;
        if (Main.rand.Next(2) == 0)
          direction = -1;
        if (!WorldGen.OpenDoor(i, j, direction))
        {
          if (!WorldGen.OpenDoor(i, j, -direction))
            return;
          NetMessage.SendData(19, -1, -1, (NetworkText) null, 0, (float) i, (float) j, (float) -direction, 0, 0, 0);
        }
        else
          NetMessage.SendData(19, -1, -1, (NetworkText) null, 0, (float) i, (float) j, (float) direction, 0, 0, 0);
      }
      else if (type == 216)
      {
        WorldGen.LaunchRocket(i, j);
        Wiring.SkipWire(i, j);
      }
      else if (type == 335)
      {
        int num1 = j - (int) tile1.frameY / 18;
        int num2 = i - (int) tile1.frameX / 18;
        Wiring.SkipWire(num2, num1);
        Wiring.SkipWire(num2, num1 + 1);
        Wiring.SkipWire(num2 + 1, num1);
        Wiring.SkipWire(num2 + 1, num1 + 1);
        if (!Wiring.CheckMech(num2, num1, 30))
          return;
        WorldGen.LaunchRocketSmall(num2, num1);
      }
      else if (type == 338)
      {
        int num1 = j - (int) tile1.frameY / 18;
        int num2 = i - (int) tile1.frameX / 18;
        Wiring.SkipWire(num2, num1);
        Wiring.SkipWire(num2, num1 + 1);
        if (!Wiring.CheckMech(num2, num1, 30))
          return;
        bool flag = false;
        for (int index = 0; index < 1000; ++index)
        {
          if (Main.projectile[index].active && Main.projectile[index].aiStyle == 73 && ((double) Main.projectile[index].ai[0] == (double) num2 && (double) Main.projectile[index].ai[1] == (double) num1))
          {
            flag = true;
            break;
          }
        }
        if (flag)
          return;
        Projectile.NewProjectile((float) (num2 * 16 + 8), (float) (num1 * 16 + 2), 0.0f, 0.0f, 419 + Main.rand.Next(4), 0, 0.0f, Main.myPlayer, (float) num2, (float) num1);
      }
      else if (type == 235)
      {
        int num1 = i - (int) tile1.frameX / 18;
        if ((int) tile1.wall == 87 && (double) j > Main.worldSurface && !NPC.downedPlantBoss)
          return;
        if (Wiring._teleport[0].X == -1.0)
        {
          Wiring._teleport[0].X = (__Null) (double) num1;
          Wiring._teleport[0].Y = (__Null) (double) j;
          if (!tile1.halfBrick())
            return;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local = @Wiring._teleport[0];
          // ISSUE: explicit reference operation
          double num2 = (^local).Y + 0.5;
          // ISSUE: explicit reference operation
          (^local).Y = (__Null) num2;
        }
        else
        {
          if (Wiring._teleport[0].X == (double) num1 && Wiring._teleport[0].Y == (double) j)
            return;
          Wiring._teleport[1].X = (__Null) (double) num1;
          Wiring._teleport[1].Y = (__Null) (double) j;
          if (!tile1.halfBrick())
            return;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local = @Wiring._teleport[1];
          // ISSUE: explicit reference operation
          double num2 = (^local).Y + 0.5;
          // ISSUE: explicit reference operation
          (^local).Y = (__Null) num2;
        }
      }
      else if (type == 4)
      {
        if ((int) tile1.frameX < 66)
          tile1.frameX += (short) 66;
        else
          tile1.frameX -= (short) 66;
        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
      }
      else if (type == 429)
      {
        int num1 = (int) Main.tile[i, j].frameX / 18;
        bool flag1 = num1 % 2 >= 1;
        bool flag2 = num1 % 4 >= 2;
        bool flag3 = num1 % 8 >= 4;
        bool flag4 = num1 % 16 >= 8;
        bool flag5 = false;
        short num2 = 0;
        switch (Wiring._currentWireColor)
        {
          case 1:
            num2 = (short) 18;
            flag5 = !flag1;
            break;
          case 2:
            num2 = (short) 72;
            flag5 = !flag3;
            break;
          case 3:
            num2 = (short) 36;
            flag5 = !flag2;
            break;
          case 4:
            num2 = (short) 144;
            flag5 = !flag4;
            break;
        }
        if (flag5)
          tile1.frameX += num2;
        else
          tile1.frameX -= num2;
        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
      }
      else if (type == 149)
      {
        if ((int) tile1.frameX < 54)
          tile1.frameX += (short) 54;
        else
          tile1.frameX -= (short) 54;
        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
      }
      else if (type == 244)
      {
        int num1 = (int) tile1.frameX / 18;
        while (num1 >= 3)
          num1 -= 3;
        int num2 = (int) tile1.frameY / 18;
        while (num2 >= 3)
          num2 -= 3;
        int index1 = i - num1;
        int index2 = j - num2;
        int num3 = 54;
        if ((int) Main.tile[index1, index2].frameX >= 54)
          num3 = -54;
        for (int x = index1; x < index1 + 3; ++x)
        {
          for (int y = index2; y < index2 + 2; ++y)
          {
            Wiring.SkipWire(x, y);
            Main.tile[x, y].frameX = (short) ((int) Main.tile[x, y].frameX + num3);
          }
        }
        NetMessage.SendTileSquare(-1, index1 + 1, index2 + 1, 3, TileChangeType.None);
      }
      else if (type == 42)
      {
        int num1 = (int) tile1.frameY / 18;
        while (num1 >= 2)
          num1 -= 2;
        int y = j - num1;
        short num2 = 18;
        if ((int) tile1.frameX > 0)
          num2 = (short) -18;
        Main.tile[i, y].frameX += num2;
        Main.tile[i, y + 1].frameX += num2;
        Wiring.SkipWire(i, y);
        Wiring.SkipWire(i, y + 1);
        NetMessage.SendTileSquare(-1, i, j, 2, TileChangeType.None);
      }
      else if (type == 93)
      {
        int num1 = (int) tile1.frameY / 18;
        while (num1 >= 3)
          num1 -= 3;
        int y = j - num1;
        short num2 = 18;
        if ((int) tile1.frameX > 0)
          num2 = (short) -18;
        Main.tile[i, y].frameX += num2;
        Main.tile[i, y + 1].frameX += num2;
        Main.tile[i, y + 2].frameX += num2;
        Wiring.SkipWire(i, y);
        Wiring.SkipWire(i, y + 1);
        Wiring.SkipWire(i, y + 2);
        NetMessage.SendTileSquare(-1, i, y + 1, 3, TileChangeType.None);
      }
      else if (type == 126 || type == 95 || (type == 100 || type == 173))
      {
        int num1 = (int) tile1.frameY / 18;
        while (num1 >= 2)
          num1 -= 2;
        int index1 = j - num1;
        int num2 = (int) tile1.frameX / 18;
        if (num2 > 1)
          num2 -= 2;
        int index2 = i - num2;
        short num3 = 36;
        if ((int) Main.tile[index2, index1].frameX > 0)
          num3 = (short) -36;
        Main.tile[index2, index1].frameX += num3;
        Main.tile[index2, index1 + 1].frameX += num3;
        Main.tile[index2 + 1, index1].frameX += num3;
        Main.tile[index2 + 1, index1 + 1].frameX += num3;
        Wiring.SkipWire(index2, index1);
        Wiring.SkipWire(index2 + 1, index1);
        Wiring.SkipWire(index2, index1 + 1);
        Wiring.SkipWire(index2 + 1, index1 + 1);
        NetMessage.SendTileSquare(-1, index2, index1, 3, TileChangeType.None);
      }
      else if (type == 34)
      {
        int num1 = (int) tile1.frameY / 18;
        while (num1 >= 3)
          num1 -= 3;
        int index1 = j - num1;
        int num2 = (int) tile1.frameX % 108 / 18;
        if (num2 > 2)
          num2 -= 3;
        int index2 = i - num2;
        short num3 = 54;
        if ((int) Main.tile[index2, index1].frameX % 108 > 0)
          num3 = (short) -54;
        for (int x = index2; x < index2 + 3; ++x)
        {
          for (int y = index1; y < index1 + 3; ++y)
          {
            Main.tile[x, y].frameX += num3;
            Wiring.SkipWire(x, y);
          }
        }
        NetMessage.SendTileSquare(-1, index2 + 1, index1 + 1, 3, TileChangeType.None);
      }
      else if (type == 314)
      {
        if (!Wiring.CheckMech(i, j, 5))
          return;
        Minecart.FlipSwitchTrack(i, j);
      }
      else if (type == 33 || type == 174)
      {
        short num = 18;
        if ((int) tile1.frameX > 0)
          num = (short) -18;
        tile1.frameX += num;
        NetMessage.SendTileSquare(-1, i, j, 3, TileChangeType.None);
      }
      else if (type == 92)
      {
        int num1 = j - (int) tile1.frameY / 18;
        short num2 = 18;
        if ((int) tile1.frameX > 0)
          num2 = (short) -18;
        for (int y = num1; y < num1 + 6; ++y)
        {
          Main.tile[i, y].frameX += num2;
          Wiring.SkipWire(i, y);
        }
        NetMessage.SendTileSquare(-1, i, num1 + 3, 7, TileChangeType.None);
      }
      else if (type == 137)
      {
        int num1 = (int) tile1.frameY / 18;
        Vector2 zero = Vector2.get_Zero();
        float SpeedX = 0.0f;
        float SpeedY = 0.0f;
        int Type = 0;
        int Damage = 0;
        switch (num1)
        {
          case 0:
          case 1:
          case 2:
            if (Wiring.CheckMech(i, j, 200))
            {
              int num2 = (int) tile1.frameX == 0 ? -1 : ((int) tile1.frameX == 18 ? 1 : 0);
              int num3 = (int) tile1.frameX < 36 ? 0 : ((int) tile1.frameX < 72 ? -1 : 1);
              // ISSUE: explicit reference operation
              ((Vector2) @zero).\u002Ector((float) (i * 16 + 8 + 10 * num2), (float) (j * 16 + 9 + num3 * 9));
              float num4 = 3f;
              if (num1 == 0)
              {
                Type = 98;
                Damage = 20;
                num4 = 12f;
              }
              if (num1 == 1)
              {
                Type = 184;
                Damage = 40;
                num4 = 12f;
              }
              if (num1 == 2)
              {
                Type = 187;
                Damage = 40;
                num4 = 5f;
              }
              SpeedX = (float) num2 * num4;
              SpeedY = (float) num3 * num4;
              break;
            }
            break;
          case 3:
            if (Wiring.CheckMech(i, j, 300))
            {
              int num2 = 200;
              for (int index = 0; index < 1000; ++index)
              {
                if (Main.projectile[index].active && Main.projectile[index].type == Type)
                {
                  Vector2 vector2 = Vector2.op_Subtraction(new Vector2((float) (i * 16 + 8), (float) (j * 18 + 8)), Main.projectile[index].Center);
                  // ISSUE: explicit reference operation
                  float num3 = ((Vector2) @vector2).Length();
                  if ((double) num3 < 50.0)
                    num2 -= 50;
                  else if ((double) num3 < 100.0)
                    num2 -= 15;
                  else if ((double) num3 < 200.0)
                    num2 -= 10;
                  else if ((double) num3 < 300.0)
                    num2 -= 8;
                  else if ((double) num3 < 400.0)
                    num2 -= 6;
                  else if ((double) num3 < 500.0)
                    num2 -= 5;
                  else if ((double) num3 < 700.0)
                    num2 -= 4;
                  else if ((double) num3 < 900.0)
                    num2 -= 3;
                  else if ((double) num3 < 1200.0)
                    num2 -= 2;
                  else
                    --num2;
                }
              }
              if (num2 > 0)
              {
                Type = 185;
                Damage = 40;
                int num3 = 0;
                int num4 = 0;
                switch ((int) tile1.frameX / 18)
                {
                  case 0:
                  case 1:
                    num3 = 0;
                    num4 = 1;
                    break;
                  case 2:
                    num3 = 0;
                    num4 = -1;
                    break;
                  case 3:
                    num3 = -1;
                    num4 = 0;
                    break;
                  case 4:
                    num3 = 1;
                    num4 = 0;
                    break;
                }
                SpeedX = (float) (4 * num3) + (float) Main.rand.Next((num3 == 1 ? 20 : 0) - 20, 21 - (num3 == -1 ? 20 : 0)) * 0.05f;
                SpeedY = (float) (4 * num4) + (float) Main.rand.Next((num4 == 1 ? 20 : 0) - 20, 21 - (num4 == -1 ? 20 : 0)) * 0.05f;
                // ISSUE: explicit reference operation
                ((Vector2) @zero).\u002Ector((float) (i * 16 + 8 + 14 * num3), (float) (j * 16 + 8 + 14 * num4));
                break;
              }
              break;
            }
            break;
          case 4:
            if (Wiring.CheckMech(i, j, 90))
            {
              int num2 = 0;
              int num3 = 0;
              switch ((int) tile1.frameX / 18)
              {
                case 0:
                case 1:
                  num2 = 0;
                  num3 = 1;
                  break;
                case 2:
                  num2 = 0;
                  num3 = -1;
                  break;
                case 3:
                  num2 = -1;
                  num3 = 0;
                  break;
                case 4:
                  num2 = 1;
                  num3 = 0;
                  break;
              }
              SpeedX = (float) (8 * num2);
              SpeedY = (float) (8 * num3);
              Damage = 60;
              Type = 186;
              // ISSUE: explicit reference operation
              ((Vector2) @zero).\u002Ector((float) (i * 16 + 8 + 18 * num2), (float) (j * 16 + 8 + 18 * num3));
              break;
            }
            break;
        }
        switch (num1 + 10)
        {
          case 0:
            if (Wiring.CheckMech(i, j, 200))
            {
              int num2 = -1;
              if ((int) tile1.frameX != 0)
                num2 = 1;
              SpeedX = (float) (12 * num2);
              Damage = 20;
              Type = 98;
              // ISSUE: explicit reference operation
              ((Vector2) @zero).\u002Ector((float) (i * 16 + 8), (float) (j * 16 + 7));
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local1 = @zero;
              // ISSUE: explicit reference operation
              double num3 = (^local1).X + (double) (10 * num2);
              // ISSUE: explicit reference operation
              (^local1).X = (__Null) num3;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local2 = @zero;
              // ISSUE: explicit reference operation
              double num4 = (^local2).Y + 2.0;
              // ISSUE: explicit reference operation
              (^local2).Y = (__Null) num4;
              break;
            }
            break;
          case 1:
            if (Wiring.CheckMech(i, j, 200))
            {
              int num2 = -1;
              if ((int) tile1.frameX != 0)
                num2 = 1;
              SpeedX = (float) (12 * num2);
              Damage = 40;
              Type = 184;
              // ISSUE: explicit reference operation
              ((Vector2) @zero).\u002Ector((float) (i * 16 + 8), (float) (j * 16 + 7));
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local1 = @zero;
              // ISSUE: explicit reference operation
              double num3 = (^local1).X + (double) (10 * num2);
              // ISSUE: explicit reference operation
              (^local1).X = (__Null) num3;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local2 = @zero;
              // ISSUE: explicit reference operation
              double num4 = (^local2).Y + 2.0;
              // ISSUE: explicit reference operation
              (^local2).Y = (__Null) num4;
              break;
            }
            break;
          case 2:
            if (Wiring.CheckMech(i, j, 200))
            {
              int num2 = -1;
              if ((int) tile1.frameX != 0)
                num2 = 1;
              SpeedX = (float) (5 * num2);
              Damage = 40;
              Type = 187;
              // ISSUE: explicit reference operation
              ((Vector2) @zero).\u002Ector((float) (i * 16 + 8), (float) (j * 16 + 7));
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local1 = @zero;
              // ISSUE: explicit reference operation
              double num3 = (^local1).X + (double) (10 * num2);
              // ISSUE: explicit reference operation
              (^local1).X = (__Null) num3;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local2 = @zero;
              // ISSUE: explicit reference operation
              double num4 = (^local2).Y + 2.0;
              // ISSUE: explicit reference operation
              (^local2).Y = (__Null) num4;
              break;
            }
            break;
          case 3:
            if (Wiring.CheckMech(i, j, 300))
            {
              Type = 185;
              int num2 = 200;
              for (int index = 0; index < 1000; ++index)
              {
                if (Main.projectile[index].active && Main.projectile[index].type == Type)
                {
                  Vector2 vector2 = Vector2.op_Subtraction(new Vector2((float) (i * 16 + 8), (float) (j * 18 + 8)), Main.projectile[index].Center);
                  // ISSUE: explicit reference operation
                  float num3 = ((Vector2) @vector2).Length();
                  if ((double) num3 < 50.0)
                    num2 -= 50;
                  else if ((double) num3 < 100.0)
                    num2 -= 15;
                  else if ((double) num3 < 200.0)
                    num2 -= 10;
                  else if ((double) num3 < 300.0)
                    num2 -= 8;
                  else if ((double) num3 < 400.0)
                    num2 -= 6;
                  else if ((double) num3 < 500.0)
                    num2 -= 5;
                  else if ((double) num3 < 700.0)
                    num2 -= 4;
                  else if ((double) num3 < 900.0)
                    num2 -= 3;
                  else if ((double) num3 < 1200.0)
                    num2 -= 2;
                  else
                    --num2;
                }
              }
              if (num2 > 0)
              {
                SpeedX = (float) Main.rand.Next(-20, 21) * 0.05f;
                SpeedY = (float) (4.0 + (double) Main.rand.Next(0, 21) * 0.0500000007450581);
                Damage = 40;
                // ISSUE: explicit reference operation
                ((Vector2) @zero).\u002Ector((float) (i * 16 + 8), (float) (j * 16 + 16));
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                Vector2& local = @zero;
                // ISSUE: explicit reference operation
                double num3 = (^local).Y + 6.0;
                // ISSUE: explicit reference operation
                (^local).Y = (__Null) num3;
                Projectile.NewProjectile((float) (int) zero.X, (float) (int) zero.Y, SpeedX, SpeedY, Type, Damage, 2f, Main.myPlayer, 0.0f, 0.0f);
                break;
              }
              break;
            }
            break;
          case 4:
            if (Wiring.CheckMech(i, j, 90))
            {
              SpeedX = 0.0f;
              SpeedY = 8f;
              Damage = 60;
              Type = 186;
              // ISSUE: explicit reference operation
              ((Vector2) @zero).\u002Ector((float) (i * 16 + 8), (float) (j * 16 + 16));
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local = @zero;
              // ISSUE: explicit reference operation
              double num2 = (^local).Y + 10.0;
              // ISSUE: explicit reference operation
              (^local).Y = (__Null) num2;
              break;
            }
            break;
        }
        if (Type == 0)
          return;
        Projectile.NewProjectile((float) (int) zero.X, (float) (int) zero.Y, SpeedX, SpeedY, Type, Damage, 2f, Main.myPlayer, 0.0f, 0.0f);
      }
      else if (type == 443)
      {
        int num = (int) tile1.frameX / 36;
        int i1 = i - ((int) tile1.frameX - num * 36) / 18;
        int j1 = j;
        if (!Wiring.CheckMech(i1, j1, 200))
          return;
        Vector2.get_Zero();
        Vector2 zero = Vector2.get_Zero();
        int Type = 654;
        int Damage = 20;
        Vector2 vector2;
        if (num < 2)
        {
          vector2 = Vector2.op_Multiply(new Vector2((float) (i1 + 1), (float) j1), 16f);
          // ISSUE: explicit reference operation
          ((Vector2) @zero).\u002Ector(0.0f, -8f);
        }
        else
        {
          vector2 = Vector2.op_Multiply(new Vector2((float) (i1 + 1), (float) (j1 + 1)), 16f);
          // ISSUE: explicit reference operation
          ((Vector2) @zero).\u002Ector(0.0f, 8f);
        }
        if (Type == 0)
          return;
        Projectile.NewProjectile((float) (int) vector2.X, (float) (int) vector2.Y, (float) zero.X, (float) zero.Y, Type, Damage, 2f, Main.myPlayer, 0.0f, 0.0f);
      }
      else if (type == 139 || type == 35)
        WorldGen.SwitchMB(i, j);
      else if (type == 207)
        WorldGen.SwitchFountain(i, j);
      else if (type == 410)
        WorldGen.SwitchMonolith(i, j);
      else if (type == 455)
        BirthdayParty.ToggleManualParty();
      else if (type == 141)
      {
        WorldGen.KillTile(i, j, false, false, true);
        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
        Projectile.NewProjectile((float) (i * 16 + 8), (float) (j * 16 + 8), 0.0f, 0.0f, 108, 500, 10f, Main.myPlayer, 0.0f, 0.0f);
      }
      else if (type == 210)
        WorldGen.ExplodeMine(i, j);
      else if (type == 142 || type == 143)
      {
        int y = j - (int) tile1.frameY / 18;
        int num1 = (int) tile1.frameX / 18;
        if (num1 > 1)
          num1 -= 2;
        int x = i - num1;
        Wiring.SkipWire(x, y);
        Wiring.SkipWire(x, y + 1);
        Wiring.SkipWire(x + 1, y);
        Wiring.SkipWire(x + 1, y + 1);
        if (type == 142)
        {
          for (int index = 0; index < 4 && Wiring._numInPump < 19; ++index)
          {
            int num2;
            int num3;
            if (index == 0)
            {
              num2 = x;
              num3 = y + 1;
            }
            else if (index == 1)
            {
              num2 = x + 1;
              num3 = y + 1;
            }
            else if (index == 2)
            {
              num2 = x;
              num3 = y;
            }
            else
            {
              num2 = x + 1;
              num3 = y;
            }
            Wiring._inPumpX[Wiring._numInPump] = num2;
            Wiring._inPumpY[Wiring._numInPump] = num3;
            ++Wiring._numInPump;
          }
        }
        else
        {
          for (int index = 0; index < 4 && Wiring._numOutPump < 19; ++index)
          {
            int num2;
            int num3;
            if (index == 0)
            {
              num2 = x;
              num3 = y + 1;
            }
            else if (index == 1)
            {
              num2 = x + 1;
              num3 = y + 1;
            }
            else if (index == 2)
            {
              num2 = x;
              num3 = y;
            }
            else
            {
              num2 = x + 1;
              num3 = y;
            }
            Wiring._outPumpX[Wiring._numOutPump] = num2;
            Wiring._outPumpY[Wiring._numOutPump] = num3;
            ++Wiring._numOutPump;
          }
        }
      }
      else if (type == 105)
      {
        int num1 = j - (int) tile1.frameY / 18;
        int num2 = (int) tile1.frameX / 18;
        int num3 = 0;
        while (num2 >= 2)
        {
          num2 -= 2;
          ++num3;
        }
        int num4 = i - num2;
        int num5 = i - (int) tile1.frameX % 36 / 18;
        int num6 = j - (int) tile1.frameY % 54 / 18;
        int num7 = (int) tile1.frameX / 36 + (int) tile1.frameY / 54 * 55;
        Wiring.SkipWire(num5, num6);
        Wiring.SkipWire(num5, num6 + 1);
        Wiring.SkipWire(num5, num6 + 2);
        Wiring.SkipWire(num5 + 1, num6);
        Wiring.SkipWire(num5 + 1, num6 + 1);
        Wiring.SkipWire(num5 + 1, num6 + 2);
        int X = num5 * 16 + 16;
        int Y = (num6 + 3) * 16;
        int index1 = -1;
        int num8 = -1;
        bool flag1 = true;
        bool flag2 = false;
        switch (num7)
        {
          case 51:
            num8 = (int) Utils.SelectRandom<short>(Main.rand, new short[2]
            {
              (short) 299,
              (short) 538
            });
            break;
          case 52:
            num8 = 356;
            break;
          case 53:
            num8 = 357;
            break;
          case 54:
            num8 = (int) Utils.SelectRandom<short>(Main.rand, new short[2]
            {
              (short) 355,
              (short) 358
            });
            break;
          case 55:
            num8 = (int) Utils.SelectRandom<short>(Main.rand, new short[2]
            {
              (short) 367,
              (short) 366
            });
            break;
          case 56:
            num8 = (int) Utils.SelectRandom<short>(Main.rand, new short[5]
            {
              (short) 359,
              (short) 359,
              (short) 359,
              (short) 359,
              (short) 360
            });
            break;
          case 57:
            num8 = 377;
            break;
          case 58:
            num8 = 300;
            break;
          case 59:
            num8 = (int) Utils.SelectRandom<short>(Main.rand, new short[2]
            {
              (short) 364,
              (short) 362
            });
            break;
          case 60:
            num8 = 148;
            break;
          case 61:
            num8 = 361;
            break;
          case 62:
            num8 = (int) Utils.SelectRandom<short>(Main.rand, new short[3]
            {
              (short) 487,
              (short) 486,
              (short) 485
            });
            break;
          case 63:
            num8 = 164;
            flag1 &= NPC.MechSpawn((float) X, (float) Y, 165);
            break;
          case 64:
            num8 = 86;
            flag2 = true;
            break;
          case 65:
            num8 = 490;
            break;
          case 66:
            num8 = 82;
            break;
          case 67:
            num8 = 449;
            break;
          case 68:
            num8 = 167;
            break;
          case 69:
            num8 = 480;
            break;
          case 70:
            num8 = 48;
            break;
          case 71:
            num8 = (int) Utils.SelectRandom<short>(Main.rand, new short[3]
            {
              (short) 170,
              (short) 180,
              (short) 171
            });
            flag2 = true;
            break;
          case 72:
            num8 = 481;
            break;
          case 73:
            num8 = 482;
            break;
          case 74:
            num8 = 430;
            break;
          case 75:
            num8 = 489;
            break;
        }
        if (num8 != -1 && Wiring.CheckMech(num5, num6, 30) && (NPC.MechSpawn((float) X, (float) Y, num8) && flag1))
        {
          if (!flag2 || !Collision.SolidTiles(num5 - 2, num5 + 3, num6, num6 + 2))
          {
            index1 = NPC.NewNPC(X, Y - 12, num8, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          }
          else
          {
            Vector2 position = Vector2.op_Subtraction(new Vector2((float) (X - 4), (float) (Y - 22)), new Vector2(10f));
            Utils.PoofOfSmoke(position);
            NetMessage.SendData(106, -1, -1, (NetworkText) null, (int) position.X, (float) position.Y, 0.0f, 0.0f, 0, 0, 0);
          }
        }
        if (index1 <= -1)
        {
          if (num7 == 4)
          {
            if (Wiring.CheckMech(num5, num6, 30) && NPC.MechSpawn((float) X, (float) Y, 1))
              index1 = NPC.NewNPC(X, Y - 12, 1, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          }
          else if (num7 == 7)
          {
            if (Wiring.CheckMech(num5, num6, 30) && NPC.MechSpawn((float) X, (float) Y, 49))
              index1 = NPC.NewNPC(X - 4, Y - 6, 49, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          }
          else if (num7 == 8)
          {
            if (Wiring.CheckMech(num5, num6, 30) && NPC.MechSpawn((float) X, (float) Y, 55))
              index1 = NPC.NewNPC(X, Y - 12, 55, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          }
          else if (num7 == 9)
          {
            if (Wiring.CheckMech(num5, num6, 30) && NPC.MechSpawn((float) X, (float) Y, 46))
              index1 = NPC.NewNPC(X, Y - 12, 46, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          }
          else if (num7 == 10)
          {
            if (Wiring.CheckMech(num5, num6, 30) && NPC.MechSpawn((float) X, (float) Y, 21))
              index1 = NPC.NewNPC(X, Y, 21, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          }
          else if (num7 == 18)
          {
            if (Wiring.CheckMech(num5, num6, 30) && NPC.MechSpawn((float) X, (float) Y, 67))
              index1 = NPC.NewNPC(X, Y - 12, 67, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          }
          else if (num7 == 23)
          {
            if (Wiring.CheckMech(num5, num6, 30) && NPC.MechSpawn((float) X, (float) Y, 63))
              index1 = NPC.NewNPC(X, Y - 12, 63, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          }
          else if (num7 == 27)
          {
            if (Wiring.CheckMech(num5, num6, 30) && NPC.MechSpawn((float) X, (float) Y, 85))
              index1 = NPC.NewNPC(X - 9, Y, 85, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          }
          else if (num7 == 28)
          {
            if (Wiring.CheckMech(num5, num6, 30) && NPC.MechSpawn((float) X, (float) Y, 74))
              index1 = NPC.NewNPC(X, Y - 12, (int) Utils.SelectRandom<short>(Main.rand, new short[3]
              {
                (short) 74,
                (short) 297,
                (short) 298
              }), 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          }
          else if (num7 == 34)
          {
            for (int index2 = 0; index2 < 2; ++index2)
            {
              for (int index3 = 0; index3 < 3; ++index3)
              {
                Tile tile2 = Main.tile[num5 + index2, num6 + index3];
                tile2.type = (ushort) 349;
                tile2.frameX = (short) (index2 * 18 + 216);
                tile2.frameY = (short) (index3 * 18);
              }
            }
            Animation.NewTemporaryAnimation(0, (ushort) 349, num5, num6);
            if (Main.netMode == 2)
              NetMessage.SendTileRange(-1, num5, num6, 2, 3, TileChangeType.None);
          }
          else if (num7 == 42)
          {
            if (Wiring.CheckMech(num5, num6, 30) && NPC.MechSpawn((float) X, (float) Y, 58))
              index1 = NPC.NewNPC(X, Y - 12, 58, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          }
          else if (num7 == 37)
          {
            if (Wiring.CheckMech(num5, num6, 600) && Item.MechSpawn((float) X, (float) Y, 58) && (Item.MechSpawn((float) X, (float) Y, 1734) && Item.MechSpawn((float) X, (float) Y, 1867)))
              Item.NewItem(X, Y - 16, 0, 0, 58, 1, false, 0, false, false);
          }
          else if (num7 == 50)
          {
            if (Wiring.CheckMech(num5, num6, 30) && NPC.MechSpawn((float) X, (float) Y, 65))
            {
              if (!Collision.SolidTiles(num5 - 2, num5 + 3, num6, num6 + 2))
              {
                index1 = NPC.NewNPC(X, Y - 12, 65, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
              }
              else
              {
                Vector2 position = Vector2.op_Subtraction(new Vector2((float) (X - 4), (float) (Y - 22)), new Vector2(10f));
                Utils.PoofOfSmoke(position);
                NetMessage.SendData(106, -1, -1, (NetworkText) null, (int) position.X, (float) position.Y, 0.0f, 0.0f, 0, 0, 0);
              }
            }
          }
          else if (num7 == 2)
          {
            if (Wiring.CheckMech(num5, num6, 600) && Item.MechSpawn((float) X, (float) Y, 184) && (Item.MechSpawn((float) X, (float) Y, 1735) && Item.MechSpawn((float) X, (float) Y, 1868)))
              Item.NewItem(X, Y - 16, 0, 0, 184, 1, false, 0, false, false);
          }
          else if (num7 == 17)
          {
            if (Wiring.CheckMech(num5, num6, 600) && Item.MechSpawn((float) X, (float) Y, 166))
              Item.NewItem(X, Y - 20, 0, 0, 166, 1, false, 0, false, false);
          }
          else if (num7 == 40)
          {
            if (Wiring.CheckMech(num5, num6, 300))
            {
              int[] numArray = new int[10];
              int maxValue = 0;
              for (int index2 = 0; index2 < 200; ++index2)
              {
                if (Main.npc[index2].active && (Main.npc[index2].type == 17 || Main.npc[index2].type == 19 || (Main.npc[index2].type == 22 || Main.npc[index2].type == 38) || (Main.npc[index2].type == 54 || Main.npc[index2].type == 107 || (Main.npc[index2].type == 108 || Main.npc[index2].type == 142)) || (Main.npc[index2].type == 160 || Main.npc[index2].type == 207 || (Main.npc[index2].type == 209 || Main.npc[index2].type == 227) || (Main.npc[index2].type == 228 || Main.npc[index2].type == 229 || (Main.npc[index2].type == 358 || Main.npc[index2].type == 369))) || Main.npc[index2].type == 550))
                {
                  numArray[maxValue] = index2;
                  ++maxValue;
                  if (maxValue >= 9)
                    break;
                }
              }
              if (maxValue > 0)
              {
                int number = numArray[Main.rand.Next(maxValue)];
                Main.npc[number].position.X = (__Null) (double) (X - Main.npc[number].width / 2);
                Main.npc[number].position.Y = (__Null) (double) (Y - Main.npc[number].height - 1);
                NetMessage.SendData(23, -1, -1, (NetworkText) null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              }
            }
          }
          else if (num7 == 41 && Wiring.CheckMech(num5, num6, 300))
          {
            int[] numArray = new int[10];
            int maxValue = 0;
            for (int index2 = 0; index2 < 200; ++index2)
            {
              if (Main.npc[index2].active && (Main.npc[index2].type == 18 || Main.npc[index2].type == 20 || (Main.npc[index2].type == 124 || Main.npc[index2].type == 178) || (Main.npc[index2].type == 208 || Main.npc[index2].type == 353)))
              {
                numArray[maxValue] = index2;
                ++maxValue;
                if (maxValue >= 9)
                  break;
              }
            }
            if (maxValue > 0)
            {
              int number = numArray[Main.rand.Next(maxValue)];
              Main.npc[number].position.X = (__Null) (double) (X - Main.npc[number].width / 2);
              Main.npc[number].position.Y = (__Null) (double) (Y - Main.npc[number].height - 1);
              NetMessage.SendData(23, -1, -1, (NetworkText) null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
          }
        }
        if (index1 < 0)
          return;
        Main.npc[index1].value = 0.0f;
        Main.npc[index1].npcSlots = 0.0f;
        Main.npc[index1].SpawnedFromStatue = true;
      }
      else
      {
        if (type != 349)
          return;
        int index1 = j - (int) tile1.frameY / 18;
        int num1 = (int) tile1.frameX / 18;
        while (num1 >= 2)
          num1 -= 2;
        int index2 = i - num1;
        Wiring.SkipWire(index2, index1);
        Wiring.SkipWire(index2, index1 + 1);
        Wiring.SkipWire(index2, index1 + 2);
        Wiring.SkipWire(index2 + 1, index1);
        Wiring.SkipWire(index2 + 1, index1 + 1);
        Wiring.SkipWire(index2 + 1, index1 + 2);
        short num2 = (int) Main.tile[index2, index1].frameX != 0 ? (short) -216 : (short) 216;
        for (int index3 = 0; index3 < 2; ++index3)
        {
          for (int index4 = 0; index4 < 3; ++index4)
            Main.tile[index2 + index3, index1 + index4].frameX += num2;
        }
        if (Main.netMode == 2)
          NetMessage.SendTileRange(-1, index2, index1, 2, 3, TileChangeType.None);
        Animation.NewTemporaryAnimation((int) num2 > 0 ? 0 : 1, (ushort) 349, index2, index1);
      }
    }

    private static void Teleport()
    {
      if (Wiring._teleport[0].X < Wiring._teleport[1].X + 3.0 && Wiring._teleport[0].X > Wiring._teleport[1].X - 3.0 && (Wiring._teleport[0].Y > Wiring._teleport[1].Y - 3.0 && Wiring._teleport[0].Y < Wiring._teleport[1].Y))
        return;
      Rectangle[] rectangleArray = new Rectangle[2];
      rectangleArray[0].X = (__Null) (int) (Wiring._teleport[0].X * 16.0);
      rectangleArray[0].Width = (__Null) 48;
      rectangleArray[0].Height = (__Null) 48;
      rectangleArray[0].Y = (__Null) (int) (Wiring._teleport[0].Y * 16.0 - (double) (float) rectangleArray[0].Height);
      rectangleArray[1].X = (__Null) (int) (Wiring._teleport[1].X * 16.0);
      rectangleArray[1].Width = (__Null) 48;
      rectangleArray[1].Height = (__Null) 48;
      rectangleArray[1].Y = (__Null) (int) (Wiring._teleport[1].Y * 16.0 - (double) (float) rectangleArray[1].Height);
      for (int index1 = 0; index1 < 2; ++index1)
      {
        Vector2 vector2_1;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2_1).\u002Ector((float) (rectangleArray[1].X - rectangleArray[0].X), (float) (rectangleArray[1].Y - rectangleArray[0].Y));
        if (index1 == 1)
        {
          // ISSUE: explicit reference operation
          ((Vector2) @vector2_1).\u002Ector((float) (rectangleArray[0].X - rectangleArray[1].X), (float) (rectangleArray[0].Y - rectangleArray[1].Y));
        }
        if (!Wiring.blockPlayerTeleportationForOneIteration)
        {
          for (int playerIndex = 0; playerIndex < (int) byte.MaxValue; ++playerIndex)
          {
            // ISSUE: explicit reference operation
            if (Main.player[playerIndex].active && !Main.player[playerIndex].dead && (!Main.player[playerIndex].teleporting && ((Rectangle) @rectangleArray[index1]).Intersects(Main.player[playerIndex].getRect())))
            {
              Vector2 vector2_2 = Vector2.op_Addition(Main.player[playerIndex].position, vector2_1);
              Main.player[playerIndex].teleporting = true;
              if (Main.netMode == 2)
                RemoteClient.CheckSection(playerIndex, vector2_2, 1);
              Main.player[playerIndex].Teleport(vector2_2, 0, 0);
              if (Main.netMode == 2)
                NetMessage.SendData(65, -1, -1, (NetworkText) null, 0, (float) playerIndex, (float) vector2_2.X, (float) vector2_2.Y, 0, 0, 0);
            }
          }
        }
        for (int index2 = 0; index2 < 200; ++index2)
        {
          if (Main.npc[index2].active && !Main.npc[index2].teleporting && (Main.npc[index2].lifeMax > 5 && !Main.npc[index2].boss) && !Main.npc[index2].noTileCollide)
          {
            int type = Main.npc[index2].type;
            // ISSUE: explicit reference operation
            if (!NPCID.Sets.TeleportationImmune[type] && ((Rectangle) @rectangleArray[index1]).Intersects(Main.npc[index2].getRect()))
            {
              Main.npc[index2].teleporting = true;
              Main.npc[index2].Teleport(Vector2.op_Addition(Main.npc[index2].position, vector2_1), 0, 0);
            }
          }
        }
      }
      for (int index = 0; index < (int) byte.MaxValue; ++index)
        Main.player[index].teleporting = false;
      for (int index = 0; index < 200; ++index)
        Main.npc[index].teleporting = false;
    }

    private static void DeActive(int i, int j)
    {
      if (!Main.tile[i, j].active())
        return;
      bool flag = Main.tileSolid[(int) Main.tile[i, j].type] && !TileID.Sets.NotReallySolid[(int) Main.tile[i, j].type];
      switch (Main.tile[i, j].type)
      {
        case 314:
        case 386:
        case 387:
        case 388:
        case 389:
          flag = false;
          break;
      }
      if (!flag || Main.tile[i, j - 1].active() && ((int) Main.tile[i, j - 1].type == 5 || TileID.Sets.BasicChest[(int) Main.tile[i, j - 1].type] || ((int) Main.tile[i, j - 1].type == 26 || (int) Main.tile[i, j - 1].type == 77) || ((int) Main.tile[i, j - 1].type == 72 || (int) Main.tile[i, j - 1].type == 88)))
        return;
      Main.tile[i, j].inActive(true);
      WorldGen.SquareTileFrame(i, j, false);
      if (Main.netMode == 1)
        return;
      NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
    }

    private static void ReActive(int i, int j)
    {
      Main.tile[i, j].inActive(false);
      WorldGen.SquareTileFrame(i, j, false);
      if (Main.netMode == 1)
        return;
      NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
    }

    private static void MassWireOperationInner(Point ps, Point pe, Vector2 dropPoint, bool dir, ref int wireCount, ref int actuatorCount)
    {
      Math.Abs((int) (ps.X - pe.X));
      Math.Abs((int) (ps.Y - pe.Y));
      int num1 = Math.Sign((int) (pe.X - ps.X));
      int num2 = Math.Sign((int) (pe.Y - ps.Y));
      WiresUI.Settings.MultiToolMode toolMode = WiresUI.Settings.ToolMode;
      Point pt = (Point) null;
      bool flag1 = false;
      Item.StartCachingType(530);
      Item.StartCachingType(849);
      bool flag2 = dir;
      int num3;
      int num4;
      int num5;
      if (flag2)
      {
        pt.X = ps.X;
        num3 = (int) ps.Y;
        num4 = (int) pe.Y;
        num5 = num2;
      }
      else
      {
        pt.Y = ps.Y;
        num3 = (int) ps.X;
        num4 = (int) pe.X;
        num5 = num1;
      }
      int num6 = num3;
      while (num6 != num4 && !flag1)
      {
        if (flag2)
          pt.Y = (__Null) num6;
        else
          pt.X = (__Null) num6;
        bool? nullable = Wiring.MassWireOperationStep(pt, toolMode, ref wireCount, ref actuatorCount);
        if (nullable.HasValue && !nullable.Value)
        {
          flag1 = true;
          break;
        }
        num6 += num5;
      }
      int num7;
      int num8;
      int num9;
      if (flag2)
      {
        pt.Y = pe.Y;
        num7 = (int) ps.X;
        num8 = (int) pe.X;
        num9 = num1;
      }
      else
      {
        pt.X = pe.X;
        num7 = (int) ps.Y;
        num8 = (int) pe.Y;
        num9 = num2;
      }
      int num10 = num7;
      while (num10 != num8 && !flag1)
      {
        if (!flag2)
          pt.Y = (__Null) num10;
        else
          pt.X = (__Null) num10;
        bool? nullable = Wiring.MassWireOperationStep(pt, toolMode, ref wireCount, ref actuatorCount);
        if (nullable.HasValue && !nullable.Value)
        {
          flag1 = true;
          break;
        }
        num10 += num9;
      }
      if (!flag1)
        Wiring.MassWireOperationStep(pe, toolMode, ref wireCount, ref actuatorCount);
      Item.DropCache(dropPoint, Vector2.get_Zero(), 530, true);
      Item.DropCache(dropPoint, Vector2.get_Zero(), 849, true);
    }

    private static bool? MassWireOperationStep(Point pt, WiresUI.Settings.MultiToolMode mode, ref int wiresLeftToConsume, ref int actuatorsLeftToConstume)
    {
      if (!WorldGen.InWorld((int) pt.X, (int) pt.Y, 1))
        return new bool?();
      Tile tile = Main.tile[(int) pt.X, (int) pt.Y];
      if (tile == null)
        return new bool?();
      if (!mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Cutter))
      {
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Red) && !tile.wire())
        {
          if (wiresLeftToConsume <= 0)
            return new bool?(false);
          --wiresLeftToConsume;
          WorldGen.PlaceWire((int) pt.X, (int) pt.Y);
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 5, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        }
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Green) && !tile.wire3())
        {
          if (wiresLeftToConsume <= 0)
            return new bool?(false);
          --wiresLeftToConsume;
          WorldGen.PlaceWire3((int) pt.X, (int) pt.Y);
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 12, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        }
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Blue) && !tile.wire2())
        {
          if (wiresLeftToConsume <= 0)
            return new bool?(false);
          --wiresLeftToConsume;
          WorldGen.PlaceWire2((int) pt.X, (int) pt.Y);
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 10, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        }
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Yellow) && !tile.wire4())
        {
          if (wiresLeftToConsume <= 0)
            return new bool?(false);
          --wiresLeftToConsume;
          WorldGen.PlaceWire4((int) pt.X, (int) pt.Y);
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 16, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        }
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Actuator) && !tile.actuator())
        {
          if (actuatorsLeftToConstume <= 0)
            return new bool?(false);
          --actuatorsLeftToConstume;
          WorldGen.PlaceActuator((int) pt.X, (int) pt.Y);
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 8, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        }
      }
      if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Cutter))
      {
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Red) && tile.wire() && WorldGen.KillWire((int) pt.X, (int) pt.Y))
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 6, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Green) && tile.wire3() && WorldGen.KillWire3((int) pt.X, (int) pt.Y))
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 13, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Blue) && tile.wire2() && WorldGen.KillWire2((int) pt.X, (int) pt.Y))
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 11, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Yellow) && tile.wire4() && WorldGen.KillWire4((int) pt.X, (int) pt.Y))
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 17, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Actuator) && tile.actuator() && WorldGen.KillActuator((int) pt.X, (int) pt.Y))
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 9, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
      }
      return new bool?(true);
    }
  }
}
