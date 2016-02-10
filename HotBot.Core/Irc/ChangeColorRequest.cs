using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	[DefaultPublishType(typeof(IrcTransmitRequest))]
	public sealed class ChangeColorRequest : ChatTransmitRequest
	{
		private ChatColor _color;

		public ChatColor Color
		{
			get { return _color; }
			private set
			{
				_color = value;
				Text = $"/color {Color.ToString()}";
			}
		}

		public ChangeColorRequest(Channel channel, ChatColor color) : base(channel)
		{
			Color = color;
		}

		public enum ChatColor
		{
			Blue,
			BlueViolet,
			CadetBlue,
			Chocolate,
			Coral,
			DodgerBlue,
			Firebrick,
			GoldenRod,
			Green,
			HotPink,
			OrangeRed,
			Red,
			SeaGreen,
			SpringGreen,
			YellowGreen
		}
	}
}