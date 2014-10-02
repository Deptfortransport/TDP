// *********************************************** 
// NAME                 : TestStubTravelNewsHandler.cs 
// AUTHOR               : Murat Guney
// DATE CREATED         : 17/02/2006
// DESCRIPTION  : Class that makes travel news available to clients
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNews/TestStubTravelNewsHandler.cs-arc  $
//
//   Rev 1.2   Sep 01 2011 10:44:00   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.1   Sep 29 2009 11:44:18   RBroddle
//CCN 485a Travel News Parts 3 and 4 Hierarchy & Roadworks.
//Resolution for 5321: Travel News Parts 3 and 4 Hierarchy & Roadworks
//
//   Rev 1.0   Nov 08 2007 12:50:26   mturner
//Initial revision.
//
//   Rev 1.1   Mar 28 2006 11:08:58   build
//Automatically merged from branch for stream0024
//
//   Rev 1.0.1.0   Mar 03 2006 16:59:04   AViitanen
//Updated refs to IntVerySevere and VerySevere. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.0   Feb 21 2006 10:02:28   mguney
//Initial revision.



using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.TravelNewsInterface;




namespace TransportDirect.UserPortal.TravelNews
{
	/// <summary>
	/// This class should not be used/instanciated directly, only use the copy in the TDServiceDiscovery.
	/// </summary>
	[Serializable()]
	[CLSCompliant(false)]
	public class TestStubTravelNewsHandler : ITravelNewsHandler
	{
		private DataTable dataTableGrid;		
		private HeadlineItem[] headlines;
		private bool travelNewsAvailable = false;
		private string travelNewsUnavailableText = string.Empty;

		private static readonly string STUBALLDATAFILE = "\\travelnews\\StubTravelNewsAll.xml";		

		#region Contructor
		/// <summary>
		/// Get the data and store in this class instance
		/// </summary>
		public TestStubTravelNewsHandler()
		{
			string stubDataFile = (Directory.GetCurrentDirectory().IndexOf("\\debug") > 0) ?
				Directory.GetCurrentDirectory() + STUBALLDATAFILE :
				Directory.GetCurrentDirectory() + "\\debug" + STUBALLDATAFILE;
			if (!File.Exists(stubDataFile))
				throw new FileNotFoundException(stubDataFile + " doesn't exist.");
			//if (!File.Exists(STUBHEADLINESDATAFILE))
			//	throw new FileNotFoundException(STUBHEADLINESDATAFILE + " doesn't exist.");

			DataSet dataSet = new DataSet();
			//use "TravelNewsHeadlines" to return data
			dataSet.ReadXml(stubDataFile);
			
			dataTableGrid = dataSet.Tables[0];


			DataTable dataTableHeadlines = dataTableGrid.Copy();
			for (int i=dataTableHeadlines.Columns.Count-1;i >= 0; i--)
			{
				string columnName = dataTableHeadlines.Columns[i].ColumnName;
				if (!columnName.Equals(Keys.uidColumn) &&
					!columnName.Equals(Keys.severityLevelColumn) &&
					!columnName.Equals(Keys.headlineTextColumn) &&
					!columnName.Equals(Keys.modeOfTransportColumn) &&
					!columnName.Equals(Keys.regionsColumn))
				{
					dataTableHeadlines.Columns.RemoveAt(i);
				}
			}
			DataColumn startToNowMinDiff = new DataColumn("StartToNowMinDiff",typeof(int),"941936");
			dataTableHeadlines.Columns.Add(startToNowMinDiff);
			

			//DataTable dataTableHeadlines = dataSet.Tables[0];
			DataRow[] rows = dataTableHeadlines.Select();
			headlines = new HeadlineItem[rows.Length];
			int rowIndex = 0;
			foreach ( DataRow row in rows)
			{
				headlines[rowIndex++] = BuildHeadlineItem(row);
			}			

			if (dataTableGrid.Rows.Count == 1 && (string)dataTableGrid.Rows[0][Keys.uidColumn] == Values.UidUnavailable)
			{
				travelNewsUnavailableText = (string)dataTableGrid.Rows[0][Keys.headlineTextColumn];
			}
			else
			{
				travelNewsAvailable = true;
			}
		}


		#endregion

