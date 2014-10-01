// *********************************************** 
// NAME			: CarCostCalculator.cs
// AUTHOR		: Richard Hopkins
// DATE CREATED	: 15/12/04
// DESCRIPTION	: Class to provide cost data for road journeys
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/CarCostCalculator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:38   mturner
//Initial revision.
//
//   Rev 1.6   Dec 07 2006 14:38:00   build
//Automatically merged from branch for stream4240
//
//   Rev 1.5.1.0   Nov 24 2006 11:37:32   mmodi
//Added Journey emissions FuelFactors
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.5   Apr 13 2005 12:32:02   esevern
//updated comments for running cost and fuel cost in line with new database values (from DUD192)
//
//   Rev 1.4   Mar 23 2005 15:41:06   rhopkins
//Correct exception handling for DB access
//
//   Rev 1.3   Mar 21 2005 12:56:10   rhopkins
//Corrected FxCop issues
//Resolution for 1957: DEV Code Review: CC - Fuel and Running Costs Calculation
//
//   Rev 1.2   Jan 11 2005 19:09:00   rhopkins
//Correction to logging
//
//   Rev 1.1   Jan 06 2005 16:43:22   rhopkins
//Correction to data types
//

using System;
using System.Collections;
using System.Data.SqlClient;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Provides cost data for road journeys.
	/// </summary>
	[CLSCompliant(false)]
	public sealed class CarCostCalculator
	{
		/// <summary>
		/// Structure used to define hashkey for accessing data cached in hashtables
		/// </summary>
		private struct CarSizeFuelType
		{
			public string carSize, fuelType;

			public CarSizeFuelType(string carSize, string fuelType)
			{
				this.carSize = carSize;
				this.fuelType = fuelType;
			}
		}

		private const string DataChangeNotificationGroup = "CarCosting";

		private bool receivingChangeNotifications;

		Hashtable runningCosts;
		Hashtable fuelConsumptions;
		Hashtable fuelCosts;
		Hashtable fuelFactors;

		/// <summary>
		/// Data should be loaded when the item is first created
		/// </summary>
		public CarCostCalculator()
		{
			LoadData();
			receivingChangeNotifications = RegisterForChangeNotification();
		}

		#region Public methods

		/// <summary>
		/// Returns the non-fuel running costs for an average vehicle of the specified size/type and fuel type, for the specified journey distance
		/// </summary>
		/// <param name="carSize">Size/type of vehicle</param>
		/// <param name="fuelType">Type of fuel</param>
		/// <param name="kilometres">Journey distance in kilometres</param>
		/// <returns>Average running cost in tenths of pence</returns>
		public int CalcRunningCost(string carSize, string fuelType, double kilometres)
		{
			CarSizeFuelType hashTableKey = new CarSizeFuelType(carSize, fuelType);
			if (runningCosts.ContainsKey(hashTableKey))
			{
				return (int)Math.Round(((int)runningCosts[hashTableKey]) * kilometres);
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Returns the fuel consumption rate for an average vehicle of the specified size/type and fuel type
		/// </summary>
		/// <param name="carSize">Size/type of vehicle</param>
		/// <param name="fuelType">Type of fuel</param>
		/// <returns>Rate of fuel consumption in metres per litre</returns>
		public int GetFuelConsumption(string carSize, string fuelType)
		{
			CarSizeFuelType hashTableKey = new CarSizeFuelType(carSize, fuelType);
			if (fuelConsumptions.ContainsKey(hashTableKey))
			{
				return (int)fuelConsumptions[hashTableKey];
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Returns the average cost for the specified fuel type
		/// </summary>
		/// <param name="fuelType">Type of fuel</param>
		/// <returns>Fuel cost in pence</returns>
		public int GetFuelCost(string fuelType)
		{
			if (fuelCosts.ContainsKey(fuelType))
			{
				return (int)fuelCosts[fuelType];
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Returns the fuel factor for the specified fuel type, used in calculating CO2 Emissions
		/// </summary>
		/// <param name="fuelType">Type of fuel</param>
		/// <returns>Fuel factor as a decimal</returns>
		public int GetFuelFactor(string fuelType)
		{
			if (fuelFactors.ContainsKey(fuelType))
			{
				return (int)fuelFactors[fuelType];
			}
			else
			{
				return 0;
			}
		}

		#endregion Public methods

		#region Private methods

		/// <summary>
		/// Registers an event handler with the data change notification service
		/// </summary>
		private bool RegisterForChangeNotification()
		{
			IDataChangeNotification notificationService;
			try
			{
				notificationService = (IDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
			}
			catch (TDException e)
			{
				// If the SDInvalidKey TDException is thrown, return false as the notification service
				// hasn't been initialised.
				// Otherwise, rethrow the exception that was received.
				if (e.Identifier == TDExceptionIdentifier.SDInvalidKey)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising CarCostCalculator"));
					return false;
				}
				else
					throw;
			}
			catch
			{
				// Non-CLS-compliant exception
				throw;
			}

			notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
			return true;
		}


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

				// Get cost and consumption data
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading car fuel cost, consumption, and factor started" ));
				}

				CarSizeFuelType hashTableKey;

				runningCosts = new Hashtable();
				fuelConsumptions = new Hashtable();
				fuelCosts = new Hashtable();
				fuelFactors = new Hashtable();

				// Execute the GetCarCostRunningCost stored procedure.
				// This returns the Running Cost for each combination of CarSize and FuelType in CarCostRunningCost.
				reader = helper.GetReader("GetCarCostRunningCost", new Hashtable());
				while (reader.Read())
				{
					hashTableKey = new CarSizeFuelType(reader.GetString(0), reader.GetString(1));
					runningCosts.Add(hashTableKey, reader.GetInt32(2));
				}
				reader.Close();

				// Execute the GetCarCostFuelConsumption stored procedure.
				// This returns the Fuel Consumption for each combination of CarSize and FuelType in CarCostFuelConsumption.
				reader = helper.GetReader("GetCarCostFuelConsumption", new Hashtable());
				while (reader.Read())
				{
					hashTableKey = new CarSizeFuelType(reader.GetString(0), reader.GetString(1));
					fuelConsumptions.Add(hashTableKey, reader.GetInt32(2));
				}
				reader.Close();

				// Execute the GetCarCostFuelCost stored procedure.
				// This returns the Fuel Cost for each FuelType in CarCostFuelCost.
				reader = helper.GetReader("GetCarCostFuelCost", new Hashtable());
				while (reader.Read())
				{
					fuelCosts.Add(reader.GetString(0), reader.GetInt32(1));
				}
				reader.Close();

				// Execute the GetCarCostFuelFactor stored procedure.
				// This returns the Fuel factor for each FuelType in CarCostFuelFactor.
				reader = helper.GetReader("GetCarCostFuelFactor", new Hashtable());
				while (reader.Read())
				{
					fuelFactors.Add(reader.GetString(0), reader.GetInt32(1));
				}
				reader.Close();

				// Record the fact that the data was loaded.
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading car fuel cost, consumption, and factor completed" ));
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
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An error occurred whilst attempting to reload the car fuel cost and consumption data.", e));
				if ((runningCosts == null) || (runningCosts.Count == 0)
					|| (fuelConsumptions == null) || (fuelConsumptions.Count == 0)
					|| (fuelCosts == null) || (fuelCosts.Count == 0))
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

		#endregion Private methods

		#region Event handler

		/// <summary>
		/// Used by the Data Change Notification service to reload the data if it is changed in the DB
		/// </summary>
		private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
		{
			if (e.GroupId == DataChangeNotificationGroup)
				LoadData();
		}

		#endregion

	}
}
