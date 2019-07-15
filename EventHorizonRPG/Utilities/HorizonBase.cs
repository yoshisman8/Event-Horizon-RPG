using System;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.Commands;
using Discord.Addons.Interactive;
using Discord.Addon.InteractiveMenus;
using EventHorizonRPG.Services;
using LiteDB;

namespace EventHorizonRPG.Utilities
{
	public class HorizonBase <T> : InteractiveBase<T>
		where T : SocketCommandContext
	{
		/// <summary>
		/// The Bot's database.
		/// </summary>
		public LiteDatabase Database { get; set; }
		/// <summary>
		/// The Command Hanlder Service, contains the Command Cache.
		/// </summary>
		public CommandHandlingService Command { get; set; }
		/// <summary>
		/// The Menu handler Service
		/// </summary>
		public MenuService MenuService { get; set; }


		/// <summary>
		/// Sends a message to the same channel that evoked this command.
		/// If the command was edited, it will edit the reply to that command instead.
		/// </summary>
		/// <param name="Contents">The contents of the message.</param>
		/// <param name="Embed">The embed of the message, if any.</param>
		/// <param name="IsTTS">Is this message TextToSpeech?</param>
		/// <returns>The RestUserMessage sent or edited. </returns>
		public async Task<RestUserMessage> ReplyAsync(string Contents,Embed Embed = null,bool IsTTS = false)
		{
			if (Command.CommandCache.TryGetValue(Context.Message.Id, out ulong Id))
			{
				RestUserMessage msg = await Context.Channel.GetMessageAsync(Id) as RestUserMessage;
				if (msg!=null)
				{
					await msg.ModifyAsync(x => x.Content = Contents);
					await msg.ModifyAsync(x => x.Embed = Embed);
					return msg;
				}
				else
				{
					msg = await Context.Channel.SendMessageAsync(Contents, IsTTS, Embed);
					Command.CommandCache.TryAdd(Context.Message.Id, msg.Id);
					return msg;
				}
			}
			else
			{
				var msg = await Context.Channel.SendMessageAsync(Contents, IsTTS, Embed);
				Command.CommandCache.TryAdd(Context.Message.Id, msg.Id);
				return msg;
			}
		}
	}
}
