using System;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Addons.Interactive;
using Discord.Addons.CommandCache;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace EventHorizonRPG.Services
{
	public class CommandHandlingService
	{
		private readonly DiscordSocketClient _discord;
		private readonly CommandService _commands;
		private IServiceProvider _provider;
		private LiteDatabase _database;
		private readonly IConfiguration _config;
		private CommandCacheService _cache;

		public Dictionary<ulong, ulong> CommandCache { get; set; }
		public CommandHandlingService(IConfiguration config, IServiceProvider provider, DiscordSocketClient discord, CommandService commands, CommandCacheService cache, LiteDatabase database)
		{
			_discord = discord;
			_commands = commands;
			_provider = provider;
			_config = config;
			_database = database;
			_cache = cache;

			CommandCache = new Dictionary<ulong, ulong>();

			_discord.MessageReceived += MessageReceived;
			_discord.MessageUpdated += OnMessageUpdated;
			_discord.Ready += InitializeGuildsDB;
		}
		public async Task InitializeAsync(IServiceProvider provider)
		{
			_provider = provider;
			await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
			// Add additional initialization code here...
		}
		private Task InitializeGuildsDB()
		{
			throw new NotImplementedException();
		}
		#region Events
		public async Task OnMessageUpdated(Cacheable<IMessage, ulong> _OldMsg, SocketMessage NewMsg, ISocketMessageChannel Channel)
		{
			var OldMsg = await _OldMsg.DownloadAsync();
			if (OldMsg == null || NewMsg == null) return;
			if (OldMsg.Source != MessageSource.User || NewMsg.Source != MessageSource.User) return;
			await MessageReceived(NewMsg);
		}
		private async Task MessageReceived(SocketMessage rawMessage)
		{
			// Ignore system messages and messages from bots
			if (!(rawMessage is SocketUserMessage message)) return;
			if (message.Source != MessageSource.User) return;

			var context = new SocketCommandContext(_discord, message);

			int argPos = 0;
			if (!message.HasStringPrefix(_config["prefix"], ref argPos) && !message.HasMentionPrefix(_discord.CurrentUser, ref argPos)) return;

			var result = await _commands.ExecuteAsync(context, argPos, _provider);

			if (result.Error.HasValue && (result.Error.Value != CommandError.UnknownCommand))
			{
				Console.WriteLine(result.Error.Value + "\n" + result.ErrorReason);
			}

			if (result.Error.HasValue && result.Error.Value == CommandError.ObjectNotFound)
			{
				var msg = await context.Channel.SendMessageAsync("Sorry. " + result.ErrorReason);
				_cache.Add(context.Message.Id, msg.Id);
			}
		}
		#endregion
	}
}
