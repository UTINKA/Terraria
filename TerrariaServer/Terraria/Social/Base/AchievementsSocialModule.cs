// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.AchievementsSocialModule
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

namespace Terraria.Social.Base
{
  public abstract class AchievementsSocialModule : ISocialModule
  {
    public abstract void Initialize();

    public abstract void Shutdown();

    public abstract byte[] GetEncryptionKey();

    public abstract string GetSavePath();

    public abstract void UpdateIntStat(string name, int value);

    public abstract void UpdateFloatStat(string name, float value);

    public abstract void CompleteAchievement(string name);

    public abstract bool IsAchievementCompleted(string name);

    public abstract void StoreStats();
  }
}
