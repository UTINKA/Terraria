// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIText
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIText : UIElement
  {
    private object _text = (object) "";
    private float _textScale = 1f;
    private Vector2 _textSize = Vector2.get_Zero();
    private Color _color = Color.get_White();
    private bool _isLarge;

    public string Text
    {
      get
      {
        return this._text.ToString();
      }
    }

    public Color TextColor
    {
      get
      {
        return this._color;
      }
      set
      {
        this._color = value;
      }
    }

    public UIText(string text, float textScale = 1f, bool large = false)
    {
      this.InternalSetText((object) text, textScale, large);
    }

    public UIText(LocalizedText text, float textScale = 1f, bool large = false)
    {
      this.InternalSetText((object) text, textScale, large);
    }

    public override void Recalculate()
    {
      this.InternalSetText(this._text, this._textScale, this._isLarge);
      base.Recalculate();
    }

    public void SetText(string text)
    {
      this.InternalSetText((object) text, this._textScale, this._isLarge);
    }

    public void SetText(LocalizedText text)
    {
      this.InternalSetText((object) text, this._textScale, this._isLarge);
    }

    public void SetText(string text, float textScale, bool large)
    {
      this.InternalSetText((object) text, textScale, large);
    }

    public void SetText(LocalizedText text, float textScale, bool large)
    {
      this.InternalSetText((object) text, textScale, large);
    }

    private void InternalSetText(object text, float textScale, bool large)
    {
      Vector2 vector2 = Vector2.op_Multiply(new Vector2((float) (large ? Main.fontDeathText : Main.fontMouseText).MeasureString(text.ToString()).X, large ? 32f : 16f), textScale);
      this._text = text;
      this._textScale = textScale;
      this._textSize = vector2;
      this._isLarge = large;
      this.MinWidth.Set((float) vector2.X + this.PaddingLeft + this.PaddingRight, 0.0f);
      this.MinHeight.Set((float) vector2.Y + this.PaddingTop + this.PaddingBottom, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      Vector2 pos = innerDimensions.Position();
      if (this._isLarge)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local = @pos.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num = (double) ^(float&) local - 10.0 * (double) this._textScale;
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
        double num = (double) ^(float&) local - 2.0 * (double) this._textScale;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local = (float) num;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local1 = @pos.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num1 = (double) ^(float&) local1 + ((double) innerDimensions.Width - this._textSize.X) * 0.5;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local1 = (float) num1;
      if (this._isLarge)
        Utils.DrawBorderStringBig(spriteBatch, this.Text, pos, this._color, this._textScale, 0.0f, 0.0f, -1);
      else
        Utils.DrawBorderString(spriteBatch, this.Text, pos, this._color, this._textScale, 0.0f, 0.0f, -1);
    }
  }
}
