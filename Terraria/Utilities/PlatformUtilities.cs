// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.PlatformUtilities
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Terraria.Utilities
{
  public static class PlatformUtilities
  {
    public const bool IsXNA = true;
    public const bool IsFNA = false;
    public const bool IsWindows = true;
    public const bool IsOSX = false;
    public const bool IsLinux = false;

    public static string GetClipboard()
    {
      string clipboardText = "";
      Thread thread = new Thread((ThreadStart) (() => clipboardText = Clipboard.GetText()));
      thread.SetApartmentState(ApartmentState.STA);
      thread.Start();
      thread.Join();
      char[] chArray = new char[clipboardText.Length];
      int length = 0;
      for (int index = 0; index < clipboardText.Length; ++index)
      {
        if ((int) clipboardText[index] >= 32 && (int) clipboardText[index] != (int) sbyte.MaxValue)
          chArray[length++] = clipboardText[index];
      }
      return new string(chArray, 0, length);
    }

    public static void SetClipboard(string text)
    {
      Thread thread = new Thread((ThreadStart) (() =>
      {
        if (text.Length <= 0)
          return;
        Clipboard.SetText(text);
      }));
      thread.SetApartmentState(ApartmentState.STA);
      thread.Start();
      thread.Join();
    }

    public static string GetStoragePath()
    {
      return Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "My Games"), "Terraria");
    }
  }
}
