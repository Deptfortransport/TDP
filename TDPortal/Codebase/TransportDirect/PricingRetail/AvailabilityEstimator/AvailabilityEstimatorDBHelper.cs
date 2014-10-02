// *********************************************** 
// NAME			: AvailabilityEstimatorDBHelper.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Implementation of the AvailabilityEstimatorDBHelper class
//				  This class handles all the necessary database calls					
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/AvailabilityEstimator/AvailabilityEstimatorDBHelper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:06   mturner
//Initial revision.
//
//   Rev 1.5   May 17 2006 15:01:54   rphilpott
//Add RouteCode to UnavailableProducts table, associated SP's and all classes that use them.
//Resolution for 4084: DD075: Unavailable products - ticket and route codes
//
//   Rev 1.4   May 26 2005 14:03:32   RPhilpott
//Use current CultureInfo to format dates and times for SQL SP's,  not hard-coded values based on "en-GB".
//Resolution for 2546: PT costing - find-a-fare journeys not returned consistently
//
//   Rev 1.3   Apr 28 2005 16:35:14   jbroome
//UnavailableProducts now stored by Outward and Return dates
//Resolution for 2302: PT - Product availability does not handle return products adequately.
//
//   Rev 1.2   Mar 18 2005 15:06:00   jbroome
//Added missing class documentation comments and minor updates after code review
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.1   Feb 17 2005 15:45:24   jbroome
//Updated according to stored procedure changes
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.0   Feb 08 2005 09:52:34   jbroome
//Initial revision.

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{
	/// <summary>
	/// Class handles all calls to stored procedures by 
	/// the AvailabiltiyEstimator classes. 
	/// </summary>
	public class AvailabilityEstimatorDBHelper
	{
		
		#region Private Members
		
		// Stored Procs
        private const string SPCheckUnavailableProducts =	"CheckUnavailableProducts";
		private const string SPAddUnavailableProduct =		"AddUnavailableProduct";
		private const string SPGetProductProfile =			"GetProductProfile";
		private const string SPAddAvailabilityHistory =		"AddAvailabilityHistory";
		// Params
		private const string paramProfileId =			"@ProfileId";
		private const string paramMode =				"@Mode";
		private const string paramOrigin =				"@Origin";
		private const string paramDestination =			"@Destination";
		private const string paramTicketCode =			"@TicketCode";
		private const string paramRouteCode =			"@RouteCode";
		private const string paramTravelDate =			"@TravelDate";
		private const string paramOutTravelDate =		"@OutwardTravelDate";
		private const string paramRetTravelDate =		"@ReturnTravelDate";
		private const string paramTravelDatetime =		"@TravelDatetime";
		private const string paramAvailable =			"@Available";

		private string dateFormat;
		private string datetimeFormat;

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public AvailabilityEstimatorDBHelper()
		{
			dateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
			datetimeFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
		}


		#region Public Methods

		/// <summary>
		/// Calls the CheckUnavailableProducts stored procedure
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="outwardTravelDate"></param>
		/// <param name="returnTravelDate"></param>
		/// <param name="ticketCode"></param>
		/// <param name="routeCode"></param>
		/// <returns>true if product known to be unavailable</returns>
		public bool CheckUnavailableProducts(string mode, string origin, string destination,
												TDDateTime outwardTravelDate, TDDateTime returnTravelDate, 
												string ticketCode, string routeCode)
		{
			SqlHelper sqlHelper = new SqlHelper();
			bool unavailable = false;
			int result = 0;
			// Create parameters	
			Hashtable parameters = new Hashtable(6);
			parameters.Add (paramMode, mode);
			parameters.Add (paramOrigin, origin);
			parameters.Add (paramDestination, destination);
			parameters.Add (paramTicketCode, ticketCode); 
			parameters.Add (paramRouteCode, routeCode); 
			parameters.Add (paramOutTravelDate, outwardTravelDate.ToString(dateFormat)); 
			
			if (returnTravelDate == null)
				parameters.Add (paramRetTravelDate, DBNull.Value); 
			else
				parameters.Add (paramRetTravelDate, returnTravelDate.ToString(dateFormat)); 

			Hashtable types = new Hashtable(6);
			types.Add(paramMode, SqlDbType.VarChar);
			types.Add(paramOrigin, SqlDbType.VarChar);
			types.Add(paramDestination, SqlDbType.VarChar);
			types.Add(paramTicketCode, SqlDbType.VarChar);
			types.Add(paramRouteCode, SqlDbType.VarChar);
			types.Add(paramOutTravelDate, SqlDbType.DateTime);
			types.Add(paramRetTravelDate, SqlDbType.DateTime);
			
			// Open DB connection 
			sqlHelper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			
			try
			{
				// Call Stored Procedure
				result =  (int)sqlHelper.GetScalar(SPCheckUnavailableProducts, parameters, types);
				if (result > 0) 
					unavailable = true;
			}
			catch (SqlException ex)
			{
				// Error has occured in stored procedure - this needs to be logged.
                // However, do not want application to crash.
				Logger.Write( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose, "Error in CheckUnavailableProducts stored procedure. Unable to return query results. " + ex.Message ));
			}
			finally
			{
				// Close DB connection
				sqlHelper.ConnClose();
			}

			return unavailable;
		}

		/// <summary>
		/// Calls the AddUnavailableProduct stored procedure
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="outwardTravelDate"></param>
		/// <param name="returnTravelDate"></param>
		/// <param name="ticketCode"></param>
		/// <param name="routeCode"></param>
		/// <returns>true if product added successfully</returns>
		public bool AddUnavailableProduct(string mode, string origin, string destination,
											TDDateTime outwardTravelDate, TDDateTime returnTravelDate, 
											string ticketCode, string routeCode)
		{
			SqlHelper sqlHelper = new SqlHelper();
			bool success = false;
			// Create parameters	
			Hashtable parameters = new Hashtable(6);
			parameters.Add (paramMode, mode);
			parameters.Add (paramOrigin, origin);
			parameters.Add (paramDestination, destination);
			parameters.Add (paramTicketCode, ticketCode); 
			parameters.Add (paramRouteCode, routeCode); 
			parameters.Add (paramOutTravelDate, outwardTravelDate.ToString(dateFormat)); 
			
			if (returnTravelDate == null)
				parameters.Add (paramRetTravelDate, DBNull.Value); 
			else
				parameters.Add (paramRetTravelDate, returnTravelDate.ToString(dateFormat)); 

			Hashtable types = new Hashtable(5);
			types.Add(paramMode, SqlDbType.VarChar);
			types.Add(paramOrigin, SqlDbType.VarChar);
			types.Add(paramDestination, SqlDbType.VarChar);
			types.Add(paramTicketCode, SqlDbType.VarChar);
			types.Add(paramRouteCode, SqlDbType.VarChar);
			types.Add(paramOutTravelDate, SqlDbType.DateTime);
			types.Add(paramRetTravelDate, SqlDbType.DateTime);

			// Open DB connection 
			sqlHelper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			
			try
			{
				// Call Stored Procedure
				sqlHelper.Execute(SPAddUnavailableProduct, parameters, types);
				success = true;
			}
			catch (SqlException ex)
			{
				// Error 2627: Violated unique constraint error i.e. Unavailable Product already exists.
				// Do not want to raise error in this case, handle smoothly - do nothing
				if (ex.Number != 2627)
				{
					// Error has occured in stored procedure - this needs to be logged.
					// However, do not want application to crash.
					Logger.Write( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose, "Error in AddUnavailableProduct stored procedure. Unable to insert record. " + ex.Message ));
				}
			}
			finally
			{
				// Close DB connection
				sqlHelper.ConnClose();
			}

			return success;
		}

		/// <summary>
		/// Calls the GetProductProfiles stored procedure
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="ticketCode"></param>
		/// <param name="travelDate"></param>
		/// <returns>string availability estimate for result from query</returns>
		public string GetProductProfile(string mode, string origin, string destination, string ticketCode, TDDateTime travelDate)
		{
			SqlHelper sqlHelper = new SqlHelper();
			string estimate = string.Empty;
			SqlDataReader dr = null;

			// Create parameters	
			Hashtable parameters = new Hashtable(5);
			parameters.Add (paramMode, mode); 
			parameters.Add (paramOrigin, origin); 
			parameters.Add (paramDestination, destination); 
			parameters.Add (paramTravelDate, travelDate.ToString(dateFormat)); 
			parameters.Add (paramTicketCode, ticketCode); 

			Hashtable types = new Hashtable(5);
			types.Add(paramMode, SqlDbType.VarChar);
			types.Add(paramOrigin, SqlDbType.VarChar);
			types.Add(paramDestination, SqlDbType.VarChar);
			types.Add(paramTravelDate, SqlDbType.DateTime);
			types.Add(paramTicketCode, SqlDbType.VarChar);

			// Open DB connection 
			sqlHelper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			
			try
			{
				// Call Stored Procedure
				dr = sqlHelper.GetReader(SPGetProductProfile, parameters, types);
				// Stored procedure has multiple SELECT queries, which each return a result
				// set, even if it is empty. Need to navigate through until reach the one 
				// that contains the result
				bool moreResults = true;
				while (moreResults && (dr!=null))
				{
					if (dr.HasRows)
					{
						// Found the result set, now read results
						while (dr.Read())
						{
							// Procedure returns profile ID for test/debug purposes
							// However, helper only concerned with estimate value
							estimate = dr.GetString(1);
						}
						moreResults = false;
					}
					else moreResults = dr.NextResult();
				}
			}
			catch (SqlException ex)
			{
				// Error has occured in stored procedure - this needs to be logged.
				// However, do not want application to crash, so return null.
				Logger.Write( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose, "Error in GetProductProfile stored procedure. Unable to return query results. " + ex.Message ));
				dr = null;
			}
			finally
			{
				// Close DB connection
				if ((dr != null) && (!dr.IsClosed))
				{
					dr.Close();
				}
				sqlHelper.ConnClose();
			}

			return estimate;
		}

		/// <summary>
		/// Calls the AddAvailabilityHistory stored procedure
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="ticketCode"></param>
		/// <param name="travelDate"></param>
		/// <param name="available"></param>
		/// <returns>true if record added successfully</returns>
		public bool AddAvailabilityHistory(string mode, string origin, string destination, string ticketCode, TDDateTime travelDatetime, bool available)
		{
			SqlHelper sqlHelper = new SqlHelper();
			bool success = false;			
			// Create parameters	
			Hashtable parameters = new Hashtable(5);
			parameters.Add (paramMode, mode); 
			parameters.Add (paramOrigin, origin); 
			parameters.Add (paramDestination, destination); 
			parameters.Add (paramTravelDatetime, travelDatetime.ToString(datetimeFormat)); 
			parameters.Add (paramTicketCode, ticketCode); 
			parameters.Add (paramAvailable, available);

			Hashtable types = new Hashtable(5);
			types.Add(paramMode, SqlDbType.VarChar);
			types.Add(paramOrigin, SqlDbType.VarChar);
			types.Add(paramDestination, SqlDbType.VarChar);
			types.Add(paramTravelDatetime, SqlDbType.DateTime);
			types.Add(paramTicketCode, SqlDbType.VarChar);
			types.Add(paramAvailable, SqlDbType.Bit);

			// Open DB connection 
			sqlHelper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);

			try
			{
				// Call Stored Procedure
				sqlHelper.Execute(SPAddAvailabilityHistory, parameters, types);
				success = true;
			}
			catch (SqlException ex)
			{
				// Error has occured in stored procedure - this needs to be logged.
				// However, do not want application to crash.
				Logger.Write( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose, "Error in AddAvailabilityHistory stored procedure. Unable to insert record. " + ex.Message ));
			}
			finally
			{
				// Close DB connection
				sqlHelper.ConnClose();
			}
			
			return success;
		}

		#endregion
	}
}
