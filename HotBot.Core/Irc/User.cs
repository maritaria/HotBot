using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using HotBot.Core.Permissions;

namespace HotBot.Core.Irc
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
				throw new ArgumentNullException("username");
			}
			if (username.Length < MinimumNameLength)
			{
				throw new ArgumentException($"A username must be at least {MinimumNameLength} characters", "username");
			}
			if (username.Length > MaximumNameLength)
			{
				throw new ArgumentException($"A username cannot be longer than {MaximumNameLength} characters", "username");
			}
		}

	}
}