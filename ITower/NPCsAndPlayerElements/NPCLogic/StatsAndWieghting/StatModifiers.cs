using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting
{
	public static class StatModifiers
	{
		static StatModifiers()
		{
			setUpgrades = new Dictionary<upgrades, int>();
		}
		public enum upgrades
		{
			EmpathyInhibitor,
			SkillRegulator,
			Dopamineregulator,
			ArmourImprovement,
			AIEnegineering,
			Cybernetics
		}
		private static Dictionary<upgrades, int> setUpgrades;
		private static int totalUpgradePoints;
		public static void UpgradeStat(upgrades upgrade, int amount) 
		{ 
			setUpgrades[upgrade] = amount;
		}
		public static Dictionary<upgrades, int> GetStatModifiers()
		{
			return setUpgrades;
		}
	}
}
