using System;

namespace TransportDirect.ReportDataProvider.DatabasePublishers
{
	/// <summary>
	/// Summary description for SqlTypeHelper.
	/// </summary>
	public class SqlTypeHelper
	{
		public static readonly DateTime MinDateTime = new DateTime(1753, 1, 1,12, 0, 0);
		public static readonly DateTime MaxDateTime = new DateTime(9999,12,31,23,59,59);
		private SqlTypeHelper()
		{
			
		}
		public static bool IsSqlDateTimeCompatible(DateTime dateTime)
		{
			

			if (dateTime >= MinDateTime && dateTime <= MaxDateTime)
				return true;
			else
				return false;
		}

	}



}
