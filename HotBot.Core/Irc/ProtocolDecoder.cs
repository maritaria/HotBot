using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public sealed class ProtocolDecoder
	{
		public MessageBus Bus { get; }
		
		public ProtocolDecoder(MessageBus bus)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Bus = bus;
			Bus.Subscribe(this);
		}

		[Subscribe]
		public void OnResponse(Response response)
		{
			if (response.Command == "PRIVMSG")
			{

			}
		}

	}
}