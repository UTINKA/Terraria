// Decompiled with JetBrains decompiler
// Type: Terraria.RecipeGroup
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;

namespace Terraria
{
  public class RecipeGroup
  {
    public static Dictionary<int, RecipeGroup> recipeGroups = new Dictionary<int, RecipeGroup>();
    public static Dictionary<string, int> recipeGroupIDs = new Dictionary<string, int>();
    public static int nextRecipeGroupIndex = 0;
    public Func<string> GetText;
    public List<int> ValidItems;
    public int IconicItemIndex;

    public RecipeGroup(Func<string> getName, params int[] validItems)
    {
      this.GetText = getName;
      this.ValidItems = new List<int>((IEnumerable<int>) validItems);
    }

    public static int RegisterGroup(string name, RecipeGroup rec)
    {
      int key = RecipeGroup.nextRecipeGroupIndex++;
      RecipeGroup.recipeGroups.Add(key, rec);
      RecipeGroup.recipeGroupIDs.Add(name, key);
      return key;
    }
  }
}
