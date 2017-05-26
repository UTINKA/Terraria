// Decompiled with JetBrains decompiler
// Type: Terraria.Dust
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.Graphics.Shaders;
using Terraria.Utilities;

namespace Terraria
{
  public class Dust
  {
    public static float dCount;
    public static int lavaBubbles;
    public static int SandStormCount;
    public int dustIndex;
    public Vector2 position;
    public Vector2 velocity;
    public float fadeIn;
    public bool noGravity;
    public float scale;
    public float rotation;
    public bool noLight;
    public bool active;
    public int type;
    public Color color;
    public int alpha;
    public Rectangle frame;
    public ArmorShaderData shader;
    public object customData;
    public bool firstFrame;

    public static Dust NewDustPerfect(Vector2 Position, int Type, Vector2? Velocity = null, int Alpha = 0, Color newColor = null, float Scale = 1f)
    {
      Dust dust = Main.dust[Dust.NewDust(Position, 0, 0, Type, 0.0f, 0.0f, Alpha, newColor, Scale)];
      dust.position = Position;
      if (Velocity.HasValue)
        dust.velocity = Velocity.Value;
      return dust;
    }

    public static Dust NewDustDirect(Vector2 Position, int Width, int Height, int Type, float SpeedX = 0.0f, float SpeedY = 0.0f, int Alpha = 0, Color newColor = null, float Scale = 1f)
    {
      Dust dust = Main.dust[Dust.NewDust(Position, Width, Height, Type, SpeedX, SpeedY, Alpha, newColor, Scale)];
      if (dust.velocity.HasNaNs())
        dust.velocity = Vector2.get_Zero();
      return dust;
    }

