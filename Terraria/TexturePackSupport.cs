// Decompiled with JetBrains decompiler
// Type: Terraria.TexturePackSupport
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Ionic.Zip;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Terraria
{
  public class TexturePackSupport
  {
    public static bool Enabled = false;
    public static int ReplacedTextures = 0;
    private static Dictionary<string, ZipEntry> entries = new Dictionary<string, ZipEntry>();
    private static Stopwatch test = new Stopwatch();
    private static ZipFile texturePack;

    public static bool FetchTexture(string path, out Texture2D tex)
    {
      ZipEntry zipEntry;
      if (TexturePackSupport.entries.TryGetValue(path, out zipEntry))
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          zipEntry.Extract((Stream) memoryStream);
          tex = TexturePackSupport.FromStreamSlow(Main.instance.GraphicsDevice, (Stream) memoryStream);
          ++TexturePackSupport.ReplacedTextures;
          return true;
        }
      }
      else
      {
        tex = (Texture2D) null;
        return false;
      }
    }

    public static Texture2D FromStreamSlow(GraphicsDevice graphicsDevice, Stream stream)
    {
      Texture2D texture2D = Texture2D.FromStream(graphicsDevice, stream);
      Color[] data = new Color[texture2D.Width * texture2D.Height];
      texture2D.GetData<Color>(data);
      for (int index = 0; index != data.Length; ++index)
        data[index] = Color.FromNonPremultiplied(data[index].ToVector4());
      texture2D.SetData<Color>(data);
      return texture2D;
    }

    public static void FindTexturePack()
    {
      string path = Main.SavePath + "/Texture Pack.zip";
      if (!File.Exists(path))
        return;
      TexturePackSupport.entries.Clear();
      TexturePackSupport.texturePack = ZipFile.Read((Stream) File.OpenRead(path));
      using (IEnumerator<ZipEntry> enumerator = ((IEnumerable<ZipEntry>) TexturePackSupport.texturePack.get_Entries()).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          ZipEntry current = enumerator.Current;
          TexturePackSupport.entries.Add(current.get_FileName().Replace("/", "\\"), current);
        }
      }
    }
  }
}
