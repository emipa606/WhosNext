using Mlie;
using UnityEngine;
using Verse;

namespace CM_Who_Next;

public class WhoNextMod : Mod
{
    public static string CurrentVersion;

    public static WhoNextMod Instance;
    public readonly WhoNextModSettings Settings;

    public WhoNextMod(ModContentPack content) : base(content)
    {
        Instance = this;
        Settings = GetSettings<WhoNextModSettings>();
        CurrentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    public override string SettingsCategory()
    {
        return "Who's Next?";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        Settings.DoSettingsWindowContents(inRect);
    }
}