    public static int NewDust(Vector2 Position, int Width, int Height, int Type, float SpeedX = 0.0f, float SpeedY = 0.0f, int Alpha = 0, Color newColor = null, float Scale = 1f)
    {
      if (Main.gameMenu)
        return 6000;
      if (Main.rand == null)
        Main.rand = new UnifiedRandom((int) DateTime.Now.Ticks);
      if (Main.gamePaused || WorldGen.gen || Main.netMode == 2)
        return 6000;
      int num1 = (int) (400.0 * (1.0 - (double) Dust.dCount));
      Rectangle rectangle1;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle1).\u002Ector((int) (Main.screenPosition.X - (double) num1), (int) (Main.screenPosition.Y - (double) num1), Main.screenWidth + num1 * 2, Main.screenHeight + num1 * 2);
      Rectangle rectangle2;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle2).\u002Ector((int) Position.X, (int) Position.Y, 10, 10);
      // ISSUE: explicit reference operation
      if (!((Rectangle) @rectangle1).Intersects(rectangle2))
        return 6000;
      int num2 = 6000;
      for (int index = 0; index < 6000; ++index)
      {
        Dust dust1 = Main.dust[index];
        if (!dust1.active)
        {
          if ((double) index > (double) Main.maxDustToDraw * 0.9)
          {
            if (Main.rand.Next(4) != 0)
              return 5999;
          }
          else if ((double) index > (double) Main.maxDustToDraw * 0.8)
          {
            if (Main.rand.Next(3) != 0)
              return 5999;
          }
          else if ((double) index > (double) Main.maxDustToDraw * 0.7)
          {
            if (Main.rand.Next(2) == 0)
              return 5999;
          }
          else if ((double) index > (double) Main.maxDustToDraw * 0.6)
          {
            if (Main.rand.Next(4) == 0)
              return 5999;
          }
          else if ((double) index > (double) Main.maxDustToDraw * 0.5)
          {
            if (Main.rand.Next(5) == 0)
              return 5999;
          }
          else
            Dust.dCount = 0.0f;
          int num3 = Width;
          int num4 = Height;
          if (num3 < 5)
            num3 = 5;
          if (num4 < 5)
            num4 = 5;
          num2 = index;
          dust1.fadeIn = 0.0f;
          dust1.active = true;
          dust1.type = Type;
          dust1.noGravity = false;
          dust1.color = newColor;
          dust1.alpha = Alpha;
          dust1.position.X = (__Null) (Position.X + (double) Main.rand.Next(num3 - 4) + 4.0);
          dust1.position.Y = (__Null) (Position.Y + (double) Main.rand.Next(num4 - 4) + 4.0);
          dust1.velocity.X = (__Null) ((double) Main.rand.Next(-20, 21) * 0.100000001490116 + (double) SpeedX);
          dust1.velocity.Y = (__Null) ((double) Main.rand.Next(-20, 21) * 0.100000001490116 + (double) SpeedY);
          dust1.frame.X = (__Null) (10 * Type);
          dust1.frame.Y = (__Null) (10 * Main.rand.Next(3));
          dust1.shader = (ArmorShaderData) null;
          dust1.customData = (object) null;
          int num5 = Type;
          while (num5 >= 100)
          {
            num5 -= 100;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local1 = @dust1.frame.X;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            int num6 = ^(int&) local1 - 1000;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) local1 = num6;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local2 = @dust1.frame.Y;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            int num7 = ^(int&) local2 + 30;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) local2 = num7;
          }
          dust1.frame.Width = (__Null) 8;
          dust1.frame.Height = (__Null) 8;
          dust1.rotation = 0.0f;
          dust1.scale = (float) (1.0 + (double) Main.rand.Next(-20, 21) * 0.00999999977648258);
          dust1.scale *= Scale;
          dust1.noLight = false;
          dust1.firstFrame = true;
          if (dust1.type == 228 || dust1.type == 269 || (dust1.type == 135 || dust1.type == 6) || (dust1.type == 242 || dust1.type == 75 || (dust1.type == 169 || dust1.type == 29)) || (dust1.type >= 59 && dust1.type <= 65 || dust1.type == 158))
          {
            dust1.velocity.Y = (__Null) ((double) Main.rand.Next(-10, 6) * 0.100000001490116);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local = @dust1.velocity.X;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num6 = (double) ^(float&) local * 0.300000011920929;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local = (float) num6;
            dust1.scale *= 0.7f;
          }
          if (dust1.type == (int) sbyte.MaxValue || dust1.type == 187)
          {
            Dust dust2 = dust1;
            Vector2 vector2 = Vector2.op_Multiply(dust2.velocity, 0.3f);
            dust2.velocity = vector2;
            dust1.scale *= 0.7f;
          }
          if (dust1.type == 33 || dust1.type == 52 || (dust1.type == 266 || dust1.type == 98) || (dust1.type == 99 || dust1.type == 100 || (dust1.type == 101 || dust1.type == 102)) || (dust1.type == 103 || dust1.type == 104 || dust1.type == 105))
          {
            dust1.alpha = 170;
            Dust dust2 = dust1;
            Vector2 vector2 = Vector2.op_Multiply(dust2.velocity, 0.5f);
            dust2.velocity = vector2;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local = @dust1.velocity.Y;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num6 = (double) ^(float&) local + 1.0;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local = (float) num6;
          }
          if (dust1.type == 41)
          {
            Dust dust2 = dust1;
            Vector2 vector2 = Vector2.op_Multiply(dust2.velocity, 0.0f);
            dust2.velocity = vector2;
          }
          if (dust1.type == 80)
            dust1.alpha = 50;
          if (dust1.type == 34 || dust1.type == 35 || dust1.type == 152)
          {
            Dust dust2 = dust1;
            Vector2 vector2 = Vector2.op_Multiply(dust2.velocity, 0.1f);
            dust2.velocity = vector2;
            dust1.velocity.Y = (__Null) -0.5;
            if (dust1.type == 34 && !Collision.WetCollision(new Vector2((float) dust1.position.X, (float) (dust1.position.Y - 8.0)), 4, 4))
            {
              dust1.active = false;
              break;
            }
            break;
          }
          break;
        }
      }
      return num2;
    }

    public static Dust CloneDust(int dustIndex)
    {
      return Dust.CloneDust(Main.dust[dustIndex]);
    }

    public static Dust CloneDust(Dust rf)
    {
      if (rf.dustIndex == Main.maxDustToDraw)
        return rf;
      int index = Dust.NewDust(rf.position, 0, 0, rf.type, 0.0f, 0.0f, 0, (Color) null, 1f);
      Dust dust = Main.dust[index];
      Vector2 position = rf.position;
      dust.position = position;
      Vector2 velocity = rf.velocity;
      dust.velocity = velocity;
      double fadeIn = (double) rf.fadeIn;
      dust.fadeIn = (float) fadeIn;
      int num1 = rf.noGravity ? 1 : 0;
      dust.noGravity = num1 != 0;
      double scale = (double) rf.scale;
      dust.scale = (float) scale;
      double rotation = (double) rf.rotation;
      dust.rotation = (float) rotation;
      int num2 = rf.noLight ? 1 : 0;
      dust.noLight = num2 != 0;
      int num3 = rf.active ? 1 : 0;
      dust.active = num3 != 0;
      int type = rf.type;
      dust.type = type;
      Color color = rf.color;
      dust.color = color;
      int alpha = rf.alpha;
      dust.alpha = alpha;
      Rectangle frame = rf.frame;
      dust.frame = frame;
      ArmorShaderData shader = rf.shader;
      dust.shader = shader;
      object customData = rf.customData;
      dust.customData = customData;
      return dust;
    }

    public static Dust QuickDust(Point tileCoords, Color color)
    {
      return Dust.QuickDust(tileCoords.ToWorldCoordinates(8f, 8f), color);
    }

    public static void QuickBox(Vector2 topLeft, Vector2 bottomRight, int divisions, Color color, Action<Dust> manipulator)
    {
      float num1 = (float) (divisions + 2);
      for (float num2 = 0.0f; (double) num2 <= (double) (divisions + 2); ++num2)
      {
        Dust dust1 = Dust.QuickDust(new Vector2(MathHelper.Lerp((float) topLeft.X, (float) bottomRight.X, num2 / num1), (float) topLeft.Y), color);
        if (manipulator != null)
          manipulator(dust1);
        Dust dust2 = Dust.QuickDust(new Vector2(MathHelper.Lerp((float) topLeft.X, (float) bottomRight.X, num2 / num1), (float) bottomRight.Y), color);
        if (manipulator != null)
          manipulator(dust2);
        Dust dust3 = Dust.QuickDust(new Vector2((float) topLeft.X, MathHelper.Lerp((float) topLeft.Y, (float) bottomRight.Y, num2 / num1)), color);
        if (manipulator != null)
          manipulator(dust3);
        Dust dust4 = Dust.QuickDust(new Vector2((float) bottomRight.X, MathHelper.Lerp((float) topLeft.Y, (float) bottomRight.Y, num2 / num1)), color);
        if (manipulator != null)
          manipulator(dust4);
      }
    }

    public static Dust QuickDust(Vector2 pos, Color color)
    {
      Dust dust = Main.dust[Dust.NewDust(pos, 0, 0, 267, 0.0f, 0.0f, 0, (Color) null, 1f)];
      Vector2 vector2 = pos;
      dust.position = vector2;
      Vector2 zero = Vector2.get_Zero();
      dust.velocity = zero;
      double num1 = 1.0;
      dust.fadeIn = (float) num1;
      int num2 = 1;
      dust.noLight = num2 != 0;
      int num3 = 1;
      dust.noGravity = num3 != 0;
      Color color1 = color;
      dust.color = color1;
      return dust;
    }

    public static void QuickDustLine(Vector2 start, Vector2 end, float splits, Color color)
    {
      Dust.QuickDust(start, color).scale = 2f;
      Dust.QuickDust(end, color).scale = 2f;
      float num1 = 1f / splits;
      float num2 = 0.0f;
      while ((double) num2 < 1.0)
      {
        Dust.QuickDust(Vector2.Lerp(start, end, num2), color).scale = 2f;
        num2 += num1;
      }
    }

    public static int dustWater()
    {
      switch (Main.waterStyle)
      {
        case 2:
          return 98;
        case 3:
          return 99;
        case 4:
          return 100;
        case 5:
          return 101;
        case 6:
          return 102;
        case 7:
          return 103;
        case 8:
          return 104;
        case 9:
          return 105;
        case 10:
          return 123;
        default:
          return 33;
      }
    }

    public static void UpdateDust()
    {
      int num1 = 0;
      Dust.lavaBubbles = 0;
      Main.snowDust = 0;
      Dust.SandStormCount = 0;
      bool flag = Sandstorm.Happening && Main.player[Main.myPlayer].ZoneSandstorm && ((Main.bgStyle == 2 || Main.bgStyle == 5) && Main.bgDelay < 50);
      for (int index1 = 0; index1 < 6000; ++index1)
      {
        Dust dust1 = Main.dust[index1];
        if (index1 < Main.maxDustToDraw)
        {
          if (dust1.active)
          {
            ++Dust.dCount;
            if ((double) dust1.scale > 10.0)
              dust1.active = false;
            if (dust1.firstFrame && !ChildSafety.Disabled && ChildSafety.DangerousDust(dust1.type))
            {
              if (Main.rand.Next(2) == 0)
              {
                dust1.firstFrame = false;
                dust1.type = 16;
                dust1.scale = (float) ((double) Main.rand.NextFloat() * 1.60000002384186 + 0.300000011920929);
                dust1.color = Color.get_Transparent();
                dust1.frame.X = (__Null) (10 * dust1.type);
                dust1.frame.Y = (__Null) (10 * Main.rand.Next(3));
                dust1.shader = (ArmorShaderData) null;
                dust1.customData = (object) null;
                int num2 = dust1.type / 100;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local1 = @dust1.frame.X;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                int num3 = ^(int&) local1 - 1000 * num2;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(int&) local1 = num3;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local2 = @dust1.frame.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                int num4 = ^(int&) local2 + 30 * num2;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(int&) local2 = num4;
                dust1.noGravity = true;
              }
              else
                dust1.active = false;
            }
            if (dust1.type == 35)
              ++Dust.lavaBubbles;
            Dust dust2 = dust1;
            Vector2 vector2_1 = Vector2.op_Addition(dust2.position, dust1.velocity);
            dust2.position = vector2_1;
            if (dust1.type == 258)
            {
              dust1.noGravity = true;
              dust1.scale += 0.015f;
            }
            if (dust1.type >= 86 && dust1.type <= 92 && !dust1.noLight)
            {
              float num2 = dust1.scale * 0.6f;
              if ((double) num2 > 1.0)
                num2 = 1f;
              int num3 = dust1.type - 85;
              float num4 = num2;
              float num5 = num2;
              float num6 = num2;
              if (num3 == 3)
              {
                num4 *= 0.0f;
                num5 *= 0.1f;
                num6 *= 1.3f;
              }
              else if (num3 == 5)
              {
                num4 *= 1f;
                num5 *= 0.1f;
                num6 *= 0.1f;
              }
              else if (num3 == 4)
              {
                num4 *= 0.0f;
                num5 *= 1f;
                num6 *= 0.1f;
              }
              else if (num3 == 1)
              {
                num4 *= 0.9f;
                num5 *= 0.0f;
                num6 *= 0.9f;
              }
              else if (num3 == 6)
              {
                num4 *= 1.3f;
                num5 *= 1.3f;
                num6 *= 1.3f;
              }
              else if (num3 == 2)
              {
                num4 *= 0.9f;
                num5 *= 0.9f;
                num6 *= 0.0f;
              }
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num4, num2 * num5, num2 * num6);
            }
            if (dust1.type >= 86 && dust1.type <= 92)
            {
              if (dust1.customData != null && dust1.customData is Player)
              {
                Player customData = (Player) dust1.customData;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPosition));
                dust3.position = vector2_2;
              }
              else if (dust1.customData != null && dust1.customData is Projectile)
              {
                Projectile customData = (Projectile) dust1.customData;
                if (customData.active)
                {
                  Dust dust3 = dust1;
                  Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPosition));
                  dust3.position = vector2_2;
                }
              }
            }
            if (dust1.type == 262 && !dust1.noLight)
            {
              Vector3 rgb = Vector3.op_Multiply(Vector3.op_Multiply(new Vector3(0.9f, 0.6f, 0.0f), dust1.scale), 0.6f);
              Lighting.AddLight(dust1.position, rgb);
            }
            if (dust1.type == 240 && dust1.customData != null && dust1.customData is Projectile)
            {
              Projectile customData = (Projectile) dust1.customData;
              if (customData.active)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPosition));
                dust3.position = vector2_2;
              }
            }
            if ((dust1.type == 259 || dust1.type == 6 || dust1.type == 158) && (dust1.customData != null && dust1.customData is int))
            {
              if ((int) dust1.customData == 0)
              {
                if (Collision.SolidCollision(Vector2.op_Subtraction(dust1.position, Vector2.op_Multiply(Vector2.get_One(), 5f)), 10, 10) && (double) dust1.fadeIn == 0.0)
                {
                  dust1.scale *= 0.9f;
                  Dust dust3 = dust1;
                  Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.25f);
                  dust3.velocity = vector2_2;
                }
              }
              else if ((int) dust1.customData == 1)
              {
                dust1.scale *= 0.98f;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local * 0.980000019073486;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local = (float) num2;
                if (Collision.SolidCollision(Vector2.op_Subtraction(dust1.position, Vector2.op_Multiply(Vector2.get_One(), 5f)), 10, 10) && (double) dust1.fadeIn == 0.0)
                {
                  dust1.scale *= 0.9f;
                  Dust dust3 = dust1;
                  Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.25f);
                  dust3.velocity = vector2_2;
                }
              }
            }
            if (dust1.type == 263 || dust1.type == 264)
            {
              if (!dust1.noLight)
              {
                // ISSUE: explicit reference operation
                Vector3 rgb = Vector3.op_Multiply(Vector3.op_Multiply(((Color) @dust1.color).ToVector3(), dust1.scale), 0.4f);
                Lighting.AddLight(dust1.position, rgb);
              }
              if (dust1.customData != null && dust1.customData is Player)
              {
                Player customData = (Player) dust1.customData;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPosition));
                dust3.position = vector2_2;
                dust1.customData = (object) null;
              }
              else if (dust1.customData != null && dust1.customData is Projectile)
              {
                Projectile customData = (Projectile) dust1.customData;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPosition));
                dust3.position = vector2_2;
              }
            }
            if (dust1.type == 230)
            {
              float num2 = dust1.scale * 0.6f;
              float num3 = num2;
              float num4 = num2;
              float num5 = num2;
              float num6 = num3 * 0.5f;
              float num7 = num4 * 0.9f;
              float num8 = num5 * 1f;
              dust1.scale += 0.02f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num6, num2 * num7, num2 * num8);
              if (dust1.customData != null && dust1.customData is Player)
              {
                Vector2 center = ((Entity) dust1.customData).Center;
                Vector2 vector2_2 = Vector2.op_Subtraction(dust1.position, center);
                // ISSUE: explicit reference operation
                float val2 = ((Vector2) @vector2_2).Length();
                Vector2 vector2_3 = Vector2.op_Division(vector2_2, val2);
                dust1.scale = Math.Min(dust1.scale, (float) ((double) val2 / 24.0 - 1.0));
                Dust dust3 = dust1;
                Vector2 vector2_4 = Vector2.op_Subtraction(dust3.velocity, Vector2.op_Multiply(vector2_3, 100f / Math.Max(50f, val2)));
                dust3.velocity = vector2_4;
              }
            }
            if (dust1.type == 154 || dust1.type == 218)
            {
              dust1.rotation += (float) (dust1.velocity.X * 0.300000011920929);
              dust1.scale -= 0.03f;
            }
            if (dust1.type == 172)
            {
              float num2 = dust1.scale * 0.5f;
              if ((double) num2 > 1.0)
                num2 = 1f;
              float num3 = num2;
              float num4 = num2;
              float num5 = num2;
              float num6 = num3 * 0.0f;
              float num7 = num4 * 0.25f;
              float num8 = num5 * 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num6, num2 * num7, num2 * num8);
            }
            if (dust1.type == 182)
            {
              ++dust1.rotation;
              if (!dust1.noLight)
              {
                float num2 = dust1.scale * 0.25f;
                if ((double) num2 > 1.0)
                  num2 = 1f;
                float num3 = num2;
                float num4 = num2;
                float num5 = num2;
                float num6 = num3 * 1f;
                float num7 = num4 * 0.2f;
                float num8 = num5 * 0.1f;
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num6, num2 * num7, num2 * num8);
              }
              if (dust1.customData != null && dust1.customData is Player)
              {
                Player customData = (Player) dust1.customData;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPosition));
                dust3.position = vector2_2;
                dust1.customData = (object) null;
              }
            }
            if (dust1.type == 261)
            {
              if (!dust1.noLight)
              {
                float num2 = dust1.scale * 0.3f;
                if ((double) num2 > 1.0)
                  num2 = 1f;
                Lighting.AddLight(dust1.position, Vector3.op_Multiply(new Vector3(0.4f, 0.6f, 0.7f), num2));
              }
              if (dust1.noGravity)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.93f);
                dust3.velocity = vector2_2;
                if ((double) dust1.fadeIn == 0.0)
                  dust1.scale += 1f / 400f;
              }
              Dust dust4 = dust1;
              Vector2 vector2_3 = Vector2.op_Multiply(dust4.velocity, new Vector2(0.97f, 0.99f));
              dust4.velocity = vector2_3;
              dust1.scale -= 1f / 400f;
              if (dust1.customData != null && dust1.customData is Player)
              {
                Player customData = (Player) dust1.customData;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPosition));
                dust3.position = vector2_2;
              }
            }
            if (dust1.type == 254)
            {
              float num2 = dust1.scale * 0.35f;
              if ((double) num2 > 1.0)
                num2 = 1f;
              float num3 = num2;
              float num4 = num2;
              float num5 = num2;
              float num6 = num3 * 0.9f;
              float num7 = num4 * 0.1f;
              float num8 = num5 * 0.75f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num6, num2 * num7, num2 * num8);
            }
            if (dust1.type == (int) byte.MaxValue)
            {
              float num2 = dust1.scale * 0.25f;
              if ((double) num2 > 1.0)
                num2 = 1f;
              float num3 = num2;
              float num4 = num2;
              float num5 = num2;
              float num6 = num3 * 0.9f;
              float num7 = num4 * 0.1f;
              float num8 = num5 * 0.75f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num6, num2 * num7, num2 * num8);
            }
            if (dust1.type == 211 && dust1.noLight && Collision.SolidCollision(dust1.position, 4, 4))
              dust1.active = false;
            if (dust1.type == 213 || dust1.type == 260)
            {
              dust1.rotation = 0.0f;
              float num2 = (float) ((double) dust1.scale / 2.5 * 0.200000002980232);
              Vector3 zero = Vector3.get_Zero();
              switch (dust1.type)
              {
                case 213:
                  // ISSUE: explicit reference operation
                  ((Vector3) @zero).\u002Ector((float) byte.MaxValue, 217f, 48f);
                  break;
                case 260:
                  // ISSUE: explicit reference operation
                  ((Vector3) @zero).\u002Ector((float) byte.MaxValue, 48f, 48f);
                  break;
              }
              Vector3 vector3_1 = Vector3.op_Division(zero, (float) byte.MaxValue);
              if ((double) num2 > 1.0)
                num2 = 1f;
              Vector3 vector3_2 = Vector3.op_Multiply(vector3_1, num2);
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), (float) vector3_2.X, (float) vector3_2.Y, (float) vector3_2.Z);
            }
            if (dust1.type == 157)
            {
              float num2 = dust1.scale * 0.2f;
              float num3 = num2;
              float num4 = num2;
              float num5 = num2;
              float num6 = num3 * 0.25f;
              float num7 = num4 * 1f;
              float num8 = num5 * 0.5f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num6, num2 * num7, num2 * num8);
            }
            if (dust1.type == 206)
            {
              dust1.scale -= 0.1f;
              float num2 = dust1.scale * 0.4f;
              float num3 = num2;
              float num4 = num2;
              float num5 = num2;
              float num6 = num3 * 0.1f;
              float num7 = num4 * 0.6f;
              float num8 = num5 * 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num6, num2 * num7, num2 * num8);
            }
            if (dust1.type == 163)
            {
              float num2 = dust1.scale * 0.25f;
              float num3 = num2;
              float num4 = num2;
              float num5 = num2;
              float num6 = num3 * 0.25f;
              float num7 = num4 * 1f;
              float num8 = num5 * 0.05f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num6, num2 * num7, num2 * num8);
            }
            if (dust1.type == 205)
            {
              float num2 = dust1.scale * 0.25f;
              float num3 = num2;
              float num4 = num2;
              float num5 = num2;
              float num6 = num3 * 1f;
              float num7 = num4 * 0.05f;
              float num8 = num5 * 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num6, num2 * num7, num2 * num8);
            }
            if (dust1.type == 170)
            {
              float num2 = dust1.scale * 0.5f;
              float num3 = num2;
              float num4 = num2;
              float num5 = num2;
              float num6 = num3 * 1f;
              float num7 = num4 * 1f;
              float num8 = num5 * 0.05f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num6, num2 * num7, num2 * num8);
            }
            if (dust1.type == 156)
            {
              float num2 = dust1.scale * 0.6f;
              int type = dust1.type;
              float num3 = num2;
              float num4 = num2;
              float num5 = num2;
              float num6 = num3 * 0.5f;
              float num7 = num4 * 0.9f;
              float num8 = num5 * 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num6, num2 * num7, num2 * num8);
            }
            if (dust1.type == 234)
            {
              float num2 = dust1.scale * 0.6f;
              int type = dust1.type;
              float num3 = num2;
              float num4 = num2;
              float num5 = num2;
              float num6 = num3 * 0.95f;
              float num7 = num4 * 0.65f;
              float num8 = num5 * 1.3f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num6, num2 * num7, num2 * num8);
            }
            if (dust1.type == 175)
              dust1.scale -= 0.05f;
            if (dust1.type == 174)
            {
              dust1.scale -= 0.01f;
              float R = dust1.scale * 1f;
              if ((double) R > 0.600000023841858)
                R = 0.6f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), R, R * 0.4f, 0.0f);
            }
            if (dust1.type == 235)
            {
              Vector2 vector2_2;
              // ISSUE: explicit reference operation
              ((Vector2) @vector2_2).\u002Ector((float) Main.rand.Next(-100, 101), (float) Main.rand.Next(-100, 101));
              // ISSUE: explicit reference operation
              ((Vector2) @vector2_2).Normalize();
              vector2_2 = Vector2.op_Multiply(vector2_2, 15f);
              dust1.scale -= 0.01f;
            }
            else if (dust1.type == 228 || dust1.type == 229 || (dust1.type == 6 || dust1.type == 242) || (dust1.type == 135 || dust1.type == (int) sbyte.MaxValue || (dust1.type == 187 || dust1.type == 75)) || (dust1.type == 169 || dust1.type == 29 || dust1.type >= 59 && dust1.type <= 65 || dust1.type == 158))
            {
              if (!dust1.noGravity)
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local + 0.0500000007450581;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local = (float) num2;
              }
              if (dust1.type == 229 || dust1.type == 228)
              {
                if (dust1.customData != null && dust1.customData is NPC)
                {
                  NPC customData = (NPC) dust1.customData;
                  Dust dust3 = dust1;
                  Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPos[1]));
                  dust3.position = vector2_2;
                }
                else if (dust1.customData != null && dust1.customData is Player)
                {
                  Player customData = (Player) dust1.customData;
                  Dust dust3 = dust1;
                  Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPosition));
                  dust3.position = vector2_2;
                }
                else if (dust1.customData != null && dust1.customData is Vector2)
                {
                  Vector2 vector2_2 = Vector2.op_Subtraction((Vector2) dust1.customData, dust1.position);
                  if (Vector2.op_Inequality(vector2_2, Vector2.get_Zero()))
                  {
                    // ISSUE: explicit reference operation
                    ((Vector2) @vector2_2).Normalize();
                  }
                  // ISSUE: explicit reference operation
                  dust1.velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(dust1.velocity, 4f), Vector2.op_Multiply(vector2_2, ((Vector2) @dust1.velocity).Length())), 5f);
                }
              }
              if (!dust1.noLight)
              {
                float num2 = dust1.scale * 1.4f;
                if (dust1.type == 29)
                {
                  if ((double) num2 > 1.0)
                    num2 = 1f;
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.1f, num2 * 0.4f, num2);
                }
                else if (dust1.type == 75)
                {
                  if ((double) num2 > 1.0)
                    num2 = 1f;
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.7f, num2, num2 * 0.2f);
                }
                else if (dust1.type == 169)
                {
                  if ((double) num2 > 1.0)
                    num2 = 1f;
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 1.1f, num2 * 1.1f, num2 * 0.2f);
                }
                else if (dust1.type == 135)
                {
                  if ((double) num2 > 1.0)
                    num2 = 1f;
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.2f, num2 * 0.7f, num2);
                }
                else if (dust1.type == 158)
                {
                  if ((double) num2 > 1.0)
                    num2 = 1f;
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 1f, num2 * 0.5f, 0.0f);
                }
                else if (dust1.type == 228)
                {
                  if ((double) num2 > 1.0)
                    num2 = 1f;
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.7f, num2 * 0.65f, num2 * 0.3f);
                }
                else if (dust1.type == 229)
                {
                  if ((double) num2 > 1.0)
                    num2 = 1f;
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.3f, num2 * 0.65f, num2 * 0.7f);
                }
                else if (dust1.type == 242)
                {
                  if ((double) num2 > 1.0)
                    num2 = 1f;
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2, 0.0f, num2);
                }
                else if (dust1.type >= 59 && dust1.type <= 65)
                {
                  if ((double) num2 > 0.800000011920929)
                    num2 = 0.8f;
                  int num3 = dust1.type - 58;
                  float num4 = 1f;
                  float num5 = 1f;
                  float num6 = 1f;
                  if (num3 == 1)
                  {
                    num4 = 0.0f;
                    num5 = 0.1f;
                    num6 = 1.3f;
                  }
                  else if (num3 == 2)
                  {
                    num4 = 1f;
                    num5 = 0.1f;
                    num6 = 0.1f;
                  }
                  else if (num3 == 3)
                  {
                    num4 = 0.0f;
                    num5 = 1f;
                    num6 = 0.1f;
                  }
                  else if (num3 == 4)
                  {
                    num4 = 0.9f;
                    num5 = 0.0f;
                    num6 = 0.9f;
                  }
                  else if (num3 == 5)
                  {
                    num4 = 1.3f;
                    num5 = 1.3f;
                    num6 = 1.3f;
                  }
                  else if (num3 == 6)
                  {
                    num4 = 0.9f;
                    num5 = 0.9f;
                    num6 = 0.0f;
                  }
                  else if (num3 == 7)
                  {
                    num4 = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                    num5 = 0.3f;
                    num6 = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                  }
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * num4, num2 * num5, num2 * num6);
                }
                else if (dust1.type == (int) sbyte.MaxValue)
                {
                  float R = num2 * 1.3f;
                  if ((double) R > 1.0)
                    R = 1f;
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), R, R * 0.45f, R * 0.2f);
                }
                else if (dust1.type == 187)
                {
                  float B = num2 * 1.3f;
                  if ((double) B > 1.0)
                    B = 1f;
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), B * 0.2f, B * 0.45f, B);
                }
                else
                {
                  if ((double) num2 > 0.600000023841858)
                    num2 = 0.6f;
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2, num2 * 0.65f, num2 * 0.4f);
                }
              }
            }
            else if (dust1.type == 269)
            {
              if (!dust1.noLight)
              {
                float num2 = dust1.scale * 1.4f;
                if ((double) num2 > 1.0)
                  num2 = 1f;
                Vector3 vector3;
                // ISSUE: explicit reference operation
                ((Vector3) @vector3).\u002Ector(0.7f, 0.65f, 0.3f);
                Lighting.AddLight(dust1.position, Vector3.op_Multiply(vector3, num2));
              }
              if (dust1.customData != null && dust1.customData is Vector2)
              {
                Vector2 vector2_2 = Vector2.op_Subtraction((Vector2) dust1.customData, dust1.position);
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local = @dust1.velocity.X;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local + 1.0 * (double) Math.Sign((float) vector2_2.X) * (double) dust1.scale;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local = (float) num2;
              }
            }
            else if (dust1.type == 159)
            {
              float num2 = dust1.scale * 1.3f;
              if ((double) num2 > 1.0)
                num2 = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2, num2, num2 * 0.1f);
              if (dust1.noGravity)
              {
                if ((double) dust1.scale < 0.699999988079071)
                {
                  Dust dust3 = dust1;
                  Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 1.075f);
                  dust3.velocity = vector2_2;
                }
                else if (Main.rand.Next(2) == 0)
                {
                  Dust dust3 = dust1;
                  Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, -0.95f);
                  dust3.velocity = vector2_2;
                }
                else
                {
                  Dust dust3 = dust1;
                  Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 1.05f);
                  dust3.velocity = vector2_2;
                }
                dust1.scale -= 0.03f;
              }
              else
              {
                dust1.scale += 0.005f;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.9f);
                dust3.velocity = vector2_2;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local1 = @dust1.velocity.X;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num3 = (double) ^(float&) local1 + (double) Main.rand.Next(-10, 11) * 0.0199999995529652;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local1 = (float) num3;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local2 = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num4 = (double) ^(float&) local2 + (double) Main.rand.Next(-10, 11) * 0.0199999995529652;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local2 = (float) num4;
                if (Main.rand.Next(5) == 0)
                {
                  int index2 = Dust.NewDust(dust1.position, 4, 4, dust1.type, 0.0f, 0.0f, 0, (Color) null, 1f);
                  Main.dust[index2].noGravity = true;
                  Main.dust[index2].scale = dust1.scale * 2.5f;
                }
              }
            }
            else if (dust1.type == 164)
            {
              float R = dust1.scale;
              if ((double) R > 1.0)
                R = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), R, R * 0.1f, R * 0.8f);
              if (dust1.noGravity)
              {
                if ((double) dust1.scale < 0.699999988079071)
                {
                  Dust dust3 = dust1;
                  Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 1.075f);
                  dust3.velocity = vector2_2;
                }
                else if (Main.rand.Next(2) == 0)
                {
                  Dust dust3 = dust1;
                  Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, -0.95f);
                  dust3.velocity = vector2_2;
                }
                else
                {
                  Dust dust3 = dust1;
                  Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 1.05f);
                  dust3.velocity = vector2_2;
                }
                dust1.scale -= 0.03f;
              }
              else
              {
                dust1.scale -= 0.005f;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.9f);
                dust3.velocity = vector2_2;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local1 = @dust1.velocity.X;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local1 + (double) Main.rand.Next(-10, 11) * 0.0199999995529652;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local1 = (float) num2;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local2 = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num3 = (double) ^(float&) local2 + (double) Main.rand.Next(-10, 11) * 0.0199999995529652;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local2 = (float) num3;
                if (Main.rand.Next(5) == 0)
                {
                  int index2 = Dust.NewDust(dust1.position, 4, 4, dust1.type, 0.0f, 0.0f, 0, (Color) null, 1f);
                  Main.dust[index2].noGravity = true;
                  Main.dust[index2].scale = dust1.scale * 2.5f;
                }
              }
            }
            else if (dust1.type == 173)
            {
              float B = dust1.scale;
              if ((double) B > 1.0)
                B = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), B * 0.4f, B * 0.1f, B);
              if (dust1.noGravity)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.8f);
                dust3.velocity = vector2_2;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local1 = @dust1.velocity.X;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local1 + (double) Main.rand.Next(-20, 21) * 0.00999999977648258;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local1 = (float) num2;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local2 = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num3 = (double) ^(float&) local2 + (double) Main.rand.Next(-20, 21) * 0.00999999977648258;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local2 = (float) num3;
                dust1.scale -= 0.01f;
              }
              else
              {
                dust1.scale -= 0.015f;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.8f);
                dust3.velocity = vector2_2;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local1 = @dust1.velocity.X;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local1 + (double) Main.rand.Next(-10, 11) * 0.00499999988824129;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local1 = (float) num2;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local2 = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num3 = (double) ^(float&) local2 + (double) Main.rand.Next(-10, 11) * 0.00499999988824129;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local2 = (float) num3;
                if (Main.rand.Next(10) == 10)
                {
                  int index2 = Dust.NewDust(dust1.position, 4, 4, dust1.type, 0.0f, 0.0f, 0, (Color) null, 1f);
                  Main.dust[index2].noGravity = true;
                  Main.dust[index2].scale = dust1.scale;
                }
              }
            }
            else if (dust1.type == 184)
            {
              if (!dust1.noGravity)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.0f);
                dust3.velocity = vector2_2;
                dust1.scale -= 0.01f;
              }
            }
            else if (dust1.type == 160 || dust1.type == 162)
            {
              float num2 = dust1.scale * 1.3f;
              if ((double) num2 > 1.0)
                num2 = 1f;
              if (dust1.type == 162)
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2, num2 * 0.7f, num2 * 0.1f);
              else
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.1f, num2, num2);
              if (dust1.noGravity)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.8f);
                dust3.velocity = vector2_2;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local1 = @dust1.velocity.X;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num3 = (double) ^(float&) local1 + (double) Main.rand.Next(-20, 21) * 0.0399999991059303;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local1 = (float) num3;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local2 = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num4 = (double) ^(float&) local2 + (double) Main.rand.Next(-20, 21) * 0.0399999991059303;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local2 = (float) num4;
                dust1.scale -= 0.1f;
              }
              else
              {
                dust1.scale -= 0.1f;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local1 = @dust1.velocity.X;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num3 = (double) ^(float&) local1 + (double) Main.rand.Next(-10, 11) * 0.0199999995529652;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local1 = (float) num3;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local2 = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num4 = (double) ^(float&) local2 + (double) Main.rand.Next(-10, 11) * 0.0199999995529652;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local2 = (float) num4;
                if ((double) dust1.scale > 0.3 && Main.rand.Next(50) == 0)
                {
                  int index2 = Dust.NewDust(new Vector2((float) (dust1.position.X - 4.0), (float) (dust1.position.Y - 4.0)), 1, 1, dust1.type, 0.0f, 0.0f, 0, (Color) null, 1f);
                  Main.dust[index2].noGravity = true;
                  Main.dust[index2].scale = dust1.scale * 1.5f;
                }
              }
            }
            else if (dust1.type == 168)
            {
              float R = dust1.scale * 0.8f;
              if ((double) R > 0.55)
                R = 0.55f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), R, 0.0f, R * 0.8f);
              dust1.scale += 0.03f;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num2 = (double) ^(float&) local1 + (double) Main.rand.Next(-10, 11) * 0.0199999995529652;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num2;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local2 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num3 = (double) ^(float&) local2 + (double) Main.rand.Next(-10, 11) * 0.0199999995529652;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local2 = (float) num3;
              Dust dust3 = dust1;
              Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.99f);
              dust3.velocity = vector2_2;
            }
            else if (dust1.type >= 139 && dust1.type < 143)
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num2 = (double) ^(float&) local1 * 0.980000019073486;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num2;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local2 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num3 = (double) ^(float&) local2 * 0.980000019073486;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local2 = (float) num3;
              if (dust1.velocity.Y < 1.0)
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local3 = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num4 = (double) ^(float&) local3 + 0.0500000007450581;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local3 = (float) num4;
              }
              dust1.scale += 0.009f;
              dust1.rotation -= (float) (dust1.velocity.X * 0.400000005960464);
              if (dust1.velocity.X > 0.0)
                dust1.rotation += 0.005f;
              else
                dust1.rotation -= 0.005f;
            }
            else if (dust1.type == 14 || dust1.type == 16 || (dust1.type == 31 || dust1.type == 46) || (dust1.type == 124 || dust1.type == 186 || dust1.type == 188))
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num2 = (double) ^(float&) local1 * 0.980000019073486;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num2;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local2 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num3 = (double) ^(float&) local2 * 0.980000019073486;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local2 = (float) num3;
              if (dust1.type == 31 && dust1.noGravity)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 1.02f);
                dust3.velocity = vector2_2;
                dust1.scale += 0.02f;
                dust1.alpha += 4;
                if (dust1.alpha > (int) byte.MaxValue)
                {
                  dust1.scale = 0.0001f;
                  dust1.alpha = (int) byte.MaxValue;
                }
              }
            }
            else if (dust1.type == 32)
            {
              dust1.scale -= 0.01f;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num2 = (double) ^(float&) local1 * 0.959999978542328;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num2;
              if (!dust1.noGravity)
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local2 = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num3 = (double) ^(float&) local2 + 0.100000001490116;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local2 = (float) num3;
              }
            }
            else if (dust1.type >= 244 && dust1.type <= 247)
            {
              dust1.rotation += 0.1f * dust1.scale;
              Color color = Lighting.GetColor((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0));
              int num2;
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              float num3 = (float) (((double) (num2 = (int) (byte) (((int) ((Color) @color).get_R() + (int) ((Color) @color).get_G() + (int) ((Color) @color).get_B()) / 3)) / 270.0 + 1.0) / 2.0);
              float num4 = (float) (((double) num2 / 270.0 + 1.0) / 2.0);
              float num5 = (float) (((double) num2 / 270.0 + 1.0) / 2.0);
              float num6 = num3 * (dust1.scale * 0.9f);
              float num7 = num4 * (dust1.scale * 0.9f);
              float num8 = num5 * (dust1.scale * 0.9f);
              if (dust1.alpha < (int) byte.MaxValue)
              {
                dust1.scale += 0.09f;
                if ((double) dust1.scale >= 1.0)
                {
                  dust1.scale = 1f;
                  dust1.alpha = (int) byte.MaxValue;
                }
              }
              else
              {
                if ((double) dust1.scale < 0.8)
                  dust1.scale -= 0.01f;
                if ((double) dust1.scale < 0.5)
                  dust1.scale -= 0.01f;
              }
              float num9 = 1f;
              if (dust1.type == 244)
              {
                num6 *= 0.8862745f;
                num7 *= 0.4627451f;
                num8 *= 0.2980392f;
                num9 = 0.9f;
              }
              else if (dust1.type == 245)
              {
                num6 *= 0.5137255f;
                num7 *= 0.6745098f;
                num8 *= 0.6784314f;
                num9 = 1f;
              }
              else if (dust1.type == 246)
              {
                num6 *= 0.8f;
                num7 *= 0.7098039f;
                num8 *= 0.282353f;
                num9 = 1.1f;
              }
              else if (dust1.type == 247)
              {
                num6 *= 0.6f;
                num7 *= 0.6745098f;
                num8 *= 0.7254902f;
                num9 = 1.2f;
              }
              float R = num6 * num9;
              float G = num7 * num9;
              float B = num8 * num9;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), R, G, B);
            }
            else if (dust1.type == 43)
            {
              dust1.rotation += 0.1f * dust1.scale;
              Color color = Lighting.GetColor((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0));
              // ISSUE: explicit reference operation
              float num2 = (float) ((Color) @color).get_R() / 270f;
              // ISSUE: explicit reference operation
              float num3 = (float) ((Color) @color).get_G() / 270f;
              // ISSUE: explicit reference operation
              float num4 = (float) ((Color) @color).get_B() / 270f;
              // ISSUE: explicit reference operation
              float num5 = (float) ((int) ((Color) @dust1.color).get_R() / (int) byte.MaxValue);
              // ISSUE: explicit reference operation
              float num6 = (float) ((int) ((Color) @dust1.color).get_G() / (int) byte.MaxValue);
              // ISSUE: explicit reference operation
              float num7 = (float) ((int) ((Color) @dust1.color).get_B() / (int) byte.MaxValue);
              float R = num2 * (dust1.scale * 1.07f * num5);
              float G = num3 * (dust1.scale * 1.07f * num6);
              float B = num4 * (dust1.scale * 1.07f * num7);
              if (dust1.alpha < (int) byte.MaxValue)
              {
                dust1.scale += 0.09f;
                if ((double) dust1.scale >= 1.0)
                {
                  dust1.scale = 1f;
                  dust1.alpha = (int) byte.MaxValue;
                }
              }
              else
              {
                if ((double) dust1.scale < 0.8)
                  dust1.scale -= 0.01f;
                if ((double) dust1.scale < 0.5)
                  dust1.scale -= 0.01f;
              }
              if ((double) R < 0.05 && (double) G < 0.05 && (double) B < 0.05)
                dust1.active = false;
              else
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), R, G, B);
            }
            else if (dust1.type == 15 || dust1.type == 57 || (dust1.type == 58 || dust1.type == 274))
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num2 = (double) ^(float&) local1 * 0.980000019073486;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num2;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local2 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num3 = (double) ^(float&) local2 * 0.980000019073486;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local2 = (float) num3;
              float num4 = dust1.scale;
              if (dust1.type != 15)
                num4 = dust1.scale * 0.8f;
              if (dust1.noLight)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.95f);
                dust3.velocity = vector2_2;
              }
              if ((double) num4 > 1.0)
                num4 = 1f;
              if (dust1.type == 15)
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num4 * 0.45f, num4 * 0.55f, num4);
              else if (dust1.type == 57)
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num4 * 0.95f, num4 * 0.95f, num4 * 0.45f);
              else if (dust1.type == 58)
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num4, num4 * 0.55f, num4 * 0.75f);
            }
            else if (dust1.type == 204)
            {
              if ((double) dust1.fadeIn > (double) dust1.scale)
                dust1.scale += 0.02f;
              else
                dust1.scale -= 0.02f;
              Dust dust3 = dust1;
              Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.95f);
              dust3.velocity = vector2_2;
            }
            else if (dust1.type == 110)
            {
              float G = dust1.scale * 0.1f;
              if ((double) G > 1.0)
                G = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), G * 0.2f, G, G * 0.5f);
            }
            else if (dust1.type == 111)
            {
              float B = dust1.scale * 0.125f;
              if ((double) B > 1.0)
                B = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), B * 0.2f, B * 0.7f, B);
            }
            else if (dust1.type == 112)
            {
              float num2 = dust1.scale * 0.1f;
              if ((double) num2 > 1.0)
                num2 = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.8f, num2 * 0.2f, num2 * 0.8f);
            }
            else if (dust1.type == 113)
            {
              float num2 = dust1.scale * 0.1f;
              if ((double) num2 > 1.0)
                num2 = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.2f, num2 * 0.3f, num2 * 1.3f);
            }
            else if (dust1.type == 114)
            {
              float num2 = dust1.scale * 0.1f;
              if ((double) num2 > 1.0)
                num2 = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 1.2f, num2 * 0.5f, num2 * 0.4f);
            }
            else if (dust1.type == 66)
            {
              if (dust1.velocity.X < 0.0)
                --dust1.rotation;
              else
                ++dust1.rotation;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num2 = (double) ^(float&) local1 * 0.980000019073486;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num2;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local2 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num3 = (double) ^(float&) local2 * 0.980000019073486;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local2 = (float) num3;
              dust1.scale += 0.02f;
              float num4 = dust1.scale;
              if (dust1.type != 15)
                num4 = dust1.scale * 0.8f;
              if ((double) num4 > 1.0)
                num4 = 1f;
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num4 * ((float) ((Color) @dust1.color).get_R() / (float) byte.MaxValue), num4 * ((float) ((Color) @dust1.color).get_G() / (float) byte.MaxValue), num4 * ((float) ((Color) @dust1.color).get_B() / (float) byte.MaxValue));
            }
            else if (dust1.type == 267)
            {
              if (dust1.velocity.X < 0.0)
                --dust1.rotation;
              else
                ++dust1.rotation;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num2 = (double) ^(float&) local1 * 0.980000019073486;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num2;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local2 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num3 = (double) ^(float&) local2 * 0.980000019073486;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local2 = (float) num3;
              dust1.scale += 0.02f;
              float num4 = dust1.scale * 0.8f;
              if ((double) num4 > 1.0)
                num4 = 1f;
              if (dust1.noLight)
                dust1.noLight = false;
              if (!dust1.noLight)
              {
                // ISSUE: explicit reference operation
                // ISSUE: explicit reference operation
                // ISSUE: explicit reference operation
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num4 * ((float) ((Color) @dust1.color).get_R() / (float) byte.MaxValue), num4 * ((float) ((Color) @dust1.color).get_G() / (float) byte.MaxValue), num4 * ((float) ((Color) @dust1.color).get_B() / (float) byte.MaxValue));
              }
            }
            else if (dust1.type == 20 || dust1.type == 21 || dust1.type == 231)
            {
              dust1.scale += 0.005f;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num2 = (double) ^(float&) local1 * 0.939999997615814;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num2;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local2 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num3 = (double) ^(float&) local2 * 0.939999997615814;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local2 = (float) num3;
              float B1 = dust1.scale * 0.8f;
              if ((double) B1 > 1.0)
                B1 = 1f;
              if (dust1.type == 21)
              {
                float B2 = dust1.scale * 0.4f;
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), B2 * 0.8f, B2 * 0.3f, B2);
              }
              else if (dust1.type == 231)
              {
                float R = dust1.scale * 0.4f;
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), R, R * 0.5f, R * 0.3f);
              }
              else
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), B1 * 0.3f, B1 * 0.6f, B1);
            }
            else if (dust1.type == 27 || dust1.type == 45)
            {
              if (dust1.type == 27 && (double) dust1.fadeIn >= 100.0)
              {
                if ((double) dust1.scale >= 1.5)
                  dust1.scale -= 0.01f;
                else
                  dust1.scale -= 0.05f;
                if ((double) dust1.scale <= 0.5)
                  dust1.scale -= 0.05f;
                if ((double) dust1.scale <= 0.25)
                  dust1.scale -= 0.05f;
              }
              Dust dust3 = dust1;
              Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.94f);
              dust3.velocity = vector2_2;
              dust1.scale += 1f / 500f;
              float B = dust1.scale;
              if (dust1.noLight)
              {
                B *= 0.1f;
                dust1.scale -= 0.06f;
                if ((double) dust1.scale < 1.0)
                  dust1.scale -= 0.06f;
                if (Main.player[Main.myPlayer].wet)
                {
                  Dust dust4 = dust1;
                  Vector2 vector2_3 = Vector2.op_Addition(dust4.position, Vector2.op_Multiply(Main.player[Main.myPlayer].velocity, 0.5f));
                  dust4.position = vector2_3;
                }
                else
                {
                  Dust dust4 = dust1;
                  Vector2 vector2_3 = Vector2.op_Addition(dust4.position, Main.player[Main.myPlayer].velocity);
                  dust4.position = vector2_3;
                }
              }
              if ((double) B > 1.0)
                B = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), B * 0.6f, B * 0.2f, B);
            }
            else if (dust1.type == 55 || dust1.type == 56 || (dust1.type == 73 || dust1.type == 74))
            {
              Dust dust3 = dust1;
              Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.98f);
              dust3.velocity = vector2_2;
              float num2 = dust1.scale * 0.8f;
              if (dust1.type == 55)
              {
                if ((double) num2 > 1.0)
                  num2 = 1f;
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2, num2, num2 * 0.6f);
              }
              else if (dust1.type == 73)
              {
                if ((double) num2 > 1.0)
                  num2 = 1f;
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2, num2 * 0.35f, num2 * 0.5f);
              }
              else if (dust1.type == 74)
              {
                if ((double) num2 > 1.0)
                  num2 = 1f;
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.35f, num2, num2 * 0.5f);
              }
              else
              {
                float B = dust1.scale * 1.2f;
                if ((double) B > 1.0)
                  B = 1f;
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), B * 0.35f, B * 0.5f, B);
              }
            }
            else if (dust1.type == 71 || dust1.type == 72)
            {
              Dust dust3 = dust1;
              Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.98f);
              dust3.velocity = vector2_2;
              float num2 = dust1.scale;
              if ((double) num2 > 1.0)
                num2 = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.2f, 0.0f, num2 * 0.1f);
            }
            else if (dust1.type == 76)
            {
              ++Main.snowDust;
              dust1.scale += 0.009f;
              float y = (float) Main.player[Main.myPlayer].velocity.Y;
              if ((double) y > 0.0 && (double) dust1.fadeIn == 0.0 && dust1.velocity.Y < (double) y)
                dust1.velocity.Y = (__Null) (double) MathHelper.Lerp((float) dust1.velocity.Y, y, 0.04f);
              if (!dust1.noLight && (double) y > 0.0)
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local = @dust1.position.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local + Main.player[Main.myPlayer].velocity.Y * 0.200000002980232;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local = (float) num2;
              }
              if (Collision.SolidCollision(Vector2.op_Subtraction(dust1.position, Vector2.op_Multiply(Vector2.get_One(), 5f)), 10, 10) && (double) dust1.fadeIn == 0.0)
              {
                dust1.scale *= 0.9f;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.25f);
                dust3.velocity = vector2_2;
              }
            }
            else if (dust1.type == 270)
            {
              Dust dust3 = dust1;
              Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 1.005025f);
              dust3.velocity = vector2_2;
              dust1.scale += 0.01f;
              dust1.rotation = 0.0f;
              if (Collision.SolidCollision(Vector2.op_Subtraction(dust1.position, Vector2.op_Multiply(Vector2.get_One(), 5f)), 10, 10) && (double) dust1.fadeIn == 0.0)
              {
                dust1.scale *= 0.95f;
                Dust dust4 = dust1;
                Vector2 vector2_3 = Vector2.op_Multiply(dust4.velocity, 0.25f);
                dust4.velocity = vector2_3;
              }
              else
              {
                dust1.velocity.Y = (__Null) (Math.Sin(dust1.position.X * 0.0043982295319438) * 2.0);
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local1 = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local1 - 3.0;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local1 = (float) num2;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local2 = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num3 = (double) ^(float&) local2 / 20.0;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local2 = (float) num3;
              }
            }
            else if (dust1.type == 271)
            {
              Dust dust3 = dust1;
              Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 1.005025f);
              dust3.velocity = vector2_2;
              dust1.scale += 3f / 1000f;
              dust1.rotation = 0.0f;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num2 = (double) ^(float&) local1 - 4.0;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num2;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local2 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num3 = (double) ^(float&) local2 / 6.0;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local2 = (float) num3;
            }
            else if (dust1.type == 268)
            {
              ++Dust.SandStormCount;
              Dust dust3 = dust1;
              Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 1.005025f);
              dust3.velocity = vector2_2;
              dust1.scale += 0.01f;
              if (!flag)
                dust1.scale -= 0.05f;
              dust1.rotation = 0.0f;
              float y = (float) Main.player[Main.myPlayer].velocity.Y;
              if ((double) y > 0.0 && (double) dust1.fadeIn == 0.0 && dust1.velocity.Y < (double) y)
                dust1.velocity.Y = (__Null) (double) MathHelper.Lerp((float) dust1.velocity.Y, y, 0.04f);
              if (!dust1.noLight && (double) y > 0.0)
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local = @dust1.position.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local + (double) y * 0.200000002980232;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local = (float) num2;
              }
              if (Collision.SolidCollision(Vector2.op_Subtraction(dust1.position, Vector2.op_Multiply(Vector2.get_One(), 5f)), 10, 10) && (double) dust1.fadeIn == 0.0)
              {
                dust1.scale *= 0.9f;
                Dust dust4 = dust1;
                Vector2 vector2_3 = Vector2.op_Multiply(dust4.velocity, 0.25f);
                dust4.velocity = vector2_3;
              }
              else
              {
                dust1.velocity.Y = (__Null) (Math.Sin(dust1.position.X * 0.0043982295319438) * 2.0);
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local + 3.0;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local = (float) num2;
              }
            }
            else if (!dust1.noGravity && dust1.type != 41 && dust1.type != 44)
            {
              if (dust1.type == 107)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.9f);
                dust3.velocity = vector2_2;
              }
              else
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local + 0.100000001490116;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local = (float) num2;
              }
            }
            if (dust1.type == 5 || dust1.type == 273 && dust1.noGravity)
              dust1.scale -= 0.04f;
            if (dust1.type == 33 || dust1.type == 52 || (dust1.type == 266 || dust1.type == 98) || (dust1.type == 99 || dust1.type == 100 || (dust1.type == 101 || dust1.type == 102)) || (dust1.type == 103 || dust1.type == 104 || (dust1.type == 105 || dust1.type == 123)))
            {
              if (dust1.velocity.X == 0.0)
              {
                if (Collision.SolidCollision(dust1.position, 2, 2))
                  dust1.scale = 0.0f;
                dust1.rotation += 0.5f;
                dust1.scale -= 0.01f;
              }
              if (Collision.WetCollision(new Vector2((float) dust1.position.X, (float) dust1.position.Y), 4, 4))
              {
                dust1.alpha += 20;
                dust1.scale -= 0.1f;
              }
              dust1.alpha += 2;
              dust1.scale -= 0.005f;
              if (dust1.alpha > (int) byte.MaxValue)
                dust1.scale = 0.0f;
              if (dust1.velocity.Y > 4.0)
                dust1.velocity.Y = (__Null) 4.0;
              if (dust1.noGravity)
              {
                if (dust1.velocity.X < 0.0)
                  dust1.rotation -= 0.2f;
                else
                  dust1.rotation += 0.2f;
                dust1.scale += 0.03f;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local1 = @dust1.velocity.X;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local1 * 1.04999995231628;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local1 = (float) num2;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local2 = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num3 = (double) ^(float&) local2 + 0.150000005960464;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local2 = (float) num3;
              }
            }
            if (dust1.type == 35 && dust1.noGravity)
            {
              dust1.scale += 0.03f;
              if ((double) dust1.scale < 1.0)
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local + 0.0750000029802322;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local = (float) num2;
              }
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num3 = (double) ^(float&) local1 * 1.08000004291534;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num3;
              if (dust1.velocity.X > 0.0)
                dust1.rotation += 0.01f;
              else
                dust1.rotation -= 0.01f;
              float R = dust1.scale * 0.6f;
              if ((double) R > 1.0)
                R = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0 + 1.0), R, R * 0.3f, R * 0.1f);
            }
            else if (dust1.type == 152 && dust1.noGravity)
            {
              dust1.scale += 0.03f;
              if ((double) dust1.scale < 1.0)
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local + 0.0750000029802322;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local = (float) num2;
              }
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num3 = (double) ^(float&) local1 * 1.08000004291534;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num3;
              if (dust1.velocity.X > 0.0)
                dust1.rotation += 0.01f;
              else
                dust1.rotation -= 0.01f;
            }
            else if (dust1.type == 67 || dust1.type == 92)
            {
              float B = dust1.scale;
              if ((double) B > 1.0)
                B = 1f;
              if (dust1.noLight)
                B *= 0.1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), 0.0f, B * 0.8f, B);
            }
            else if (dust1.type == 185)
            {
              float B = dust1.scale;
              if ((double) B > 1.0)
                B = 1f;
              if (dust1.noLight)
                B *= 0.1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), B * 0.1f, B * 0.7f, B);
            }
            else if (dust1.type == 107)
            {
              float G = dust1.scale * 0.5f;
              if ((double) G > 1.0)
                G = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), G * 0.1f, G, G * 0.4f);
            }
            else if (dust1.type == 34 || dust1.type == 35 || dust1.type == 152)
            {
              if (!Collision.WetCollision(new Vector2((float) dust1.position.X, (float) (dust1.position.Y - 8.0)), 4, 4))
              {
                dust1.scale = 0.0f;
              }
              else
              {
                dust1.alpha += Main.rand.Next(2);
                if (dust1.alpha > (int) byte.MaxValue)
                  dust1.scale = 0.0f;
                dust1.velocity.Y = (__Null) -0.5;
                if (dust1.type == 34)
                {
                  dust1.scale += 0.005f;
                }
                else
                {
                  ++dust1.alpha;
                  dust1.scale -= 0.01f;
                  dust1.velocity.Y = (__Null) -0.200000002980232;
                }
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local = @dust1.velocity.X;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num2 = (double) ^(float&) local + (double) Main.rand.Next(-10, 10) * (1.0 / 500.0);
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local = (float) num2;
                if ((double) dust1.velocity.X < -0.25)
                  dust1.velocity.X = (__Null) -0.25;
                if ((double) dust1.velocity.X > 0.25)
                  dust1.velocity.X = (__Null) 0.25;
              }
              if (dust1.type == 35)
              {
                float R = (float) ((double) dust1.scale * 0.300000011920929 + 0.400000005960464);
                if ((double) R > 1.0)
                  R = 1f;
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), R, R * 0.5f, R * 0.3f);
              }
            }
            if (dust1.type == 68)
            {
              float B = dust1.scale * 0.3f;
              if ((double) B > 1.0)
                B = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), B * 0.1f, B * 0.2f, B);
            }
            if (dust1.type == 70)
            {
              float B = dust1.scale * 0.3f;
              if ((double) B > 1.0)
                B = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), B * 0.5f, 0.0f, B);
            }
            if (dust1.type == 41)
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num2 = (double) ^(float&) local1 + (double) Main.rand.Next(-10, 11) * 0.00999999977648258;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num2;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local2 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num3 = (double) ^(float&) local2 + (double) Main.rand.Next(-10, 11) * 0.00999999977648258;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local2 = (float) num3;
              if ((double) dust1.velocity.X > 0.75)
                dust1.velocity.X = (__Null) 0.75;
              if ((double) dust1.velocity.X < -0.75)
                dust1.velocity.X = (__Null) -0.75;
              if ((double) dust1.velocity.Y > 0.75)
                dust1.velocity.Y = (__Null) 0.75;
              if ((double) dust1.velocity.Y < -0.75)
                dust1.velocity.Y = (__Null) -0.75;
              dust1.scale += 0.007f;
              float B = dust1.scale * 0.7f;
              if ((double) B > 1.0)
                B = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), B * 0.4f, B * 0.9f, B);
            }
            else if (dust1.type == 44)
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num2 = (double) ^(float&) local1 + (double) Main.rand.Next(-10, 11) * (3.0 / 1000.0);
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num2;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local2 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num3 = (double) ^(float&) local2 + (double) Main.rand.Next(-10, 11) * (3.0 / 1000.0);
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local2 = (float) num3;
              if ((double) dust1.velocity.X > 0.35)
                dust1.velocity.X = (__Null) 0.349999994039536;
              if ((double) dust1.velocity.X < -0.35)
                dust1.velocity.X = (__Null) -0.349999994039536;
              if ((double) dust1.velocity.Y > 0.35)
                dust1.velocity.Y = (__Null) 0.349999994039536;
              if ((double) dust1.velocity.Y < -0.35)
                dust1.velocity.Y = (__Null) -0.349999994039536;
              dust1.scale += 0.0085f;
              float G = dust1.scale * 0.7f;
              if ((double) G > 1.0)
                G = 1f;
              Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), G * 0.7f, G, G * 0.8f);
            }
            else
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num2 = (double) ^(float&) local * 0.990000009536743;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local = (float) num2;
            }
            if (dust1.type != 79 && dust1.type != 268)
              dust1.rotation += (float) (dust1.velocity.X * 0.5);
            if ((double) dust1.fadeIn > 0.0 && (double) dust1.fadeIn < 100.0)
            {
              if (dust1.type == 235)
              {
                dust1.scale += 0.007f;
                int index2 = (int) dust1.fadeIn - 1;
                if (index2 >= 0 && index2 <= (int) byte.MaxValue)
                {
                  Vector2 vector2_2 = Vector2.op_Subtraction(dust1.position, Main.player[index2].Center);
                  // ISSUE: explicit reference operation
                  float num2 = 100f - ((Vector2) @vector2_2).Length();
                  if ((double) num2 > 0.0)
                    dust1.scale -= num2 * 0.0015f;
                  // ISSUE: explicit reference operation
                  ((Vector2) @vector2_2).Normalize();
                  float num3 = (float) ((1.0 - (double) dust1.scale) * 20.0);
                  Vector2 vector2_3 = Vector2.op_Multiply(vector2_2, -num3);
                  dust1.velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(dust1.velocity, 4f), vector2_3), 5f);
                }
              }
              else if (dust1.type == 46)
                dust1.scale += 0.1f;
              else if (dust1.type == 213 || dust1.type == 260)
                dust1.scale += 0.1f;
              else
                dust1.scale += 0.03f;
              if ((double) dust1.scale > (double) dust1.fadeIn)
                dust1.fadeIn = 0.0f;
            }
            else if (dust1.type == 213 || dust1.type == 260)
              dust1.scale -= 0.2f;
            else
              dust1.scale -= 0.01f;
            if (dust1.type >= 130 && dust1.type <= 134)
            {
              float num2 = dust1.scale;
              if ((double) num2 > 1.0)
                num2 = 1f;
              if (dust1.type == 130)
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 1f, num2 * 0.5f, num2 * 0.4f);
              if (dust1.type == 131)
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.4f, num2 * 1f, num2 * 0.6f);
              if (dust1.type == 132)
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.3f, num2 * 0.5f, num2 * 1f);
              if (dust1.type == 133)
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.9f, num2 * 0.9f, num2 * 0.3f);
              if (dust1.noGravity)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.93f);
                dust3.velocity = vector2_2;
                if ((double) dust1.fadeIn == 0.0)
                  dust1.scale += 1f / 400f;
              }
              else if (dust1.type == 131)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.98f);
                dust3.velocity = vector2_2;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                __Null& local = @dust1.velocity.Y;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                double num3 = (double) ^(float&) local - 0.100000001490116;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) local = (float) num3;
                dust1.scale += 1f / 400f;
              }
              else
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.95f);
                dust3.velocity = vector2_2;
                dust1.scale -= 1f / 400f;
              }
            }
            else if (dust1.type >= 219 && dust1.type <= 223)
            {
              float num2 = dust1.scale;
              if ((double) num2 > 1.0)
                num2 = 1f;
              if (!dust1.noLight)
              {
                if (dust1.type == 219)
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 1f, num2 * 0.5f, num2 * 0.4f);
                if (dust1.type == 220)
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.4f, num2 * 1f, num2 * 0.6f);
                if (dust1.type == 221)
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.3f, num2 * 0.5f, num2 * 1f);
                if (dust1.type == 222)
                  Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.9f, num2 * 0.9f, num2 * 0.3f);
              }
              if (dust1.noGravity)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.93f);
                dust3.velocity = vector2_2;
                if ((double) dust1.fadeIn == 0.0)
                  dust1.scale += 1f / 400f;
              }
              Dust dust4 = dust1;
              Vector2 vector2_3 = Vector2.op_Multiply(dust4.velocity, new Vector2(0.97f, 0.99f));
              dust4.velocity = vector2_3;
              dust1.scale -= 1f / 400f;
              if (dust1.customData != null && dust1.customData is Player)
              {
                Player customData = (Player) dust1.customData;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPosition));
                dust3.position = vector2_2;
              }
            }
            else if (dust1.type == 226)
            {
              float num2 = dust1.scale;
              if ((double) num2 > 1.0)
                num2 = 1f;
              if (!dust1.noLight)
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.2f, num2 * 0.7f, num2 * 1f);
              if (dust1.noGravity)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.93f);
                dust3.velocity = vector2_2;
                if ((double) dust1.fadeIn == 0.0)
                  dust1.scale += 1f / 400f;
              }
              Dust dust4 = dust1;
              Vector2 vector2_3 = Vector2.op_Multiply(dust4.velocity, new Vector2(0.97f, 0.99f));
              dust4.velocity = vector2_3;
              if (dust1.customData != null && dust1.customData is Player)
              {
                Player customData = (Player) dust1.customData;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPosition));
                dust3.position = vector2_2;
              }
              dust1.scale -= 0.01f;
            }
            else if (dust1.type == 272)
            {
              float num2 = dust1.scale;
              if ((double) num2 > 1.0)
                num2 = 1f;
              if (!dust1.noLight)
                Lighting.AddLight((int) (dust1.position.X / 16.0), (int) (dust1.position.Y / 16.0), num2 * 0.5f, num2 * 0.2f, num2 * 0.8f);
              if (dust1.noGravity)
              {
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.93f);
                dust3.velocity = vector2_2;
                if ((double) dust1.fadeIn == 0.0)
                  dust1.scale += 1f / 400f;
              }
              Dust dust4 = dust1;
              Vector2 vector2_3 = Vector2.op_Multiply(dust4.velocity, new Vector2(0.97f, 0.99f));
              dust4.velocity = vector2_3;
              if (dust1.customData != null && dust1.customData is Player)
              {
                Player customData = (Player) dust1.customData;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPosition));
                dust3.position = vector2_2;
              }
              if (dust1.customData != null && dust1.customData is NPC)
              {
                NPC customData = (NPC) dust1.customData;
                Dust dust3 = dust1;
                Vector2 vector2_2 = Vector2.op_Addition(dust3.position, Vector2.op_Subtraction(customData.position, customData.oldPosition));
                dust3.position = vector2_2;
              }
              dust1.scale -= 0.01f;
            }
            else if (dust1.noGravity)
            {
              Dust dust3 = dust1;
              Vector2 vector2_2 = Vector2.op_Multiply(dust3.velocity, 0.92f);
              dust3.velocity = vector2_2;
              if ((double) dust1.fadeIn == 0.0)
                dust1.scale -= 0.04f;
            }
            if (dust1.position.Y > Main.screenPosition.Y + (double) Main.screenHeight)
              dust1.active = false;
            float num10 = 0.1f;
            if ((double) Dust.dCount == 0.5)
              dust1.scale -= 1f / 1000f;
            if ((double) Dust.dCount == 0.6)
              dust1.scale -= 1f / 400f;
            if ((double) Dust.dCount == 0.7)
              dust1.scale -= 0.005f;
            if ((double) Dust.dCount == 0.8)
              dust1.scale -= 0.01f;
            if ((double) Dust.dCount == 0.9)
              dust1.scale -= 0.02f;
            if ((double) Dust.dCount == 0.5)
              num10 = 0.11f;
            if ((double) Dust.dCount == 0.6)
              num10 = 0.13f;
            if ((double) Dust.dCount == 0.7)
              num10 = 0.16f;
            if ((double) Dust.dCount == 0.8)
              num10 = 0.22f;
            if ((double) Dust.dCount == 0.9)
              num10 = 0.25f;
            if ((double) dust1.scale < (double) num10)
              dust1.active = false;
          }
        }
        else
          dust1.active = false;
      }
      int num11 = num1;
      if ((double) num11 > (double) Main.maxDustToDraw * 0.9)
        Dust.dCount = 0.9f;
      else if ((double) num11 > (double) Main.maxDustToDraw * 0.8)
        Dust.dCount = 0.8f;
      else if ((double) num11 > (double) Main.maxDustToDraw * 0.7)
        Dust.dCount = 0.7f;
      else if ((double) num11 > (double) Main.maxDustToDraw * 0.6)
        Dust.dCount = 0.6f;
      else if ((double) num11 > (double) Main.maxDustToDraw * 0.5)
        Dust.dCount = 0.5f;
      else
        Dust.dCount = 0.0f;
    }

    public Color GetAlpha(Color newColor)
    {
      float num1 = (float) ((int) byte.MaxValue - this.alpha) / (float) byte.MaxValue;
      if (this.type == 259)
        return new Color(230, 230, 230, 230);
      if (this.type == 261)
        return new Color(230, 230, 230, 115);
      if (this.type == 254 || this.type == (int) byte.MaxValue)
        return new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      if (this.type == 258)
        return new Color(150, 50, 50, 0);
      if (this.type == 263 || this.type == 264)
      {
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        return Color.op_Multiply(new Color((int) ((Color) @this.color).get_R() / 2 + (int) sbyte.MaxValue, (int) ((Color) @this.color).get_G() + (int) sbyte.MaxValue, (int) ((Color) @this.color).get_B() + (int) sbyte.MaxValue, (int) ((Color) @this.color).get_A() / 8), 0.5f);
      }
      if (this.type == 235)
        return new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      if ((this.type >= 86 && this.type <= 91 || this.type == 262) && !this.noLight)
        return new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      if (this.type == 213 || this.type == 260)
      {
        int num2 = (int) ((double) this.scale / 2.5 * (double) byte.MaxValue);
        return new Color(num2, num2, num2, num2);
      }
      if (this.type == 64 && this.alpha == (int) byte.MaxValue && this.noLight)
        return new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      if (this.type == 197)
        return new Color(250, 250, 250, 150);
      if (this.type >= 110 && this.type <= 114)
        return new Color(200, 200, 200, 0);
      if (this.type == 204)
        return new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      if (this.type == 181)
        return new Color(200, 200, 200, 0);
      if (this.type == 182 || this.type == 206)
        return new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      if (this.type == 159)
        return new Color(250, 250, 250, 50);
      if (this.type == 163 || this.type == 205)
        return new Color(250, 250, 250, 0);
      if (this.type == 170)
        return new Color(200, 200, 200, 100);
      if (this.type == 180)
        return new Color(200, 200, 200, 0);
      if (this.type == 175)
        return new Color(200, 200, 200, 0);
      if (this.type == 183)
        return new Color(50, 0, 0, 0);
      if (this.type == 172)
        return new Color(250, 250, 250, 150);
      if (this.type == 160 || this.type == 162 || (this.type == 164 || this.type == 173))
      {
        int num2 = (int) (250.0 * (double) this.scale);
        int num3 = 0;
        return new Color(num2, num2, num2, num3);
      }
      if (this.type == 92 || this.type == 106 || this.type == 107)
        return new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      if (this.type == 185)
        return new Color(200, 200, (int) byte.MaxValue, 125);
      if (this.type == (int) sbyte.MaxValue || this.type == 187)
      {
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        return new Color((int) ((Color) @newColor).get_R(), (int) ((Color) @newColor).get_G(), (int) ((Color) @newColor).get_B(), 25);
      }
      if (this.type == 156 || this.type == 230 || this.type == 234)
        return new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      if (this.type == 270)
      {
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        return new Color((int) ((Color) @newColor).get_R() / 2 + (int) sbyte.MaxValue, (int) ((Color) @newColor).get_G() / 2 + (int) sbyte.MaxValue, (int) ((Color) @newColor).get_B() / 2 + (int) sbyte.MaxValue, 25);
      }
      if (this.type == 271)
      {
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        return new Color((int) ((Color) @newColor).get_R() / 2 + (int) sbyte.MaxValue, (int) ((Color) @newColor).get_G() / 2 + (int) sbyte.MaxValue, (int) ((Color) @newColor).get_B() / 2 + (int) sbyte.MaxValue, (int) sbyte.MaxValue);
      }
      if (this.type == 6 || this.type == 242 || (this.type == 174 || this.type == 135) || (this.type == 75 || this.type == 20 || (this.type == 21 || this.type == 231)) || (this.type == 169 || this.type >= 130 && this.type <= 134 || this.type == 158))
      {
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        return new Color((int) ((Color) @newColor).get_R(), (int) ((Color) @newColor).get_G(), (int) ((Color) @newColor).get_B(), 25);
      }
      if (this.type >= 219 && this.type <= 223)
      {
        newColor = Color.Lerp(newColor, Color.get_White(), 0.5f);
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        return new Color((int) ((Color) @newColor).get_R(), (int) ((Color) @newColor).get_G(), (int) ((Color) @newColor).get_B(), 25);
      }
      if (this.type == 226 || this.type == 272)
      {
        newColor = Color.Lerp(newColor, Color.get_White(), 0.8f);
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        return new Color((int) ((Color) @newColor).get_R(), (int) ((Color) @newColor).get_G(), (int) ((Color) @newColor).get_B(), 25);
      }
      if (this.type == 228)
      {
        newColor = Color.Lerp(newColor, Color.get_White(), 0.8f);
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        return new Color((int) ((Color) @newColor).get_R(), (int) ((Color) @newColor).get_G(), (int) ((Color) @newColor).get_B(), 25);
      }
      if (this.type == 229 || this.type == 269)
      {
        newColor = Color.Lerp(newColor, Color.get_White(), 0.6f);
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        return new Color((int) ((Color) @newColor).get_R(), (int) ((Color) @newColor).get_G(), (int) ((Color) @newColor).get_B(), 25);
      }
      if ((this.type == 68 || this.type == 70) && this.noGravity)
        return new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      if (this.type == 157)
      {
        int maxValue;
        int num2 = maxValue = (int) byte.MaxValue;
        int num3 = maxValue;
        int num4 = maxValue;
        float num5 = (float) ((double) Main.mouseTextColor / 100.0 - 1.60000002384186);
        int num6 = (int) ((double) num4 * (double) num5);
        int num7 = (int) ((double) num3 * (double) num5);
        int num8 = (int) ((double) num2 * (double) num5);
        int num9 = (int) (100.0 * (double) num5);
        int num10 = num6 + 50;
        if (num10 > (int) byte.MaxValue)
          num10 = (int) byte.MaxValue;
        int num11 = num7 + 50;
        if (num11 > (int) byte.MaxValue)
          num11 = (int) byte.MaxValue;
        int num12 = num8 + 50;
        if (num12 > (int) byte.MaxValue)
          num12 = (int) byte.MaxValue;
        return new Color(num10, num11, num12, num9);
      }
      if (this.type == 15 || this.type == 274 || (this.type == 20 || this.type == 21) || (this.type == 29 || this.type == 35 || (this.type == 41 || this.type == 44)) || (this.type == 27 || this.type == 45 || (this.type == 55 || this.type == 56) || (this.type == 57 || this.type == 58 || (this.type == 73 || this.type == 74))))
        num1 = (float) (((double) num1 + 3.0) / 4.0);
      else if (this.type == 43)
      {
        num1 = (float) (((double) num1 + 9.0) / 10.0);
      }
      else
      {
        if (this.type >= 244 && this.type <= 247)
          return new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
        if (this.type == 66)
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          return new Color((int) ((Color) @newColor).get_R(), (int) ((Color) @newColor).get_G(), (int) ((Color) @newColor).get_B(), 0);
        }
        if (this.type == 267)
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          return new Color((int) ((Color) @this.color).get_R(), (int) ((Color) @this.color).get_G(), (int) ((Color) @this.color).get_B(), 0);
        }
        if (this.type == 71)
          return new Color(200, 200, 200, 0);
        if (this.type == 72)
          return new Color(200, 200, 200, 200);
      }
      // ISSUE: explicit reference operation
      int num13 = (int) ((double) ((Color) @newColor).get_R() * (double) num1);
      // ISSUE: explicit reference operation
      int num14 = (int) ((double) ((Color) @newColor).get_G() * (double) num1);
      // ISSUE: explicit reference operation
      int num15 = (int) ((double) ((Color) @newColor).get_B() * (double) num1);
      // ISSUE: explicit reference operation
      int num16 = (int) ((Color) @newColor).get_A() - this.alpha;
      if (num16 < 0)
        num16 = 0;
      if (num16 > (int) byte.MaxValue)
        num16 = (int) byte.MaxValue;
      return new Color(num13, num14, num15, num16);
    }

    public Color GetColor(Color newColor)
    {
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      int num1 = (int) ((Color) @this.color).get_R() - ((int) byte.MaxValue - (int) ((Color) @newColor).get_R());
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      int num2 = (int) ((Color) @this.color).get_G() - ((int) byte.MaxValue - (int) ((Color) @newColor).get_G());
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      int num3 = (int) ((Color) @this.color).get_B() - ((int) byte.MaxValue - (int) ((Color) @newColor).get_B());
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      int num4 = (int) ((Color) @this.color).get_A() - ((int) byte.MaxValue - (int) ((Color) @newColor).get_A());
      if (num1 < 0)
        num1 = 0;
      if (num1 > (int) byte.MaxValue)
        num1 = (int) byte.MaxValue;
      if (num2 < 0)
        num2 = 0;
      if (num2 > (int) byte.MaxValue)
        num2 = (int) byte.MaxValue;
      if (num3 < 0)
        num3 = 0;
      if (num3 > (int) byte.MaxValue)
        num3 = (int) byte.MaxValue;
      if (num4 < 0)
        num4 = 0;
      if (num4 > (int) byte.MaxValue)
        num4 = (int) byte.MaxValue;
      return new Color(num1, num2, num3, num4);
    }
  }
}
