using System;
using System.Data.Entity;
using System.Linq;

namespace HotBot.Core
{
	public class DbContextDataStore : DbContext, DataStore, MessageHandler<SaveChanges>
	{
		public MessageBus Bus { get; }

		public DbSet<User> Users { get; set; }

		public DbSet<Channel> Channels { get; set; }

		static DbContextDataStore()
		{
			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DbContextDataStore>());
		}

		public DbContextDataStore(MessageBus bus) : base()
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Bus = bus;
			Bus.Subscribe(this);

			//TODO: read config for database clear instructions
		}

		public void Initialize()
		{
			//TODO: do the heavy lifting
			Database.Connection.Open();
			if (!Database.CompatibleWithModel(false))
			{
			}
		}

		void MessageHandler<SaveChanges>.HandleMessage(SaveChanges message)
		{
			SaveChanges();
		}
	}
}