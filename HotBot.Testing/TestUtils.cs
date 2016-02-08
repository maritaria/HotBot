using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
	}
}