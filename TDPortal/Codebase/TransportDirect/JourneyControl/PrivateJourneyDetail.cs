// *********************************************** 
// NAME         : PrivateJourneyDetail.cs
// AUTHOR       : Richard Hopkins
// DATE CREATED : 10/03/2006
// DESCRIPTION  : This class represents the high level view of the private journey.
//				  It is the complement to the PublicJourneyDetails class.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/PrivateJourneyDetail.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:54   mturner
//Initial revision.
//
//   Rev 1.0   Mar 10 2006 19:13:00   rhopkins
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//

using System;
using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for PublicJourneyDetail.
	/// </summary>
	[Serializable()]
	public class PrivateJourneyDetail : JourneyLeg
	{
		#region Constructors

		/// <summary>
		/// Default constructor - defined to allow XML serialisation.
		/// </summary>
		public PrivateJourneyDetail()
		{
		}

		/// <summary>
		/// Constructor to create journey leg for a private journey
		/// </summary>
		/// <param name="departLocation">Location from which the journey departs</param>
		/// <param name="arriveLocation">Location at which the journey arrives</param>
		/// <param name="departDateTime">Date/time at which the journey departs</param>
		/// <param name="arriveDateTime">Date/time at which the journey arrives</param>
		public PrivateJourneyDetail( ModeType mode, TDLocation departLocation, TDLocation arriveLocation, TDDateTime departDateTime, TDDateTime arriveDateTime)
		{
			this.mode = mode;

			this.legStart = new PublicJourneyCallingPoint(
				departLocation,
				null,
				departDateTime,
				PublicJourneyCallingPointType.Board);

			this.legEnd = new PublicJourneyCallingPoint(
				arriveLocation,
				arriveDateTime,
				null,
				PublicJourneyCallingPointType.Alight);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the array of vehicle feature indicators.
		/// There are none for a private journey.
		/// </summary>
		public override int[] GetVehicleFeatures()
		{
			return new int[0];
		}

		/// <summary>
		/// Gets the Display Notes.
		/// There are none for a private journey.
		/// </summary>
		public override string[] GetDisplayNotes()
		{
			return new string[0];
		}

		#endregion
	}
}
