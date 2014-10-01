// *********************************************** 
// NAME         : JourneyLeg.cs
// AUTHOR       : Richard Hopkins
// DATE CREATED : 19/01/2006
// DESCRIPTION  : This class represents the high level view of a single journey leg.
//				  It contains the properties and methods that are common to the PrivateJourneyDetails and PublicJourneyDetails classes.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyLeg.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:48   mturner
//Initial revision.
//
//   Rev 1.3   Mar 22 2006 16:27:06   rhopkins
//Minor FxCop fixes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Mar 10 2006 18:59:06   rhopkins
//Made this class abstract.
//Also removed JourneyDetail class and general code review tidy.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 15 2006 15:34:34   tmollart
//Added properties to get journey leg start/end times.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Jan 26 2006 20:05:50   rhopkins
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
	public abstract class JourneyLeg
	{
		private int duration;

		protected ModeType mode;

		protected PublicJourneyCallingPoint legStart;			// boarding point
		protected PublicJourneyCallingPoint legEnd;				// alighting point

		#region Constructors

		/// <summary>
		/// Default constructor - defined to allow XML serialisation.
		/// </summary>
		protected JourneyLeg()
		{
		}

		#endregion


		#region Abstract Properties

		/// <summary>
		/// Gets the array of vehicle feature indicators  
		/// </summary>
		public abstract int[] GetVehicleFeatures();

		/// <summary>
		/// Gets the Display Notes
		/// </summary>
		public abstract string[] GetDisplayNotes();

		#endregion


		#region Concrete Properties

		/// <summary>
		/// Read/write property. Transport mode for this leg.
		/// </summary>
		/// <remarks>
		/// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
		/// </remarks>
		public ModeType Mode
		{
			get { return mode; }
			set { mode = value; }
		}

		/// <summary>
		/// Read/write property. Calling point at the start of this leg (boarding point).
		/// </summary>
		/// <remarks>
		/// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
		/// </remarks>
		public PublicJourneyCallingPoint LegStart
		{
			get { return legStart; }
			set { legStart = value; }
		}

		/// <summary>
		/// Read/write property. Calling point at the end of this leg (alighting point).
		/// </summary>
		/// <remarks>
		/// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
		/// </remarks>
		public PublicJourneyCallingPoint LegEnd
		{
			get { return legEnd; }
			set { legEnd = value; }
		}

		/// <summary>
		/// Read-only property. Departure time for the start of this leg
		/// </summary>
		public virtual TDDateTime StartTime
		{
			get { return legStart.DepartureDateTime; }
		}

		/// <summary>
		/// Read-only property. Arrival time for the end of this leg
		/// </summary>
		public virtual TDDateTime EndTime
		{
			get { return legEnd.ArrivalDateTime; }
		}

		/// <summary>
		/// Read/write property. Duration of this leg in seconds.
		/// </summary>
		/// <remarks>
		/// Setter is provided so that TD Transaction Injector can serialize/deserialize the property in the subclasses.
		/// </remarks>
		public virtual int Duration
		{
			get { return (int)((legEnd.ArrivalDateTime.GetDateTime().Ticks - legStart.DepartureDateTime.GetDateTime().Ticks) / TimeSpan.TicksPerSecond); }
			set { duration = value; }
		}

		/// <summary>
		/// Read-only property. Indicates whether the Start or End location of this leg has invalid co-ordinates (less than or equal to 0)
		/// </summary>
		public bool HasInvalidCoordinates
		{
			get
			{
				return !(legStart.Location.GridReference.IsValid && legEnd.Location.GridReference.IsValid);
			}
		}

		#endregion
	}
}
