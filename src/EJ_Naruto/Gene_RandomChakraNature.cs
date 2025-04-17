using NarutoMod;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TaranMagicFramework;
using Verse;

namespace EJ_Naruto
{
    public class ChakraChanceData
    {
        public AbilityTreeDef abilityTreeDef;
        public float chance = 0.2f;
    }
    public class Gene_RandomChakraNature : Gene
    {

        GeneDef_RandomChakraNature Def => (GeneDef_RandomChakraNature)def;

        protected bool HasGranted = false;


        public override void PostAdd()
        {
            base.PostAdd();

            TryUnlockTree(this.pawn);
        }


        public void TryUnlockTree(Pawn pawn)
        {
            CompAbilities comp = pawn.GetComp<CompAbilities>();

            List<ChakraChanceData> availableChakraTypes = Def.chakraTypes
                .Where(x => !comp.AllUnlockedAbilityClasses
                    .SelectMany(y => y.UnlockedTrees)
                    .Contains(x.abilityTreeDef))
                .ToList();


            Log.Message($"availableChakraTypes {availableChakraTypes.Count}");
            ChakraChanceData selectedChakra = GenCollection.RandomElementByWeight(
                availableChakraTypes,
                x => x.chance);

            Log.Message($"selectedChakra {selectedChakra}");
            if (selectedChakra != null)
            {
                foreach (AbilityClass abilityClass in comp.AllUnlockedAbilityClasses)
                {
                    if (abilityClass.def.abilityTrees.Contains(selectedChakra.abilityTreeDef))
                    {
                        abilityClass.UnlockTree(selectedChakra.abilityTreeDef);
                        Log.Message($"Unlocking {abilityClass}");
                    }
                }
                HasGranted = true;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref HasGranted, "HasGranted");
        }
    }

}
