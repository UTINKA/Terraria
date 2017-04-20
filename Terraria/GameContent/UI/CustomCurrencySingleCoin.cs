// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.CustomCurrencySingleCoin
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI
{
  public class CustomCurrencySingleCoin : CustomCurrencySystem
  {
    public float CurrencyDrawScale = 0.8f;
    public string CurrencyTextKey = "Currency.DefenderMedals";
    public Color CurrencyTextColor = new Color(240, 100, 120);

    public CustomCurrencySingleCoin(int coinItemID, long currencyCap)
    {
      this.Include(coinItemID, 1);
      this.SetCurrencyCap(currencyCap);
    }

    public override bool TryPurchasing(int price, List<Item[]> inv, List<Point> slotCoins, List<Point> slotsEmpty, List<Point> slotEmptyBank, List<Point> slotEmptyBank2, List<Point> slotEmptyBank3)
    {
      List<Tuple<Point, Item>> cache = this.ItemCacheCreate(inv);
      int num1 = price;
      for (int index = 0; index < slotCoins.Count; ++index)
      {
        Point slotCoin = slotCoins[index];
        int num2 = num1;
        if (inv[(int) slotCoin.X][slotCoin.Y].stack < num2)
          num2 = inv[(int) slotCoin.X][slotCoin.Y].stack;
        num1 -= num2;
        inv[(int) slotCoin.X][slotCoin.Y].stack -= num2;
        if (inv[(int) slotCoin.X][slotCoin.Y].stack == 0)
        {
          switch ((int) slotCoin.X)
          {
            case 0:
              slotsEmpty.Add(slotCoin);
              break;
            case 1:
              slotEmptyBank.Add(slotCoin);
              break;
            case 2:
              slotEmptyBank2.Add(slotCoin);
              break;
            case 3:
              slotEmptyBank3.Add(slotCoin);
              break;
          }
          slotCoins.Remove(slotCoin);
          --index;
        }
        if (num1 == 0)
          break;
      }
      if (num1 == 0)
        return true;
      this.ItemCacheRestore(cache, inv);
      return false;
    }

    public override void DrawSavingsMoney(SpriteBatch sb, string text, float shopx, float shopy, long totalCoins, bool horizontal = false)
    {
      int index = this._valuePerUnit.Keys.ElementAt<int>(0);
      Texture2D tex = Main.itemTexture[index];
      if (horizontal)
      {
        Vector2 vector2;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2).\u002Ector((float) ((double) shopx + ChatManager.GetStringSize(Main.fontMouseText, text, Vector2.get_One(), -1f).X + 45.0), shopy + 50f);
        sb.Draw(tex, vector2, new Rectangle?(), Color.get_White(), 0.0f, Vector2.op_Division(tex.Size(), 2f), this.CurrencyDrawScale, (SpriteEffects) 0, 0.0f);
        Utils.DrawBorderStringFourWay(sb, Main.fontItemStack, totalCoins.ToString(), (float) (vector2.X - 11.0), (float) vector2.Y, Color.get_White(), Color.get_Black(), new Vector2(0.3f), 0.75f);
      }
      else
      {
        int num = totalCoins > 99L ? -6 : 0;
        sb.Draw(tex, new Vector2(shopx + 11f, shopy + 75f), new Rectangle?(), Color.get_White(), 0.0f, Vector2.op_Division(tex.Size(), 2f), this.CurrencyDrawScale, (SpriteEffects) 0, 0.0f);
        Utils.DrawBorderStringFourWay(sb, Main.fontItemStack, totalCoins.ToString(), shopx + (float) num, shopy + 75f, Color.get_White(), Color.get_Black(), new Vector2(0.3f), 0.75f);
      }
    }

    public override void GetPriceText(string[] lines, ref int currentLine, int price)
    {
      Color color = Color.op_Multiply(this.CurrencyTextColor, (float) Main.mouseTextColor / (float) byte.MaxValue);
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      lines[currentLine++] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", (object) ((Color) @color).get_R(), (object) ((Color) @color).get_G(), (object) ((Color) @color).get_B(), (object) Lang.tip[50].Value, (object) price, (object) Language.GetTextValue(this.CurrencyTextKey).ToLower());
    }
  }
}
