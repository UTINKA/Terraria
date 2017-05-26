// Decompiled with JetBrains decompiler
// Type: Terraria.Lang
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.GameContent.Events;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria
{
  public class Lang
  {
    [Obsolete("Lang arrays have been replaced with the new Language.GetText system.")]
    public static LocalizedText[] menu = new LocalizedText[253];
    [Obsolete("Lang arrays have been replaced with the new Language.GetText system.")]
    public static LocalizedText[] gen = new LocalizedText[82];
    [Obsolete("Lang arrays have been replaced with the new Language.GetText system.")]
    public static LocalizedText[] misc = new LocalizedText[201];
    [Obsolete("Lang arrays have been replaced with the new Language.GetText system.")]
    public static LocalizedText[] inter = new LocalizedText[129];
    [Obsolete("Lang arrays have been replaced with the new Language.GetText system.")]
    public static LocalizedText[] tip = new LocalizedText[60];
    [Obsolete("Lang arrays have been replaced with the new Language.GetText system.")]
    public static LocalizedText[] mp = new LocalizedText[23];
    [Obsolete("Lang arrays have been replaced with the new Language.GetText system.")]
    public static LocalizedText[] chestType = new LocalizedText[52];
    [Obsolete("Lang arrays have been replaced with the new Language.GetText system.")]
    public static LocalizedText[] dresserType = new LocalizedText[32];
    [Obsolete("Lang arrays have been replaced with the new Language.GetText system.")]
    public static LocalizedText[] chestType2 = new LocalizedText[2];
    public static LocalizedText[] prefix = new LocalizedText[84];
    private static LocalizedText[] _itemNameCache = new LocalizedText[3930];
    private static LocalizedText[] _projectileNameCache = new LocalizedText[714];
    private static LocalizedText[] _npcNameCache = new LocalizedText[580];
    private static LocalizedText[] _negativeNpcNameCache = new LocalizedText[65];
    private static LocalizedText[] _buffNameCache = new LocalizedText[206];
    private static LocalizedText[] _buffDescriptionCache = new LocalizedText[206];
    private static ItemTooltip[] _itemTooltipCache = new ItemTooltip[3930];
    public static LocalizedText[] _mapLegendCache;

    public static string GetMapObjectName(int id)
    {
      if (Lang._mapLegendCache != null)
        return Lang._mapLegendCache[id].Value;
      return string.Empty;
    }

    public static object CreateDialogSubstitutionObject(NPC npc = null)
    {
      return (object) new
      {
        Nurse = NPC.GetFirstNPCNameOrNull(18),
        Merchant = NPC.GetFirstNPCNameOrNull(17),
        ArmsDealer = NPC.GetFirstNPCNameOrNull(19),
        Dryad = NPC.GetFirstNPCNameOrNull(20),
        Demolitionist = NPC.GetFirstNPCNameOrNull(38),
        Clothier = NPC.GetFirstNPCNameOrNull(54),
        Guide = NPC.GetFirstNPCNameOrNull(22),
        Wizard = NPC.GetFirstNPCNameOrNull(108),
        GoblinTinkerer = NPC.GetFirstNPCNameOrNull(107),
        Mechanic = NPC.GetFirstNPCNameOrNull(124),
        Truffle = NPC.GetFirstNPCNameOrNull(160),
        Steampunker = NPC.GetFirstNPCNameOrNull(178),
        DyeTrader = NPC.GetFirstNPCNameOrNull(207),
        PartyGirl = NPC.GetFirstNPCNameOrNull(208),
        Cyborg = NPC.GetFirstNPCNameOrNull(209),
        Painter = NPC.GetFirstNPCNameOrNull(227),
        WitchDoctor = NPC.GetFirstNPCNameOrNull(228),
        Pirate = NPC.GetFirstNPCNameOrNull(229),
        Stylist = NPC.GetFirstNPCNameOrNull(353),
        TravelingMerchant = NPC.GetFirstNPCNameOrNull(368),
        Angler = NPC.GetFirstNPCNameOrNull(369),
        Bartender = NPC.GetFirstNPCNameOrNull(550),
        WorldName = Main.ActiveWorldFileData.Name,
        Day = Main.dayTime,
        BloodMoon = Main.bloodMoon,
        MoonLordDefeated = NPC.downedMoonlord,
        HardMode = Main.hardMode,
        Homeless = (npc != null && npc.homeless),
        InventoryKey = Main.cInv,
        PlayerName = Main.player[Main.myPlayer].name
      };
    }

    [Obsolete("dialog is deprecated. Please use Language.GetText instead.")]
    public static string dialog(int l, bool english = false)
    {
      return Language.GetTextValueWith("LegacyDialog." + (object) l, Lang.CreateDialogSubstitutionObject((NPC) null));
    }

    public static string GetNPCNameValue(int netID)
    {
      return Lang.GetNPCName(netID).Value;
    }

    public static LocalizedText GetNPCName(int netID)
    {
      if (netID > 0 && netID < 580)
        return Lang._npcNameCache[netID];
      if (netID < 0 && -netID - 1 < Lang._negativeNpcNameCache.Length)
        return Lang._negativeNpcNameCache[-netID - 1];
      return LocalizedText.Empty;
    }

    public static ItemTooltip GetTooltip(int itemId)
    {
      return Lang._itemTooltipCache[itemId];
    }

    public static LocalizedText GetItemName(int id)
    {
      id = (int) ItemID.FromNetId((short) id);
      if (id > 0 && id < 3930 && Lang._itemNameCache[id] != null)
        return Lang._itemNameCache[id];
      return LocalizedText.Empty;
    }

    public static string GetItemNameValue(int id)
    {
      return Lang.GetItemName(id).Value;
    }

    public static string GetBuffName(int id)
    {
      return Lang._buffNameCache[id].Value;
    }

    public static string GetBuffDescription(int id)
    {
      return Lang._buffDescriptionCache[id].Value;
    }

    public static string GetDryadWorldStatusDialog()
    {
      int tGood = (int) WorldGen.tGood;
      int tEvil = (int) WorldGen.tEvil;
      int tBlood = (int) WorldGen.tBlood;
      string textValue;
      if (tGood > 0 && tEvil > 0 && tBlood > 0)
        textValue = Language.GetTextValue("DryadSpecialText.WorldStatusAll", (object) Main.worldName, (object) tGood, (object) tEvil, (object) tBlood);
      else if (tGood > 0 && tEvil > 0)
        textValue = Language.GetTextValue("DryadSpecialText.WorldStatusHallowCorrupt", (object) Main.worldName, (object) tGood, (object) tEvil);
      else if (tGood > 0 && tBlood > 0)
        textValue = Language.GetTextValue("DryadSpecialText.WorldStatusHallowCrimson", (object) Main.worldName, (object) tGood, (object) tBlood);
      else if (tEvil > 0 && tBlood > 0)
        textValue = Language.GetTextValue("DryadSpecialText.WorldStatusCorruptCrimson", (object) Main.worldName, (object) tEvil, (object) tBlood);
      else if (tEvil > 0)
        textValue = Language.GetTextValue("DryadSpecialText.WorldStatusCorrupt", (object) Main.worldName, (object) tEvil);
      else if (tBlood > 0)
      {
        textValue = Language.GetTextValue("DryadSpecialText.WorldStatusCrimson", (object) Main.worldName, (object) tBlood);
      }
      else
      {
        if (tGood <= 0)
          return Language.GetTextValue("DryadSpecialText.WorldStatusPure", (object) Main.worldName);
        textValue = Language.GetTextValue("DryadSpecialText.WorldStatusHallow", (object) Main.worldName, (object) tGood);
      }
      string str = (double) tGood * 1.2 < (double) (tEvil + tBlood) || (double) tGood * 0.8 > (double) (tEvil + tBlood) ? (tGood < tEvil + tBlood ? (tEvil + tBlood <= tGood + 20 ? (tEvil + tBlood <= 10 ? Language.GetTextValue("DryadSpecialText.WorldDescriptionClose") : Language.GetTextValue("DryadSpecialText.WorldDescriptionWork")) : Language.GetTextValue("DryadSpecialText.WorldDescriptionGrim")) : Language.GetTextValue("DryadSpecialText.WorldDescriptionFairyTale")) : Language.GetTextValue("DryadSpecialText.WorldDescriptionBalanced");
      return string.Format("{0} {1}", (object) textValue, (object) str);
    }

    public static string GetRandomGameTitle()
    {
      return Language.RandomFromCategory("GameTitle", (UnifiedRandom) null).Value;
    }

    public static string DyeTraderQuestChat(bool gotDye = false)
    {
      object substitutionObject = Lang.CreateDialogSubstitutionObject((NPC) null);
      LocalizedText[] all = Language.FindAll(Lang.CreateDialogFilter(gotDye ? "DyeTraderSpecialText.HasPlant" : "DyeTraderSpecialText.NoPlant", substitutionObject));
      return all[Main.rand.Next(all.Length)].FormatWith(substitutionObject);
    }

    public static string BartenderHelpText(NPC npc)
    {
      object substitutionObject = Lang.CreateDialogSubstitutionObject(npc);
      Player player = Main.player[Main.myPlayer];
      if (player.bartenderQuestLog == 0)
      {
        ++player.bartenderQuestLog;
        Item newItem = new Item();
        newItem.SetDefaults(3817, false);
        newItem.stack = 5;
        newItem.position = player.Center;
        Item obj = player.GetItem(player.whoAmI, newItem, true, false);
        if (obj.stack > 0)
        {
          int number = Item.NewItem((int) player.position.X, (int) player.position.Y, player.width, player.height, obj.type, obj.stack, false, 0, true, false);
          if (Main.netMode == 1)
            NetMessage.SendData(21, -1, -1, (NetworkText) null, number, 1f, 0.0f, 0.0f, 0, 0, 0);
        }
        return Language.GetTextValueWith("BartenderSpecialText.FirstHelp", substitutionObject);
      }
      LocalizedText[] all = Language.FindAll(Lang.CreateDialogFilter("BartenderHelpText.", substitutionObject));
      if (Main.BartenderHelpTextIndex >= all.Length)
        Main.BartenderHelpTextIndex = 0;
      return all[Main.BartenderHelpTextIndex++].FormatWith(substitutionObject);
    }

    public static string BartenderChat(NPC npc)
    {
      object substitutionObject = Lang.CreateDialogSubstitutionObject(npc);
      if (Main.rand.Next(5) == 0)
        return Language.GetTextValueWith(!DD2Event.DownedInvasionT3 ? (!DD2Event.DownedInvasionT2 ? (!DD2Event.DownedInvasionT1 ? "BartenderSpecialText.BeforeDD2Tier1" : "BartenderSpecialText.AfterDD2Tier1") : "BartenderSpecialText.AfterDD2Tier2") : "BartenderSpecialText.AfterDD2Tier3", substitutionObject);
      return Language.SelectRandom(Lang.CreateDialogFilter("BartenderChatter.", substitutionObject), (UnifiedRandom) null).FormatWith(substitutionObject);
    }

    public static LanguageSearchFilter CreateDialogFilter(string startsWith, object substitutions)
    {
      return (LanguageSearchFilter) ((key, text) =>
      {
        if (key.StartsWith(startsWith))
          return text.CanFormatWith(substitutions);
        return false;
      });
    }

    public static LanguageSearchFilter CreateDialogFilter(string startsWith)
    {
      return (LanguageSearchFilter) ((key, text) => key.StartsWith(startsWith));
    }

    public static string AnglerQuestChat(bool turnIn = false)
    {
      object substitutionObject = Lang.CreateDialogSubstitutionObject((NPC) null);
      if (turnIn)
        return Language.SelectRandom(Lang.CreateDialogFilter("AnglerQuestText.TurnIn_", substitutionObject), (UnifiedRandom) null).FormatWith(substitutionObject);
      if (Main.anglerQuestFinished)
        return Language.SelectRandom(Lang.CreateDialogFilter("AnglerQuestText.NoQuest_", substitutionObject), (UnifiedRandom) null).FormatWith(substitutionObject);
      int anglerQuestItemNetId = Main.anglerQuestItemNetIDs[Main.anglerQuest];
      Main.npcChatCornerItem = anglerQuestItemNetId;
      return Language.GetTextValueWith("AnglerQuestText.Quest_" + ItemID.Search.GetName(anglerQuestItemNetId), substitutionObject);
    }

    public static LocalizedText GetProjectileName(int type)
    {
      if (type >= 0 && type < Lang._projectileNameCache.Length && Lang._projectileNameCache[type] != null)
        return Lang._projectileNameCache[type];
      return LocalizedText.Empty;
    }

    private static void FillNameCacheArray<IdClass, IdType>(string category, LocalizedText[] nameCache, bool leaveMissingEntriesBlank = false) where IdType : IConvertible
    {
      for (int index = 0; index < nameCache.Length; ++index)
        nameCache[index] = LocalizedText.Empty;
      ((IEnumerable<FieldInfo>) typeof (IdClass).GetFields(BindingFlags.Static | BindingFlags.Public)).Where<FieldInfo>((Func<FieldInfo, bool>) (f => f.FieldType == typeof (IdType))).ToList<FieldInfo>().ForEach((Action<FieldInfo>) (field =>
      {
        long int64 = Convert.ToInt64((object) (IdType) field.GetValue((object) null));
        if (int64 > 0L && int64 < (long) nameCache.Length)
        {
          nameCache[int64] = !leaveMissingEntriesBlank || Language.Exists(category + "." + field.Name) ? Language.GetText(category + "." + field.Name) : LocalizedText.Empty;
        }
        else
        {
          if (int64 != 0L || !(field.Name == "None"))
            return;
          nameCache[int64] = LocalizedText.Empty;
        }
      }));
    }

    public static void InitializeLegacyLocalization()
    {
      Lang.FillNameCacheArray<PrefixID, int>("Prefix", Lang.prefix, false);
      for (int index = 0; index < Lang.gen.Length; ++index)
        Lang.gen[index] = Language.GetText("LegacyWorldGen." + (object) index);
      for (int index = 0; index < Lang.menu.Length; ++index)
        Lang.menu[index] = Language.GetText("LegacyMenu." + (object) index);
      for (int index = 0; index < Lang.inter.Length; ++index)
        Lang.inter[index] = Language.GetText("LegacyInterface." + (object) index);
      for (int index = 0; index < Lang.misc.Length; ++index)
        Lang.misc[index] = Language.GetText("LegacyMisc." + (object) index);
      for (int index = 0; index < Lang.mp.Length; ++index)
        Lang.mp[index] = Language.GetText("LegacyMultiplayer." + (object) index);
      for (int index = 0; index < Lang.tip.Length; ++index)
        Lang.tip[index] = Language.GetText("LegacyTooltip." + (object) index);
      for (int index = 0; index < Lang.chestType.Length; ++index)
        Lang.chestType[index] = Language.GetText("LegacyChestType." + (object) index);
      for (int index = 0; index < Lang.chestType2.Length; ++index)
        Lang.chestType2[index] = Language.GetText("LegacyChestType2." + (object) index);
      for (int index = 0; index < Lang.dresserType.Length; ++index)
        Lang.dresserType[index] = Language.GetText("LegacyDresserType." + (object) index);
      Lang.FillNameCacheArray<ItemID, short>("ItemName", Lang._itemNameCache, false);
      Lang.FillNameCacheArray<ProjectileID, short>("ProjectileName", Lang._projectileNameCache, false);
      Lang.FillNameCacheArray<NPCID, short>("NPCName", Lang._npcNameCache, false);
      Lang.FillNameCacheArray<BuffID, int>("BuffName", Lang._buffNameCache, false);
      Lang.FillNameCacheArray<BuffID, int>("BuffDescription", Lang._buffDescriptionCache, false);
      for (int id = -65; id < 0; ++id)
        Lang._negativeNpcNameCache[-id - 1] = Lang._npcNameCache[NPCID.FromNetId(id)];
      Lang._negativeNpcNameCache[0] = Language.GetText("NPCName.Slimeling");
      Lang._negativeNpcNameCache[1] = Language.GetText("NPCName.Slimer2");
      Lang._negativeNpcNameCache[2] = Language.GetText("NPCName.GreenSlime");
      Lang._negativeNpcNameCache[3] = Language.GetText("NPCName.Pinky");
      Lang._negativeNpcNameCache[4] = Language.GetText("NPCName.BabySlime");
      Lang._negativeNpcNameCache[5] = Language.GetText("NPCName.BlackSlime");
      Lang._negativeNpcNameCache[6] = Language.GetText("NPCName.PurpleSlime");
      Lang._negativeNpcNameCache[7] = Language.GetText("NPCName.RedSlime");
      Lang._negativeNpcNameCache[8] = Language.GetText("NPCName.YellowSlime");
      Lang._negativeNpcNameCache[9] = Language.GetText("NPCName.JungleSlime");
      Lang._negativeNpcNameCache[53] = Language.GetText("NPCName.SmallRainZombie");
      Lang._negativeNpcNameCache[54] = Language.GetText("NPCName.BigRainZombie");
      ItemTooltip.AddGlobalProcessor((TooltipProcessor) (tooltip =>
      {
        if (tooltip.Contains("<right>"))
        {
          InputMode index = InputMode.XBoxGamepad;
          if (PlayerInput.UsingGamepad)
            index = InputMode.XBoxGamepadUI;
          if (index == InputMode.XBoxGamepadUI)
          {
            string newValue = PlayerInput.BuildCommand("", true, PlayerInput.CurrentProfile.InputModes[index].KeyStatus["MouseRight"]).Replace(": ", "");
            tooltip = tooltip.Replace("<right>", newValue);
          }
          else
            tooltip = tooltip.Replace("<right>", Language.GetTextValue("Controls.RightClick"));
        }
        return tooltip;
      }));
      for (int index = 0; index < Lang._itemTooltipCache.Length; ++index)
        Lang._itemTooltipCache[index] = ItemTooltip.None;
      ((IEnumerable<FieldInfo>) typeof (ItemID).GetFields(BindingFlags.Static | BindingFlags.Public)).Where<FieldInfo>((Func<FieldInfo, bool>) (f => f.FieldType == typeof (short))).ToList<FieldInfo>().ForEach((Action<FieldInfo>) (field =>
      {
        short num = (short) field.GetValue((object) null);
        if ((int) num <= 0 || (int) num >= Lang._itemTooltipCache.Length)
          return;
        Lang._itemTooltipCache[(int) num] = ItemTooltip.FromLanguageKey("ItemTooltip." + field.Name);
      }));
    }

    public static void BuildMapAtlas()
    {
    }

    public static NetworkText CreateDeathMessage(string deadPlayerName, int plr = -1, int npc = -1, int proj = -1, int other = -1, int projType = 0, int plrItemType = 0)
    {
      NetworkText networkText1 = NetworkText.Empty;
      NetworkText networkText2 = NetworkText.Empty;
      NetworkText networkText3 = NetworkText.Empty;
      NetworkText networkText4 = NetworkText.Empty;
      if (proj >= 0)
        networkText1 = NetworkText.FromKey(Lang.GetProjectileName(projType).Key);
      if (npc >= 0)
        networkText2 = Main.npc[npc].GetGivenOrTypeNetName();
      if (plr >= 0 && plr < (int) byte.MaxValue)
        networkText3 = NetworkText.FromLiteral(Main.player[plr].name);
      if (plrItemType >= 0)
        networkText4 = NetworkText.FromKey(Lang.GetItemName(plrItemType).Key);
      bool flag1 = networkText1 != NetworkText.Empty;
      bool flag2 = plr >= 0 && plr < (int) byte.MaxValue;
      bool flag3 = networkText2 != NetworkText.Empty;
      NetworkText networkText5 = NetworkText.Empty;
      NetworkText empty = NetworkText.Empty;
      NetworkText networkText6 = NetworkText.FromKey(Language.RandomFromCategory("DeathTextGeneric", (UnifiedRandom) null).Key, (object) deadPlayerName, (object) Main.worldName);
      if (flag2)
        networkText5 = NetworkText.FromKey("DeathSource.Player", (object) networkText6, (object) networkText3, (object) (flag1 ? networkText1 : networkText4));
      else if (flag3)
        networkText5 = NetworkText.FromKey("DeathSource.NPC", (object) networkText6, (object) networkText2);
      else if (flag1)
        networkText5 = NetworkText.FromKey("DeathSource.Projectile", (object) networkText6, (object) networkText1);
      else if (other >= 0)
      {
        if (other == 0)
          networkText5 = NetworkText.FromKey("DeathText.Fell_" + (object) (Main.rand.Next(2) + 1), (object) deadPlayerName);
        else if (other == 1)
          networkText5 = NetworkText.FromKey("DeathText.Drowned_" + (object) (Main.rand.Next(4) + 1), (object) deadPlayerName);
        else if (other == 2)
          networkText5 = NetworkText.FromKey("DeathText.Lava_" + (object) (Main.rand.Next(4) + 1), (object) deadPlayerName);
        else if (other == 3)
          networkText5 = NetworkText.FromKey("DeathText.Default", (object) networkText6);
        else if (other == 4)
          networkText5 = NetworkText.FromKey("DeathText.Slain", (object) deadPlayerName);
        else if (other == 5)
          networkText5 = NetworkText.FromKey("DeathText.Petrified_" + (object) (Main.rand.Next(4) + 1), (object) deadPlayerName);
        else if (other == 6)
          networkText5 = NetworkText.FromKey("DeathText.Stabbed", (object) deadPlayerName);
        else if (other == 7)
          networkText5 = NetworkText.FromKey("DeathText.Suffocated", (object) deadPlayerName);
        else if (other == 8)
          networkText5 = NetworkText.FromKey("DeathText.Burned", (object) deadPlayerName);
        else if (other == 9)
          networkText5 = NetworkText.FromKey("DeathText.Poisoned", (object) deadPlayerName);
        else if (other == 10)
          networkText5 = NetworkText.FromKey("DeathText.Electrocuted", (object) deadPlayerName);
        else if (other == 11)
          networkText5 = NetworkText.FromKey("DeathText.TriedToEscape", (object) deadPlayerName);
        else if (other == 12)
          networkText5 = NetworkText.FromKey("DeathText.WasLicked", (object) deadPlayerName);
        else if (other == 13)
          networkText5 = NetworkText.FromKey("DeathText.Teleport_1", (object) deadPlayerName);
        else if (other == 14)
          networkText5 = NetworkText.FromKey("DeathText.Teleport_2_Male", (object) deadPlayerName);
        else if (other == 15)
          networkText5 = NetworkText.FromKey("DeathText.Teleport_2_Female", (object) deadPlayerName);
        else if (other == 254)
          networkText5 = NetworkText.Empty;
        else if (other == (int) byte.MaxValue)
          networkText5 = NetworkText.FromKey("DeathText.Slain", (object) deadPlayerName);
      }
      return networkText5;
    }

    public static NetworkText GetInvasionWaveText(int wave, params short[] npcIds)
    {
      NetworkText[] networkTextArray = new NetworkText[npcIds.Length + 1];
      for (int index = 0; index < npcIds.Length; ++index)
        networkTextArray[index + 1] = NetworkText.FromKey(Lang.GetNPCName((int) npcIds[index]).Key);
      if (wave == -1)
        networkTextArray[0] = NetworkText.FromKey("Game.FinalWave");
      else if (wave == 1)
        networkTextArray[0] = NetworkText.FromKey("Game.FirstWave");
      else
        networkTextArray[0] = NetworkText.FromKey("Game.Wave", (object) wave);
      return NetworkText.FromKey("Game.InvasionWave_Type" + (object) npcIds.Length, (object[]) networkTextArray);
    }

    public static string LocalizedDuration(TimeSpan time, bool abbreviated, bool showAllAvailableUnits)
    {
      string str1 = "";
      abbreviated |= !GameCulture.English.IsActive;
      if (time.Days > 0)
      {
        string str2 = str1 + (object) time.Days + (abbreviated ? (object) (" " + Language.GetTextValue("Misc.ShortDays")) : (time.Days == 1 ? (object) " day" : (object) " days"));
        if (!showAllAvailableUnits)
          return str2;
        str1 = str2 + " ";
      }
      if (time.Hours > 0)
      {
        string str2 = str1 + (object) time.Hours + (abbreviated ? (object) (" " + Language.GetTextValue("Misc.ShortHours")) : (time.Hours == 1 ? (object) " hour" : (object) " hours"));
        if (!showAllAvailableUnits)
          return str2;
        str1 = str2 + " ";
      }
      if (time.Minutes > 0)
      {
        string str2 = str1 + (object) time.Minutes + (abbreviated ? (object) (" " + Language.GetTextValue("Misc.ShortMinutes")) : (time.Minutes == 1 ? (object) " minute" : (object) " minutes"));
        if (!showAllAvailableUnits)
          return str2;
        str1 = str2 + " ";
      }
      return str1 + (object) time.Seconds + (abbreviated ? (object) (" " + Language.GetTextValue("Misc.ShortSeconds")) : (time.Seconds == 1 ? (object) " second" : (object) " seconds"));
    }
  }
}
