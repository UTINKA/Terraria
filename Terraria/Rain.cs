// Decompiled with JetBrains decompiler
// Type: Terraria.Rain
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using Terraria.Graphics.Effects;

namespace Terraria
{
  public class Rain
  {
    public Vector2 position;
    public Vector2 velocity;
    public float scale;
    public float rotation;
    public int alpha;
    public bool active;
    public byte type;

    public static void MakeRain()
    {
      if ((double) Main.screenPosition.Y > Main.worldSurface * 16.0 || Main.gameMenu)
        return;
      float num1 = (float) Main.screenWidth / 1920f * 25f * (float) (0.25 + 1.0 * (double) Main.cloudAlpha);
      if (Filters.Scene["Sandstorm"].IsActive())
        return;
      for (int index = 0; (double) index < (double) num1; ++index)
      {
        int num2 = 600;
        if (Main.player[Main.myPlayer].velocity.Y < 0.0)
          num2 += (int) ((double) Math.Abs((float) Main.player[Main.myPlayer].velocity.Y) * 30.0);
        Vector2 Position;
        Position.X = (__Null) (double) Main.rand.Next((int) Main.screenPosition.X - num2, (int) Main.screenPosition.X + Main.screenWidth + num2);
        Position.Y = (__Null) (Main.screenPosition.Y - (double) Main.rand.Next(20, 100));
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local1 = @Position;
        // ISSUE: explicit reference operation
        double num3 = (^local1).X - (double) Main.windSpeed * 15.0 * 40.0;
        // ISSUE: explicit reference operation
        (^local1).X = (__Null) num3;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local2 = @Position;
        // ISSUE: explicit reference operation
        double num4 = (^local2).X + Main.player[Main.myPlayer].velocity.X * 40.0;
        // ISSUE: explicit reference operation
        (^local2).X = (__Null) num4;
        if (Position.X < 0.0)
          Position.X = (__Null) 0.0;
        if (Position.X > (double) ((Main.maxTilesX - 1) * 16))
          Position.X = (__Null) (double) ((Main.maxTilesX - 1) * 16);
        int i = (int) Position.X / 16;
        int j = (int) Position.Y / 16;
        if (i < 0)
          i = 0;
        if (i > Main.maxTilesX - 1)
          i = Main.maxTilesX - 1;
        if (Main.gameMenu || !WorldGen.SolidTile(i, j) && (int) Main.tile[i, j].wall <= 0)
        {
          Vector2 Velocity;
          // ISSUE: explicit reference operation
          ((Vector2) @Velocity).\u002Ector(Main.windSpeed * 12f, 14f);
          Rain.NewRain(Position, Velocity);
        }
      }
    }

    public void Update()
    {
      Rain rain = this;
      Vector2 vector2_1 = Vector2.op_Addition(rain.position, this.velocity);
      rain.position = vector2_1;
      if (!Collision.SolidCollision(this.position, 2, 2) && this.position.Y <= Main.screenPosition.Y + (double) Main.screenHeight + 100.0 && !Collision.WetCollision(this.position, 2, 2))
        return;
      this.active = false;
      if ((double) Main.rand.Next(100) >= (double) Main.gfxQuality * 100.0)
        return;
      int Type = 154;
      if ((int) this.type == 3 || (int) this.type == 4 || (int) this.type == 5)
        Type = 218;
      int index = Dust.NewDust(Vector2.op_Subtraction(this.position, this.velocity), 2, 2, Type, 0.0f, 0.0f, 0, (Color) null, 1f);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local = @Main.dust[index].position;
      // ISSUE: explicit reference operation
      double num = (^local).X - 2.0;
      // ISSUE: explicit reference operation
      (^local).X = (__Null) num;
      Main.dust[index].alpha = 38;
      Dust dust1 = Main.dust[index];
      Vector2 vector2_2 = Vector2.op_Multiply(dust1.velocity, 0.1f);
      dust1.velocity = vector2_2;
      Dust dust2 = Main.dust[index];
      Vector2 vector2_3 = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.op_UnaryNegation(this.velocity), 0.025f));
      dust2.velocity = vector2_3;
      Main.dust[index].scale = 0.75f;
    }

    public static int NewRain(Vector2 Position, Vector2 Velocity)
    {
      int index1 = -1;
      int num1 = (int) ((double) Main.maxRain * (double) Main.cloudAlpha);
      if (num1 > Main.maxRain)
        num1 = Main.maxRain;
      float num2 = (float) Main.maxTilesX / 6400f;
      float num3 = Math.Max(0.0f, Math.Min(1f, (float) ((Main.player[Main.myPlayer].position.Y / 16.0 - 85.0 * (double) num2) / (60.0 * (double) num2))));
      float num4 = num3 * num3;
      int num5 = (int) ((double) num1 * (double) num4);
      float num6 = (float) ((1.0 + (double) Main.gfxQuality) / 2.0);
      if ((double) num6 < 0.9)
        num5 = (int) ((double) num5 * (double) num6);
      float num7 = (float) (800 - Main.snowTiles);
      if ((double) num7 < 0.0)
        num7 = 0.0f;
      float num8 = num7 / 800f;
      int num9 = (int) ((double) num5 * (double) num8);
      for (int index2 = 0; index2 < num9; ++index2)
      {
        if (!Main.rain[index2].active)
        {
          index1 = index2;
          break;
        }
      }
      if (index1 == -1)
        return Main.maxRain;
      Rain rain = Main.rain[index1];
      rain.active = true;
      rain.position = Position;
      rain.scale = (float) (1.0 + (double) Main.rand.Next(-20, 21) * 0.00999999977648258);
      rain.velocity = Vector2.op_Multiply(Velocity, rain.scale);
      rain.rotation = (float) Math.Atan2((double) rain.velocity.X, (double) -rain.velocity.Y);
      rain.type = (byte) Main.rand.Next(3);
      if (Main.bloodMoon)
        rain.type += (byte) 3;
      return index1;
    }
  }
}
