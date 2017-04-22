// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.ConditionFloatTracker
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
