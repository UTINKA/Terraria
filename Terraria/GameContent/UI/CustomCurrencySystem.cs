// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.CustomCurrencySystem
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Terraria.GameContent.UI
{
  public class CustomCurrencySystem
  {
    protected Dictionary<int, int> _valuePerUnit = new Dictionary<int, int>();
    private long _currencyCap = 999999999;

    public long CurrencyCap
    {
      get
      {
        return this._currencyCap;
      }
    }

    public void Include(int coin, int howMuchIsItWorth)
    {
      this._valuePerUnit[coin] = howMuchIsItWorth;
    }

    public void SetCurrencyCap(long cap)
    {
      this._currencyCap = cap;
    }

    public virtual long CountCurrency(out bool overFlowing, Item[] inv, params int[] ignoreSlots)
    {
      List<int> intList = new List<int>((IEnumerable<int>) ignoreSlots);
      long num1 = 0;
      for (int index = 0; index < inv.Length; ++index)
      {
        if (!intList.Contains(index))
        {
          int num2;
          if (this._valuePerUnit.TryGetValue(inv[index].type, out num2))
            num1 += (long) (num2 * inv[index].stack);
          if (num1 >= this.CurrencyCap)
          {
            overFlowing = true;
            return this.CurrencyCap;
          }
        }
      }
      overFlowing = false;
      return num1;
    }

    public virtual long CombineStacks(out bool overFlowing, params long[] coinCounts)
    {
      long num = 0;
      foreach (long coinCount in coinCounts)
      {
        num += coinCount;
        if (num >= this.CurrencyCap)
        {
          overFlowing = true;
          return this.CurrencyCap;
        }
      }
      overFlowing = false;
      return num;
    }

    public virtual bool TryPurchasing(int price, List<Item[]> inv, List<Point> slotCoins, List<Point> slotsEmpty, List<Point> slotEmptyBank, List<Point> slotEmptyBank2, List<Point> slotEmptyBank3)
    {
      long num1 = (long) price;
      Dictionary<Point, Item> dictionary = new Dictionary<Point, Item>();
      bool flag = true;
      while (num1 > 0L)
      {
        long num2 = 1000000;
        for (int index = 0; index < 4; ++index)
        {
          if (num1 >= num2)
          {
            using (List<Point>.Enumerator enumerator = slotCoins.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                Point current = enumerator.Current;
                if (inv[(int) current.X][current.Y].type == 74 - index)
                {
                  long num3 = num1 / num2;
                  dictionary[current] = inv[(int) current.X][current.Y].Clone();
                  if (num3 < (long) inv[(int) current.X][current.Y].stack)
                  {
                    inv[(int) current.X][current.Y].stack -= (int) num3;
                  }
                  else
                  {
                    inv[(int) current.X][current.Y].SetDefaults(0, false);
                    slotsEmpty.Add(current);
                  }
                  num1 -= num2 * (long) (dictionary[current].stack - inv[(int) current.X][current.Y].stack);
                }
              }
            }
          }
          num2 /= 100L;
        }
        if (num1 > 0L)
        {
          if (slotsEmpty.Count > 0)
          {
            slotsEmpty.Sort(new Comparison<Point>(DelegateMethods.CompareYReverse));
            Point point;
            // ISSUE: explicit reference operation
            ((Point) @point).\u002Ector(-1, -1);
            for (int index1 = 0; index1 < inv.Count; ++index1)
            {
              long num3 = 10000;
              for (int index2 = 0; index2 < 3; ++index2)
              {
                if (num1 >= num3)
                {
                  using (List<Point>.Enumerator enumerator = slotCoins.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      Point current = enumerator.Current;
                      if (current.X == index1 && inv[(int) current.X][current.Y].type == 74 - index2 && inv[(int) current.X][current.Y].stack >= 1)
                      {
                        List<Point> pointList = slotsEmpty;
                        if (index1 == 1 && slotEmptyBank.Count > 0)
                          pointList = slotEmptyBank;
                        if (index1 == 2 && slotEmptyBank2.Count > 0)
                          pointList = slotEmptyBank2;
                        if (index1 == 3 && slotEmptyBank3.Count > 0)
                          pointList = slotEmptyBank3;
                        if (--inv[(int) current.X][current.Y].stack <= 0)
                        {
                          inv[(int) current.X][current.Y].SetDefaults(0, false);
                          pointList.Add(current);
                        }
                        dictionary[pointList[0]] = inv[(int) pointList[0].X][pointList[0].Y].Clone();
                        inv[(int) pointList[0].X][pointList[0].Y].SetDefaults(73 - index2, false);
                        inv[(int) pointList[0].X][pointList[0].Y].stack = 100;
                        point = pointList[0];
                        pointList.RemoveAt(0);
                        break;
                      }
                    }
                  }
                }
                if (point.X == -1 && point.Y == -1)
                  num3 /= 100L;
                else
                  break;
              }
              for (int index2 = 0; index2 < 2; ++index2)
              {
                if (point.X == -1 && point.Y == -1)
                {
                  using (List<Point>.Enumerator enumerator = slotCoins.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      Point current = enumerator.Current;
                      if (current.X == index1 && inv[(int) current.X][current.Y].type == 73 + index2 && inv[(int) current.X][current.Y].stack >= 1)
                      {
                        List<Point> pointList = slotsEmpty;
                        if (index1 == 1 && slotEmptyBank.Count > 0)
                          pointList = slotEmptyBank;
                        if (index1 == 2 && slotEmptyBank2.Count > 0)
                          pointList = slotEmptyBank2;
                        if (index1 == 3 && slotEmptyBank3.Count > 0)
                          pointList = slotEmptyBank3;
                        if (--inv[(int) current.X][current.Y].stack <= 0)
                        {
                          inv[(int) current.X][current.Y].SetDefaults(0, false);
                          pointList.Add(current);
                        }
                        dictionary[pointList[0]] = inv[(int) pointList[0].X][pointList[0].Y].Clone();
                        inv[(int) pointList[0].X][pointList[0].Y].SetDefaults(72 + index2, false);
                        inv[(int) pointList[0].X][pointList[0].Y].stack = 100;
                        point = pointList[0];
                        pointList.RemoveAt(0);
                        break;
                      }
                    }
                  }
                }
              }
              if (point.X != -1 && point.Y != -1)
              {
                slotCoins.Add(point);
                break;
              }
            }
            slotsEmpty.Sort(new Comparison<Point>(DelegateMethods.CompareYReverse));
            slotEmptyBank.Sort(new Comparison<Point>(DelegateMethods.CompareYReverse));
            slotEmptyBank2.Sort(new Comparison<Point>(DelegateMethods.CompareYReverse));
            slotEmptyBank3.Sort(new Comparison<Point>(DelegateMethods.CompareYReverse));
          }
          else
          {
            using (Dictionary<Point, Item>.Enumerator enumerator = dictionary.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                KeyValuePair<Point, Item> current = enumerator.Current;
                inv[(int) current.Key.X][current.Key.Y] = current.Value.Clone();
              }
            }
            flag = false;
            break;
          }
        }
      }
      return flag;
    }

    public virtual bool Accepts(Item item)
    {
      return this._valuePerUnit.ContainsKey(item.type);
    }

    public virtual void DrawSavingsMoney(SpriteBatch sb, string text, float shopx, float shopy, long totalCoins, bool horizontal = false)
    {
    }

    public virtual void GetPriceText(string[] lines, ref int currentLine, int price)
    {
    }

    protected int SortByHighest(Tuple<int, int> valueA, Tuple<int, int> valueB)
    {
      return valueA.Item2 > valueB.Item2 || valueA.Item2 != valueB.Item2 ? -1 : 0;
    }

    protected List<Tuple<Point, Item>> ItemCacheCreate(List<Item[]> inventories)
    {
      List<Tuple<Point, Item>> tupleList = new List<Tuple<Point, Item>>();
      for (int index1 = 0; index1 < inventories.Count; ++index1)
      {
        for (int index2 = 0; index2 < inventories[index1].Length; ++index2)
        {
          Item obj = inventories[index1][index2];
          tupleList.Add(new Tuple<Point, Item>(new Point(index1, index2), obj.DeepClone()));
        }
      }
      return tupleList;
    }

    protected void ItemCacheRestore(List<Tuple<Point, Item>> cache, List<Item[]> inventories)
    {
      using (List<Tuple<Point, Item>>.Enumerator enumerator = cache.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Tuple<Point, Item> current = enumerator.Current;
          inventories[(int) current.Item1.X][current.Item1.Y] = current.Item2;
        }
      }
    }
  }
}
