// Decompiled with JetBrains decompiler
// Type: Terraria.WindowsLaunch
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Terraria.Social;

namespace Terraria
{
  public static class WindowsLaunch
  {
    private static WindowsLaunch.HandlerRoutine _handleRoutine;

    private static bool ConsoleCtrlCheck(WindowsLaunch.CtrlTypes ctrlType)
    {
      bool flag = false;
      switch (ctrlType)
      {
        case WindowsLaunch.CtrlTypes.CTRL_C_EVENT:
          flag = true;
          break;
        case WindowsLaunch.CtrlTypes.CTRL_BREAK_EVENT:
          flag = true;
          break;
        case WindowsLaunch.CtrlTypes.CTRL_CLOSE_EVENT:
          flag = true;
          break;
        case WindowsLaunch.CtrlTypes.CTRL_LOGOFF_EVENT:
        case WindowsLaunch.CtrlTypes.CTRL_SHUTDOWN_EVENT:
          flag = true;
          break;
      }
      if (flag)
        SocialAPI.Shutdown();
      return true;
    }

    [DllImport("Kernel32")]
    public static extern bool SetConsoleCtrlHandler(WindowsLaunch.HandlerRoutine Handler, bool Add);

    private static void Main(string[] args)
    {
      AppDomain.CurrentDomain.AssemblyResolve += (ResolveEventHandler) ((sender, sargs) =>
      {
        string resourceName = new AssemblyName(sargs.Name).Name + ".dll";
        string name = Array.Find<string>(typeof (Program).Assembly.GetManifestResourceNames(), (Predicate<string>) (element => element.EndsWith(resourceName)));
        if (name == null)
          return (Assembly) null;
        using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
        {
          byte[] numArray = new byte[manifestResourceStream.Length];
          manifestResourceStream.Read(numArray, 0, numArray.Length);
          return Assembly.Load(numArray);
        }
      });
      WindowsLaunch._handleRoutine = new WindowsLaunch.HandlerRoutine(WindowsLaunch.ConsoleCtrlCheck);
      WindowsLaunch.SetConsoleCtrlHandler(WindowsLaunch._handleRoutine, true);
      Program.LaunchGame(args);
    }

    public delegate bool HandlerRoutine(WindowsLaunch.CtrlTypes CtrlType);

    public enum CtrlTypes
    {
      CTRL_C_EVENT = 0,
      CTRL_BREAK_EVENT = 1,
      CTRL_CLOSE_EVENT = 2,
      CTRL_LOGOFF_EVENT = 5,
      CTRL_SHUTDOWN_EVENT = 6,
    }
  }
}
