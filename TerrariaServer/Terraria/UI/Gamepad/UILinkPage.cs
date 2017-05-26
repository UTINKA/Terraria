// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Gamepad.UILinkPage
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
      // ISSUE: reference to a compiler-generated field
      if (this.UpdateEvent == null)
        return;
      // ISSUE: reference to a compiler-generated field
      this.UpdateEvent();
    }

    public void Leave()
    {
      // ISSUE: reference to a compiler-generated field
      if (this.LeaveEvent == null)
        return;
      // ISSUE: reference to a compiler-generated field
      this.LeaveEvent();
    }

    public void Enter()
    {
      // ISSUE: reference to a compiler-generated field
      if (this.EnterEvent == null)
        return;
      // ISSUE: reference to a compiler-generated field
      this.EnterEvent();
    }

    public bool IsValid()
    {
      // ISSUE: reference to a compiler-generated field
      if (this.IsValidEvent != null)
      {
        // ISSUE: reference to a compiler-generated field
        return this.IsValidEvent();
      }
      return true;
    }

    public bool CanEnter()
    {
      // ISSUE: reference to a compiler-generated field
      if (this.CanEnterEvent != null)
      {
        // ISSUE: reference to a compiler-generated field
        return this.CanEnterEvent();
      }
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
        // ISSUE: reference to a compiler-generated field
        if (this.ReachEndEvent == null)
          return;
        // ISSUE: reference to a compiler-generated field
        this.ReachEndEvent(this.CurrentPoint, next);
        // ISSUE: reference to a compiler-generated field
        if (this.TravelEvent == null)
          return;
        // ISSUE: reference to a compiler-generated field
        this.TravelEvent();
      }
      else
      {
        UILinkPointNavigator.ChangePoint(next);
        // ISSUE: reference to a compiler-generated field
        if (this.TravelEvent == null)
          return;
        // ISSUE: reference to a compiler-generated field
        this.TravelEvent();
      }
    }

    public string SpecialInteractions()
    {
      // ISSUE: reference to a compiler-generated field
      if (this.OnSpecialInteracts != null)
      {
        // ISSUE: reference to a compiler-generated field
        return this.OnSpecialInteracts();
      }
      return string.Empty;
    }
  }
}
