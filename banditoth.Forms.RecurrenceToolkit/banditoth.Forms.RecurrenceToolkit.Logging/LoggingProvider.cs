using banditoth.Forms.RecurrenceToolkit.Logging.Entities;
using banditoth.Forms.RecurrenceToolkit.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace banditoth.Forms.RecurrenceToolkit.Logging
{
	public static class LoggingProvider
	{
		private static BaseLogger[] _loggers = null;

		public static void Initalize(params BaseLogger[] loggers)
		{
			_loggers = loggers;
		}

		private static void TryExecuteLoggingMethod(Action loggingMethod)
		{
			try
			{
				loggingMethod.Invoke();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Logging failed, throwed an exception: {ex}");
			}
		}

		public static void LogCritical(string criticalMessage, [CallerMemberName] string callerMethod = null, [CallerFilePath] string filePath = null)
		{
			if (_loggers != null)
			{
				foreach (BaseLogger logger in _loggers)
				{
					TryExecuteLoggingMethod(() => logger.LogCritical(criticalMessage, callerMethod, filePath));
				}
			}
		}

		public static void LogDebug(string debugMessage, [CallerMemberName] string callerMethod = null, [CallerFilePath] string filePath = null)
		{
			if (_loggers != null)
			{
				foreach (BaseLogger logger in _loggers)
				{
					TryExecuteLoggingMethod(() => logger.LogDebug(debugMessage, callerMethod, filePath));
				}
			}
		}

		public static void LogError(string errorMessage, [CallerMemberName] string callerMethod = null, [CallerFilePath] string filePath = null)
		{
			if (_loggers != null)
			{
				foreach (BaseLogger logger in _loggers)
				{
					TryExecuteLoggingMethod(() => logger.LogError(errorMessage, callerMethod, filePath));
				}
			}
		}

		public static void LogException(Exception exception, string message, [CallerMemberName] string callerMethod = null, [CallerFilePath] string filePath = null)
		{
			if (_loggers != null)
			{
				foreach (BaseLogger logger in _loggers)
				{
					TryExecuteLoggingMethod(() => logger.LogException(exception, message, callerMethod, filePath));
				}
			}
		}

		public static void LogInformation(string informationMessage, [CallerMemberName] string callerMethod = null, [CallerFilePath] string filePath = null)
		{
			if (_loggers != null)
			{
				foreach (BaseLogger logger in _loggers)
				{
					TryExecuteLoggingMethod(() => logger.LogInformation(informationMessage, callerMethod, filePath));
				}
			}
		}

		public static void LogTrace(string traceMessage, [CallerMemberName] string callerMethod = null, [CallerFilePath] string filePath = null)
		{
			if (_loggers != null)
			{
				foreach (BaseLogger logger in _loggers)
				{
					TryExecuteLoggingMethod(() => logger.LogTrace(traceMessage, callerMethod, filePath));
				}
			}
		}

		public static void LogTrace(StackTrace trace, [CallerMemberName] string callerMethod = null, [CallerFilePath] string filePath = null)
		{
			if (_loggers != null)
			{
				foreach (BaseLogger logger in _loggers)
				{
					TryExecuteLoggingMethod(() => logger.LogTrace(trace, callerMethod, filePath));
				}
			}
		}

		public static void LogWarning(string warningMessage, [CallerMemberName] string callerMethod = null, [CallerFilePath] string filePath = null)
		{
			if (_loggers != null)
			{
				foreach (BaseLogger logger in _loggers)
				{
					TryExecuteLoggingMethod(() => logger.LogWarning(warningMessage, callerMethod, filePath));
				}
			}
		}
	}
}
