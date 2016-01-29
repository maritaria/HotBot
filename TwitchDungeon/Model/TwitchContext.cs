using System;
using System.Data.Entity;

namespace TwitchDungeon.Model
{
	public sealed class TwitchContext : DbContext
	{
		public TwitchContext() : base()
		{

		}

		public DbSet<User> Users { get; set; }
	}
}