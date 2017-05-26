// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Events.Sandstorm
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.Utilities;

namespace Terraria.GameContent.Events
{
  public class Sandstorm
  {
    public static bool Happening;
    public static int TimeLeft;
    public static float Severity;
    public static float IntendedSeverity;
    private static bool _effectsUp;

    public static void WorldClear()
    {
      Sandstorm.Happening = false;
    }

    public static void UpdateTime()
    {
      if (Main.netMode != 1)
      {
        if (Sandstorm.Happening)
        {
          if (Sandstorm.TimeLeft > 86400)
            Sandstorm.TimeLeft = 0;
          Sandstorm.TimeLeft -= Main.dayRate;
          if (Sandstorm.TimeLeft <= 0)
            Sandstorm.StopSandstorm();
        }
        else
        {
          int num = (int) ((double) Main.windSpeed * 100.0);
          for (int index = 0; index < Main.dayRate; ++index)
          {
            if (Main.rand.Next(777600) == 0)
              Sandstorm.StartSandstorm();
            else if ((Main.numClouds < 40 || Math.Abs(num) > 50) && Main.rand.Next(518400) == 0)
              Sandstorm.StartSandstorm();
          }
        }
        if (Main.rand.Next(18000) == 0)
          Sandstorm.ChangeSeverityIntentions();
      }
      Sandstorm.UpdateSeverity();
    }

    private static void ChangeSeverityIntentions()
    {
      Sandstorm.IntendedSeverity = !Sandstorm.Happening ? (Main.rand.Next(3) != 0 ? Main.rand.NextFloat() * 0.3f : 0.0f) : 0.4f + Main.rand.NextFloat();
      if (Main.netMode == 1)
        return;
      NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }

    private static void UpdateSeverity()
    {
      int num1 = Math.Sign(Sandstorm.IntendedSeverity - Sandstorm.Severity);
      Sandstorm.Severity = MathHelper.Clamp(Sandstorm.Severity + 3f / 1000f * (float) num1, 0.0f, 1f);
      int num2 = Math.Sign(Sandstorm.IntendedSeverity - Sandstorm.Severity);
      if (num1 == num2)
        return;
      Sandstorm.Severity = Sandstorm.IntendedSeverity;
    }

    private static void StartSandstorm()
    {
      Sandstorm.Happening = true;
      Sandstorm.TimeLeft = (int) (3600.0 * (8.0 + (double) Main.rand.NextFloat() * 16.0));
      Sandstorm.ChangeSeverityIntentions();
    }

    private static void StopSandstorm()
    {
      Sandstorm.Happening = false;
      Sandstorm.TimeLeft = 0;
      Sandstorm.ChangeSeverityIntentions();
    }

    public static void HandleEffectAndSky(bool toState)
    {
      if (toState == Sandstorm._effectsUp)
        return;
      Sandstorm._effectsUp = toState;
      Vector2 center = Main.player[Main.myPlayer].Center;
      if (Sandstorm._effectsUp)
      {
        SkyManager.Instance.Activate("Sandstorm", center);
        Filters.Scene.Activate("Sandstorm", center);
        Overlays.Scene.Activate("Sandstorm", center);
      }
      else
      {
        SkyManager.Instance.Deactivate("Sandstorm");
        Filters.Scene.Deactivate("Sandstorm");
        Overlays.Scene.Deactivate("Sandstorm");
      }
    }

