using HotBot.Core.Irc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Permissions
{
	public interface PermissionManager
	{
		string GetGroup(User user);
		void SetGroup(User user, string group);
		bool HasPermission(User user, string permissionNode);
	}
}
