// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Achievements.CustomFlagCondition
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
