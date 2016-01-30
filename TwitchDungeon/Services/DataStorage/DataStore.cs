using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.DataStorage
{
	public interface DataStore
	{
		DbSet<User> Users { get; }
		DbSet<Channel> Channels { get; }

		int SaveChanges();
		Task<int> SaveChangesAsync();
		Task<int> SaveChangesAsync(CancellationToken token);

	}
}