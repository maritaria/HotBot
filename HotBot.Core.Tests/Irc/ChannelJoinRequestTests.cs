using HotBot.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace HotBot.Core.Irc.Tests
{
	[TestClass()]
	public class ChannelJoinRequestTests
	{
		[TestMethod()]
		public void ChannelJoinRequest_Constructor()
		{
			var channel = new Mock<Channel>("test");
			var channelJoinRequest = new ChannelJoinRequest(channel.Object);

			TestUtils.AssertArgumentException(() => new ChannelJoinRequest(null));
			
			Assert.AreEqual(channel.Object, channelJoinRequest.Channel);
		}
	}
}