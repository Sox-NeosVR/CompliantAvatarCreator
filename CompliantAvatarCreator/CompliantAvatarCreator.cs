using HarmonyLib;
using NeosModLoader;
using FrooxEngine;

namespace CompliantAvatarCreator
{
    public class CompliantAvatarCreator : NeosMod
    {
        public override string Name => "CompliantAvatarCreator";
        public override string Author => "Sox";
        public override string Version => "1.0.0";
        public override string Link => "";

        public object Config { get; private set; }

        public override void OnEngineInit()
        {

            CompliantAvatarCreator.config = base.GetConfiguration();
            Harmony harmony = new Harmony("net.Sox.CompliantAvatarCreator");
            harmony.PatchAll();
        }

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<bool> JustDontDrive = new ModConfigurationKey<bool>("CompliantAvatarCreator", "Toggle 'DontDrive' on the Avatar Creator by default.", () => true, false, null);

        private static ModConfiguration config;
        [HarmonyPatch(typeof(AvatarCreator))]
        class PatchAvatarCreator
        {
            [HarmonyPostfix]
            [HarmonyPatch("AddAnchor")]
            public static void PostFix(Slider slider)
            {
                slider.DontDrive.Value = config.GetValue(JustDontDrive);
            }
        }   
    }
}