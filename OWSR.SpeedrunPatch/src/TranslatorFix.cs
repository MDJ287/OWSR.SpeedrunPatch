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
    internal class TranslatorFix
    {
        public static void ApplyPatch(ModuleDefinition module)
        {
            TypeDefinition translatorType = module.GetType("NomaiTranslatorProp");
            MethodDefinition displayNodeMethod = translatorType.Methods.First(method => method.Name == "DisplayTextNode");

            ILProcessor ilProcessor = displayNodeMethod.Body.GetILProcessor();
            Collection<Instruction> instructions = ilProcessor.Body.Instructions;

            // first instance of stfld _numTranslatedWords is the end of the if statement
            Instruction? lastIfBodyInstruction = null;
            int lastIfBodyInstructionIndex = 0;
            for (int i = 0; i < instructions.Count; i++)
            {
                Instruction instruction = instructions[i];
                if (
                    instruction.OpCode == OpCodes.Stfld &&
                    ((FieldDefinition)instruction.Operand).FullName.Contains("NomaiTranslatorProp::_numTranslatedWords")
                )
                {
                    lastIfBodyInstruction = instruction;
                    lastIfBodyInstructionIndex = i;
                    break;
                }
            }

            if (lastIfBodyInstruction == null) return;

            // instruction before previous instance of ldfld _translationTimeElapsed is the start of the if condition
            Instruction? firstIfConditionInstruction = null;
            for (int i = lastIfBodyInstructionIndex; i >= 0; i--)
            {
                Instruction instruction = instructions[i];
                if (
                    instruction.OpCode == OpCodes.Ldfld &&
                    ((FieldDefinition)instruction.Operand).FullName.Contains("NomaiTranslatorProp::_translationTimeElapsed")
                )
                {
                    firstIfConditionInstruction = instruction.Previous;
                    break;
                }
            }

            if (firstIfConditionInstruction == null) return;

            // branch to the start of the "if" condition at the end of its body (turn it into a while loop)
            ilProcessor.InsertAfter(lastIfBodyInstruction, ilProcessor.Create(OpCodes.Br_S, firstIfConditionInstruction));
        }
    }
}
