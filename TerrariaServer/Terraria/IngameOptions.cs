// Decompiled with JetBrains decompiler
// Type: Terraria.IngameOptions
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria
{
  public static class IngameOptions
  {
    public static float[] leftScale = new float[9]
    {
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f
    };
    public static float[] rightScale = new float[15]
    {
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f,
      0.7f
    };
    public static bool[] skipRightSlot = new bool[20];
    public static int leftHover = -1;
    public static int rightHover = -1;
    public static int oldLeftHover = -1;
    public static int oldRightHover = -1;
    public static int rightLock = -1;
    public static bool inBar = false;
    public static bool notBar = false;
    public static bool noSound = false;
    private static Rectangle _GUIHover = new Rectangle();
    public static int category = 0;
    public static Vector2 valuePosition = Vector2.Zero;
    public const int width = 670;
    public const int height = 480;
    private static string _mouseOverText;

    public static void Open()
    {
      Main.playerInventory = false;
      Main.editChest = false;
      Main.npcChatText = "";
      Main.PlaySound(10, -1, -1, 1, 1f, 0.0f);
      Main.ingameOptionsWindow = true;
      IngameOptions.category = 0;
      for (int index = 0; index < IngameOptions.leftScale.Length; ++index)
        IngameOptions.leftScale[index] = 0.0f;
      for (int index = 0; index < IngameOptions.rightScale.Length; ++index)
        IngameOptions.rightScale[index] = 0.0f;
      IngameOptions.leftHover = -1;
      IngameOptions.rightHover = -1;
      IngameOptions.oldLeftHover = -1;
      IngameOptions.oldRightHover = -1;
      IngameOptions.rightLock = -1;
      IngameOptions.inBar = false;
      IngameOptions.notBar = false;
      IngameOptions.noSound = false;
    }

    public static void Close()
    {
      if (Main.setKey != -1)
        return;
      Main.ingameOptionsWindow = false;
      Main.PlaySound(11, -1, -1, 1, 1f, 0.0f);
      Recipe.FindRecipes();
      Main.playerInventory = true;
      Main.SaveSettings();
    }

    public static void Draw(Main mainInstance, SpriteBatch sb)
    {
      if (Main.player[Main.myPlayer].dead && !Main.player[Main.myPlayer].ghost)
      {
        Main.setKey = -1;
        IngameOptions.Close();
        Main.playerInventory = false;
      }
      else
      {
        for (int index = 0; index < IngameOptions.skipRightSlot.Length; ++index)
          IngameOptions.skipRightSlot[index] = false;
        bool flag1 = GameCulture.Russian.IsActive || GameCulture.Portuguese.IsActive || GameCulture.Polish.IsActive || GameCulture.French.IsActive;
        bool isActive1 = GameCulture.Polish.IsActive;
        bool isActive2 = GameCulture.German.IsActive;
        bool flag2 = GameCulture.Italian.IsActive || GameCulture.Spanish.IsActive;
        bool flag3 = false;
        int num1 = 70;
        float scale = 0.75f;
        float num2 = 60f;
        float num3 = 300f;
        if (flag1)
          flag3 = true;
        if (isActive1)
          num3 = 200f;
        Vector2 vector2_1 = new Vector2((float) Main.mouseX, (float) Main.mouseY);
        bool flag4 = Main.mouseLeft && Main.mouseLeftRelease;
        Vector2 vector2_2 = new Vector2((float) Main.screenWidth, (float) Main.screenHeight);
        Vector2 vector2_3 = new Vector2(670f, 480f);
        Vector2 vector2_4 = vector2_2 / 2f - vector2_3 / 2f;
        int num4 = 20;
        IngameOptions._GUIHover = new Rectangle((int) ((double) vector2_4.X - (double) num4), (int) ((double) vector2_4.Y - (double) num4), (int) ((double) vector2_3.X + (double) (num4 * 2)), (int) ((double) vector2_3.Y + (double) (num4 * 2)));
        Utils.DrawInvBG(sb, vector2_4.X - (float) num4, vector2_4.Y - (float) num4, vector2_3.X + (float) (num4 * 2), vector2_3.Y + (float) (num4 * 2), new Color(33, 15, 91, (int) byte.MaxValue) * 0.685f);
        if (new Rectangle((int) vector2_4.X - num4, (int) vector2_4.Y - num4, (int) vector2_3.X + num4 * 2, (int) vector2_3.Y + num4 * 2).Contains(new Point(Main.mouseX, Main.mouseY)))
          Main.player[Main.myPlayer].mouseInterface = true;
        Utils.DrawBorderString(sb, Language.GetTextValue("GameUI.SettingsMenu"), vector2_4 + vector2_3 * new Vector2(0.5f, 0.0f), Color.White, 1f, 0.5f, 0.0f, -1);
        if (flag1)
        {
          Utils.DrawInvBG(sb, vector2_4.X + (float) (num4 / 2), vector2_4.Y + (float) (num4 * 5 / 2), vector2_3.X / 3f - (float) num4, vector2_3.Y - (float) (num4 * 3), new Color());
          Utils.DrawInvBG(sb, vector2_4.X + vector2_3.X / 3f + (float) num4, vector2_4.Y + (float) (num4 * 5 / 2), (float) ((double) vector2_3.X * 2.0 / 3.0) - (float) (num4 * 3 / 2), vector2_3.Y - (float) (num4 * 3), new Color());
        }
        else
        {
          Utils.DrawInvBG(sb, vector2_4.X + (float) (num4 / 2), vector2_4.Y + (float) (num4 * 5 / 2), vector2_3.X / 2f - (float) num4, vector2_3.Y - (float) (num4 * 3), new Color());
          Utils.DrawInvBG(sb, vector2_4.X + vector2_3.X / 2f + (float) num4, vector2_4.Y + (float) (num4 * 5 / 2), vector2_3.X / 2f - (float) (num4 * 3 / 2), vector2_3.Y - (float) (num4 * 3), new Color());
        }
        float num5 = 0.7f;
        float num6 = 0.8f;
        float num7 = 0.01f;
        if (flag1)
        {
          num5 = 0.4f;
          num6 = 0.44f;
        }
        if (isActive2)
        {
          num5 = 0.55f;
          num6 = 0.6f;
        }
        if (IngameOptions.oldLeftHover != IngameOptions.leftHover && IngameOptions.leftHover != -1)
          Main.PlaySound(12, -1, -1, 1, 1f, 0.0f);
        if (IngameOptions.oldRightHover != IngameOptions.rightHover && IngameOptions.rightHover != -1)
          Main.PlaySound(12, -1, -1, 1, 1f, 0.0f);
        if (flag4 && IngameOptions.rightHover != -1 && !IngameOptions.noSound)
          Main.PlaySound(12, -1, -1, 1, 1f, 0.0f);
        IngameOptions.oldLeftHover = IngameOptions.leftHover;
        IngameOptions.oldRightHover = IngameOptions.rightHover;
        IngameOptions.noSound = false;
        bool flag5 = SocialAPI.Network != null && SocialAPI.Network.CanInvite();
        int num8 = 5 + (flag5 ? 1 : 0) + 2;
        Vector2 anchor1 = new Vector2(vector2_4.X + vector2_3.X / 4f, vector2_4.Y + (float) (num4 * 5 / 2));
        Vector2 offset1 = new Vector2(0.0f, vector2_3.Y - (float) (num4 * 5)) / (float) (num8 + 1);
        if (flag1)
          anchor1.X -= 55f;
        UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_LEFT = num8 + 1;
        for (int index = 0; index <= num8; ++index)
        {
          if (IngameOptions.leftHover == index || index == IngameOptions.category)
            IngameOptions.leftScale[index] += num7;
          else
            IngameOptions.leftScale[index] -= num7;
          if ((double) IngameOptions.leftScale[index] < (double) num5)
            IngameOptions.leftScale[index] = num5;
          if ((double) IngameOptions.leftScale[index] > (double) num6)
            IngameOptions.leftScale[index] = num6;
        }
        IngameOptions.leftHover = -1;
        int category = IngameOptions.category;
        int i1 = 0;
        if (IngameOptions.DrawLeftSide(sb, Lang.menu[114].Value, i1, anchor1, offset1, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
        {
          IngameOptions.leftHover = i1;
          if (flag4)
          {
            IngameOptions.category = 0;
            Main.PlaySound(10, -1, -1, 1, 1f, 0.0f);
          }
        }
        int i2 = i1 + 1;
        if (IngameOptions.DrawLeftSide(sb, Lang.menu[210].Value, i2, anchor1, offset1, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
        {
          IngameOptions.leftHover = i2;
          if (flag4)
          {
            IngameOptions.category = 1;
            Main.PlaySound(10, -1, -1, 1, 1f, 0.0f);
          }
        }
        int i3 = i2 + 1;
        if (IngameOptions.DrawLeftSide(sb, Lang.menu[63].Value, i3, anchor1, offset1, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
        {
          IngameOptions.leftHover = i3;
          if (flag4)
          {
            IngameOptions.category = 2;
            Main.PlaySound(10, -1, -1, 1, 1f, 0.0f);
          }
        }
        int i4 = i3 + 1;
        if (IngameOptions.DrawLeftSide(sb, Lang.menu[218].Value, i4, anchor1, offset1, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
        {
          IngameOptions.leftHover = i4;
          if (flag4)
          {
            IngameOptions.category = 3;
            Main.PlaySound(10, -1, -1, 1, 1f, 0.0f);
          }
        }
        int i5 = i4 + 1;
        if (IngameOptions.DrawLeftSide(sb, Lang.menu[66].Value, i5, anchor1, offset1, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
        {
          IngameOptions.leftHover = i5;
          if (flag4)
          {
            IngameOptions.Close();
            IngameFancyUI.OpenKeybinds();
          }
        }
        int i6 = i5 + 1;
        if (flag5 && IngameOptions.DrawLeftSide(sb, Lang.menu[147].Value, i6, anchor1, offset1, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
        {
          IngameOptions.leftHover = i6;
          if (flag4)
          {
            IngameOptions.Close();
            SocialAPI.Network.OpenInviteInterface();
          }
        }
        if (flag5)
          ++i6;
        if (IngameOptions.DrawLeftSide(sb, Lang.menu[131].Value, i6, anchor1, offset1, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
        {
          IngameOptions.leftHover = i6;
          if (flag4)
          {
            IngameOptions.Close();
            IngameFancyUI.OpenAchievements();
          }
        }
        int i7 = i6 + 1;
        if (IngameOptions.DrawLeftSide(sb, Lang.menu[118].Value, i7, anchor1, offset1, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
        {
          IngameOptions.leftHover = i7;
          if (flag4)
            IngameOptions.Close();
        }
        int i8 = i7 + 1;
        if (IngameOptions.DrawLeftSide(sb, Lang.inter[35].Value, i8, anchor1, offset1, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
        {
          IngameOptions.leftHover = i8;
          if (flag4)
          {
            IngameOptions.Close();
            Main.menuMode = 10;
            WorldGen.SaveAndQuit((Action) null);
          }
        }
        int num9 = i8 + 1;
        if (category != IngameOptions.category)
        {
          for (int index = 0; index < IngameOptions.rightScale.Length; ++index)
            IngameOptions.rightScale[index] = 0.0f;
        }
        int num10 = 0;
        switch (IngameOptions.category)
        {
          case 0:
            num10 = 15;
            num5 = 1f;
            num6 = 1.001f;
            num7 = 1f / 1000f;
            break;
          case 1:
            num10 = 6;
            num5 = 1f;
            num6 = 1.001f;
            num7 = 1f / 1000f;
            break;
          case 2:
            num10 = 12;
            num5 = 1f;
            num6 = 1.001f;
            num7 = 1f / 1000f;
            break;
          case 3:
            num10 = 15;
            num5 = 1f;
            num6 = 1.001f;
            num7 = 1f / 1000f;
            break;
        }
        if (flag1)
        {
          num5 -= 0.1f;
          num6 -= 0.1f;
        }
        if (isActive2 && IngameOptions.category == 3)
        {
          num5 -= 0.15f;
          num6 -= 0.15f;
        }
        if (flag2 && (IngameOptions.category == 0 || IngameOptions.category == 3))
        {
          num5 -= 0.2f;
          num6 -= 0.2f;
        }
        UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_RIGHT = num10;
        Vector2 anchor2 = new Vector2(vector2_4.X + (float) ((double) vector2_3.X * 3.0 / 4.0), vector2_4.Y + (float) (num4 * 5 / 2));
        if (flag1)
          anchor2.X = vector2_4.X + (float) ((double) vector2_3.X * 2.0 / 3.0);
        Vector2 offset2 = new Vector2(0.0f, vector2_3.Y - (float) (num4 * 3)) / (float) (num10 + 1);
        if (IngameOptions.category == 2)
          offset2.Y -= 2f;
        for (int index = 0; index < 15; ++index)
        {
          if (IngameOptions.rightLock == index || IngameOptions.rightHover == index && IngameOptions.rightLock == -1)
            IngameOptions.rightScale[index] += num7;
          else
            IngameOptions.rightScale[index] -= num7;
          if ((double) IngameOptions.rightScale[index] < (double) num5)
            IngameOptions.rightScale[index] = num5;
          if ((double) IngameOptions.rightScale[index] > (double) num6)
            IngameOptions.rightScale[index] = num6;
        }
        IngameOptions.inBar = false;
        IngameOptions.rightHover = -1;
        if (!Main.mouseLeft)
          IngameOptions.rightLock = -1;
        if (IngameOptions.rightLock == -1)
          IngameOptions.notBar = false;
        if (IngameOptions.category == 0)
        {
          int i9 = 0;
          IngameOptions.DrawRightSide(sb, Lang.menu[65].Value, i9, anchor2, offset2, IngameOptions.rightScale[i9], 1f, new Color());
          IngameOptions.skipRightSlot[i9] = true;
          int i10 = i9 + 1;
          anchor2.X -= (float) num1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[99].Value + " " + (object) Math.Round((double) Main.musicVolume * 100.0) + "%", i10, anchor2, offset2, IngameOptions.rightScale[i10], (float) (((double) IngameOptions.rightScale[i10] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.noSound = true;
            IngameOptions.rightHover = i10;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          float num11 = IngameOptions.DrawValueBar(sb, scale, Main.musicVolume, 0, (Utils.ColorLerpMethod) null);
          if ((IngameOptions.inBar || IngameOptions.rightLock == i10) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i10;
            if (Main.mouseLeft && IngameOptions.rightLock == i10)
              Main.musicVolume = num11;
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i10;
          }
          if (IngameOptions.rightHover == i10)
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 2;
          int i11 = i10 + 1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[98].Value + " " + (object) Math.Round((double) Main.soundVolume * 100.0) + "%", i11, anchor2, offset2, IngameOptions.rightScale[i11], (float) (((double) IngameOptions.rightScale[i11] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i11;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          float num12 = IngameOptions.DrawValueBar(sb, scale, Main.soundVolume, 0, (Utils.ColorLerpMethod) null);
          if ((IngameOptions.inBar || IngameOptions.rightLock == i11) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i11;
            if (Main.mouseLeft && IngameOptions.rightLock == i11)
            {
              Main.soundVolume = num12;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i11;
          }
          if (IngameOptions.rightHover == i11)
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 3;
          int i12 = i11 + 1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[119].Value + " " + (object) Math.Round((double) Main.ambientVolume * 100.0) + "%", i12, anchor2, offset2, IngameOptions.rightScale[i12], (float) (((double) IngameOptions.rightScale[i12] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i12;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          float num13 = IngameOptions.DrawValueBar(sb, scale, Main.ambientVolume, 0, (Utils.ColorLerpMethod) null);
          if ((IngameOptions.inBar || IngameOptions.rightLock == i12) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i12;
            if (Main.mouseLeft && IngameOptions.rightLock == i12)
            {
              Main.ambientVolume = num13;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i12;
          }
          if (IngameOptions.rightHover == i12)
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 4;
          int i13 = i12 + 1;
          anchor2.X += (float) num1;
          IngameOptions.DrawRightSide(sb, "", i13, anchor2, offset2, IngameOptions.rightScale[i13], 1f, new Color());
          IngameOptions.skipRightSlot[i13] = true;
          int i14 = i13 + 1;
          IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.ZoomCategory"), i14, anchor2, offset2, IngameOptions.rightScale[i14], 1f, new Color());
          IngameOptions.skipRightSlot[i14] = true;
          int i15 = i14 + 1;
          anchor2.X -= (float) num1;
          string txt1 = Language.GetTextValue("GameUI.GameZoom", (object) Math.Round((double) Main.GameZoomTarget * 100.0), (object) Math.Round((double) Main.GameViewMatrix.Zoom.X * 100.0));
          if (flag3)
            txt1 = Main.fontItemStack.CreateWrappedText(txt1, num3, Language.ActiveCulture.CultureInfo);
          if (IngameOptions.DrawRightSide(sb, txt1, i15, anchor2, offset2, IngameOptions.rightScale[i15] * 0.85f, (float) (((double) IngameOptions.rightScale[i15] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i15;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          float num14 = IngameOptions.DrawValueBar(sb, scale, Main.GameZoomTarget - 1f, 0, (Utils.ColorLerpMethod) null);
          if ((IngameOptions.inBar || IngameOptions.rightLock == i15) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i15;
            if (Main.mouseLeft && IngameOptions.rightLock == i15)
              Main.GameZoomTarget = num14 + 1f;
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i15;
          }
          if (IngameOptions.rightHover == i15)
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 10;
          int i16 = i15 + 1;
          bool flag6 = false;
          if ((double) Main.temporaryGUIScaleSlider == -1.0)
            Main.temporaryGUIScaleSlider = Main.UIScaleWanted;
          string txt2 = Language.GetTextValue("GameUI.UIScale", (object) Math.Round((double) Main.temporaryGUIScaleSlider * 100.0), (object) Math.Round((double) Main.UIScale * 100.0));
          if (flag3)
            txt2 = Main.fontItemStack.CreateWrappedText(txt2, num3, Language.ActiveCulture.CultureInfo);
          if (IngameOptions.DrawRightSide(sb, txt2, i16, anchor2, offset2, IngameOptions.rightScale[i16] * 0.75f, (float) (((double) IngameOptions.rightScale[i16] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i16;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          float num15 = IngameOptions.DrawValueBar(sb, scale, Main.temporaryGUIScaleSlider - 1f, 0, (Utils.ColorLerpMethod) null);
          if ((IngameOptions.inBar || IngameOptions.rightLock == i16) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i16;
            if (Main.mouseLeft && IngameOptions.rightLock == i16)
            {
              Main.temporaryGUIScaleSlider = num15 + 1f;
              Main.temporaryGUIScaleSliderUpdate = true;
              flag6 = true;
            }
          }
          if (!flag6 && Main.temporaryGUIScaleSliderUpdate && (double) Main.temporaryGUIScaleSlider != -1.0)
          {
            Main.UIScale = Main.temporaryGUIScaleSlider;
            Main.temporaryGUIScaleSliderUpdate = false;
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i16;
          }
          if (IngameOptions.rightHover == i16)
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 11;
          int i17 = i16 + 1;
          anchor2.X += (float) num1;
          IngameOptions.DrawRightSide(sb, "", i17, anchor2, offset2, IngameOptions.rightScale[i17], 1f, new Color());
          IngameOptions.skipRightSlot[i17] = true;
          int i18 = i17 + 1;
          IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.Gameplay"), i18, anchor2, offset2, IngameOptions.rightScale[i18], 1f, new Color());
          IngameOptions.skipRightSlot[i18] = true;
          int i19 = i18 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.autoSave ? Lang.menu[67].Value : Lang.menu[68].Value, i19, anchor2, offset2, IngameOptions.rightScale[i19], (float) (((double) IngameOptions.rightScale[i19] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i19;
            if (flag4)
              Main.autoSave = !Main.autoSave;
          }
          int i20 = i19 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.autoPause ? Lang.menu[69].Value : Lang.menu[70].Value, i20, anchor2, offset2, IngameOptions.rightScale[i20], (float) (((double) IngameOptions.rightScale[i20] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i20;
            if (flag4)
              Main.autoPause = !Main.autoPause;
          }
          int i21 = i20 + 1;
          if (IngameOptions.DrawRightSide(sb, Player.SmartCursorSettings.SmartWallReplacement ? Lang.menu[226].Value : Lang.menu[225].Value, i21, anchor2, offset2, IngameOptions.rightScale[i21], (float) (((double) IngameOptions.rightScale[i21] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i21;
            if (flag4)
              Player.SmartCursorSettings.SmartWallReplacement = !Player.SmartCursorSettings.SmartWallReplacement;
          }
          int i22 = i21 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.ReversedUpDownArmorSetBonuses ? Lang.menu[220].Value : Lang.menu[221].Value, i22, anchor2, offset2, IngameOptions.rightScale[i22], (float) (((double) IngameOptions.rightScale[i22] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i22;
            if (flag4)
              Main.ReversedUpDownArmorSetBonuses = !Main.ReversedUpDownArmorSetBonuses;
          }
          int i23 = i22 + 1;
          IngameOptions.DrawRightSide(sb, "", i23, anchor2, offset2, IngameOptions.rightScale[i23], 1f, new Color());
          IngameOptions.skipRightSlot[i23] = true;
          int num16 = i23 + 1;
        }
        if (IngameOptions.category == 1)
        {
          int i9 = 0;
          if (IngameOptions.DrawRightSide(sb, Main.showItemText ? Lang.menu[71].Value : Lang.menu[72].Value, i9, anchor2, offset2, IngameOptions.rightScale[i9], (float) (((double) IngameOptions.rightScale[i9] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i9;
            if (flag4)
              Main.showItemText = !Main.showItemText;
          }
          int i10 = i9 + 1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[123].Value + " " + (object) Lang.menu[124 + Main.invasionProgressMode], i10, anchor2, offset2, IngameOptions.rightScale[i10], (float) (((double) IngameOptions.rightScale[i10] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i10;
            if (flag4)
            {
              ++Main.invasionProgressMode;
              if (Main.invasionProgressMode >= 3)
                Main.invasionProgressMode = 0;
            }
          }
          int i11 = i10 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.placementPreview ? Lang.menu[128].Value : Lang.menu[129].Value, i11, anchor2, offset2, IngameOptions.rightScale[i11], (float) (((double) IngameOptions.rightScale[i11] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i11;
            if (flag4)
              Main.placementPreview = !Main.placementPreview;
          }
          int i12 = i11 + 1;
          if (IngameOptions.DrawRightSide(sb, ItemSlot.Options.HighlightNewItems ? Lang.inter[117].Value : Lang.inter[116].Value, i12, anchor2, offset2, IngameOptions.rightScale[i12], (float) (((double) IngameOptions.rightScale[i12] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i12;
            if (flag4)
              ItemSlot.Options.HighlightNewItems = !ItemSlot.Options.HighlightNewItems;
          }
          int i13 = i12 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.MouseShowBuildingGrid ? Lang.menu[229].Value : Lang.menu[230].Value, i13, anchor2, offset2, IngameOptions.rightScale[i13], (float) (((double) IngameOptions.rightScale[i13] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i13;
            if (flag4)
              Main.MouseShowBuildingGrid = !Main.MouseShowBuildingGrid;
          }
          int i14 = i13 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.GamepadDisableInstructionsDisplay ? Lang.menu[241].Value : Lang.menu[242].Value, i14, anchor2, offset2, IngameOptions.rightScale[i14], (float) (((double) IngameOptions.rightScale[i14] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i14;
            if (flag4)
              Main.GamepadDisableInstructionsDisplay = !Main.GamepadDisableInstructionsDisplay;
          }
          int num11 = i14 + 1;
        }
        if (IngameOptions.category == 2)
        {
          int i9 = 0;
          if (IngameOptions.DrawRightSide(sb, Main.graphics.IsFullScreen ? Lang.menu[49].Value : Lang.menu[50].Value, i9, anchor2, offset2, IngameOptions.rightScale[i9], (float) (((double) IngameOptions.rightScale[i9] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i9;
            if (flag4)
              Main.ToggleFullScreen();
          }
          int i10 = i9 + 1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[51].Value + ": " + (object) Main.PendingResolutionWidth + "x" + (object) Main.PendingResolutionHeight, i10, anchor2, offset2, IngameOptions.rightScale[i10], (float) (((double) IngameOptions.rightScale[i10] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i10;
            if (flag4)
            {
              int num11 = 0;
              for (int index = 0; index < Main.numDisplayModes; ++index)
              {
                if (Main.displayWidth[index] == Main.PendingResolutionWidth && Main.displayHeight[index] == Main.PendingResolutionHeight)
                {
                  num11 = index;
                  break;
                }
              }
              int index1 = num11 + 1;
              if (index1 >= Main.numDisplayModes)
                index1 = 0;
              Main.PendingResolutionWidth = Main.displayWidth[index1];
              Main.PendingResolutionHeight = Main.displayHeight[index1];
            }
          }
          int i11 = i10 + 1;
          anchor2.X -= (float) num1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[52].Value + ": " + (object) Main.bgScroll + "%", i11, anchor2, offset2, IngameOptions.rightScale[i11], (float) (((double) IngameOptions.rightScale[i11] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.noSound = true;
            IngameOptions.rightHover = i11;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          float num12 = IngameOptions.DrawValueBar(sb, scale, (float) Main.bgScroll / 100f, 0, (Utils.ColorLerpMethod) null);
          if ((IngameOptions.inBar || IngameOptions.rightLock == i11) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i11;
            if (Main.mouseLeft && IngameOptions.rightLock == i11)
            {
              Main.bgScroll = (int) ((double) num12 * 100.0);
              Main.caveParallax = (float) (1.0 - (double) Main.bgScroll / 500.0);
            }
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i11;
          }
          if (IngameOptions.rightHover == i11)
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 1;
          int i12 = i11 + 1;
          anchor2.X += (float) num1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[247 + Main.FrameSkipMode].Value, i12, anchor2, offset2, IngameOptions.rightScale[i12], (float) (((double) IngameOptions.rightScale[i12] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i12;
            if (flag4)
            {
              ++Main.FrameSkipMode;
              if (Main.FrameSkipMode < 0 || Main.FrameSkipMode > 2)
                Main.FrameSkipMode = 0;
            }
          }
          int i13 = i12 + 1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[55 + Lighting.lightMode].Value, i13, anchor2, offset2, IngameOptions.rightScale[i13], (float) (((double) IngameOptions.rightScale[i13] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i13;
            if (flag4)
              Lighting.NextLightMode();
          }
          int i14 = i13 + 1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[116].Value + " " + (Lighting.LightingThreads > 0 ? string.Concat((object) (Lighting.LightingThreads + 1)) : Lang.menu[117].Value), i14, anchor2, offset2, IngameOptions.rightScale[i14], (float) (((double) IngameOptions.rightScale[i14] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i14;
            if (flag4)
            {
              ++Lighting.LightingThreads;
              if (Lighting.LightingThreads > Environment.ProcessorCount - 1)
                Lighting.LightingThreads = 0;
            }
          }
          int i15 = i14 + 1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[59 + Main.qaStyle].Value, i15, anchor2, offset2, IngameOptions.rightScale[i15], (float) (((double) IngameOptions.rightScale[i15] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i15;
            if (flag4)
            {
              ++Main.qaStyle;
              if (Main.qaStyle > 3)
                Main.qaStyle = 0;
            }
          }
          int i16 = i15 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.BackgroundEnabled ? Lang.menu[100].Value : Lang.menu[101].Value, i16, anchor2, offset2, IngameOptions.rightScale[i16], (float) (((double) IngameOptions.rightScale[i16] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i16;
            if (flag4)
              Main.BackgroundEnabled = !Main.BackgroundEnabled;
          }
          int i17 = i16 + 1;
          if (IngameOptions.DrawRightSide(sb, ChildSafety.Disabled ? Lang.menu[132].Value : Lang.menu[133].Value, i17, anchor2, offset2, IngameOptions.rightScale[i17], (float) (((double) IngameOptions.rightScale[i17] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i17;
            if (flag4)
              ChildSafety.Disabled = !ChildSafety.Disabled;
          }
          int i18 = i17 + 1;
          if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.HeatDistortion", Main.UseHeatDistortion ? (object) Language.GetTextValue("GameUI.Enabled") : (object) Language.GetTextValue("GameUI.Disabled")), i18, anchor2, offset2, IngameOptions.rightScale[i18], (float) (((double) IngameOptions.rightScale[i18] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i18;
            if (flag4)
              Main.UseHeatDistortion = !Main.UseHeatDistortion;
          }
          int i19 = i18 + 1;
          if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.StormEffects", Main.UseStormEffects ? (object) Language.GetTextValue("GameUI.Enabled") : (object) Language.GetTextValue("GameUI.Disabled")), i19, anchor2, offset2, IngameOptions.rightScale[i19], (float) (((double) IngameOptions.rightScale[i19] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i19;
            if (flag4)
              Main.UseStormEffects = !Main.UseStormEffects;
          }
          int i20 = i19 + 1;
          string textValue;
          switch (Main.WaveQuality)
          {
            case 1:
              textValue = Language.GetTextValue("GameUI.QualityLow");
              break;
            case 2:
              textValue = Language.GetTextValue("GameUI.QualityMedium");
              break;
            case 3:
              textValue = Language.GetTextValue("GameUI.QualityHigh");
              break;
            default:
              textValue = Language.GetTextValue("GameUI.QualityOff");
              break;
          }
          if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.WaveQuality", (object) textValue), i20, anchor2, offset2, IngameOptions.rightScale[i20], (float) (((double) IngameOptions.rightScale[i20] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i20;
            if (flag4)
              Main.WaveQuality = (Main.WaveQuality + 1) % 4;
          }
          int num13 = i20 + 1;
        }
        if (IngameOptions.category == 3)
        {
          int i9 = 0;
          float num11 = (float) num1;
          if (flag1)
            num2 = 126f;
          Vector3 hslVector = Main.mouseColorSlider.GetHSLVector();
          Main.mouseColorSlider.ApplyToMainLegacyBars();
          IngameOptions.DrawRightSide(sb, Lang.menu[64].Value, i9, anchor2, offset2, IngameOptions.rightScale[i9], 1f, new Color());
          IngameOptions.skipRightSlot[i9] = true;
          int i10 = i9 + 1;
          anchor2.X -= num11;
          if (IngameOptions.DrawRightSide(sb, "", i10, anchor2, offset2, IngameOptions.rightScale[i10], (float) (((double) IngameOptions.rightScale[i10] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i10;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          IngameOptions.valuePosition.X -= num2;
          DelegateMethods.v3_1 = hslVector;
          float num12 = IngameOptions.DrawValueBar(sb, scale, hslVector.X, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_H));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i10) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i10;
            if (Main.mouseLeft && IngameOptions.rightLock == i10)
            {
              hslVector.X = num12;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i10;
          }
          if (IngameOptions.rightHover == i10)
          {
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 5;
            Main.menuMode = 25;
          }
          int i11 = i10 + 1;
          if (IngameOptions.DrawRightSide(sb, "", i11, anchor2, offset2, IngameOptions.rightScale[i11], (float) (((double) IngameOptions.rightScale[i11] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i11;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          IngameOptions.valuePosition.X -= num2;
          DelegateMethods.v3_1 = hslVector;
          float num13 = IngameOptions.DrawValueBar(sb, scale, hslVector.Y, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_S));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i11) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i11;
            if (Main.mouseLeft && IngameOptions.rightLock == i11)
            {
              hslVector.Y = num13;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i11;
          }
          if (IngameOptions.rightHover == i11)
          {
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 6;
            Main.menuMode = 25;
          }
          int i12 = i11 + 1;
          if (IngameOptions.DrawRightSide(sb, "", i12, anchor2, offset2, IngameOptions.rightScale[i12], (float) (((double) IngameOptions.rightScale[i12] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i12;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          IngameOptions.valuePosition.X -= num2;
          DelegateMethods.v3_1 = hslVector;
          DelegateMethods.v3_1.Z = Utils.InverseLerp(0.15f, 1f, DelegateMethods.v3_1.Z, true);
          float num14 = IngameOptions.DrawValueBar(sb, scale, DelegateMethods.v3_1.Z, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_L));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i12) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i12;
            if (Main.mouseLeft && IngameOptions.rightLock == i12)
            {
              hslVector.Z = (float) ((double) num14 * 0.850000023841858 + 0.150000005960464);
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i12;
          }
          if (IngameOptions.rightHover == i12)
          {
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 7;
            Main.menuMode = 25;
          }
          int i13 = i12 + 1;
          if ((double) hslVector.Z < 0.150000005960464)
            hslVector.Z = 0.15f;
          Main.mouseColorSlider.SetHSL(hslVector);
          Main.mouseColor = Main.mouseColorSlider.GetColor();
          anchor2.X += num11;
          IngameOptions.DrawRightSide(sb, "", i13, anchor2, offset2, IngameOptions.rightScale[i13], 1f, new Color());
          IngameOptions.skipRightSlot[i13] = true;
          int i14 = i13 + 1;
          hslVector = Main.mouseBorderColorSlider.GetHSLVector();
          if (PlayerInput.UsingGamepad && IngameOptions.rightHover == -1)
            Main.mouseBorderColorSlider.ApplyToMainLegacyBars();
          IngameOptions.DrawRightSide(sb, Lang.menu[217].Value, i14, anchor2, offset2, IngameOptions.rightScale[i14], 1f, new Color());
          IngameOptions.skipRightSlot[i14] = true;
          int i15 = i14 + 1;
          anchor2.X -= num11;
          if (IngameOptions.DrawRightSide(sb, "", i15, anchor2, offset2, IngameOptions.rightScale[i15], (float) (((double) IngameOptions.rightScale[i15] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i15;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          IngameOptions.valuePosition.X -= num2;
          DelegateMethods.v3_1 = hslVector;
          float num15 = IngameOptions.DrawValueBar(sb, scale, hslVector.X, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_H));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i15) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i15;
            if (Main.mouseLeft && IngameOptions.rightLock == i15)
            {
              hslVector.X = num15;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i15;
          }
          if (IngameOptions.rightHover == i15)
          {
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 5;
            Main.menuMode = 252;
          }
          int i16 = i15 + 1;
          if (IngameOptions.DrawRightSide(sb, "", i16, anchor2, offset2, IngameOptions.rightScale[i16], (float) (((double) IngameOptions.rightScale[i16] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i16;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          IngameOptions.valuePosition.X -= num2;
          DelegateMethods.v3_1 = hslVector;
          float num16 = IngameOptions.DrawValueBar(sb, scale, hslVector.Y, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_S));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i16) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i16;
            if (Main.mouseLeft && IngameOptions.rightLock == i16)
            {
              hslVector.Y = num16;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i16;
          }
          if (IngameOptions.rightHover == i16)
          {
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 6;
            Main.menuMode = 252;
          }
          int i17 = i16 + 1;
          if (IngameOptions.DrawRightSide(sb, "", i17, anchor2, offset2, IngameOptions.rightScale[i17], (float) (((double) IngameOptions.rightScale[i17] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i17;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          IngameOptions.valuePosition.X -= num2;
          DelegateMethods.v3_1 = hslVector;
          float num17 = IngameOptions.DrawValueBar(sb, scale, hslVector.Z, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_L));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i17) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i17;
            if (Main.mouseLeft && IngameOptions.rightLock == i17)
            {
              hslVector.Z = num17;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i17;
          }
          if (IngameOptions.rightHover == i17)
          {
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 7;
            Main.menuMode = 252;
          }
          int i18 = i17 + 1;
          if (IngameOptions.DrawRightSide(sb, "", i18, anchor2, offset2, IngameOptions.rightScale[i18], (float) (((double) IngameOptions.rightScale[i18] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i18;
          }
          IngameOptions.valuePosition.X = (float) ((double) vector2_4.X + (double) vector2_3.X - (double) (num4 / 2) - 20.0);
          IngameOptions.valuePosition.Y -= 3f;
          IngameOptions.valuePosition.X -= num2;
          DelegateMethods.v3_1 = hslVector;
          float perc = Main.mouseBorderColorSlider.Alpha;
          float num18 = IngameOptions.DrawValueBar(sb, scale, perc, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_O));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i18) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i18;
            if (Main.mouseLeft && IngameOptions.rightLock == i18)
            {
              perc = num18;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > (double) vector2_4.X + (double) vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < (double) IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > (double) IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= (double) IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i18;
          }
          if (IngameOptions.rightHover == i18)
          {
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 8;
            Main.menuMode = 252;
          }
          int i19 = i18 + 1;
          Main.mouseBorderColorSlider.SetHSL(hslVector);
          Main.mouseBorderColorSlider.Alpha = perc;
          Main.MouseBorderColor = Main.mouseBorderColorSlider.GetColor();
          anchor2.X += num11;
          IngameOptions.DrawRightSide(sb, "", i19, anchor2, offset2, IngameOptions.rightScale[i19], 1f, new Color());
          IngameOptions.skipRightSlot[i19] = true;
          int i20 = i19 + 1;
          string txt = "";
          switch (LockOnHelper.UseMode)
          {
            case LockOnHelper.LockOnMode.FocusTarget:
              txt = Lang.menu[232].Value;
              break;
            case LockOnHelper.LockOnMode.TargetClosest:
              txt = Lang.menu[233].Value;
              break;
            case LockOnHelper.LockOnMode.ThreeDS:
              txt = Lang.menu[234].Value;
              break;
          }
          if (IngameOptions.DrawRightSide(sb, txt, i20, anchor2, offset2, IngameOptions.rightScale[i20] * 0.9f, (float) (((double) IngameOptions.rightScale[i20] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i20;
            if (flag4)
              LockOnHelper.CycleUseModes();
          }
          int i21 = i20 + 1;
          if (IngameOptions.DrawRightSide(sb, Player.SmartCursorSettings.SmartBlocksEnabled ? Lang.menu[215].Value : Lang.menu[216].Value, i21, anchor2, offset2, IngameOptions.rightScale[i21] * 0.9f, (float) (((double) IngameOptions.rightScale[i21] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i21;
            if (flag4)
              Player.SmartCursorSettings.SmartBlocksEnabled = !Player.SmartCursorSettings.SmartBlocksEnabled;
          }
          int i22 = i21 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.cSmartCursorToggle ? Lang.menu[121].Value : Lang.menu[122].Value, i22, anchor2, offset2, IngameOptions.rightScale[i22], (float) (((double) IngameOptions.rightScale[i22] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i22;
            if (flag4)
              Main.cSmartCursorToggle = !Main.cSmartCursorToggle;
          }
          int i23 = i22 + 1;
          if (IngameOptions.DrawRightSide(sb, Player.SmartCursorSettings.SmartAxeAfterPickaxe ? Lang.menu[214].Value : Lang.menu[213].Value, i23, anchor2, offset2, IngameOptions.rightScale[i23] * 0.9f, (float) (((double) IngameOptions.rightScale[i23] - (double) num5) / ((double) num6 - (double) num5)), new Color()))
          {
            IngameOptions.rightHover = i23;
            if (flag4)
              Player.SmartCursorSettings.SmartAxeAfterPickaxe = !Player.SmartCursorSettings.SmartAxeAfterPickaxe;
          }
          int num19 = i23 + 1;
        }
        if (IngameOptions.rightHover != -1 && IngameOptions.rightLock == -1)
          IngameOptions.rightLock = IngameOptions.rightHover;
        for (int index = 0; index < num8 + 1; ++index)
          UILinkPointNavigator.SetPosition(2900 + index, anchor1 + offset1 * (float) (index + 1));
        int num20 = 0;
        Vector2 zero = Vector2.Zero;
        if (flag1)
          zero.X = -40f;
        for (int index = 0; index < num10; ++index)
        {
          if (!IngameOptions.skipRightSlot[index])
          {
            UILinkPointNavigator.SetPosition(2930 + num20, anchor2 + zero + offset2 * (float) (index + 1));
            ++num20;
          }
        }
        UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_RIGHT = num20;
        Main.DrawGamepadInstructions();
        Main.mouseText = false;
        Main.instance.GUIBarsDraw();
        Main.instance.DrawMouseOver();
        Main.DrawCursor(Main.DrawThickCursor(false), false);
      }
    }

    public static void MouseOver()
    {
      if (!Main.ingameOptionsWindow)
        return;
      if (IngameOptions._GUIHover.Contains(Main.MouseScreen.ToPoint()))
        Main.mouseText = true;
      if (IngameOptions._mouseOverText != null)
        Main.instance.MouseText(IngameOptions._mouseOverText, 0, (byte) 0, -1, -1, -1, -1);
      IngameOptions._mouseOverText = (string) null;
    }

    public static bool DrawLeftSide(SpriteBatch sb, string txt, int i, Vector2 anchor, Vector2 offset, float[] scales, float minscale = 0.7f, float maxscale = 0.8f, float scalespeed = 0.01f)
    {
      bool flag = i == IngameOptions.category;
      Color color = Color.Lerp(Color.Gray, Color.White, (float) (((double) scales[i] - (double) minscale) / ((double) maxscale - (double) minscale)));
      if (flag)
        color = Color.Gold;
      Vector2 vector2 = Utils.DrawBorderStringBig(sb, txt, anchor + offset * (float) (1 + i), color, scales[i], 0.5f, 0.5f, -1);
      return new Rectangle((int) anchor.X - (int) vector2.X / 2, (int) anchor.Y + (int) ((double) offset.Y * (double) (1 + i)) - (int) vector2.Y / 2, (int) vector2.X, (int) vector2.Y).Contains(new Point(Main.mouseX, Main.mouseY));
    }

    public static bool DrawRightSide(SpriteBatch sb, string txt, int i, Vector2 anchor, Vector2 offset, float scale, float colorScale, Color over = default (Color))
    {
      Color color = Color.Lerp(Color.Gray, Color.White, colorScale);
      if (over != new Color())
        color = over;
      Vector2 vector2 = Utils.DrawBorderString(sb, txt, anchor + offset * (float) (1 + i), color, scale, 0.5f, 0.5f, -1);
      IngameOptions.valuePosition = anchor + offset * (float) (1 + i) + vector2 * new Vector2(0.5f, 0.0f);
      return new Rectangle((int) anchor.X - (int) vector2.X / 2, (int) anchor.Y + (int) ((double) offset.Y * (double) (1 + i)) - (int) vector2.Y / 2, (int) vector2.X, (int) vector2.Y).Contains(new Point(Main.mouseX, Main.mouseY));
    }

    public static bool DrawValue(SpriteBatch sb, string txt, int i, float scale, Color over = default (Color))
    {
      Color color = Color.Gray;
      Vector2 vector2 = Main.fontMouseText.MeasureString(txt) * scale;
      bool flag = new Rectangle((int) IngameOptions.valuePosition.X, (int) IngameOptions.valuePosition.Y - (int) vector2.Y / 2, (int) vector2.X, (int) vector2.Y).Contains(new Point(Main.mouseX, Main.mouseY));
      if (flag)
        color = Color.White;
      if (over != new Color())
        color = over;
      Utils.DrawBorderString(sb, txt, IngameOptions.valuePosition, color, scale, 0.0f, 0.5f, -1);
      IngameOptions.valuePosition.X += vector2.X;
      return flag;
    }

    public static float DrawValueBar(SpriteBatch sb, float scale, float perc, int lockState = 0, Utils.ColorLerpMethod colorMethod = null)
    {
      if (colorMethod == null)
        colorMethod = new Utils.ColorLerpMethod(Utils.ColorLerp_BlackToWhite);
      Texture2D colorBarTexture = Main.colorBarTexture;
      Vector2 vector2 = new Vector2((float) colorBarTexture.Width, (float) colorBarTexture.Height) * scale;
      IngameOptions.valuePosition.X -= (float) (int) vector2.X;
      Rectangle destinationRectangle1 = new Rectangle((int) IngameOptions.valuePosition.X, (int) IngameOptions.valuePosition.Y - (int) vector2.Y / 2, (int) vector2.X, (int) vector2.Y);
      Rectangle destinationRectangle2 = destinationRectangle1;
      sb.Draw(colorBarTexture, destinationRectangle1, Color.White);
      int num1 = 167;
      float num2 = (float) destinationRectangle1.X + 5f * scale;
      float y = (float) destinationRectangle1.Y + 4f * scale;
      for (float num3 = 0.0f; (double) num3 < (double) num1; ++num3)
      {
        float percent = num3 / (float) num1;
        sb.Draw(Main.colorBlipTexture, new Vector2(num2 + num3 * scale, y), new Rectangle?(), colorMethod(percent), 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
      }
      destinationRectangle1.X = (int) num2;
      destinationRectangle1.Y = (int) y;
      bool flag = destinationRectangle1.Contains(new Point(Main.mouseX, Main.mouseY));
      if (lockState == 2)
        flag = false;
      if (flag || lockState == 1)
        sb.Draw(Main.colorHighlightTexture, destinationRectangle2, Main.OurFavoriteColor);
      sb.Draw(Main.colorSliderTexture, new Vector2(num2 + 167f * scale * perc, y + 4f * scale), new Rectangle?(), Color.White, 0.0f, new Vector2(0.5f * (float) Main.colorSliderTexture.Width, 0.5f * (float) Main.colorSliderTexture.Height), scale, SpriteEffects.None, 0.0f);
      if (Main.mouseX >= destinationRectangle1.X && Main.mouseX <= destinationRectangle1.X + destinationRectangle1.Width)
      {
        IngameOptions.inBar = flag;
        return (float) (Main.mouseX - destinationRectangle1.X) / (float) destinationRectangle1.Width;
      }
      IngameOptions.inBar = false;
      return destinationRectangle1.X >= Main.mouseX ? 0.0f : 1f;
    }
  }
}
