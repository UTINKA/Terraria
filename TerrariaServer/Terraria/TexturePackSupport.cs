// Decompiled with JetBrains decompiler
// Type: Terraria.TexturePackSupport
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
      Color[] colorArray = new Color[texture2D.get_Width() * texture2D.get_Height()];
      texture2D.GetData<Color>((M0[]) colorArray);
      for (int index = 0; index != colorArray.Length; ++index)
      {
        // ISSUE: explicit reference operation
        colorArray[index] = Color.FromNonPremultiplied(((Color) @colorArray[index]).ToVector4());
      }
      texture2D.SetData<Color>((M0[]) colorArray);
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
