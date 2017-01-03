// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Generation.ActionStalagtite
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.World.Generation;

namespace Terraria.GameContent.Generation
{
  public class ActionStalagtite : GenAction
  {
    public override bool Apply(Point origin, int x, int y, params object[] args)
    {
      WorldGen.PlaceTight(x, y, (ushort) 165, false);
      return this.UnitApply(origin, x, y, args);
    }
  }
}
