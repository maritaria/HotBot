using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using HotBot.Core;
using HotBot.Core.Irc;

namespace HotBot.Plugin.Lottery
{
	public sealed class Lottery
	{
		private Timer _timer;
		private object _stateLock = new object();
		private User _winner;

		private Collection<User> _participants = new Collection<User>();
		public ReadOnlyCollection<User> Participants { get; private set; }
		public Channel Channel { get; private set; }

		public MessageBus Bus { get; }

		/// <summary>
		/// The winner of the lottery, null if no winner has been picked yet
		/// </summary>
		public User Winner
		{
			get { return _winner; }
			private set
			{
				_winner = value;
				Bus.Publish(new LotteryWinnerEvent(this));
			}
		}

		public LotteryState State { get; private set; } = LotteryState.NotStarted;
		private double _pot = 0;

		public double Pot
		{
			get { return _pot; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "cannot be lower than zero");
				}
				_pot = value;
			}
		}

		public TimeSpan Duration
		{
			get { return TimeSpan.FromMilliseconds(_timer.Interval); }
			set { _timer.Interval = value.TotalMilliseconds; }
		}

		public Lottery(MessageBus bus)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Bus = bus;

			Participants = new ReadOnlyCollection<User>(_participants);
			_timer = new Timer();
			_timer.Interval = 60 * 1000;
			_timer.Elapsed += Timer_Elapsed;
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			_timer.Dispose();
			_timer = null;
			Finish();
		}

		public void Finish()
		{
			lock (_stateLock)
			{
				switch (State)
				{
					case LotteryState.NotStarted:
						throw new InvalidOperationException("Lottery not yet started");
					case LotteryState.Finished:
						throw new InvalidOperationException("Lottery already finished");
					case LotteryState.Open:
						State = LotteryState.Finished;
						PickWinner();
						break;
				}
			}
		}

		public void Start(Channel channel)
		{
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
			lock (_stateLock)
			{
				if (State != LotteryState.NotStarted)
				{
					throw new InvalidOperationException("Lottery already started");
				}
				_timer.Start();
				Channel = channel;
				State = LotteryState.Open;
			}
		}

		private void PickWinner()
		{
			if (Participants.Count == 0)
			{
				Bus.Publish(new ChatTransmitRequest(Channel, $"The lottery is over, but nobody participated :("));
			}
			else
			{
				Winner = GetRandomParticipant();
			}
		}

		private User GetRandomParticipant()
		{
			Random r = new Random();
			int userIndex = r.Next(Participants.Count - 1);
			return Participants[userIndex];
		}

		public void VerifyCanJoin(User user)
		{
			if (State != LotteryState.Open)
			{
				throw new LotteryException($"Lottery not opened (State: {State.ToString()}");
			}
		}

		public void Join(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			VerifyCanJoin(user);
			_participants.Add(user);
		}
	}
}