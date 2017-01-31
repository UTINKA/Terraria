// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Generation.ActionStalagtite
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

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
