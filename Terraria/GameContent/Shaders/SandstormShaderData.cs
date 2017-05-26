// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Shaders.SandstormShaderData
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

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
      Vector2 vector2 = Vector2.op_Multiply(new Vector2(-Main.windSpeed, -1f), new Vector2(20f, 0.1f));
      // ISSUE: explicit reference operation
      ((Vector2) @vector2).Normalize();
      Vector2 direction = Vector2.op_Multiply(vector2, new Vector2(2f, 0.2f));
      if (!Main.gamePaused && Main.hasFocus)
        this._texturePosition = Vector2.op_Addition(this._texturePosition, Vector2.op_Multiply(direction, (float) gameTime.get_ElapsedGameTime().TotalSeconds));
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local1 = @this._texturePosition.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num1 = (double) ^(float&) local1 % 10.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local1 = (float) num1;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local2 = @this._texturePosition.Y;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num2 = (double) ^(float&) local2 % 10.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local2 = (float) num2;
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
