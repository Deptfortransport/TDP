// *********************************************** 
// NAME                 : FindFlightPageState.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 13/05/2003 
// DESCRIPTION  : Session state for the FindFlight page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindFlightPageState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:24   mturner
//Initial revision.
//
//   Rev 1.8   Oct 01 2004 11:03:10   COwczarek
//Make behaviour consistent for all Find A input pages when in ambiguity mode.
//Resolution for 1562: Find A input page in ambiguity mode always shows travel details
//
//   Rev 1.7   Sep 06 2004 19:04:50   jgeorge
//Added PrepareForAmendJourney method
//Resolution for 1255: Impossible to go back to change flight details
//
//   Rev 1.6   Aug 25 2004 10:13:56   jmorrissey
//IR1357. Added properties ResolvedFromLocation and ResolvedToLocation.
//
//   Rev 1.5   Jul 14 2004 13:01:28   passuied
//Changes in SessionManager for Del6.1. Compiles
//
//   Rev 1.4   Jun 10 2004 10:17:52   jgeorge
//Updated property names
//
//   Rev 1.3   Jun 09 2004 17:14:20   jgeorge
//Interim check in
//
//   Rev 1.2   Jun 02 2004 14:01:08   jgeorge
//Work in progress
//
//   Rev 1.1   May 26 2004 08:57:38   jgeorge
//Work in progress
//
//   Rev 1.0   May 13 2004 12:38:28   jgeorge
//Initial revision.

using System;
using System.Collections;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.SessionManager
{

	/// <summary>
	/// Enumeration for the possible ways of selecting airports
	/// </summary>
	public enum FlightLocationSelectionMethod
	{
		BrowseControl,
		FindStationTool
	}

	/// <summary>
	/// Session state for the FindFlight page
	/// </summary>
	[CLSCompliant(false)]
	[Serializable()]
	public class FindFlightPageState : FindPageState
	{
		#region Private variables

		private FlightLocationSelectionMethod originLocationSelectionMethod;
		private bool originLocationFixed;

		private FlightLocationSelectionMethod destinationLocationSelectionMethod;
		private bool destinationLocationFixed;

		private bool travelDetailsChanged;

		private TDLocation resolvedFromLocation = null;
		private TDLocation resolvedToLocation = null;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor. Initialises the object
		/// </summary>
		public FindFlightPageState()
		{
			findMode = FindAMode.Flight;
			Initialise();
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Resets all of the properties to their defaults
		/// </summary>
		public override void Initialise()
		{
			originLocationSelectionMethod = FlightLocationSelectionMethod.BrowseControl;
			originLocationFixed = false;
			destinationLocationSelectionMethod = FlightLocationSelectionMethod.BrowseControl;
			destinationLocationFixed = false;

			bTravelDetailsVisible = false;
			travelDetailsChanged = false;
            amendMode = false;
			
			bAmbiguityMode = false;

			ResolvedFromLocation = new TDLocation();
			ResolvedToLocation = new TDLocation();
		}

		/// <summary>
		/// Resets the outward location properties
		/// <seealso cref="OriginLocationSelectionMethod"/>
		/// <seealso cref="OriginLocationFixed"/>
		/// </summary>
		public void ResetOriginLocation()
		{
			originLocationSelectionMethod = FlightLocationSelectionMethod.BrowseControl;
			originLocationFixed = false;
		}

		/// <summary>
		/// Resets the return location properties
		/// <seealso cref="DestinationLocationSelectionMethod"/>
		/// <seealso cref="DestinationLocationFixed"/>
		/// </summary>
		public void ResetDestinationLocation()
		{
			destinationLocationSelectionMethod = FlightLocationSelectionMethod.BrowseControl;
			destinationLocationFixed = false;
		}

		public override void PrepareForAmendJourney()
		{
			base.PrepareForAmendJourney();

			// Ensure that origin and destination locations are fixed
			OriginLocationFixed = true;
			DestinationLocationFixed = true;

			// For consistencys sake, set the selection methods to dropdown
			// in both cases
			OriginLocationSelectionMethod = FlightLocationSelectionMethod.BrowseControl;
			DestinationLocationSelectionMethod = FlightLocationSelectionMethod.BrowseControl;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Specifies how the origin location was chosen
		/// </summary>
		public FlightLocationSelectionMethod OriginLocationSelectionMethod
		{
			get { return originLocationSelectionMethod; }
			set 
			{ 
				originLocationSelectionMethod = value; 
				if (value == FlightLocationSelectionMethod.FindStationTool)
					OriginLocationFixed = true;
			}
		}

		/// <summary>
		/// Whether or not the origin location is fixed
		/// </summary>
		public bool OriginLocationFixed
		{
			get { return originLocationFixed; }
			set 
			{ 
				originLocationFixed = value; 
				if (value == false)
					originLocationSelectionMethod = FlightLocationSelectionMethod.BrowseControl;
			}
		}

		/// <summary>
		/// Specifies how the origin location was chosen
		/// </summary>
		public FlightLocationSelectionMethod DestinationLocationSelectionMethod
		{
			get { return destinationLocationSelectionMethod; }
			set 
			{
				destinationLocationSelectionMethod = value; 
				if (value == FlightLocationSelectionMethod.FindStationTool)
					DestinationLocationFixed = true;
			}
		}

		/// <summary>
		/// Whether or not the destination location is fixed
		/// </summary>
		public bool DestinationLocationFixed
		{
			get { return destinationLocationFixed; }
			set 
			{
				destinationLocationFixed = value; 
				if (value == false)
					destinationLocationSelectionMethod = FlightLocationSelectionMethod.BrowseControl;
			}
		}

		

		/// <summary>
		/// Whether or not the travel details have been changed
		/// </summary>
		public bool TravelDetailsChanged
		{
			get { return travelDetailsChanged; }
			set { travelDetailsChanged = value; }
		}

		/// <summary>
		/// TDLocation object that stores the resolved from location for Find Flight
		/// </summary>
		public TDLocation ResolvedFromLocation
		{
			get
			{
				return resolvedFromLocation;
			}
			set
			{
				resolvedFromLocation = value;
			}
		}

		/// <summary>
		/// TDLocation object that stores the resolved to location for Find Flight
		/// </summary>
		public TDLocation ResolvedToLocation
		{
			get
			{
				return resolvedToLocation;
			}
			set
			{
				resolvedToLocation = value;
			}
		}

		#endregion


	}
}
