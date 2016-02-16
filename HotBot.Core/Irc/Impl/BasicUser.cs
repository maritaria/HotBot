using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using HotBot.Core.Permissions;
using HotBot.Core.Util;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicUser : User
	{
		//TODO: Store this somewhere safe.
		public const string ChannelPrefix = "#";
		public const char HandlePrefix = '@';//TODO: Store this somewhere safe

		[Key]
		public Guid Id { get; private set; }

		[Index(IsUnique = true)]
		[StringLength(Verify.MaximumUsernameLength, MinimumLength = Verify.MinimumUsernameLength)]
		public string Name { get; private set; }

		public double Money { get; set; }

		public UserRole Role { get; set; }

		public BasicUser(string name) : this()
		{
			try
			{
				Verify.Username(name);
			}
			catch(Exception ex)
			{
				throw new ArgumentException(ex.Message, "name", ex);
			}
			Name = name;
		}

		private BasicUser()
		{
			Id = Guid.NewGuid();
			Role = UserRole.User;
		}

	}
}