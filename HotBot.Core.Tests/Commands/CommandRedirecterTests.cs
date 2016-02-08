using HotBot.Testing;
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
		public void CommandRedirecter_Constructor()
		{
			TestUtils.AssertArgumentException(() => new CommandRedirecter(null), "bus");

			var bus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(bus.Object);

			Assert.AreEqual(bus.Object, redirecter.Bus, "Bus property incorrect");
		}
		
		[TestMethod()]
		public void AddListener()
		{
			var bus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(bus.Object);
			var commandListener = new Mock<CommandListener>();

			TestUtils.AssertArgumentException(() => redirecter.AddListener(null, commandListener.Object));
			TestUtils.AssertArgumentException(() => redirecter.AddListener("", commandListener.Object));
			TestUtils.AssertArgumentException(() => redirecter.AddListener("command", null));
			
			redirecter.AddListener("command", commandListener.Object);
		}

		[TestMethod()]
		public void RemoveListener()
		{
			var bus = new Mock<MessageBus>();
			var commandListener = new Mock<CommandListener>();
			var redirecter = new CommandRedirecter(bus.Object);
			TestUtils.AssertArgumentException(() => redirecter.RemoveListener(null, commandListener.Object));
			TestUtils.AssertArgumentException(() => redirecter.RemoveListener("", commandListener.Object));
			TestUtils.AssertArgumentException(() => redirecter.RemoveListener("command", null));
		}

		[TestMethod()]
		public void RemoveListener_NotAdded()
		{
			var bus = new Mock<MessageBus>();
			var commandListener = new Mock<CommandListener>();
			var redirecter = new CommandRedirecter(bus.Object);
			//Should not throw exception when removing a listener that hasnt been added
			redirecter.RemoveListener("command", commandListener.Object);
		}

		[TestMethod()]
		public void RemoveListener_RemoveTwice()
		{
			var bus = new Mock<MessageBus>();
			var commandListener = new Mock<CommandListener>();
			var redirecter = new CommandRedirecter(bus.Object);
			redirecter.AddListener("command", commandListener.Object);
			redirecter.RemoveListener("command", commandListener.Object);
			redirecter.RemoveListener("command", commandListener.Object);
		}

		[TestMethod()]
		public void ExecuteCommand()
		{
			var bus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(bus.Object);
			TestUtils.AssertArgumentException(() => redirecter.ExecuteCommand(null));
		}

		[TestMethod()]
		public void ExecuteCommand_NoListener()
		{
			var bus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(bus.Object);
			var user = new Mock<User>();
			var channel = new Mock<Channel>("test");
			var commandInfo = new CommandEvent(channel.Object, user.Object, "command", "args");
			redirecter.ExecuteCommand(commandInfo);
		}

		[TestMethod()]
		public void ExecuteCommand_RunOnlyOnce()
		{
			var bus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(bus.Object);
			var user = new Mock<User>("user");
			var channel = new Mock<Channel>("test");
			var commandInfo = new CommandEvent(channel.Object, user.Object, "command", "args");
			var listener = new Mock<CommandListener>();

			redirecter.AddListener("command", listener.Object);
			redirecter.ExecuteCommand(commandInfo);

			listener.Verify(l => l.OnCommand(commandInfo), Times.Once());
		}

		[TestMethod()]
		public void ExecuteCommand_NoRunAfterRemove()
		{
			var bus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(bus.Object);
			var user = new Mock<User>("user");
			var channel = new Mock<Channel>("test");
			var commandInfo = new CommandEvent(channel.Object, user.Object, "command", "args");
			var listener = new Mock<CommandListener>();

			redirecter.AddListener("command", listener.Object);
			redirecter.RemoveListener("command", listener.Object);
			redirecter.ExecuteCommand(commandInfo);

			listener.Verify(l => l.OnCommand(commandInfo), Times.Never());
		}

		[TestMethod()]
		public void HandleCommand()
		{
			var bus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(bus.Object);
			TestUtils.AssertArgumentException(() => (redirecter as MessageHandler<CommandEvent>).HandleMessage(null));
		}

		[TestMethod()]
		public void HandleCommand_RunOnlyOnce()
		{
			var bus = new Mock<MessageBus>();
			var redirecter = new CommandRedirecter(bus.Object);
			var user = new Mock<User>("user");
			var channel = new Mock<Channel>("test");
			var commandInfo = new CommandEvent(channel.Object, user.Object, "command", "args");
			var listener = new Mock<CommandListener>();

			redirecter.AddListener("command", listener.Object);
			(redirecter as MessageHandler<CommandEvent>).HandleMessage(commandInfo);

			listener.Verify(l => l.OnCommand(commandInfo), Times.Once());
		}
	}
}