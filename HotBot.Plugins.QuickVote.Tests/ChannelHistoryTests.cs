using HotBot.Core.Irc;
using HotBot.Core.Irc.Impl;
using HotBot.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace HotBot.Plugins.QuickVote.Tests
{
	[TestClass()]
	public class ChannelHistoryTests
	{
		[TestMethod()]
		public void ChannelHistory()
		{
			var channel = new Mock<Channel>();
			var channelHistory = new ChannelHistory(channel.Object);

			Assert.AreEqual(channel, channelHistory.ObservedChannel);

			TestUtils.AssertArgumentException(() => new ChannelHistory(null));
		}

		[TestMethod()]
		public void RecordFromChat()
		{
			var channelHistory = CreateChannelHistory("testChannel");
			var chat = "#testTag";

			channelHistory.RecordFromChat(chat);

			Assert.IsNotNull(channelHistory.GetRecord("testTag"));
			Assert.IsNotNull(channelHistory.GetRecord("testtag"));
			Assert.IsNotNull(channelHistory.GetRecord("TESTTAG"));
			Assert.IsNotNull(channelHistory.GetRecord("#testTag"));
			Assert.IsNotNull(channelHistory.GetRecord("#testtag"));
			Assert.IsNotNull(channelHistory.GetRecord("#TESTTAG"));

			var record = channelHistory.GetRecord("testTag");
			Assert.AreSame(record, channelHistory.GetRecord("testtag"));
			Assert.AreSame(record, channelHistory.GetRecord("TESTTAG"));
			Assert.AreSame(record, channelHistory.GetRecord("#testTag"));
			Assert.AreSame(record, channelHistory.GetRecord("#testtag"));
			Assert.AreSame(record, channelHistory.GetRecord("#TESTTAG"));

			TestUtils.AssertArgumentException(() => channelHistory.RecordFromChat(null));
		}

		[TestMethod()]
		public void RecordTag()
		{
			var channelHistory = CreateChannelHistory("testChannel");
			var tag = "testTag";

			var record = channelHistory.RecordTag(tag);
			Assert.IsNotNull(record);
			Assert.AreEqual(tag.ToLower(), record.Tag);
			Assert.AreSame(record, channelHistory.GetRecord("testTag"));
			Assert.AreSame(record, channelHistory.GetRecord("testtag"));
			Assert.AreSame(record, channelHistory.GetRecord("TESTTAG"));
			Assert.AreSame(record, channelHistory.GetRecord("#testTag"));
			Assert.AreSame(record, channelHistory.GetRecord("#testtag"));
			Assert.AreSame(record, channelHistory.GetRecord("#TESTTAG"));

			Assert.AreSame(record, channelHistory.GetRecord("#testXag"));

			TestUtils.AssertArgumentException(() => channelHistory.RecordTag(null));
			TestUtils.AssertArgumentException(() => channelHistory.RecordTag(""));
		}

		[TestMethod()]
		public void GetRecord()
		{
			var channelHistory = CreateChannelHistory("testChannel");
			var tag = "testTag";
			channelHistory.RecordTag(tag);

			Assert.IsNull(channelHistory.GetRecord("asdf"));
			Assert.IsNotNull(channelHistory.GetRecord(tag));

			TestUtils.AssertArgumentException(() => channelHistory.GetRecord(null));
			TestUtils.AssertArgumentException(() => channelHistory.GetRecord(""));
		}

		[TestMethod()]
		public void Delete()
		{
			var channelHistory = CreateChannelHistory("testChannel");
			var tag = "testTag";
			channelHistory.RecordTag(tag);
			channelHistory.Delete(channelHistory.GetRecord(tag));

			Assert.IsNull(channelHistory.GetRecord(tag));

			TestUtils.AssertArgumentException(() => channelHistory.Delete(null));
		}

		[TestMethod()]
		public void DeleteExpiredRecords()
		{
			var channelHistory = CreateChannelHistory("testChannel");
			var tag = "testTag";
			channelHistory.RecordLifetime = TimeSpan.FromMilliseconds(100);
			var record = channelHistory.RecordTag(tag);
			record.ExpirationDate = DateTime.Now.Subtract(TimeSpan.FromMinutes(1));
			channelHistory.DeleteExpiredRecords();
			Assert.IsNull(channelHistory.GetRecord(tag));
		}

		private static ChannelHistory CreateChannelHistory(string channelName)
		{

			var channel = new Mock<Channel>();
			return new ChannelHistory(channel.Object);
		}

	}
}