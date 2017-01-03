// Decompiled with JetBrains decompiler
// Type: Terraria.UI.UIMouseEvent
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

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
