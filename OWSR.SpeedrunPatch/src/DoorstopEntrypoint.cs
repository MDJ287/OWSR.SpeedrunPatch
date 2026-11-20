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
            File.WriteAllText("_start.txt", "");
            new Harmony("OWSR.SpeedrunPatch").PatchAll(Assembly.GetExecutingAssembly());
            File.WriteAllText("_done.txt", $"{typeof(NomaiTranslatorProp).Name}");
        }
    }
}
