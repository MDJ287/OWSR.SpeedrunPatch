
using Mono.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using HarmonyLib;
using System.Reflection.Emit;
using System.IO;

namespace OWSR.SpeedrunPatch.src
{
    [HarmonyPatch(typeof(NomaiTranslatorProp), "DisplayTextNode")]
    internal class TranslatorFix
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            File.WriteAllText("_trans_start.txt", $"{instructions.Count()}");

            return instructions;

            /*

            // first instance of stfld _numTranslatedWords is the end of the if statement
            CodeInstruction? lastIfBodyInstruction = null;
            int lastIfBodyInstructionIndex = 0;
            for (int i = 0; i < instructions.Count(); i++)
            {
                CodeInstruction instruction = instructions.ElementAt(i);
                if (
                    instruction.opcode == OpCodes.Stfld &&
                    ((FieldInfo)instruction.operand).Name.Equals("_numTranslatedWords")
                )
                {
                    lastIfBodyInstruction = instruction;
                    lastIfBodyInstructionIndex = i;
                    break;
                }
            }

            if (lastIfBodyInstruction == null)
            {
                File.WriteAllText("SPEEDRUNPATCH-ERRORS.txt", "stfld _numTranslatedWords not found!");
                throw new Exception("speedrun patch failed");
            };

            // instruction before previous instance of ldfld _translationTimeElapsed is the start of the if condition
            CodeInstruction? firstIfConditionInstruction = null;
            Label firstIfConditionLabel = generator.DefineLabel();
            for (int i = lastIfBodyInstructionIndex; i >= 0; i--)
            {
                CodeInstruction instruction = instructions.ElementAt(i);
                if (
                    instruction.opcode == OpCodes.Ldfld &&
                    ((FieldInfo)instruction.operand).Name.Equals("_translationTimeElapsed")
                )
                {
                    firstIfConditionInstruction = instructions.ElementAt(i-1);
                    break;
                }
            }

            if (firstIfConditionInstruction == null)
            {
                File.WriteAllText("SPEEDRUNPATCH-ERRORS.txt", "ldfld _translationTimeElapsed not found!");
                throw new Exception("speedrun patch failed");
            }

            firstIfConditionInstruction.labels.Add(firstIfConditionLabel);

            File.WriteAllText("hi.txt", "found the 2 things");

            // branch to the start of the "if" condition at the end of its body (turn it into a while loop)
            for (int i=0; i<instructions.Count(); i++)
            {
                if (instructions.ElementAt(i) == lastIfBodyInstruction)
                {
                    yield return instructions.ElementAt(i);
                    yield return new CodeInstruction(OpCodes.Br_S, firstIfConditionLabel);
                }
                yield return instructions.ElementAt(i);
            }
            /**/
        }
    }
}
