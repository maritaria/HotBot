using System;
using System.Linq;

namespace HotBot.Core.Permissions
{
	public interface Authorizer
	{
		UserRole Role { get; }
	}
}