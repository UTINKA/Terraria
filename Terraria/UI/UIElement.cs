// Decompiled with JetBrains decompiler
// Type: Terraria.UI.UIElement
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;

namespace Terraria.UI
{
  public class UIElement : IComparable
  {
    public string Id = "";
    protected List<UIElement> Elements = new List<UIElement>();
    public StyleDimension MaxWidth = StyleDimension.Fill;
    public StyleDimension MaxHeight = StyleDimension.Fill;
    public StyleDimension MinWidth = StyleDimension.Empty;
    public StyleDimension MinHeight = StyleDimension.Empty;
    public UIElement Parent;
    public StyleDimension Top;
    public StyleDimension Left;
    public StyleDimension Width;
    public StyleDimension Height;
    private bool _isInitialized;
    public bool OverflowHidden;
    public float PaddingTop;
    public float PaddingLeft;
    public float PaddingRight;
    public float PaddingBottom;
    public float MarginTop;
    public float MarginLeft;
    public float MarginRight;
    public float MarginBottom;
    public float HAlign;
    public float VAlign;
    private CalculatedStyle _innerDimensions;
    private CalculatedStyle _dimensions;
    private CalculatedStyle _outerDimensions;
    private static RasterizerState _overflowHiddenRasterizerState;
    protected bool _useImmediateMode;
    private SnapPoint _snapPoint;
    private bool _isMouseHovering;

    public bool IsMouseHovering
    {
      get
      {
        return this._isMouseHovering;
      }
    }

    public event UIElement.MouseEvent OnMouseDown;

    public event UIElement.MouseEvent OnMouseUp;

    public event UIElement.MouseEvent OnClick;

    public event UIElement.MouseEvent OnMouseOver;

    public event UIElement.MouseEvent OnMouseOut;

    public event UIElement.MouseEvent OnDoubleClick;

    public event UIElement.ScrollWheelEvent OnScrollWheel;

    public UIElement()
    {
      if (UIElement._overflowHiddenRasterizerState != null)
        return;
      RasterizerState rasterizerState = new RasterizerState();
      int num1 = 0;
      rasterizerState.set_CullMode((CullMode) num1);
      int num2 = 1;
      rasterizerState.set_ScissorTestEnable(num2 != 0);
      UIElement._overflowHiddenRasterizerState = rasterizerState;
    }

    public void SetSnapPoint(string name, int id, Vector2? anchor = null, Vector2? offset = null)
    {
      if (!anchor.HasValue)
        anchor = new Vector2?(new Vector2(0.5f));
      if (!offset.HasValue)
        offset = new Vector2?(Vector2.get_Zero());
      this._snapPoint = new SnapPoint(name, id, anchor.Value, offset.Value);
    }

    public bool GetSnapPoint(out SnapPoint point)
    {
      point = this._snapPoint;
      if (this._snapPoint != null)
        this._snapPoint.Calculate(this);
      return this._snapPoint != null;
    }

    protected virtual void DrawSelf(SpriteBatch spriteBatch)
    {
    }

    protected virtual void DrawChildren(SpriteBatch spriteBatch)
    {
      foreach (UIElement element in this.Elements)
        element.Draw(spriteBatch);
    }

    public void Append(UIElement element)
    {
      element.Remove();
      element.Parent = this;
      this.Elements.Add(element);
      element.Recalculate();
    }

    public void Remove()
    {
      if (this.Parent == null)
        return;
      this.Parent.RemoveChild(this);
    }

    public void RemoveChild(UIElement child)
    {
      this.Elements.Remove(child);
      child.Parent = (UIElement) null;
    }

