using System;
using System.Linq;
using System.Collections.Generic;
using LiteDB;
using HorizonRPG.Game_Classes;

namespace HorizonRPG.System_Classes
{
	public class GuildSettings
	{
		[BsonId]
		public ulong GuildId { get; set; }
		
		public Dictionary<ulong,ZoneType> Zones { get; set; }
	}
	
}
