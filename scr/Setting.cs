
using MelonLoader;
using ModSettings;
using System;
using System.IO;
using System.Reflection;
/*
 * cal notes by ttr
 * name -> real life per 1kg (averaged) => ratio based on game cal/kg
 * below are averaged values

GEAR_RawMeatBear -> 1600 (game) => 1.78
GEAR_RawMeatDeer -> 1160 (venison) => 1.28
GEAR_RawMeatRabbit -> 1730 (venison) => 3.4 
GEAR_RawMeatWolf -> 1430 => 1.79
GEAR_RawMeatMoose -> 1000 => 1.12
GEAR_RawCohoSalmon -> 1780 => 5.91
GEAR_RawLakeWhiteFish -> 1200 => 3.11
GEAR_RawRainbowTrout -> 1410 => 3.66
GEAR_RawSmallMouthBass -> 1280 => 4.2

GEAR_CookedMeatBear
GEAR_CookedMeatDeer
GEAR_CookedMeatRabbit
GEAR_CookedMeatWolf
GEAR_CookedMeatMoose
GEAR_CookedCohoSalmon
GEAR_CookedLakeWhiteFish
GEAR_CookedRainbowTrout
GEAR_CookedSmallMouthBass

And bonus of this - in game is 0.5kg and 900cal
GEAR_PeanutButter -> 6000 => 3.34
so it should be 3000 for 0.5kg.
 */

namespace WarmFood
{
    internal class WarmFoodSettings : JsonModSettings
    {
        [Section("MRE:")]
        [Name("MRE heating bags")]
        [Description("If turned on, MRE's will heat it self on first openning.")]
        public bool MREheating = true;

        [Name("MRE Buff Scale")]
        [Description("How strong the MRE Heating Buff is.")]
        [Slider(0, 5f, 51)]
        public float MREBuffScale = 1f;


        [Name("MRE Buff Duration")]
        [Description("Scale for the Duratinn of MRE Heating Buff.")]
        [Slider(0, 5f, 51)]
        public float MREBuffDuration = 1f;


        [Section("Meat and Fish:")]
        [Name("Warm Buff")]
        [Description("If turned on, Meat and Fish will get the warmth buff after cooking it.")]
        public bool MeatHeating = true;

        [Name("Buff Scale")]
        [Description("How strong the Meat and Fish Heating Buff is.")]
        [Slider(0, 5f, 51)]
        public float MeatScale = 1f;


        [Name("Buff Duration")]
        [Description("Scale for the Duratinn of the Meat and Fish Heating Buff.")]
        [Slider(0, 5f, 51)]
        public float MeatDuration = 1f;

        [Section("Calories:")]

        [Name("Preset")]
        [Description("Select preset")]
        [Choice("Vanila", "Real Life (math)", "Real life (adjusted)", "Custom")]
        public int preset = 0;

        [Name("Bear")]
        [Description("Calories ratio for Bear")]
        [Slider(0, 6f, 51)]
        public float calBear = 1f;

        [Name("Deer")]
        [Description("Calories ratio for Deer")]
        [Slider(0, 6f, 51)]
        public float calDeer = 1f;

        [Name("Rabbit")]
        [Description("Calories ratio for Rabbit")]
        [Slider(0, 6f, 51)]
        public float calRabbit = 1f;

        [Name("Moose")]
        [Description("Calories ratio for Moose")]
        [Slider(0, 6f, 51)]
        public float calMoose = 1f;

        [Name("Wolf")]
        [Description("Calories ratio for Wolf")]
        [Slider(0, 6f, 51)]
        public float calWolf = 1f;

        [Name("Salmon")]
        [Description("Calories ratio for Coho Salmon")]
        [Slider(0, 6f, 51)]
        public float calSalmon = 1f;

        [Name("Lake White")]
        [Description("Calories ratio for Lake White Fish")]
        [Slider(0, 6f, 51)]
        public float calLakeWhite = 1f;

