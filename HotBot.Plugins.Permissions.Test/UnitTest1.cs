using HotBot.Core.Irc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace HotBot.Plugins.Permissions.Test
{
	[TestClass]
	public class PermissionPluginTests
	{
		public static readonly Guid UserGuid = new Guid(5234, 534, 25643, 6, 4, 2, 4, 6, 3, 1, 5);

		private static Mock<User> CreateUser()
		{
			var user = new Mock<User>();
			user.SetupGet(u => u.Id).Returns(UserGuid);
			return user;
		}

		[TestMethod]
		public void UserGroups()
		{
			Mock<User> user = CreateUser();
			var plugin = new PermissionPlugin();
			string testGroup = "hello.world";
			plugin.SetGroup(user.Object, testGroup);
			string group = plugin.GetGroup(user.Object);
			Assert.AreEqual(testGroup, group);
		}
	}
}