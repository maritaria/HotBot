using System;
using System.Data.Entity;
using System.Linq;

namespace HotBot.Core
{
	//TODO: Allow plugins to have their own database connection(?)
	public sealed class DbContextDataStore : DbContext, DataStore
	{
		public MessageBus Bus { get; }

		public DbSet<User> Users { get; set; }

		public DbSet<Channel> Channels { get; set; }

		static DbContextDataStore()
		{
			var strategy = new DropCreateDatabaseIfModelChanges<DbContextDataStore>();
			Database.SetInitializer(strategy);
		}

		public DbContextDataStore(MessageBus bus) : base()
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Bus = bus;
			Bus.Subscribe(this);
		}

		public void Initialize()
		{
			Console.WriteLine("Initializing database");//TODO: Better logging
			Database.Initialize(false);
			Database.Connection.Open();
			Console.WriteLine("Initializing database DONE");
		}

		[Subscribe]
		public void OnSaveDatabaseChanges(SaveDatabaseChangesRequest message)
		{
			SaveChanges();
		}
	}
}