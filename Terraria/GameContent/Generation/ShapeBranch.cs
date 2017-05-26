// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Generation.ShapeBranch
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.World.Generation;

namespace Terraria.GameContent.Generation
{
  public class ShapeBranch : GenShape
  {
    private Point _offset;
    private List<Point> _endPoints;

    public ShapeBranch()
    {
      this._offset = new Point(10, -5);
    }

    public ShapeBranch(Point offset)
    {
      this._offset = offset;
    }

    public ShapeBranch(double angle, double distance)
    {
      this._offset = new Point((int) (Math.Cos(angle) * distance), (int) (Math.Sin(angle) * distance));
    }

    private bool PerformSegment(Point origin, GenAction action, Point start, Point end, int size)
    {
      size = Math.Max(1, size);
      for (int index1 = -(size >> 1); index1 < size - (size >> 1); ++index1)
      {
        for (int index2 = -(size >> 1); index2 < size - (size >> 1); ++index2)
        {
          if (!Utils.PlotLine(new Point(start.X + index1, start.Y + index2), end, (Utils.PerLinePoint) ((tileX, tileY) =>
          {
            if (!this.UnitApply(action, origin, tileX, tileY))
              return !this._quitOnFail;
            return true;
          }), false))
            return false;
        }
      }
      return true;
    }

    public override bool Perform(Point origin, GenAction action)
    {
      Vector2 vector2;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2).\u002Ector((float) this._offset.X, (float) this._offset.Y);
      // ISSUE: explicit reference operation
      float num1 = ((Vector2) @vector2).Length();
      int size = (int) ((double) num1 / 6.0);
      if (this._endPoints != null)
        this._endPoints.Add(new Point((int) (origin.X + this._offset.X), (int) (origin.Y + this._offset.Y)));
      if (!this.PerformSegment(origin, action, origin, new Point((int) (origin.X + this._offset.X), (int) (origin.Y + this._offset.Y)), size))
        return false;
      int num2 = (int) ((double) num1 / 8.0);
      for (int index = 0; index < num2; ++index)
      {
        float num3 = (float) (((double) index + 1.0) / ((double) num2 + 1.0));
        Point point1;
        // ISSUE: explicit reference operation
        ((Point) @point1).\u002Ector((int) ((double) num3 * (double) (float) this._offset.X), (int) ((double) num3 * (double) (float) this._offset.Y));
        Vector2 spinningpoint;
        // ISSUE: explicit reference operation
        ((Vector2) @spinningpoint).\u002Ector((float) (this._offset.X - point1.X), (float) (this._offset.Y - point1.Y));
        spinningpoint = Vector2.op_Multiply(spinningpoint.RotatedBy((GenBase._random.NextDouble() * 0.5 + 1.0) * (GenBase._random.Next(2) == 0 ? -1.0 : 1.0), (Vector2) null), 0.75f);
        Point point2;
        // ISSUE: explicit reference operation
        ((Point) @point2).\u002Ector((int) spinningpoint.X + point1.X, (int) spinningpoint.Y + point1.Y);
        if (this._endPoints != null)
          this._endPoints.Add(new Point((int) (point2.X + origin.X), (int) (point2.Y + origin.Y)));
        if (!this.PerformSegment(origin, action, new Point((int) (point1.X + origin.X), (int) (point1.Y + origin.Y)), new Point((int) (point2.X + origin.X), (int) (point2.Y + origin.Y)), size - 1))
          return false;
      }
      return true;
    }

    public ShapeBranch OutputEndpoints(List<Point> endpoints)
    {
      this._endPoints = endpoints;
      return this;
    }
  }
}
