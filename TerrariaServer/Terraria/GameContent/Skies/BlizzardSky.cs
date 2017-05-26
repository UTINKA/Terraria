// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.BlizzardSky
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
  public class BlizzardSky : CustomSky
  {
    private UnifiedRandom _random = new UnifiedRandom();
    private bool _isActive;
    private bool _isLeaving;
    private float _opacity;

    public override void OnLoad()
    {
    }

    public override void Update(GameTime gameTime)
    {
      if (Main.gamePaused || !Main.hasFocus)
        return;
      if (this._isLeaving)
      {
        this._opacity = this._opacity - (float) gameTime.get_ElapsedGameTime().TotalSeconds;
        if ((double) this._opacity >= 0.0)
          return;
        this._isActive = false;
        this._opacity = 0.0f;
      }
      else
      {
        this._opacity = this._opacity + (float) gameTime.get_ElapsedGameTime().TotalSeconds;
        if ((double) this._opacity <= 1.0)
          return;
        this._opacity = 1f;
      }
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
      if ((double) minDepth >= 1.0 && (double) maxDepth != 3.40282346638529E+38)
        return;
      float num = Math.Min(1f, Main.cloudAlpha * 2f);
      // ISSUE: explicit reference operation
      Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(new Color(Vector4.op_Multiply(new Vector4(1f), ((Color) @Main.bgColor).ToVector4())), this._opacity), 0.7f), num);
      spriteBatch.Draw(Main.magicPixel, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
    }

    internal override void Activate(Vector2 position, params object[] args)
    {
      this._isActive = true;
      this._isLeaving = false;
    }

    internal override void Deactivate(params object[] args)
    {
      this._isLeaving = true;
    }

    public override void Reset()
    {
      this._opacity = 0.0f;
      this._isActive = false;
    }

    public override bool IsActive()
    {
      return this._isActive;
    }
  }
}
