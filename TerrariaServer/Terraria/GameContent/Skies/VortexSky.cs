// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.VortexSky
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
  public class VortexSky : CustomSky
  {
    private UnifiedRandom _random = new UnifiedRandom();
    private Texture2D _planetTexture;
    private Texture2D _bgTexture;
    private Texture2D _boltTexture;
    private Texture2D _flashTexture;
    private bool _isActive;
    private int _ticksUntilNextBolt;
    private float _fadeOpacity;
    private VortexSky.Bolt[] _bolts;

    public override void OnLoad()
    {
      this._planetTexture = TextureManager.Load("Images/Misc/VortexSky/Planet");
      this._bgTexture = TextureManager.Load("Images/Misc/VortexSky/Background");
      this._boltTexture = TextureManager.Load("Images/Misc/VortexSky/Bolt");
      this._flashTexture = TextureManager.Load("Images/Misc/VortexSky/Flash");
    }

    public override void Update(GameTime gameTime)
    {
      this._fadeOpacity = !this._isActive ? Math.Max(0.0f, this._fadeOpacity - 0.01f) : Math.Min(1f, 0.01f + this._fadeOpacity);
      if (this._ticksUntilNextBolt <= 0)
      {
        this._ticksUntilNextBolt = this._random.Next(1, 5);
        int index = 0;
        while (this._bolts[index].IsAlive && index != this._bolts.Length - 1)
          ++index;
        this._bolts[index].IsAlive = true;
        this._bolts[index].Position.X = (__Null) ((double) this._random.NextFloat() * ((double) Main.maxTilesX * 16.0 + 4000.0) - 2000.0);
        this._bolts[index].Position.Y = (__Null) ((double) this._random.NextFloat() * 500.0);
        this._bolts[index].Depth = (float) ((double) this._random.NextFloat() * 8.0 + 2.0);
        this._bolts[index].Life = 30;
      }
      this._ticksUntilNextBolt = this._ticksUntilNextBolt - 1;
      for (int index = 0; index < this._bolts.Length; ++index)
      {
        if (this._bolts[index].IsAlive)
        {
          --this._bolts[index].Life;
          if (this._bolts[index].Life <= 0)
            this._bolts[index].IsAlive = false;
        }
      }
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
        spriteBatch.Draw(this._bgTexture, new Rectangle(0, Math.Max(0, (int) ((Main.worldSurface * 16.0 - (double) Main.screenPosition.Y - 2400.0) * 0.100000001490116)), Main.screenWidth, Main.screenHeight), Color.op_Multiply(Color.op_Multiply(Color.get_White(), Math.Min(1f, (float) ((Main.screenPosition.Y - 800.0) / 1000.0))), this._fadeOpacity));
        Vector2 vector2_1;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2_1).\u002Ector((float) (Main.screenWidth >> 1), (float) (Main.screenHeight >> 1));
        Vector2 vector2_2 = Vector2.op_Multiply(0.01f, Vector2.op_Subtraction(new Vector2((float) Main.maxTilesX * 8f, (float) Main.worldSurface / 2f), Main.screenPosition));
        spriteBatch.Draw(this._planetTexture, Vector2.op_Addition(Vector2.op_Addition(vector2_1, new Vector2(-200f, -200f)), vector2_2), new Rectangle?(), Color.op_Multiply(Color.op_Multiply(Color.get_White(), 0.9f), this._fadeOpacity), 0.0f, new Vector2((float) (this._planetTexture.get_Width() >> 1), (float) (this._planetTexture.get_Height() >> 1)), 1f, (SpriteEffects) 0, 1f);
      }
      float num1 = Math.Min(1f, (float) ((Main.screenPosition.Y - 1000.0) / 1000.0));
      Vector2 vector2_3 = Vector2.op_Addition(Main.screenPosition, new Vector2((float) (Main.screenWidth >> 1), (float) (Main.screenHeight >> 1)));
      Rectangle rectangle;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle).\u002Ector(-1000, -1000, 4000, 4000);
      for (int index = 0; index < this._bolts.Length; ++index)
      {
        if (this._bolts[index].IsAlive && (double) this._bolts[index].Depth > (double) minDepth && (double) this._bolts[index].Depth < (double) maxDepth)
        {
          Vector2 vector2_1;
          // ISSUE: explicit reference operation
          ((Vector2) @vector2_1).\u002Ector(1f / this._bolts[index].Depth, 0.9f / this._bolts[index].Depth);
          Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Multiply(Vector2.op_Subtraction(this._bolts[index].Position, vector2_3), vector2_1), vector2_3), Main.screenPosition);
          // ISSUE: explicit reference operation
          if (((Rectangle) @rectangle).Contains((int) vector2_2.X, (int) vector2_2.Y))
          {
            Texture2D texture2D = this._boltTexture;
            int life = this._bolts[index].Life;
            if (life > 26 && life % 2 == 0)
              texture2D = this._flashTexture;
            float num2 = (float) life / 30f;
            spriteBatch.Draw(texture2D, vector2_2, new Rectangle?(), Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.get_White(), num1), num2), this._fadeOpacity), 0.0f, Vector2.get_Zero(), (float) (vector2_1.X * 5.0), (SpriteEffects) 0, 0.0f);
          }
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
      this._bolts = new VortexSky.Bolt[500];
      for (int index = 0; index < this._bolts.Length; ++index)
        this._bolts[index].IsAlive = false;
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

    private struct Bolt
    {
      public Vector2 Position;
      public float Depth;
      public int Life;
      public bool IsAlive;
    }
  }
}
