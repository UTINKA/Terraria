// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Achievements.CustomIntCondition
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
  public class CustomIntCondition : AchievementCondition
  {
    [JsonProperty("Value")]
    private int _value;
    private int _maxValue;

    public int Value
    {
      get
      {
        return this._value;
      }
      set
      {
        int newValue = Utils.Clamp<int>(value, 0, this._maxValue);
        if (this._tracker != null)
          ((AchievementTracker<int>) this._tracker).SetValue(newValue, true);
        this._value = newValue;
        if (this._value != this._maxValue)
          return;
        this.Complete();
      }
    }

    private CustomIntCondition(string name, int maxValue)
      : base(name)
    {
      this._maxValue = maxValue;
      this._value = 0;
    }

    public override void Clear()
    {
      this._value = 0;
      base.Clear();
    }

    public override void Load(JObject state)
    {
      base.Load(state);
      this._value = JToken.op_Explicit(state.get_Item("Value"));
      if (this._tracker == null)
        return;
      ((AchievementTracker<int>) this._tracker).SetValue(this._value, false);
    }

    protected override IAchievementTracker CreateAchievementTracker()
    {
      return (IAchievementTracker) new ConditionIntTracker(this._maxValue);
    }

    public static AchievementCondition Create(string name, int maxValue)
    {
      return (AchievementCondition) new CustomIntCondition(name, maxValue);
    }
  }
}
