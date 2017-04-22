// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.DrillDebugDraw
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
  public struct DrillDebugDraw
  {
    public Vector2 point;
    public Color color;

    public DrillDebugDraw(Vector2 p, Color c)
    {
      this.point = p;
      this.color = c;
    }
  }
}
