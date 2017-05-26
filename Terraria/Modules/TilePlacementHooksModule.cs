// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.TilePlacementHooksModule
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

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
