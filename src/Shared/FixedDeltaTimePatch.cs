using UnityEngine;
using BepInEx.Logging;

namespace UnityFixedDeltaTimeFix.Shared
{
    public sealed class FixedDeltaTimePatch(ManualLogSource logger)
    {
        private readonly ManualLogSource _logger = logger;
        private int _lastLoggedRefreshRate = -1;
        private bool _invalidRefreshRateLogged;

        public void Update()
        {
            var refreshRate = Screen.currentResolution.refreshRate;

            if (refreshRate <= 0)
            {
                if (!_invalidRefreshRateLogged)
                {
                    _logger.LogWarning($"Unable to update Time.fixedDeltaTime because the display refresh rate is invalid ({refreshRate} Hz).");
                    _invalidRefreshRateLogged = true;
                }

                // Log the display state again when a valid refresh rate becomes available.
                _lastLoggedRefreshRate = -1;

                return;
            }

            _invalidRefreshRateLogged = false;

            var fixedDeltaTime = 1f / refreshRate;
            var previousFixedDeltaTime = Time.fixedDeltaTime;

            if (previousFixedDeltaTime != fixedDeltaTime)
            {
                Time.fixedDeltaTime = fixedDeltaTime;
            }

            if (_lastLoggedRefreshRate == refreshRate)
            {
                return;
            }

            _lastLoggedRefreshRate = refreshRate;

            if (previousFixedDeltaTime == fixedDeltaTime)
            {
                _logger.LogInfo($"Display refresh rate is {refreshRate} Hz. Time.fixedDeltaTime is already set to {fixedDeltaTime:0.########} s.");
                return;
            }

            _logger.LogInfo($"Display refresh rate is {refreshRate} Hz. Time.fixedDeltaTime changed from {previousFixedDeltaTime:0.########} s to {fixedDeltaTime:0.########} s.");
        }
    }
}
