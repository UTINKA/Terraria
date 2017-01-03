// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.AchievementsSocialModule
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Steamworks;
using System;
using System.Threading;

namespace Terraria.Social.Steam
{
  public class AchievementsSocialModule : Terraria.Social.Base.AchievementsSocialModule
  {
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

    public override void UpdateIntStat(string name, int value)
    {
      int num;
      SteamUserStats.GetStat(name, ref num);
      if (num >= value)
        return;
      SteamUserStats.SetStat(name, value);
    }

    public override void UpdateFloatStat(string name, float value)
    {
      float num;
      SteamUserStats.GetStat(name, ref num);
      if ((double) num >= (double) value)
        return;
      SteamUserStats.SetStat(name, value);
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
