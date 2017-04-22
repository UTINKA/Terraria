// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.LegacySoundStyle
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework.Audio;
using Terraria.Utilities;

namespace Terraria.Audio
{
  public class LegacySoundStyle : SoundStyle
  {
    private static UnifiedRandom _random = new UnifiedRandom();
    private int _style;
    private int _styleVariations;
    private int _soundId;

    public int Style
    {
      get
      {
        if (this._styleVariations != 1)
          return LegacySoundStyle._random.Next(this._style, this._style + this._styleVariations);
        return this._style;
      }
    }

    public int Variations
    {
      get
      {
        return this._styleVariations;
      }
    }

    public int SoundId
    {
      get
      {
        return this._soundId;
      }
    }

    public override bool IsTrackable
    {
      get
      {
        return this._soundId == 42;
      }
    }

    public LegacySoundStyle(int soundId, int style, SoundType type = SoundType.Sound)
      : base(type)
    {
      this._style = style;
      this._styleVariations = 1;
      this._soundId = soundId;
    }

    public LegacySoundStyle(int soundId, int style, int variations, SoundType type = SoundType.Sound)
      : base(type)
    {
      this._style = style;
      this._styleVariations = variations;
      this._soundId = soundId;
    }

    private LegacySoundStyle(int soundId, int style, int variations, SoundType type, float volume, float pitchVariance)
      : base(volume, pitchVariance, type)
    {
      this._style = style;
      this._styleVariations = variations;
      this._soundId = soundId;
    }

    public LegacySoundStyle WithVolume(float volume)
    {
      return new LegacySoundStyle(this._soundId, this._style, this._styleVariations, this.Type, volume, this.PitchVariance);
    }

    public LegacySoundStyle WithPitchVariance(float pitchVariance)
    {
      return new LegacySoundStyle(this._soundId, this._style, this._styleVariations, this.Type, this.Volume, pitchVariance);
    }

    public LegacySoundStyle AsMusic()
    {
      return new LegacySoundStyle(this._soundId, this._style, this._styleVariations, SoundType.Music, this.Volume, this.PitchVariance);
    }

    public LegacySoundStyle AsAmbient()
    {
      return new LegacySoundStyle(this._soundId, this._style, this._styleVariations, SoundType.Ambient, this.Volume, this.PitchVariance);
    }

    public LegacySoundStyle AsSound()
    {
      return new LegacySoundStyle(this._soundId, this._style, this._styleVariations, SoundType.Sound, this.Volume, this.PitchVariance);
    }

    public bool Includes(int soundId, int style)
    {
      if (this._soundId == soundId && style >= this._style)
        return style < this._style + this._styleVariations;
      return false;
    }

    public override SoundEffect GetRandomSound()
    {
      if (this.IsTrackable)
        return Main.trackableSounds[this.Style];
      return (SoundEffect) null;
    }
  }
}
