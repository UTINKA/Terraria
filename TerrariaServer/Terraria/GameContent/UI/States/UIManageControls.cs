// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UIManageControls
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Graphics;
using Terraria.Initializers;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
  public class UIManageControls : UIState
  {
    public static int ForceMoveTo = -1;
    private static List<string> _BindingsFullLine = new List<string>()
    {
      "Throw",
      "Inventory",
      "RadialHotbar",
      "RadialQuickbar",
      "LockOn",
      "sp3",
      "sp4",
      "sp5",
      "sp6",
      "sp7",
      "sp8",
      "sp18",
      "sp19",
      "sp9",
      "sp10",
      "sp11",
      "sp12",
      "sp13"
    };
    private static List<string> _BindingsHalfSingleLine = new List<string>()
    {
      "sp9",
      "sp10",
      "sp11",
      "sp12",
      "sp13"
    };
    private static int SnapPointIndex = 0;
    private bool OnKeyboard = true;
    private bool OnGameplay = true;
    private List<UIElement> _bindsKeyboard = new List<UIElement>();
    private List<UIElement> _bindsGamepad = new List<UIElement>();
    private List<UIElement> _bindsKeyboardUI = new List<UIElement>();
    private List<UIElement> _bindsGamepadUI = new List<UIElement>();
    private const float PanelTextureHeight = 30f;
    private UIElement _outerContainer;
    private UIList _uilist;
    private UIImageFramed _buttonKeyboard;
    private UIImageFramed _buttonGamepad;
    private UIImageFramed _buttonBorder1;
    private UIImageFramed _buttonBorder2;
    private UIKeybindingSimpleListItem _buttonProfile;
    private UIElement _buttonBack;
    private UIImageFramed _buttonVs1;
    private UIImageFramed _buttonVs2;
    private UIImageFramed _buttonBorderVs1;
    private UIImageFramed _buttonBorderVs2;
    private Texture2D _KeyboardGamepadTexture;
    private Texture2D _keyboardGamepadBorderTexture;
    private Texture2D _GameplayVsUITexture;
    private Texture2D _GameplayVsUIBorderTexture;

    public override void OnInitialize()
    {
      this._KeyboardGamepadTexture = TextureManager.Load("Images/UI/Settings_Inputs");
      this._keyboardGamepadBorderTexture = TextureManager.Load("Images/UI/Settings_Inputs_Border");
      this._GameplayVsUITexture = TextureManager.Load("Images/UI/Settings_Inputs_2");
      this._GameplayVsUIBorderTexture = TextureManager.Load("Images/UI/Settings_Inputs_2_Border");
      UIElement element = new UIElement();
      element.Width.Set(0.0f, 0.8f);
      element.MaxWidth.Set(600f, 0.0f);
      element.Top.Set(220f, 0.0f);
      element.Height.Set(-200f, 1f);
      element.HAlign = 0.5f;
      this._outerContainer = element;
      UIPanel uiPanel = new UIPanel();
      uiPanel.Width.Set(0.0f, 1f);
      uiPanel.Height.Set(-110f, 1f);
      uiPanel.BackgroundColor = Color.op_Multiply(new Color(33, 43, 79), 0.8f);
      element.Append((UIElement) uiPanel);
      this._buttonKeyboard = new UIImageFramed(this._KeyboardGamepadTexture, this._KeyboardGamepadTexture.Frame(2, 2, 0, 0));
      this._buttonKeyboard.VAlign = 0.0f;
      this._buttonKeyboard.HAlign = 0.0f;
      this._buttonKeyboard.Left.Set(0.0f, 0.0f);
      this._buttonKeyboard.Top.Set(8f, 0.0f);
      this._buttonKeyboard.OnClick += new UIElement.MouseEvent(this.KeyboardButtonClick);
      this._buttonKeyboard.OnMouseOver += new UIElement.MouseEvent(this.ManageBorderKeyboardOn);
      this._buttonKeyboard.OnMouseOut += new UIElement.MouseEvent(this.ManageBorderKeyboardOff);
      uiPanel.Append((UIElement) this._buttonKeyboard);
      this._buttonGamepad = new UIImageFramed(this._KeyboardGamepadTexture, this._KeyboardGamepadTexture.Frame(2, 2, 1, 1));
      this._buttonGamepad.VAlign = 0.0f;
      this._buttonGamepad.HAlign = 0.0f;
      this._buttonGamepad.Left.Set(76f, 0.0f);
      this._buttonGamepad.Top.Set(8f, 0.0f);
      this._buttonGamepad.OnClick += new UIElement.MouseEvent(this.GamepadButtonClick);
      this._buttonGamepad.OnMouseOver += new UIElement.MouseEvent(this.ManageBorderGamepadOn);
      this._buttonGamepad.OnMouseOut += new UIElement.MouseEvent(this.ManageBorderGamepadOff);
      uiPanel.Append((UIElement) this._buttonGamepad);
      this._buttonBorder1 = new UIImageFramed(this._keyboardGamepadBorderTexture, this._keyboardGamepadBorderTexture.Frame(1, 1, 0, 0));
      this._buttonBorder1.VAlign = 0.0f;
      this._buttonBorder1.HAlign = 0.0f;
      this._buttonBorder1.Left.Set(0.0f, 0.0f);
      this._buttonBorder1.Top.Set(8f, 0.0f);
      this._buttonBorder1.Color = Color.get_Silver();
      uiPanel.Append((UIElement) this._buttonBorder1);
      this._buttonBorder2 = new UIImageFramed(this._keyboardGamepadBorderTexture, this._keyboardGamepadBorderTexture.Frame(1, 1, 0, 0));
      this._buttonBorder2.VAlign = 0.0f;
      this._buttonBorder2.HAlign = 0.0f;
      this._buttonBorder2.Left.Set(76f, 0.0f);
      this._buttonBorder2.Top.Set(8f, 0.0f);
      this._buttonBorder2.Color = Color.get_Transparent();
      uiPanel.Append((UIElement) this._buttonBorder2);
      this._buttonVs1 = new UIImageFramed(this._GameplayVsUITexture, this._GameplayVsUITexture.Frame(2, 2, 0, 0));
      this._buttonVs1.VAlign = 0.0f;
      this._buttonVs1.HAlign = 0.0f;
      this._buttonVs1.Left.Set(172f, 0.0f);
      this._buttonVs1.Top.Set(8f, 0.0f);
      this._buttonVs1.OnClick += new UIElement.MouseEvent(this.VsGameplayButtonClick);
      this._buttonVs1.OnMouseOver += new UIElement.MouseEvent(this.ManageBorderGameplayOn);
      this._buttonVs1.OnMouseOut += new UIElement.MouseEvent(this.ManageBorderGameplayOff);
      uiPanel.Append((UIElement) this._buttonVs1);
      this._buttonVs2 = new UIImageFramed(this._GameplayVsUITexture, this._GameplayVsUITexture.Frame(2, 2, 1, 1));
      this._buttonVs2.VAlign = 0.0f;
      this._buttonVs2.HAlign = 0.0f;
      this._buttonVs2.Left.Set(212f, 0.0f);
      this._buttonVs2.Top.Set(8f, 0.0f);
      this._buttonVs2.OnClick += new UIElement.MouseEvent(this.VsMenuButtonClick);
      this._buttonVs2.OnMouseOver += new UIElement.MouseEvent(this.ManageBorderMenuOn);
      this._buttonVs2.OnMouseOut += new UIElement.MouseEvent(this.ManageBorderMenuOff);
      uiPanel.Append((UIElement) this._buttonVs2);
      this._buttonBorderVs1 = new UIImageFramed(this._GameplayVsUIBorderTexture, this._GameplayVsUIBorderTexture.Frame(1, 1, 0, 0));
      this._buttonBorderVs1.VAlign = 0.0f;
      this._buttonBorderVs1.HAlign = 0.0f;
      this._buttonBorderVs1.Left.Set(172f, 0.0f);
      this._buttonBorderVs1.Top.Set(8f, 0.0f);
      this._buttonBorderVs1.Color = Color.get_Silver();
      uiPanel.Append((UIElement) this._buttonBorderVs1);
      this._buttonBorderVs2 = new UIImageFramed(this._GameplayVsUIBorderTexture, this._GameplayVsUIBorderTexture.Frame(1, 1, 0, 0));
      this._buttonBorderVs2.VAlign = 0.0f;
      this._buttonBorderVs2.HAlign = 0.0f;
      this._buttonBorderVs2.Left.Set(212f, 0.0f);
      this._buttonBorderVs2.Top.Set(8f, 0.0f);
      this._buttonBorderVs2.Color = Color.get_Transparent();
      uiPanel.Append((UIElement) this._buttonBorderVs2);
      this._buttonProfile = new UIKeybindingSimpleListItem((Func<string>) (() => PlayerInput.CurrentProfile.Name), Color.op_Multiply(new Color(73, 94, 171, (int) byte.MaxValue), 0.9f));
      this._buttonProfile.VAlign = 0.0f;
      this._buttonProfile.HAlign = 1f;
      this._buttonProfile.Width.Set(180f, 0.0f);
      this._buttonProfile.Height.Set(30f, 0.0f);
      this._buttonProfile.MarginRight = 30f;
      this._buttonProfile.Left.Set(0.0f, 0.0f);
      this._buttonProfile.Top.Set(8f, 0.0f);
      this._buttonProfile.OnClick += new UIElement.MouseEvent(this.profileButtonClick);
      uiPanel.Append((UIElement) this._buttonProfile);
      this._uilist = new UIList();
      this._uilist.Width.Set(-25f, 1f);
      this._uilist.Height.Set(-50f, 1f);
      this._uilist.VAlign = 1f;
      this._uilist.PaddingBottom = 5f;
      this._uilist.ListPadding = 20f;
      uiPanel.Append((UIElement) this._uilist);
      this.AssembleBindPanels();
      this.FillList();
      UIScrollbar scrollbar = new UIScrollbar();
      scrollbar.SetView(100f, 1000f);
      scrollbar.Height.Set(-67f, 1f);
      scrollbar.HAlign = 1f;
      scrollbar.VAlign = 1f;
      scrollbar.MarginBottom = 11f;
      uiPanel.Append((UIElement) scrollbar);
      this._uilist.SetScrollbar(scrollbar);
      UITextPanel<LocalizedText> uiTextPanel1 = new UITextPanel<LocalizedText>(Language.GetText("UI.Keybindings"), 0.7f, true);
      uiTextPanel1.HAlign = 0.5f;
      uiTextPanel1.Top.Set(-45f, 0.0f);
      uiTextPanel1.Left.Set(-10f, 0.0f);
      uiTextPanel1.SetPadding(15f);
      uiTextPanel1.BackgroundColor = new Color(73, 94, 171);
      element.Append((UIElement) uiTextPanel1);
      UITextPanel<LocalizedText> uiTextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
      uiTextPanel2.Width.Set(-10f, 0.5f);
      uiTextPanel2.Height.Set(50f, 0.0f);
      uiTextPanel2.VAlign = 1f;
      uiTextPanel2.HAlign = 0.5f;
      uiTextPanel2.Top.Set(-45f, 0.0f);
      uiTextPanel2.OnMouseOver += new UIElement.MouseEvent(this.FadedMouseOver);
      uiTextPanel2.OnMouseOut += new UIElement.MouseEvent(this.FadedMouseOut);
      uiTextPanel2.OnClick += new UIElement.MouseEvent(this.GoBackClick);
      element.Append((UIElement) uiTextPanel2);
      this._buttonBack = (UIElement) uiTextPanel2;
      this.Append(element);
    }

    private void AssembleBindPanels()
    {
      List<string> bindings1 = new List<string>()
      {
        "MouseLeft",
        "MouseRight",
        "Up",
        "Down",
        "Left",
        "Right",
        "Jump",
        "Grapple",
        "SmartSelect",
        "SmartCursor",
        "QuickMount",
        "QuickHeal",
        "QuickMana",
        "QuickBuff",
        "Throw",
        "Inventory",
        "ViewZoomIn",
        "ViewZoomOut",
        "sp9"
      };
      List<string> bindings2 = new List<string>()
      {
        "MouseLeft",
        "MouseRight",
        "Up",
        "Down",
        "Left",
        "Right",
        "Jump",
        "Grapple",
        "SmartSelect",
        "SmartCursor",
        "QuickMount",
        "QuickHeal",
        "QuickMana",
        "QuickBuff",
        "LockOn",
        "Throw",
        "Inventory",
        "sp9"
      };
      List<string> bindings3 = new List<string>()
      {
        "HotbarMinus",
        "HotbarPlus",
        "Hotbar1",
        "Hotbar2",
        "Hotbar3",
        "Hotbar4",
        "Hotbar5",
        "Hotbar6",
        "Hotbar7",
        "Hotbar8",
        "Hotbar9",
        "Hotbar10",
        "sp10"
      };
      List<string> bindings4 = new List<string>()
      {
        "MapZoomIn",
        "MapZoomOut",
        "MapAlphaUp",
        "MapAlphaDown",
        "MapFull",
        "MapStyle",
        "sp11"
      };
      List<string> bindings5 = new List<string>()
      {
        "sp1",
        "sp2",
        "RadialHotbar",
        "RadialQuickbar",
        "sp12"
      };
      List<string> bindings6 = new List<string>()
      {
        "sp3",
        "sp4",
        "sp5",
        "sp6",
        "sp7",
        "sp8",
        "sp14",
        "sp15",
        "sp16",
        "sp17",
        "sp18",
        "sp19",
        "sp13"
      };
      InputMode currentInputMode1 = InputMode.Keyboard;
      this._bindsKeyboard.Add((UIElement) this.CreateBindingGroup(0, bindings1, currentInputMode1));
      this._bindsKeyboard.Add((UIElement) this.CreateBindingGroup(1, bindings4, currentInputMode1));
      this._bindsKeyboard.Add((UIElement) this.CreateBindingGroup(2, bindings3, currentInputMode1));
      InputMode currentInputMode2 = InputMode.XBoxGamepad;
      this._bindsGamepad.Add((UIElement) this.CreateBindingGroup(0, bindings2, currentInputMode2));
      this._bindsGamepad.Add((UIElement) this.CreateBindingGroup(1, bindings4, currentInputMode2));
      this._bindsGamepad.Add((UIElement) this.CreateBindingGroup(2, bindings3, currentInputMode2));
      this._bindsGamepad.Add((UIElement) this.CreateBindingGroup(3, bindings5, currentInputMode2));
      this._bindsGamepad.Add((UIElement) this.CreateBindingGroup(4, bindings6, currentInputMode2));
      InputMode currentInputMode3 = InputMode.KeyboardUI;
      this._bindsKeyboardUI.Add((UIElement) this.CreateBindingGroup(0, bindings1, currentInputMode3));
      this._bindsKeyboardUI.Add((UIElement) this.CreateBindingGroup(1, bindings4, currentInputMode3));
      this._bindsKeyboardUI.Add((UIElement) this.CreateBindingGroup(2, bindings3, currentInputMode3));
      InputMode currentInputMode4 = InputMode.XBoxGamepadUI;
      this._bindsGamepadUI.Add((UIElement) this.CreateBindingGroup(0, bindings2, currentInputMode4));
      this._bindsGamepadUI.Add((UIElement) this.CreateBindingGroup(1, bindings4, currentInputMode4));
      this._bindsGamepadUI.Add((UIElement) this.CreateBindingGroup(2, bindings3, currentInputMode4));
      this._bindsGamepadUI.Add((UIElement) this.CreateBindingGroup(3, bindings5, currentInputMode4));
      this._bindsGamepadUI.Add((UIElement) this.CreateBindingGroup(4, bindings6, currentInputMode4));
    }

    private UISortableElement CreateBindingGroup(int elementIndex, List<string> bindings, InputMode currentInputMode)
    {
      UISortableElement uiSortableElement = new UISortableElement(elementIndex);
      uiSortableElement.HAlign = 0.5f;
      uiSortableElement.Width.Set(0.0f, 1f);
      uiSortableElement.Height.Set(2000f, 0.0f);
      UIPanel uiPanel = new UIPanel();
      uiPanel.Width.Set(0.0f, 1f);
      uiPanel.Height.Set(-16f, 1f);
      uiPanel.VAlign = 1f;
      uiPanel.BackgroundColor = Color.op_Multiply(new Color(33, 43, 79), 0.8f);
      uiSortableElement.Append((UIElement) uiPanel);
      UIList parent = new UIList();
      parent.OverflowHidden = false;
      parent.Width.Set(0.0f, 1f);
      parent.Height.Set(-8f, 1f);
      parent.VAlign = 1f;
      parent.ListPadding = 5f;
      uiPanel.Append((UIElement) parent);
      Color backgroundColor = uiPanel.BackgroundColor;
      switch (elementIndex)
      {
        case 0:
          uiPanel.BackgroundColor = Color.Lerp(uiPanel.BackgroundColor, Color.get_Green(), 0.18f);
          break;
        case 1:
          uiPanel.BackgroundColor = Color.Lerp(uiPanel.BackgroundColor, Color.get_Goldenrod(), 0.18f);
          break;
        case 2:
          uiPanel.BackgroundColor = Color.Lerp(uiPanel.BackgroundColor, Color.get_HotPink(), 0.18f);
          break;
        case 3:
          uiPanel.BackgroundColor = Color.Lerp(uiPanel.BackgroundColor, Color.get_Indigo(), 0.18f);
          break;
        case 4:
          uiPanel.BackgroundColor = Color.Lerp(uiPanel.BackgroundColor, Color.get_Turquoise(), 0.18f);
          break;
      }
      this.CreateElementGroup(parent, bindings, currentInputMode, uiPanel.BackgroundColor);
      uiPanel.BackgroundColor = uiPanel.BackgroundColor.MultiplyRGBA(new Color(111, 111, 111));
      LocalizedText text = LocalizedText.Empty;
      switch (elementIndex)
      {
        case 0:
          text = currentInputMode == InputMode.Keyboard || currentInputMode == InputMode.XBoxGamepad ? Lang.menu[164] : Lang.menu[243];
          break;
        case 1:
          text = Lang.menu[165];
          break;
        case 2:
          text = Lang.menu[166];
          break;
        case 3:
          text = Lang.menu[167];
          break;
        case 4:
          text = Lang.menu[198];
          break;
      }
      UITextPanel<LocalizedText> uiTextPanel1 = new UITextPanel<LocalizedText>(text, 0.7f, false);
      uiTextPanel1.VAlign = 0.0f;
      uiTextPanel1.HAlign = 0.5f;
      UITextPanel<LocalizedText> uiTextPanel2 = uiTextPanel1;
      uiSortableElement.Append((UIElement) uiTextPanel2);
      uiSortableElement.Recalculate();
      float totalHeight = parent.GetTotalHeight();
      uiSortableElement.Width.Set(0.0f, 1f);
      uiSortableElement.Height.Set((float) ((double) totalHeight + 30.0 + 16.0), 0.0f);
      return uiSortableElement;
    }

    private void CreateElementGroup(UIList parent, List<string> bindings, InputMode currentInputMode, Color color)
    {
      for (int index = 0; index < bindings.Count; ++index)
      {
        string binding = bindings[index];
        UISortableElement uiSortableElement = new UISortableElement(index);
        uiSortableElement.Width.Set(0.0f, 1f);
        uiSortableElement.Height.Set(30f, 0.0f);
        uiSortableElement.HAlign = 0.5f;
        parent.Add((UIElement) uiSortableElement);
        if (UIManageControls._BindingsHalfSingleLine.Contains(bindings[index]))
        {
          UIElement panel = this.CreatePanel(bindings[index], currentInputMode, color);
          panel.Width.Set(0.0f, 0.5f);
          panel.HAlign = 0.5f;
          panel.Height.Set(0.0f, 1f);
          panel.SetSnapPoint("Wide", UIManageControls.SnapPointIndex++, new Vector2?(), new Vector2?());
          uiSortableElement.Append(panel);
        }
        else if (UIManageControls._BindingsFullLine.Contains(bindings[index]))
        {
          UIElement panel = this.CreatePanel(bindings[index], currentInputMode, color);
          panel.Width.Set(0.0f, 1f);
          panel.Height.Set(0.0f, 1f);
          panel.SetSnapPoint("Wide", UIManageControls.SnapPointIndex++, new Vector2?(), new Vector2?());
          uiSortableElement.Append(panel);
        }
        else
        {
          UIElement panel1 = this.CreatePanel(bindings[index], currentInputMode, color);
          panel1.Width.Set(-5f, 0.5f);
          panel1.Height.Set(0.0f, 1f);
          panel1.SetSnapPoint("Thin", UIManageControls.SnapPointIndex++, new Vector2?(), new Vector2?());
          uiSortableElement.Append(panel1);
          ++index;
          if (index < bindings.Count)
          {
            UIElement panel2 = this.CreatePanel(bindings[index], currentInputMode, color);
            panel2.Width.Set(-5f, 0.5f);
            panel2.Height.Set(0.0f, 1f);
            panel2.HAlign = 1f;
            panel2.SetSnapPoint("Thin", UIManageControls.SnapPointIndex++, new Vector2?(), new Vector2?());
            uiSortableElement.Append(panel2);
          }
        }
      }
    }

    public UIElement CreatePanel(string bind, InputMode currentInputMode, Color color)
    {
      switch (bind)
      {
        case "sp1":
          UIElement uiElement1 = (UIElement) new UIKeybindingToggleListItem((Func<string>) (() => Lang.menu[196].Value), (Func<bool>) (() =>
          {
            if (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"].Contains(((object) (Buttons) 1).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"].Contains(((object) (Buttons) 8).ToString()) && (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"].Contains(((object) (Buttons) 2).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"].Contains(((object) (Buttons) 4).ToString())) && (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"].Contains(((object) (Buttons) 1).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"].Contains(((object) (Buttons) 8).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"].Contains(((object) (Buttons) 2).ToString())))
              return PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"].Contains(((object) (Buttons) 4).ToString());
            return false;
          }), color);
          uiElement1.OnClick += new UIElement.MouseEvent(this.SnapButtonClick);
          return uiElement1;
        case "sp2":
          UIElement uiElement2 = (UIElement) new UIKeybindingToggleListItem((Func<string>) (() => Lang.menu[197].Value), (Func<bool>) (() =>
          {
            if (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"].Contains(((object) (Buttons) 1).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"].Contains(((object) (Buttons) 8).ToString()) && (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"].Contains(((object) (Buttons) 2).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"].Contains(((object) (Buttons) 4).ToString())) && (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"].Contains(((object) (Buttons) 1).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"].Contains(((object) (Buttons) 8).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"].Contains(((object) (Buttons) 2).ToString())))
              return PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"].Contains(((object) (Buttons) 4).ToString());
            return false;
          }), color);
          uiElement2.OnClick += new UIElement.MouseEvent(this.RadialButtonClick);
          return uiElement2;
        case "sp3":
          return (UIElement) new UIKeybindingSliderItem((Func<string>) (() => Lang.menu[199].Value + " (" + PlayerInput.CurrentProfile.TriggersDeadzone.ToString("P1") + ")"), (Func<float>) (() => PlayerInput.CurrentProfile.TriggersDeadzone), (Action<float>) (f => PlayerInput.CurrentProfile.TriggersDeadzone = f), (Action) (() => PlayerInput.CurrentProfile.TriggersDeadzone = UILinksInitializer.HandleSlider(PlayerInput.CurrentProfile.TriggersDeadzone, 0.0f, 0.95f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f)), 1000, color);
        case "sp4":
          return (UIElement) new UIKeybindingSliderItem((Func<string>) (() => Lang.menu[200].Value + " (" + PlayerInput.CurrentProfile.InterfaceDeadzoneX.ToString("P1") + ")"), (Func<float>) (() => PlayerInput.CurrentProfile.InterfaceDeadzoneX), (Action<float>) (f => PlayerInput.CurrentProfile.InterfaceDeadzoneX = f), (Action) (() => PlayerInput.CurrentProfile.InterfaceDeadzoneX = UILinksInitializer.HandleSlider(PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.0f, 0.95f, 0.35f, 0.35f)), 1001, color);
        case "sp5":
          return (UIElement) new UIKeybindingSliderItem((Func<string>) (() => Lang.menu[201].Value + " (" + PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX.ToString("P1") + ")"), (Func<float>) (() => PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX), (Action<float>) (f => PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX = f), (Action) (() => PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX = UILinksInitializer.HandleSlider(PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX, 0.0f, 0.95f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f)), 1002, color);
        case "sp6":
          return (UIElement) new UIKeybindingSliderItem((Func<string>) (() => Lang.menu[202].Value + " (" + PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY.ToString("P1") + ")"), (Func<float>) (() => PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY), (Action<float>) (f => PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY = f), (Action) (() => PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY = UILinksInitializer.HandleSlider(PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY, 0.0f, 0.95f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f)), 1003, color);
        case "sp7":
          return (UIElement) new UIKeybindingSliderItem((Func<string>) (() => Lang.menu[203].Value + " (" + PlayerInput.CurrentProfile.RightThumbstickDeadzoneX.ToString("P1") + ")"), (Func<float>) (() => PlayerInput.CurrentProfile.RightThumbstickDeadzoneX), (Action<float>) (f => PlayerInput.CurrentProfile.RightThumbstickDeadzoneX = f), (Action) (() => PlayerInput.CurrentProfile.RightThumbstickDeadzoneX = UILinksInitializer.HandleSlider(PlayerInput.CurrentProfile.RightThumbstickDeadzoneX, 0.0f, 0.95f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f)), 1004, color);
        case "sp8":
          return (UIElement) new UIKeybindingSliderItem((Func<string>) (() => Lang.menu[204].Value + " (" + PlayerInput.CurrentProfile.RightThumbstickDeadzoneY.ToString("P1") + ")"), (Func<float>) (() => PlayerInput.CurrentProfile.RightThumbstickDeadzoneY), (Action<float>) (f => PlayerInput.CurrentProfile.RightThumbstickDeadzoneY = f), (Action) (() => PlayerInput.CurrentProfile.RightThumbstickDeadzoneY = UILinksInitializer.HandleSlider(PlayerInput.CurrentProfile.RightThumbstickDeadzoneY, 0.0f, 0.95f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f)), 1005, color);
        case "sp9":
          UIElement uiElement3 = (UIElement) new UIKeybindingSimpleListItem((Func<string>) (() => Lang.menu[86].Value), color);
          uiElement3.OnClick += (UIElement.MouseEvent) ((evt, listeningElement) =>
          {
            string copyableProfileName = UIManageControls.GetCopyableProfileName();
            PlayerInput.CurrentProfile.CopyGameplaySettingsFrom(PlayerInput.OriginalProfiles[copyableProfileName], currentInputMode);
          });
          return uiElement3;
        case "sp10":
          UIElement uiElement4 = (UIElement) new UIKeybindingSimpleListItem((Func<string>) (() => Lang.menu[86].Value), color);
          uiElement4.OnClick += (UIElement.MouseEvent) ((evt, listeningElement) =>
          {
            string copyableProfileName = UIManageControls.GetCopyableProfileName();
            PlayerInput.CurrentProfile.CopyHotbarSettingsFrom(PlayerInput.OriginalProfiles[copyableProfileName], currentInputMode);
          });
          return uiElement4;
        case "sp11":
          UIElement uiElement5 = (UIElement) new UIKeybindingSimpleListItem((Func<string>) (() => Lang.menu[86].Value), color);
          uiElement5.OnClick += (UIElement.MouseEvent) ((evt, listeningElement) =>
          {
            string copyableProfileName = UIManageControls.GetCopyableProfileName();
            PlayerInput.CurrentProfile.CopyMapSettingsFrom(PlayerInput.OriginalProfiles[copyableProfileName], currentInputMode);
          });
          return uiElement5;
        case "sp12":
          UIElement uiElement6 = (UIElement) new UIKeybindingSimpleListItem((Func<string>) (() => Lang.menu[86].Value), color);
          uiElement6.OnClick += (UIElement.MouseEvent) ((evt, listeningElement) =>
          {
            string copyableProfileName = UIManageControls.GetCopyableProfileName();
            PlayerInput.CurrentProfile.CopyGamepadSettingsFrom(PlayerInput.OriginalProfiles[copyableProfileName], currentInputMode);
          });
          return uiElement6;
        case "sp13":
          UIElement uiElement7 = (UIElement) new UIKeybindingSimpleListItem((Func<string>) (() => Lang.menu[86].Value), color);
          uiElement7.OnClick += (UIElement.MouseEvent) ((evt, listeningElement) =>
          {
            string copyableProfileName = UIManageControls.GetCopyableProfileName();
            PlayerInput.CurrentProfile.CopyGamepadAdvancedSettingsFrom(PlayerInput.OriginalProfiles[copyableProfileName], currentInputMode);
          });
          return uiElement7;
        case "sp14":
          UIElement uiElement8 = (UIElement) new UIKeybindingToggleListItem((Func<string>) (() => Lang.menu[205].Value), (Func<bool>) (() => PlayerInput.CurrentProfile.LeftThumbstickInvertX), color);
          uiElement8.OnClick += (UIElement.MouseEvent) ((evt, listeningElement) =>
          {
            if (!PlayerInput.CurrentProfile.AllowEditting)
              return;
            PlayerInput.CurrentProfile.LeftThumbstickInvertX = !PlayerInput.CurrentProfile.LeftThumbstickInvertX;
          });
          return uiElement8;
        case "sp15":
          UIElement uiElement9 = (UIElement) new UIKeybindingToggleListItem((Func<string>) (() => Lang.menu[206].Value), (Func<bool>) (() => PlayerInput.CurrentProfile.LeftThumbstickInvertY), color);
          uiElement9.OnClick += (UIElement.MouseEvent) ((evt, listeningElement) =>
          {
            if (!PlayerInput.CurrentProfile.AllowEditting)
              return;
            PlayerInput.CurrentProfile.LeftThumbstickInvertY = !PlayerInput.CurrentProfile.LeftThumbstickInvertY;
          });
          return uiElement9;
        case "sp16":
          UIElement uiElement10 = (UIElement) new UIKeybindingToggleListItem((Func<string>) (() => Lang.menu[207].Value), (Func<bool>) (() => PlayerInput.CurrentProfile.RightThumbstickInvertX), color);
          uiElement10.OnClick += (UIElement.MouseEvent) ((evt, listeningElement) =>
          {
            if (!PlayerInput.CurrentProfile.AllowEditting)
              return;
            PlayerInput.CurrentProfile.RightThumbstickInvertX = !PlayerInput.CurrentProfile.RightThumbstickInvertX;
          });
          return uiElement10;
        case "sp17":
          UIElement uiElement11 = (UIElement) new UIKeybindingToggleListItem((Func<string>) (() => Lang.menu[208].Value), (Func<bool>) (() => PlayerInput.CurrentProfile.RightThumbstickInvertY), color);
          uiElement11.OnClick += (UIElement.MouseEvent) ((evt, listeningElement) =>
          {
            if (!PlayerInput.CurrentProfile.AllowEditting)
              return;
            PlayerInput.CurrentProfile.RightThumbstickInvertY = !PlayerInput.CurrentProfile.RightThumbstickInvertY;
          });
          return uiElement11;
        case "sp18":
          return (UIElement) new UIKeybindingSliderItem((Func<string>) (() =>
          {
            int holdTimeRequired = PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired;
            if (holdTimeRequired == -1)
              return Lang.menu[228].Value;
            return Lang.menu[227].Value + " (" + ((float) holdTimeRequired / 60f).ToString("F2") + "s)";
          }), (Func<float>) (() =>
          {
            if (PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired == -1)
              return 1f;
            return (float) PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired / 301f;
          }), (Action<float>) (f =>
          {
            PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired = (int) ((double) f * 301.0);
            if ((double) PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired != 301.0)
              return;
            PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired = -1;
          }), (Action) (() =>
          {
            PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired = (int) ((double) UILinksInitializer.HandleSlider(PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired == -1 ? 1f : (float) PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired / 301f, 0.0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f) * 301.0);
            if ((double) PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired != 301.0)
              return;
            PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired = -1;
          }), 1007, color);
        case "sp19":
          return (UIElement) new UIKeybindingSliderItem((Func<string>) (() =>
          {
            int inventoryMoveCd = PlayerInput.CurrentProfile.InventoryMoveCD;
            return Lang.menu[252].Value + " (" + ((float) inventoryMoveCd / 60f).ToString("F2") + "s)";
          }), (Func<float>) (() => Utils.InverseLerp(4f, 12f, (float) PlayerInput.CurrentProfile.InventoryMoveCD, true)), (Action<float>) (f => PlayerInput.CurrentProfile.InventoryMoveCD = (int) Math.Round((double) MathHelper.Lerp(4f, 12f, f))), (Action) (() =>
          {
            if (UILinkPointNavigator.Shortcuts.INV_MOVE_OPTION_CD > 0)
              --UILinkPointNavigator.Shortcuts.INV_MOVE_OPTION_CD;
            if (UILinkPointNavigator.Shortcuts.INV_MOVE_OPTION_CD != 0)
              return;
            float currentValue = Utils.InverseLerp(4f, 12f, (float) PlayerInput.CurrentProfile.InventoryMoveCD, true);
            float num = UILinksInitializer.HandleSlider(currentValue, 0.0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f);
            if ((double) currentValue == (double) num)
              return;
            UILinkPointNavigator.Shortcuts.INV_MOVE_OPTION_CD = 8;
            PlayerInput.CurrentProfile.InventoryMoveCD = (int) MathHelper.Clamp((float) (PlayerInput.CurrentProfile.InventoryMoveCD + Math.Sign(num - currentValue)), 4f, 12f);
          }), 1008, color);
        default:
          return (UIElement) new UIKeybindingListItem(bind, currentInputMode, color);
      }
    }

    public override void OnActivate()
    {
      if (Main.gameMenu)
      {
        this._outerContainer.Top.Set(220f, 0.0f);
        this._outerContainer.Height.Set(-220f, 1f);
      }
      else
      {
        this._outerContainer.Top.Set(120f, 0.0f);
        this._outerContainer.Height.Set(-120f, 1f);
      }
      if (!PlayerInput.UsingGamepadUI)
        return;
      UILinkPointNavigator.ChangePoint(3002);
    }

    private static string GetCopyableProfileName()
    {
      string str = "Redigit's Pick";
      if (PlayerInput.OriginalProfiles.ContainsKey(PlayerInput.CurrentProfile.Name))
        str = PlayerInput.CurrentProfile.Name;
      return str;
    }

    private void FillList()
    {
      List<UIElement> uiElementList = this._bindsKeyboard;
      if (!this.OnKeyboard)
        uiElementList = this._bindsGamepad;
      if (!this.OnGameplay)
        uiElementList = this.OnKeyboard ? this._bindsKeyboardUI : this._bindsGamepadUI;
      this._uilist.Clear();
      foreach (UIElement uiElement in uiElementList)
        this._uilist.Add(uiElement);
    }

    private void SnapButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      if (!PlayerInput.CurrentProfile.AllowEditting)
        return;
      Main.PlaySound(12, -1, -1, 1, 1f, 0.0f);
      if (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"].Contains(((object) (Buttons) 1).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"].Contains(((object) (Buttons) 8).ToString()) && (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"].Contains(((object) (Buttons) 2).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"].Contains(((object) (Buttons) 4).ToString())) && (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"].Contains(((object) (Buttons) 1).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"].Contains(((object) (Buttons) 8).ToString()) && (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"].Contains(((object) (Buttons) 2).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"].Contains(((object) (Buttons) 4).ToString()))))
      {
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"].Clear();
      }
      else
      {
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"] = new List<string>()
        {
          ((object) (Buttons) 1).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"] = new List<string>()
        {
          ((object) (Buttons) 8).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"] = new List<string>()
        {
          ((object) (Buttons) 2).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"] = new List<string>()
        {
          ((object) (Buttons) 4).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"] = new List<string>()
        {
          ((object) (Buttons) 1).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"] = new List<string>()
        {
          ((object) (Buttons) 8).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"] = new List<string>()
        {
          ((object) (Buttons) 2).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"] = new List<string>()
        {
          ((object) (Buttons) 4).ToString()
        };
      }
    }

    private void RadialButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      if (!PlayerInput.CurrentProfile.AllowEditting)
        return;
      Main.PlaySound(12, -1, -1, 1, 1f, 0.0f);
      if (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"].Contains(((object) (Buttons) 1).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"].Contains(((object) (Buttons) 8).ToString()) && (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"].Contains(((object) (Buttons) 2).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"].Contains(((object) (Buttons) 4).ToString())) && (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"].Contains(((object) (Buttons) 1).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"].Contains(((object) (Buttons) 8).ToString()) && (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"].Contains(((object) (Buttons) 2).ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"].Contains(((object) (Buttons) 4).ToString()))))
      {
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"].Clear();
      }
      else
      {
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"].Clear();
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"] = new List<string>()
        {
          ((object) (Buttons) 1).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"] = new List<string>()
        {
          ((object) (Buttons) 8).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"] = new List<string>()
        {
          ((object) (Buttons) 2).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"] = new List<string>()
        {
          ((object) (Buttons) 4).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"] = new List<string>()
        {
          ((object) (Buttons) 1).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"] = new List<string>()
        {
          ((object) (Buttons) 8).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"] = new List<string>()
        {
          ((object) (Buttons) 2).ToString()
        };
        PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"] = new List<string>()
        {
          ((object) (Buttons) 4).ToString()
        };
      }
    }

    private void KeyboardButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonKeyboard.SetFrame(this._KeyboardGamepadTexture.Frame(2, 2, 0, 0));
      this._buttonGamepad.SetFrame(this._KeyboardGamepadTexture.Frame(2, 2, 1, 1));
      this.OnKeyboard = true;
      this.FillList();
    }

    private void GamepadButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonKeyboard.SetFrame(this._KeyboardGamepadTexture.Frame(2, 2, 0, 1));
      this._buttonGamepad.SetFrame(this._KeyboardGamepadTexture.Frame(2, 2, 1, 0));
      this.OnKeyboard = false;
      this.FillList();
    }

    private void ManageBorderKeyboardOn(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonBorder2.Color = !this.OnKeyboard ? Color.get_Silver() : Color.get_Black();
      this._buttonBorder1.Color = Main.OurFavoriteColor;
    }

    private void ManageBorderKeyboardOff(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonBorder2.Color = !this.OnKeyboard ? Color.get_Silver() : Color.get_Black();
      this._buttonBorder1.Color = this.OnKeyboard ? Color.get_Silver() : Color.get_Black();
    }

    private void ManageBorderGamepadOn(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonBorder1.Color = this.OnKeyboard ? Color.get_Silver() : Color.get_Black();
      this._buttonBorder2.Color = Main.OurFavoriteColor;
    }

    private void ManageBorderGamepadOff(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonBorder1.Color = this.OnKeyboard ? Color.get_Silver() : Color.get_Black();
      this._buttonBorder2.Color = !this.OnKeyboard ? Color.get_Silver() : Color.get_Black();
    }

    private void VsGameplayButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonVs1.SetFrame(this._GameplayVsUITexture.Frame(2, 2, 0, 0));
      this._buttonVs2.SetFrame(this._GameplayVsUITexture.Frame(2, 2, 1, 1));
      this.OnGameplay = true;
      this.FillList();
    }

    private void VsMenuButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonVs1.SetFrame(this._GameplayVsUITexture.Frame(2, 2, 0, 1));
      this._buttonVs2.SetFrame(this._GameplayVsUITexture.Frame(2, 2, 1, 0));
      this.OnGameplay = false;
      this.FillList();
    }

    private void ManageBorderGameplayOn(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonBorderVs2.Color = !this.OnGameplay ? Color.get_Silver() : Color.get_Black();
      this._buttonBorderVs1.Color = Main.OurFavoriteColor;
    }

    private void ManageBorderGameplayOff(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonBorderVs2.Color = !this.OnGameplay ? Color.get_Silver() : Color.get_Black();
      this._buttonBorderVs1.Color = this.OnGameplay ? Color.get_Silver() : Color.get_Black();
    }

    private void ManageBorderMenuOn(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonBorderVs1.Color = this.OnGameplay ? Color.get_Silver() : Color.get_Black();
      this._buttonBorderVs2.Color = Main.OurFavoriteColor;
    }

    private void ManageBorderMenuOff(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonBorderVs1.Color = this.OnGameplay ? Color.get_Silver() : Color.get_Black();
      this._buttonBorderVs2.Color = !this.OnGameplay ? Color.get_Silver() : Color.get_Black();
    }

    private void profileButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      string name = PlayerInput.CurrentProfile.Name;
      List<string> list = PlayerInput.Profiles.Keys.ToList<string>();
      int index = list.IndexOf(name) + 1;
      if (index >= list.Count)
        index -= list.Count;
      PlayerInput.SetSelectedProfile(list[index]);
    }

    private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      Main.PlaySound(12, -1, -1, 1, 1f, 0.0f);
      ((UIPanel) evt.Target).BackgroundColor = new Color(73, 94, 171);
    }

    private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
      ((UIPanel) evt.Target).BackgroundColor = Color.op_Multiply(new Color(63, 82, 151), 0.7f);
    }

    private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
    {
      Main.menuMode = 1127;
      IngameFancyUI.Close();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      this.SetupGamepadPoints(spriteBatch);
    }

    private void SetupGamepadPoints(SpriteBatch spriteBatch)
    {
      UILinkPointNavigator.Shortcuts.BackButtonCommand = 4;
      int index1 = 3000;
      int ID1 = index1;
      Rectangle rectangle1 = this._buttonBack.GetInnerDimensions().ToRectangle();
      // ISSUE: explicit reference operation
      Vector2 vector2_1 = ((Rectangle) @rectangle1).get_Center().ToVector2();
      UILinkPointNavigator.SetPosition(ID1, vector2_1);
      int ID2 = index1 + 1;
      Rectangle rectangle2 = this._buttonKeyboard.GetInnerDimensions().ToRectangle();
      // ISSUE: explicit reference operation
      Vector2 vector2_2 = ((Rectangle) @rectangle2).get_Center().ToVector2();
      UILinkPointNavigator.SetPosition(ID2, vector2_2);
      int ID3 = index1 + 2;
      Rectangle rectangle3 = this._buttonGamepad.GetInnerDimensions().ToRectangle();
      // ISSUE: explicit reference operation
      Vector2 vector2_3 = ((Rectangle) @rectangle3).get_Center().ToVector2();
      UILinkPointNavigator.SetPosition(ID3, vector2_3);
      int ID4 = index1 + 3;
      Rectangle rectangle4 = this._buttonProfile.GetInnerDimensions().ToRectangle();
      // ISSUE: explicit reference operation
      Vector2 vector2_4 = ((Rectangle) @rectangle4).get_Center().ToVector2();
      UILinkPointNavigator.SetPosition(ID4, vector2_4);
      int ID5 = index1 + 4;
      Rectangle rectangle5 = this._buttonVs1.GetInnerDimensions().ToRectangle();
      // ISSUE: explicit reference operation
      Vector2 vector2_5 = ((Rectangle) @rectangle5).get_Center().ToVector2();
      UILinkPointNavigator.SetPosition(ID5, vector2_5);
      int ID6 = index1 + 5;
      Rectangle rectangle6 = this._buttonVs2.GetInnerDimensions().ToRectangle();
      // ISSUE: explicit reference operation
      Vector2 vector2_6 = ((Rectangle) @rectangle6).get_Center().ToVector2();
      UILinkPointNavigator.SetPosition(ID6, vector2_6);
      int index2 = index1;
      UILinkPoint point1 = UILinkPointNavigator.Points[index2];
      point1.Unlink();
      point1.Up = index1 + 6;
      int index3 = index1 + 1;
      UILinkPoint point2 = UILinkPointNavigator.Points[index3];
      point2.Unlink();
      point2.Right = index1 + 2;
      point2.Down = index1 + 6;
      int index4 = index1 + 2;
      UILinkPoint point3 = UILinkPointNavigator.Points[index4];
      point3.Unlink();
      point3.Left = index1 + 1;
      point3.Right = index1 + 4;
      point3.Down = index1 + 6;
      int index5 = index1 + 4;
      UILinkPoint point4 = UILinkPointNavigator.Points[index5];
      point4.Unlink();
      point4.Left = index1 + 2;
      point4.Right = index1 + 5;
      point4.Down = index1 + 6;
      int index6 = index1 + 5;
      UILinkPoint point5 = UILinkPointNavigator.Points[index6];
      point5.Unlink();
      point5.Left = index1 + 4;
      point5.Right = index1 + 3;
      point5.Down = index1 + 6;
      int index7 = index1 + 3;
      UILinkPoint point6 = UILinkPointNavigator.Points[index7];
      point6.Unlink();
      point6.Left = index1 + 5;
      point6.Down = index1 + 6;
      float num = 1f / Main.UIScale;
      Rectangle clippingRectangle = this._uilist.GetClippingRectangle(spriteBatch);
      Vector2 minimum = Vector2.op_Multiply(clippingRectangle.TopLeft(), num);
      Vector2 maximum = Vector2.op_Multiply(clippingRectangle.BottomRight(), num);
      List<SnapPoint> snapPoints = this._uilist.GetSnapPoints();
      for (int index8 = 0; index8 < snapPoints.Count; ++index8)
      {
        if (!snapPoints[index8].Position.Between(minimum, maximum))
        {
          Vector2 position = snapPoints[index8].Position;
          snapPoints.Remove(snapPoints[index8]);
          --index8;
        }
      }
      snapPoints.Sort((Comparison<SnapPoint>) ((x, y) => x.ID.CompareTo(y.ID)));
      for (int index8 = 0; index8 < snapPoints.Count; ++index8)
      {
        int ID7 = index1 + 6 + index8;
        if (snapPoints[index8].Name == "Thin")
        {
          UILinkPoint point7 = UILinkPointNavigator.Points[ID7];
          point7.Unlink();
          UILinkPointNavigator.SetPosition(ID7, snapPoints[index8].Position);
          point7.Right = ID7 + 1;
          point7.Down = index8 < snapPoints.Count - 2 ? ID7 + 2 : index1;
          point7.Up = index8 < 2 ? index1 + 1 : (snapPoints[index8 - 1].Name == "Wide" ? ID7 - 1 : ID7 - 2);
          UILinkPointNavigator.Points[index1].Up = ID7;
          UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = ID7;
          ++index8;
          if (index8 < snapPoints.Count)
          {
            int ID8 = index1 + 6 + index8;
            UILinkPoint point8 = UILinkPointNavigator.Points[ID8];
            point8.Unlink();
            UILinkPointNavigator.SetPosition(ID8, snapPoints[index8].Position);
            point8.Left = ID8 - 1;
            point8.Down = index8 < snapPoints.Count - 1 ? (snapPoints[index8 + 1].Name == "Wide" ? ID8 + 1 : ID8 + 2) : index1;
            point8.Up = index8 < 2 ? index1 + 1 : ID8 - 2;
            UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = ID8;
          }
        }
        else
        {
          UILinkPoint point7 = UILinkPointNavigator.Points[ID7];
          point7.Unlink();
          UILinkPointNavigator.SetPosition(ID7, snapPoints[index8].Position);
          point7.Down = index8 < snapPoints.Count - 1 ? ID7 + 1 : index1;
          point7.Up = index8 < 1 ? index1 + 1 : (snapPoints[index8 - 1].Name == "Wide" ? ID7 - 1 : ID7 - 2);
          UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = ID7;
          UILinkPointNavigator.Points[index1].Up = ID7;
        }
      }
      if (UIManageControls.ForceMoveTo == -1)
        return;
      UILinkPointNavigator.ChangePoint((int) MathHelper.Clamp((float) UIManageControls.ForceMoveTo, (float) index1, (float) UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX));
      UIManageControls.ForceMoveTo = -1;
    }
  }
}
