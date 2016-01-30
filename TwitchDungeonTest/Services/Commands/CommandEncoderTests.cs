using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using TwitchDungeon.Services.Irc;
using TwitchDungeon.Services.Messages;

namespace TwitchDungeon.Services.Commands.Tests
{
	[TestClass()]
	public class CommandEncoderTests
	{
		[TestMethod()]
		public void CommandEncoder_Properties()
		{
			var mockBus = new Mock<MessageBus>();
			var encoder = new CommandEncoder(mockBus.Object);
			Assert.AreEqual(mockBus.Object, encoder.Bus);
		}

		[TestMethod()]
		public void CommandEncoder_Constructor_Valid()
		{
			var mockBus = new Mock<MessageBus>();
			var encoder = new CommandEncoder(mockBus.Object);
		}

		[TestMethod()]
		public void CommandEncoder_Constructor_Subscribed()
		{
			var mockBus = new Mock<MessageBus>();
			var encoder = new CommandEncoder(mockBus.Object);
			mockBus.Verify(
				m => m.Subscribe(
					It.Is<MessageHandler<IrcMessageEnhanced>>(v => v == encoder)
				),
				Times.Once(),
				"Not subscribed to IrcMessageEnhanced"
			);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CommandEncoder_Constructor_Null()
		{
			var encoder = new CommandEncoder(null);
		}
	}
}