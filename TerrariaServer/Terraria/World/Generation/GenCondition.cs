// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenCondition
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

namespace Terraria.World.Generation
{
  public abstract class GenCondition : GenBase
  {
    private GenCondition.AreaType _areaType = GenCondition.AreaType.None;
    private bool InvertResults;
    private int _width;
    private int _height;

    public bool IsValid(int x, int y)
    {
      switch (this._areaType)
      {
        case GenCondition.AreaType.And:
          for (int x1 = x; x1 < x + this._width; ++x1)
          {
            for (int y1 = y; y1 < y + this._height; ++y1)
            {
              if (!this.CheckValidity(x1, y1))
                return this.InvertResults;
            }
          }
          return !this.InvertResults;
        case GenCondition.AreaType.Or:
          for (int x1 = x; x1 < x + this._width; ++x1)
          {
            for (int y1 = y; y1 < y + this._height; ++y1)
            {
              if (this.CheckValidity(x1, y1))
                return !this.InvertResults;
            }
          }
          return this.InvertResults;
        case GenCondition.AreaType.None:
          return this.CheckValidity(x, y) ^ this.InvertResults;
        default:
          return true;
      }
    }

    public GenCondition Not()
    {
      this.InvertResults = !this.InvertResults;
      return this;
    }

    public GenCondition AreaOr(int width, int height)
    {
      this._areaType = GenCondition.AreaType.Or;
      this._width = width;
      this._height = height;
      return this;
    }

    public GenCondition AreaAnd(int width, int height)
    {
      this._areaType = GenCondition.AreaType.And;
      this._width = width;
      this._height = height;
      return this;
    }

    protected abstract bool CheckValidity(int x, int y);

    private enum AreaType
    {
      And,
      Or,
      None,
    }
  }
}
