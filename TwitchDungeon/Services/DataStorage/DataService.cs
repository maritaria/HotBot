using System;
using System.Linq;
using System.Data.Entity;

namespace TwitchDungeon.Services.DataStorage
{
	public sealed class DataService : DbContext
	{
		private DataService() : base()
		{
		}

		public DbSet<User> Users { get; set; }

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