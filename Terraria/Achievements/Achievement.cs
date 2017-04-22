// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.Achievement
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.Social;

namespace Terraria.Achievements
{
  [JsonObject]
  public class Achievement
  {
    public readonly int Id = Achievement._totalAchievements++;
    [JsonProperty("Conditions")]
    private Dictionary<string, AchievementCondition> _conditions = new Dictionary<string, AchievementCondition>();
    private static int _totalAchievements;
    public readonly string Name;
    public readonly LocalizedText FriendlyName;
    public readonly LocalizedText Description;
    private AchievementCategory _category;
    private IAchievementTracker _tracker;
    private int _completedCount;

    public AchievementCategory Category
    {
      get
      {
        return this._category;
      }
    }

    public bool HasTracker
    {
      get
      {
        return this._tracker != null;
      }
    }

    public bool IsCompleted
    {
      get
      {
        return this._completedCount == this._conditions.Count;
      }
    }

    public event Achievement.AchievementCompleted OnCompleted;

    public Achievement(string name)
    {
      this.Name = name;
      this.FriendlyName = Language.GetText("Achievements." + name + "_Name");
      this.Description = Language.GetText("Achievements." + name + "_Description");
    }

    public IAchievementTracker GetTracker()
    {
      return this._tracker;
    }

    public void ClearProgress()
    {
      this._completedCount = 0;
      foreach (KeyValuePair<string, AchievementCondition> condition in this._conditions)
        condition.Value.Clear();
      if (this._tracker == null)
        return;
      this._tracker.Clear();
    }

    public void Load(Dictionary<string, JObject> conditions)
    {
      using (Dictionary<string, JObject>.Enumerator enumerator = conditions.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, JObject> current = enumerator.Current;
          AchievementCondition achievementCondition;
          if (this._conditions.TryGetValue(current.Key, out achievementCondition))
          {
            achievementCondition.Load(current.Value);
            if (achievementCondition.IsCompleted)
              ++this._completedCount;
          }
        }
      }
      if (this._tracker == null)
        return;
      this._tracker.Load();
    }

    public void AddCondition(AchievementCondition condition)
    {
      this._conditions[condition.Name] = condition;
      condition.OnComplete += new AchievementCondition.AchievementUpdate(this.OnConditionComplete);
    }

    private void OnConditionComplete(AchievementCondition condition)
    {
      ++this._completedCount;
      if (this._completedCount != this._conditions.Count)
        return;
      if (this._tracker == null && SocialAPI.Achievements != null)
        SocialAPI.Achievements.CompleteAchievement(this.Name);
      if (this.OnCompleted == null)
        return;
      this.OnCompleted(this);
    }

    private void UseTracker(IAchievementTracker tracker)
    {
      tracker.ReportAs("STAT_" + this.Name);
      this._tracker = tracker;
    }

    public void UseTrackerFromCondition(string conditionName)
    {
      this.UseTracker(this.GetConditionTracker(conditionName));
    }

    public void UseConditionsCompletedTracker()
    {
      ConditionsCompletedTracker completedTracker = new ConditionsCompletedTracker();
      foreach (KeyValuePair<string, AchievementCondition> condition in this._conditions)
        completedTracker.AddCondition(condition.Value);
      this.UseTracker((IAchievementTracker) completedTracker);
    }

    public void UseConditionsCompletedTracker(params string[] conditions)
    {
      ConditionsCompletedTracker completedTracker = new ConditionsCompletedTracker();
      for (int index = 0; index < conditions.Length; ++index)
      {
        string condition = conditions[index];
        completedTracker.AddCondition(this._conditions[condition]);
      }
      this.UseTracker((IAchievementTracker) completedTracker);
    }

    public void ClearTracker()
    {
      this._tracker = (IAchievementTracker) null;
    }

    private IAchievementTracker GetConditionTracker(string name)
    {
      return this._conditions[name].GetAchievementTracker();
    }

    public void AddConditions(params AchievementCondition[] conditions)
    {
      for (int index = 0; index < conditions.Length; ++index)
        this.AddCondition(conditions[index]);
    }

    public AchievementCondition GetCondition(string conditionName)
    {
      AchievementCondition achievementCondition;
      if (this._conditions.TryGetValue(conditionName, out achievementCondition))
        return achievementCondition;
      return (AchievementCondition) null;
    }

    public void SetCategory(AchievementCategory category)
    {
      this._category = category;
    }

    public delegate void AchievementCompleted(Achievement achievement);
  }
}
