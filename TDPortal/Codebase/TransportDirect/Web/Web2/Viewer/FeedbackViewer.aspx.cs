// *********************************************** 
// NAME                 : FeedbackViewer.aspx.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 14/01/2006
// DESCRIPTION			: Page to view the Feedback information submitted by users
//						  ** Access restricted to non-standard users **
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Viewer/FeedbackViewer.aspx.cs-arc  $
//
//   Rev 1.4   Jan 16 2009 08:05:58   apatel
//XHTML Compliance changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 18 2008 15:58:28   mmodi
//Corrected manual code merge issue with missing code
//Resolution for 4862: Feedback Viewer does not work
//
//   Rev 1.2   Mar 31 2008 13:27:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.1   Mar 12 2008 13:22:22   build
//Manual merge for Del10.0
//
//   Rev 1.0.1.0   Mar 12 2008 13:21:52   build
//CHanges for .Net2 compliance
//
//   Rev 1.1   Jan 24 2008 11:46:18   build
//Automatically merged from branch for stream4542
//
//   Rev 1.0.1.0   Jan 07 2008 15:00:04   mmodi
//Updated to fix load feedback problems following .NET2 updates
//Resolution for 4542: IR to cover stream for 9.10 Apps Support changes
//
//   Rev 1.0   Nov 08 2007 13:29:16   mturner
//Initial revision.
//
//   Rev 1.2   Jan 22 2007 13:50:28   mmodi
//Updated following enable view state being added to controls on page
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.1   Jan 17 2007 18:13:00   mmodi
//Added controls to display Feedback information
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.0   Jan 14 2007 17:29:04   mmodi
//Initial revision.
//Resolution for 4332: Contact Us Improvements - workstream
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Web.Adapters;

