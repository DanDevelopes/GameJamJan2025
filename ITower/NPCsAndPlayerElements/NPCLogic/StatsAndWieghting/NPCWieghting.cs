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
            multipliers.rangeOfView = 0.50f;
            multipliers.isPlayer = true;
            npcMultipliers["BasicTrooper"] = multipliers;

            multipliers = new Multipliers();
            multipliers.health = 1f;
            multipliers.maxHealth = 1f;
            multipliers.speed = 0;
            multipliers.damage = 0.05f;
            multipliers.accuracy = 0.40f;
            multipliers.feildOfView = 0.05f;
            multipliers.sanity = 0;
            multipliers.moral = 0;
            multipliers.intelegent = 0.40f;
            multipliers.rangeOfView = 1f;
            multipliers.isPlayer = true;
            npcMultipliers["SmallTurret"] = multipliers;

            multipliers = new Multipliers();
            multipliers.health = 5f;
            multipliers.maxHealth = 1f;
            multipliers.speed = 0;
            multipliers.damage = 0f;
            multipliers.accuracy = 0f;
            multipliers.feildOfView = 0f;
            multipliers.sanity = 0;
            multipliers.moral = 0;
            multipliers.intelegent = 0f;
            multipliers.rangeOfView = 0f;
            multipliers.isPlayer = true;
            npcMultipliers["MainTower"] = multipliers;

            // Eyeborgs
            multipliers = new Multipliers();
            multipliers.health = 0.60f;
            multipliers.maxHealth = 0.60f;
            multipliers.speed = 0.10f;
            multipliers.damage = 0.20f;
            multipliers.accuracy = 0.60f;
            multipliers.feildOfView = 0.15f;
            multipliers.rangeOfView = 0.60f;
            multipliers.sanity = 0.70f;
            multipliers.moral = 0.50f;
            multipliers.intelegent = 0.40f;
            multipliers.isPlayer = false;
            npcMultipliers["EyeBorg"] = multipliers;

            // Slithem
            multipliers = new Multipliers();
            multipliers.health = 0.60f;
            multipliers.maxHealth = 0.60f;
            multipliers.speed = 0.20f;
            multipliers.damage = 0.20f;
            multipliers.accuracy = 0.60f;
            multipliers.feildOfView = 0.30f;
            multipliers.rangeOfView = 0.20f;
            multipliers.sanity = 0.70f;
            multipliers.moral = 0.50f;
            multipliers.intelegent = 0.01f;
            multipliers.isPlayer = false;
            npcMultipliers["Slithem"] = multipliers;

            // Eyeclopses
            multipliers = new Multipliers();
            multipliers.health = 0.60f;
            multipliers.maxHealth = 0.60f;
            multipliers.speed = 0.10f;
            multipliers.damage = 0.20f;
            multipliers.accuracy = 0.60f;
            multipliers.feildOfView = 0.20f;
            multipliers.sanity = 0.70f;
            multipliers.moral = 0.50f;
            multipliers.intelegent = 0.40f;
            multipliers.rangeOfView = 40f;
            multipliers.isPlayer = false;
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
            public float rangeOfView;
            public bool isPlayer;
        }
    }
}
