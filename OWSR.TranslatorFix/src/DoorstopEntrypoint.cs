using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;
using OWSR.SpeedrunPatch.src;

namespace Doorstop
{
    public class Entrypoint
    {
        public static void Start()
        {
            string asmDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OuterWilds_Data", "Managed", "Assembly-CSharp.dll");
            string thisDirectory = typeof(Entrypoint).Assembly.Location;

            DefaultAssemblyResolver resolver = new DefaultAssemblyResolver();
            resolver.AddSearchDirectory(Path.GetDirectoryName(asmDirectory));
            resolver.AddSearchDirectory(Path.GetDirectoryName(thisDirectory));
            resolver.AddSearchDirectory(Path.GetDirectoryName(typeof(object).Assembly.Location));
            ReaderParameters readerParams = new ReaderParameters
            {
                AssemblyResolver = resolver,
                ReadWrite = false
            };

            AssemblyDefinition asm = AssemblyDefinition.ReadAssembly(asmDirectory, readerParams);
            ModuleDefinition module = asm.MainModule;

            // apply patches

            Watermark.ApplyPatch(module);

            TranslatorFix.ApplyPatch(module);

            // write the modified assembly to a file in the mod's folder
            // this will be read by the game in place of the original file
            // due to unity doorstop's dll_search_path_override
            asm.Write(Path.Combine(Path.GetDirectoryName(thisDirectory), "Assembly-CSharp.dll"));

            // cleanup
            asm.Dispose();
        }
    }
}
