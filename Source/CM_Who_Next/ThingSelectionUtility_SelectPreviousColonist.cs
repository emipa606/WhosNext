using HarmonyLib;
using RimWorld;

namespace CM_Who_Next;

[HarmonyPatch(typeof(ThingSelectionUtility))]
[HarmonyPatch("SelectPreviousColonist", MethodType.Normal)]
public static class ThingSelectionUtility_SelectPreviousColonist
{
    [HarmonyPrefix]
    public static bool Prefix()
    {
        //Log.Message("ThingSelectionUtility.SelectPreviousColonist prefix");

        return !CM_Who_Next.AttemptSelectNewPawn(-1);
    }
}