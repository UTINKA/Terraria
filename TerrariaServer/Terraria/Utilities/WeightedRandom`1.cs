// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.WeightedRandom`1
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.Utilities
{
  public class WeightedRandom<T>
  {
    public readonly List<Tuple<T, double>> elements = new List<Tuple<T, double>>();
    public bool needsRefresh = true;
    public readonly UnifiedRandom random;
    private double _totalWeight;

    public WeightedRandom()
    {
      this.random = new UnifiedRandom();
    }

    public WeightedRandom(int seed)
    {
      this.random = new UnifiedRandom(seed);
    }

    public WeightedRandom(UnifiedRandom random)
    {
      this.random = random;
    }

    public WeightedRandom(params Tuple<T, double>[] theElements)
    {
      this.random = new UnifiedRandom();
      this.elements = ((IEnumerable<Tuple<T, double>>) theElements).ToList<Tuple<T, double>>();
    }

    public WeightedRandom(int seed, params Tuple<T, double>[] theElements)
    {
      this.random = new UnifiedRandom(seed);
      this.elements = ((IEnumerable<Tuple<T, double>>) theElements).ToList<Tuple<T, double>>();
    }

    public WeightedRandom(UnifiedRandom random, params Tuple<T, double>[] theElements)
    {
      this.random = random;
      this.elements = ((IEnumerable<Tuple<T, double>>) theElements).ToList<Tuple<T, double>>();
    }

    public static implicit operator T(WeightedRandom<T> weightedRandom)
    {
      return weightedRandom.Get();
    }

    public void Add(T element, double weight = 1.0)
    {
      this.elements.Add(new Tuple<T, double>(element, weight));
      this.needsRefresh = true;
    }

    public T Get()
    {
      if (this.needsRefresh)
        this.CalculateTotalWeight();
      double num = this.random.NextDouble() * this._totalWeight;
      foreach (Tuple<T, double> element in this.elements)
      {
        if (num <= element.Item2)
          return element.Item1;
        num -= element.Item2;
      }
      return default (T);
    }

    public void CalculateTotalWeight()
    {
      this._totalWeight = 0.0;
      foreach (Tuple<T, double> element in this.elements)
        this._totalWeight = this._totalWeight + element.Item2;
      this.needsRefresh = false;
    }

    public void Clear()
    {
      this.elements.Clear();
    }
  }
}
