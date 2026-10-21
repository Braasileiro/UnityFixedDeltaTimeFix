# UnityFixedDeltaTimeFix

Adjusts Unity [Time.fixedDeltaTime](https://docs.unity3d.com/ScriptReference/Time-fixedDeltaTime.html) according to your current refresh rate.

The `Time.fixedDeltaTime` calculation formula is `1f / current_refresh_rate`.

This is mostly enough to solve camera stuttering problems, but it's not a definitive solution for all games that suffer from it, as it directly affects the timing of Unity's physics calculation.

In some cases, this can introduce bugs and also increase CPU consumption and affect performance. The higher the refresh rate, the tighter the timing.

Each game may behave differently, so test it out and see if it's satisfactory.

## Installation

1. Identify the scripting backend used by the game:

   - **IL2CPP** games usually have a `GameAssembly.dll` file in the game directory.
   - **Mono** games have an `Assembly-CSharp.dll` file inside `<GameName>_Data/Managed` and no `GameAssembly.dll`.

2. Install the appropriate [BepInEx 6](https://docs.bepinex.dev/master/articles/user_guide/installation/index.html) build for the game's scripting backend, operating system and architecture.

   Builds are available from the [BepInEx bleeding edge page](https://builds.bepinex.dev/projects/bepinex_be):

   - **Mono:** `BepInEx-Unity.Mono-<os>-<architecture>-<build>.zip`
   - **IL2CPP:** `BepInEx-Unity.IL2CPP-<os>-<architecture>-<build>.zip`

3. Start the game once so BepInEx can create its directories and configuration files, then close it.

4. Download the asset for the correct scripting backend from the [latest release](https://github.com/Braasileiro/UnityFixedDeltaTimeFix/releases/latest).

5. Extract the archive into the game's `BepInEx/plugins` directory. The resulting installation should contain the matching plugin DLL:

   ```text
   <GameDirectory>/
   └── BepInEx/
       └── plugins/
           └── UnityFixedDeltaTimeFix.Mono.dll
   ```

   or:

   ```text
   <GameDirectory>/
   └── BepInEx/
       └── plugins/
           └── UnityFixedDeltaTimeFix.IL2CPP.dll
   ```

Do not install both versions. BepInEx will load the plugin automatically the next time the game starts.

## Usage

Just start the game. The plugin keeps `Time.fixedDeltaTime` synchronized with the current refresh rate.

## Building

The repository includes the Unity and IL2CPP compile-time reference assemblies required by both projects, so a game installation is not required to build them.

### Using mise

Install [mise](https://mise.jdx.dev/getting-started.html), then run:

```shell
mise install
mise build
```

To create release builds:

```shell
mise publish
```

The outputs are written to:

- Debug builds: `artifacts/bin/<project>/debug`
- Release builds: `artifacts/publish/<project>/release`

### Using the .NET SDK directly

Install the .NET 10 SDK and run:

```shell
dotnet build UnityFixedDeltaTimeFix.slnx --maxcpucount:1
```

To create release builds:

```shell
dotnet publish src/IL2CPP/UnityFixedDeltaTimeFix.IL2CPP.csproj --configuration Release
dotnet publish src/Mono/UnityFixedDeltaTimeFix.Mono.csproj --configuration Release
```

The bundled IL2CPP reference assemblies target Unity `2020.3.49f1`. To compile against another reference pack, set `UnityIl2CppInteropDir`:

```shell
dotnet build src/IL2CPP/UnityFixedDeltaTimeFix.IL2CPP.csproj \
  -p:UnityIl2CppInteropDir=/path/to/game/BepInEx/interop
```

### Creating a release

The release workflow runs when a version tag matching the version in `Directory.Build.props` is pushed. For example:

```shell
git tag --annotate 0.1.0 --message "Release 0.1.0"
git push origin 0.1.0
```

The workflow publishes both projects and creates separate Mono and IL2CPP archives in the GitHub release.

## Notes

To disable the BepInEx console, open `BepInEx/config/BepInEx.cfg`, find the `[Logging.Console]` section and set `Enabled` to `false`.

## Thanks

[BepInEx](https://github.com/BepInEx/BepInEx)
