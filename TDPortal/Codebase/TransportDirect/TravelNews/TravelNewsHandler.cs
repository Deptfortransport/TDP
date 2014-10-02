// *********************************************** 
// NAME                 : TravelNewsHandler.cs 
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 29/09/2003 
// DESCRIPTION  : Class that makes travel news available
// to clients
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNews/TravelNewsHandler.cs-arc  $
//
//   Rev 1.14   May 29 2013 12:12:12   PScott
//IR 5935 -  Travel incidents don't affect planning if end date before startdate 
//Resolution for 5935: Travel incidents don't affect planning if end date before startdate
//
//   Rev 1.13   Sep 26 2011 14:50:04   mmodi
//Updated to check for Null AffectedToids value in the data table before reading and parsing to string
//Resolution for 5744: Real Time In Car - Error in web logs when viewing TravelNews page
//
//   Rev 1.12   Sep 19 2011 10:27:14   mmodi
//Test for min max daily start and end time before using
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.11   Sep 08 2011 13:10:18   apatel
//Updated to resolve the issues with printer friendly, padding for daily end date and daily end date adjustment issues
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.10   Sep 06 2011 12:03:46   mmodi
//Deleted commented out line
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.9   Sep 06 2011 11:20:40   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.8   Sep 02 2011 10:22:06   apatel
//Real time car changes
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.7   Sep 01 2011 10:44:02   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.6   Nov 03 2010 10:56:42   RBroddle
//Updated filtering to remove filter params when clicked through from critical incident. (also added some extra DBNull checks when loading travel news items in from DB)
//Resolution for 5629: Critical child incidents starting in the future not showing on click through from Critical Master.
//
//   Rev 1.5   Nov 18 2009 16:59:58   rbroddle
//Updated to accept Nulls in certain fields
//Resolution for 5339: "At least one column name in the TravelNews DB hasn't got a consistent type" fault being thrown during Travel News load
//
//   Rev 1.4   Sep 29 2009 11:44:18   RBroddle
//CCN 485a Travel News Parts 3 and 4 Hierarchy & Roadworks.
//Resolution for 5321: Travel News Parts 3 and 4 Hierarchy & Roadworks
//
//   Rev 1.3   Mar 10 2008 15:28:26   mturner
//Initial Del10 Codebase from Dev Factory
//
//  Rev DevFactory Feb 03 2008 18:00:00 mmodi
//Updated sort order for Planned incidents
//
//  Rev DevFactory Jan 11 2008 10:19:43 apatel
//  BuildTravelNewsItem updated to set new PlannedIncident property
//
//   Rev 1.0   Nov 08 2007 12:50:28   mturner
//Initial revision.
//
//  Rev DevFactory Jan 10 2008 10:38:33 apatel
//  Modification to GetDetails method to implement the new sort expression. 
//  Added new method GetSortedRows to sort the rows with new sort order as in CCN 0421
//
// Rev DevFactory Jan 08 2008 12:01:33 apatel
//Modification to GetFilterExpression so that the incident type is used in the filtering
//
//   Rev 1.25   Aug 20 2007 11:05:12   pscott
//IR 4479 - Travel News freeform text search changes
//
//   Rev 1.24   Jan 19 2007 13:41:28   build
//Automatically merged from branch for stream4329
//
//   Rev 1.23.1.1   Jan 18 2007 17:39:44   tmollart
//Modifications to search method and search word highlighting.
//Resolution for 4329: Travel News Updates and Search
//
//   Rev 1.23.1.0   Jan 12 2007 13:45:44   tmollart
//Added code to filter travel news on search phrase and also to highlight search phrase in the travel news location and headline.
//Resolution for 4329: Travel News Updates and Search
//
//   Rev 1.23   Apr 19 2006 13:31:44   mtillett
//Reapply fix for IR3575  that was lost during merge of stream0024
//Resolution for 3941: Mobile Exposed Services - GetTravelNewsDetails exception: "Cannot perform '<=' operation"
//
//   Rev 1.22   Apr 12 2006 15:31:00   AViitanen
//Amended GetDelayTypes. 
//Resolution for 3901: Travel News: error occurs in web log file when travel news first loads
//
//   Rev 1.21   Apr 10 2006 11:23:54   mtillett
//Add notes of clarification of requirements
//Resolution for 3810: Mobile: Travel News Service for South East errors
//
//   Rev 1.20   Apr 05 2006 13:38:44   AViitanen
//Increased filters size in GetFilterExpression. 
//Resolution for 3779: DN091 Travel News Updates:  Clicking on critical incident on Home Page results in server error
//
//   Rev 1.19   Mar 28 2006 11:08:58   build
//Automatically merged from branch for stream0024
//
//   Rev 1.18.1.3   Mar 15 2006 14:04:22   AViitanen
//Updated following code review. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.18.1.2   Mar 09 2006 14:43:18   AViitanen
//Amended GetFilterExpression to not display critical incidents with 'all' and 'recent' delays.
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.18.1.1   Mar 08 2006 10:44:36   AViitanen
//Amended GetFilterExpression to show incidents of severity level 3 and under, but not level 1.
//
//   Rev 1.18.1.0   Mar 03 2006 18:04:50   AViitanen
//Updates for critical incidents.
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.17   Sep 21 2005 16:52:12   CRees
//IR2783 - Fix for Travel News Table display date filter
//
//   Rev 1.16   Aug 18 2005 11:29:50   jgeorge
//Automatically merged from branch for stream2558
//
//   Rev 1.15.1.0   Jul 01 2005 11:20:50   jmorrissey
//Update to GetFilterExpression to use the user's selected date
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.15   Jan 31 2005 18:31:02   passuied
//latest changes to return needed info by Mobile UI
//
//   Rev 1.14   Jan 26 2005 16:23:06   passuied
//added handling for exceptions
//
//   Rev 1.13   Jan 26 2005 15:15:48   passuied
//added more info in headlines (regions, transporttype)
//
//   Rev 1.12   Dec 16 2004 15:26:34   passuied
//Refactoring the TravelNews component
//
//   Rev 1.11   Sep 06 2004 21:08:56   JHaydock
//Major update to travel news
//
//   Rev 1.10   Jul 20 2004 09:26:54   CHosegood
//Added recent delays combo box option and removed sort by ability on travelnews
//Resolution for 1168: Add 'recent delays' pulldown to travel news and remove the ability to sort headings
//
//   Rev 1.9   May 26 2004 10:22:50   jgeorge
//IR954 fix
//
//   Rev 1.8   Mar 12 2004 17:04:22   jmorrissey
//added error handling to DummyTravelNewsCheck method
//
//   Rev 1.7   Mar 12 2004 14:28:20   rgreenwood
//Added call to Stored Procedure in TransientPortal DB to check whether Serco Dummy file is present in TrafficAndTravelNews table
//
//   Rev 1.6   Oct 22 2003 12:07:58   JMorrissey
//updated comments
//
//   Rev 1.5   Oct 14 2003 18:10:14   JHaydock
//Update to TravelNews to allow selective display of data
//
//   Rev 1.4   Oct 14 2003 14:37:28   JMorrissey
//Added new method
//
//   Rev 1.3   Oct 13 2003 10:32:36   JMorrissey
//Updated method signatures
//
//   Rev 1.2   Oct 10 2003 16:16:48   JMorrissey
//Updated GetTravelHeadlines method. Commented out methods not complete yet.
//
//   Rev 1.1   Oct 09 2003 16:09:44   JMorrissey
//Added methods
//
//   Rev 1.0   Sep 29 2003 17:49:12   JMorrissey
//Initial Revision

