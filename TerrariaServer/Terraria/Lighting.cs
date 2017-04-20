// Decompiled with JetBrains decompiler
// Type: Terraria.Lighting
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria
{
  public class Lighting
  {
    public static int maxRenderCount = 4;
    public static float brightness = 1f;
    public static float defBrightness = 1f;
    public static int lightMode = 0;
    public static bool RGB = true;
    private static float oldSkyColor = 0.0f;
    private static float skyColor = 0.0f;
    private static int lightCounter = 0;
    public static int offScreenTiles = 45;
    public static int offScreenTiles2 = 35;
    public static int LightingThreads = 0;
    private static int maxTempLights = 2000;
    private static float negLight = 0.04f;
    private static float negLight2 = 0.16f;
    private static float wetLightR = 0.16f;
    private static float wetLightG = 0.16f;
    private static float wetLightB = 0.16f;
    private static float honeyLightR = 0.16f;
    private static float honeyLightG = 0.16f;
    private static float honeyLightB = 0.16f;
    private static float blueWave = 1f;
    private static int blueDir = 1;
    private static int firstTileX;
    private static int lastTileX;
    private static int firstTileY;
    private static int lastTileY;
    private static Lighting.LightingState[][] states;
    private static Lighting.LightingState[][] axisFlipStates;
    private static Lighting.LightingSwipeData swipe;
    private static Lighting.LightingSwipeData[] threadSwipes;
    private static CountdownEvent countdown;
    public static int scrX;
    public static int scrY;
    public static int minX;
    public static int maxX;
    public static int minY;
    public static int maxY;
    private static Dictionary<Point16, Lighting.ColorTriplet> tempLights;
    private static int firstToLightX;
    private static int firstToLightY;
    private static int lastToLightX;
    private static int lastToLightY;
    private static int minX7;
    private static int maxX7;
    private static int minY7;
    private static int maxY7;
    private static int firstTileX7;
    private static int lastTileX7;
    private static int lastTileY7;
    private static int firstTileY7;
    private static int firstToLightX7;
    private static int lastToLightX7;
    private static int firstToLightY7;
    private static int lastToLightY7;
    private static int firstToLightX27;
    private static int lastToLightX27;
    private static int firstToLightY27;
    private static int lastToLightY27;

    public static bool NotRetro
    {
      get
      {
        return Lighting.lightMode < 2;
      }
    }

    public static bool UpdateEveryFrame
    {
      get
      {
        if (Main.LightingEveryFrame && !Main.RenderTargetsRequired)
          return !Lighting.NotRetro;
        return false;
      }
    }

    public static bool LightingDrawToScreen
    {
      get
      {
        return Main.drawToScreen;
      }
    }

    public static void Initialize(bool resize = false)
    {
      if (!resize)
      {
        Lighting.tempLights = new Dictionary<Point16, Lighting.ColorTriplet>();
        Lighting.swipe = new Lighting.LightingSwipeData();
        Lighting.countdown = new CountdownEvent(0);
        Lighting.threadSwipes = new Lighting.LightingSwipeData[Environment.ProcessorCount];
        for (int index = 0; index < Lighting.threadSwipes.Length; ++index)
          Lighting.threadSwipes[index] = new Lighting.LightingSwipeData();
      }
      int length1 = Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10;
      int length2 = Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10;
      if (Lighting.states != null && Lighting.states.Length >= length1 && Lighting.states[0].Length >= length2)
        return;
      Lighting.states = new Lighting.LightingState[length1][];
      Lighting.axisFlipStates = new Lighting.LightingState[length2][];
      for (int index = 0; index < length2; ++index)
        Lighting.axisFlipStates[index] = new Lighting.LightingState[length1];
      for (int index1 = 0; index1 < length1; ++index1)
      {
        Lighting.LightingState[] lightingStateArray = new Lighting.LightingState[length2];
        for (int index2 = 0; index2 < length2; ++index2)
        {
          Lighting.LightingState lightingState = new Lighting.LightingState();
          lightingStateArray[index2] = lightingState;
          Lighting.axisFlipStates[index2][index1] = lightingState;
        }
        Lighting.states[index1] = lightingStateArray;
      }
    }

    public static void LightTiles(int firstX, int lastX, int firstY, int lastY)
    {
      Main.render = true;
      Lighting.oldSkyColor = Lighting.skyColor;
      // ISSUE: explicit reference operation
      float num1 = (float) ((Color) @Main.tileColor).get_R() / (float) byte.MaxValue;
      // ISSUE: explicit reference operation
      float num2 = (float) ((Color) @Main.tileColor).get_G() / (float) byte.MaxValue;
      // ISSUE: explicit reference operation
      float num3 = (float) ((Color) @Main.tileColor).get_B() / (float) byte.MaxValue;
      Lighting.skyColor = (float) (((double) num1 + (double) num2 + (double) num3) / 3.0);
      if (Lighting.lightMode < 2)
      {
        Lighting.brightness = 1.2f;
        Lighting.offScreenTiles2 = 34;
        Lighting.offScreenTiles = 40;
      }
      else
      {
        Lighting.brightness = 1f;
        Lighting.offScreenTiles2 = 18;
        Lighting.offScreenTiles = 23;
      }
      Lighting.brightness = 1.2f;
      if (Main.player[Main.myPlayer].blind)
        Lighting.brightness = 1f;
      Lighting.defBrightness = Lighting.brightness;
      Lighting.firstTileX = firstX;
      Lighting.lastTileX = lastX;
      Lighting.firstTileY = firstY;
      Lighting.lastTileY = lastY;
      Lighting.firstToLightX = Lighting.firstTileX - Lighting.offScreenTiles;
      Lighting.firstToLightY = Lighting.firstTileY - Lighting.offScreenTiles;
      Lighting.lastToLightX = Lighting.lastTileX + Lighting.offScreenTiles;
      Lighting.lastToLightY = Lighting.lastTileY + Lighting.offScreenTiles;
      ++Lighting.lightCounter;
      ++Main.renderCount;
      int num4 = Main.screenWidth / 16 + Lighting.offScreenTiles * 2;
      int num5 = Main.screenHeight / 16 + Lighting.offScreenTiles * 2;
      Vector2 screenLastPosition = Main.screenLastPosition;
      if (Main.renderCount < 3)
        Lighting.doColors();
      if (Main.renderCount == 2)
      {
        Vector2 screenPosition = Main.screenPosition;
        int num6 = (int) Math.Floor(Main.screenPosition.X / 16.0) - Lighting.scrX;
        int num7 = (int) Math.Floor(Main.screenPosition.Y / 16.0) - Lighting.scrY;
        if (num6 > 16)
          num6 = 0;
        if (num7 > 16)
          num7 = 0;
        int num8 = 0;
        int num9 = num4;
        int num10 = 0;
        int num11 = num5;
        if (num6 < 0)
          num8 -= num6;
        else
          num9 -= num6;
        if (num7 < 0)
          num10 -= num7;
        else
          num11 -= num7;
        if (Lighting.RGB)
        {
          int num12 = num4;
          if (Lighting.states.Length <= num12 + num6)
            num12 = Lighting.states.Length - num6 - 1;
          for (int index1 = num8; index1 < num12; ++index1)
          {
            Lighting.LightingState[] state1 = Lighting.states[index1];
            Lighting.LightingState[] state2 = Lighting.states[index1 + num6];
            int num13 = num11;
            if (state2.Length <= num13 + num6)
              num13 = state2.Length - num7 - 1;
            for (int index2 = num10; index2 < num13; ++index2)
            {
              Lighting.LightingState lightingState1 = state1[index2];
              Lighting.LightingState lightingState2 = state2[index2 + num7];
              lightingState1.r = lightingState2.r2;
              lightingState1.g = lightingState2.g2;
              lightingState1.b = lightingState2.b2;
            }
          }
        }
        else
        {
          int num12 = num9;
          if (Lighting.states.Length <= num12 + num6)
            num12 = Lighting.states.Length - num6 - 1;
          for (int index1 = num8; index1 < num12; ++index1)
          {
            Lighting.LightingState[] state1 = Lighting.states[index1];
            Lighting.LightingState[] state2 = Lighting.states[index1 + num6];
            int num13 = num11;
            if (state2.Length <= num13 + num6)
              num13 = state2.Length - num7 - 1;
            for (int index2 = num10; index2 < num13; ++index2)
            {
              Lighting.LightingState lightingState1 = state1[index2];
              Lighting.LightingState lightingState2 = state2[index2 + num7];
              lightingState1.r = lightingState2.r2;
              lightingState1.g = lightingState2.r2;
              lightingState1.b = lightingState2.r2;
            }
          }
        }
      }
      else if (!Main.renderNow)
      {
        int num6 = (int) Math.Floor(Main.screenPosition.X / 16.0) - (int) Math.Floor(screenLastPosition.X / 16.0);
        if (num6 > 5 || num6 < -5)
          num6 = 0;
        int num7;
        int num8;
        int num9;
        if (num6 < 0)
        {
          num7 = -1;
          num6 *= -1;
          num8 = num4;
          num9 = num6;
        }
        else
        {
          num7 = 1;
          num8 = 0;
          num9 = num4 - num6;
        }
        int num10 = (int) Math.Floor(Main.screenPosition.Y / 16.0) - (int) Math.Floor(screenLastPosition.Y / 16.0);
        if (num10 > 5 || num10 < -5)
          num10 = 0;
        int num11;
        int num12;
        int num13;
        if (num10 < 0)
        {
          num11 = -1;
          num10 *= -1;
          num12 = num5;
          num13 = num10;
        }
        else
        {
          num11 = 1;
          num12 = 0;
          num13 = num5 - num10;
        }
        if (num6 != 0 || num10 != 0)
        {
          int index1 = num8;
          while (index1 != num9)
          {
            Lighting.LightingState[] state1 = Lighting.states[index1];
            Lighting.LightingState[] state2 = Lighting.states[index1 + num6 * num7];
            int index2 = num12;
            while (index2 != num13)
            {
              Lighting.LightingState lightingState1 = state1[index2];
              Lighting.LightingState lightingState2 = state2[index2 + num10 * num11];
              lightingState1.r = lightingState2.r;
              lightingState1.g = lightingState2.g;
              lightingState1.b = lightingState2.b;
              index2 += num11;
            }
            index1 += num7;
          }
        }
        if (Netplay.Connection.StatusMax > 0)
          Main.mapTime = 1;
        if (Main.mapTime == 0 && Main.mapEnabled)
        {
          if (Main.renderCount == 3)
          {
            try
            {
              Main.mapTime = Main.mapTimeMax;
              Main.updateMap = true;
              Main.mapMinX = Utils.Clamp<int>(Lighting.firstToLightX + Lighting.offScreenTiles, 0, Main.maxTilesX - 1);
              Main.mapMaxX = Utils.Clamp<int>(Lighting.lastToLightX - Lighting.offScreenTiles, 0, Main.maxTilesX - 1);
              Main.mapMinY = Utils.Clamp<int>(Lighting.firstToLightY + Lighting.offScreenTiles, 0, Main.maxTilesY - 1);
              Main.mapMaxY = Utils.Clamp<int>(Lighting.lastToLightY - Lighting.offScreenTiles, 0, Main.maxTilesY - 1);
              for (int mapMinX = Main.mapMinX; mapMinX < Main.mapMaxX; ++mapMinX)
              {
                Lighting.LightingState[] state = Lighting.states[mapMinX - Lighting.firstTileX + Lighting.offScreenTiles];
                for (int mapMinY = Main.mapMinY; mapMinY < Main.mapMaxY; ++mapMinY)
                {
                  Lighting.LightingState lightingState = state[mapMinY - Lighting.firstTileY + Lighting.offScreenTiles];
                  Tile tile = Main.tile[mapMinX, mapMinY];
                  float num14 = 0.0f;
                  if ((double) lightingState.r > (double) num14)
                    num14 = lightingState.r;
                  if ((double) lightingState.g > (double) num14)
                    num14 = lightingState.g;
                  if ((double) lightingState.b > (double) num14)
                    num14 = lightingState.b;
                  if (Lighting.lightMode < 2)
                    num14 *= 1.5f;
                  byte light = (byte) Math.Min((float) byte.MaxValue, num14 * (float) byte.MaxValue);
                  if ((double) mapMinY < Main.worldSurface && !tile.active() && ((int) tile.wall == 0 && (int) tile.liquid == 0))
                    light = (byte) 22;
                  if ((int) light > 18 || (int) Main.Map[mapMinX, mapMinY].Light > 0)
                  {
                    if ((int) light < 22)
                      light = (byte) 22;
                    Main.Map.UpdateLighting(mapMinX, mapMinY, light);
                  }
                }
              }
            }
            catch
            {
            }
          }
        }
        if ((double) Lighting.oldSkyColor != (double) Lighting.skyColor)
        {
          int num14 = Utils.Clamp<int>(Lighting.firstToLightX, 0, Main.maxTilesX - 1);
          int num15 = Utils.Clamp<int>(Lighting.lastToLightX, 0, Main.maxTilesX - 1);
          int num16 = Utils.Clamp<int>(Lighting.firstToLightY, 0, Main.maxTilesY - 1);
          int num17 = Utils.Clamp<int>(Lighting.lastToLightY, 0, (int) Main.worldSurface - 1);
          if ((double) num16 < Main.worldSurface)
          {
            for (int index1 = num14; index1 < num15; ++index1)
            {
              Lighting.LightingState[] state = Lighting.states[index1 - Lighting.firstToLightX];
              for (int index2 = num16; index2 < num17; ++index2)
              {
                Lighting.LightingState lightingState = state[index2 - Lighting.firstToLightY];
                Tile tile = Main.tile[index1, index2];
                if (tile == null)
                {
                  tile = new Tile();
                  Main.tile[index1, index2] = tile;
                }
                if ((!tile.active() || !Main.tileNoSunLight[(int) tile.type]) && ((double) lightingState.r < (double) Lighting.skyColor && (int) tile.liquid < 200) && (Main.wallLight[(int) tile.wall] || (int) tile.wall == 73))
                {
                  lightingState.r = num1;
                  if ((double) lightingState.g < (double) Lighting.skyColor)
                    lightingState.g = num2;
                  if ((double) lightingState.b < (double) Lighting.skyColor)
                    lightingState.b = num3;
                }
              }
            }
          }
        }
      }
      else
        Lighting.lightCounter = 0;
      if (Main.renderCount <= Lighting.maxRenderCount)
        return;
      Lighting.PreRenderPhase();
    }

    public static void PreRenderPhase()
    {
      // ISSUE: explicit reference operation
      float num1 = (float) ((Color) @Main.tileColor).get_R() / (float) byte.MaxValue;
      // ISSUE: explicit reference operation
      float num2 = (float) ((Color) @Main.tileColor).get_G() / (float) byte.MaxValue;
      // ISSUE: explicit reference operation
      float num3 = (float) ((Color) @Main.tileColor).get_B() / (float) byte.MaxValue;
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      int num4 = 0;
      int num5 = Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10;
      int num6 = 0;
      int num7 = Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10;
      Lighting.minX = num5;
      Lighting.maxX = num4;
      Lighting.minY = num7;
      Lighting.maxY = num6;
      Lighting.RGB = Lighting.lightMode == 0 || Lighting.lightMode == 3;
      for (int index1 = num4; index1 < num5; ++index1)
      {
        Lighting.LightingState[] state = Lighting.states[index1];
        for (int index2 = num6; index2 < num7; ++index2)
        {
          Lighting.LightingState lightingState = state[index2];
          lightingState.r2 = 0.0f;
          lightingState.g2 = 0.0f;
          lightingState.b2 = 0.0f;
          lightingState.stopLight = false;
          lightingState.wetLight = false;
          lightingState.honeyLight = false;
        }
      }
      if (Main.wof >= 0)
      {
        if (Main.player[Main.myPlayer].gross)
        {
          try
          {
            int num8 = (int) Main.screenPosition.Y / 16 - 10;
            int num9 = (int) (Main.screenPosition.Y + (double) Main.screenHeight) / 16 + 10;
            int num10 = (int) Main.npc[Main.wof].position.X / 16;
            int num11 = Main.npc[Main.wof].direction <= 0 ? num10 + 2 : num10 - 3;
            int num12 = num11 + 8;
            float num13 = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
            float num14 = 0.3f;
            float num15 = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
            float num16 = num13 * 0.2f;
            float num17 = num14 * 0.1f;
            float num18 = num15 * 0.3f;
            for (int index1 = num11; index1 <= num12; ++index1)
            {
              Lighting.LightingState[] state = Lighting.states[index1 - num11];
              for (int index2 = num8; index2 <= num9; ++index2)
              {
                Lighting.LightingState lightingState = state[index2 - Lighting.firstToLightY];
                if ((double) lightingState.r2 < (double) num16)
                  lightingState.r2 = num16;
                if ((double) lightingState.g2 < (double) num17)
                  lightingState.g2 = num17;
                if ((double) lightingState.b2 < (double) num18)
                  lightingState.b2 = num18;
              }
            }
          }
          catch
          {
          }
        }
      }
      Main.sandTiles = 0;
      Main.evilTiles = 0;
      Main.bloodTiles = 0;
      Main.shroomTiles = 0;
      Main.snowTiles = 0;
      Main.holyTiles = 0;
      Main.meteorTiles = 0;
      Main.jungleTiles = 0;
      Main.dungeonTiles = 0;
      Main.campfire = false;
      Main.sunflower = false;
      Main.starInBottle = false;
      Main.heartLantern = false;
      Main.campfire = false;
      Main.clock = false;
      Main.musicBox = -1;
      Main.waterCandles = 0;
      for (int index = 0; index < Main.player[Main.myPlayer].NPCBannerBuff.Length; ++index)
        Main.player[Main.myPlayer].NPCBannerBuff[index] = false;
      Main.player[Main.myPlayer].hasBanner = false;
      int[] screenTileCounts = Main.screenTileCounts;
      Array.Clear((Array) screenTileCounts, 0, screenTileCounts.Length);
      int num19 = Utils.Clamp<int>(Lighting.firstToLightX, 5, Main.maxTilesX - 1);
      int num20 = Utils.Clamp<int>(Lighting.lastToLightX, 5, Main.maxTilesX - 1);
      int num21 = Utils.Clamp<int>(Lighting.firstToLightY, 5, Main.maxTilesY - 1);
      int num22 = Utils.Clamp<int>(Lighting.lastToLightY, 5, Main.maxTilesY - 1);
      int num23 = (num20 - num19 - Main.zoneX) / 2;
      int num24 = (num22 - num21 - Main.zoneY) / 2;
      Main.fountainColor = -1;
      Main.monolithType = -1;
      for (int index1 = num19; index1 < num20; ++index1)
      {
        Lighting.LightingState[] state = Lighting.states[index1 - Lighting.firstToLightX];
        for (int index2 = num21; index2 < num22; ++index2)
        {
          Lighting.LightingState lightingState = state[index2 - Lighting.firstToLightY];
          Tile tile = Main.tile[index1, index2];
          if (tile == null)
          {
            tile = new Tile();
            Main.tile[index1, index2] = tile;
          }
          float num8 = 0.0f;
          float num9 = 0.0f;
          float num10 = 0.0f;
          if ((double) index2 < Main.worldSurface)
          {
            if ((!tile.active() || !Main.tileNoSunLight[(int) tile.type] || ((int) tile.slope() != 0 || tile.halfBrick()) && ((int) Main.tile[index1, index2 - 1].liquid == 0 && (int) Main.tile[index1, index2 + 1].liquid == 0) && ((int) Main.tile[index1 - 1, index2].liquid == 0 && (int) Main.tile[index1 + 1, index2].liquid == 0)) && ((double) lightingState.r2 < (double) Lighting.skyColor && (Main.wallLight[(int) tile.wall] || (int) tile.wall == 73 || (int) tile.wall == 227) && ((int) tile.liquid < 200 && (!tile.halfBrick() || (int) Main.tile[index1, index2 - 1].liquid < 200))))
            {
              num8 = num1;
              num9 = num2;
              num10 = num3;
            }
            if ((!tile.active() || tile.halfBrick() || !Main.tileNoSunLight[(int) tile.type]) && ((int) tile.wall >= 88 && (int) tile.wall <= 93 && (int) tile.liquid < (int) byte.MaxValue))
            {
              num8 = num1;
              num9 = num2;
              num10 = num3;
              switch (tile.wall)
              {
                case 88:
                  num8 *= 0.9f;
                  num9 *= 0.15f;
                  num10 *= 0.9f;
                  break;
                case 89:
                  num8 *= 0.9f;
                  num9 *= 0.9f;
                  num10 *= 0.15f;
                  break;
                case 90:
                  num8 *= 0.15f;
                  num9 *= 0.15f;
                  num10 *= 0.9f;
                  break;
                case 91:
                  num8 *= 0.15f;
                  num9 *= 0.9f;
                  num10 *= 0.15f;
                  break;
                case 92:
                  num8 *= 0.9f;
                  num9 *= 0.15f;
                  num10 *= 0.15f;
                  break;
                case 93:
                  float num11 = 0.2f;
                  float num12 = 0.7f - num11;
                  num8 *= num12 + (float) Main.DiscoR / (float) byte.MaxValue * num11;
                  num9 *= num12 + (float) Main.DiscoG / (float) byte.MaxValue * num11;
                  num10 *= num12 + (float) Main.DiscoB / (float) byte.MaxValue * num11;
                  break;
              }
            }
            if (!Lighting.RGB)
            {
              double num11;
              num10 = (float) (num11 = ((double) num8 + (double) num9 + (double) num10) / 3.0);
              num9 = (float) num11;
              num8 = (float) num11;
            }
            if ((double) lightingState.r2 < (double) num8)
              lightingState.r2 = num8;
            if ((double) lightingState.g2 < (double) num9)
              lightingState.g2 = num9;
            if ((double) lightingState.b2 < (double) num10)
              lightingState.b2 = num10;
          }
          float num13 = (float) (0.550000011920929 + Math.Sin((double) Main.GlobalTime * 2.0) * 0.0799999982118607);
          if (index2 > Main.maxTilesY - 200)
          {
            if ((!tile.active() || !Main.tileNoSunLight[(int) tile.type] || ((int) tile.slope() != 0 || tile.halfBrick()) && ((int) Main.tile[index1, index2 - 1].liquid == 0 && (int) Main.tile[index1, index2 + 1].liquid == 0) && ((int) Main.tile[index1 - 1, index2].liquid == 0 && (int) Main.tile[index1 + 1, index2].liquid == 0)) && ((double) lightingState.r2 < (double) num13 && (Main.wallLight[(int) tile.wall] || (int) tile.wall == 73 || (int) tile.wall == 227) && ((int) tile.liquid < 200 && (!tile.halfBrick() || (int) Main.tile[index1, index2 - 1].liquid < 200))))
            {
              num8 = num13;
              num9 = num13 * 0.6f;
              num10 = num13 * 0.2f;
            }
            if ((!tile.active() || tile.halfBrick() || !Main.tileNoSunLight[(int) tile.type]) && ((int) tile.wall >= 88 && (int) tile.wall <= 93 && (int) tile.liquid < (int) byte.MaxValue))
            {
              num8 = num13;
              num9 = num13 * 0.6f;
              num10 = num13 * 0.2f;
              switch (tile.wall)
              {
                case 88:
                  num8 *= 0.9f;
                  num9 *= 0.15f;
                  num10 *= 0.9f;
                  break;
                case 89:
                  num8 *= 0.9f;
                  num9 *= 0.9f;
                  num10 *= 0.15f;
                  break;
                case 90:
                  num8 *= 0.15f;
                  num9 *= 0.15f;
                  num10 *= 0.9f;
                  break;
                case 91:
                  num8 *= 0.15f;
                  num9 *= 0.9f;
                  num10 *= 0.15f;
                  break;
                case 92:
                  num8 *= 0.9f;
                  num9 *= 0.15f;
                  num10 *= 0.15f;
                  break;
                case 93:
                  float num11 = 0.2f;
                  float num12 = 0.7f - num11;
                  num8 *= num12 + (float) Main.DiscoR / (float) byte.MaxValue * num11;
                  num9 *= num12 + (float) Main.DiscoG / (float) byte.MaxValue * num11;
                  num10 *= num12 + (float) Main.DiscoB / (float) byte.MaxValue * num11;
                  break;
              }
            }
            if (!Lighting.RGB)
            {
              double num11;
              num10 = (float) (num11 = ((double) num8 + (double) num9 + (double) num10) / 3.0);
              num9 = (float) num11;
              num8 = (float) num11;
            }
            if ((double) lightingState.r2 < (double) num8)
              lightingState.r2 = num8;
            if ((double) lightingState.g2 < (double) num9)
              lightingState.g2 = num9;
            if ((double) lightingState.b2 < (double) num10)
              lightingState.b2 = num10;
          }
          byte wall = tile.wall;
          if ((uint) wall <= 137U)
          {
            if ((int) wall != 33)
            {
              if ((int) wall != 44)
              {
                if ((int) wall == 137 && (!tile.active() || !Main.tileBlockLight[(int) tile.type]))
                {
                  float num11 = 0.4f + (float) (270 - (int) Main.mouseTextColor) / 1500f + (float) Main.rand.Next(0, 50) * 0.0005f;
                  num8 = 1f * num11;
                  num9 = 0.5f * num11;
                  num10 = 0.1f * num11;
                }
              }
              else if (!tile.active() || !Main.tileBlockLight[(int) tile.type])
              {
                num8 = (float) ((double) Main.DiscoR / (double) byte.MaxValue * 0.150000005960464);
                num9 = (float) ((double) Main.DiscoG / (double) byte.MaxValue * 0.150000005960464);
                num10 = (float) ((double) Main.DiscoB / (double) byte.MaxValue * 0.150000005960464);
              }
            }
            else if (!tile.active() || !Main.tileBlockLight[(int) tile.type])
            {
              num8 = 0.09f;
              num9 = 0.0525f;
              num10 = 0.24f;
            }
          }
          else if ((uint) wall <= 166U)
          {
            switch (wall)
            {
              case 153:
                num8 = 0.6f;
                num9 = 0.3f;
                break;
              case 154:
                num8 = 0.6f;
                num10 = 0.6f;
                break;
              case 155:
                num8 = 0.6f;
                num9 = 0.6f;
                num10 = 0.6f;
                break;
              case 156:
                num9 = 0.6f;
                break;
              case 164:
                num8 = 0.6f;
                break;
              case 165:
                num10 = 0.6f;
                break;
              case 166:
                num8 = 0.6f;
                num9 = 0.6f;
                break;
            }
          }
          else
          {
            switch (wall)
            {
              case 174:
                if (!tile.active() || !Main.tileBlockLight[(int) tile.type])
                {
                  num8 = 0.2975f;
                  break;
                }
                break;
              case 175:
                if (!tile.active() || !Main.tileBlockLight[(int) tile.type])
                {
                  num8 = 0.075f;
                  num9 = 0.15f;
                  num10 = 0.4f;
                  break;
                }
                break;
              case 176:
                if (!tile.active() || !Main.tileBlockLight[(int) tile.type])
                {
                  num8 = 0.1f;
                  num9 = 0.1f;
                  num10 = 0.1f;
                  break;
                }
                break;
              case 182:
                if (!tile.active() || !Main.tileBlockLight[(int) tile.type])
                {
                  num8 = 0.24f;
                  num9 = 0.12f;
                  num10 = 0.09f;
                  break;
                }
                break;
            }
          }
          if (tile.active())
          {
            if (index1 > num19 + num23 && index1 < num20 - num23 && (index2 > num21 + num24 && index2 < num22 - num24))
            {
              ++screenTileCounts[(int) tile.type];
              if ((int) tile.type == 215 && (int) tile.frameY < 36)
                Main.campfire = true;
              if ((int) tile.type == 405)
                Main.campfire = true;
              if ((int) tile.type == 42 && (int) tile.frameY >= 324 && (int) tile.frameY <= 358)
                Main.heartLantern = true;
              if ((int) tile.type == 42 && (int) tile.frameY >= 252 && (int) tile.frameY <= 286)
                Main.starInBottle = true;
              if ((int) tile.type == 91 && ((int) tile.frameX >= 396 || (int) tile.frameY >= 54))
              {
                int banner = (int) tile.frameX / 18 - 21;
                int frameY = (int) tile.frameY;
                while (frameY >= 54)
                {
                  banner = banner + 90 + 21;
                  frameY -= 54;
                }
                int index3 = Item.BannerToItem(banner);
                if (ItemID.Sets.BannerStrength[index3].Enabled)
                {
                  Main.player[Main.myPlayer].NPCBannerBuff[banner] = true;
                  Main.player[Main.myPlayer].hasBanner = true;
                }
              }
            }
            switch (tile.type)
            {
              case 139:
                if ((int) tile.frameX >= 36)
                {
                  Main.musicBox = (int) tile.frameY / 36;
                  break;
                }
                break;
              case 207:
                if ((int) tile.frameY >= 72)
                {
                  switch ((int) tile.frameX / 36)
                  {
                    case 0:
                      Main.fountainColor = 0;
                      break;
                    case 1:
                      Main.fountainColor = 6;
                      break;
                    case 2:
                      Main.fountainColor = 3;
                      break;
                    case 3:
                      Main.fountainColor = 5;
                      break;
                    case 4:
                      Main.fountainColor = 2;
                      break;
                    case 5:
                      Main.fountainColor = 10;
                      break;
                    case 6:
                      Main.fountainColor = 4;
                      break;
                    case 7:
                      Main.fountainColor = 9;
                      break;
                    default:
                      Main.fountainColor = -1;
                      break;
                  }
                }
                else
                  break;
              case 410:
                if ((int) tile.frameY >= 56)
                {
                  Main.monolithType = (int) tile.frameX / 36;
                  break;
                }
                break;
            }
            if (Main.tileBlockLight[(int) tile.type] && (Lighting.lightMode >= 2 || (int) tile.type != 131 && !tile.inActive() && (int) tile.slope() == 0))
              lightingState.stopLight = true;
            if ((int) tile.type == 104)
              Main.clock = true;
            if (Main.tileLighted[(int) tile.type])
            {
              ushort type = tile.type;
              if ((uint) type <= 184U)
              {
                if ((uint) type <= 77U)
                {
                  if ((uint) type <= 37U)
                  {
                    if ((uint) type <= 17U)
                    {
                      if ((int) type != 4)
                      {
                        if ((int) type == 17)
                          goto label_348;
                        else
                          goto label_424;
                      }
                      else if ((int) tile.frameX < 66)
                      {
                        switch ((int) tile.frameY / 22)
                        {
                          case 0:
                            num8 = 1f;
                            num9 = 0.95f;
                            num10 = 0.8f;
                            goto label_424;
                          case 1:
                            num8 = 0.0f;
                            num9 = 0.1f;
                            num10 = 1.3f;
                            goto label_424;
                          case 2:
                            num8 = 1f;
                            num9 = 0.1f;
                            num10 = 0.1f;
                            goto label_424;
                          case 3:
                            num8 = 0.0f;
                            num9 = 1f;
                            num10 = 0.1f;
                            goto label_424;
                          case 4:
                            num8 = 0.9f;
                            num9 = 0.0f;
                            num10 = 0.9f;
                            goto label_424;
                          case 5:
                            num8 = 1.3f;
                            num9 = 1.3f;
                            num10 = 1.3f;
                            goto label_424;
                          case 6:
                            num8 = 0.9f;
                            num9 = 0.9f;
                            num10 = 0.0f;
                            goto label_424;
                          case 7:
                            num8 = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                            num9 = 0.3f;
                            num10 = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                            goto label_424;
                          case 8:
                            num8 = 0.85f;
                            num9 = 1f;
                            num10 = 0.7f;
                            goto label_424;
                          case 9:
                            num8 = 0.7f;
                            num9 = 0.85f;
                            num10 = 1f;
                            goto label_424;
                          case 10:
                            num8 = 1f;
                            num9 = 0.5f;
                            num10 = 0.0f;
                            goto label_424;
                          case 11:
                            num8 = 1.25f;
                            num9 = 1.25f;
                            num10 = 0.8f;
                            goto label_424;
                          case 12:
                            num8 = 0.75f;
                            num9 = 1.2825f;
                            num10 = 1.2f;
                            goto label_424;
                          case 13:
                            num8 = 0.95f;
                            num9 = 0.65f;
                            num10 = 1.3f;
                            goto label_424;
                          case 14:
                            num8 = (float) Main.DiscoR / (float) byte.MaxValue;
                            num9 = (float) Main.DiscoG / (float) byte.MaxValue;
                            num10 = (float) Main.DiscoB / (float) byte.MaxValue;
                            goto label_424;
                          case 15:
                            num8 = 1f;
                            num9 = 0.0f;
                            num10 = 1f;
                            goto label_424;
                          default:
                            num8 = 1f;
                            num9 = 0.95f;
                            num10 = 0.8f;
                            goto label_424;
                        }
                      }
                      else
                        goto label_424;
                    }
                    else
                    {
                      switch (type)
                      {
                        case 22:
                          break;
                        case 26:
                        case 31:
                          if ((int) tile.type == 31 && (int) tile.frameX >= 36 || (int) tile.type == 26 && (int) tile.frameX >= 54)
                          {
                            float num11 = (float) Main.rand.Next(-5, 6) * (1f / 400f);
                            num8 = (float) (0.5 + (double) num11 * 2.0);
                            num9 = 0.2f + num11;
                            num10 = 0.1f;
                            goto label_424;
                          }
                          else
                          {
                            float num11 = (float) Main.rand.Next(-5, 6) * (1f / 400f);
                            num8 = 0.31f + num11;
                            num9 = 0.1f;
                            num10 = (float) (0.439999997615814 + (double) num11 * 2.0);
                            goto label_424;
                          }
                        case 27:
                          if ((int) tile.frameY < 36)
                          {
                            num8 = 0.3f;
                            num9 = 0.27f;
                            goto label_424;
                          }
                          else
                            goto label_424;
                        case 33:
                          if ((int) tile.frameX == 0)
                          {
                            switch ((int) tile.frameY / 22)
                            {
                              case 19:
                                num8 = 0.37f;
                                num9 = 0.8f;
                                num10 = 1f;
                                goto label_424;
                              case 20:
                                num8 = 0.0f;
                                num9 = 0.9f;
                                num10 = 1f;
                                goto label_424;
                              case 21:
                                num8 = 0.25f;
                                num9 = 0.7f;
                                num10 = 1f;
                                goto label_424;
                              case 25:
                                num8 = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                                num9 = 0.3f;
                                num10 = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                                goto label_424;
                              case 28:
                                num8 = 0.9f;
                                num9 = 0.75f;
                                num10 = 1f;
                                goto label_424;
                              case 30:
                                Color rgb1 = Main.hslToRgb((float) ((double) Main.demonTorch * 0.119999997317791 + 0.689999997615814), 1f, 0.75f);
                                // ISSUE: explicit reference operation
                                Vector3 vector3_1 = Vector3.op_Multiply(((Color) @rgb1).ToVector3(), 1.2f);
                                num8 = (float) vector3_1.X;
                                num9 = (float) vector3_1.Y;
                                num10 = (float) vector3_1.Z;
                                goto label_424;
                              case 0:
                                num8 = 1f;
                                num9 = 0.95f;
                                num10 = 0.65f;
                                goto label_424;
                              case 1:
                                num8 = 0.55f;
                                num9 = 0.85f;
                                num10 = 0.35f;
                                goto label_424;
                              case 2:
                                num8 = 0.65f;
                                num9 = 0.95f;
                                num10 = 0.5f;
                                goto label_424;
                              case 3:
                                num8 = 0.2f;
                                num9 = 0.75f;
                                num10 = 1f;
                                goto label_424;
                              case 14:
                                num8 = 1f;
                                num9 = 1f;
                                num10 = 0.6f;
                                goto label_424;
                              default:
                                num8 = 1f;
                                num9 = 0.95f;
                                num10 = 0.65f;
                                goto label_424;
                            }
                          }
                          else
                            goto label_424;
                        case 34:
                          if ((int) tile.frameX % 108 < 54)
                          {
                            switch ((int) tile.frameY / 54 + 37 * ((int) tile.frameX / 108))
                            {
                              case 7:
                                num8 = 0.95f;
                                num9 = 0.95f;
                                num10 = 0.5f;
                                goto label_424;
                              case 8:
                                num8 = 0.85f;
                                num9 = 0.6f;
                                num10 = 1f;
                                goto label_424;
                              case 9:
                                num8 = 1f;
                                num9 = 0.6f;
                                num10 = 0.6f;
                                goto label_424;
                              case 11:
                              case 17:
                                num8 = 0.75f;
                                num9 = 0.9f;
                                num10 = 1f;
                                goto label_424;
                              case 15:
                                num8 = 1f;
                                num9 = 1f;
                                num10 = 0.7f;
                                goto label_424;
                              case 18:
                                num8 = 1f;
                                num9 = 1f;
                                num10 = 0.6f;
                                goto label_424;
                              case 24:
                                num8 = 0.37f;
                                num9 = 0.8f;
                                num10 = 1f;
                                goto label_424;
                              case 25:
                                num8 = 0.0f;
                                num9 = 0.9f;
                                num10 = 1f;
                                goto label_424;
                              case 26:
                                num8 = 0.25f;
                                num9 = 0.7f;
                                num10 = 1f;
                                goto label_424;
                              case 27:
                                num8 = 0.55f;
                                num9 = 0.85f;
                                num10 = 0.35f;
                                goto label_424;
                              case 28:
                                num8 = 0.65f;
                                num9 = 0.95f;
                                num10 = 0.5f;
                                goto label_424;
                              case 29:
                                num8 = 0.2f;
                                num9 = 0.75f;
                                num10 = 1f;
                                goto label_424;
                              case 32:
                                num8 = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                                num9 = 0.3f;
                                num10 = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                                goto label_424;
                              case 35:
                                num8 = 0.9f;
                                num9 = 0.75f;
                                num10 = 1f;
                                goto label_424;
                              case 37:
                                Color rgb2 = Main.hslToRgb((float) ((double) Main.demonTorch * 0.119999997317791 + 0.689999997615814), 1f, 0.75f);
                                // ISSUE: explicit reference operation
                                Vector3 vector3_2 = Vector3.op_Multiply(((Color) @rgb2).ToVector3(), 1.2f);
                                num8 = (float) vector3_2.X;
                                num9 = (float) vector3_2.Y;
                                num10 = (float) vector3_2.Z;
                                goto label_424;
                              default:
                                num8 = 1f;
                                num9 = 0.95f;
                                num10 = 0.8f;
                                goto label_424;
                            }
                          }
                          else
                            goto label_424;
                        case 35:
                          if ((int) tile.frameX < 36)
                          {
                            num8 = 0.75f;
                            num9 = 0.6f;
                            num10 = 0.3f;
                            goto label_424;
                          }
                          else
                            goto label_424;
                        case 37:
                          num8 = 0.56f;
                          num9 = 0.43f;
                          num10 = 0.15f;
                          goto label_424;
                        default:
                          goto label_424;
                      }
                    }
                  }
                  else if ((uint) type <= 49U)
                  {
                    if ((int) type != 42)
                    {
                      if ((int) type == 49)
                      {
                        num8 = 0.0f;
                        num9 = 0.35f;
                        num10 = 0.8f;
                        goto label_424;
                      }
                      else
                        goto label_424;
                    }
                    else if ((int) tile.frameX == 0)
                    {
                      switch ((int) tile.frameY / 36)
                      {
                        case 0:
                          num8 = 0.7f;
                          num9 = 0.65f;
                          num10 = 0.55f;
                          goto label_424;
                        case 1:
                          num8 = 0.9f;
                          num9 = 0.75f;
                          num10 = 0.6f;
                          goto label_424;
                        case 2:
                          num8 = 0.8f;
                          num9 = 0.6f;
                          num10 = 0.6f;
                          goto label_424;
                        case 3:
                          num8 = 0.65f;
                          num9 = 0.5f;
                          num10 = 0.2f;
                          goto label_424;
                        case 4:
                          num8 = 0.5f;
                          num9 = 0.7f;
                          num10 = 0.4f;
                          goto label_424;
                        case 5:
                          num8 = 0.9f;
                          num9 = 0.4f;
                          num10 = 0.2f;
                          goto label_424;
                        case 6:
                          num8 = 0.7f;
                          num9 = 0.75f;
                          num10 = 0.3f;
                          goto label_424;
                        case 7:
                          float num12 = Main.demonTorch * 0.2f;
                          num8 = 0.9f - num12;
                          num9 = 0.9f - num12;
                          num10 = 0.7f + num12;
                          goto label_424;
                        case 8:
                          num8 = 0.75f;
                          num9 = 0.6f;
                          num10 = 0.3f;
                          goto label_424;
                        case 9:
                          float num14 = 1f;
                          float num15 = 0.3f;
                          num10 = 0.5f + Main.demonTorch * 0.2f;
                          num8 = num14 - Main.demonTorch * 0.1f;
                          num9 = num15 - Main.demonTorch * 0.2f;
                          goto label_424;
                        case 28:
                          num8 = 0.37f;
                          num9 = 0.8f;
                          num10 = 1f;
                          goto label_424;
                        case 29:
                          num8 = 0.0f;
                          num9 = 0.9f;
                          num10 = 1f;
                          goto label_424;
                        case 30:
                          num8 = 0.25f;
                          num9 = 0.7f;
                          num10 = 1f;
                          goto label_424;
                        case 32:
                          num8 = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                          num9 = 0.3f;
                          num10 = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                          goto label_424;
                        case 35:
                          num8 = 0.7f;
                          num9 = 0.6f;
                          num10 = 0.9f;
                          goto label_424;
                        case 37:
                          Color rgb3 = Main.hslToRgb((float) ((double) Main.demonTorch * 0.119999997317791 + 0.689999997615814), 1f, 0.75f);
                          // ISSUE: explicit reference operation
                          Vector3 vector3_3 = Vector3.op_Multiply(((Color) @rgb3).ToVector3(), 1.2f);
                          num8 = (float) vector3_3.X;
                          num9 = (float) vector3_3.Y;
                          num10 = (float) vector3_3.Z;
                          goto label_424;
                        default:
                          num8 = 1f;
                          num9 = 1f;
                          num10 = 1f;
                          goto label_424;
                      }
                    }
                    else
                      goto label_424;
                  }
                  else
                  {
                    switch (type)
                    {
                      case 61:
                        if ((int) tile.frameX == 144)
                        {
                          float num11 = (float) (1.0 + (double) (270 - (int) Main.mouseTextColor) / 400.0);
                          float num16 = (float) (0.800000011920929 - (double) (270 - (int) Main.mouseTextColor) / 400.0);
                          num8 = 0.42f * num16;
                          num9 = 0.81f * num11;
                          num10 = 0.52f * num16;
                          goto label_424;
                        }
                        else
                          goto label_424;
                      case 70:
                      case 71:
                      case 72:
                        goto label_389;
                      case 77:
                        num8 = 0.75f;
                        num9 = 0.45f;
                        num10 = 0.25f;
                        goto label_424;
                      default:
                        goto label_424;
                    }
                  }
                }
                else
                {
                  if ((uint) type <= 133U)
                  {
                    if ((uint) type <= 100U)
                    {
                      switch (type)
                      {
                        case 83:
                          if ((int) tile.frameX == 18 && !Main.dayTime)
                          {
                            num8 = 0.1f;
                            num9 = 0.4f;
                            num10 = 0.6f;
                          }
                          if ((int) tile.frameX == 90 && !Main.raining && Main.time > 40500.0)
                          {
                            num8 = 0.9f;
                            num9 = 0.72f;
                            num10 = 0.18f;
                            goto label_424;
                          }
                          else
                            goto label_424;
                        case 84:
                          int num11 = (int) tile.frameX / 18;
                          if (num11 == 2)
                          {
                            float num16 = (float) (270 - (int) Main.mouseTextColor) / 800f;
                            if ((double) num16 > 1.0)
                              num16 = 1f;
                            else if ((double) num16 < 0.0)
                              num16 = 0.0f;
                            num8 = num16 * 0.7f;
                            num9 = num16;
                            num10 = num16 * 0.1f;
                            goto label_424;
                          }
                          else if (num11 == 5)
                          {
                            float num16 = 0.9f;
                            num8 = num16;
                            num9 = num16 * 0.8f;
                            num10 = num16 * 0.2f;
                            goto label_424;
                          }
                          else if (num11 == 6)
                          {
                            float num16 = 0.08f;
                            num9 = num16 * 0.8f;
                            num10 = num16;
                            goto label_424;
                          }
                          else
                            goto label_424;
                        case 92:
                          if ((int) tile.frameY <= 18 && (int) tile.frameX == 0)
                          {
                            num8 = 1f;
                            num9 = 1f;
                            num10 = 1f;
                            goto label_424;
                          }
                          else
                            goto label_424;
                        case 93:
                          if ((int) tile.frameX == 0)
                          {
                            switch ((int) tile.frameY / 54)
                            {
                              case 1:
                                num8 = 0.95f;
                                num9 = 0.95f;
                                num10 = 0.5f;
                                goto label_424;
                              case 2:
                                num8 = 0.85f;
                                num9 = 0.6f;
                                num10 = 1f;
                                goto label_424;
                              case 3:
                                num8 = 0.75f;
                                num9 = 1f;
                                num10 = 0.6f;
                                goto label_424;
                              case 4:
                              case 5:
                                num8 = 0.75f;
                                num9 = 0.9f;
                                num10 = 1f;
                                goto label_424;
                              case 9:
                                num8 = 1f;
                                num9 = 1f;
                                num10 = 0.7f;
                                goto label_424;
                              case 13:
                                num8 = 1f;
                                num9 = 1f;
                                num10 = 0.6f;
                                goto label_424;
                              case 19:
                                num8 = 0.37f;
                                num9 = 0.8f;
                                num10 = 1f;
                                goto label_424;
                              case 20:
                                num8 = 0.0f;
                                num9 = 0.9f;
                                num10 = 1f;
                                goto label_424;
                              case 21:
                                num8 = 0.25f;
                                num9 = 0.7f;
                                num10 = 1f;
                                goto label_424;
                              case 23:
                                num8 = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                                num9 = 0.3f;
                                num10 = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                                goto label_424;
                              case 24:
                                num8 = 0.35f;
                                num9 = 0.5f;
                                num10 = 0.3f;
                                goto label_424;
                              case 25:
                                num8 = 0.34f;
                                num9 = 0.4f;
                                num10 = 0.31f;
                                goto label_424;
                              case 26:
                                num8 = 0.25f;
                                num9 = 0.32f;
                                num10 = 0.5f;
                                goto label_424;
                              case 29:
                                num8 = 0.9f;
                                num9 = 0.75f;
                                num10 = 1f;
                                goto label_424;
                              case 31:
                                Color rgb4 = Main.hslToRgb((float) ((double) Main.demonTorch * 0.119999997317791 + 0.689999997615814), 1f, 0.75f);
                                // ISSUE: explicit reference operation
                                Vector3 vector3_4 = Vector3.op_Multiply(((Color) @rgb4).ToVector3(), 1.2f);
                                num8 = (float) vector3_4.X;
                                num9 = (float) vector3_4.Y;
                                num10 = (float) vector3_4.Z;
                                goto label_424;
                              default:
                                num8 = 1f;
                                num9 = 0.97f;
                                num10 = 0.85f;
                                goto label_424;
                            }
                          }
                          else
                            goto label_424;
                        case 95:
                          if ((int) tile.frameX < 36)
                          {
                            num8 = 1f;
                            num9 = 0.95f;
                            num10 = 0.8f;
                            goto label_424;
                          }
                          else
                            goto label_424;
                        case 96:
                          if ((int) tile.frameX >= 36)
                          {
                            num8 = 0.5f;
                            num9 = 0.35f;
                            num10 = 0.1f;
                            goto label_424;
                          }
                          else
                            goto label_424;
                        case 98:
                          if ((int) tile.frameY == 0)
                          {
                            num8 = 1f;
                            num9 = 0.97f;
                            num10 = 0.85f;
                            goto label_424;
                          }
                          else
                            goto label_424;
                        case 100:
                          break;
                        default:
                          goto label_424;
                      }
                    }
                    else
                    {
                      switch (type)
                      {
                        case 125:
                          float num17 = (float) Main.rand.Next(28, 42) * 0.01f + (float) (270 - (int) Main.mouseTextColor) / 800f;
                          num9 = lightingState.g2 = 0.3f * num17;
                          num10 = lightingState.b2 = 0.6f * num17;
                          goto label_424;
                        case 126:
                          if ((int) tile.frameX < 36)
                          {
                            num8 = (float) Main.DiscoR / (float) byte.MaxValue;
                            num9 = (float) Main.DiscoG / (float) byte.MaxValue;
                            num10 = (float) Main.DiscoB / (float) byte.MaxValue;
                            goto label_424;
                          }
                          else
                            goto label_424;
                        case 129:
                          switch ((int) tile.frameX / 18 % 3)
                          {
                            case 0:
                              num8 = 0.0f;
                              num9 = 0.05f;
                              num10 = 0.25f;
                              goto label_424;
                            case 1:
                              num8 = 0.2f;
                              num9 = 0.0f;
                              num10 = 0.15f;
                              goto label_424;
                            case 2:
                              num8 = 0.1f;
                              num9 = 0.0f;
                              num10 = 0.2f;
                              goto label_424;
                            default:
                              goto label_424;
                          }
                        case 133:
                          goto label_348;
                        default:
                          goto label_424;
                      }
                    }
                  }
                  else if ((uint) type <= 149U)
                  {
                    if ((int) type != 140)
                    {
                      if ((int) type == 149 && (int) tile.frameX <= 36)
                      {
                        switch ((int) tile.frameX / 18)
                        {
                          case 0:
                            num8 = 0.1f;
                            num9 = 0.2f;
                            num10 = 0.5f;
                            break;
                          case 1:
                            num8 = 0.5f;
                            num9 = 0.1f;
                            num10 = 0.1f;
                            break;
                          case 2:
                            num8 = 0.2f;
                            num9 = 0.5f;
                            num10 = 0.1f;
                            break;
                        }
                        num8 *= (float) Main.rand.Next(970, 1031) * (1f / 1000f);
                        num9 *= (float) Main.rand.Next(970, 1031) * (1f / 1000f);
                        num10 *= (float) Main.rand.Next(970, 1031) * (1f / 1000f);
                        goto label_424;
                      }
                      else
                        goto label_424;
                    }
                    else
                      goto label_351;
                  }
                  else
                  {
                    switch (type)
                    {
                      case 160:
                        num8 = (float) ((double) Main.DiscoR / (double) byte.MaxValue * 0.25);
                        num9 = (float) ((double) Main.DiscoG / (double) byte.MaxValue * 0.25);
                        num10 = (float) ((double) Main.DiscoB / (double) byte.MaxValue * 0.25);
                        goto label_424;
                      case 171:
                        int index3 = index1;
                        int index4 = index2;
                        if ((int) tile.frameX < 10)
                        {
                          index3 -= (int) tile.frameX;
                          index4 -= (int) tile.frameY;
                        }
                        switch (((int) Main.tile[index3, index4].frameY & 15360) >> 10)
                        {
                          case 1:
                            num8 = 0.1f;
                            num9 = 0.1f;
                            num10 = 0.1f;
                            break;
                          case 2:
                            num8 = 0.2f;
                            break;
                          case 3:
                            num9 = 0.2f;
                            break;
                          case 4:
                            num10 = 0.2f;
                            break;
                          case 5:
                            num8 = 0.125f;
                            num9 = 0.125f;
                            break;
                          case 6:
                            num8 = 0.2f;
                            num9 = 0.1f;
                            break;
                          case 7:
                            num8 = 0.125f;
                            num9 = 0.125f;
                            break;
                          case 8:
                            num8 = 0.08f;
                            num9 = 0.175f;
                            break;
                          case 9:
                            num9 = 0.125f;
                            num10 = 0.125f;
                            break;
                          case 10:
                            num8 = 0.125f;
                            num10 = 0.125f;
                            break;
                          case 11:
                            num8 = 0.1f;
                            num9 = 0.1f;
                            num10 = 0.2f;
                            break;
                          default:
                            double num18;
                            num10 = (float) (num18 = 0.0);
                            num9 = (float) num18;
                            num8 = (float) num18;
                            break;
                        }
                        num8 *= 0.5f;
                        num9 *= 0.5f;
                        num10 *= 0.5f;
                        goto label_424;
                      case 173:
                        break;
                      case 174:
                        if ((int) tile.frameX == 0)
                        {
                          num8 = 1f;
                          num9 = 0.95f;
                          num10 = 0.65f;
                          goto label_424;
                        }
                        else
                          goto label_424;
                      case 184:
                        if ((int) tile.frameX == 110)
                        {
                          num8 = 0.25f;
                          num9 = 0.1f;
                          num10 = 0.0f;
                          goto label_424;
                        }
                        else
                          goto label_424;
                      default:
                        goto label_424;
                    }
                  }
                  if ((int) tile.frameX < 36)
                  {
                    switch ((int) tile.frameY / 36)
                    {
                      case 1:
                        num8 = 0.95f;
                        num9 = 0.95f;
                        num10 = 0.5f;
                        goto label_424;
                      case 3:
                        num8 = 1f;
                        num9 = 0.6f;
                        num10 = 0.6f;
                        goto label_424;
                      case 6:
                      case 9:
                        num8 = 0.75f;
                        num9 = 0.9f;
                        num10 = 1f;
                        goto label_424;
                      case 11:
                        num8 = 1f;
                        num9 = 1f;
                        num10 = 0.7f;
                        goto label_424;
                      case 13:
                        num8 = 1f;
                        num9 = 1f;
                        num10 = 0.6f;
                        goto label_424;
                      case 19:
                        num8 = 0.37f;
                        num9 = 0.8f;
                        num10 = 1f;
                        goto label_424;
                      case 20:
                        num8 = 0.0f;
                        num9 = 0.9f;
                        num10 = 1f;
                        goto label_424;
                      case 21:
                        num8 = 0.25f;
                        num9 = 0.7f;
                        num10 = 1f;
                        goto label_424;
                      case 22:
                        num8 = 0.35f;
                        num9 = 0.5f;
                        num10 = 0.3f;
                        goto label_424;
                      case 23:
                        num8 = 0.34f;
                        num9 = 0.4f;
                        num10 = 0.31f;
                        goto label_424;
                      case 24:
                        num8 = 0.25f;
                        num9 = 0.32f;
                        num10 = 0.5f;
                        goto label_424;
                      case 25:
                        num8 = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                        num9 = 0.3f;
                        num10 = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                        goto label_424;
                      case 29:
                        num8 = 0.9f;
                        num9 = 0.75f;
                        num10 = 1f;
                        goto label_424;
                      case 31:
                        Color rgb5 = Main.hslToRgb((float) ((double) Main.demonTorch * 0.119999997317791 + 0.689999997615814), 1f, 0.75f);
                        // ISSUE: explicit reference operation
                        Vector3 vector3_5 = Vector3.op_Multiply(((Color) @rgb5).ToVector3(), 1.2f);
                        num8 = (float) vector3_5.X;
                        num9 = (float) vector3_5.Y;
                        num10 = (float) vector3_5.Z;
                        goto label_424;
                      default:
                        num8 = 1f;
                        num9 = 0.95f;
                        num10 = 0.65f;
                        goto label_424;
                    }
                  }
                  else
                    goto label_424;
                }
label_351:
                num8 = 0.12f;
                num9 = 0.07f;
                num10 = 0.32f;
                goto label_424;
              }
              else
              {
                if ((uint) type <= 318U)
                {
                  if ((uint) type <= 215U)
                  {
                    if ((uint) type <= 204U)
                    {
                      if ((int) type != 190)
                      {
                        if ((int) type != 204)
                          goto label_424;
                      }
                      else
                        goto label_389;
                    }
                    else if ((int) type != 209)
                    {
                      if ((int) type == 215 && (int) tile.frameY < 36)
                      {
                        float num11 = (float) Main.rand.Next(28, 42) * 0.005f + (float) (270 - (int) Main.mouseTextColor) / 700f;
                        float num12;
                        float num14;
                        float num15;
                        switch ((int) tile.frameX / 54)
                        {
                          case 1:
                            num12 = 0.7f;
                            num14 = 1f;
                            num15 = 0.5f;
                            break;
                          case 2:
                            num12 = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                            num14 = 0.3f;
                            num15 = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                            break;
                          case 3:
                            num12 = 0.45f;
                            num14 = 0.75f;
                            num15 = 1f;
                            break;
                          case 4:
                            num12 = 1.15f;
                            num14 = 1.15f;
                            num15 = 0.5f;
                            break;
                          case 5:
                            num12 = (float) Main.DiscoR / (float) byte.MaxValue;
                            num14 = (float) Main.DiscoG / (float) byte.MaxValue;
                            num15 = (float) Main.DiscoB / (float) byte.MaxValue;
                            break;
                          case 6:
                            num12 = 0.75f;
                            num14 = 1.2825f;
                            num15 = 1.2f;
                            break;
                          case 7:
                            num12 = 0.95f;
                            num14 = 0.65f;
                            num15 = 1.3f;
                            break;
                          default:
                            num12 = 0.9f;
                            num14 = 0.3f;
                            num15 = 0.1f;
                            break;
                        }
                        num8 = num12 + num11;
                        num9 = num14 + num11;
                        num10 = num15 + num11;
                        goto label_424;
                      }
                      else
                        goto label_424;
                    }
                    else if ((int) tile.frameX == 234 || (int) tile.frameX == 252)
                    {
                      Color portalColor = PortalHelper.GetPortalColor(Main.myPlayer, 0);
                      // ISSUE: explicit reference operation
                      Vector3 vector3 = Vector3.op_Multiply(((Color) @portalColor).ToVector3(), 0.65f);
                      num8 = (float) vector3.X;
                      num9 = (float) vector3.Y;
                      num10 = (float) vector3.Z;
                      goto label_424;
                    }
                    else if ((int) tile.frameX == 306 || (int) tile.frameX == 324)
                    {
                      Color portalColor = PortalHelper.GetPortalColor(Main.myPlayer, 1);
                      // ISSUE: explicit reference operation
                      Vector3 vector3 = Vector3.op_Multiply(((Color) @portalColor).ToVector3(), 0.65f);
                      num8 = (float) vector3.X;
                      num9 = (float) vector3.Y;
                      num10 = (float) vector3.Z;
                      goto label_424;
                    }
                    else
                      goto label_424;
                  }
                  else if ((uint) type <= 271U)
                  {
                    switch (type)
                    {
                      case 235:
                        if ((double) lightingState.r2 < 0.6)
                          lightingState.r2 = 0.6f;
                        if ((double) lightingState.g2 < 0.6)
                        {
                          lightingState.g2 = 0.6f;
                          goto label_424;
                        }
                        else
                          goto label_424;
                      case 237:
                        num8 = 0.1f;
                        num9 = 0.1f;
                        goto label_424;
                      case 238:
                        if ((double) lightingState.r2 < 0.5)
                          lightingState.r2 = 0.5f;
                        if ((double) lightingState.b2 < 0.5)
                        {
                          lightingState.b2 = 0.5f;
                          goto label_424;
                        }
                        else
                          goto label_424;
                      case 262:
                        num8 = 0.75f;
                        num10 = 0.75f;
                        goto label_424;
                      case 263:
                        num8 = 0.75f;
                        num9 = 0.75f;
                        goto label_424;
                      case 264:
                        num10 = 0.75f;
                        goto label_424;
                      case 265:
                        num9 = 0.75f;
                        goto label_424;
                      case 266:
                        num8 = 0.75f;
                        goto label_424;
                      case 267:
                        num8 = 0.75f;
                        num9 = 0.75f;
                        num10 = 0.75f;
                        goto label_424;
                      case 268:
                        num8 = 0.75f;
                        num9 = 0.375f;
                        goto label_424;
                      case 270:
                        num8 = 0.73f;
                        num9 = 1f;
                        num10 = 0.41f;
                        goto label_424;
                      case 271:
                        num8 = 0.45f;
                        num9 = 0.95f;
                        num10 = 1f;
                        goto label_424;
                      default:
                        goto label_424;
                    }
                  }
                  else
                  {
                    switch (type)
                    {
                      case 286:
                        num8 = 0.1f;
                        num9 = 0.2f;
                        num10 = 0.7f;
                        goto label_424;
                      case 302:
                        goto label_348;
                      case 316:
                      case 317:
                      case 318:
                        int index3 = (index1 - (int) tile.frameX / 18) / 2 * ((index2 - (int) tile.frameY / 18) / 3) % Main.cageFrames;
                        bool flag1 = (int) Main.jellyfishCageMode[(int) tile.type - 316, index3] == 2;
                        if ((int) tile.type == 316)
                        {
                          if (flag1)
                          {
                            num8 = 0.2f;
                            num9 = 0.3f;
                            num10 = 0.8f;
                          }
                          else
                          {
                            num8 = 0.1f;
                            num9 = 0.2f;
                            num10 = 0.5f;
                          }
                        }
                        if ((int) tile.type == 317)
                        {
                          if (flag1)
                          {
                            num8 = 0.2f;
                            num9 = 0.7f;
                            num10 = 0.3f;
                          }
                          else
                          {
                            num8 = 0.05f;
                            num9 = 0.45f;
                            num10 = 0.1f;
                          }
                        }
                        if ((int) tile.type == 318)
                        {
                          if (flag1)
                          {
                            num8 = 0.7f;
                            num9 = 0.2f;
                            num10 = 0.5f;
                            goto label_424;
                          }
                          else
                          {
                            num8 = 0.4f;
                            num9 = 0.1f;
                            num10 = 0.25f;
                            goto label_424;
                          }
                        }
                        else
                          goto label_424;
                      default:
                        goto label_424;
                    }
                  }
                }
                else if ((uint) type <= 381U)
                {
                  if ((uint) type <= 350U)
                  {
                    switch (type)
                    {
                      case 327:
                        float num16 = 0.5f + (float) (270 - (int) Main.mouseTextColor) / 1500f + (float) Main.rand.Next(0, 50) * 0.0005f;
                        num8 = 1f * num16;
                        num9 = 0.5f * num16;
                        num10 = 0.1f * num16;
                        goto label_424;
                      case 336:
                        num8 = 0.85f;
                        num9 = 0.5f;
                        num10 = 0.3f;
                        goto label_424;
                      case 340:
                        num8 = 0.45f;
                        num9 = 1f;
                        num10 = 0.45f;
                        goto label_424;
                      case 341:
                        num8 = (float) (0.400000005960464 * (double) Main.demonTorch + 0.600000023841858 * (1.0 - (double) Main.demonTorch));
                        num9 = 0.35f;
                        num10 = (float) (1.0 * (double) Main.demonTorch + 0.600000023841858 * (1.0 - (double) Main.demonTorch));
                        goto label_424;
                      case 342:
                        num8 = 0.5f;
                        num9 = 0.5f;
                        num10 = 1.1f;
                        goto label_424;
                      case 343:
                        num8 = 0.85f;
                        num9 = 0.85f;
                        num10 = 0.3f;
                        goto label_424;
                      case 344:
                        num8 = 0.6f;
                        num9 = 1.026f;
                        num10 = 0.96f;
                        goto label_424;
                      case 347:
                        break;
                      case 348:
                      case 349:
                        goto label_389;
                      case 350:
                        double num17 = Main.time * 0.08;
                        float num18 = (float) (-Math.Cos((int) (num17 / 6.283) % 3 == 1 ? num17 : 0.0) * 0.1 + 0.1);
                        num8 = num18;
                        num9 = num18;
                        num10 = num18;
                        goto label_424;
                      default:
                        goto label_424;
                    }
                  }
                  else
                  {
                    switch (type)
                    {
                      case 370:
                        num8 = 0.32f;
                        num9 = 0.16f;
                        num10 = 0.12f;
                        goto label_424;
                      case 372:
                        if ((int) tile.frameX == 0)
                        {
                          num8 = 0.9f;
                          num9 = 0.1f;
                          num10 = 0.75f;
                          goto label_424;
                        }
                        else
                          goto label_424;
                      case 381:
                        num8 = 0.25f;
                        num9 = 0.1f;
                        num10 = 0.0f;
                        goto label_424;
                      default:
                        goto label_424;
                    }
                  }
                }
                else if ((uint) type <= 405U)
                {
                  switch (type)
                  {
                    case 390:
                      num8 = 0.4f;
                      num9 = 0.2f;
                      num10 = 0.1f;
                      goto label_424;
                    case 391:
                      num8 = 0.3f;
                      num9 = 0.1f;
                      num10 = 0.25f;
                      goto label_424;
                    case 405:
                      if ((int) tile.frameX < 54)
                      {
                        float num11 = (float) Main.rand.Next(28, 42) * 0.005f + (float) (270 - (int) Main.mouseTextColor) / 700f;
                        float num12;
                        float num14;
                        float num15;
                        switch ((int) tile.frameX / 54)
                        {
                          case 1:
                            num12 = 0.7f;
                            num14 = 1f;
                            num15 = 0.5f;
                            break;
                          case 2:
                            num12 = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                            num14 = 0.3f;
                            num15 = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                            break;
                          case 3:
                            num12 = 0.45f;
                            num14 = 0.75f;
                            num15 = 1f;
                            break;
                          case 4:
                            num12 = 1.15f;
                            num14 = 1.15f;
                            num15 = 0.5f;
                            break;
                          case 5:
                            num12 = (float) Main.DiscoR / (float) byte.MaxValue;
                            num14 = (float) Main.DiscoG / (float) byte.MaxValue;
                            num15 = (float) Main.DiscoB / (float) byte.MaxValue;
                            break;
                          default:
                            num12 = 0.9f;
                            num14 = 0.3f;
                            num15 = 0.1f;
                            break;
                        }
                        num8 = num12 + num11;
                        num9 = num14 + num11;
                        num10 = num15 + num11;
                        goto label_424;
                      }
                      else
                        goto label_424;
                    default:
                      goto label_424;
                  }
                }
                else
                {
                  switch (type)
                  {
                    case 415:
                      num8 = 0.7f;
                      num9 = 0.5f;
                      num10 = 0.1f;
                      goto label_424;
                    case 416:
                      num8 = 0.0f;
                      num9 = 0.6f;
                      num10 = 0.7f;
                      goto label_424;
                    case 417:
                      num8 = 0.6f;
                      num9 = 0.2f;
                      num10 = 0.6f;
                      goto label_424;
                    case 418:
                      num8 = 0.6f;
                      num9 = 0.6f;
                      num10 = 0.9f;
                      goto label_424;
                    case 429:
                      int num25 = (int) tile.frameX / 18;
                      bool flag2 = num25 % 2 >= 1;
                      bool flag3 = num25 % 4 >= 2;
                      bool flag4 = num25 % 8 >= 4;
                      bool flag5 = num25 % 16 >= 8;
                      if (flag2)
                        num8 += 0.5f;
                      if (flag3)
                        num9 += 0.5f;
                      if (flag4)
                        num10 += 0.5f;
                      if (flag5)
                      {
                        num8 += 0.2f;
                        num9 += 0.2f;
                        goto label_424;
                      }
                      else
                        goto label_424;
                    case 463:
                      num8 = 0.2f;
                      num9 = 0.4f;
                      num10 = 0.8f;
                      goto label_424;
                    default:
                      goto label_424;
                  }
                }
                num8 = 0.35f;
                goto label_424;
              }
label_348:
              num8 = 0.83f;
              num9 = 0.6f;
              num10 = 0.5f;
              goto label_424;
label_389:
              if ((int) tile.type != 349 || (int) tile.frameX >= 36)
              {
                float num11 = (float) Main.rand.Next(28, 42) * 0.005f + (float) (270 - (int) Main.mouseTextColor) / 1000f;
                num8 = 0.1f;
                num9 = (float) (0.200000002980232 + (double) num11 / 2.0);
                num10 = 0.7f + num11;
              }
            }
          }
label_424:
          if (Lighting.RGB)
          {
            if ((double) lightingState.r2 < (double) num8)
              lightingState.r2 = num8;
            if ((double) lightingState.g2 < (double) num9)
              lightingState.g2 = num9;
            if ((double) lightingState.b2 < (double) num10)
              lightingState.b2 = num10;
          }
          else
          {
            float num11 = (float) (((double) num8 + (double) num9 + (double) num10) / 3.0);
            if ((double) lightingState.r2 < (double) num11)
              lightingState.r2 = num11;
          }
          if (tile.lava() && (int) tile.liquid > 0)
          {
            if (Lighting.RGB)
            {
              float num11 = (float) ((double) ((int) tile.liquid / (int) byte.MaxValue) * 0.409999996423721 + 0.140000000596046);
              float num12 = 0.55f + (float) (270 - (int) Main.mouseTextColor) / 900f;
              if ((double) lightingState.r2 < (double) num12)
                lightingState.r2 = num12;
              if ((double) lightingState.g2 < (double) num12)
                lightingState.g2 = num12 * 0.6f;
              if ((double) lightingState.b2 < (double) num12)
                lightingState.b2 = num12 * 0.2f;
            }
            else
            {
              float num11 = (float) ((double) ((int) tile.liquid / (int) byte.MaxValue) * 0.379999995231628 + 0.0799999982118607) + (float) (270 - (int) Main.mouseTextColor) / 2000f;
              if ((double) lightingState.r2 < (double) num11)
                lightingState.r2 = num11;
            }
          }
          else if ((int) tile.liquid > 128)
          {
            lightingState.wetLight = true;
            if (tile.honey())
              lightingState.honeyLight = true;
          }
          if ((double) lightingState.r2 > 0.0 || Lighting.RGB && ((double) lightingState.g2 > 0.0 || (double) lightingState.b2 > 0.0))
          {
            int num11 = index1 - Lighting.firstToLightX;
            int num12 = index2 - Lighting.firstToLightY;
            if (Lighting.minX > num11)
              Lighting.minX = num11;
            if (Lighting.maxX < num11 + 1)
              Lighting.maxX = num11 + 1;
            if (Lighting.minY > num12)
              Lighting.minY = num12;
            if (Lighting.maxY < num12 + 1)
              Lighting.maxY = num12 + 1;
          }
        }
      }
      foreach (KeyValuePair<Point16, Lighting.ColorTriplet> tempLight in Lighting.tempLights)
      {
        int index1 = (int) tempLight.Key.X - Lighting.firstTileX + Lighting.offScreenTiles;
        int index2 = (int) tempLight.Key.Y - Lighting.firstTileY + Lighting.offScreenTiles;
        if (index1 >= 0 && index1 < Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 && (index2 >= 0 && index2 < Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10))
        {
          Lighting.LightingState lightingState = Lighting.states[index1][index2];
          if ((double) lightingState.r2 < (double) tempLight.Value.r)
            lightingState.r2 = tempLight.Value.r;
          if ((double) lightingState.g2 < (double) tempLight.Value.g)
            lightingState.g2 = tempLight.Value.g;
          if ((double) lightingState.b2 < (double) tempLight.Value.b)
            lightingState.b2 = tempLight.Value.b;
          if (Lighting.minX > index1)
            Lighting.minX = index1;
          if (Lighting.maxX < index1 + 1)
            Lighting.maxX = index1 + 1;
          if (Lighting.minY > index2)
            Lighting.minY = index2;
          if (Lighting.maxY < index2 + 1)
            Lighting.maxY = index2 + 1;
        }
      }
      if (!Main.gamePaused)
        Lighting.tempLights.Clear();
      if (screenTileCounts[27] > 0)
        Main.sunflower = true;
      Main.holyTiles = screenTileCounts[109] + screenTileCounts[110] + screenTileCounts[113] + screenTileCounts[117] + screenTileCounts[116] + screenTileCounts[164] + screenTileCounts[403] + screenTileCounts[402];
      Main.evilTiles = screenTileCounts[23] + screenTileCounts[24] + screenTileCounts[25] + screenTileCounts[32] + screenTileCounts[112] + screenTileCounts[163] + screenTileCounts[400] + screenTileCounts[398] + -5 * screenTileCounts[27];
      Main.bloodTiles = screenTileCounts[199] + screenTileCounts[203] + screenTileCounts[200] + screenTileCounts[401] + screenTileCounts[399] + screenTileCounts[234] + screenTileCounts[352] - 5 * screenTileCounts[27];
      Main.snowTiles = screenTileCounts[147] + screenTileCounts[148] + screenTileCounts[161] + screenTileCounts[162] + screenTileCounts[164] + screenTileCounts[163] + screenTileCounts[200];
      Main.jungleTiles = screenTileCounts[60] + screenTileCounts[61] + screenTileCounts[62] + screenTileCounts[74] + screenTileCounts[226];
      Main.shroomTiles = screenTileCounts[70] + screenTileCounts[71] + screenTileCounts[72];
      Main.meteorTiles = screenTileCounts[37];
      Main.dungeonTiles = screenTileCounts[41] + screenTileCounts[43] + screenTileCounts[44];
      Main.sandTiles = screenTileCounts[53] + screenTileCounts[112] + screenTileCounts[116] + screenTileCounts[234] + screenTileCounts[397] + screenTileCounts[398] + screenTileCounts[402] + screenTileCounts[399] + screenTileCounts[396] + screenTileCounts[400] + screenTileCounts[403] + screenTileCounts[401];
      Main.waterCandles = screenTileCounts[49];
      Main.peaceCandles = screenTileCounts[372];
      Main.partyMonoliths = screenTileCounts[455];
      if (Main.player[Main.myPlayer].accOreFinder)
      {
        Main.player[Main.myPlayer].bestOre = -1;
        for (int index = 0; index < 470; ++index)
        {
          if (screenTileCounts[index] > 0 && (int) Main.tileValue[index] > 0 && (Main.player[Main.myPlayer].bestOre < 0 || (int) Main.tileValue[index] > (int) Main.tileValue[Main.player[Main.myPlayer].bestOre]))
            Main.player[Main.myPlayer].bestOre = index;
        }
      }
      if (Main.holyTiles < 0)
        Main.holyTiles = 0;
      if (Main.evilTiles < 0)
        Main.evilTiles = 0;
      if (Main.bloodTiles < 0)
        Main.bloodTiles = 0;
      int holyTiles = Main.holyTiles;
      Main.holyTiles -= Main.evilTiles;
      Main.holyTiles -= Main.bloodTiles;
      Main.evilTiles -= holyTiles;
      Main.bloodTiles -= holyTiles;
      if (Main.holyTiles < 0)
        Main.holyTiles = 0;
      if (Main.evilTiles < 0)
        Main.evilTiles = 0;
      if (Main.bloodTiles < 0)
        Main.bloodTiles = 0;
      Lighting.minX += Lighting.firstToLightX;
      Lighting.maxX += Lighting.firstToLightX;
      Lighting.minY += Lighting.firstToLightY;
      Lighting.maxY += Lighting.firstToLightY;
      Lighting.minX7 = Lighting.minX;
      Lighting.maxX7 = Lighting.maxX;
      Lighting.minY7 = Lighting.minY;
      Lighting.maxY7 = Lighting.maxY;
      Lighting.firstTileX7 = Lighting.firstTileX;
      Lighting.lastTileX7 = Lighting.lastTileX;
      Lighting.lastTileY7 = Lighting.lastTileY;
      Lighting.firstTileY7 = Lighting.firstTileY;
      Lighting.firstToLightX7 = Lighting.firstToLightX;
      Lighting.lastToLightX7 = Lighting.lastToLightX;
      Lighting.firstToLightY7 = Lighting.firstToLightY;
      Lighting.lastToLightY7 = Lighting.lastToLightY;
      Lighting.firstToLightX27 = Lighting.firstTileX - Lighting.offScreenTiles2;
      Lighting.firstToLightY27 = Lighting.firstTileY - Lighting.offScreenTiles2;
      Lighting.lastToLightX27 = Lighting.lastTileX + Lighting.offScreenTiles2;
      Lighting.lastToLightY27 = Lighting.lastTileY + Lighting.offScreenTiles2;
      Lighting.scrX = (int) Math.Floor(Main.screenPosition.X / 16.0);
      Lighting.scrY = (int) Math.Floor(Main.screenPosition.Y / 16.0);
      Main.renderCount = 0;
      TimeLogger.LightingTime(0, stopwatch.Elapsed.TotalMilliseconds);
      Lighting.doColors();
    }

    public static void doColors()
    {
      if (Lighting.lightMode < 2)
      {
        Lighting.blueWave += (float) Lighting.blueDir * 0.0001f;
        if ((double) Lighting.blueWave > 1.0)
        {
          Lighting.blueWave = 1f;
          Lighting.blueDir = -1;
        }
        else if ((double) Lighting.blueWave < 0.970000028610229)
        {
          Lighting.blueWave = 0.97f;
          Lighting.blueDir = 1;
        }
        if (Lighting.RGB)
        {
          Lighting.negLight = 0.91f;
          Lighting.negLight2 = 0.56f;
          Lighting.honeyLightG = 0.7f * Lighting.negLight * Lighting.blueWave;
          Lighting.honeyLightR = 0.75f * Lighting.negLight * Lighting.blueWave;
          Lighting.honeyLightB = 0.6f * Lighting.negLight * Lighting.blueWave;
          switch (Main.waterStyle)
          {
            case 0:
            case 1:
            case 7:
            case 8:
              Lighting.wetLightG = 0.96f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightR = 0.88f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightB = 1.015f * Lighting.negLight * Lighting.blueWave;
              break;
            case 2:
              Lighting.wetLightG = 0.85f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightR = 0.94f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightB = 1.01f * Lighting.negLight * Lighting.blueWave;
              break;
            case 3:
              Lighting.wetLightG = 0.95f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightR = 0.84f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightB = 1.015f * Lighting.negLight * Lighting.blueWave;
              break;
            case 4:
              Lighting.wetLightG = 0.86f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightR = 0.9f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightB = 1.01f * Lighting.negLight * Lighting.blueWave;
              break;
            case 5:
              Lighting.wetLightG = 0.99f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightR = 0.84f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightB = 1.01f * Lighting.negLight * Lighting.blueWave;
              break;
            case 6:
              Lighting.wetLightG = 0.98f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightR = 0.95f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightB = 0.85f * Lighting.negLight * Lighting.blueWave;
              break;
            case 9:
              Lighting.wetLightG = 0.88f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightR = 1f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightB = 0.84f * Lighting.negLight * Lighting.blueWave;
              break;
            case 10:
              Lighting.wetLightG = 1f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightR = 0.83f * Lighting.negLight * Lighting.blueWave;
              Lighting.wetLightB = 1f * Lighting.negLight * Lighting.blueWave;
              break;
            default:
              Lighting.wetLightG = 0.0f;
              Lighting.wetLightR = 0.0f;
              Lighting.wetLightB = 0.0f;
              break;
          }
        }
        else
        {
          Lighting.negLight = 0.9f;
          Lighting.negLight2 = 0.54f;
          Lighting.wetLightR = 0.95f * Lighting.negLight * Lighting.blueWave;
        }
        if (Main.player[Main.myPlayer].nightVision)
        {
          Lighting.negLight *= 1.03f;
          Lighting.negLight2 *= 1.03f;
        }
        if (Main.player[Main.myPlayer].blind)
        {
          Lighting.negLight *= 0.95f;
          Lighting.negLight2 *= 0.95f;
        }
        if (Main.player[Main.myPlayer].blackout)
        {
          Lighting.negLight *= 0.85f;
          Lighting.negLight2 *= 0.85f;
        }
        if (Main.player[Main.myPlayer].headcovered)
        {
          Lighting.negLight *= 0.85f;
          Lighting.negLight2 *= 0.85f;
        }
      }
      else
      {
        Lighting.negLight = 0.04f;
        Lighting.negLight2 = 0.16f;
        if (Main.player[Main.myPlayer].nightVision)
        {
          Lighting.negLight -= 0.013f;
          Lighting.negLight2 -= 0.04f;
        }
        if (Main.player[Main.myPlayer].blind)
        {
          Lighting.negLight += 0.03f;
          Lighting.negLight2 += 0.06f;
        }
        if (Main.player[Main.myPlayer].blackout)
        {
          Lighting.negLight += 0.09f;
          Lighting.negLight2 += 0.18f;
        }
        if (Main.player[Main.myPlayer].headcovered)
        {
          Lighting.negLight += 0.09f;
          Lighting.negLight2 += 0.18f;
        }
        Lighting.wetLightR = Lighting.negLight * 1.2f;
        Lighting.wetLightG = Lighting.negLight * 1.1f;
      }
      int num1;
      int num2;
      switch (Main.renderCount)
      {
        case 0:
          num1 = 0;
          num2 = 1;
          break;
        case 1:
          num1 = 1;
          num2 = 3;
          break;
        case 2:
          num1 = 3;
          num2 = 4;
          break;
        default:
          num1 = 0;
          num2 = 0;
          break;
      }
      if (Lighting.LightingThreads < 0)
        Lighting.LightingThreads = 0;
      if (Lighting.LightingThreads >= Environment.ProcessorCount)
        Lighting.LightingThreads = Environment.ProcessorCount - 1;
      int lightingThreads = Lighting.LightingThreads;
      if (lightingThreads > 0)
        ++lightingThreads;
      Stopwatch stopwatch = new Stopwatch();
      for (int index1 = num1; index1 < num2; ++index1)
      {
        stopwatch.Restart();
        switch (index1)
        {
          case 0:
            Lighting.swipe.innerLoop1Start = Lighting.minY7 - Lighting.firstToLightY7;
            Lighting.swipe.innerLoop1End = Lighting.lastToLightY27 + Lighting.maxRenderCount - Lighting.firstToLightY7;
            Lighting.swipe.innerLoop2Start = Lighting.maxY7 - Lighting.firstToLightY;
            Lighting.swipe.innerLoop2End = Lighting.firstTileY7 - Lighting.maxRenderCount - Lighting.firstToLightY7;
            Lighting.swipe.outerLoopStart = Lighting.minX7 - Lighting.firstToLightX7;
            Lighting.swipe.outerLoopEnd = Lighting.maxX7 - Lighting.firstToLightX7;
            Lighting.swipe.jaggedArray = Lighting.states;
            break;
          case 1:
            Lighting.swipe.innerLoop1Start = Lighting.minX7 - Lighting.firstToLightX7;
            Lighting.swipe.innerLoop1End = Lighting.lastTileX7 + Lighting.maxRenderCount - Lighting.firstToLightX7;
            Lighting.swipe.innerLoop2Start = Lighting.maxX7 - Lighting.firstToLightX7;
            Lighting.swipe.innerLoop2End = Lighting.firstTileX7 - Lighting.maxRenderCount - Lighting.firstToLightX7;
            Lighting.swipe.outerLoopStart = Lighting.firstToLightY7 - Lighting.firstToLightY7;
            Lighting.swipe.outerLoopEnd = Lighting.lastToLightY7 - Lighting.firstToLightY7;
            Lighting.swipe.jaggedArray = Lighting.axisFlipStates;
            break;
          case 2:
            Lighting.swipe.innerLoop1Start = Lighting.firstToLightY27 - Lighting.firstToLightY7;
            Lighting.swipe.innerLoop1End = Lighting.lastTileY7 + Lighting.maxRenderCount - Lighting.firstToLightY7;
            Lighting.swipe.innerLoop2Start = Lighting.lastToLightY27 - Lighting.firstToLightY;
            Lighting.swipe.innerLoop2End = Lighting.firstTileY7 - Lighting.maxRenderCount - Lighting.firstToLightY7;
            Lighting.swipe.outerLoopStart = Lighting.firstToLightX27 - Lighting.firstToLightX7;
            Lighting.swipe.outerLoopEnd = Lighting.lastToLightX27 - Lighting.firstToLightX7;
            Lighting.swipe.jaggedArray = Lighting.states;
            break;
          case 3:
            Lighting.swipe.innerLoop1Start = Lighting.firstToLightX27 - Lighting.firstToLightX7;
            Lighting.swipe.innerLoop1End = Lighting.lastTileX7 + Lighting.maxRenderCount - Lighting.firstToLightX7;
            Lighting.swipe.innerLoop2Start = Lighting.lastToLightX27 - Lighting.firstToLightX7;
            Lighting.swipe.innerLoop2End = Lighting.firstTileX7 - Lighting.maxRenderCount - Lighting.firstToLightX7;
            Lighting.swipe.outerLoopStart = Lighting.firstToLightY27 - Lighting.firstToLightY7;
            Lighting.swipe.outerLoopEnd = Lighting.lastToLightY27 - Lighting.firstToLightY7;
            Lighting.swipe.jaggedArray = Lighting.axisFlipStates;
            break;
        }
        if (Lighting.swipe.innerLoop1Start > Lighting.swipe.innerLoop1End)
          Lighting.swipe.innerLoop1Start = Lighting.swipe.innerLoop1End;
        if (Lighting.swipe.innerLoop2Start < Lighting.swipe.innerLoop2End)
          Lighting.swipe.innerLoop2Start = Lighting.swipe.innerLoop2End;
        if (Lighting.swipe.outerLoopStart > Lighting.swipe.outerLoopEnd)
          Lighting.swipe.outerLoopStart = Lighting.swipe.outerLoopEnd;
        switch (Lighting.lightMode)
        {
          case 0:
            Lighting.swipe.function = new Action<Lighting.LightingSwipeData>(Lighting.doColors_Mode0_Swipe);
            break;
          case 1:
            Lighting.swipe.function = new Action<Lighting.LightingSwipeData>(Lighting.doColors_Mode1_Swipe);
            break;
          case 2:
            Lighting.swipe.function = new Action<Lighting.LightingSwipeData>(Lighting.doColors_Mode2_Swipe);
            break;
          case 3:
            Lighting.swipe.function = new Action<Lighting.LightingSwipeData>(Lighting.doColors_Mode3_Swipe);
            break;
          default:
            Lighting.swipe.function = (Action<Lighting.LightingSwipeData>) null;
            break;
        }
        if (lightingThreads == 0)
        {
          Lighting.swipe.function(Lighting.swipe);
        }
        else
        {
          int num3 = Lighting.swipe.outerLoopEnd - Lighting.swipe.outerLoopStart;
          int num4 = num3 / lightingThreads;
          int num5 = num3 % lightingThreads;
          int outerLoopStart = Lighting.swipe.outerLoopStart;
          Lighting.countdown.Reset(lightingThreads);
          for (int index2 = 0; index2 < lightingThreads; ++index2)
          {
            Lighting.LightingSwipeData threadSwipe = Lighting.threadSwipes[index2];
            threadSwipe.CopyFrom(Lighting.swipe);
            threadSwipe.outerLoopStart = outerLoopStart;
            outerLoopStart += num4;
            if (num5 > 0)
            {
              ++outerLoopStart;
              --num5;
            }
            threadSwipe.outerLoopEnd = outerLoopStart;
            ThreadPool.QueueUserWorkItem(new WaitCallback(Lighting.callback_LightingSwipe), (object) threadSwipe);
          }
          while (Lighting.countdown.CurrentCount != 0)
            ;
        }
        TimeLogger.LightingTime(index1 + 1, stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    private static void callback_LightingSwipe(object obj)
    {
      Lighting.LightingSwipeData lightingSwipeData = obj as Lighting.LightingSwipeData;
      try
      {
        lightingSwipeData.function(lightingSwipeData);
      }
      catch
      {
      }
      Lighting.countdown.Signal();
    }

    private static void doColors_Mode0_Swipe(Lighting.LightingSwipeData swipeData)
    {
      try
      {
        bool flag1 = true;
        while (true)
        {
          int num1;
          int num2;
          int num3;
          if (flag1)
          {
            num1 = 1;
            num2 = swipeData.innerLoop1Start;
            num3 = swipeData.innerLoop1End;
          }
          else
          {
            num1 = -1;
            num2 = swipeData.innerLoop2Start;
            num3 = swipeData.innerLoop2End;
          }
          int outerLoopStart = swipeData.outerLoopStart;
          int outerLoopEnd = swipeData.outerLoopEnd;
          for (int index1 = outerLoopStart; index1 < outerLoopEnd; ++index1)
          {
            Lighting.LightingState[] jagged = swipeData.jaggedArray[index1];
            float num4 = 0.0f;
            float num5 = 0.0f;
            float num6 = 0.0f;
            int num7 = num2;
            int num8 = num3;
            int index2 = num7;
            while (index2 != num8)
            {
              Lighting.LightingState lightingState1 = jagged[index2];
              Lighting.LightingState lightingState2 = jagged[index2 + num1];
              bool flag2;
              bool flag3 = flag2 = false;
              if ((double) lightingState1.r2 > (double) num4)
                num4 = lightingState1.r2;
              else if ((double) num4 <= 0.0185)
                flag3 = true;
              else if ((double) lightingState1.r2 < (double) num4)
                lightingState1.r2 = num4;
              if (!flag3 && (double) lightingState2.r2 <= (double) num4)
              {
                if (lightingState1.stopLight)
                  num4 *= Lighting.negLight2;
                else if (lightingState1.wetLight)
                {
                  if (lightingState1.honeyLight)
                    num4 *= (float) ((double) Lighting.honeyLightR * (double) swipeData.rand.Next(98, 100) * 0.00999999977648258);
                  else
                    num4 *= (float) ((double) Lighting.wetLightR * (double) swipeData.rand.Next(98, 100) * 0.00999999977648258);
                }
                else
                  num4 *= Lighting.negLight;
              }
              if ((double) lightingState1.g2 > (double) num5)
                num5 = lightingState1.g2;
              else if ((double) num5 <= 0.0185)
                flag2 = true;
              else
                lightingState1.g2 = num5;
              if (!flag2 && (double) lightingState2.g2 <= (double) num5)
              {
                if (lightingState1.stopLight)
                  num5 *= Lighting.negLight2;
                else if (lightingState1.wetLight)
                {
                  if (lightingState1.honeyLight)
                    num5 *= (float) ((double) Lighting.honeyLightG * (double) swipeData.rand.Next(97, 100) * 0.00999999977648258);
                  else
                    num5 *= (float) ((double) Lighting.wetLightG * (double) swipeData.rand.Next(97, 100) * 0.00999999977648258);
                }
                else
                  num5 *= Lighting.negLight;
              }
              if ((double) lightingState1.b2 > (double) num6)
                num6 = lightingState1.b2;
              else if ((double) num6 > 0.0185)
                lightingState1.b2 = num6;
              else
                goto label_45;
              if ((double) lightingState2.b2 < (double) num6)
              {
                if (lightingState1.stopLight)
                  num6 *= Lighting.negLight2;
                else if (lightingState1.wetLight)
                {
                  if (lightingState1.honeyLight)
                    num6 *= (float) ((double) Lighting.honeyLightB * (double) swipeData.rand.Next(97, 100) * 0.00999999977648258);
                  else
                    num6 *= (float) ((double) Lighting.wetLightB * (double) swipeData.rand.Next(97, 100) * 0.00999999977648258);
                }
                else
                  num6 *= Lighting.negLight;
              }
label_45:
              index2 += num1;
            }
          }
          if (flag1)
            flag1 = false;
          else
            break;
        }
      }
      catch
      {
      }
    }

    private static void doColors_Mode1_Swipe(Lighting.LightingSwipeData swipeData)
    {
      try
      {
        bool flag = true;
        while (true)
        {
          int num1;
          int num2;
          int num3;
          if (flag)
          {
            num1 = 1;
            num2 = swipeData.innerLoop1Start;
            num3 = swipeData.innerLoop1End;
          }
          else
          {
            num1 = -1;
            num2 = swipeData.innerLoop2Start;
            num3 = swipeData.innerLoop2End;
          }
          int outerLoopStart = swipeData.outerLoopStart;
          int outerLoopEnd = swipeData.outerLoopEnd;
          for (int index1 = outerLoopStart; index1 < outerLoopEnd; ++index1)
          {
            Lighting.LightingState[] jagged = swipeData.jaggedArray[index1];
            float num4 = 0.0f;
            int index2 = num2;
            while (index2 != num3)
            {
              Lighting.LightingState lightingState = jagged[index2];
              if ((double) lightingState.r2 > (double) num4)
                num4 = lightingState.r2;
              else if ((double) num4 > 0.0185)
              {
                if ((double) lightingState.r2 < (double) num4)
                  lightingState.r2 = num4;
              }
              else
                goto label_19;
              if ((double) jagged[index2 + num1].r2 <= (double) num4)
              {
                if (lightingState.stopLight)
                  num4 *= Lighting.negLight2;
                else if (lightingState.wetLight)
                {
                  if (lightingState.honeyLight)
                    num4 *= (float) ((double) Lighting.honeyLightR * (double) swipeData.rand.Next(98, 100) * 0.00999999977648258);
                  else
                    num4 *= (float) ((double) Lighting.wetLightR * (double) swipeData.rand.Next(98, 100) * 0.00999999977648258);
                }
                else
                  num4 *= Lighting.negLight;
              }
label_19:
              index2 += num1;
            }
          }
          if (flag)
            flag = false;
          else
            break;
        }
      }
      catch
      {
      }
    }

    private static void doColors_Mode2_Swipe(Lighting.LightingSwipeData swipeData)
    {
      try
      {
        bool flag = true;
        while (true)
        {
          int num1;
          int num2;
          int num3;
          if (flag)
          {
            num1 = 1;
            num2 = swipeData.innerLoop1Start;
            num3 = swipeData.innerLoop1End;
          }
          else
          {
            num1 = -1;
            num2 = swipeData.innerLoop2Start;
            num3 = swipeData.innerLoop2End;
          }
          int outerLoopStart = swipeData.outerLoopStart;
          int outerLoopEnd = swipeData.outerLoopEnd;
          for (int index1 = outerLoopStart; index1 < outerLoopEnd; ++index1)
          {
            Lighting.LightingState[] jagged = swipeData.jaggedArray[index1];
            float num4 = 0.0f;
            int index2 = num2;
            while (index2 != num3)
            {
              Lighting.LightingState lightingState = jagged[index2];
              if ((double) lightingState.r2 > (double) num4)
                num4 = lightingState.r2;
              else if ((double) num4 > 0.0)
                lightingState.r2 = num4;
              else
                goto label_15;
              if (lightingState.stopLight)
                num4 -= Lighting.negLight2;
              else if (lightingState.wetLight)
                num4 -= Lighting.wetLightR;
              else
                num4 -= Lighting.negLight;
label_15:
              index2 += num1;
            }
          }
          if (flag)
            flag = false;
          else
            break;
        }
      }
      catch
      {
      }
    }

    private static void doColors_Mode3_Swipe(Lighting.LightingSwipeData swipeData)
    {
      try
      {
        bool flag1 = true;
        while (true)
        {
          int num1;
          int num2;
          int num3;
          if (flag1)
          {
            num1 = 1;
            num2 = swipeData.innerLoop1Start;
            num3 = swipeData.innerLoop1End;
          }
          else
          {
            num1 = -1;
            num2 = swipeData.innerLoop2Start;
            num3 = swipeData.innerLoop2End;
          }
          int outerLoopStart = swipeData.outerLoopStart;
          int outerLoopEnd = swipeData.outerLoopEnd;
          for (int index1 = outerLoopStart; index1 < outerLoopEnd; ++index1)
          {
            Lighting.LightingState[] jagged = swipeData.jaggedArray[index1];
            float num4 = 0.0f;
            float num5 = 0.0f;
            float num6 = 0.0f;
            int index2 = num2;
            while (index2 != num3)
            {
              Lighting.LightingState lightingState = jagged[index2];
              bool flag2;
              bool flag3 = flag2 = false;
              if ((double) lightingState.r2 > (double) num4)
                num4 = lightingState.r2;
              else if ((double) num4 <= 0.0)
                flag3 = true;
              else
                lightingState.r2 = num4;
              if (!flag3)
              {
                if (lightingState.stopLight)
                  num4 -= Lighting.negLight2;
                else if (lightingState.wetLight)
                  num4 -= Lighting.wetLightR;
                else
                  num4 -= Lighting.negLight;
              }
              if ((double) lightingState.g2 > (double) num5)
                num5 = lightingState.g2;
              else if ((double) num5 <= 0.0)
                flag2 = true;
              else
                lightingState.g2 = num5;
              if (!flag2)
              {
                if (lightingState.stopLight)
                  num5 -= Lighting.negLight2;
                else if (lightingState.wetLight)
                  num5 -= Lighting.wetLightG;
                else
                  num5 -= Lighting.negLight;
              }
              if ((double) lightingState.b2 > (double) num6)
                num6 = lightingState.b2;
              else if ((double) num6 > 0.0)
                lightingState.b2 = num6;
              else
                goto label_35;
              if (lightingState.stopLight)
                num6 -= Lighting.negLight2;
              else
                num6 -= Lighting.negLight;
label_35:
              index2 += num1;
            }
          }
          if (flag1)
            flag1 = false;
          else
            break;
        }
      }
      catch
      {
      }
    }

    public static void AddLight(Vector2 position, Vector3 rgb)
    {
      Lighting.AddLight((int) (position.X / 16.0), (int) (position.Y / 16.0), (float) rgb.X, (float) rgb.Y, (float) rgb.Z);
    }

    public static void AddLight(Vector2 position, float R, float G, float B)
    {
      Lighting.AddLight((int) (position.X / 16.0), (int) (position.Y / 16.0), R, G, B);
    }

    public static void AddLight(int i, int j, float R, float G, float B)
    {
      if (Main.gamePaused || Main.netMode == 2 || (i - Lighting.firstTileX + Lighting.offScreenTiles < 0 || i - Lighting.firstTileX + Lighting.offScreenTiles >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10) || (j - Lighting.firstTileY + Lighting.offScreenTiles < 0 || j - Lighting.firstTileY + Lighting.offScreenTiles >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10 || Lighting.tempLights.Count == Lighting.maxTempLights))
        return;
      Point16 key = new Point16(i, j);
      Lighting.ColorTriplet colorTriplet;
      if (Lighting.tempLights.TryGetValue(key, out colorTriplet))
      {
        if (Lighting.RGB)
        {
          if ((double) colorTriplet.r < (double) R)
            colorTriplet.r = R;
          if ((double) colorTriplet.g < (double) G)
            colorTriplet.g = G;
          if ((double) colorTriplet.b < (double) B)
            colorTriplet.b = B;
          Lighting.tempLights[key] = colorTriplet;
        }
        else
        {
          float averageColor = (float) (((double) R + (double) G + (double) B) / 3.0);
          if ((double) colorTriplet.r >= (double) averageColor)
            return;
          Lighting.tempLights[key] = new Lighting.ColorTriplet(averageColor);
        }
      }
      else
      {
        colorTriplet = !Lighting.RGB ? new Lighting.ColorTriplet((float) (((double) R + (double) G + (double) B) / 3.0)) : new Lighting.ColorTriplet(R, G, B);
        Lighting.tempLights.Add(key, colorTriplet);
      }
    }

    public static void NextLightMode()
    {
      Lighting.lightCounter += 100;
      ++Lighting.lightMode;
      if (Lighting.lightMode >= 4)
        Lighting.lightMode = 0;
      if (Lighting.lightMode != 2 && Lighting.lightMode != 0)
        return;
      Main.renderCount = 0;
      Main.renderNow = true;
      Lighting.BlackOut();
    }

    public static void BlackOut()
    {
      int num1 = Main.screenWidth / 16 + Lighting.offScreenTiles * 2;
      int num2 = Main.screenHeight / 16 + Lighting.offScreenTiles * 2;
      for (int index1 = 0; index1 < num1; ++index1)
      {
        Lighting.LightingState[] state = Lighting.states[index1];
        for (int index2 = 0; index2 < num2; ++index2)
        {
          Lighting.LightingState lightingState = state[index2];
          lightingState.r = 0.0f;
          lightingState.g = 0.0f;
          lightingState.b = 0.0f;
        }
      }
    }

    public static Color GetColor(int x, int y, Color oldColor)
    {
      int index1 = x - Lighting.firstTileX + Lighting.offScreenTiles;
      int index2 = y - Lighting.firstTileY + Lighting.offScreenTiles;
      if (Main.gameMenu)
        return oldColor;
      if (index1 < 0 || index2 < 0 || (index1 >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 || index2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10))
        return Color.get_Black();
      Color white = Color.get_White();
      Lighting.LightingState lightingState = Lighting.states[index1][index2];
      // ISSUE: explicit reference operation
      int num1 = (int) ((double) ((Color) @oldColor).get_R() * (double) lightingState.r * (double) Lighting.brightness);
      // ISSUE: explicit reference operation
      int num2 = (int) ((double) ((Color) @oldColor).get_G() * (double) lightingState.g * (double) Lighting.brightness);
      // ISSUE: explicit reference operation
      int num3 = (int) ((double) ((Color) @oldColor).get_B() * (double) lightingState.b * (double) Lighting.brightness);
      if (num1 > (int) byte.MaxValue)
        num1 = (int) byte.MaxValue;
      if (num2 > (int) byte.MaxValue)
        num2 = (int) byte.MaxValue;
      if (num3 > (int) byte.MaxValue)
        num3 = (int) byte.MaxValue;
      // ISSUE: explicit reference operation
      ((Color) @white).set_R((byte) num1);
      // ISSUE: explicit reference operation
      ((Color) @white).set_G((byte) num2);
      // ISSUE: explicit reference operation
      ((Color) @white).set_B((byte) num3);
      return white;
    }

    public static Color GetColor(int x, int y)
    {
      int index1 = x - Lighting.firstTileX + Lighting.offScreenTiles;
      int index2 = y - Lighting.firstTileY + Lighting.offScreenTiles;
      if (Main.gameMenu)
        return Color.get_White();
      if (index1 < 0 || index2 < 0 || (index1 >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 || index2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2))
        return Color.get_Black();
      Lighting.LightingState lightingState = Lighting.states[index1][index2];
      int num1 = (int) ((double) byte.MaxValue * (double) lightingState.r * (double) Lighting.brightness);
      int num2 = (int) ((double) byte.MaxValue * (double) lightingState.g * (double) Lighting.brightness);
      int num3 = (int) ((double) byte.MaxValue * (double) lightingState.b * (double) Lighting.brightness);
      if (num1 > (int) byte.MaxValue)
        num1 = (int) byte.MaxValue;
      if (num2 > (int) byte.MaxValue)
        num2 = (int) byte.MaxValue;
      if (num3 > (int) byte.MaxValue)
        num3 = (int) byte.MaxValue;
      Color color;
      // ISSUE: explicit reference operation
      ((Color) @color).\u002Ector((int) (byte) num1, (int) (byte) num2, (int) (byte) num3, (int) byte.MaxValue);
      return color;
    }

    public static void GetColor9Slice(int centerX, int centerY, ref Color[] slices)
    {
      int num1 = centerX - Lighting.firstTileX + Lighting.offScreenTiles;
      int num2 = centerY - Lighting.firstTileY + Lighting.offScreenTiles;
      if (num1 <= 0 || num2 <= 0 || (num1 >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 - 1 || num2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 - 1))
      {
        for (int index = 0; index < 9; ++index)
          slices[index] = Color.get_Black();
      }
      else
      {
        int index1 = 0;
        for (int index2 = num1 - 1; index2 <= num1 + 1; ++index2)
        {
          Lighting.LightingState[] state = Lighting.states[index2];
          for (int index3 = num2 - 1; index3 <= num2 + 1; ++index3)
          {
            Lighting.LightingState lightingState = state[index3];
            int num3 = (int) ((double) byte.MaxValue * (double) lightingState.r * (double) Lighting.brightness);
            int num4 = (int) ((double) byte.MaxValue * (double) lightingState.g * (double) Lighting.brightness);
            int num5 = (int) ((double) byte.MaxValue * (double) lightingState.b * (double) Lighting.brightness);
            if (num3 > (int) byte.MaxValue)
              num3 = (int) byte.MaxValue;
            if (num4 > (int) byte.MaxValue)
              num4 = (int) byte.MaxValue;
            if (num5 > (int) byte.MaxValue)
              num5 = (int) byte.MaxValue;
            slices[index1] = new Color((int) (byte) num3, (int) (byte) num4, (int) (byte) num5, (int) byte.MaxValue);
            index1 += 3;
          }
          index1 -= 8;
        }
      }
    }

    public static Vector3 GetSubLight(Vector2 position)
    {
      Vector2 vector2_1 = Vector2.op_Subtraction(Vector2.op_Division(position, 16f), new Vector2(0.5f, 0.5f));
      Vector2 vector2_2;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_2).\u002Ector((float) (vector2_1.X % 1.0), (float) (vector2_1.Y % 1.0));
      int index1 = (int) vector2_1.X - Lighting.firstTileX + Lighting.offScreenTiles;
      int index2 = (int) vector2_1.Y - Lighting.firstTileY + Lighting.offScreenTiles;
      if (index1 <= 0 || index2 <= 0 || (index1 >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 - 1 || index2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 - 1))
        return Vector3.get_One();
      Vector3 vector3_1 = Lighting.states[index1][index2].ToVector3();
      Vector3 vector3_2 = Lighting.states[index1 + 1][index2].ToVector3();
      Vector3 vector3_3 = Lighting.states[index1][index2 + 1].ToVector3();
      Vector3 vector3_4 = Lighting.states[index1 + 1][index2 + 1].ToVector3();
      return Vector3.Lerp(Vector3.Lerp(vector3_1, vector3_2, (float) vector2_2.X), Vector3.Lerp(vector3_3, vector3_4, (float) vector2_2.X), (float) vector2_2.Y);
    }

    public static void GetColor4Slice_New(int centerX, int centerY, out VertexColors vertices, float scale = 1f)
    {
      int index1 = centerX - Lighting.firstTileX + Lighting.offScreenTiles;
      int index2 = centerY - Lighting.firstTileY + Lighting.offScreenTiles;
      if (index1 <= 0 || index2 <= 0 || (index1 >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 - 1 || index2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 - 1))
      {
        vertices.BottomLeftColor = Color.get_Black();
        vertices.BottomRightColor = Color.get_Black();
        vertices.TopLeftColor = Color.get_Black();
        vertices.TopRightColor = Color.get_Black();
      }
      else
      {
        Lighting.LightingState lightingState1 = Lighting.states[index1][index2];
        Lighting.LightingState lightingState2 = Lighting.states[index1][index2 - 1];
        Lighting.LightingState lightingState3 = Lighting.states[index1][index2 + 1];
        Lighting.LightingState lightingState4 = Lighting.states[index1 - 1][index2];
        Lighting.LightingState lightingState5 = Lighting.states[index1 + 1][index2];
        Lighting.LightingState lightingState6 = Lighting.states[index1 - 1][index2 - 1];
        Lighting.LightingState lightingState7 = Lighting.states[index1 + 1][index2 - 1];
        Lighting.LightingState lightingState8 = Lighting.states[index1 - 1][index2 + 1];
        Lighting.LightingState lightingState9 = Lighting.states[index1 + 1][index2 + 1];
        float num1 = (float) ((double) Lighting.brightness * (double) scale * (double) byte.MaxValue * 0.25);
        float num2 = (lightingState2.r + lightingState6.r + lightingState4.r + lightingState1.r) * num1;
        float num3 = (lightingState2.g + lightingState6.g + lightingState4.g + lightingState1.g) * num1;
        float num4 = (lightingState2.b + lightingState6.b + lightingState4.b + lightingState1.b) * num1;
        if ((double) num2 > (double) byte.MaxValue)
          num2 = (float) byte.MaxValue;
        if ((double) num3 > (double) byte.MaxValue)
          num3 = (float) byte.MaxValue;
        if ((double) num4 > (double) byte.MaxValue)
          num4 = (float) byte.MaxValue;
        vertices.TopLeftColor = new Color((int) (byte) num2, (int) (byte) num3, (int) (byte) num4, (int) byte.MaxValue);
        float num5 = (lightingState2.r + lightingState7.r + lightingState5.r + lightingState1.r) * num1;
        float num6 = (lightingState2.g + lightingState7.g + lightingState5.g + lightingState1.g) * num1;
        float num7 = (lightingState2.b + lightingState7.b + lightingState5.b + lightingState1.b) * num1;
        if ((double) num5 > (double) byte.MaxValue)
          num5 = (float) byte.MaxValue;
        if ((double) num6 > (double) byte.MaxValue)
          num6 = (float) byte.MaxValue;
        if ((double) num7 > (double) byte.MaxValue)
          num7 = (float) byte.MaxValue;
        vertices.TopRightColor = new Color((int) (byte) num5, (int) (byte) num6, (int) (byte) num7, (int) byte.MaxValue);
        float num8 = (lightingState3.r + lightingState8.r + lightingState4.r + lightingState1.r) * num1;
        float num9 = (lightingState3.g + lightingState8.g + lightingState4.g + lightingState1.g) * num1;
        float num10 = (lightingState3.b + lightingState8.b + lightingState4.b + lightingState1.b) * num1;
        if ((double) num8 > (double) byte.MaxValue)
          num8 = (float) byte.MaxValue;
        if ((double) num9 > (double) byte.MaxValue)
          num9 = (float) byte.MaxValue;
        if ((double) num10 > (double) byte.MaxValue)
          num10 = (float) byte.MaxValue;
        vertices.BottomLeftColor = new Color((int) (byte) num8, (int) (byte) num9, (int) (byte) num10, (int) byte.MaxValue);
        float num11 = (lightingState3.r + lightingState9.r + lightingState5.r + lightingState1.r) * num1;
        float num12 = (lightingState3.g + lightingState9.g + lightingState5.g + lightingState1.g) * num1;
        float num13 = (lightingState3.b + lightingState9.b + lightingState5.b + lightingState1.b) * num1;
        if ((double) num11 > (double) byte.MaxValue)
          num11 = (float) byte.MaxValue;
        if ((double) num12 > (double) byte.MaxValue)
          num12 = (float) byte.MaxValue;
        if ((double) num13 > (double) byte.MaxValue)
          num13 = (float) byte.MaxValue;
        vertices.BottomRightColor = new Color((int) (byte) num11, (int) (byte) num12, (int) (byte) num13, (int) byte.MaxValue);
      }
    }

    public static void GetColor4Slice_New(int centerX, int centerY, out VertexColors vertices, Color centerColor, float scale = 1f)
    {
      int index1 = centerX - Lighting.firstTileX + Lighting.offScreenTiles;
      int index2 = centerY - Lighting.firstTileY + Lighting.offScreenTiles;
      if (index1 <= 0 || index2 <= 0 || (index1 >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 - 1 || index2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 - 1))
      {
        vertices.BottomLeftColor = Color.get_Black();
        vertices.BottomRightColor = Color.get_Black();
        vertices.TopLeftColor = Color.get_Black();
        vertices.TopRightColor = Color.get_Black();
      }
      else
      {
        // ISSUE: explicit reference operation
        float num1 = (float) ((Color) @centerColor).get_R() / (float) byte.MaxValue;
        // ISSUE: explicit reference operation
        float num2 = (float) ((Color) @centerColor).get_G() / (float) byte.MaxValue;
        // ISSUE: explicit reference operation
        float num3 = (float) ((Color) @centerColor).get_B() / (float) byte.MaxValue;
        Lighting.LightingState lightingState1 = Lighting.states[index1][index2 - 1];
        Lighting.LightingState lightingState2 = Lighting.states[index1][index2 + 1];
        Lighting.LightingState lightingState3 = Lighting.states[index1 - 1][index2];
        Lighting.LightingState lightingState4 = Lighting.states[index1 + 1][index2];
        Lighting.LightingState lightingState5 = Lighting.states[index1 - 1][index2 - 1];
        Lighting.LightingState lightingState6 = Lighting.states[index1 + 1][index2 - 1];
        Lighting.LightingState lightingState7 = Lighting.states[index1 - 1][index2 + 1];
        Lighting.LightingState lightingState8 = Lighting.states[index1 + 1][index2 + 1];
        float num4 = (float) ((double) Lighting.brightness * (double) scale * (double) byte.MaxValue * 0.25);
        float num5 = (lightingState1.r + lightingState5.r + lightingState3.r + num1) * num4;
        float num6 = (lightingState1.g + lightingState5.g + lightingState3.g + num2) * num4;
        float num7 = (lightingState1.b + lightingState5.b + lightingState3.b + num3) * num4;
        if ((double) num5 > (double) byte.MaxValue)
          num5 = (float) byte.MaxValue;
        if ((double) num6 > (double) byte.MaxValue)
          num6 = (float) byte.MaxValue;
        if ((double) num7 > (double) byte.MaxValue)
          num7 = (float) byte.MaxValue;
        vertices.TopLeftColor = new Color((int) (byte) num5, (int) (byte) num6, (int) (byte) num7, (int) byte.MaxValue);
        float num8 = (lightingState1.r + lightingState6.r + lightingState4.r + num1) * num4;
        float num9 = (lightingState1.g + lightingState6.g + lightingState4.g + num2) * num4;
        float num10 = (lightingState1.b + lightingState6.b + lightingState4.b + num3) * num4;
        if ((double) num8 > (double) byte.MaxValue)
          num8 = (float) byte.MaxValue;
        if ((double) num9 > (double) byte.MaxValue)
          num9 = (float) byte.MaxValue;
        if ((double) num10 > (double) byte.MaxValue)
          num10 = (float) byte.MaxValue;
        vertices.TopRightColor = new Color((int) (byte) num8, (int) (byte) num9, (int) (byte) num10, (int) byte.MaxValue);
        float num11 = (lightingState2.r + lightingState7.r + lightingState3.r + num1) * num4;
        float num12 = (lightingState2.g + lightingState7.g + lightingState3.g + num2) * num4;
        float num13 = (lightingState2.b + lightingState7.b + lightingState3.b + num3) * num4;
        if ((double) num11 > (double) byte.MaxValue)
          num11 = (float) byte.MaxValue;
        if ((double) num12 > (double) byte.MaxValue)
          num12 = (float) byte.MaxValue;
        if ((double) num13 > (double) byte.MaxValue)
          num13 = (float) byte.MaxValue;
        vertices.BottomLeftColor = new Color((int) (byte) num11, (int) (byte) num12, (int) (byte) num13, (int) byte.MaxValue);
        float num14 = (lightingState2.r + lightingState8.r + lightingState4.r + num1) * num4;
        float num15 = (lightingState2.g + lightingState8.g + lightingState4.g + num2) * num4;
        float num16 = (lightingState2.b + lightingState8.b + lightingState4.b + num3) * num4;
        if ((double) num14 > (double) byte.MaxValue)
          num14 = (float) byte.MaxValue;
        if ((double) num15 > (double) byte.MaxValue)
          num15 = (float) byte.MaxValue;
        if ((double) num16 > (double) byte.MaxValue)
          num16 = (float) byte.MaxValue;
        vertices.BottomRightColor = new Color((int) (byte) num14, (int) (byte) num15, (int) (byte) num16, (int) byte.MaxValue);
      }
    }

    public static void GetColor4Slice(int centerX, int centerY, ref Color[] slices)
    {
      int index1 = centerX - Lighting.firstTileX + Lighting.offScreenTiles;
      int index2 = centerY - Lighting.firstTileY + Lighting.offScreenTiles;
      if (index1 <= 0 || index2 <= 0 || (index1 >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 - 1 || index2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 - 1))
      {
        for (int index3 = 0; index3 < 4; ++index3)
          slices[index3] = Color.get_Black();
      }
      else
      {
        Lighting.LightingState lightingState1 = Lighting.states[index1][index2 - 1];
        Lighting.LightingState lightingState2 = Lighting.states[index1][index2 + 1];
        Lighting.LightingState lightingState3 = Lighting.states[index1 - 1][index2];
        Lighting.LightingState lightingState4 = Lighting.states[index1 + 1][index2];
        float num1 = lightingState1.r + lightingState1.g + lightingState1.b;
        float num2 = lightingState2.r + lightingState2.g + lightingState2.b;
        float num3 = lightingState4.r + lightingState4.g + lightingState4.b;
        float num4 = lightingState3.r + lightingState3.g + lightingState3.b;
        if ((double) num1 >= (double) num4)
        {
          int num5 = (int) ((double) byte.MaxValue * (double) lightingState3.r * (double) Lighting.brightness);
          int num6 = (int) ((double) byte.MaxValue * (double) lightingState3.g * (double) Lighting.brightness);
          int num7 = (int) ((double) byte.MaxValue * (double) lightingState3.b * (double) Lighting.brightness);
          if (num5 > (int) byte.MaxValue)
            num5 = (int) byte.MaxValue;
          if (num6 > (int) byte.MaxValue)
            num6 = (int) byte.MaxValue;
          if (num7 > (int) byte.MaxValue)
            num7 = (int) byte.MaxValue;
          slices[0] = new Color((int) (byte) num5, (int) (byte) num6, (int) (byte) num7, (int) byte.MaxValue);
        }
        else
        {
          int num5 = (int) ((double) byte.MaxValue * (double) lightingState1.r * (double) Lighting.brightness);
          int num6 = (int) ((double) byte.MaxValue * (double) lightingState1.g * (double) Lighting.brightness);
          int num7 = (int) ((double) byte.MaxValue * (double) lightingState1.b * (double) Lighting.brightness);
          if (num5 > (int) byte.MaxValue)
            num5 = (int) byte.MaxValue;
          if (num6 > (int) byte.MaxValue)
            num6 = (int) byte.MaxValue;
          if (num7 > (int) byte.MaxValue)
            num7 = (int) byte.MaxValue;
          slices[0] = new Color((int) (byte) num5, (int) (byte) num6, (int) (byte) num7, (int) byte.MaxValue);
        }
        if ((double) num1 >= (double) num3)
        {
          int num5 = (int) ((double) byte.MaxValue * (double) lightingState4.r * (double) Lighting.brightness);
          int num6 = (int) ((double) byte.MaxValue * (double) lightingState4.g * (double) Lighting.brightness);
          int num7 = (int) ((double) byte.MaxValue * (double) lightingState4.b * (double) Lighting.brightness);
          if (num5 > (int) byte.MaxValue)
            num5 = (int) byte.MaxValue;
          if (num6 > (int) byte.MaxValue)
            num6 = (int) byte.MaxValue;
          if (num7 > (int) byte.MaxValue)
            num7 = (int) byte.MaxValue;
          slices[1] = new Color((int) (byte) num5, (int) (byte) num6, (int) (byte) num7, (int) byte.MaxValue);
        }
        else
        {
          int num5 = (int) ((double) byte.MaxValue * (double) lightingState1.r * (double) Lighting.brightness);
          int num6 = (int) ((double) byte.MaxValue * (double) lightingState1.g * (double) Lighting.brightness);
          int num7 = (int) ((double) byte.MaxValue * (double) lightingState1.b * (double) Lighting.brightness);
          if (num5 > (int) byte.MaxValue)
            num5 = (int) byte.MaxValue;
          if (num6 > (int) byte.MaxValue)
            num6 = (int) byte.MaxValue;
          if (num7 > (int) byte.MaxValue)
            num7 = (int) byte.MaxValue;
          slices[1] = new Color((int) (byte) num5, (int) (byte) num6, (int) (byte) num7, (int) byte.MaxValue);
        }
        if ((double) num2 >= (double) num4)
        {
          int num5 = (int) ((double) byte.MaxValue * (double) lightingState3.r * (double) Lighting.brightness);
          int num6 = (int) ((double) byte.MaxValue * (double) lightingState3.g * (double) Lighting.brightness);
          int num7 = (int) ((double) byte.MaxValue * (double) lightingState3.b * (double) Lighting.brightness);
          if (num5 > (int) byte.MaxValue)
            num5 = (int) byte.MaxValue;
          if (num6 > (int) byte.MaxValue)
            num6 = (int) byte.MaxValue;
          if (num7 > (int) byte.MaxValue)
            num7 = (int) byte.MaxValue;
          slices[2] = new Color((int) (byte) num5, (int) (byte) num6, (int) (byte) num7, (int) byte.MaxValue);
        }
        else
        {
          int num5 = (int) ((double) byte.MaxValue * (double) lightingState2.r * (double) Lighting.brightness);
          int num6 = (int) ((double) byte.MaxValue * (double) lightingState2.g * (double) Lighting.brightness);
          int num7 = (int) ((double) byte.MaxValue * (double) lightingState2.b * (double) Lighting.brightness);
          if (num5 > (int) byte.MaxValue)
            num5 = (int) byte.MaxValue;
          if (num6 > (int) byte.MaxValue)
            num6 = (int) byte.MaxValue;
          if (num7 > (int) byte.MaxValue)
            num7 = (int) byte.MaxValue;
          slices[2] = new Color((int) (byte) num5, (int) (byte) num6, (int) (byte) num7, (int) byte.MaxValue);
        }
        if ((double) num2 >= (double) num3)
        {
          int num5 = (int) ((double) byte.MaxValue * (double) lightingState4.r * (double) Lighting.brightness);
          int num6 = (int) ((double) byte.MaxValue * (double) lightingState4.g * (double) Lighting.brightness);
          int num7 = (int) ((double) byte.MaxValue * (double) lightingState4.b * (double) Lighting.brightness);
          if (num5 > (int) byte.MaxValue)
            num5 = (int) byte.MaxValue;
          if (num6 > (int) byte.MaxValue)
            num6 = (int) byte.MaxValue;
          if (num7 > (int) byte.MaxValue)
            num7 = (int) byte.MaxValue;
          slices[3] = new Color((int) (byte) num5, (int) (byte) num6, (int) (byte) num7, (int) byte.MaxValue);
        }
        else
        {
          int num5 = (int) ((double) byte.MaxValue * (double) lightingState2.r * (double) Lighting.brightness);
          int num6 = (int) ((double) byte.MaxValue * (double) lightingState2.g * (double) Lighting.brightness);
          int num7 = (int) ((double) byte.MaxValue * (double) lightingState2.b * (double) Lighting.brightness);
          if (num5 > (int) byte.MaxValue)
            num5 = (int) byte.MaxValue;
          if (num6 > (int) byte.MaxValue)
            num6 = (int) byte.MaxValue;
          if (num7 > (int) byte.MaxValue)
            num7 = (int) byte.MaxValue;
          slices[3] = new Color((int) (byte) num5, (int) (byte) num6, (int) (byte) num7, (int) byte.MaxValue);
        }
      }
    }

    public static Color GetBlackness(int x, int y)
    {
      int index1 = x - Lighting.firstTileX + Lighting.offScreenTiles;
      int index2 = y - Lighting.firstTileY + Lighting.offScreenTiles;
      if (index1 < 0 || index2 < 0 || (index1 >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 || index2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10))
        return Color.get_Black();
      Color color;
      // ISSUE: explicit reference operation
      ((Color) @color).\u002Ector(0, 0, 0, (int) (byte) ((double) byte.MaxValue - (double) byte.MaxValue * (double) Lighting.states[index1][index2].r));
      return color;
    }

    public static float Brightness(int x, int y)
    {
      int index1 = x - Lighting.firstTileX + Lighting.offScreenTiles;
      int index2 = y - Lighting.firstTileY + Lighting.offScreenTiles;
      if (index1 < 0 || index2 < 0 || (index1 >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 || index2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10))
        return 0.0f;
      Lighting.LightingState lightingState = Lighting.states[index1][index2];
      return (float) ((double) Lighting.brightness * ((double) lightingState.r + (double) lightingState.g + (double) lightingState.b) / 3.0);
    }

    public static float BrightnessAverage(int x, int y, int width, int height)
    {
      int num1 = x - Lighting.firstTileX + Lighting.offScreenTiles;
      int num2 = y - Lighting.firstTileY + Lighting.offScreenTiles;
      int num3 = num1 + width;
      int num4 = num2 + height;
      if (num1 < 0)
        num1 = 0;
      if (num2 < 0)
        num2 = 0;
      if (num3 >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10)
        num3 = Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10;
      if (num4 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10)
        num4 = Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10;
      float num5 = 0.0f;
      float num6 = 0.0f;
      for (int index1 = num1; index1 < num3; ++index1)
      {
        for (int index2 = num2; index2 < num4; ++index2)
        {
          ++num5;
          Lighting.LightingState lightingState = Lighting.states[index1][index2];
          num6 += (float) (((double) lightingState.r + (double) lightingState.g + (double) lightingState.b) / 3.0);
        }
      }
      if ((double) num5 == 0.0)
        return 0.0f;
      return num6 / num5;
    }

    private class LightingSwipeData
    {
      public int outerLoopStart;
      public int outerLoopEnd;
      public int innerLoop1Start;
      public int innerLoop1End;
      public int innerLoop2Start;
      public int innerLoop2End;
      public UnifiedRandom rand;
      public Action<Lighting.LightingSwipeData> function;
      public Lighting.LightingState[][] jaggedArray;

      public LightingSwipeData()
      {
        this.innerLoop1Start = 0;
        this.outerLoopStart = 0;
        this.innerLoop1End = 0;
        this.outerLoopEnd = 0;
        this.innerLoop2Start = 0;
        this.innerLoop2End = 0;
        this.function = (Action<Lighting.LightingSwipeData>) null;
        this.rand = new UnifiedRandom();
      }

      public void CopyFrom(Lighting.LightingSwipeData from)
      {
        this.innerLoop1Start = from.innerLoop1Start;
        this.outerLoopStart = from.outerLoopStart;
        this.innerLoop1End = from.innerLoop1End;
        this.outerLoopEnd = from.outerLoopEnd;
        this.innerLoop2Start = from.innerLoop2Start;
        this.innerLoop2End = from.innerLoop2End;
        this.function = from.function;
        this.jaggedArray = from.jaggedArray;
      }
    }

    private class LightingState
    {
      public float r;
      public float r2;
      public float g;
      public float g2;
      public float b;
      public float b2;
      public bool stopLight;
      public bool wetLight;
      public bool honeyLight;

      public Vector3 ToVector3()
      {
        return new Vector3(this.r, this.g, this.b);
      }
    }

    private struct ColorTriplet
    {
      public float r;
      public float g;
      public float b;

      public ColorTriplet(float R, float G, float B)
      {
        this.r = R;
        this.g = G;
        this.b = B;
      }

      public ColorTriplet(float averageColor)
      {
        this.r = this.g = this.b = averageColor;
      }
    }
  }
}
