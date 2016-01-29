using NHibernate;
using System;

namespace TwitchDungeon.DataStorage.NHibernate
{
	public sealed class NHibernateUserRepository : UserRepository
	{

		public User Get(Guid id)
		{
			using (ISession session = NHibernateHelper.OpenSession())
				return session.Get<User>(id);
		}

		public void Save(User user)
		{
			using (ISession session = NHibernateHelper.OpenSession())
			using (ITransaction transactiohn = session.BeginTransaction())
			{
				session.Save(user);
				transactiohn.Commit();
			}
		}

		public void Update(User person)
		{
			using (ISession session = NHibernateHelper.OpenSession())
			using (ITransaction transaction = session.BeginTransaction())
			{
				session.Update(person);
				transaction.Commit();
			}
		}

		public void Delete(User person)
		{
			using (ISession session = NHibernateHelper.OpenSession())
			using (ITransaction transaction = session.BeginTransaction())
			{
				session.Delete(person);
				transaction.Commit();
			}
		}

		public long RowCount()
		{
			using (ISession session = NHibernateHelper.OpenSession())
			{
				return session.QueryOver<User>().RowCountInt64();
			}
		}
	}
}