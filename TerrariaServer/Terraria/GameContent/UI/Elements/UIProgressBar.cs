// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIProgressBar
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
