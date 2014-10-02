//********************************************************************************
//NAME         : LookupTransform.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 13/01/2004
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/LookupTransform.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:10   mturner
//Initial revision.
//
//   Rev 1.3   Apr 26 2006 12:15:02   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.2.1.1   Apr 06 2006 19:12:10   RPhilpott
//FxCop fixes.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.2.1.0   Apr 05 2006 17:13:04   RPhilpott
//Add method to get station name from NLC. 
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.2   Nov 24 2005 18:20:58   RPhilpott
//1) Use NLC lookups, not GRP.
//
//2) Generalise to get stations in a group as well as v.v.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.1   Apr 25 2005 21:12:52   RPhilpott
//Change LBO lookups to get all groups to which a location belongs, and remove associated redundant code.
//Resolution for 2328: PT - fares between Three Bridges and Victoria
//
//   Rev 1.0   Jan 13 2004 13:14:08   CHosegood
//Initial Revision

#region using
using System;
using System.Text;
using System.Diagnostics;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;
#endregion

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Summary description for LookupTransform.
	/// </summary>
	public class LookupTransform
	{

        /// <summary>
        /// Transforms LBO output into domain objects
        /// </summary>
		public LookupTransform() 
		{ 
		}
 		
		/// <summary>
		/// Returns a LocationDto for each of the Fare Groups to which an NLC belongs.
		/// </summary>
		/// <param name="nlc">Individual station</param>
		/// <param name="date">Applicable date</param>
		/// <returns>Array of groups for station</returns>
		public LocationDto[] GetFareGroupsForStation(string nlc, TDDateTime date)
		{
			return LookupFareGroups(nlc, date, true);
		}

		/// <summary>
		/// Returns a LocationDto for each of the individual stations making up a Fare Group.
		/// </summary>
		/// <param name="nlc">Fare Group</param>
		/// <param name="date">Applicable date</param>
		/// <returns>Array of individual stations in group</returns>
		public LocationDto[] GetStationsForFareGroup(string nlc, TDDateTime date)
		{
			return LookupFareGroups(nlc, date, false);
		}

		/// <summary>
		/// Returns the name of the location of the passed NLC (which 
		/// may be the code for a fare group or an individual location).
		/// </summary>
		/// <param name="nlc">NLC</param>
		/// <param name="date">Applicable date</param>
		/// <returns>Location name (or an empty string if not found)</returns>
		public string LookupNameForNlc(string nlc, TDDateTime date)
		{
			string locationName = string.Empty;

			int outputLength = 30;						
			BusinessObject lbo = null;
			BusinessObjectOutput output = null;
            
			LBOPool lboPool = LBOPool.GetLBOPool();
			
			try 
			{
				LookupNlcNameRequest nlcRequest
					= new LookupNlcNameRequest(lboPool.InterfaceVersion, outputLength, nlc, date);

				lbo = lboPool.GetInstance();
				output = lbo.Process(nlcRequest);
				lboPool.Release(ref lbo);

				LookupNlcNameResult result = new LookupNlcNameResult(output);

				locationName = result.LocationName;
			} 
			catch (TDException tde) 
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Info, "An error was encountered while looking up NLC name for " + nlc, tde ) );
				throw; 
			} 
			finally 
			{
				if ( lbo != null ) 
				{
					lboPool.Release(ref lbo);
				}
			}
			return locationName;
		}


		/// <summary>
		/// Returns a LocationDto for each of the Fare Groups to which an NLC belongs, or 
		/// for each of the individual stations making up a group.
		/// </summary>
		/// <param name="nlc">Individual or group station</param>
		/// <param name="date">Applicable date</param>
		/// <param name="fromIndividualToGroup">True to get groups for a station, false for v.v.</param>
		/// <returns>Array of groups for station or array of individual stations in group</returns>
		private LocationDto[] LookupFareGroups(string nlc, TDDateTime date, bool fromIndividualToGroup) 
		{
			LocationDto[] groupLocations = null;
			int outputLength = 913;						
			BusinessObject lbo = null;
			BusinessObjectOutput output = null;
            
			LBOPool lboPool = LBOPool.GetLBOPool();
			
			try 
			{
				LookupLocationGroupRequest nlcRequest
					= new LookupLocationGroupRequest(lboPool.InterfaceVersion, outputLength, nlc, date, fromIndividualToGroup);

				lbo = lboPool.GetInstance();
				output = lbo.Process(nlcRequest);
				lboPool.Release(ref lbo);

				LookupLocationGroupResult result = new LookupLocationGroupResult(output, nlc, fromIndividualToGroup);

				groupLocations = result.GroupLocations;
			} 
			catch (TDException tde) 
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Info, "An error was encountered while looking up station group ::", tde ) );
				throw; 
			} 
			finally 
			{
				if ( lbo != null ) 
				{
					lboPool.Release(ref lbo);
				}
			}
			return (groupLocations != null ? groupLocations : new LocationDto[0]);
		}
	}
}
