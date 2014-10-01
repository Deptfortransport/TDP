// *********************************************** 
// NAME			: CarCostCalculator.cs
// AUTHOR		: Richard Broddle
// DATE CREATED	: 21/08/2012
// DESCRIPTION	: Class to provide calorie data for cycle journeys
// ************************************************ 
// * $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CalorieCalculator.cs-arc  $
//
//   Rev 1.2   Sep 11 2012 09:14:10   rbroddle
//Actioned code review comments
//Resolution for 5828: CCN - RFC ATO666 CYCLE CALORIE COUNTER
//
//   Rev 1.1   Aug 28 2012 11:03:26   RBroddle
//Removed uneccessary     [CLSCompliant(false)] declaration
//Resolution for 5828: CCN - RFC ATO666 CYCLE CALORIE COUNTER
//
//   Rev 1.0   Aug 24 2012 16:00:24   rbroddle
//Initial revision.
//Resolution for 5828: CCN - RFC ATO666 CYCLE CALORIE COUNTER
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DataServices;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    /// <summary>
    /// Provides calorie data for cycle journeys.
    /// </summary>
    public sealed class CalorieCalculator
    {
        /// <summary>
        /// Structure used to define a Metabolic Equivalent category for given speed range
        /// </summary>
        private struct CycleMetValue
        {
            public decimal MetValue, MinSpeed, MaxSpeed;

            public CycleMetValue(decimal MetValue, decimal MinSpeed, decimal MaxSpeed)
            {
                this.MetValue = MetValue;
                this.MinSpeed = MinSpeed;
                this.MaxSpeed = MaxSpeed;
            }
        }

        // List to contain metabolic equivalents loaded from the DB
        private List<CycleMetValue> CycleMetValues = new List<CycleMetValue>();
        private decimal calorieCountDefaultWeightKGs;

        /// <summary>
		/// Constructor - data should be loaded when the item is first created
		/// </summary>
        public CalorieCalculator()
		{
            try
            {
            calorieCountDefaultWeightKGs = Convert.ToDecimal(Properties.Current["CyclePlanner.PlannerControl.CalorieCountDefaultWeightGrams"]) / 1000;
						}
			catch
			{
                const string CalorieCountPropertyError = "Missing/Bad format for CyclePlanner.PlannerControl.CalorieCountDefaultWeightGrams property";

				OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, CalorieCountPropertyError);
				Logger.Write(operationalEvent);

				throw new TDException(CalorieCountPropertyError,true,TDExceptionIdentifier.PSMissingProperty);
			}
            LoadData();
		}

        /// <summary>
        /// Returns the estimated calories burnt calculated as:
        /// MET for the speed category x weight of cyclist (kg) x journey time (hours)
        /// Uses a default weight from properties DB for the calculation
        /// </summary>
        /// <param name="journeyDuration">Journey Duration (seconds)</param>
        /// <param name="journeyDistance">Journey Distance (metres)</param>
        /// <returns>Estimated calories burnt in journey</returns>
        public decimal GetCycleCalorieCount(long journeyDuration, int journeyDistance)
        {
            try
            {
                decimal journeyTimeHours = (decimal)journeyDuration / (decimal)3600;

                //The average speed in mph of the cycle journey is calculated as overall journey
                //distance (converted to miles) divided by overall journey time (converted to hours)
                //rounded up to nearest 0.1 mph
                decimal journeySpeedMph;
                journeySpeedMph = (journeyDistance * (decimal)0.000621371192) / journeyTimeHours;
                journeySpeedMph = Math.Round(journeySpeedMph, 1);

                //default to -1 if for some reason no result is found the caller can decide what to do
                decimal metValueToUse = -1;

                //Get relevant MET value for the speed - just cycle through the collection there's only 8 or so..    
                foreach (CycleMetValue met in CycleMetValues)
                {
                    if ((journeySpeedMph > met.MaxSpeed) || (journeySpeedMph < met.MinSpeed))
                    {
                        //This isn't the MET value you're looking for - keep on looking
                        continue;
                    }
                    else
                    {
                        metValueToUse = met.MetValue;
                        break;
                    }
                }
            //Calories = MET for the speed category x weight of cyclist x journey time 
            return metValueToUse * calorieCountDefaultWeightKGs * journeyTimeHours;
            }
            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                    "Exception: ", e));
                //Return -1 if any error occurred - caller can decide what to do
                return -1;
            }
        }

        /// <summary>
        /// Loads the CycleMetValues database table into memory
        /// </summary>
        private void LoadData()
        {
            SqlDataReader dataReader = null;
            SqlHelper sqlHelper = new SqlHelper();

            try
            {
                sqlHelper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

                // Load MET data
                dataReader = sqlHelper.GetReader("GetCycleMetValues");

                // Add all the MET data to the list 
                while (dataReader.Read())
                {
                    CycleMetValue tempMet = new CycleMetValue();
                    tempMet.MetValue = dataReader.GetDecimal(0);
                    tempMet.MinSpeed = dataReader.GetDecimal(1);
                    tempMet.MaxSpeed = dataReader.GetDecimal(2);
                    CycleMetValues.Add(tempMet);
                }
            }
            catch (SqlException sqlEx)
            {
                string message = "SqlException : " + sqlEx.Message;
                OperationalEvent oe = new OperationalEvent(
                    TDEventCategory.Database,
                    TDTraceLevel.Error,
                    message,
                    sqlEx);
                Logger.Write(oe);
                throw new TDException(
                    message,
                    sqlEx,
                    true,
                    TDExceptionIdentifier.CYCalorieMETDataSQLCommandFailed);
            }
            finally
            {
                // Release database resources
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                if (sqlHelper.ConnIsOpen)
                {
                    sqlHelper.ConnClose();
                }
            }
        }

    }
}
