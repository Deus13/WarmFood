
using ModSettings;
using System;
using System.IO;
using System.Reflection;


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

        [Name("Meat kcal")]
        [Description("Multimplies the the meat kcal with this nummber.")]
        [Slider(0, 5f, 51)]
        public float Meatkcal = 1f;

        [Name("Fish kcal")]
        [Description("Multimplies the the meat kcal with this nummber.")]
        [Slider(0, 5f, 51)]
        public float Fishkcal = 1f;


        protected override void OnChange(FieldInfo field, object oldVal, object newVal)
        {
            RefreshFields();
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