using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Globalization;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.TravelNewsInterface;
using System.Collections.Generic;



namespace TransportDirect.UserPortal.TravelNews
{
	/// <summary>
	/// This class should not be used/instanciated directly, only use the copy in the TDServiceDiscovery.
	/// </summary>
	[Serializable()]
	[CLSCompliant(false)]
	public class TravelNewsHandler : ITravelNewsHandler
	{
		private DataTable dataTableGrid;
		private HeadlineItem[] headlines;
		private bool travelNewsAvailable = false;
		private string travelNewsUnavailableText = string.Empty;

		#region Contructor
		/// <summary>
		/// Get the data and store in this class instance
		/// </summary>
		public TravelNewsHandler()
		{
			SqlHelper sqlHelper = new SqlHelper();

			try
			{
				//open connection to TransientPortalDB
				sqlHelper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

				DataSet dataSet;

				//use stored procedure "TravelNewsHeadlines" to return data
				dataSet = sqlHelper.GetDataSet(Keys.travelNewsHeadlinesProc, new Hashtable());
				//store the resultant data table
				DataTable dataTableHeadlines = dataSet.Tables[0];

				DataRow[] rows = dataTableHeadlines.Select();

				headlines = new HeadlineItem[rows.Length];

				int i = 0;
				foreach ( DataRow row in rows)
				{
					headlines[i++] = BuildHeadlineItem(row);
				}

				//use stored procedure TravelNewsGrid to fill dataset
				dataSet = sqlHelper.GetDataSet(Keys.travelNewsAllProc, new Hashtable());
				//store the resultant data table
				dataTableGrid = dataSet.Tables[0];
			}
			catch (SqlException sqlEx)
			{
				OperationalEvent oe = new OperationalEvent(	TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "SQLNUM["+sqlEx.Number+"] :"+sqlEx.Message+":" );

				throw new TDException("SqlException caught : " + sqlEx.Message, sqlEx,
					false, TDExceptionIdentifier.TNSQLHelperError);
			}
			catch (TDException tdex)
			{    
				string message = "Error Calling Stored Procedure : TravelNewsHeadlines " + tdex.Message;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "TDException :" + tdex.Message + ":");

				throw new TDException(message, tdex, false, TDExceptionIdentifier.TNSQLHelperStoredProcedureFailure);
			}
			finally
			{
				//close the database connection
				sqlHelper.ConnClose();
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
		/// Returns headline data with default filtering set to all severe ones
		/// </summary>
		/// <returns></returns>
		public HeadlineItem[] GetHeadlines()
		{
			return headlines;
		}

		/// <summary>
		/// Returns headline data with given filtering using 
		/// the same filtering rules used by GetDetails, which 
		/// may differ from those used by other GetHeadlines() methods
		/// </summary>
		/// <returns></returns>
		public HeadlineItem[] GetHeadlines(TravelNewsState travelNewsState)
		{

			DataRow[] rows = dataTableGrid.Select(GetFilterExpression(travelNewsState), Keys.severityLevelColumn +" ASC, " +Keys.startDateTimeColumn +" DESC");


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
        /// 
        /// CCN 0421 modified sort expression 
        /// </summary>
        /// <remarks>
        /// CCN 0421 modified sort expression to include the following sort order
        /// Severity - showing the most severe incidents first
        /// Motorway incidents to appear before other road type and PT incidents
        /// Unplanned or planned - Unplanned incidents will be shown first
        /// Open incidents will be shown first, then closed and then pending
        /// Open and close incidents will be shown with newest one first
        /// Pending incidents will be show ascending in incident start time order - ignoring date
        /// </remarks>
        /// <param name="travelNewsState">Current display settings (drop-down values)</param>
        /// <returns></returns>
		public TravelNewsItem[] GetDetails(TravelNewsState travelNewsState)
		{
			string filterExpression = GetFilterExpression(travelNewsState);
			dataTableGrid.CaseSensitive = false;

            // Gets all the rows according to filterExpression
            DataRow[] filteredrows = dataTableGrid.Select(filterExpression);

            DataTable sortingTable = dataTableGrid.Clone();

            //importing all the filtered rows to our sortingTable
            foreach (DataRow filteredRow in filteredrows)
            {
                sortingTable.ImportRow(filteredRow);
            }

            DataRow[] rows = GetSortedRows(sortingTable); 

			ArrayList tnItems = new ArrayList();

			foreach (DataRow row in rows)
			{

				TravelNewsItem tempNewsItem = BuildTravelNewsItem(row);

				// Filter list again based on search terms
				if (travelNewsState.SearchTokens != null)
				{
					int tokenCount = travelNewsState.SearchTokens.Count;

					// Check travel news contains required search terms
					// if not then set it to null and it wont be
					// returned from this procedure
					string combinedItemDetail = tempNewsItem.Location.ToUpper() + " " + tempNewsItem.DetailText.ToUpper();
					combinedItemDetail = combinedItemDetail.Replace("(M)", "M");

					//combinedItemDetail.con
					bool fullMatch = false;
					int locatedTokens = 0;

					foreach (string token in travelNewsState.SearchTokens)
					{
						string newToken = token;
						//string newToken = token; //.Replace("(M)", "M");
						Regex wordMatch = new Regex(@"\b"+newToken+@"\b", RegexOptions.IgnoreCase);

						if (wordMatch.IsMatch(combinedItemDetail))
						{
							// We have matched a token against the text so we need to
							// see what type of token it is and replace the text in the 
							// travel news with the correct highlighted text.

							// Create regex's to match the tokens to road names
							Regex roadMatch = new Regex(@"[aA][0-9]+[mM]");

							// If the token is a road name in this format Ann(M) then we need
							// to feed a suitable regex into the match evaluator to highlight it.
							if (roadMatch.IsMatch(newToken))
							{
								// If the token is in that format then we need to modify the token
								// before feeding it into the highlight evaluator
								newToken = newToken.ToUpper().Replace("M", "\\(M\\)");
							}

							SearchTokenMatchEvaluator c = new SearchTokenMatchEvaluator();
							Regex ex = new Regex(@newToken, RegexOptions.IgnoreCase);
							MatchEvaluator evaluator = new MatchEvaluator(c.TokenHighlighter);
							string newLocation = ex.Replace(tempNewsItem.Location, evaluator);
							string newDetailText = ex.Replace(tempNewsItem.DetailText, evaluator);
							tempNewsItem.Location = newLocation;
							tempNewsItem.DetailText = newDetailText;
							locatedTokens ++;
						}
					}

					fullMatch = (locatedTokens == tokenCount);

					if (!fullMatch)
					{
						tempNewsItem = null;
					}
				}

				if (tempNewsItem != null)
				{
					tnItems.Add(tempNewsItem);
				}
			}

			return (TravelNewsItem[])tnItems.ToArray(typeof(TravelNewsItem));

		}

        /// <summary>
        /// Creates two new columns to enable sorting according to CCN 0421
        /// and returns sorted DataRows
        /// </summary>
        /// <param name="sortingTable"> Table with filtered travel news data</param>
        /// <returns>Sorted DataRows as specified in CCN 0421</returns>
        private DataRow[] GetSortedRows(DataTable sortingTable)
        {

            // Creating new columns to enable sorting on IncidentStatus and DateTime
            sortingTable.Columns.Add(new DataColumn("TimeColumn", typeof(DateTime)));
            sortingTable.Columns.Add(new DataColumn("IncidentSortOrder", typeof(int)));
            sortingTable.Columns.Add(new DataColumn("RoadTypeSortOrder", typeof(int)));

            // fill new columns with TimeColumn as todays date with no time if incident is
            // of type open or close and with the time as StartDateTime Column's time if incident is pending
            // IncidentSortOrder column will be 0 if Open, 2 if pending and 1 if closed to enable sorting in open, closed, pending order
            foreach (DataRow srow in sortingTable.Rows)
            {
                switch (srow[Keys.incidentStatusColumn].ToString().Trim())
                {
                    case "O":
                        //Even if marked as open by trafficlink still treat as pending if not actually active at current time
                        if ((bool)srow[Keys.IncidentActiveStatusColumn])
                        {
                            srow["IncidentSortOrder"] = 0;
                            srow["TimeColumn"] = DateTime.Today.Date;
                        }
                        else
                        {
                            goto case "P";
                        }
                        break;
                    case "P":
                        DateTime stDate = DateTime.Parse(srow[Keys.startDateTimeColumn].ToString());
                        srow["IncidentSortOrder"] = 2;
                        srow["TimeColumn"] = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, stDate.Hour, stDate.Minute, stDate.Second);
                        break;
                    case "C":
                        srow["IncidentSortOrder"] = 1;
                        srow["TimeColumn"] = DateTime.Today.Date;
                        break;
                }

                if (srow[Keys.roadTypeColumn].ToString().Trim().ToUpper() == "M")
                {
                    srow["RoadTypeSortOrder"] = 0;
                }
                else
                    srow["RoadTypeSortOrder"] = 1;
            }


            string sortExpression = Keys.severityLevelColumn + " ASC, RoadTypeSortOrder ASC, " + Keys.plannedIncidentColumn + " ASC, IncidentSortOrder ASC, TimeColumn ASC, " + Keys.startDateTimeColumn + " DESC";

            return sortingTable.Select(null, sortExpression);
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
        /// Returns a list of travel news items which have one or more toids matching the supplied list of toids
        /// on the date supplied
        /// </summary>
        /// <param name="toidsList">Toids needs to match with travel news toids</param>
        /// <returns>List of travel news items</returns>
        public TravelNewsItem[] GetTravelNewsByAffectedToids(string[] toidsList)
        {
            List<TravelNewsItem> affectingTravelNewsItems = new List<TravelNewsItem>();
            #region Logging variables

            string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";
            DateTime tnStartTime = DateTime.Now;
            DateTime journeyStartDateTime = DateTime.Now;
            string message = string.Empty;

            #endregion
      

            // If there are toids, build up the request for the query call
            if (toidsList.Length > 0)
            {
                #region Build XML for the query request

                string toidPrefix = Properties.Current["JourneyControl.ToidPrefix"];

                if (string.IsNullOrEmpty(toidPrefix))
                {
                    toidPrefix = string.Empty;
                }

                StringBuilder sbToidsXml = new StringBuilder();

                // Build the xml required by the query
                sbToidsXml.Append("<TOIDs>");

                foreach (string toid in toidsList)
                {
                    // Remove toid prefix if needed
                    sbToidsXml.Append(string.Format("<TOID>{0}</TOID>",
                        toid.StartsWith(toidPrefix) ?
                            toid.Substring(toidPrefix.Length, toid.Length - toidPrefix.Length) : toid));
                }

                sbToidsXml.Append("</TOIDs>");

                #endregion

                
                #region Get the affected toids/incidents

                // As this is a Prototype, go straight to the database,
                // rather than using the TravelNewsService which would be the better solution
                SqlHelper sqlHelper = new SqlHelper();

                string SP_TravelNewsToids = "TravelNewsToids";
                SqlHelperDatabase database = SqlHelperDatabase.TransientPortalDB;
                SqlDataReader sqlReader = null;

                try
                {
                    message = string.Format("TravelNewsToids - opening database connection to [{0}] and executing stored procedure[{1}].", database, SP_TravelNewsToids);
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, message));

                    // Build up the sql request
                    Hashtable parameters = new Hashtable();
                    parameters.Add("@TOIDs", sbToidsXml.ToString());

                    // Execute the query
                    sqlHelper.ConnOpen(database);

                    // Call stored procedure
                    sqlReader = sqlHelper.GetReader(SP_TravelNewsToids, parameters);

                    #region Column ordinals

                    // Assign the column ordinals
                    int ordinalIncidentId = sqlReader.GetOrdinal("UID");
                    

                    #endregion

                    while (sqlReader.Read())
                    {
                        #region Read data

                        // Read the database values returned
                        string incidentId = GetString(sqlReader, ordinalIncidentId);

                        if (!string.IsNullOrEmpty(incidentId))
                        {
                            TravelNewsItem travelNewsItem = GetDetailsByUid(incidentId);
                            if (travelNewsItem != null)
                            {
                                affectingTravelNewsItems.Add(travelNewsItem);
                            }
                        }
                        #endregion

                        

                    }
                }
                #region Error handling and Finally
                catch (SqlException sqlEx)
                {
                    message = string.Format("TravelNewsToids - SQL Helper error when excuting stored procedure [{0}.{1}], Message:{2}",
                                    database, SP_TravelNewsToids, sqlEx.Message);
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, message));
                }
                catch (Exception ex)
                {
                    message = string.Format("TravelNewsToids - Error occurred attempting to obtain TOIDs affected by Travel News incidents from the database[{0}], Exception Message[{1}].",
                                    database, ex.Message);
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, message));
                }
                finally
                {
                    //close the database connections
                    if (sqlReader != null)
                        sqlReader.Close();

                    if (sqlHelper.ConnIsOpen)
                        sqlHelper.ConnClose();
                }
                #endregion

                #endregion

            }

            DateTime tnEndTime = DateTime.Now;

            message = string.Format("TravelNewsToids - processing ended at[{0}]", tnEndTime.ToString(dateTimeFormat));
            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, message));

            return affectingTravelNewsItems.ToArray();
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
				item.SeverityLevel =			Parsing.ParseSeverityLevel((byte)travelNewsRow[Keys.severityLevelColumn]);
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
				
				decimal tempEast = (decimal)travelNewsRow[Keys.eastingColumn];
				decimal tempNorth = (decimal)travelNewsRow[Keys.northingColumn];
				item.Easting =			(int)tempEast;
				item.Northing = (int)tempNorth;
				
				item.ReportedDateTime =		(DateTime)travelNewsRow[Keys.reportedDateTimeColumn];
				item.StartDateTime =			(DateTime)travelNewsRow[Keys.startDateTimeColumn];
				item.StartToNowMinDiff =		(int)travelNewsRow[Keys.startToNowMinDiffColumn];
				item.LastModifiedDateTime =	(DateTime)travelNewsRow[Keys.lastModifiedDateTimeColumn];
				if (travelNewsRow[Keys.clearedDateTimeColumn] != DBNull.Value)
					item.ClearedDateTime =			(DateTime)travelNewsRow[Keys.clearedDateTimeColumn];
				if (travelNewsRow[Keys.expiryDateTimeColumn] != DBNull.Value)
					item.ExpiryDateTime =			(DateTime)travelNewsRow[Keys.expiryDateTimeColumn];
                
                // setting up planned incident property CCN 421
                item.PlannedIncident = (bool)travelNewsRow[Keys.plannedIncidentColumn];

                // set the affected toids
                item.AffectedToids = null;

                if (travelNewsRow[Keys.AffectedToidsColumn] != DBNull.Value)
                {
                    string afffectedToids = (string)travelNewsRow[Keys.AffectedToidsColumn];

                    if (!string.IsNullOrEmpty(afffectedToids))
                    {
                        item.AffectedToids = afffectedToids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                }

                // set the bool flag determining if the closure/blockage happen 
                item.IsClosure = (bool)travelNewsRow[Keys.IsClosureColumn];



                //new members for travel news hierarchy, times of roadworks & rss functionality
                if (travelNewsRow[Keys.IncidentParentColumn] != DBNull.Value)
                {
                    item.IncidentParent = (string)travelNewsRow[Keys.IncidentParentColumn];
                }
                if (travelNewsRow[Keys.CarriagewayDirectionColumn] != DBNull.Value)
                {
                    item.CarriagewayDirection = (string)travelNewsRow[Keys.CarriagewayDirectionColumn];
                }
                if (travelNewsRow[Keys.RoadNumberColumn] != DBNull.Value)
                {
                    item.RoadNumber = (string)travelNewsRow[Keys.RoadNumberColumn];
                }

                if (travelNewsRow[Keys.DayMaskColumn] != DBNull.Value)
                {
                    if ((string)travelNewsRow[Keys.DayMaskColumn] != "")
                    {
                        item.DayMask = (string)travelNewsRow[Keys.DayMaskColumn];
                    }
                }

                if (travelNewsRow[Keys.DailyStartTimeColumn] != DBNull.Value)
                {
                    item.DailyStartTime = (TimeSpan)travelNewsRow[Keys.DailyStartTimeColumn];
                }
                if (travelNewsRow[Keys.DailyEndTimeColumn] != DBNull.Value)
                {
                    item.DailyEndTime = (TimeSpan)travelNewsRow[Keys.DailyEndTimeColumn];
                }

                if (travelNewsRow[Keys.ItemChangeStatusColumn] != DBNull.Value)
                {
                    item.ItemChangeStatus = (string)travelNewsRow[Keys.ItemChangeStatusColumn];
                }

                if (travelNewsRow[Keys.IncidentActiveStatusColumn] != DBNull.Value)
                {
                    if ((bool)travelNewsRow[Keys.IncidentActiveStatusColumn])
                    { item.IncidentActiveStatus = IncidentActiveStatus.Active; }
                    else
                    { item.IncidentActiveStatus = IncidentActiveStatus.Inactive; }
                }

                item.TravelNewsCalendar = GetTravelNewsCalendar(item);

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
					string.Format("An exception was thrown while building a TravelNewsItem. Reason: {0}",exc.Message));
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
			// Array list object to hold each filter. Why use a string array? This is much tidier and
			// has a very small performance overhead and we just turn the array list to an array at the 
			// end of the process. No fixed array size, counters and empty objects!
			ArrayList filterList = new ArrayList();
			string filterExpression = string.Empty;

			// Severity column filter
			filterList.Add (Keys.severityLevelColumn + " <> " +  Convert.ToInt32(Enum.Format(typeof(SeverityLevel), SeverityLevel.Critical, "d")));

           if (travelNewsState.SelectedSeverityFilter == SeverityFilter.CriticalIncidents)
            {
			    // Critical incidents filter - on click through from critical incident DN states "...show all critical child 
                // entries for display." so we can ignore all other filter parameters & just select all "Serious" incidents.
                filterList.Add(Keys.severityLevelColumn + " = " + Enum.Format(typeof(SeverityLevel), SeverityLevel.Serious, "d"));
            }
            else
            // Not a critical incident click through therefore build filter as normal
            {

                // Modes of transport filter
                switch (travelNewsState.SelectedTransport)
                {
                    case (TransportType.All):
                        break;
                    case (TransportType.PublicTransport):
                        filterList.Add(Keys.modeOfTransportColumn + " <> '" + Values.Road + "'");
                        break;
                    default:
                        filterList.Add(Keys.modeOfTransportColumn + " = '" + Converting.ToString(travelNewsState.SelectedTransport) + "'");
                        break;
                }
                // Selected Region Filter
                switch (travelNewsState.SelectedRegion)
                {
                    case (""):
                    case ("All"):
                        break;
                    default:
                        filterList.Add(Keys.regionsColumn + " LIKE '%" + travelNewsState.SelectedRegion + "%'");
                        break;
                }
                switch (travelNewsState.SelectedDelays)
                {
                    case (DelayType.Major):
                        filterList.Add(Keys.severityLevelColumn + " <= " + Convert.ToInt32(Enum.Format(typeof(SeverityLevel), SeverityLevel.Severe, "d")));
                        break;
                    case (DelayType.Recent):
                        filterList.Add(Keys.startToNowMinDiffColumn + " < " + Values.RecentCriteria);
                        break;
                }
                //CCN 0421 Modes of incidentType filter
                switch (travelNewsState.SelectedIncidentType)
                {
                    case (IncidentType.All):
                        break;
                    case (IncidentType.Planned):
                        filterList.Add(Keys.plannedIncidentColumn + " = 1");
                        break;
                    case (IncidentType.Unplanned):
                        filterList.Add(Keys.plannedIncidentColumn + " = 0");
                        break;
                }
                //filter for travelNewsState.SelectedDate
                if (travelNewsState.SelectedDate != null)
                {
                    filterList.Add(Keys.startDateTimeColumn + " <= " + "'" + travelNewsState.SelectedDate.ToString("yyyy-MM-dd") + " 23:59:59'" + " AND " + Keys.expiryDateTimeColumn + " >= " + "'" + travelNewsState.SelectedDate.ToString("yyyy-MM-dd") + " 00:00:00'");
                }

                #region searchExpression

                // Clear search tokens from page state
                travelNewsState.SearchTokens = null;

                // If a search expression has been entered then parse it
                if (travelNewsState.SearchPhrase.Length > 0)
                {
                    travelNewsState.SelectedRegion = "All";
                    TravelNewsSearchParser parser = new TravelNewsSearchParser();
                    ArrayList tokens = parser.GetSearchTokens(travelNewsState.SearchPhrase);

                    // Add the keys to the travel news state
                    travelNewsState.SearchTokens = tokens;

                    // If we got some tokens back...
                    if (tokens.Count > 0)
                    {
                        // Build a new search filter based on these phrases
                        StringBuilder filter = new StringBuilder();

                        // Start of filter expression
                        filter.Append("(");

                        // Headline expression
                        StringBuilder headlineExpression = new StringBuilder();

                        foreach (string token in tokens)
                        {
                            if (headlineExpression.Length > 0)
                            {
                                // We have added one headline statement so add an OR statement
                                headlineExpression.Append(" OR ");
                            }

                            headlineExpression.Append(Keys.locationColumn);
                            headlineExpression.Append(" LIKE '%");
                            headlineExpression.Append(token);
                            headlineExpression.Append("%'");
                        }

                        // Add headline to the filter
                        filter.Append(headlineExpression.ToString().ToLower());

                        // Or express between hadline and detail
                        filter.Append(" ) OR ( ");

                        // Detail expression
                        StringBuilder detailExpression = new StringBuilder();

                        foreach (string token in tokens)
                        {
                            // We have added at least one detail statement so add an AND statement
                            if (detailExpression.Length > 0)
                            {
                                detailExpression.Append(" AND ");
                            }

                            detailExpression.Append(Keys.detailTextColumn);
                            detailExpression.Append(" LIKE '%");
                            detailExpression.Append(token);
                            detailExpression.Append("%'");
                        }

                        // Add detail to the filter
                        filter.Append(detailExpression.ToString().ToLower());

                        // Final brackets
                        filter.Append(")");

                        string temp = filter.ToString();

                        // Add to filterlist
                        //filterList.Add(filter.ToString());

                    }
                }

                #endregion

                //filterList.Add (" DetailText LIKE '[Chester]' ");
            }

			// Compile complete list of filters.
			foreach (string filter in filterList)
			{
				// If filter express is not empty then add an AND
				// onto the end of the filter.
				if (filterExpression != string.Empty)
				{
					filterExpression += " AND ";
				}
				
				// Add the new filter in.
				filterExpression += filter;
			}

			return filterExpression;
		}

        /// <summary>
        /// Does a null check for the data reader column value and returns empty string if found null 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        private string GetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        /// <summary>
        /// Builds the travel news calendar days for which travel news is active
        /// The calendar gives a daily start and end datetime for each day travel news incident is active
        /// </summary>
        /// <returns>Array of TravelNewsEventDateTime objects</returns>
        private TravelNewsEventDateTime[] GetTravelNewsCalendar(TravelNewsItem tnItem)
        {
            List<TravelNewsEventDateTime> traveNewsEventDateTimes = new List<TravelNewsEventDateTime>();

            if (tnItem.StartDateTime != DateTime.MinValue)
            {
                #region Determine calendar start/end date, and daily start/end time

                DateTime tnStartDateTime = tnItem.StartDateTime;
                DateTime tnEndDateTime = tnItem.ExpiryDateTime;

                DateTime today = DateTime.Now.Date;
                DateTime yesterday = today.AddDays(-1);

                // Update start date to at least include yesterdays to cover overnight incidents starting last night
                // (prevents creating calendar for days far in the past)
                if (tnStartDateTime < yesterday)
                {
                    tnStartDateTime = yesterday;
                }
                                
                // Update end date to be cleared date if necessary 
                // (prevents creating calendar for days it is no longer active)
                if (tnItem.ClearedDateTime != DateTime.MinValue && tnItem.ClearedDateTime < tnItem.ExpiryDateTime)
                {
                    tnEndDateTime = tnItem.ClearedDateTime;
                }

                // Ensure end date is no more than X days from start
                // (prevents having very large calendars)
                int maxDaysToCheck = 95;
                if (!int.TryParse(Properties.Current["TravelNewsItem.Calendar.MaxDays"], out maxDaysToCheck))
                {
                    maxDaysToCheck = 95; //Default value
                }
                if (tnEndDateTime > tnStartDateTime.AddDays(maxDaysToCheck))
                {
                    tnEndDateTime = tnStartDateTime.AddDays(maxDaysToCheck);
                }

                // Daily start/end time may not be populated for an incident
                TimeSpan dailyStartTime = new TimeSpan(0, 0, 0);
                TimeSpan dailyEndTime = new TimeSpan(23, 59, 59);

                if (tnItem.DailyStartTime != TimeSpan.MinValue)
                {
                    dailyStartTime = tnItem.DailyStartTime;
                }
                if (tnItem.DailyEndTime != TimeSpan.MaxValue)
                {
                    dailyEndTime = tnItem.DailyEndTime;
                }

                // Special logic required if handling overnight incident
                bool isOvernight = dailyEndTime < dailyStartTime;

                #endregion

                // Daily date to work with
                DateTime referenceDate = tnStartDateTime.Date;

                while (referenceDate.Date <= tnEndDateTime.Date)
                {
                    DateTime dailyStartDateTime = new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day, dailyStartTime.Hours, dailyStartTime.Minutes, dailyStartTime.Seconds);
                    DateTime dailyEndDateTime = new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day, dailyEndTime.Hours, dailyEndTime.Minutes, dailyEndTime.Seconds);

                    #region Overnight midnight check

                    // If overnight adjust the daily incident window
                    if (isOvernight)
                    {
                        // Overnight so daily end date set to tomorrow
                        dailyEndDateTime = dailyEndDateTime.AddDays(1);
                    }

                    #endregion

                    // If daily end date goes beyond incident end date, then adjust end time
                    if (dailyEndDateTime > tnEndDateTime)
                    {
                        dailyEndDateTime = tnEndDateTime;
                    }

                    // Add to calendar
                    if (!string.IsNullOrEmpty(tnItem.DayMask))
                    {
                        if (tnItem.DayMask.ToLower().Contains(dailyStartDateTime.DayOfWeek.ToString().Substring(0, 2).ToLower()))
                        {
                            traveNewsEventDateTimes.Add(new TravelNewsEventDateTime(dailyStartDateTime, dailyEndDateTime));
                        }
                    }
                    else
                    {
                        traveNewsEventDateTimes.Add(new TravelNewsEventDateTime(dailyStartDateTime, dailyEndDateTime));
                    }

                    referenceDate = referenceDate.AddDays(1);
                }
            }

            return traveNewsEventDateTimes.ToArray();

        }
		#endregion
	}
}
