using System;
using System.Collections.Generic;
using System.Linq;

using HarmonyLib;
using RimWorld;
using Verse;

namespace CM_Who_Next
{
    [StaticConstructorOnStartup]
    public static class ThingSelectionUtility_Patches
    {
        private static bool AttemptSelectNewPawn(int direction)
        {
            bool madeSwitch = false;

            Map map = Find.CurrentMap;
            if (map != null)
            {
                Pawn selectedPawn = Find.Selector.FirstSelectedObject as Pawn;

                if (selectedPawn != null)
                {
                    // Prisoners
                    if (selectedPawn.IsPrisonerOfColony)
                        madeSwitch = SelectNextMatchingPawn(map, selectedPawn, p => p.IsPrisonerOfColony, direction);
                    // Animals
                    else if (selectedPawn.AnimalOrWildMan())
                        madeSwitch = SelectNextMatchingPawn(map, selectedPawn, p => p.Faction == selectedPawn.Faction && p.AnimalOrWildMan(), direction);
                    // Peoples
                    else if (selectedPawn.Faction != Faction.OfPlayer)
                        madeSwitch = SelectNextMatchingPawn(map, selectedPawn, p => p.Faction == selectedPawn.Faction && !p.IsPrisonerOfColony && !p.AnimalOrWildMan(), direction);
                }
            }

            return madeSwitch;
        }

        private static bool SelectNextMatchingPawn(Map map, Pawn selectedPawn, Func<Pawn, bool> filter, int direction = 1)
        {
            Log.Message("Attempting to select next pawn");

            List<Pawn> filteredPawns = map.mapPawns.AllPawns.Where(filter).ToList();
            int selectedIndex = filteredPawns.FindIndex(p => p == selectedPawn);
            if (selectedIndex >= 0)
            {
                selectedIndex += direction;
                if (selectedIndex >= filteredPawns.Count)
                    selectedIndex = 0;
                if (selectedIndex < 0)
                    selectedIndex = filteredPawns.Count - 1;

                CameraJumper.TryJumpAndSelect(filteredPawns[selectedIndex]);

                return true;
            }

            return false;
        }

        [HarmonyPatch(typeof(ThingSelectionUtility))]
        [HarmonyPatch("SelectNextColonist", MethodType.Normal)]
        public static class ThingSelectionUtility_SelectNextColonist
        {
            [HarmonyPrefix]
            public static bool Prefix()
            {
                Log.Message("ThingSelectionUtility.SelectNextColonist prefix");

                bool callOriginal = !AttemptSelectNewPawn(1);
                return callOriginal;
            }
        }

        [HarmonyPatch(typeof(ThingSelectionUtility))]
        [HarmonyPatch("SelectPreviousColonist", MethodType.Normal)]
        public static class ThingSelectionUtility_SelectPreviousColonist
        {
            [HarmonyPrefix]
            public static bool Prefix()
            {
                Log.Message("ThingSelectionUtility.SelectPreviousColonist prefix");

                bool callOriginal = !AttemptSelectNewPawn(-1);
                return callOriginal;
            }
        }
    }
}