		#region Public Methods

		/// <summary>
		/// Returns whether the travel news is available
		/// </summary>
		public bool IsTravelNewsAvaliable
		{
			get
			{
				return travelNewsAvailable;
			}
		}

		/// <summary>
		/// Returns the travel news unavailable text
		/// </summary>
		public string TravelNewsUnavailableText
		{
			get
			{
				return travelNewsUnavailableText;
			}
		}

        /// <summary>
        /// Returns children of given travel news ID
        /// </summary>
        /// <param name="uid">ID of parent to find children for</param>
        /// <returns></returns>
        public TravelNewsItem[] GetChildrenDetailsByUid(string uid)
        {
            string filterExpression = Keys.IncidentParentColumn + " = '" + uid + "'";
            DataRow[] rows = dataTableGrid.Select(filterExpression);
            ArrayList tnItems = new ArrayList();

            foreach (DataRow row in rows)
            {
                TravelNewsItem tempNewsItem = BuildTravelNewsItem(row);
                if (tempNewsItem != null)
                {
                    tnItems.Add(tempNewsItem);
                }
            }
            return (TravelNewsItem[])tnItems.ToArray(typeof(TravelNewsItem));
        }


		/// <summary>
		/// Returns headline data with default filtering set to all severe ones
		/// </summary>
		/// <returns></returns>
		public HeadlineItem[] GetHeadlines()
		{
			return headlines;
		}

		/// <summary>
		/// Returns headline data with given filtering 
		/// </summary>
		/// <returns></returns>
		public HeadlineItem[] GetHeadlines(TravelNewsState travelNewsState)
		{
			DataRow[] rows = dataTableGrid.Select();//GetFilterExpression(travelNewsState), Keys.severityLevelColumn +" ASC, " +Keys.startDateTimeColumn +" DESC");


			HeadlineItem[] hlItems = new HeadlineItem[rows.Length];

			int i=0;
			foreach (DataRow row in rows)
			{
				hlItems[i++] = BuildHeadlineItem(row);
			}

			return hlItems;
		}

		/// <summary>
		/// Returns filtered/sorted data grid data
		/// </summary>
		/// <param name="travelNewsState">Current display settings (drop-down values)</param>
		/// <returns></returns>
		public TravelNewsItem[] GetDetails(TravelNewsState travelNewsState)
		{
			DataRow[] rows = dataTableGrid.Select();//GetFilterExpression(travelNewsState), Keys.severityLevelColumn +" ASC, " +Keys.startDateTimeColumn +" DESC");

			TravelNewsItem[] tnItems = new TravelNewsItem[rows.Length];

			int i=0;
			foreach (DataRow row in rows)
			{
				tnItems[i++] = BuildTravelNewsItem(row);
			}

			return tnItems;

		}

		/// <summary>
		/// Ret
		/// </summary>
		/// <param name="travelNewsState">Current display settings (drop-down values)</param>
		/// <returns></returns>
		public TravelNewsItem GetDetailsByUid(string uid)
		{
			string filterExpression = Keys.uidColumn +" = '" +uid +"'";
			DataRow[] rows = dataTableGrid.Select(filterExpression);

			// create an item if 1 row returned. Otherwise item set to Nul;
			TravelNewsItem item = (rows.Length == 1)? BuildTravelNewsItem(rows[0]) : null;

			

			return item;

		}

        /// <summary>
        /// Dummy method returning null - Gets the travel news items affected by the toids list supplied
        /// </summary>
        /// <param name="toidsList"></param>
        /// <returns></returns>
        public TravelNewsItem[] GetTravelNewsByAffectedToids(string[] toidsList)
        {
            return null;
        }

	#endregion

		#region Private Methods