using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for FeedbackViewer.
	/// </summary>
	public partial class FeedbackViewer : TDPage
	{
		#region Controls
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl	headerControl;

		


		#region Feedback information result controls





		#endregion

		#endregion

		#region Private variables

		private ControlPopulator populator;
		private FeedbackHelper fbHelper;

		private Feedback feedbackRecord;

		#endregion

		#region Constructor
		/// <summary>
		/// Constructor, sets the PageId and calls base.
		/// </summary>
		public FeedbackViewer() : base()
		{
			pageId = PageId.FeedbackViewer;
		}

		#endregion

		#region Page_Load

		/// <summary>
		/// Page load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			fbHelper = new FeedbackHelper();

			InitialiseControls();

            #region Load lists
            int listDeleteSelected = listDelete.SelectedIndex;
            int listFeedbackStatusSelected = listFeedbackStatus.SelectedIndex;

			populator.LoadListControl(DataServiceType.UserFeedbackJourneyConfirm, listDelete);
			populator.LoadListControl(DataServiceType.UserFeedbackStatus, listFeedbackStatus);

            listDelete.SelectedIndex = listDeleteSelected;
            listFeedbackStatus.SelectedIndex = listFeedbackStatusSelected;
            #endregion

            // Hide panels
			panelFeedbackDetails.Visible = false;
			panelSaved.Visible = false;
			panelFeedbackError.Visible = false;

			// Set Feedback record to null
			feedbackRecord = null;


			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);

            //Added for white labelling:
            ConfigureLeftMenu("FeedbackViewer.clientLink.BookmarkTitle", "FeedbackViewer.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFeedbackViewer);
            expandableMenuControl.AddExpandedCategory("Related links");
        }

		#endregion

		#region Private methods

		/// <summary>
		/// Method to initialise controls that are on the page.
		/// </summary>
		private void InitialiseControls()
		{			
			// Set up buttons
			buttonSubmit.Text = GetResource("FeedbackViewer.buttonSubmit.Text");
			buttonUpdate.Text = GetResource("FeedbackViewer.buttonUpdate.Text");
			buttonJourneyRequest.Text = GetResource("FeedbackViewer.buttonJourneyRequest.Text");
			buttonJourneyResult.Text = GetResource("FeedbackViewer.buttonJourneyResult.Text");
			buttonItineraryManager.Text = GetResource("FeedbackViewer.buttonItineraryManager.Text");

			// Set up labels
			labelFeedbackViewerTitle.Text = GetResource("FeedbackViewer.FeedbackViewerTitle.Text");
			labelInstruction.Text = GetResource("FeedbackViewer.Instruction.Text");
			labelFeedbackIdInput.Text = GetResource("FeedbackViewer.FeedbackId.Text");
			
			labelSaved.Text = GetResource("FeedbackViewer.FeedbackSaved.Success.Text");
			labelFeedbackError.Text = GetResource("FeedbackViewer.FeedbackError.NotFound.Text");
			labelSessionWarning.Text = GetResource("FeedbackViewer.SessionWarning.Text");
		
			labelFeedbackId.Text = GetResource("FeedbackViewer.FeedbackId.Text");
			labelSessionId.Text = GetResource("FeedbackViewer.SessionId.Text");
			labelSessionCreated.Text = GetResource("FeedbackViewer.SessionCreated.Text");
			labelSessionExpires.Text = GetResource("FeedbackViewer.SessionExpires.Text");
			labelSubmittedTime.Text = GetResource("FeedbackViewer.SubmittedTime.Text");
			labelAcknowledgementSent.Text = GetResource("FeedbackViewer.AcknowledgementSent.Text");
			labelAcknowledgedTime.Text = GetResource("FeedbackViewer.AcknowledgedTime.Text");
			labelEmailAddress.Text = GetResource("FeedbackViewer.EmailAddress.Text");
			labelUserLoggedOn.Text = GetResource("FeedbackViewer.UserLoggedOn.Text");
			labelVantiveId.Text = GetResource("FeedbackViewer.VantiveId.Text");
			labelFeedbackStatus.Text = GetResource("FeedbackViewer.FeedbackStatus.Text");
			labelDelete.Text = GetResource("FeedbackViewer.Delete.Text");
			labelTimeLogged.Text = GetResource("FeedbackViewer.TimeLogged.Text");
			labelJourneyRequest.Text = GetResource("FeedbackViewer.JourneyRequest.Text");
			labelJourneyResult.Text = GetResource("FeedbackViewer.JourneyResult.Text");
			labelItineraryManager.Text = GetResource("FeedbackViewer.ItineraryManager.Text");
			labelUserFeedbackOptions.Text = GetResource("FeedbackViewer.FeedbackOptions.Text");
			labelUserFeedbackDetails.Text = GetResource("FeedbackViewer.FeedbackDetails.Text");

		}

		/// <summary>
		/// Hides the panels used to display Session inforamtion
		/// and sets the text box values to empty
		/// </summary>
		private void HideSessionPanels()
		{
			panelJourneyRequest.Visible = false;
			panelJourneyResult.Visible = false;
			panelItineraryManager.Visible = false;

			// if we're hiding the panels, then also set the text to empty to prevent needless 
			// data being sent over network increasing page load times
			textJourneyRequest.Text = string.Empty;
			textJourneyResult.Text = string.Empty;
			textItineraryManager.Text = string.Empty;
		}

		/// <summary>
		/// Populates text fields on page with information from a Feedback record.
		/// This does NOT populate the Feedback Session information
		/// </summary>
		/// <param name="feedbackRecord"></param>
		private void PopulateFeedback(Feedback feedbackRecord)
		{
			textFeedbackId.Text = feedbackRecord.FeedbackId.ToString();
			textSessionId.Text = feedbackRecord.SessionId;
			textSessionCreated.Text = feedbackRecord.SessionCreated.ToString("dd/MM/yyyy HH:mm:ss");
			textSessionExpires.Text = feedbackRecord.SessionExpires.ToString("dd/MM/yyyy HH:mm:ss");
			textSubmittedTime.Text = feedbackRecord.EmailSubmittedTime.ToString("dd/MM/yyyy HH:mm:ss");

			textAcknowledgementSent.Text = feedbackRecord.AcknowledgementSent.ToString();
			if (feedbackRecord.AcknowledgementSent)
			{
				textAcknowledgedTime.Text = feedbackRecord.EmailAcknowledgedTime.ToString("dd/MM/yyyy HH:mm:ss");
				textEmailAddress.Text = feedbackRecord.Email;
					
				labelAcknowledgedTime.Visible = true;
				textAcknowledgedTime.Visible = true;
				labelEmailAddress.Visible = true;
				textEmailAddress.Visible = true;
			}
			else
			{
				labelAcknowledgedTime.Visible = false;
				textAcknowledgedTime.Visible = false;
				labelEmailAddress.Visible = false;
				textEmailAddress.Visible = false;
			}

			textUserLoggedOn.Text = feedbackRecord.UserLoggedOn.ToString();
			textVantiveId.Text = feedbackRecord.VantiveId;

			// Reset the selected index to default
			listFeedbackStatus.SelectedIndex = 0;

			// Now set the selected index to the item selected in the Feedback record
			foreach (ListItem li in listFeedbackStatus.Items)
			{
				if (li.Text == feedbackRecord.FeedbackStatus.Trim())
				{
					li.Selected = true;
					break;
				}
				else
					li.Selected = false;
			}

			listDelete.SelectedIndex = (feedbackRecord.DeleteFlag ? 0 : 1);
			textTimeLogged.Text = feedbackRecord.TimeLogged.ToString("dd/MM/yyyy HH:mm:ss");

			textUserFeedbackOptions.Text = feedbackRecord.FeedbackOptions;
			textUserFeedbackDetails.Text = feedbackRecord.FeedbackDetails;
		}

        /// <summary>
        /// Loads the feedback record from the feedbackID already searched on, uses textFeedbackId
        /// </summary>
        private void LoadFeedbackRecord()
        {
            LoadFeedbackRecord(Convert.ToInt32(textFeedbackId.Text));
        }

        /// <summary>
        /// Loads the feedback record for a feedback ID
        /// </summary>
        /// <param name="feedbackId"></param>
        private void LoadFeedbackRecord(int feedbackId)
        {
            // Search for Feedback Id in database
            if (feedbackId > 0)
                feedbackRecord = fbHelper.GetFeedback(feedbackId);

            // Populate fields on this page with appropriate session information if found
            if (feedbackRecord != null)
            {
                PopulateFeedback(feedbackRecord);

                panelFeedbackDetails.Visible = true;
                panelSaved.Visible = false;
                panelFeedbackError.Visible = false;
            }
            else
            {   // Show the error panel
                panelFeedbackDetails.Visible = false;
                panelSaved.Visible = false;
                panelFeedbackError.Visible = true;
            }
        }

        /// <summary>
        /// Loads the Journey data in the information panels
        /// </summary>
        private void LoadSessionPanels()
        {
            #region Request
            if (panelJourneyRequest.Visible)
            {
                // if we're showing the panel, then need get and populate the relevant session data
                string journeyRequest = fbHelper.GetFeedbackJourneyRequest(Convert.ToInt32(textFeedbackId.Text), textSessionId.Text);

                if (journeyRequest != null)
                    textJourneyRequest.Text = journeyRequest;
                else
                    textJourneyRequest.Text = "Unable to load Journey Request information";
            }
            #endregion

            #region Result
            if (panelJourneyResult.Visible)
            {
                // if we're showing the panel, then need get and populate the relevant session data
                string journeyResult = fbHelper.GetFeedbackJourneyResult(Convert.ToInt32(textFeedbackId.Text), textSessionId.Text);

                if (journeyResult != null)
                    textJourneyResult.Text = journeyResult;
                else
                    textJourneyResult.Text = "Unable to load Journey Result information";
            }
            #endregion

            #region Itinerary
            if (panelItineraryManager.Visible)
            {
                // if we're showing the panel, then need get and populate the relevant session data
                string itineraryManager = fbHelper.GetFeedbackItineraryManager(Convert.ToInt32(textFeedbackId.Text), textSessionId.Text);

                if (itineraryManager != null)
                    textItineraryManager.Text = itineraryManager;
                else
                    textItineraryManager.Text = "Unable to load Itinerary Manager information";
            }
            #endregion
        }

		#endregion

		#region Event handlers

		/// <summary>
		/// Submit button click event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ButtonSubmit_Click(object sender, EventArgs e)
		{
			// Hide all the session information panels
			HideSessionPanels();

			string feedbackIdInput;
			int feedbackId = 0;
			
			// Get feedback id entered
			if (textFeedbackIdInput.Text.Trim().Length != 0)
			{
				feedbackIdInput = HttpUtility.HtmlEncode(textFeedbackIdInput.Text.Trim());
				try
				{
					feedbackId = Convert.ToInt32(feedbackIdInput);
				}
				catch
				{
					feedbackId = 0;
				}
			}

            LoadFeedbackRecord(feedbackId);
		}

		/// <summary>
		/// Event handler for default action
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void DefaultActionClick(object sender, EventArgs e)
		{
			ImageClickEventArgs imageEventArgs = new ImageClickEventArgs(0,0);
			ButtonSubmit_Click(sender, e); 
		}

		/// <summary>
		/// Update button click event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ButtonUpdate_Click(object sender, EventArgs e)
		{
			// Retrieve the record we're editing
			feedbackRecord = fbHelper.GetFeedback(Convert.ToInt32(textFeedbackId.Text));

			if (feedbackRecord != null)
			{
				// Obtain delete value selected
				feedbackRecord.DeleteFlag = (listDelete.SelectedIndex == 0);

				// Obtain feedback status selected
				feedbackRecord.FeedbackStatus = listFeedbackStatus.SelectedValue;

				// Obtain vantive id entered
				feedbackRecord.VantiveId = HttpUtility.HtmlEncode(textVantiveId.Text);

				// No other details can be changed

				if (fbHelper == null)
					fbHelper = new FeedbackHelper();

				// Save feedback record
				bool saved = fbHelper.SaveFeedback(feedbackRecord);
            
				panelSaved.Visible = true;

				if (saved)
					labelSaved.Text = GetResource("FeedbackViewer.FeedbackSaved.Success.Text");
				else
					labelSaved.Text = GetResource("FeedbackViewer.FeedbackSaved.Failure.Text");
			}

            LoadFeedbackRecord();
            LoadSessionPanels();
		}

		/// <summary>
		/// Journey Request button click event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ButtonJourneyRequest_Click(object sender, EventArgs e)
		{
            LoadFeedbackRecord();

			if (panelJourneyRequest.Visible)
			{
				panelJourneyRequest.Visible = false;
				textJourneyRequest.Text = string.Empty;
			}
			else
			{			
				panelJourneyRequest.Visible = true;
			}

            LoadSessionPanels();
		}

		/// <summary>
		/// Journey Result button click event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ButtonJourneyResult_Click(object sender, EventArgs e)
		{
            LoadFeedbackRecord();

			if (panelJourneyResult.Visible)
			{
				panelJourneyResult.Visible = false;
				textJourneyResult.Text = string.Empty;
			}
			else
			{
				panelJourneyResult.Visible = true;		
			}

            LoadSessionPanels();
		}

		/// <summary>
		/// Itinerary Manager button click event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ButtonItineraryManager_Click(object sender, EventArgs e)
		{
            LoadFeedbackRecord();

			if (panelItineraryManager.Visible)
			{
				panelItineraryManager.Visible = false;
				textItineraryManager.Text = string.Empty;
			}
			else
			{			
				panelItineraryManager.Visible = true;
			}

            LoadSessionPanels();
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            buttonSubmit.Click += new EventHandler(this.ButtonSubmit_Click);
            buttonJourneyRequest.Click += new EventHandler(this.ButtonJourneyRequest_Click);
            buttonJourneyResult.Click += new EventHandler(this.ButtonJourneyResult_Click);
            buttonItineraryManager.Click += new EventHandler(this.ButtonItineraryManager_Click);
            buttonUpdate.Click += new EventHandler(this.ButtonUpdate_Click);
            headerControl.DefaultActionEvent += new EventHandler(this.DefaultActionClick);
		}
		#endregion
	}
}
