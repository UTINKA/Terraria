// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Shaders.SandstormShaderData
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
  public class SandstormShaderData : ScreenShaderData
  {
    private Vector2 _texturePosition = Vector2.get_Zero();

    public SandstormShaderData(string passName)
      : base(passName)
    {
    }

    public override void Update(GameTime gameTime)
    {
      Vector2 vector2_1 = Vector2.op_Multiply(new Vector2(-Main.windSpeed, -1f), new Vector2(20f, 0.1f));
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_1).Normalize();
      Vector2 direction = Vector2.op_Multiply(vector2_1, new Vector2(2f, 0.2f));
      if (!Main.gamePaused && Main.hasFocus)
      {
        SandstormShaderData sandstormShaderData = this;
        Vector2 vector2_2 = Vector2.op_Addition(sandstormShaderData._texturePosition, Vector2.op_Multiply(direction, (float) gameTime.get_ElapsedGameTime().TotalSeconds));
        sandstormShaderData._texturePosition = vector2_2;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local1 = @this._texturePosition;
      // ISSUE: explicit reference operation
      double num1 = (^local1).X % 10.0;
      // ISSUE: explicit reference operation
      (^local1).X = (__Null) num1;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local2 = @this._texturePosition;
      // ISSUE: explicit reference operation
      double num2 = (^local2).Y % 10.0;
      // ISSUE: explicit reference operation
      (^local2).Y = (__Null) num2;
      this.UseDirection(direction);
      base.Update(gameTime);
    }

    public override void Apply()
    {
      this.UseTargetPosition(this._texturePosition);
      base.Apply();
    }
  }
}
