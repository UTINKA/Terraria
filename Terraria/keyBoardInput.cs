// Decompiled with JetBrains decompiler
// Type: Terraria.keyBoardInput
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Terraria
{
  public class keyBoardInput
  {
    public static bool slashToggle = true;

    public static event Action<char> newKeyEvent;

    static keyBoardInput()
    {
      Application.AddMessageFilter((IMessageFilter) new keyBoardInput.inKey());
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern bool TranslateMessage(IntPtr message);

    public class inKey : IMessageFilter
    {
      public bool PreFilterMessage(ref Message m)
      {
        if (m.Msg == 258)
        {
          char wparam = (char) (int) m.WParam;
          Console.WriteLine(wparam);
          if (keyBoardInput.newKeyEvent != null)
            keyBoardInput.newKeyEvent(wparam);
        }
        else if (m.Msg == 256)
        {
          IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf((object) m));
          Marshal.StructureToPtr((object) m, num, true);
          keyBoardInput.TranslateMessage(num);
        }
        return false;
      }
    }
  }
}
