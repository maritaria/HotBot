using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using HotBot.Core;

namespace HotBot.Core.Irc.Tests
{
	[TestClass()]
	public class ChatTransmitEventTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Channel_Null()
		{
			string chatMessage = "ChatMessage";
			ChatTransmitRequest command = new ChatTransmitRequest(null, chatMessage);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Message_Null()
		{
			string channelName = "TestChannel";
			Channel channel = new Channel(channelName);
			ChatTransmitRequest command = new ChatTransmitRequest(channel, null);
		}

		[TestMethod]
		public void Constructor_Valid()
		{
			string channelName = "TestChannel";
			string chatMessage = "ChatMessage";
			Channel channel = new Channel(channelName);
			ChatTransmitRequest command = new ChatTransmitRequest(channel, chatMessage);

			Assert.AreEqual(channel, command.Channel);
			Assert.AreEqual(chatMessage, command.Text);
			Assert.AreEqual("PRIVMSG #TestChannel :ChatMessage", command.IrcCommand);
		}
	}
}