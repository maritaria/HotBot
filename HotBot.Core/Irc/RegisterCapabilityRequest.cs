using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	[DefaultPublishType(typeof(IrcTransmitRequest))]
	public sealed class RegisterCapabilityRequest : IrcTransmitRequest
	{
		public static readonly string TwitchMembership = @"twitch.tv/membership";
		public static readonly string ExtendedCommands = @"twitch.tv/commands"; 

		public string Capability { get; }

		public override string IrcCommand
		{
			get
			{
				return $"CAP REQ :{Capability}";
			}
		}

		public RegisterCapabilityRequest(string capability)
		{
			try
			{
				VerifyCapability(capability);
			}
			catch(Exception ex)
			{
				throw new ArgumentException(ex.Message, "capability", ex);
			}
			Capability = capability;
		}

		public static void VerifyCapability(string capability)
		{
			if (capability == null)

			{
				throw new ArgumentNullException("capability");
			}
			if (capability == string.Empty)
			{
				throw new ArgumentException("Cannot be an empty string", "capability");
			}
		}
	}
}