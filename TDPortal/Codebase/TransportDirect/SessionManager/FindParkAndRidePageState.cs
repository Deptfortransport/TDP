// *********************************************** 
// NAME                 : FindParkAndRidePageState.cs
// AUTHOR               : Scott Angle
// DATE CREATED         : 06/06/2007 
// DESCRIPTION  : Page state for FindParkAndRide input page
// ************************************************ 
//
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
	public class FindParkAndRidePageState : FindCarPageState
	{

		/// <summary>
		/// Constructor. Sets the Mode property to indicate Find A Coach mode.
		/// </summary>
		public FindParkAndRidePageState()
		{
			findMode = FindAMode.ParkAndRide;
		}
	}
}