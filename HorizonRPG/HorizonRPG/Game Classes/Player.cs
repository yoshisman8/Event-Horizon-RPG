using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using Discord;
using HorizonRPG.System_Classes;

namespace HorizonRPG.Game_Classes
{
    public class Player
    {
        [BsonId]
        public ulong Id { get; set; }
		[BsonRef("Guilds")]
		public GuildSettings Guild { get; set; }
		
    }
}
