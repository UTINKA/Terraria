// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.ArmorShaderDataSet
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
  public class ArmorShaderDataSet
  {
    protected List<ArmorShaderData> _shaderData = new List<ArmorShaderData>();
    protected Dictionary<int, int> _shaderLookupDictionary = new Dictionary<int, int>();
    protected int _shaderDataCount;

    public T BindShader<T>(int itemId, T shaderData) where T : ArmorShaderData
    {
      Dictionary<int, int> lookupDictionary = this._shaderLookupDictionary;
      int index = itemId;
      int num1 = this._shaderDataCount + 1;
      this._shaderDataCount = num1;
      int num2 = num1;
      lookupDictionary[index] = num2;
      this._shaderData.Add((ArmorShaderData) shaderData);
      return shaderData;
    }

    public void Apply(int shaderId, Entity entity, DrawData? drawData = null)
    {
      if (shaderId != 0 && shaderId <= this._shaderDataCount)
        this._shaderData[shaderId - 1].Apply(entity, drawData);
      else
        Main.pixelShader.get_CurrentTechnique().get_Passes().get_Item(0).Apply();
    }

    public void ApplySecondary(int shaderId, Entity entity, DrawData? drawData = null)
    {
      if (shaderId != 0 && shaderId <= this._shaderDataCount)
        this._shaderData[shaderId - 1].GetSecondaryShader(entity).Apply(entity, drawData);
      else
        Main.pixelShader.get_CurrentTechnique().get_Passes().get_Item(0).Apply();
    }

    public ArmorShaderData GetShaderFromItemId(int type)
    {
      if (this._shaderLookupDictionary.ContainsKey(type))
        return this._shaderData[this._shaderLookupDictionary[type] - 1];
      return (ArmorShaderData) null;
    }

    public int GetShaderIdFromItemId(int type)
    {
      if (this._shaderLookupDictionary.ContainsKey(type))
        return this._shaderLookupDictionary[type];
      return 0;
    }

    public ArmorShaderData GetSecondaryShader(int id, Player player)
    {
      if (id != 0 && id <= this._shaderDataCount && this._shaderData[id - 1] != null)
        return this._shaderData[id - 1].GetSecondaryShader((Entity) player);
      return (ArmorShaderData) null;
    }
  }
}
