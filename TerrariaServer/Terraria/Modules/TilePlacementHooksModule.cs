// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.TilePlacementHooksModule
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Terraria.DataStructures;

namespace Terraria.Modules
{
  public class TilePlacementHooksModule
  {
    public PlacementHook check;
    public PlacementHook postPlaceEveryone;
    public PlacementHook postPlaceMyPlayer;
    public PlacementHook placeOverride;

    public TilePlacementHooksModule(TilePlacementHooksModule copyFrom = null)
    {
      if (copyFrom == null)
      {
        this.check = new PlacementHook();
        this.postPlaceEveryone = new PlacementHook();
        this.postPlaceMyPlayer = new PlacementHook();
        this.placeOverride = new PlacementHook();
      }
      else
      {
        this.check = copyFrom.check;
        this.postPlaceEveryone = copyFrom.postPlaceEveryone;
        this.postPlaceMyPlayer = copyFrom.postPlaceMyPlayer;
        this.placeOverride = copyFrom.placeOverride;
      }
    }
  }
}
