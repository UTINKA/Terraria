// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.HairShaderDataSet
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

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
        Main.pixelShader.CurrentTechnique.Passes[0].Apply();
    }

    public Color GetColor(short shaderId, Player player, Color lightColor)
    {
      if ((int) shaderId != 0 && (int) shaderId <= (int) this._shaderDataCount)
        return this._shaderData[(int) shaderId - 1].GetColor(player, lightColor);
      return new Color(lightColor.ToVector4() * player.hairColor.ToVector4());
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
