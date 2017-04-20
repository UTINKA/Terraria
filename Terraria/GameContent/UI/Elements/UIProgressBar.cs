// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIProgressBar
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIProgressBar : UIElement
  {
    private UIProgressBar.UIInnerProgressBar _progressBar = new UIProgressBar.UIInnerProgressBar();
    private float _visualProgress;
    private float _targetProgress;

    public UIProgressBar()
    {
      this._progressBar.Height.Precent = 1f;
      this._progressBar.Recalculate();
      this.Append((UIElement) this._progressBar);
    }

    public void SetProgress(float value)
    {
      this._targetProgress = value;
      if ((double) value >= (double) this._visualProgress)
        return;
      this._visualProgress = value;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      this._visualProgress = (float) ((double) this._visualProgress * 0.949999988079071 + 0.0500000007450581 * (double) this._targetProgress);
      this._progressBar.Width.Precent = this._visualProgress;
      this._progressBar.Recalculate();
    }

    private class UIInnerProgressBar : UIElement
    {
      protected override void DrawSelf(SpriteBatch spriteBatch)
      {
        CalculatedStyle dimensions = this.GetDimensions();
        spriteBatch.Draw(Main.magicPixel, new Vector2(dimensions.X, dimensions.Y), new Rectangle?(), Color.get_Blue(), 0.0f, Vector2.get_Zero(), new Vector2(dimensions.Width, dimensions.Height / 1000f), (SpriteEffects) 0, 0.0f);
      }
    }
  }
}
