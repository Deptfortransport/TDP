//******************************************************************************
//NAME			: ExtendJourneyOptionsControl.ascx
//AUTHOR		: Andrew Sinclair
//DATE CREATED	: 15/12/2005
//DESCRIPTION	: Control that asks the user which direction and using which modes
// they would like to extend their journey in.   
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ExtendJourneyOptionsControl.ascx.cs-arc  $ 
//
//   Rev 1.8   Dec 11 2012 11:58:52   mmodi
//Hide extend options for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Jul 28 2011 16:18:54   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.6   Oct 27 2010 09:10:14   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.5   Jan 06 2009 15:27:00   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   Oct 14 2008 12:00:14   mmodi
//Manual merge for stream5014
//
//   Rev 1.3   Jul 01 2008 15:00:34   rbroddle
//Added code to set extendImage.AlternateText 
//Resolution for 5016: WAI WCAG level A compliance faults - Missing Alt text
//
//   Rev 1.2.1.0   Jul 30 2008 11:14:56   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:19:58   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:12   mturner
//Initial revision.
//
//   Rev 1.8   Mar 30 2006 19:46:12   asinclair
//Fix to set the location text correctly
//Resolution for 3658: Extend, Replan & Adjust: extension options - airports
//
//   Rev 1.7   Mar 30 2006 18:16:22   pcross
//Updated to allow whole of extend journey option to be shown hidden as part of this control. Plus added logic to handle when extend permitted (dependent on current time)
//Resolution for 3704: DN068 Extend: Allowed to add a 2nd extension to start despite 1st extension being planned in the past
//
//   Rev 1.6   Mar 21 2006 11:36:20   asinclair
//Actioned Code Review comments
//
//   Rev 1.5   Mar 16 2006 11:22:56   asinclair
//Removed unused code
//
//   Rev 1.4   Mar 14 2006 19:49:56   NMoorhouse
//fix stream3353 merge issues
//
//   Rev 1.3   Mar 08 2006 16:02:56   RGriffith
//FxCop Suggested Changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 21 2006 13:55:48   asinclair
//Updated, added code comments fix to allow extended journeys to be extended again
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 16 2006 18:49:28   asinclair
//Added property to access radiobuttons
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Jan 13 2006 14:34:34   asinclair
//Initial revision.
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.JourneyControl;

	/// <summary>
	///		Summary description for ExtendJourneyOptionsControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class ExtendJourneyOptionsControl :  TDUserControl
	{


		private string outwardOriginLocationDescription;
		private string outwardDestinationLocationDescription;
		private string returnOriginLocationDescription;
		private string returnDestinationLocationDescription;

		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;

			ITDSessionManager sessionManager = TDSessionManager.Current;

			labelPromptText.Text = GetResource("ExtendOptionsControl.PromptText");
					
			extendImage.ImageUrl = GetResource("RefineJourney.extendJourney.ImageURL");
            //extendImage.AlternateText = labelPromptText.Text;

		}

		/// <summary>
		/// Prerender event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			SetControlVisibility();
		}

		/// <summary>
		/// Sets visibility of extend journey buttons.
		/// Initial display allows user to find extension from end/to start.
		/// If departure is in past then disallow add to start
		/// If arrival is in past then disallow both add to start and end
		/// </summary>
		private void SetControlVisibility()
		{

			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			// Ensure correct options are displayed

			TDDateTime timeNow = TDDateTime.Now;

			// Get the user-selected journey from the session
			ResultsAdapter resultsAdapter = new ResultsAdapter();
            Journey outwardJourney = resultsAdapter.GetRequiredJourney(sessionManager.JourneyResult, sessionManager.CycleResult, sessionManager.JourneyViewState, false);

			string locationFrom;
			string locationTo;
			
			locationTo = outwardJourney.JourneyLegs[outwardJourney.JourneyLegs.Length - 1].LegEnd.Location.Description;
			locationFrom = outwardJourney.JourneyLegs[0].LegStart.Location.Description;
			
			labelExtendStartPreText.Text = GetResource("ExtendOptionsControl.ExtendStartPreText");
			labelExtendStartPreText2.Text = GetResource("ExtendOptionsControl.ExtendStartPreText");

			labelExtendStartPostText2.Text = locationTo;
			labelExtendStartPostText.Text = locationFrom;

			if (sessionManager.ItineraryMode == ItineraryManagerMode.None)
			{
				// Get the departure time for the journey
				TDDateTime departDateTime = outwardJourney.JourneyLegs[0].StartTime;

				// Get the arrival time for the journey
				TDDateTime arrivalDateTime = outwardJourney.JourneyLegs[outwardJourney.JourneyLegs.Length - 1].EndTime;

				if (departDateTime < timeNow) 
				{
					// Departure in past
					labelExtendStartPreText.Visible = false;
					labelExtendStartPostText.Visible = false;
					scriptableRadioButtonExtendDirection1.Visible = false;
				} 

				if (arrivalDateTime < timeNow)
				{
					// Arrival in past - no options available
					extensionControlTable.Visible = false;
				} 

			}
			else
			{
				if (itineraryManager.OutwardDepartDateTime() < timeNow || (itineraryManager.ReturnLength > 0 && itineraryManager.ReturnArriveDateTime() < timeNow)) 
				{
					// Departure in past
					labelExtendStartPreText.Visible = false;
					labelExtendStartPostText.Visible = false;
					scriptableRadioButtonExtendDirection1.Visible = false;
				} 

				if ( (itineraryManager.OutwardArriveDateTime() < timeNow)
					|| ( (itineraryManager.ReturnLength > 0)
					&& ((itineraryManager.ReturnDepartDateTime() < timeNow) || (itineraryManager.OutwardArriveDateTime() >= itineraryManager.ReturnDepartDateTime()) )
					)
					)
				{
					// Arrival in past - no options available
					extensionControlTable.Visible = false;
				} 
			}

            if (extensionControlTable.Visible)
            {
                RefineHelper refineHelper = new RefineHelper();

                // Don't display for accessible journey
                if (refineHelper.IsAccessibleJourney(outwardJourney))
                {
                    extensionControlTable.Visible = false;
                }
            }
		}

		#region  Properties

		/// <summary>
		///  Gets/Sets the outwardOriginLocationDescription
		/// </summary>		
		public string OutwardOriginLocationDescription
		{
			get {return outwardOriginLocationDescription;}
			set {outwardOriginLocationDescription = value;}
		}

		/// <summary>
		///  Gets/Sets the outwardDestinationLocationDescription
		/// </summary>		
		public string OutwardDestinationLocationDescription
		{
			get {return outwardDestinationLocationDescription;}
			set {outwardDestinationLocationDescription = value;}
		}

		/// <summary>
		///  Gets/Sets the returnOriginLocationDescription
		/// </summary>		
		public string ReturnOriginLocationDescription
		{
			get {return returnOriginLocationDescription;}
			set {returnOriginLocationDescription = value;}
		}

		/// <summary>
		///  Gets/Sets the returnDestinationLocationDescription
		/// </summary>		
		public string ReturnDestinationLocationDescription
		{
			get {return returnDestinationLocationDescription;}
			set {returnDestinationLocationDescription = value;}
		}

		/// <summary>
		///  Gets the  scriptableRadioButtonExtendDirection1
		/// </summary>
		public ScriptableGroupRadioButton ScriptableRadioButtonExtendDirection1
		{
			get {return scriptableRadioButtonExtendDirection1;}
		}

		/// <summary>
		/// Gets the  scriptableRadioButtonExtendDirection2
		/// </summary>
		public ScriptableGroupRadioButton ScriptableRadioButtonExtendDirection2
		{
			get {return scriptableRadioButtonExtendDirection2;}
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion



	}
}
