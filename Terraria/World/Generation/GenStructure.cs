// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenStructure
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.World.Generation
{
  public abstract class GenStructure : GenBase
  {
    public abstract bool Place(Point origin, StructureMap structures);
  }
}
