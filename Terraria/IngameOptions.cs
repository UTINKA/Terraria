// Decompiled with JetBrains decompiler
// Type: Terraria.IngameOptions
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

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
    private static Rectangle _GUIHover = (Rectangle) null;
    public static int category = 0;
    public static Vector2 valuePosition = Vector2.get_Zero();
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
        Vector2 vector2_2;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2_2).\u002Ector((float) Main.screenWidth, (float) Main.screenHeight);
        Vector2 vector2_3;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2_3).\u002Ector(670f, 480f);
        Vector2 vector2_4 = Vector2.op_Subtraction(Vector2.op_Division(vector2_2, 2f), Vector2.op_Division(vector2_3, 2f));
        int num4 = 20;
        IngameOptions._GUIHover = new Rectangle((int) (vector2_4.X - (double) num4), (int) (vector2_4.Y - (double) num4), (int) (vector2_3.X + (double) (num4 * 2)), (int) (vector2_3.Y + (double) (num4 * 2)));
        Utils.DrawInvBG(sb, (float) vector2_4.X - (float) num4, (float) vector2_4.Y - (float) num4, (float) vector2_3.X + (float) (num4 * 2), (float) vector2_3.Y + (float) (num4 * 2), Color.op_Multiply(new Color(33, 15, 91, (int) byte.MaxValue), 0.685f));
        Rectangle rectangle = new Rectangle((int) vector2_4.X - num4, (int) vector2_4.Y - num4, (int) vector2_3.X + num4 * 2, (int) vector2_3.Y + num4 * 2);
        // ISSUE: explicit reference operation
        if (((Rectangle) @rectangle).Contains(new Point(Main.mouseX, Main.mouseY)))
          Main.player[Main.myPlayer].mouseInterface = true;
        Utils.DrawBorderString(sb, Language.GetTextValue("GameUI.SettingsMenu"), Vector2.op_Addition(vector2_4, Vector2.op_Multiply(vector2_3, new Vector2(0.5f, 0.0f))), Color.get_White(), 1f, 0.5f, 0.0f, -1);
        if (flag1)
        {
          Utils.DrawInvBG(sb, (float) vector2_4.X + (float) (num4 / 2), (float) vector2_4.Y + (float) (num4 * 5 / 2), (float) (vector2_3.X / 3.0) - (float) num4, (float) vector2_3.Y - (float) (num4 * 3), (Color) null);
          Utils.DrawInvBG(sb, (float) (vector2_4.X + vector2_3.X / 3.0) + (float) num4, (float) vector2_4.Y + (float) (num4 * 5 / 2), (float) (vector2_3.X * 2.0 / 3.0) - (float) (num4 * 3 / 2), (float) vector2_3.Y - (float) (num4 * 3), (Color) null);
        }
        else
        {
          Utils.DrawInvBG(sb, (float) vector2_4.X + (float) (num4 / 2), (float) vector2_4.Y + (float) (num4 * 5 / 2), (float) (vector2_3.X / 2.0) - (float) num4, (float) vector2_3.Y - (float) (num4 * 3), (Color) null);
          Utils.DrawInvBG(sb, (float) (vector2_4.X + vector2_3.X / 2.0) + (float) num4, (float) vector2_4.Y + (float) (num4 * 5 / 2), (float) (vector2_3.X / 2.0) - (float) (num4 * 3 / 2), (float) vector2_3.Y - (float) (num4 * 3), (Color) null);
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
        Vector2 anchor1;
        // ISSUE: explicit reference operation
        ((Vector2) @anchor1).\u002Ector((float) (vector2_4.X + vector2_3.X / 4.0), (float) vector2_4.Y + (float) (num4 * 5 / 2));
        Vector2 offset1 = Vector2.op_Division(new Vector2(0.0f, (float) vector2_3.Y - (float) (num4 * 5)), (float) (num8 + 1));
        if (flag1)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local = @anchor1;
          // ISSUE: explicit reference operation
          double num9 = (^local).X - 55.0;
          // ISSUE: explicit reference operation
          (^local).X = (__Null) num9;
        }
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
        int num10 = i8 + 1;
        if (category != IngameOptions.category)
        {
          for (int index = 0; index < IngameOptions.rightScale.Length; ++index)
            IngameOptions.rightScale[index] = 0.0f;
        }
        int num11 = 0;
        switch (IngameOptions.category)
        {
          case 0:
            num11 = 15;
            num5 = 1f;
            num6 = 1.001f;
            num7 = 1f / 1000f;
            break;
          case 1:
            num11 = 6;
            num5 = 1f;
            num6 = 1.001f;
            num7 = 1f / 1000f;
            break;
          case 2:
            num11 = 12;
            num5 = 1f;
            num6 = 1.001f;
            num7 = 1f / 1000f;
            break;
          case 3:
            num11 = 15;
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
        UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_RIGHT = num11;
        Vector2 anchor2;
        // ISSUE: explicit reference operation
        ((Vector2) @anchor2).\u002Ector((float) (vector2_4.X + vector2_3.X * 3.0 / 4.0), (float) vector2_4.Y + (float) (num4 * 5 / 2));
        if (flag1)
          anchor2.X = (__Null) (vector2_4.X + vector2_3.X * 2.0 / 3.0);
        Vector2 offset2 = Vector2.op_Division(new Vector2(0.0f, (float) vector2_3.Y - (float) (num4 * 3)), (float) (num11 + 1));
        if (IngameOptions.category == 2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local = @offset2;
          // ISSUE: explicit reference operation
          double num9 = (^local).Y - 2.0;
          // ISSUE: explicit reference operation
          (^local).Y = (__Null) num9;
        }
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
          IngameOptions.DrawRightSide(sb, Lang.menu[65].Value, i9, anchor2, offset2, IngameOptions.rightScale[i9], 1f, (Color) null);
          IngameOptions.skipRightSlot[i9] = true;
          int i10 = i9 + 1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local1 = @anchor2;
          // ISSUE: explicit reference operation
          double num9 = (^local1).X - (double) num1;
          // ISSUE: explicit reference operation
          (^local1).X = (__Null) num9;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[99].Value + " " + (object) Math.Round((double) Main.musicVolume * 100.0) + "%", i10, anchor2, offset2, IngameOptions.rightScale[i10], (float) (((double) IngameOptions.rightScale[i10] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.noSound = true;
            IngameOptions.rightHover = i10;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local2 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num12 = (^local2).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local2).Y = (__Null) num12;
          float num13 = IngameOptions.DrawValueBar(sb, scale, Main.musicVolume, 0, (Utils.ColorLerpMethod) null);
          if ((IngameOptions.inBar || IngameOptions.rightLock == i10) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i10;
            if (Main.mouseLeft && IngameOptions.rightLock == i10)
              Main.musicVolume = num13;
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i10;
          }
          if (IngameOptions.rightHover == i10)
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 2;
          int i11 = i10 + 1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[98].Value + " " + (object) Math.Round((double) Main.soundVolume * 100.0) + "%", i11, anchor2, offset2, IngameOptions.rightScale[i11], (float) (((double) IngameOptions.rightScale[i11] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i11;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local3 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num14 = (^local3).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local3).Y = (__Null) num14;
          float num15 = IngameOptions.DrawValueBar(sb, scale, Main.soundVolume, 0, (Utils.ColorLerpMethod) null);
          if ((IngameOptions.inBar || IngameOptions.rightLock == i11) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i11;
            if (Main.mouseLeft && IngameOptions.rightLock == i11)
            {
              Main.soundVolume = num15;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i11;
          }
          if (IngameOptions.rightHover == i11)
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 3;
          int i12 = i11 + 1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[119].Value + " " + (object) Math.Round((double) Main.ambientVolume * 100.0) + "%", i12, anchor2, offset2, IngameOptions.rightScale[i12], (float) (((double) IngameOptions.rightScale[i12] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i12;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local4 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num16 = (^local4).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local4).Y = (__Null) num16;
          float num17 = IngameOptions.DrawValueBar(sb, scale, Main.ambientVolume, 0, (Utils.ColorLerpMethod) null);
          if ((IngameOptions.inBar || IngameOptions.rightLock == i12) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i12;
            if (Main.mouseLeft && IngameOptions.rightLock == i12)
            {
              Main.ambientVolume = num17;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i12;
          }
          if (IngameOptions.rightHover == i12)
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 4;
          int i13 = i12 + 1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local5 = @anchor2;
          // ISSUE: explicit reference operation
          double num18 = (^local5).X + (double) num1;
          // ISSUE: explicit reference operation
          (^local5).X = (__Null) num18;
          IngameOptions.DrawRightSide(sb, "", i13, anchor2, offset2, IngameOptions.rightScale[i13], 1f, (Color) null);
          IngameOptions.skipRightSlot[i13] = true;
          int i14 = i13 + 1;
          IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.ZoomCategory"), i14, anchor2, offset2, IngameOptions.rightScale[i14], 1f, (Color) null);
          IngameOptions.skipRightSlot[i14] = true;
          int i15 = i14 + 1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local6 = @anchor2;
          // ISSUE: explicit reference operation
          double num19 = (^local6).X - (double) num1;
          // ISSUE: explicit reference operation
          (^local6).X = (__Null) num19;
          string txt1 = Language.GetTextValue("GameUI.GameZoom", (object) Math.Round((double) Main.GameZoomTarget * 100.0), (object) Math.Round(Main.GameViewMatrix.Zoom.X * 100.0));
          if (flag3)
            txt1 = Main.fontItemStack.CreateWrappedText(txt1, num3, Language.ActiveCulture.CultureInfo);
          if (IngameOptions.DrawRightSide(sb, txt1, i15, anchor2, offset2, IngameOptions.rightScale[i15] * 0.85f, (float) (((double) IngameOptions.rightScale[i15] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i15;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local7 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num20 = (^local7).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local7).Y = (__Null) num20;
          float num21 = IngameOptions.DrawValueBar(sb, scale, Main.GameZoomTarget - 1f, 0, (Utils.ColorLerpMethod) null);
          if ((IngameOptions.inBar || IngameOptions.rightLock == i15) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i15;
            if (Main.mouseLeft && IngameOptions.rightLock == i15)
              Main.GameZoomTarget = num21 + 1f;
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
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
          if (IngameOptions.DrawRightSide(sb, txt2, i16, anchor2, offset2, IngameOptions.rightScale[i16] * 0.75f, (float) (((double) IngameOptions.rightScale[i16] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i16;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local8 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num22 = (^local8).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local8).Y = (__Null) num22;
          float num23 = IngameOptions.DrawValueBar(sb, scale, Main.temporaryGUIScaleSlider - 1f, 0, (Utils.ColorLerpMethod) null);
          if ((IngameOptions.inBar || IngameOptions.rightLock == i16) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i16;
            if (Main.mouseLeft && IngameOptions.rightLock == i16)
            {
              Main.temporaryGUIScaleSlider = num23 + 1f;
              Main.temporaryGUIScaleSliderUpdate = true;
              flag6 = true;
            }
          }
          if (!flag6 && Main.temporaryGUIScaleSliderUpdate && (double) Main.temporaryGUIScaleSlider != -1.0)
          {
            Main.UIScale = Main.temporaryGUIScaleSlider;
            Main.temporaryGUIScaleSliderUpdate = false;
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i16;
          }
          if (IngameOptions.rightHover == i16)
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 11;
          int i17 = i16 + 1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local9 = @anchor2;
          // ISSUE: explicit reference operation
          double num24 = (^local9).X + (double) num1;
          // ISSUE: explicit reference operation
          (^local9).X = (__Null) num24;
          IngameOptions.DrawRightSide(sb, "", i17, anchor2, offset2, IngameOptions.rightScale[i17], 1f, (Color) null);
          IngameOptions.skipRightSlot[i17] = true;
          int i18 = i17 + 1;
          IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.Gameplay"), i18, anchor2, offset2, IngameOptions.rightScale[i18], 1f, (Color) null);
          IngameOptions.skipRightSlot[i18] = true;
          int i19 = i18 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.autoSave ? Lang.menu[67].Value : Lang.menu[68].Value, i19, anchor2, offset2, IngameOptions.rightScale[i19], (float) (((double) IngameOptions.rightScale[i19] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i19;
            if (flag4)
              Main.autoSave = !Main.autoSave;
          }
          int i20 = i19 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.autoPause ? Lang.menu[69].Value : Lang.menu[70].Value, i20, anchor2, offset2, IngameOptions.rightScale[i20], (float) (((double) IngameOptions.rightScale[i20] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i20;
            if (flag4)
              Main.autoPause = !Main.autoPause;
          }
          int i21 = i20 + 1;
          if (IngameOptions.DrawRightSide(sb, Player.SmartCursorSettings.SmartWallReplacement ? Lang.menu[226].Value : Lang.menu[225].Value, i21, anchor2, offset2, IngameOptions.rightScale[i21], (float) (((double) IngameOptions.rightScale[i21] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i21;
            if (flag4)
              Player.SmartCursorSettings.SmartWallReplacement = !Player.SmartCursorSettings.SmartWallReplacement;
          }
          int i22 = i21 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.ReversedUpDownArmorSetBonuses ? Lang.menu[220].Value : Lang.menu[221].Value, i22, anchor2, offset2, IngameOptions.rightScale[i22], (float) (((double) IngameOptions.rightScale[i22] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i22;
            if (flag4)
              Main.ReversedUpDownArmorSetBonuses = !Main.ReversedUpDownArmorSetBonuses;
          }
          int i23 = i22 + 1;
          IngameOptions.DrawRightSide(sb, "", i23, anchor2, offset2, IngameOptions.rightScale[i23], 1f, (Color) null);
          IngameOptions.skipRightSlot[i23] = true;
          int num25 = i23 + 1;
        }
        if (IngameOptions.category == 1)
        {
          int i9 = 0;
          if (IngameOptions.DrawRightSide(sb, Main.showItemText ? Lang.menu[71].Value : Lang.menu[72].Value, i9, anchor2, offset2, IngameOptions.rightScale[i9], (float) (((double) IngameOptions.rightScale[i9] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i9;
            if (flag4)
              Main.showItemText = !Main.showItemText;
          }
          int i10 = i9 + 1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[123].Value + " " + (object) Lang.menu[124 + Main.invasionProgressMode], i10, anchor2, offset2, IngameOptions.rightScale[i10], (float) (((double) IngameOptions.rightScale[i10] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
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
          if (IngameOptions.DrawRightSide(sb, Main.placementPreview ? Lang.menu[128].Value : Lang.menu[129].Value, i11, anchor2, offset2, IngameOptions.rightScale[i11], (float) (((double) IngameOptions.rightScale[i11] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i11;
            if (flag4)
              Main.placementPreview = !Main.placementPreview;
          }
          int i12 = i11 + 1;
          if (IngameOptions.DrawRightSide(sb, ItemSlot.Options.HighlightNewItems ? Lang.inter[117].Value : Lang.inter[116].Value, i12, anchor2, offset2, IngameOptions.rightScale[i12], (float) (((double) IngameOptions.rightScale[i12] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i12;
            if (flag4)
              ItemSlot.Options.HighlightNewItems = !ItemSlot.Options.HighlightNewItems;
          }
          int i13 = i12 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.MouseShowBuildingGrid ? Lang.menu[229].Value : Lang.menu[230].Value, i13, anchor2, offset2, IngameOptions.rightScale[i13], (float) (((double) IngameOptions.rightScale[i13] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i13;
            if (flag4)
              Main.MouseShowBuildingGrid = !Main.MouseShowBuildingGrid;
          }
          int i14 = i13 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.GamepadDisableInstructionsDisplay ? Lang.menu[241].Value : Lang.menu[242].Value, i14, anchor2, offset2, IngameOptions.rightScale[i14], (float) (((double) IngameOptions.rightScale[i14] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i14;
            if (flag4)
              Main.GamepadDisableInstructionsDisplay = !Main.GamepadDisableInstructionsDisplay;
          }
          int num9 = i14 + 1;
        }
        if (IngameOptions.category == 2)
        {
          int i9 = 0;
          if (IngameOptions.DrawRightSide(sb, Main.graphics.get_IsFullScreen() ? Lang.menu[49].Value : Lang.menu[50].Value, i9, anchor2, offset2, IngameOptions.rightScale[i9], (float) (((double) IngameOptions.rightScale[i9] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i9;
            if (flag4)
              Main.ToggleFullScreen();
          }
          int i10 = i9 + 1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[51].Value + ": " + (object) Main.PendingResolutionWidth + "x" + (object) Main.PendingResolutionHeight, i10, anchor2, offset2, IngameOptions.rightScale[i10], (float) (((double) IngameOptions.rightScale[i10] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i10;
            if (flag4)
            {
              int num9 = 0;
              for (int index = 0; index < Main.numDisplayModes; ++index)
              {
                if (Main.displayWidth[index] == Main.PendingResolutionWidth && Main.displayHeight[index] == Main.PendingResolutionHeight)
                {
                  num9 = index;
                  break;
                }
              }
              int index1 = num9 + 1;
              if (index1 >= Main.numDisplayModes)
                index1 = 0;
              Main.PendingResolutionWidth = Main.displayWidth[index1];
              Main.PendingResolutionHeight = Main.displayHeight[index1];
            }
          }
          int i11 = i10 + 1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local1 = @anchor2;
          // ISSUE: explicit reference operation
          double num12 = (^local1).X - (double) num1;
          // ISSUE: explicit reference operation
          (^local1).X = (__Null) num12;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[52].Value + ": " + (object) Main.bgScroll + "%", i11, anchor2, offset2, IngameOptions.rightScale[i11], (float) (((double) IngameOptions.rightScale[i11] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.noSound = true;
            IngameOptions.rightHover = i11;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local2 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num13 = (^local2).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local2).Y = (__Null) num13;
          float num14 = IngameOptions.DrawValueBar(sb, scale, (float) Main.bgScroll / 100f, 0, (Utils.ColorLerpMethod) null);
          if ((IngameOptions.inBar || IngameOptions.rightLock == i11) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i11;
            if (Main.mouseLeft && IngameOptions.rightLock == i11)
            {
              Main.bgScroll = (int) ((double) num14 * 100.0);
              Main.caveParallax = (float) (1.0 - (double) Main.bgScroll / 500.0);
            }
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i11;
          }
          if (IngameOptions.rightHover == i11)
            UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 1;
          int i12 = i11 + 1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local3 = @anchor2;
          // ISSUE: explicit reference operation
          double num15 = (^local3).X + (double) num1;
          // ISSUE: explicit reference operation
          (^local3).X = (__Null) num15;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[247 + Main.FrameSkipMode].Value, i12, anchor2, offset2, IngameOptions.rightScale[i12], (float) (((double) IngameOptions.rightScale[i12] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
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
          if (IngameOptions.DrawRightSide(sb, Lang.menu[55 + Lighting.lightMode].Value, i13, anchor2, offset2, IngameOptions.rightScale[i13], (float) (((double) IngameOptions.rightScale[i13] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i13;
            if (flag4)
              Lighting.NextLightMode();
          }
          int i14 = i13 + 1;
          if (IngameOptions.DrawRightSide(sb, Lang.menu[116].Value + " " + (Lighting.LightingThreads > 0 ? string.Concat((object) (Lighting.LightingThreads + 1)) : Lang.menu[117].Value), i14, anchor2, offset2, IngameOptions.rightScale[i14], (float) (((double) IngameOptions.rightScale[i14] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
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
          if (IngameOptions.DrawRightSide(sb, Lang.menu[59 + Main.qaStyle].Value, i15, anchor2, offset2, IngameOptions.rightScale[i15], (float) (((double) IngameOptions.rightScale[i15] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
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
          if (IngameOptions.DrawRightSide(sb, Main.BackgroundEnabled ? Lang.menu[100].Value : Lang.menu[101].Value, i16, anchor2, offset2, IngameOptions.rightScale[i16], (float) (((double) IngameOptions.rightScale[i16] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i16;
            if (flag4)
              Main.BackgroundEnabled = !Main.BackgroundEnabled;
          }
          int i17 = i16 + 1;
          if (IngameOptions.DrawRightSide(sb, ChildSafety.Disabled ? Lang.menu[132].Value : Lang.menu[133].Value, i17, anchor2, offset2, IngameOptions.rightScale[i17], (float) (((double) IngameOptions.rightScale[i17] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i17;
            if (flag4)
              ChildSafety.Disabled = !ChildSafety.Disabled;
          }
          int i18 = i17 + 1;
          if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.HeatDistortion", Main.UseHeatDistortion ? (object) Language.GetTextValue("GameUI.Enabled") : (object) Language.GetTextValue("GameUI.Disabled")), i18, anchor2, offset2, IngameOptions.rightScale[i18], (float) (((double) IngameOptions.rightScale[i18] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i18;
            if (flag4)
              Main.UseHeatDistortion = !Main.UseHeatDistortion;
          }
          int i19 = i18 + 1;
          if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.StormEffects", Main.UseStormEffects ? (object) Language.GetTextValue("GameUI.Enabled") : (object) Language.GetTextValue("GameUI.Disabled")), i19, anchor2, offset2, IngameOptions.rightScale[i19], (float) (((double) IngameOptions.rightScale[i19] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
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
          if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.WaveQuality", (object) textValue), i20, anchor2, offset2, IngameOptions.rightScale[i20], (float) (((double) IngameOptions.rightScale[i20] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i20;
            if (flag4)
              Main.WaveQuality = (Main.WaveQuality + 1) % 4;
          }
          int num16 = i20 + 1;
        }
        if (IngameOptions.category == 3)
        {
          int i9 = 0;
          float num9 = (float) num1;
          if (flag1)
            num2 = 126f;
          Vector3 hslVector = Main.mouseColorSlider.GetHSLVector();
          Main.mouseColorSlider.ApplyToMainLegacyBars();
          IngameOptions.DrawRightSide(sb, Lang.menu[64].Value, i9, anchor2, offset2, IngameOptions.rightScale[i9], 1f, (Color) null);
          IngameOptions.skipRightSlot[i9] = true;
          int i10 = i9 + 1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local1 = @anchor2;
          // ISSUE: explicit reference operation
          double num12 = (^local1).X - (double) num9;
          // ISSUE: explicit reference operation
          (^local1).X = (__Null) num12;
          if (IngameOptions.DrawRightSide(sb, "", i10, anchor2, offset2, IngameOptions.rightScale[i10], (float) (((double) IngameOptions.rightScale[i10] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i10;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local2 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num13 = (^local2).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local2).Y = (__Null) num13;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local3 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num14 = (^local3).X - (double) num2;
          // ISSUE: explicit reference operation
          (^local3).X = (__Null) num14;
          DelegateMethods.v3_1 = hslVector;
          float num15 = IngameOptions.DrawValueBar(sb, scale, (float) hslVector.X, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_H));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i10) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i10;
            if (Main.mouseLeft && IngameOptions.rightLock == i10)
            {
              hslVector.X = (__Null) (double) num15;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
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
          if (IngameOptions.DrawRightSide(sb, "", i11, anchor2, offset2, IngameOptions.rightScale[i11], (float) (((double) IngameOptions.rightScale[i11] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i11;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local4 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num16 = (^local4).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local4).Y = (__Null) num16;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local5 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num17 = (^local5).X - (double) num2;
          // ISSUE: explicit reference operation
          (^local5).X = (__Null) num17;
          DelegateMethods.v3_1 = hslVector;
          float num18 = IngameOptions.DrawValueBar(sb, scale, (float) hslVector.Y, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_S));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i11) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i11;
            if (Main.mouseLeft && IngameOptions.rightLock == i11)
            {
              hslVector.Y = (__Null) (double) num18;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
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
          if (IngameOptions.DrawRightSide(sb, "", i12, anchor2, offset2, IngameOptions.rightScale[i12], (float) (((double) IngameOptions.rightScale[i12] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i12;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local6 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num19 = (^local6).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local6).Y = (__Null) num19;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local7 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num20 = (^local7).X - (double) num2;
          // ISSUE: explicit reference operation
          (^local7).X = (__Null) num20;
          DelegateMethods.v3_1 = hslVector;
          DelegateMethods.v3_1.Z = (__Null) (double) Utils.InverseLerp(0.15f, 1f, (float) DelegateMethods.v3_1.Z, true);
          float num21 = IngameOptions.DrawValueBar(sb, scale, (float) DelegateMethods.v3_1.Z, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_L));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i12) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i12;
            if (Main.mouseLeft && IngameOptions.rightLock == i12)
            {
              hslVector.Z = (__Null) ((double) num21 * 0.850000023841858 + 0.150000005960464);
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
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
          if (hslVector.Z < 0.150000005960464)
            hslVector.Z = (__Null) 0.150000005960464;
          Main.mouseColorSlider.SetHSL(hslVector);
          Main.mouseColor = Main.mouseColorSlider.GetColor();
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local8 = @anchor2;
          // ISSUE: explicit reference operation
          double num22 = (^local8).X + (double) num9;
          // ISSUE: explicit reference operation
          (^local8).X = (__Null) num22;
          IngameOptions.DrawRightSide(sb, "", i13, anchor2, offset2, IngameOptions.rightScale[i13], 1f, (Color) null);
          IngameOptions.skipRightSlot[i13] = true;
          int i14 = i13 + 1;
          hslVector = Main.mouseBorderColorSlider.GetHSLVector();
          if (PlayerInput.UsingGamepad && IngameOptions.rightHover == -1)
            Main.mouseBorderColorSlider.ApplyToMainLegacyBars();
          IngameOptions.DrawRightSide(sb, Lang.menu[217].Value, i14, anchor2, offset2, IngameOptions.rightScale[i14], 1f, (Color) null);
          IngameOptions.skipRightSlot[i14] = true;
          int i15 = i14 + 1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local9 = @anchor2;
          // ISSUE: explicit reference operation
          double num23 = (^local9).X - (double) num9;
          // ISSUE: explicit reference operation
          (^local9).X = (__Null) num23;
          if (IngameOptions.DrawRightSide(sb, "", i15, anchor2, offset2, IngameOptions.rightScale[i15], (float) (((double) IngameOptions.rightScale[i15] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i15;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local10 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num24 = (^local10).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local10).Y = (__Null) num24;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local11 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num25 = (^local11).X - (double) num2;
          // ISSUE: explicit reference operation
          (^local11).X = (__Null) num25;
          DelegateMethods.v3_1 = hslVector;
          float num26 = IngameOptions.DrawValueBar(sb, scale, (float) hslVector.X, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_H));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i15) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i15;
            if (Main.mouseLeft && IngameOptions.rightLock == i15)
            {
              hslVector.X = (__Null) (double) num26;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
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
          if (IngameOptions.DrawRightSide(sb, "", i16, anchor2, offset2, IngameOptions.rightScale[i16], (float) (((double) IngameOptions.rightScale[i16] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i16;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local12 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num27 = (^local12).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local12).Y = (__Null) num27;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local13 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num28 = (^local13).X - (double) num2;
          // ISSUE: explicit reference operation
          (^local13).X = (__Null) num28;
          DelegateMethods.v3_1 = hslVector;
          float num29 = IngameOptions.DrawValueBar(sb, scale, (float) hslVector.Y, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_S));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i16) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i16;
            if (Main.mouseLeft && IngameOptions.rightLock == i16)
            {
              hslVector.Y = (__Null) (double) num29;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
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
          if (IngameOptions.DrawRightSide(sb, "", i17, anchor2, offset2, IngameOptions.rightScale[i17], (float) (((double) IngameOptions.rightScale[i17] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i17;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local14 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num30 = (^local14).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local14).Y = (__Null) num30;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local15 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num31 = (^local15).X - (double) num2;
          // ISSUE: explicit reference operation
          (^local15).X = (__Null) num31;
          DelegateMethods.v3_1 = hslVector;
          float num32 = IngameOptions.DrawValueBar(sb, scale, (float) hslVector.Z, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_L));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i17) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i17;
            if (Main.mouseLeft && IngameOptions.rightLock == i17)
            {
              hslVector.Z = (__Null) (double) num32;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
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
          if (IngameOptions.DrawRightSide(sb, "", i18, anchor2, offset2, IngameOptions.rightScale[i18], (float) (((double) IngameOptions.rightScale[i18] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            if (IngameOptions.rightLock == -1)
              IngameOptions.notBar = true;
            IngameOptions.rightHover = i18;
          }
          IngameOptions.valuePosition.X = (__Null) (vector2_4.X + vector2_3.X - (double) (num4 / 2) - 20.0);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local16 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num33 = (^local16).Y - 3.0;
          // ISSUE: explicit reference operation
          (^local16).Y = (__Null) num33;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local17 = @IngameOptions.valuePosition;
          // ISSUE: explicit reference operation
          double num34 = (^local17).X - (double) num2;
          // ISSUE: explicit reference operation
          (^local17).X = (__Null) num34;
          DelegateMethods.v3_1 = hslVector;
          float perc = Main.mouseBorderColorSlider.Alpha;
          float num35 = IngameOptions.DrawValueBar(sb, scale, perc, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_O));
          if ((IngameOptions.inBar || IngameOptions.rightLock == i18) && !IngameOptions.notBar)
          {
            IngameOptions.rightHover = i18;
            if (Main.mouseLeft && IngameOptions.rightLock == i18)
            {
              perc = num35;
              IngameOptions.noSound = true;
            }
          }
          if ((double) Main.mouseX > vector2_4.X + vector2_3.X * 2.0 / 3.0 + (double) num4 && (double) Main.mouseX < IngameOptions.valuePosition.X + 3.75 && ((double) Main.mouseY > IngameOptions.valuePosition.Y - 10.0 && (double) Main.mouseY <= IngameOptions.valuePosition.Y + 10.0))
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
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local18 = @anchor2;
          // ISSUE: explicit reference operation
          double num36 = (^local18).X + (double) num9;
          // ISSUE: explicit reference operation
          (^local18).X = (__Null) num36;
          IngameOptions.DrawRightSide(sb, "", i19, anchor2, offset2, IngameOptions.rightScale[i19], 1f, (Color) null);
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
          if (IngameOptions.DrawRightSide(sb, txt, i20, anchor2, offset2, IngameOptions.rightScale[i20] * 0.9f, (float) (((double) IngameOptions.rightScale[i20] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i20;
            if (flag4)
              LockOnHelper.CycleUseModes();
          }
          int i21 = i20 + 1;
          if (IngameOptions.DrawRightSide(sb, Player.SmartCursorSettings.SmartBlocksEnabled ? Lang.menu[215].Value : Lang.menu[216].Value, i21, anchor2, offset2, IngameOptions.rightScale[i21] * 0.9f, (float) (((double) IngameOptions.rightScale[i21] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i21;
            if (flag4)
              Player.SmartCursorSettings.SmartBlocksEnabled = !Player.SmartCursorSettings.SmartBlocksEnabled;
          }
          int i22 = i21 + 1;
          if (IngameOptions.DrawRightSide(sb, Main.cSmartCursorToggle ? Lang.menu[121].Value : Lang.menu[122].Value, i22, anchor2, offset2, IngameOptions.rightScale[i22], (float) (((double) IngameOptions.rightScale[i22] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i22;
            if (flag4)
              Main.cSmartCursorToggle = !Main.cSmartCursorToggle;
          }
          int i23 = i22 + 1;
          if (IngameOptions.DrawRightSide(sb, Player.SmartCursorSettings.SmartAxeAfterPickaxe ? Lang.menu[214].Value : Lang.menu[213].Value, i23, anchor2, offset2, IngameOptions.rightScale[i23] * 0.9f, (float) (((double) IngameOptions.rightScale[i23] - (double) num5) / ((double) num6 - (double) num5)), (Color) null))
          {
            IngameOptions.rightHover = i23;
            if (flag4)
              Player.SmartCursorSettings.SmartAxeAfterPickaxe = !Player.SmartCursorSettings.SmartAxeAfterPickaxe;
          }
          int num37 = i23 + 1;
        }
        if (IngameOptions.rightHover != -1 && IngameOptions.rightLock == -1)
          IngameOptions.rightLock = IngameOptions.rightHover;
        for (int index = 0; index < num8 + 1; ++index)
          UILinkPointNavigator.SetPosition(2900 + index, Vector2.op_Addition(anchor1, Vector2.op_Multiply(offset1, (float) (index + 1))));
        int num38 = 0;
        Vector2 zero = Vector2.get_Zero();
        if (flag1)
          zero.X = (__Null) -40.0;
        for (int index = 0; index < num11; ++index)
        {
          if (!IngameOptions.skipRightSlot[index])
          {
            UILinkPointNavigator.SetPosition(2930 + num38, Vector2.op_Addition(Vector2.op_Addition(anchor2, zero), Vector2.op_Multiply(offset2, (float) (index + 1))));
            ++num38;
          }
        }
        UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_RIGHT = num38;
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
      // ISSUE: explicit reference operation
      if (((Rectangle) @IngameOptions._GUIHover).Contains(Main.MouseScreen.ToPoint()))
        Main.mouseText = true;
      if (IngameOptions._mouseOverText != null)
        Main.instance.MouseText(IngameOptions._mouseOverText, 0, (byte) 0, -1, -1, -1, -1);
      IngameOptions._mouseOverText = (string) null;
    }

    public static bool DrawLeftSide(SpriteBatch sb, string txt, int i, Vector2 anchor, Vector2 offset, float[] scales, float minscale = 0.7f, float maxscale = 0.8f, float scalespeed = 0.01f)
    {
      bool flag = i == IngameOptions.category;
      Color color = Color.Lerp(Color.get_Gray(), Color.get_White(), (float) (((double) scales[i] - (double) minscale) / ((double) maxscale - (double) minscale)));
      if (flag)
        color = Color.get_Gold();
      Vector2 vector2 = Utils.DrawBorderStringBig(sb, txt, Vector2.op_Addition(anchor, Vector2.op_Multiply(offset, (float) (1 + i))), color, scales[i], 0.5f, 0.5f, -1);
      Rectangle rectangle = new Rectangle((int) anchor.X - (int) vector2.X / 2, (int) anchor.Y + (int) (offset.Y * (double) (1 + i)) - (int) vector2.Y / 2, (int) vector2.X, (int) vector2.Y);
      // ISSUE: explicit reference operation
      return ((Rectangle) @rectangle).Contains(new Point(Main.mouseX, Main.mouseY));
    }

    public static bool DrawRightSide(SpriteBatch sb, string txt, int i, Vector2 anchor, Vector2 offset, float scale, float colorScale, Color over = null)
    {
      Color color = Color.Lerp(Color.get_Gray(), Color.get_White(), colorScale);
      if (Color.op_Inequality(over, (Color) null))
        color = over;
      Vector2 vector2 = Utils.DrawBorderString(sb, txt, Vector2.op_Addition(anchor, Vector2.op_Multiply(offset, (float) (1 + i))), color, scale, 0.5f, 0.5f, -1);
      IngameOptions.valuePosition = Vector2.op_Addition(Vector2.op_Addition(anchor, Vector2.op_Multiply(offset, (float) (1 + i))), Vector2.op_Multiply(vector2, new Vector2(0.5f, 0.0f)));
      Rectangle rectangle = new Rectangle((int) anchor.X - (int) vector2.X / 2, (int) anchor.Y + (int) (offset.Y * (double) (1 + i)) - (int) vector2.Y / 2, (int) vector2.X, (int) vector2.Y);
      // ISSUE: explicit reference operation
      return ((Rectangle) @rectangle).Contains(new Point(Main.mouseX, Main.mouseY));
    }

    public static bool DrawValue(SpriteBatch sb, string txt, int i, float scale, Color over = null)
    {
      Color color = Color.get_Gray();
      Vector2 vector2 = Vector2.op_Multiply(Main.fontMouseText.MeasureString(txt), scale);
      Rectangle rectangle = new Rectangle((int) IngameOptions.valuePosition.X, (int) IngameOptions.valuePosition.Y - (int) vector2.Y / 2, (int) vector2.X, (int) vector2.Y);
      // ISSUE: explicit reference operation
      bool flag = ((Rectangle) @rectangle).Contains(new Point(Main.mouseX, Main.mouseY));
      if (flag)
        color = Color.get_White();
      if (Color.op_Inequality(over, (Color) null))
        color = over;
      Utils.DrawBorderString(sb, txt, IngameOptions.valuePosition, color, scale, 0.0f, 0.5f, -1);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local1 = @IngameOptions.valuePosition;
      // ISSUE: explicit reference operation
      // ISSUE: variable of the null type
      __Null local2 = (^local1).X + vector2.X;
      // ISSUE: explicit reference operation
      (^local1).X = local2;
      return flag;
    }

    public static float DrawValueBar(SpriteBatch sb, float scale, float perc, int lockState = 0, Utils.ColorLerpMethod colorMethod = null)
    {
      if (colorMethod == null)
        colorMethod = new Utils.ColorLerpMethod(Utils.ColorLerp_BlackToWhite);
      Texture2D colorBarTexture = Main.colorBarTexture;
      Vector2 vector2 = Vector2.op_Multiply(new Vector2((float) colorBarTexture.get_Width(), (float) colorBarTexture.get_Height()), scale);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local = @IngameOptions.valuePosition;
      // ISSUE: explicit reference operation
      double num1 = (^local).X - (double) (int) vector2.X;
      // ISSUE: explicit reference operation
      (^local).X = (__Null) num1;
      Rectangle rectangle1;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle1).\u002Ector((int) IngameOptions.valuePosition.X, (int) IngameOptions.valuePosition.Y - (int) vector2.Y / 2, (int) vector2.X, (int) vector2.Y);
      Rectangle rectangle2 = rectangle1;
      sb.Draw(colorBarTexture, rectangle1, Color.get_White());
      int num2 = 167;
      float num3 = (float) rectangle1.X + 5f * scale;
      float num4 = (float) rectangle1.Y + 4f * scale;
      for (float num5 = 0.0f; (double) num5 < (double) num2; ++num5)
      {
        float percent = num5 / (float) num2;
        sb.Draw(Main.colorBlipTexture, new Vector2(num3 + num5 * scale, num4), new Rectangle?(), colorMethod(percent), 0.0f, Vector2.get_Zero(), scale, (SpriteEffects) 0, 0.0f);
      }
      rectangle1.X = (__Null) (int) num3;
      rectangle1.Y = (__Null) (int) num4;
      // ISSUE: explicit reference operation
      bool flag = ((Rectangle) @rectangle1).Contains(new Point(Main.mouseX, Main.mouseY));
      if (lockState == 2)
        flag = false;
      if (flag || lockState == 1)
        sb.Draw(Main.colorHighlightTexture, rectangle2, Main.OurFavoriteColor);
      sb.Draw(Main.colorSliderTexture, new Vector2(num3 + 167f * scale * perc, num4 + 4f * scale), new Rectangle?(), Color.get_White(), 0.0f, new Vector2(0.5f * (float) Main.colorSliderTexture.get_Width(), 0.5f * (float) Main.colorSliderTexture.get_Height()), scale, (SpriteEffects) 0, 0.0f);
      if (Main.mouseX >= rectangle1.X && Main.mouseX <= rectangle1.X + rectangle1.Width)
      {
        IngameOptions.inBar = flag;
        return (float) (Main.mouseX - rectangle1.X) / (float) rectangle1.Width;
      }
      IngameOptions.inBar = false;
      return rectangle1.X >= Main.mouseX ? 0.0f : 1f;
    }
  }
}
