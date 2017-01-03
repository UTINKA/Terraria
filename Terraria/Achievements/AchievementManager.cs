// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.AchievementManager
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria.Achievements
{
  public class AchievementManager
  {
    private static object _ioLock = new object();
    private Dictionary<string, Achievement> _achievements = new Dictionary<string, Achievement>();
    private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings();
    private Dictionary<string, int> _achievementIconIndexes = new Dictionary<string, int>();
    private string _savePath;
    private bool _isCloudSave;
    private byte[] _cryptoKey;

    public event Achievement.AchievementCompleted OnAchievementCompleted;

    public AchievementManager()
    {
      if (SocialAPI.Achievements != null)
      {
        this._savePath = SocialAPI.Achievements.GetSavePath();
        this._isCloudSave = true;
        this._cryptoKey = SocialAPI.Achievements.GetEncryptionKey();
      }
      else
      {
        this._savePath = Main.SavePath + (object) Path.DirectorySeparatorChar + "achievements.dat";
        this._isCloudSave = false;
        this._cryptoKey = Encoding.ASCII.GetBytes("RELOGIC-TERRARIA");
      }
    }

    public void Save()
    {
      this.Save(this._savePath, this._isCloudSave);
    }

    private void Save(string path, bool cloud)
    {
      lock (AchievementManager._ioLock)
      {
        if (SocialAPI.Achievements != null)
          SocialAPI.Achievements.StoreStats();
        try
        {
          using (MemoryStream resource_2 = new MemoryStream())
          {
            using (CryptoStream resource_1 = new CryptoStream((Stream) resource_2, new RijndaelManaged().CreateEncryptor(this._cryptoKey, this._cryptoKey), CryptoStreamMode.Write))
            {
              using (BsonWriter resource_0 = new BsonWriter((Stream) resource_1))
              {
                JsonSerializer.Create(this._serializerSettings).Serialize((JsonWriter) resource_0, (object) this._achievements);
                ((JsonWriter) resource_0).Flush();
                resource_1.FlushFinalBlock();
                FileUtilities.WriteAllBytes(path, resource_2.ToArray(), cloud);
              }
            }
          }
        }
        catch (Exception exception_0)
        {
        }
      }
    }

    public List<Achievement> CreateAchievementsList()
    {
      return this._achievements.Values.ToList<Achievement>();
    }

    public void Load()
    {
      this.Load(this._savePath, this._isCloudSave);
    }

    private void Load(string path, bool cloud)
    {
      bool flag = false;
      lock (AchievementManager._ioLock)
      {
        if (!FileUtilities.Exists(path, cloud))
          return;
        byte[] local_1 = FileUtilities.ReadAllBytes(path, cloud);
        Dictionary<string, AchievementManager.StoredAchievement> local_2 = (Dictionary<string, AchievementManager.StoredAchievement>) null;
        try
        {
          using (MemoryStream resource_2 = new MemoryStream(local_1))
          {
            using (CryptoStream resource_1 = new CryptoStream((Stream) resource_2, new RijndaelManaged().CreateDecryptor(this._cryptoKey, this._cryptoKey), CryptoStreamMode.Read))
            {
              using (BsonReader resource_0 = new BsonReader((Stream) resource_1))
                local_2 = (Dictionary<string, AchievementManager.StoredAchievement>) JsonSerializer.Create(this._serializerSettings).Deserialize<Dictionary<string, AchievementManager.StoredAchievement>>((JsonReader) resource_0);
            }
          }
        }
        catch (Exception exception_0)
        {
          FileUtilities.Delete(path, cloud);
          return;
        }
        if (local_2 == null)
          return;
        foreach (KeyValuePair<string, AchievementManager.StoredAchievement> item_0 in local_2)
        {
          if (this._achievements.ContainsKey(item_0.Key))
            this._achievements[item_0.Key].Load(item_0.Value.Conditions);
        }
        if (SocialAPI.Achievements != null)
        {
          foreach (KeyValuePair<string, Achievement> item_1 in this._achievements)
          {
            if (item_1.Value.IsCompleted && !SocialAPI.Achievements.IsAchievementCompleted(item_1.Key))
            {
              flag = true;
              item_1.Value.ClearProgress();
            }
          }
        }
      }
      if (!flag)
        return;
      this.Save();
    }

    private void AchievementCompleted(Achievement achievement)
    {
      this.Save();
      if (this.OnAchievementCompleted == null)
        return;
      this.OnAchievementCompleted(achievement);
    }

    public void Register(Achievement achievement)
    {
      this._achievements.Add(achievement.Name, achievement);
      achievement.OnCompleted += new Achievement.AchievementCompleted(this.AchievementCompleted);
    }

    public void RegisterIconIndex(string achievementName, int iconIndex)
    {
      this._achievementIconIndexes.Add(achievementName, iconIndex);
    }

    public void RegisterAchievementCategory(string achievementName, AchievementCategory category)
    {
      this._achievements[achievementName].SetCategory(category);
    }

    public Achievement GetAchievement(string achievementName)
    {
      Achievement achievement;
      if (this._achievements.TryGetValue(achievementName, out achievement))
        return achievement;
      return (Achievement) null;
    }

    public T GetCondition<T>(string achievementName, string conditionName) where T : AchievementCondition
    {
      return this.GetCondition(achievementName, conditionName) as T;
    }

    public AchievementCondition GetCondition(string achievementName, string conditionName)
    {
      Achievement achievement;
      if (this._achievements.TryGetValue(achievementName, out achievement))
        return achievement.GetCondition(conditionName);
      return (AchievementCondition) null;
    }

    public int GetIconIndex(string achievementName)
    {
      int num;
      if (this._achievementIconIndexes.TryGetValue(achievementName, out num))
        return num;
      return 0;
    }

    private class StoredAchievement
    {
      public Dictionary<string, JObject> Conditions;
    }
  }
}
