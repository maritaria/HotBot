using HotBot.Core.Intercom;
using HotBot.Core.Irc;
using HotBot.Core.Permissions;
using HotBot.Core.Plugins;
using HotBot.Core.Util;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace HotBot.Plugins.Permissions
{
	public class PermissionPlugin : Plugin, PermissionManager
	{
		public PluginDescription Description { get; } = new PluginDescription("permissions", "Adds support for permissions to the system");

		[Dependency]
		public PluginManager PluginManager { get; set; }

		[Dependency]
		public MessageBus Bus { get; set; }

		public void Load()
		{
			Bus.Subscribe(this);
		}

		public void Unload()
		{
			Bus.Unsubscribe(this);
		}

		public string GetGroup(User user)
		{
			Verify.NotNull(user, "user");
			using (var context = new PermissionContext())
			{
				return context.GetUserGroup(user)?.Group;
			}
		}

		public void SetGroup(User user, string group)
		{
			Verify.NotNull(user, "user");
			if (group == null)
			{
				DeleteGroup(user);
			}
			else
			{
				Verify.UserGroup(group, "group");
				UpdateGroup(user, group);
			}
		}

		private static void DeleteGroup(User user)
		{
			using (var context = new PermissionContext())
			{
				context.DeleteUserGroup(user);
				context.SaveChanges();
			}
		}

		private static void UpdateGroup(User user, string group)
		{
			using (var context = new PermissionContext())
			{
				context.UpdateOrInsertUserGroup(user, group);
				context.SaveChanges();
			}
		}

		public bool HasPermission(User user, string permissionNode)
		{
			Verify.NotNull(user, "user");
			Verify.PermissionNode(permissionNode, "permissionNode");

			using (var context = new PermissionContext())
			{
				return context.Nodes.Any(n => n.UserId == user.Id && n.Node == permissionNode);
			}

		}
	}
}