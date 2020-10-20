using UnityEngine;
using Harmony;
using MelonLoader;


namespace WarmFood
{
    internal class Implementation : MelonMod
    {
        private const string NAME = "Warm-Food";
        //private static bool RabbitSnared = false;
        //private static float TtnUAH;

        // private static bool AcusticHalluzinationsActive = false;


        public override void OnApplicationStart() {
            Debug.Log($"[{InfoAttribute.Name}] Version {InfoAttribute.Version} loaded!");
            Settings.OnLoad();

        }

        internal static void Buffs(GearItem gi, float nV)
        {
            if ((bool)gi.m_FoodItem)
            {

                string name = gi.name.ToLower();
                if (name.Contains("mre")&& Settings.options.MeatHeating)
                {

                   // AccessTools.Field(typeof(FoodItem), "m_PreventHeatLoss").SetValue(gi, true);

                    //Log(gi.m_FoodItem.m_CaloriesRemaining.ToString() + "   " + (gi.m_FoodItem.m_CaloriesTotal*(1-nV)).ToString() + "   " + nV);
                    if (Mathf.Abs(gi.m_FoodItem.m_CaloriesRemaining - gi.m_FoodItem.m_CaloriesTotal * (1 - nV))<1) //Initial selfheating
                    {
                        if (gi.m_FreezingBuff == null) gi.m_FreezingBuff = new FreezingBuff();
                        gi.m_FreezingBuff.m_InitialPercentDecrease = 10f* Settings.options.MREBuffScale;
                        gi.m_FreezingBuff.m_RateOfIncreaseScale = 0.5f;
                        gi.m_FreezingBuff.m_DurationHours = 2 * Settings.options.MREBuffDuration;
                        gi.m_FoodItem.m_HeatPercent = 100;
                        gi.m_FoodItem.m_PercentHeatLossPerMinuteIndoors = 0.5f;
                        gi.m_FoodItem.m_PercentHeatLossPerMinuteOutdoors = 1f;

                        Debug.Log(gi.m_FreezingBuff.m_InitialPercentDecrease.ToString() + "   " + gi.m_FreezingBuff.m_RateOfIncreaseScale.ToString() + "   " + gi.m_FreezingBuff.m_DurationHours.ToString());
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
        internal static void MayApplychanges(GearItem gi)
        {
            string name = gi.name.ToLower();

            if ((bool)gi.m_FoodItem && gi.m_FoodItem.m_IsMeat)
            {
                gi.m_FoodItem.m_CaloriesTotal *= Settings.options.Meatkcal;
                gi.m_FoodItem.m_CaloriesRemaining *= Settings.options.Meatkcal;

                if (!gi.m_FoodItem.m_IsRawMeat&& Settings.options.MeatHeating)
                {
                    //if (gi.m_Cookable == null) gi.m_Cookable = new Cookable();
                    //SetCookable(gi,true);

                    gi.m_FoodItem.m_HeatedWhenCooked = true;
                    gi.m_FoodItem.m_PercentHeatLossPerMinuteIndoors = 1f;
                    gi.m_FoodItem.m_PercentHeatLossPerMinuteOutdoors = 2f;
                    gi.m_FoodItem.m_HeatPercent = 100;

                    if (gi.m_FreezingBuff == null) gi.m_FreezingBuff = new FreezingBuff();
                    gi.m_FreezingBuff.m_InitialPercentDecrease = 10f* Settings.options.MeatScale;
                    gi.m_FreezingBuff.m_RateOfIncreaseScale = 0.5f;
                    gi.m_FreezingBuff.m_DurationHours = 1f * Settings.options.MeatDuration;
                }

            }
            if ((bool)gi.m_FoodItem && gi.m_FoodItem.m_IsFish)
            {
                gi.m_FoodItem.m_CaloriesTotal *= Settings.options.Fishkcal;
                gi.m_FoodItem.m_CaloriesRemaining *= Settings.options.Fishkcal;

                if (name.Contains("cooked") && Settings.options.MeatHeating)
                {
                    gi.m_FoodItem.m_HeatedWhenCooked = true;
                    gi.m_FoodItem.m_PercentHeatLossPerMinuteIndoors = 1f;
                    gi.m_FoodItem.m_PercentHeatLossPerMinuteOutdoors = 2f;
                    gi.m_FoodItem.m_HeatPercent = 100;

                    if (gi.m_FreezingBuff == null) gi.m_FreezingBuff = new FreezingBuff();
                    gi.m_FreezingBuff.m_InitialPercentDecrease = 10f * Settings.options.MeatScale;
                    gi.m_FreezingBuff.m_RateOfIncreaseScale = 0.5f;
                    gi.m_FreezingBuff.m_DurationHours = 1f* Settings.options.MeatDuration;
                }
            }


        }

        internal static void SetCookable(GearItem gi,bool meat)
        {

            gi.m_Cookable.m_CookedPrefab = gi;
            gi.m_Cookable.m_CookableType = Cookable.CookableType.Meat;
            gi.m_Cookable.m_CookTimeMinutes = 15;
            gi.m_Cookable.m_ReadyTimeMinutes = 10;
            gi.m_Cookable.m_PutInPotAudio = "PLAY_ADDMEATPAN";
            gi.m_Cookable.m_CookAudio = "Play_FryingHeavy";


        }

    }
   
}