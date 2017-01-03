// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.MultiTimer
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Terraria.Utilities
{
  public class MultiTimer
  {
    private int _ticksBetweenPrint = 100;
    private Stopwatch _timer = new Stopwatch();
    private Dictionary<string, MultiTimer.TimerData> _timerDataMap = new Dictionary<string, MultiTimer.TimerData>();
    private int _ticksElapsedForPrint;

    public MultiTimer()
    {
    }

    public MultiTimer(int ticksBetweenPrint)
    {
      this._ticksBetweenPrint = ticksBetweenPrint;
    }

    public void Start()
    {
      this._timer.Reset();
      this._timer.Start();
    }

    public void Record(string key)
    {
      double totalMilliseconds = this._timer.Elapsed.TotalMilliseconds;
      MultiTimer.TimerData timerData;
      if (!this._timerDataMap.TryGetValue(key, out timerData))
        this._timerDataMap.Add(key, new MultiTimer.TimerData(totalMilliseconds));
      else
        this._timerDataMap[key] = timerData.AddTick(totalMilliseconds);
      this._timer.Restart();
    }

    public bool StopAndPrint()
    {
      ++this._ticksElapsedForPrint;
      if (this._ticksElapsedForPrint != this._ticksBetweenPrint)
        return false;
      this._ticksElapsedForPrint = 0;
      Console.WriteLine("Average elapsed time: ");
      double num = 0.0;
      int val2 = 0;
      foreach (KeyValuePair<string, MultiTimer.TimerData> timerData in this._timerDataMap)
        val2 = Math.Max(timerData.Key.Length, val2);
      foreach (KeyValuePair<string, MultiTimer.TimerData> timerData1 in this._timerDataMap)
      {
        MultiTimer.TimerData timerData2 = timerData1.Value;
        string str = new string(' ', val2 - timerData1.Key.Length);
        Console.WriteLine(timerData1.Key + str + " : (Average: " + timerData2.Average.ToString("F4") + " Min: " + timerData2.Min.ToString("F4") + " Max: " + timerData2.Max.ToString("F4") + " from " + (object) (int) timerData2.Ticks + " records)");
        num += timerData2.Total;
      }
      this._timerDataMap.Clear();
      Console.WriteLine("Total : " + (object) (float) (num / (double) this._ticksBetweenPrint) + "ms");
      return true;
    }

    private struct TimerData
    {
      public readonly double Min;
      public readonly double Max;
      public readonly double Ticks;
      public readonly double Total;

      public double Average
      {
        get
        {
          return this.Total / this.Ticks;
        }
      }

      private TimerData(double min, double max, double ticks, double total)
      {
        this.Min = min;
        this.Max = max;
        this.Ticks = ticks;
        this.Total = total;
      }

      public TimerData(double startTime)
      {
        this.Min = startTime;
        this.Max = startTime;
        this.Ticks = 1.0;
        this.Total = startTime;
      }

      public MultiTimer.TimerData AddTick(double time)
      {
        return new MultiTimer.TimerData(Math.Min(this.Min, time), Math.Max(this.Max, time), this.Ticks + 1.0, this.Total + time);
      }
    }
  }
}
