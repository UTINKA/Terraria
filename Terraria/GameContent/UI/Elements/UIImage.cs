// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIImage
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIImage : UIElement
  {
    public float ImageScale = 1f;
    private Texture2D _texture;

    public UIImage(Texture2D texture)
    {
      this._texture = texture;
      this.Width.Set((float) this._texture.get_Width(), 0.0f);
      this.Height.Set((float) this._texture.get_Height(), 0.0f);
    }

    public void SetImage(Texture2D texture)
    {
      this._texture = texture;
      this.Width.Set((float) this._texture.get_Width(), 0.0f);
      this.Height.Set((float) this._texture.get_Height(), 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      spriteBatch.Draw(this._texture, Vector2.op_Addition(dimensions.Position(), Vector2.op_Division(Vector2.op_Multiply(this._texture.Size(), 1f - this.ImageScale), 2f)), new Rectangle?(), Color.get_White(), 0.0f, Vector2.get_Zero(), this.ImageScale, (SpriteEffects) 0, 0.0f);
    }
  }
}
