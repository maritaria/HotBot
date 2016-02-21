using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicTwitchApi : TwitchApi
	{
		private WebClient _client = new WebClient();

		public IEnumerable<ConnectionInfo> GetChatServers(string channelName)
		{
			string responseJson = _client.DownloadString($"https://api.twitch.tv/api/channels/{channelName}/chat_properties");
			JObject response = JObject.Parse(responseJson);
			foreach (JToken token in response.SelectToken("chat_servers"))
			{
				yield return new ConnectionInfo(token.ToString());
			}
		}

		public IEnumerable<ConnectionInfo> GetWhisperServers()
		{
			string responseJson = _client.DownloadString($"http://tmi.twitch.tv/servers?cluster=group");
			JObject response = JObject.Parse(responseJson);
			foreach (JToken token in response.SelectToken("servers"))
			{
				yield return new ConnectionInfo(token.ToString());
			}
		}
	}
}