// Decompiled with JetBrains decompiler
// Type: Terraria.Localization.LanguageManager
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Terraria.Utilities;

namespace Terraria.Localization
{
  public class LanguageManager
  {
    public static LanguageManager Instance = new LanguageManager();
    private Dictionary<string, LocalizedText> _localizedTexts = new Dictionary<string, LocalizedText>();
    private Dictionary<string, List<string>> _categoryGroupedKeys = new Dictionary<string, List<string>>();
    private GameCulture _fallbackCulture = GameCulture.English;

    public GameCulture ActiveCulture { get; private set; }

    public event LanguageChangeCallback OnLanguageChanging;

    public event LanguageChangeCallback OnLanguageChanged;

    private LanguageManager()
    {
      this._localizedTexts[""] = LocalizedText.Empty;
    }

    public int GetCategorySize(string name)
    {
      if (this._categoryGroupedKeys.ContainsKey(name))
        return this._categoryGroupedKeys[name].Count;
      return 0;
    }

    public void SetLanguage(int legacyId)
    {
      this.SetLanguage(GameCulture.FromLegacyId(legacyId));
    }

    public void SetLanguage(string cultureName)
    {
      this.SetLanguage(GameCulture.FromName(cultureName));
    }

    private void SetAllTextValuesToKeys()
    {
      foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
        localizedText.Value.SetValue(localizedText.Key);
    }

    private string[] GetLanguageFilesForCulture(GameCulture culture)
    {
      Assembly.GetExecutingAssembly();
      return Array.FindAll<string>(typeof (Program).Assembly.GetManifestResourceNames(), (Predicate<string>) (element =>
      {
        if (element.StartsWith("Terraria.Localization.Content." + culture.CultureInfo.Name))
          return element.EndsWith(".json");
        return false;
      }));
    }

    public void SetLanguage(GameCulture culture)
    {
      if (this.ActiveCulture == culture)
        return;
      if (culture != this._fallbackCulture && this.ActiveCulture != this._fallbackCulture)
      {
        this.SetAllTextValuesToKeys();
        this.LoadLanguage(this._fallbackCulture);
      }
      this.LoadLanguage(culture);
      this.ActiveCulture = culture;
      Thread.CurrentThread.CurrentCulture = culture.CultureInfo;
      Thread.CurrentThread.CurrentUICulture = culture.CultureInfo;
      if (this.OnLanguageChanged == null)
        return;
      this.OnLanguageChanged(this);
    }

    private void LoadLanguage(GameCulture culture)
    {
      this.LoadFilesForCulture(culture);
      if (this.OnLanguageChanging != null)
        this.OnLanguageChanging(this);
      this.ProcessCopyCommandsInTexts();
    }

    private void LoadFilesForCulture(GameCulture culture)
    {
      foreach (string path in this.GetLanguageFilesForCulture(culture))
      {
        try
        {
          string fileText = LanguageManager.ReadEmbeddedResource(path);
          if (fileText == null || fileText.Length < 2)
            throw new FileFormatException();
          this.LoadLanguageFromFileText(fileText);
        }
        catch (Exception ex)
        {
          if (Debugger.IsAttached)
            Debugger.Break();
          Console.WriteLine("Failed to load language file: " + path);
          break;
        }
      }
    }

    private static string ReadEmbeddedResource(string path)
    {
      using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
      {
        using (StreamReader streamReader = new StreamReader(manifestResourceStream))
          return streamReader.ReadToEnd();
      }
    }

    private void ProcessCopyCommandsInTexts()
    {
      Regex regex = new Regex("{\\$(\\w+\\.\\w+)}", RegexOptions.Compiled);
      foreach (KeyValuePair<string, LocalizedText> localizedText1 in this._localizedTexts)
      {
        LocalizedText localizedText2 = localizedText1.Value;
        for (int index = 0; index < 100; ++index)
        {
          string text = regex.Replace(localizedText2.Value, (MatchEvaluator) (match => this.GetTextValue(match.Groups[1].ToString())));
          if (!(text == localizedText2.Value))
            localizedText2.SetValue(text);
          else
            break;
        }
      }
    }

