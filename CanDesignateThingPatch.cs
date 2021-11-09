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
    [HarmonyPatch(typeof(Designator_Slaughter), "CanDesignateThing")]
    class CanDesignateThingPatch
    {
        static void Postfix(ref AcceptanceReport __result, Thing t)
        {
            if (__result.Accepted && Current.Game.GetComponent<NotThisOneGameComponent>().list.Contains(t))
            {
                __result = false;
            }
        }
    }
}
