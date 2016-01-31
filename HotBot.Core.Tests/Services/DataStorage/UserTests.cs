using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using HotBot.Core.DataStorage.Permissions;

namespace HotBot.Core.Services.DataStorage.Tests
{
	[TestClass()]
	public class UserTests
	{
		[TestMethod()]
		[ExpectedException(typeof(User.InvalidNameException))]
		public void Constructor_Username_Null()
		{
			new User(null);
		}

		[TestMethod()]
		[ExpectedException(typeof(User.InvalidNameException))]
		public void Constructor_Username_TooShort()
		{
			new User("");
		}

		[TestMethod()]
		[ExpectedException(typeof(User.InvalidNameException))]
		public void Constructor_Username_TooLong()
		{
			new User("123456789012345678901234567890");
		}

		[TestMethod()]
		public void Constructor_Valid()
		{
			User user = new User("username");
			Assert.AreNotEqual(Guid.Empty, user.Id, "Id not randomized");
			Assert.AreEqual("username", user.Name);
			Assert.AreEqual(0d, user.Money, "Money does not start at 0");
			Assert.AreEqual(UserRole.User, user.Role, "Incorrect default role");
		}
	}
}