// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenSearch
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;

namespace Terraria.World.Generation
{
  public abstract class GenSearch : GenBase
  {
    public static Point NOT_FOUND = new Point(int.MaxValue, int.MaxValue);
    private bool _requireAll = true;
    private GenCondition[] _conditions;

    public GenSearch Conditions(params GenCondition[] conditions)
    {
      this._conditions = conditions;
      return this;
    }

    public abstract Point Find(Point origin);

    protected bool Check(int x, int y)
    {
      for (int index = 0; index < this._conditions.Length; ++index)
      {
        if (this._requireAll ^ this._conditions[index].IsValid(x, y))
          return !this._requireAll;
      }
      return this._requireAll;
    }

    public GenSearch RequireAll(bool mode)
    {
      this._requireAll = mode;
      return this;
    }
  }
}
