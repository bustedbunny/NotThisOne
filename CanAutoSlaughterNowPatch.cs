using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;

namespace NotThisOne
{
    [HarmonyPatch(typeof(AutoSlaughterManager), "CanAutoSlaughterNow")]
    class CanAutoSlaughterNowPatch
    {
        static bool Postfix(bool __result, Pawn animal)
        {

            if (!__result)
            {
                return false;
            }
            if (Current.Game.GetComponent<NotThisOneGameComponent>().list.Contains(animal))
            {
                Log.Message("Returned false");
                return false;
            }
            return true;
        }
    }
}
