using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

namespace HorizonRPG.Game_Classes
{
	public class Entity
	{
		public string Name { get; set; }
		public Resource Health { get; set; } = new Resource() { MaxValue = 100, CurrentValue = 100 };
		public Resource Stamina { get; set; } = new Resource() { MaxValue = 100, CurrentValue = 100 };
		public ScoreBlock BaseAbilityScores { get; set; } = new ScoreBlock();
		public Inventory Inventory { get; set; }
		public List<Aliment> Aliments { get; set; }

		public void RecalcluateStats()
		{
			int str = BaseAbilityScores.Strength;
			int dex = BaseAbilityScores.Dexterity;
			int agi = BaseAbilityScores.Agility;
			int con = BaseAbilityScores.Constitution;
			int lck = BaseAbilityScores.Luck;
			int hp = Health.MaxValue;
			int stm = Stamina.MaxValue;

			if (Inventory != null)
			{

			}
		}
	}
	public class ScoreBlock
	{
		public int Strength { get; set; }
		public int Dexterity { get; set; }
		public int Agility { get; set; }
		public int Constitution { get; set; }
		public int Luck { get; set; }
	}
	public class Resource
	{
		public int MaxValue { get; set; }
		public int CurrentValue { get; set; }
		// Future Values for Future burnout mechanics
		public int SecondaryMaxValue { get; set; }
		public int SecondaryCurrentValue { get; set; }
	}

	public class Aliment
	{
		[BsonId]
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Duration { get; set; }
	}

	public class Inventory
	{
		public Equipment Helmet { get; set; }
		public Equipment ChestPiece { get; set; }
		public Equipment LegPiece { get; set; }
		public Equipment Boots { get; set; }
		public Equipment Accessory1 { get; set; }
		public Equipment Accessory2 { get; set; }
		public Equipment MainHand { get; set; }
		public Equipment OffHand { get; set; }

		public double Money { get; set; }
		public Dictionary<int, int> Pouch { get; set; }
	}
}
