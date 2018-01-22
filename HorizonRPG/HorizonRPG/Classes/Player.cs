using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using Discord;

namespace HorizonRPG.Classes
{
    public class Player
    {
        [BsonId]
        public int ID { get; set; }
        public ulong DiscordID { get; set; }
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; } 
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int BaseAgility { get; set; }
        public PlayerVariables PlayerVariables { get; set; }
        public Inventory Inventory { get; set; }
        public List<Aliment> Aliments { get; set; }
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
        public List<Item> Items { get; set; }
        public List<Resource> Resources { get; set; }
    }

    public class PlayerVariables
    {
        public bool IsSafe { get; set; } = true;
        public bool IsCombat { get; set; } = false;
        public bool IsAdventure { get; set; } = true;
    }
}
