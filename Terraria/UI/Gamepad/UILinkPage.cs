// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Gamepad.UILinkPage
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;

namespace Terraria.UI.Gamepad
{
  public class UILinkPage
  {
    public int PageOnLeft = -1;
    public int PageOnRight = -1;
    public Dictionary<int, UILinkPoint> LinkMap = new Dictionary<int, UILinkPoint>();
    public int ID;
    public int DefaultPoint;
    public int CurrentPoint;

    public event Action<int, int> ReachEndEvent;

    public event Action TravelEvent;

    public event Action LeaveEvent;

    public event Action EnterEvent;

    public event Action UpdateEvent;

    public event Func<bool> IsValidEvent;

    public event Func<bool> CanEnterEvent;

    public event Func<string> OnSpecialInteracts;

    public UILinkPage()
    {
    }

    public UILinkPage(int id)
    {
      this.ID = id;
    }

    public void Update()
    {
      if (this.UpdateEvent == null)
        return;
      this.UpdateEvent();
    }

    public void Leave()
    {
      if (this.LeaveEvent == null)
        return;
      this.LeaveEvent();
    }

    public void Enter()
    {
      if (this.EnterEvent == null)
        return;
      this.EnterEvent();
    }

    public bool IsValid()
    {
      if (this.IsValidEvent != null)
        return this.IsValidEvent();
      return true;
    }

    public bool CanEnter()
    {
      if (this.CanEnterEvent != null)
        return this.CanEnterEvent();
      return true;
    }

    public void TravelUp()
    {
      this.Travel(this.LinkMap[this.CurrentPoint].Up);
    }

    public void TravelDown()
    {
      this.Travel(this.LinkMap[this.CurrentPoint].Down);
    }

    public void TravelLeft()
    {
      this.Travel(this.LinkMap[this.CurrentPoint].Left);
    }

    public void TravelRight()
    {
      this.Travel(this.LinkMap[this.CurrentPoint].Right);
    }

    public void SwapPageLeft()
    {
      UILinkPointNavigator.ChangePage(this.PageOnLeft);
    }

    public void SwapPageRight()
    {
      UILinkPointNavigator.ChangePage(this.PageOnRight);
    }

    private void Travel(int next)
    {
      if (next < 0)
      {
        if (this.ReachEndEvent == null)
          return;
        this.ReachEndEvent(this.CurrentPoint, next);
        if (this.TravelEvent == null)
          return;
        this.TravelEvent();
      }
      else
      {
        UILinkPointNavigator.ChangePoint(next);
        if (this.TravelEvent == null)
          return;
        this.TravelEvent();
      }
    }

    public string SpecialInteractions()
    {
      if (this.OnSpecialInteracts != null)
        return this.OnSpecialInteracts();
      return string.Empty;
    }
  }
}
