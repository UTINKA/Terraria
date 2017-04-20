// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.HairShaderDataSet
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
      this._shaderLookupDictionary[itemId] = (short) ++this._shaderDataCount;
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
