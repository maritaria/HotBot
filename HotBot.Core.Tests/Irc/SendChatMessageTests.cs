using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using HotBot.Core;

namespace HotBot.Core.Irc.Tests
{
	[TestClass()]
	public class SendChatMessageTests
	{
		[TestMethod]
		public void SendChatMessage_Properties()
		{
			string channelName = "TestChannel";
			string chatMessage = "ChatMessage";
			Channel channel = new Channel(channelName);
			SendChatMessage command = new SendChatMessage(channel, chatMessage);

			Assert.AreEqual(channel, command.Channel);
			Assert.AreEqual(chatMessage, command.Text);
		}

		[TestMethod()]
		public void SendChatMessage_IrcCommand()
		{
			string channelName = "TestChannel";
			string chatMessage = "ChatMessage";
			Channel channel = new Channel(channelName);
			SendChatMessage command = new SendChatMessage(channel, chatMessage);

			Assert.AreEqual("PRIVMSG #TestChannel :ChatMessage", command.IrcCommand);
		}
	}
}