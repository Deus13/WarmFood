
using System;
using System.IO;
using System.Reflection;
using JsonModSettings;
using ModSettings;

namespace WarmFood
{
    internal class WarmFoodSettings : JsonModSettingsBase<WarmFoodSettings>
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





        protected override void OnChange(FieldInfo field, object oldValue, object newValue)
        {
            base.OnChange(field, oldValue, newValue);

            if (field.Name == nameof(MREheating))
            {
                hideMREheating();
            }
            if (field.Name == nameof(MeatHeating))
            {
                hideMeatHeating();
            }
            
            RefreshGUI();
        }

        private void hideMREheating()
        {
            SetFieldVisible(nameof(MREBuffScale), MREheating);
            SetFieldVisible(nameof(MREBuffDuration), MREheating);
            
        }

        private void hideMeatHeating()
        {
            SetFieldVisible(nameof(MeatScale), MeatHeating);
            SetFieldVisible(nameof(MeatDuration), MeatHeating);
           
        }





        public static void OnLoad()
        {
            Instance = JsonModSettingsLoader.Load<WarmFoodSettings>();
        }
    }
}
