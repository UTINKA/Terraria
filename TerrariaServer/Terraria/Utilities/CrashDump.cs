// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.CrashDump
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using ReLogic.OS;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Terraria.Utilities
{
  public static class CrashDump
  {
    public static bool WriteException(CrashDump.Options options, string outputDirectory = ".")
    {
      return CrashDump.Write(options, CrashDump.ExceptionInfo.Present, outputDirectory);
    }

    public static bool Write(CrashDump.Options options, string outputDirectory = ".")
    {
      return CrashDump.Write(options, CrashDump.ExceptionInfo.None, outputDirectory);
    }

    private static string CreateDumpName()
    {
      DateTime localTime = DateTime.Now.ToLocalTime();
      return string.Format("{0}_{1}_{2}_{3}.dmp", (object) "TerrariaServer", (object) Main.versionNumber, (object) localTime.ToString("MM-dd-yy_HH-mm-ss-ffff", (IFormatProvider) CultureInfo.InvariantCulture), (object) Thread.CurrentThread.ManagedThreadId);
    }

    private static bool Write(CrashDump.Options options, CrashDump.ExceptionInfo exceptionInfo, string outputDirectory)
    {
      if (!Platform.get_IsWindows())
        return false;
      string path = Path.Combine(outputDirectory, CrashDump.CreateDumpName());
      if (!Directory.Exists(outputDirectory))
        Directory.CreateDirectory(outputDirectory);
      using (FileStream fileStream = File.Create(path))
        return CrashDump.Write((SafeHandle) fileStream.SafeFileHandle, options, exceptionInfo);
    }

    private static bool Write(SafeHandle fileHandle, CrashDump.Options options, CrashDump.ExceptionInfo exceptionInfo)
    {
      if (!Platform.get_IsWindows())
        return false;
      Process currentProcess = Process.GetCurrentProcess();
      IntPtr handle = currentProcess.Handle;
      uint id = (uint) currentProcess.Id;
      CrashDump.MiniDumpExceptionInformation expParam;
      expParam.ThreadId = CrashDump.GetCurrentThreadId();
      expParam.ClientPointers = false;
      expParam.ExceptionPointers = IntPtr.Zero;
      if (exceptionInfo == CrashDump.ExceptionInfo.Present)
        expParam.ExceptionPointers = Marshal.GetExceptionPointers();
      return !(expParam.ExceptionPointers == IntPtr.Zero) ? CrashDump.MiniDumpWriteDump(handle, id, fileHandle, (uint) options, ref expParam, IntPtr.Zero, IntPtr.Zero) : CrashDump.MiniDumpWriteDump(handle, id, fileHandle, (uint) options, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
    }

    [DllImport("dbghelp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    private static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, ref CrashDump.MiniDumpExceptionInformation expParam, IntPtr userStreamParam, IntPtr callbackParam);

    [DllImport("dbghelp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    private static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();

    [Flags]
    public enum Options : uint
    {
      Normal = 0,
      WithDataSegs = 1,
      WithFullMemory = 2,
      WithHandleData = 4,
      FilterMemory = 8,
      ScanMemory = 16,
      WithUnloadedModules = 32,
      WithIndirectlyReferencedMemory = 64,
      FilterModulePaths = 128,
      WithProcessThreadData = 256,
      WithPrivateReadWriteMemory = 512,
      WithoutOptionalData = 1024,
      WithFullMemoryInfo = 2048,
      WithThreadInfo = 4096,
      WithCodeSegs = 8192,
      WithoutAuxiliaryState = 16384,
      WithFullAuxiliaryState = 32768,
      WithPrivateWriteCopyMemory = 65536,
      IgnoreInaccessibleMemory = 131072,
      ValidTypeFlags = IgnoreInaccessibleMemory | WithPrivateWriteCopyMemory | WithFullAuxiliaryState | WithoutAuxiliaryState | WithCodeSegs | WithThreadInfo | WithFullMemoryInfo | WithoutOptionalData | WithPrivateReadWriteMemory | WithProcessThreadData | FilterModulePaths | WithIndirectlyReferencedMemory | WithUnloadedModules | ScanMemory | FilterMemory | WithHandleData | WithFullMemory | WithDataSegs,
    }

    private enum ExceptionInfo
    {
      None,
      Present,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct MiniDumpExceptionInformation
    {
      public uint ThreadId;
      public IntPtr ExceptionPointers;
      [MarshalAs(UnmanagedType.Bool)]
      public bool ClientPointers;
    }
  }
}
