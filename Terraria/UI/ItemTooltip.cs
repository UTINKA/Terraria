// Decompiled with JetBrains decompiler
// Type: Terraria.UI.ItemTooltip
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.UI
{
  public class ItemTooltip
  {
    public static readonly ItemTooltip None = new ItemTooltip();
    private static List<TooltipProcessor> _globalProcessors = new List<TooltipProcessor>();
    private string[] _tooltipLines;
    private GameCulture _lastCulture;
    private LocalizedText _text;
    private string _processedText;

    public int Lines
    {
      get
      {
        this.ValidateTooltip();
        if (this._tooltipLines == null)
          return 0;
        return this._tooltipLines.Length;
      }
    }

    private ItemTooltip()
    {
    }

    private ItemTooltip(string key)
    {
      this._text = Language.GetText(key);
    }

    public static ItemTooltip FromLanguageKey(string key)
    {
      if (!Language.Exists(key))
        return ItemTooltip.None;
      return new ItemTooltip(key);
    }

    public string GetLine(int line)
    {
      this.ValidateTooltip();
      return this._tooltipLines[line];
    }

    private void ValidateTooltip()
    {
      if (this._lastCulture == Language.ActiveCulture)
        return;
      this._lastCulture = Language.ActiveCulture;
      if (this._text == null)
      {
        this._tooltipLines = (string[]) null;
        this._processedText = string.Empty;
      }
      else
      {
        string tooltip = this._text.Value;
        foreach (TooltipProcessor globalProcessor in ItemTooltip._globalProcessors)
          tooltip = globalProcessor(tooltip);
        this._tooltipLines = tooltip.Split('\n');
        this._processedText = tooltip;
      }
    }

    public static void AddGlobalProcessor(TooltipProcessor processor)
    {
      ItemTooltip._globalProcessors.Add(processor);
    }

    public static void RemoveGlobalProcessor(TooltipProcessor processor)
    {
      ItemTooltip._globalProcessors.Remove(processor);
    }

    public static void ClearGlobalProcessors()
    {
      ItemTooltip._globalProcessors.Clear();
    }
  }
}
