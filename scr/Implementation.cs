using UnityEngine;
using Harmony;


namespace WarmFood
{
    public class Implementation
    {
        private const string NAME = "Warm-Food";
        //private static bool RabbitSnared = false;
        //private static float TtnUAH;
        
        // private static bool AcusticHalluzinationsActive = false;


        public static void OnLoad()
        {
            Log("Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
            
        }





        public static void Buffs(GearItem gi, float nV)
        {
            if ((bool)gi.m_FoodItem)
            {

                var setting = WarmFoodSettings.Instance;
                string name = gi.name.ToLower();
                if (name.Contains("mre")&&setting.MeatHeating)
                {

                   // AccessTools.Field(typeof(FoodItem), "m_PreventHeatLoss").SetValue(gi, true);

                    //Log(gi.m_FoodItem.m_CaloriesRemaining.ToString() + "   " + (gi.m_FoodItem.m_CaloriesTotal*(1-nV)).ToString() + "   " + nV);
                    if (Mathf.Abs(gi.m_FoodItem.m_CaloriesRemaining - gi.m_FoodItem.m_CaloriesTotal * (1 - nV))<1) //Initial selfheating
                    {
                        if (gi.m_FreezingBuff == null) gi.m_FreezingBuff = new FreezingBuff();
                        gi.m_FreezingBuff.m_InitialPercentDecrease = 10f* setting.MREBuffScale;
                        gi.m_FreezingBuff.m_RateOfIncreaseScale = 0.5f;
                        gi.m_FreezingBuff.m_DurationHours = 2 * setting.MREBuffDuration;
                        gi.m_FoodItem.m_HeatPercent = 100;
                        gi.m_FoodItem.m_PercentHeatLossPerMinuteIndoors = 0.5f;
                        gi.m_FoodItem.m_PercentHeatLossPerMinuteOutdoors = 1f;

                        Log(gi.m_FreezingBuff.m_InitialPercentDecrease.ToString() + "   " + gi.m_FreezingBuff.m_RateOfIncreaseScale.ToString() + "   " + gi.m_FreezingBuff.m_DurationHours.ToString());
                        gi.m_FreezingBuff.Apply(nV);
                    }
                    if(gi.m_FoodItem.IsHot())
                    {
                        gi.m_FreezingBuff.Apply(nV);
                    }
                }
                if(gi.m_FoodItem.m_IsMeat)
                {
                    if (gi.m_FoodItem.IsHot())
                    {
                        gi.m_FreezingBuff.Apply(nV);
                    }
                }
            }
        }
        public static void MayApplychanges(GearItem gi)
        {
            string name = gi.name.ToLower();
            var setting = WarmFoodSettings.Instance;

            if ((bool)gi.m_FoodItem && gi.m_FoodItem.m_IsMeat)
            {
                gi.m_FoodItem.m_CaloriesTotal *= setting.Meatkcal;
                gi.m_FoodItem.m_CaloriesRemaining *= setting.Meatkcal;

                if (!gi.m_FoodItem.m_IsRawMeat&&setting.MeatHeating)
                {
                    //if (gi.m_Cookable == null) gi.m_Cookable = new Cookable();
                    //SetCookable(gi,true);

                    gi.m_FoodItem.m_HeatedWhenCooked = true;
                    gi.m_FoodItem.m_PercentHeatLossPerMinuteIndoors = 1f;
                    gi.m_FoodItem.m_PercentHeatLossPerMinuteOutdoors = 2f;
                    gi.m_FoodItem.m_HeatPercent = 100;

                    if (gi.m_FreezingBuff == null) gi.m_FreezingBuff = new FreezingBuff();
                    gi.m_FreezingBuff.m_InitialPercentDecrease = 10f* setting.MeatScale;
                    gi.m_FreezingBuff.m_RateOfIncreaseScale = 0.5f;
                    gi.m_FreezingBuff.m_DurationHours = 1f * setting.MeatDuration;
                }

            }
            if ((bool)gi.m_FoodItem && gi.m_FoodItem.m_IsFish)
            {
                gi.m_FoodItem.m_CaloriesTotal *= setting.Fishkcal;
                gi.m_FoodItem.m_CaloriesRemaining *= setting.Fishkcal;

                if (name.Contains("raw") && setting.MeatHeating)
                {
                    gi.m_FoodItem.m_HeatedWhenCooked = true;
                    gi.m_FoodItem.m_PercentHeatLossPerMinuteIndoors = 1f;
                    gi.m_FoodItem.m_PercentHeatLossPerMinuteOutdoors = 2f;
                    gi.m_FoodItem.m_HeatPercent = 100;

                    if (gi.m_FreezingBuff == null) gi.m_FreezingBuff = new FreezingBuff();
                    gi.m_FreezingBuff.m_InitialPercentDecrease = 10f * setting.MeatScale;
                    gi.m_FreezingBuff.m_RateOfIncreaseScale = 0.5f;
                    gi.m_FreezingBuff.m_DurationHours = 1f* setting.MeatDuration;
                }
            }


        }

        public static void SetCookable(GearItem gi,bool meat)
        {

            gi.m_Cookable.m_CookedPrefab = gi;
            gi.m_Cookable.m_CookableType = Cookable.CookableType.Meat;
            gi.m_Cookable.m_CookTimeMinutes = 15;
            gi.m_Cookable.m_ReadyTimeMinutes = 10;
            gi.m_Cookable.m_PutInPotAudio = "PLAY_ADDMEATPAN";
            gi.m_Cookable.m_CookAudio = "Play_FryingHeavy";


        }


            internal static void Log(string message)
        {
            Debug.LogFormat("[" + NAME + "] {0}", message);
        }

        internal static void Log(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format("[" + NAME + "] {0}", message);
            Debug.LogFormat(preformattedMessage, parameters);
        }
    }
   
}