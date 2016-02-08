using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using HotBot.Core;
using HotBot.Testing;

namespace HotBot.Core.Irc.Tests
{
	[TestClass()]
	public class ChatTransmitEventTests
	{
		[TestMethod]
		public void ChatTransmitEvent_Constructor()
		{
			string channelName = "TestChannel";
			string chatMessage = "ChatMessage";
			Channel channel = new Channel(channelName);
			ChatTransmitRequest command = new ChatTransmitRequest(channel, chatMessage);

			TestUtils.AssertArgumentException(() => new ChatTransmitRequest(null, chatMessage));
			TestUtils.AssertArgumentException(() => new ChatTransmitRequest(channel, null));
			
			Assert.AreEqual(channel, command.Channel);
			Assert.AreEqual(chatMessage, command.Text);
			Assert.AreEqual("PRIVMSG #TestChannel :ChatMessage", command.IrcCommand);
		}
	}
}