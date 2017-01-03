// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UISortableElement
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Terraria.UI;

namespace Terraria.GameContent.UI.States
{
  public class UISortableElement : UIElement
  {
    public int OrderIndex;

    public UISortableElement(int index)
    {
      this.OrderIndex = index;
    }

    public override int CompareTo(object obj)
    {
      UISortableElement uiSortableElement = obj as UISortableElement;
      if (uiSortableElement != null)
        return this.OrderIndex.CompareTo(uiSortableElement.OrderIndex);
      return base.CompareTo(obj);
    }
  }
}
