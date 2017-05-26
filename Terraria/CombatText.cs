// Decompiled with JetBrains decompiler
// Type: Terraria.CombatText
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria
{
  public class CombatText
  {
    public static readonly Color DamagedFriendly = new Color((int) byte.MaxValue, 80, 90, (int) byte.MaxValue);
    public static readonly Color DamagedFriendlyCrit = new Color((int) byte.MaxValue, 100, 30, (int) byte.MaxValue);
    public static readonly Color DamagedHostile = new Color((int) byte.MaxValue, 160, 80, (int) byte.MaxValue);
    public static readonly Color DamagedHostileCrit = new Color((int) byte.MaxValue, 100, 30, (int) byte.MaxValue);
    public static readonly Color OthersDamagedHostile = Color.op_Multiply(CombatText.DamagedHostile, 0.4f);
    public static readonly Color OthersDamagedHostileCrit = Color.op_Multiply(CombatText.DamagedHostileCrit, 0.4f);
    public static readonly Color HealLife = new Color(100, (int) byte.MaxValue, 100, (int) byte.MaxValue);
    public static readonly Color HealMana = new Color(100, 100, (int) byte.MaxValue, (int) byte.MaxValue);
    public static readonly Color LifeRegen = new Color((int) byte.MaxValue, 60, 70, (int) byte.MaxValue);
    public static readonly Color LifeRegenNegative = new Color((int) byte.MaxValue, 140, 40, (int) byte.MaxValue);
    public int alphaDir = 1;
    public float scale = 1f;
    public Vector2 position;
    public Vector2 velocity;
    public float alpha;
    public string text;
    public float rotation;
    public Color color;
    public bool active;
    public int lifeTime;
    public bool crit;
    public bool dot;

    public static float TargetScale
    {
      get
      {
        return Main.UIScale / ((float) Main.GameViewMatrix.Zoom.X / Main.ForcedMinimumZoom);
      }
    }

    public static int NewText(Rectangle location, Color color, int amount, bool dramatic = false, bool dot = false)
    {
      return CombatText.NewText(location, color, amount.ToString(), dramatic, dot);
    }

    public static int NewText(Rectangle location, Color color, string text, bool dramatic = false, bool dot = false)
    {
      if (Main.netMode == 2)
        return 100;
      for (int index1 = 0; index1 < 100; ++index1)
      {
        if (!Main.combatText[index1].active)
        {
          int index2 = 0;
          if (dramatic)
            index2 = 1;
          Vector2 vector2 = Main.fontCombatText[index2].MeasureString(text);
          Main.combatText[index1].alpha = 1f;
          Main.combatText[index1].alphaDir = -1;
          Main.combatText[index1].active = true;
          Main.combatText[index1].scale = 0.0f;
          Main.combatText[index1].rotation = 0.0f;
          Main.combatText[index1].position.X = (__Null) ((double) (float) location.X + (double) (float) location.Width * 0.5 - vector2.X * 0.5);
          Main.combatText[index1].position.Y = (__Null) ((double) (float) location.Y + (double) (float) location.Height * 0.25 - vector2.Y * 0.5);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __Null& local1 = @Main.combatText[index1].position.X;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          double num1 = (double) ^(float&) local1 + (double) Main.rand.Next(-(int) ((double) location.Width * 0.5), (int) ((double) location.Width * 0.5) + 1);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) local1 = (float) num1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __Null& local2 = @Main.combatText[index1].position.Y;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          double num2 = (double) ^(float&) local2 + (double) Main.rand.Next(-(int) ((double) location.Height * 0.5), (int) ((double) location.Height * 0.5) + 1);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) local2 = (float) num2;
          Main.combatText[index1].color = color;
          Main.combatText[index1].text = text;
          Main.combatText[index1].velocity.Y = (__Null) -7.0;
          if ((double) Main.player[Main.myPlayer].gravDir == -1.0)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local3 = @Main.combatText[index1].velocity.Y;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num3 = (double) ^(float&) local3 * -1.0;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local3 = (float) num3;
            Main.combatText[index1].position.Y = (__Null) ((double) (float) location.Y + (double) (float) location.Height * 0.75 + vector2.Y * 0.5);
          }
          Main.combatText[index1].lifeTime = 60;
          Main.combatText[index1].crit = dramatic;
          Main.combatText[index1].dot = dot;
          if (dramatic)
          {
            Main.combatText[index1].text = text;
            Main.combatText[index1].lifeTime *= 2;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local3 = @Main.combatText[index1].velocity.Y;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num3 = (double) ^(float&) local3 * 2.0;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local3 = (float) num3;
            Main.combatText[index1].velocity.X = (__Null) ((double) Main.rand.Next(-25, 26) * 0.0500000007450581);
            Main.combatText[index1].rotation = (float) (Main.combatText[index1].lifeTime / 2) * (1f / 500f);
            if (Main.combatText[index1].velocity.X < 0.0)
              Main.combatText[index1].rotation *= -1f;
          }
          if (dot)
          {
            Main.combatText[index1].velocity.Y = (__Null) -4.0;
            Main.combatText[index1].lifeTime = 40;
          }
          return index1;
        }
      }
      return 100;
    }

    public static void clearAll()
    {
      for (int index = 0; index < 100; ++index)
        Main.combatText[index].active = false;
    }

    public void Update()
    {
      if (!this.active)
        return;
      float targetScale = CombatText.TargetScale;
      this.alpha = this.alpha + (float) this.alphaDir * 0.05f;
      if ((double) this.alpha <= 0.6)
        this.alphaDir = 1;
      if ((double) this.alpha >= 1.0)
      {
        this.alpha = 1f;
        this.alphaDir = -1;
      }
      if (this.dot)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local = @this.velocity.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num = (double) ^(float&) local + 0.150000005960464;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local = (float) num;
      }
      else
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local1 = @this.velocity.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num1 = (double) ^(float&) local1 * 0.920000016689301;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local1 = (float) num1;
        if (this.crit)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __Null& local2 = @this.velocity.Y;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          double num2 = (double) ^(float&) local2 * 0.920000016689301;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) local2 = (float) num2;
        }
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local3 = @this.velocity.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num3 = (double) ^(float&) local3 * 0.930000007152557;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local3 = (float) num3;
      this.position = Vector2.op_Addition(this.position, this.velocity);
      this.lifeTime = this.lifeTime - 1;
      if (this.lifeTime <= 0)
      {
        this.scale = this.scale - 0.1f * targetScale;
        if ((double) this.scale < 0.1)
          this.active = false;
        this.lifeTime = 0;
        if (!this.crit)
          return;
        this.alphaDir = -1;
        this.scale = this.scale + 0.07f * targetScale;
      }
      else
      {
        if (this.crit)
          this.rotation = this.velocity.X >= 0.0 ? this.rotation - 1f / 1000f : this.rotation + 1f / 1000f;
        if (this.dot)
        {
          this.scale = this.scale + 0.5f * targetScale;
          if ((double) this.scale <= 0.8 * (double) targetScale)
            return;
          this.scale = 0.8f * targetScale;
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
    }

    public static void UpdateCombatText()
    {
      for (int index = 0; index < 100; ++index)
      {
        if (Main.combatText[index].active)
          Main.combatText[index].Update();
      }
    }
  }
}
