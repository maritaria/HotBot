using HotBot.Core.Irc;
using HotBot.Core.Irc.Impl;
using HotBot.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace HotBot.Core.Commands.Tests
{
	[TestClass()]
	public class CommandInfoTests
	{
		[TestMethod()]
		public void CommandInfo_Constructor()
		{
			var channel = new Mock<ChannelData>("TestChannel");
			var user = new Mock<BasicUser>("TestChannel");
			var authorizer = new Mock<BasicUser>("TestAuthorizer");
			var command = "command";
			var args = "args";
			var info1 = new CommandEvent(channel.Object, user.Object, command, args);

			Assert.AreEqual(channel.Object, info1.Channel);
			Assert.AreEqual(user.Object, info1.User);
			Assert.AreEqual(user.Object, info1.Authorizer);
			Assert.AreEqual(command, info1.CommandName);
			Assert.AreEqual(args, info1.ArgumentText);

			var info2 = new CommandEvent(channel.Object, user.Object, authorizer.Object, command, args);

			Assert.AreEqual(channel.Object, info2.Channel);
			Assert.AreEqual(user.Object, info2.User);
			Assert.AreEqual(authorizer.Object, info2.Authorizer);
			Assert.AreEqual(command, info2.CommandName);
			Assert.AreEqual(args, info2.ArgumentText);
		}

		[TestMethod]//TODO: Move this to Unit test for Verify
		public void VerifyCommandName()
		{
			TestUtils.AssertArgumentException(() => VerifyCommandName.VerifyCommandName(null));
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