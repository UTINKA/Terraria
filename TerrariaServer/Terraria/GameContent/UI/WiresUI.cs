// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.WiresUI
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameInput;

namespace Terraria.GameContent.UI
{
  public class WiresUI
  {
    private static WiresUI.WiresRadial radial = new WiresUI.WiresRadial();

    public static bool Open
    {
      get
      {
        return WiresUI.radial.active;
      }
    }

    public static void HandleWiresUI(SpriteBatch spriteBatch)
    {
      WiresUI.radial.Update();
      WiresUI.radial.Draw(spriteBatch);
    }

    public static class Settings
    {
      public static WiresUI.Settings.MultiToolMode ToolMode = WiresUI.Settings.MultiToolMode.Red;
      private static int _lastActuatorEnabled = 0;

      public static bool DrawWires
      {
        get
        {
          if (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].mech)
            return true;
          if (Main.player[Main.myPlayer].InfoAccMechShowWires)
            return Main.player[Main.myPlayer].builderAccStatus[8] == 0;
          return false;
        }
      }

      public static bool HideWires
      {
        get
        {
          return Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type == 3620;
        }
      }

      public static bool DrawToolModeUI
      {
        get
        {
          int type = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type;
          if (type != 3611)
            return type == 3625;
          return true;
        }
      }

      public static bool DrawToolAllowActuators
      {
        get
        {
          int type = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type;
          int num1 = 3611;
          if (type == num1)
            WiresUI.Settings._lastActuatorEnabled = 2;
          int num2 = 3625;
          if (type == num2)
            WiresUI.Settings._lastActuatorEnabled = 1;
          return WiresUI.Settings._lastActuatorEnabled == 2;
        }
      }

      [Flags]
      public enum MultiToolMode
      {
        Red = 1,
        Green = 2,
        Blue = 4,
        Yellow = 8,
        Actuator = 16,
        Cutter = 32,
      }
    }

    public class WiresRadial
    {
      public Vector2 position;
      public bool active;
      public bool OnWiresMenu;
      private float _lineOpacity;

      public void Update()
      {
        this.FlowerUpdate();
        this.LineUpdate();
      }

      private void LineUpdate()
      {
        bool flag1 = true;
        float min = 0.75f;
        Player player = Main.player[Main.myPlayer];
        if (!WiresUI.Settings.DrawToolModeUI || Main.drawingPlayerChat)
        {
          flag1 = false;
          min = 0.0f;
        }
        bool flag2;
        if (player.dead || Main.mouseItem.type > 0)
        {
          flag2 = false;
          this._lineOpacity = 0.0f;
        }
        else if (player.showItemIcon && player.showItemIcon2 != 0 && player.showItemIcon2 != 3625)
        {
          flag2 = false;
          this._lineOpacity = 0.0f;
        }
        else if (!player.showItemIcon && (!PlayerInput.UsingGamepad && !WiresUI.Settings.DrawToolAllowActuators || (player.mouseInterface || player.lastMouseInterface)) || (Main.ingameOptionsWindow || Main.InGameUI.IsVisible))
        {
          flag2 = false;
          this._lineOpacity = 0.0f;
        }
        else
        {
          float num = Utils.Clamp<float>(this._lineOpacity + 0.05f * (float) flag1.ToDirectionInt(), min, 1f);
          this._lineOpacity = this._lineOpacity + 0.05f * (float) Math.Sign(num - this._lineOpacity);
          if ((double) Math.Abs(this._lineOpacity - num) >= 0.0500000007450581)
            return;
          this._lineOpacity = num;
        }
      }

      private void FlowerUpdate()
      {
        Player player = Main.player[Main.myPlayer];
        if (!WiresUI.Settings.DrawToolModeUI)
          this.active = false;
        else if ((player.mouseInterface || player.lastMouseInterface) && !this.OnWiresMenu)
          this.active = false;
        else if (player.dead || Main.mouseItem.type > 0)
        {
          this.active = false;
          this.OnWiresMenu = false;
        }
        else
        {
          this.OnWiresMenu = false;
          if (!Main.mouseRight || !Main.mouseRightRelease || (PlayerInput.LockTileUseButton || player.noThrow != 0) || (Main.HoveringOverAnNPC || player.talkNPC != -1))
            return;
          if (this.active)
          {
            this.active = false;
          }
          else
          {
            if (Main.SmartInteractShowingGenuine)
              return;
            this.active = true;
            this.position = Main.MouseScreen;
            if (!PlayerInput.UsingGamepad || !Main.SmartCursorEnabled)
              return;
            this.position = Vector2.op_Division(new Vector2((float) Main.screenWidth, (float) Main.screenHeight), 2f);
          }
        }
      }

