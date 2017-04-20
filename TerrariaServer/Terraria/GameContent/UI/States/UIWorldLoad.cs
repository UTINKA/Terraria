// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UIWorldLoad
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.World.Generation;

namespace Terraria.GameContent.UI.States
{
  public class UIWorldLoad : UIState
  {
    private UIGenProgressBar _progressBar = new UIGenProgressBar();
    private UIHeader _progressMessage = new UIHeader();
    private GenerationProgress _progress;

    public UIWorldLoad(GenerationProgress progress)
    {
      this._progressBar.Top.Pixels = 370f;
      this._progressBar.HAlign = 0.5f;
      this._progressBar.VAlign = 0.0f;
      this._progressBar.Recalculate();
      this._progressMessage.CopyStyle((UIElement) this._progressBar);
      this._progressMessage.Top.Pixels -= 70f;
      this._progressMessage.Recalculate();
      this._progress = progress;
      this.Append((UIElement) this._progressBar);
      this.Append((UIElement) this._progressMessage);
    }

    public override void OnActivate()
    {
      if (!PlayerInput.UsingGamepadUI)
        return;
      UILinkPointNavigator.Points[3000].Unlink();
      UILinkPointNavigator.ChangePoint(3000);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      this._progressBar.SetProgress(this._progress.TotalProgress, this._progress.Value);
      this._progressMessage.Text = this._progress.Message;
      this.UpdateGamepadSquiggle();
    }

    private void UpdateGamepadSquiggle()
    {
      Vector2 vector2 = Vector2.op_Addition(Vector2.op_Multiply(new Vector2((float) Math.Cos((double) Main.GlobalTime * 6.28318548202515), (float) Math.Sin((double) Main.GlobalTime * 6.28318548202515 * 2.0)), new Vector2(30f, 15f)), Vector2.op_Multiply(Vector2.get_UnitY(), 20f));
      UILinkPointNavigator.Points[3000].Unlink();
      UILinkPointNavigator.SetPosition(3000, Vector2.op_Addition(Vector2.op_Division(new Vector2((float) Main.screenWidth, (float) Main.screenHeight), 2f), vector2));
    }

    public string GetStatusText()
    {
      return string.Format("{0:0.0%} - " + this._progress.Message + " - {1:0.0%}", (object) this._progress.TotalProgress, (object) this._progress.Value);
    }
  }
}
