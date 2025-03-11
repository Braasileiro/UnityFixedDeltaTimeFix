# UnityFixedDeltaTimeFix
Adjusts Unity [Time.fixedDeltaTime](https://docs.unity3d.com/ScriptReference/Time-fixedDeltaTime.html) according to the lowest multiple above 50 FPS of your refresh rate.

This plugin will automatically select the lowest multiple of your refresh rate above 50 FPS.

Examples:
* 60 Hz: The lowest multiple of 60 Hz above 50 FPS is 60 FPS itself.
* 120 Hz: 60 FPS is the lowest multiple.
* 144 Hz: 72 FPS is the lowest multiple.

The `Time.fixedDeltaTime` calculation formula is `1f / lowest_refresh_rate_multiple_above50`.

This is mostly enough to solve camera stuttering problems, but it's not a definitive solution for all games that suffer from it, as it directly affects the timing of Unity's physics calculation.

In some cases, this can introduce bugs and also increase CPU consumption and affect performance. The higher the refresh rate, the tighter the timing.

Each game may behave differently, so test it out and see if it's satisfactory.

# Installation
1. Install [BepInEx 6](https://docs.bepinex.dev/master/articles/user_guide/installation/index.html).

2. Select the [latest artifact here](https://builds.bepinex.dev/projects/bepinex_be).
> * **Mono**: Download BepInEx-Unity.Mono-`windows|linux|macos`-`x86|x64`-xxx.zip *(BepInEx Unity (Mono) for `Windows|Linux|macOS` (`x86|x64`) games)*.
> * **IL2CPP**: Download BepInEx-Unity.IL2CPP-`windows|linux|macos`-`x86|x64`-xxx.zip *(BepInEx Unity (IL2CPP) for `Windows|Linux|macOS` (`x86|x64`) games)*.

3. Open the game at least once after installing BepInEx.

4. Download the [latest release](https://github.com/Braasileiro/UnityFixedDeltaTimeFix/releases/latest) and extract the zip file into the ***`BepInEx\plugins`*** folder in the game installation directory.

# Usage
Just play the game my little PogChamp. The plugin will apply the timing according to your refresh rate.

# Notes
If you wish, you can disable the BepInEx console. Simply open the ***`BepInEx\config\BepInEx.cfg`*** file, navigate to `[Logging.Console]` section and change `Enabled` to `false`.

# Thanks
[BepInEx](https://github.com/BepInEx/BepInEx)
