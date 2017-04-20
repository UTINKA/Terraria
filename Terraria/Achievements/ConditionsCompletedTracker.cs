// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.ConditionsCompletedTracker
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

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
