// ************************************************************** 
// NAME			: CoachOperator.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Represents a coach operator
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/CoachOperator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:38   mturner
//Initial revision.
//
//   Rev 1.1   Jan 17 2006 17:44:54   RPhilpott
//Code review updates.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.0   Oct 26 2005 09:55:38   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 20 2005 09:27:24   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 19 2005 14:47:56   RWilby
//Updated override Equals method not to case twice
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 16:24:54   RWilby
//Added some more comments
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 10 2005 17:04:00   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// Represents a specific coach operator
	/// </summary>
	public class CoachOperator
	{
		private string code;
		private NaPTANDictionary naptanDictionary = new NaPTANDictionary();
		private QuotaFareList quotaFareList = new QuotaFareList();
		
		/// <summary>
		/// Constructor
		/// </summary>
		public CoachOperator()
		{
		}

		/// <summary>
		/// Read/Write Property. Coach Operator code
		/// </summary>
		public string Code
		{
			get{return code;}
			set{code = value;}
		}

		/// <summary>
		///Read/Write Property. Dictionary collection of NaPTAN objects for this specific operator.
		/// </summary>
		public NaPTANDictionary NaPTANDictionary
		{
			get{return naptanDictionary;}
			set{naptanDictionary = value;}
		}
		
	
		/// <summary>
		/// Read/Write Property. Collection of QuotaFares that this specific operator provides
		/// </summary>
		public QuotaFareList QuotaFareList
		{
			get{return quotaFareList;}
			set{quotaFareList = value;}
		}

		/// <summary>
		/// Static data access Fetch method to return a data-populated Coach Operator object
		/// </summary>
		/// <param name="OperatorID">Datebase unique OperatorID</param>
		/// <returns>CoachOperator</returns>
		public static CoachOperator Fetch(int coachOperatorId)
		{
			CoachOperator coachOperator = new CoachOperator();
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
						TDTraceLevel.Verbose, "Populating CoachOperator object" ));
				}
			
				// Build the Hash tables for parameters and types
				Hashtable parameter = new Hashtable(1);
				parameter.Add("@OperatorID",coachOperatorId);
				Hashtable parametertype = new Hashtable(1);
				parametertype.Add("@OperatorID",SqlDbType.Int);

				// Execute the GetCoachRouteOperatorData stored procedure.
				// This returns 5 result sets containing all the coach routes and quota fares data
				DataSet ds = helper.GetDataSet("GetCoachRouteOperatorData",parameter,parametertype);
				
				//Populate operator
				coachOperator.Code = (ds.Tables[0].Rows[0] as DataRow)[0].ToString(); 

				//Populate NaPTANDictionary child collection
				coachOperator.naptanDictionary = NaPTANDictionary.Fetch(ds);
				
				//Populate QuotaFares child collection
				coachOperator.quotaFareList = QuotaFareList.Fetch(ds);

				// Log the fact that the data was loaded.
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "CoachOperator object successfully populated" ));
				}
			
			}
			catch (SqlException sqle)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An SQL exception occurred in TransportDirect.UserPortal.CostSearchCoachRoutes.CoachOperator.Fetch method", sqle));
			}
			finally
			{
				//close the database connection
				helper.ConnClose();
			}

			return coachOperator;
		}

		/// <summary>
		/// Static equals method to allow checking of two operators for equality
		/// </summary>
		/// <param name="objA"></param>
		/// <param name="objB"></param>
		/// <returns></returns>
		public new static bool Equals(object objA, object objB)
		{
			if(objA is CoachOperator && objB is CoachOperator)
				return ((CoachOperator)objA).Equals((CoachOperator)objB);
			else
				return false;
		}

		/// <summary>
		/// Overridden Equals method to allow checking of two operators for equality
		/// </summary>
		/// <param name="_operator"></param>
		/// <returns></returns>
		public override bool Equals(object coachOperator)
		{
			if(coachOperator is CoachOperator)
				return Equals(coachOperator);
			else
				return false;
		}
		
		/// <summary>
		/// Checking equality of operator object based on CoachOperator.Code
		/// </summary>
		/// <param name="_operator"></param>
		/// <returns></returns>
		public bool Equals(CoachOperator coachOperator)
		{
			return code.Equals(coachOperator.Code);
		}

		/// <summary>
		/// Overridden GetHashCode() for operator
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return code.GetHashCode();
		}
		
		/// <summary>
		/// Overridden ToString() for operator
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return code;
		}

	}

}
