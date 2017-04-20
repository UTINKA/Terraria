// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.AchievementsSocialModule
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Steamworks;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Terraria.Social.Steam
{
  public class AchievementsSocialModule : Terraria.Social.Base.AchievementsSocialModule
  {
    private Dictionary<string, int> _intStatCache = new Dictionary<string, int>();
    private Dictionary<string, float> _floatStatCache = new Dictionary<string, float>();
    private const string FILE_NAME = "/achievements-steam.dat";
    private Callback<UserStatsReceived_t> _userStatsReceived;
    private bool _areStatsReceived;

    public override void Initialize()
    {
      // ISSUE: method pointer
      this._userStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate((object) this, __methodptr(OnUserStatsReceived)));
      SteamUserStats.RequestCurrentStats();
      while (!this._areStatsReceived)
      {
        CoreSocialModule.Pulse();
        Thread.Sleep(10);
      }
    }

    public override void Shutdown()
    {
      this.StoreStats();
    }

    public override bool IsAchievementCompleted(string name)
    {
      bool flag;
      if (SteamUserStats.GetAchievement(name, ref flag))
        return flag;
      return false;
    }

    public override byte[] GetEncryptionKey()
    {
      byte[] numArray = new byte[16];
      byte[] bytes = BitConverter.GetBytes((ulong) SteamUser.GetSteamID().m_SteamID);
      Array.Copy((Array) bytes, (Array) numArray, 8);
      Array.Copy((Array) bytes, 0, (Array) numArray, 8, 8);
      return numArray;
    }

    public override string GetSavePath()
    {
      return "/achievements-steam.dat";
    }

    private int GetIntStat(string name)
    {
      int num;
      if (this._intStatCache.TryGetValue(name, out num) || !SteamUserStats.GetStat(name, ref num))
        return num;
      this._intStatCache.Add(name, num);
      return num;
    }

    private float GetFloatStat(string name)
    {
      float num;
      if (this._floatStatCache.TryGetValue(name, out num) || !SteamUserStats.GetStat(name, ref num))
        return num;
      this._floatStatCache.Add(name, num);
      return num;
    }

    private bool SetFloatStat(string name, float value)
    {
      this._floatStatCache[name] = value;
      return SteamUserStats.SetStat(name, value);
    }

    public override void UpdateIntStat(string name, int value)
    {
      if (this.GetIntStat(name) >= value)
        return;
      this.SetIntStat(name, value);
    }

    private bool SetIntStat(string name, int value)
    {
      this._intStatCache[name] = value;
      return SteamUserStats.SetStat(name, value);
    }

    public override void UpdateFloatStat(string name, float value)
    {
      if ((double) this.GetFloatStat(name) >= (double) value)
        return;
      this.SetFloatStat(name, value);
    }

    public override void StoreStats()
    {
      SteamUserStats.StoreStats();
    }

    public override void CompleteAchievement(string name)
    {
      SteamUserStats.SetAchievement(name);
    }

    private void OnUserStatsReceived(UserStatsReceived_t results)
    {
      if (results.m_nGameID != 105600L || !CSteamID.op_Equality((CSteamID) results.m_steamIDUser, SteamUser.GetSteamID()))
        return;
      this._areStatsReceived = true;
    }
  }
}
