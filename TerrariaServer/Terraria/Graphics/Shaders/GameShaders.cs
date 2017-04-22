// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.GameShaders
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
