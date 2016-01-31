using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HotBot.Core.Services.DataStorage.Tests
{
	[TestClass()]
	public class ChannelTests
	{
		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Null()
		{
			new Channel(null);
		}

		[TestMethod()]
		[ExpectedException(typeof(User.InvalidNameException))]
		public void Constructor_ChannelName_TooShort()
		{
			new Channel("");
		}

		[TestMethod()]
		[ExpectedException(typeof(User.InvalidNameException))]
		public void Constructor_ChannelName_TooLong()
		{
			new Channel("123456789012345678901234567890");
		}

		[TestMethod()]
		public void Constructor_Valid()
		{
			var channel = new Channel("test");
			Assert.AreNotEqual(Guid.Empty, channel.Id, "Id not randomized");
			Assert.AreEqual("test", channel.Name, "Name property invalid");
		}

		[TestMethod()]
		public void ToString_Prefix()
		{
			var channel = new Channel("test");
			Assert.AreEqual(Channel.ChannelPrefix + "test", channel.ToString());
		}
	}
}