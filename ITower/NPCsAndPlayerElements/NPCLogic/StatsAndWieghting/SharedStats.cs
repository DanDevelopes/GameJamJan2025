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
            GD.Print("Testing allNPCStats Dictionary");
            allNPCStats.Add(npcName, stats);
            GD.Print("Tested allNPCStats Dictionary");
        }
        public static NPCStats getStats(string npcName) 
        {
            return allNPCStats[npcName];
        }
    }
}
