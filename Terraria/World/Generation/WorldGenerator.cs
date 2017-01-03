// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.WorldGenerator
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using System.Diagnostics;
using Terraria.GameContent.UI.States;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.World.Generation
{
  public class WorldGenerator
  {
    private List<GenPass> _passes = new List<GenPass>();
    private float _totalLoadWeight;
    private int _seed;

    public WorldGenerator(int seed)
    {
      this._seed = seed;
    }

    public void Append(GenPass pass)
    {
      this._passes.Add(pass);
      this._totalLoadWeight += pass.Weight;
    }

    public void GenerateWorld(GenerationProgress progress = null)
    {
      Stopwatch stopwatch = new Stopwatch();
      float num = 0.0f;
      foreach (GenPass pass in this._passes)
        num += pass.Weight;
      if (progress == null)
        progress = new GenerationProgress();
      progress.TotalWeight = num;
      Main.menuMode = 888;
      Main.MenuUI.SetState((UIState) new UIWorldLoad(progress));
      foreach (GenPass pass in this._passes)
      {
        WorldGen._genRand = new UnifiedRandom(this._seed);
        Main.rand = new UnifiedRandom(this._seed);
        stopwatch.Start();
        progress.Start(pass.Weight);
        pass.Apply(progress);
        progress.End();
        stopwatch.Reset();
      }
    }
  }
}
