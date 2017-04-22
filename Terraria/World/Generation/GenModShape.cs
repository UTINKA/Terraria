// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenModShape
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
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
