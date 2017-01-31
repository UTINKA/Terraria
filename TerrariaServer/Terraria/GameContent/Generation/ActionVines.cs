// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Generation.ActionVines
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Terraria.World.Generation;

namespace Terraria.GameContent.Generation
{
  public class ActionVines : GenAction
  {
    private int _minLength;
    private int _maxLength;
    private int _vineId;

    public ActionVines(int minLength = 6, int maxLength = 10, int vineId = 52)
    {
      this._minLength = minLength;
      this._maxLength = maxLength;
      this._vineId = vineId;
    }

    public override bool Apply(Point origin, int x, int y, params object[] args)
    {
      int num1 = GenBase._random.Next(this._minLength, this._maxLength + 1);
      int num2;
      for (num2 = 0; num2 < num1 && !GenBase._tiles[x, y + num2].active(); ++num2)
      {
        GenBase._tiles[x, y + num2].type = (ushort) this._vineId;
        GenBase._tiles[x, y + num2].active(true);
      }
      if (num2 > 0)
        return this.UnitApply(origin, x, y, args);
      return false;
    }
  }
}
