// Decompiled with JetBrains decompiler
// Type: Terraria.Cloud
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;

namespace Terraria
{
  public class Cloud
  {
    private static UnifiedRandom rand = new UnifiedRandom();
    public Vector2 position;
    public float scale;
    public float rotation;
    public float rSpeed;
    public float sSpeed;
    public bool active;
    public SpriteEffects spriteDir;
    public int type;
    public int width;
    public int height;
    public float Alpha;
    public bool kill;

    public static void resetClouds()
    {
      if (Main.dedServ || Main.cloudLimit < 10)
        return;
      Main.windSpeed = Main.windSpeedSet;
      for (int index = 0; index < 200; ++index)
        Main.cloud[index].active = false;
      for (int index = 0; index < Main.numClouds; ++index)
      {
        Cloud.addCloud();
        Main.cloud[index].Alpha = 1f;
      }
      for (int index = 0; index < 200; ++index)
        Main.cloud[index].Alpha = 1f;
    }

    public static void addCloud()
    {
      if (Main.netMode == 2)
        return;
      int index1 = -1;
      for (int index2 = 0; index2 < 200; ++index2)
      {
        if (!Main.cloud[index2].active)
        {
          index1 = index2;
          break;
        }
      }
      if (index1 < 0)
        return;
      Main.cloud[index1].kill = false;
      Main.cloud[index1].rSpeed = 0.0f;
      Main.cloud[index1].sSpeed = 0.0f;
      Main.cloud[index1].scale = (float) Cloud.rand.Next(70, 131) * 0.01f;
      Main.cloud[index1].rotation = (float) Cloud.rand.Next(-10, 11) * 0.01f;
      Main.cloud[index1].width = (int) ((double) Main.cloudTexture[Main.cloud[index1].type].get_Width() * (double) Main.cloud[index1].scale);
      Main.cloud[index1].height = (int) ((double) Main.cloudTexture[Main.cloud[index1].type].get_Height() * (double) Main.cloud[index1].scale);
      Main.cloud[index1].Alpha = 0.0f;
      Main.cloud[index1].spriteDir = (SpriteEffects) 0;
      if (Cloud.rand.Next(2) == 0)
        Main.cloud[index1].spriteDir = (SpriteEffects) 1;
      float num1 = Main.windSpeed;
      if (!Main.gameMenu)
        num1 = Main.windSpeed - (float) (Main.player[Main.myPlayer].velocity.X * 0.100000001490116);
      int num2 = 0;
      int num3 = 0;
      if ((double) num1 > 0.0)
        num2 -= 200;
      if ((double) num1 < 0.0)
        num3 += 200;
      int num4 = 300;
      float num5 = (float) WorldGen.genRand.Next(num2 - num4, Main.screenWidth + num3 + num4);
      Main.cloud[index1].Alpha = 0.0f;
      Main.cloud[index1].position.Y = (__Null) (double) Cloud.rand.Next((int) ((double) -Main.screenHeight * 0.25), (int) ((double) Main.screenHeight * 0.25));
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local1 = @Main.cloud[index1].position.Y;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num6 = (double) ^(float&) local1 - (double) Cloud.rand.Next((int) ((double) Main.screenHeight * 0.150000005960464));
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local1 = (float) num6;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local2 = @Main.cloud[index1].position.Y;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num7 = (double) ^(float&) local2 - (double) Cloud.rand.Next((int) ((double) Main.screenHeight * 0.150000005960464));
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local2 = (float) num7;
      Main.cloud[index1].type = Cloud.rand.Next(4);
      if ((double) Main.cloudAlpha > 0.0 && Cloud.rand.Next(4) != 0 || (double) Main.cloudBGActive >= 1.0 && Cloud.rand.Next(2) == 0)
      {
        Main.cloud[index1].type = Cloud.rand.Next(18, 22);
        if ((double) Main.cloud[index1].scale >= 1.15)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __Null& local3 = @Main.cloud[index1].position.Y;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          double num8 = (double) ^(float&) local3 - 150.0;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) local3 = (float) num8;
        }
        if ((double) Main.cloud[index1].scale >= 1.0)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __Null& local3 = @Main.cloud[index1].position.Y;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          double num8 = (double) ^(float&) local3 - 150.0;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) local3 = (float) num8;
        }
      }
      else if (((double) Main.cloudBGActive <= 0.0 && (double) Main.cloudAlpha == 0.0 && ((double) Main.cloud[index1].scale < 1.0 && Main.cloud[index1].position.Y < (double) -Main.screenHeight * 0.200000002980232) || Main.cloud[index1].position.Y < (double) -Main.screenHeight * 0.200000002980232) && (double) Main.numClouds < 50.0)
        Main.cloud[index1].type = Cloud.rand.Next(9, 14);
      else if (((double) Main.cloud[index1].scale < 1.15 && Main.cloud[index1].position.Y < (double) -Main.screenHeight * 0.300000011920929 || (double) Main.cloud[index1].scale < 0.85 && Main.cloud[index1].position.Y < (double) Main.screenHeight * 0.150000005960464) && ((double) Main.numClouds > 70.0 || (double) Main.cloudBGActive >= 1.0))
        Main.cloud[index1].type = Cloud.rand.Next(4, 9);
      else if (Main.cloud[index1].position.Y > (double) -Main.screenHeight * 0.150000005960464 && Cloud.rand.Next(2) == 0 && (double) Main.numClouds > 20.0)
        Main.cloud[index1].type = Cloud.rand.Next(14, 18);
      if ((double) Main.cloud[index1].scale > 1.2)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local3 = @Main.cloud[index1].position.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num8 = (double) ^(float&) local3 + 100.0;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local3 = (float) num8;
      }
      if ((double) Main.cloud[index1].scale > 1.3)
        Main.cloud[index1].scale = 1.3f;
      if ((double) Main.cloud[index1].scale < 0.7)
        Main.cloud[index1].scale = 0.7f;
      Main.cloud[index1].active = true;
      Main.cloud[index1].position.X = (__Null) (double) num5;
      if (Main.cloud[index1].position.X > (double) (Main.screenWidth + 100))
        Main.cloud[index1].Alpha = 1f;
      if (Main.cloud[index1].position.X + (double) Main.cloudTexture[Main.cloud[index1].type].get_Width() * (double) Main.cloud[index1].scale < -100.0)
        Main.cloud[index1].Alpha = 1f;
      Rectangle rectangle1;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle1).\u002Ector((int) Main.cloud[index1].position.X, (int) Main.cloud[index1].position.Y, Main.cloud[index1].width, Main.cloud[index1].height);
      for (int index2 = 0; index2 < 200; ++index2)
      {
        if (index1 != index2 && Main.cloud[index2].active)
        {
          Rectangle rectangle2;
          // ISSUE: explicit reference operation
          ((Rectangle) @rectangle2).\u002Ector((int) Main.cloud[index2].position.X, (int) Main.cloud[index2].position.Y, Main.cloud[index2].width, Main.cloud[index2].height);
          // ISSUE: explicit reference operation
          if (((Rectangle) @rectangle1).Intersects(rectangle2))
            Main.cloud[index1].active = false;
        }
      }
    }

    public Color cloudColor(Color bgColor)
    {
      float num = this.scale * this.Alpha;
      if ((double) num > 1.0)
        num = 1f;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      return new Color((int) (byte) (float) (int) ((double) ((Color) @bgColor).get_R() * (double) num), (int) (byte) (float) (int) ((double) ((Color) @bgColor).get_G() * (double) num), (int) (byte) (float) (int) ((double) ((Color) @bgColor).get_B() * (double) num), (int) (byte) (float) (int) ((double) ((Color) @bgColor).get_A() * (double) num));
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    public static void UpdateClouds()
    {
      if (Main.netMode == 2)
        return;
      int maxValue = 0;
      for (int index = 0; index < 200; ++index)
      {
        if (Main.cloud[index].active)
        {
          Main.cloud[index].Update();
          if (!Main.cloud[index].kill)
            ++maxValue;
        }
      }
      for (int index = 0; index < 200; ++index)
      {
        if (Main.cloud[index].active)
        {
          if (index > 1 && (!Main.cloud[index - 1].active || (double) Main.cloud[index - 1].scale > (double) Main.cloud[index].scale + 0.02))
          {
            Cloud cloud = (Cloud) Main.cloud[index - 1].Clone();
            Main.cloud[index - 1] = (Cloud) Main.cloud[index].Clone();
            Main.cloud[index] = cloud;
          }
          if (index < 199 && (!Main.cloud[index].active || (double) Main.cloud[index + 1].scale < (double) Main.cloud[index].scale - 0.02))
          {
            Cloud cloud = (Cloud) Main.cloud[index + 1].Clone();
            Main.cloud[index + 1] = (Cloud) Main.cloud[index].Clone();
            Main.cloud[index] = cloud;
          }
        }
      }
      if (maxValue < Main.numClouds)
      {
        Cloud.addCloud();
      }
      else
      {
        if (maxValue <= Main.numClouds)
          return;
        int index1 = Cloud.rand.Next(maxValue);
        for (int index2 = 0; Main.cloud[index1].kill && index2 < 100; index1 = Cloud.rand.Next(maxValue))
          ++index2;
        Main.cloud[index1].kill = true;
      }
    }

    public void Update()
    {
      if (Main.gameMenu)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local = @this.position.X;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num = (double) ^(float&) local + (double) Main.windSpeed * (double) this.scale * 3.0;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local = (float) num;
      }
      else
      {
        if ((double) this.scale == 1.0)
          this.scale = this.scale - 0.0001f;
        if ((double) this.scale == 1.15)
          this.scale = this.scale - 0.0001f;
        float num1;
        if ((double) this.scale < 1.0)
        {
          float num2 = 0.07f;
          float num3 = (float) (((double) (this.scale + 0.15f) + 1.0) / 2.0);
          float num4 = num3 * num3;
          num1 = num2 * num4;
        }
        else if ((double) this.scale <= 1.15)
        {
          float num2 = 0.19f;
          float num3 = this.scale - 0.075f;
          float num4 = num3 * num3;
          num1 = num2 * num4;
        }
        else
        {
          float num2 = 0.23f;
          float num3 = (float) ((double) this.scale - 0.150000005960464 - 0.0750000029802322);
          float num4 = num3 * num3;
          num1 = num2 * num4;
        }
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local1 = @this.position.X;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num5 = (double) ^(float&) local1 + (double) Main.windSpeed * (double) num1 * 5.0 * (double) Main.dayRate;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local1 = (float) num5;
        float num6 = (float) (Main.screenPosition.X - Main.screenLastPosition.X);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local2 = @this.position.X;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num7 = (double) ^(float&) local2 - (double) num6 * (double) num1;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local2 = (float) num7;
      }
      float num8 = 600f;
      if (!this.kill)
      {
        if ((double) this.Alpha < 1.0)
        {
          this.Alpha = this.Alpha + 1f / 1000f * (float) Main.dayRate;
          if ((double) this.Alpha > 1.0)
            this.Alpha = 1f;
        }
      }
      else
      {
        this.Alpha = this.Alpha - 1f / 1000f * (float) Main.dayRate;
        if ((double) this.Alpha <= 0.0)
          this.active = false;
      }
      if (this.position.X + (double) Main.cloudTexture[this.type].get_Width() * (double) this.scale < -(double) num8 || this.position.X > (double) Main.screenWidth + (double) num8)
        this.active = false;
      this.rSpeed = this.rSpeed + (float) Cloud.rand.Next(-10, 11) * 2E-05f;
      if ((double) this.rSpeed > 0.0002)
        this.rSpeed = 0.0002f;
      if ((double) this.rSpeed < -0.0002)
        this.rSpeed = -0.0002f;
      if ((double) this.rotation > 0.02)
        this.rotation = 0.02f;
      if ((double) this.rotation < -0.02)
        this.rotation = -0.02f;
      this.rotation = this.rotation + this.rSpeed;
      this.width = (int) ((double) Main.cloudTexture[this.type].get_Width() * (double) this.scale);
      this.height = (int) ((double) Main.cloudTexture[this.type].get_Height() * (double) this.scale);
      if (this.type < 9 || this.type > 13 || (double) Main.cloudAlpha <= 0.0 && (double) Main.cloudBGActive < 1.0)
        return;
      this.kill = true;
    }
  }
}
