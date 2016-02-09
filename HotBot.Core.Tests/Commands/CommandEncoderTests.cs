using HotBot.Core.Irc;
using HotBot.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace HotBot.Core.Commands.Tests
{
	[TestClass()]
	public class CommandEncoderTests
	{
		[TestMethod()]
		public void CommandEncoder_Constructor()
		{
			var bus = new Mock<MessageBus>();
			var config = new Mock<CommandConfig>();
			//Invalid calls
			TestUtils.AssertArgumentException(() => new ChatCommandScanner(null, config.Object));
			TestUtils.AssertArgumentException(() => new ChatCommandScanner(bus.Object, null));
			//Valid calls
			var encoder = new ChatCommandScanner(bus.Object, config.Object);
			bus.Verify(m => m.Subscribe<ChatReceivedEvent>(encoder), Times.Once(), "Not subscribed to IrcMessageEnhanced");
			Assert.AreEqual(bus.Object, encoder.Bus, "Bus property not correct");
		}

		[TestMethod()]
		public void HandleMessage_SingleCharPrefix()
		{
			var mockBus = new Mock<MessageBus>();
			var config = new Mock<CommandConfig>();
			config.SetupGet(c => c.Prefixes).Returns(() => new string[] { "!", "#" });
			var encoder = new ChatCommandScanner(mockBus.Object, config.Object);

			TestUtils.AssertArgumentException(() => encoder.HandleMessage(null));

			var channel = new Mock<Channel>("TestChannel");
			var user = new Mock<User>("TestUser");
			var message = new ChatReceivedEvent(channel.Object, user.Object, "#command argument1 argument2");

			encoder.HandleMessage(message);

			mockBus.Verify(b => b.Publish(It.Is<CommandEvent>(info =>
				info.User == user.Object &&
				info.Channel == channel.Object &&
				info.CommandName == "command" &&
				info.ArgumentText == "argument1 argument2"
			)), Times.Once());
		}

		[TestMethod()]
		public void HandleMessage_MultiCharPrefix()
		{
			var bus = new Mock<MessageBus>();
			var config = new Mock<CommandConfig>();
			config.SetupGet(c => c.Prefixes).Returns(() => new string[] { "QQ", "#" });
			var encoder = new ChatCommandScanner(bus.Object, config.Object);
			var channel = new Mock<Channel>("TestChannel");
			var user = new Mock<User>("TestUser");
			var message = new ChatReceivedEvent(channel.Object, user.Object, "QQcommand argument1 argument2");

			encoder.HandleMessage(message);

			bus.Verify(b => b.Publish(It.Is<CommandEvent>(info =>
				info.User == user.Object &&
				info.Channel == channel.Object &&
				info.CommandName == "command" &&
				info.ArgumentText == "argument1 argument2"
			)), Times.Once());
		}
	}
}