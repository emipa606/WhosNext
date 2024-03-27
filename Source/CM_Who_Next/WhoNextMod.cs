using Mlie;
using UnityEngine;
using Verse;

namespace CM_Who_Next;

public class WhoNextMod : Mod
{
    public static string currentVersion;

    public static WhoNextMod Instance;
    public readonly WhoNextModSettings settings;

    public WhoNextMod(ModContentPack content) : base(content)
    {
        Instance = this;
        settings = GetSettings<WhoNextModSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    public override string SettingsCategory()
    {
        return "Who's Next?";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        settings.DoSettingsWindowContents(inRect);
    }

    public override void WriteSettings()
    {
        base.WriteSettings();
        settings.UpdateSettings();
    }
}