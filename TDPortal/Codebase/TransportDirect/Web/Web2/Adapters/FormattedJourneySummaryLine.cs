//*********************************** 
// NAME			: FormattedJourneySummaryLine.cs
// AUTHOR		: Alistair Caunt
// DATE CREATED	: 11.05.005
// DESCRIPTION	: Provides a wrapper to JourneySummaryLine
// which incorporates all the methods to obtain displayable information
// (functionality was previously in SummaryResultTableControl)
/// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FormattedJourneySummaryLine.cs-arc  $
//
//   Rev 1.7   Sep 01 2010 09:31:58   apatel
//Updated to include a space in duration between hour number and 'hour/hours' string. Also removed a comma between hours and mins.
//Resolution for 5597: No space after hour number in journey results summary table
//
//   Rev 1.6   Feb 24 2010 17:34:30   mmodi
//If summary line has a duration specified, use that rather than calculating it
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Feb 12 2010 11:13:28   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Oct 21 2008 14:26:28   jfrank
//Updated for XHTML compliance
//Resolution for 5146: WAI AAA copmpliance work (CCN 474)
//
//   Rev 1.3   Oct 13 2008 16:41:26   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.1   Sep 17 2008 12:06:16   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Aug 22 2008 10:25:02   mmodi
//Updated display for cycle journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 12:59:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:22   mturner
//Initial revision.
//
//   Rev 1.20   May 21 2007 15:01:20   asinclair
//no longer replacing mins with m
//Resolution for 4412: 9.6 - WAI / Accessibility Issues
//
//   Rev 1.19   Mar 06 2007 12:29:52   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.18.1.0   Feb 20 2007 15:49:56   mmodi
//Added methods to return Departure and Arrival times without dates
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.18   Apr 04 2006 15:49:12   RGriffith
//IR3701 Fix: Addition of direction to FormattedJourneySummaryLine adapter to set correct origin/destination locations
//
//   Rev 1.17   Mar 20 2006 18:02:30   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.16   Mar 13 2006 15:25:28   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.13.1.1   Feb 24 2006 10:26:10   pcross
//Force the date onto a new line when journey out and return dates are shown with time
//
//   Rev 1.13.1.0   Feb 03 2006 14:39:54   pcross
//New method to get abbreviated times for leg durations
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.15   Feb 23 2006 17:01:42   RWilby
//Merged stream3129
//
//   Rev 1.14   Jan 04 2006 10:06:22   tolomolaiye
//Updates folllowing Visit Planner code review
//
//   Rev 1.13   Dec 21 2005 19:26:26   pcross
//Added AllModes from JourneySummaryLine
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.12   Sep 23 2004 12:23:20   RPhilpott
//Allow for empty list of operator names (eg, tube only journey)
//Resolution for 1629: No results returned for London Euston to Luton airport
//
//   Rev 1.11   Sep 20 2004 14:18:04   jmorrissey
//Updated GetDuration method in so that the duration time gets rounded up correctly.
//
//   Rev 1.10   Sep 20 2004 11:19:52   esevern
//IR1599: added check for journeySummaryLine destination description being null
//
//   Rev 1.9   Sep 17 2004 15:13:40   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.8   Aug 09 2004 16:13:38   jbroome
//IR 1258 - Ensure consistency of rounded time values.
//
//   Rev 1.7   Jul 19 2004 15:24:44   jgeorge
//Del 6.1 updates
//
//   Rev 1.6   Jun 22 2004 12:20:08   JHaydock
//FindMap page done. Corrections to printable map controls and pages. Various updates to Find pages.
//
//   Rev 1.5   Jun 10 2004 16:08:56   RHopkins
//Itinerary: changes to DisplayNumber and date display
//
//   Rev 1.4   Jun 08 2004 12:53:04   JHaydock
//Update to AmendSaveSendControl for Find A Flight plus formatting changes for Welsh display
//
//   Rev 1.3   Jun 03 2004 19:23:52   RHopkins
//Remove "." from end of option/journey number
//
//   Rev 1.2   May 24 2004 20:50:02   JHaydock
//Correction to GetOperatorNames as array can contain blank string entries
//
//   Rev 1.1   May 20 2004 18:03:00   JHaydock
//Intermediary check-in for FindSummary with FindSummaryResultControl operational
//
//   Rev 1.0   May 12 2004 14:11:44   acaunt
//Initial revision.
//
using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Web.Support;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Summary description for FormattedJourneySummaryLine.
	/// </summary>
	[Serializable]
	public class FormattedJourneySummaryLine : TDWebAdapter
	{
		private JourneySummaryLine summary;
		private int originalArrayIndex = -1;
		private double conversionFactor;
		private int requestedDay;
		private TimeSpan duration = new TimeSpan(0);
		private string originDescription = "";
		private string destinationDescription = "";
		private string displayNumber = "";
		private bool multipleDates = false;
		private bool forceDates = false;

		/// <summary>
		/// Construct an object using a JourneySummaryLine and other context information
		/// </summary>
		/// <param name="summary">The underlying JourneySummaryLine</param>
		/// <param name="requestedDay">The request day of the journey (may not be the same as the actual day)</param>
		/// <param name="conversionFactor">Mileage conversion factor</param>
		public FormattedJourneySummaryLine(JourneySummaryLine summary, int requestedDay, double conversionFactor)
		{
			this.summary = summary;
			this.requestedDay = requestedDay;
			this.conversionFactor = conversionFactor;
			this.originDescription = summary.OriginDescription;
			this.destinationDescription = summary.DestinationDescription;

			if (summary.DisplayNumber == "-1")
				this.displayNumber = Global.tdResourceManager.GetString("SummaryItineraryTable.fullItineraryRow", TDCultureInfo.CurrentUICulture);
			else
				this.displayNumber = summary.DisplayNumber;
		}

		/// <summary>
		/// Construct an object using a JourneySummaryLine and other context information
		/// </summary>
		/// <param name="summary">The underlying JourneySummaryLine</param>
		/// <param name="requestedDay">The request day of the journey (may not be the same as the actual day)</param>
		/// <param name="conversionFactor">Mileage conversion factor</param>
		public FormattedJourneySummaryLine(JourneySummaryLine summary, 
			int requestedDay, double conversionFactor, bool multipleDates)
			: this(summary, requestedDay, conversionFactor)
		{
			this.multipleDates = multipleDates;
			this.forceDates = true;
		}

		/// <summary>
		/// Construct an object using a JourneySummaryLine and other context information
		/// This constructor is used by FormattedJourneySummaryLines
		/// </summary>
		/// <param name="summary">The underlying JourneySummaryLine</param>
		/// <param name="originalArrayIndex">The underlying JourneySummaryLine's original array index location</param>
		/// <param name="requestedDay">The request day of the journey (may not be the same as the actual day)</param>
		/// <param name="conversionFactor">Mileage conversion factor</param>
		/// <param name="defaultOrigin">If there is no origin given in summary, then use this origin</param>
		/// <param name="defaultDestination">If there is no destination given in summary, then use this destination</param>
		/// <param name="outward">Bool to determine direction of FormattedJourneySummaryLine</param>
		public FormattedJourneySummaryLine(JourneySummaryLine summary, int originalArrayIndex, 
			int requestedDay, double conversionFactor, string defaultOrigin, string defaultDestination, bool outward)
		{
			this.summary = summary;
			this.originalArrayIndex = originalArrayIndex;
			this.conversionFactor = conversionFactor;
			this.requestedDay = requestedDay;

			if (summary.OriginDescription == "")
			{
				if (outward)
					this.originDescription = defaultOrigin;
				else
					this.originDescription = defaultDestination;
			}
			else
				this.originDescription = summary.OriginDescription;

			if (summary.DestinationDescription == "" || summary.DestinationDescription == null)
			{
				if (outward)
					this.destinationDescription = defaultDestination;
				else
					this.destinationDescription = defaultOrigin;
			}
			else
				this.destinationDescription = summary.DestinationDescription;

			if (summary.DisplayNumber == "-1")
				this.displayNumber = Global.tdResourceManager.GetString("SummaryItineraryTable.fullItineraryRow", TDCultureInfo.CurrentUICulture);
			else
				this.displayNumber = summary.DisplayNumber;
		}

		///<summary>Pass through of JourneySummaryLine property </summary>
		public int JourneyIndex
		{
			get { return summary.JourneyIndex; }
		}

		///<summary>Overridden pass through of JourneySummaryLine property </summary>
		public string OriginDescription
		{
			get { return originDescription; }
		}

		///<summary>Overridden pass through of JourneySummaryLine property </summary>
		public string DestinationDescription
		{
			get { return destinationDescription; }
		}

		///<summary>Pass through of JourneySummaryLine property </summary>
		public TDJourneyType Type
		{
			get { return summary.Type; }
		}

		///<summary>Pass through of JourneySummaryLine property </summary>
		public int InterchangeCount
		{
			get { return summary.InterchangeCount; }
		}

		///<summary>Pass through of JourneySummaryLine property </summary>
		public TDDateTime DepartureDateTime
		{
			get { return summary.DepartureDateTime; }
		}

		///<summary>Pass through of JourneySummaryLine property </summary>
		public TDDateTime ArrivalDateTime
		{
			get { return summary.ArrivalDateTime; }
		}

		///<summary>Pass through of JourneySummaryLine property </summary>
		public int RoadMiles
		{
			get { return summary.RoadMiles; }
		}

		///<summary>Pass through of JourneySummaryLine property </summary>
		public string DisplayNumber
		{
			get { return displayNumber; }
		}

		///<summary>Pass through of JourneySummaryLine property </summary>
		public string[] OperatorNames
		{
			get { return summary.OperatorNames; }
		}

		///<summary>Pass through of JourneySummaryLine property </summary>
		public ModeType[] AllModes
		{
			get { return summary.AllModes; }
		}

        /// <summary>
        /// Returns the all the modes used for this JourneySummaryLine
        /// </summary>
        public ModeType[] ModeTypes
        {
            get { return summary.Modes; }
        }
            

		///<summary>When dealing with an array of JourneySummaryLines, this
		///property can be used to store the original index position</summary>
		public int OriginalArrayIndex
		{
			get { return originalArrayIndex; }
		}

		/// <summary>
		/// Returns the origin description limited to approx 27 chars
		/// </summary>
		public string GetOriginDescription()
		{
			if (originDescription.Length > 27)
			{
				return originDescription.Substring(0, 25) + "...";
			}
			else
			{
				return originDescription;
			}
		}

		/// <summary>
		/// Returns the desination description limited to approx 27 chars
		/// </summary>
		public string GetDestinationDescription()
		{
			if (destinationDescription.Length > 27)
			{
				return destinationDescription.Substring(0, 25) + "...";
			}
			else
			{
				return destinationDescription;
			}
		}

		/// <summary>
		/// Returns a string of all the transport modes used.
		/// </summary>
		/// <returns>All the transport modes (comma seperated)</returns>
		public string GetTransportModes()
		{
			ModeType[] modeTypes = summary.Modes;
			int i;
			string resourceManagerKey;
			string mode;
			SortedList sortedModes;
			string modes = String.Empty;

			//Read the strings from Resourcing Manager given the enumerations.
			//Alphabetically sort the laguage-converted modes.
			sortedModes = new SortedList(modeTypes.Length);
			for(i = 0; i < modeTypes.Length; i++)
			{
                if (modeTypes[i] != ModeType.Transfer)
                {
                    resourceManagerKey = "TransportMode." + modeTypes[i].ToString();
                    mode = Global.tdResourceManager.GetString(resourceManagerKey, TDCultureInfo.CurrentUICulture);
                    sortedModes.Add(mode, mode);
                }
			}

			//Create the comma-separated mode list.
			modes = "";
			for(i = 0; i < sortedModes.Count - 1; i++)
			{
				modes += sortedModes.GetByIndex(i) + ", ";
			}
			//add final mode
			modes += sortedModes.GetByIndex(i);

			return modes;
		}

		/// <summary>
		/// Returns the departure time.
		/// </summary>
		/// <param name="summary">Current item being rendered.</param>
		/// <returns>Departure time</returns>
		public string GetDepartureTime()
		{
			TDDateTime time = summary.DepartureDateTime;

			if(time.Second >= 30)
				time = time.AddMinutes(1);

			// Check to see if the date is different from the request date.
			// For example, if the user has searched for a journey commencing on
			// a Sunday, but the first available train is on a Monday
			int actualDay = time.Day;
			if ((forceDates && multipleDates) || (!forceDates && (actualDay != requestedDay)))
			{
				// Days are different, return the time with the dates appended
				string date = time.ToString("dd/MM");
				return time.ToString("HH:mm") + "<br />(" + date + ")";
			}
			else
			{
				// Dates are the same, simply return the time.
				return time.ToString("HH:mm");
			}
		}

		/// <summary>
		/// Returns the departure time only, does not append the date of travel
		/// </summary>
		/// <param name="summary">Current item being rendered.</param>
		/// <returns>Departure time</returns>
		public string GetDepartureTimeNoDate()
		{
			TDDateTime time = summary.DepartureDateTime;

			if(time.Second >= 30)
				time = time.AddMinutes(1);

			return time.ToString("HH:mm");
		}

		/// <summary>
		/// Returns the arrival time
		/// </summary>
		/// <param name="summary">Current item being rendered.</param>
		/// <returns>Arrival time</returns>
		public string GetArrivalTime()
		{
			TDDateTime time = summary.ArrivalDateTime;

			if(time.Second >= 30)
				time = time.AddMinutes(1);
			
			// Check to see if it is a different day
			int actualDay = time.Day;
			if ((forceDates && multipleDates) || (!forceDates && (actualDay != requestedDay)))
			{
				// Days are different, return the time with the dates appended
				string date = time.ToString("dd/MM");
				return time.ToString("HH:mm") + "<br />(" + date + ")";
			}
			else
			{
				// Dates are the same, simply return the time.
				return time.ToString("HH:mm");
			}
		}

		/// <summary>
		/// Returns the arrival time only, does not append the date of travel
		/// </summary>
		/// <param name="summary">Current item being rendered.</param>
		/// <returns>Arrival time</returns>
		public string GetArrivalTimeNoDate()
		{
			TDDateTime time = summary.ArrivalDateTime;

			if(time.Second >= 30)
				time = time.AddMinutes(1);
			
			return time.ToString("HH:mm");
		}

		/// <summary>
		/// Returns the duration as a formatted string.
		/// </summary>
		/// <param name="summary">Current item being rendered.</param>
		/// <returns>Duration</returns>
		public string GetDuration()
		{
			CalculateDuration();

			string durationReturn = String.Empty;

			string hoursString = string.Format(" {0}",Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.hoursString", TDCultureInfo.CurrentUICulture));

			string minutesString = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.minutesString", TDCultureInfo.CurrentUICulture);

			string hourString = string.Format(" {0}",Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.hourString", TDCultureInfo.CurrentUICulture));

			string minuteString = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.minuteString", TDCultureInfo.CurrentUICulture);

			string milesString = Global.tdResourceManager.GetString(
				"SummaryResultTable.labelMilesString", TDCultureInfo.CurrentUICulture);

			//Get duration hours and minutes
			int durationHours = 0;
			int durationMinutes = 0;
            
			//Greater than 24 hours case
			if(duration.Days > 0)
			{
				// For each day, there are 24 hours
				durationHours = 24 * duration.Days + duration.Hours;
			}
			else if(duration.Hours != 0)
			{
				durationHours = duration.Hours;
			}
			
			durationMinutes = duration.Minutes;

			// Round up if necessary for consistency with start/end times.
			if (duration.Seconds >= 30)
				durationMinutes += 1;

			//IR1593 - if the rounding up of minutes takes it to 60,
			//then increment hours by 1 and set mins to zero
			if (durationMinutes == 60)
			{
				durationHours ++;
				durationMinutes = 0;
			}

			if (durationHours == 1)
				durationReturn += durationHours.ToString(TDCultureInfo.CurrentCulture.NumberFormat) + hourString;
			else if (durationHours > 1)
				durationReturn += durationHours.ToString(TDCultureInfo.CurrentCulture.NumberFormat) + hoursString;

			if(durationMinutes != 0)
			{
				// if hour was not equal to 0 then add a comma
				if(durationHours != 0)
					durationReturn += " ";

				// Check to see if minutes requires a 0 padding.
				// Pad with 0 only if an hour was present and minute is a single digit.
				if(durationMinutes < 10 & durationHours != 0)
					durationReturn += "0";

				durationReturn += durationMinutes.ToString(TDCultureInfo.CurrentCulture.NumberFormat);
				
				if(durationMinutes > 1)
					durationReturn += minutesString;
				else
					durationReturn += minuteString;
			}
			else if(durationHours == 0 && durationMinutes == 0)
			{
				// This leg has 0 hours 0 minutes, e.g. a journey to itself.
				// Should never really happen, but still required otherwise
				// no duration will be displayed.
				durationReturn += "0";

				if(durationMinutes > 1)
					durationReturn += minutesString;
				else
					durationReturn += minuteString;
			}

			// If journey type is by car or cycle, then concat the mileage to the duration.
			if((summary.Type == TDJourneyType.RoadCongested) || (summary.Type == TDJourneyType.Cycle))
			{
				// Add the mileage as well since a car is used, using the MileageConversion factor
				double result = (double)summary.RoadMiles/ conversionFactor;

				durationReturn += " / " + result.ToString("F1", TDCultureInfo.CurrentCulture.NumberFormat) + milesString;
			}

			return durationReturn;
		}

		/// <summary>
		/// Returns the duration as an abbreviated formatted string.
		/// </summary>
		/// <param name="summary">Current item being rendered.</param>
		/// <returns>Abbreviated duration</returns>
		/// <remarks>Based on existing GetDuration method. Uses result and replaces
		/// "hour" / "hours" with "h" and "min" / " mins" with "m". Also removes commas.</remarks>
		public string GetAbbreviatedDuration()
		{
			string duration = GetDuration();

			// The returned string will be of the format:
			// 22 mins					--> 22m
			// 1hour;					--> 1hr
			// 2hours;					--> 2hr
			// 3hours, 01min			--> 3hr 01m
			// 3hours, 12 mins			--> 3hr 12m
			// 27 mins / 12.1 miles		--> 27m / 12.1 miles

			// Get the old strings from resource file
			string oldHoursString = string.Format(" {0}",GetResource("JourneyDetailsTableControl.hoursString"));

			string oldMinutesString = GetResource("JourneyDetailsTableControl.minutesString");

			string oldHourString = string.Format(" {0}",GetResource("JourneyDetailsTableControl.hourString"));

			string oldMinuteString = GetResource("JourneyDetailsTableControl.minuteString");

			string oldMilesString = GetResource("SummaryResultTable.labelMilesString");

			// Get the replacement strings from resource file
			string newHourString = GetResource("FormatDuration.AbbreviatedHourString");

			string newMinuteString = GetResource("FormatDuration.AbbreviatedMinuteString");

			// Replace verbose with abbreviated versions
			duration = duration.Replace(oldHoursString, newHourString);
			duration = duration.Replace(oldHourString, newHourString);
			
			// Remove comma
			duration = duration.Replace(",", "");
			duration = duration.Replace("/", "<br />");

			return duration;
		}

		/// <summary>
		/// Returns journey duration as a TimeSpan
		/// </summary>
		public TimeSpan Duration
		{
			get
			{
				CalculateDuration();

				return duration;
			}
		}

		/// <summary>
		/// Calculates the journey duration only once.
		/// </summary>
		private void CalculateDuration()
		{
			if (duration != new TimeSpan(0))
				return;

            // If the summary line already has a duration specified, use that
            if (summary.Duration != new TimeSpan(0))
            {
                duration = summary.Duration;
                return;
            }

			TDDateTime arrivalTime = summary.ArrivalDateTime;
			TDDateTime departureTime = summary.DepartureDateTime;

			DateTime dateTimeArrivalTime = new DateTime
				(arrivalTime.Year, arrivalTime.Month, arrivalTime.Day, arrivalTime.Hour, arrivalTime.Minute, arrivalTime.Second, arrivalTime.Millisecond);

			DateTime dateTimeDepartureTime = new DateTime
				(departureTime.Year, departureTime.Month, departureTime.Day, departureTime.Hour, departureTime.Minute, departureTime.Second, departureTime.Millisecond);

			// find the difference between the two times
			duration = dateTimeArrivalTime.Subtract(dateTimeDepartureTime);
		}

		/// <summary>
		/// Returns a command name that should be associated with the radio button.
		/// </summary>
		/// <param name="summary">Current item being rendered.</param>
		/// <returns>Command name string.</returns>
		public string GetButtonCommandName()
		{
			return summary.JourneyIndex + "," + summary.DisplayNumber;
		}

		/// <summary>
		/// Returns the display number.
		/// </summary>
		/// <param name="summary">Current item being rendered.</param>
		/// <returns>Display number.</returns>
		public string GetFormattedDisplayNumber()
		{
			return DisplayNumber;
		}

		/// <summary>
		/// Returns a string of all the transport operator names used.
		/// </summary>
		/// <returns>All the transport operator names (comma seperated)</returns>
		public string GetOperatorNames()
		{
			string[] operators = summary.OperatorNames;
			string operatorName = null;
			string operatorNames = "";
			int i;

			if (operators != null && operators.Length > 0)
			{
				//Alphabetically sort the operator list.
				Array.Sort(operators);

				//Create the comma-separated mode list.
				for(i = 0; i < operators.Length - 1; i++)
				{
					operatorName = (string)operators[i];

					if (operatorName != null && operatorName.Length > 0)
					{
						operatorNames += operatorName + ", ";
					}
				}
				
				//add final mode

				if (operators[i] == null || operators[i].Length == 0)
				{
					if	(operatorNames.Length > 2)
					{
						// strip off unnecessary trailing comma and space ...
						operatorNames = operatorNames.Substring(0, operatorNames.Length - 2);
					}
				}
				else
				{
					operatorNames += operators[i];
				}
			}

			return operatorNames;
		}
	}
}
