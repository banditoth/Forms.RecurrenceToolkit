using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace banditoth.Forms.RecurrenceToolkit.Logging.Interfaces
{
	public interface ILogger
	{
		void LogTrace(string traceMessage, string callerMethod, string filePath);

		void LogTrace(StackTrace trace, string callerMethod, string filePath);

		void LogDebug(string debugMessage, string callerMethod, string filePath);

		void LogInformation(string informationMessage, string callerMethod, string filePath);

		void LogWarning(string warningMessage, string callerMethod, string filePath);

		void LogError(string errorMessage, string callerMethod, string filePath);

		void LogCritical(string criticalMessage, string callerMethod, string filePath);

		void LogException(Exception exception, string message, string callerMethod, string filePath);
	}
}
