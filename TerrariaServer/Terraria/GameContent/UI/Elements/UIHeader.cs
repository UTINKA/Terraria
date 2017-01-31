// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIHeader
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

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
