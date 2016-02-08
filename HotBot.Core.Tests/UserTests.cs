using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using HotBot.Core.Permissions;

namespace HotBot.Core.Tests
{
	[TestClass()]
	public class UserTests
	{
		[TestMethod()]
		public void User_Constructor()
		{
			TestUtils.AssertArgumentException(() => new User(null));
			TestUtils.AssertArgumentException(() => new User(""));
			TestUtils.AssertArgumentException(() => new User("123456789012345678901234567890"));

			User user = new User("username");
			Assert.AreNotEqual(Guid.Empty, user.Id, "Id not randomized");
			Assert.AreEqual("username", user.Name);
			Assert.AreEqual(0d, user.Money, "Money does not start at 0");
			Assert.AreEqual(UserRole.User, user.Role, "Incorrect default role");
		}
	}
}