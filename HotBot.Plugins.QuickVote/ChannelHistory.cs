using HotBot.Core.Irc;
using HotBot.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Plugins.QuickVote
{
	/// <summary>
	/// Keeps track of tag records for a specific <see cref="ChannelData"/>.
	/// </summary>
	public sealed class ChannelHistory
	{
		private Dictionary<string, TagRecord> _history = new Dictionary<string, TagRecord>();

		/// <summary>
		/// Gets the <see cref="Channel"/> the <see cref="ChannelHistory"/> is monitoring.
		/// </summary>
		public Channel ObservedChannel { get; }

		/// <summary>
		/// Gets or sets the amount of time a <see cref="TagRecord"/> stays alive for.
		/// Whenever a tag gets recorded, the <see cref="TagRecord.ExpirationDate"/> property is updated to expire after the time specified in this property.
		/// </summary>
		public TimeSpan RecordLifetime { get; set; }//TODO: read from config (Requires dynamic config support)

		public ChannelHistory(Channel channel)
		{
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
			ObservedChannel = channel;
		}

		/// <summary>
		/// Records all hashtags of a given chat message.
		/// </summary>
		/// <param name="message">The message to record the tags of.</param>
		public void RecordFromChat(string message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			foreach (string tag in message.Split(' ').Where(s => s.StartsWith("#")))
			{
				RecordTag(tag);
			}
		}

		/// <summary>
		/// Updates the record of a specific tag
		/// </summary>
		/// <param name="tag">The tag to update. This is case insensitive and a single prefixed # is ignored if present.</param>
		public TagRecord RecordTag(string tag)
		{
			try
			{
				TagRecord.VerifyTag(tag);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "tag", ex);
			}
			var record = FindNearestTag(tag) ?? CreateTagRecord(tag);
			IncrementRecord(record);
			return record;
		}

		/// <summary>
		/// Gets the record of a specific tag. Ignores expiration.
		/// </summary>
		/// <param name="tag">The tag to find the record for</param>
		/// <returns>The record that is keeping track of the given tag. Null if none were found.</returns>
		public TagRecord GetRecord(string tag)
		{
			try
			{
				TagRecord.VerifyTag(tag);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "tag", ex);
			}
			return GetRecordInternal(tag);
		}

		/// <summary>
		/// Deletes a record from the history.
		/// </summary>
		/// <param name="record">The record to delete</param>
		public void Delete(TagRecord record)
		{
			if (record == null)
			{
				throw new ArgumentNullException("record");
			}
			_history.Remove(record.Tag);
		}

		/// <summary>
		/// Deletes all tags whose IsExpired property is true.
		/// </summary>
		public void DeleteExpiredRecords()
		{
			foreach (TagRecord record in _history.Values.ToArray())
			{
				if (record.IsExpired)
				{
					_history.Remove(record.Tag);
				}
			}
		}

		private TagRecord CreateTagRecord(string tag)
		{
			string loweredTag = NormalizeTag(tag);
			var record = new TagRecord(loweredTag);
			_history[record.Tag] = record;
			return record;
		}

		private TagRecord GetRecordInternal(string tag)
		{
			var loweredTag = NormalizeTag(tag);
			TagRecord nearestTag = FindNearestTag(loweredTag);
			return nearestTag;
		}

		private TagRecord FindNearestTag(string tag)
		{
			int maximumDistance = MaximumLevenshteinDistance(tag);

			int lowestDistance = maximumDistance;
			TagRecord closestRecord = null;

			foreach (TagRecord record in _history.Values)
			{
				int distance = record.Tag.LevenshteinDistance(tag);
				if (distance < lowestDistance)
				{
					closestRecord = record;
				}
			}

			return closestRecord;
		}

		private int MaximumLevenshteinDistance(string tag)
		{
			return (int)(tag.Length * 0.35);
		}

		private string NormalizeTag(string tag)
		{
			if (tag.StartsWith("#"))
			{
				tag = tag.Substring(1);
			}
			return tag.ToLower();
		}

		private TagRecord GetNonExpiredRecordInternal(string tag)
		{
			var record = GetRecordInternal(tag);
			if (record == null || record.IsExpired)
			{
				record = CreateTagRecord(tag);
			}
			return record;
		}

		private void IncrementRecord(TagRecord record)
		{
			record.MentionCount++;
			record.ExpirationDate = DateTime.Now + RecordLifetime;
		}
	}
}