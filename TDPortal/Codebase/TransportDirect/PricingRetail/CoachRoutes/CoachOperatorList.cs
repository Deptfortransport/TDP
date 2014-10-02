// ************************************************************** 
// NAME			: CoachOperatorList.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Strong typed collection for CoachOperator objects
//	
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/CoachOperatorList.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:38   mturner
//Initial revision.
//
//   Rev 1.1   Jan 17 2006 17:44:54   RPhilpott
//Code review updates.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.0   Oct 26 2005 09:55:42   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 20 2005 09:27:00   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 10 2005 17:04:08   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price

using System;
using System.Collections;
using System.Data.SqlClient;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// CoachOperatorList collection
	/// </summary>
	public class CoachOperatorList : CollectionBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public CoachOperatorList()
		{}

		/// <summary>
		/// Adds a CoachOperator object to the end of the CollectionBase.
		/// </summary>
		/// <param name="_operator"></param>
		/// <returns></returns>
		public int Add(CoachOperator coachOperator)
		{
			return List.Add(coachOperator);
		}

		/// <summary>
		///  Determines whether the collection contains a specific CoachOperator object
		/// </summary>
		/// <param name="_operator"></param>
		/// <returns></returns>
		public bool Contains(CoachOperator coachOperator)
		{
			return List.Contains(coachOperator);
		}

		/// <summary>
		/// Searches for the specified CoachOperator object and returns the zero-based index of the first occurrence within the entire collection
		/// </summary>
		/// <param name="_operator"></param>
		/// <returns></returns>
		public int IndexOf(CoachOperator coachOperator)
		{
			return List.IndexOf(coachOperator);
		}

		/// <summary>
		/// REad-only collection indexer 
		/// </summary>
		public CoachOperator this[int index]
		{
			get { return (CoachOperator)List[index]; }
		}

		/// <summary>
		/// Provides type specific validation when using the collection
		/// </summary>
		/// <param name="item"></param>
		protected override void OnValidate(object item)
		{
			if (!(item is CoachOperator))
			{
				throw new ArgumentException(
				"This collection only accepts the CoachOperator type or types that derive from CoachOperator");
			}
		}

		/// <summary>
		/// Static data access Fetch method to return data-populated OperatorList
		/// </summary>
		/// <returns>OperatorList</returns>
		public static CoachOperatorList Fetch()
		{
			CoachOperatorList coachOperatorlist = new CoachOperatorList();
			SqlDataReader reader;
			SqlHelper helper = new SqlHelper();

			try
			{
				// Initialise a SqlHelper and connect to the database.
				Logger.Write( new OperationalEvent( TDEventCategory.Business,
					TDTraceLevel.Info, "Opening database connection to " + SqlHelperDatabase.TransientPortalDB.ToString() ));
				
				helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);
				
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Populating OperatorList" ));
				}
			
				// Execute the GetCoachRouteOperators stored procedure.
				// This returns all the unique ID entries from the CoachRouteOperators table
				// needed to populate the operator objects and composite child objects
				reader = helper.GetReader( "GetCoachRouteOperators");
				
				//Iterate the result set instantiating an CoachOperator object using Operator.Fetch() 
				//then add it to the operatorList collection
				while (reader.Read())
				{
					coachOperatorlist.Add(CoachOperator.Fetch(reader.GetInt32(0)));
						
				}
				reader.Close();
				

				// Log the fact that the data was loaded.
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "OperatorList successfully populated" ));
				}
			
			}
			catch (SqlException sqle)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An SQL exception occurred in TransportDirect.UserPortal.CostSearchCoachRoutes.OperatorList.Fetch method", sqle));
			}
			finally
			{
				//close the database connection
				helper.ConnClose();
			}

			return coachOperatorlist;
		}

	}
}
