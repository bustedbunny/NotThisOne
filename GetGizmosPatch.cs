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
    [HarmonyPatch(typeof(Pawn), "GetGizmos")]
    public class GetGizmosPatch
    {
        static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> __result, Pawn __instance)
        {
            foreach (Gizmo result in __result)
            {
                yield return result;
            }
            if (__instance.RaceProps.Animal && __instance.Faction == Faction.OfPlayer)
            {
                yield return new Command_Toggle
                {
                    defaultLabel = "NotThisOneLabel".Translate(),
                    defaultDesc = "NotThisOneDesc".Translate(),
                    icon = TexCommand.ForbidOn,
                    isActive = () => Current.Game.GetComponent<NotThisOneGameComponent>().list.Contains(__instance),
                    toggleAction = delegate
                    {
                        if (__instance.Map == null)
                        {
                            return;
                        }
                        if (Current.Game.GetComponent<NotThisOneGameComponent>().list.Contains(__instance))
                        {
                            Current.Game.GetComponent<NotThisOneGameComponent>().list.Remove(__instance);
                            __instance.Map.autoSlaughterManager.Notify_PawnChangedFaction();
                        }
                        else
                        {
                            Current.Game.GetComponent<NotThisOneGameComponent>().list.Add(__instance);
                            __instance.Map.designationManager.TryRemoveDesignationOn(__instance, DesignationDefOf.Slaughter);
                            __instance.Map.autoSlaughterManager.Notify_PawnChangedFaction();
                        }
                    }
                };
            }

        }
    }
}
