using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using HotBot.Core;
using HotBot.Core.Irc;

namespace HotBot.Core.Commands.Tests
{
	[TestClass()]
	public class CommandEncoderTests
	{
		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CommandEncoder_Constructor_Null()
		{
			var encoder = new CommandEncoder(null);
		}

		[TestMethod()]
		public void CommandEncoder_Constructor_Valid()
		{
			var mockBus = new Mock<MessageBus>();
			var encoder = new CommandEncoder(mockBus.Object) { Prefixes = { "!", "#" } };
			mockBus.Verify(m => m.Subscribe<IrcMessageEnhanced>(encoder), Times.Once(), "Not subscribed to IrcMessageEnhanced");
			Assert.AreEqual(mockBus.Object, encoder.Bus, "Bus property not correct");
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void HandleMessage_Null()
		{
			var mockBus = new Mock<MessageBus>();
			var encoder = new CommandEncoder(mockBus.Object) { Prefixes = { "!", "#" } };
			encoder.HandleMessage(null);
		}

		[TestMethod()]
		public void HandleMessage_Valid()
		{
			var mockBus = new Mock<MessageBus>();
			var encoder = new CommandEncoder(mockBus.Object) { Prefixes = { "!", "#" } };
			var channel = new Mock<Channel>("TestChannel");
			var user = new Mock<User>("TestUser");
			var message = new IrcMessageEnhanced(channel.Object, user.Object, "#command argument1 argument2");

			encoder.HandleMessage(message);

			mockBus.Verify(b => b.Publish(It.Is<CommandInfo>(info =>
				info.User == user.Object &&
				info.Channel == channel.Object &&
				info.CommandName == "command" &&
				info.ArgumentText == "argument1 argument2"
			)), Times.Once());
		}

		[TestMethod()]
		public void HandleMessage_Valid_MultiCharPrefix()
		{
			var mockBus = new Mock<MessageBus>();
			var encoder = new CommandEncoder(mockBus.Object) { Prefixes = { "QQ", "#" } };
			var channel = new Mock<Channel>("TestChannel");
			var user = new Mock<User>("TestUser");
			var message = new IrcMessageEnhanced(channel.Object, user.Object, "QQcommand argument1 argument2");

			encoder.HandleMessage(message);

			mockBus.Verify(b => b.Publish(It.Is<CommandInfo>(info =>
				info.User == user.Object &&
				info.Channel == channel.Object &&
				info.CommandName == "command" &&
				info.ArgumentText == "argument1 argument2"
			)), Times.Once());
		}
	}
}