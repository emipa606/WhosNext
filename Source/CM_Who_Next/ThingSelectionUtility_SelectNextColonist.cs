using HarmonyLib;
using RimWorld;

namespace CM_Who_Next;

[HarmonyPatch(typeof(ThingSelectionUtility), nameof(ThingSelectionUtility.SelectNextColonist), MethodType.Normal)]
public static class ThingSelectionUtility_SelectNextColonist
{
    public static bool Prefix()
    {
        return !CM_Who_Next.AttemptSelectNewPawn(1);
    }
}