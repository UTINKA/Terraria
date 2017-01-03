// Decompiled with JetBrains decompiler
// Type: Terraria.IO.Preferences
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Terraria.Localization;

namespace Terraria.IO
{
  public class Preferences
  {
    private Dictionary<string, object> _data = new Dictionary<string, object>();
    private readonly object _lock = new object();
    private readonly string _path;
    private readonly JsonSerializerSettings _serializerSettings;
    public readonly bool UseBson;
    public bool AutoSave;

    public event Action<Preferences> OnSave;

    public event Action<Preferences> OnLoad;

    public event Preferences.TextProcessAction OnProcessText;

    public Preferences(string path, bool parseAllTypes = false, bool useBson = false)
    {
      this._path = path;
      this.UseBson = useBson;
      if (parseAllTypes)
      {
        JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
        serializerSettings.set_TypeNameHandling((TypeNameHandling) 4);
        serializerSettings.set_MetadataPropertyHandling((MetadataPropertyHandling) 1);
        serializerSettings.set_Formatting((Formatting) 1);
        this._serializerSettings = serializerSettings;
      }
      else
      {
        JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
        serializerSettings.set_Formatting((Formatting) 1);
        this._serializerSettings = serializerSettings;
      }
    }

    public bool Load()
    {
      lock (this._lock)
      {
        if (!File.Exists(this._path))
          return false;
        try
        {
          if (!this.UseBson)
          {
            this._data = (Dictionary<string, object>) JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(this._path), this._serializerSettings);
          }
          else
          {
            using (FileStream resource_1 = File.OpenRead(this._path))
            {
              using (BsonReader resource_0 = new BsonReader((Stream) resource_1))
                this._data = (Dictionary<string, object>) JsonSerializer.Create(this._serializerSettings).Deserialize<Dictionary<string, object>>((JsonReader) resource_0);
            }
          }
          if (this._data == null)
            this._data = new Dictionary<string, object>();
          if (this.OnLoad != null)
            this.OnLoad(this);
          return true;
        }
        catch (Exception exception_0)
        {
          return false;
        }
      }
    }

    public bool Save(bool createFile = true)
    {
      lock (this._lock)
      {
        try
        {
          if (this.OnSave != null)
            this.OnSave(this);
          if (!createFile && !File.Exists(this._path))
            return false;
          Directory.GetParent(this._path).Create();
          if (!createFile)
            File.SetAttributes(this._path, FileAttributes.Normal);
          if (!this.UseBson)
          {
            string local_0 = JsonConvert.SerializeObject((object) this._data, this._serializerSettings);
            if (this.OnProcessText != null)
              this.OnProcessText(ref local_0);
            File.WriteAllText(this._path, local_0);
            File.SetAttributes(this._path, FileAttributes.Normal);
          }
          else
          {
            using (FileStream resource_1 = File.Create(this._path))
            {
              using (BsonWriter resource_0 = new BsonWriter((Stream) resource_1))
              {
                File.SetAttributes(this._path, FileAttributes.Normal);
                JsonSerializer.Create(this._serializerSettings).Serialize((JsonWriter) resource_0, (object) this._data);
              }
            }
          }
        }
        catch (Exception exception_0)
        {
          Console.WriteLine(Language.GetTextValue("Error.UnableToWritePreferences", (object) this._path));
          Console.WriteLine(exception_0.ToString());
          Monitor.Exit(this._lock);
          return false;
        }
        return true;
      }
    }

    public void Clear()
    {
      this._data.Clear();
    }

    public void Put(string name, object value)
    {
      lock (this._lock)
      {
        this._data[name] = value;
        if (!this.AutoSave)
          return;
        this.Save(true);
      }
    }

    public T Get<T>(string name, T defaultValue)
    {
      lock (this._lock)
      {
        try
        {
          object local_0;
          if (!this._data.TryGetValue(name, out local_0))
            return defaultValue;
          if (local_0 is T)
            return (T) local_0;
          if (local_0 is JObject)
            return JsonConvert.DeserializeObject<T>(((object) (JObject) local_0).ToString());
          return (T) Convert.ChangeType(local_0, typeof (T));
        }
        catch
        {
          return defaultValue;
        }
      }
    }

    public void Get<T>(string name, ref T currentValue)
    {
      currentValue = this.Get<T>(name, currentValue);
    }

    public List<string> GetAllKeys()
    {
      return this._data.Keys.ToList<string>();
    }

    public delegate void TextProcessAction(ref string text);
  }
}
