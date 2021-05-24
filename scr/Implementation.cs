using UnityEngine;
using Harmony;
using MelonLoader;



namespace WarmFood
{
    internal class Implementation : MelonMod
    {

        public override void OnApplicationStart()
        {
            Debug.Log($"[{InfoAttribute.Name}] Version {InfoAttribute.Version} loaded!");
            Settings.OnLoad();

        }

        internal static void Buffs(GearItem gi, float nV)
        {
            if ((bool)gi.m_FoodItem)
            {
                string name = gi.name.ToLower();
                if (name.Contains("mre") && Settings.options.MREheating)
                {
                    // MelonLogger.Log(gi.m_FoodItem.m_CaloriesRemaining.ToString() + "   " + (gi.m_FoodItem.m_CaloriesTotal*(1-nV)).ToString() + "   " + nV);
                    if (Mathf.Abs(gi.m_FoodItem.m_CaloriesRemaining - gi.m_FoodItem.m_CaloriesTotal * (1 - nV)) < 1) //Initial selfheating
                    {

                        if (!gi.m_FreezingBuff)
                        {
                            gi.m_FreezingBuff = gi.gameObject.AddComponent<FreezingBuff>();
                        }
                        gi.m_FreezingBuff.m_InitialPercentDecrease = 10f * Settings.options.MREBuffScale;
                        gi.m_FreezingBuff.m_RateOfIncreaseScale = 0.5f;
                        gi.m_FreezingBuff.m_DurationHours = 2f * Settings.options.MREBuffDuration;
                        gi.m_FoodItem.m_HeatPercent = 100;
                        gi.m_FoodItem.m_PercentHeatLossPerMinuteIndoors = 0.5f;
                        gi.m_FoodItem.m_PercentHeatLossPerMinuteOutdoors = 1f;

                        gi.m_FreezingBuff.Apply(nV);
                    }
                    if (gi.m_FoodItem.IsHot())
                    {
                        gi.m_FreezingBuff.Apply(nV);
                    }
                }
                if (gi.m_FoodItem.m_IsMeat)
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
            if (gi.m_FoodItem)
            {
                // Another ugly code
                //MelonLogger.Log(name + " from " + gi.m_FoodItem.m_CaloriesTotal + " " + gi.m_FoodItem.m_CaloriesRemaining + " " + gi.m_FoodWeight?.m_CaloriesPerKG);
                if (name.Contains("meatbear"))
                {
                    gi.m_FoodItem.m_CaloriesTotal *= Settings.options.calBear;
                    gi.m_FoodItem.m_CaloriesRemaining *= Settings.options.calBear;
                }
                else if (name.Contains("meatdeer"))
                {
                    gi.m_FoodItem.m_CaloriesTotal *= Settings.options.calDeer;
                    gi.m_FoodItem.m_CaloriesRemaining *= Settings.options.calDeer;
                }
                else if (name.Contains("meatrabbit"))
                {
                    gi.m_FoodItem.m_CaloriesTotal *= Settings.options.calRabbit;
                    gi.m_FoodItem.m_CaloriesRemaining *= Settings.options.calRabbit;
                }
                else if (name.Contains("meatwolf"))
                {
                    gi.m_FoodItem.m_CaloriesTotal *= Settings.options.calWolf;
                    gi.m_FoodItem.m_CaloriesRemaining *= Settings.options.calWolf;
                }
                else if (name.Contains("meatmoose"))
                {
                    gi.m_FoodItem.m_CaloriesTotal *= Settings.options.calMoose;
                    gi.m_FoodItem.m_CaloriesRemaining *= Settings.options.calMoose;
                }
                else if (name.Contains("cohosalmon"))
                {
                    gi.m_FoodItem.m_CaloriesTotal *= Settings.options.calSalmon;
                    gi.m_FoodItem.m_CaloriesRemaining *= Settings.options.calSalmon;
                    gi.m_FoodWeight.m_CaloriesPerKG *= Settings.options.calSalmon;
                }
                else if (name.Contains("lakewhitefish"))
                {
                    gi.m_FoodItem.m_CaloriesTotal *= Settings.options.calLakeWhite;
                    gi.m_FoodItem.m_CaloriesRemaining *= Settings.options.calLakeWhite;
                    gi.m_FoodWeight.m_CaloriesPerKG *= Settings.options.calLakeWhite;
                }
                else if (name.Contains("rainbowtrout"))
                {
                    gi.m_FoodItem.m_CaloriesTotal *= Settings.options.calRainbowTrout;
                    gi.m_FoodItem.m_CaloriesRemaining *= Settings.options.calRainbowTrout;
                    gi.m_FoodWeight.m_CaloriesPerKG *= Settings.options.calRainbowTrout;
                }
                else if (name.Contains("smallmouthbass"))
                {
                    gi.m_FoodItem.m_CaloriesTotal *= Settings.options.calSmallmouthBass;
                    gi.m_FoodItem.m_CaloriesRemaining *= Settings.options.calSmallmouthBass;
                    gi.m_FoodWeight.m_CaloriesPerKG *= Settings.options.calSmallmouthBass;
                }
                else if (name.Contains("peanutbutter"))
                {
                    gi.m_FoodItem.m_CaloriesTotal *= Settings.options.calPeanutButter;
                    gi.m_FoodItem.m_CaloriesRemaining *= Settings.options.calPeanutButter;

                }
                //MelonLogger.Log(name + " to " + gi.m_FoodItem.m_CaloriesTotal + " " + gi.m_FoodItem.m_CaloriesRemaining + " " + gi.m_FoodWeight?.m_CaloriesPerKG);

                if (gi.m_FoodItem.m_IsMeat || gi.m_FoodItem.m_IsFish)
                {
                    if (name.Contains("cooked") && Settings.options.MeatHeating)
                    {
                        gi.m_FoodItem.m_HeatedWhenCooked = true;
                        gi.m_FoodItem.m_PercentHeatLossPerMinuteIndoors = 1f;
                        gi.m_FoodItem.m_PercentHeatLossPerMinuteOutdoors = 2f;
                        gi.m_FoodItem.m_HeatPercent = 100;

                        if (!gi.m_FreezingBuff)
                        {
                            gi.m_FreezingBuff = gi.gameObject.AddComponent<FreezingBuff>();
                        }
                        gi.m_FreezingBuff.m_InitialPercentDecrease = 10f * Settings.options.MeatScale;
                        gi.m_FreezingBuff.m_RateOfIncreaseScale = 0.5f;
                        gi.m_FreezingBuff.m_DurationHours = 1f * Settings.options.MeatDuration;
                        // MelonLogger.Log(name + " buff " + gi.m_FreezingBuff.m_InitialPercentDecrease + " " + gi.m_FreezingBuff.m_DurationHours);
                    }
                }
            }

        }
    }
}