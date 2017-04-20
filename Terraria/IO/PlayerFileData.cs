// Decompiled with JetBrains decompiler
// Type: Terraria.IO.PlayerFileData
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria.IO
{
  public class PlayerFileData : FileData
  {
    private TimeSpan _playTime = TimeSpan.Zero;
    private Stopwatch _timer = new Stopwatch();
    private Player _player;
    private bool _isTimerActive;

    public Player Player
    {
      get
      {
        return this._player;
      }
      set
      {
        this._player = value;
        if (value == null)
          return;
        this.Name = this._player.name;
      }
    }

    public PlayerFileData()
      : base("Player")
    {
    }

    public PlayerFileData(string path, bool cloudSave)
      : base("Player", path, cloudSave)
    {
    }

    public static PlayerFileData CreateAndSave(Player player)
    {
      PlayerFileData playerFile = new PlayerFileData();
      playerFile.Metadata = FileMetadata.FromCurrentSettings(FileType.Player);
      playerFile.Player = player;
      playerFile._isCloudSave = SocialAPI.Cloud != null && SocialAPI.Cloud.EnabledByDefault;
      playerFile._path = Main.GetPlayerPathFromName(player.name, playerFile.IsCloudSave);
      (playerFile.IsCloudSave ? Main.CloudFavoritesData : Main.LocalFavoriteData).ClearEntry((FileData) playerFile);
      Player.SavePlayer(playerFile, true);
      return playerFile;
    }

    public override void SetAsActive()
    {
      Main.ActivePlayerFileData = this;
      Main.player[Main.myPlayer] = this.Player;
    }

    public override void MoveToCloud()
    {
      if (this.IsCloudSave || SocialAPI.Cloud == null)
        return;
      string playerPathFromName = Main.GetPlayerPathFromName(this.Name, true);
      if (!FileUtilities.MoveToCloud(this.Path, playerPathFromName))
        return;
      string fileName = this.GetFileName(false);
      string path = Main.PlayerPath + (object) Path.DirectorySeparatorChar + fileName + (object) Path.DirectorySeparatorChar;
      if (Directory.Exists(path))
      {
        string[] files = Directory.GetFiles(path);
        for (int index = 0; index < files.Length; ++index)
        {
          string cloudPath = Main.CloudPlayerPath + "/" + fileName + "/" + FileUtilities.GetFileName(files[index], true);
          FileUtilities.MoveToCloud(files[index], cloudPath);
        }
      }
      Main.LocalFavoriteData.ClearEntry((FileData) this);
      this._isCloudSave = true;
      this._path = playerPathFromName;
      Main.CloudFavoritesData.SaveFavorite((FileData) this);
    }

    public override void MoveToLocal()
    {
      if (!this.IsCloudSave || SocialAPI.Cloud == null)
        return;
      string playerPathFromName = Main.GetPlayerPathFromName(this.Name, false);
      if (!FileUtilities.MoveToLocal(this.Path, playerPathFromName))
        return;
      string fileName = this.GetFileName(false);
      string mapPath = Path.Combine(Main.CloudPlayerPath, fileName);
      foreach (string str in SocialAPI.Cloud.GetFiles().Where<string>((Func<string, bool>) (path =>
      {
        if (path.StartsWith(mapPath, StringComparison.CurrentCultureIgnoreCase))
          return path.EndsWith(".map", StringComparison.CurrentCultureIgnoreCase);
        return false;
      })))
      {
        string localPath = Path.Combine(Main.PlayerPath, fileName, FileUtilities.GetFileName(str, true));
        FileUtilities.MoveToLocal(str, localPath);
      }
      Main.CloudFavoritesData.ClearEntry((FileData) this);
      this._isCloudSave = false;
      this._path = playerPathFromName;
      Main.LocalFavoriteData.SaveFavorite((FileData) this);
    }

    public void UpdatePlayTimer()
    {
      if (Main.instance.get_IsActive() && !Main.gamePaused && (Main.hasFocus && this._isTimerActive))
        this.StartPlayTimer();
      else
        this.PausePlayTimer();
    }

    public void StartPlayTimer()
    {
      this._isTimerActive = true;
      if (this._timer.IsRunning)
        return;
      this._timer.Start();
    }

    public void PausePlayTimer()
    {
      if (!this._timer.IsRunning)
        return;
      this._timer.Stop();
    }

    public TimeSpan GetPlayTime()
    {
      if (this._timer.IsRunning)
        return this._playTime + this._timer.Elapsed;
      return this._playTime;
    }

    public void StopPlayTimer()
    {
      this._isTimerActive = false;
      if (!this._timer.IsRunning)
        return;
      this._playTime += this._timer.Elapsed;
      this._timer.Reset();
    }

    public void SetPlayTime(TimeSpan time)
    {
      this._playTime = time;
    }
  }
}
