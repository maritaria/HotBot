using HotBot.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HotBot.Core.Tests
{
	[TestClass()]
	public class ChannelTests
	{
		[TestMethod()]
		public void Channel_Constructor()
		{
			TestUtils.AssertArgumentException(() => new Channel(null));
			TestUtils.AssertArgumentException(() => new Channel(""));
			TestUtils.AssertArgumentException(() => new Channel("123456789012345678901234567890"));

			var channel = new Channel("test");
			Assert.AreNotEqual(Guid.Empty, channel.Id, "Id not randomized");
			Assert.AreEqual("test", channel.Name, "Name property invalid");
		}
		
		[TestMethod()]
		public void ToString_ContainsPrefix()
		{
			var channel = new Channel("test");
			Assert.AreEqual(Channel.ChannelPrefix + "test", channel.ToString());
		}
	}
}