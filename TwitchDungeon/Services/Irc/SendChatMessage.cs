using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchDungeon.Services.DataStorage;

namespace TwitchDungeon.Services.Irc
{
	public class SendChatMessage
	{
		public string Text { get; }
		public Channel Channel { get; }
	}
}
