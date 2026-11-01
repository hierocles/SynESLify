using System;
using System.Linq;
using System.Threading.Tasks;

using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace SynCOBJ
{
    internal class Program
    {
        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(GameRelease.SkyrimSE, "SynCOBJ.esp")
                .Run(args);
        }
        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            var overrides = state.PatchMod.EnumerateMajorRecords().Where(x=>x.FormKey.ModKey != state.PatchMod.ModKey).Count();
            var fidcheck = state.PatchMod.EnumerateMajorRecords()
                .Where(x => x.FormKey.ModKey == state.PatchMod.ModKey)
                .Select(x => x.FormKey.ID)
                .DefaultIfEmpty(0u)
                .Max() <= 0x000FFFFF;
            var totalLocal = state.PatchMod.EnumerateMajorRecords().Count(x => x.FormKey.ModKey == state.PatchMod.ModKey);
            var totalRecorded = state.PatchMod.EnumerateMajorRecords().Count();
            var newRecordCount = Math.max(totalRecorded - overrides, 0);
            if (state.PatchMod.ModHeader.Stats.NumRecords - overrides < 4096 && fidcheck)
            {
                Console.WriteLine($"ESLIfing patch stack with {overrides} overrides and {newRecordCount} new records");
                state.PatchMod.IsSmallMaster = true;
            }
    I think I see what your issue is.
You seem to have installed the "pines only" version of Happy Little Trees but checked my HLT patch in the installer. That patch is designed for the full version of HLT. The Pines Only version does not need a patch.    }
    }
}
