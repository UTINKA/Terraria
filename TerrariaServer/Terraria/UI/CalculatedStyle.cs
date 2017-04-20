// Decompiled with JetBrains decompiler
// Type: Terraria.UI.CalculatedStyle
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;

namespace Terraria.UI
{
  public struct CalculatedStyle
  {
    public float X;
    public float Y;
    public float Width;
    public float Height;

    public CalculatedStyle(float x, float y, float width, float height)
    {
      this.X = x;
      this.Y = y;
      this.Width = width;
      this.Height = height;
    }

    public Rectangle ToRectangle()
    {
      return new Rectangle((int) this.X, (int) this.Y, (int) this.Width, (int) this.Height);
    }

    public Vector2 Position()
    {
      return new Vector2(this.X, this.Y);
    }

    public Vector2 Center()
    {
      return new Vector2(this.X + this.Width * 0.5f, this.Y + this.Height * 0.5f);
    }
  }
}
