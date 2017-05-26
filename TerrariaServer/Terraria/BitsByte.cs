// Decompiled with JetBrains decompiler
// Type: Terraria.BitsByte
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace Terraria
{
  public struct BitsByte
  {
    private static bool Null;
    private byte value;

    public bool this[int key]
    {
      get
      {
        return ((uint) this.value & (uint) (1 << key)) > 0U;
      }
      set
      {
        if (value)
          this.value = (byte) ((uint) this.value | (uint) (byte) (1 << key));
        else
          this.value = (byte) ((uint) this.value & (uint) (byte) ~(1 << key));
      }
    }

    public BitsByte(bool b1 = false, bool b2 = false, bool b3 = false, bool b4 = false, bool b5 = false, bool b6 = false, bool b7 = false, bool b8 = false)
    {
      this.value = (byte) 0;
      this[0] = b1;
      this[1] = b2;
      this[2] = b3;
      this[3] = b4;
      this[4] = b5;
      this[5] = b6;
      this[6] = b7;
      this[7] = b8;
    }

    public static implicit operator byte(BitsByte bb)
    {
      return bb.value;
    }

    public static implicit operator BitsByte(byte b)
    {
      return new BitsByte() { value = b };
    }

    public void ClearAll()
    {
      this.value = (byte) 0;
    }

    public void SetAll()
    {
      this.value = byte.MaxValue;
    }

    public void Retrieve(ref bool b0)
    {
      this.Retrieve(ref b0, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
    }

    public void Retrieve(ref bool b0, ref bool b1)
    {
      this.Retrieve(ref b0, ref b1, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
    }

    public void Retrieve(ref bool b0, ref bool b1, ref bool b2)
    {
      this.Retrieve(ref b0, ref b1, ref b2, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
    }

    public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3)
    {
      this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
    }

    public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4)
    {
      this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
    }

    public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4, ref bool b5)
    {
      this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref BitsByte.Null, ref BitsByte.Null);
    }

    public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4, ref bool b5, ref bool b6)
    {
      this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref BitsByte.Null);
    }

    public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4, ref bool b5, ref bool b6, ref bool b7)
    {
      b0 = this[0];
      b1 = this[1];
      b2 = this[2];
      b3 = this[3];
      b4 = this[4];
      b5 = this[5];
      b6 = this[6];
      b7 = this[7];
    }

    public static BitsByte[] ComposeBitsBytesChain(bool optimizeLength, params bool[] flags)
    {
      int length1 = flags.Length;
      int length2 = 0;
      while (length1 > 0)
      {
        ++length2;
        length1 -= 7;
      }
      BitsByte[] array = new BitsByte[length2];
      int index1 = 0;
      int index2 = 0;
      for (int index3 = 0; index3 < flags.Length; ++index3)
      {
        array[index2][index1] = flags[index3];
        ++index1;
        if (index1 == 7 && index2 < length2 - 1)
        {
          array[index2][index1] = true;
          index1 = 0;
          ++index2;
        }
      }
      if (optimizeLength)
      {
        int index3;
        for (index3 = array.Length - 1; (int) (byte) array[index3] == 0 && index3 > 0; --index3)
          array[index3 - 1][7] = false;
        Array.Resize<BitsByte>(ref array, index3 + 1);
      }
      return array;
    }

    public static BitsByte[] DecomposeBitsBytesChain(BinaryReader reader)
    {
      List<BitsByte> bitsByteList = new List<BitsByte>();
      BitsByte bitsByte;
      do
      {
        bitsByte = (BitsByte) reader.ReadByte();
        bitsByteList.Add(bitsByte);
      }
      while (bitsByte[7]);
      return bitsByteList.ToArray();
    }

    public static void SortOfAUnitTest()
    {
      MemoryStream memoryStream = new MemoryStream();
      BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream);
      BinaryReader reader = new BinaryReader((Stream) memoryStream);
      int num1 = 0;
      bool[] flagArray = new bool[28];
      flagArray[3] = true;
      flagArray[14] = true;
      BitsByte[] bitsByteArray1 = BitsByte.ComposeBitsBytesChain(num1 != 0, flagArray);
      foreach (BitsByte bitsByte in bitsByteArray1)
      {
        byte num2 = (byte) bitsByte;
        binaryWriter.Write(num2);
      }
      memoryStream.Position = 0L;
      BitsByte[] bitsByteArray2 = BitsByte.DecomposeBitsBytesChain(reader);
      string str1 = "";
      string str2 = "";
      foreach (BitsByte bitsByte in bitsByteArray1)
        str1 = str1 + (object) (byte) bitsByte + ", ";
      foreach (BitsByte bitsByte in bitsByteArray2)
        str2 = str2 + (object) (byte) bitsByte + ", ";
      Main.NewText("done", byte.MaxValue, byte.MaxValue, byte.MaxValue, false);
    }
  }
}
