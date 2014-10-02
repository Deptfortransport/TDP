// *********************************************** 
// NAME                 : AmendSaveSendControl.ascx.cs 
// AUTHOR               : Kenny Cheung (Modified by Esther Severn & Halim Ahad)
// DATE CREATED         : 20/08/2003 
// DESCRIPTION			: A custom user control to
// display the Amend/Save/Send tab control.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendSaveSendControl.ascx.cs-arc  $
//
//   Rev 1.10   Oct 26 2010 12:18:44   apatel
//updated generateCyclePlannerIntro method to include cycle planner landing url.
//Resolution for 5624: Email for the cycle journey to a friend does not include landing URL
//
//   Rev 1.9   Mar 30 2010 09:13:46   apatel
//Cycle planner white label changes
//Resolution for 5488: Cycle Planner white label changes
//
//   Rev 1.8   Oct 21 2009 08:44:22   apatel
//Set link in email to have partner Id as 'Email'
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.7   Sep 30 2009 16:25:46   apatel
//Social book marking added to Find Car Park Results
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.6   Sep 30 2009 09:06:46   apatel
//CCN 530 Social Bookmarking code changes
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.5   May 01 2009 13:15:54   mmodi
//Updated to adjust email column widths for the return journey
//Resolution for 5286: Send to friend email width can be too large
//
//   Rev 1.4   Apr 30 2009 17:32:28   mmodi
//Updated to adjust the Email column widths for Public Transport journey directions
//Resolution for 5286: Send to friend email width can be too large
//
//   Rev 1.3   Oct 13 2008 16:41:34   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.2   Oct 10 2008 15:30:34   mmodi
//Updated detail formatter call
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.1   Sep 02 2008 10:30:18   mmodi
//Email and Save cycle journey changes
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Jun 20 2008 14:39:50   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:19:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:06   mturner
//Initial revision.
//
//   Rev 1.103   Dec 07 2006 13:36:46   mturner
//Manual merge of stream4240
//
//   Rev 1.101.1.2   Dec 06 2006 11:47:38   mmodi
//Removed Help button for AmendCarDetails mode
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.101.1.1   Nov 24 2006 09:08:20   dsawe
//added amendcardetails control
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.101.1.0   Nov 10 2006 15:46:24   mmodi
//Amended to display appropriate tabs for Journey Emissions page
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.101   Jun 14 2006 11:26:32   jfrank
//Fix for vantive: 3918704 - Enable buttons when the outward journey fails but a return journey is returned.
//Changed code so that send to a friend is enabled for return only journeys and to ensure layout is correct.
//Resolution for 3686: Buttons disabled when return journey returned and outward not
//
//   Rev 1.100   May 16 2006 16:30:50   CRees
//Hid return date control for pre-Del 8.1 park and ride issues.
//
//   Rev 1.99   Apr 06 2006 10:36:58   mdambrine
//Not showing "save to favorites" button when the mode is "find A bus"
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.98   Mar 17 2006 15:21:16   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.97   Mar 14 2006 09:48:34   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.93.2.4   Mar 09 2006 16:31:36   RGriffith
//Extra check that itinerary manger is not in ExtendInProgress mode
//
//   Rev 1.93.2.3   Mar 09 2006 13:16:02   RGriffith
//Changes to make AmendFares visible if not using an ItineraryManager
//
//   Rev 1.93.2.2   Mar 06 2006 17:33:26   RGriffith
//Changes made for tickets/costs
//
//   Rev 1.93.2.1   Mar 01 2006 13:27:40   RGriffith
//Changes for use with Tickets and Costs page
//
//   Rev 1.93.2.0   Dec 20 2005 19:52:36   rhopkins
//Changed to use new methods of TDItineraryManager
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.96   Mar 07 2006 17:50:10   CRees
//IR2126 / Vantive: 3597604 - Added service details to PT results in send-to-a-friend.
//
//   Rev 1.95   Feb 23 2006 16:43:44   halkatib
//merged changes for stream 3129 enhanced exposed services
//
//   Rev 1.94   Jan 05 2006 11:15:28   esevern
//Added setting of help button alt text specific to fares, save and send tabs
//Resolution for 3418: Del 8: WAI changes
//
//   Rev 1.93   Nov 16 2005 10:02:46   tolomolaiye
//Modified email text for Visit Planner
//Resolution for 3065: Visit Planner - Send to A Friend email contains errors in email body
//
//   Rev 1.92   Nov 04 2005 14:06:26   ECHAN
//merge stream2816 for DEL8
//
//   Rev 1.91   Nov 01 2005 15:12:10   build
//Automatically merged from branch for stream2638
//
//   Rev 1.90.1.2   Oct 28 2005 17:11:14   tolomolaiye
//Updates following code review and running fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.90.1.1   Oct 19 2005 18:31:36   jbroome
//Added email output for Visit Planner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.90.1.0   Oct 13 2005 19:12:52   jbroome
//Added case for Visit Planner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.90   May 12 2005 17:35:28   asinclair
//Passing in an extra value into the constructor
//
//   Rev 1.89   May 09 2005 11:01:42   COwczarek
//Disable send to a friend tool tab if no outward results exist
//Resolution for 2449: Intermittant crash on car journeys
//
//   Rev 1.88   May 06 2005 10:37:00   jgeorge
//Check for full itinerary before examining journey results
//Resolution for 2444: Ticket Purchase .NET Error Clicking On Trainline.com Buy Button
//
//   Rev 1.87   May 04 2005 13:49:44   jgeorge
//Provided way for page to force the control to reevaluate the state of the results and show tabs accordingly.
//Resolution for 2394: DEL 7 Car Costing - Amend Fares
//
//   Rev 1.86   May 03 2005 16:22:00   tmollart
//Road units now derived from InputPageState and passed into method use for creating emails so correct units are used.
//Resolution for 2384: Del 7 - Car Costing - Send to a friend is always in miles
//
//   Rev 1.85   Apr 22 2005 14:05:06   rscott
//Fix For IR1985 send to friend problem
//
//   Rev 1.84   Apr 20 2005 14:48:24   rgreenwood
//IR 2190: Added references for global resource manager when displaying custom help controls, so that welsh text can be accessed.
//Resolution for 2190: PT - FindFareDateSelection: No help text for Amend Date tab
//
//   Rev 1.83   Apr 15 2005 12:47:46   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.82   Mar 30 2005 13:49:36   rhopkins
//Make AmendPanelMode enumeration public so that it can be used by parent page.
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.81   Mar 29 2005 10:24:52   COwczarek
//Work in progress
//
//   Rev 1.80   Mar 18 2005 14:55:20   rgeraghty
//Added amend view control
//
//   Rev 1.79   Mar 08 2005 14:15:56   COwczarek
//Work in progress
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.78   Mar 07 2005 13:56:04   rhopkins
//Added AmendCostSearchDateControl
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.77   Mar 01 2005 16:26:28   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.76   Feb 15 2005 20:36:00   rhopkins
//Work in progress
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.75   Jan 18 2005 17:21:46   rgeraghty
//Added AmendFaresControl
//
//   Rev 1.74   Oct 04 2004 15:34:28   jbroome
//IR 1675 - Ensure consistency of email text with changes to JourneysSearchedForControl.
//
//   Rev 1.73   Sep 30 2004 12:00:00   rhopkins
//IR1648 Output correct label for Return details in Send to a friend
//
//   Rev 1.72   Sep 23 2004 14:43:46   RHopkins
//IR1498 Corrected formatting of emails for Extend Journey Send to a friend
//
//   Rev 1.71   Sep 17 2004 15:13:42   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.70   Aug 11 2004 14:51:28   RHopkins
//IR1125 Rewrite logic for determining which tabs to display and which panel/content to display.
//
//   Rev 1.69   Aug 09 2004 16:14:42   jbroome
//IR 1258 - Ensure consistency of rounded time values.
//
//   Rev 1.68   Aug 06 2004 09:21:20   RHopkins
//Make "Send to a friend" code work with Itinerary.
//
//   Rev 1.67   Jul 22 2004 14:12:10   RHopkins
//IR1113 Change to technique for exposing subcontrol states
//
//   Rev 1.66   Jul 22 2004 11:13:30   jgeorge
//Updates for Find a (Del 6.1)
//
//   Rev 1.65   Jul 20 2004 11:43:06   RHopkins
//IR1113 Enable control to expose the states of subcontrols so that "Amend date/time" anchor link on page can be varied according to these states
//
//   Rev 1.64   Jun 25 2004 10:56:26   asinclair
//Fix for IR 964
//
//   Rev 1.63   Jun 17 2004 10:15:40   RHopkins
//Corrected behaviour when switching between display of Itinerary and Normal results
//
//   Rev 1.62   Jun 08 2004 12:53:04   JHaydock
//Update to AmendSaveSendControl for Find A Flight plus formatting changes for Welsh display
//
//   Rev 1.61   May 27 2004 16:03:02   ESevern
//added amendstopover (for extended journey) tab
//
//   Rev 1.60   Apr 28 2004 16:19:56   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.59   Feb 13 2004 12:13:14   esevern
//DEL5.2 seperation of login and register - removed login/register functions 
//
//   Rev 1.58   Feb 04 2004 16:54:34   COwczarek
//Rework code for generating email content to support internationalisation and improved formatting
//Resolution for 613: Refactoring of code that displays car journey details
//
//   Rev 1.57   Feb 02 2004 13:44:10   COwczarek
//Fix internationlisation bug - resource strings should not be initialised in instance variable declaration
//Resolution for 613: Refactoring of code that displays car journey details
//
//   Rev 1.56   Jan 27 2004 17:48:10   COwczarek
//Work in progress
//Resolution for 613: Refactoring of code that displays car journey details
//
//   Rev 1.55   Jan 23 2004 16:17:14   COwczarek
//Work in progress
//Resolution for 613: Refactoring of code that displays car journey details
//
//   Rev 1.54   Jan 23 2004 11:13:34   PNorell
//Updates for 5.2
//
//   Rev 1.53   Jan 20 2004 09:50:22   asinclair
//Comented out the handlers for the forgotten passsword button, this is now covered by a click event on a different page.
//
//   Rev 1.52   Jan 13 2004 19:55:56   RPhilpott
//Handle emailing of car journey details from JoourneyMap page.
//Resolution for 549: Send to a Friend email eight various problems
//
//   Rev 1.51   Jan 13 2004 12:40:12   RPhilpott
//Corrections after unit testing, etc.
//Resolution for 549: Send to a Friend email eight various problems
//
//   Rev 1.50   Jan 09 2004 17:21:12   esevern
//resolution for IR549
//Resolution for 549: Send to a Friend email eight various problems
//
//   Rev 1.49   Jan 07 2004 11:40:50   JHaydock
//Changed email subject to "<user email> sent you a message" and email from address to that of the currently logged in user
//
//   Rev 1.48   Dec 02 2003 10:32:50   PNorell
//Updated the control to use TDUser.
//Added all logging as well as it only created events and never wrote them anywhere.
//
//   Rev 1.47   Nov 27 2003 17:09:18   passuied
//removed calls to display/hide Extra message (You must me logged in to...)
//Resolution for 429: QA: Add (you must be logged in...) to the save as favourite
//
//   Rev 1.46   Nov 25 2003 18:17:00   passuied
//fix so, on login control extra message is displayed if want to send a message and not logged in
//Resolution for 387: QA: You must be logged in message
//
//   Rev 1.45   Nov 25 2003 11:02:26   passuied
//Fix for #347.
//Checks a flag in JourneyViewState to display the confirmation message when a journey has already  been saved.
//Resolution for 347: Strange behaviour on login control
//
//   Rev 1.44   Nov 25 2003 09:44:14   passuied
//completed character limitation for user registration controls (#138)
//Fixed some coding mistakes
//Resolution for 138: User Registration - Character Limitation
//
//   Rev 1.43   Nov 24 2003 11:26:18   kcheung
//Updated so welsh buttons appear properly.
//Resolution for 300: Tab buttons on AmendSaveSendControl do not change language
//
//   Rev 1.42   Nov 19 2003 17:34:48   hahad
//Changed format of Journey details sent in the send to friend Email
//
//   Rev 1.41   Nov 19 2003 16:26:34   hahad
//Bug fix for Update Panel
//
//   Rev 1.40   Nov 18 2003 15:25:46   passuied
//made it work for when max is reached
//
//   Rev 1.39   Nov 15 2003 15:37:42   PNorell
//Switched the help button to not scroll down.
//
//   Rev 1.38   Nov 13 2003 13:28:08   hahad
//uses custom password Validator from AmendSaveSendLoginControl
//
//   Rev 1.37   Nov 06 2003 10:38:20   hahad
//Code Review fixes
//
//   Rev 1.36   Oct 30 2003 20:32:42   esevern
//added call to load drop list on updatepanel
//
//   Rev 1.35   Oct 30 2003 14:40:46   esevern
//added check for valid email address at login
//
//   Rev 1.34   Oct 29 2003 17:46:16   esevern
//corrected default favourite journey name
//
//   Rev 1.33   Oct 29 2003 16:41:32   esevern
//amended pageload - check for logged in, amended amendSaveSendSendControl ok button event args and handler
//
//   Rev 1.32   Oct 29 2003 12:20:58   kcheung
//Fixed so that Message appears after you Save
//
//   Rev 1.31   Oct 29 2003 11:27:38   hahad
//A few bug fixes
//
//   Rev 1.30   Oct 27 2003 13:38:50   esevern
//bugfix
//
//   Rev 1.29   Oct 24 2003 14:37:00   esevern
//added forgotten password functionality
//
//   Rev 1.28   Oct 22 2003 17:09:24   hahad
//Made changes to AmendSaveSendLoginControl
//
//   Rev 1.27   Oct 22 2003 10:23:14   esevern
//added save favourite journey
//
//   Rev 1.26   Oct 20 2003 17:06:28   kcheung
//Updated variable names to comply with FXCOP
//
//   Rev 1.25   Oct 20 2003 13:36:12   esevern
//login/register now return bool - successful completion
//
//   Rev 1.24   Oct 14 2003 14:05:08   kcheung
//Fixed help labels
//
//   Rev 1.23   Oct 14 2003 10:09:14   hahad
//Checked in to allow access for Kenny
//
//   Rev 1.22   Oct 13 2003 12:59:12   kcheung
//Fixed ALT tags
//
//   Rev 1.21   Oct 13 2003 11:20:52   hahad
//Amendments to Okbutton click
//
//   Rev 1.20   Oct 12 2003 13:53:50   hahad
//Added Registration Login Code
//
//   Rev 1.19   Oct 10 2003 10:46:32   hahad
//Uncommented out TODO code
//
//   Rev 1.18   Oct 09 2003 12:43:26   kcheung
//Fixed html to make it compliant with w3c
//
//   Rev 1.17   Oct 09 2003 10:37:06   hahad
//Changed Event Handlers for OKButton and ForgottenPasswordButton
//
//   Rev 1.16   Sep 25 2003 16:39:10   kcheung
//Integrated stylesheet stuff
//
//   Rev 1.15   Sep 23 2003 17:19:16   PNorell
//Made sure the initial state of the help label is invisible.
//
//   Rev 1.14   Sep 23 2003 14:49:48   PNorell
//Updated page states and the wait page to function according to spec.
//Updated the different controls to ensure they have correct PageId and that they call the ValidateAndRun properly.
//Removed some 'warning' messages - a clean project is nice to see.
//
//   Rev 1.13   Sep 21 2003 14:50:08   kcheung
//Added call to JourneyPlannerRunner.. added JourneyViewState code to keep track of the pane that the user is in.
//
//   Rev 1.12   Sep 19 2003 20:09:14   kcheung
//Updated
//
//   Rev 1.11   Sep 19 2003 11:01:48   PNorell
//Added help component.
//
//   Rev 1.10   Sep 08 2003 16:52:56   kcheung
//Updated error messages
//
//   Rev 1.9   Sep 08 2003 15:07:30   kcheung
//Updated to remove initialise method as longer required.
//
//   Rev 1.8   Aug 20 2003 17:58:38   kcheung
//Removed hard-coded messages - now all read from Resource Manager
//
//   Rev 1.7   Aug 20 2003 17:12:26   kcheung
//Added comments, headers.  Also updated some of the page load logic to check for login.

