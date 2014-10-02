// *********************************************************
// NAME 		: TDJourneyParametersVisitPlan.cs
// AUTHOR 		: Tim 
// DATE CREATED : 05/09/2005
// DESCRIPTION 	: 
//
// **********************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDJourneyParametersVisitPlan.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:40   mturner
//Initial revision.
//
//   Rev 1.5   Nov 23 2005 16:39:42   tolomolaiye
//Fix for IR 3155
//Resolution for 3155: Visit Planner - Default Return to starting location option
//
//   Rev 1.4   Oct 20 2005 09:51:38   asinclair
//Changed constructor
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Oct 11 2005 16:29:08   tmollart
//Changed Constructor
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Sep 27 2005 08:48:16   pcross
//Initialisation of arrays controlling number of locations, stopover times, etc
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 21 2005 10:59:08   asinclair
//Added Del 7.1 code
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 13 2005 11:42:22   tmollart
//Initial revision.

using System;
using System.Text;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for TDJourneyParametersVisitPlan.
	/// </summary>
	[Serializable()][CLSCompliant(false)]
	public class TDJourneyParametersVisitPlan : TDJourneyParameters
	{

		#region Declarations

		/// <summary>
		/// Private storage specifying maximum size of a segment.
		/// </summary>
		/// <remarks>This will be used to initialise arrays controlling number of locations, stopover times, etc</remarks>
		private int maxSegmentSize;

		/// <summary>
		/// Private storage for array of visit locations.
		/// </summary>
		private TDLocation[] visitLocation;
		
		/// <summary>
		/// Private storage for array of visit location types.
		/// </summary>
		private TDJourneyParameters.LocationSelectControlType[] visitLocationTypes;
		
		/// <summary>
		/// Private storage for array of visit location searches.
		/// </summary>
		private LocationSearch[] visitLocationSearches;
		
		/// <summary>
		/// Private storage for stay durations between visit locations in minutes.
		/// </summary>
		private int[] stayDuration;

		/// <summary>
		/// Private storage to determine if the user plans to return to their
		/// origin location after their visit locations.
		/// </summary>
		private bool returnToOrigin;

		#endregion

		#region Constructor
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TDJourneyParametersVisitPlan()
		{
			// Initialise arrays.
			maxSegmentSize = int.Parse(Properties.Current["VisitPlan.MaxSegmentSize"]);
			
			visitLocation = new TDLocation[maxSegmentSize];
			visitLocationTypes = new LocationSelectControlType[maxSegmentSize];
			visitLocationSearches = new LocationSearch[maxSegmentSize];
			stayDuration = new int[maxSegmentSize - 1];

			// Populate arrays with new items.
			for (int i=0; i < maxSegmentSize; i++)
			{
				visitLocation[i] = new TDLocation();
				visitLocationTypes[i] = new LocationSelectControlType(ControlType.Default);
				
				visitLocationSearches[i] = new LocationSearch();
				visitLocationSearches[i].SearchType = GetDefaultSearchType(DataServiceType.VisitPlannerLocationSelection);
				returnToOrigin = true;
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Read/Write. Sets if the user is returning to their origin location
		/// after the visit location(s).
		/// </summary>
		public bool ReturnToOrigin
		{
			get{ return returnToOrigin; }
			set{ returnToOrigin = value; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Returns location object from specified index location.
		/// </summary>
		/// <param name="index">Index of object to return</param>
		/// <returns>TDLocation object</returns>
		public TDLocation GetLocation(int index)
		{
			return visitLocation[index];
		}


		/// <summary>
		/// Return location select control type from spcified index location.
		/// </summary>
		/// <param name="index">Index of object to return</param>
		/// <returns>TDJourneyParameters.LocationSelectControlType</returns>
		public TDJourneyParameters.LocationSelectControlType GetLocationType(int index)
		{
			return visitLocationTypes[index];
		}


		/// <summary>
		/// Return location search object for the specified index.
		/// </summary>
		/// <param name="index">Index of object to return</param>
		/// <returns>LocationSearch object</returns>
		public LocationSearch GetLocationSearch(int index)
		{
			return visitLocationSearches[index];
		}


		/// <summary>
		/// Returns the stay duration for the specified index.
		/// </summary>
		/// <param name="index">Index of duration to return</param>
		/// <returns>Integer containing duration (in minutes)</returns>
		public int GetStayDuration(int index)
		{
			return stayDuration[index]; //In minutes
		}


		/// <summary>
		/// Sets the location object for the spcified index.
		/// </summary>
		/// <param name="index">Index of location object to set</param>
		/// <param name="location">TDLocation object</param>
		public void SetLocation(int index, TDLocation location)
		{
			visitLocation[index] = location;
		}


		/// <summary>
		/// Sets the location type object for the specified index
		/// </summary>
		/// <param name="index">Index of the location type object to set</param>
		/// <param name="locationType">LocationSelectControlType object</param>
		public void SetLocationType(int index, TDJourneyParameters.LocationSelectControlType locationType)
		{
			visitLocationTypes[index] = locationType;
		}


		/// <summary>
		/// Sets the location search for the specified index
		/// </summary>
		/// <param name="index">Index of the location type object to set</param>
		/// <param name="locationSearch">LocationSearch object</param>
		public void SetLocationSearch(int index, LocationSearch locationSearch)
		{
			visitLocationSearches[index] = locationSearch;
		}


		/// <summary>
		/// Sets the stay duration.
		/// </summary>
		/// <param name="index">Index of stay duration to set</param>
		/// <param name="minutes">Time in minutes</param>
		public void SetStayDuration(int index, int minutes)
		{
			stayDuration[index] = minutes;
		}


		/// <summary>
		/// Returns a formatted string containing information about the
		/// requested visit.
		/// </summary>
		/// <returns></returns>
		public override string InputSummary()
		{
			StringBuilder summary = new StringBuilder();

			//Initial location
			summary.Append("From: ");
			summary.Append(visitLocation[0].Description);
			summary.Append("\n");

			//First visit and stop over time
			summary.Append("First visit: ");
			summary.Append(visitLocation[1].Description);
			summary.Append("\n");
			summary.Append("Length of stay: ");
			summary.Append(stayDuration[0].ToString());
			summary.Append("(mins)");
			summary.Append("\n");

			//Second visit and stop over time
			summary.Append("Second visit: ");
			summary.Append(visitLocation[2].Description);
			summary.Append("\n");
			summary.Append("Length of stay: ");
			summary.Append(stayDuration[1].ToString());
			summary.Append("(mins)");
			summary.Append("\n");

			//Return to origin
			summary.Append("Return to origin: ");
			summary.Append(returnToOrigin);
			summary.Append("\n");

			//Visit date/time
			summary.Append("Visit date/time: ");
			summary.Append(base.GetFormattedOutwardDateTime());
			summary.Append("\n");

			//Options
			string options = base.GetFormattedPTOptions();
			if (options.Length > 0)
			{
				summary.Append("Options:");
				summary.Append("\n");
				summary.Append(options);
			}

			//Return final output to the user
			return summary.ToString();
		}

		#endregion

	}
}
