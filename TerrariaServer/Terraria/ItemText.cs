// Decompiled with JetBrains decompiler
// Type: Terraria.ItemText
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria
{
  public class ItemText
  {
    public static int activeTime = 60;
    public int alphaDir = 1;
    public float scale = 1f;
    public Vector2 position;
    public Vector2 velocity;
    public float alpha;
    public string name;
    public int stack;
    public float rotation;
    public Color color;
    public bool active;
    public int lifeTime;
    public static int numActive;
    public bool NoStack;
    public bool coinText;
    public int coinValue;
    public bool expert;

    public static float TargetScale
    {
      get
      {
        return Main.UIScale / (float) Main.GameViewMatrix.Zoom.X;
      }
    }

    public static void NewText(Item newItem, int stack, bool noStack = false, bool longText = false)
    {
      bool flag = newItem.type >= 71 && newItem.type <= 74;
      if (!Main.showItemText || newItem.Name == null || (!newItem.active || Main.netMode == 2))
        return;
      for (int index = 0; index < 20; ++index)
      {
        if (Main.itemText[index].active && (Main.itemText[index].name == newItem.AffixName() || flag && Main.itemText[index].coinText) && (!Main.itemText[index].NoStack && !noStack))
        {
          string str1 = newItem.Name + " (" + (object) (Main.itemText[index].stack + stack) + ")";
          string str2 = newItem.Name;
          if (Main.itemText[index].stack > 1)
            str2 = str2 + " (" + (object) Main.itemText[index].stack + ")";
          Main.fontMouseText.MeasureString(str2);
          Vector2 vector2 = Main.fontMouseText.MeasureString(str1);
          if (Main.itemText[index].lifeTime < 0)
            Main.itemText[index].scale = 1f;
          if (Main.itemText[index].lifeTime < 60)
            Main.itemText[index].lifeTime = 60;
          if (flag && Main.itemText[index].coinText)
          {
            int num = 0;
            if (newItem.type == 71)
              num += newItem.stack;
            else if (newItem.type == 72)
              num += 100 * newItem.stack;
            else if (newItem.type == 73)
              num += 10000 * newItem.stack;
            else if (newItem.type == 74)
              num += 1000000 * newItem.stack;
            Main.itemText[index].coinValue += num;
            string name = ItemText.ValueToName(Main.itemText[index].coinValue);
            vector2 = Main.fontMouseText.MeasureString(name);
            Main.itemText[index].name = name;
            if (Main.itemText[index].coinValue >= 1000000)
            {
              if (Main.itemText[index].lifeTime < 300)
                Main.itemText[index].lifeTime = 300;
              Main.itemText[index].color = new Color(220, 220, 198);
            }
            else if (Main.itemText[index].coinValue >= 10000)
            {
              if (Main.itemText[index].lifeTime < 240)
                Main.itemText[index].lifeTime = 240;
              Main.itemText[index].color = new Color(224, 201, 92);
            }
            else if (Main.itemText[index].coinValue >= 100)
            {
              if (Main.itemText[index].lifeTime < 180)
                Main.itemText[index].lifeTime = 180;
              Main.itemText[index].color = new Color(181, 192, 193);
            }
            else if (Main.itemText[index].coinValue >= 1)
            {
              if (Main.itemText[index].lifeTime < 120)
                Main.itemText[index].lifeTime = 120;
              Main.itemText[index].color = new Color(246, 138, 96);
            }
          }
          Main.itemText[index].stack += stack;
          Main.itemText[index].scale = 0.0f;
          Main.itemText[index].rotation = 0.0f;
          Main.itemText[index].position.X = (__Null) (newItem.position.X + (double) newItem.width * 0.5 - vector2.X * 0.5);
          Main.itemText[index].position.Y = (__Null) (newItem.position.Y + (double) newItem.height * 0.25 - vector2.Y * 0.5);
          Main.itemText[index].velocity.Y = (__Null) -7.0;
          if (!Main.itemText[index].coinText)
            return;
          Main.itemText[index].stack = 1;
          return;
        }
      }
      int index1 = -1;
      for (int index2 = 0; index2 < 20; ++index2)
      {
        if (!Main.itemText[index2].active)
        {
          index1 = index2;
          break;
        }
      }
      if (index1 == -1)
      {
        double num = (double) Main.bottomWorld;
        for (int index2 = 0; index2 < 20; ++index2)
        {
          if (num > (double) Main.itemText[index2].position.Y)
          {
            index1 = index2;
            num = (double) Main.itemText[index2].position.Y;
          }
        }
      }
      if (index1 < 0)
        return;
      string str = newItem.AffixName();
      if (stack > 1)
        str = str + " (" + (object) stack + ")";
      Vector2 vector2_1 = Main.fontMouseText.MeasureString(str);
      Main.itemText[index1].alpha = 1f;
      Main.itemText[index1].alphaDir = -1;
      Main.itemText[index1].active = true;
      Main.itemText[index1].scale = 0.0f;
      Main.itemText[index1].NoStack = noStack;
      Main.itemText[index1].rotation = 0.0f;
      Main.itemText[index1].position.X = (__Null) (newItem.position.X + (double) newItem.width * 0.5 - vector2_1.X * 0.5);
      Main.itemText[index1].position.Y = (__Null) (newItem.position.Y + (double) newItem.height * 0.25 - vector2_1.Y * 0.5);
      Main.itemText[index1].color = Color.get_White();
      if (newItem.rare == 1)
        Main.itemText[index1].color = new Color(150, 150, (int) byte.MaxValue);
      else if (newItem.rare == 2)
        Main.itemText[index1].color = new Color(150, (int) byte.MaxValue, 150);
      else if (newItem.rare == 3)
        Main.itemText[index1].color = new Color((int) byte.MaxValue, 200, 150);
      else if (newItem.rare == 4)
        Main.itemText[index1].color = new Color((int) byte.MaxValue, 150, 150);
      else if (newItem.rare == 5)
        Main.itemText[index1].color = new Color((int) byte.MaxValue, 150, (int) byte.MaxValue);
      else if (newItem.rare == -11)
        Main.itemText[index1].color = new Color((int) byte.MaxValue, 175, 0);
      else if (newItem.rare == -1)
        Main.itemText[index1].color = new Color(130, 130, 130);
      else if (newItem.rare == 6)
        Main.itemText[index1].color = new Color(210, 160, (int) byte.MaxValue);
      else if (newItem.rare == 7)
        Main.itemText[index1].color = new Color(150, (int) byte.MaxValue, 10);
      else if (newItem.rare == 8)
        Main.itemText[index1].color = new Color((int) byte.MaxValue, (int) byte.MaxValue, 10);
      else if (newItem.rare == 9)
        Main.itemText[index1].color = new Color(5, 200, (int) byte.MaxValue);
      else if (newItem.rare == 10)
        Main.itemText[index1].color = new Color((int) byte.MaxValue, 40, 100);
      else if (newItem.rare >= 11)
        Main.itemText[index1].color = new Color(180, 40, (int) byte.MaxValue);
      Main.itemText[index1].expert = newItem.expert;
      Main.itemText[index1].name = newItem.AffixName();
      Main.itemText[index1].stack = stack;
      Main.itemText[index1].velocity.Y = (__Null) -7.0;
      Main.itemText[index1].lifeTime = 60;
      if (longText)
        Main.itemText[index1].lifeTime *= 5;
      Main.itemText[index1].coinValue = 0;
      Main.itemText[index1].coinText = newItem.type >= 71 && newItem.type <= 74;
      if (!Main.itemText[index1].coinText)
        return;
      if (newItem.type == 71)
        Main.itemText[index1].coinValue += Main.itemText[index1].stack;
      else if (newItem.type == 72)
        Main.itemText[index1].coinValue += 100 * Main.itemText[index1].stack;
      else if (newItem.type == 73)
        Main.itemText[index1].coinValue += 10000 * Main.itemText[index1].stack;
      else if (newItem.type == 74)
        Main.itemText[index1].coinValue += 1000000 * Main.itemText[index1].stack;
      Main.itemText[index1].ValueToName();
      Main.itemText[index1].stack = 1;
      int index3 = index1;
      if (Main.itemText[index3].coinValue >= 1000000)
      {
        if (Main.itemText[index3].lifeTime < 300)
          Main.itemText[index3].lifeTime = 300;
        Main.itemText[index3].color = new Color(220, 220, 198);
      }
      else if (Main.itemText[index3].coinValue >= 10000)
      {
        if (Main.itemText[index3].lifeTime < 240)
          Main.itemText[index3].lifeTime = 240;
        Main.itemText[index3].color = new Color(224, 201, 92);
      }
      else if (Main.itemText[index3].coinValue >= 100)
      {
        if (Main.itemText[index3].lifeTime < 180)
          Main.itemText[index3].lifeTime = 180;
        Main.itemText[index3].color = new Color(181, 192, 193);
      }
      else
      {
        if (Main.itemText[index3].coinValue < 1)
          return;
        if (Main.itemText[index3].lifeTime < 120)
          Main.itemText[index3].lifeTime = 120;
        Main.itemText[index3].color = new Color(246, 138, 96);
      }
    }

    private static string ValueToName(int coinValue)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = coinValue;
      while (num5 > 0)
      {
        if (num5 >= 1000000)
        {
          num5 -= 1000000;
          ++num1;
        }
        else if (num5 >= 10000)
        {
          num5 -= 10000;
          ++num2;
        }
        else if (num5 >= 100)
        {
          num5 -= 100;
          ++num3;
        }
        else if (num5 >= 1)
        {
          --num5;
          ++num4;
        }
      }
      string str = "";
      if (num1 > 0)
        str = str + (object) num1 + string.Format(" {0} ", (object) Language.GetTextValue("Currency.Platinum"));
      if (num2 > 0)
        str = str + (object) num2 + string.Format(" {0} ", (object) Language.GetTextValue("Currency.Gold"));
      if (num3 > 0)
        str = str + (object) num3 + string.Format(" {0} ", (object) Language.GetTextValue("Currency.Silver"));
      if (num4 > 0)
        str = str + (object) num4 + string.Format(" {0} ", (object) Language.GetTextValue("Currency.Copper"));
      if (str.Length > 1)
        str = str.Substring(0, str.Length - 1);
      return str;
    }

    private void ValueToName()
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int coinValue = this.coinValue;
      while (coinValue > 0)
      {
        if (coinValue >= 1000000)
        {
          coinValue -= 1000000;
          ++num1;
        }
        else if (coinValue >= 10000)
        {
          coinValue -= 10000;
          ++num2;
        }
        else if (coinValue >= 100)
        {
          coinValue -= 100;
          ++num3;
        }
        else if (coinValue >= 1)
        {
          --coinValue;
          ++num4;
        }
      }
      this.name = "";
      if (num1 > 0)
        this.name = this.name + (object) num1 + string.Format(" {0} ", (object) Language.GetTextValue("Currency.Platinum"));
      if (num2 > 0)
        this.name = this.name + (object) num2 + string.Format(" {0} ", (object) Language.GetTextValue("Currency.Gold"));
      if (num3 > 0)
        this.name = this.name + (object) num3 + string.Format(" {0} ", (object) Language.GetTextValue("Currency.Silver"));
      if (num4 > 0)
        this.name = this.name + (object) num4 + string.Format(" {0} ", (object) Language.GetTextValue("Currency.Copper"));
      if (this.name.Length <= 1)
        return;
      this.name = this.name.Substring(0, this.name.Length - 1);
    }

    public void Update(int whoAmI)
    {
      if (!this.active)
        return;
      float targetScale = ItemText.TargetScale;
      this.alpha = this.alpha + (float) this.alphaDir * 0.01f;
      if ((double) this.alpha <= 0.7)
      {
        this.alpha = 0.7f;
        this.alphaDir = 1;
      }
      if ((double) this.alpha >= 1.0)
      {
        this.alpha = 1f;
        this.alphaDir = -1;
      }
      if (this.expert && this.expert)
        this.color = new Color((int) (byte) Main.DiscoR, (int) (byte) Main.DiscoG, (int) (byte) Main.DiscoB, (int) Main.mouseTextColor);
      bool flag = false;
      string str1 = this.name;
      if (this.stack > 1)
        str1 = str1 + " (" + (object) this.stack + ")";
      Vector2 vector2_1 = Vector2.op_Multiply(Main.fontMouseText.MeasureString(str1), this.scale);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local1 = @vector2_1.Y;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num1 = (double) ^(float&) local1 * 0.800000011920929;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local1 = (float) num1;
      Rectangle rectangle1;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle1).\u002Ector((int) (this.position.X - vector2_1.X / 2.0), (int) (this.position.Y - vector2_1.Y / 2.0), (int) vector2_1.X, (int) vector2_1.Y);
      for (int index = 0; index < 20; ++index)
      {
        if (Main.itemText[index].active && index != whoAmI)
        {
          string str2 = Main.itemText[index].name;
          if (Main.itemText[index].stack > 1)
            str2 = str2 + " (" + (object) Main.itemText[index].stack + ")";
          Vector2 vector2_2 = Vector2.op_Multiply(Main.fontMouseText.MeasureString(str2), Main.itemText[index].scale);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __Null& local2 = @vector2_2.Y;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          double num2 = (double) ^(float&) local2 * 0.800000011920929;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) local2 = (float) num2;
          Rectangle rectangle2;
          // ISSUE: explicit reference operation
          ((Rectangle) @rectangle2).\u002Ector((int) (Main.itemText[index].position.X - vector2_2.X / 2.0), (int) (Main.itemText[index].position.Y - vector2_2.Y / 2.0), (int) vector2_2.X, (int) vector2_2.Y);
          // ISSUE: explicit reference operation
          if (((Rectangle) @rectangle1).Intersects(rectangle2) && (this.position.Y < Main.itemText[index].position.Y || this.position.Y == Main.itemText[index].position.Y && whoAmI < index))
          {
            flag = true;
            int num3 = ItemText.numActive;
            if (num3 > 3)
              num3 = 3;
            Main.itemText[index].lifeTime = ItemText.activeTime + 15 * num3;
            this.lifeTime = ItemText.activeTime + 15 * num3;
          }
        }
      }
      if (!flag)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local2 = @this.velocity.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num2 = (double) ^(float&) local2 * 0.860000014305115;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local2 = (float) num2;
        if ((double) this.scale == (double) targetScale)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __Null& local3 = @this.velocity.Y;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          double num3 = (double) ^(float&) local3 * 0.400000005960464;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) local3 = (float) num3;
        }
      }
      else if (this.velocity.Y > -6.0)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local2 = @this.velocity.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num2 = (double) ^(float&) local2 - 0.200000002980232;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local2 = (float) num2;
      }
      else
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local2 = @this.velocity.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num2 = (double) ^(float&) local2 * 0.860000014305115;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local2 = (float) num2;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local4 = @this.velocity.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num4 = (double) ^(float&) local4 * 0.930000007152557;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local4 = (float) num4;
      this.position = Vector2.op_Addition(this.position, this.velocity);
      this.lifeTime = this.lifeTime - 1;
      if (this.lifeTime <= 0)
      {
        this.scale = this.scale - 0.03f * targetScale;
        if ((double) this.scale < 0.1 * (double) targetScale)
          this.active = false;
        this.lifeTime = 0;
      }
      else
      {
        if ((double) this.scale < (double) targetScale)
          this.scale = this.scale + 0.1f * targetScale;
        if ((double) this.scale <= (double) targetScale)
          return;
        this.scale = targetScale;
      }
    }

    public static void UpdateItemText()
    {
      int num = 0;
      for (int whoAmI = 0; whoAmI < 20; ++whoAmI)
      {
        if (Main.itemText[whoAmI].active)
        {
          ++num;
          Main.itemText[whoAmI].Update(whoAmI);
        }
      }
      ItemText.numActive = num;
    }
  }
}
