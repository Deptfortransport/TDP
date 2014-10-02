// *********************************************** 
// NAME			: DisplayFormatAdapter.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 03/08/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/DisplayFormatAdapter.cs-arc  $
//
//   Rev 1.3   Sep 22 2011 14:57:38   mmodi
//Added datetime format to match travel news popup on map format
//Resolution for 5741: Real Time In Car - Severity not shown on travel news popup and datetime format
//
//   Rev 1.2   Mar 31 2008 12:58:54   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:14   mturner
//Initial revision.
//
//   Rev 1.4   Mar 28 2006 09:36:06   AViitanen
//Manual merge for Travel news (stream0024)
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.3   Mar 20 2006 14:47:00   NMoorhouse
//Updated following review comments
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 23 2006 19:16:06   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.1   Jan 30 2006 12:15:18   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1.1.0   Jan 10 2006 15:17:32   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Nov 01 2005 15:11:42   build
//Automatically merged from branch for stream2638
//
//   Rev 1.0.1.1   Oct 28 2005 17:11:06   tolomolaiye
//Updates following code review and running fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0.1.0   Oct 06 2005 14:42:06   jbroome
//Added BooleanToDisplayText and NonDefaultAdvancedOptionsDisplayText methods
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 14 2005 11:26:08   tolomolaiye
//Work in progress
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Aug 03 2005 12:36:30   jgeorge
//Initial revision.

