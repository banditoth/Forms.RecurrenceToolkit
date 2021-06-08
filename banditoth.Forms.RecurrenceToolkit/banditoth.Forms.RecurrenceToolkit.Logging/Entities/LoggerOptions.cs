using banditoth.Forms.RecurrenceToolkit.Logging.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace banditoth.Forms.RecurrenceToolkit.Logging.Entities
{
	public class LoggerOptions
	{
		public bool IncludeCallerMethodName { get; set; } = true;

		public bool IncludeCallerSourceFullFileName { get; set; } = false;

		public bool IncludeCallerSourceShortFileName { get; set; } = true;

		public LogLevel ExceptionLevel { get; set; } = LogLevel.Error;
	}
}
