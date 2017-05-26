// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIKeybindingSliderItem
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameInput;
using Terraria.Graphics;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
  public class UIKeybindingSliderItem : UIElement
  {
    private Color _color;
    private Func<string> _TextDisplayFunction;
    private Func<float> _GetStatusFunction;
    private Action<float> _SlideKeyboardAction;
    private Action _SlideGamepadAction;
    private int _sliderIDInPage;
    private Texture2D _toggleTexture;

    public UIKeybindingSliderItem(Func<string> getText, Func<float> getStatus, Action<float> setStatusKeyboard, Action setStatusGamepad, int sliderIDInPage, Color color)
    {
      this._color = color;
      this._toggleTexture = TextureManager.Load("Images/UI/Settings_Toggle");
      this._TextDisplayFunction = getText != null ? getText : (Func<string>) (() => "???");
      this._GetStatusFunction = getStatus != null ? getStatus : (Func<float>) (() => 0.0f);
      this._SlideKeyboardAction = setStatusKeyboard != null ? setStatusKeyboard : (Action<float>) (s => {});
      this._SlideGamepadAction = setStatusGamepad != null ? setStatusGamepad : (Action) (() => {});
      this._sliderIDInPage = sliderIDInPage;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      float num1 = 6f;
      base.DrawSelf(spriteBatch);
      int lockState = 0;
      IngameOptions.rightHover = -1;
      if (!Main.mouseLeft)
        IngameOptions.rightLock = -1;
      if (IngameOptions.rightLock == this._sliderIDInPage)
        lockState = 1;
      else if (IngameOptions.rightLock != -1)
        lockState = 2;
      CalculatedStyle dimensions = this.GetDimensions();
      float num2 = dimensions.Width + 1f;
      Vector2 vector2 = new Vector2(dimensions.X, dimensions.Y);
      int num3 = 0;
      bool flag = this.IsMouseHovering;
      if (lockState == 1)
        flag = true;
      if (lockState == 2)
        flag = false;
      Vector2 baseScale;
      // ISSUE: explicit reference operation
      ((Vector2) @baseScale).\u002Ector(0.8f);
      Color baseColor = Color.Lerp(num3 != 0 ? Color.get_Gold() : (flag ? Color.get_White() : Color.get_Silver()), Color.get_White(), flag ? 0.5f : 0.0f);
      Color color = flag ? this._color : this._color.MultiplyRGBA(new Color(180, 180, 180));
      Vector2 position = vector2;
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
      Main.colorBarTexture.Frame(1, 1, 0, 0);
      // ISSUE: explicit reference operation
      ((Vector2) @position).\u002Ector((float) ((double) dimensions.X + (double) dimensions.Width - 10.0), dimensions.Y + 10f + num1);
      IngameOptions.valuePosition = position;
      float num7 = IngameOptions.DrawValueBar(spriteBatch, 1f, this._GetStatusFunction(), lockState, (Utils.ColorLerpMethod) null);
      if (IngameOptions.inBar || IngameOptions.rightLock == this._sliderIDInPage)
      {
        IngameOptions.rightHover = this._sliderIDInPage;
        if (PlayerInput.Triggers.Current.MouseLeft && PlayerInput.CurrentProfile.AllowEditting && (!PlayerInput.UsingGamepad && IngameOptions.rightLock == this._sliderIDInPage))
          this._SlideKeyboardAction(num7);
      }
      if (IngameOptions.rightHover != -1 && IngameOptions.rightLock == -1)
        IngameOptions.rightLock = IngameOptions.rightHover;
      if (!this.IsMouseHovering || !PlayerInput.CurrentProfile.AllowEditting)
        return;
      this._SlideGamepadAction();
    }
  }
}
