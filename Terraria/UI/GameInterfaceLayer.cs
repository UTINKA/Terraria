// Decompiled with JetBrains decompiler
// Type: Terraria.UI.GameInterfaceLayer
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;

namespace Terraria.UI
{
  public class GameInterfaceLayer
  {
    public readonly string Name;
    public InterfaceScaleType ScaleType;

    public GameInterfaceLayer(string name, InterfaceScaleType scaleType)
    {
      this.Name = name;
      this.ScaleType = scaleType;
    }

    public bool Draw()
    {
      Matrix matrix;
      if (this.ScaleType == InterfaceScaleType.Game)
      {
        PlayerInput.SetZoom_World();
        matrix = Main.GameViewMatrix.ZoomMatrix;
      }
      else if (this.ScaleType == InterfaceScaleType.UI)
      {
        PlayerInput.SetZoom_UI();
        matrix = Main.UIScaleMatrix;
      }
      else
      {
        PlayerInput.SetZoom_Unscaled();
        matrix = Matrix.get_Identity();
      }
      Main.spriteBatch.Begin((SpriteSortMode) 0, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, matrix);
      int num = this.DrawSelf() ? 1 : 0;
      Main.spriteBatch.End();
      return num != 0;
    }

    protected virtual bool DrawSelf()
    {
      return true;
    }
  }
}
