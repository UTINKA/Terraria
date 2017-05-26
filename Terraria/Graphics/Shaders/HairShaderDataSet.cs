// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.HairShaderDataSet
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
  public class HairShaderDataSet
  {
    protected List<HairShaderData> _shaderData = new List<HairShaderData>();
    protected Dictionary<int, short> _shaderLookupDictionary = new Dictionary<int, short>();
    protected byte _shaderDataCount;

    public T BindShader<T>(int itemId, T shaderData) where T : HairShaderData
    {
      if ((int) this._shaderDataCount == (int) byte.MaxValue)
        throw new Exception("Too many shaders bound.");
      Dictionary<int, short> lookupDictionary = this._shaderLookupDictionary;
      int index = itemId;
      byte num1 = (byte) ((uint) this._shaderDataCount + 1U);
      this._shaderDataCount = num1;
      int num2 = (int) num1;
      lookupDictionary[index] = (short) num2;
      this._shaderData.Add((HairShaderData) shaderData);
      return shaderData;
    }

    public void Apply(short shaderId, Player player, DrawData? drawData = null)
    {
      if ((int) shaderId != 0 && (int) shaderId <= (int) this._shaderDataCount)
        this._shaderData[(int) shaderId - 1].Apply(player, drawData);
      else
        Main.pixelShader.get_CurrentTechnique().get_Passes().get_Item(0).Apply();
    }

    public Color GetColor(short shaderId, Player player, Color lightColor)
    {
      if ((int) shaderId != 0 && (int) shaderId <= (int) this._shaderDataCount)
        return this._shaderData[(int) shaderId - 1].GetColor(player, lightColor);
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      return new Color(Vector4.op_Multiply(((Color) @lightColor).ToVector4(), ((Color) @player.hairColor).ToVector4()));
    }

    public HairShaderData GetShaderFromItemId(int type)
    {
      if (this._shaderLookupDictionary.ContainsKey(type))
        return this._shaderData[(int) this._shaderLookupDictionary[type] - 1];
      return (HairShaderData) null;
    }

    public short GetShaderIdFromItemId(int type)
    {
      if (this._shaderLookupDictionary.ContainsKey(type))
        return this._shaderLookupDictionary[type];
      return -1;
    }
  }
}
