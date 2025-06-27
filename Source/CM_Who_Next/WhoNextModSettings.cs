using RimWorld;
using UnityEngine;
using Verse;

namespace CM_Who_Next;

public class WhoNextModSettings : ModSettings
{
    public bool AllowSwitchingBetweenCorpsesAndLiving = true;

    public override void ExposeData()
    {
        base.ExposeData();

        Scribe_Values.Look(ref AllowSwitchingBetweenCorpsesAndLiving, "allowSwitchingBetweenCorpsesAndLiving", true);
    }

    public void DoSettingsWindowContents(Rect inRect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);

        listingStandard.CheckboxLabeled("CM_Who_Next_Setting_AllowSwitchingBetweenCorpsesAndLiving_Label".Translate(),
            ref AllowSwitchingBetweenCorpsesAndLiving,
            "CM_Who_Next_Setting_AllowSwitchingBetweenCorpsesAndLiving_Description".Translate());
        listingStandard.Label("CM_Who_Next_Setting_CurrentKeys_Label".Translate(
            KeyBindingDefOf.PreviousColonist.MainKeyLabel, KeyBindingDefOf.NextColonist.MainKeyLabel));
        if (WhoNextMod.CurrentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("CM_Who_Next_Setting_CurrentModVersion_Label".Translate(WhoNextMod.CurrentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }
}