    public void RemoveAllChildren()
    {
      foreach (UIElement element in this.Elements)
        element.Parent = (UIElement) null;
      this.Elements.Clear();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      int num1 = this.OverflowHidden ? 1 : 0;
      int num2 = this._useImmediateMode ? 1 : 0;
      RasterizerState rasterizerState = ((GraphicsResource) spriteBatch).get_GraphicsDevice().get_RasterizerState();
      Rectangle scissorRectangle = ((GraphicsResource) spriteBatch).get_GraphicsDevice().get_ScissorRectangle();
      SamplerState anisotropicClamp = (SamplerState) SamplerState.AnisotropicClamp;
      if (num2 != 0)
      {
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 1, (BlendState) BlendState.AlphaBlend, anisotropicClamp, (DepthStencilState) DepthStencilState.None, UIElement._overflowHiddenRasterizerState, (Effect) null, Main.UIScaleMatrix);
        this.DrawSelf(spriteBatch);
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 0, (BlendState) BlendState.AlphaBlend, anisotropicClamp, (DepthStencilState) DepthStencilState.None, UIElement._overflowHiddenRasterizerState, (Effect) null, Main.UIScaleMatrix);
      }
      else
        this.DrawSelf(spriteBatch);
      if (num1 != 0)
      {
        spriteBatch.End();
        Rectangle clippingRectangle = this.GetClippingRectangle(spriteBatch);
        ((GraphicsResource) spriteBatch).get_GraphicsDevice().set_ScissorRectangle(clippingRectangle);
        spriteBatch.Begin((SpriteSortMode) 0, (BlendState) BlendState.AlphaBlend, anisotropicClamp, (DepthStencilState) DepthStencilState.None, UIElement._overflowHiddenRasterizerState, (Effect) null, Main.UIScaleMatrix);
      }
      this.DrawChildren(spriteBatch);
      if (num1 == 0)
        return;
      spriteBatch.End();
      ((GraphicsResource) spriteBatch).get_GraphicsDevice().set_ScissorRectangle(scissorRectangle);
      spriteBatch.Begin((SpriteSortMode) 0, (BlendState) BlendState.AlphaBlend, anisotropicClamp, (DepthStencilState) DepthStencilState.None, rasterizerState, (Effect) null, Main.UIScaleMatrix);
    }

    public virtual void Update(GameTime gameTime)
    {
      foreach (UIElement element in this.Elements)
        element.Update(gameTime);
    }

    public Rectangle GetClippingRectangle(SpriteBatch spriteBatch)
    {
      Vector2 vector2_1;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_1).\u002Ector(this._innerDimensions.X, this._innerDimensions.Y);
      Vector2 vector2_2 = Vector2.op_Addition(new Vector2(this._innerDimensions.Width, this._innerDimensions.Height), vector2_1);
      Vector2 vector2_3 = Vector2.Transform(vector2_1, Main.UIScaleMatrix);
      Vector2 vector2_4 = Vector2.Transform(vector2_2, Main.UIScaleMatrix);
      Rectangle rectangle;
      // ISSUE: explicit reference operation
      ((Rectangle) @rectangle).\u002Ector((int) vector2_3.X, (int) vector2_3.Y, (int) (vector2_4.X - vector2_3.X), (int) (vector2_4.Y - vector2_3.Y));
      Viewport viewport1 = ((GraphicsResource) spriteBatch).get_GraphicsDevice().get_Viewport();
      // ISSUE: explicit reference operation
      int width = ((Viewport) @viewport1).get_Width();
      Viewport viewport2 = ((GraphicsResource) spriteBatch).get_GraphicsDevice().get_Viewport();
      // ISSUE: explicit reference operation
      int height = ((Viewport) @viewport2).get_Height();
      rectangle.X = (__Null) Utils.Clamp<int>((int) rectangle.X, 0, width);
      rectangle.Y = (__Null) Utils.Clamp<int>((int) rectangle.Y, 0, height);
      rectangle.Width = (__Null) Utils.Clamp<int>((int) rectangle.Width, 0, width - rectangle.X);
      rectangle.Height = (__Null) Utils.Clamp<int>((int) rectangle.Height, 0, height - rectangle.Y);
      return rectangle;
    }

    public virtual List<SnapPoint> GetSnapPoints()
    {
      List<SnapPoint> snapPointList = new List<SnapPoint>();
      SnapPoint point;
      if (this.GetSnapPoint(out point))
        snapPointList.Add(point);
      foreach (UIElement element in this.Elements)
        snapPointList.AddRange((IEnumerable<SnapPoint>) element.GetSnapPoints());
      return snapPointList;
    }

    public virtual void Recalculate()
    {
      CalculatedStyle calculatedStyle1 = this.Parent == null ? UserInterface.ActiveInstance.GetDimensions() : this.Parent.GetInnerDimensions();
      if (this.Parent != null && this.Parent is UIList)
        calculatedStyle1.Height = float.MaxValue;
      CalculatedStyle calculatedStyle2;
      calculatedStyle2.X = this.Left.GetValue(calculatedStyle1.Width) + calculatedStyle1.X;
      calculatedStyle2.Y = this.Top.GetValue(calculatedStyle1.Height) + calculatedStyle1.Y;
      float num1 = this.MinWidth.GetValue(calculatedStyle1.Width);
      float num2 = this.MaxWidth.GetValue(calculatedStyle1.Width);
      float num3 = this.MinHeight.GetValue(calculatedStyle1.Height);
      float num4 = this.MaxHeight.GetValue(calculatedStyle1.Height);
      calculatedStyle2.Width = MathHelper.Clamp(this.Width.GetValue(calculatedStyle1.Width), num1, num2);
      calculatedStyle2.Height = MathHelper.Clamp(this.Height.GetValue(calculatedStyle1.Height), num3, num4);
      calculatedStyle2.Width += this.MarginLeft + this.MarginRight;
      calculatedStyle2.Height += this.MarginTop + this.MarginBottom;
      calculatedStyle2.X += (float) ((double) calculatedStyle1.Width * (double) this.HAlign - (double) calculatedStyle2.Width * (double) this.HAlign);
      calculatedStyle2.Y += (float) ((double) calculatedStyle1.Height * (double) this.VAlign - (double) calculatedStyle2.Height * (double) this.VAlign);
      this._outerDimensions = calculatedStyle2;
      calculatedStyle2.X += this.MarginLeft;
      calculatedStyle2.Y += this.MarginTop;
      calculatedStyle2.Width -= this.MarginLeft + this.MarginRight;
      calculatedStyle2.Height -= this.MarginTop + this.MarginBottom;
      this._dimensions = calculatedStyle2;
      calculatedStyle2.X += this.PaddingLeft;
      calculatedStyle2.Y += this.PaddingTop;
      calculatedStyle2.Width -= this.PaddingLeft + this.PaddingRight;
      calculatedStyle2.Height -= this.PaddingTop + this.PaddingBottom;
      this._innerDimensions = calculatedStyle2;
      this.RecalculateChildren();
    }

    public UIElement GetElementAt(Vector2 point)
    {
      UIElement uiElement = (UIElement) null;
      foreach (UIElement element in this.Elements)
      {
        if (element.ContainsPoint(point))
        {
          uiElement = element;
          break;
        }
      }
      if (uiElement != null)
        return uiElement.GetElementAt(point);
      if (this.ContainsPoint(point))
        return this;
      return (UIElement) null;
    }

    public virtual bool ContainsPoint(Vector2 point)
    {
      if (point.X > (double) this._dimensions.X && point.Y > (double) this._dimensions.Y && point.X < (double) this._dimensions.X + (double) this._dimensions.Width)
        return point.Y < (double) this._dimensions.Y + (double) this._dimensions.Height;
      return false;
    }

    public void SetPadding(float pixels)
    {
      this.PaddingBottom = pixels;
      this.PaddingLeft = pixels;
      this.PaddingRight = pixels;
      this.PaddingTop = pixels;
    }

    public virtual void RecalculateChildren()
    {
      foreach (UIElement element in this.Elements)
        element.Recalculate();
    }

    public CalculatedStyle GetInnerDimensions()
    {
      return this._innerDimensions;
    }

    public CalculatedStyle GetDimensions()
    {
      return this._dimensions;
    }

    public CalculatedStyle GetOuterDimensions()
    {
      return this._outerDimensions;
    }

    public void CopyStyle(UIElement element)
    {
      this.Top = element.Top;
      this.Left = element.Left;
      this.Width = element.Width;
      this.Height = element.Height;
      this.PaddingBottom = element.PaddingBottom;
      this.PaddingLeft = element.PaddingLeft;
      this.PaddingRight = element.PaddingRight;
      this.PaddingTop = element.PaddingTop;
      this.HAlign = element.HAlign;
      this.VAlign = element.VAlign;
      this.MinWidth = element.MinWidth;
      this.MaxWidth = element.MaxWidth;
      this.MinHeight = element.MinHeight;
      this.MaxHeight = element.MaxHeight;
      this.Recalculate();
    }

    public virtual void MouseDown(UIMouseEvent evt)
    {
      // ISSUE: reference to a compiler-generated field
      if (this.OnMouseDown != null)
      {
        // ISSUE: reference to a compiler-generated field
        this.OnMouseDown(evt, this);
      }
      if (this.Parent == null)
        return;
      this.Parent.MouseDown(evt);
    }

    public virtual void MouseUp(UIMouseEvent evt)
    {
      // ISSUE: reference to a compiler-generated field
      if (this.OnMouseUp != null)
      {
        // ISSUE: reference to a compiler-generated field
        this.OnMouseUp(evt, this);
      }
      if (this.Parent == null)
        return;
      this.Parent.MouseUp(evt);
    }

    public virtual void MouseOver(UIMouseEvent evt)
    {
      this._isMouseHovering = true;
      // ISSUE: reference to a compiler-generated field
      if (this.OnMouseOver != null)
      {
        // ISSUE: reference to a compiler-generated field
        this.OnMouseOver(evt, this);
      }
      if (this.Parent == null)
        return;
      this.Parent.MouseOver(evt);
    }

    public virtual void MouseOut(UIMouseEvent evt)
    {
      this._isMouseHovering = false;
      // ISSUE: reference to a compiler-generated field
      if (this.OnMouseOut != null)
      {
        // ISSUE: reference to a compiler-generated field
        this.OnMouseOut(evt, this);
      }
      if (this.Parent == null)
        return;
      this.Parent.MouseOut(evt);
    }

    public virtual void Click(UIMouseEvent evt)
    {
      // ISSUE: reference to a compiler-generated field
      if (this.OnClick != null)
      {
        // ISSUE: reference to a compiler-generated field
        this.OnClick(evt, this);
      }
      if (this.Parent == null)
        return;
      this.Parent.Click(evt);
    }

    public virtual void DoubleClick(UIMouseEvent evt)
    {
      // ISSUE: reference to a compiler-generated field
      if (this.OnDoubleClick != null)
      {
        // ISSUE: reference to a compiler-generated field
        this.OnDoubleClick(evt, this);
      }
      if (this.Parent == null)
        return;
      this.Parent.DoubleClick(evt);
    }

    public virtual void ScrollWheel(UIScrollWheelEvent evt)
    {
      // ISSUE: reference to a compiler-generated field
      if (this.OnScrollWheel != null)
      {
        // ISSUE: reference to a compiler-generated field
        this.OnScrollWheel(evt, this);
      }
      if (this.Parent == null)
        return;
      this.Parent.ScrollWheel(evt);
    }

    public void Activate()
    {
      if (!this._isInitialized)
        this.Initialize();
      this.OnActivate();
      foreach (UIElement element in this.Elements)
        element.Activate();
    }

    public virtual void OnActivate()
    {
    }

    public void Deactivate()
    {
      this.OnDeactivate();
      foreach (UIElement element in this.Elements)
        element.Deactivate();
    }

    public virtual void OnDeactivate()
    {
    }

    public void Initialize()
    {
      this.OnInitialize();
      this._isInitialized = true;
    }

    public virtual void OnInitialize()
    {
    }

    public virtual int CompareTo(object obj)
    {
      return 0;
    }

    public delegate void MouseEvent(UIMouseEvent evt, UIElement listeningElement);

    public delegate void ScrollWheelEvent(UIScrollWheelEvent evt, UIElement listeningElement);
  }
}
