using banditoth.Forms.RecurrenceToolkit.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace banditoth.Forms.RecurrenceToolkit.Logging.Entities
{
	public abstract class BaseLogger : ILogger
	{
		public LoggerOptions LoggerOptions { get; private set; }

		public BaseLogger(LoggerOptions loggerOptions)
		{
			if (loggerOptions == null)
				throw new ArgumentNullException(nameof(loggerOptions));

			LoggerOptions = loggerOptions;
		}

		public abstract void LogTrace(string traceMessage, string callerMethod, string filePath);

		public abstract void LogTrace(StackTrace trace, string callerMethod, string filePath);

		public abstract void LogDebug(string debugMessage, string callerMethod, string filePath);

		public abstract void LogInformation(string informationMessage, string callerMethod, string filePath);

		public abstract void LogWarning(string warningMessage, string callerMethod, string filePath);

		public abstract void LogError(string errorMessage, string callerMethod, string filePath);

		public abstract void LogCritical(string criticalMessage, string callerMethod, string filePath);

		public abstract void LogException(Exception ex, string message, string callerMethod, string filePath);
	}
}
