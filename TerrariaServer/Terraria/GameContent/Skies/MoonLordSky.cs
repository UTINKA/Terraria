// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.MoonLordSky
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
  public class MoonLordSky : CustomSky
  {
    private UnifiedRandom _random = new UnifiedRandom();
    private int _moonLordIndex = -1;
    private bool _isActive;

    public override void OnLoad()
    {
    }

    public override void Update(GameTime gameTime)
    {
    }

    private float GetIntensity()
    {
      if (!this.UpdateMoonLordIndex())
        return 0.0f;
      float x = 0.0f;
      if (this._moonLordIndex != -1)
        x = Vector2.Distance(Main.player[Main.myPlayer].Center, Main.npc[this._moonLordIndex].Center);
      return 1f - Utils.SmoothStep(3000f, 6000f, x);
    }

    public override Color OnTileColor(Color inColor)
    {
      float intensity = this.GetIntensity();
      // ISSUE: explicit reference operation
      return new Color(Vector4.Lerp(new Vector4(0.5f, 0.8f, 1f, 1f), ((Color) @inColor).ToVector4(), 1f - intensity));
    }

    private bool UpdateMoonLordIndex()
    {
      if (this._moonLordIndex >= 0 && Main.npc[this._moonLordIndex].active && Main.npc[this._moonLordIndex].type == 398)
        return true;
      int num = -1;
      for (int index = 0; index < Main.npc.Length; ++index)
      {
        if (Main.npc[index].active && Main.npc[index].type == 398)
        {
          num = index;
          break;
        }
      }
      this._moonLordIndex = num;
      return num != -1;
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
      if ((double) maxDepth < 0.0 || (double) minDepth >= 0.0)
        return;
      float intensity = this.GetIntensity();
      spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.op_Multiply(Color.get_Black(), intensity));
    }

    public override float GetCloudAlpha()
    {
      return 0.0f;
    }

    internal override void Activate(Vector2 position, params object[] args)
    {
      this._isActive = true;
    }

    internal override void Deactivate(params object[] args)
    {
      this._isActive = false;
    }

    public override void Reset()
    {
      this._isActive = false;
    }

    public override bool IsActive()
    {
      return this._isActive;
    }
  }
}