using System;
using TransportDirect.Common.ResourceManager;
using System.Globalization;
using System.Collections;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// This adapter class provides formatting of specific types of object for display 
	/// to users in the presentation layer. All methods provide formatting of particular 
	/// types of object and return a string that is appropriate for display in the current 
	/// culture.
	/// This adapter class is not specific to any area of the presentation layer. New 
	/// methods e.g. for different date formats or object types should be added as required. 
	/// Reuse of existing methods is highly encouraged to ensure a consistent display of 
	/// data to the user.
	/// </summary>
	public sealed class DisplayFormatAdapter : TDWebAdapter
	{

		#region Date and time formatting constants

		/// <summary>
		/// Format string for StandardDateFormat
		/// </summary>
		private const string StandardDateFormatString = "ddd dd MMM yyyy";

		/// <summary>
		/// Format string for StandardTimeFormat
		/// </summary>
		private const string StandardTimeFormatString = "HH:mm";

		/// <summary>
		/// Format string for StandardDateAndTimeFormat
		/// </summary>
		private const string StandardDateAndTimeFormatString = "ddd dd MMM yyyy HH:mm";

		/// <summary>
		/// Format string for StandardTNDateAndTimeFormat
		/// </summary>
		private const string StandardTNDateAndTimeFormatString = "dd MMM yyyy HH:mm";

        /// <summary>
        /// Format string for StandardTNMapDateAndTimeFormat
        /// </summary>
        private const string StandardTNMapDateAndTimeFormatString = "dd/MM/yyyy HH:mm";

		#endregion

		#region Constructor

		/// <summary>
		/// public constructor to allow instantiation
		/// </summary>
		public DisplayFormatAdapter()
		{ }

		#endregion

		#region Date and time formatting methods

		/// <summary>
		/// Returns a string formatted using the standard Transport Direct date format.
		/// Calls the ToString method on the TDDateTime object with the appropriate format 
		/// as the argument.
		/// </summary>
		/// <param name="dateobject"></param>
		/// <returns>The date formatted as "ddd dd MMM yyyy"</returns>
		public static string StandardDateFormat(TDDateTime date)
		{
			return date.ToString(StandardDateFormatString);
		}
		
		/// <summary>
		/// Returns a string formatted using the standard Transport Direct time format.
		/// Calls the ToString method on the TDDateTime object with the appropriate format 
		/// as the argument.
		/// </summary>
		/// <param name="dateobject"></param>
		/// <returns>The date formatted as "HH:mm"</returns>
		public static string StandardTimeFormat(TDDateTime date)
		{
			return date.ToString(StandardTimeFormatString);
		}

		/// <summary>
		/// Returns a string formatted using the standard Transport Direct date/time format.
		/// Calls the ToString method on the TDDateTime object with the appropriate format 
		/// as the argument. The format should be stored as a constant. 
		/// </summary>
		/// <param name="dateobject"></param>
		/// <returns>The date formatted as "ddd dd MMM yyyy HH:mm"</returns>
		public static string StandardDateAndTimeFormat(TDDateTime date)
		{
			return date.ToString(StandardDateAndTimeFormatString);
		}

		/// <summary>
		/// Returns a string formatted using the standard Transport Direct date/time format.
		/// Calls the ToString method on the TDDateTime object with the appropriate format 
		/// as the argument. Non-breaking spaces added for alignment on travel news page.
		/// </summary>
		/// <param name="dateobject"></param>
		/// <returns>The date formatted as "dd MMM yyyy HH:mm"</returns>
		public static string StandardTNDateAndTimeFormat(TDDateTime date)
		{
			StringBuilder formattedDate = new StringBuilder();
			string convertedDate =  date.ToString(StandardTNDateAndTimeFormatString);

			string[] convertedDateArray = convertedDate.Split(' ');

			formattedDate.Append(convertedDateArray[0]);
			formattedDate.Append("&nbsp;");
			formattedDate.Append(convertedDateArray[1]);
			formattedDate.Append("&nbsp;");
			formattedDate.Append(convertedDateArray[2]);
			formattedDate.Append(" ");
			formattedDate.Append(convertedDateArray[3]);

			return formattedDate.ToString();
		}

        /// Returns a string formatted using the standard Transport Direct date/time format.
        /// Calls the ToString method on the TDDateTime object with the appropriate format 
        /// as the argument. Non-breaking spaces added for alignment on travel news page.
        /// </summary>
        /// <param name="dateobject"></param>
        /// <returns>The date formatted as "dd/MM/yyyy HH:mm"</returns>
        public static string StandardTNMapDateAndTimeFormat(TDDateTime date)
        {
            string convertedDate = date.ToString(StandardTNMapDateAndTimeFormatString);

            return convertedDate;
        }

		/// <summary> 
		/// Returns a string formatted using the standard Transport Direct time format. 
		/// Calls the ToString method on the TDDateTime object with the appropriate format 
		/// as the argument. 
		/// </summary> 
		/// <param name="dateobject"></param> 
		/// <returns>The date formatted as "HH:mm"</returns> 
		public static string StandardTimeFormatWithRoundingUp(TDDateTime date) 
		{ 
			// Round up if necessary for consistency. 
			if(date.Second >= 30) 
				date = date.AddMinutes(1); 

			return date.ToString(StandardTimeFormatString); 
		} 
		#endregion

		#region Boolean formatting method

		/// <summary>
		/// Converts a Boolean value to a string value of either yes or no. 
		/// </summary>
		/// <param name="booleanValue"></param>
		/// <returns>The boolean value </returns>
		public string BooleanToDisplayText(bool booleanValue)
		{
			string strBooleanValue = String.Empty;
			const string BooleanTrueKey = "Boolean.True";
			const string BooleanFalseKey = "Boolean.False";
			
			if (booleanValue)
			{
				strBooleanValue =  GetResource(BooleanTrueKey);
			}
			else
			{
				strBooleanValue = GetResource(BooleanFalseKey);
			}

			return strBooleanValue;
		}
		#endregion

		#region Integer formatting method
		/// <summary>
		/// Rounds the given double to the nearest int.
		/// If double is 0.5, then rounds up.
		/// Using this instead of Math.Round because Math.Round
		/// ALWAYS returns the even number when rounding a .5 -
		/// this is not behaviour we want.
		/// </summary>
		/// <param name="valueToRound">Value to round.</param>
		/// <returns>Nearest integer</returns>
		public static int RoundNumber(double valueToRound)
		{
			// Get the decimal point
			double valueFloored = Math.Floor(valueToRound);
			double remain = valueToRound - valueFloored;

			if(remain >= 0.5)
				return (int)Math.Ceiling(valueToRound);
			else
				return (int)Math.Floor(valueToRound);
		}
		#endregion

		/// <summary>
		/// Returns an array of strings. Each string is formatted with the appropriate key and 
		/// selected value in the language of the current culture (e.g. “Walking Speed: Slow”). 
		/// Only non-default options are returned.
		/// </summary>
		/// <param name="journey">TDJourneyParameters</param>
		/// <returns>An array of strings</returns>
		public string[] NonDefaultAdvancedOptionsDisplayText(TDJourneyParameters journey)
		{
			// Arraylist to hold strings to return
			ArrayList options = new ArrayList();
			// string constants
			const string space = "&nbsp;";
			const string comma = ",";

			// Get Data Services from Service Discovery
			DataServices.IDataServices populator = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			// Get all the transport modes from Data Services
			ArrayList allModes = populator.GetList(DataServiceType.PublicTransportsCheck);
			int defaultModesNo = allModes.Count;
			// Used to hold the modes selected
			ArrayList selectedModes = new ArrayList();

			// Get resource id for each transport mode selected
			for (int i=0; i<journey.PublicModes.Length; i++)
			{
				string resourceId = populator.GetResourceId(DataServiceType.PublicTransportsCheck, journey.PublicModes[i].ToString());
				// If mode has a resource id (some are joined together e.g. Bus/Coach)
				if (resourceId.Length != 0)
				{
					selectedModes.Add(string.Format("DataServices.PublicTransportsCheck.{0}", resourceId));
				}
			}				

			// Is default number of transport modes selected?
			if ((allModes.Count != selectedModes.Count) && (selectedModes.Count != 0))
			{
				// Build string to output
				StringBuilder sb = new StringBuilder(20);
				sb.Append(GetResource("DisplayFormatAdapter.TransportTypes"));
				sb.Append(space);
				for (int i=0; i<selectedModes.Count; i++)
				{
					// Add list of selected modes
					sb.Append(GetResource(selectedModes[i].ToString()));
					if (i != (selectedModes.Count-1))
						sb.Append(comma);					
				}
				// Add string to array
				options.Add(sb.ToString());
			}

			// Is default Number of Changes option selected?
			if(!journey.PublicAlgorithmType.Equals(PublicAlgorithmType.Default))
			{
				string resourceIdSuffix = populator.GetResourceId(DataServiceType.ChangesFindDrop, Enum.GetName(typeof(PublicAlgorithmType), journey.PublicAlgorithmType));
				string resourceId = string.Format("DataServices.ChangesFindDrop.{0}", resourceIdSuffix);
				options.Add(BuildDisplayText("DisplayFormatAdapter.NumberChanges", resourceId));
			}

			// Is default Speed of Changes option selected?
			if (!IsDefaultValue(journey.InterchangeSpeed, DataServiceType.ChangesSpeedDrop))
			{
				string resourceIdSuffix = populator.GetResourceId(DataServiceType.ChangesSpeedDrop, journey.InterchangeSpeed.ToString());
				string resourceId = string.Format("DataServices.ChangesSpeedDrop.{0}", resourceIdSuffix);
				options.Add(BuildDisplayText("DisplayFormatAdapter.SpeedChanges", resourceId));
			}

			// Is default Walking Speed option selected?
			if (!IsDefaultValue(journey.WalkingSpeed, DataServiceType.WalkingSpeedDrop))
			{
				string resourceIdSuffix = populator.GetResourceId(DataServiceType.WalkingSpeedDrop, journey.WalkingSpeed.ToString());
				string resourceId = string.Format("DataServices.WalkingSpeedDrop.{0}", resourceIdSuffix);
				options.Add(BuildDisplayText("DisplayFormatAdapter.WalkingSpeed", resourceId));
			}

			// Is Maximum Walking Time option selected?
			if (!IsDefaultValue(journey.MaxWalkingTime, DataServiceType.WalkingMaxTimeDrop))
			{
				string resourceIdSuffix = populator.GetResourceId(DataServiceType.WalkingMaxTimeDrop, journey.MaxWalkingTime.ToString());
				string resourceId = string.Format("DataServices.WalkingMaxTimeDrop.{0}", resourceIdSuffix);
				StringBuilder sb = new StringBuilder();
				sb.Append(BuildDisplayText("DisplayFormatAdapter.WalkingTime", resourceId));
                sb.Append(space);
				sb.Append(GetResource("DisplayFormatAdapter.mins"));
				options.Add(sb.ToString());
			}

			return (string[])options.ToArray(typeof(string));
		}

		/// <summary>
		/// Method returns a concatenated string of 
		/// key / value for non default options
		/// </summary>
		/// <param name="key">string key</param>
		/// <param name="val">string value</param>
		/// <returns>concatenated string</returns>
		private string BuildDisplayText(string key, string val)
		{
			const string space = "&nbsp;";
			StringBuilder sb = new StringBuilder(100);

			sb.Append(GetResource(key));
			sb.Append(space);
			sb.Append(GetResource(val));
			
			return sb.ToString();
		}

		/// <summary>
		/// Method returns boolean value according to
		/// whether the selected value is the default
		/// value for the specified Data Service type
		/// </summary>
		/// <param name="selectedValue">selected value</param>
		/// <param name="type">data service type</param>
		/// <returns>true/false is default value</returns>
		private bool IsDefaultValue(int selectedValue, DataServiceType type)
		{
			// Get Data Services from Service Discovery
			DataServices.IDataServices populator = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			// Get default value for list type
			int defaultValue = Convert.ToInt32(populator.GetDefaultListControlValue(type));
            // Is default value selected?
			return (selectedValue == defaultValue);
		}

	}
}
