// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.VertexColors
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;

namespace Terraria.Graphics
{
  public struct VertexColors
  {
    public Color TopLeftColor;
    public Color TopRightColor;
    public Color BottomLeftColor;
    public Color BottomRightColor;

    public VertexColors(Color color)
    {
      this.TopLeftColor = color;
      this.TopRightColor = color;
      this.BottomRightColor = color;
      this.BottomLeftColor = color;
    }

    public VertexColors(Color topLeft, Color topRight, Color bottomRight, Color bottomLeft)
    {
      this.TopLeftColor = topLeft;
      this.TopRightColor = topRight;
      this.BottomLeftColor = bottomLeft;
      this.BottomRightColor = bottomRight;
    }
  }
}
