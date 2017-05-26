// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Generation.ShapeFloodFill
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.World.Generation;

namespace Terraria.GameContent.Generation
{
  public class ShapeFloodFill : GenShape
  {
    private int _maximumActions;

    public ShapeFloodFill(int maximumActions = 100)
    {
      this._maximumActions = maximumActions;
    }

    public override bool Perform(Point origin, GenAction action)
    {
      Queue<Point> pointQueue = new Queue<Point>();
      HashSet<Point16> point16Set = new HashSet<Point16>();
      pointQueue.Enqueue(origin);
      int maximumActions = this._maximumActions;
      while (pointQueue.Count > 0 && maximumActions > 0)
      {
        Point point = pointQueue.Dequeue();
        if (!point16Set.Contains(new Point16((int) point.X, (int) point.Y)) && this.UnitApply(action, origin, (int) point.X, (int) point.Y))
        {
          point16Set.Add(new Point16(point));
          --maximumActions;
          if (point.X + 1 < Main.maxTilesX - 1)
            pointQueue.Enqueue(new Point(point.X + 1, (int) point.Y));
          if (point.X - 1 >= 1)
            pointQueue.Enqueue(new Point(point.X - 1, (int) point.Y));
          if (point.Y + 1 < Main.maxTilesY - 1)
            pointQueue.Enqueue(new Point((int) point.X, point.Y + 1));
          if (point.Y - 1 >= 1)
            pointQueue.Enqueue(new Point((int) point.X, point.Y - 1));
        }
      }
      while (pointQueue.Count > 0)
      {
        Point point = pointQueue.Dequeue();
        if (!point16Set.Contains(new Point16((int) point.X, (int) point.Y)))
        {
          pointQueue.Enqueue(point);
          break;
        }
      }
      return pointQueue.Count == 0;
    }
  }
}
