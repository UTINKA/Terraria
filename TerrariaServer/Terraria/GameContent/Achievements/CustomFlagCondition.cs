// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Achievements.CustomFlagCondition
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
  public class CustomFlagCondition : AchievementCondition
  {
    private CustomFlagCondition(string name)
      : base(name)
    {
    }

    public static AchievementCondition Create(string name)
    {
      return (AchievementCondition) new CustomFlagCondition(name);
    }
  }
}
