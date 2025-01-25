using System;
using System.Collections.Generic;
using Godot;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
namespace ITower.NPCsAndPlayerElements.NPCLogic
{
    public static class OtherElement
    {
        static Random rnd = new Random();
        public static void attackTarget(string npcName, int damage, int accuracy)
        {
            if (rnd.Next(100) < accuracy)
            {
                
                SharedStats.getStats(npcName).health -= damage;
            }
        }
        static Dictionary<string, Godot.Vector2> positions = new Dictionary<string, Vector2>();
        public static List<string> npcNames = new List<string>();
        public static Dictionary<string, Godot.Vector2> GetPosition(string npcName) 
        {
            return positions;
        }
        public static void AddPosition(string npcName, Godot.Vector2 position)
        {
            if (positions.ContainsKey(npcName))
                positions[npcName] = position;
            else
                positions.Add(npcName, position);
        }
    }
}
