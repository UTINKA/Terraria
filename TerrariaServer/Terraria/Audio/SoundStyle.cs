// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.SoundStyle
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework.Audio;
using Terraria.Utilities;

namespace Terraria.Audio
{
  public abstract class SoundStyle
  {
    private static UnifiedRandom _random = new UnifiedRandom();
    private float _volume;
    private float _pitchVariance;
    private SoundType _type;

    public float Volume
    {
      get
      {
        return this._volume;
      }
    }

    public float PitchVariance
    {
      get
      {
        return this._pitchVariance;
      }
    }

    public SoundType Type
    {
      get
      {
        return this._type;
      }
    }

    public abstract bool IsTrackable { get; }

    public SoundStyle(float volume, float pitchVariance, SoundType type = SoundType.Sound)
    {
      this._volume = volume;
      this._pitchVariance = pitchVariance;
      this._type = type;
    }

    public SoundStyle(SoundType type = SoundType.Sound)
    {
      this._volume = 1f;
      this._pitchVariance = 0.0f;
      this._type = type;
    }

    public float GetRandomPitch()
    {
      return (float) ((double) SoundStyle._random.NextFloat() * (double) this.PitchVariance - (double) this.PitchVariance * 0.5);
    }

    public abstract SoundEffect GetRandomSound();
  }
}
