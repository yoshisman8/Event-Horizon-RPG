using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using System.IO;
using Discord.Commands;
using Discord.WebSocket;
using LiteDB;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DiscordBot.Services
{
    public class CommandHandlingService
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private IConfiguration _config;
        private IServiceProvider _provider;
        private LiteDatabase _database;

        public CommandHandlingService(IServiceProvider provider, DiscordSocketClient discord,IConfiguration config, CommandService commands, LiteDatabase database)
        {
            _discord = discord;
            _commands = commands;
            _provider = provider;
            _database = database;
            _config = config;

            _discord.MessageReceived += MessageReceived;
        }

        public async Task InitializeAsync(IServiceProvider provider)
        {
            _provider = provider;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
            // Add additional initialization code here...
        }

        private async Task MessageReceived(SocketMessage rawMessage)
        {
            // Ignore system messages and messages from bots
            if (!(rawMessage is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;
            var msg = rawMessage as SocketUserMessage;
            int argPos = 0;
            var context = new SocketCommandContext(_discord, message);

            if (msg.HasStringPrefix(_config["prefix"], ref argPos) || msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _provider);     // Execute the command

            }
        }

    }
}
