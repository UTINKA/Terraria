// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.ConditionIntTracker
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Terraria.Social;

namespace Terraria.Achievements
{
  public class ConditionIntTracker : AchievementTracker<int>
  {
    public ConditionIntTracker()
      : base(TrackerType.Int)
    {
    }

    public ConditionIntTracker(int maxValue)
      : base(TrackerType.Int)
    {
      this._maxValue = maxValue;
    }

    public override void ReportUpdate()
    {
      if (SocialAPI.Achievements == null || this._name == null)
        return;
      SocialAPI.Achievements.UpdateIntStat(this._name, this._value);
    }

    protected override void Load()
    {
    }
  }
}
