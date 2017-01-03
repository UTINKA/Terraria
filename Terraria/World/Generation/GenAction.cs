// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenAction
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.World.Generation
{
  public abstract class GenAction : GenBase
  {
    private bool _returnFalseOnFailure = true;
    public GenAction NextAction;
    public ShapeData OutputData;

    public abstract bool Apply(Point origin, int x, int y, params object[] args);

    protected bool UnitApply(Point origin, int x, int y, params object[] args)
    {
      if (this.OutputData != null)
        this.OutputData.Add(x - origin.X, y - origin.Y);
      if (this.NextAction != null)
        return this.NextAction.Apply(origin, x, y, args);
      return true;
    }

    public GenAction IgnoreFailures()
    {
      this._returnFalseOnFailure = false;
      return this;
    }

    protected bool Fail()
    {
      return !this._returnFalseOnFailure;
    }

    public GenAction Output(ShapeData data)
    {
      this.OutputData = data;
      return this;
    }
  }
}
