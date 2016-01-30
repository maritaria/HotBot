using System;
using System.Data.Entity;
using System.Linq;

namespace TwitchDungeon.Services.DataStorage
{
	public class DbContextDataStore : DbContext, DataStore
	{
		public DbSet<User> Users { get; set; }

		public DbSet<Channel> Channels { get; set; }

		public DbContextDataStore() : base()
		{
			//TODO: read config for database clear instructions
		}

		public void Initialize()
		{
			//TODO: do the heavy lifting
			Database.Connection.Open();
		}
	}
}