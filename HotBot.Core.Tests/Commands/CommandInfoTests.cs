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
		public void Constructor_Valid()
		{
			var channel = new Mock<Channel>("TestChannel");
			var user = new Mock<User>("TestChannel");
			var command = "command";
			var args = "args";
			var info = new CommandEvent(channel.Object, user.Object, command, args);

			Assert.AreEqual(channel.Object, info.Channel, "Channel not equal");
			Assert.AreEqual(user.Object, info.User, "User not equal");
			Assert.AreEqual(user.Object, info.Authorizer, "Authorizer not equal to user");
			Assert.AreEqual(command, info.CommandName, "CommandName not equal");
			Assert.AreEqual(args, info.ArgumentText, "ArgumentText not equal");
		}

		[TestMethod()]
		public void Constructor_Valid_Autorizer()
		{
			var channel = new Mock<Channel>("TestChannel");
			var user = new Mock<User>("TestUser");
			var authorizer = new Mock<User>("TestAuthorizer");
			var command = "command";
			var args = "args";
			var info = new CommandEvent(channel.Object, user.Object, authorizer.Object, command, args);

			Assert.AreEqual(channel.Object, info.Channel, "Channel not equal");
			Assert.AreEqual(user.Object, info.User, "User not equal");
			Assert.AreEqual(authorizer.Object, info.Authorizer, "Authorizer not equal to user");
			Assert.AreEqual(command, info.CommandName, "CommandName not equal");
			Assert.AreEqual(args, info.ArgumentText, "ArgumentText not equal");
		}

		[TestMethod]
		[ExpectedException(typeof(CommandEvent.InvalidCommandNameException))]
		public void VerifyCommandName_Null()
		{
			CommandEvent.VerifyCommandName(null);
		}

		[TestMethod]
		[ExpectedException(typeof(CommandEvent.InvalidCommandNameException))]
		public void VerifyCommandName_Empty()
		{
			CommandEvent.VerifyCommandName("");
		}

		[TestMethod]
		[ExpectedException(typeof(CommandEvent.InvalidCommandNameException))]
		public void VerifyCommandName_ContainsWhitespace()
		{
			CommandEvent.VerifyCommandName("hello world");
		}

		[TestMethod]
		public void VerifyCommandName_Valid()
		{
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