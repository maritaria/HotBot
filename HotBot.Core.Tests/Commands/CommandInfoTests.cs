using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using HotBot.Core;

namespace HotBot.Core.Commands.Tests
{
	[TestClass()]
	public class CommandInfoTests
	{
		[TestMethod()]
		public void CommandInfo()
		{
			var channel = new Mock<Channel>("TestChannel");
			var user = new Mock<User>("TestChannel");
			var authorizer = new Mock<User>("TestAuthorizer");
			var command = "command";
			var args = "args";
			var info1 = new CommandEvent(channel.Object, user.Object, command, args);

			Assert.AreEqual(channel.Object, info1.Channel, "Channel not equal");
			Assert.AreEqual(user.Object, info1.User, "User not equal");
			Assert.AreEqual(user.Object, info1.Authorizer, "Authorizer not equal to user");
			Assert.AreEqual(command, info1.CommandName, "CommandName not equal");
			Assert.AreEqual(args, info1.ArgumentText, "ArgumentText not equal");

			var info2 = new CommandEvent(channel.Object, user.Object, authorizer.Object, command, args);

			Assert.AreEqual(channel.Object, info2.Channel, "Channel not equal");
			Assert.AreEqual(user.Object, info2.User, "User not equal");
			Assert.AreEqual(authorizer.Object, info2.Authorizer, "Authorizer not equal to user");
			Assert.AreEqual(command, info2.CommandName, "CommandName not equal");
			Assert.AreEqual(args, info2.ArgumentText, "ArgumentText not equal");
		}

		[TestMethod]
		public void VerifyCommandName()
		{
			TestUtils.AssertArgumentException(() => CommandEvent.VerifyCommandName(null));
			TestUtils.AssertArgumentException(() => CommandEvent.VerifyCommandName(""));
			TestUtils.AssertArgumentException(() => CommandEvent.VerifyCommandName("hello world"));

			CommandEvent.VerifyCommandName("a");
			CommandEvent.VerifyCommandName("hello");
			CommandEvent.VerifyCommandName("can");
			CommandEvent.VerifyCommandName("asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasd");
			CommandEvent.VerifyCommandName("hello_q");
			CommandEvent.VerifyCommandName("hello-q");
			CommandEvent.VerifyCommandName("hello'q");
			CommandEvent.VerifyCommandName("hello\"q");
			CommandEvent.VerifyCommandName("hello\\q");
		}
	}
}