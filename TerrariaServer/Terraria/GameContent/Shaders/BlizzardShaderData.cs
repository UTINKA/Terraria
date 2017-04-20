// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Shaders.BlizzardShaderData
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
      {
        BlizzardShaderData blizzardShaderData = this;
        Vector2 vector2 = Vector2.op_Addition(blizzardShaderData._texturePosition, Vector2.op_Multiply(direction, (float) gameTime.get_ElapsedGameTime().TotalSeconds));
        blizzardShaderData._texturePosition = vector2;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local1 = @this._texturePosition;
      // ISSUE: explicit reference operation
      double num2 = (^local1).X % 10.0;
      // ISSUE: explicit reference operation
      (^local1).X = (__Null) num2;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local2 = @this._texturePosition;
      // ISSUE: explicit reference operation
      double num3 = (^local2).Y % 10.0;
      // ISSUE: explicit reference operation
      (^local2).Y = (__Null) num3;
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
