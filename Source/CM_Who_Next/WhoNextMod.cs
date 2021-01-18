using HarmonyLib;
using RimWorld;
using Verse;

namespace CM_Who_Next
{
    public class WhoNextMod : Mod
    {
        private static WhoNextMod _instance;
        public static WhoNextMod Instance => _instance;

        public WhoNextMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("CM_Who_Next");
            harmony.PatchAll();

            _instance = this;
        }
    }
}