        [Name("Rainbow Trout")]
        [Description("Calories ratio for Rainbow Trout")]
        [Slider(0, 6f, 51)]
        public float calRainbowTrout = 1f;

        [Name("Smallmouth Bass")]
        [Description("Calories ratio for Smallmouth Bass")]
        [Slider(0, 6f, 51)]
        public float calSmallmouthBass = 1f;

        [Name("Bonus: Peanut Butter")]
        [Description("Calories ratio for Peanut Butter")]
        [Slider(0, 6f, 51)]
        public float calPeanutButter = 1f;

        protected override void OnChange(FieldInfo field, object oldValue, object newValue)
        {
            if (field.Name == nameof(preset) && preset != 3)
            {
                UsePreset((int)newValue);
            }
            else if (
                (field.Name == nameof(calBear)) ||
                (field.Name == nameof(calDeer)) ||
                (field.Name == nameof(calLakeWhite)) ||
                (field.Name == nameof(calMoose)) || 
                (field.Name == nameof(calPeanutButter)) ||
                (field.Name == nameof(calRabbit)) ||
                (field.Name == nameof(calRainbowTrout)) ||
                (field.Name == nameof(calSalmon)) ||
                (field.Name == nameof(calSmallmouthBass)) ||
                (field.Name == nameof(calWolf))
                )

            {
                 // MelonLogger.Log(field.Name + " " + oldValue.ToString() + " " + newValue.ToString());
                 // disabled as it was always showing "Custom" - will fix it latter
                 // preset = 3; // Custom
            }
            //RefreshFields();
            // Call this method to make the newly set field values show up in the GUI!
            RefreshGUI();
        }
        private void UsePreset(int preset)
        {
            // Ugly code ahead!
            switch (preset)
            {
                case 0:
                    calBear = 1f;
                    calDeer = 1f;
                    calLakeWhite = 1f;
                    calMoose = 1f;
                    calRabbit = 1f;
                    calRainbowTrout = 1f;
                    calSalmon = 1f;
                    calSmallmouthBass = 1f;
                    calWolf = 1f;
                    calPeanutButter = 1f;
                    break;
                case 1:
                    calBear = 1.78f;
                    calDeer = 1.28f;
                    calLakeWhite = 3.11f;
                    calMoose = 1.12f;
                    calRabbit = 3.4f;
                    calRainbowTrout = 3.66f;
                    calSalmon = 5.91f;
                    calSmallmouthBass = 4.2f;
                    calWolf = 1.79f;
                    calPeanutButter = 3.34f;
                    break;
                case 2:
                    calBear = 1.6f; // reduced
                    calDeer = 1.20f; // reduced
                    calLakeWhite = 2.17f; // 70% 
                    calMoose = 1.12f; // 100%
                    calRabbit = 1.7f; // 50%
                    calRainbowTrout = 2.56f; // 70%
                    calSalmon = 3f; // 50-51%
                    calSmallmouthBass = 2.94f; // 70%
                    calWolf = 1.70f; // reduced
                    calPeanutButter = 3.34f; // 100%
                    break;
            }

        }
    
    internal void RefreshFields()
        {
            if (MREheating == true)
            {
                SetFieldVisible(nameof(MREBuffScale), true);
                SetFieldVisible(nameof(MREBuffDuration), true);
            }
            else
            {
                SetFieldVisible(nameof(MREBuffScale), false);
                SetFieldVisible(nameof(MREBuffDuration), false);
            }
            if (MeatHeating == true)
            {
                SetFieldVisible(nameof(MeatScale), true);
                SetFieldVisible(nameof(MeatDuration), true);
            }
            else
            {
                SetFieldVisible(nameof(MeatScale), false);
                SetFieldVisible(nameof(MeatDuration), false);
            }

        }
    }
    internal static class Settings
    {
        public static WarmFoodSettings options;
        public static void OnLoad()
        {
            options = new WarmFoodSettings();
            options.RefreshFields();
            options.AddToModSettings("WarmFood Settings");
        }
    }
}
