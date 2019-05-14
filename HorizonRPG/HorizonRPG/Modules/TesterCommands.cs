using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using System.Threading.Tasks;

namespace HorizonRPG.Modules
{

    public class TesterCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Ping")]
        public async Task ping()
        {
            await ReplyAsync(Context.User.Mention + " PONG!");
        }
        [Command("About")]
        public async Task About()
        {
            var Vyk = Context.Client.GetUser(165212654388903936);

            var builder = new EmbedBuilder()
                .WithAuthor(Context.Client.CurrentUser)
                .WithTimestamp(DateTime.Now)
				.WithColor(Color.DarkBlue)
                .WithThumbnailUrl(Context.Client.CurrentUser.GetAvatarUrl())
                .WithDescription("Project Event Horizon RPG\nCoded, Designed and Hosted by: " + Vyk.Mention+ ".");
            await ReplyAsync("", embed: builder.Build());
        }
    }
}
