// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.VertexColors
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

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
