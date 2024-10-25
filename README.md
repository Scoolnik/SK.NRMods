# Night-Runners mods

## Mods list

Mod | Description | Download
-|-|:-:
| [**Drag stats**](https://github.com/Scoolnik/SK.NRMods/tree/master/SK.NRMods.DragStats/README.md) | This mod allows you to measure drag stats (like time from 0 to 100km/h) | [click](https://github.com/Scoolnik/SK.NRMods/releases/download/DS-v1.0.2/SK.NRMods.DragStats.dll)|
| [**Custom music**](https://github.com/Scoolnik/SK.NRMods/blob/master/SK.NRMods.CustomMusic/README.md) | This mod allows you to use custom tracks in game | [click](https://github.com/Scoolnik/SK.NRMods/releases/download/CM-v1.0.2/SK.NRMods.CustomMusic.zip)|

## Installation
1. Install [BepInEx 6 Unity (IL2CPP)](https://builds.bepinex.dev/projects/bepinex_be/692/BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.692%2B851521c.zip), at least v647, but its better to have the latest version. Official site: [builds.bepinex.dev﻿](https://builds.bepinex.dev/projects/bepinex_be)
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
