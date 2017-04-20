// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UISortableElement
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
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
