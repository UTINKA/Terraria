// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenAction
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
