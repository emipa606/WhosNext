﻿using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace CM_Who_Next;

[StaticConstructorOnStartup]
public static class CM_Who_Next
{
    static CM_Who_Next()
    {
        new Harmony("CM_Who_Next").PatchAll(Assembly.GetExecutingAssembly());
    }

    public static bool AttemptSelectNewPawn(int direction)
    {
        var madeSwitch = false;

        var map = Find.CurrentMap;
        if (map == null)
        {
            return false;
        }

        var selectedThing = Find.Selector.FirstSelectedObject as Thing;
        var selectedCorpse = selectedThing as Corpse;
        var corpseSelected = selectedCorpse != null;
        var selectedPawn = selectedThing as Pawn ?? selectedCorpse?.InnerPawn;

        if (selectedPawn == null)
        {
            return false;
        }

        // Prisoners
        if (selectedPawn.IsPrisonerOfColony)
        {
            madeSwitch = selectNextMatchingPawn(map, selectedThing, corpseSelected, p => p.IsPrisonerOfColony,
                direction);
        }
        // Animals
        else if (selectedPawn.AnimalOrWildMan())
        {
            madeSwitch = selectNextMatchingPawn(map, selectedThing, corpseSelected,
                p => p.Faction == selectedPawn.Faction && p.AnimalOrWildMan(), direction);
        }
        // Peoples
        else if (selectedPawn.Faction != Faction.OfPlayer)
        {
            madeSwitch = selectNextMatchingPawn(map, selectedThing, corpseSelected,
                p => p.Faction == selectedPawn.Faction && !p.IsPrisonerOfColony && !p.AnimalOrWildMan(),
                direction);
        }

        return madeSwitch;
    }

    private static bool selectNextMatchingPawn(Map map, Thing selectedThing, bool corpseSelected,
        Func<Pawn, bool> filter, int direction = 1)
    {
        //Log.Message("Attempting to select next pawn");

        var pawnsAndCorpsePawns = map.listerThings.AllThings.Where(pawnAndCorpseFilter).ToList();

        var selectedIndex = pawnsAndCorpsePawns.FindIndex(thing => thing == selectedThing);
        if (selectedIndex < 0)
        {
            return false;
        }

        selectedIndex += direction;
        if (selectedIndex >= pawnsAndCorpsePawns.Count)
        {
            selectedIndex = 0;
        }

        if (selectedIndex < 0)
        {
            selectedIndex = pawnsAndCorpsePawns.Count - 1;
        }

        CameraJumper.TryJumpAndSelect(pawnsAndCorpsePawns[selectedIndex]);

        return true;

        bool pawnAndCorpseFilter(Thing thing)
        {
            switch (thing)
            {
                case Pawn pawn when WhoNextMod.Instance.Settings.AllowSwitchingBetweenCorpsesAndLiving ||
                                    !corpseSelected:
                    return filter(pawn);
                case Corpse { InnerPawn: not null } thingCorpse
                    when WhoNextMod.Instance.Settings.AllowSwitchingBetweenCorpsesAndLiving || corpseSelected:
                    return filter(thingCorpse.InnerPawn);
                default:
                    return false;
            }
        }
    }
}