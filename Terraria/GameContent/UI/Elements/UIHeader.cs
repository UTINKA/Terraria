// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIHeader
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIHeader : UIElement
  {
    private string _text;

    public string Text
    {
      get
      {
        return this._text;
      }
      set
      {
        if (!(this._text != value))
          return;
        this._text = value;
        Vector2 vector2 = Main.fontDeathText.MeasureString(this.Text);
        this.Width.Pixels = vector2.X;
        this.Height.Pixels = vector2.Y;
        this.Width.Precent = 0.0f;
        this.Height.Precent = 0.0f;
        this.Recalculate();
      }
    }

    public UIHeader()
    {
      this.Text = "";
    }

    public UIHeader(string text)
    {
      this.Text = text;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      spriteBatch.DrawString(Main.fontDeathText, this.Text, new Vector2(dimensions.X, dimensions.Y), Color.White);
    }
  }
}
