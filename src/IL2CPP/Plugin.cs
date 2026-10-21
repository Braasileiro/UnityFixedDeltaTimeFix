using System;
using BepInEx;
using UnityEngine;
using BepInEx.Unity.IL2CPP;
using UnityFixedDeltaTimeFix.Shared;

namespace UnityFixedDeltaTimeFix.IL2CPP
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public sealed class Plugin : BasePlugin
    {
        internal static FixedDeltaTimePatch FixedDeltaTimePatch;

        public override void Load()
        {
            Log.LogInfo($"{MyPluginInfo.PLUGIN_NAME} {MyPluginInfo.PLUGIN_VERSION} loaded.");

            FixedDeltaTimePatch = new FixedDeltaTimePatch(Log);
            FixedDeltaTimePatch.Update();

            AddComponent<FixedDeltaTimeListener>();
        }
    }

    public sealed class FixedDeltaTimeListener(IntPtr pointer) : MonoBehaviour(pointer)
    {
        private void Update()
        {
            Plugin.FixedDeltaTimePatch.Update();
        }
    }
}
