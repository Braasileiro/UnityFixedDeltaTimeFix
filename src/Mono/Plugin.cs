using BepInEx;
using BepInEx.Unity.Mono;
using UnityFixedDeltaTimeFix.Shared;

namespace UnityFixedDeltaTimeFix.Mono
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public sealed class Plugin : BaseUnityPlugin
    {
        private FixedDeltaTimePatch _fixedDeltaTimePatch;

        private void Awake()
        {
            Logger.LogInfo($"{MyPluginInfo.PLUGIN_NAME} {MyPluginInfo.PLUGIN_VERSION} loaded.");

            _fixedDeltaTimePatch = new FixedDeltaTimePatch(Logger);
            _fixedDeltaTimePatch.Update();
        }

        private void Update()
        {
            _fixedDeltaTimePatch.Update();
        }
    }
}
