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
			setUpgrades = new Dictionary<Upgrades, int>();
			setUpgrades[Upgrades.EmpathyInhibitor] = 1;
            setUpgrades[Upgrades.ArmourImprovement] = 0;
            setUpgrades[Upgrades.Cybernetics] = 0;
            setUpgrades[Upgrades.Dopamineregulator] = 0;
            setUpgrades[Upgrades.AdvancedEnegineering] = 0;
        }
		public enum Upgrades
		{
			EmpathyInhibitor,
            Cybernetics,
			Dopamineregulator,
			ArmourImprovement,
			AdvancedEnegineering,
		}
		private static Dictionary<Upgrades, int> setUpgrades;
		private static int totalUpgradePoints;
		public static void SubtractPoints(int amount) 
		{
			totalUpgradePoints -= amount;
		}
		public static void UpgradeStat(Upgrades upgrade, int amount) 
		{ 
			setUpgrades[upgrade] = amount;
		}
		public static Dictionary<Upgrades, int> GetStatModifiers()
		{
			return setUpgrades;
		}
		public static void AddPoints(int points) 
		{
			totalUpgradePoints += points;
		}
		public static int GetTotalPoints()
		{
			return totalUpgradePoints;
		}
	}
}
