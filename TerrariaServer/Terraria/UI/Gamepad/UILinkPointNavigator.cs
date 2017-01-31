// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Gamepad.UILinkPointNavigator
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameInput;

namespace Terraria.UI.Gamepad
{
  public class UILinkPointNavigator
  {
    public static Dictionary<int, UILinkPage> Pages = new Dictionary<int, UILinkPage>();
    public static Dictionary<int, UILinkPoint> Points = new Dictionary<int, UILinkPoint>();
    public static int CurrentPage = 1000;
    public static int OldPage = 1000;
    private static int XCooldown = 0;
    private static int YCooldown = 0;
    private static int PageLeftCD = 0;
    private static int PageRightCD = 0;
    public static int OverridePoint = -1;
    private static Vector2 LastInput;
    public static bool InUse;

    public static int CurrentPoint
    {
      get
      {
        return UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].CurrentPoint;
      }
    }

    public static bool Available
    {
      get
      {
        if (!Main.playerInventory && !Main.ingameOptionsWindow && (Main.player[Main.myPlayer].talkNPC == -1 && Main.player[Main.myPlayer].sign == -1) && (!Main.mapFullscreen && !Main.clothesWindow && !Main.MenuUI.IsVisible))
          return Main.InGameUI.IsVisible;
        return true;
      }
    }

    public static void GoToDefaultPage(int specialFlag = 0)
    {
      if (Main.MenuUI.IsVisible)
        UILinkPointNavigator.CurrentPage = 1004;
      else if (Main.InGameUI.IsVisible || specialFlag == 1)
        UILinkPointNavigator.CurrentPage = 1004;
      else if (Main.gameMenu)
        UILinkPointNavigator.CurrentPage = 1000;
      else if (Main.ingameOptionsWindow)
        UILinkPointNavigator.CurrentPage = 1001;
      else if (Main.hairWindow)
        UILinkPointNavigator.CurrentPage = 12;
      else if (Main.clothesWindow)
        UILinkPointNavigator.CurrentPage = 15;
      else if (Main.npcShop != 0)
        UILinkPointNavigator.CurrentPage = 13;
      else if (Main.InGuideCraftMenu)
        UILinkPointNavigator.CurrentPage = 9;
      else if (Main.InReforgeMenu)
        UILinkPointNavigator.CurrentPage = 5;
      else if (Main.player[Main.myPlayer].chest != -1)
        UILinkPointNavigator.CurrentPage = 4;
      else if (Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1)
        UILinkPointNavigator.CurrentPage = 1003;
      else
        UILinkPointNavigator.CurrentPage = 0;
    }

