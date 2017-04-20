// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.NebulaSky
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
  public class NebulaSky : CustomSky
  {
    private UnifiedRandom _random = new UnifiedRandom();
    private NebulaSky.LightPillar[] _pillars;
    private Texture2D _planetTexture;
    private Texture2D _bgTexture;
    private Texture2D _beamTexture;
    private Texture2D[] _rockTextures;
    private bool _isActive;
    private float _fadeOpacity;

    public override void OnLoad()
    {
      this._planetTexture = TextureManager.Load("Images/Misc/NebulaSky/Planet");
      this._bgTexture = TextureManager.Load("Images/Misc/NebulaSky/Background");
      this._beamTexture = TextureManager.Load("Images/Misc/NebulaSky/Beam");
      this._rockTextures = new Texture2D[3];
      for (int index = 0; index < this._rockTextures.Length; ++index)
        this._rockTextures[index] = TextureManager.Load("Images/Misc/NebulaSky/Rock_" + (object) index);
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
      for (int index = 0; index < this._pillars.Length; ++index)
      {
        float depth = this._pillars[index].Depth;
        if (num1 == -1 && (double) depth < (double) maxDepth)
          num1 = index;
        if ((double) depth > (double) minDepth)
          num2 = index;
        else
          break;
      }
      if (num1 == -1)
        return;
      Vector2 vector2_3 = Vector2.op_Addition(Main.screenPosition, new Vector2((float) (Main.screenWidth >> 1), (float) (Main.screenHeight >> 1)));
      Rectangle rectangle;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle).\u002Ector(-1000, -1000, 4000, 4000);
      float num3 = Math.Min(1f, (float) ((Main.screenPosition.Y - 1000.0) / 1000.0));
      for (int index1 = num1; index1 < num2; ++index1)
      {
        Vector2 vector2_1;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2_1).\u002Ector(1f / this._pillars[index1].Depth, 0.9f / this._pillars[index1].Depth);
        Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Multiply(Vector2.op_Subtraction(this._pillars[index1].Position, vector2_3), vector2_1), vector2_3), Main.screenPosition);
        // ISSUE: explicit reference operation
        if (((Rectangle) @rectangle).Contains((int) vector2_2.X, (int) vector2_2.Y))
        {
          float num4 = (float) (vector2_1.X * 450.0);
          spriteBatch.Draw(this._beamTexture, vector2_2, new Rectangle?(), Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.get_White(), 0.2f), num3), this._fadeOpacity), 0.0f, Vector2.get_Zero(), new Vector2(num4 / 70f, num4 / 45f), (SpriteEffects) 0, 0.0f);
          int index2 = 0;
          float num5 = 0.0f;
          while ((double) num5 <= 1.0)
          {
            float num6 = (float) (1.0 - ((double) num5 + (double) Main.GlobalTime * 0.0199999995529652 + Math.Sin((double) index1)) % 1.0);
            spriteBatch.Draw(this._rockTextures[index2], Vector2.op_Addition(vector2_2, new Vector2((float) (Math.Sin((double) num5 * 1582.0) * ((double) num4 * 0.5) + (double) num4 * 0.5), num6 * 2000f)), new Rectangle?(), Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.get_White(), num6), num3), this._fadeOpacity), num6 * 20f, new Vector2((float) (this._rockTextures[index2].get_Width() >> 1), (float) (this._rockTextures[index2].get_Height() >> 1)), 0.9f, (SpriteEffects) 0, 0.0f);
            index2 = (index2 + 1) % this._rockTextures.Length;
            num5 += 0.03f;
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
      this._pillars = new NebulaSky.LightPillar[40];
      for (int index = 0; index < this._pillars.Length; ++index)
      {
        this._pillars[index].Position.X = (__Null) ((double) index / (double) this._pillars.Length * ((double) Main.maxTilesX * 16.0 + 20000.0) + (double) this._random.NextFloat() * 40.0 - 20.0 - 20000.0);
        this._pillars[index].Position.Y = (__Null) ((double) this._random.NextFloat() * 200.0 - 2000.0);
        this._pillars[index].Depth = (float) ((double) this._random.NextFloat() * 8.0 + 7.0);
      }
      Array.Sort<NebulaSky.LightPillar>(this._pillars, new Comparison<NebulaSky.LightPillar>(this.SortMethod));
    }

    private int SortMethod(NebulaSky.LightPillar pillar1, NebulaSky.LightPillar pillar2)
    {
      return pillar2.Depth.CompareTo(pillar1.Depth);
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

    private struct LightPillar
    {
      public Vector2 Position;
      public float Depth;
    }
  }
}
