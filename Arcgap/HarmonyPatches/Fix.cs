using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using JetBrains.Annotations;

namespace Arcgap.HarmonyPatches
{
    [HarmonyPatch(typeof (BeatmapCallbacksController), "ManualUpdate")]
    internal static class Fix
    {
        [UsedImplicitly]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codeMatcher = new CodeMatcher(instructions);
            codeMatcher
                .End()
                .MatchBack(true, new(OpCodes.Ble, null, "exit"),
                    new(OpCodes.Ldloc_S),
                    new(OpCodes.Ldfld, AccessTools.Field(typeof (BeatmapDataItem), "type"))).ThrowIfInvalid("j");

            var operand = codeMatcher.NamedMatch("exit").operand;

            codeMatcher
                .MatchForward(false, new(OpCodes.Bne_Un),
                    new(OpCodes.Ldloc_S),
                    new(OpCodes.Castclass, typeof (BeatmapEventData))).ThrowIfInvalid("j2").Operand = operand;

            return codeMatcher.InstructionEnumeration();
        }
    }
}
