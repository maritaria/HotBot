using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;

namespace HotBot.Testing
{
	public static class TestUtils
	{
		public static void AssertException<TException>(Action callback) where TException : Exception
		{
			try
			{
				callback();
			}
			catch (Exception ex)
			{
				if (ex.GetType() == typeof(TException))
				{
					return;
				}
				else
				{
					Assert.Fail($"An exception of the wrong type was thrown. Expected type <{typeof(TException).FullName}>. But actually thrown was <{ex.GetType().FullName}>");
				}
			}
			Assert.Fail($"No exception was thrown but expected was <{typeof(TException).FullName}>");
		}

		public static void AssertExceptionDerivedFrom<TException>(Action callback) where TException : Exception
		{
			try
			{
				callback();
			}
			catch (Exception ex)
			{
				if (ex is TException)
				{
					return;
				}
				else
				{
					Assert.Fail($"An exception of the wrong type was thrown. Expected type derived of <{typeof(TException).FullName}>. But actually thrown was <{ex.GetType().FullName}>");
				}
			}
			Assert.Fail($"No exception was thrown but expected was <{typeof(TException).FullName}>");
		}

		public static void AssertArgumentException(Action callback)
		{
			AssertExceptionDerivedFrom<ArgumentException>(callback);
		}

		public static void AssertArgumentException(Action callback, string parameterName)
		{
			//TODO: check the argument for which the exception is thrown
			try
			{
				callback();
			}
			catch (ArgumentException ex)
			{
				if (ex.ParamName == parameterName)
				{
					return;
				}
				else
				{
					Assert.Fail($"An ArgumentException was thrown for the wrong argument. Expected for '{parameterName}'. But actually '{ex.ParamName}'");
				}
			}
			catch (Exception ex)
			{
				Assert.Fail($"An exception of the wrong type was thrown. Expected type derived of <{typeof(ArgumentException).Name}>. But actually thrown was <{ex.GetType().Name}>");
			}
			Assert.Fail($"No exception was thrown but expected was <{typeof(ArgumentException).FullName}>");
		}
		/// <summary>
		/// Waits for the specified amount of time.
		/// </summary>
		/// <param name="timeSpan">The time that has to elapse before the function returns.</param>
		public static void Wait(TimeSpan timeSpan)
		{
			Thread.Sleep(timeSpan);
		}
		/// <summary>
		/// Blocks until the predicate returns true.
		/// </summary>
		/// <param name="predicate">Callback that controls when the wait returns.</param>
		/// <param name="predicateCooldown">The amount of time to wait inbetween calls to the predicate function.</param>
		public static void Wait(Func<bool> predicate, TimeSpan predicateCooldown)
		{
			while (!predicate())
			{
				Wait(predicateCooldown);
			}
		}
	}
}