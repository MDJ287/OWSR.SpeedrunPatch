# Compiling the code

My steps to get to the .zip in releases:

* Open the code in Visual Studio 2022
* Build the program using Visual Studio
* Place `OWSR.SpeedrunPatch.dll` in a folder called `owsr`
* Drop all the files from [Unity Doorstop v4.6.0](https://github.com/NeighTools/UnityDoorstop/releases/tag/v4.6.0) into a folder along with the `owsr` folder
* Update `doorstop_config.ini` to have `target_assembly=owsr/OWSR.SpeedrunPatch.dll` and `dll_search_path_override=owsr/`
* Send the contents of that outer folder to a zip file

Yeah I should probably have a more consistent build process
