using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	public class ChannelNotificationRequest : ChatTransmitRequest
	{
		public const string NotificationCommand = @"/me";

		public ChannelNotificationRequest(Channel channel, string notificationText) : base(channel, notificationText)
		{

		}

		protected override string GenerateIrcCommand(string text)
		{
			return base.GenerateIrcCommand($"{NotificationCommand} {text}");
		}
	}
}
