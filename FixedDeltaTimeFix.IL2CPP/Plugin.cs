using System;
using BepInEx;
using UnityEngine;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using FixedDeltaTimeFix.Shared;
using Il2CppInterop.Runtime.Injection;

namespace FixedDeltaTimeFix.IL2CPP
{
    [BepInPlugin(MyPluginInfoEx.PLUGIN_GUID, MyPluginInfoEx.PLUGIN_NAME, MyPluginInfoEx.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        internal static new ManualLogSource Log;

        public GameObject FixedDeltaTimeListener;

        private static int _refreshRate;
        private static float _fixedDeltaTime;

        public override void Load()
        {
            // Global logger
            Log = base.Log;

            Log.LogInfo($"{MyPluginInfoEx.PLUGIN_GUID} loaded.");

            // Initial fixedDeltaTime
            Log.LogInfo($"Initial fixedDeltaTime is '{Time.fixedDeltaTime}'.");

            UpdateFixedDeltaTime();

            // Listener GameObject
            ClassInjector.RegisterTypeInIl2Cpp<FixedDeltaTimeListenerComponent>();

            FixedDeltaTimeListener = new GameObject("FixedDeltaTimeFix.Listener");
            GameObject.DontDestroyOnLoad(FixedDeltaTimeListener);
            FixedDeltaTimeListener.hideFlags = HideFlags.HideAndDontSave;
            FixedDeltaTimeListener.AddComponent<FixedDeltaTimeListenerComponent>();

            Log.LogInfo($"Listener GameObject created.");
        }

        public static float GetCurrentFixedDeltaTime()
        {
            return _fixedDeltaTime;
        }

        public static void UpdateFixedDeltaTime()
        {
            _refreshRate = Screen.currentResolution.refreshRate;
            _fixedDeltaTime = 1f / Utils.GetLowestRefreshRateMultipleAbove50(_refreshRate);
            Time.fixedDeltaTime = _fixedDeltaTime;

            Log.LogInfo($"Updated fixedDeltaTime to '{Time.fixedDeltaTime}'.");
        }
    }

    public class FixedDeltaTimeListenerComponent(IntPtr handle) : MonoBehaviour(handle)
    {
        public void Update()
        {
            if (Time.fixedDeltaTime != Plugin.GetCurrentFixedDeltaTime())
            {
                Plugin.Log.LogInfo($"The current fixedDeltaTime value of '{Time.fixedDeltaTime}' doesn't match the target value of '{Plugin.GetCurrentFixedDeltaTime()}'. Updating...");

                Plugin.UpdateFixedDeltaTime();
            }
        }
    }
}
