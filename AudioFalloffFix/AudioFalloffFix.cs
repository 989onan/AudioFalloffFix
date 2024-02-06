using HarmonyLib;
using ResoniteModLoader;
using UnityFrooxEngineRunner;
using UnityEngine;
using UnityEngine.Audio;
using Elements.Core;

namespace AudioFalloffFix
{
    public class AudioFalloffFix : ResoniteMod
    {
        public static ModConfiguration Config;

        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint0 = new ModConfigurationKey<float2>("Curve point 10", "how much to make voices fall off on log at point 10", () => new float2(0f, 1));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint1 = new ModConfigurationKey<float2>("Curve point 1", "how much to make voices fall off on log at point 1", () => new float2(0.1f,0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint2 = new ModConfigurationKey<float2>("Curve point 2", "how much to make voices fall off on log at point 2", () => new float2(.2f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint3 = new ModConfigurationKey<float2>("Curve point 3", "how much to make voices fall off on log at point 3", () => new float2(.3f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint4 = new ModConfigurationKey<float2>("Curve point 4", "how much to make voices fall off on log at point 4", () => new float2(.4f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint5 = new ModConfigurationKey<float2>("Curve point 5", "how much to make voices fall off on log at point 5", () => new float2(.5f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint6 = new ModConfigurationKey<float2>("Curve point 6", "how much to make voices fall off on log at point 6", () => new float2(.6f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint7 = new ModConfigurationKey<float2>("Curve point 7", "how much to make voices fall off on log at point 7", () => new float2(.7f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint8 = new ModConfigurationKey<float2>("Curve point 8", "how much to make voices fall off on log at point 8", () => new float2(.8f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint9 = new ModConfigurationKey<float2>("Curve point 9", "how much to make voices fall off on log at point 9", () => new float2(.9f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint10 = new ModConfigurationKey<float2>("Curve point 10", "how much to make voices fall off on log at point 10", () => new float2(1f, 0));

        public override string Author => "989onan";

        public override string Link => "https://github.com/989onan/AudioFalloffFix";

        public override string Name => "AudioFalloffFix";

        public override string Version => "1.0.1";

        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony($"{Author}.{Name}");
            Config = GetConfiguration();
            Config.Save(true);
            harmony.PatchAll();
        }

        [HarmonyPatch]
        private class PatchKnockbackNode
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(AudioOutputConnector), "ApplyChanges")]
            static void ApplyChangesPostFix(AudioOutputConnector __instance)
            {

                AudioSource unityaudio = __instance.UnityAudioSource;
                if(__instance.UnityAudioSource != null)
                {
                    if (__instance.Owner != null)
                    {
                        if (__instance.Owner.RolloffMode == FrooxEngine.AudioRolloffMode.Logarithmic)
                        {
                            if(unityaudio.rolloffMode == AudioRolloffMode.Logarithmic)
                            {
                                float2 run0 = Config.GetValue(curvepoint0);
                                float2 run1 = Config.GetValue(curvepoint1);
                                float2 run2 = Config.GetValue(curvepoint2);
                                float2 run3 = Config.GetValue(curvepoint3);
                                float2 run4 = Config.GetValue(curvepoint4);
                                float2 run5 = Config.GetValue(curvepoint5);
                                float2 run6 = Config.GetValue(curvepoint6);
                                float2 run7 = Config.GetValue(curvepoint7);
                                float2 run8 = Config.GetValue(curvepoint8);
                                float2 run9 = Config.GetValue(curvepoint9);
                                float2 run10 = Config.GetValue(curvepoint10);



                                unityaudio.rolloffMode = AudioRolloffMode.Custom;
                                unityaudio.SetCustomCurve(AudioSourceCurveType.CustomRolloff, new AnimationCurve(new Keyframe[] { 
                                    new Keyframe(run0.x,run0.y), 
                                    new Keyframe(run1.x,run1.y),
                                    new Keyframe(run2.x,run2.y),
                                    new Keyframe(run3.x,run3.y),
                                    new Keyframe(run4.x,run4.y),
                                    new Keyframe(run5.x,run5.y),
                                    new Keyframe(run6.x,run6.y),
                                    new Keyframe(run7.x,run7.y),
                                    new Keyframe(run8.x,run8.y),
                                    new Keyframe(run9.x,run9.y),
                                    new Keyframe(run10.x, run10.y)
                                }));
                            }
                        }
                    }
                }
                
                

                
            }

        }
    }
}