// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenShape
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.World.Generation
{
  public abstract class GenShape : GenBase
  {
    private ShapeData _outputData;
    protected bool _quitOnFail;

    public abstract bool Perform(Point origin, GenAction action);

    protected bool UnitApply(GenAction action, Point origin, int x, int y, params object[] args)
    {
      if (this._outputData != null)
        this._outputData.Add(x - origin.X, y - origin.Y);
      return action.Apply(origin, x, y, args);
    }

    public GenShape Output(ShapeData outputData)
    {
      this._outputData = outputData;
      return this;
    }

    public GenShape QuitOnFail(bool value = true)
    {
      this._quitOnFail = value;
      return this;
    }
  }
}
