// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.AnchorDataModule
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Terraria.DataStructures;

namespace Terraria.Modules
{
  public class AnchorDataModule
  {
    public AnchorData top;
    public AnchorData bottom;
    public AnchorData left;
    public AnchorData right;
    public bool wall;

    public AnchorDataModule(AnchorDataModule copyFrom = null)
    {
      if (copyFrom == null)
      {
        this.top = new AnchorData();
        this.bottom = new AnchorData();
        this.left = new AnchorData();
        this.right = new AnchorData();
        this.wall = false;
      }
      else
      {
        this.top = copyFrom.top;
        this.bottom = copyFrom.bottom;
        this.left = copyFrom.left;
        this.right = copyFrom.right;
        this.wall = copyFrom.wall;
      }
    }
  }
}
