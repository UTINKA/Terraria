// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Capture.CaptureCamera
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Terraria.Localization;

namespace Terraria.Graphics.Capture
{
  internal class CaptureCamera
  {
    private readonly object _captureLock = new object();
    private Queue<CaptureCamera.CaptureChunk> _renderQueue = new Queue<CaptureCamera.CaptureChunk>();
    public const int CHUNK_SIZE = 128;
    public const int FRAMEBUFFER_PIXEL_SIZE = 2048;
    public const int INNER_CHUNK_SIZE = 126;
    public const int MAX_IMAGE_SIZE = 4096;
    public const string CAPTURE_DIRECTORY = "Captures";
    private static bool CameraExists;
    private RenderTarget2D _frameBuffer;
    private RenderTarget2D _scaledFrameBuffer;
    private GraphicsDevice _graphics;
    private bool _isDisposed;
    private CaptureSettings _activeSettings;
    private SpriteBatch _spriteBatch;
    private byte[] _scaledFrameData;
    private byte[] _outputData;
    private Size _outputImageSize;
    private SamplerState _downscaleSampleState;
    private float _tilesProcessed;
    private float _totalTiles;

    public bool IsCapturing
    {
      get
      {
        Monitor.Enter(this._captureLock);
        bool flag = this._activeSettings != null;
        Monitor.Exit(this._captureLock);
        return flag;
      }
    }

    public CaptureCamera(GraphicsDevice graphics)
    {
      CaptureCamera.CameraExists = true;
      this._graphics = graphics;
      this._spriteBatch = new SpriteBatch(graphics);
      try
      {
        this._frameBuffer = new RenderTarget2D(graphics, 2048, 2048, false, graphics.get_PresentationParameters().get_BackBufferFormat(), (DepthFormat) 2);
      }
      catch
      {
        Main.CaptureModeDisabled = true;
        return;
      }
      this._downscaleSampleState = (SamplerState) SamplerState.AnisotropicClamp;
    }

    ~CaptureCamera()
    {
      this.Dispose();
    }

    public void Capture(CaptureSettings settings)
    {
      Main.GlobalTimerPaused = true;
      Monitor.Enter(this._captureLock);
      if (this._activeSettings != null)
        throw new InvalidOperationException("Capture called while another capture was already active.");
      this._activeSettings = settings;
      Rectangle area = settings.Area;
      float num1 = 1f;
      if (settings.UseScaling)
      {
        if (area.Width << 4 > 4096)
          num1 = 4096f / (float) (area.Width << 4);
        if (area.Height << 4 > 4096)
          num1 = Math.Min(num1, 4096f / (float) (area.Height << 4));
        num1 = Math.Min(1f, num1);
        this._outputImageSize = new Size((int) MathHelper.Clamp((float) (int) ((double) num1 * (double) (float) (area.Width << 4)), 1f, 4096f), (int) MathHelper.Clamp((float) (int) ((double) num1 * (double) (float) (area.Height << 4)), 1f, 4096f));
        this._outputData = new byte[4 * this._outputImageSize.Width * this._outputImageSize.Height];
        int num2 = (int) Math.Floor((double) num1 * 2048.0);
        this._scaledFrameData = new byte[4 * num2 * num2];
        this._scaledFrameBuffer = new RenderTarget2D(this._graphics, num2, num2, false, this._graphics.get_PresentationParameters().get_BackBufferFormat(), (DepthFormat) 2);
      }
      else
        this._outputData = new byte[16777216];
      this._tilesProcessed = 0.0f;
      this._totalTiles = (float) (area.Width * area.Height);
      int x = (int) area.X;
      while (x < area.X + area.Width)
      {
        int y = (int) area.Y;
        while (y < area.Y + area.Height)
        {
          int num2 = Math.Min(128, area.X + area.Width - x);
          int num3 = Math.Min(128, area.Y + area.Height - y);
          int num4 = (int) Math.Floor((double) num1 * (double) (num2 << 4));
          int num5 = (int) Math.Floor((double) num1 * (double) (num3 << 4));
          int num6 = (int) Math.Floor((double) num1 * (double) (x - area.X << 4));
          int num7 = (int) Math.Floor((double) num1 * (double) (y - area.Y << 4));
          this._renderQueue.Enqueue(new CaptureCamera.CaptureChunk(new Rectangle(x, y, num2, num3), new Rectangle(num6, num7, num4, num5)));
          y += 126;
        }
        x += 126;
      }
      Monitor.Exit(this._captureLock);
    }

