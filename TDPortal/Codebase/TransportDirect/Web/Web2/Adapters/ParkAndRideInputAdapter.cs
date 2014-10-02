// *********************************************** 
// NAME                 : ParkAndRideInputAdapter.cs
// AUTHOR               : Tolu Olomolaiye
// DATE CREATED         : 29 Mar 2006 
// DESCRIPTION  : Input Adapter for Park And Ride
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/ParkAndRideInputAdapter.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 12:59:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:26   mturner
//Initial revision.
//
//   Rev 1.3   Apr 20 2006 15:56:06   tmollart
//Removed DisableLocailityQuery method calls.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.2   Apr 05 2006 16:26:04   mdambrine
//propertyservice reference was missing.
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.1   Apr 05 2006 15:42:52   build
//Automatically merged from branch for stream0030
//
//   Rev 1.0.1.0   Mar 29 2006 18:38:58   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.0   Mar 29 2006 16:49:54   tolomolaiye
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;

using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Park and Ride Input Adapter 
	/// </summary>
	public class ParkAndRideInputAdapter : FindCarInputAdapter
	{
		IDataServices populator = null;

		/// <summary>
		/// Constructor 
		/// </summary>
		public ParkAndRideInputAdapter(TDJourneyParametersMulti journeyParams, FindCarPageState pageState, InputPageState inputPageState) :
			base(journeyParams, pageState, inputPageState)
		{
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];		
		}

		/// <summary>
		/// overide method of base class. Modify so that fixed locations are not overwritten
		/// </summary>
		public override void InitJourneyParameters()
		{
			base.InitJourneyParameters();

			// Also Initialise the ValidationError object
			if (TDSessionManager.Current.ValidationError != null)
				TDSessionManager.Current.ValidationError.Initialise();

			if (journeyParams.Origin.LocationFixed)
			{
				journeyParams.Destination.SearchType = SearchType.Locality;
			}
			else if (journeyParams.Destination.LocationFixed)
			{
				journeyParams.Origin.SearchType = SearchType.Locality;
			}
			else
			{	
				journeyParams.Origin.SearchType = SearchType.Locality;
				journeyParams.Destination.SearchType = SearchType.Locality;
			}

			journeyParams.PrivateViaType = new 
				TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
			journeyParams.PrivateVia = new LocationSearch();

			journeyParams.PrivateVia.SearchType = SearchType.Locality;

			journeyParams.AvoidMotorWays = false;
			journeyParams.AvoidFerries = false;
			journeyParams.AvoidTolls = false;
			journeyParams.DoNotUseMotorways = false;

			string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.DrivingFindDrop);
			journeyParams.PrivateAlgorithmType = (PrivateAlgorithmType)Enum.Parse(typeof(PrivateAlgorithmType),defaultItemValue);

			journeyParams.DrivingSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.DrivingMaxSpeedDrop), TDCultureInfo.CurrentCulture);
			journeyParams.PrivateRequired = true;
			journeyParams.PublicRequired = false;
			journeyParams.PublicModes = new ModeType[0];
			journeyParams.CarSize = populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
			journeyParams.CarFuelType = populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
			journeyParams.FuelConsumptionUnit = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.UnitsDrop), TDCultureInfo.CurrentCulture);

			LoadTravelDetails();
		}

		/// <summary>
		/// Retrieves a AsyncCallState object for Park and Ride plans
		/// </summary>
		/// <returns></returns>
		public override void InitialiseAsyncCallState()
		{
			AsyncCallState acs = new JourneyPlanState();

			// Determine refresh interval and resource string for the wait page from parameters passed in
			acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.ParkAndRide"]);
			acs.WaitPageMessageResourceFile = "langStrings";
			acs.WaitPageMessageResourceId = "WaitPageMessage.ParkAndRide";

			acs.AmbiguityPage = PageId.ParkAndRideInput;
            acs.DestinationPage = PageId.JourneyDetails;
            acs.ErrorPage = PageId.JourneyDetails;
			acs.Status = AsyncCallStatus.None;
			tdSessionManager.AsyncCallState = acs;
		}
	}
}