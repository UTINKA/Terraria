// Decompiled with JetBrains decompiler
// Type: Terraria.Localization.GameCulture
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Terraria.Localization
{
  public class GameCulture
  {
    public static readonly GameCulture English = new GameCulture("en-US", 1);
    public static readonly GameCulture German = new GameCulture("de-DE", 2);
    public static readonly GameCulture Italian = new GameCulture("it-IT", 3);
    public static readonly GameCulture French = new GameCulture("fr-FR", 4);
    public static readonly GameCulture Spanish = new GameCulture("es-ES", 5);
    public static readonly GameCulture Russian = new GameCulture("ru-RU", 6);
    public static readonly GameCulture Chinese = new GameCulture("zh-Hans", 7);
    public static readonly GameCulture Portuguese = new GameCulture("pt-BR", 8);
    public static readonly GameCulture Polish = new GameCulture("pl-PL", 9);
    private static Dictionary<int, GameCulture> _legacyCultures;
    public readonly CultureInfo CultureInfo;
    public readonly int LegacyId;

    public bool IsActive
    {
      get
      {
        return Language.ActiveCulture == this;
      }
    }

    public string Name
    {
      get
      {
        return this.CultureInfo.Name;
      }
    }

    public GameCulture(string name, int legacyId)
    {
      this.CultureInfo = new CultureInfo(name);
      this.LegacyId = legacyId;
      GameCulture.RegisterLegacyCulture(this, legacyId);
    }

    public static GameCulture FromLegacyId(int id)
    {
      if (id < 1)
        id = 1;
      return GameCulture._legacyCultures[id];
    }

    public static GameCulture FromName(string name)
    {
      return GameCulture._legacyCultures.Values.SingleOrDefault<GameCulture>((Func<GameCulture, bool>) (culture => culture.Name == name)) ?? GameCulture.English;
    }

    private static void RegisterLegacyCulture(GameCulture culture, int legacyId)
    {
      if (GameCulture._legacyCultures == null)
        GameCulture._legacyCultures = new Dictionary<int, GameCulture>();
      GameCulture._legacyCultures.Add(legacyId, culture);
    }
  }
}
