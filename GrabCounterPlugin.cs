using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrabCounter
{
    [BepInPlugin("com.Dullfin.FPSLimiter", "FPS Limiter", "1.0")]
    public class GrabCounter : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        private readonly Harmony harmony = new Harmony("com.Dullfin.FPSLimiter");

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;
            SceneManager.sceneLoaded += OnSceneLoaded;

            harmony.PatchAll();
            Logger.LogInfo($"FPS Limiter Plugin has been loaded.");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TitleScreen")
            {
                // Locate the UI-Gameplay object
                var uiGameplay = GameObject.Find("UI-Menu_Settings/c_Settings/c_SettingsSubMenu/c_controls/scrollView/viewport/content");
                Logger.LogInfo($"uiGameplay {uiGameplay} has been loaded.");
                var newLocalPos = new Vector3(0, 0, 0);
                GameObject grabCounterTextObj;

                //get children of UI-Gameplay
                foreach (Transform child in uiGameplay.transform)
                {
                    Logger.LogInfo($"Child: {child.name}");
                    if (child.gameObject.activeInHierarchy && child.name != "txt_GrabCounter")
                    {
                        var childPos = child.transform.position;
                        newLocalPos = new Vector3(child.transform.localPosition.x, transform.localPosition.x + 50, child.transform.localPosition.z);
                        child.transform.localPosition = newLocalPos;
                        if (child.name == "txt_Control")
                        {
                            grabCounterTextObj = Instantiate(child.gameObject, childPos, Quaternion.identity, uiGameplay.transform);
                            var grabCounterLocalPos = grabCounterTextObj.transform.localPosition;
                            grabCounterTextObj.transform.localPosition = new Vector3(grabCounterLocalPos.x, grabCounterLocalPos.y - 50, grabCounterLocalPos.z);
                            grabCounterTextObj.name = "txt_GrabCounter";
                            grabCounterTextObj.GetComponent<TextMeshProUGUI>().text = "Grabs: 0";
                        }
                    }
                }
            }
        }
    }
}
