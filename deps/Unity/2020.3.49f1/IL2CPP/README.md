# IL2CPP reference assemblies

These compile-time reference assemblies were generated for Unity `2020.3.49f1` by BepInEx `6.0.0-be.790` with Il2CppInterop `1.5.3`.

The project uses only these files:

- `Il2Cppmscorlib.dll`
- `Il2CppSystem.dll`
- `Il2CppSystem.Core.dll`
- `UnityEngine.CoreModule.dll`

They are marked with `Private=false` and must not be distributed with the plugin. At runtime, BepInEx loads the interop assemblies generated for the target game.

To compile temporarily against a different reference pack, set `UnityIl2CppInteropDir`:

```shell
dotnet build UnityFixedDeltaTimeFix.slnx -p:UnityIl2CppInteropDir=/path/to/game/BepInEx/interop
```
