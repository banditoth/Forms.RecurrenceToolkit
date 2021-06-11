using banditoth.Forms.RecurrenceToolkit.Logging.Enumerations;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace banditoth.Forms.RecurrenceToolkit.Logging.SQLite.Entities
{
	public class Log
	{
		[AutoIncrement]
		[PrimaryKey]
		public long Id { get; set; }

		public DateTime Date { get; set; }

		public LogLevel Level { get; set; }

		public string Message { get; set; }
	}
}
