using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using HorizonRPG.Game_Classes;
using HorizonRPG.System_Classes;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;


namespace HorizonRPG.Utilities
{
	public class Exclude : Attribute
	{
	}
	public class RequireSpecificZone : PreconditionAttribute
	{
		private readonly ZoneType[] _Zones;

		public RequireSpecificZone(ZoneType zone) => _Zones = new ZoneType[1] { zone };
		public RequireSpecificZone(ZoneType[] zones) => _Zones = zones;

		public async override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
		{
			var database = services.GetRequiredService<LiteDatabase>();
			var guildsettings = database.GetCollection<GuildSettings>("Guilds");
			var guild = guildsettings.FindOne(x => x.GuildId == context.Guild.Id);
			
			if(guild.Zones.TryGetValue(context.Channel.Id,out ZoneType Zone))
			{
				if (_Zones.Contains(Zone)) return PreconditionResult.FromSuccess();
				else return PreconditionResult.FromError("This command cannot be used in this type of Zone!");
			}

			return PreconditionResult.FromError("This channel isn't registered on the Horizon systems. Have your server administrators use `.RegisterChannel` if you think this channel should be used.");
		}
	}
}
