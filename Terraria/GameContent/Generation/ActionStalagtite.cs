// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Generation.ActionStalagtite
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

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