    public void DrawTick()
    {
      Monitor.Enter(this._captureLock);
      if (this._activeSettings == null)
        return;
      if (this._renderQueue.Count > 0)
      {
        CaptureCamera.CaptureChunk captureChunk = this._renderQueue.Dequeue();
        this._graphics.SetRenderTarget(this._frameBuffer);
        this._graphics.Clear(Color.get_Transparent());
        Main.instance.DrawCapture(captureChunk.Area, this._activeSettings);
        if (this._activeSettings.UseScaling)
        {
          this._graphics.SetRenderTarget(this._scaledFrameBuffer);
          this._graphics.Clear(Color.get_Transparent());
          this._spriteBatch.Begin((SpriteSortMode) 0, (BlendState) BlendState.AlphaBlend, this._downscaleSampleState, (DepthStencilState) DepthStencilState.Default, (RasterizerState) RasterizerState.CullNone);
          this._spriteBatch.Draw((Texture2D) this._frameBuffer, new Rectangle(0, 0, ((Texture2D) this._scaledFrameBuffer).get_Width(), ((Texture2D) this._scaledFrameBuffer).get_Height()), Color.get_White());
          this._spriteBatch.End();
          this._graphics.SetRenderTarget((RenderTarget2D) null);
          ((Texture2D) this._scaledFrameBuffer).GetData<byte>((M0[]) this._scaledFrameData, 0, ((Texture2D) this._scaledFrameBuffer).get_Width() * ((Texture2D) this._scaledFrameBuffer).get_Height() * 4);
          this.DrawBytesToBuffer(this._scaledFrameData, this._outputData, ((Texture2D) this._scaledFrameBuffer).get_Width(), this._outputImageSize.Width, captureChunk.ScaledArea);
        }
        else
        {
          this._graphics.SetRenderTarget((RenderTarget2D) null);
          this.SaveImage((Texture2D) this._frameBuffer, (int) captureChunk.ScaledArea.Width, (int) captureChunk.ScaledArea.Height, ImageFormat.Png, this._activeSettings.OutputName, captureChunk.Area.X.ToString() + "-" + (object) (int) captureChunk.Area.Y + ".png");
        }
        this._tilesProcessed += (float) (captureChunk.Area.Width * captureChunk.Area.Height);
      }
      if (this._renderQueue.Count == 0)
        this.FinishCapture();
      Monitor.Exit(this._captureLock);
    }

    private unsafe void DrawBytesToBuffer(byte[] sourceBuffer, byte[] destinationBuffer, int sourceBufferWidth, int destinationBufferWidth, Rectangle area)
    {
      fixed (byte* numPtr1 = &destinationBuffer[0])
        fixed (byte* numPtr2 = &sourceBuffer[0])
        {
          byte* numPtr3 = numPtr1 + (destinationBufferWidth * area.Y + area.X << 2);
          for (int index1 = 0; index1 < area.Height; ++index1)
          {
            for (int index2 = 0; index2 < area.Width; ++index2)
            {
              numPtr3[2] = *numPtr2;
              numPtr3[1] = numPtr2[1];
              *numPtr3 = numPtr2[2];
              numPtr3[3] = numPtr2[3];
              numPtr2 += 4;
              numPtr3 += 4;
            }
            numPtr2 += sourceBufferWidth - area.Width << 2;
            numPtr3 += destinationBufferWidth - area.Width << 2;
          }
        }
    }

