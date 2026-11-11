using Mono.Cecil.Cil;
using Mono.Cecil;
using Mono.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace OWSR.SpeedrunPatch.src
{
    internal class Watermark
    {
        private static readonly string patchVersion = "0.1.2";

        public static void ApplyPatch(ModuleDefinition module)
        {
            string patchText = $"Speedrun Patch v{patchVersion}: TranslatorFix";

            TypeDefinition titleScreenType = module.GetType("TitleScreenManager");
            MethodDefinition fadeInTitleMethod = titleScreenType.Methods.First(method => method.Name == "FadeInTitleLogo");

            ILProcessor titleScreenIl = fadeInTitleMethod.Body.GetILProcessor();
            Collection<Instruction> titleScreenInstructions = titleScreenIl.Body.Instructions;

            // before first ldfld _gameVersionTextDisplay is when we want to update the string
            Instruction? loadVersionDisplayInstruction = null;
            for (int i = 0; i < titleScreenInstructions.Count; i++)
            {
                Instruction instruction = titleScreenInstructions[i];
                if (
                    instruction.OpCode == OpCodes.Ldfld &&
                    ((FieldDefinition)instruction.Operand).FullName.Contains("TitleScreenManager::_gameVersionTextDisplay")
                )
                {
                    loadVersionDisplayInstruction = instruction.Previous;
                    break;
                }
            }
            if (loadVersionDisplayInstruction == null) return;

            MethodReference concatReference = module.ImportReference(typeof(string).GetMethod(
                "Concat",
                BindingFlags.Public | BindingFlags.Static,
                null,
                new[] { typeof(string), typeof(string) },
                null
            ));

            // append text to ow version number
            Instruction[] instructionsToAdd = new Instruction[] {
                // text += (string)
                titleScreenIl.Create(OpCodes.Ldloc_0),
                titleScreenIl.Create(OpCodes.Ldstr, $"\n{patchText}"),
                titleScreenIl.Create(OpCodes.Call, concatReference),
                titleScreenIl.Create(OpCodes.Stloc_0),
                // re-add the instruction we removed
                // (we can't re-add the original because old branch references will point to it)
                titleScreenIl.Create(OpCodes.Ldarg_0)
            };

            titleScreenIl.Replace(loadVersionDisplayInstruction, instructionsToAdd[0]);

            for (int i = 1; i < instructionsToAdd.Length; i++)
            {
                titleScreenIl.InsertAfter(instructionsToAdd[i - 1], instructionsToAdd[i]);
            }
        }
    }
}
