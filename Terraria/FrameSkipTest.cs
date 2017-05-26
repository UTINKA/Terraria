// Decompiled with JetBrains decompiler
// Type: Terraria.FrameSkipTest
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System.Collections.Generic;
using System.Threading;

namespace Terraria
{
  public class FrameSkipTest
  {
    private static float CallsThisSecond = 0.0f;
    private static float DeltasThisSecond = 0.0f;
    private static List<float> DeltaSamples = new List<float>();
    private static MultiTimer serverFramerateTest = new MultiTimer(60);
    private static int LastRecordedSecondNumber;
    private const int SamplesCount = 5;

    public static void Update(GameTime gameTime)
    {
      Thread.Sleep((int) MathHelper.Clamp((float) ((1.0 / 60.0 - gameTime.get_ElapsedGameTime().TotalSeconds) * 1000.0 + 1.0), 0.0f, 1000f));
    }

    public static void CheckReset(GameTime gameTime)
    {
      if (FrameSkipTest.LastRecordedSecondNumber == gameTime.get_TotalGameTime().Seconds)
        return;
      FrameSkipTest.DeltaSamples.Add(FrameSkipTest.DeltasThisSecond / FrameSkipTest.CallsThisSecond);
      if (FrameSkipTest.DeltaSamples.Count > 5)
        FrameSkipTest.DeltaSamples.RemoveAt(0);
      FrameSkipTest.CallsThisSecond = 0.0f;
      FrameSkipTest.DeltasThisSecond = 0.0f;
      FrameSkipTest.LastRecordedSecondNumber = gameTime.get_TotalGameTime().Seconds;
    }

    public static void UpdateServerTest()
    {
      FrameSkipTest.serverFramerateTest.Record("frame time");
      FrameSkipTest.serverFramerateTest.StopAndPrint();
      FrameSkipTest.serverFramerateTest.Start();
    }
  }
}