// Logic currently doesn't work completely because it requires the
// Authenticated property of the session to be set correctly - this would
// then make it work completely.

using System;
using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Mail;
using System.Text;
using System.Globalization;

using TransportDirect.Web.Support;
using TransportDirect.UserPortal.JourneyPlanRunner;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common;
using TransportDirect.UserPortal.Resource;

//Added for Registration Control

using System.Web.UI;
using System.Collections;

using TransportDirect.UserPortal.Web.Controls;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.Web.Events;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.CyclePlannerControl;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Enumeration to control visibility of Amend panels
	/// </summary>
	public enum AmendPanelMode : int
	{
		None,
		AmendFareDetail,
		AmendCostSearchDate,
		AmendView,
		AmendCostSearchDay,
		AmendDateTimeNormal,
		AmendStopoverNormal,
		AmendCarDetails,
		SaveFavouriteNormal,
		SaveFavouriteLogin,
		SaveFavouriteListIsFull,
		SaveFavouriteConfirmation,
		SendEmailNormal,
		SendEmailLogin,
		SendEmailConfirmation
	}

	/// <summary>
	///	A custom user control to
	/// display the Amend/Save/Send tab control
	/// </summary>
	public partial  class AmendSaveSendControl : TDUserControl
	{
		private enum EmailJourneyDetailType : int
		{
			Boilerplate,
			Summary,
			Public,
			Road
		}

		/// <summary>
		/// Structure used to hold each part of the email message.
		/// </summary>
		private class EmailJourneyDetailTable
		{
			private EmailJourneyDetailType detailType;
			private IList details;
			private int lineBreak;

			public EmailJourneyDetailTable(string plainText)
			{
				ArrayList textList = new ArrayList();
				string[] row = new String[1];
				row[0] = plainText;
				textList.Add(row);
				this.detailType = EmailJourneyDetailType.Boilerplate;
				this.details = textList;
				this.lineBreak = 0;
			}

			public EmailJourneyDetailTable(EmailJourneyDetailType detailType, IList details, int lineBreak)
			{
				this.detailType = detailType;
				this.details = details;
				this.lineBreak = lineBreak;
			}

			public EmailJourneyDetailType DetailType
			{
				get { return detailType; }
				set { detailType = value; }
			}

			public IList Details
			{
				get { return details; }
				set { details = value; }
			}

			public int LineBreak
			{
				get { return lineBreak; }
				set { lineBreak = value; }
			}
		}

		// indicates which tab is currently being displayed.
		private AmendPanelMode currentControl;

		protected string userId = string.Empty;

		#region URLS of tab button images

		private string stopoverTabSelectedImageUrl;
		private string stopoverTabUnselectedImageUrl;

		private string amendTabSelectedImageUrl;
		private string amendTabUnselectedImageUrl;

		private string saveTabSelectedImageUrl;
		private string saveTabUnselectedImageUrl;

		private string sendTabSelectedImageUrl;
		private string sendTabUnselectedImageUrl;

		private string fareTabSelectedImageUrl;
		private string fareTabUnselectedImageUrl;

		private string amendDayTabSelectedImageUrl;
        private string amendDayTabUnselectedImageUrl;

        private string costSearchDateTabSelectedImageUrl;
        private string costSearchDateTabUnselectedImageUrl;

		private string amendCarDetailsTabSelectedImageUrl;
		private string amendCarDetailsTabUnSelectedImageUrl;

		private string amendViewTabSelectedImageUrl;
		private string amendViewTabUnselectedImageUrl;

		#endregion URLS of tab button images

		#region Tab buttons



		#endregion Tab buttons

		#region Help controls
		#endregion Help controls

		protected System.Web.UI.WebControls.Panel Panel1;

		#region Amend/Save/Send/Login/Password/Fare Controls

		protected AmendSaveSendAmendControl theAmendSaveSendAmendControl;
		protected AmendStopoverControl theAmendStopoverControl;
		protected AmendSaveSendSaveFullControl theAmendSaveSendSaveFullControl;
		protected AmendSaveSendSendControl theAmendSaveSendSendControl;
		protected AmendSaveSendLoginControl theAmendSaveSendLoginControl;
		protected AmendSaveSendMessageControl theAmendSaveSendMessageControl;
		protected AmendSaveSendSaveControl theAmendSaveSendSaveControl;
		protected AmendFaresControl theAmendFaresControl;
		protected AmendCostSearchDayControl theAmendCostSearchDayControl;
		protected AmendCostSearchDateControl theAmendCostSearchDateControl;
		protected AmendViewControl theAmendViewControl;
		protected AmendCarDetailsControl theAmendCarDetailsControl;

		protected CarAllDetailsControl carControl;
		protected JourneyMapControl journeyMapControl;
		protected CarJourneyDetailsTableControl carJourneyDetailsControl;

		#endregion Amend/Save/Send/Login/Password Controls
		
		#region messages - read from resource manager

		private string messageLoginIncorrect;
		private string messageInvalidEmailAddress;
		private string messsageJourneyDetailsSent;
		private string messageProblemSendingEmail;
		private string messsageJourneyDetailsSaved;
		private string emailIntro;
        private string emailPlanLinkText;
		private string emailJourneyFrom;
		private string emailJourneyTo;
		private string emailDepart;
		private string emailArrive;
		private string emailTransport;
		private string emailDuration;
		private string emailChanges;
		private string emailLeave;
		private string emailFrom;
		private string emailTo;
		private string emailSummaryOutward;
		private string emailSummaryOutwardNoDate;
		private string emailDetailsOutward;
		private string emailItineraryDetailsOutward;
		private string emailSummaryReturn;
		private string emailSummaryReturnNoDate;
		private string emailDetailsReturn;
		private string emailItineraryDetailsReturn;
		private string emailLabelArrivingBefore;
		private string emailLabelLeavingAfter;
		private string emailOutwardJourneyFrom;
		private string emailReturnJourneyFrom;	
		private string emailLengthOfStay;
		private string emailHours;
		private string emailDetailsJourney;
		private string emailSummaryJourneys;

		#endregion messages - read from resource manager

		private bool itineraryExists;
		private bool extendInProgress ;
		private bool showItinerary ;
		private bool returnExists ;
        private bool outwardExists ;
        private bool outwardArriveBefore ;
		private bool returnArriveBefore ;
		private bool showFindA ;
		private bool showSaveFavorites ;
		private bool isCostBased ;
		private bool emailShowLocations;
		private bool preRenderDone ;  // Flags that prerender code has already been called manually and should not be done again
		private bool showFares ;
		private bool hideHelpButton ;
		
		private static readonly string separatorLine = "\n".PadLeft(80,'_');
		private const string space = " ";
		private const string comma = ",";

		private ArrayList emailJourneyDetails;

		/// <summary>
		/// Page Load checks to see if this control is being loaded
		/// for the first time by checking to see if it is a postback
		/// or not.  If it is not a postback (i.e. first time it
		/// is being loaded, it will set the current tab to the first
		/// tab and consequently the first tabbed panel will be displayed.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			DetermineStateOfResults();

			initialiseResourceStrings();

			currentControl = (AmendPanelMode)TDSessionManager.Current.JourneyViewState.SelectedTabIndex;
            
            //removing the ISPostBack condition to facilitate change of language
            //if (!IsPostBack)
            //{
				SetupControls();
            //}

            // Set AlternateText
            SetAlternateText();
		}

		/// <summary>
		/// OnPreRender - updates the state of the controls.
		/// </summary>
		protected override void OnPreRender(System.EventArgs e)
		{
			// DO NOT put state changing logic in here - put it in the manualPreRender()

			if ( !preRenderDone )
			{
				manualPreRender();
			}

			preRenderDone = false;

			// Call base
			base.OnPreRender(e);
		}

		/// <summary>
		/// Allows the OnPreRender() of the parent page/control to invoke the prerender logic
		/// of this control earlier than normal, so that the resulting state of the control
		/// can be interrogated in the parent's OnPreRender() method.
		/// </summary>
		public void manualPreRender()
		{
			manualPreRender(false);
		}

		/// <summary>
		/// Allows the OnPreRender() of the parent page/control to invoke the prerender logic
		/// of this control earlier than normal, so that the resulting state of the control
		/// can be interrogated in the parent's OnPreRender() method.
		/// </summary>
		/// <param name="reevaluateStatus">If true, then the control will reevaluate the state
		/// of the results to determine which tabs to show</param>
		public void manualPreRender(bool reEvaluateStatus)
		{
			// If the screen is switching in either direction between the display of normal results and
			// the display of the Itinerary then the controls must be reset
			if (TDItineraryManager.Current.ItineraryManagerModeChanged || reEvaluateStatus)
			{
				DetermineStateOfResults();
				SetupControls();
			}

			initialiseHelpControls();

			preRenderDone = true;
		}


		/// <summary>
		/// Establish what mode the Itinerary Manager is in and whether we have any Return results
		/// </summary>
		private void DetermineStateOfResults()
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			ITDSessionManager sessionManager = TDSessionManager.Current;

			itineraryExists = (itineraryManager.Length > 0);
			extendInProgress = itineraryManager.ExtendInProgress;
			showItinerary = (itineraryExists && !extendInProgress);
			showFindA = (!showItinerary && (sessionManager.IsFindAMode));			
			isCostBased = FindInputAdapter.IsCostBasedSearchMode(sessionManager.FindAMode);
            
            // Save favourite option is only available for Door to door and Find a cycle
			showSaveFavorites = ((!showFindA && sessionManager.TimeBasedFindAMode != FindAMode.Bus)
                                 ||
                                 (showFindA && sessionManager.TimeBasedFindAMode == FindAMode.Cycle));
			
			//determine if calling page is a fares page
			switch (((TDPage)Page).PageId)
			{
				case (PageId.JourneyFares):
				case (PageId.RefineTickets):
					ItineraryManagerMode itineraryManagerMode = TDSessionManager.Current.ItineraryMode;

					// Determine if Fares tab should be shown if current journeys are public
					// - Requires inspecting the itinerary manager if in replan or extend mode and inspecting viewstate if not
					if ((itineraryManagerMode == ItineraryManagerMode.Replan)
						|| ((itineraryManagerMode == ItineraryManagerMode.ExtendJourney) && (!itineraryManager.ExtendInProgress)))
					{
						showFares = itineraryManager.OutwardIsPublic || itineraryManager.ReturnIsPublic;
					} 
					else
					{
						TDJourneyViewState viewState = itineraryManager.JourneyViewState;
						bool outwardIsPublic = (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended) || (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal);
						bool returnIsPublic = false;
						if (itineraryManager.JourneyResult.ReturnPublicJourneyCount > 0)
							returnIsPublic = (viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended) || (viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal);

						showFares = outwardIsPublic || returnIsPublic;
					}

					if ( showFares && (!IsPostBack))
						// Ensure that the amend fare detail tab is marked for selection
						sessionManager.JourneyViewState.SelectedTabIndex = (int)AmendPanelMode.AmendFareDetail;
					break;
			}
			
			if ( showItinerary )
			{
				returnExists = (itineraryManager.ReturnLength > 0);
                outwardExists = (itineraryManager.OutwardLength > 0);
			}
			else
			{
                //check for cycle result
                PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(sessionManager);
                outwardExists = plannerOutputAdapter.CycleExists(true);
                returnExists = plannerOutputAdapter.CycleExists(false);

				//check for normal result
				ITDJourneyResult result = sessionManager.JourneyResult;
				if(result != null) 
				{
					returnExists = (((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid) || returnExists;
                    outwardExists = (((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid) || outwardExists;
					// Get time types for journey.
					outwardArriveBefore = sessionManager.JourneyViewState.JourneyLeavingTimeSearchType;
					returnArriveBefore = sessionManager.JourneyViewState.JourneyReturningTimeSearchType;
				}
			}

			emailShowLocations = ( itineraryExists || ( showFindA && (sessionManager.FindAMode != FindAMode.Car)));

			// CR 16/05/06 - Hide return selection when journey planned from Park and Ride. 
			if(TDSessionManager.Current.JourneyParameters.Destination.SearchType == SearchType.ParkAndRide)
			{
				theAmendSaveSendAmendControl.HideReturnDate();
			}
			// end CR 16/05/06
		
		}


		private void initialiseHelpControls() 
		{
			// Set visibility and text of help controls

			AmendPanelMode panelToShow = 
				(AmendPanelMode)TDSessionManager.Current.JourneyViewState.SelectedTabIndex;

			switch (panelToShow) 
			{
				case AmendPanelMode.AmendCostSearchDay :
                    //amendHelpCustomControl.Visible = (!hideHelpButton);
					amendHelpLabelControl.Text = Global.tdResourceManager.GetString("amendHelpLabelControlCostSearchDay", TDCultureInfo.CurrentUICulture);
					break;
                case AmendPanelMode.AmendCostSearchDate :
                    //amendHelpCustomControl.Visible = (!hideHelpButton);
					amendHelpLabelControl.Text = Global.tdResourceManager.GetString("amendHelpLabelControlCostSearchDate", TDCultureInfo.CurrentUICulture);
                    break;
                case AmendPanelMode.AmendFareDetail :
					//amendHelpCustomControl.Visible = (!hideHelpButton);
					//amendHelpCustomControl.AlternateText = GetResource("amendHelpCustomControlFares.AlternateText");
					amendHelpLabelControl.Text = Global.tdResourceManager.GetString("amendHelpLabelControlFare", TDCultureInfo.CurrentUICulture);
					break;
				case AmendPanelMode.AmendView :
					//amendHelpCustomControl.Visible = (!hideHelpButton);
					amendHelpLabelControl.Text = Global.tdResourceManager.GetString("amendHelpLabelControlView", TDCultureInfo.CurrentUICulture);
					break;
				case AmendPanelMode.AmendDateTimeNormal :
					//amendHelpCustomControl.Visible = (!hideHelpButton);
					amendHelpLabelControl.Text = Global.tdResourceManager.GetString("amendHelpLabelControlAmend", TDCultureInfo.CurrentUICulture);
					break;
				case AmendPanelMode.SaveFavouriteNormal:
				case AmendPanelMode.SaveFavouriteListIsFull:
				case AmendPanelMode.SaveFavouriteLogin:
					//amendHelpCustomControl.Visible = (!hideHelpButton);
					//amendHelpCustomControl.AlternateText = GetResource("amendHelpCustomControlSave.AlternateText");
					amendHelpLabelControl.Text = Global.tdResourceManager.GetString("amendHelpLabelControlSave", TDCultureInfo.CurrentUICulture);
					break;
				case AmendPanelMode.SendEmailNormal:
				case AmendPanelMode.SendEmailLogin:
					//amendHelpCustomControl.Visible = (!hideHelpButton);
					//amendHelpCustomControl.AlternateText = GetResource("amendHelpCustomControlSend.AlternateText");
					amendHelpLabelControl.Text = Global.tdResourceManager.GetString("amendHelpLabelControlSend", TDCultureInfo.CurrentUICulture);
					break;
				case AmendPanelMode.AmendCarDetails:
					//amendHelpCustomControl.Visible = false; //(!hideHelpButton);
					break;

			}
		}

        /// <summary>
        /// Gets property value for the property
        /// This property should be used for the properties which have boolean values defined
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        /// <returns>Boolean value of property</returns>
        private bool GetPropertyValue(string propertyName)
        {
            bool propertyValue = true;

            string propertyValueString = Properties.Current[propertyName];

            if (!string.IsNullOrEmpty(propertyValueString))
            {
                if (!bool.TryParse(propertyValueString, out propertyValue))
                {
                    propertyValue = true;
                }
            }

            return propertyValue;
        }


		/// <summary>
		/// Set the visibility and data sources for the controls
		/// </summary>
		private void SetupControls()
		{
			// Logic:
			// Decide which tabs are valid to display for this data set.
			// Display the previously selected panel if the corresponding tab is visible.
			// If previously selected panel cannot be displayed then display the leftmost panel.

			TDUser user = TDSessionManager.Current.CurrentUser;

			// Default all tabs to invisible so that the following logic only needs to decide which tabs to make visible
			imageButtonTabAmendDateTime.Visible = false;
			imageButtonTabAmendStopover.Visible = false;
			imageButtonTabSaveFavourite.Visible = false;
			imageButtonTabSendEmail.Visible = false;
			imageButtonTabFareDetail.Visible = false;
			imageButtonTabAmendDay.Visible = false;
			imageButtonTabAmendCostSearchDate.Visible = false;
			imageButtonTabAmendView.Visible = false;
			imageButtonTabAmendCarDetails.Visible = false;
			
			
			//show the amend view tab if partitioning is available
			imageButtonTabAmendView.Visible = AmendViewControl.PartitionSelectionAvailable;

			// Determine which tabs should be visible
			switch (((TDPage)Page).PageId)
			{
				case PageId.FindFareDateSelection:
    				imageButtonTabAmendCostSearchDate.Visible = true;
	    			imageButtonTabFareDetail.Visible = true;					
					break;
				case PageId.FindFareTicketSelection:
                case PageId.FindFareTicketSelectionReturn:
                case PageId.FindFareTicketSelectionSingles:
                    imageButtonTabAmendDay.Visible = theAmendCostSearchDayControl.DatesAreFlexible;					
					break;
				case PageId.VisitPlannerResults:
					imageButtonTabSendEmail.Visible = outwardExists || returnExists;
					break;
				case PageId.RefineTickets:
					imageButtonTabFareDetail.Visible = showFares;
					break;
				case PageId.JourneyEmissions:
					imageButtonTabAmendDateTime.Visible = true;
					imageButtonTabSendEmail.Visible = true;
					imageButtonTabAmendCarDetails.Visible = true;
					break;
				default:
                    imageButtonTabSendEmail.Visible = (outwardExists || returnExists) && GetPropertyValue("AmendSaveSendControl.SendEmail.Visible");

					if (!isCostBased)
					{
						imageButtonTabAmendDateTime.Visible = true;

                        if (showSaveFavorites && GetPropertyValue("AmendSaveSendControl.SaveFavourite.Visible"))
						{
							imageButtonTabSaveFavourite.Visible = true;
						}
					}

					if (showFares)
					{
						imageButtonTabFareDetail.Visible = true;
					}
					break;
			}

			AmendPanelMode panelToShow = AmendPanelMode.None;  // panelToShow is initially unknown

			// Test whether we can display previously selected panel
			switch (currentControl)
			{
				case AmendPanelMode.AmendCostSearchDate:
					if (imageButtonTabAmendCostSearchDate.Visible)
					{
						panelToShow = AmendPanelMode.AmendCostSearchDate;
					}
					break;

				case AmendPanelMode.AmendFareDetail:
					if (imageButtonTabFareDetail.Visible)
					{
						panelToShow = AmendPanelMode.AmendFareDetail;
					}
					break;

				case AmendPanelMode.AmendDateTimeNormal :
					if (imageButtonTabAmendDateTime.Visible)
					{
						panelToShow = AmendPanelMode.AmendDateTimeNormal;
					}
					break;

				case AmendPanelMode.AmendStopoverNormal :
					if (imageButtonTabAmendStopover.Visible)
					{
						panelToShow = AmendPanelMode.AmendStopoverNormal;
					}
					break;

				case AmendPanelMode.AmendCostSearchDay:
					if (imageButtonTabAmendDay.Visible)
					{
						panelToShow = AmendPanelMode.AmendCostSearchDay;
					}
					break;

				case AmendPanelMode.AmendView:
					if (imageButtonTabAmendView.Visible)
					{
						panelToShow = AmendPanelMode.AmendView;
					}
					break;

				case AmendPanelMode.AmendCarDetails:
					if (imageButtonTabAmendCarDetails.Visible)
					{
						panelToShow = AmendPanelMode.AmendCarDetails;
					}
					break;

				case AmendPanelMode.SaveFavouriteNormal :
				case AmendPanelMode.SaveFavouriteLogin :
				case AmendPanelMode.SaveFavouriteListIsFull :
					if (imageButtonTabSaveFavourite.Visible)
					{
						if (user != null)
						{
							if( user.FullFavouriteList ) 
							{
								panelToShow = AmendPanelMode.SaveFavouriteListIsFull;
							}
							else
							{
								panelToShow = AmendPanelMode.SaveFavouriteNormal;
							}
						}
						else
						{
							panelToShow = AmendPanelMode.SaveFavouriteLogin;
						}
					}
					break;

				case AmendPanelMode.SaveFavouriteConfirmation :
					if (imageButtonTabSaveFavourite.Visible)
					{
						panelToShow = AmendPanelMode.SaveFavouriteConfirmation;
					}
					break;

				case AmendPanelMode.SendEmailNormal :
				case AmendPanelMode.SendEmailLogin :
					if (imageButtonTabSendEmail.Visible)
					{
						if (user != null)
						{
							panelToShow = AmendPanelMode.SendEmailNormal;
						}
						else
						{
							panelToShow = AmendPanelMode.SendEmailLogin;
						}
					}
					break;

				case AmendPanelMode.SendEmailConfirmation :
					if (imageButtonTabSendEmail.Visible)
					{
						panelToShow = AmendPanelMode.SendEmailConfirmation;
					}
					break;				
			}

			// If previously selected panel cannot be displayed then display leftmost panel
			if (panelToShow == AmendPanelMode.None)
			{
				// Test visibility of each tab in LEFT to RIGHT direction

				// MAINTENANCE NOTE:
				// If adding a new tab, ensure that the test is inserted in the
				// correct place corresponding to the displayed location.

				if (imageButtonTabAmendCostSearchDate.Visible)
				{
					panelToShow = AmendPanelMode.AmendCostSearchDate;
				}
				else if (imageButtonTabFareDetail.Visible)
				{
					panelToShow = AmendPanelMode.AmendFareDetail;
				}
				else if (imageButtonTabAmendView.Visible)
				{
					panelToShow = AmendPanelMode.AmendView;
				}
				else if (imageButtonTabAmendDay.Visible)
				{
					panelToShow = AmendPanelMode.AmendCostSearchDay;
				}
				else if (imageButtonTabAmendDateTime.Visible)
				{
					panelToShow = AmendPanelMode.AmendDateTimeNormal;
				}
				else if (imageButtonTabAmendStopover.Visible)
				{
					panelToShow = AmendPanelMode.AmendStopoverNormal;
				}
				else if (imageButtonTabAmendCarDetails.Visible)
				{
					panelToShow = AmendPanelMode.AmendCarDetails;
				}
				else if (imageButtonTabSaveFavourite.Visible)
				{
					if (imageButtonTabSaveFavourite.Visible)
					{
						if (user != null)
						{
							if( user.FullFavouriteList ) 
							{
								panelToShow = AmendPanelMode.SaveFavouriteListIsFull;
							}
							else
							{
								panelToShow = AmendPanelMode.SaveFavouriteNormal;
							}
						}
						else
						{
							panelToShow = AmendPanelMode.SaveFavouriteLogin;
						}
					}
				}
				else if (imageButtonTabSendEmail.Visible)
				{
					if (user != null)
					{
						panelToShow = AmendPanelMode.SendEmailNormal;
					}
					else
					{
						panelToShow = AmendPanelMode.SendEmailLogin;
					}
				}
				else if (imageButtonTabAmendDay.Visible) 
				{
					panelToShow = AmendPanelMode.AmendCostSearchDay;
				}
			}

			// Display the panel that we have determined needs to be displayed
			UpdatePanel(panelToShow);
		}

		/// <summary>
		/// Initialises language sensitive strings using resource manager
		/// </summary>
		private void initialiseResourceStrings()
		{

			stopoverTabSelectedImageUrl = GetResource("AmendSaveSend.ImageButtonStopoverTabSelected");
			stopoverTabUnselectedImageUrl = GetResource("AmendSaveSend.ImageButtonStopoverTabUnselected");
			
			amendTabSelectedImageUrl = GetResource("AmendSaveSend.ImageButtonAmendTabSelected");

			amendTabUnselectedImageUrl = GetResource("AmendSaveSend.ImageButtonAmendTabUnselected");

			saveTabSelectedImageUrl = GetResource("AmendSaveSend.ImageButtonSaveTabSelected");

			saveTabUnselectedImageUrl = GetResource("AmendSaveSend.ImageButtonSaveTabUnselected");

			sendTabSelectedImageUrl = GetResource("AmendSaveSend.ImageButtonSendTabSelected");

			sendTabUnselectedImageUrl = GetResource("AmendSaveSend.ImageButtonSendTabUnselected");
			
			fareTabSelectedImageUrl = GetResource("AmendSaveSend.ImageButtonFareTabSelected");

			fareTabUnselectedImageUrl = GetResource("AmendSaveSend.ImageButtonFareTabUnselected");

			amendViewTabSelectedImageUrl = GetResource("AmendSaveSend.ImageButtonAmendViewTabSelected");

			amendViewTabUnselectedImageUrl = GetResource("AmendSaveSend.ImageButtonAmendViewTabUnSelected");

			amendDayTabSelectedImageUrl = GetResource("AmendSaveSend.ImageButtonAmendDayTabSelected");

            amendDayTabUnselectedImageUrl = GetResource("AmendSaveSend.ImageButtonAmendDayTabUnselected");

            costSearchDateTabSelectedImageUrl = GetResource("AmendSaveSend.ImageButtonCostSearchDateTabSelected");

            costSearchDateTabUnselectedImageUrl = GetResource("AmendSaveSend.ImageButtonCostSearchDateTabUnselected");

			amendCarDetailsTabSelectedImageUrl = GetResource("AmendSaveSend.ImageButtonAmendCarDetailsTabSelected");

			amendCarDetailsTabUnSelectedImageUrl = GetResource("AmendSaveSend.ImageButtonAmendCarDetailsTabUnselected");

			messageLoginIncorrect = GetResource("AmendSaveSend.messageLoginIncorrect");

			messageInvalidEmailAddress = GetResource("AmendSaveSend.messageInvalidEmailAddress");

			messsageJourneyDetailsSent = GetResource("AmendSaveSend.messsageJourneyDetailsSent");

			messageProblemSendingEmail = GetResource("AmendSaveSend.messageProblemSendingEmail");

			messsageJourneyDetailsSaved = GetResource("AmendSaveSend.messsageJourneyDetailsSaved");

			emailIntro = GetResource("AmendSaveSendControl.emailIntro");
            
			emailJourneyFrom = GetResource("AmendSaveSendControl.emailJourneyFrom");

			emailJourneyTo = GetResource("AmendSaveSendControl.emailJourneyTo");
        
			emailDepart = GetResource("AmendSaveSendControl.emailDepart");
        
			emailArrive = GetResource("AmendSaveSendControl.emailArrive");
            
			emailTransport = GetResource("AmendSaveSendControl.emailTransport");
                
			emailDuration = GetResource("AmendSaveSendControl.emailDuration");                
                
			emailChanges = GetResource("AmendSaveSendControl.emailChanges");                
                
			emailLeave = GetResource("AmendSaveSendControl.emailLeave");                
                
			emailFrom = GetResource("AmendSaveSendControl.emailFrom");                
                
			emailTo = GetResource("AmendSaveSendControl.emailTo");                
                
			emailSummaryOutward = GetResource("AmendSaveSendControl.emailSummaryOutward");                

			emailSummaryOutwardNoDate = GetResource("AmendSaveSendControl.emailSummaryOutwardNoDate");                

			emailDetailsOutward = GetResource("AmendSaveSendControl.emailDetailsOutward");                

			emailItineraryDetailsOutward = GetResource("AmendSaveSendControl.emailItineraryDetailsOutward");                

			emailSummaryReturn = GetResource("AmendSaveSendControl.emailSummaryReturn");                

			emailSummaryReturnNoDate = GetResource("AmendSaveSendControl.emailSummaryReturnNoDate");                

			emailDetailsReturn = GetResource("AmendSaveSendControl.emailDetailsReturn");                

			emailItineraryDetailsReturn = GetResource("AmendSaveSendControl.emailItineraryDetailsReturn");                

			emailLabelArrivingBefore = GetResource("JourneyPlanner.labelArrivingBefore");
                    
			emailLabelLeavingAfter = GetResource("JourneyPlanner.labelLeavingAfter");

			emailOutwardJourneyFrom = GetResource("AmendSaveSendControl.emailOutwardJourneyFrom");
 
			emailReturnJourneyFrom = GetResource("AmendSaveSendControl.emailReturnJourneyFrom");		

			emailLengthOfStay = GetResource("AmendSaveSendControl.emailLengthOfStay");

			emailHours = GetResource("AmendSaveSendControl.emailHours");

			emailDetailsJourney = GetResource("AmendSaveSendControl.emailDetailsJourney");

			emailSummaryJourneys = GetResource("AmendSaveSendControl.emailSummaryJourneys");

            emailPlanLinkText = GetResource("AmendSaveSendControl.emailPlanLinkText");
        
		}


		/// <summary>
		/// Method to set the alternate text of the tab buttons.
		/// </summary>
		private void SetAlternateText()
		{
			imageButtonTabAmendDateTime.AlternateText = GetResource("AmendSaveSendControl.AmendDateTimeAltText");

			imageButtonTabSaveFavourite.AlternateText = GetResource("AmendSaveSendControl.SaveAsFavouriteAltText");

			imageButtonTabSendEmail.AlternateText = GetResource("AmendSaveSendControl.SendToFriendAltText");

			imageButtonTabAmendStopover.AlternateText = GetResource("AmendSaveSendControl.StopoverAltText");

			imageButtonTabFareDetail.AlternateText = GetResource("AmendSaveSendControl.FareDetailAltText");

			imageButtonTabAmendDay.AlternateText = GetResource("AmendSaveSendControl.AmendDayAltText");

            imageButtonTabAmendCostSearchDate.AlternateText = GetResource("AmendSaveSendControl.CostSearchDateAltText"); 

			imageButtonTabAmendView.AlternateText = GetResource("AmendSaveSendControl.ViewAltText"); 
			
			imageButtonTabAmendCarDetails.AlternateText = GetResource("AmendSaveSendControl.AmendCarDetailsAltText");
			
		}


		/// <summary>
		/// Initialises this control.
		/// </summary>
		/// <param name="callingPageId">PageId of calling page.</param>
		public void Initialise(PageId callingPageId)
		{
			theAmendSaveSendAmendControl.Initialise(callingPageId);
			theAmendStopoverControl.Initialise(callingPageId);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{

			// Handlers for the buttons on the controls inside the panel
			theAmendSaveSendSaveControl.OKButton.Click += new EventHandler(this.SaveOKButton_Click);
			theAmendSaveSendSaveFullControl.OKButton.Click += new EventHandler(this.FullOKButton_Click);
			theAmendSaveSendSendControl.SendButton.Click += new EventHandler(this.EmailJourneySendButton_Click);
            imageButtonTabFareDetail.Click += new System.Web.UI.ImageClickEventHandler(this.imageButtonTabFareDetail_Click);
            imageButtonTabAmendDateTime.Click += new System.Web.UI.ImageClickEventHandler(this.imageButtonTabAmendDateTimeClick);
            imageButtonTabAmendStopover.Click += new System.Web.UI.ImageClickEventHandler(this.imageButtonTabAmendStopover_Click);
            imageButtonTabSaveFavourite.Click += new System.Web.UI.ImageClickEventHandler(this.imageButtonTabSaveFavouriteClick);
            imageButtonTabSendEmail.Click += new System.Web.UI.ImageClickEventHandler(this.imageButtonTabSendEmailClick);			
            imageButtonTabAmendCostSearchDate.Click += new System.Web.UI.ImageClickEventHandler(this.imageButtonTabAmendCostSearchDate_Click);
			imageButtonTabAmendView.Click += new System.Web.UI.ImageClickEventHandler(this.imageButtonTabAmendView_Click);
            imageButtonTabAmendDay.Click += new System.Web.UI.ImageClickEventHandler(this.imageButtonTabAmendDay_Click);
			imageButtonTabAmendCarDetails.Click += new System.Web.UI.ImageClickEventHandler(this.imageButtonTabAmendCarDetails_Click);

			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion Web Form Designer generated code

		
		/// Checks the current authentication information to 
		/// determine whether the user is authenticated.  Returns true if
		/// authenticated, false otherwise.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsLoggedIn()
		{
			return TDSessionManager.Current.Authenticated; 
		}

        /// <summary>
        /// Public method to enable active tab of the control
        /// </summary>
        /// <param name="panelToShow">Tab to make active</param>
        public void SetActiveTab(AmendPanelMode panelToShow)
        {
            //if user not logged in and panelToShow is SendEmailNormal set it to SendEmailLogin
            if (!IsLoggedIn() && panelToShow == AmendPanelMode.SendEmailNormal)
            {
                panelToShow = AmendPanelMode.SendEmailLogin;
            }
            UpdatePanel(panelToShow);
        }


		/// <summary>
		/// Changes the current display to the given control.
		/// </summary>
		/// <param name="panelToShow">The new control to display in the panel.</param>
		private void UpdatePanel(AmendPanelMode panelToShow)
		{

			theAmendCostSearchDateControl.Visible = (panelToShow == AmendPanelMode.AmendCostSearchDate);
			theAmendCarDetailsControl.Visible = (panelToShow == AmendPanelMode.AmendCarDetails);
			theAmendFaresControl.Visible = (panelToShow == AmendPanelMode.AmendFareDetail);
			theAmendSaveSendAmendControl.Visible = panelToShow == AmendPanelMode.AmendDateTimeNormal;
			theAmendSaveSendSaveControl.Visible = panelToShow == AmendPanelMode.SaveFavouriteNormal;
			theAmendSaveSendSaveFullControl.Visible = panelToShow == AmendPanelMode.SaveFavouriteListIsFull;
			theAmendSaveSendMessageControl.Visible = (panelToShow == AmendPanelMode.SaveFavouriteConfirmation) || (panelToShow == AmendPanelMode.SendEmailConfirmation);
			theAmendSaveSendSendControl.Visible = panelToShow == AmendPanelMode.SendEmailNormal;
			theAmendSaveSendLoginControl.Visible = (panelToShow == AmendPanelMode.SendEmailLogin) || (panelToShow == AmendPanelMode.SaveFavouriteLogin);
			theAmendStopoverControl.Visible = panelToShow == AmendPanelMode.AmendStopoverNormal;
			theAmendCostSearchDayControl.Visible = panelToShow == AmendPanelMode.AmendCostSearchDay;
			theAmendViewControl.Visible = panelToShow == AmendPanelMode.AmendView;

			// Set the tab image button images

            // First set all images to unselected tabs
            imageButtonTabAmendView.ImageUrl = amendViewTabUnselectedImageUrl;					
            imageButtonTabFareDetail.ImageUrl = fareTabUnselectedImageUrl;					
            imageButtonTabAmendCostSearchDate.ImageUrl = costSearchDateTabUnselectedImageUrl;					
            imageButtonTabAmendDateTime.ImageUrl = amendTabUnselectedImageUrl;
            imageButtonTabSaveFavourite.ImageUrl = saveTabUnselectedImageUrl;
            imageButtonTabSendEmail.ImageUrl = sendTabUnselectedImageUrl;	
            imageButtonTabAmendDay.ImageUrl = amendDayTabUnselectedImageUrl;
            imageButtonTabAmendStopover.ImageUrl = stopoverTabUnselectedImageUrl;
			imageButtonTabAmendCarDetails.ImageUrl = amendCarDetailsTabUnSelectedImageUrl;

            // Now set the image of the selected tab
            switch (panelToShow)
			{
				case AmendPanelMode.AmendCostSearchDate :
					imageButtonTabAmendCostSearchDate.ImageUrl = costSearchDateTabSelectedImageUrl;                    
                    break;
				case AmendPanelMode.AmendFareDetail :
                    imageButtonTabFareDetail.ImageUrl = fareTabSelectedImageUrl;					
                    break;
				case AmendPanelMode.AmendView:
					imageButtonTabAmendView.ImageUrl = amendViewTabSelectedImageUrl;					
                    break;
				case AmendPanelMode.AmendDateTimeNormal :					
					imageButtonTabAmendDateTime.ImageUrl = amendTabSelectedImageUrl;
                    break;
				case AmendPanelMode.AmendStopoverNormal :
					imageButtonTabAmendStopover.ImageUrl = stopoverTabSelectedImageUrl;
					break;
				case AmendPanelMode.AmendCarDetails :
					imageButtonTabAmendCarDetails.ImageUrl = amendCarDetailsTabSelectedImageUrl;
					break;
				case AmendPanelMode.SaveFavouriteNormal :
				case AmendPanelMode.SaveFavouriteLogin :
				case AmendPanelMode.SaveFavouriteConfirmation :
					imageButtonTabSaveFavourite.ImageUrl = saveTabSelectedImageUrl;
                    break;
				case AmendPanelMode.SaveFavouriteListIsFull :
					theAmendSaveSendSaveFullControl.LoadDropList();
					imageButtonTabSaveFavourite.ImageUrl = saveTabSelectedImageUrl;
                    break;		
				case AmendPanelMode.SendEmailNormal :
				case AmendPanelMode.SendEmailLogin :
				case AmendPanelMode.SendEmailConfirmation :
					imageButtonTabSendEmail.ImageUrl = sendTabSelectedImageUrl;
                    break;
				case AmendPanelMode.AmendCostSearchDay:
                    imageButtonTabAmendDay.ImageUrl = amendDayTabSelectedImageUrl;
					break;
			}

            // Hide entire control if previous logic has determined that no panel should be shown
            this.Visible = panelToShow != AmendPanelMode.None;

			// Updated TDJourneyViewState to indicate the tab selected
			TDSessionManager.Current.JourneyViewState.SelectedTabIndex = (int)panelToShow;
		}

		#region Event Handlers for the tab buttons

		/// <summary>
		/// Event handler to handle the Amend Date Time tab button click.
		/// </summary>
		private void imageButtonTabAmendDateTimeClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdatePanel(AmendPanelMode.AmendDateTimeNormal);
		}

		/// <summary>
		/// Event handler to handle the Cost Search Date tab button click.
		/// </summary>
		private void imageButtonTabAmendCostSearchDate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdatePanel(AmendPanelMode.AmendCostSearchDate);
		}

		
		/// <summary>
		/// Event handler to handle the Amend Car Details tab button click.
		/// </summary>
		private void imageButtonTabAmendCarDetails_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdatePanel(AmendPanelMode.AmendCarDetails);
		}

		/// <summary>
		/// Event handler to handle the Fare detail tab button click.
		/// </summary>
		private void imageButtonTabFareDetail_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdatePanel(AmendPanelMode.AmendFareDetail);
		}

		/// <summary>
		/// Event handler to handle the Save Favourite tab button click.
		/// </summary>
		private void imageButtonTabSaveFavouriteClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			TDUser user = TDSessionManager.Current.CurrentUser;
			// Check if the user is logged on
			if( user != null) 
			{
				// if the journey has already been saved, display the confirmation message
				if (TDSessionManager.Current.JourneyViewState.FavouriteJourneySaved)
				{
					UpdatePanel(AmendPanelMode.SaveFavouriteConfirmation);
					theAmendSaveSendMessageControl.MessageLabel.Text = GetResource("AmendSaveSend.messsageJourneyDetailsSaved");
				}
				else
				{
					// check if their favourite journey list is full
					if( user.FullFavouriteList )
					{
						UpdatePanel(AmendPanelMode.SaveFavouriteListIsFull);
					}
					else
					{
						UpdatePanel(AmendPanelMode.SaveFavouriteNormal);
					}
				}
			}
			else 
			{
				UpdatePanel(AmendPanelMode.SaveFavouriteLogin); //display login
			}
		}

		/// <summary>
		/// Event handler to handle the Send Email tab button click.
		/// </summary>
		private void imageButtonTabSendEmailClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			// Check to see if the user is logged on.
			if(IsLoggedIn()) 
			{
				UpdatePanel(AmendPanelMode.SendEmailNormal); 
			}
			else 
			{
				UpdatePanel(AmendPanelMode.SendEmailLogin); //display login
			}
		}

		/// <summary>
		/// Event handler to handle the Amend Stoppover tab button click.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void imageButtonTabAmendStopover_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			// This tab should only be visible if the user has elected to add an extension !
			UpdatePanel(AmendPanelMode.AmendStopoverNormal);
		}

		/// <summary>
		/// Event handler to handle the Amend View tab button click.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void imageButtonTabAmendView_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{			
			UpdatePanel(AmendPanelMode.AmendView);
		}

        /// <summary>
        /// Event handler to handle the Amend Date (for ticket selection) tab button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageButtonTabAmendDay_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {			
            UpdatePanel(AmendPanelMode.AmendCostSearchDay);
        }

		#endregion Event Handlers for the tab buttons

		#region Event handlers for buttons that exist in the panels

		/// <summary>
		/// Handler for the AmendSaveSendSaveFullControl Ok image button click event.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">ImageClickEventArgs</param>
		private void FullOKButton_Click(object sender, EventArgs e)
		{
			TDUser user = TDSessionManager.Current.CurrentUser;
			if( user != null )
			{
				// obtain selected journey GUID from drop list
				string journeyGUID = theAmendSaveSendSaveFullControl.JourneyDropList.SelectedItem.Value;
				FavouriteJourney fj = user.FindRegisteredFavourite( journeyGUID );
				if(fj != null) 
				{
					// rename the journey - if there is no name entered, leave it as it was before
					if(theAmendSaveSendSaveFullControl.FavouriteName.Length != 0) 
					{
						fj.Name = theAmendSaveSendSaveFullControl.FavouriteName;

					}
					SaveFavouriteDetails(fj);	
				}
			} 
		}

		public void SaveFavouriteDetails(FavouriteJourney journey) 
		{
			FavouriteJourneyHelper.SaveFavouriteJourney( journey );

			//clear name from text box for redisplay
			theAmendSaveSendSaveControl.NameTextBox.Text = string.Empty;

			//update the panel 
			theAmendSaveSendMessageControl.MessageLabel.Text = messsageJourneyDetailsSaved;
			theAmendSaveSendMessageControl.MessageLabel.Visible = true;

			// Update ViewState to save a journey has been saved
			TDSessionManager.Current.JourneyViewState.FavouriteJourneySaved = true;

			UpdatePanel(AmendPanelMode.SaveFavouriteConfirmation);
		}

		/// <summary>
		/// Handler for the AmendSaveSendSaveControl Ok image button click event.
		/// Sets favourite journey details from the current journey parameters.  
		/// Saves the cached FavouriteJourney Profile.  Links the new/overwritten 
		/// favourite journey to the user's profile
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">ImageClickEventArgs</param>
		private void SaveOKButton_Click(object sender, EventArgs e)
		{
			//create favourite journey profile 
			string jName = string.Empty; // journey name string

			TDUser user = TDSessionManager.Current.CurrentUser;
			if( user != null )
			{
				int index = user.NextFavouriteJourneyIndex();
				// obtain selected journey GUID from drop list
				FavouriteJourney fj = user.NewJourney();

				jName = theAmendSaveSendSaveControl.JourneyName;

				if (jName.Length > 50) // size of field in the database
				{
					jName = jName.Substring(0,50); // truncate the Name to fit in database
				}

				// if they entered a name, use it, otherwise use the default
				if(jName.Length == 0) 
				{
					jName = GetResource("AmendSaveSendSaveControl.labelCurrentName") + " " + (index + 1);
				}

				fj.Name = jName;

				SaveFavouriteDetails(fj);
			}
		}
        
		/// <summary>
		/// Event handler to handle the email journey send button click.
		/// Assembles journey data from Itinerary journey result and request.
		/// </summary>
		private void EmailJourneySendButton_Click(object sender, EventArgs e)
		{
			bool validEmailAddress = theAmendSaveSendSendControl.ValidEmailAddress();

			//Get currently logged in user's email address
			string emailFromAddress = TDSessionManager.Current.CurrentUser.Username;

			//Set email subject using property service
			string emailSubject = emailFromAddress + GetResource("AmendSaveSendSendControl.EmailSubject");

			if(validEmailAddress)
			{
				try
				{
					//create contents of mail message                       
					string emailBody = generateMailBody();

					theAmendSaveSendSendControl.SendEmail(emailFromAddress, emailSubject, emailBody);

					//update panel to include email sent message 
					theAmendSaveSendMessageControl.MessageLabel.Text = messsageJourneyDetailsSent;

					UpdatePanel(AmendPanelMode.SendEmailConfirmation);
				}
				catch(TDException tde)
				{
					// log exception and re-throw TDException
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, GetResource("AmendSaveSendControl.emailFailed"));

					Logger.Write( operationalEvent );

					throw new TDException(GetResource("AmendSaveSendControl.emailFailed"), tde, true, TDExceptionIdentifier.BTCSendJourneyDetailsFailed); 
				}

			}

		}

		#endregion Event handlers for buttons that exist in the panels

		#region Duration  and Mileage Calculations - see SummaryResultTableControl

		/// <summary>
		/// Returns the duration as a formatted string in the form "0hrs 0mins". 
		/// The duration is calculated as the difference between the supplied departure and 
		/// arrival times
		/// </summary>
		/// <param name="arrivalTime">end time</param>
		/// <param name="departureTime">start time</param>
		/// <returns></returns>
		public string GetDuration(TDDateTime arrivalTime, TDDateTime departureTime)
		{			
			if(arrivalTime.Second >= 30)
				arrivalTime = arrivalTime.AddMinutes(1);

			DateTime dateTimeArrivalTime = new DateTime
				(arrivalTime.Year, arrivalTime.Month, arrivalTime.Day, arrivalTime.Hour, arrivalTime.Minute, 
				arrivalTime.Second, arrivalTime.Millisecond);

			DateTime dateTimeDepartureTime = new DateTime
				(departureTime.Year, departureTime.Month, departureTime.Day, departureTime.Hour, 
				departureTime.Minute, departureTime.Second, departureTime.Millisecond);

			// find the difference between the two times
			TimeSpan diff = dateTimeArrivalTime.Subtract(dateTimeDepartureTime);

			return GetDuration(diff);

		}

		/// <summary>
		/// Returns the duration as a formatted string in the form "0hrs 0mins"
		/// </summary>
		/// <param name="summary">The duration</param>
		/// <returns>Formatted duration string</returns>
		public string GetDuration(TimeSpan durationTimeSpan)
		{			
			string hoursString = GetResource(
				"JourneyDetailsTableControl.hoursString");

			string minutesString = GetResource(
				"JourneyDetailsTableControl.minutesString");

			string hourString = GetResource(
				"JourneyDetailsTableControl.hourString");

			string minuteString = GetResource(
				"JourneyDetailsTableControl.minuteString");

			// format the duration string.
			string duration = String.Empty;

			int hours = 0;

			// Greater than 24 hours case
			if(durationTimeSpan.Days > 0)
			{
				// For each day, there are 24 hours
				hours = 24 * durationTimeSpan.Days;
				hours += durationTimeSpan.Hours;
				duration += hours.ToString(TDCultureInfo.CurrentCulture.NumberFormat) + hoursString;
			}
			else if(durationTimeSpan.Hours != 0)
			{
				hours += durationTimeSpan.Hours;
				duration += hours.ToString(TDCultureInfo.CurrentCulture.NumberFormat);

				if(durationTimeSpan.Hours > 1)
					duration += hoursString;
				else
					duration += hourString;
			}

			// Round up to nearest minute for consistency
			if (durationTimeSpan.Seconds >= 30) 
			{
				durationTimeSpan = durationTimeSpan.Add(new TimeSpan(0, 1, 0));
			}

			if(durationTimeSpan.Minutes != 0)
			{
				// if hour was not equal to 0 then add a comma
				if(hours != 0)
					duration += ", ";

				// Check to see if minutes requires a 0 padding.
				// Pad with 0 only if an hour was present and minute is a single digit.
				if(durationTimeSpan.Minutes < 10 & hours != 0)
					duration += "0";

				duration += durationTimeSpan.Minutes.ToString(TDCultureInfo.CurrentCulture.NumberFormat);

				if(durationTimeSpan.Minutes > 1)
					duration += minutesString;
				else
					duration += minuteString;
			}
			else if(hours == 0 && durationTimeSpan.Minutes == 0)
			{
				// This leg has 0 hours 0 minutes, e.g. a journey to itself.
				// Should never really happen, but still required otherwise
				// no duration will be displayed.
				duration += "0";

				if(durationTimeSpan.Minutes > 1)
					duration += minutesString;
				else
					duration += minuteString;
			}

			return duration;
		}

		/// <summary>
		/// Formats the road journey number of miles, returing them as a string  
		/// </summary>
		/// <param name="roadMiles">int value of number of miles</param>
		/// <returns>Distance</returns>
		public string GetRoadDistance(int roadMiles)
		{
			string distance = String.Empty;

			// obtain miles string from td resource
			string milesString = GetResource(
				"SummaryResultTable.labelMilesString");

			// Retrieve the conversion factor from the Properties Service.
			double conversionFactor = Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], 
				TDCultureInfo.CurrentCulture.NumberFormat);

			double result = (double)roadMiles/ conversionFactor;

			distance = result.ToString("F1", TDCultureInfo.CurrentCulture.NumberFormat) 
				+ " " + milesString;
			return distance;
		}

		#endregion Duration  and Mileage Calculations - see SummaryResultTableControl

        #region Generate Email

        #region Helpers

        /// <summary>
        /// Formats the given TDDateTime for display.
        /// </summary>
        /// <param name="time">TDDateTime to format.</param>
        /// <returns>Formatted time string for display in this control.</returns>
        private string FormatTDDateTime(TDDateTime time)
        {
            // Round up if necessary for consistency.
            if (time.Second >= 30)
                time = time.AddMinutes(1);

            return time.ToString("HH:mm");
        }

        /// <summary>
		/// Returns the selected journey line mode as a string
		/// </summary>
		/// <param name="sumLine">JourneySummaryLine</param>
		/// <returns>string</returns>
		public string GetModes(JourneySummaryLine sumLine) 
		{
			ModeType[] modes = sumLine.Modes;
			string modesString = string.Empty;

			// Read the strings from Resourcing Manager given the enumerations.
			int i=0;
			string resourceManagerKey = string.Empty;

			for(i=0; i<modes.Length-1; i++)
			{
				resourceManagerKey = "TransportMode." +
					modes[i].ToString();
				modesString += GetResource(
					resourceManagerKey);
				modesString += ", ";
			}

			// final mode element
			resourceManagerKey = "TransportMode." +
				modes[i].ToString();
			modesString += GetResource(
				resourceManagerKey);

			return modesString;
		}

		/// <summary>
		/// Takes a list of string arrays where each element in the list
		/// represents a row and each element in the string array represents 
		/// a column. The output is a string containing a formatted table
		/// where each column is padded with spaces to take account of the
		/// largest string in the column.
		/// </summary>
		/// <param name="table">A collection representing the table</param>
		/// <param name="lineBreak">Specifies after how many rows to output
		/// an additional line break. Zero if not required.</param>
		/// <returns>A formatted table</returns>
		private void getTableColumnWidths(IList table, ref int[] columnWidths)
		{
			// determine width of each column
			for (int columnCount = 0; columnCount < columnWidths.Length; columnCount++) 
			{
				foreach(object[] row in table) 
				{
					if (row[columnCount] != null) 
					{
						if (row[columnCount].ToString().Length > columnWidths[columnCount]) 
						{
							columnWidths[columnCount] = row[columnCount].ToString().Length;
						}
					}
				}
			}
        }

        /// <summary>
        /// Gets the total for the columnWidths in the array
        /// </summary>
        /// <param name="columnWidths"></param>
        /// <returns></returns>
        private int GetTotalColumnWidth(int[] columnWidths)
        {
            int totalWidth = 0;

            foreach (int width in columnWidths)
            {
                totalWidth += width;
            }

            return totalWidth;
        }

        /// <summary>
        /// Method to reduce column widths to the maxRowLength supplied.
        /// Where possible the original column width will be retained.
        /// </summary>
        /// <param name="columnWidths"></param>
        /// <param name="maxColumnWidth"></param>
        /// <returns></returns>
        private int[] AdjustColumnWidths(int[] columnWidths, int maxRowLength)
        {
            // Go through each column width and calculate the spare width, i.e. any columns which 
            // are less than the max column width.
            // Then redistribute equally the spare width to those columns which are over 
            // the max width so that their new width is the max + spare bit, up to the columns
            // original width.
            // This ensures the overall row length adds up to less than or equal to maxRowLength.
            // (Decimals are rounded down where applicable)

            // Original columnWidths - used to ensure new column widths dont go over the original
            ArrayList originalColumnWidthsTemp = new ArrayList();
            originalColumnWidthsTemp.AddRange(columnWidths);
            int[] originalColumnWidths = (int[])originalColumnWidthsTemp.ToArray(typeof(int));
            
            #region Get spare width

            // Average column width, becomes the max allowed column width
            int maxColumnWidth = (int)(Decimal.Floor(maxRowLength / columnWidths.Length));

            // Determine the amount of spare width we can play with, 
            // and set any columns to max allowed width as required
            int spareWidth = 0;
            
            for (int i = 0; i < columnWidths.Length; i++)
            {
                int columnWidth = columnWidths[i];

                if (columnWidth < maxColumnWidth)
                {
                    // Column is less than max allowed width, add diference to spare 
                    spareWidth += (maxColumnWidth - columnWidth);
                }
                else if (columnWidth > maxColumnWidth)
                {
                    // Because its too large, set the column width to be the max allowed width
                    columnWidths[i] = maxColumnWidth;
                }
            }

            #endregion

            #region Adjust column widths

            // Now add the spare width to each column which was too large.
            bool allColumnsProcessed = false;

            // Stop when we've used all the spare width up or once all the columns have got an appropriate width
            while ((spareWidth > 0) && (!allColumnsProcessed))
            {
                allColumnsProcessed = true;

                for (int i = 0; i < columnWidths.Length; i++)
                {
                    int columnWidth = columnWidths[i];

                    // Only add to width if its less than what it originally was,
                    // we're allowed to go over the max allowed width because we've 
                    // taken the spare width from the other columns
                    if (columnWidth < originalColumnWidths[i])
                    {
                        columnWidths[i] = columnWidth + 1;

                        spareWidth--;

                        allColumnsProcessed = false;
                    }

                    // Spare width all used
                    if (spareWidth <= 0)
                    {
                        break;
                    }
                }
            }

            #endregion

            // Finally, sanity check we're still less than or equal to max row length.
            // Only done to inform support why there may be some emails which go over the max length
            int rowLength = GetTotalColumnWidth(columnWidths);
            if ( rowLength > maxRowLength)
            {
                Logger.Write(new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Warning, 
                        string.Format("Adjusting column widths for email failed, max row length is configured to [{0}], after adjustment row length is [{1}]. Email processing carried on.", 
                            maxRowLength, rowLength)));
            }

            return columnWidths;
        }

        /// <summary>
        /// Method which takes an EmailJourneyDetailTable and moves text in a column on to a new row
        /// if it is larger than its specified column width
        /// </summary>
        /// <param name="journeyDetailTable"></param>
        /// <param name="columnWidths"></param>
        /// <returns></returns>
        private EmailJourneyDetailTable AdjustJourneyDetailTable(EmailJourneyDetailTable journeyDetailTable, int[] columnWidths)
        {
            // Get the values from the original EJDT
            EmailJourneyDetailType ejdType = journeyDetailTable.DetailType;
            int ejdLineBreak = journeyDetailTable.LineBreak;
            ArrayList ejdDetails = (ArrayList)journeyDetailTable.Details;

            // Set up the adjusted details
            ArrayList ejdNewDetails = new ArrayList();
            ArrayList newRow = new ArrayList();

            // The below loops through each row in the Arraylist. 
            // Each row contains a number of string columns. If the length of a column is above its 
            // allowed column width, then the remaining text is moved into its column on a new row.

            // Assume each row is a string
            for (int k = 0; k < ejdDetails.Count; k++)
            {
                string[] row = (string[])ejdDetails[k];

                // While looop to allow multiple new Rows to be created for this current Row
                bool completed = false;
                while (!completed)
                {
                    // Initialise the new Row iteration
                    newRow = new ArrayList();

                    // Assume we'll finish on this loop cycle
                    completed = true;

                    for (int i = 0; i < row.Length; i++)
                    {
                        // Get the column value
                        string rowColumnValue = row[i];

                        rowColumnValue = (string.IsNullOrEmpty(rowColumnValue)) ? string.Empty : rowColumnValue;

                        if (rowColumnValue.Length <= columnWidths[i])
                        {   
                            // Column value length is OK, just add it to the new row
                            newRow.Add(rowColumnValue);

                            row[i] = string.Empty;
                        }
                        else
                        {
                            // Column value length is too large, take the allowed length and put remainder back
                            // in to the original row
                            newRow.Add(rowColumnValue.Substring(0, columnWidths[i]));

                            row[i] = rowColumnValue.Substring(columnWidths[i]);

                            // if there is any text left over for this column, we need another pass through
                            if (row[i].Length > 0)
                            {
                                completed = false;
                            }
                        }

                    }

                    // Convert Row to string[] and place in the Details array
                    ejdNewDetails.Add((string[])newRow.ToArray(typeof(string)));
                }
            }

            // Return a newly created EJDT from the new Details created
            return new EmailJourneyDetailTable(ejdType, ejdNewDetails, ejdLineBreak);
        }

        #endregion

        #region Format

        /// <summary>
        /// Method which takes a list of EmailJourneyDetailTable and formats in to a string to 
        /// be sent as the email body
        /// </summary>
        /// <param name="detailTables"></param>
        /// <returns></returns>
        private string formatTable(IList detailTables)
		{
			int[] columnWidths = new int[0];
			int[] summaryColumnWidths = new int[0];
			int[] publicColumnWidths = new int[0];
			int[] roadColumnWidths = new int[0];

			StringBuilder builder = new StringBuilder();
			IList table;
			int lineBreak;

            #region Set the column widths
            foreach (EmailJourneyDetailTable journeyDetailTable in detailTables) 
			{
				if (journeyDetailTable != null) 
				{
					switch (journeyDetailTable.DetailType)
					{
						case EmailJourneyDetailType.Summary:
							if (summaryColumnWidths.Length == 0)
							{
								summaryColumnWidths = new int[((string[])journeyDetailTable.Details[0]).Length];
							}
							getTableColumnWidths(journeyDetailTable.Details, ref summaryColumnWidths);
							break;
						case EmailJourneyDetailType.Public:
							if (publicColumnWidths.Length == 0)
							{
								publicColumnWidths = new int[((string[])journeyDetailTable.Details[0]).Length];
							}
							getTableColumnWidths(journeyDetailTable.Details, ref publicColumnWidths);
							break;
						case EmailJourneyDetailType.Road:
							if (roadColumnWidths.Length == 0)
							{
								roadColumnWidths = new int[((string[])journeyDetailTable.Details[0]).Length];
							}
							getTableColumnWidths(journeyDetailTable.Details, ref roadColumnWidths);
							break;
						default:
							break;
					}
				}
            }

            #endregion

            #region Adjust column widths
            // Adjust the column widths so that max row width length is not over a configured value.
            // If it is then the EmailJourneyDetailTable needs to be adjusted so that text which overflows
            // the column is moved on to a new table row.
            // This is done to preserve the spacing of text to remain in its column.
            int maxRowLength = Convert.ToInt32(Properties.Current["Web.AmendSaveSendControl.Email.RowLength"]);

            // Used to hold the adjusted details
            ArrayList newDetailTables = new ArrayList();
            bool publicColumnWidthsAdjusted = false;
            
            foreach (EmailJourneyDetailTable journeyDetailTable in detailTables)
            {
                if (journeyDetailTable != null)
                {
                    switch (journeyDetailTable.DetailType)
                    {
                            // Only done for public, emails are generally ok for all other types
                        case EmailJourneyDetailType.Public:

                            // If the total row length is too large, do the adjustments
                            if ((GetTotalColumnWidth(publicColumnWidths) > maxRowLength) || (publicColumnWidthsAdjusted))
                            {
                                // Prevents adjust columns again, but allows the Details to be updated for both
                                // outward and return journeys
                                if (!publicColumnWidthsAdjusted)
                                {
                                    // We assume a sensible max length is used, and column widths 
                                    // are adjusted to this max length.
                                    publicColumnWidths = AdjustColumnWidths(publicColumnWidths, maxRowLength);

                                    publicColumnWidthsAdjusted = true;
                                }
                                
                                EmailJourneyDetailTable newJourneyDetailTable = AdjustJourneyDetailTable(journeyDetailTable, publicColumnWidths);
                                
                                newDetailTables.Add(newJourneyDetailTable);
                            }
                            else
                            {
                                newDetailTables.Add(journeyDetailTable);
                            }
                            break;

                        default:
                            newDetailTables.Add(journeyDetailTable);
                            break;
                    }
                }
            }
            // Override the original Detail table with the new
            detailTables = newDetailTables;

            #endregion

            #region Generate body string

            // generate string containing table contents with each
			// column padded to the maximum column width (plus two for column spacing)
			foreach (EmailJourneyDetailTable journeyDetailTable in detailTables) 
			{
				if (journeyDetailTable != null) 
				{
					switch (journeyDetailTable.DetailType)
					{
						case EmailJourneyDetailType.Summary:
							columnWidths = summaryColumnWidths;
							break;
						case EmailJourneyDetailType.Public:
							columnWidths = publicColumnWidths;
							break;
						case EmailJourneyDetailType.Road:
							columnWidths = roadColumnWidths;
							break;
						default:
							break;
					}

					table = journeyDetailTable.Details;
					lineBreak = journeyDetailTable.LineBreak;

					for (int rowCount = 0; rowCount < table.Count; rowCount++) 
					{
						object[] row = (object[])table[rowCount];

						// Boilerplate isn't formatted into columns, it just exists as a single string in the first element of the row
						if (journeyDetailTable.DetailType == EmailJourneyDetailType.Boilerplate)
						{
							builder.Append(row[0].ToString());
						}
						else
						{
							for (int columnCount = 0; columnCount < columnWidths.Length; columnCount++) 
							{
								if (row[columnCount] != null) 
								{
									builder.Append(row[columnCount].ToString().PadRight(columnWidths[columnCount] + 2,' '));
								} 
								else 
								{
									builder.Append(String.Empty.PadRight(columnWidths[columnCount] + 2,' '));
								}
							}

							// If line break specified, insert break every n instructions 
							// but not if last row
							if ((lineBreak != 0) && ((rowCount+1) % lineBreak == 0) && (rowCount != table.Count-1)) 
							{
								builder.Append("\n");
							}
							builder.Append("\n");
						}
					}
				}
            }

            #endregion

            return builder.ToString();

        }

        #endregion

        #region Journey Details

        /// <summary>
		/// Returns the road journey detail breakdown
		/// </summary>
		/// <param name="outward">True if for an outward journey, false for a return journey</param>
		/// <param name="journeyResult">The JourneyResult that contains the desired car journey</param>
		/// <param name="journeyViewState">The JourneyViewState that contains state information for the desired car journey</param>
		/// <returns>Formatted table of journey instructions including header</returns>
		private EmailJourneyDetailTable assembleRoadDetails(bool outward, TDJourneyResult journeyResult, TDJourneyViewState journeyViewState, RoadUnitsEnum roadUnits)
		{
			CarJourneyDetailFormatter detailFormatter = new EmailCarJourneyDetailFormatter(
				journeyResult,
				journeyViewState,
				outward,
				TDCultureInfo.CurrentUICulture,
				roadUnits, false
				);

			ArrayList table = new ArrayList();
			string[] row = detailFormatter.GetDetailHeadings();
			table.Add(row);

			IList instructions = detailFormatter.GetJourneyDetails();

			foreach (object[] details in instructions) 
			{
				table.Add(details);
			}

			return new EmailJourneyDetailTable(EmailJourneyDetailType.Road, table, 5);
		}

        /// <summary>
        /// Returns the cycle journey detail breakdown
        /// </summary>
        /// <param name="outward">True if for an outward journey, false for a return journey</param>
        /// <param name="journeyResult">The CycleResult that contains the desired cycle journey</param>
        /// <param name="journeyViewState">The JourneyViewState that contains state information for the desired car journey</param>
        /// <returns>Formatted table of journey instructions including header</returns>
        private EmailJourneyDetailTable assembleCycleDetails(bool outward, CycleJourney cycleJourney, TDJourneyViewState journeyViewState, RoadUnitsEnum roadUnits)
        {
            CycleJourneyDetailFormatter detailFormatter = new EmailCycleJourneyDetailFormatter(
                cycleJourney,
                journeyViewState,
                outward,
                roadUnits,
                false,
                true);
            
            ArrayList table = new ArrayList();
            string[] row = detailFormatter.GetDetailHeadings();
            table.Add(row);

            IList instructions = detailFormatter.GetJourneyDetails();

            foreach (object[] details in instructions)
            {
                table.Add(details);
            }

            return new EmailJourneyDetailTable(EmailJourneyDetailType.Road, table, 5);
        }

		/// <summary>
		/// Returns the public journey details breakdown
		/// </summary>
		/// <param name="line">JourneyControl.PublicJourney</param>
		/// <returns>formatted table containing journey details</returns>
		private EmailJourneyDetailTable assemblePublicDetails(JourneyControl.PublicJourney line) 
		{
			PublicJourneyDetail[] journeyDetails = line.Details;

			ArrayList table = new ArrayList();
			string[] row = new string[6];

			row[0] = emailFrom;
			row[1] = emailDepart;
			row[2] = emailTo;
			row[3] = emailArrive;
			row[4] = emailTransport;
			row[5] = emailDuration;

			table.Add(row);

			for(int count = 0; count < journeyDetails.Length; count++)
			{
				PublicJourneyDetail journeyDetail = journeyDetails[count];
				row = new String[6];
				row[0] = journeyDetail.LegStart.Location.Description;
				row[2] = journeyDetail.LegEnd.Location.Description;

				// Don't show arrival and departure times for frequency based legs or
				// walk modes although always show start and end journey times.

				if (journeyDetail is PublicJourneyFrequencyDetail 
					|| journeyDetail.Mode == ModeType.Walk)
				{
					if (count == 0) 
					{
						row[1] = FormatTDDateTime(journeyDetail.LegStart.DepartureDateTime);
					} 
					else 
					{
						row[1] = "-";
					}
					if (count == journeyDetails.Length - 1) 
					{
						row[3] = FormatTDDateTime(journeyDetail.LegEnd.ArrivalDateTime);
					} 
					else 
					{
						row[3] = "-";
					}
				} 
				else 
				{
					row[1] = FormatTDDateTime(journeyDetail.LegStart.DepartureDateTime); 
					row[3] = FormatTDDateTime(journeyDetail.LegEnd.ArrivalDateTime);
				}

				string resourceManagerKey = "TransportMode." +
					journeyDetail.Mode.ToString();
				row[4] = GetResource(
					resourceManagerKey);				

				// Durations for frequency based legs are stored in minutes so
				// make appropriate adjustement

				if (journeyDetail is PublicJourneyFrequencyDetail) 
				{
					row[5] = GetDuration(new TimeSpan(0,journeyDetail.Duration,0)); 
				} 
				else 
				{
					row[5] = GetDuration(new TimeSpan(0,0,journeyDetail.Duration)); 
				}
				//Begin new insert for IR2126 - add bus number to journey detail
				string serviceInformation = String.Empty;
				if (journeyDetail.Services.Length > 0) 
				{
					// there is some service information - NB this exists for walk legs too.
					for (int i=0; i<journeyDetail.Services.Length;i++)
					{
						
						if ((journeyDetail.Services[i].OperatorName != null) && (journeyDetail.Services[i].OperatorName.Length > 0) && (journeyDetail.Services[i].ServiceNumber != null) && (journeyDetail.Services[i].ServiceNumber.Length > 0)) 
						{
							serviceInformation = serviceInformation + journeyDetail.Services[i].OperatorName + "/" + journeyDetail.Services[i].ServiceNumber + " ";
						}
					}
					if (serviceInformation != String.Empty) 
					{
						row[4] = row[4] + ": " + serviceInformation;
					}
				}
				// end IR 2126

				table.Add(row);
			}

			return new EmailJourneyDetailTable(EmailJourneyDetailType.Public, table, 0);
        }

        #endregion

        #region Introduction

        /// <summary>
		/// Generates email introductory text for
		/// Visit Planner results
		/// </summary>
		/// <returns>Introductory text</returns>
		private string generateVisitPlannerIntro() 
		{
			//the maximum number of journeys possible for Visit planner is 3
			// (1)	Origin - First Location
			// (2)	First location - Seconf Location
			// (3) Second Location - Origin
			const int MAX_SEGMENTS = 3;
 
			TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;
			
		
			StringBuilder builder = new StringBuilder();

			builder.Append("\n" + emailIntro + "\n\n");

			// Build up date
			StringBuilder visitDateTime = new StringBuilder();
			visitDateTime.Append(parameters.OutwardDayOfMonth);
			visitDateTime.Append(space);
			visitDateTime.Append(parameters.OutwardMonthYear);
			visitDateTime.Append(space);
			visitDateTime.Append(parameters.OutwardHour);
			visitDateTime.Append(":");
			visitDateTime.Append(parameters.OutwardMinute);
			TDDateTime date = TDDateTime.Parse(visitDateTime.ToString(), CultureInfo.CurrentCulture);

			builder.Append(emailSummaryJourneys);
			builder.Append(space);
			builder.Append(DisplayFormatAdapter.StandardDateFormat(date));
			builder.Append(space);
			builder.Append(emailLabelLeavingAfter);
			builder.Append(space);
			builder.Append(DisplayFormatAdapter.StandardTimeFormat(date));
			builder.Append('\n');

			for (int i=0; i<TDItineraryManager.Current.Length; i++)
			{
				builder.Append(emailJourneyFrom);
				builder.Append(space);
				builder.Append(parameters.GetLocation(i).Description);
				builder.Append(space);
				builder.Append(emailJourneyTo);
				builder.Append(space);

				//format the text differently if this is the return segment
				if (i == MAX_SEGMENTS - 1)
				{
					builder.Append(parameters.GetLocation(0).Description);
				}
				else
				{
					builder.Append(parameters.GetLocation(i+1).Description);
					builder.Append(comma);
					builder.Append(space);
					builder.Append(emailLengthOfStay);
					builder.Append(space);
					builder.Append(string.Format(TDCultureInfo.CurrentCulture, emailHours, (parameters.GetStayDuration(i) / 60) ));
				}
				
				builder.Append('\n');
			}

			builder.Append(separatorLine);	

			return builder.ToString();
		}

        /// <summary>
        /// Generates email introductory text for Cycle Planner
        /// </summary>
        /// <returns>Introductory text</returns>
        private string generateCyclePlannerIntro()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("\n" + emailIntro + "\n\n");

            ResultsAdapter resultAdapater = new ResultsAdapter();
            string landingPageUrl = resultAdapater.GenerateLandingPageUrl("Email");
            if (!string.IsNullOrEmpty(landingPageUrl))
            {
                builder.Append("\n" + emailPlanLinkText + "\n");
                builder.Append(landingPageUrl + "\n\n");
            }

            ITDCyclePlannerRequest journeyRequest = TDSessionManager.Current.CycleRequest;

            builder.Append(emailJourneyFrom + " ");
            builder.Append(journeyRequest.OriginLocation.Description);
            builder.Append(" " + emailJourneyTo + " ");
            builder.Append(journeyRequest.DestinationLocation.Description);


            builder.Append("\n");
            builder.Append(separatorLine);
            builder.Append('\n');

            return builder.ToString();
        }
		
		/// <summary>
		/// Generates email introductory text
		/// </summary>
		/// <returns>Introductory text</returns>
		private string generateIntro() 
		{
			StringBuilder builder = new StringBuilder();

			builder.Append("\n" + emailIntro + "\n\n");

            ResultsAdapter resultAdapater = new ResultsAdapter();
            string landingPageUrl = resultAdapater.GenerateLandingPageUrl("Email");
            if (!string.IsNullOrEmpty(landingPageUrl))
            {
                builder.Append("\n" + emailPlanLinkText + "\n");
                builder.Append(landingPageUrl + "\n\n");
            }

			if (showItinerary)
			{
				if (TDItineraryManager.Current.OutwardExists)
				{
					builder.Append(emailOutwardJourneyFrom + " ");
					builder.Append(TDItineraryManager.Current.OutwardDepartLocation().Description);
					builder.Append(" " + emailJourneyTo + " ");
					builder.Append(TDItineraryManager.Current.OutwardArriveLocation().Description);
				}
				
				if (TDItineraryManager.Current.ReturnExists)
				{
					if (outwardExists)
					{
						builder.Append("\n");
					}
					builder.Append(emailReturnJourneyFrom + " ");
					builder.Append(TDItineraryManager.Current.ReturnDepartLocation().Description);
					builder.Append(" " + emailJourneyTo + " ");
					builder.Append(TDItineraryManager.Current.ReturnArriveLocation().Description);
				}
			}
			else
			{
				ITDJourneyRequest journeyRequest = TDItineraryManager.Current.JourneyRequest;

				builder.Append(emailJourneyFrom + " ");
				builder.Append(journeyRequest.OriginLocation.Description);
				builder.Append(" " + emailJourneyTo + " ");
				builder.Append(journeyRequest.DestinationLocation.Description);
			}

			builder.Append("\n");
			builder.Append(separatorLine);	
			builder.Append('\n');

			return builder.ToString();

        }

        #endregion

        #region Summary

        /// <summary>
		/// Generates a formatted table containing journey summaries for 
		/// outbound or return journeys.
		/// </summary>
		/// <param name="outward">True if outbound summary required, false if return required</param>
		/// <param name="selectedSummaryLine">The journey currently selected by the user</param>
		/// <param name="selectedID">The id of the journey currently selected by the user</param>
		/// <returns>formatted table containing journey summaries for 
		/// outbound or return journeys</returns>
		private EmailJourneyDetailTable generateSummary(bool outward, out JourneySummaryLine selectedSummaryLine, out int selectedID) 
		{
            selectedSummaryLine = null;
			selectedID = -1;

			ArrayList table = new ArrayList();
			string[] row;
			int rowColumn = 0;

			JourneySummaryLine[] journeySummary = null;

			TDItineraryManager itineraryManager = TDItineraryManager.Current;
            ITDSessionManager sessionManager = TDSessionManager.Current;

            bool cyclePlannerResults = (sessionManager.FindAMode == FindAMode.Cycle);

			if (showItinerary)
			{
				journeySummary = itineraryManager.FullItinerarySummary();
			}
			else
			{
				if(outward)
				{
					journeySummary = (cyclePlannerResults) ?
                        sessionManager.CycleResult.OutwardJourneySummary(outwardArriveBefore, new ModeType[] { ModeType.Cycle })
                        : itineraryManager.JourneyResult.OutwardJourneySummary(outwardArriveBefore);
					
                    selectedID = (cyclePlannerResults) ?
                        sessionManager.JourneyViewState.SelectedOutwardJourneyID
                        : itineraryManager.JourneyViewState.SelectedOutwardJourneyID;
				}
				else
				{
					journeySummary = (cyclePlannerResults) ?
                        sessionManager.CycleResult.ReturnJourneySummary(returnArriveBefore, new ModeType[] { ModeType.Cycle })
                        : itineraryManager.JourneyResult.ReturnJourneySummary(returnArriveBefore);
					
                    selectedID = (cyclePlannerResults) ?
                        sessionManager.JourneyViewState.SelectedReturnJourneyID
                        : itineraryManager.JourneyViewState.SelectedReturnJourneyID;
				}
			}

			// Generate table containing details of each journey

			foreach (JourneySummaryLine summaryLine in journeySummary) 
			{
				rowColumn = 0;

				if (emailShowLocations)
				{
					row = new string[8];
				}
				else
				{
					row = new string[6];
				}

				// generate column for journey number and
				// highlight selected journey
				if (showItinerary)
				{
					// Full Itinerary summary row
					row[0] = GetResource("SummaryItineraryTable.fullItineraryRow");
				}
				else if (summaryLine.JourneyIndex == selectedID)
				{
					selectedSummaryLine = summaryLine;
					row[0] = '*' + (summaryLine.DisplayNumber.ToString(
						System.Globalization.CultureInfo.InvariantCulture)+ ":");
				}
				else
				{
					row[0] = summaryLine.DisplayNumber.ToString(
						System.Globalization.CultureInfo.InvariantCulture)+ ":";
				}

				if (emailShowLocations)
				{
					row[1] = summaryLine.OriginDescription;
					row[2] = summaryLine.DestinationDescription;
					rowColumn = 2;
				}

				// generate column for modes
				row[++rowColumn] = GetModes(summaryLine);

				// generate column for number of changes
				if ((summaryLine.Type != TDJourneyType.RoadCongested)
                    && (summaryLine.Type != TDJourneyType.Cycle))
				{
					// Public Transport or full Itinerary Summary
					row[++rowColumn] = (emailChanges+ ": " + summaryLine.InterchangeCount.ToString(
						System.Globalization.CultureInfo.InvariantCulture));
				}
				else if (showItinerary || !showFindA)
				{
					row[++rowColumn] = String.Empty;
				}

				// generate columns for arrival and departure times
				row[++rowColumn]= emailLeave + ": "  + FormatTDDateTime(summaryLine.DepartureDateTime); 
				row[++rowColumn] = emailArrive +": " + FormatTDDateTime(summaryLine.ArrivalDateTime); 

				// generate column for duration
				// If journey type is by car add mileage

				if ((summaryLine.Type == TDJourneyType.RoadCongested) ||
                    (summaryLine.Type == TDJourneyType.Cycle))
				{
					row[++rowColumn] = emailDuration +": "+ GetDuration(summaryLine.ArrivalDateTime, 
						summaryLine.DepartureDateTime) + "/"+ GetRoadDistance(summaryLine.RoadMiles);
				} 
				else 
				{
					row[++rowColumn] = emailDuration +": "+ GetDuration(summaryLine.ArrivalDateTime, 
						summaryLine.DepartureDateTime);
				}

				table.Add(row);

			}

			return new EmailJourneyDetailTable(EmailJourneyDetailType.Summary, table, 0);
        }

        #endregion

        #region Mail Body

        /// <summary>
		/// Generates the contents of the mail message, comprising of
		/// introduction, summary of outward journeys, details of selected
		/// outward journey. If return journeys were planned, also output
		/// summary of return journeys and details of selected return journey.
		/// </summary>
		/// <returns>Mail message contents</returns>
		private string generateMailBody() 
		{
			int selectedID = -1;
			JourneySummaryLine selectedSummaryLine = null;
			StringBuilder builder = new StringBuilder();

            ITDSessionManager sessionManager = TDSessionManager.Current;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			bool visitPlannerResults = (sessionManager.ItineraryMode == ItineraryManagerMode.VisitPlanner);
            bool cyclePlannerResults = (sessionManager.FindAMode == FindAMode.Cycle);

			ITDJourneyRequest journeyRequest = itineraryManager.JourneyRequest;
			ITDJourneyResult result = itineraryManager.JourneyResult;
			RoadUnitsEnum emailRoadUnits = TDSessionManager.Current.InputPageState.Units;

            ITDCyclePlannerRequest cycleRequest = sessionManager.CycleRequest;
            ITDCyclePlannerResult cycleResult = sessionManager.CycleResult;

			// IR4270: Congestion Charge - Same day return journey shows charge of £0
			// The fix for this IR means we need to control the state of the CongestionChargeAdded flag
			// Must reset so when the Car Details are being created, the Congestion Charge is
			// added correctly for the Outward or Return on the same day.
			// This is not a nice solution and when the Car Deatils code is refactored, this should be looked at.
			TDSessionManager.Current.JourneyViewState.CongestionCostAdded = false;

			emailJourneyDetails = new ArrayList();
			EmailJourneyDetailTable separatorLineDetail = new EmailJourneyDetailTable(separatorLine);

			// If dealing with VisitPlanner results
			if (visitPlannerResults)
            {
                #region VisitPlanner
                // Visit Planner specific Intro 
				builder.Append(generateVisitPlannerIntro());

				emailJourneyDetails.Add(new EmailJourneyDetailTable(builder.ToString()));

				TDJourneyResult journeyResult;
				TDJourneyViewState journeyViewState;
				JourneyControl.PublicJourney pubJourneyDetails;

				int itineraryLength = itineraryManager.Length;

				for (int i=0; i<itineraryLength; i++)
				{
					journeyResult = (TDJourneyResult)itineraryManager.SpecificJourneyResult(i);
					journeyViewState = itineraryManager.SpecificJourneyViewState(i);

					// generate details for outward Itinerary journeys
					emailJourneyDetails.Add(new EmailJourneyDetailTable("\n" + String.Format(TDCultureInfo.CurrentUICulture, emailDetailsJourney, i+1) + "\n\n"));
					pubJourneyDetails = journeyResult.OutwardPublicJourney(journeyViewState.SelectedOutwardJourneyID);
					emailJourneyDetails.Add(assemblePublicDetails(pubJourneyDetails));

					emailJourneyDetails.Add(separatorLineDetail);
                }
                #endregion
            }
            else if (cyclePlannerResults)
            {
                #region CyclePlanner
                // Add introductory text
                builder.Append(generateCyclePlannerIntro());

                // date and time of outward journey travel
                TDDateTime outwardDateTime = cycleRequest.OutwardDateTime[0];
                builder.Append(emailSummaryOutward + " " + outwardDateTime.ToString("dddd dd MMMM yyyy"));

                if (outwardArriveBefore)
                {
                    builder.Append(" " + emailLabelArrivingBefore + " ");
                }
                else
                {
                    builder.Append(" " + emailLabelLeavingAfter + " ");
                }

                builder.Append(outwardDateTime.ToString("HH:mm"));
                builder.Append("\n\n");

                emailJourneyDetails.Add(new EmailJourneyDetailTable(builder.ToString()));

                builder = new StringBuilder();

                // generate summary for outward journeys
                emailJourneyDetails.Add(generateSummary(true, out selectedSummaryLine, out selectedID));
                emailJourneyDetails.Add(separatorLineDetail);

                // generate details for selected outward journey
                emailJourneyDetails.Add(new EmailJourneyDetailTable("\n" + emailDetailsOutward + "\n\n"));
                emailJourneyDetails.Add(assembleCycleDetails(true, cycleResult.OutwardCycleJourney(), sessionManager.JourneyViewState, emailRoadUnits));

                emailJourneyDetails.Add(separatorLineDetail);
                #endregion
            }
            else if (outwardExists)
            {
                #region PT/Car
                // Add introductory text
                builder.Append(generateIntro());

                // date and time of outward journey travel
                if (showItinerary)
                {
                    if (!itineraryManager.OutwardMultipleDates)
                    {
                        builder.Append(emailSummaryOutward + " " + itineraryManager.OutwardDepartDateTime().ToString("dddd dd MMMM yyyy"));
                    }
                    else
                    {
                        builder.Append(emailSummaryOutwardNoDate);
                    }
                }
                else
                {
                    TDDateTime outwardDateTime = journeyRequest.OutwardDateTime[0];
                    builder.Append(emailSummaryOutward + " " + outwardDateTime.ToString("dddd dd MMMM yyyy"));

                    if (outwardArriveBefore)
                    {
                        builder.Append(" " + emailLabelArrivingBefore + " ");
                    }
                    else
                    {
                        builder.Append(" " + emailLabelLeavingAfter + " ");
                    }

                    builder.Append(outwardDateTime.ToString("HH:mm"));
                }

                builder.Append("\n\n");

                emailJourneyDetails.Add(new EmailJourneyDetailTable(builder.ToString()));

                builder = new StringBuilder();

                // generate summary for outward journeys
                emailJourneyDetails.Add(generateSummary(true, out selectedSummaryLine, out selectedID));
                emailJourneyDetails.Add(separatorLineDetail);

                if (!itineraryExists)
                {
                    TDJourneyResult journeyResult = (TDJourneyResult)itineraryManager.JourneyResult;
                    TDJourneyViewState journeyViewState = itineraryManager.JourneyViewState;

                    // generate details for selected outward journey
                    emailJourneyDetails.Add(new EmailJourneyDetailTable("\n" + emailDetailsOutward + "\n\n"));

                    // check for public or private journey
                    if (selectedSummaryLine.Type == TDJourneyType.RoadCongested)
                    {
                        emailJourneyDetails.Add(assembleRoadDetails(true, journeyResult, journeyViewState, emailRoadUnits));
                    }
                    else // public journey
                    {
                        JourneyControl.PublicJourney pubJourneyDetails = result.OutwardPublicJourney(selectedID);
                        emailJourneyDetails.Add(assemblePublicDetails(pubJourneyDetails));
                    }

                    emailJourneyDetails.Add(separatorLineDetail);
                }
                else
                {
                    TDJourneyResult journeyResult;
                    TDJourneyViewState journeyViewState;
                    JourneyControl.PublicJourney pubJourneyDetails;


                    int itineraryLength = itineraryManager.Length;

                    for (int i = 0; i < itineraryLength; i++)
                    {
                        journeyResult = (TDJourneyResult)itineraryManager.SpecificJourneyResult(i);
                        journeyViewState = itineraryManager.SpecificJourneyViewState(i);

                        // generate details for outward Itinerary journeys
                        emailJourneyDetails.Add(new EmailJourneyDetailTable("\n" + String.Format(TDCultureInfo.CurrentUICulture, emailItineraryDetailsOutward, i + 1) + "\n\n"));

                        if (journeyViewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested)
                        {
                            emailJourneyDetails.Add(assembleRoadDetails(true, journeyResult, journeyViewState, emailRoadUnits));
                        }
                        else
                        {
                            pubJourneyDetails = journeyResult.OutwardPublicJourney(journeyViewState.SelectedOutwardJourneyID);
                            emailJourneyDetails.Add(assemblePublicDetails(pubJourneyDetails));
                        }

                        emailJourneyDetails.Add(separatorLineDetail);
                    }
                }
                #endregion
            }

			
			// if there's a return journey, add the summary and details
			// (VisitPlanner never has return journeys, so no specific processing here)
			if (returnExists)
			{
				if (cyclePlannerResults)
                {
                    #region CyclePlanner
                    if (!outwardExists)
                    {
                        // Add introductory text
                        builder.Append(generateCyclePlannerIntro());
                    }

                    // date and time of outward journey travel
                    TDDateTime returnDateTime = cycleRequest.ReturnDateTime[0];
                    builder.Append(emailSummaryReturn + " " + returnDateTime.ToString("dddd dd MMMM yyyy"));

                    if (returnArriveBefore)
                    {
                        builder.Append(" " + emailLabelArrivingBefore + " ");
                    }
                    else
                    {
                        builder.Append(" " + emailLabelLeavingAfter + " ");
                    }

                    builder.Append(returnDateTime.ToString("HH:mm"));
                    builder.Append("\n\n");

                    emailJourneyDetails.Add(new EmailJourneyDetailTable(builder.ToString()));

                    builder = new StringBuilder();

                    // generate summary for outward journeys
                    emailJourneyDetails.Add(generateSummary(false, out selectedSummaryLine, out selectedID));
                    emailJourneyDetails.Add(separatorLineDetail);

                    // generate details for selected outward journey
                    emailJourneyDetails.Add(new EmailJourneyDetailTable("\n" + emailDetailsOutward + "\n\n"));
                    emailJourneyDetails.Add(assembleCycleDetails(false, cycleResult.ReturnCycleJourney(), sessionManager.JourneyViewState, emailRoadUnits));

                    emailJourneyDetails.Add(separatorLineDetail);
                    #endregion
                }
                else
                {
                    #region PT/Car
                    if (!outwardExists)
                    {
                        // Add introductory text
                        builder.Append(generateIntro());
                    }

                    // date and time of outward journey travel
                    if (showItinerary)
                    {
                        if (!itineraryManager.ReturnMultipleDates)
                        {
                            if (outwardExists)
                            {
                                builder.Append("\n");
                            }

                            builder.Append(emailSummaryReturn + " " + itineraryManager.ReturnDepartDateTime().ToString("dddd dd MMMM yyyy"));
                        }
                        else
                        {
                            if (outwardExists)
                            {
                                builder.Append("\n");
                            }

                            builder.Append(emailSummaryReturnNoDate);
                        }
                    }
                    else
                    {
                        TDDateTime returnDateTime = journeyRequest.ReturnDateTime[0];

                        if (outwardExists)
                        {
                            builder.Append("\n");
                        }

                        builder.Append(emailSummaryReturn + " " + returnDateTime.ToString("dddd dd MMMM yyyy"));

                        if (returnArriveBefore)
                        {
                            builder.Append(" " + emailLabelArrivingBefore + " ");
                        }
                        else
                        {
                            builder.Append(" " + emailLabelLeavingAfter + " ");
                        }

                        builder.Append(returnDateTime.ToString("HH:mm"));
                    }

                    builder.Append("\n\n");

                    emailJourneyDetails.Add(new EmailJourneyDetailTable(builder.ToString()));

                    builder = new StringBuilder();

                    // generate summary for return journeys
                    if (!showItinerary)
                    {
                        emailJourneyDetails.Add(generateSummary(false, out selectedSummaryLine, out selectedID));
                    }

                    emailJourneyDetails.Add(separatorLineDetail);

                    if (!itineraryExists)
                    {
                        TDJourneyResult journeyResult = (TDJourneyResult)itineraryManager.JourneyResult;
                        TDJourneyViewState journeyViewState = itineraryManager.JourneyViewState;

                        // generate details for selected return journey									
                        emailJourneyDetails.Add(new EmailJourneyDetailTable("\n" + emailDetailsReturn + "\n\n"));

                        // check for public or private journey
                        if (selectedSummaryLine.Type == TDJourneyType.RoadCongested)
                        {
                            emailJourneyDetails.Add(assembleRoadDetails(false, journeyResult, journeyViewState, emailRoadUnits));
                        }
                        else // public journey
                        {
                            JourneyControl.PublicJourney pubJourneyDetails = result.ReturnPublicJourney(selectedID);
                            emailJourneyDetails.Add(assemblePublicDetails(pubJourneyDetails));
                        }

                        emailJourneyDetails.Add(separatorLineDetail);
                    }
                    else
                    {
                        TDJourneyResult journeyResult;
                        TDJourneyViewState journeyViewState;
                        JourneyControl.PublicJourney pubJourneyDetails;

                        int itineraryLength = itineraryManager.Length;

                        for (int i = itineraryLength - 1; i > -1; i--)
                        {
                            journeyResult = (TDJourneyResult)itineraryManager.SpecificJourneyResult(i);
                            journeyViewState = itineraryManager.SpecificJourneyViewState(i);

                            // generate details for return Itinerary journeys
                            emailJourneyDetails.Add(new EmailJourneyDetailTable("\n" + String.Format(TDCultureInfo.CurrentUICulture, emailItineraryDetailsReturn, itineraryLength - i) + "\n\n"));

                            if (journeyViewState.SelectedReturnJourneyType == TDJourneyType.RoadCongested)
                            {
                                emailJourneyDetails.Add(assembleRoadDetails(false, journeyResult, journeyViewState, emailRoadUnits));
                            }
                            else
                            {
                                pubJourneyDetails = journeyResult.ReturnPublicJourney(journeyViewState.SelectedReturnJourneyID);
                                emailJourneyDetails.Add(assemblePublicDetails(pubJourneyDetails));
                            }

                            emailJourneyDetails.Add(separatorLineDetail);
                        }
                    }

                    #endregion
                }
			}

			return formatTable(emailJourneyDetails);
        }

        #endregion
        
        #endregion

        #region Public Properties - Control Visibility

        /// <summary>
		/// Get the visibility of the tab for the Amend date/time control
		/// </summary>
		public bool AmendSaveSendAmendDateTimeTabVisible
		{
			get { return imageButtonTabAmendDateTime.Visible; }
		}

		/// <summary>
		/// Get the visibility of the tab for the Stopover control
		/// </summary>
		public bool AmendStopoverTabVisible
		{
			get { return imageButtonTabAmendStopover.Visible; }
		}

		/// <summary>
		/// Get the visibility of the tab for the amend fare detail control
		/// </summary>
		public bool AmendFareDetailTabVisible
		{
			get { return imageButtonTabFareDetail.Visible; }
		}

		/// <summary>
		/// Get the visibility of the tab for the amend view control
		/// </summary>
		public bool AmendViewTabVisible
		{
			get { return imageButtonTabAmendView.Visible; }
		}

		/// <summary>
		/// Get the visibility of the tab for the amend cost search date control
		/// </summary>
		public bool AmendCostSearchDateTabVisible
		{
			get { return imageButtonTabAmendCostSearchDate.Visible; }
		}

		/// <summary>
		/// Get the visibility of the tab for the amend Car details control
		/// </summary>
		public bool AmendCarDetailsTabVisible
		{
			get { return imageButtonTabAmendCarDetails.Visible;}
        }

        #endregion

        #region Public Properties - Get Controls

        /// <summary>
        /// Read only property returning the nested AmendFaresControl
		/// </summary>
		public AmendFaresControl AmendFaresControl
		{
			get { return theAmendFaresControl; }
		}

		/// Read only property returning the nested AmendViewControl
		/// </summary>
		public AmendViewControl AmendViewControl
		{
			get { return theAmendViewControl; }
		}

		/// <summary>
		/// Read only property returning the nested AmendCostSearchDateControl
		/// </summary>
		public AmendCostSearchDateControl AmendCostSearchDateControl
		{
			get { return theAmendCostSearchDateControl; }
		}

		/// <summary>
		/// Read only property returning the nested AmendCarDetailsControl
		/// </summary>
		public AmendCarDetailsControl AmendCarDetailsControl
		{
			get { return theAmendCarDetailsControl;}
		}

        /// <summary>
		/// Read only property returning the nested AmendCostSearchDayControl
		/// </summary>
		public AmendCostSearchDayControl AmendCostSearchDayControl 
		{
			get { return theAmendCostSearchDayControl;}
		}

        /// <summary>
        /// Read only property returning the imageButtonTabSendEmail image button
        /// </summary>
        public ImageButton SendEmailTabButton
        {
            get { return imageButtonTabSendEmail; }
        }

		/// <summary>
		/// Read/write property to hide the Help button
		/// </summary>
		public bool HideHelpButton
		{
			get {return hideHelpButton;}
			set {hideHelpButton = value;}
        }

        #endregion
    }
}
