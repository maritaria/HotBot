using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using HotBot.Core.DataStorage.Permissions;

namespace HotBot.Core.Services.DataStorage
{
	public class User : Authorizer
	{
		public const int MinimumNameLength = 4;
		public const int MaximumNameLength = 25;
		public const char HandlePrefix = '@';

		[Key]
		public Guid Id { get; private set; }

		[Index(IsUnique = true)]
		[StringLength(MaximumNameLength, MinimumLength = MinimumNameLength)]
		public string Name { get; private set; }

		public double Money { get; set; }

		public UserRole Role { get; set; }

		public User(string name) : this()
		{
			VerifyName(name);
			Name = name;
		}

		protected User()
		{
			Id = Guid.NewGuid();
			Role = UserRole.User;
		}

		public static void VerifyName(string username)
		{
			if (username == null)
			{
				throw new InvalidNameException("Username cannot be null");
			}
			if (username.Length < MinimumNameLength)
			{
				throw new InvalidNameException($"Username must be at least {MinimumNameLength} characters");
			}
			if (username.Length > MaximumNameLength)
			{
				throw new InvalidNameException($"Username cannot be longer than {MaximumNameLength} characters");
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