// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UITextPanel`1
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UITextPanel<T> : UIPanel
  {
    private T _text = default (T);
    private float _textScale = 1f;
    private Vector2 _textSize = Vector2.Zero;
    private Color _color = Color.White;
    private bool _drawPanel = true;
    private bool _isLarge;

    public bool IsLarge
    {
      get
      {
        return this._isLarge;
      }
    }

    public bool DrawPanel
    {
      get
      {
        return this._drawPanel;
      }
      set
      {
        this._drawPanel = value;
      }
    }

    public float TextScale
    {
      get
      {
        return this._textScale;
      }
      set
      {
        this._textScale = value;
      }
    }

    public Vector2 TextSize
    {
      get
      {
        return this._textSize;
      }
    }

    public string Text
    {
      get
      {
        if ((object) this._text != null)
          return this._text.ToString();
        return "";
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

    public UITextPanel(T text, float textScale = 1f, bool large = false)
    {
      this.SetText(text, textScale, large);
    }

    public override void Recalculate()
    {
      this.SetText(this._text, this._textScale, this._isLarge);
      base.Recalculate();
    }

    public void SetText(T text)
    {
      this.SetText(text, this._textScale, this._isLarge);
    }

    public virtual void SetText(T text, float textScale, bool large)
    {
      Vector2 vector2 = new Vector2((large ? Main.fontDeathText : Main.fontMouseText).MeasureString(text.ToString()).X, large ? 32f : 16f) * textScale;
      this._text = text;
      this._textScale = textScale;
      this._textSize = vector2;
      this._isLarge = large;
      this.MinWidth.Set(vector2.X + this.PaddingLeft + this.PaddingRight, 0.0f);
      this.MinHeight.Set(vector2.Y + this.PaddingTop + this.PaddingBottom, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      if (this._drawPanel)
        base.DrawSelf(spriteBatch);
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      Vector2 pos = innerDimensions.Position();
      if (this._isLarge)
        pos.Y -= 10f * this._textScale * this._textScale;
      else
        pos.Y -= 2f * this._textScale;
      pos.X += (float) (((double) innerDimensions.Width - (double) this._textSize.X) * 0.5);
      if (this._isLarge)
        Utils.DrawBorderStringBig(spriteBatch, this.Text, pos, this._color, this._textScale, 0.0f, 0.0f, -1);
      else
        Utils.DrawBorderString(spriteBatch, this.Text, pos, this._color, this._textScale, 0.0f, 0.0f, -1);
    }
  }
}
