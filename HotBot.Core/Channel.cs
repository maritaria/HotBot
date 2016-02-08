using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HotBot.Core
{
	public class Channel
	{
		public const int MinimumNameLength = User.MinimumNameLength;
		public const int MaximumNameLength = User.MaximumNameLength;
		public const string ChannelPrefix = "#";

		[Key]
		public Guid Id { get; private set; }

		[Index(IsUnique = true)]
		[StringLength(25)]
		public string Name { get; private set; }

		public Channel(string name) : this()
		{
			try
			{
				VerifyName(name);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "name", ex);
			}
			Name = name;
		}

		protected Channel()
		{
			Id = Guid.NewGuid();
		}

		public override string ToString()
		{
			return ChannelPrefix + Name;
		}

		public static void VerifyName(string channelName)
		{
			if (channelName == null)
			{
				throw new ArgumentNullException("channelName");
			}
			if (channelName.Length < MinimumNameLength)
			{
				throw new ArgumentException($"A channel name must be at least {MinimumNameLength} characters", "channelName");
			}
			if (channelName.Length > MaximumNameLength)
			{
				throw new ArgumentException($"A channel name cannot be longer than {MaximumNameLength} characters", "channelName");
			}
		}
	}
}