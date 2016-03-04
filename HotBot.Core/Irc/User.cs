using HotBot.Core.Permissions;
using System;

namespace HotBot.Core.Irc
{
	public interface User
	{
		Guid Id { get; }
		string Name { get; }
	}
}