# OWSR.SpeedrunPatch

**(NOT YET SPEEDRUN-LEGAL)** patch for Outer Wilds making text translation no longer framerate dependent.

Inspired by Silksong speedrunning's [SpeedPatch](https://github.com/hk-speedrunning/Silksong.SpeedPatch).

# Installing the patch

* Download `TranslatorFix-v*.*.*.zip` from releases
* Right click Outer Wilds on Steam, select Manage > Browse local files
* Copy the contents of `TranslatorFix-v*.*.*.zip` into the `Outer Wilds` folder
* Open the game. Try reading the easter egg scroll on Brittle Hollow to see if the patch worked; if it did, it should take less than a second to translate.

# Uninstalling the patch

Browse local files for Outer Wilds, delete the `owsr` folder and its contents as well as `.doorstop_version`, `doorstop_config.ini`, and `winhttp.dll`. This should remove any trace that the patch was ever installed.

# Q&A

## Why not go through the Mod Loader?

The Outer Wilds Mod Loader has some effects that affect speedruns; for example, it adds an extra button in the pause menu, changing the behavior of quick-savequits. This patch only fixes the translator and does nothing else.

## How exactly does this patch work?

TODO

Temporary short answer: it uses [Unity Doorstop](https://github.com/NeighTools/UnityDoorstop) and [Mono.Cecil](https://www.mono-project.com/docs/tools+libraries/libraries/Mono.Cecil/) to modify the assembly of the game so that the translator translates more than 1 word per frame.

## Is this patch legal for speedruns?

Not yet! We will probably be holding a community vote at some point. Currently most people seem to be in favor.

## I have a question/suggestion, or I think you made a mistake, or I want to discuss the patch

Please ping me (**@mdj287**) on the [Outer Wilds Speedrun Discord](https://discord.gg/4us2uATJpn)! I will be happy to respond to anything about the patch.
