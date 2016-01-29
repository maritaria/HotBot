using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace TwitchDungeon
{
	public class User
	{
		public virtual Guid Id { get; protected set; }

		public virtual string Username { get; protected set; }

		public virtual long Money { get; set; }

		protected User()
		{
		}

		public User(string username)
		{
		}

		public sealed class Mapping : ClassMapping<User>
		{
			public Mapping()
			{
				Id(u => u.Id, m => m.Generator(Generators.GuidComb));
				Property(u => u.Username, m => m.Unique(true));
				Property(u => u.Money);
			}
		}
	}
}