// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.Point16
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
  public struct Point16
  {
    public static Point16 Zero = new Point16(0, 0);
    public static Point16 NegativeOne = new Point16(-1, -1);
    public readonly short X;
    public readonly short Y;

    public Point16(Point point)
    {
      this.X = (short) point.X;
      this.Y = (short) point.Y;
    }

    public Point16(int X, int Y)
    {
      this.X = (short) X;
      this.Y = (short) Y;
    }

    public Point16(short X, short Y)
    {
      this.X = X;
      this.Y = Y;
    }

    public static bool operator ==(Point16 first, Point16 second)
    {
      if ((int) first.X == (int) second.X)
        return (int) first.Y == (int) second.Y;
      return false;
    }

    public static bool operator !=(Point16 first, Point16 second)
    {
      if ((int) first.X == (int) second.X)
        return (int) first.Y != (int) second.Y;
      return true;
    }

    public static Point16 Max(int firstX, int firstY, int secondX, int secondY)
    {
      return new Point16(firstX > secondX ? firstX : secondX, firstY > secondY ? firstY : secondY);
    }

    public Point16 Max(int compareX, int compareY)
    {
      return new Point16((int) this.X > compareX ? (int) this.X : compareX, (int) this.Y > compareY ? (int) this.Y : compareY);
    }

    public Point16 Max(Point16 compareTo)
    {
      return new Point16((int) this.X > (int) compareTo.X ? this.X : compareTo.X, (int) this.Y > (int) compareTo.Y ? this.Y : compareTo.Y);
    }

    public override bool Equals(object obj)
    {
      Point16 point16 = (Point16) obj;
      return (int) this.X == (int) point16.X && (int) this.Y == (int) point16.Y;
    }

    public override int GetHashCode()
    {
      return (int) this.X << 16 | (int) (ushort) this.Y;
    }

    public override string ToString()
    {
      return string.Format("{{{0}, {1}}}", (object) this.X, (object) this.Y);
    }
  }
}
