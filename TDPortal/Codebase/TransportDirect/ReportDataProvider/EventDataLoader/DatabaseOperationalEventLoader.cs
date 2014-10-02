// *********************************************** 
// NAME                 : DatabaseOperationalEventLoader.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 01/07/2004
// DESCRIPTION  : Class which loads operational events
// from a database sink
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventDataLoader/DatabaseOperationalEventLoader.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:38:24   mturner
//Initial revision.
//
//   Rev 1.6   Mar 08 2006 10:58:44   CRees
//Fix for IR 2240, Vantive 3613481 - Events returned in incorrect order from DB; added sort by id to the sql query.
//
//   Rev 1.5   Jul 12 2004 11:40:12   jgeorge
//Bug fix
//
//   Rev 1.4   Jul 12 2004 10:34:10   jgeorge
//Modifications to the way filtering works
//
//   Rev 1.3   Jul 02 2004 10:07:08   jgeorge
//Updated to fix bugs highlighted by unit testing
//
//   Rev 1.2   Jul 01 2004 17:15:46   jgeorge
//Interim check-in
//
//   Rev 1.1   Jul 01 2004 15:58:00   jgeorge
//Work in progress
//
//   Rev 1.0   Jul 01 2004 14:58:56   jgeorge
//Initial revision.

