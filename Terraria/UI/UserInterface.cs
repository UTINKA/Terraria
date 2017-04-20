// Decompiled with JetBrains decompiler
// Type: Terraria.UI.UserInterface
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.GameInput;

namespace Terraria.UI
{
  public class UserInterface
  {
    public static UserInterface ActiveInstance = new UserInterface();
    private List<UIState> _history = new List<UIState>();
    private const double DOUBLE_CLICK_TIME = 500.0;
    private const double STATE_CHANGE_CLICK_DISABLE_TIME = 200.0;
    private const int MAX_HISTORY_SIZE = 32;
    private const int HISTORY_PRUNE_SIZE = 4;
    public Vector2 MousePosition;
    private bool _wasMouseDown;
    private UIElement _lastElementHover;
    private UIElement _lastElementDown;
    private UIElement _lastElementClicked;
    private double _lastMouseDownTime;
    private double _clickDisabledTimeRemaining;
    public bool IsVisible;
    private UIState _currentState;

    public UIState CurrentState
    {
      get
      {
        return this._currentState;
      }
    }

    public UserInterface()
    {
      UserInterface.ActiveInstance = this;
    }

    public void ResetLasts()
    {
      this._lastElementHover = (UIElement) null;
      this._lastElementDown = (UIElement) null;
      this._lastElementClicked = (UIElement) null;
    }

    public void Use()
    {
      if (UserInterface.ActiveInstance != this)
      {
        UserInterface.ActiveInstance = this;
        this.Recalculate();
      }
      else
        UserInterface.ActiveInstance = this;
    }

    private void ResetState()
    {
      this.MousePosition = new Vector2((float) Main.mouseX, (float) Main.mouseY);
      this._wasMouseDown = Main.mouseLeft;
      if (this._lastElementHover != null)
        this._lastElementHover.MouseOut(new UIMouseEvent(this._lastElementHover, this.MousePosition));
      this._lastElementHover = (UIElement) null;
      this._lastElementDown = (UIElement) null;
      this._lastElementClicked = (UIElement) null;
      this._lastMouseDownTime = 0.0;
      this._clickDisabledTimeRemaining = Math.Max(this._clickDisabledTimeRemaining, 200.0);
    }

    public void Update(GameTime time)
    {
      if (this._currentState == null)
        return;
      this.MousePosition = new Vector2((float) Main.mouseX, (float) Main.mouseY);
      bool flag1 = Main.mouseLeft && Main.hasFocus;
      UIElement target = Main.hasFocus ? this._currentState.GetElementAt(this.MousePosition) : (UIElement) null;
      this._clickDisabledTimeRemaining = Math.Max(0.0, this._clickDisabledTimeRemaining - time.get_ElapsedGameTime().TotalMilliseconds);
      bool flag2 = this._clickDisabledTimeRemaining > 0.0;
      if (target != this._lastElementHover)
      {
        if (this._lastElementHover != null)
          this._lastElementHover.MouseOut(new UIMouseEvent(this._lastElementHover, this.MousePosition));
        if (target != null)
          target.MouseOver(new UIMouseEvent(target, this.MousePosition));
        this._lastElementHover = target;
      }
      if (flag1 && !this._wasMouseDown && (target != null && !flag2))
      {
        this._lastElementDown = target;
        target.MouseDown(new UIMouseEvent(target, this.MousePosition));
        if (this._lastElementClicked == target && time.get_TotalGameTime().TotalMilliseconds - this._lastMouseDownTime < 500.0)
        {
          target.DoubleClick(new UIMouseEvent(target, this.MousePosition));
          this._lastElementClicked = (UIElement) null;
        }
        this._lastMouseDownTime = time.get_TotalGameTime().TotalMilliseconds;
      }
      else if (!flag1 && this._wasMouseDown && (this._lastElementDown != null && !flag2))
      {
        UIElement lastElementDown = this._lastElementDown;
        if (lastElementDown.ContainsPoint(this.MousePosition))
        {
          lastElementDown.Click(new UIMouseEvent(lastElementDown, this.MousePosition));
          this._lastElementClicked = this._lastElementDown;
        }
        lastElementDown.MouseUp(new UIMouseEvent(lastElementDown, this.MousePosition));
        this._lastElementDown = (UIElement) null;
      }
      if (PlayerInput.ScrollWheelDeltaForUI != 0)
      {
        if (target != null)
          target.ScrollWheel(new UIScrollWheelEvent(target, this.MousePosition, PlayerInput.ScrollWheelDeltaForUI));
        PlayerInput.ScrollWheelDeltaForUI = 0;
      }
      this._wasMouseDown = flag1;
      if (this._currentState == null)
        return;
      this._currentState.Update(time);
    }

    public void Draw(SpriteBatch spriteBatch, GameTime time)
    {
      this.Use();
      if (this._currentState == null)
        return;
      this._currentState.Draw(spriteBatch);
    }

    public void SetState(UIState state)
    {
      if (state != null)
        this.AddToHistory(state);
      if (this._currentState != null)
        this._currentState.Deactivate();
      this._currentState = state;
      this.ResetState();
      if (state == null)
        return;
      state.Activate();
      state.Recalculate();
    }

    public void GoBack()
    {
      if (this._history.Count < 2)
        return;
      UIState state = this._history[this._history.Count - 2];
      this._history.RemoveRange(this._history.Count - 2, 2);
      this.SetState(state);
    }

    private void AddToHistory(UIState state)
    {
      this._history.Add(state);
      if (this._history.Count <= 32)
        return;
      this._history.RemoveRange(0, 4);
    }

    public void Recalculate()
    {
      if (this._currentState == null)
        return;
      this._currentState.Recalculate();
    }

    public CalculatedStyle GetDimensions()
    {
      return new CalculatedStyle(0.0f, 0.0f, (float) Main.screenWidth, (float) Main.screenHeight);
    }

    internal void RefreshState()
    {
      if (this._currentState != null)
        this._currentState.Deactivate();
      this.ResetState();
      this._currentState.Activate();
      this._currentState.Recalculate();
    }

    public bool IsElementUnderMouse()
    {
      if (this.IsVisible && this._lastElementHover != null)
        return !(this._lastElementHover is UIState);
      return false;
    }
  }
}
