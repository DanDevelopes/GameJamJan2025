using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting
{
    public static class SharedStats
    {
        static SharedStats() 
        {
            allNPCStats = new Dictionary<string, NPCStats>();
        }
        static Dictionary<string, NPCStats> allNPCStats;
        public static void addStats(string npcName, NPCStats stats)
        {
            allNPCStats.Add(npcName, stats);
        }
        public static NPCStats getStats(string npcName) 
        {
            return allNPCStats[npcName];
        }

        internal static void ClearStats()
        {
            allNPCStats.Clear();    
        }
    }
}
