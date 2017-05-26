// Decompiled with JetBrains decompiler
// Type: Terraria.UI.UIMouseEvent
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.UI
{
  public class UIMouseEvent : UIEvent
  {
    public readonly Vector2 MousePosition;

    public UIMouseEvent(UIElement target, Vector2 mousePosition)
      : base(target)
    {
      this.MousePosition = mousePosition;
    }
  }
}
