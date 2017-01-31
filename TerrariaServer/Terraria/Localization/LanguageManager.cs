// Decompiled with JetBrains decompiler
// Type: Terraria.Localization.LanguageManager
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Terraria.Utilities;

namespace Terraria.Localization
{
  public class LanguageManager
  {
    public static LanguageManager Instance = new LanguageManager();
    private string _language = "";
    private Dictionary<string, LocalizedText> _languageMap = new Dictionary<string, LocalizedText>();
    private Dictionary<string, List<string>> _categoryLists = new Dictionary<string, List<string>>();

    public event LanguageChangedCallback OnLanguageChanged;

    private LanguageManager()
    {
    }

    public int GetCategorySize(string name)
    {
      if (this._categoryLists.ContainsKey(name))
        return this._categoryLists[name].Count;
      return 0;
    }

    public void SetLanguage(string name)
    {
      if (this._language == name)
        return;
      if (this._language != "English" && name != "English")
      {
        foreach (KeyValuePair<string, LocalizedText> language in this._languageMap)
          language.Value.SetValue(language.Key);
        this.SetLanguage("English");
      }
      this._language = name;
      Assembly executingAssembly = Assembly.GetExecutingAssembly();
      foreach (string name1 in Array.FindAll<string>(typeof (Program).Assembly.GetManifestResourceNames(), (Predicate<string>) (element =>
      {
        if (element.StartsWith("Terraria.Localization.Content." + name))
          return element.EndsWith(".json");
        return false;
      })))
      {
        try
        {
          using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(name1))
          {
            using (StreamReader streamReader = new StreamReader(manifestResourceStream))
            {
              string end = streamReader.ReadToEnd();
              if (end == null || end.Length < 2)
              {
                Console.WriteLine("Failed to load language file: " + name1);
                return;
              }
              this.LoadLanguageFromFileText(end);
            }
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine("Failed to load language file: " + name1);
          return;
        }
      }
      if (this.OnLanguageChanged == null)
        return;
      this.OnLanguageChanged(this);
    }

    public void LoadLanguageFromFileText(string fileText)
    {
      foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair1 in (Dictionary<string, Dictionary<string, string>>) JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(fileText))
      {
        string key1 = keyValuePair1.Key;
        foreach (KeyValuePair<string, string> keyValuePair2 in keyValuePair1.Value)
        {
          string key2 = keyValuePair1.Key + "." + keyValuePair2.Key;
          if (this._languageMap.ContainsKey(key2))
          {
            this._languageMap[key2].SetValue(keyValuePair2.Value);
          }
          else
          {
            this._languageMap.Add(key2, new LocalizedText(keyValuePair2.Value));
            if (!this._categoryLists.ContainsKey(keyValuePair1.Key))
              this._categoryLists.Add(keyValuePair1.Key, new List<string>());
            this._categoryLists[keyValuePair1.Key].Add(keyValuePair2.Key);
          }
        }
      }
    }

    public LocalizedText[] FindAll(Regex regex)
    {
      int length = 0;
      foreach (KeyValuePair<string, LocalizedText> language in this._languageMap)
      {
        if (regex.IsMatch(language.Key))
          ++length;
      }
      LocalizedText[] localizedTextArray = new LocalizedText[length];
      int index = 0;
      foreach (KeyValuePair<string, LocalizedText> language in this._languageMap)
      {
        if (regex.IsMatch(language.Key))
        {
          localizedTextArray[index] = language.Value;
          ++index;
        }
      }
      return localizedTextArray;
    }

    public LocalizedText[] FindAll(LanguageSearchFilter filter)
    {
      LinkedList<LocalizedText> source = new LinkedList<LocalizedText>();
      foreach (KeyValuePair<string, LocalizedText> language in this._languageMap)
      {
        if (filter(language.Key, language.Value))
          source.AddLast(language.Value);
      }
      return source.ToArray<LocalizedText>();
    }

    public LocalizedText RandomFromCategory(string categoryName, UnifiedRandom random = null)
    {
      if (!this._categoryLists.ContainsKey(categoryName))
        return new LocalizedText(categoryName + ".RANDOM");
      List<string> categoryList = this._categoryLists[categoryName];
      return this.GetText(categoryName + "." + categoryList[(random ?? Main.rand).Next(categoryList.Count)]);
    }

    public bool Exists(string key)
    {
      return this._languageMap.ContainsKey(key);
    }

    public LocalizedText GetText(string key)
    {
      if (!this._languageMap.ContainsKey(key))
        return new LocalizedText(key);
      return this._languageMap[key];
    }

    public string GetTextValue(string key)
    {
      if (this._languageMap.ContainsKey(key))
        return this._languageMap[key].Value;
      return key;
    }

    public string GetTextValue(string key, object arg0)
    {
      if (this._languageMap.ContainsKey(key))
        return this._languageMap[key].Format(arg0);
      return key;
    }

    public string GetTextValue(string key, object arg0, object arg1)
    {
      if (this._languageMap.ContainsKey(key))
        return this._languageMap[key].Format(arg0, arg1);
      return key;
    }

    public string GetTextValue(string key, object arg0, object arg1, object arg2)
    {
      if (this._languageMap.ContainsKey(key))
        return this._languageMap[key].Format(arg0, arg1, arg2);
      return key;
    }

    public string GetTextValue(string key, params object[] args)
    {
      if (this._languageMap.ContainsKey(key))
        return this._languageMap[key].Format(args);
      return key;
    }
  }
}