    public float GetProgress()
    {
      return this._tilesProcessed / this._totalTiles;
    }

    private bool SaveImage(int width, int height, ImageFormat imageFormat, string filename)
    {
      try
      {
        Directory.CreateDirectory(Main.SavePath + (object) Path.DirectorySeparatorChar + "Captures" + (object) Path.DirectorySeparatorChar);
        using (Bitmap bitmap = new Bitmap(width, height))
        {
          Rectangle rect = new Rectangle(0, 0, width, height);
          BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
          Marshal.Copy(this._outputData, 0, bitmapdata.Scan0, width * height * 4);
          bitmap.UnlockBits(bitmapdata);
          bitmap.Save(filename, imageFormat);
          bitmap.Dispose();
        }
        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
        return false;
      }
    }

    private void SaveImage(Texture2D texture, int width, int height, ImageFormat imageFormat, string foldername, string filename)
    {
      string str = Main.SavePath + (object) Path.DirectorySeparatorChar + "Captures" + (object) Path.DirectorySeparatorChar + foldername;
      string filename1 = Path.Combine(str, filename);
      Directory.CreateDirectory(str);
      using (Bitmap bitmap = new Bitmap(width, height))
      {
        Rectangle rect = new Rectangle(0, 0, width, height);
        int num1 = texture.get_Width() * texture.get_Height() * 4;
        texture.GetData<byte>((M0[]) this._outputData, 0, num1);
        int index1 = 0;
        int index2 = 0;
        for (int index3 = 0; index3 < height; ++index3)
        {
          for (int index4 = 0; index4 < width; ++index4)
          {
            byte num2 = this._outputData[index1 + 2];
            this._outputData[index2 + 2] = this._outputData[index1];
            this._outputData[index2] = num2;
            this._outputData[index2 + 1] = this._outputData[index1 + 1];
            this._outputData[index2 + 3] = this._outputData[index1 + 3];
            index1 += 4;
            index2 += 4;
          }
          index1 += texture.get_Width() - width << 2;
        }
        BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
        Marshal.Copy(this._outputData, 0, bitmapdata.Scan0, width * height * 4);
        bitmap.UnlockBits(bitmapdata);
        bitmap.Save(filename1, imageFormat);
      }
    }

    private void FinishCapture()
    {
      if (this._activeSettings.UseScaling)
      {
        int num = 0;
        do
        {
          if (!this.SaveImage(this._outputImageSize.Width, this._outputImageSize.Height, ImageFormat.Png, Main.SavePath + (object) Path.DirectorySeparatorChar + "Captures" + (object) Path.DirectorySeparatorChar + this._activeSettings.OutputName + ".png"))
          {
            GC.Collect();
            Thread.Sleep(5);
            ++num;
            Console.WriteLine(Language.GetTextValue("Error.CaptureError"));
          }
          else
            goto label_5;
        }
        while (num <= 5);
        Console.WriteLine(Language.GetTextValue("Error.UnableToCapture"));
      }
label_5:
      this._outputData = (byte[]) null;
      this._scaledFrameData = (byte[]) null;
      Main.GlobalTimerPaused = false;
      CaptureInterface.EndCamera();
      if (this._scaledFrameBuffer != null)
      {
        ((GraphicsResource) this._scaledFrameBuffer).Dispose();
        this._scaledFrameBuffer = (RenderTarget2D) null;
      }
      this._activeSettings = (CaptureSettings) null;
    }

    public void Dispose()
    {
    }

    private class CaptureChunk
    {
      public readonly Rectangle Area;
      public readonly Rectangle ScaledArea;

      public CaptureChunk(Rectangle area, Rectangle scaledArea)
      {
        this.Area = area;
        this.ScaledArea = scaledArea;
      }
    }
  }
}
