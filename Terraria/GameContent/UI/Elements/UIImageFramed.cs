// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIImageFramed
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIImageFramed : UIElement
  {
    public Color Color = Color.White;
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
