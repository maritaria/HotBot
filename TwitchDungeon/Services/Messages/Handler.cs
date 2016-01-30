using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.Messages
{
	public interface Handler
	{
		MessageBus Bus { get; }

		void Handle(object o);
	}

	public abstract class Handler<T> : Handler
	{
		public MessageBus Bus { get; internal set; }

		public void Handle(object o)
		{
			Handle((T)o);
		}

		protected abstract void Handle(T item);
	}
}
