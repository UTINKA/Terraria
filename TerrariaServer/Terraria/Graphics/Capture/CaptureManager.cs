// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Capture.CaptureManager
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Capture
{
  public class CaptureManager
  {
    public static CaptureManager Instance = new CaptureManager();
    private CaptureInterface _interface;
    private CaptureCamera _camera;

    public bool IsCapturing
    {
      get
      {
        return false;
      }
    }

    public bool Active
    {
      get
      {
        return this._interface.Active;
      }
      set
      {
        if (Main.CaptureModeDisabled || this._interface.Active == value)
          return;
        this._interface.ToggleCamera(value);
      }
    }

    public bool UsingMap
    {
      get
      {
        if (!this.Active)
          return false;
        return this._interface.UsingMap();
      }
    }

    public CaptureManager()
    {
      this._interface = new CaptureInterface();
    }

    public void Scrolling()
    {
      this._interface.Scrolling();
    }

    public void Update()
    {
      this._interface.Update();
    }

    public void Draw(SpriteBatch sb)
    {
      this._interface.Draw(sb);
    }

    public float GetProgress()
    {
      return this._camera.GetProgress();
    }

    public void Capture()
    {
      this.Capture(new CaptureSettings()
      {
        Area = new Rectangle(2660, 100, 1000, 1000),
        UseScaling = false
      });
    }

    public void Capture(CaptureSettings settings)
    {
      this._camera.Capture(settings);
    }

    public void DrawTick()
    {
      this._camera.DrawTick();
    }
  }
}
