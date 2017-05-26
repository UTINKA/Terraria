// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIKeybindingToggleListItem
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Graphics;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
  public class UIKeybindingToggleListItem : UIElement
  {
    private Color _color;
    private Func<string> _TextDisplayFunction;
    private Func<bool> _IsOnFunction;
    private Texture2D _toggleTexture;

    public UIKeybindingToggleListItem(Func<string> getText, Func<bool> getStatus, Color color)
    {
      this._color = color;
      this._toggleTexture = TextureManager.Load("Images/UI/Settings_Toggle");
      this._TextDisplayFunction = getText != null ? getText : (Func<string>) (() => "???");
      this._IsOnFunction = getStatus != null ? getStatus : (Func<bool>) (() => false);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      float num1 = 6f;
      base.DrawSelf(spriteBatch);
      CalculatedStyle dimensions = this.GetDimensions();
      float num2 = dimensions.Width + 1f;
      Vector2 vector2_1 = new Vector2(dimensions.X, dimensions.Y);
      int num3 = 0;
      Vector2 baseScale;
      // ISSUE: explicit reference operation
      ((Vector2) @baseScale).\u002Ector(0.8f);
      Color baseColor = Color.Lerp(num3 != 0 ? Color.get_Gold() : (this.IsMouseHovering ? Color.get_White() : Color.get_Silver()), Color.get_White(), this.IsMouseHovering ? 0.5f : 0.0f);
      Color color = this.IsMouseHovering ? this._color : this._color.MultiplyRGBA(new Color(180, 180, 180));
      Vector2 position = vector2_1;
      Utils.DrawSettingsPanel(spriteBatch, position, num2, color);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local1 = @position.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num4 = (double) ^(float&) local1 + 8.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local1 = (float) num4;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local2 = @position.Y;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num5 = (double) ^(float&) local2 + (2.0 + (double) num1);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local2 = (float) num5;
      ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, this._TextDisplayFunction(), position, baseColor, 0.0f, Vector2.get_Zero(), baseScale, num2, 2f);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local3 = @position.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num6 = (double) ^(float&) local3 - 17.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local3 = (float) num6;
      Rectangle rectangle;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle).\u002Ector(this._IsOnFunction() ? (this._toggleTexture.get_Width() - 2) / 2 + 2 : 0, 0, (this._toggleTexture.get_Width() - 2) / 2, this._toggleTexture.get_Height());
      Vector2 vector2_2;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_2).\u002Ector((float) rectangle.Width, 0.0f);
      // ISSUE: explicit reference operation
      ((Vector2) @position).\u002Ector((float) ((double) dimensions.X + (double) dimensions.Width - vector2_2.X - 10.0), dimensions.Y + 2f + num1);
      spriteBatch.Draw(this._toggleTexture, position, new Rectangle?(rectangle), Color.get_White(), 0.0f, Vector2.get_Zero(), Vector2.get_One(), (SpriteEffects) 0, 0.0f);
    }
  }
}
