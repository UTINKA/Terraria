// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.AchievementCondition
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Terraria.Achievements
{
  [JsonObject]
  public abstract class AchievementCondition
  {
    public readonly string Name;
    protected IAchievementTracker _tracker;
    [JsonProperty("Completed")]
    private bool _isCompleted;

    public bool IsCompleted
    {
      get
      {
        return this._isCompleted;
      }
    }

    public event AchievementCondition.AchievementUpdate OnComplete;

    protected AchievementCondition(string name)
    {
      this.Name = name;
    }

    public virtual void Load(JObject state)
    {
      this._isCompleted = JToken.op_Explicit(state.get_Item("Completed"));
    }

    public virtual void Clear()
    {
      this._isCompleted = false;
    }

    public virtual void Complete()
    {
      if (this._isCompleted)
        return;
      this._isCompleted = true;
      if (this.OnComplete == null)
        return;
      this.OnComplete(this);
    }

    protected virtual IAchievementTracker CreateAchievementTracker()
    {
      return (IAchievementTracker) null;
    }

    public IAchievementTracker GetAchievementTracker()
    {
      if (this._tracker == null)
        this._tracker = this.CreateAchievementTracker();
      return this._tracker;
    }

    public delegate void AchievementUpdate(AchievementCondition condition);
  }
}
