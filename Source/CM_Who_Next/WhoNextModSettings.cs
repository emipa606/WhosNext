using RimWorld;
using UnityEngine;
using Verse;

namespace CM_Who_Next;

public class WhoNextModSettings : ModSettings
{
    public bool allowSwitchingBetweenCorpsesAndLiving = true;

    public override void ExposeData()
    {
        base.ExposeData();

        Scribe_Values.Look(ref allowSwitchingBetweenCorpsesAndLiving, "allowSwitchingBetweenCorpsesAndLiving", true);
    }

    public void DoSettingsWindowContents(Rect inRect)
    {
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(inRect);

        listing_Standard.CheckboxLabeled("CM_Who_Next_Setting_AllowSwitchingBetweenCorpsesAndLiving_Label".Translate(),
            ref allowSwitchingBetweenCorpsesAndLiving,
            "CM_Who_Next_Setting_AllowSwitchingBetweenCorpsesAndLiving_Description".Translate());
        listing_Standard.Label("CM_Who_Next_Setting_CurrentKeys_Label".Translate(
            KeyBindingDefOf.PreviousColonist.MainKeyLabel, KeyBindingDefOf.NextColonist.MainKeyLabel));
        if (WhoNextMod.currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("CM_Who_Next_Setting_CurrentModVersion_Label".Translate(WhoNextMod.currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
    }

    public void UpdateSettings()
    {
    }
}