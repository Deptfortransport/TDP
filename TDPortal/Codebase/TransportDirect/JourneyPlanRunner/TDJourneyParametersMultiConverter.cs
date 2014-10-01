// ****************************************************************
// NAME         : TDJourneyParametersMultiConverter.cs
// AUTHOR       : Andrew Sinclair
// DATE CREATED : 2005-06-07
// DESCRIPTION  : 
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/TDJourneyParametersMultiConverter.cs-arc  $
//
//   Rev 1.7   Dec 05 2012 14:11:38   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.6   Nov 16 2012 14:00:50   DLane
//Setting walk parameters in the journey request
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.5   Nov 13 2012 13:15:02   mmodi
//Added TDAccessiblePreferences
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.4   Sep 01 2011 10:43:38   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.3   Mar 14 2011 15:11:56   RPhilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.2   Oct 12 2009 09:11:02   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 08:39:48   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Feb 02 2009 16:45:28   mmodi
//Populate Routing Guide properties for the request
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:24:46   mturner
//Initial revision.
//
//   Rev 1.1   Jun 21 2005 14:59:42   asinclair
//Added IsTrunkRequest to constructor
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.0   Jun 15 2005 14:20:52   asinclair
//Initial revision.

using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;
using MeasureConvert = System.Convert;
using System.Collections.Generic;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for TDJourneyParametersMultiConverter.
	/// </summary>
	[Serializable]
	public class TDJourneyParametersMultiConverter  : ITDJourneyParameterConverter
	{
		private bool isTrunkRequest;

		public TDJourneyParametersMultiConverter(bool isTrunkRequest)
		{
			this.isTrunkRequest = isTrunkRequest;
        }

		public ITDJourneyRequest Convert (TDJourneyParameters parameters, TDDateTime outwardDateTime, TDDateTime returnDateTime)
		{

			//Guid cjpCallRequestID =   SetInitialJourneyPlanStateData( journeyPlanStateData );

			ITDJourneyRequest request = new TDJourneyRequest();

			TDJourneyParametersMulti jp = (TDJourneyParametersMulti)parameters; 			

			request.AlternateLocations = jp.AlternateLocations;
			request.AlternateLocationsFrom = jp.AlternateLocationsFrom;
			request.AvoidMotorways = jp.AvoidMotorWays;
			request.AvoidFerries = jp.AvoidFerries;
			request.AvoidTolls = jp.AvoidTolls;
            request.BanUnknownLimitedAccess = jp.BanUnknownLimitedAccess;
			request.DoNotUseMotorways = jp.DoNotUseMotorways;
			request.VehicleType = jp.VehicleType;
			request.DestinationLocation = jp.DestinationLocation;
			request.DrivingSpeed = jp.DrivingSpeed;
			request.InterchangeSpeed = jp.InterchangeSpeed;
			request.IsReturnRequired = jp.IsReturnRequired && returnDateTime != null; 
			request.MaxWalkingTime = jp.MaxWalkingTime;
			request.OriginLocation = jp.OriginLocation;
			request.OutwardArriveBefore = jp.OutwardArriveBefore;
			string fuelConsumption = "";
			//Only convert the value of fuel consumption if the value is entered since for the average option,  the value is already in meters per litre in the database table
            
			if (jp.FuelConsumptionOption == false)
			{
				if( jp.FuelConsumptionUnit == 1 )
				{
				
					// Translate from GallonsPerMile to LitresPerMeter
					fuelConsumption = MeasurementConversion.Convert(MeasureConvert.ToDouble( jp.FuelConsumptionEntered ), ConversionType.MilesPerGallonToMetersPerLitre).ToString();
				}
				else
				{
					// Translate from GallonsPerMile to LitresPerMeter
					fuelConsumption = MeasurementConversion.Convert((MeasureConvert.ToDouble( jp.FuelConsumptionEntered )), ConversionType.LitresPer100KilometersToMetersPerLitre).ToString();
				}
			}
			else
			{
				fuelConsumption = jp.FuelConsumptionEntered;
			}
			request.FuelConsumption = fuelConsumption;

			//Only convert the value of fuel cost if the value is entered since for the average option, the value is already in tenth of pence per litre in the database table
			if (jp.FuelCostOption == false)
			{
				request.FuelPrice = MeasurementConversion.Convert((MeasureConvert.ToDouble(jp.FuelCostEntered)), ConversionType.TenthOfPencePerLitre).ToString();
			}
			else
			{
				request.FuelPrice = jp.FuelCostEntered;
			}
			if (outwardDateTime != null)
			{
				request.OutwardDateTime = new TDDateTime[1];
				request.OutwardDateTime[0] = outwardDateTime;
			} 
			else 
			{
				request.OutwardDateTime = new TDDateTime[0];
			}
			
			request.OutwardAnyTime = jp.OutwardAnyTime;
			request.PrivateViaLocation = jp.PrivateViaLocation;
			request.PublicAlgorithm = jp.PublicAlgorithmType;
			request.PrivateAlgorithm = jp.PrivateAlgorithmType;
			
			request.PublicViaLocations = new TDLocation[1];
			request.PublicViaLocations[0] = jp.PublicViaLocation;

			request.ReturnDestinationLocation = jp.ReturnDestinationLocation;
			request.ReturnOriginLocation = jp.ReturnOriginLocation;
			request.ReturnArriveBefore = jp.ReturnArriveBefore;

			if (returnDateTime != null)
			{
				request.ReturnDateTime = new TDDateTime[1];
				request.ReturnDateTime[0] = returnDateTime;
			} 
			else 
			{
				request.ReturnDateTime = new TDDateTime[0];
			}
			
			request.ReturnAnyTime = jp.ReturnAnyTime;
			request.WalkingSpeed = jp.WalkingSpeed;
			request.UseOnlySpecifiedOperators = jp.OnlyUseSpecifiedOperators;
			request.SelectedOperators = jp.SelectedOperators;
			request.IsTrunkRequest = isTrunkRequest;
			request.ExtraCheckinTime = DateTime.MinValue;
			request.DirectFlightsOnly = jp.DirectFlightsOnly;

			ArrayList avoidRoads = new ArrayList();
			for( int i = 0; i < jp.AvoidRoadsList.Length; i++)
			{
				TDRoad road = jp.AvoidRoadsList[i];
				if( road != null && road.RoadName != string.Empty )
				{
					avoidRoads.Add( road.RoadName );
				}
			}
			request.AvoidRoads =  (string[]) avoidRoads.ToArray( string.Empty.GetType() );

            // Array of toids to be avoided when planning road journey
            request.AvoidToidsOutward = jp.AvoidToidsListOutward;
            request.AvoidToidsReturn = jp.AvoidToidsListReturn;

			request.AvoidFerries = jp.AvoidFerries;
			request.AvoidMotorways = jp.AvoidMotorWays;
			request.AvoidTolls = jp.AvoidTolls;
            request.BanUnknownLimitedAccess = jp.BanUnknownLimitedAccess;
			request.DoNotUseMotorways = jp.DoNotUseMotorways;


			ArrayList includeRoads = new ArrayList();
			for( int i = 0; i < jp.UseRoadsList.Length; i++)
			{
				TDRoad road = jp.UseRoadsList[i];
				if( road != null && road.RoadName != string.Empty )
				{
					includeRoads.Add( road.RoadName );
				}
			}
			request.IncludeRoads =  (string[]) includeRoads.ToArray( string.Empty.GetType() );

			request.FuelType = jp.CarFuelType; 
			request.CarSize = jp.CarSize;


			request.Modes = new ModeType[jp.PublicModes.Length + (jp.PrivateRequired ? 1 : 0)];

			Array.Copy(jp.PublicModes, request.Modes, jp.PublicModes.Length);

			if	(jp.PrivateRequired) 
			{
				request.Modes[request.Modes.Length - 1] = ModeType.Car;
				request.VehicleType = VehicleType.Car;
			}

            request.RoutingGuideInfluenced = jp.RoutingGuideInfluenced;
            request.RoutingGuideCompliantJourneysOnly = jp.RoutingGuideCompliantJourneysOnly;
            request.RouteCodes = jp.RouteCodes;

            request.IgnoreCongestion = jp.IgnoreCongestion;
            request.CongestionValue = jp.CongestionValue;

            #region Accessible preferences 

            // Accessible journey preferences
            TDAccessiblePreferences accessiblePreferences = new TDAccessiblePreferences();
            accessiblePreferences.DoNotUseUnderground = jp.DoNotUseUnderground;
            accessiblePreferences.RequireSpecialAssistance = jp.RequireSpecialAssistance;
            accessiblePreferences.RequireStepFreeAccess = jp.RequireStepFreeAccess;
            accessiblePreferences.RequireFewerInterchanges = jp.RequireFewerInterchanges;

            request.AccessiblePreferences = accessiblePreferences;

            // Override for accessible preferences
            if (accessiblePreferences.Accessible)
            {
                // Update the walk speed and distance
                if ((jp.AccessibleWalkDistance > 0) && (jp.AccessibleWalkSpeed > 0))
                {
                    request.WalkingSpeed = jp.AccessibleWalkSpeed;
                    request.MaxWalkingDistance = jp.AccessibleWalkDistance;
                }

                // Remove underground from modes list if it was selected
                if (accessiblePreferences.DoNotUseUnderground)
                {
                    List<ModeType> modes = new List<ModeType>(request.Modes);
                    if (modes.Contains(ModeType.Underground))
                    {
                        modes.Remove(ModeType.Underground);
                    }
                    request.Modes = modes.ToArray();
                }

                // Fewer changes journey algorithm
                if (accessiblePreferences.RequireFewerInterchanges)
                {
                    request.PublicAlgorithm = PublicAlgorithmType.MinChanges;
                }

                // Accessible request (CJP flag to perform single region journey planning)
                request.AccessibleRequest = Parse(Properties.Current["AccessibleOptions.AccessibleRequest.Olympic"]);

                // Dont force coach (CJP override flag)
                request.DontForceCoach = UpdateDontForceCoach(jp, accessiblePreferences);

                // Remove awkward overnight journeys flag
                request.RemoveAwkwardOvernight = Parse(Properties.Current["AccessibleOptions.RemoveAwkwardOvernight"]);
            }

            

            #endregion

            return ( request );	
		}

        /// <summary>
        /// Updates the dont force coach flag in the journey request
        /// </summary>
        /// <param name="request"></param>
        private bool UpdateDontForceCoach(TDJourneyParametersMulti jp, TDAccessiblePreferences accessiblePreferences)
        {
            // Default false to let CJP determine how to apply force coach rule
            bool dontForceCoach = false;

            #region Dont Force Coach

            IPropertyProvider pp = Properties.Current;

            // Dont force coach is set using Property switches, and for the following scenarios only

            // If PT journey...
            if (jp.PublicModes.Length > 0)
            {
                string londonAdminAreaCode = pp["AccessibleOptions.AdminAreaCode.London"];
                string naptanCoachPrefix = pp["FindA.NaptanPrefix.Coach"];
                
                bool propertyDontForceCoach_Accessible_OriginDestinationLondon = Parse(pp["AccessibleOptions.DontForceCoach.OriginDestinationLondon"]);
                bool propertyDontForceCoach_Accessible_FewerChanges = Parse(pp["AccessibleOptions.DontForceCoach.FewerChanges"]);

                // ... and is accessible request (step free/assistance only),
                // ... and both origin and destination are in london (adminarea = 82),
                // ... and neither location is coach (naptan = 9000)
                if ((accessiblePreferences.RequireStepFreeAccess || accessiblePreferences.RequireSpecialAssistance)
                    && (jp.OriginLocation != null 
                        && jp.OriginLocation.AdminDistrict.NPTGAdminCode == londonAdminAreaCode
                        && jp.OriginLocation.NaPTANs.Length > 0 
                        && !jp.OriginLocation.NaPTANs[0].Naptan.StartsWith(naptanCoachPrefix) )
                    && (jp.DestinationLocation != null 
                        && jp.DestinationLocation.AdminDistrict.NPTGAdminCode == londonAdminAreaCode
                        && jp.DestinationLocation.NaPTANs.Length > 0
                        && !jp.DestinationLocation.NaPTANs[0].Naptan.StartsWith(naptanCoachPrefix))
                    && propertyDontForceCoach_Accessible_OriginDestinationLondon)
                {
                    dontForceCoach = true;
                }
                // ... or fewer changes required
                else if (accessiblePreferences.RequireFewerInterchanges
                    && propertyDontForceCoach_Accessible_FewerChanges)
                {
                    dontForceCoach = true;
                }
            }

            #endregion

            return dontForceCoach;
        }

        /// <summary>
        /// Parses a string value into a bool. Default is false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool Parse(string value)
        {
            bool result = false;

            bool.TryParse(value, out result);

            return result;
        }
	}
}
