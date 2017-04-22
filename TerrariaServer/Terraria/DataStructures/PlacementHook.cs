// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.PlacementHook
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System;

namespace Terraria.DataStructures
{
  public struct PlacementHook
  {
    public static PlacementHook Empty = new PlacementHook((Func<int, int, int, int, int, int>) null, 0, 0, false);
    public const int Response_AllInvalid = 0;
    public Func<int, int, int, int, int, int> hook;
    public int badReturn;
    public int badResponse;
    public bool processedCoordinates;

    public PlacementHook(Func<int, int, int, int, int, int> hook, int badReturn, int badResponse, bool processedCoordinates)
    {
      this.hook = hook;
      this.badResponse = badResponse;
      this.badReturn = badReturn;
      this.processedCoordinates = processedCoordinates;
    }

    public static bool operator ==(PlacementHook first, PlacementHook second)
    {
      if (first.hook == second.hook && first.badResponse == second.badResponse && first.badReturn == second.badReturn)
        return first.processedCoordinates == second.processedCoordinates;
      return false;
    }

    public static bool operator !=(PlacementHook first, PlacementHook second)
    {
      if (!(first.hook != second.hook) && first.badResponse == second.badResponse && first.badReturn == second.badReturn)
        return first.processedCoordinates != second.processedCoordinates;
      return true;
    }

    public override bool Equals(object obj)
    {
      if (obj is PlacementHook)
        return this == (PlacementHook) obj;
      return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
