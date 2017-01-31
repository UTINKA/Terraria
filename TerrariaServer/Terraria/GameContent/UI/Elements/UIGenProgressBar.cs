// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIGenProgressBar
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIGenProgressBar : UIElement
  {
    private Texture2D _texInnerDirt;
    private Texture2D _texOuterCrimson;
    private Texture2D _texOuterCorrupt;
    private Texture2D _texOuterLower;
    private float _visualOverallProgress;
    private float _targetOverallProgress;
    private float _visualCurrentProgress;
    private float _targetCurrentProgress;

    public UIGenProgressBar()
    {
      if (Main.netMode != 2)
      {
        this._texInnerDirt = TextureManager.Load("Images/UI/WorldGen/Outer Dirt");
        this._texOuterCorrupt = TextureManager.Load("Images/UI/WorldGen/Outer Corrupt");
        this._texOuterCrimson = TextureManager.Load("Images/UI/WorldGen/Outer Crimson");
        this._texOuterLower = TextureManager.Load("Images/UI/WorldGen/Outer Lower");
      }
      this.Recalculate();
    }

    public override void Recalculate()
    {
      this.Width.Precent = 0.0f;
      this.Height.Precent = 0.0f;
      this.Width.Pixels = 612f;
      this.Height.Pixels = 70f;
      base.Recalculate();
    }

    public void SetProgress(float overallProgress, float currentProgress)
    {
      this._targetCurrentProgress = currentProgress;
      this._targetOverallProgress = overallProgress;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      this._visualOverallProgress = this._targetOverallProgress;
      this._visualCurrentProgress = this._targetCurrentProgress;
      CalculatedStyle dimensions = this.GetDimensions();
      int completedWidth1 = (int) ((double) this._visualOverallProgress * 504.0);
      int completedWidth2 = (int) ((double) this._visualCurrentProgress * 504.0);
      Vector2 vector2 = new Vector2(dimensions.X, dimensions.Y);
      Color filled = new Color();
      filled.PackedValue = WorldGen.crimson ? 4286836223U : 4283888223U;
      this.DrawFilling2(spriteBatch, vector2 + new Vector2(20f, 40f), 16, completedWidth1, 564, filled, Color.Lerp(filled, Color.Black, 0.5f), new Color(48, 48, 48));
      filled.PackedValue = 4290947159U;
      this.DrawFilling2(spriteBatch, vector2 + new Vector2(50f, 60f), 8, completedWidth2, 504, filled, Color.Lerp(filled, Color.Black, 0.5f), new Color(33, 33, 33));
      Rectangle rectangle = this.GetDimensions().ToRectangle();
      rectangle.X -= 8;
      spriteBatch.Draw(WorldGen.crimson ? this._texOuterCrimson : this._texOuterCorrupt, rectangle.TopLeft(), Color.White);
      spriteBatch.Draw(this._texOuterLower, rectangle.TopLeft() + new Vector2(44f, 60f), Color.White);
    }

    private void DrawFilling(SpriteBatch spritebatch, Texture2D tex, Texture2D texShadow, Vector2 topLeft, int completedWidth, int totalWidth, Color separator, Color empty)
    {
      if (completedWidth % 2 != 0)
        --completedWidth;
      Vector2 position = topLeft + (float) completedWidth * Vector2.UnitX;
      int num = completedWidth;
      Rectangle rectangle = tex.Frame(1, 1, 0, 0);
      while (num > 0)
      {
        if (rectangle.Width > num)
        {
          rectangle.X += rectangle.Width - num;
          rectangle.Width = num;
        }
        spritebatch.Draw(tex, position, new Rectangle?(rectangle), Color.White, 0.0f, new Vector2((float) rectangle.Width, 0.0f), 1f, SpriteEffects.None, 0.0f);
        position.X -= (float) rectangle.Width;
        num -= rectangle.Width;
      }
      if (texShadow != null)
        spritebatch.Draw(texShadow, topLeft, new Rectangle?(new Rectangle(0, 0, completedWidth, texShadow.Height)), Color.White);
      spritebatch.Draw(Main.magicPixel, new Rectangle((int) topLeft.X + completedWidth, (int) topLeft.Y, totalWidth - completedWidth, tex.Height), new Rectangle?(new Rectangle(0, 0, 1, 1)), empty);
      spritebatch.Draw(Main.magicPixel, new Rectangle((int) topLeft.X + completedWidth - 2, (int) topLeft.Y, 2, tex.Height), new Rectangle?(new Rectangle(0, 0, 1, 1)), separator);
    }

    private void DrawFilling2(SpriteBatch spritebatch, Vector2 topLeft, int height, int completedWidth, int totalWidth, Color filled, Color separator, Color empty)
    {
      if (completedWidth % 2 != 0)
        --completedWidth;
      spritebatch.Draw(Main.magicPixel, new Rectangle((int) topLeft.X, (int) topLeft.Y, completedWidth, height), new Rectangle?(new Rectangle(0, 0, 1, 1)), filled);
      spritebatch.Draw(Main.magicPixel, new Rectangle((int) topLeft.X + completedWidth, (int) topLeft.Y, totalWidth - completedWidth, height), new Rectangle?(new Rectangle(0, 0, 1, 1)), empty);
      spritebatch.Draw(Main.magicPixel, new Rectangle((int) topLeft.X + completedWidth - 2, (int) topLeft.Y, 2, height), new Rectangle?(new Rectangle(0, 0, 1, 1)), separator);
    }
  }
}
