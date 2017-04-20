// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenModShape
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.World.Generation
{
  public abstract class GenModShape : GenShape
  {
    protected ShapeData _data;

    public GenModShape(ShapeData data)
    {
      this._data = data;
    }
  }
}
