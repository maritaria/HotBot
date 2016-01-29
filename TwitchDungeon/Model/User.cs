using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitchDungeon
{
	public class User
	{
		public Guid Id { get; private set; }

		[Index(IsUnique = true)]
		[StringLength(25)]
		public string Username { get; private set; }

		public long Money { get; set; }

		public User(string username) : this()
		{
			Username = username;
		}

		protected User()
		{
			Id = Guid.NewGuid();
		}
	}
}