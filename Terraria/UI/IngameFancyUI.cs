// Decompiled with JetBrains decompiler
// Type: Terraria.UI.IngameFancyUI
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Achievements;
using Terraria.GameContent.UI.States;
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
      string labelText = "";
      switch (keyboardContext)
      {
        case 1:
          Main.editSign = true;
          labelText = Language.GetTextValue("UI.EnterMessage");
          break;
        case 2:
          labelText = Language.GetTextValue("UI.EnterNewName");
          Player player = Main.player[Main.myPlayer];
          Main.npcChatText = Main.chest[player.chest].name;
          if ((int) Main.tile[player.chestX, player.chestY].type == 21)
            Main.defaultChestName = Lang.chestType[(int) Main.tile[player.chestX, player.chestY].frameX / 36];
          if ((int) Main.tile[player.chestX, player.chestY].type == 88)
            Main.defaultChestName = Lang.dresserType[(int) Main.tile[player.chestX, player.chestY].frameX / 54];
          if (Main.npcChatText == "")
            Main.npcChatText = Main.defaultChestName;
          Main.editChest = true;
          break;
      }
      Main.clrInput();
      if (!IngameFancyUI.CanShowVirtualKeyboard(keyboardContext))
        return;
      Main.inFancyUI = true;
      switch (keyboardContext)
      {
        case 1:
          Main.InGameUI.SetState((UIState) new UIVirtualKeyboard(labelText, Main.npcChatText, (UIVirtualKeyboard.KeyboardSubmitEvent) (s =>
          {
            Main.SubmitSignText();
            IngameFancyUI.Close();
          }), (Action) (() =>
          {
            Main.InputTextSignCancel();
            IngameFancyUI.Close();
          }), keyboardContext, false));
          break;
        case 2:
          Main.InGameUI.SetState((UIState) new UIVirtualKeyboard(labelText, Main.npcChatText, (UIVirtualKeyboard.KeyboardSubmitEvent) (s =>
          {
            ChestUI.RenameChestSubmit(Main.player[Main.myPlayer]);
            IngameFancyUI.Close();
          }), (Action) (() =>
          {
            ChestUI.RenameChestCancel();
            IngameFancyUI.Close();
          }), keyboardContext, false));
          break;
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
        if (Main.screenWidth >= 1705)
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
