// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.ConditionsCompletedTracker
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using System;
using System.Collections.Generic;

namespace Terraria.Achievements
{
  public class ConditionsCompletedTracker : ConditionIntTracker
  {
    private List<AchievementCondition> _conditions = new List<AchievementCondition>();

    public void AddCondition(AchievementCondition condition)
    {
      ++this._maxValue;
      condition.OnComplete += new AchievementCondition.AchievementUpdate(this.OnConditionCompleted);
      this._conditions.Add(condition);
    }

    private void OnConditionCompleted(AchievementCondition condition)
    {
      this.SetValue(Math.Min(this._value + 1, this._maxValue), true);
    }

    protected override void Load()
    {
      for (int index = 0; index < this._conditions.Count; ++index)
      {
        if (this._conditions[index].IsCompleted)
          ++this._value;
      }
    }
  }
}
