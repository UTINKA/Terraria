// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.StardustSky
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
  public class StardustSky : CustomSky
  {
    private UnifiedRandom _random = new UnifiedRandom();
    private Texture2D _planetTexture;
    private Texture2D _bgTexture;
    private Texture2D[] _starTextures;
    private bool _isActive;
    private StardustSky.Star[] _stars;
    private float _fadeOpacity;

    public override void OnLoad()
    {
      this._planetTexture = TextureManager.Load("Images/Misc/StarDustSky/Planet");
      this._bgTexture = TextureManager.Load("Images/Misc/StarDustSky/Background");
      this._starTextures = new Texture2D[2];
      for (int index = 0; index < this._starTextures.Length; ++index)
        this._starTextures[index] = TextureManager.Load("Images/Misc/StarDustSky/Star " + (object) index);
    }

    public override void Update(GameTime gameTime)
    {
      if (this._isActive)
        this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
      else
        this._fadeOpacity = Math.Max(0.0f, this._fadeOpacity - 0.01f);
    }

    public override Color OnTileColor(Color inColor)
    {
      // ISSUE: explicit reference operation
      return new Color(Vector4.Lerp(((Color) @inColor).ToVector4(), Vector4.get_One(), this._fadeOpacity * 0.5f));
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
      if ((double) maxDepth >= 3.40282346638529E+38 && (double) minDepth < 3.40282346638529E+38)
      {
        spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.op_Multiply(Color.get_Black(), this._fadeOpacity));
        spriteBatch.Draw(this._bgTexture, new Rectangle(0, Math.Max(0, (int) ((Main.worldSurface * 16.0 - (double) Main.screenPosition.Y - 2400.0) * 0.100000001490116)), Main.screenWidth, Main.screenHeight), Color.op_Multiply(Color.get_White(), Math.Min(1f, (float) ((Main.screenPosition.Y - 800.0) / 1000.0) * this._fadeOpacity)));
        Vector2 vector2_1;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2_1).\u002Ector((float) (Main.screenWidth >> 1), (float) (Main.screenHeight >> 1));
        Vector2 vector2_2 = Vector2.op_Multiply(0.01f, Vector2.op_Subtraction(new Vector2((float) Main.maxTilesX * 8f, (float) Main.worldSurface / 2f), Main.screenPosition));
        spriteBatch.Draw(this._planetTexture, Vector2.op_Addition(Vector2.op_Addition(vector2_1, new Vector2(-200f, -200f)), vector2_2), new Rectangle?(), Color.op_Multiply(Color.op_Multiply(Color.get_White(), 0.9f), this._fadeOpacity), 0.0f, new Vector2((float) (this._planetTexture.get_Width() >> 1), (float) (this._planetTexture.get_Height() >> 1)), 1f, (SpriteEffects) 0, 1f);
      }
      int num1 = -1;
      int num2 = 0;
      for (int index = 0; index < this._stars.Length; ++index)
      {
        float depth = this._stars[index].Depth;
        if (num1 == -1 && (double) depth < (double) maxDepth)
          num1 = index;
        if ((double) depth > (double) minDepth)
          num2 = index;
        else
          break;
      }
      if (num1 == -1)
        return;
      float num3 = Math.Min(1f, (float) ((Main.screenPosition.Y - 1000.0) / 1000.0));
      Vector2 vector2_3 = Vector2.op_Addition(Main.screenPosition, new Vector2((float) (Main.screenWidth >> 1), (float) (Main.screenHeight >> 1)));
      Rectangle rectangle;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle).\u002Ector(-1000, -1000, 4000, 4000);
      for (int index = num1; index < num2; ++index)
      {
        Vector2 vector2_1;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2_1).\u002Ector(1f / this._stars[index].Depth, 1.1f / this._stars[index].Depth);
        Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Multiply(Vector2.op_Subtraction(this._stars[index].Position, vector2_3), vector2_1), vector2_3), Main.screenPosition);
        // ISSUE: explicit reference operation
        if (((Rectangle) @rectangle).Contains((int) vector2_2.X, (int) vector2_2.Y))
        {
          float num4 = (float) Math.Sin((double) this._stars[index].AlphaFrequency * (double) Main.GlobalTime + (double) this._stars[index].SinOffset) * this._stars[index].AlphaAmplitude + this._stars[index].AlphaAmplitude;
          float num5 = (float) (Math.Sin((double) this._stars[index].AlphaFrequency * (double) Main.GlobalTime * 5.0 + (double) this._stars[index].SinOffset) * 0.100000001490116 - 0.100000001490116);
          float num6 = MathHelper.Clamp(num4, 0.0f, 1f);
          Texture2D starTexture = this._starTextures[this._stars[index].TextureIndex];
          spriteBatch.Draw(starTexture, vector2_2, new Rectangle?(), Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.get_White(), num3), num6), 0.8f), 1f - num5), this._fadeOpacity), 0.0f, new Vector2((float) (starTexture.get_Width() >> 1), (float) (starTexture.get_Height() >> 1)), (float) ((vector2_1.X * 0.5 + 0.5) * ((double) num6 * 0.300000011920929 + 0.699999988079071)), (SpriteEffects) 0, 0.0f);
        }
      }
    }

    public override float GetCloudAlpha()
    {
      return (float) ((1.0 - (double) this._fadeOpacity) * 0.300000011920929 + 0.699999988079071);
    }

    internal override void Activate(Vector2 position, params object[] args)
    {
      this._fadeOpacity = 1f / 500f;
      this._isActive = true;
      int num1 = 200;
      int num2 = 10;
      this._stars = new StardustSky.Star[num1 * num2];
      int index1 = 0;
      for (int index2 = 0; index2 < num1; ++index2)
      {
        float num3 = (float) index2 / (float) num1;
        for (int index3 = 0; index3 < num2; ++index3)
        {
          float num4 = (float) index3 / (float) num2;
          this._stars[index1].Position.X = (__Null) ((double) num3 * (double) Main.maxTilesX * 16.0);
          this._stars[index1].Position.Y = (__Null) ((double) num4 * (Main.worldSurface * 16.0 + 2000.0) - 1000.0);
          this._stars[index1].Depth = (float) ((double) this._random.NextFloat() * 8.0 + 1.5);
          this._stars[index1].TextureIndex = this._random.Next(this._starTextures.Length);
          this._stars[index1].SinOffset = this._random.NextFloat() * 6.28f;
          this._stars[index1].AlphaAmplitude = this._random.NextFloat() * 5f;
          this._stars[index1].AlphaFrequency = this._random.NextFloat() + 1f;
          ++index1;
        }
      }
      Array.Sort<StardustSky.Star>(this._stars, new Comparison<StardustSky.Star>(this.SortMethod));
    }

    private int SortMethod(StardustSky.Star meteor1, StardustSky.Star meteor2)
    {
      return meteor2.Depth.CompareTo(meteor1.Depth);
    }

    internal override void Deactivate(params object[] args)
    {
      this._isActive = false;
    }

    public override void Reset()
    {
      this._isActive = false;
    }

    public override bool IsActive()
    {
      if (!this._isActive)
        return (double) this._fadeOpacity > 1.0 / 1000.0;
      return true;
    }

    private struct Star
    {
      public Vector2 Position;
      public float Depth;
      public int TextureIndex;
      public float SinOffset;
      public float AlphaFrequency;
      public float AlphaAmplitude;
    }
  }
}
