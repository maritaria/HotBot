using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotBot.Core.DataStorage
{
	public interface DataStore
	{
		DbSet<User> Users { get; }
		DbSet<Channel> Channels { get; }

		void Initialize();
		int SaveChanges();
		Task<int> SaveChangesAsync();
		Task<int> SaveChangesAsync(CancellationToken token);
	}
}