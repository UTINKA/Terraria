// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIImageFramed
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIImageFramed : UIElement
  {
    public Color Color = Color.get_White();
    private Texture2D _texture;
    private Rectangle _frame;

    public UIImageFramed(Texture2D texture, Rectangle frame)
    {
      this._texture = texture;
      this._frame = frame;
      this.Width.Set((float) this._frame.Width, 0.0f);
      this.Height.Set((float) this._frame.Height, 0.0f);
    }

    public void SetImage(Texture2D texture, Rectangle frame)
    {
      this._texture = texture;
      this._frame = frame;
      this.Width.Set((float) this._frame.Width, 0.0f);
      this.Height.Set((float) this._frame.Height, 0.0f);
    }

    public void SetFrame(Rectangle frame)
    {
      this._frame = frame;
      this.Width.Set((float) this._frame.Width, 0.0f);
      this.Height.Set((float) this._frame.Height, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      spriteBatch.Draw(this._texture, dimensions.Position(), new Rectangle?(this._frame), this.Color);
    }
  }
}
