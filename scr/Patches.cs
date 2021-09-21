using Harmony;
using System;
using UnityEngine;

namespace WarmFood
{


    [HarmonyPatch(typeof(GearItem), "ApplyBuffs",new Type[] { typeof(float) })]
    internal static class GearItem_ApplyBuffs
    {
        private static void Prefix(GearItem __instance, float normalizedValue)        //using Prefix to let the vanilla funtions still reduce willpower puration. altough on an starnge way were the realtime played is used for the calcs.
        {
            Implementation.Buffs(__instance, normalizedValue);

        }
    }
    [HarmonyPatch(typeof(GearItem), "Awake", null)]
    public class GearItem_Awake
    {
        private static void Postfix(GearItem __instance)
        {

            Implementation.MayApplychanges(__instance);

        }
    }
}