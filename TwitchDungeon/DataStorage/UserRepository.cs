using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.DataStorage
{
	public interface UserRepository
	{
		User Get(Guid id);
		void Save(User user);
		void Update(User user);
		void Delete(User user);
		long RowCount();
	}
}