using System;
using System.Text;
using System.Collections;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.EventDataLoader
{
	/// <summary>
	/// Class which loads operational events from a database sink
	/// </summary>
	public class DatabaseOperationalEventLoader : IOperationalEventLoader
	{
		#region Constant strings

		// Stored procedure name
		// IR 2240 / Vantive 3613481 - Added sort by ID column to resolve problem where events are returned out of order
		private static readonly string RetrieveOperationalEventsSql= "select {0} Id, SessionId, Message, MachineName, AssemblyName, MethodName, TypeName, Level, Category, Target, TimeLogged from OperationalEvent {1} order by TimeLogged, Id asc";
		// end IR 2240

		// Column indexes
		private static readonly int DataColumnSessionId = 1;
		private static readonly int DataColumnMessage = 2;
		private static readonly int DataColumnMachineName = 3;
		private static readonly int DataColumnAssemblyName = 4;
		private static readonly int DataColumnMethodName = 5;
		private static readonly int DataColumnTypeName = 6;
		private static readonly int DataColumnLevel = 7;
		private static readonly int DataColumnCategory = 8;
		private static readonly int DataColumnTimeLogged = 10;

		#endregion

		private static int maxNoOfEvents = -1;

		private SqlHelperDatabase dataSource;

		#region Constructor

		/// <summary>
		/// Default constructor. The name of the database containing the OperationalEvents
		/// table and stored procedure to read this data must be supplied.
		/// </summary>
		/// <param name="dataSource"></param>
		public DatabaseOperationalEventLoader(SqlHelperDatabase dataSource)
		{
			this.dataSource = dataSource;
		}

		#endregion

		#region Implementation of IOperationalEventLoader

		/// <summary>
		/// Loads all events from the database
		/// </summary>
		/// <returns></returns>
		public LoadedOperationalEvent[] GetEvents()
		{
			return GetEvents(null);
		}

		/// <summary>
		/// Loads events and filters them using the specified filters and matching method
		/// </summary>
		/// <param name="filters"></param>
		/// <param name="filterMethod"></param>
		/// <returns></returns>
		public LoadedOperationalEvent[] GetEvents(OperationalEventFilter filter)
		{
			ArrayList loadedData = new ArrayList();
			SqlHelper helper = new SqlHelper();
			try
			{
				helper.ConnOpen(dataSource);
				
				string top = string.Empty;
				if (MaxNoOfEvents > 0)
					top = "TOP " + MaxNoOfEvents.ToString();
				string where = ConvertFiltersToSql(filter);
				string sql = string.Format(RetrieveOperationalEventsSql, top, where.Length == 0 ? "" : "WHERE " + where);

				SqlDataReader reader = helper.GetReader(sql);
	
				// Read data into an arraylist
				while (reader.Read())
					loadedData.Add(ReadDataRow(reader));

			}
			finally
			{
				if (helper.ConnIsOpen)
					helper.ConnClose();
			}

			return (LoadedOperationalEvent[])loadedData.ToArray(typeof(LoadedOperationalEvent));
		}

		#endregion

		#region Properties

		public static int MaxNoOfEvents
		{
			get
			{
				if (maxNoOfEvents == -1)
				{
					// Retrieve the value from the properties service
					string retrievedValue = Properties.Current["EventDataLoader.DatabaseOperationalEventLoader.MaxEventsToLoad"];
					if (retrievedValue != null && retrievedValue.Length != 0)
						maxNoOfEvents = Convert.ToInt32(retrievedValue);
					else
						maxNoOfEvents = 0;
				}
				return maxNoOfEvents;
			}
		}

		#endregion

		#region Helper methods

		/// <summary>
		/// Reads data from the current row of the SqlDataReader and uses
		/// it to create a LoadedOperationalEvent
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public LoadedOperationalEvent ReadDataRow(SqlDataReader reader)
		{
			// Variables to hold the loaded data temporarily
			object[] data = new object[reader.FieldCount];
			SqlDateTime time;
			SqlString sessionId;
			SqlString message;
			SqlString category;
			SqlString level;
			SqlString machineName;
			SqlString typeName;
			SqlString methodName;
			SqlString assemblyName;

			// Call GetSqlValues to retrieve all data
			reader.GetSqlValues(data);
			time = (SqlDateTime)data[DataColumnTimeLogged];
			sessionId = (SqlString)data[DataColumnSessionId];
			message = (SqlString)data[DataColumnMessage];
			category = (SqlString)data[DataColumnCategory];
			level = (SqlString)data[DataColumnLevel];
			machineName = (SqlString)data[DataColumnMachineName];
			typeName = (SqlString)data[DataColumnTypeName];
			methodName = (SqlString)data[DataColumnMethodName];
			assemblyName = (SqlString)data[DataColumnAssemblyName];

			// Create the new event taking account of nulls in the data
			return new LoadedOperationalEvent( time.IsNull ? DateTime.MinValue : time.Value,
				sessionId.IsNull ? "" : sessionId.Value,
				message.IsNull ? "" : message.Value,
				category.IsNull ? "" : category.Value,
				level.IsNull ? "" : level.Value,
				machineName.IsNull ? "" : machineName.Value,
				typeName.IsNull ? "" : typeName.Value,
				methodName.IsNull ? "" : methodName.Value,
				assemblyName.IsNull ? "" : assemblyName.Value);
		}

		/// <summary>
		/// Method which converts the filter into a SQL string for use against the
		/// Sql server database.
		/// Marked internal for testing purposes
		/// </summary>
		/// <param name="filters"></param>
		/// <returns></returns>
		internal string ConvertFiltersToSql(OperationalEventFilter filter)
		{
			StringBuilder statement = new StringBuilder();
			if (filter == null)
				return string.Empty;
			else if (filter.Type == OperationalEventFilterType.Standard)
			{
				string preposition = string.Empty;
				if (filter.Method == OperationalEventFilterMethod.And || filter.Method == OperationalEventFilterMethod.Or)
					preposition = "";
				else
					preposition = "NOT";

				if (filter.AssemblyNames.Length != 0)
					statement.AppendFormat("({0} AssemblyName = '{1}')", preposition, FormatForSql(filter.AssemblyNames));
				
				else if (filter.Categories.Length != 0)
					statement.AppendFormat("({0} Category = '{1}')", preposition, FormatForSql(filter.Categories[0].ToString()));
				
				else if (filter.EndTime != DateTime.MaxValue || filter.StartTime != DateTime.MinValue)
					statement.AppendFormat("({0} TimeLogged BETWEEN CAST('{1}-{2}-{3}' as DateTime) AND CAST('{4}-{5}-{6}' as DateTime))", preposition, filter.StartTime.Year, filter.StartTime.Month, filter.StartTime.Day, filter.EndTime.Year, filter.EndTime.Month, filter.EndTime.Day);
				
				else if (filter.Levels.Length != 0)
					statement.AppendFormat("({0} Level = '{1}')", preposition, FormatForSql(filter.Levels[0].ToString()));
				
				else if (filter.MachineName.Length != 0)
					statement.AppendFormat("({0} MachineName = '{1}')", preposition, FormatForSql(filter.MachineName));
				
				else if (filter.MessageContains.Length != 0)
					statement.AppendFormat("({0} Message LIKE '%{1}%')", preposition, FormatForSql(filter.MessageContains));

				else if (filter.MessageEquals.Length != 0)
					statement.AppendFormat("({0} Message = '{1}')", preposition, FormatForSql(filter.MessageEquals));
				
				else if (filter.MethodName.Length != 0)
					statement.AppendFormat("({0} MethodName = '{1}')", preposition, FormatForSql(filter.MethodName));
				
				else if (filter.SessionId.Length != 0)
					statement.AppendFormat("({0} SessionId = '{1}')", preposition, FormatForSql(filter.SessionId));
				
				else if (filter.TypeName.Length != 0)
					statement.AppendFormat("({0} TypeName = '{1}')", preposition, FormatForSql(filter.TypeName));

			}
			else if (filter.Filters.Length > 0)
			{
				OperationalEventFilter[] subFilters = filter.Filters;
				statement.Append("(");

				OperationalEventFilter currFilter = subFilters[0];
				statement.Append(ConvertFiltersToSql(currFilter));
				for (int index = 1; index < subFilters.Length; index++)
				{
					currFilter = subFilters[index];
					switch (currFilter.Method)
					{
						case OperationalEventFilterMethod.And:
						case OperationalEventFilterMethod.AndNot:
							statement.Append(" AND ");
							break;
						case OperationalEventFilterMethod.Or:
						case OperationalEventFilterMethod.OrNot:
							statement.Append(" OR ");
							break;
					}
					statement.Append(ConvertFiltersToSql(currFilter));
				}

				statement.Append(")");
			}

			return statement.ToString();
		}

		/// <summary>
		/// Escape characters with special meanings
		/// Guards against errors and Sql injection attacks
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private string FormatForSql(string data)
		{
			string result = data.Replace("'", "''");
			return result;
		}


		#endregion
	}
}
