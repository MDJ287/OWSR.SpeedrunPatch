using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Collections.Generic;
using HarmonyLib;
using OWSR.SpeedrunPatch.src;

namespace Doorstop
{
    public class Entrypoint
    {
        public static void Start()
        {
            new Harmony("com.owsr.speedrunpatch").PatchAll();

            // apply patches

            Watermark.ApplyPatch();

            TranslatorFix.ApplyPatch();
        }
    }
}
