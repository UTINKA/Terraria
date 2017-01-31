// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Events.BirthdayParty
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Graphics.Effects;

namespace Terraria.GameContent.Events
{
  public class BirthdayParty
  {
    public static bool ManualParty = false;
    public static bool GenuineParty = false;
    public static int PartyDaysOnCooldown = 0;
    public static List<int> CelebratingNPCs = new List<int>();
    private static bool _wasCelebrating = false;

    public static bool PartyIsUp
    {
      get
      {
        if (!BirthdayParty.GenuineParty)
          return BirthdayParty.ManualParty;
        return true;
      }
    }

    public static void CheckMorning()
    {
      BirthdayParty.NaturalAttempt();
    }

    public static void CheckNight()
    {
      bool flag = false;
      if (BirthdayParty.GenuineParty)
      {
        flag = true;
        BirthdayParty.GenuineParty = false;
        BirthdayParty.CelebratingNPCs.Clear();
      }
      if (BirthdayParty.ManualParty)
      {
        flag = true;
        BirthdayParty.ManualParty = false;
      }
      if (!flag)
        return;
      Color color = new Color((int) byte.MaxValue, 0, 160);
      WorldGen.BroadcastText(Lang.misc[99], color);
    }

    private static void NaturalAttempt()
    {
      if (Main.netMode == 1)
        return;
      if (BirthdayParty.PartyDaysOnCooldown > 0)
      {
        --BirthdayParty.PartyDaysOnCooldown;
      }
      else
      {
        if (Main.rand.Next(10) != 0)
          return;
        List<NPC> source = new List<NPC>();
        for (int index = 0; index < 200; ++index)
        {
          NPC npc = Main.npc[index];
          if (npc.active && npc.townNPC && (npc.type != 37 && npc.type != 453) && npc.aiStyle != 0)
            source.Add(npc);
        }
        if (source.Count < 5)
          return;
        BirthdayParty.GenuineParty = true;
        BirthdayParty.PartyDaysOnCooldown = Main.rand.Next(5, 11);
        BirthdayParty.CelebratingNPCs.Clear();
        List<int> intList = new List<int>();
        int num = 1;
        if (Main.rand.Next(5) == 0 && source.Count > 12)
          num = 3;
        else if (Main.rand.Next(3) == 0)
          num = 2;
        List<NPC> list = source.OrderBy<NPC, int>((Func<NPC, int>) (i => Main.rand.Next())).ToList<NPC>();
        for (int index = 0; index < num; ++index)
          intList.Add(index);
        for (int index = 0; index < intList.Count; ++index)
          BirthdayParty.CelebratingNPCs.Add(list[intList[index]].whoAmI);
        Color color = new Color((int) byte.MaxValue, 0, 160);
        if (BirthdayParty.CelebratingNPCs.Count == 3)
          WorldGen.BroadcastText(Lang.misc[96] + Main.npc[BirthdayParty.CelebratingNPCs[0]].displayName + ", " + Main.npc[BirthdayParty.CelebratingNPCs[1]].displayName + " & " + Main.npc[BirthdayParty.CelebratingNPCs[2]].displayName + Lang.misc[98], color);
        else if (BirthdayParty.CelebratingNPCs.Count == 2)
          WorldGen.BroadcastText(Lang.misc[96] + Main.npc[BirthdayParty.CelebratingNPCs[0]].displayName + " & " + Main.npc[BirthdayParty.CelebratingNPCs[1]].displayName + Lang.misc[98], color);
        else
          WorldGen.BroadcastText(Lang.misc[96] + Main.npc[BirthdayParty.CelebratingNPCs[0]].displayName + Lang.misc[97], color);
      }
    }

    public static void ToggleManualParty()
    {
      bool partyIsUp = BirthdayParty.PartyIsUp;
      if (Main.netMode != 1)
        BirthdayParty.ManualParty = !BirthdayParty.ManualParty;
      else
        NetMessage.SendData(111, -1, -1, "", 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      if (partyIsUp == BirthdayParty.PartyIsUp || Main.netMode != 2)
        return;
      NetMessage.SendData(7, -1, -1, "", 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }

    public static void WorldClear()
    {
      BirthdayParty.ManualParty = false;
      BirthdayParty.GenuineParty = false;
      BirthdayParty.PartyDaysOnCooldown = 0;
      BirthdayParty.CelebratingNPCs.Clear();
      BirthdayParty._wasCelebrating = false;
    }

    public static void UpdateTime()
    {
      if (BirthdayParty._wasCelebrating != BirthdayParty.PartyIsUp)
      {
        if (Main.netMode != 2)
        {
          if (BirthdayParty.PartyIsUp)
            SkyManager.Instance.Activate("Party", new Vector2());
          else
            SkyManager.Instance.Deactivate("Party");
        }
        if (Main.netMode != 1 && BirthdayParty.CelebratingNPCs.Count > 0)
        {
          for (int index = 0; index < BirthdayParty.CelebratingNPCs.Count; ++index)
          {
            NPC npc = Main.npc[BirthdayParty.CelebratingNPCs[index]];
            if (!npc.active || !npc.townNPC || (npc.type == 37 || npc.type == 453) || npc.aiStyle == 0)
              BirthdayParty.CelebratingNPCs.RemoveAt(index);
          }
          if (BirthdayParty.CelebratingNPCs.Count == 0)
          {
            BirthdayParty.GenuineParty = false;
            if (!BirthdayParty.ManualParty)
            {
              Color color = new Color((int) byte.MaxValue, 0, 160);
              WorldGen.BroadcastText(Lang.misc[99], color);
              NetMessage.SendData(7, -1, -1, "", 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
          }
        }
      }
      BirthdayParty._wasCelebrating = BirthdayParty.PartyIsUp;
    }
  }
}
