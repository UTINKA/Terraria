// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Dyes.TeamArmorShaderData
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
  public class TeamArmorShaderData : ArmorShaderData
  {
    private static bool isInitialized = false;
    private static ArmorShaderData[] dustShaderData;

    public TeamArmorShaderData(Ref<Effect> shader, string passName)
      : base(shader, passName)
    {
      if (TeamArmorShaderData.isInitialized)
        return;
      TeamArmorShaderData.isInitialized = true;
      TeamArmorShaderData.dustShaderData = new ArmorShaderData[Main.teamColor.Length];
      for (int index = 1; index < Main.teamColor.Length; ++index)
        TeamArmorShaderData.dustShaderData[index] = new ArmorShaderData(shader, passName).UseColor(Main.teamColor[index]);
      TeamArmorShaderData.dustShaderData[0] = new ArmorShaderData(shader, "Default");
    }

    public override void Apply(Entity entity, DrawData? drawData)
    {
      Player player = entity as Player;
      if (player == null || player.team == 0)
      {
        TeamArmorShaderData.dustShaderData[0].Apply((Entity) player, drawData);
      }
      else
      {
        this.UseColor(Main.teamColor[player.team]);
        base.Apply((Entity) player, drawData);
      }
    }

    public override ArmorShaderData GetSecondaryShader(Entity entity)
    {
      Player player = entity as Player;
      return TeamArmorShaderData.dustShaderData[player.team];
    }
  }
}
