using HarmonyLib;
using RimWorld;

namespace CM_Who_Next;

[HarmonyPatch(typeof(ThingSelectionUtility))]
[HarmonyPatch("SelectNextColonist", MethodType.Normal)]
public static class ThingSelectionUtility_SelectNextColonist
{
    [HarmonyPrefix]
    public static bool Prefix()
    {
        //Log.Message("ThingSelectionUtility.SelectNextColonist prefix");

        return !CM_Who_Next.AttemptSelectNewPawn(1);
    }
}