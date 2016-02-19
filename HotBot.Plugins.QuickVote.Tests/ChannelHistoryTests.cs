using HotBot.Core;
using HotBot.Core.Irc;
using HotBot.Core.Irc.Impl;
using HotBot.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
			var channel = new BasicChannelData("testChannel");
			var channelHistory = new ChannelHistory(channel);

			Assert.AreEqual(channel, channelHistory.ObservedChannel);

			TestUtils.AssertArgumentException(() => new ChannelHistory(null));
		}

		[TestMethod()]
		public void RecordFromChat()
		{
			var channel = new BasicChannelData("testChannel");
			var channelHistory = new ChannelHistory(channel);
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
			var channel = new BasicChannelData("testChannel");
			var channelHistory = new ChannelHistory(channel);
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
			var channel = new BasicChannelData("testChannel");
			var channelHistory = new ChannelHistory(channel);
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
			var channel = new BasicChannelData("testChannel");
			var channelHistory = new ChannelHistory(channel);
			var tag = "testTag";
			channelHistory.RecordTag(tag);
			channelHistory.Delete(channelHistory.GetRecord(tag));

			Assert.IsNull(channelHistory.GetRecord(tag));

			TestUtils.AssertArgumentException(() => channelHistory.Delete(null));
		}

		[TestMethod()]
		public void DeleteExpiredRecords()
		{
			var channel = new BasicChannelData("testChannel");
			var channelHistory = new ChannelHistory(channel);
			var tag = "testTag";

			channelHistory.RecordLifetime = TimeSpan.FromMilliseconds(100);
			var record = channelHistory.RecordTag(tag);
			record.ExpirationDate = DateTime.Now.Subtract(TimeSpan.FromMinutes(1));
			channelHistory.DeleteExpiredRecords();
			Assert.IsNull(channelHistory.GetRecord(tag));
		}
	}
}