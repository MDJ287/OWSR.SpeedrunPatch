# Compiling the code

My steps to get to the .zip in releases:

* Open the code in Visual Studio 2022
* Import [Mono.Cecil v0.11.6](https://www.nuget.org/packages/Mono.Cecil/) as a dependency
* Build the program using Visual Studio
* Place `OWSR.SpeedrunPatch.dll` and `Mono.Cecil.dll` in a folder called `owsr`
* Drop all the files from [Unity Doorstop v4.6.0](https://github.com/NeighTools/UnityDoorstop/releases/tag/v4.6.0) into a folder along with the `owsr` folder
* Update `doorstop_config.ini` to have `target_assembly=owsr/OWSR.SpeedrunPatch.dll` and `dll_search_path_override=owsr/`
* Send the contents of that outer folder to a zip file

Yeah I should probably have a more consistent build process