    public static void EmitDust()
    {
      if (Main.gamePaused)
        return;
      int sandTiles = Main.sandTiles;
      Player player = Main.player[Main.myPlayer];
      bool flag = Sandstorm.Happening && player.ZoneSandstorm && ((Main.bgStyle == 2 || Main.bgStyle == 5) && Main.bgDelay < 50);
      Sandstorm.HandleEffectAndSky(flag && Main.UseStormEffects);
      if (sandTiles < 100 || (double) player.position.Y > Main.worldSurface * 16.0 || player.ZoneBeach)
        return;
      int maxValue1 = 1;
      if (!flag || Main.rand.Next(maxValue1) != 0)
        return;
      int num1 = Math.Sign(Main.windSpeed);
      float num2 = Math.Abs(Main.windSpeed);
      if ((double) num2 < 0.00999999977648258)
        return;
      float num3 = (float) num1 * MathHelper.Lerp(0.9f, 1f, num2);
      float num4 = 2000f / (float) sandTiles;
      float num5 = MathHelper.Clamp(3f / num4, 0.77f, 1f);
      int num6 = (int) num4;
      int num7 = (int) (1000.0 * (double) ((float) Main.screenWidth / (float) Main.maxScreenW));
      float num8 = 20f * Sandstorm.Severity;
      float num9 = (float) ((double) num7 * ((double) Main.gfxQuality * 0.5 + 0.5) + (double) num7 * 0.100000001490116) - (float) Dust.SandStormCount;
      if ((double) num9 <= 0.0)
        return;
      float num10 = (float) Main.screenWidth + 1000f;
      float screenHeight = (float) Main.screenHeight;
      Vector2 vector2_1 = Vector2.op_Addition(Main.screenPosition, player.velocity);
      WeightedRandom<Color> weightedRandom = new WeightedRandom<Color>();
      weightedRandom.Add(new Color(200, 160, 20, 180), (double) (Main.screenTileCounts[53] + Main.screenTileCounts[396] + Main.screenTileCounts[397]));
      weightedRandom.Add(new Color(103, 98, 122, 180), (double) (Main.screenTileCounts[112] + Main.screenTileCounts[400] + Main.screenTileCounts[398]));
      weightedRandom.Add(new Color(135, 43, 34, 180), (double) (Main.screenTileCounts[234] + Main.screenTileCounts[401] + Main.screenTileCounts[399]));
      weightedRandom.Add(new Color(213, 196, 197, 180), (double) (Main.screenTileCounts[116] + Main.screenTileCounts[403] + Main.screenTileCounts[402]));
      float num11 = MathHelper.Lerp(0.2f, 0.35f, Sandstorm.Severity);
      float num12 = MathHelper.Lerp(0.5f, 0.7f, Sandstorm.Severity);
      int maxValue2 = (int) MathHelper.Lerp(1f, 10f, (float) (((double) num5 - 0.769999980926514) / 0.230000019073486));
      for (int index1 = 0; (double) index1 < (double) num8; ++index1)
      {
        if (Main.rand.Next(num6 / 4) == 0)
        {
          Vector2 Position;
          // ISSUE: explicit reference operation
          ((Vector2) @Position).\u002Ector((float) ((double) Main.rand.NextFloat() * (double) num10 - 500.0), Main.rand.NextFloat() * -50f);
          if (Main.rand.Next(3) == 0 && num1 == 1)
            Position.X = (__Null) (double) (Main.rand.Next(500) - 500);
          else if (Main.rand.Next(3) == 0 && num1 == -1)
            Position.X = (__Null) (double) (Main.rand.Next(500) + Main.screenWidth);
          if (Position.X < 0.0 || Position.X > (double) Main.screenWidth)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local = @Position.Y;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num13 = (double) ^(float&) local + (double) Main.rand.NextFloat() * (double) screenHeight * 0.899999976158142;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local = (float) num13;
          }
          Position = Vector2.op_Addition(Position, vector2_1);
          int index2 = (int) Position.X / 16;
          int index3 = (int) Position.Y / 16;
          if (Main.tile[index2, index3] != null && (int) Main.tile[index2, index3].wall == 0)
          {
            for (int index4 = 0; index4 < 1; ++index4)
            {
              Dust dust1 = Main.dust[Dust.NewDust(Position, 10, 10, 268, 0.0f, 0.0f, 0, (Color) null, 1f)];
              dust1.velocity.Y = (__Null) (2.0 + (double) Main.rand.NextFloat() * 0.200000002980232);
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local1 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num13 = (double) ^(float&) local1 * (double) dust1.scale;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local1 = (float) num13;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local2 = @dust1.velocity.Y;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num14 = (double) ^(float&) local2 * 0.349999994039536;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local2 = (float) num14;
              dust1.velocity.X = (__Null) ((double) num3 * 5.0 + (double) Main.rand.NextFloat() * 1.0);
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              __Null& local3 = @dust1.velocity.X;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              double num15 = (double) ^(float&) local3 + (double) num3 * (double) num12 * 20.0;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) local3 = (float) num15;
              dust1.fadeIn += num12 * 0.2f;
              Dust dust2 = dust1;
              Vector2 vector2_2 = Vector2.op_Multiply(dust2.velocity, (float) (1.0 + (double) num11 * 0.5));
              dust2.velocity = vector2_2;
              dust1.color = (Color) weightedRandom;
              Dust dust3 = dust1;
              Vector2 vector2_3 = Vector2.op_Multiply(dust3.velocity, 1f + num11);
              dust3.velocity = vector2_3;
              Dust dust4 = dust1;
              Vector2 vector2_4 = Vector2.op_Multiply(dust4.velocity, num5);
              dust4.velocity = vector2_4;
              dust1.scale = 0.9f;
              --num9;
              if ((double) num9 > 0.0)
              {
                if (Main.rand.Next(maxValue2) != 0)
                {
                  --index4;
                  Position = Vector2.op_Addition(Position, Vector2.op_Addition(Utils.RandomVector2(Main.rand, -10f, 10f), Vector2.op_Multiply(dust1.velocity, -1.1f)));
                  int x = (int) Position.X / 16;
                  int y = (int) Position.Y / 16;
                  if (WorldGen.InWorld(x, y, 10) && Main.tile[x, y] != null)
                  {
                    int wall = (int) Main.tile[x, y].wall;
                  }
                }
              }
              else
                break;
            }
            if ((double) num9 <= 0.0)
              break;
          }
        }
      }
    }

    public static void DrawGrains(SpriteBatch spriteBatch)
    {
    }
  }
}
