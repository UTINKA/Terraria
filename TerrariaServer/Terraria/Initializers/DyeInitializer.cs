// Decompiled with JetBrains decompiler
// Type: Terraria.Initializers.DyeInitializer
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent.Dyes;
using Terraria.Graphics.Shaders;

namespace Terraria.Initializers
{
  public static class DyeInitializer
  {
    private static void LoadBasicColorDye(int baseDyeItem, int blackDyeItem, int brightDyeItem, int silverDyeItem, float r, float g, float b, float saturation = 1f, int oldShader = 1)
    {
      Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
      GameShaders.Armor.BindShader<ArmorShaderData>(baseDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColored")).UseColor(r, g, b).UseSaturation(saturation);
      GameShaders.Armor.BindShader<ArmorShaderData>(blackDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlack")).UseColor(r, g, b).UseSaturation(saturation);
      GameShaders.Armor.BindShader<ArmorShaderData>(brightDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColored")).UseColor((float) ((double) r * 0.5 + 0.5), (float) ((double) g * 0.5 + 0.5), (float) ((double) b * 0.5 + 0.5)).UseSaturation(saturation);
      GameShaders.Armor.BindShader<ArmorShaderData>(silverDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrim")).UseColor(r, g, b).UseSaturation(saturation);
    }

    private static void LoadBasicColorDye(int baseDyeItem, float r, float g, float b, float saturation = 1f, int oldShader = 1)
    {
      DyeInitializer.LoadBasicColorDye(baseDyeItem, baseDyeItem + 12, baseDyeItem + 31, baseDyeItem + 44, r, g, b, saturation, oldShader);
    }

    private static void LoadBasicColorDyes()
    {
      DyeInitializer.LoadBasicColorDye(1007, 1f, 0.0f, 0.0f, 1.2f, 1);
      DyeInitializer.LoadBasicColorDye(1008, 1f, 0.5f, 0.0f, 1.2f, 2);
      DyeInitializer.LoadBasicColorDye(1009, 1f, 1f, 0.0f, 1.2f, 3);
      DyeInitializer.LoadBasicColorDye(1010, 0.5f, 1f, 0.0f, 1.2f, 4);
      DyeInitializer.LoadBasicColorDye(1011, 0.0f, 1f, 0.0f, 1.2f, 5);
      DyeInitializer.LoadBasicColorDye(1012, 0.0f, 1f, 0.5f, 1.2f, 6);
      DyeInitializer.LoadBasicColorDye(1013, 0.0f, 1f, 1f, 1.2f, 7);
      DyeInitializer.LoadBasicColorDye(1014, 0.2f, 0.5f, 1f, 1.2f, 8);
      DyeInitializer.LoadBasicColorDye(1015, 0.0f, 0.0f, 1f, 1.2f, 9);
      DyeInitializer.LoadBasicColorDye(1016, 0.5f, 0.0f, 1f, 1.2f, 10);
      DyeInitializer.LoadBasicColorDye(1017, 1f, 0.0f, 1f, 1.2f, 11);
      DyeInitializer.LoadBasicColorDye(1018, 1f, 0.1f, 0.5f, 1.3f, 12);
      DyeInitializer.LoadBasicColorDye(2874, 2875, 2876, 2877, 0.4f, 0.2f, 0.0f, 1f, 1);
    }

    private static void LoadArmorDyes()
    {
      Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
      DyeInitializer.LoadBasicColorDyes();
      GameShaders.Armor.BindShader<ArmorShaderData>(1050, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(0.6f, 0.6f, 0.6f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1037, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(1f, 1f, 1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3558, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(1.5f, 1.5f, 1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2871, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(0.05f, 0.05f, 0.05f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3559, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlack")).UseColor(1f, 1f, 1f).UseSaturation(1.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1031, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(1f, 0.0f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f).UseSaturation(1.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1032, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlackGradient")).UseColor(1f, 0.0f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3550, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrimGradient")).UseColor(1f, 0.0f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1063, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessGradient")).UseColor(1f, 0.0f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1035, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0.0f, 0.0f, 1f).UseSecondaryColor(0.0f, 1f, 1f).UseSaturation(1.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1036, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlackGradient")).UseColor(0.0f, 0.0f, 1f).UseSecondaryColor(0.0f, 1f, 1f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3552, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrimGradient")).UseColor(0.0f, 0.0f, 1f).UseSecondaryColor(0.0f, 1f, 1f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1065, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessGradient")).UseColor(0.0f, 0.0f, 1f).UseSecondaryColor(0.0f, 1f, 1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1033, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0.0f, 1f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f).UseSaturation(1.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1034, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlackGradient")).UseColor(0.0f, 1f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3551, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrimGradient")).UseColor(0.0f, 1f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1064, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessGradient")).UseColor(0.0f, 1f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1068, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0.5f, 1f, 0.0f).UseSecondaryColor(1f, 0.5f, 0.0f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1069, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0.0f, 1f, 0.5f).UseSecondaryColor(0.0f, 0.5f, 1f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1070, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(1f, 0.0f, 0.5f).UseSecondaryColor(0.5f, 0.0f, 1f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1066, new ArmorShaderData(pixelShaderRef, "ArmorColoredRainbow"));
      GameShaders.Armor.BindShader<ArmorShaderData>(1067, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessRainbow"));
      GameShaders.Armor.BindShader<ArmorShaderData>(3556, new ArmorShaderData(pixelShaderRef, "ArmorMidnightRainbow"));
      GameShaders.Armor.BindShader<ArmorShaderData>(2869, new ArmorShaderData(pixelShaderRef, "ArmorLivingFlame")).UseColor(1f, 0.9f, 0.0f).UseSecondaryColor(1f, 0.2f, 0.0f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2870, new ArmorShaderData(pixelShaderRef, "ArmorLivingRainbow"));
      GameShaders.Armor.BindShader<ArmorShaderData>(2873, new ArmorShaderData(pixelShaderRef, "ArmorLivingOcean"));
      GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3026, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(1f, 1f, 1f);
      GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3027, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(1.5f, 1.2f, 0.5f);
      GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3553, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(1.35f, 0.7f, 0.4f);
      GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3554, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(0.25f, 0.0f, 0.7f);
      GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3555, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(0.4f, 0.4f, 0.4f);
      GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3190, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflective"));
      GameShaders.Armor.BindShader<TeamArmorShaderData>(1969, new TeamArmorShaderData(pixelShaderRef, "ArmorColored"));
      GameShaders.Armor.BindShader<ArmorShaderData>(2864, new ArmorShaderData(pixelShaderRef, "ArmorMartian")).UseColor(0.0f, 2f, 3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2872, new ArmorShaderData(pixelShaderRef, "ArmorInvert"));
      GameShaders.Armor.BindShader<ArmorShaderData>(2878, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(0.7f, 1f, 0.9f).UseSecondaryColor(0.35f, 0.85f, 0.8f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2879, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(1f, 1.2f, 0.0f).UseSecondaryColor(1f, 0.6f, 0.3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2885, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(1.2f, 0.8f, 0.0f).UseSecondaryColor(0.8f, 0.2f, 0.0f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2884, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(1f, 0.0f, 1f).UseSecondaryColor(1f, 0.3f, 0.6f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2883, new ArmorShaderData(pixelShaderRef, "ArmorHighContrastGlow")).UseColor(0.0f, 1f, 0.0f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3025, new ArmorShaderData(pixelShaderRef, "ArmorFlow")).UseColor(1f, 0.5f, 1f).UseSecondaryColor(0.6f, 0.1f, 1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3039, new ArmorShaderData(pixelShaderRef, "ArmorTwilight")).UseImage("Images/Misc/noise").UseColor(0.5f, 0.1f, 1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3040, new ArmorShaderData(pixelShaderRef, "ArmorAcid")).UseColor(0.5f, 1f, 0.3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3041, new ArmorShaderData(pixelShaderRef, "ArmorMushroom")).UseColor(0.05f, 0.2f, 1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3042, new ArmorShaderData(pixelShaderRef, "ArmorPhase")).UseImage("Images/Misc/noise").UseColor(0.4f, 0.2f, 1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3560, new ArmorShaderData(pixelShaderRef, "ArmorAcid")).UseColor(0.9f, 0.2f, 0.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3561, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(0.4f, 0.7f, 1.4f).UseSecondaryColor(0.0f, 0.0f, 0.1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3562, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(1.4f, 0.75f, 1f).UseSecondaryColor(0.45f, 0.1f, 0.3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3024, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(-0.5f, -1f, 0.0f).UseSecondaryColor(1.5f, 1f, 2.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3534, new ArmorShaderData(pixelShaderRef, "ArmorMirage"));
      GameShaders.Armor.BindShader<ArmorShaderData>(3028, new ArmorShaderData(pixelShaderRef, "ArmorAcid")).UseColor(0.5f, 0.7f, 1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3557, new ArmorShaderData(pixelShaderRef, "ArmorPolarized"));
      GameShaders.Armor.BindShader<ArmorShaderData>(3038, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(0.5f, 0.7f, 1.3f).UseSecondaryColor(0.5f, 0.7f, 1.3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3600, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(0.7f, 0.4f, 1.5f).UseSecondaryColor(0.7f, 0.4f, 1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3597, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(1.5f, 0.6f, 0.4f).UseSecondaryColor(1.5f, 0.6f, 0.4f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3598, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(0.1f, 0.1f, 0.1f).UseSecondaryColor(0.4f, 0.05f, 0.025f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3599, new ArmorShaderData(pixelShaderRef, "ArmorLoki")).UseColor(0.1f, 0.1f, 0.1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3533, new ArmorShaderData(pixelShaderRef, "ArmorShiftingSands")).UseImage("Images/Misc/noise").UseColor(1.1f, 1f, 0.5f).UseSecondaryColor(0.7f, 0.5f, 0.3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3535, new ArmorShaderData(pixelShaderRef, "ArmorShiftingPearlsands")).UseImage("Images/Misc/noise").UseColor(1.1f, 0.8f, 0.9f).UseSecondaryColor(0.35f, 0.25f, 0.44f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3526, new ArmorShaderData(pixelShaderRef, "ArmorSolar")).UseColor(1f, 0.0f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3527, new ArmorShaderData(pixelShaderRef, "ArmorNebula")).UseImage("Images/Misc/noise").UseColor(1f, 0.0f, 1f).UseSecondaryColor(1f, 1f, 1f).UseSaturation(1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3528, new ArmorShaderData(pixelShaderRef, "ArmorVortex")).UseImage("Images/Misc/noise").UseColor(0.1f, 0.5f, 0.35f).UseSecondaryColor(1f, 1f, 1f).UseSaturation(1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3529, new ArmorShaderData(pixelShaderRef, "ArmorStardust")).UseImage("Images/Misc/noise").UseColor(0.4f, 0.6f, 1f).UseSecondaryColor(1f, 1f, 1f).UseSaturation(1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3530, new ArmorShaderData(pixelShaderRef, "ArmorVoid"));
      DyeInitializer.FixRecipes();
    }

    private static void LoadHairDyes()
    {
      Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
      DyeInitializer.LoadLegacyHairdyes();
      GameShaders.Hair.BindShader<HairShaderData>(3259, new HairShaderData(pixelShaderRef, "ArmorTwilight")).UseImage("Images/Misc/noise").UseColor(0.5f, 0.1f, 1f);
    }

    private static void LoadLegacyHairdyes()
    {
      Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1977, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        // ISSUE: explicit reference operation
        ((Color) @newColor).set_R((byte) ((double) player.statLife / (double) player.statLifeMax2 * 235.0 + 20.0));
        // ISSUE: explicit reference operation
        ((Color) @newColor).set_B((byte) 20);
        // ISSUE: explicit reference operation
        ((Color) @newColor).set_G((byte) 20);
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1978, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        // ISSUE: explicit reference operation
        ((Color) @newColor).set_R((byte) ((1.0 - (double) player.statMana / (double) player.statManaMax2) * 200.0 + 50.0));
        // ISSUE: explicit reference operation
        ((Color) @newColor).set_B(byte.MaxValue);
        // ISSUE: explicit reference operation
        ((Color) @newColor).set_G((byte) ((1.0 - (double) player.statMana / (double) player.statManaMax2) * 180.0 + 75.0));
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1979, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        float num1 = (float) (Main.worldSurface * 0.45) * 16f;
        float num2 = (float) (Main.worldSurface + Main.rockLayer) * 8f;
        float num3 = ((float) Main.rockLayer + (float) Main.maxTilesY) * 8f;
        float num4 = (float) (Main.maxTilesY - 150) * 16f;
        Vector2 center = player.Center;
        if (center.Y < (double) num1)
        {
          float num5 = (float) center.Y / num1;
          float num6 = 1f - num5;
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_R((byte) (116.0 * (double) num6 + 28.0 * (double) num5));
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_G((byte) (160.0 * (double) num6 + 216.0 * (double) num5));
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_B((byte) (249.0 * (double) num6 + 94.0 * (double) num5));
        }
        else if (center.Y < (double) num2)
        {
          float num5 = num1;
          float num6 = (float) ((center.Y - (double) num5) / ((double) num2 - (double) num5));
          float num7 = 1f - num6;
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_R((byte) (28.0 * (double) num7 + 151.0 * (double) num6));
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_G((byte) (216.0 * (double) num7 + 107.0 * (double) num6));
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_B((byte) (94.0 * (double) num7 + 75.0 * (double) num6));
        }
        else if (center.Y < (double) num3)
        {
          float num5 = num2;
          float num6 = (float) ((center.Y - (double) num5) / ((double) num3 - (double) num5));
          float num7 = 1f - num6;
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_R((byte) (151.0 * (double) num7 + 128.0 * (double) num6));
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_G((byte) (107.0 * (double) num7 + 128.0 * (double) num6));
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_B((byte) (75.0 * (double) num7 + 128.0 * (double) num6));
        }
        else if (center.Y < (double) num4)
        {
          float num5 = num3;
          float num6 = (float) ((center.Y - (double) num5) / ((double) num4 - (double) num5));
          float num7 = 1f - num6;
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_R((byte) (128.0 * (double) num7 + (double) byte.MaxValue * (double) num6));
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_G((byte) (128.0 * (double) num7 + 50.0 * (double) num6));
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_B((byte) (128.0 * (double) num7 + 15.0 * (double) num6));
        }
        else
        {
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_R(byte.MaxValue);
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_G((byte) 50);
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_B((byte) 10);
        }
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1980, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        int num1 = 0;
        for (int index = 0; index < 54; ++index)
        {
          if (player.inventory[index].type == 71)
            num1 += player.inventory[index].stack;
          if (player.inventory[index].type == 72)
            num1 += player.inventory[index].stack * 100;
          if (player.inventory[index].type == 73)
            num1 += player.inventory[index].stack * 10000;
          if (player.inventory[index].type == 74)
            num1 += player.inventory[index].stack * 1000000;
        }
        float num2 = (float) Item.buyPrice(0, 5, 0, 0);
        float num3 = (float) Item.buyPrice(0, 50, 0, 0);
        float num4 = (float) Item.buyPrice(2, 0, 0, 0);
        Color color1;
        // ISSUE: explicit reference operation
        ((Color) @color1).\u002Ector(226, 118, 76);
        Color color2;
        // ISSUE: explicit reference operation
        ((Color) @color2).\u002Ector(174, 194, 196);
        Color color3;
        // ISSUE: explicit reference operation
        ((Color) @color3).\u002Ector(204, 181, 72);
        Color color4;
        // ISSUE: explicit reference operation
        ((Color) @color4).\u002Ector(161, 172, 173);
        if ((double) num1 < (double) num2)
        {
          float num5 = (float) num1 / num2;
          float num6 = 1f - num5;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_R((byte) ((double) ((Color) @color1).get_R() * (double) num6 + (double) ((Color) @color2).get_R() * (double) num5));
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_G((byte) ((double) ((Color) @color1).get_G() * (double) num6 + (double) ((Color) @color2).get_G() * (double) num5));
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_B((byte) ((double) ((Color) @color1).get_B() * (double) num6 + (double) ((Color) @color2).get_B() * (double) num5));
        }
        else if ((double) num1 < (double) num3)
        {
          float num5 = num2;
          float num6 = (float) (((double) num1 - (double) num5) / ((double) num3 - (double) num5));
          float num7 = 1f - num6;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_R((byte) ((double) ((Color) @color2).get_R() * (double) num7 + (double) ((Color) @color3).get_R() * (double) num6));
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_G((byte) ((double) ((Color) @color2).get_G() * (double) num7 + (double) ((Color) @color3).get_G() * (double) num6));
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_B((byte) ((double) ((Color) @color2).get_B() * (double) num7 + (double) ((Color) @color3).get_B() * (double) num6));
        }
        else if ((double) num1 < (double) num4)
        {
          float num5 = num3;
          float num6 = (float) (((double) num1 - (double) num5) / ((double) num4 - (double) num5));
          float num7 = 1f - num6;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_R((byte) ((double) ((Color) @color3).get_R() * (double) num7 + (double) ((Color) @color4).get_R() * (double) num6));
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_G((byte) ((double) ((Color) @color3).get_G() * (double) num7 + (double) ((Color) @color4).get_G() * (double) num6));
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_B((byte) ((double) ((Color) @color3).get_B() * (double) num7 + (double) ((Color) @color4).get_B() * (double) num6));
        }
        else
          newColor = color4;
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1981, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        Color color1;
        // ISSUE: explicit reference operation
        ((Color) @color1).\u002Ector(1, 142, (int) byte.MaxValue);
        Color color2;
        // ISSUE: explicit reference operation
        ((Color) @color2).\u002Ector((int) byte.MaxValue, (int) byte.MaxValue, 0);
        Color color3;
        // ISSUE: explicit reference operation
        ((Color) @color3).\u002Ector(211, 45, (int) sbyte.MaxValue);
        Color color4;
        // ISSUE: explicit reference operation
        ((Color) @color4).\u002Ector(67, 44, 118);
        if (Main.dayTime)
        {
          if (Main.time < 27000.0)
          {
            float num1 = (float) (Main.time / 27000.0);
            float num2 = 1f - num1;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            ((Color) @newColor).set_R((byte) ((double) ((Color) @color1).get_R() * (double) num2 + (double) ((Color) @color2).get_R() * (double) num1));
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            ((Color) @newColor).set_G((byte) ((double) ((Color) @color1).get_G() * (double) num2 + (double) ((Color) @color2).get_G() * (double) num1));
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            ((Color) @newColor).set_B((byte) ((double) ((Color) @color1).get_B() * (double) num2 + (double) ((Color) @color2).get_B() * (double) num1));
          }
          else
          {
            float num1 = 27000f;
            float num2 = (float) ((Main.time - (double) num1) / (54000.0 - (double) num1));
            float num3 = 1f - num2;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            ((Color) @newColor).set_R((byte) ((double) ((Color) @color2).get_R() * (double) num3 + (double) ((Color) @color3).get_R() * (double) num2));
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            ((Color) @newColor).set_G((byte) ((double) ((Color) @color2).get_G() * (double) num3 + (double) ((Color) @color3).get_G() * (double) num2));
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            ((Color) @newColor).set_B((byte) ((double) ((Color) @color2).get_B() * (double) num3 + (double) ((Color) @color3).get_B() * (double) num2));
          }
        }
        else if (Main.time < 16200.0)
        {
          float num1 = (float) (Main.time / 16200.0);
          float num2 = 1f - num1;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_R((byte) ((double) ((Color) @color3).get_R() * (double) num2 + (double) ((Color) @color4).get_R() * (double) num1));
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_G((byte) ((double) ((Color) @color3).get_G() * (double) num2 + (double) ((Color) @color4).get_G() * (double) num1));
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_B((byte) ((double) ((Color) @color3).get_B() * (double) num2 + (double) ((Color) @color4).get_B() * (double) num1));
        }
        else
        {
          float num1 = 16200f;
          float num2 = (float) ((Main.time - (double) num1) / (32400.0 - (double) num1));
          float num3 = 1f - num2;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_R((byte) ((double) ((Color) @color4).get_R() * (double) num3 + (double) ((Color) @color1).get_R() * (double) num2));
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_G((byte) ((double) ((Color) @color4).get_G() * (double) num3 + (double) ((Color) @color1).get_G() * (double) num2));
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Color) @newColor).set_B((byte) ((double) ((Color) @color4).get_B() * (double) num3 + (double) ((Color) @color1).get_B() * (double) num2));
        }
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1982, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        if (player.team >= 0 && player.team < Main.teamColor.Length)
          newColor = Main.teamColor[player.team];
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1983, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        Color color1 = (Color) null;
        if (Main.waterStyle == 2)
        {
          // ISSUE: explicit reference operation
          ((Color) @color1).\u002Ector(124, 118, 242);
        }
        else if (Main.waterStyle == 3)
        {
          // ISSUE: explicit reference operation
          ((Color) @color1).\u002Ector(143, 215, 29);
        }
        else if (Main.waterStyle == 4)
        {
          // ISSUE: explicit reference operation
          ((Color) @color1).\u002Ector(78, 193, 227);
        }
        else if (Main.waterStyle == 5)
        {
          // ISSUE: explicit reference operation
          ((Color) @color1).\u002Ector(189, 231, (int) byte.MaxValue);
        }
        else if (Main.waterStyle == 6)
        {
          // ISSUE: explicit reference operation
          ((Color) @color1).\u002Ector(230, 219, 100);
        }
        else if (Main.waterStyle == 7)
        {
          // ISSUE: explicit reference operation
          ((Color) @color1).\u002Ector(151, 107, 75);
        }
        else if (Main.waterStyle == 8)
        {
          // ISSUE: explicit reference operation
          ((Color) @color1).\u002Ector(128, 128, 128);
        }
        else if (Main.waterStyle == 9)
        {
          // ISSUE: explicit reference operation
          ((Color) @color1).\u002Ector(200, 0, 0);
        }
        else if (Main.waterStyle == 10)
        {
          // ISSUE: explicit reference operation
          ((Color) @color1).\u002Ector(208, 80, 80);
        }
        else
        {
          // ISSUE: explicit reference operation
          ((Color) @color1).\u002Ector(28, 216, 94);
        }
        Color color2 = player.hairDyeColor;
        // ISSUE: explicit reference operation
        if ((int) ((Color) @color2).get_A() == 0)
          color2 = color1;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        if ((int) ((Color) @color2).get_R() > (int) ((Color) @color1).get_R())
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Color& local = @color2;
          int num = (int) (byte) ((uint) ((Color) local).get_R() - 1U);
          ((Color) local).set_R((byte) num);
        }
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        if ((int) ((Color) @color2).get_R() < (int) ((Color) @color1).get_R())
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Color& local = @color2;
          int num = (int) (byte) ((uint) ((Color) local).get_R() + 1U);
          ((Color) local).set_R((byte) num);
        }
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        if ((int) ((Color) @color2).get_G() > (int) ((Color) @color1).get_G())
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Color& local = @color2;
          int num = (int) (byte) ((uint) ((Color) local).get_G() - 1U);
          ((Color) local).set_G((byte) num);
        }
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        if ((int) ((Color) @color2).get_G() < (int) ((Color) @color1).get_G())
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Color& local = @color2;
          int num = (int) (byte) ((uint) ((Color) local).get_G() + 1U);
          ((Color) local).set_G((byte) num);
        }
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        if ((int) ((Color) @color2).get_B() > (int) ((Color) @color1).get_B())
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Color& local = @color2;
          int num = (int) (byte) ((uint) ((Color) local).get_B() - 1U);
          ((Color) local).set_B((byte) num);
        }
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        if ((int) ((Color) @color2).get_B() < (int) ((Color) @color1).get_B())
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Color& local = @color2;
          int num = (int) (byte) ((uint) ((Color) local).get_B() + 1U);
          ((Color) local).set_B((byte) num);
        }
        newColor = color2;
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1984, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        // ISSUE: explicit reference operation
        ((Color) @newColor).\u002Ector(244, 22, 175);
        if (!Main.gameMenu && !Main.gamePaused)
        {
          if (Main.rand.Next(45) == 0)
          {
            int Type = Main.rand.Next(139, 143);
            int index = Dust.NewDust(player.position, player.width, 8, Type, 0.0f, 0.0f, 0, (Color) null, 1.2f);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local1 = @Main.dust[index].velocity.X;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num1 = (double) ^(float&) local1 * (1.0 + (double) Main.rand.Next(-50, 51) * 0.00999999977648258);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local1 = (float) num1;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local2 = @Main.dust[index].velocity.Y;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num2 = (double) ^(float&) local2 * (1.0 + (double) Main.rand.Next(-50, 51) * 0.00999999977648258);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local2 = (float) num2;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local3 = @Main.dust[index].velocity.X;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num3 = (double) ^(float&) local3 + (double) Main.rand.Next(-50, 51) * 0.00999999977648258;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local3 = (float) num3;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local4 = @Main.dust[index].velocity.Y;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num4 = (double) ^(float&) local4 + (double) Main.rand.Next(-50, 51) * 0.00999999977648258;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local4 = (float) num4;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local5 = @Main.dust[index].velocity.Y;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num5 = (double) ^(float&) local5 - 1.0;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local5 = (float) num5;
            Main.dust[index].scale *= (float) (0.699999988079071 + (double) Main.rand.Next(-30, 31) * 0.00999999977648258);
            Dust dust = Main.dust[index];
            Vector2 vector2 = Vector2.op_Addition(dust.velocity, Vector2.op_Multiply(player.velocity, 0.2f));
            dust.velocity = vector2;
          }
          if (Main.rand.Next(225) == 0)
          {
            int Type = Main.rand.Next(276, 283);
            int index = Gore.NewGore(new Vector2((float) player.position.X + (float) Main.rand.Next(player.width), (float) player.position.Y + (float) Main.rand.Next(8)), player.velocity, Type, 1f);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local1 = @Main.gore[index].velocity.X;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num1 = (double) ^(float&) local1 * (1.0 + (double) Main.rand.Next(-50, 51) * 0.00999999977648258);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local1 = (float) num1;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local2 = @Main.gore[index].velocity.Y;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num2 = (double) ^(float&) local2 * (1.0 + (double) Main.rand.Next(-50, 51) * 0.00999999977648258);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local2 = (float) num2;
            Main.gore[index].scale *= (float) (1.0 + (double) Main.rand.Next(-20, 21) * 0.00999999977648258);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local3 = @Main.gore[index].velocity.X;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num3 = (double) ^(float&) local3 + (double) Main.rand.Next(-50, 51) * 0.00999999977648258;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local3 = (float) num3;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local4 = @Main.gore[index].velocity.Y;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num4 = (double) ^(float&) local4 + (double) Main.rand.Next(-50, 51) * 0.00999999977648258;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local4 = (float) num4;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            __Null& local5 = @Main.gore[index].velocity.Y;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            double num5 = (double) ^(float&) local5 - 1.0;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) local5 = (float) num5;
            Gore gore = Main.gore[index];
            Vector2 vector2 = Vector2.op_Addition(gore.velocity, Vector2.op_Multiply(player.velocity, 0.2f));
            gore.velocity = vector2;
          }
        }
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1985, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        // ISSUE: explicit reference operation
        ((Color) @newColor).\u002Ector(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1986, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        float num1 = Math.Abs((float) player.velocity.X) + Math.Abs((float) player.velocity.Y);
        float num2 = 10f;
        if ((double) num1 > (double) num2)
          num1 = num2;
        float num3 = num1 / num2;
        float num4 = 1f - num3;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Color) @newColor).set_R((byte) (75.0 * (double) num3 + (double) ((Color) @player.hairColor).get_R() * (double) num4));
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Color) @newColor).set_G((byte) ((double) byte.MaxValue * (double) num3 + (double) ((Color) @player.hairColor).get_G() * (double) num4));
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Color) @newColor).set_B((byte) (200.0 * (double) num3 + (double) ((Color) @player.hairColor).get_B() * (double) num4));
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(2863, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        lighting = false;
        Color color = Lighting.GetColor((int) ((double) player.position.X + (double) player.width * 0.5) / 16, (int) (((double) player.position.Y + (double) player.height * 0.25) / 16.0));
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Color) @newColor).set_R((byte) ((int) ((Color) @color).get_R() + (int) ((Color) @newColor).get_R() >> 1));
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Color) @newColor).set_G((byte) ((int) ((Color) @color).get_G() + (int) ((Color) @newColor).get_G() >> 1));
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Color) @newColor).set_B((byte) ((int) ((Color) @color).get_B() + (int) ((Color) @newColor).get_B() >> 1));
        return newColor;
      })));
    }

    private static void LoadMisc()
    {
      Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
      GameShaders.Misc["ForceField"] = new MiscShaderData(pixelShaderRef, "ForceField");
      GameShaders.Misc["WaterProcessor"] = new MiscShaderData(pixelShaderRef, "WaterProcessor");
      GameShaders.Misc["WaterDistortionObject"] = new MiscShaderData(pixelShaderRef, "WaterDistortionObject");
      GameShaders.Misc["WaterDebugDraw"] = new MiscShaderData(Main.ScreenShaderRef, "WaterDebugDraw");
    }

    public static void Load()
    {
      DyeInitializer.LoadArmorDyes();
      DyeInitializer.LoadHairDyes();
      DyeInitializer.LoadMisc();
    }

    private static void FixRecipes()
    {
      for (int index = 0; index < Recipe.maxRecipes; ++index)
      {
        Main.recipe[index].createItem.dye = (byte) GameShaders.Armor.GetShaderIdFromItemId(Main.recipe[index].createItem.type);
        Main.recipe[index].createItem.hairDye = GameShaders.Hair.GetShaderIdFromItemId(Main.recipe[index].createItem.type);
      }
    }
  }
}
