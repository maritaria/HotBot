using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public enum ReplyCode
	{
		#region Server

		/// <summary>
		/// Indicates the welcome message of the server.
		/// "Welcome to the Internet Relay Network NICK!USER@HOST"
		/// </summary>
		Welcome = 1,

		/// <summary>
		/// Indicates the server information message.
		/// "Your host is SERVERNAME, running version VERSION"
		/// </summary>
		YourHost = 2,

		/// <summary>
		/// Indicates the server creation message.
		/// "This server was created DATE"
		/// </summary>
		Created = 3,

		/// <summary>
		/// Indicates the capabilities of the server.
		/// "SERVERNAME VERSION AVAILABLE_USER_MODES AVAILABLE_CHANNEL_MODES"
		/// </summary>
		MyInfo = 4,

		/// <summary>
		/// Indicates the server reporting its supported features
		/// "Try server SERVER_NAME, port PORT_NUMBER"
		/// </summary>
		ISupport = 5,

		#endregion Server

		#region Motd

		/// <summary>
		/// Indicates a piece of the MOTD.
		/// ":- TEXT"
		/// </summary>
		Motd = 372,

		/// <summary>
		/// Indicates the start of the MOTD.
		/// ":- SERVER Message of the day - "
		/// </summary>
		MotdStart = 375,

		/// <summary>
		/// Indicates the end of the MOTD.
		/// ""
		/// </summary>
		MotdEnd = 376,

		/// <summary>
		/// Indicates the server has no MOTD.
		/// ":MOTD File is missing"
		/// </summary>
		MotdNotFound = 422,

		#endregion Motd

		#region Channel

		/// <summary>
		/// Indicates the server reporting a channel has no topic.
		/// "CHANNEL :No topic is set"
		/// </summary>
		NoTopic = 331,

		/// <summary>
		/// Indicates the server reporting the topic of a channel.
		/// "CHANNEL :TOPIC"
		/// </summary>
		Topic = 332,

		/// <summary>
		/// Indicates a single entry in a list of names.
		/// "( "=" / "*" / "@" ) CHANNEL :[ "@" / "+" ] NICK *( " " [ "@" / "+" ] NICK )"
		/// </summary>
		NameReply = 353,

		/// <summary>
		/// Indicates the end of the list of names
		/// "CHANNEL :End of NAMES list"
		/// </summary>
		EndOfNames = 366,

		#endregion Channel

		#region User

		/// <summary>
		/// Indicates the server reporting the identify of a user.
		/// "NICK USER HOST * :REAL_NAME"
		/// </summary>
		WhoIsUser = 311,

		/// <summary>
		/// Indicates the server reporting the identify of a server.
		/// "NICK SERVER :SERVER_INFO"
		/// </summary>
		WhoIsServer = 312,

		/// <summary>
		/// Indicates the server reporting that a user is an operator.
		/// "NICK :is an IRC operator"
		/// </summary>
		WhoIsOperator = 313,

		/// <summary>
		/// Indicates the server reporting the idle time of a user.
		/// "NICK INTEGER :seconds idle"
		/// </summary>
		WhoIsIdle = 317,

		/// <summary>
		/// Indicates the end of a WHOIS list.
		/// "NICK :End of WHOIS list"
		/// </summary>
		EndOfWhoIs = 318,

		/// <summary>
		///	Indicates the server reporting a channel in a whois-channel list.
		///	"NICK :*( ( "@" / "+" ) CHANNEL " " )"
		/// </summary>
		WhoIsChannels = 319,

		/// <summary>
		/// Indicates the server reporting the information about a users account.
		/// "NICK AUTHNAME :INFO".
		/// Not defined by RFC.
		/// </summary>

		WhoIsAccount = 330,

		#endregion User

		#region Listing

		//TODO: https://github.com/SirCmpwn/ChatSharp/blob/master/ChatSharp/Handlers/MessageHandlers.cs#L48

		#endregion Listing
	}

	public static class ReplyCodeExtensions
	{
		public static ReplyCode ToReplyCode(this int value)
		{
			if (!Enum.IsDefined(typeof(ReplyCode), value))
			{
				throw new ArgumentException($"No ReplyCode with the value '{value}'");
			}
			return (ReplyCode)value;
		}
		public static ReplyCode ToReplyCode(this string value)
		{
			if (!Enum.IsDefined(typeof(ReplyCode), value))
			{
				throw new ArgumentException($"No ReplyCode by the name '{value}'");
			}
			return(ReplyCode) Enum.Parse(typeof(ReplyCode), value);
		}
	}
}