using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting
{
    public static class NPCWieghting
    {
        public static Dictionary<string, Multipliers> npcMultipliers;
        static NPCWieghting()
        {
            npcMultipliers = new Dictionary<string, Multipliers>();
            // Basic troops
            Multipliers multipliers = new Multipliers();
            multipliers.health = 0.60f;
            multipliers.maxHealth = 0.60f;
            multipliers.speed = 0.10f;
            multipliers.damage = 0.15f;
            multipliers.accuracy = 0.50f;
            multipliers.feildOfView = 0.30f;
            multipliers.sanity = 0.70f;
            multipliers.moral = 0.50f;
            multipliers.intelegent = 0.40f;
            npcMultipliers["BasicTrooper"] = multipliers;

            multipliers = new Multipliers();
            multipliers.health = 0.60f;
            multipliers.maxHealth = 1f;
            multipliers.speed = 0;
            multipliers.damage = 0.05f;
            multipliers.accuracy = 0.40f;
            multipliers.feildOfView = 0.05f;
            multipliers.sanity = 0;
            multipliers.moral = 1f;
            multipliers.intelegent = 0.40f;
            npcMultipliers["SmallTurret"] = multipliers;

            // Eyeborgs
            multipliers = new Multipliers();
            multipliers.health = 0.60f;
            multipliers.maxHealth = 0.60f;
            multipliers.speed = 0.50f;
            multipliers.damage = 0.20f;
            multipliers.accuracy = 0.60f;
            multipliers.feildOfView = 0.60f;
            multipliers.sanity = 0.70f;
            multipliers.moral = 0.50f;
            multipliers.intelegent = 0.40f;
            npcMultipliers["Eyeborg"] = multipliers;

            // Stormers
            multipliers = new Multipliers();
            multipliers.health = 0.60f;
            multipliers.maxHealth = 0.60f;
            multipliers.speed = 0.70f;
            multipliers.damage = 0.20f;
            multipliers.accuracy = 0.60f;
            multipliers.feildOfView = 0.60f;
            multipliers.sanity = 0.70f;
            multipliers.moral = 0.50f;
            multipliers.intelegent = 0.40f;
            npcMultipliers["Stormer"] = multipliers;

            // Eyeclopses
            multipliers = new Multipliers();
            multipliers.health = 0.60f;
            multipliers.maxHealth = 0.60f;
            multipliers.speed = 0.50f;
            multipliers.damage = 0.20f;
            multipliers.accuracy = 0.60f;
            multipliers.feildOfView = 0.60f;
            multipliers.sanity = 0.70f;
            multipliers.moral = 0.50f;
            multipliers.intelegent = 0.40f;
            npcMultipliers["Eyeclopse"] = multipliers;

        }
        public class Multipliers
        {
            public float health;
            public float maxHealth;
            public float speed;
            public float damage;
            public float accuracy;
            public float feildOfView;
            public float sanity;
            public float moral;
            public float intelegent;
        }
    }
}
