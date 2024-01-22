using BepInEx;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace StargateResolutionFix {

    [BepInProcess("stargate.exe")]
    [BepInPlugin("blargle.stargate.menufix", "menu fix", "0.1.0")]
    public class Main : BaseUnityPlugin {

        public void Awake() {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "blargle.stargate.menufix");
        }
    }

    [HarmonyPatch(typeof(MainMenuManager), "MainPanel")]
    class MainMenuManager_Patch {
        static bool alreadyAdjusted = false;
        static Quaternion rotation;

        public static void Postfix(SettingsPanel ___settingsPanel, GameObject ___mainPanelCamera) {
            if (!alreadyAdjusted) {
                rotation = ___mainPanelCamera.transform.rotation;
                alreadyAdjusted = true;
            }

            Quaternion newRotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y - 10f, rotation.eulerAngles.z);
            ___mainPanelCamera.transform.rotation = newRotation;
        }
    }
}