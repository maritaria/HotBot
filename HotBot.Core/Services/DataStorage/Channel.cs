using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.DataStorage
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
			VerifyName(name);
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
				throw new InvalidNameException("Channelname cannot be null");
			}
			if (channelName.Length < MinimumNameLength)
			{
				throw new InvalidNameException($"Channelname must be at least {MinimumNameLength} characters");
			}
			if (channelName.Length > MaximumNameLength)
			{
				throw new InvalidNameException($"Channelname cannot be longer than {MaximumNameLength} characters");
			}
		}

		public sealed class InvalidNameException : Exception
		{
			public InvalidNameException()
			{
			}

			public InvalidNameException(string message) : base(message)
			{
			}

			public InvalidNameException(string message, Exception innerException) : base(message, innerException)
			{
			}

			private InvalidNameException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}
		}
	}
}
