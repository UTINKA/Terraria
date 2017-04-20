// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIScrollbar
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIScrollbar : UIElement
  {
    private float _viewSize = 1f;
    private float _maxViewSize = 20f;
    private float _viewPosition;
    private bool _isDragging;
    private bool _isHoveringOverHandle;
    private float _dragYOffset;
    private Texture2D _texture;
    private Texture2D _innerTexture;

    public float ViewPosition
    {
      get
      {
        return this._viewPosition;
      }
      set
      {
        this._viewPosition = MathHelper.Clamp(value, 0.0f, this._maxViewSize - this._viewSize);
      }
    }

    public UIScrollbar()
    {
      this.Width.Set(20f, 0.0f);
      this.MaxWidth.Set(20f, 0.0f);
      this._texture = TextureManager.Load("Images/UI/Scrollbar");
      this._innerTexture = TextureManager.Load("Images/UI/ScrollbarInner");
      this.PaddingTop = 5f;
      this.PaddingBottom = 5f;
    }

    public void SetView(float viewSize, float maxViewSize)
    {
      viewSize = MathHelper.Clamp(viewSize, 0.0f, maxViewSize);
      this._viewPosition = MathHelper.Clamp(this._viewPosition, 0.0f, maxViewSize - viewSize);
      this._viewSize = viewSize;
      this._maxViewSize = maxViewSize;
    }

    public float GetValue()
    {
      return this._viewPosition;
    }

    private Rectangle GetHandleRectangle()
    {
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      if ((double) this._maxViewSize == 0.0 && (double) this._viewSize == 0.0)
      {
        this._viewSize = 1f;
        this._maxViewSize = 1f;
      }
      return new Rectangle((int) innerDimensions.X, (int) ((double) innerDimensions.Y + (double) innerDimensions.Height * ((double) this._viewPosition / (double) this._maxViewSize)) - 3, 20, (int) ((double) innerDimensions.Height * ((double) this._viewSize / (double) this._maxViewSize)) + 7);
    }

    private void DrawBar(SpriteBatch spriteBatch, Texture2D texture, Rectangle dimensions, Color color)
    {
      spriteBatch.Draw(texture, new Rectangle((int) dimensions.X, dimensions.Y - 6, (int) dimensions.Width, 6), new Rectangle?(new Rectangle(0, 0, texture.get_Width(), 6)), color);
      spriteBatch.Draw(texture, new Rectangle((int) dimensions.X, (int) dimensions.Y, (int) dimensions.Width, (int) dimensions.Height), new Rectangle?(new Rectangle(0, 6, texture.get_Width(), 4)), color);
      spriteBatch.Draw(texture, new Rectangle((int) dimensions.X, (int) (dimensions.Y + dimensions.Height), (int) dimensions.Width, 6), new Rectangle?(new Rectangle(0, texture.get_Height() - 6, texture.get_Width(), 6)), color);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      if (this._isDragging)
        this._viewPosition = MathHelper.Clamp(((float) UserInterface.ActiveInstance.MousePosition.Y - innerDimensions.Y - this._dragYOffset) / innerDimensions.Height * this._maxViewSize, 0.0f, this._maxViewSize - this._viewSize);
      Rectangle handleRectangle = this.GetHandleRectangle();
      Vector2 mousePosition = UserInterface.ActiveInstance.MousePosition;
      bool hoveringOverHandle = this._isHoveringOverHandle;
      // ISSUE: explicit reference operation
      this._isHoveringOverHandle = ((Rectangle) @handleRectangle).Contains(new Point((int) mousePosition.X, (int) mousePosition.Y));
      if (!hoveringOverHandle && this._isHoveringOverHandle && Main.hasFocus)
        Main.PlaySound(12, -1, -1, 1, 1f, 0.0f);
      this.DrawBar(spriteBatch, this._texture, dimensions.ToRectangle(), Color.get_White());
      this.DrawBar(spriteBatch, this._innerTexture, handleRectangle, Color.op_Multiply(Color.get_White(), this._isDragging || this._isHoveringOverHandle ? 1f : 0.85f));
    }

    public override void MouseDown(UIMouseEvent evt)
    {
      base.MouseDown(evt);
      if (evt.Target != this)
        return;
      Rectangle handleRectangle = this.GetHandleRectangle();
      // ISSUE: explicit reference operation
      if (((Rectangle) @handleRectangle).Contains(new Point((int) evt.MousePosition.X, (int) evt.MousePosition.Y)))
      {
        this._isDragging = true;
        this._dragYOffset = (float) evt.MousePosition.Y - (float) handleRectangle.Y;
      }
      else
      {
        CalculatedStyle innerDimensions = this.GetInnerDimensions();
        this._viewPosition = MathHelper.Clamp(((float) UserInterface.ActiveInstance.MousePosition.Y - innerDimensions.Y - (float) (handleRectangle.Height >> 1)) / innerDimensions.Height * this._maxViewSize, 0.0f, this._maxViewSize - this._viewSize);
      }
    }

    public override void MouseUp(UIMouseEvent evt)
    {
      base.MouseUp(evt);
      this._isDragging = false;
    }
  }
}
