// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.CustomCurrencyManager
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ID;

namespace Terraria.GameContent.UI
{
  public class CustomCurrencyManager
  {
    private static int _nextCurrencyIndex = 0;
    private static Dictionary<int, CustomCurrencySystem> _currencies = new Dictionary<int, CustomCurrencySystem>();

    public static void Initialize()
    {
      CustomCurrencyManager._nextCurrencyIndex = 0;
      CustomCurrencyID.DefenderMedals = CustomCurrencyManager.RegisterCurrency((CustomCurrencySystem) new CustomCurrencySingleCoin(3817, 999L));
    }

    public static int RegisterCurrency(CustomCurrencySystem collection)
    {
      int nextCurrencyIndex = CustomCurrencyManager._nextCurrencyIndex;
      ++CustomCurrencyManager._nextCurrencyIndex;
      CustomCurrencyManager._currencies[nextCurrencyIndex] = collection;
      return nextCurrencyIndex;
    }

    public static void DrawSavings(SpriteBatch sb, int currencyIndex, float shopx, float shopy, bool horizontal = false)
    {
      CustomCurrencySystem currency = CustomCurrencyManager._currencies[currencyIndex];
      Player player = Main.player[Main.myPlayer];
      bool overFlowing;
      long num1 = currency.CountCurrency(out overFlowing, player.bank.item);
      long num2 = currency.CountCurrency(out overFlowing, player.bank2.item);
      long num3 = currency.CountCurrency(out overFlowing, player.bank3.item);
      long totalCoins = currency.CombineStacks(out overFlowing, num1, num2, num3);
      if (totalCoins <= 0L)
        return;
      if (num3 > 0L)
        sb.Draw(Main.itemTexture[3813], Utils.CenteredRectangle(new Vector2(shopx + 80f, shopy + 50f), Vector2.op_Multiply(Main.itemTexture[3813].Size(), 0.65f)), new Rectangle?(), Color.get_White());
      if (num2 > 0L)
        sb.Draw(Main.itemTexture[346], Utils.CenteredRectangle(new Vector2(shopx + 80f, shopy + 50f), Vector2.op_Multiply(Main.itemTexture[346].Size(), 0.65f)), new Rectangle?(), Color.get_White());
      if (num1 > 0L)
        sb.Draw(Main.itemTexture[87], Utils.CenteredRectangle(new Vector2(shopx + 70f, shopy + 60f), Vector2.op_Multiply(Main.itemTexture[87].Size(), 0.65f)), new Rectangle?(), Color.get_White());
      Utils.DrawBorderStringFourWay(sb, Main.fontMouseText, Lang.inter[66].Value, shopx, shopy + 40f, Color.op_Multiply(Color.get_White(), (float) Main.mouseTextColor / (float) byte.MaxValue), Color.get_Black(), Vector2.get_Zero(), 1f);
      currency.DrawSavingsMoney(sb, Lang.inter[66].Value, shopx, shopy, totalCoins, horizontal);
    }

    public static void GetPriceText(int currencyIndex, string[] lines, ref int currentLine, int price)
    {
      CustomCurrencyManager._currencies[currencyIndex].GetPriceText(lines, ref currentLine, price);
    }

    public static bool BuyItem(Player player, int price, int currencyIndex)
    {
      CustomCurrencySystem currency = CustomCurrencyManager._currencies[currencyIndex];
      bool overFlowing;
      long num1 = currency.CountCurrency(out overFlowing, player.inventory, 58, 57, 56, 55, 54);
      long num2 = currency.CountCurrency(out overFlowing, player.bank.item);
      long num3 = currency.CountCurrency(out overFlowing, player.bank2.item);
      long num4 = currency.CountCurrency(out overFlowing, player.bank3.item);
      if (currency.CombineStacks(out overFlowing, num1, num2, num3, num4) < (long) price)
        return false;
      List<Item[]> objArrayList = new List<Item[]>();
      Dictionary<int, List<int>> slotsToIgnore = new Dictionary<int, List<int>>();
      List<Point> pointList1 = new List<Point>();
      List<Point> slotCoins = new List<Point>();
      List<Point> pointList2 = new List<Point>();
      List<Point> pointList3 = new List<Point>();
      List<Point> pointList4 = new List<Point>();
      objArrayList.Add(player.inventory);
      objArrayList.Add(player.bank.item);
      objArrayList.Add(player.bank2.item);
      objArrayList.Add(player.bank3.item);
      for (int index = 0; index < objArrayList.Count; ++index)
        slotsToIgnore[index] = new List<int>();
      slotsToIgnore[0] = new List<int>()
      {
        58,
        57,
        56,
        55,
        54
      };
      for (int index1 = 0; index1 < objArrayList.Count; ++index1)
      {
        for (int index2 = 0; index2 < objArrayList[index1].Length; ++index2)
        {
          if (!slotsToIgnore[index1].Contains(index2) && currency.Accepts(objArrayList[index1][index2]))
            slotCoins.Add(new Point(index1, index2));
        }
      }
      CustomCurrencyManager.FindEmptySlots(objArrayList, slotsToIgnore, pointList1, 0);
      CustomCurrencyManager.FindEmptySlots(objArrayList, slotsToIgnore, pointList2, 1);
      CustomCurrencyManager.FindEmptySlots(objArrayList, slotsToIgnore, pointList3, 2);
      CustomCurrencyManager.FindEmptySlots(objArrayList, slotsToIgnore, pointList4, 3);
      return currency.TryPurchasing(price, objArrayList, slotCoins, pointList1, pointList2, pointList3, pointList4);
    }

    private static void FindEmptySlots(List<Item[]> inventories, Dictionary<int, List<int>> slotsToIgnore, List<Point> emptySlots, int currentInventoryIndex)
    {
      for (int index = inventories[currentInventoryIndex].Length - 1; index >= 0; --index)
      {
        if (!slotsToIgnore[currentInventoryIndex].Contains(index) && (inventories[currentInventoryIndex][index].type == 0 || inventories[currentInventoryIndex][index].stack == 0))
          emptySlots.Add(new Point(currentInventoryIndex, index));
      }
    }
  }
}
