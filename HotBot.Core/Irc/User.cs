using HotBot.Core.Permissions;
using System;

namespace HotBot.Core.Irc
{
	public interface User : Authorizer
	{
		Guid Id { get; }
		string Name { get; }
	}
}