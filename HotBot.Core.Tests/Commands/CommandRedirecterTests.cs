using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using HotBot.Core;

namespace HotBot.Core.Commands.Tests
{
	[TestClass()]
	public class CommandRedirecterTests
	{
		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Null()
		{
			new CommandRedirecter(null);
		}

		[TestMethod()]
		public void Constructor_Valid()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);

			Assert.AreEqual(mockBus.Object, redirecter.Bus, "Bus property incorrect");
		}

		[TestMethod()]
		[ExpectedException(typeof(InvalidCommandNameException))]
		public void AddListener_Null_CommandName()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			var commandListener = new Mock<CommandListener>();
			redirecter.AddListener(null, commandListener.Object);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddListener_Null_Listener()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			redirecter.AddListener("command", null);
		}

		[TestMethod()]
		public void AddListener_Valid()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			var commandListener = new Mock<CommandListener>();
			redirecter.AddListener("command", commandListener.Object);
		}

		[TestMethod()]
		[ExpectedException(typeof(InvalidCommandNameException))]
		public void RemoveListener_Null_CommandName()
		{
			var mockBus = new Mock<MessageBus>();
			var commandListener = new Mock<CommandListener>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			redirecter.RemoveListener(null, commandListener.Object);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RemoveListener_Null_Listener()
		{
			var mockBus = new Mock<MessageBus>();
			var commandListener = new Mock<CommandListener>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			redirecter.RemoveListener("command", null);
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
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExecuteCommand_Null()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			redirecter.ExecuteCommand(null);
		}

		[TestMethod()]
		public void ExecuteCommand_NoListener()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			var user = new Mock<User>();
			var channel = new Mock<Channel>();
			var commandInfo = new CommandInfo(channel.Object, user.Object, "command", "args");
			redirecter.ExecuteCommand(commandInfo);
		}

		[TestMethod()]
		public void ExecuteCommand_RunOnce()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			var user = new Mock<User>("user");
			var channel = new Mock<Channel>("channel");
			var commandInfo = new CommandInfo(channel.Object, user.Object, "command", "args");
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
			var commandInfo = new CommandInfo(channel.Object, user.Object, "command", "args");
			var listener = new Mock<CommandListener>();

			redirecter.AddListener("command", listener.Object);
			redirecter.RemoveListener("command", listener.Object);
			redirecter.ExecuteCommand(commandInfo);

			listener.Verify(l => l.OnCommand(commandInfo), Times.Never());
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void HandleCommand_CommandInfo_Null()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			(redirecter as MessageHandler<CommandInfo>).HandleMessage(null);
		}

		[TestMethod()]
		public void HandleCommand_CommandInfo_RunOnce()
		{
			var mockBus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(mockBus.Object);
			var user = new Mock<User>("user");
			var channel = new Mock<Channel>("channel");
			var commandInfo = new CommandInfo(channel.Object, user.Object, "command", "args");
			var listener = new Mock<CommandListener>();

			redirecter.AddListener("command", listener.Object);
			(redirecter as MessageHandler<CommandInfo>).HandleMessage(commandInfo);

			listener.Verify(l => l.OnCommand(commandInfo), Times.Once());
		}
	}
}