using BepInEx;
using UnityEngine;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using FixedDeltaTimeFix.Shared;

namespace FixedDeltaTimeFix.Mono
{
    [BepInPlugin(MyPluginInfoEx.PLUGIN_GUID, MyPluginInfoEx.PLUGIN_NAME, MyPluginInfoEx.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        internal static int _refreshRate;
        internal static float _fixedDeltaTime;

        public void Awake()
        {
            // Global logger
            Logger = base.Logger;

            Logger.LogInfo($"{MyPluginInfoEx.PLUGIN_GUID} loaded.");

            // Initial fixedDeltaTime
            Logger.LogInfo($"Initial fixedDeltaTime is '{Time.fixedDeltaTime}'.");

            UpdateFixedDeltaTime();
        }

        public void Update()
        {
            if (Time.fixedDeltaTime != _fixedDeltaTime)
            {
                Logger.LogInfo($"The current fixedDeltaTime value of '{Time.fixedDeltaTime}' doesn't match the target value of '{_fixedDeltaTime}'. Updating...");

                UpdateFixedDeltaTime();
            }
        }

        private static void UpdateFixedDeltaTime()
        {
            _refreshRate = Screen.currentResolution.refreshRate;
            _fixedDeltaTime = 1f / Utils.GetLowestRefreshRateMultipleAbove50(_refreshRate);
            Time.fixedDeltaTime = _fixedDeltaTime;

            Logger.LogInfo($"Updated fixedDeltaTime to '{Time.fixedDeltaTime}'.");
        }
    }
}
