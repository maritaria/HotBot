using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using TwitchDungeon.Services.DataStorage;

namespace TwitchDungeon.Services.Commands.Tests
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
			var info = new CommandInfo(channel.Object, user.Object, command, args);

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
			var info = new CommandInfo(channel.Object, user.Object, authorizer.Object, command, args);

			Assert.AreEqual(channel.Object, info.Channel, "Channel not equal");
			Assert.AreEqual(user.Object, info.User, "User not equal");
			Assert.AreEqual(authorizer.Object, info.Authorizer, "Authorizer not equal to user");
			Assert.AreEqual(command, info.CommandName, "CommandName not equal");
			Assert.AreEqual(args, info.ArgumentText, "ArgumentText not equal");
		}
	}
}