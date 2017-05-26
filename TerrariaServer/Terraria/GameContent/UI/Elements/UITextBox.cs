// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UITextBox
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  internal class UITextBox : UITextPanel<string>
  {
    private int _maxLength = 20;
    private int _cursor;
    private int _frameCount;

    public UITextBox(string text, float textScale = 1f, bool large = false)
      : base(text, textScale, large)
    {
    }

    public void Write(string text)
    {
      this.SetText(this.Text.Insert(this._cursor, text));
      this._cursor = this._cursor + text.Length;
    }

    public override void SetText(string text, float textScale, bool large)
    {
      if (text.ToString().Length > this._maxLength)
        text = text.ToString().Substring(0, this._maxLength);
      base.SetText(text, textScale, large);
      this._cursor = Math.Min(this.Text.Length, this._cursor);
    }

    public void SetTextMaxLength(int maxLength)
    {
      this._maxLength = maxLength;
    }

    public void Backspace()
    {
      if (this._cursor == 0)
        return;
      this.SetText(this.Text.Substring(0, this.Text.Length - 1));
    }

    public void CursorLeft()
    {
      if (this._cursor == 0)
        return;
      this._cursor = this._cursor - 1;
    }

    public void CursorRight()
    {
      if (this._cursor >= this.Text.Length)
        return;
      this._cursor = this._cursor + 1;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      this._cursor = this.Text.Length;
      base.DrawSelf(spriteBatch);
      this._frameCount = this._frameCount + 1;
      if ((this._frameCount = this._frameCount % 40) > 20)
        return;
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      Vector2 pos = innerDimensions.Position();
      Vector2 vector2 = Vector2.op_Multiply(new Vector2((float) (this.IsLarge ? Main.fontDeathText : Main.fontMouseText).MeasureString(this.Text.Substring(0, this._cursor)).X, this.IsLarge ? 32f : 16f), this.TextScale);
      if (this.IsLarge)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local = @pos.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num = (double) ^(float&) local - 8.0 * (double) this.TextScale;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local = (float) num;
      }
      else
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local = @pos.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num = (double) ^(float&) local + 2.0 * (double) this.TextScale;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local = (float) num;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local1 = @pos.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num1 = (double) ^(float&) local1 + (((double) innerDimensions.Width - this.TextSize.X) * 0.5 + vector2.X - (this.IsLarge ? 8.0 : 4.0) * (double) this.TextScale + 6.0);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local1 = (float) num1;
      if (this.IsLarge)
        Utils.DrawBorderStringBig(spriteBatch, "|", pos, this.TextColor, this.TextScale, 0.0f, 0.0f, -1);
      else
        Utils.DrawBorderString(spriteBatch, "|", pos, this.TextColor, this.TextScale, 0.0f, 0.0f, -1);
    }
  }
}
