using HotBot.Core.Permissions;
using HotBot.Core.Unity;
using HotBot.Core.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotBot.Core.Irc.Impl
{
	[RegisterFor(typeof(User))]
	public class BasicUser : User
	{
		//TODO: Store this somewhere safe.
		public const string ChannelPrefix = "#";

		public const char HandlePrefix = '@';//TODO: Store this somewhere safe

		[Key]
		public Guid Id { get; private set; } = Guid.NewGuid();

		[Index(IsUnique = true)]
		[StringLength(Verify.MaximumUsernameLength, MinimumLength = Verify.MinimumUsernameLength)]
		public string Name { get; private set; }
		
		public UserRole Role { get; set; } = UserRole.User;

		public BasicUser(string name)
		{
			Verify.Username(name, "name");
			Name = name;
		}

		protected BasicUser()
		{

		}

	}
}