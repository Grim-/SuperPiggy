using System.Collections.Generic;
using Verse;

namespace EJ_Naruto
{
    public class GeneDef_RandomChakraNature : GeneDef
    {
        public List<ChakraChanceData> chakraTypes;

        public GeneDef_RandomChakraNature()
        {
            geneClass = typeof(Gene_RandomChakraNature);
        }
    }

}
