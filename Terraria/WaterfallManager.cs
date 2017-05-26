// Decompiled with JetBrains decompiler
// Type: Terraria.WaterfallManager
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace Terraria
{
  public class WaterfallManager
  {
    public Texture2D[] waterfallTexture = new Texture2D[23];
    private int waterfallDist = 100;
    private const int minWet = 160;
    private const int maxCount = 200;
    private const int maxLength = 100;
    private const int maxTypes = 23;
    private int qualityMax;
    private int currentMax;
    private WaterfallManager.WaterfallData[] waterfalls;
    private int wFallFrCounter;
    private int regularFrame;
    private int wFallFrCounter2;
    private int slowFrame;
    private int rainFrameCounter;
    private int rainFrameForeground;
    private int rainFrameBackground;
    private int snowFrameCounter;
    private int snowFrameForeground;
    private int findWaterfallCount;

    public WaterfallManager()
    {
      this.waterfalls = new WaterfallManager.WaterfallData[200];
    }

    public void LoadContent()
    {
      for (int index = 0; index < 23; ++index)
        this.waterfallTexture[index] = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Waterfall_" + (object) index);
    }

    public bool CheckForWaterfall(int i, int j)
    {
      for (int index = 0; index < this.currentMax; ++index)
      {
        if (this.waterfalls[index].x == i && this.waterfalls[index].y == j)
          return true;
      }
      return false;
    }

    public void FindWaterfalls(bool forced = false)
    {
      this.findWaterfallCount = this.findWaterfallCount + 1;
      if (this.findWaterfallCount < 30 && !forced)
        return;
      this.findWaterfallCount = 0;
      this.waterfallDist = (int) (75.0 * (double) Main.gfxQuality) + 25;
      this.qualityMax = (int) (175.0 * (double) Main.gfxQuality) + 25;
      this.currentMax = 0;
      int num1 = (int) (Main.screenPosition.X / 16.0 - 1.0);
      int num2 = (int) ((Main.screenPosition.X + (double) Main.screenWidth) / 16.0) + 2;
      int num3 = (int) (Main.screenPosition.Y / 16.0 - 1.0);
      int num4 = (int) ((Main.screenPosition.Y + (double) Main.screenHeight) / 16.0) + 2;
      int num5 = num1 - this.waterfallDist;
      int num6 = num2 + this.waterfallDist;
      int num7 = num3 - this.waterfallDist;
      int num8 = num4 + 20;
      if (num5 < 0)
        num5 = 0;
      if (num6 > Main.maxTilesX)
        num6 = Main.maxTilesX;
      if (num7 < 0)
        num7 = 0;
      if (num8 > Main.maxTilesY)
        num8 = Main.maxTilesY;
      for (int index1 = num5; index1 < num6; ++index1)
      {
        for (int index2 = num7; index2 < num8; ++index2)
        {
          Tile tile = Main.tile[index1, index2];
          if (tile == null)
          {
            tile = new Tile();
            Main.tile[index1, index2] = tile;
          }
          if (tile.active())
          {
            if (tile.halfBrick())
            {
              Tile testTile1 = Main.tile[index1, index2 - 1];
              if (testTile1 == null)
              {
                testTile1 = new Tile();
                Main.tile[index1, index2 - 1] = testTile1;
              }
              if ((int) testTile1.liquid < 16 || WorldGen.SolidTile(testTile1))
              {
                Tile testTile2 = Main.tile[index1 + 1, index2];
                if (testTile2 == null)
                {
                  testTile2 = new Tile();
                  Main.tile[index1 - 1, index2] = testTile2;
                }
                Tile testTile3 = Main.tile[index1 - 1, index2];
                if (testTile3 == null)
                {
                  testTile3 = new Tile();
                  Main.tile[index1 + 1, index2] = testTile3;
                }
                if (((int) testTile2.liquid > 160 || (int) testTile3.liquid > 160) && ((int) testTile2.liquid == 0 && !WorldGen.SolidTile(testTile2) && (int) testTile2.slope() == 0 || (int) testTile3.liquid == 0 && !WorldGen.SolidTile(testTile3) && (int) testTile3.slope() == 0) && this.currentMax < this.qualityMax)
                {
                  this.waterfalls[this.currentMax].type = 0;
                  this.waterfalls[this.currentMax].type = testTile1.lava() || testTile3.lava() || testTile2.lava() ? 1 : (testTile1.honey() || testTile3.honey() || testTile2.honey() ? 14 : 0);
                  this.waterfalls[this.currentMax].x = index1;
                  this.waterfalls[this.currentMax].y = index2;
                  this.currentMax = this.currentMax + 1;
                }
              }
            }
            if ((int) tile.type == 196)
            {
              Tile testTile = Main.tile[index1, index2 + 1];
              if (testTile == null)
              {
                testTile = new Tile();
                Main.tile[index1, index2 + 1] = testTile;
              }
              if (!WorldGen.SolidTile(testTile) && (int) testTile.slope() == 0 && this.currentMax < this.qualityMax)
              {
                this.waterfalls[this.currentMax].type = 11;
                this.waterfalls[this.currentMax].x = index1;
                this.waterfalls[this.currentMax].y = index2 + 1;
                this.currentMax = this.currentMax + 1;
              }
            }
            if ((int) tile.type == 460)
            {
              Tile testTile = Main.tile[index1, index2 + 1];
              if (testTile == null)
              {
                testTile = new Tile();
                Main.tile[index1, index2 + 1] = testTile;
              }
              if (!WorldGen.SolidTile(testTile) && (int) testTile.slope() == 0 && this.currentMax < this.qualityMax)
              {
                this.waterfalls[this.currentMax].type = 22;
                this.waterfalls[this.currentMax].x = index1;
                this.waterfalls[this.currentMax].y = index2 + 1;
                this.currentMax = this.currentMax + 1;
              }
            }
          }
        }
      }
    }

    public void UpdateFrame()
    {
      this.wFallFrCounter = this.wFallFrCounter + 1;
      if (this.wFallFrCounter > 2)
      {
        this.wFallFrCounter = 0;
        this.regularFrame = this.regularFrame + 1;
        if (this.regularFrame > 15)
          this.regularFrame = 0;
      }
      this.wFallFrCounter2 = this.wFallFrCounter2 + 1;
      if (this.wFallFrCounter2 > 6)
      {
        this.wFallFrCounter2 = 0;
        this.slowFrame = this.slowFrame + 1;
        if (this.slowFrame > 15)
          this.slowFrame = 0;
      }
      this.rainFrameCounter = this.rainFrameCounter + 1;
      if (this.rainFrameCounter > 0)
      {
        this.rainFrameForeground = this.rainFrameForeground + 1;
        if (this.rainFrameForeground > 7)
          this.rainFrameForeground = this.rainFrameForeground - 8;
        if (this.rainFrameCounter > 2)
        {
          this.rainFrameCounter = 0;
          this.rainFrameBackground = this.rainFrameBackground - 1;
          if (this.rainFrameBackground < 0)
            this.rainFrameBackground = 7;
        }
      }
      int num1 = this.snowFrameCounter + 1;
      this.snowFrameCounter = num1;
      if (num1 <= 3)
        return;
      this.snowFrameCounter = 0;
      int num2 = this.snowFrameForeground + 1;
      this.snowFrameForeground = num2;
      if (num2 <= 7)
        return;
      this.snowFrameForeground = 0;
    }

    private void DrawWaterfall(SpriteBatch spriteBatch, int Style = 0, float Alpha = 1f)
    {
      float num1 = 0.0f;
      float num2 = 99999f;
      float num3 = 99999f;
      int num4 = -1;
      int num5 = -1;
      float num6 = 0.0f;
      float num7 = 99999f;
      float num8 = 99999f;
      int num9 = -1;
      int num10 = -1;
      for (int index1 = 0; index1 < this.currentMax; ++index1)
      {
        int num11 = 0;
        int index2 = this.waterfalls[index1].type;
        int x = this.waterfalls[index1].x;
        int y = this.waterfalls[index1].y;
        int num12 = 0;
        int num13 = 0;
        int num14 = 0;
        int num15 = 0;
        int num16 = 0;
        int index3 = 0;
        int num17;
        if (index2 == 1 || index2 == 14)
        {
          if (!Main.drewLava && this.waterfalls[index1].stopAtStep != 0)
            num17 = 32 * this.slowFrame;
          else
            continue;
        }
        else
        {
          if (index2 == 11 || index2 == 22)
          {
            if (!Main.drewLava)
            {
              int num18 = this.waterfallDist / 4;
              if (index2 == 22)
                num18 = this.waterfallDist / 2;
              if (this.waterfalls[index1].stopAtStep > num18)
                this.waterfalls[index1].stopAtStep = num18;
              if (this.waterfalls[index1].stopAtStep != 0 && (double) (y + num18) >= Main.screenPosition.Y / 16.0 && ((double) x >= Main.screenPosition.X / 16.0 - 1.0 && (double) x <= (Main.screenPosition.X + (double) Main.screenWidth) / 16.0 + 1.0))
              {
                int num19;
                int num20;
                if (x % 2 == 0)
                {
                  num19 = this.rainFrameForeground + 3;
                  if (num19 > 7)
                    num19 -= 8;
                  num20 = this.rainFrameBackground + 2;
                  if (num20 > 7)
                    num20 -= 8;
                  if (index2 == 22)
                  {
                    num19 = this.snowFrameForeground + 3;
                    if (num19 > 7)
                      num19 -= 8;
                  }
                }
                else
                {
                  num19 = this.rainFrameForeground;
                  num20 = this.rainFrameBackground;
                  if (index2 == 22)
                    num19 = this.snowFrameForeground;
                }
                Rectangle rectangle1;
                // ISSUE: explicit reference operation
                ((Rectangle) @rectangle1).\u002Ector(num20 * 18, 0, 16, 16);
                Rectangle rectangle2;
                // ISSUE: explicit reference operation
                ((Rectangle) @rectangle2).\u002Ector(num19 * 18, 0, 16, 16);
                Vector2 vector2_1;
                // ISSUE: explicit reference operation
                ((Vector2) @vector2_1).\u002Ector(8f, 8f);
                Vector2 vector2_2 = y % 2 != 0 ? Vector2.op_Subtraction(new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)), Main.screenPosition) : Vector2.op_Subtraction(new Vector2((float) (x * 16 + 9), (float) (y * 16 + 8)), Main.screenPosition);
                bool flag = false;
                for (int index4 = 0; index4 < num18; ++index4)
                {
                  Color color1 = Lighting.GetColor(x, y);
                  float num21 = 0.6f;
                  float num22 = 0.3f;
                  if (index4 > num18 - 8)
                  {
                    float num23 = (float) (num18 - index4) / 8f;
                    num21 *= num23;
                    num22 *= num23;
                  }
                  double num24 = (double) num21;
                  Color color2 = Color.op_Multiply(color1, (float) num24);
                  double num25 = (double) num22;
                  Color color3 = Color.op_Multiply(color1, (float) num25);
                  if (index2 == 22)
                  {
                    spriteBatch.Draw(this.waterfallTexture[22], vector2_2, new Rectangle?(rectangle2), color2, 0.0f, vector2_1, 1f, (SpriteEffects) 0, 0.0f);
                  }
                  else
                  {
                    spriteBatch.Draw(this.waterfallTexture[12], vector2_2, new Rectangle?(rectangle1), color3, 0.0f, vector2_1, 1f, (SpriteEffects) 0, 0.0f);
                    spriteBatch.Draw(this.waterfallTexture[11], vector2_2, new Rectangle?(rectangle2), color2, 0.0f, vector2_1, 1f, (SpriteEffects) 0, 0.0f);
                  }
                  if (!flag)
                  {
                    ++y;
                    Tile testTile = Main.tile[x, y];
                    if (WorldGen.SolidTile(testTile))
                      flag = true;
                    if ((int) testTile.liquid > 0)
                    {
                      int num23 = (int) (16.0 * ((double) testTile.liquid / (double) byte.MaxValue)) & 254;
                      if (num23 < 15)
                      {
                        // ISSUE: explicit reference operation
                        // ISSUE: variable of a reference type
                        __Null& local1 = @rectangle2.Height;
                        // ISSUE: cast to a reference type
                        // ISSUE: explicit reference operation
                        int num26 = ^(int&) local1 - num23;
                        // ISSUE: cast to a reference type
                        // ISSUE: explicit reference operation
                        ^(int&) local1 = num26;
                        // ISSUE: explicit reference operation
                        // ISSUE: variable of a reference type
                        __Null& local2 = @rectangle1.Height;
                        // ISSUE: cast to a reference type
                        // ISSUE: explicit reference operation
                        int num27 = ^(int&) local2 - num23;
                        // ISSUE: cast to a reference type
                        // ISSUE: explicit reference operation
                        ^(int&) local2 = num27;
                      }
                      else
                        break;
                    }
                    if (y % 2 == 0)
                    {
                      // ISSUE: explicit reference operation
                      // ISSUE: variable of a reference type
                      __Null& local = @vector2_2.X;
                      // ISSUE: cast to a reference type
                      // ISSUE: explicit reference operation
                      double num23 = (double) ^(float&) local + 1.0;
                      // ISSUE: cast to a reference type
                      // ISSUE: explicit reference operation
                      ^(float&) local = (float) num23;
                    }
                    else
                    {
                      // ISSUE: explicit reference operation
                      // ISSUE: variable of a reference type
                      __Null& local = @vector2_2.X;
                      // ISSUE: cast to a reference type
                      // ISSUE: explicit reference operation
                      double num23 = (double) ^(float&) local - 1.0;
                      // ISSUE: cast to a reference type
                      // ISSUE: explicit reference operation
                      ^(float&) local = (float) num23;
                    }
                    // ISSUE: explicit reference operation
                    // ISSUE: variable of a reference type
                    __Null& local3 = @vector2_2.Y;
                    // ISSUE: cast to a reference type
                    // ISSUE: explicit reference operation
                    double num28 = (double) ^(float&) local3 + 16.0;
                    // ISSUE: cast to a reference type
                    // ISSUE: explicit reference operation
                    ^(float&) local3 = (float) num28;
                  }
                  else
                    break;
                }
                this.waterfalls[index1].stopAtStep = 0;
                continue;
              }
              continue;
            }
            continue;
          }
          if (index2 == 0)
            index2 = Style;
          else if (index2 == 2 && Main.drewLava)
            continue;
          num17 = 32 * this.regularFrame;
        }
        int num29 = 0;
        int num30 = this.waterfallDist;
        Color color4 = Color.get_White();
        for (int index4 = 0; index4 < num30; ++index4)
        {
          if (num29 < 2)
          {
            switch (index2)
            {
              case 1:
                double num18;
                float R1 = (float) (num18 = (0.550000011920929 + (double) (270 - (int) Main.mouseTextColor) / 900.0) * 0.400000005960464);
                double num19 = 0.300000011920929;
                float G1 = (float) (num18 * num19);
                double num20 = 0.100000001490116;
                float B1 = (float) (num18 * num20);
                Lighting.AddLight(x, y, R1, G1, B1);
                break;
              case 2:
                float num21 = (float) Main.DiscoR / (float) byte.MaxValue;
                float num22 = (float) Main.DiscoG / (float) byte.MaxValue;
                float num23 = (float) Main.DiscoB / (float) byte.MaxValue;
                float R2 = num21 * 0.2f;
                float G2 = num22 * 0.2f;
                float B2 = num23 * 0.2f;
                Lighting.AddLight(x, y, R2, G2, B2);
                break;
              case 15:
                float R3 = 0.0f;
                float G3 = 0.0f;
                float B3 = 0.2f;
                Lighting.AddLight(x, y, R3, G3, B3);
                break;
              case 16:
                float R4 = 0.0f;
                float G4 = 0.2f;
                float B4 = 0.0f;
                Lighting.AddLight(x, y, R4, G4, B4);
                break;
              case 17:
                float R5 = 0.0f;
                float G5 = 0.0f;
                float B5 = 0.2f;
                Lighting.AddLight(x, y, R5, G5, B5);
                break;
              case 18:
                float R6 = 0.0f;
                float G6 = 0.2f;
                float B6 = 0.0f;
                Lighting.AddLight(x, y, R6, G6, B6);
                break;
              case 19:
                float R7 = 0.2f;
                float G7 = 0.0f;
                float B7 = 0.0f;
                Lighting.AddLight(x, y, R7, G7, B7);
                break;
              case 20:
                Lighting.AddLight(x, y, 0.2f, 0.2f, 0.2f);
                break;
              case 21:
                float R8 = 0.2f;
                float G8 = 0.0f;
                float B8 = 0.0f;
                Lighting.AddLight(x, y, R8, G8, B8);
                break;
            }
            Tile tile = Main.tile[x, y];
            if (tile == null)
            {
              tile = new Tile();
              Main.tile[x, y] = tile;
            }
            Tile testTile1 = Main.tile[x - 1, y];
            if (testTile1 == null)
            {
              testTile1 = new Tile();
              Main.tile[x - 1, y] = testTile1;
            }
            Tile testTile2 = Main.tile[x, y + 1];
            if (testTile2 == null)
            {
              testTile2 = new Tile();
              Main.tile[x, y + 1] = testTile2;
            }
            Tile testTile3 = Main.tile[x + 1, y];
            if (testTile3 == null)
            {
              testTile3 = new Tile();
              Main.tile[x + 1, y] = testTile3;
            }
            int num24 = (int) tile.liquid / 16;
            int num25 = 0;
            int num26 = num15;
            int num27;
            int num28;
            if (testTile2.topSlope())
            {
              if ((int) testTile2.slope() == 1)
              {
                num25 = 1;
                num27 = 1;
                num14 = 1;
                num15 = num14;
              }
              else
              {
                num25 = -1;
                num27 = -1;
                num14 = -1;
                num15 = num14;
              }
              num28 = 1;
            }
            else if ((!WorldGen.SolidTile(testTile2) && !testTile2.bottomSlope() || (int) testTile2.type == 162) && !tile.halfBrick() || !testTile2.active() && !tile.halfBrick())
            {
              num29 = 0;
              num28 = 1;
              num27 = 0;
            }
            else if ((WorldGen.SolidTile(testTile1) || testTile1.topSlope() || (int) testTile1.liquid > 0) && (!WorldGen.SolidTile(testTile3) && (int) testTile3.liquid == 0))
            {
              if (num14 == -1)
                ++num29;
              num27 = 1;
              num28 = 0;
              num14 = 1;
            }
            else if ((WorldGen.SolidTile(testTile3) || testTile3.topSlope() || (int) testTile3.liquid > 0) && (!WorldGen.SolidTile(testTile1) && (int) testTile1.liquid == 0))
            {
              if (num14 == 1)
                ++num29;
              num27 = -1;
              num28 = 0;
              num14 = -1;
            }
            else if ((!WorldGen.SolidTile(testTile3) && !tile.topSlope() || (int) testTile3.liquid == 0) && (!WorldGen.SolidTile(testTile1) && !tile.topSlope() && (int) testTile1.liquid == 0))
            {
              num28 = 0;
              num27 = num14;
            }
            else
            {
              ++num29;
              num28 = 0;
              num27 = 0;
            }
            if (num29 >= 2)
            {
              num14 *= -1;
              num27 *= -1;
            }
            int num31 = -1;
            if (index2 != 1 && index2 != 14)
            {
              if (testTile2.active())
                num31 = (int) testTile2.type;
              if (tile.active())
                num31 = (int) tile.type;
            }
            if (num31 != -1)
            {
              if (num31 == 160)
                index2 = 2;
              else if (num31 >= 262 && num31 <= 268)
                index2 = 15 + num31 - 262;
            }
            if (WorldGen.SolidTile(testTile2) && !tile.halfBrick())
              num11 = 8;
            else if (num13 != 0)
              num11 = 0;
            Color color1 = Lighting.GetColor(x, y);
            Color color2 = color1;
            float num32 = index2 != 1 ? (index2 != 14 ? ((int) tile.wall != 0 || (double) y >= Main.worldSurface ? 0.6f * Alpha : Alpha) : 0.8f) : 1f;
            if (index4 > num30 - 10)
              num32 *= (float) (num30 - index4) / 10f;
            // ISSUE: explicit reference operation
            float num33 = (float) ((Color) @color1).get_R() * num32;
            // ISSUE: explicit reference operation
            float num34 = (float) ((Color) @color1).get_G() * num32;
            // ISSUE: explicit reference operation
            float num35 = (float) ((Color) @color1).get_B() * num32;
            // ISSUE: explicit reference operation
            float num36 = (float) ((Color) @color1).get_A() * num32;
            if (index2 == 1)
            {
              if ((double) num33 < 190.0 * (double) num32)
                num33 = 190f * num32;
              if ((double) num34 < 190.0 * (double) num32)
                num34 = 190f * num32;
              if ((double) num35 < 190.0 * (double) num32)
                num35 = 190f * num32;
            }
            else if (index2 == 2)
            {
              num33 = (float) Main.DiscoR * num32;
              num34 = (float) Main.DiscoG * num32;
              num35 = (float) Main.DiscoB * num32;
            }
            else if (index2 >= 15 && index2 <= 21)
            {
              num33 = (float) byte.MaxValue * num32;
              num34 = (float) byte.MaxValue * num32;
              num35 = (float) byte.MaxValue * num32;
            }
            // ISSUE: explicit reference operation
            ((Color) @color1).\u002Ector((int) num33, (int) num34, (int) num35, (int) num36);
            if (index2 == 1)
            {
              float num37 = Math.Abs((float) (x * 16 + 8) - ((float) Main.screenPosition.X + (float) (Main.screenWidth / 2)));
              float num38 = Math.Abs((float) (y * 16 + 8) - ((float) Main.screenPosition.Y + (float) (Main.screenHeight / 2)));
              if ((double) num37 < (double) (Main.screenWidth * 2) && (double) num38 < (double) (Main.screenHeight * 2))
              {
                float num39 = (float) (1.0 - Math.Sqrt((double) num37 * (double) num37 + (double) num38 * (double) num38) / ((double) Main.screenWidth * 0.75));
                if ((double) num39 > 0.0)
                  num6 += num39;
              }
              if ((double) num37 < (double) num7)
              {
                num7 = num37;
                num9 = x * 16 + 8;
              }
              if ((double) num38 < (double) num8)
              {
                num8 = num37;
                num10 = y * 16 + 8;
              }
            }
            else if (index2 != 1 && index2 != 14 && (index2 != 11 && index2 != 12) && index2 != 22)
            {
              float num37 = Math.Abs((float) (x * 16 + 8) - ((float) Main.screenPosition.X + (float) (Main.screenWidth / 2)));
              float num38 = Math.Abs((float) (y * 16 + 8) - ((float) Main.screenPosition.Y + (float) (Main.screenHeight / 2)));
              if ((double) num37 < (double) (Main.screenWidth * 2) && (double) num38 < (double) (Main.screenHeight * 2))
              {
                float num39 = (float) (1.0 - Math.Sqrt((double) num37 * (double) num37 + (double) num38 * (double) num38) / ((double) Main.screenWidth * 0.75));
                if ((double) num39 > 0.0)
                  num1 += num39;
              }
              if ((double) num37 < (double) num2)
              {
                num2 = num37;
                num4 = x * 16 + 8;
              }
              if ((double) num38 < (double) num3)
              {
                num3 = num37;
                num5 = y * 16 + 8;
              }
            }
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            if (index4 > 50 && ((int) ((Color) @color2).get_R() > 20 || (int) ((Color) @color2).get_B() > 20 || (int) ((Color) @color2).get_G() > 20))
            {
              // ISSUE: explicit reference operation
              float num37 = (float) ((Color) @color2).get_R();
              // ISSUE: explicit reference operation
              if ((double) ((Color) @color2).get_G() > (double) num37)
              {
                // ISSUE: explicit reference operation
                num37 = (float) ((Color) @color2).get_G();
              }
              // ISSUE: explicit reference operation
              if ((double) ((Color) @color2).get_B() > (double) num37)
              {
                // ISSUE: explicit reference operation
                num37 = (float) ((Color) @color2).get_B();
              }
              if ((double) Main.rand.Next(20000) < (double) num37 / 30.0)
              {
                int index5 = Dust.NewDust(new Vector2((float) (x * 16 - num14 * 7), (float) (y * 16 + 6)), 10, 8, 43, 0.0f, 0.0f, 254, Color.get_White(), 0.5f);
                Dust dust = Main.dust[index5];
                Vector2 vector2 = Vector2.op_Multiply(dust.velocity, 0.0f);
                dust.velocity = vector2;
              }
            }
            if (num12 == 0 && num25 != 0 && (num13 == 1 && num14 != num15))
            {
              num25 = 0;
              num14 = num15;
              color1 = Color.get_White();
              if (num14 == 1)
                spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16 - 16), (float) (y * 16 + 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 16 - num24)), color1, 0.0f, Vector2.get_Zero(), 1f, (SpriteEffects) 1, 0.0f);
              else
                spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16 - 16), (float) (y * 16 + 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 8)), color1, 0.0f, Vector2.get_Zero(), 1f, (SpriteEffects) 1, 0.0f);
            }
            if (num16 != 0 && num27 == 0 && num28 == 1)
            {
              if (num14 == 1)
              {
                if (index3 != index2)
                  spriteBatch.Draw(this.waterfallTexture[index3], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16 + num11 + 8)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 0, 16, 16 - num24 - 8)), color4, 0.0f, Vector2.get_Zero(), 1f, (SpriteEffects) 1, 0.0f);
                else
                  spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16 + num11 + 8)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 0, 16, 16 - num24 - 8)), color1, 0.0f, Vector2.get_Zero(), 1f, (SpriteEffects) 1, 0.0f);
              }
              else
                spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16 + num11 + 8)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 0, 16, 16 - num24 - 8)), color1, 0.0f, Vector2.get_Zero(), 1f, (SpriteEffects) 0, 0.0f);
            }
            if (num11 == 8 && num13 == 1 && num16 == 0)
            {
              if (num15 == -1)
              {
                if (index3 != index2)
                  spriteBatch.Draw(this.waterfallTexture[index3], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 8)), color4, 0.0f, (Vector2) null, 1f, (SpriteEffects) 0, 0.0f);
                else
                  spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 8)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 0, 0.0f);
              }
              else if (index3 != index2)
                spriteBatch.Draw(this.waterfallTexture[index3], Vector2.op_Subtraction(new Vector2((float) (x * 16 - 16), (float) (y * 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 8)), color4, 0.0f, (Vector2) null, 1f, (SpriteEffects) 1, 0.0f);
              else
                spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16 - 16), (float) (y * 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 8)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 1, 0.0f);
            }
            if (num25 != 0 && num12 == 0)
            {
              if (num26 == 1)
              {
                if (index3 != index2)
                  spriteBatch.Draw(this.waterfallTexture[index3], Vector2.op_Subtraction(new Vector2((float) (x * 16 - 16), (float) (y * 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 16 - num24)), color4, 0.0f, (Vector2) null, 1f, (SpriteEffects) 1, 0.0f);
                else
                  spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16 - 16), (float) (y * 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 16 - num24)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 1, 0.0f);
              }
              else if (index3 != index2)
                spriteBatch.Draw(this.waterfallTexture[index3], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 16 - num24)), color4, 0.0f, (Vector2) null, 1f, (SpriteEffects) 0, 0.0f);
              else
                spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 16 - num24)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 0, 0.0f);
            }
            if (num28 == 1 && num25 == 0 && num16 == 0)
            {
              if (num14 == -1)
              {
                if (num13 == 0)
                  spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16 + num11)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 0, 16, 16 - num24)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 0, 0.0f);
                else if (index3 != index2)
                  spriteBatch.Draw(this.waterfallTexture[index3], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 16 - num24)), color4, 0.0f, (Vector2) null, 1f, (SpriteEffects) 0, 0.0f);
                else
                  spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 16 - num24)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 0, 0.0f);
              }
              else if (num13 == 0)
                spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16 + num11)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 0, 16, 16 - num24)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 1, 0.0f);
              else if (index3 != index2)
                spriteBatch.Draw(this.waterfallTexture[index3], Vector2.op_Subtraction(new Vector2((float) (x * 16 - 16), (float) (y * 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 16 - num24)), color4, 0.0f, (Vector2) null, 1f, (SpriteEffects) 1, 0.0f);
              else
                spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16 - 16), (float) (y * 16)), Main.screenPosition), new Rectangle?(new Rectangle(num17, 24, 32, 16 - num24)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 1, 0.0f);
            }
            else if (num27 == 1)
            {
              if ((int) Main.tile[x, y].liquid <= 0 || Main.tile[x, y].halfBrick())
              {
                if (num25 == 1)
                {
                  for (int index5 = 0; index5 < 8; ++index5)
                  {
                    int num37 = index5 * 2;
                    int num38 = 14 - index5 * 2;
                    int num39 = num37;
                    num11 = 8;
                    if (num12 == 0 && index5 < 2)
                      num39 = 4;
                    spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16 + num37), (float) (y * 16 + num11 + num39)), Main.screenPosition), new Rectangle?(new Rectangle(16 + num17 + num38, 0, 2, 16 - num11)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 1, 0.0f);
                  }
                }
                else
                  spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16 + num11)), Main.screenPosition), new Rectangle?(new Rectangle(16 + num17, 0, 16, 16)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 1, 0.0f);
              }
            }
            else if (num27 == -1)
            {
              if ((int) Main.tile[x, y].liquid <= 0 || Main.tile[x, y].halfBrick())
              {
                if (num25 == -1)
                {
                  for (int index5 = 0; index5 < 8; ++index5)
                  {
                    int num37 = index5 * 2;
                    int num38 = index5 * 2;
                    int num39 = 14 - index5 * 2;
                    num11 = 8;
                    if (num12 == 0 && index5 > 5)
                      num39 = 4;
                    spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16 + num37), (float) (y * 16 + num11 + num39)), Main.screenPosition), new Rectangle?(new Rectangle(16 + num17 + num38, 0, 2, 16 - num11)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 1, 0.0f);
                  }
                }
                else
                  spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16 + num11)), Main.screenPosition), new Rectangle?(new Rectangle(16 + num17, 0, 16, 16)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 0, 0.0f);
              }
            }
            else if (num27 == 0 && num28 == 0)
            {
              if ((int) Main.tile[x, y].liquid <= 0 || Main.tile[x, y].halfBrick())
                spriteBatch.Draw(this.waterfallTexture[index2], Vector2.op_Subtraction(new Vector2((float) (x * 16), (float) (y * 16 + num11)), Main.screenPosition), new Rectangle?(new Rectangle(16 + num17, 0, 16, 16)), color1, 0.0f, (Vector2) null, 1f, (SpriteEffects) 0, 0.0f);
              index4 = 1000;
            }
            if ((int) tile.liquid > 0 && !tile.halfBrick())
              index4 = 1000;
            num13 = num28;
            num15 = num14;
            num12 = num27;
            x += num27;
            y += num28;
            num16 = num25;
            color4 = color1;
            if (index3 != index2)
              index3 = index2;
            if (testTile1.active() && ((int) testTile1.type == 189 || (int) testTile1.type == 196) || testTile3.active() && ((int) testTile3.type == 189 || (int) testTile3.type == 196) || testTile2.active() && ((int) testTile2.type == 189 || (int) testTile2.type == 196))
              num30 = (int) ((double) (40 * (Main.maxTilesX / 4200)) * (double) Main.gfxQuality);
          }
        }
      }
      Main.ambientWaterfallX = (float) num4;
      Main.ambientWaterfallY = (float) num5;
      Main.ambientWaterfallStrength = num1;
      Main.ambientLavafallX = (float) num9;
      Main.ambientLavafallY = (float) num10;
      Main.ambientLavafallStrength = num6;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      for (int index = 0; index < this.currentMax; ++index)
        this.waterfalls[index].stopAtStep = this.waterfallDist;
      Main.drewLava = false;
      if ((double) Main.liquidAlpha[0] > 0.0)
        this.DrawWaterfall(spriteBatch, 0, Main.liquidAlpha[0]);
      if ((double) Main.liquidAlpha[2] > 0.0)
        this.DrawWaterfall(spriteBatch, 3, Main.liquidAlpha[2]);
      if ((double) Main.liquidAlpha[3] > 0.0)
        this.DrawWaterfall(spriteBatch, 4, Main.liquidAlpha[3]);
      if ((double) Main.liquidAlpha[4] > 0.0)
        this.DrawWaterfall(spriteBatch, 5, Main.liquidAlpha[4]);
      if ((double) Main.liquidAlpha[5] > 0.0)
        this.DrawWaterfall(spriteBatch, 6, Main.liquidAlpha[5]);
      if ((double) Main.liquidAlpha[6] > 0.0)
        this.DrawWaterfall(spriteBatch, 7, Main.liquidAlpha[6]);
      if ((double) Main.liquidAlpha[7] > 0.0)
        this.DrawWaterfall(spriteBatch, 8, Main.liquidAlpha[7]);
      if ((double) Main.liquidAlpha[8] > 0.0)
        this.DrawWaterfall(spriteBatch, 9, Main.liquidAlpha[8]);
      if ((double) Main.liquidAlpha[9] > 0.0)
        this.DrawWaterfall(spriteBatch, 10, Main.liquidAlpha[9]);
      if ((double) Main.liquidAlpha[10] <= 0.0)
        return;
      this.DrawWaterfall(spriteBatch, 13, Main.liquidAlpha[10]);
    }

    public struct WaterfallData
    {
      public int x;
      public int y;
      public int type;
      public int stopAtStep;
    }
  }
}
