using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.DataStorage
{
	public class DbContextDataStore : DbContext, DataStore
	{
		public DbContextDataStore() : base()
		{
			//TODO: read config for database clear instructions
		}

		public DbSet<User> Users { get; set; }

		public DbSet<Channel> Channels { get; set; }
		
		private void NewMethod()
		{
			string targetUsername = "";// e.Message.Username;
			User user = Users.Where(u => u.Username == targetUsername).FirstOrDefault();
			if (user == null)
			{
				user = new User("");// e.Message.Username);
				user.Money = 100;
				Users.Add(user);
			}
			else
			{
				user.Money += 100;
			}
			SaveChanges();
		}
	}
}
