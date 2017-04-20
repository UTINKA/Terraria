// Decompiled with JetBrains decompiler
// Type: Terraria.Enums.AnchorType
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System;

namespace Terraria.Enums
{
  [Flags]
  public enum AnchorType
  {
    None = 0,
    SolidTile = 1,
    SolidWithTop = 2,
    Table = 4,
    SolidSide = 8,
    Tree = 16,
    AlternateTile = 32,
    EmptyTile = 64,
    SolidBottom = 128,
  }
}
