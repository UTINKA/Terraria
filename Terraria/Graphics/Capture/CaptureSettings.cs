// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Capture.CaptureSettings
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

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
