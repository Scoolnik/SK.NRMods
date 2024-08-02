# Custom music mod (aka NR_LibreMusic)

A mod that adds custom music support.

## How to use
1. Download mod and unzip it to plugins folder (BepInEx required, check [README](https://github.com/Scoolnik/SK.NRMods/blob/master/README.md) for more info
2. Run game, so all the required folders will be created
3. Put your audio files to `Music` folder inside game folder
4. Run the game and listen to your favourite music :)
 
Custom tracks will be added to default playlists. 

You can add song to a specific scene (like garage or race) by putting it to corresponding subfolder inside `Music` folder

If you need to add a track to several scenes, just create folder with the name of both scenes separated by space (ex: `main_menu garage`)

Supported scenes: cruise, garage, main_menu (hello screen), meetspot, race.

![image](https://github.com/user-attachments/assets/1cd47e76-6cf5-4553-af4f-92d42e75801d)

## Supported file formats
Almost any (handled by ffmpeg)

## How it works
On any scene change plugin loads playlist for this scene and selects a random track (with priority to custom tracks)

If a song is not .ogg (or .wav, if im not mistaken) it will be converted to .ogg in background and stored in .cache folder.

I accidentally tested it with 800mb .flac file and it was converted in about a minute. Disk space overhead was about 50mb.

## TODO
- Yandex music/Spotify/Youtube/etc support, so u could just use link to listen music
- race win/lose music support
- download FFmpeg in runtime

![preview-min](https://github.com/user-attachments/assets/ca6d8314-a099-4054-90fc-88d061381012)
