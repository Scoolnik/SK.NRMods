# Custom music mod (aka LibreMusic)

A mod that adds custom music support.

## How to use
1. Download mod and unzip it to plugins folder
2. Run game, so all the required folders will be created
3. Put your audio files to `Music` folder inside game folder (TODO: pic)
4. Run the game and listen to your favourite music :)
 
Custom tracks will be added to default playlists. 
You can add song to a specific scene (like garage or race) by putting it to corresponding subfolder inside `Music` folder
If you need to add a track to several scenes, just create folder with the name of both scenes separated by space (ex: `cruise race`)
Supported scenes: cruise, garage, main_menu (hello screen), meetspot, race.

## Supported formats
Almost any

## How it works
On any scene change plugin loads playlist for this scene and selects a random track (with priority to custom tracks)
If a song is not .ogg (or .wav, if im not mistaken) it will be converted to .ogg in background and stored in .cache folder.
	I accidentally tested it with 800mb .flac file and it was converted in about a minute. Disk space overhead was about 50mb.

## TODO
Yandex music/Spotify/Youtube/etc support, so u could just use link to listen music