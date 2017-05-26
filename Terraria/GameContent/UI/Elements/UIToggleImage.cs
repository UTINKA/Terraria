// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIToggleImage
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIToggleImage : UIElement
  {
    private Point _onTextureOffset = Point.get_Zero();
    private Point _offTextureOffset = Point.get_Zero();
    private Texture2D _onTexture;
    private Texture2D _offTexture;
    private int _drawWidth;
    private int _drawHeight;
    private bool _isOn;

    public bool IsOn
    {
      get
      {
        return this._isOn;
      }
    }

    public UIToggleImage(Texture2D texture, int width, int height, Point onTextureOffset, Point offTextureOffset)
    {
      this._onTexture = texture;
      this._offTexture = texture;
      this._offTextureOffset = offTextureOffset;
      this._onTextureOffset = onTextureOffset;
      this._drawWidth = width;
      this._drawHeight = height;
      this.Width.Set((float) width, 0.0f);
      this.Height.Set((float) height, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      Texture2D texture2D;
      Point point;
      if (this._isOn)
      {
        texture2D = this._onTexture;
        point = this._onTextureOffset;
      }
      else
      {
        texture2D = this._offTexture;
        point = this._offTextureOffset;
      }
      Color color = this.IsMouseHovering ? Color.get_White() : Color.get_Silver();
      spriteBatch.Draw(texture2D, new Rectangle((int) dimensions.X, (int) dimensions.Y, this._drawWidth, this._drawHeight), new Rectangle?(new Rectangle((int) point.X, (int) point.Y, this._drawWidth, this._drawHeight)), color);
    }

    public override void Click(UIMouseEvent evt)
    {
      this.Toggle();
      base.Click(evt);
    }

    public void SetState(bool value)
    {
      this._isOn = value;
    }

    public void Toggle()
    {
      this._isOn = !this._isOn;
    }
  }
}
