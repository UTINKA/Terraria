// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Capture.CaptureSettings
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria.Graphics.Capture
{
  public class CaptureSettings
  {
    public bool UseScaling = true;
    public bool CaptureEntities = true;
    public CaptureBiome Biome = CaptureBiome.Biomes[0];
    public Rectangle Area;
    public string OutputName;
    public bool CaptureMech;
    public bool CaptureBackground;

    public CaptureSettings()
    {
      DateTime localTime = DateTime.Now.ToLocalTime();
      this.OutputName = "Capture " + localTime.Year.ToString("D4") + "-" + localTime.Month.ToString("D2") + "-" + localTime.Day.ToString("D2") + " " + localTime.Hour.ToString("D2") + "_" + localTime.Minute.ToString("D2") + "_" + localTime.Second.ToString("D2");
    }
  }
}
