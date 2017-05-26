// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.TextureManager
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Terraria.Graphics
{
  public static class TextureManager
  {
    private static ConcurrentDictionary<string, Texture2D> _textures = new ConcurrentDictionary<string, Texture2D>();
    private static ConcurrentQueue<TextureManager.LoadPair> _loadQueue = new ConcurrentQueue<TextureManager.LoadPair>();
    private static readonly object _loadThreadLock = new object();
    private static Thread _loadThread;
    public static Texture2D BlankTexture;

    public static void Initialize()
    {
      TextureManager.BlankTexture = new Texture2D(Main.graphics.get_GraphicsDevice(), 4, 4);
    }

    public static Texture2D Load(string name)
    {
      if (TextureManager._textures.ContainsKey(name))
        return TextureManager._textures[name];
      Texture2D blankTexture = TextureManager.BlankTexture;
      TextureManager._textures[name] = blankTexture;
      return blankTexture;
    }

    public static Ref<Texture2D> AsyncLoad(string name)
    {
      return new Ref<Texture2D>(TextureManager.Load(name));
    }

    private static void Run(object context)
    {
      bool looping = true;
      Main.instance.Exiting += (EventHandler<EventArgs>) ((obj, args) =>
      {
        looping = false;
        if (!Monitor.TryEnter(TextureManager._loadThreadLock))
          return;
        Monitor.Pulse(TextureManager._loadThreadLock);
        Monitor.Exit(TextureManager._loadThreadLock);
      });
      Monitor.Enter(TextureManager._loadThreadLock);
      while (looping)
      {
        if (TextureManager._loadQueue.Count != 0)
        {
          TextureManager.LoadPair result;
          if (TextureManager._loadQueue.TryDequeue(out result))
            result.TextureRef.Value = TextureManager.Load(result.Path);
        }
        else
          Monitor.Wait(TextureManager._loadThreadLock);
      }
      Monitor.Exit(TextureManager._loadThreadLock);
    }

    private struct LoadPair
    {
      public string Path;
      public Ref<Texture2D> TextureRef;

      public LoadPair(string path, Ref<Texture2D> textureRef)
      {
        this.Path = path;
        this.TextureRef = textureRef;
      }
    }
  }
}
