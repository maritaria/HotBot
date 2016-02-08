using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace HotBot.Core.Irc.Tests
{
	//Todo; collapse all constructor tests into 1 test; requires custom assert util class
	[TestClass()]
	public class WhisperUserEventTests
	{
		[TestMethod()]
		public void WhisperUserEvent_Constructor()
		{
			var username = "testUser";
			var message = "test message";
			var channel = new Mock<Channel>("test");
			var expectedIrcCommand = $"PRIVMSG :/w testUser test message";

			var whisperUserRequest = new WhisperUserRequest(channel.Object, username, message);

			TestUtils.AssertArgumentException(() => new WhisperUserRequest(null, username, message));
			TestUtils.AssertArgumentException(() => new WhisperUserRequest(channel.Object, null, message));
			TestUtils.AssertArgumentException(() => new WhisperUserRequest(channel.Object, "", message));
			TestUtils.AssertArgumentException(() => new WhisperUserRequest(channel.Object, "t", message));
			TestUtils.AssertArgumentException(() => new WhisperUserRequest(channel.Object, "123456789012345678901234567890", message));
			TestUtils.AssertArgumentException(() => new WhisperUserRequest(channel.Object, username, null));

			Assert.AreEqual(channel.Object, whisperUserRequest.Channel);
			Assert.AreEqual(username, whisperUserRequest.TargetUsername);
			Assert.AreEqual(message, whisperUserRequest.Text);
			Assert.AreEqual(expectedIrcCommand, whisperUserRequest.IrcCommand);
		}
	}
}