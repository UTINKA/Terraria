// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.TileObjectSubTilesModule
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System.Collections.Generic;
using Terraria.ObjectData;

namespace Terraria.Modules
{
  public class TileObjectSubTilesModule
  {
    public List<TileObjectData> data;

    public TileObjectSubTilesModule(TileObjectSubTilesModule copyFrom = null, List<TileObjectData> newData = null)
    {
      if (copyFrom == null)
        this.data = (List<TileObjectData>) null;
      else if (copyFrom.data == null)
      {
        this.data = (List<TileObjectData>) null;
      }
      else
      {
        this.data = new List<TileObjectData>(copyFrom.data.Count);
        for (int index = 0; index < this.data.Count; ++index)
          this.data.Add(new TileObjectData(copyFrom.data[index]));
      }
    }
  }
}
