// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.GameShaders
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System.Collections.Generic;

namespace Terraria.Graphics.Shaders
{
  public class GameShaders
  {
    public static ArmorShaderDataSet Armor = new ArmorShaderDataSet();
    public static HairShaderDataSet Hair = new HairShaderDataSet();
    public static Dictionary<string, MiscShaderData> Misc = new Dictionary<string, MiscShaderData>();
  }
}
