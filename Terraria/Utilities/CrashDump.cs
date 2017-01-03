// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.CrashDump
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Terraria.Utilities
{
  public class CrashDump
  {
    [DllImport("dbghelp.dll")]
    private static extern bool MiniDumpWriteDump(IntPtr hProcess, int ProcessId, IntPtr hFile, CrashDump.MINIDUMP_TYPE DumpType, IntPtr ExceptionParam, IntPtr UserStreamParam, IntPtr CallackParam);

    public static void Create()
    {
      DateTime localTime = DateTime.Now.ToLocalTime();
      CrashDump.Create("Terraria " + Main.versionNumber + " " + localTime.Year.ToString("D4") + "-" + localTime.Month.ToString("D2") + "-" + localTime.Day.ToString("D2") + " " + localTime.Hour.ToString("D2") + "_" + localTime.Minute.ToString("D2") + "_" + localTime.Second.ToString("D2") + ".dmp");
    }

    public static void CreateFull()
    {
      DateTime localTime = DateTime.Now.ToLocalTime();
      using (FileStream fileStream = File.Create("DMP-FULL Terraria " + Main.versionNumber + " " + localTime.Year.ToString("D4") + "-" + localTime.Month.ToString("D2") + "-" + localTime.Day.ToString("D2") + " " + localTime.Hour.ToString("D2") + "_" + localTime.Minute.ToString("D2") + "_" + localTime.Second.ToString("D2") + ".dmp"))
      {
        Process currentProcess = Process.GetCurrentProcess();
        CrashDump.MiniDumpWriteDump(currentProcess.Handle, currentProcess.Id, fileStream.SafeFileHandle.DangerousGetHandle(), CrashDump.MINIDUMP_TYPE.MiniDumpWithFullMemory, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
      }
    }

    public static void Create(string path)
    {
      bool flag = Program.LaunchParameters.ContainsKey("-fulldump");
      using (FileStream fileStream = File.Create(path))
      {
        Process currentProcess = Process.GetCurrentProcess();
        CrashDump.MiniDumpWriteDump(currentProcess.Handle, currentProcess.Id, fileStream.SafeFileHandle.DangerousGetHandle(), flag ? CrashDump.MINIDUMP_TYPE.MiniDumpWithFullMemory : CrashDump.MINIDUMP_TYPE.MiniDumpWithIndirectlyReferencedMemory, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
      }
    }

    internal enum MINIDUMP_TYPE
    {
      MiniDumpNormal = 0,
      MiniDumpWithDataSegs = 1,
      MiniDumpWithFullMemory = 2,
      MiniDumpWithHandleData = 4,
      MiniDumpFilterMemory = 8,
      MiniDumpScanMemory = 16,
      MiniDumpWithUnloadedModules = 32,
      MiniDumpWithIndirectlyReferencedMemory = 64,
      MiniDumpFilterModulePaths = 128,
      MiniDumpWithProcessThreadData = 256,
      MiniDumpWithPrivateReadWriteMemory = 512,
      MiniDumpWithoutOptionalData = 1024,
      MiniDumpWithFullMemoryInfo = 2048,
      MiniDumpWithThreadInfo = 4096,
      MiniDumpWithCodeSegs = 8192,
    }
  }
}
