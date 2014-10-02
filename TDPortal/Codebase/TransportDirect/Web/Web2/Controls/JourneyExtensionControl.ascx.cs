// *********************************************** 
// NAME                 : JourneyExtensionControl.ascx.cs 
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 13.05.04
// DESCRIPTION			: Contains the buttons that allow extensions to be added and
//                        removed from the journey itinerary.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyExtensionControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:21:30   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:36   mturner
//Initial revision.
//
//   Rev 1.13   Feb 23 2006 19:16:50   build
//Automatically merged from branch for stream3129
//
//   Rev 1.12.1.0   Jan 10 2006 15:25:40   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.12   Nov 03 2005 16:10:02   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.11.1.0   Oct 20 2005 15:38:40   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.11   Sep 17 2004 12:11:24   jbroome
//IR1591 - Extend Journey Usability Changes

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
    using System.Collections;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using TransportDirect.Web.Support;
    using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.Common;
    using TransportDirect.UserPortal.JourneyControl;

	/// <summary>
    ///	Contains the buttons that allow extensions to be added and
    /// removed from the journey itinerary.
	/// </summary>
	public partial  class JourneyExtensionControl : TDUserControl
	{

		#region Instance Members
		//protected System.Web.UI.HtmlControls.HtmlGenericControl addExtensionButtonsGroup;
		//protected System.Web.UI.HtmlControls.HtmlGenericControl undoExtensionButtonsGroup;
		//protected System.Web.UI.HtmlControls.HtmlGenericControl undoLastExtensionButtonsGroup;
		//protected System.Web.UI.HtmlControls.HtmlGenericControl backToSummaryButtonsGroup;

		/// <summary>
        /// Button that initiates an extension from the current start location of the journey
        /// </summary>
        protected TransportDirect.UserPortal.Web.Controls.TDButton findTransportToStartButton;

		/// <summary>
        /// Button that initiates an extension from the current end location of the journey
        /// </summary>
        protected TransportDirect.UserPortal.Web.Controls.TDButton findTransportFromEndButton;
        
		/// <summary>
		/// Table that contains all the image buttons, used for layout purposes only.
		/// </summary>
		protected System.Web.UI.HtmlControls.HtmlTable tableExtensionButtons;
		
        private PageId callingPageId = PageId.Empty;
		private bool outward = true;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises this control.
		/// </summary>
		/// <param name="callingPageId"></param>
		public void Initialise(PageId callingPageId)
		{
			Initialise(callingPageId, true);
		}

		/// <summary>
		/// Initialises this control for Outward or Return.
		/// </summary>
		/// <param name="callingPageId"></param>
		public void Initialise(PageId callingPageId, bool outward)
		{
			this.callingPageId = callingPageId;
			this.outward = outward;
		}
		#endregion

		#region ViewState Code
		/// <summary>
		/// Loads the ViewState
		/// </summary>
		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at SavedViewState.
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					callingPageId = (PageId)myState[1]; 

			}
		}
	
		/// <summary>
		/// Overrides the base SaveViewState to customise viestate behaviour.
		/// </summary>
		/// <returns>The ViewState object to be saved.</returns>
		protected override object SaveViewState()
		{ 
			// Save State as a cumulative array of objects.
			object baseState = base.SaveViewState();
		
			object[] allStates = new object[2];
			allStates[0] = baseState;
			allStates[1] = callingPageId;

			return allStates;
		}
		#endregion

		#region Page Load and Pre Render
		/// <summary>
		/// OnLoad method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{

            if ((true == true)) {

                // Initialise buttons with text
				findTransportFromEndButton.Text = GetResource("JourneyExtensionControl.findTransportFromEndButton.Text");
				findTransportToStartButton.Text = GetResource("JourneyExtensionControl.findTransportToStartButton.Text");

				SetControlVisibility();
			}
			
        }

		/// <summary>
		/// OnPreRender method.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			if (TDItineraryManager.Current.ItineraryManagerModeChanged)
			{
				SetControlVisibility();
			}
		}
		#endregion

		#region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
		{
			ExtraWiringEvents();
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
    	/// <summary>
        ///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Sets up the necessary event handlers.
		/// </summary>
		private void ExtraWiringEvents() 
		{
			this.findTransportToStartButton.Click += new EventHandler(this.findTransportToStartButton_Click);
			this.findTransportFromEndButton.Click += new EventHandler(this.findTransportFromEndButton_Click);
		}
		#endregion

		#region Event Handlers
		/// <summary>
        /// Handle button click to initiate an extension from the current start location of the journey
        /// </summary>
        /// <param name="sender">Originator of event</param>
        /// <param name="e">Event parameters</param>
        private void findTransportToStartButton_Click(object sender, EventArgs e)
        {
			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			sessionManager.InputPageState.JourneyInputReturnStack.Push(callingPageId);

			TDDateTime timeNow = TDDateTime.Now;

            sessionManager.ValidationError.ErrorIDs = null;
            sessionManager.ValidationError.MessageIDs = new Hashtable();

            if (itineraryManager.OutwardDepartDateTime() < timeNow)
            {
                sessionManager.ValidationError.ErrorIDs = new ValidationErrorID[] {ValidationErrorID.ExtendToStartOutwardInPast};
                sessionManager.ValidationError.MessageIDs.Add(ValidationErrorID.ExtendToStartOutwardInPast,"ValidateAndRun.CannotExtendToStartOutwardInPast");
            } 
            else if (itineraryManager.ReturnLength > 0 && itineraryManager.ReturnArriveDateTime() < timeNow) 
            {
                sessionManager.ValidationError.ErrorIDs = new ValidationErrorID[] {ValidationErrorID.ExtendToStartReturnInPast};
                sessionManager.ValidationError.MessageIDs.Add(ValidationErrorID.ExtendToStartReturnInPast,"ValidateAndRun.CannotExtendToStartReturnInPast");
            }

            if (sessionManager.ValidationError.ErrorIDs == null) 
            {
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;
				itineraryManager.ExtendToItineraryStartPoint();
				itineraryManager.ExtendedFromInitialResultsPage = false;
			} 
            else 
            {
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputErrors;
            }

	      }

        /// <summary>
        /// Handle button click to initiate an extension from the current end location of the journey
        /// </summary>
        /// <param name="sender">Originator of event</param>
        /// <param name="e">Event parameters</param>
        private void findTransportFromEndButton_Click(object sender, EventArgs e)
        {
			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

            sessionManager.InputPageState.JourneyInputReturnStack.Push(callingPageId);

            TDDateTime timeNow = TDDateTime.Now;

            sessionManager.ValidationError.ErrorIDs = null;
            sessionManager.ValidationError.MessageIDs = new Hashtable();

            if (itineraryManager.OutwardArriveDateTime() < timeNow)
            {
                sessionManager.ValidationError.ErrorIDs = new ValidationErrorID[] {ValidationErrorID.ExtendFromEndOutwardInPast};
                sessionManager.ValidationError.MessageIDs.Add(ValidationErrorID.ExtendFromEndOutwardInPast,"ValidateAndRun.CannotExtendFromEndOutwardInPast");
            } 
            else if (itineraryManager.ReturnLength > 0 && itineraryManager.ReturnDepartDateTime() < timeNow) 
            {
                sessionManager.ValidationError.ErrorIDs = new ValidationErrorID[] {ValidationErrorID.ExtendFromEndReturnInPast};
                sessionManager.ValidationError.MessageIDs.Add(ValidationErrorID.ExtendFromEndReturnInPast,"ValidateAndRun.CannotExtendFromEndReturnInPast");
            }

            if (sessionManager.ValidationError.ErrorIDs == null) 
            {
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;
				itineraryManager.ExtendFromItineraryEndPoint();
				itineraryManager.ExtendedFromInitialResultsPage = false;
			} 
            else 
            {
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputErrors;
            }
        }
		#endregion

		#region SetControlVisibility

		/// <summary>
		/// Sets visibility of extend journey buttons.  Initial display allows
		/// user to find extension from end/to start or to return to initial
		/// journey results.
		/// </summary>
		public void SetControlVisibility() 
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			bool usingItinerary = ((itineraryManager.Length > 0) && (!itineraryManager.ExtendInProgress));

			if (usingItinerary)
			{
				// Ensure correct buttons are displayed

				TDDateTime timeNow = TDDateTime.Now;
				// Buttons are only displayed for the outward control,
				// and only if extensions are permitted by the current Itinerary Manager state.
				if (!outward || !itineraryManager.ExtendPermitted)
				{
					// Hide everything
					tableExtensionButtons.Visible = false;
					return;
				}
				else 
				{
					tableExtensionButtons.Visible = true;
				}

				if ( (itineraryManager.OutwardArriveDateTime() < timeNow)
					|| ( (itineraryManager.ReturnLength > 0)
					&& ((itineraryManager.ReturnDepartDateTime() < timeNow) || (itineraryManager.OutwardArriveDateTime() >= itineraryManager.ReturnDepartDateTime()) )
					)
					)
				{
					findTransportFromEndButton.Visible = false;
				} 
				else 
				{
					findTransportFromEndButton.Visible = true;
				}

				if (itineraryManager.OutwardDepartDateTime() < timeNow || (itineraryManager.ReturnLength > 0 && itineraryManager.ReturnArriveDateTime() < timeNow)) 
				{
					findTransportToStartButton.Visible = false;
				} 
				else 
				{
					findTransportToStartButton.Visible = true;
				}
			}
			else
			{
				// Not using itinerary - hide everything. 
				tableExtensionButtons.Visible = false;	
			}
 		}
		#endregion
    }
}