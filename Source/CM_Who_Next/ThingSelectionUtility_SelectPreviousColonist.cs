using HarmonyLib;
using RimWorld;

namespace CM_Who_Next;

[HarmonyPatch(typeof(ThingSelectionUtility), nameof(ThingSelectionUtility.SelectPreviousColonist), MethodType.Normal)]
public static class ThingSelectionUtility_SelectPreviousColonist
{
    public static bool Prefix()
    {
        return !CM_Who_Next.AttemptSelectNewPawn(-1);
    }
}