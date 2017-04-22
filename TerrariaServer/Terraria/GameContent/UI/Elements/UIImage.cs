// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIImage
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
      this.Width.Set((float) this._texture.Width, 0.0f);
      this.Height.Set((float) this._texture.Height, 0.0f);
    }

    public void SetImage(Texture2D texture)
    {
      this._texture = texture;
      this.Width.Set((float) this._texture.Width, 0.0f);
      this.Height.Set((float) this._texture.Height, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      spriteBatch.Draw(this._texture, dimensions.Position() + this._texture.Size() * (1f - this.ImageScale) / 2f, new Rectangle?(), Color.White, 0.0f, Vector2.Zero, this.ImageScale, SpriteEffects.None, 0.0f);
    }
  }
}
