using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace HotBot.Core.Commands.Tests
{
	[TestClass()]
	public class CommandRedirecterTests
	{
		[TestMethod()]
		public void Constructor_InvalidArguments()
		{
			TestUtils.AssertArgumentException(() => new CommandRedirecter(null), "bus");
		}

		[TestMethod()]
		public void Constructor_ValidArguments()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);

			Assert.AreEqual(mockBus.Object, redirecter.Bus, "Bus property incorrect");
		}

		[TestMethod()]
		public void AddListener_InvalidArguments()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			var commandListener = new Mock<CommandListener>();

			TestUtils.AssertArgumentException(() => redirecter.AddListener(null, commandListener.Object));
			TestUtils.AssertArgumentException(() => redirecter.AddListener("", commandListener.Object));
			TestUtils.AssertArgumentException(() => redirecter.AddListener("command", null));
		}

		[TestMethod()]
		public void AddListener_ValidArguments()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			var commandListener = new Mock<CommandListener>();
			redirecter.AddListener("command", commandListener.Object);
		}

		[TestMethod()]
		public void RemoveListener_InvalidArguments()
		{
			var mockBus = new Mock<MessageBus>();
			var commandListener = new Mock<CommandListener>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			TestUtils.AssertArgumentException(() => redirecter.RemoveListener(null, commandListener.Object));
			TestUtils.AssertArgumentException(() => redirecter.RemoveListener("", commandListener.Object));
			TestUtils.AssertArgumentException(() => redirecter.RemoveListener("command", null));
		}

		[TestMethod()]
		public void RemoveListener_NotAdded()
		{
			var mockBus = new Mock<MessageBus>();
			var commandListener = new Mock<CommandListener>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			redirecter.RemoveListener("command", commandListener.Object);
		}

		[TestMethod()]
		public void RemoveListener_RemoveTwice()
		{
			var mockBus = new Mock<MessageBus>();
			var commandListener = new Mock<CommandListener>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			redirecter.AddListener("command", commandListener.Object);
			redirecter.RemoveListener("command", commandListener.Object);
			redirecter.RemoveListener("command", commandListener.Object);
		}

		[TestMethod()]
		public void ExecuteCommand_InvalidArguments()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			TestUtils.AssertArgumentException(() => redirecter.ExecuteCommand(null));
		}

		[TestMethod()]
		public void ExecuteCommand_NoListener()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			var user = new Mock<User>();
			var channel = new Mock<Channel>();
			var commandInfo = new CommandEvent(channel.Object, user.Object, "command", "args");
			redirecter.ExecuteCommand(commandInfo);
		}

		[TestMethod()]
		public void ExecuteCommand_RunOnlyOnce()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			var user = new Mock<User>("user");
			var channel = new Mock<Channel>("channel");
			var commandInfo = new CommandEvent(channel.Object, user.Object, "command", "args");
			var listener = new Mock<CommandListener>();

			redirecter.AddListener("command", listener.Object);
			redirecter.ExecuteCommand(commandInfo);

			listener.Verify(l => l.OnCommand(commandInfo), Times.Once());
		}

		[TestMethod()]
		public void ExecuteCommand_NoRunAfterRemove()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			var user = new Mock<User>("user");
			var channel = new Mock<Channel>("channel");
			var commandInfo = new CommandEvent(channel.Object, user.Object, "command", "args");
			var listener = new Mock<CommandListener>();

			redirecter.AddListener("command", listener.Object);
			redirecter.RemoveListener("command", listener.Object);
			redirecter.ExecuteCommand(commandInfo);

			listener.Verify(l => l.OnCommand(commandInfo), Times.Never());
		}

		[TestMethod()]
		public void HandleCommand_InvalidArguments()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			TestUtils.AssertArgumentException(() => (redirecter as MessageHandler<CommandEvent>).HandleMessage(null));
		}

		[TestMethod()]
		public void HandleCommand_RunOnlyOnce()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			var user = new Mock<User>("user");
			var channel = new Mock<Channel>("channel");
			var commandInfo = new CommandEvent(channel.Object, user.Object, "command", "args");
			var listener = new Mock<CommandListener>();

			redirecter.AddListener("command", listener.Object);
			(redirecter as MessageHandler<CommandEvent>).HandleMessage(commandInfo);

			listener.Verify(l => l.OnCommand(commandInfo), Times.Once());
		}
	}
}