		/// <summary>
		/// Build a new TravelNewsItem from a given DataRow.
		/// </summary>
		/// <param name="headlineRow">DataRow containing Travel news info. This object must have a specific compatible format with appropriate column name and type</param>
		/// <returns>a new populated TravelNewsItem</returns>
		private TravelNewsItem BuildTravelNewsItem( DataRow travelNewsRow)
		{

			TravelNewsItem item = new TravelNewsItem();
			try
			{
				item.Uid =						(string)travelNewsRow[Keys.uidColumn];
				item.SeverityLevel =			Parsing.ParseSeverityLevel(Convert.ToByte(travelNewsRow[Keys.severityLevelColumn]));
				item.SeverityDescription =		(string)travelNewsRow[Keys.severityDescriptionColumn];
				if (travelNewsRow[Keys.publicTransportOperatorColumn] != DBNull.Value)
					item.PublicTransportOperator =	(string)travelNewsRow[Keys.publicTransportOperatorColumn];
				item.Operator =					(string)travelNewsRow[Keys.operatorColumn];
				item.ModeOfTransport =			(string)travelNewsRow[Keys.modeOfTransportColumn];
				if (travelNewsRow[Keys.regionsColumn] != DBNull.Value)
					item.Regions =					(string)travelNewsRow[Keys.regionsColumn];
				item.Location =					(string)travelNewsRow[Keys.locationColumn];
				item.RegionsLocation =			(string)travelNewsRow[Keys.regionsLocationColumn];
				item.IncidentType =				(string)travelNewsRow[Keys.incidentTypeColumn];
				item.HeadlineText =				(string)travelNewsRow[Keys.headlineTextColumn];
				item.DetailText =				(string)travelNewsRow[Keys.detailTextColumn];
				item.IncidentStatus =			(string) travelNewsRow[Keys.incidentStatusColumn];
				
				decimal tempEast = Convert.ToDecimal(travelNewsRow[Keys.eastingColumn]);
				decimal tempNorth = Convert.ToDecimal(travelNewsRow[Keys.northingColumn]);
				item.Easting =			(int)tempEast;
				item.Northing = (int)tempNorth;
				
				item.ReportedDateTime =		Convert.ToDateTime(travelNewsRow[Keys.reportedDateTimeColumn]);
				item.StartDateTime =			Convert.ToDateTime(travelNewsRow[Keys.startDateTimeColumn]);
				item.StartToNowMinDiff =		Convert.ToInt32(travelNewsRow[Keys.startToNowMinDiffColumn]);
				item.LastModifiedDateTime =	Convert.ToDateTime(travelNewsRow[Keys.lastModifiedDateTimeColumn]);
				if (travelNewsRow[Keys.clearedDateTimeColumn] != DBNull.Value)
					item.ClearedDateTime =			Convert.ToDateTime(travelNewsRow[Keys.clearedDateTimeColumn]);
				if (travelNewsRow[Keys.expiryDateTimeColumn] != DBNull.Value)
					item.ExpiryDateTime =			Convert.ToDateTime(travelNewsRow[Keys.expiryDateTimeColumn]);

			
			}

			catch (InvalidCastException)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Error,
					"At least one column name in the TravelNews DB hasn't got a consistent type with the one expected by the portal. Check these types.");
				Logger.Write(oe);
			}
			catch (IndexOutOfRangeException)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Error,
					"At least one column name in the TravelNews DB is not consistent with the ones expected by the portal. Check these column names.");
				Logger.Write(oe);
			}

			catch (Exception exc)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Error,
					string.Format("An exception was thrown while building a TravelNewsItem. Reason:",exc.Message));
				Logger.Write(oe);
					
			}
			
			return item;
			
		}

		/// <summary>
		/// Returns an array with Delay Type filters applicable for the given severity and timespan
		/// </summary>
		/// <param name="severityValue">severity level</param>
		/// <param name="startToNowMinDiffValue">difference in minutes between</param>
		/// <returns></returns>
		private DelayType[] GetDelayTypes(byte severityValue, int startToNowMinDiffValue)
		{
			bool isSevere = false;
			bool isRecent = false;

			
			if ( ((int)severityValue) < Convert.ToInt32(Enum.Format(typeof(SeverityLevel), SeverityLevel.VerySevere, "d")))
				isSevere = true;

			
			if ( startToNowMinDiffValue < Values.IntRecentCritera)
				isRecent = true;
			

			if (isSevere && isRecent)
			{
				return new DelayType[]{DelayType.Major, DelayType.Recent};
			}
			if (isSevere)
			{
				return new DelayType[]{DelayType.Major};
			}
			if (isRecent)
			{
				return new DelayType[]{DelayType.Recent};
			}

			return new DelayType[0];
				
		}



		/// <summary>
		/// Build a new HeadlineItem from a given DataRow.
		/// </summary>
		/// <param name="headlineRow">DataRow containing headlines info. This object must have a specific compatible format with appropriate column name and type</param>
		/// <returns>a new populated HeadlineItem</returns>
		private HeadlineItem BuildHeadlineItem(DataRow headlineRow)
		{
			HeadlineItem item = new HeadlineItem();
			
			try
			{
				item.Uid = (string)headlineRow[Keys.uidColumn];
				item.HeadlineText = (string) headlineRow[Keys.headlineTextColumn];
				item.SeverityLevel = Parsing.ParseSeverityLevel((byte)headlineRow[Keys.severityLevelColumn]);
				// if value of ModeOfTransport column is equal to road, then assign TransportType road, otherwise assign Public transport
				item.TransportType = ((string)headlineRow[Keys.modeOfTransportColumn] == Values.Road)? TransportType.Road : TransportType.PublicTransport;
				item.Regions = (string)headlineRow[Keys.regionsColumn];
				item.DelayTypes = GetDelayTypes((byte)headlineRow[Keys.severityLevelColumn], (int)headlineRow[Keys.startToNowMinDiffColumn]);
			}
			catch (InvalidCastException)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Error,
					"At least one column name in the TravelNews DB hasn't got a consistent type with the one expected by the portal. Check these types.");
				Logger.Write(oe);
			}
			catch (IndexOutOfRangeException)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Error,
					"At least one column name in the TravelNews DB is not consistent with the ones expected by the portal. Check these column names.");
				Logger.Write(oe);
			}

			catch (Exception exc)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Error,
					string.Format("An exception was thrown while building a HeadlineItem. Reason:",exc.Message));
				Logger.Write(oe);
					
			}			
			return item;			
		}

		/// <summary>
		/// Builds and returns the filter expression to filter the grid data table
		/// </summary>
		private string GetFilterExpression(TravelNewsState travelNewsState)
		{
			string[] filters = new String[4];
			int filterCount = 0;
			string filterExpression = string.Empty;

			switch (travelNewsState.SelectedTransport)
			{
				
				case (TransportType.All):
					break;
				case (TransportType.PublicTransport):
					filters[filterCount] = Keys.modeOfTransportColumn +" <> '" +Values.Road +"'";
					filterCount++;
					break;
				default:
					filters[filterCount] = Keys.modeOfTransportColumn +" = '" + Converting.ToString(travelNewsState.SelectedTransport) + "'";
					filterCount++;
					break;
			}

			switch (travelNewsState.SelectedRegion)
			{
				case (""):
				case ("All"):
					break;
				default:
					filters[filterCount] = Keys.regionsColumn +" LIKE '%" + travelNewsState.SelectedRegion + "%'";
					filterCount++;
					break;
			}

			switch (travelNewsState.SelectedDelays)
			{
				case (DelayType.Major):
					filters[filterCount] = Keys.severityLevelColumn +" < " + Convert.ToInt32(Enum.Format(typeof(SeverityLevel), SeverityLevel.VerySevere, "d"));
					filterCount++;
					break;
				case (DelayType.Recent):
					filters[filterCount] = Keys.startToNowMinDiffColumn +" < " +Values.RecentCriteria;
					filterCount++;
					break;
			}

			//filter for travelNewsState.SelectedDate
			if (travelNewsState.SelectedDate != null)
			{

				// CR - IR 2783 - Fix to display incidents contained within the day - added times to comparison strings
				//incidents should be selected if the selected date is greater than the start date but
				//less than expiry date
				filters[filterCount] = Keys.startDateTimeColumn + " <= " + "'" + travelNewsState.SelectedDate.ToString("dd/MM/yy") + " 23:59:59'" +
				" AND " + Keys.expiryDateTimeColumn + " >= " + "'" + travelNewsState.SelectedDate.ToString("dd/MM/yy")+ " 00:00:00'" ;
				filterCount++;						
				// end IR 2783
			}

			for (int i = 0; i < filterCount; i++)
			{
				filterExpression += filters[i];

				if (i < filterCount - 1)
					filterExpression += " AND ";
			}

			return filterExpression;
		}

		#endregion

	}
}