      public void Draw(SpriteBatch spriteBatch)
      {
        this.DrawFlower(spriteBatch);
        this.DrawCursorArea(spriteBatch);
      }

      private void DrawLine(SpriteBatch spriteBatch)
      {
        if (this.active || (double) this._lineOpacity == 0.0)
          return;
        Vector2 vector2_1 = Main.MouseScreen;
        Vector2 vector2_2;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2_2).\u002Ector((float) (Main.screenWidth / 2), (float) (Main.screenHeight - 70));
        if (PlayerInput.UsingGamepad)
          vector2_1 = Vector2.get_Zero();
        Vector2 v = Vector2.op_Subtraction(vector2_1, vector2_2);
        double num1 = (double) Vector2.Dot(Vector2.Normalize(v), Vector2.get_UnitX());
        double num2 = (double) Vector2.Dot(Vector2.Normalize(v), Vector2.get_UnitY());
        double rotation = (double) v.ToRotation();
        // ISSUE: explicit reference operation
        double num3 = (double) ((Vector2) @v).Length();
        bool flag1 = false;
        bool toolAllowActuators = WiresUI.Settings.DrawToolAllowActuators;
        for (int index = 0; index < 6; ++index)
        {
          if (toolAllowActuators || index != 5)
          {
            bool flag2 = WiresUI.Settings.ToolMode.HasFlag((Enum) (WiresUI.Settings.MultiToolMode) (1 << index));
            if (index == 5)
              flag2 = WiresUI.Settings.ToolMode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Actuator);
            Vector2 center = Vector2.op_Addition(vector2_2, Vector2.op_Multiply(Vector2.get_UnitX(), (float) (45.0 * ((double) index - 1.5))));
            int num4 = index;
            if (index == 0)
              num4 = 3;
            if (index == 3)
              num4 = 0;
            switch (num4)
            {
              case 0:
              case 1:
                center = Vector2.op_Addition(vector2_2, Vector2.op_Multiply(new Vector2((float) (45.0 + (toolAllowActuators ? 15.0 : 0.0)) * (float) (2 - num4), 0.0f), this._lineOpacity));
                break;
              case 2:
              case 3:
                center = Vector2.op_Addition(vector2_2, Vector2.op_Multiply(new Vector2((float) -(45.0 + (toolAllowActuators ? 15.0 : 0.0)) * (float) (num4 - 1), 0.0f), this._lineOpacity));
                break;
              case 4:
                flag2 = false;
                center = Vector2.op_Subtraction(vector2_2, Vector2.op_Multiply(new Vector2(0.0f, toolAllowActuators ? 22f : 0.0f), this._lineOpacity));
                break;
              case 5:
                center = Vector2.op_Addition(vector2_2, Vector2.op_Multiply(new Vector2(0.0f, 22f), this._lineOpacity));
                break;
            }
            bool flag3 = false;
            if (!PlayerInput.UsingGamepad)
              flag3 = (double) Vector2.Distance(center, vector2_1) < 19.0 * (double) this._lineOpacity;
            if (flag1)
              flag3 = false;
            if (flag3)
              flag1 = true;
            Texture2D tex1 = Main.wireUITexture[(WiresUI.Settings.ToolMode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Cutter) ? 8 : 0) + (flag3 ? 1 : 0)];
            Texture2D tex2 = (Texture2D) null;
            switch (index)
            {
              case 0:
              case 1:
              case 2:
              case 3:
                tex2 = Main.wireUITexture[2 + index];
                break;
              case 4:
                tex2 = Main.wireUITexture[WiresUI.Settings.ToolMode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Cutter) ? 7 : 6];
                break;
              case 5:
                tex2 = Main.wireUITexture[10];
                break;
            }
            Color white1 = Color.get_White();
            Color white2 = Color.get_White();
            if (!flag2 && index != 4)
            {
              if (flag3)
              {
                // ISSUE: explicit reference operation
                ((Color) @white2).\u002Ector(100, 100, 100);
                // ISSUE: explicit reference operation
                ((Color) @white2).\u002Ector(120, 120, 120);
                // ISSUE: explicit reference operation
                ((Color) @white1).\u002Ector(200, 200, 200);
              }
              else
              {
                // ISSUE: explicit reference operation
                ((Color) @white2).\u002Ector(150, 150, 150);
                // ISSUE: explicit reference operation
                ((Color) @white2).\u002Ector(80, 80, 80);
                // ISSUE: explicit reference operation
                ((Color) @white1).\u002Ector(100, 100, 100);
              }
            }
            Utils.CenteredRectangle(center, new Vector2(40f));
            if (flag3)
            {
              if (Main.mouseLeft && Main.mouseLeftRelease)
              {
                switch (index)
                {
                  case 0:
                    WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Red;
                    break;
                  case 1:
                    WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Green;
                    break;
                  case 2:
                    WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Blue;
                    break;
                  case 3:
                    WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Yellow;
                    break;
                  case 4:
                    WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Cutter;
                    break;
                  case 5:
                    WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Actuator;
                    break;
                }
              }
              if (!Main.mouseLeft || Main.player[Main.myPlayer].mouseInterface)
                Main.player[Main.myPlayer].mouseInterface = true;
              this.OnWiresMenu = true;
            }
            int num5 = flag3 ? 1 : 0;
            spriteBatch.Draw(tex1, center, new Rectangle?(), Color.op_Multiply(white1, this._lineOpacity), 0.0f, Vector2.op_Division(tex1.Size(), 2f), this._lineOpacity, (SpriteEffects) 0, 0.0f);
            spriteBatch.Draw(tex2, center, new Rectangle?(), Color.op_Multiply(white2, this._lineOpacity), 0.0f, Vector2.op_Division(tex2.Size(), 2f), this._lineOpacity, (SpriteEffects) 0, 0.0f);
          }
        }
        if (!Main.mouseLeft || !Main.mouseLeftRelease || flag1)
          return;
        this.active = false;
      }

      private void DrawFlower(SpriteBatch spriteBatch)
      {
        if (!this.active)
          return;
        Vector2 vector2 = Main.MouseScreen;
        Vector2 position = this.position;
        if (PlayerInput.UsingGamepad && Main.SmartCursorEnabled)
          vector2 = !Vector2.op_Inequality(PlayerInput.GamepadThumbstickRight, Vector2.get_Zero()) ? (!Vector2.op_Inequality(PlayerInput.GamepadThumbstickLeft, Vector2.get_Zero()) ? this.position : Vector2.op_Addition(this.position, Vector2.op_Multiply(PlayerInput.GamepadThumbstickLeft, 40f))) : Vector2.op_Addition(this.position, Vector2.op_Multiply(PlayerInput.GamepadThumbstickRight, 40f));
        Vector2 v = Vector2.op_Subtraction(vector2, position);
        double num1 = (double) Vector2.Dot(Vector2.Normalize(v), Vector2.get_UnitX());
        double num2 = (double) Vector2.Dot(Vector2.Normalize(v), Vector2.get_UnitY());
        float rotation = v.ToRotation();
        // ISSUE: explicit reference operation
        float num3 = ((Vector2) @v).Length();
        bool flag1 = false;
        bool toolAllowActuators = WiresUI.Settings.DrawToolAllowActuators;
        float num4 = (float) (4 + toolAllowActuators.ToInt());
        float num5 = toolAllowActuators ? 11f : -0.5f;
        for (int index = 0; index < 6; ++index)
        {
          if (toolAllowActuators || index != 5)
          {
            bool flag2 = WiresUI.Settings.ToolMode.HasFlag((Enum) (WiresUI.Settings.MultiToolMode) (1 << index));
            if (index == 5)
              flag2 = WiresUI.Settings.ToolMode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Actuator);
            Vector2 center = Vector2.op_Addition(position, Vector2.op_Multiply(Vector2.get_UnitX(), (float) (45.0 * ((double) index - 1.5))));
            switch (index)
            {
              case 0:
              case 1:
              case 2:
              case 3:
                float num6 = (float) index;
                if (index == 0)
                  num6 = 3f;
                if (index == 3)
                  num6 = 0.0f;
                center = Vector2.op_Addition(position, Vector2.op_Multiply(Vector2.get_UnitX().RotatedBy((double) num6 * 6.28318548202515 / (double) num4 - 3.14159274101257 / (double) num5, (Vector2) null), 45f));
                break;
              case 4:
                flag2 = false;
                center = position;
                break;
              case 5:
                center = Vector2.op_Addition(position, Vector2.op_Multiply(Vector2.get_UnitX().RotatedBy((double) (index - 1) * 6.28318548202515 / (double) num4 - 3.14159274101257 / (double) num5, (Vector2) null), 45f));
                break;
            }
            bool flag3 = false;
            if (index == 4)
              flag3 = (double) num3 < 20.0;
            switch (index)
            {
              case 0:
              case 1:
              case 2:
              case 3:
              case 5:
                float num7 = Vector2.op_Subtraction(center, position).ToRotation().AngleTowards(rotation, (float) (6.28318548202515 / ((double) num4 * 2.0))) - rotation;
                if ((double) num3 >= 20.0 && (double) Math.Abs(num7) < 0.00999999977648258)
                {
                  flag3 = true;
                  break;
                }
                break;
              case 4:
                flag3 = (double) num3 < 20.0;
                break;
            }
            if (!PlayerInput.UsingGamepad)
              flag3 = (double) Vector2.Distance(center, vector2) < 19.0;
            if (flag1)
              flag3 = false;
            if (flag3)
              flag1 = true;
            Texture2D tex1 = Main.wireUITexture[(WiresUI.Settings.ToolMode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Cutter) ? 8 : 0) + (flag3 ? 1 : 0)];
            Texture2D tex2 = (Texture2D) null;
            switch (index)
            {
              case 0:
              case 1:
              case 2:
              case 3:
                tex2 = Main.wireUITexture[2 + index];
                break;
              case 4:
                tex2 = Main.wireUITexture[WiresUI.Settings.ToolMode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Cutter) ? 7 : 6];
                break;
              case 5:
                tex2 = Main.wireUITexture[10];
                break;
            }
            Color white1 = Color.get_White();
            Color white2 = Color.get_White();
            if (!flag2 && index != 4)
            {
              if (flag3)
              {
                // ISSUE: explicit reference operation
                ((Color) @white2).\u002Ector(100, 100, 100);
                // ISSUE: explicit reference operation
                ((Color) @white2).\u002Ector(120, 120, 120);
                // ISSUE: explicit reference operation
                ((Color) @white1).\u002Ector(200, 200, 200);
              }
              else
              {
                // ISSUE: explicit reference operation
                ((Color) @white2).\u002Ector(150, 150, 150);
                // ISSUE: explicit reference operation
                ((Color) @white2).\u002Ector(80, 80, 80);
                // ISSUE: explicit reference operation
                ((Color) @white1).\u002Ector(100, 100, 100);
              }
            }
            Utils.CenteredRectangle(center, new Vector2(40f));
            if (flag3)
            {
              if (Main.mouseLeft && Main.mouseLeftRelease)
              {
                switch (index)
                {
                  case 0:
                    WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Red;
                    break;
                  case 1:
                    WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Green;
                    break;
                  case 2:
                    WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Blue;
                    break;
                  case 3:
                    WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Yellow;
                    break;
                  case 4:
                    WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Cutter;
                    break;
                  case 5:
                    WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Actuator;
                    break;
                }
              }
              Main.player[Main.myPlayer].mouseInterface = true;
              this.OnWiresMenu = true;
            }
            int num8 = flag3 ? 1 : 0;
            spriteBatch.Draw(tex1, center, new Rectangle?(), white1, 0.0f, Vector2.op_Division(tex1.Size(), 2f), 1f, (SpriteEffects) 0, 0.0f);
            spriteBatch.Draw(tex2, center, new Rectangle?(), white2, 0.0f, Vector2.op_Division(tex2.Size(), 2f), 1f, (SpriteEffects) 0, 0.0f);
          }
        }
        if (!Main.mouseLeft || !Main.mouseLeftRelease || flag1)
          return;
        this.active = false;
      }

      private void DrawCursorArea(SpriteBatch spriteBatch)
      {
        if (this.active || (double) this._lineOpacity == 0.0)
          return;
        Vector2 vector2_1 = Vector2.op_Addition(Main.MouseScreen, new Vector2((float) (10 - 9 * PlayerInput.UsingGamepad.ToInt()), 25f));
        Color color1;
        // ISSUE: explicit reference operation
        ((Color) @color1).\u002Ector(50, 50, 50);
        bool toolAllowActuators = WiresUI.Settings.DrawToolAllowActuators;
        if (!toolAllowActuators)
          vector2_1 = PlayerInput.UsingGamepad ? Vector2.op_Addition(vector2_1, new Vector2(0.0f, 10f)) : Vector2.op_Addition(vector2_1, new Vector2(-20f, 10f));
        Texture2D builderAccTexture = Main.builderAccTexture;
        Texture2D texture2D = builderAccTexture;
        Rectangle r1;
        // ISSUE: explicit reference operation
        ((Rectangle) @r1).\u002Ector(140, 2, 6, 6);
        Rectangle r2;
        // ISSUE: explicit reference operation
        ((Rectangle) @r2).\u002Ector(148, 2, 6, 6);
        Rectangle r3;
        // ISSUE: explicit reference operation
        ((Rectangle) @r3).\u002Ector(128, 0, 10, 10);
        float num1 = 1f;
        float num2 = 1f;
        bool flag1 = false;
        if (flag1 && !toolAllowActuators)
          num1 *= Main.cursorScale;
        float lineOpacity = this._lineOpacity;
        if (PlayerInput.UsingGamepad)
          lineOpacity *= Main.GamepadCursorAlpha;
        for (int index = 0; index < 5; ++index)
        {
          if (toolAllowActuators || index != 4)
          {
            float num3 = lineOpacity;
            Vector2 vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.get_UnitX(), (float) (45.0 * ((double) index - 1.5))));
            int num4 = index;
            if (index == 0)
              num4 = 3;
            if (index == 1)
              num4 = 2;
            if (index == 2)
              num4 = 1;
            if (index == 3)
              num4 = 0;
            if (index == 4)
              num4 = 5;
            int num5 = num4;
            switch (num5)
            {
              case 2:
                num5 = 1;
                break;
              case 1:
                num5 = 2;
                break;
            }
            bool flag2 = WiresUI.Settings.ToolMode.HasFlag((Enum) (WiresUI.Settings.MultiToolMode) (1 << num5));
            if (num5 == 5)
              flag2 = WiresUI.Settings.ToolMode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Actuator);
            Color color2 = Color.get_HotPink();
            switch (num4)
            {
              case 0:
                // ISSUE: explicit reference operation
                ((Color) @color2).\u002Ector(253, 58, 61);
                break;
              case 1:
                // ISSUE: explicit reference operation
                ((Color) @color2).\u002Ector(83, 180, 253);
                break;
              case 2:
                // ISSUE: explicit reference operation
                ((Color) @color2).\u002Ector(83, 253, 153);
                break;
              case 3:
                // ISSUE: explicit reference operation
                ((Color) @color2).\u002Ector(253, 254, 83);
                break;
              case 5:
                color2 = Color.get_WhiteSmoke();
                break;
            }
            if (!flag2)
              color2 = Color.Lerp(color2, Color.get_Black(), 0.65f);
            if (flag1)
            {
              if (toolAllowActuators)
              {
                switch (num4)
                {
                  case 0:
                    vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(new Vector2(-12f, 0.0f), num1));
                    break;
                  case 1:
                    vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(new Vector2(-6f, 12f), num1));
                    break;
                  case 2:
                    vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(new Vector2(6f, 12f), num1));
                    break;
                  case 3:
                    vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(new Vector2(12f, 0.0f), num1));
                    break;
                  case 5:
                    vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(new Vector2(0.0f, 0.0f), num1));
                    break;
                }
              }
              else
                vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(new Vector2((float) (12 * (num4 + 1)), (float) (12 * (3 - num4))), num1));
            }
            else if (toolAllowActuators)
            {
              switch (num4)
              {
                case 0:
                  vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(new Vector2(-12f, 0.0f), num1));
                  break;
                case 1:
                  vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(new Vector2(-6f, 12f), num1));
                  break;
                case 2:
                  vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(new Vector2(6f, 12f), num1));
                  break;
                case 3:
                  vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(new Vector2(12f, 0.0f), num1));
                  break;
                case 5:
                  vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(new Vector2(0.0f, 0.0f), num1));
                  break;
              }
            }
            else
            {
              float num6 = 0.7f;
              switch (num4)
              {
                case 0:
                  vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.op_Multiply(new Vector2(0.0f, -12f), num1), num6));
                  break;
                case 1:
                  vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.op_Multiply(new Vector2(-12f, 0.0f), num1), num6));
                  break;
                case 2:
                  vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.op_Multiply(new Vector2(0.0f, 12f), num1), num6));
                  break;
                case 3:
                  vec = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.op_Multiply(new Vector2(12f, 0.0f), num1), num6));
                  break;
              }
            }
            Vector2 vector2_2 = vec.Floor();
            spriteBatch.Draw(texture2D, vector2_2, new Rectangle?(r3), Color.op_Multiply(color1, num3), 0.0f, Vector2.op_Division(r3.Size(), 2f), num2, (SpriteEffects) 0, 0.0f);
            spriteBatch.Draw(builderAccTexture, vector2_2, new Rectangle?(r1), Color.op_Multiply(color2, num3), 0.0f, Vector2.op_Division(r1.Size(), 2f), num2, (SpriteEffects) 0, 0.0f);
            if (WiresUI.Settings.ToolMode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Cutter))
              spriteBatch.Draw(builderAccTexture, vector2_2, new Rectangle?(r2), Color.op_Multiply(color1, num3), 0.0f, Vector2.op_Division(r2.Size(), 2f), num2, (SpriteEffects) 0, 0.0f);
          }
        }
      }
    }
  }
}