    public static void Update()
    {
      bool inUse = UILinkPointNavigator.InUse;
      UILinkPointNavigator.InUse = false;
      bool flag1 = true;
      if (flag1)
      {
        switch (PlayerInput.CurrentInputMode)
        {
          case InputMode.Keyboard:
          case InputMode.KeyboardUI:
          case InputMode.Mouse:
            if (!Main.gameMenu)
            {
              flag1 = false;
              break;
            }
            break;
        }
      }
      if (flag1 && PlayerInput.NavigatorRebindingLock > 0)
        flag1 = false;
      if (flag1 && !Main.gameMenu && !PlayerInput.UsingGamepadUI)
        flag1 = false;
      if (flag1 && !Main.gameMenu && PlayerInput.InBuildingMode)
        flag1 = false;
      if (flag1 && !Main.gameMenu && !UILinkPointNavigator.Available)
        flag1 = false;
      bool flag2 = false;
      UILinkPage uiLinkPage;
      if (!UILinkPointNavigator.Pages.TryGetValue(UILinkPointNavigator.CurrentPage, out uiLinkPage))
        flag2 = true;
      else if (!uiLinkPage.IsValid())
        flag2 = true;
      if (flag2)
      {
        UILinkPointNavigator.GoToDefaultPage(0);
        UILinkPointNavigator.ProcessChanges();
        flag1 = false;
      }
      if (inUse != flag1)
      {
        if (!flag1)
        {
          uiLinkPage.Leave();
          UILinkPointNavigator.GoToDefaultPage(0);
          UILinkPointNavigator.ProcessChanges();
        }
        else
        {
          UILinkPointNavigator.GoToDefaultPage(0);
          UILinkPointNavigator.ProcessChanges();
          uiLinkPage.Enter();
        }
        if (flag1)
        {
          Main.player[Main.myPlayer].releaseInventory = false;
          Main.player[Main.myPlayer].releaseUseTile = false;
          PlayerInput.LockTileUseButton = true;
        }
        if (!Main.gameMenu)
        {
          if (flag1)
            PlayerInput.NavigatorCachePosition();
          else
            PlayerInput.NavigatorUnCachePosition();
        }
      }
      if (!flag1)
        return;
      UILinkPointNavigator.InUse = true;
      UILinkPointNavigator.OverridePoint = -1;
      if (UILinkPointNavigator.PageLeftCD > 0)
        --UILinkPointNavigator.PageLeftCD;
      if (UILinkPointNavigator.PageRightCD > 0)
        --UILinkPointNavigator.PageRightCD;
      Vector2 navigatorDirections = PlayerInput.Triggers.Current.GetNavigatorDirections();
      bool flag3 = PlayerInput.Triggers.Current.HotbarMinus && !PlayerInput.Triggers.Current.HotbarPlus;
      bool flag4 = PlayerInput.Triggers.Current.HotbarPlus && !PlayerInput.Triggers.Current.HotbarMinus;
      if (!flag3)
        UILinkPointNavigator.PageLeftCD = 0;
      if (!flag4)
        UILinkPointNavigator.PageRightCD = 0;
      bool flag5 = flag3 && UILinkPointNavigator.PageLeftCD == 0;
      bool flag6 = flag4 && UILinkPointNavigator.PageRightCD == 0;
      if ((double) UILinkPointNavigator.LastInput.X != (double) navigatorDirections.X)
        UILinkPointNavigator.XCooldown = 0;
      if ((double) UILinkPointNavigator.LastInput.Y != (double) navigatorDirections.Y)
        UILinkPointNavigator.YCooldown = 0;
      if (UILinkPointNavigator.XCooldown > 0)
        --UILinkPointNavigator.XCooldown;
      if (UILinkPointNavigator.YCooldown > 0)
        --UILinkPointNavigator.YCooldown;
      UILinkPointNavigator.LastInput = navigatorDirections;
      if (flag5)
        UILinkPointNavigator.PageLeftCD = 16;
      if (flag6)
        UILinkPointNavigator.PageRightCD = 16;
      UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].Update();
      int num = 10;
      if (!Main.gameMenu && Main.playerInventory && (!Main.ingameOptionsWindow && !Main.inFancyUI) && (UILinkPointNavigator.CurrentPage == 0 || UILinkPointNavigator.CurrentPage == 4 || (UILinkPointNavigator.CurrentPage == 2 || UILinkPointNavigator.CurrentPage == 1)))
        num = PlayerInput.CurrentProfile.InventoryMoveCD;
      if ((double) navigatorDirections.X == -1.0 && UILinkPointNavigator.XCooldown == 0)
      {
        UILinkPointNavigator.XCooldown = num;
        UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelLeft();
      }
      if ((double) navigatorDirections.X == 1.0 && UILinkPointNavigator.XCooldown == 0)
      {
        UILinkPointNavigator.XCooldown = num;
        UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelRight();
      }
      if ((double) navigatorDirections.Y == -1.0 && UILinkPointNavigator.YCooldown == 0)
      {
        UILinkPointNavigator.YCooldown = num;
        UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelUp();
      }
      if ((double) navigatorDirections.Y == 1.0 && UILinkPointNavigator.YCooldown == 0)
      {
        UILinkPointNavigator.YCooldown = num;
        UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelDown();
      }
      UILinkPointNavigator.XCooldown = UILinkPointNavigator.YCooldown = Math.Max(UILinkPointNavigator.XCooldown, UILinkPointNavigator.YCooldown);
      if (flag5)
        UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].SwapPageLeft();
      if (flag6)
        UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].SwapPageRight();
      if (PlayerInput.Triggers.Current.UsedMovementKey)
      {
        Vector2 position = UILinkPointNavigator.Points[UILinkPointNavigator.CurrentPoint].Position;
        Vector2 vector2_1 = new Vector2((float) PlayerInput.MouseX, (float) PlayerInput.MouseY);
        float amount = 0.3f;
        if (PlayerInput.InvisibleGamepadInMenus)
          amount = 1f;
        Vector2 vector2_2 = Vector2.Lerp(vector2_1, position, amount);
        if (Main.gameMenu)
        {
          if ((double) Math.Abs(vector2_2.X - position.X) <= 5.0)
            vector2_2.X = position.X;
          if ((double) Math.Abs(vector2_2.Y - position.Y) <= 5.0)
            vector2_2.Y = position.Y;
        }
        PlayerInput.MouseX = (int) vector2_2.X;
        PlayerInput.MouseY = (int) vector2_2.Y;
      }
      UILinkPointNavigator.ResetFlagsEnd();
    }

    public static void ResetFlagsEnd()
    {
      UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 0;
      UILinkPointNavigator.Shortcuts.BackButtonLock = false;
      UILinkPointNavigator.Shortcuts.BackButtonCommand = 0;
    }

    public static string GetInstructions()
    {
      string str1 = UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].SpecialInteractions();
      string str2 = UILinkPointNavigator.Points[UILinkPointNavigator.CurrentPoint].SpecialInteractions();
      if (!string.IsNullOrEmpty(str2))
      {
        if (string.IsNullOrEmpty(str1))
          return str2;
        str1 = str1 + "   " + str2;
      }
      return str1;
    }

    public static void SetPosition(int ID, Vector2 Position)
    {
      UILinkPointNavigator.Points[ID].Position = Position;
    }

    public static void RegisterPage(UILinkPage page, int ID, bool automatedDefault = true)
    {
      if (automatedDefault)
        page.DefaultPoint = page.LinkMap.Keys.First<int>();
      page.CurrentPoint = page.DefaultPoint;
      page.ID = ID;
      UILinkPointNavigator.Pages.Add(page.ID, page);
      foreach (KeyValuePair<int, UILinkPoint> link in page.LinkMap)
      {
        link.Value.SetPage(ID);
        UILinkPointNavigator.Points.Add(link.Key, link.Value);
      }
    }

    public static void ChangePage(int PageID)
    {
      if (!UILinkPointNavigator.Pages.ContainsKey(PageID) || !UILinkPointNavigator.Pages[PageID].CanEnter())
        return;
      UILinkPointNavigator.CurrentPage = PageID;
      UILinkPointNavigator.ProcessChanges();
    }

    public static void ChangePoint(int PointID)
    {
      if (!UILinkPointNavigator.Points.ContainsKey(PointID))
        return;
      UILinkPointNavigator.CurrentPage = UILinkPointNavigator.Points[PointID].Page;
      UILinkPointNavigator.OverridePoint = PointID;
      UILinkPointNavigator.ProcessChanges();
    }

    public static void ProcessChanges()
    {
      UILinkPage page = UILinkPointNavigator.Pages[UILinkPointNavigator.OldPage];
      if (UILinkPointNavigator.OldPage != UILinkPointNavigator.CurrentPage)
      {
        page.Leave();
        if (!UILinkPointNavigator.Pages.TryGetValue(UILinkPointNavigator.CurrentPage, out page))
        {
          UILinkPointNavigator.GoToDefaultPage(0);
          UILinkPointNavigator.ProcessChanges();
          UILinkPointNavigator.OverridePoint = -1;
        }
        page.CurrentPoint = page.DefaultPoint;
        page.Enter();
        page.Update();
        UILinkPointNavigator.OldPage = UILinkPointNavigator.CurrentPage;
      }
      if (UILinkPointNavigator.OverridePoint == -1 || !page.LinkMap.ContainsKey(UILinkPointNavigator.OverridePoint))
        return;
      page.CurrentPoint = UILinkPointNavigator.OverridePoint;
    }

    public static class Shortcuts
    {
      public static int NPCS_IconsPerColumn = 100;
      public static int NPCS_IconsTotal = 0;
      public static int NPCS_LastHovered = -1;
      public static bool NPCS_IconsDisplay = false;
      public static int CRAFT_IconsPerRow = 100;
      public static int CRAFT_IconsPerColumn = 100;
      public static int CRAFT_CurrentIngridientsCount = 0;
      public static int CRAFT_CurrentRecipeBig = 0;
      public static int CRAFT_CurrentRecipeSmall = 0;
      public static bool NPCCHAT_ButtonsLeft = false;
      public static bool NPCCHAT_ButtonsMiddle = false;
      public static bool NPCCHAT_ButtonsRight = false;
      public static int INGAMEOPTIONS_BUTTONS_LEFT = 0;
      public static int INGAMEOPTIONS_BUTTONS_RIGHT = 0;
      public static int OPTIONS_BUTTON_SPECIALFEATURE = 0;
      public static int BackButtonCommand = 0;
      public static bool BackButtonInUse = false;
      public static bool BackButtonLock = false;
      public static int FANCYUI_HIGHEST_INDEX = 1;
      public static int FANCYUI_SPECIAL_INSTRUCTIONS = 0;
      public static int INFOACCCOUNT = 0;
      public static int BUILDERACCCOUNT = 0;
      public static int BUFFS_PER_COLUMN = 0;
      public static int BUFFS_DRAWN = 0;
      public static int INV_MOVE_OPTION_CD = 0;
    }
  }
}
