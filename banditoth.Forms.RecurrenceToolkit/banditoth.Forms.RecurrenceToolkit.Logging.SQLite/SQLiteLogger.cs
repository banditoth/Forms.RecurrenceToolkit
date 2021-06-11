using banditoth.Forms.RecurrenceToolkit.Logging.Entities;
using banditoth.Forms.RecurrenceToolkit.Logging.SQLite.Entities;
using SQLite;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace banditoth.Forms.RecurrenceToolkit.Logging.SQLite
{
	public class SQLiteLogger : BaseLogger
	{
		public readonly string DefaultLoggingDatabaseFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Logs.db3");

		SQLiteConnection _databaseConnection = null;

		public SQLiteLogger(SQLiteConnection databaseConnection = null) : base(new LoggerOptions()
		{
			IncludeCallerSourceFullFileName = false,
			IncludeCallerSourceShortFileName = true,
			IncludeCallerMethodName = true,
			ExceptionLevel = Enumerations.LogLevel.Error
		})
		{
			if(databaseConnection == null)
			{
				databaseConnection = new SQLiteConnection(DefaultLoggingDatabaseFilePath);
			}

			_databaseConnection = databaseConnection;
			_databaseConnection.CreateTable<Log>();
		}

		private void InsertLog(Log logToInsert)
		{
			if (logToInsert == null)
				return;

			_databaseConnection.Insert(logToInsert);
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
			InsertLog(new Log()
			{
				Date = DateTime.Now.ToUniversalTime(),
				Level = Enumerations.LogLevel.Critical,
				Message = $"[{GetCallPointBySettings(filePath, callerMethod)}][{criticalMessage}]"
			});
		}

		public override void LogDebug(string debugMessage, string callerMethod, string filePath)
		{
			InsertLog(new Log()
			{
				Date = DateTime.Now.ToUniversalTime(),
				Level = Enumerations.LogLevel.Debug,
				Message = $"[{GetCallPointBySettings(filePath, callerMethod)}][{debugMessage}]"
			});
		}

		public override void LogError(string errorMessage, string callerMethod, string filePath)
		{
			InsertLog(new Log()
			{
				Date = DateTime.Now.ToUniversalTime(),
				Level = Enumerations.LogLevel.Error,
				Message = $"[{GetCallPointBySettings(filePath, callerMethod)}][{errorMessage}]"
			});
		}

		public override void LogException(Exception ex, string message, string callerMethod, string filePath)
		{
			InsertLog(new Log()
			{
				Date = DateTime.Now.ToUniversalTime(),
				Level = LoggerOptions.ExceptionLevel,
				Message = $"[{GetCallPointBySettings(filePath, callerMethod)}][{message}][{ex}]"
			});
		}

		public override void LogInformation(string informationMessage, string callerMethod, string filePath)
		{
			InsertLog(new Log()
			{
				Date = DateTime.Now.ToUniversalTime(),
				Level = Enumerations.LogLevel.Information,
				Message = $"[{GetCallPointBySettings(filePath, callerMethod)}][{informationMessage}]"
			});
		}

		public override void LogTrace(string traceMessage, string callerMethod, string filePath)
		{
			InsertLog(new Log()
			{
				Date = DateTime.Now.ToUniversalTime(),
				Level = Enumerations.LogLevel.Trace,
				Message = $"[{GetCallPointBySettings(filePath, callerMethod)}][{traceMessage}]"
			});
		}

		public override void LogTrace(StackTrace trace, string callerMethod, string filePath)
		{
			InsertLog(new Log()
			{
				Date = DateTime.Now.ToUniversalTime(),
				Level = Enumerations.LogLevel.Trace,
				Message = $"[{GetCallPointBySettings(filePath, callerMethod)}][{trace}]"
			});
		}

		public override void LogWarning(string warningMessage, string callerMethod, string filePath)
		{
			InsertLog(new Log()
			{
				Date = DateTime.Now.ToUniversalTime(),
				Level = Enumerations.LogLevel.Warning,
				Message = $"[{GetCallPointBySettings(filePath, callerMethod)}][{warningMessage}]"
			});
		}
	}
}
