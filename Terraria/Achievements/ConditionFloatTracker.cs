// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.ConditionFloatTracker
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Terraria.Social;

namespace Terraria.Achievements
{
  public class ConditionFloatTracker : AchievementTracker<float>
  {
    public ConditionFloatTracker(float maxValue)
      : base(TrackerType.Float)
    {
      this._maxValue = maxValue;
    }

    public ConditionFloatTracker()
      : base(TrackerType.Float)
    {
    }

    public override void ReportUpdate()
    {
      if (SocialAPI.Achievements == null || this._name == null)
        return;
      SocialAPI.Achievements.UpdateFloatStat(this._name, this._value);
    }

    protected override void Load()
    {
    }
  }
}
