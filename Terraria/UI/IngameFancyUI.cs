// Decompiled with JetBrains decompiler
// Type: Terraria.UI.IngameFancyUI
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Achievements;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI.Gamepad;

namespace Terraria.UI
{
  public class IngameFancyUI
  {
    private static bool CoverForOneUIFrame;

    public static void CoverNextFrame()
    {
      IngameFancyUI.CoverForOneUIFrame = true;
    }

    public static bool CanCover()
    {
      if (!IngameFancyUI.CoverForOneUIFrame)
        return false;
      IngameFancyUI.CoverForOneUIFrame = false;
      return true;
    }

    public static void OpenAchievements()
    {
      IngameFancyUI.CoverNextFrame();
      Main.playerInventory = false;
      Main.editChest = false;
      Main.npcChatText = "";
      Main.inFancyUI = true;
      Main.InGameUI.SetState((UIState) Main.AchievementsMenu);
    }

    public static void OpenAchievementsAndGoto(Achievement achievement)
    {
      IngameFancyUI.OpenAchievements();
      Main.AchievementsMenu.GotoAchievement(achievement);
    }

    public static void OpenKeybinds()
    {
      IngameFancyUI.CoverNextFrame();
      Main.playerInventory = false;
      Main.editChest = false;
      Main.npcChatText = "";
      Main.inFancyUI = true;
      Main.InGameUI.SetState((UIState) Main.ManageControlsMenu);
    }

    public static bool CanShowVirtualKeyboard(int context)
    {
      return UIVirtualKeyboard.CanDisplay(context);
    }

    public static void OpenVirtualKeyboard(int keyboardContext)
    {
      IngameFancyUI.CoverNextFrame();
      Main.PlaySound(12, -1, -1, 1, 1f, 0.0f);
      string str = "";
      if (keyboardContext != 1)
      {
        if (keyboardContext == 2)
        {
          str = Language.GetTextValue("UI.EnterNewName");
          Player player = Main.player[Main.myPlayer];
          Main.npcChatText = Main.chest[player.chest].name;
          if ((int) Main.tile[player.chestX, player.chestY].type == 21)
            Main.defaultChestName = Lang.chestType[(int) Main.tile[player.chestX, player.chestY].frameX / 36].Value;
          if ((int) Main.tile[player.chestX, player.chestY].type == 467)
            Main.defaultChestName = Lang.chestType2[(int) Main.tile[player.chestX, player.chestY].frameX / 36].Value;
          if ((int) Main.tile[player.chestX, player.chestY].type == 88)
            Main.defaultChestName = Lang.dresserType[(int) Main.tile[player.chestX, player.chestY].frameX / 54].Value;
          if (Main.npcChatText == "")
            Main.npcChatText = Main.defaultChestName;
          Main.editChest = true;
        }
      }
      else
      {
        Main.editSign = true;
        str = Language.GetTextValue("UI.EnterMessage");
      }
      Main.clrInput();
      if (!IngameFancyUI.CanShowVirtualKeyboard(keyboardContext))
        return;
      Main.inFancyUI = true;
      if (keyboardContext != 1)
      {
        if (keyboardContext == 2)
        {
          UserInterface inGameUi = Main.InGameUI;
          string labelText = str;
          string npcChatText = Main.npcChatText;
          UIVirtualKeyboard.KeyboardSubmitEvent keyboardSubmitEvent = (UIVirtualKeyboard.KeyboardSubmitEvent) (s =>
          {
            ChestUI.RenameChestSubmit(Main.player[Main.myPlayer]);
            IngameFancyUI.Close();
          });
          int inputMode = keyboardContext;
          int num = 0;
          UIVirtualKeyboard.KeyboardSubmitEvent submitAction;
          UIVirtualKeyboard uiVirtualKeyboard = new UIVirtualKeyboard(labelText, npcChatText, submitAction, (Action) (() =>
          {
            ChestUI.RenameChestCancel();
            IngameFancyUI.Close();
          }), inputMode, num != 0);
          inGameUi.SetState((UIState) uiVirtualKeyboard);
        }
      }
      else
      {
        UserInterface inGameUi = Main.InGameUI;
        string labelText = str;
        string npcChatText = Main.npcChatText;
        UIVirtualKeyboard.KeyboardSubmitEvent keyboardSubmitEvent = (UIVirtualKeyboard.KeyboardSubmitEvent) (s =>
        {
          Main.SubmitSignText();
          IngameFancyUI.Close();
        });
        int inputMode = keyboardContext;
        int num = 0;
        UIVirtualKeyboard.KeyboardSubmitEvent submitAction;
        UIVirtualKeyboard uiVirtualKeyboard = new UIVirtualKeyboard(labelText, npcChatText, submitAction, (Action) (() =>
        {
          Main.InputTextSignCancel();
          IngameFancyUI.Close();
        }), inputMode, num != 0);
        inGameUi.SetState((UIState) uiVirtualKeyboard);
      }
      UILinkPointNavigator.GoToDefaultPage(1);
    }

    public static void Close()
    {
      Main.inFancyUI = false;
      Main.PlaySound(11, -1, -1, 1, 1f, 0.0f);
      if (!Main.gameMenu && (!(Main.InGameUI.CurrentState is UIVirtualKeyboard) || UIVirtualKeyboard.KeyboardContext == 2))
        Main.playerInventory = true;
      Main.InGameUI.SetState((UIState) null);
      UILinkPointNavigator.Shortcuts.FANCYUI_SPECIAL_INSTRUCTIONS = 0;
    }

    public static bool Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
      if (!Main.gameMenu && Main.player[Main.myPlayer].dead && !Main.player[Main.myPlayer].ghost)
      {
        IngameFancyUI.Close();
        Main.playerInventory = false;
        return false;
      }
      bool flag = false;
      if (Main.InGameUI.CurrentState is UIVirtualKeyboard && UIVirtualKeyboard.KeyboardContext > 0)
      {
        if (!Main.inFancyUI)
          Main.InGameUI.SetState((UIState) null);
        if (Main.screenWidth >= 1705 || !PlayerInput.UsingGamepad)
          flag = true;
      }
      if (!Main.gameMenu)
      {
        Main.mouseText = false;
        if (Main.InGameUI != null && Main.InGameUI.IsElementUnderMouse())
          Main.player[Main.myPlayer].mouseInterface = true;
        Main.instance.GUIBarsDraw();
        if (Main.InGameUI.CurrentState is UIVirtualKeyboard && UIVirtualKeyboard.KeyboardContext > 0)
          Main.instance.GUIChatDraw();
        if (!Main.inFancyUI)
          Main.InGameUI.SetState((UIState) null);
        Main.instance.DrawMouseOver();
        Main.DrawCursor(Main.DrawThickCursor(false), false);
      }
      return flag;
    }

    public static void MouseOver()
    {
      if (!Main.inFancyUI || !Main.InGameUI.IsElementUnderMouse())
        return;
      Main.mouseText = true;
    }
  }
}
