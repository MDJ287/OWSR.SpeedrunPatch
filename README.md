# OWSR.SpeedrunPatch

**(NOT YET SPEEDRUN-LEGAL)** patch for Outer Wilds making text translation no longer framerate dependent.

Inspired by Silksong speedrunning's [SpeedPatch](https://github.com/hk-speedrunning/Silksong.SpeedPatch).

# Installing the patch

* Download `OWSR.SpeedrunPatch-v*.*.*.zip` from releases
* Right click Outer Wilds on Steam, select Manage > Browse local files
* Copy the contents of `OWSR.SpeedrunPatch-v*.*.*.zip` into the `Outer Wilds` folder
* Launch the game. The Speedrun Patch version number should be visible in the bottom-left of the title screen, under the Outer Wilds version number.

**NOTE:** This patch **does not work** on Outer Wilds v1.0.7 (Unity Doorstop will crash the game). If playing on v1.0.7, uninstall the patch before launching the game. Every other game version works fine.

# Uninstalling the patch

Browse local files for Outer Wilds, delete the `owsr` folder and its contents as well as `.doorstop_version`, `doorstop_config.ini`, and `winhttp.dll`. This should remove any trace that the patch was ever installed.

# Q&A

## Is this patch legal for speedruns?

Not yet! We will probably be holding a community vote at some point. Currently most people seem to be in favor.

## Why not go through the Mod Loader?

The Outer Wilds Mod Loader has some effects that affect speedruns; for example, it adds an extra button in the pause menu, changing the behavior of quick-savequits. This patch only fixes the translator and does nothing else.

## How exactly does this patch work?

This patch uses [Unity Doorstop](https://github.com/NeighTools/UnityDoorstop) and [Mono.Cecil](https://www.mono-project.com/docs/tools+libraries/libraries/Mono.Cecil/) to modify the assembly of the game and introduce certain speedrun-legal patches.

### TranslatorFix

The method `DisplayTextNode()` of class `NomaiTranslatorProp` runs once per rendered frame, and can increment the number of translated words (`_numTranslatedWords`) once each time it runs. The game tries to translate every piece of text in 0.2 seconds, but this method creates a bottleneck base on the player's framerate. With this patch, the game will repeatedly increment `_numTranslatedWords` until it reaches the amount it is designed to (this is effectively equivalent to the translation time a player would have if they had "infinite" frames per second).

## I have a question/suggestion, or I think you made a mistake, or I want to discuss the patch

Please ping me (**@mdj287**) on the [Outer Wilds Speedrun Discord](https://discord.gg/4us2uATJpn)! I will be happy to respond to anything about the patch.
