// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.VertexColors
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

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
