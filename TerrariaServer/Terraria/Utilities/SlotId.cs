// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.SlotId
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

namespace Terraria.Utilities
{
  public struct SlotId
  {
    public static readonly SlotId Invalid = new SlotId((uint) ushort.MaxValue);
    private const uint KEY_INC = 65536;
    private const uint INDEX_MASK = 65535;
    private const uint ACTIVE_MASK = 2147483648;
    private const uint KEY_MASK = 2147418112;
    public readonly uint Value;

    public bool IsValid
    {
      get
      {
        return ((int) this.Value & (int) ushort.MaxValue) != (int) ushort.MaxValue;
      }
    }

    internal bool IsActive
    {
      get
      {
        if (((int) this.Value & int.MinValue) != 0)
          return this.IsValid;
        return false;
      }
    }

    internal uint Index
    {
      get
      {
        return this.Value & (uint) ushort.MaxValue;
      }
    }

    internal uint Key
    {
      get
      {
        return this.Value & 2147418112U;
      }
    }

    public SlotId(uint value)
    {
      this.Value = value;
    }

    public static bool operator ==(SlotId lhs, SlotId rhs)
    {
      return (int) lhs.Value == (int) rhs.Value;
    }

    public static bool operator !=(SlotId lhs, SlotId rhs)
    {
      return (int) lhs.Value != (int) rhs.Value;
    }

    internal SlotId ToInactive(uint freeHead)
    {
      return new SlotId(this.Key | freeHead);
    }

    internal SlotId ToActive(uint index)
    {
      return new SlotId((uint) (int.MinValue | 2147418112 & (int) this.Key + 65536) | index);
    }

    public override bool Equals(object obj)
    {
      if (!(obj is SlotId))
        return false;
      return (int) ((SlotId) obj).Value == (int) this.Value;
    }

    public override int GetHashCode()
    {
      return this.Value.GetHashCode();
    }

    public float ToFloat()
    {
      return Utils.ReadUIntAsFloat(this.Value);
    }

    public static SlotId FromFloat(float value)
    {
      return new SlotId(Utils.ReadFloatAsUInt(value));
    }
  }
}
