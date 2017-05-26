// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIKeybindingListItem
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent.UI.Chat;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
  public class UIKeybindingListItem : UIElement
  {
    private InputMode _inputmode;
    private Color _color;
    private string _keybind;

    public UIKeybindingListItem(string bind, InputMode mode, Color color)
    {
      this._keybind = bind;
      this._inputmode = mode;
      this._color = color;
      this.OnClick += new UIElement.MouseEvent(this.OnClickMethod);
    }

    public void OnClickMethod(UIMouseEvent evt, UIElement listeningElement)
    {
      if (!(PlayerInput.ListeningTrigger != this._keybind))
        return;
      if (PlayerInput.CurrentProfile.AllowEditting)
        PlayerInput.ListenFor(this._keybind, this._inputmode);
      else
        PlayerInput.ListenFor((string) null, this._inputmode);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      float num1 = 6f;
      base.DrawSelf(spriteBatch);
      CalculatedStyle dimensions = this.GetDimensions();
      float num2 = dimensions.Width + 1f;
      Vector2 vector2 = new Vector2(dimensions.X, dimensions.Y);
      bool flag = PlayerInput.ListeningTrigger == this._keybind;
      Vector2 baseScale;
      // ISSUE: explicit reference operation
      ((Vector2) @baseScale).\u002Ector(0.8f);
      Color baseColor = Color.Lerp(flag ? Color.get_Gold() : (this.IsMouseHovering ? Color.get_White() : Color.get_Silver()), Color.get_White(), this.IsMouseHovering ? 0.5f : 0.0f);
      Color color = this.IsMouseHovering ? this._color : this._color.MultiplyRGBA(new Color(180, 180, 180));
      Vector2 position = vector2;
      Utils.DrawSettingsPanel(spriteBatch, position, num2, color);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local1 = @position.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num3 = (double) ^(float&) local1 + 8.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local1 = (float) num3;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local2 = @position.Y;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num4 = (double) ^(float&) local2 + (2.0 + (double) num1);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local2 = (float) num4;
      ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, this.GetFriendlyName(), position, baseColor, 0.0f, Vector2.get_Zero(), baseScale, num2, 2f);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local3 = @position.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num5 = (double) ^(float&) local3 - 17.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local3 = (float) num5;
      string text = this.GenInput(PlayerInput.CurrentProfile.InputModes[this._inputmode].KeyStatus[this._keybind]);
      if (string.IsNullOrEmpty(text))
      {
        text = Lang.menu[195].Value;
        if (!flag)
        {
          // ISSUE: explicit reference operation
          ((Color) @baseColor).\u002Ector(80, 80, 80);
        }
      }
      Vector2 stringSize = ChatManager.GetStringSize(Main.fontItemStack, text, baseScale, -1f);
      // ISSUE: explicit reference operation
      ((Vector2) @position).\u002Ector((float) ((double) dimensions.X + (double) dimensions.Width - stringSize.X - 10.0), dimensions.Y + 2f + num1);
      if (this._inputmode == InputMode.XBoxGamepad || this._inputmode == InputMode.XBoxGamepadUI)
        position = Vector2.op_Addition(position, new Vector2(0.0f, -3f));
      GlyphTagHandler.GlyphsScale = 0.85f;
      ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, text, position, baseColor, 0.0f, Vector2.get_Zero(), baseScale, num2, 2f);
      GlyphTagHandler.GlyphsScale = 1f;
    }

    private string GenInput(List<string> list)
    {
      if (list.Count == 0)
        return "";
      string str = "";
      InputMode inputmode = this._inputmode;
      if ((uint) inputmode > 2U)
      {
        if ((uint) (inputmode - 3) <= 1U)
        {
          str = GlyphTagHandler.GenerateTag(list[0]);
          for (int index = 1; index < list.Count; ++index)
            str = str + "/" + GlyphTagHandler.GenerateTag(list[index]);
        }
      }
      else
      {
        str = list[0];
        for (int index = 1; index < list.Count; ++index)
          str = str + "/" + list[index];
      }
      return str;
    }

    private string GetFriendlyName()
    {
      string keybind = this._keybind;
      // ISSUE: reference to a compiler-generated method
      uint stringHash = \u003CPrivateImplementationDetails\u003E.ComputeStringHash(keybind);
      if (stringHash <= 2105504819U)
      {
        if (stringHash <= 1160590752U)
        {
          if (stringHash <= 540272591U)
          {
            if (stringHash <= 236909357U)
            {
              if ((int) stringHash != 135974533)
              {
                if ((int) stringHash == 236909357 && keybind == "Jump")
                  return Lang.menu[152].Value;
              }
              else if (keybind == "QuickBuff")
                return Lang.menu[157].Value;
            }
            else if ((int) stringHash != 341426238)
            {
              if ((int) stringHash != 513712005)
              {
                if ((int) stringHash == 540272591 && keybind == "Throw")
                  return Lang.menu[153].Value;
              }
              else if (keybind == "Right")
                return Lang.menu[151].Value;
            }
            else if (keybind == "MouseRight")
              return Lang.menu[163].Value;
          }
          else if (stringHash <= 731985058U)
          {
            if ((int) stringHash != 553555557)
            {
              if ((int) stringHash != 564091680)
              {
                if ((int) stringHash == 731985058 && keybind == "MapAlphaUp")
                  return Lang.menu[171].Value;
              }
              else if (keybind == "ViewZoomIn")
                return Language.GetTextValue("UI.ZoomIn");
            }
            else if (keybind == "ViewZoomOut")
              return Language.GetTextValue("UI.ZoomOut");
          }
          else if ((int) stringHash != 1038438905)
          {
            if ((int) stringHash != 1123244352)
            {
              if ((int) stringHash == 1160590752 && keybind == "Hotbar1")
                return Lang.menu[176].Value;
            }
            else if (keybind == "Up")
              return Lang.menu[148].Value;
          }
          else if (keybind == "QuickMount")
            return Lang.menu[158].Value;
        }
        else if (stringHash <= 1278034085U)
        {
          if (stringHash <= 1227701228U)
          {
            if ((int) stringHash != 1194145990)
            {
              if ((int) stringHash != 1210923609)
              {
                if ((int) stringHash == 1227701228 && keybind == "Hotbar5")
                  return Lang.menu[180].Value;
              }
              else if (keybind == "Hotbar2")
                return Lang.menu[177].Value;
            }
            else if (keybind == "Hotbar3")
              return Lang.menu[178].Value;
          }
          else if ((int) stringHash != 1244478847)
          {
            if ((int) stringHash != 1261256466)
            {
              if ((int) stringHash == 1278034085 && keybind == "Hotbar6")
                return Lang.menu[181].Value;
            }
            else if (keybind == "Hotbar7")
              return Lang.menu[182].Value;
          }
          else if (keybind == "Hotbar4")
            return Lang.menu[179].Value;
        }
        else if (stringHash <= 1791478331U)
        {
          if ((int) stringHash != 1294811704)
          {
            if ((int) stringHash != 1311589323)
            {
              if ((int) stringHash == 1791478331 && keybind == "LockOn")
                return Lang.menu[231].Value;
            }
            else if (keybind == "Hotbar8")
              return Lang.menu[183].Value;
          }
          else if (keybind == "Hotbar9")
            return Lang.menu[184].Value;
        }
        else if ((int) stringHash != 1825695843)
        {
          if ((int) stringHash != 1982550448)
          {
            if ((int) stringHash == 2105504819 && keybind == "DpadSnap1")
              return Lang.menu[191].Value;
          }
          else if (keybind == "Hotbar10")
            return Lang.menu[185].Value;
        }
        else if (keybind == "MapAlphaDown")
          return Lang.menu[170].Value;
      }
      else if (stringHash <= 3086386178U)
      {
        if (stringHash <= 2761510965U)
        {
          if (stringHash <= 2155837676U)
          {
            if ((int) stringHash != 2122282438)
            {
              if ((int) stringHash != 2139060057)
              {
                if ((int) stringHash == -2139129620 && keybind == "DpadSnap4")
                  return Lang.menu[194].Value;
              }
              else if (keybind == "DpadSnap3")
                return Lang.menu[193].Value;
            }
            else if (keybind == "DpadSnap2")
              return Lang.menu[192].Value;
          }
          else if ((int) stringHash != -1870828789)
          {
            if ((int) stringHash != -1837680496)
            {
              if ((int) stringHash == -1533456331 && keybind == "Down")
                return Lang.menu[149].Value;
            }
            else if (keybind == "Left")
              return Lang.menu[150].Value;
          }
          else if (keybind == "HotbarMinus")
            return Lang.menu[174].Value;
        }
        else if (stringHash <= 3036053321U)
        {
          if ((int) stringHash != -1522232475)
          {
            if ((int) stringHash != -1350530455)
            {
              if ((int) stringHash == -1258913975 && keybind == "DpadRadial4")
                return Lang.menu[189].Value;
            }
            else if (keybind == "MapZoomIn")
              return Lang.menu[168].Value;
          }
          else if (keybind == "MouseLeft")
            return Lang.menu[162].Value;
        }
        else if ((int) stringHash != -1242136356)
        {
          if ((int) stringHash != -1225358737)
          {
            if ((int) stringHash == -1208581118 && keybind == "DpadRadial1")
              return Lang.menu[186].Value;
          }
          else if (keybind == "DpadRadial2")
            return Lang.menu[187].Value;
        }
        else if (keybind == "DpadRadial3")
          return Lang.menu[188].Value;
      }
      else if (stringHash <= 3675592426U)
      {
        if (stringHash <= 3313084571U)
        {
          if ((int) stringHash != -1103163514)
          {
            if ((int) stringHash != -1019331836)
            {
              if ((int) stringHash == -981882725 && keybind == "QuickMana")
                return Lang.menu[156].Value;
            }
            else if (keybind == "Grapple")
              return Lang.menu[155].Value;
          }
          else if (keybind == "QuickHeal")
            return Lang.menu[159].Value;
        }
        else if ((int) stringHash != -925704993)
        {
          if ((int) stringHash != -726245264)
          {
            if ((int) stringHash == -619374870 && keybind == "MapZoomOut")
              return Lang.menu[169].Value;
          }
          else if (keybind == "SmartCursor")
            return Lang.menu[161].Value;
        }
        else if (keybind == "Inventory")
          return Lang.menu[154].Value;
      }
      else if (stringHash <= 4142436984U)
      {
        if ((int) stringHash != -451952954)
        {
          if ((int) stringHash != -342681375)
          {
            if ((int) stringHash == -152530312 && keybind == "MapFull")
              return Lang.menu[173].Value;
          }
          else if (keybind == "HotbarPlus")
            return Lang.menu[175].Value;
        }
        else if (keybind == "RadialHotbar")
          return Lang.menu[190].Value;
      }
      else if ((int) stringHash != -140831030)
      {
        if ((int) stringHash != -77963378)
        {
          if ((int) stringHash == -36980846 && keybind == "MapStyle")
            return Lang.menu[172].Value;
        }
        else if (keybind == "RadialQuickbar")
          return Lang.menu[244].Value;
      }
      else if (keybind == "SmartSelect")
        return Lang.menu[160].Value;
      return this._keybind;
    }
  }
}
