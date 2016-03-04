using HotBot.Core.Irc;
using HotBot.Core.Util;
using System;
using System.Data.Entity;
using System.Linq;

namespace HotBot.Plugins.Permissions
{
	internal sealed class PermissionContext : DbContext
	{
		public DbSet<PermissionNode> Nodes { get; set; }
		public DbSet<UserGroup> Groups { get; set; }
		
		public UserGroup GetUserGroup(User user)
		{
			Verify.NotNull(user, "user");
			return Groups.FirstOrDefault(ug => ug.UserId == user.Id);
		}

		public void DeleteUserGroup(User user)
		{
			Verify.NotNull(user, "user");
			var userGroup = GetUserGroup(user);
			if (userGroup != null)
			{
				Groups.Remove(userGroup);
			}
		}

		public void UpdateOrInsertUserGroup(User user, string group)
		{
			Verify.NotNull(user, "user");
			Verify.UserGroup(group, "group");
			var userGroup = GetUserGroup(user);
			if (userGroup == null)
			{
				InsertUserGroup(user, group);
			}
			else
			{
				UpdateUserGroup(userGroup, group);
			}
		}

		private void UpdateUserGroup(UserGroup userGroup, string group)
		{
			userGroup.Group = group;
		}

		private void InsertUserGroup(User user, string group)
		{
			UserGroup usergroup = new UserGroup { UserId = user.Id, Group = group };
			Groups.Add(usergroup);
		}
	}
}