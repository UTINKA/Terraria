// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenStructure
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.World.Generation
{
  public abstract class GenStructure : GenBase
  {
    public abstract bool Place(Point origin, StructureMap structures);
  }
}
