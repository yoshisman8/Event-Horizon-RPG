using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace HorizonRPG.Game_Classes
{
	public class Item
	{
		public string Name { get; set; }
		public string Icon { get; set; }
		public Rarity Rarity { get; set; }
		public double Value { get; set; }
	}
	public class Equipment : Item
	{
		public BodySlot bodySlot { get; set; }
		public ScoreBlock StatChanges { get; set; } = new ScoreBlock();
		public SpecialAttributes[] SpecialAttributes { get; set; }
	}
	public class CraftingMaterial : Item
	{

	}
	public class SpecialAttributes
	{

	}
	public enum Rarity {Unknown = -1,Common, Uncommon, Rare, Legendary, Unique}
	public enum BodySlot {Body, Head, Pants, Boots, Accesory, OneHanded, TwoHanded }
}
