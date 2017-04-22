// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.TilePlacementHooksModule
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
