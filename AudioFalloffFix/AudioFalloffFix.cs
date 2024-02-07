using HarmonyLib;
using ResoniteModLoader;
using UnityFrooxEngineRunner;
using UnityEngine;
using UnityEngine.Audio;
using Elements.Core;
using FrooxEngine;

namespace AudioFalloffFix
{
    public class AudioFalloffFix : ResoniteMod
    {
        public static ModConfiguration Config;


        /// <summary>
        /// This is shit. I wanna make this better, but no custom ui has forced my hand. Here, have 10 control points... PER AUDIO GROUP
        /// </summary>
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<bool> enablevoices = new ModConfigurationKey<bool>("Voices logmatrithic force", "Voices Restrictor Enabled", () => true);
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint0 = new ModConfigurationKey<float2>("Curve point 10", "how much to make voices fall off on log at point 10", () => new float2(0f, 1));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> curvepoint1 = new ModConfigurationKey<float2>("Curve point 1", "how much to make voices fall off on log at point 1", () => new float2(0.1f,.1f));
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


        [AutoRegisterConfigKey]
        private static ModConfigurationKey<bool> enablesoundeffect = new ModConfigurationKey<bool>("sound effects logmarithic force", "Sound Effects Restrictor Enabled", () => true);
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> ecurvepoint0 = new ModConfigurationKey<float2>("E Curve point 10", "how much to make voices fall off on log at point 10", () => new float2(0f, 1));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> ecurvepoint1 = new ModConfigurationKey<float2>("E Curve point 1", "how much to make voices fall off on log at point 1", () => new float2(0.1f, .1f));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> ecurvepoint2 = new ModConfigurationKey<float2>("E Curve point 2", "how much to make voices fall off on log at point 2", () => new float2(.2f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> ecurvepoint3 = new ModConfigurationKey<float2>("E Curve point 3", "how much to make voices fall off on log at point 3", () => new float2(.3f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> ecurvepoint4 = new ModConfigurationKey<float2>("E Curve point 4", "how much to make voices fall off on log at point 4", () => new float2(.4f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> ecurvepoint5 = new ModConfigurationKey<float2>("E Curve point 5", "how much to make voices fall off on log at point 5", () => new float2(.5f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> ecurvepoint6 = new ModConfigurationKey<float2>("E Curve point 6", "how much to make voices fall off on log at point 6", () => new float2(.6f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> ecurvepoint7 = new ModConfigurationKey<float2>("E Curve point 7", "how much to make voices fall off on log at point 7", () => new float2(.7f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> ecurvepoint8 = new ModConfigurationKey<float2>("E Curve point 8", "how much to make voices fall off on log at point 8", () => new float2(.8f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> ecurvepoint9 = new ModConfigurationKey<float2>("E Curve point 9", "how much to make voices fall off on log at point 9", () => new float2(.9f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> ecurvepoint10 = new ModConfigurationKey<float2>("E Curve point 10", "how much to make voices fall off on log at point 10", () => new float2(1f, 0));

        [AutoRegisterConfigKey]
        private static ModConfigurationKey<bool> enablemultimedia = new ModConfigurationKey<bool>("multimedia effects logmarithic force", "MultiMedia Restrictor Enabled", () => true);
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> mcurvepoint0 = new ModConfigurationKey<float2>("M Curve point 10", "how much to make voices fall off on log at point 10", () => new float2(0f, 1));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> mcurvepoint1 = new ModConfigurationKey<float2>("M Curve point 1", "how much to make voices fall off on log at point 1", () => new float2(0.1f, .1f));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> mcurvepoint2 = new ModConfigurationKey<float2>("M Curve point 2", "how much to make voices fall off on log at point 2", () => new float2(.2f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> mcurvepoint3 = new ModConfigurationKey<float2>("M Curve point 3", "how much to make voices fall off on log at point 3", () => new float2(.3f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> mcurvepoint4 = new ModConfigurationKey<float2>("M Curve point 4", "how much to make voices fall off on log at point 4", () => new float2(.4f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> mcurvepoint5 = new ModConfigurationKey<float2>("M Curve point 5", "how much to make voices fall off on log at point 5", () => new float2(.5f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> mcurvepoint6 = new ModConfigurationKey<float2>("M Curve point 6", "how much to make voices fall off on log at point 6", () => new float2(.6f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> mcurvepoint7 = new ModConfigurationKey<float2>("M Curve point 7", "how much to make voices fall off on log at point 7", () => new float2(.7f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> mcurvepoint8 = new ModConfigurationKey<float2>("M Curve point 8", "how much to make voices fall off on log at point 8", () => new float2(.8f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> mcurvepoint9 = new ModConfigurationKey<float2>("M Curve point 9", "how much to make voices fall off on log at point 9", () => new float2(.9f, 0));
        [AutoRegisterConfigKey]
        private static ModConfigurationKey<float2> mcurvepoint10 = new ModConfigurationKey<float2>("M Curve point 10", "how much to make voices fall off on log at point 10", () => new float2(1f, 0));




        public override string Author => "989onan";

        public override string Link => "https://github.com/989onan/AudioFalloffFix";

        public override string Name => "AudioFalloffFix";

        public override string Version => "1.0.0";

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
                            if(unityaudio.rolloffMode == UnityEngine.AudioRolloffMode.Logarithmic)
                            {
                                //More hell... At least it's a switch statement. kinda.
                                // also my variables are named terribly like valve's fingers. being mad about this is valid.
                                switch (__instance.Owner.AudioTypeGroup.Value)
                                {
                                    case AudioTypeGroup.Voice:
                                        if (!Config.GetValue(enablevoices)) break;
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



                                        unityaudio.rolloffMode = UnityEngine.AudioRolloffMode.Custom;
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
                                        break;
                                    case AudioTypeGroup.Multimedia:
                                        if (!Config.GetValue(enablemultimedia)) break;
                                        float2 run01 = Config.GetValue(mcurvepoint0);
                                        float2 run11 = Config.GetValue(mcurvepoint1);
                                        float2 run21 = Config.GetValue(mcurvepoint2);
                                        float2 run31 = Config.GetValue(mcurvepoint3);
                                        float2 run41 = Config.GetValue(mcurvepoint4);
                                        float2 run51 = Config.GetValue(mcurvepoint5);
                                        float2 run61 = Config.GetValue(mcurvepoint6);
                                        float2 run71 = Config.GetValue(mcurvepoint7);
                                        float2 run81 = Config.GetValue(mcurvepoint8);
                                        float2 run91 = Config.GetValue(mcurvepoint9);
                                        float2 run101 = Config.GetValue(mcurvepoint10);



                                        unityaudio.rolloffMode = UnityEngine.AudioRolloffMode.Custom;
                                        unityaudio.SetCustomCurve(AudioSourceCurveType.CustomRolloff, new AnimationCurve(new Keyframe[] {
                                            new Keyframe(run01.x,run01.y),
                                            new Keyframe(run11.x,run11.y),
                                            new Keyframe(run21.x,run21.y),
                                            new Keyframe(run31.x,run31.y),
                                            new Keyframe(run41.x,run41.y),
                                            new Keyframe(run51.x,run51.y),
                                            new Keyframe(run61.x,run61.y),
                                            new Keyframe(run71.x,run71.y),
                                            new Keyframe(run81.x,run81.y),
                                            new Keyframe(run91.x,run91.y),
                                            new Keyframe(run101.x, run101.y)
                                        }));
                                        break;
                                    case AudioTypeGroup.SoundEffect:
                                        if (!Config.GetValue(enablesoundeffect)) break;
                                        float2 run02 = Config.GetValue(ecurvepoint0);
                                        float2 run12 = Config.GetValue(ecurvepoint1);
                                        float2 run22 = Config.GetValue(ecurvepoint2);
                                        float2 run32 = Config.GetValue(ecurvepoint3);
                                        float2 run42 = Config.GetValue(ecurvepoint4);
                                        float2 run52 = Config.GetValue(ecurvepoint5);
                                        float2 run62 = Config.GetValue(ecurvepoint6);
                                        float2 run72 = Config.GetValue(ecurvepoint7);
                                        float2 run82 = Config.GetValue(ecurvepoint8);
                                        float2 run92 = Config.GetValue(ecurvepoint9);
                                        float2 run102 = Config.GetValue(ecurvepoint10);



                                        unityaudio.rolloffMode = UnityEngine.AudioRolloffMode.Custom;
                                        unityaudio.SetCustomCurve(AudioSourceCurveType.CustomRolloff, new AnimationCurve(new Keyframe[] {
                                            new Keyframe(run02.x,run02.y),
                                            new Keyframe(run12.x,run12.y),
                                            new Keyframe(run22.x,run22.y),
                                            new Keyframe(run32.x,run32.y),
                                            new Keyframe(run42.x,run42.y),
                                            new Keyframe(run52.x,run52.y),
                                            new Keyframe(run62.x,run62.y),
                                            new Keyframe(run72.x,run72.y),
                                            new Keyframe(run82.x,run82.y),
                                            new Keyframe(run92.x,run92.y),
                                            new Keyframe(run102.x, run102.y)
                                        }));
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
                
                

                
            }

        }
    }
}