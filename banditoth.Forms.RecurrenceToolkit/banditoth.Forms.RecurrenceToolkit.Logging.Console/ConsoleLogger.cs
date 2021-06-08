using banditoth.Forms.RecurrenceToolkit.Logging.Entities;
using System;
using System.Diagnostics;
using System.IO;

namespace banditoth.Forms.RecurrenceToolkit.Logging.Console
{
	public class ConsoleLogger : BaseLogger
	{
		public ConsoleLogger() : base(new LoggerOptions()
		{
			IncludeCallerSourceFullFileName = true,
			IncludeCallerSourceShortFileName = false,
			ExceptionLevel = Enumerations.LogLevel.Error,
			IncludeCallerMethodName = true
		})
		{

		}

		private string GetCallPointBySettings(string filePath, string callerMethod)
		{
			string result = null;

			if (LoggerOptions.IncludeCallerMethodName)
			{
				result += callerMethod;
			}

			if (LoggerOptions.IncludeCallerSourceFullFileName || LoggerOptions.IncludeCallerSourceShortFileName)
			{
				result += " at ";
				result += LoggerOptions.IncludeCallerSourceFullFileName ? filePath : (LoggerOptions.IncludeCallerSourceShortFileName ? Path.GetFileName(filePath) : null);
			}

			return string.IsNullOrWhiteSpace(result) ? null : result;
		}

		public override void LogCritical(string criticalMessage, string callerMethod, string filePath)
		{
			System.Console.WriteLine($"[CRITICAL][{DateTime.Now}][{GetCallPointBySettings(filePath, callerMethod)}][{criticalMessage}]");
		}

		public override void LogDebug(string debugMessage, string callerMethod, string filePath)
		{
			System.Console.WriteLine($"[DEBUG][{DateTime.Now}][{GetCallPointBySettings(filePath, callerMethod)}][{debugMessage}]");
		}

		public override void LogError(string errorMessage, string callerMethod, string filePath)
		{
			System.Console.WriteLine($"[ERROR][{DateTime.Now}][{GetCallPointBySettings(filePath, callerMethod)}][{errorMessage}]");
		}

		public override void LogException(Exception ex, string message, string callerMethod, string filePath)
		{
			string formattedMessage = $"{message} -> {ex}";
			switch (LoggerOptions.ExceptionLevel)
			{
				case Enumerations.LogLevel.Trace:
					LogTrace(formattedMessage, callerMethod, filePath);
					break;
				case Enumerations.LogLevel.Debug:
					LogDebug(formattedMessage, callerMethod, filePath);
					break;
				case Enumerations.LogLevel.Information:
					LogInformation(formattedMessage, callerMethod, filePath);
					break;
				case Enumerations.LogLevel.Warning:
					LogWarning(formattedMessage, callerMethod, filePath);
					break;
				case Enumerations.LogLevel.Error:
					LogError(formattedMessage, callerMethod, filePath);
					break;
				case Enumerations.LogLevel.Critical:
					LogCritical(formattedMessage, callerMethod, filePath);
					break;
				default:
					throw new Exception("Unknown exception level: " + LoggerOptions.ExceptionLevel);
			}
		}

		public override void LogInformation(string informationMessage, string callerMethod, string filePath)
		{
			System.Console.WriteLine($"[INFORMATION][{DateTime.Now}][{GetCallPointBySettings(filePath, callerMethod)}][{informationMessage}]");
		}

		public override void LogTrace(string traceMessage, string callerMethod, string filePath)
		{
			System.Console.WriteLine($"[TRACE][{DateTime.Now}][{GetCallPointBySettings(filePath, callerMethod)}][{traceMessage}]");
		}

		public override void LogTrace(StackTrace trace, string callerMethod, string filePath)
		{
			System.Console.WriteLine($"[TRACE][{DateTime.Now}][{GetCallPointBySettings(filePath, callerMethod)}][{trace}]");
		}

		public override void LogWarning(string warningMessage, string callerMethod, string filePath)
		{
			System.Console.WriteLine($"[WARNING][{DateTime.Now}][{GetCallPointBySettings(filePath, callerMethod)}][{warningMessage}]");
		}
	}
}
