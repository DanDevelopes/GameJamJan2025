using System;
using Godot;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
namespace ITower.NPCsAndPlayerElements.NPCLogic
{
    public static class CombatElement
    {
        static Random rnd = new Random();
        public static void attackTarget(string npcName, int damage, int accuracy)
        {
            if (rnd.Next(100) < accuracy)
            {
                
                SharedStats.getStats(npcName).health -= damage;
            }
        }
    }
}
