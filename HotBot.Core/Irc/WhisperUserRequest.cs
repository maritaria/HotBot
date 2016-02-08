using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	//TODO: Support receiving whispers (if possible)
	public class WhisperUserRequest : ChatTransmitRequest
	{
		public const string WhisperCommand = "/w";

		private string _targetUsername;

		public string TargetUsername
		{
			get
			{
				return _targetUsername;
			}
			protected set
			{
				try
				{
					User.VerifyName(value);
				}
				catch(Exception ex)
				{
					throw new ArgumentException(ex.Message, "value", ex);
				}
				_targetUsername = value;
				InvalidateIrcCommand();
			}
		}

		protected WhisperUserRequest(Channel channel, string chatMessage) : base(channel, chatMessage)
		{
		}

		public WhisperUserRequest(Channel channel, string username, string chatMessage) : this(channel, chatMessage)
		{
			try
			{
				User.VerifyName(username);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "username", ex);
			}
			TargetUsername = username;
		}

		protected override string GenerateIrcCommand(string text)
		{
			return base.GenerateIrcCommand($"{WhisperCommand} {TargetUsername} {text}");
		}
	}
}