// Decompiled with JetBrains decompiler
// Type: Terraria.Cinematics.FrameEventData
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

namespace Terraria.Cinematics
{
  public struct FrameEventData
  {
    private int _absoluteFrame;
    private int _start;
    private int _duration;

    public int AbsoluteFrame
    {
      get
      {
        return this._absoluteFrame;
      }
    }

    public int Start
    {
      get
      {
        return this._start;
      }
    }

    public int Duration
    {
      get
      {
        return this._duration;
      }
    }

    public int Frame
    {
      get
      {
        return this._absoluteFrame - this._start;
      }
    }

    public bool IsFirstFrame
    {
      get
      {
        return this._start == this._absoluteFrame;
      }
    }

    public bool IsLastFrame
    {
      get
      {
        return this.Remaining == 0;
      }
    }

    public int Remaining
    {
      get
      {
        return this._start + this._duration - this._absoluteFrame - 1;
      }
    }

    public FrameEventData(int absoluteFrame, int start, int duration)
    {
      this._absoluteFrame = absoluteFrame;
      this._start = start;
      this._duration = duration;
    }
  }
}
