using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public class WhisperKnownUserRequest : WhisperUserRequest
	{
		private User _targetUser;

		public User TargetUser
		{
			get { return _targetUser; }
			protected set
			{
				_targetUser = value;
				TargetUsername = value.Name;
			}
		}

		public WhisperKnownUserRequest(Channel channel, User user, string chatMessage) : base(channel, chatMessage)
		{
			TargetUser = user;
		}
	}
}