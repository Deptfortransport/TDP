// *********************************************** 
// NAME                 : FindCarPageState.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/07/2004 
// DESCRIPTION  : Page state for FindCar input page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindCarPageState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:22   mturner
//Initial revision.
//
//   Rev 1.2   Apr 20 2005 12:21:42   Ralavi
//Changes to ensure that original values are retained for car costing when selecting back button
//
//   Rev 1.1   Jan 31 2005 16:57:18   tmollart
//Changed reinstatejourneyparameters and savejourneyparameters methods to use TDJourneyParams instead of TDJourneyParamsMulti.
//
//   Rev 1.0   Jul 29 2004 17:19:26   passuied
//Initial Revision


using System;

using TransportDirect.UserPortal.LocationService;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;


namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for FindCarPageState.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class FindCarPageState : FindPageState
	{

		#region Declaration
		private LocationSearch privateViaLocationSearch;
		private TDLocation privateViaLocation;
		private LocationSelectControlType privateViaType;
        private string fuelConsumptionEntered;
        private string fuelCostEntered;
        private bool fuelConsumptionOption;
        private bool fuelCostOption;
        private int fuelConsumptionUnit;
        private TDRoad[] avoidRoadsList; 
        private TDRoad[] useRoadsList;
        

		#endregion


		public FindCarPageState()
		{
			this.findMode = FindAMode.Car;
		}

		/// <summary>
		/// Sets the journey parameters currently associated with the session to be those
		/// stored by this object, saved by previously calling SaveJourneyParameters()
		/// </summary>
		public override void ReinstateJourneyParameters(TDJourneyParameters journeyParameters) 
		{

			TDJourneyParametersMulti journeyParams = journeyParameters as TDJourneyParametersMulti;

			journeyParams.PrivateVia = privateViaLocationSearch;
			journeyParams.PrivateViaLocation = privateViaLocation;
			journeyParams.PrivateViaType = privateViaType;
            journeyParams.FuelConsumptionEntered = fuelConsumptionEntered;
            journeyParams.FuelCostEntered = fuelCostEntered;
            journeyParams.FuelConsumptionOption = fuelConsumptionOption;
            journeyParams.FuelCostOption = fuelCostOption;
            journeyParams.FuelConsumptionUnit = fuelConsumptionUnit;
            journeyParams.AvoidRoadsList = avoidRoadsList;
            journeyParams.UseRoadsList = useRoadsList;


			base.ReinstateJourneyParameters(journeyParameters);
		}

		/// <summary>
		/// Stores (references) of the journey parameters currently associated with the
		/// session so that they may be reinstated when switching from ambiguity mode
		/// back to input mode
		/// </summary>
		public override void SaveJourneyParameters(TDJourneyParameters journeyParameters) 
		{
			TDJourneyParametersMulti journeyParams = journeyParameters as TDJourneyParametersMulti;

			privateViaLocationSearch = journeyParams.PrivateVia;
			privateViaLocation = journeyParams.PrivateViaLocation;
			privateViaType = journeyParams.PrivateViaType;
            fuelConsumptionEntered = journeyParams.FuelConsumptionEntered;
            fuelCostEntered = journeyParams.FuelCostEntered;
            fuelConsumptionOption = journeyParams.FuelConsumptionOption;
            fuelCostOption = journeyParams.FuelCostOption;
            fuelConsumptionUnit = journeyParams.FuelConsumptionUnit;
            avoidRoadsList = journeyParams.AvoidRoadsList;
            useRoadsList = journeyParams.UseRoadsList;

			base.SaveJourneyParameters(journeyParameters);
            
		}


	}
}
