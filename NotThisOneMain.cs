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
    public class NotThisOneGameComponent : GameComponent
    {
        public HashSet<Pawn> list = new HashSet<Pawn>();

        public NotThisOneGameComponent(Game game)
        {

        }
        public override void ExposeData()
        {
            Scribe_Collections.Look(ref list, "AnimalsNotToSlaughter", LookMode.Reference);
            base.ExposeData();
        }

    }

    [StaticConstructorOnStartup]
    public static class StartUp
    {
        static StartUp()
        {
            var harmony = new Harmony("NotThisOne.patch");
            harmony.PatchAll();
        }
    }
}
