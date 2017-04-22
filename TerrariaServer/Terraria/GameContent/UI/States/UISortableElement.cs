// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UISortableElement
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
