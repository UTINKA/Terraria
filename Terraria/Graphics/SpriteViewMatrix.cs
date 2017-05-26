// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.SpriteViewMatrix
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Terraria.Graphics
{
  public class SpriteViewMatrix
  {
    private Vector2 _zoom = Vector2.get_One();
    private Vector2 _translation = Vector2.get_Zero();
    private Matrix _zoomMatrix = Matrix.get_Identity();
    private Matrix _transformationMatrix = Matrix.get_Identity();
    private bool _needsRebuild = true;
    private SpriteEffects _effects;
    private Matrix _effectMatrix;
    private GraphicsDevice _graphicsDevice;
    private Viewport _viewport;
    private bool _overrideSystemViewport;

    public Vector2 Zoom
    {
      get
      {
        return this._zoom;
      }
      set
      {
        if (!Vector2.op_Inequality(this._zoom, value))
          return;
        this._zoom = value;
        this._needsRebuild = true;
      }
    }

    public Vector2 Translation
    {
      get
      {
        if (this.ShouldRebuild())
          this.Rebuild();
        return this._translation;
      }
    }

    public Matrix ZoomMatrix
    {
      get
      {
        if (this.ShouldRebuild())
          this.Rebuild();
        return this._zoomMatrix;
      }
    }

    public Matrix TransformationMatrix
    {
      get
      {
        if (this.ShouldRebuild())
          this.Rebuild();
        return this._transformationMatrix;
      }
    }

    public SpriteEffects Effects
    {
      get
      {
        return this._effects;
      }
      set
      {
        if (this._effects == value)
          return;
        this._effects = value;
        this._needsRebuild = true;
      }
    }

    public Matrix EffectMatrix
    {
      get
      {
        if (this.ShouldRebuild())
          this.Rebuild();
        return this._effectMatrix;
      }
    }

    public SpriteViewMatrix(GraphicsDevice graphicsDevice)
    {
      this._graphicsDevice = graphicsDevice;
    }

    private void Rebuild()
    {
      if (!this._overrideSystemViewport)
        this._viewport = this._graphicsDevice.get_Viewport();
      Vector2 vector2_1;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_1).\u002Ector((float) ((Viewport) @this._viewport).get_Width(), (float) ((Viewport) @this._viewport).get_Height());
      Matrix matrix = Matrix.get_Identity();
      if (((Enum) (object) this._effects).HasFlag((Enum) (object) (SpriteEffects) 1))
        matrix = Matrix.op_Multiply(matrix, Matrix.op_Multiply(Matrix.CreateScale(-1f, 1f, 1f), Matrix.CreateTranslation((float) vector2_1.X, 0.0f, 0.0f)));
      if (((Enum) (object) this._effects).HasFlag((Enum) (object) (SpriteEffects) 2))
        matrix = Matrix.op_Multiply(matrix, Matrix.op_Multiply(Matrix.CreateScale(1f, -1f, 1f), Matrix.CreateTranslation(0.0f, (float) vector2_1.Y, 0.0f)));
      Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 0.5f);
      Vector2 zoom = this._zoom;
      Vector2 vector2_3 = Vector2.op_Division(vector2_2, zoom);
      Vector2 vector2_4 = Vector2.op_Subtraction(vector2_2, vector2_3);
      this._translation = vector2_4;
      this._zoomMatrix = Matrix.op_Multiply(Matrix.CreateTranslation((float) -vector2_4.X, (float) -vector2_4.Y, 0.0f), Matrix.CreateScale((float) this._zoom.X, (float) this._zoom.Y, 1f));
      this._effectMatrix = matrix;
      this._transformationMatrix = Matrix.op_Multiply(matrix, this._zoomMatrix);
      this._needsRebuild = false;
    }

    public void SetViewportOverride(Viewport viewport)
    {
      this._viewport = viewport;
      this._overrideSystemViewport = true;
    }

    public void ClearViewportOverride()
    {
      this._overrideSystemViewport = false;
    }

    private bool ShouldRebuild()
    {
      if (this._needsRebuild)
        return true;
      if (this._overrideSystemViewport)
        return false;
      Viewport viewport1 = this._graphicsDevice.get_Viewport();
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      if (((Viewport) @viewport1).get_Width() != ((Viewport) @this._viewport).get_Width())
        return true;
      Viewport viewport2 = this._graphicsDevice.get_Viewport();
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      return ((Viewport) @viewport2).get_Height() != ((Viewport) @this._viewport).get_Height();
    }
  }
}
