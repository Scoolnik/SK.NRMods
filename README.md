# Night-Runners mods

## Important notes:
Since game changed it's major version in new Private Alpha - all plugins with version "v1.x.x" should be considered only compatible with Prologue.

### In short: 
- For Prologue - use plugins with version "v1.x.x"
- For Private Alpha (2026) - use plugins with version "v2.x.x"

## Mods list

Mod | Description | Download
-|-|:-:
| [**Drag stats**](https://github.com/Scoolnik/SK.NRMods/tree/master/SK.NRMods.DragStats/README.md) | This mod allows you to measure drag stats (like time from 0 to 100km/h) | [v1.0.2](https://github.com/Scoolnik/SK.NRMods/releases/download/DS-v1.0.2/SK.NRMods.DragStats.dll)<br>[v2.0.0](https://github.com/Scoolnik/SK.NRMods/releases/download/DS-v2.0.0/SK.NRMods.DragStats.dll)|
| [**Custom music**](https://github.com/Scoolnik/SK.NRMods/blob/master/SK.NRMods.CustomMusic/README.md) | This mod allows you to use custom tracks in game | [v1.0.2](https://github.com/Scoolnik/SK.NRMods/releases/download/CM-v1.0.2/SK.NRMods.CustomMusic.zip)|
| [**Skip intro**](https://github.com/Scoolnik/SK.NRMods/blob/master/SK.NRMods.SkipIntro/README.md) | This mod allows you to skip intro and load directly to garage | [v2.0.0](https://github.com/Scoolnik/SK.NRMods/releases/download/SI-v2.0.0/SK.NRMods.SkipIntro.dll)|

## Installation
1. Install [BepInEx 6 Unity (IL2CPP)](https://builds.bepinex.dev/projects/bepinex_be/755/BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.755%2B3fab71a.zip), at least v647, but its better to have the latest version. Official site: [builds.bepinex.dev﻿](https://builds.bepinex.dev/projects/bepinex_be)
2. Download a mod from the table above or from [releases page](https://github.com/Scoolnik/SK.NRMods/releases)
3. Place mod dll to `BepInEx/plugins` folder inside game folder (will be created after first game launch with bepinex)

﻿The game folder is the folder containing the game's executable.

To open it from Steam just right-click the game in the library and select `Manage` -> `Browse local files`.
> [!TIP]
> If you don't know which bepinex version you need﻿, just look for `BepInEx Unity (IL2CPP) for Windows (x64) games`

## Build (for devs)

1. Download this repository
2. Create `game.props` file with your game path, based on [`game.props.example`](https://github.com/Scoolnik/SK.NRMods/blob/master/game.props.example) (used to reference game files and to copy plugin after build)
3. Open solution in visual studio
4. Build required mod
5. Run game to test it :)
