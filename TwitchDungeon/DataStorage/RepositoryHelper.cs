using NHibernate.Tool.hbm2ddl;
using System;
using TwitchDungeon.DataStorage.NHibernate;

namespace TwitchDungeon.DataStorage
{
	public static class RepositoryHelper
	{
		//http://gergroen.blogspot.nl/2011/11/nhibernate-getting-started-guide.html
		private static SchemaUpdate _schemaUpdate;
		private static object _schemaLock = new object();
		private static object _repoLock = new object();
		private static UserRepository _userRepo;

		public static UserRepository Users
		{
			get
			{
				CheckSchema();
				lock (_repoLock)
				{
					if (_userRepo == null)
					{
						_userRepo = new NHibernateUserRepository();
					}
					return _userRepo;
				}
			}
		}

		private static void CheckSchema()
		{
			lock (_schemaLock)
			{
				if (_schemaUpdate == null)
				{
					_schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
					_schemaUpdate.Execute(true, true);
				}
			}
		}
	}
}