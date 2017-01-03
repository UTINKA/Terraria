// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.IAchievementTracker
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

namespace Terraria.Achievements
{
  public interface IAchievementTracker
  {
    void ReportAs(string name);

    TrackerType GetTrackerType();

    void Load();

    void Clear();
  }
}
