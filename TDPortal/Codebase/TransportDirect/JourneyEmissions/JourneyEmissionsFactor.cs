// *********************************************** 
// NAME			: JourneyEmissionsFactor.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 09/02/07
// DESCRIPTION	: Class to provide emissions factor for transport modes used in Journey Emissions
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyEmissions/JourneyEmissionsFactor.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:36:56   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.0   Nov 08 2007 12:23:46   mturner
//Initial revision.
//
//   Rev 1.2   Apr 03 2007 17:46:08   mmodi
//Factor now divided by 10000 following change to database table
//Resolution for 4375: CO2: Journey Emissions Factors update
//
//   Rev 1.1   Feb 27 2007 10:36:06   mmodi
//Corrected hashtable used for LightRailSystemCode
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.0   Feb 20 2007 17:09:06   mmodi
//Initial revision.
//Resolution for 4350: CO2 Public Transport
//

using System;
using System.Collections;
using System.Data.SqlClient;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.JourneyEmissions
{
	/// <summary>
	/// Summary description for JourneyEmissionsFactor.
	/// </summary>
	public sealed class JourneyEmissionsFactor
	{
		Hashtable emissionFactors;
		Hashtable lightrailCodes;

		/// <summary>
		/// Data should be loaded when the item is first created
		/// </summary>
		public JourneyEmissionsFactor()
		{
			LoadData();
		}

		#region Public methods

		/// <summary>
		/// Returns the emissions factor for the specified transport mode
		/// </summary>
		/// <param name="transportMode">Type of transport mode</param>
		/// <returns>Fuel factor as a decimal</returns>
		public decimal GetEmissionFactor(string transportMode)
		{
			if (emissionFactors.ContainsKey(transportMode))
			{
				// Factors are stored to nearest 10000, so divde by before returning
				int factor = (int)emissionFactors[transportMode];

				return (decimal)factor/10000;
			}
			else
			{
				return -1;
			}
		}

		/// <summary>
		/// Returns the Light Rail System Code for a supplied TOC code
		/// </summary>
		/// <param name="toc">TOC as string</param>
		/// <returns>Light rail system code</returns>
		public string GetLightRailSystemCode(string toc)
		{
			if (lightrailCodes.ContainsKey(toc))
			{
				string code = (string)lightrailCodes[toc];

				return code.Trim();
			}
			else
			{
				return string.Empty;
			}
		}

		#endregion

		#region Private methods
        
		/// <summary>
		/// Loads the data and performs pre processing
		/// </summary>
		private void LoadData()
		{
			SqlDataReader reader;
			SqlHelper helper = new SqlHelper();

			try
			{
				// Initialise a SqlHelper and connect to the database.
				Logger.Write( new OperationalEvent( TDEventCategory.Business,
					TDTraceLevel.Info, "Opening database connection to " + SqlHelperDatabase.DefaultDB.ToString() ));
				helper.ConnOpen(SqlHelperDatabase.DefaultDB);


				#region Load data into hashtables

				// Get emission factors data
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Emission factors for transport modes, and Lightrail system codes started" ));
				}

				emissionFactors = new Hashtable();
				lightrailCodes = new Hashtable();

				// Execute the GetJourneyEmissionsFactor stored procedure.
				// This returns the Fuel factor for each FuelType in CarCostFuelFactor.
				reader = helper.GetReader("GetJourneyEmissionsFactor", new Hashtable());
				while (reader.Read())
				{
					emissionFactors.Add(reader.GetString(0), reader.GetInt32(1));
				}
				reader.Close();

				// Execute the GetLightRailSystemCode stored procedure.
				// This returns the LightRail System Codes for each TOC in LightRailSystemCode.
				reader = helper.GetReader("GetLightRailSystemCode", new Hashtable());
				while (reader.Read())
				{
					lightrailCodes.Add(reader.GetString(0), reader.GetString(1));
				}
				reader.Close();

				// Record the fact that the data was loaded.
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Emission factors  for transport modes, and Lightrail system codes completed" ));
				}

				#endregion Load data into hashtables
			}
			catch (Exception e)
			{
				// Catching the base Exception class because we don't want any possibility
				// of this raising any errors outside of the class in case it causes the
				// application to fall over. As long as the exception doesn't occur in 
				// the final block of code, which copies the new data into the module-level
				// hashtables and arraylists, the object will still be internally consistant,
				// although the data will be inconsistant with that stored in the database.
				// One exception to this: if this is the first time LoadData has been run,
				// the exception should be raised.
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An error occurred whilst attempting to reload the Emission factors for transport modes data.", e));
				if (((emissionFactors == null) || (emissionFactors.Count == 0))
					||((lightrailCodes == null) || (lightrailCodes.Count == 0)) )
				{
					throw;
				}
			}
			finally
			{
				//close the database connection
				helper.ConnClose();
			}
		}

		#endregion
	}
}
