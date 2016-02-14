using HotBot.Core.Util;
using System;
using System.Collections;
using System.Linq;

namespace HotBot.Core.Irc.Twitch
{
	public interface TwitchIrcClient : IrcClient
	{
		TwitchApi ApiProvider { get; set; }
		
		void SetDisplayColor(Channel channel, TwitchColor color);

		void SayThirdPerson(Channel channel, string message);

		void WhisperUser(User target, string message);

		WhisperConnection GetWhisperConnection(Channel channel);

	}
}