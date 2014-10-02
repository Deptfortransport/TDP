// *********************************************** 
// NAME			: InputAdapter.cs
// AUTHOR		: Tolu Olomolaiye
// DATE CREATED	: 17/08/2005
// DESCRIPTION	: Provides helper/adapter methods for the input pages
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/InputAdapter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:59:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:22   mturner
//Initial revision.
//
//   Rev 1.10   Apr 28 2006 12:21:14   asinclair
//Added more Error Messages to allow them to be displayed in Error Control
//Resolution for 3962: DN068: Extend: Ambiguiy / Error messages on Extend Input page
//
//   Rev 1.9   Feb 23 2006 17:03:20   RWilby
//Merged stream3129
//
//   Rev 1.8   Jan 04 2006 10:06:22   tolomolaiye
//Updates folllowing Visit Planner code review
//
//   Rev 1.7   Nov 25 2005 14:28:38   tolomolaiye
//Allow user preferences to be saved. Fix for IR 3176
//Resolution for 3176: Visit Planner  - When a logged-in user saves their Advanced preferences the options chosen are not saved
//
//   Rev 1.6   Nov 17 2005 09:08:04   asinclair
//Removed the warnings.Initialise() from the PopulateErrorDisplayControl method
//Resolution for 2961: Visit Planner - Dates and times in the past can be entered in the Input Page
//
//   Rev 1.5   Nov 08 2005 15:57:04   tolomolaiye
//Ensure validation object is cleared after error messages have been displayed
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Nov 07 2005 09:58:22   tolomolaiye
//Modifications for Visit Planner
//
//   Rev 1.3   Oct 29 2005 20:03:14   asinclair
//Updated PopulateErrorDisplayControl method
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 11 2005 10:11:52   asinclair
//Corrected Implemention to design
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Oct 05 2005 16:41:38   tolomolaiye
//Work in progress
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 30 2005 14:21:58   tolomolaiye
//Initial revision.
//

using System;
using System.Collections;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.Web;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// This class provides helper/adapter methods for use by input pages 
	/// These methods are not visit planner specific.
	/// </summary>
	public sealed class InputAdapter : TDWebAdapter
	{

		#region Constructor

		/// <summary>
		/// Private constructor to prevent instantiation
		/// </summary>
		public InputAdapter()
		{}

		#endregion

		#region Public Methods

		/// <summary>
		/// Populates the PTPreferencesControl object the current TDJourneyParameters and TDResourceManager objects.
		/// </summary>
		/// <param name="control">The PtPreferencesControl to populate</param>
		/// <returns>true or false</returns>
		public void PopulatePTPreferencesControl (PtPreferencesControl control, TDJourneyParameters journeyParameters)
		{
			control.JourneyChangesOptionsControl.Changes = journeyParameters.PublicAlgorithmType;
			control.JourneyChangesOptionsControl.ChangesSpeed = journeyParameters.InterchangeSpeed;
			control.WalkingSpeedOptionsControl.WalkingDuration = journeyParameters.MaxWalkingTime;
			control.WalkingSpeedOptionsControl.WalkingSpeed = journeyParameters.WalkingSpeed;
		}

		/// <summary>
		/// Initialises the PTPreferencesControl control to its default state,
		/// </summary>
		/// <param name="control">The PTPreferencesControl object to intialise</param>
		/// <param name="journeyParameters">The journey details</param>
		public void InitialisePTPreferencesControl(PtPreferencesControl control)
		{
			control.JourneyChangesOptionsControl.ChangesDisplayMode = GenericDisplayMode.Normal;
			control.JourneyChangesOptionsControl.ChangesSpeedDisplayMode = GenericDisplayMode.Normal;
			control.WalkingSpeedOptionsControl.WalkingDurationDisplayMode = GenericDisplayMode.Normal;
			control.WalkingSpeedOptionsControl.WalkingSpeedDisplayMode = GenericDisplayMode.Normal;
		}

		/// <summary>
		/// Populates the ErrorDisplayControl control with warning information for the input pages
		/// When the control has been populated the object should be cleared, to prevent the same 
		/// messages being redisplayed to the user again.
		/// </summary>
		/// <param name="control">The ErrorDisplayControl object to populate</param>
		/// <param name="warnings">Warnings to display</param>
		public bool PopulateErrorDisplayControl(ErrorDisplayControl control, ValidationError warnings)
		{
			if (warnings.MessageIDs.Count > 0)
			{
				ArrayList warningList = new ArrayList();

				//Add the error message for Overlaping Locations
				if (warnings.Contains(ValidationErrorID.VisitLocationsOverlap))
				{
					warnings.MsgResourceID = warnings.MessageIDs[ValidationErrorID.VisitLocationsOverlap].ToString();

					string text = Global.tdResourceManager.GetString(
						warnings.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ";

					warningList.Add( text );
				}
                
				//More Validation Error messages need to be added here

				//validation messages when journey times overlap - this is used on the results page
				if (warnings.Contains(ValidationErrorID.JourneyTimesOverlap))
				{
					warnings.MsgResourceID = warnings.MessageIDs[ValidationErrorID.JourneyTimesOverlap].ToString();
					warningList.Add(Global.tdResourceManager.GetString(warnings.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");
				}

				if(warnings.Contains(ValidationErrorID.NoModesSelected))
				{
					warnings.MsgResourceID = "ValidateAndRun.ChooseAnOption";
					warningList.Add(Global.tdResourceManager.GetString(warnings.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");
				} 

				if(warnings.Contains(ValidationErrorID.OriginAndDestinationOverlap))
				{
					if(TDItineraryManager.Current.ExtendEndOfItinerary)
					{
						warnings.MsgResourceID = "ValidateAndRun.DoorToDoorDestinationAndViaOverlap";
					}
					else if(TDItineraryManager.Current.ExtendInProgress)
					{
						warnings.MsgResourceID = "ValidateAndRun.DoorToDoorOriginAndViaOverlap";
					}
					else
					{
						warnings.MsgResourceID = "ValidateAndRun.DoorToDoorOriginAndDestinationOverlap";
					}
					warningList.Add(Global.tdResourceManager.GetString(warnings.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

				}



				control.Type = ErrorsDisplayType.Warning;
				control.ErrorStrings = (string[])warningList.ToArray(typeof(string));

				//finally, exit with true
				return true;
				
			}
			else
			{
				//if there are no errors simply exit with false
				return false;
			}	
		}

		#endregion
	}
}
