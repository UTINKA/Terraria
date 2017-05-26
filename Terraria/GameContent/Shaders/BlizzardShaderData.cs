// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Shaders.BlizzardShaderData
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
  public class BlizzardShaderData : ScreenShaderData
  {
    private Vector2 _texturePosition = Vector2.get_Zero();
    private float windSpeed = 0.1f;

    public BlizzardShaderData(string passName)
      : base(passName)
    {
    }

    public override void Update(GameTime gameTime)
    {
      float num1 = Main.windSpeed;
      if ((double) num1 >= 0.0 && (double) num1 <= 0.100000001490116)
        num1 = 0.1f;
      else if ((double) num1 <= 0.0 && (double) num1 >= -0.100000001490116)
        num1 = -0.1f;
      this.windSpeed = (float) ((double) num1 * 0.0500000007450581 + (double) this.windSpeed * 0.949999988079071);
      Vector2 direction = Vector2.op_Multiply(new Vector2(-this.windSpeed, -1f), new Vector2(10f, 2f));
      // ISSUE: explicit reference operation
      ((Vector2) @direction).Normalize();
      direction = Vector2.op_Multiply(direction, new Vector2(0.8f, 0.6f));
      if (!Main.gamePaused && Main.hasFocus)
        this._texturePosition = Vector2.op_Addition(this._texturePosition, Vector2.op_Multiply(direction, (float) gameTime.get_ElapsedGameTime().TotalSeconds));
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local1 = @this._texturePosition.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num2 = (double) ^(float&) local1 % 10.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local1 = (float) num2;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local2 = @this._texturePosition.Y;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num3 = (double) ^(float&) local2 % 10.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local2 = (float) num3;
      this.UseDirection(direction);
      this.UseTargetPosition(this._texturePosition);
      base.Update(gameTime);
    }

    public override void Apply()
    {
      this.UseTargetPosition(this._texturePosition);
      base.Apply();
    }
  }
}