    public void LoadLanguageFromFileText(string fileText)
    {
      foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair1 in (Dictionary<string, Dictionary<string, string>>) JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(fileText))
      {
        string key1 = keyValuePair1.Key;
        foreach (KeyValuePair<string, string> keyValuePair2 in keyValuePair1.Value)
        {
          string key2 = keyValuePair1.Key + "." + keyValuePair2.Key;
          if (this._localizedTexts.ContainsKey(key2))
          {
            this._localizedTexts[key2].SetValue(keyValuePair2.Value);
          }
          else
          {
            this._localizedTexts.Add(key2, new LocalizedText(key2, keyValuePair2.Value));
            if (!this._categoryGroupedKeys.ContainsKey(keyValuePair1.Key))
              this._categoryGroupedKeys.Add(keyValuePair1.Key, new List<string>());
            this._categoryGroupedKeys[keyValuePair1.Key].Add(keyValuePair2.Key);
          }
        }
      }
    }

    [Conditional("DEBUG")]
    private void ValidateAllCharactersContainedInFont(DynamicSpriteFont font)
    {
      if (font == null)
        return;
      foreach (LocalizedText localizedText in this._localizedTexts.Values)
      {
        foreach (int num in localizedText.Value)
          ;
      }
    }

    public LocalizedText[] FindAll(Regex regex)
    {
      int length = 0;
      foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
      {
        if (regex.IsMatch(localizedText.Key))
          ++length;
      }
      LocalizedText[] localizedTextArray = new LocalizedText[length];
      int index = 0;
      foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
      {
        if (regex.IsMatch(localizedText.Key))
        {
          localizedTextArray[index] = localizedText.Value;
          ++index;
        }
      }
      return localizedTextArray;
    }

    public LocalizedText[] FindAll(LanguageSearchFilter filter)
    {
      LinkedList<LocalizedText> source = new LinkedList<LocalizedText>();
      foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
      {
        if (filter(localizedText.Key, localizedText.Value))
          source.AddLast(localizedText.Value);
      }
      return source.ToArray<LocalizedText>();
    }

    public LocalizedText SelectRandom(LanguageSearchFilter filter, UnifiedRandom random = null)
    {
      int maxValue = 0;
      foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
      {
        if (filter(localizedText.Key, localizedText.Value))
          ++maxValue;
      }
      int num = (random ?? Main.rand).Next(maxValue);
      foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
      {
        if (filter(localizedText.Key, localizedText.Value) && --maxValue == num)
          return localizedText.Value;
      }
      return LocalizedText.Empty;
    }

    public LocalizedText RandomFromCategory(string categoryName, UnifiedRandom random = null)
    {
      if (!this._categoryGroupedKeys.ContainsKey(categoryName))
        return new LocalizedText(categoryName + ".RANDOM", categoryName + ".RANDOM");
      List<string> categoryGroupedKey = this._categoryGroupedKeys[categoryName];
      return this.GetText(categoryName + "." + categoryGroupedKey[(random ?? Main.rand).Next(categoryGroupedKey.Count)]);
    }

    public bool Exists(string key)
    {
      return this._localizedTexts.ContainsKey(key);
    }

    public LocalizedText GetText(string key)
    {
      if (!this._localizedTexts.ContainsKey(key))
        return new LocalizedText(key, key);
      return this._localizedTexts[key];
    }

    public string GetTextValue(string key)
    {
      if (this._localizedTexts.ContainsKey(key))
        return this._localizedTexts[key].Value;
      return key;
    }

    public string GetTextValue(string key, object arg0)
    {
      if (this._localizedTexts.ContainsKey(key))
        return this._localizedTexts[key].Format(arg0);
      return key;
    }

    public string GetTextValue(string key, object arg0, object arg1)
    {
      if (this._localizedTexts.ContainsKey(key))
        return this._localizedTexts[key].Format(arg0, arg1);
      return key;
    }

    public string GetTextValue(string key, object arg0, object arg1, object arg2)
    {
      if (this._localizedTexts.ContainsKey(key))
        return this._localizedTexts[key].Format(arg0, arg1, arg2);
      return key;
    }

    public string GetTextValue(string key, params object[] args)
    {
      if (this._localizedTexts.ContainsKey(key))
        return this._localizedTexts[key].Format(args);
      return key;
    }

    public void SetFallbackCulture(GameCulture culture)
    {
      this._fallbackCulture = culture;
    }
  }
}
