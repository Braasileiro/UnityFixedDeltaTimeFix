using UnityEngine;
using BepInEx.Logging;

namespace UnityFixedDeltaTimeFix.Shared
{
    public sealed class FixedDeltaTimePatch(ManualLogSource logger)
    {
        private const int MinimumTargetFrameRate = 50;

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

            var targetFrameRate = GetLowestDivisorAboveMinimum(refreshRate);
            var fixedDeltaTime = 1f / targetFrameRate;
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
                _logger.LogInfo($"Display refresh rate is {refreshRate} Hz. Target physics rate is {targetFrameRate} FPS. Time.fixedDeltaTime is already set to {fixedDeltaTime:0.########} s.");
                return;
            }

            _logger.LogInfo($"Display refresh rate is {refreshRate} Hz. Target physics rate is {targetFrameRate} FPS. Time.fixedDeltaTime changed from {previousFixedDeltaTime:0.########} s to {fixedDeltaTime:0.########} s.");
        }

        private static int GetLowestDivisorAboveMinimum(int refreshRate)
        {
            // An exact divisor keeps physics updates aligned with display refreshes.
            for (var targetFrameRate = MinimumTargetFrameRate + 1; targetFrameRate < refreshRate; targetFrameRate++)
            {
                if (refreshRate % targetFrameRate == 0)
                {
                    return targetFrameRate;
                }
            }

            return refreshRate;
        }
    }
}
