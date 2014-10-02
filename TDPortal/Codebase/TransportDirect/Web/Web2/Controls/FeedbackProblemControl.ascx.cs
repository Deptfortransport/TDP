// *********************************************** 
// NAME                 : FeedbackProblemControl.aspx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 05/01/2007
// DESCRIPTION          : Control to allow user to select Feedback Problem options
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FeedbackProblemControl.ascx.cs-arc  $
//
//   Rev 1.3   Jan 16 2009 13:27:24   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:20:20   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:50   mturner
//Initial revision.
//
//   Rev 1.5   Jan 24 2007 16:47:46   mmodi
//Exposed journey ref number
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.4   Jan 24 2007 12:46:20   mmodi
//Added button to provide alternative functionality when Javascript disabled
//Resolution for 4342: Contact Us: Does not work when Javascript disabled
//
//   Rev 1.3   Jan 18 2007 16:26:20   mmodi
//Exposed ShowControl method on child control
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.2   Jan 17 2007 18:11:34   mmodi
//Added screen reader controls
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.1   Jan 12 2007 14:13:58   mmodi
//Updated code as part of development
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.0   Jan 08 2007 10:29:14   mmodi
//Initial revision.
//Resolution for 4332: Contact Us Improvements - workstream
//

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
	///		Summary description for FeedbackProblemControl.
	/// </summary>
	public partial class FeedbackProblemControl : TDUserControl
	{
		#region Controls



		protected TransportDirect.UserPortal.Web.Controls.FeedbackJourneyControl feedbackJourneyControl;
		protected TransportDirect.UserPortal.Web.Controls.FeedbackJourneyInputControl feedbackJourneyInputControl;



		protected HyperlinkPostbackControl hyperlinkNext1;
		protected HyperlinkPostbackControl hyperlinkNext2;
		protected HyperlinkPostbackControl hyperlinkNext3;

		#endregion

		#region Private variables

		private IDataServices populator;

		#endregion

		#region Page_Init, Page_Load

		/// <summary>
		/// Page_Init
		/// </summary>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}

		/// <summary>
		/// Page_Load
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ProblemConfirmationLabel.Text = GetResource("FeedbackProblemControl.ProblemConfirmation");
			ProblemWithJourneyLabel.Text = GetResource("FeedbackProblemControl.ProblemWithJourney");
			ProblemWithResultsLabel.Text = GetResource("FeedbackProblemControl.ProblemWithResults");
			AnotherProblemLabel.Text = GetResource("FeedbackProblemControl.AnotherProblem");
			NextButton.Text = GetResource("FeedbackProblemControl.Next");
			
			// Hyperlink buttons displayed when Javascript has been disabled on user browser
			hyperlinkNext1.Text = GetResource("UserSurvey.ButtonSubmit.Text");
			hyperlinkNext2.Text = GetResource("UserSurvey.ButtonSubmit.Text");
			hyperlinkNext3.Text = GetResource("UserSurvey.ButtonSubmit.Text");

			// Don't display the hyperlink button labels, and set their intial visibility to false;
			hyperlinkNext1.ShowLabelForNonJS = false;
			hyperlinkNext2.ShowLabelForNonJS = false;
			hyperlinkNext3.ShowLabelForNonJS = false;
			hyperlinkNext1.Visible = false;
			hyperlinkNext2.Visible = false;
			hyperlinkNext3.Visible = false;

            int feedbackResultsConfirmationListIndex = feedbackResultsConfirmationList.SelectedIndex;
            int feedbackJourneyConfirmationListIndex = feedbackJourneyConfirmationList.SelectedIndex;
            int feedbackOtherProblemListIndex = feedbackOtherProblemList.SelectedIndex;

			populator.LoadListControl(DataServiceType.UserFeedbackJourneyResult, feedbackResultsConfirmationList);
			populator.LoadListControl(DataServiceType.UserFeedbackJourneyConfirm, feedbackJourneyConfirmationList);
			populator.LoadListControl(DataServiceType.UserFeedbackOtherOptions, feedbackOtherProblemList);

            feedbackResultsConfirmationList.SelectedIndex =  feedbackResultsConfirmationListIndex;
            feedbackJourneyConfirmationList.SelectedIndex = feedbackJourneyConfirmationListIndex;
            feedbackOtherProblemList.SelectedIndex = feedbackOtherProblemListIndex;


			// Labels to be read by Screen Reader only
			labelSRSelectOption1.Text = GetResource("FeedbackControl.SelectOption.Text");
			labelSRSelectOption2.Text = GetResource("FeedbackControl.SelectOption.Text");
			labelSRSelectOption3.Text = GetResource("FeedbackControl.SelectOption.Text");

			// Set control visibility
			if ((FeedbackProblemWithResultsOptionSelected >= 0) || (FeedbackAnotherProblemSelected >= 0) )
			{
				HideConfirmationPanel();
				
				switch (feedbackJourneyConfirmationList.SelectedIndex)
				{
					case 0:
					{	//yes
						PanelResult.Visible = true;
						feedbackResultsConfirmationList.Visible = true;
					}
						break;
					default:
					{	//No
						PanelOtherProblem.Visible = true;
						feedbackOtherProblemList.Visible = true;
					}
						break;
				}
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Hides the Report a problem confirmation panel
		/// </summary>
		private void HideConfirmationPanel()
		{
			PanelConfirmation.Visible = false;
		}

		/// <summary>
		/// Hides the Journey panel and journey confirmation list
		/// </summary>
		private void HideJourneyPanel()
		{
			PanelJourney.Visible = false;
			feedbackJourneyConfirmationList.Visible = false;
		}

		/// <summary>
		/// Hides the Result panel and result confirmation list
		/// </summary>
		private void HideResultPanel()
		{
			PanelResult.Visible = false;
			feedbackResultsConfirmationList.Visible = false;
		}

		/// <summary>
		/// Hides the Other problem panel and other problems list
		/// </summary>
		private void HideOtherProblemPanel()
		{
			PanelOtherProblem.Visible = false;
			feedbackOtherProblemList.Visible = false;
			feedbackJourneyInputControl.Visible = false;
		}

		/// <summary>
		/// Hides the Journey (Information) control
		/// </summary>
		private void HideJourneyInformation()
		{
			feedbackJourneyControl.Visible = false;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Get option selected for Report a problem with this journey question - Selected Index
		/// </summary>
		public int FeedbackProblemWithJourneyOptionSelected
		{
			get { return feedbackJourneyConfirmationList.SelectedIndex; }
		}

		/// <summary>
		/// Get option selected for Report a problem with this journey question - Selected Item
		/// </summary>
		public string FeedbackProblemWithJourneyOptionSelectedItem
		{
			get { return populator.GetValue(DataServiceType.UserFeedbackJourneyConfirm, feedbackJourneyConfirmationList.SelectedItem.Value); }
		}

		/// <summary>
		/// Get option selected for Report a problem with this journey question - Selected Value
		/// </summary>
		public string FeedbackProblemWithJourneyOptionSelectedValue
		{
			get { return feedbackJourneyConfirmationList.SelectedValue; }
		}


		/// <summary>
		/// Get option selected for Problem with results question - Selected Index
		/// </summary>
		public int FeedbackProblemWithResultsOptionSelected
		{
			get { return feedbackResultsConfirmationList.SelectedIndex; }
		}

		/// <summary>
		/// Get option selected for Problem with results question - Selected Item
		/// </summary>
		public string FeedbackProblemWithResultsOptionSelectedItem
		{
			get { return populator.GetValue(DataServiceType.UserFeedbackJourneyResult , feedbackResultsConfirmationList.SelectedItem.Value); }
		}

		/// <summary>
		/// Get option selected for Problem with results question - Selected Value
		/// </summary>
		public string FeedbackProblemWithResultsOptionSelectedValue
		{
			get { return feedbackResultsConfirmationList.SelectedValue; }
		}


		/// <summary>
		/// Get option selected for Other problems list - Selected Index
		/// </summary>
		public int FeedbackAnotherProblemSelected
		{
			get { return feedbackOtherProblemList.SelectedIndex; }
		}

		/// <summary>
		/// Get option selected for Other problems list - Selected Item
		/// </summary>
		public string FeedbackAnotherProblemSelectedItem
		{
			get { return populator.GetValue(DataServiceType.UserFeedbackOtherOptions, feedbackOtherProblemList.SelectedItem.Value); }
		}

		/// <summary>
		/// Get option selected for Other problems list - Selected Value
		/// </summary>
		public string FeedbackAnotherProblemSelectedValue
		{
			get { return feedbackOtherProblemList.SelectedValue; }
		}

		public bool ShowJourneyControl()
		{
			return feedbackJourneyControl.ShowControl;
		}

		/// <summary>
		/// Returns journey details entered on the FeedbackJourneyInputControl
		/// </summary>
		/// <returns></returns>
		public string GetJourneyDetailsEntered()
		{
			return feedbackJourneyInputControl.GetJourneyDetails();
		}

		/// <summary>
		/// Returns journey request input summary on the FeedbackJourneyControl
		/// </summary>
		/// <returns></returns>
		public string GetJourneyInputSummary()
		{
			return feedbackJourneyControl.JourneyInputSummary;
		}

		/// <summary>
		/// Returns journey reference number on the FeedbackJourneyControl
		/// </summary>
		/// <returns></returns>
		public string GetJourneyRefNumber()
		{
			return feedbackJourneyControl.JourneyReferenceNumber;
		}

		/// <summary>
		/// Resets all the options to their defaults
		/// </summary>
		public void Initialise()
		{
			feedbackJourneyConfirmationList.SelectedIndex = -1;
			feedbackResultsConfirmationList.SelectedIndex = -1;
			feedbackOtherProblemList.SelectedIndex = -1;
	
			HideJourneyPanel();
			HideResultPanel();
			HideOtherProblemPanel();

			feedbackJourneyInputControl.Visible = false;
			feedbackJourneyControl.Visible = false;
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Journey confirmation list selected index change event handler
		/// </summary>
		protected void feedbackJourneyConfirmationList_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Hide Confirmation label and Next button
			HideConfirmationPanel();

			// Continue to show the Journey question
			PanelJourney.Visible = true;
			
			switch (feedbackJourneyConfirmationList.SelectedIndex)
			{
				case 1:
				{
					//No
					PanelOtherProblem.Visible = true;
					feedbackOtherProblemList.Visible = true;

					HideResultPanel();
					
					hyperlinkNext3.Visible = !((this.Page as TDPage).IsJavascriptEnabled);
				}
					break;
				default:
				{
					//yes
					PanelResult.Visible = true;
					feedbackResultsConfirmationList.Visible = true;

					HideOtherProblemPanel();
					
					hyperlinkNext2.Visible = !((this.Page as TDPage).IsJavascriptEnabled);
				}
					break;
			}

			// Ensure the other lists have no option selected
			feedbackResultsConfirmationList.SelectedIndex = -1;
			feedbackOtherProblemList.SelectedIndex = -1;
			
			// Only display the hyperlink button if Javascript is disabled
			hyperlinkNext1.Visible = !((this.Page as TDPage).IsJavascriptEnabled);
		}

		/// <summary>
		/// Results confirmation list selected index change event handler
		/// </summary>
		protected void feedbackResultsConfirmationList_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Ensure the visibility of the other lists
			HideConfirmationPanel();
			HideOtherProblemPanel();

			PanelJourney.Visible = true;
            PanelResult.Visible = true;
			
			feedbackJourneyControl.Visible = true;

			// Set visibility of javascript links
			hyperlinkNext1.Visible = !((this.Page as TDPage).IsJavascriptEnabled);
			hyperlinkNext2.Visible = !((this.Page as TDPage).IsJavascriptEnabled);
		}

		/// <summary>
		/// Other problem list selected index change event handler
		/// </summary>
		protected void feedbackOtherProblemList_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Ensure the visibility of the other lists
			HideConfirmationPanel();
			HideResultPanel();

			if (feedbackJourneyControl.ShowControl)
			{
				PanelJourney.Visible = true;
				feedbackJourneyControl.Visible = true; 
				hyperlinkNext1.Visible = !((this.Page as TDPage).IsJavascriptEnabled);
			}
			else
			{
				HideJourneyPanel();
				HideJourneyInformation();
			}

			PanelOtherProblem.Visible = true;

			switch (feedbackOtherProblemList.SelectedIndex)
			{
				case 2:
				{
					//Another journey option was selected so show the Journey input fields
					feedbackJourneyInputControl.Visible = true;
				}
					break;
				default:
				{
					feedbackJourneyInputControl.Visible = false;
				}
					break;
			}
			
			// Set visibility of javascript link
			hyperlinkNext3.Visible = !((this.Page as TDPage).IsJavascriptEnabled);
		}

		/// <summary>
		/// Next button click event handler
		/// </summary>
		protected void nextButton_Click(object sender, EventArgs e)
		{
			// Hide Confirmation label and Next button
			HideConfirmationPanel();

			// Check if the user had submitted a journey request by looking in the Session
			if (feedbackJourneyControl.ShowControl)
			{
				feedbackJourneyControl.Visible = true;
				
				PanelJourney.Visible = true;
				feedbackJourneyConfirmationList.Visible = true;

				// Only display the hyperlink button if Javascript is disabled
				hyperlinkNext1.Visible = !((this.Page as TDPage).IsJavascriptEnabled);
			}
			else
			{
				HideJourneyInformation();

				PanelOtherProblem.Visible = true;
				feedbackOtherProblemList.Visible = true;

				// Only display the hyperlink button if Javascript is disabled
				hyperlinkNext3.Visible = !((this.Page as TDPage).IsJavascriptEnabled);
			}			
		}

		/// <summary>
		/// Method handles the click event from the hyperlinkNext buttons
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hyperlinkNext_link_Clicked(object sender, EventArgs e)
		{
			// Do nothing - we just want the page to postback so the select Option lists update
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
			this.hyperlinkNext1.link_Clicked += new EventHandler(this.hyperlinkNext_link_Clicked);
			this.hyperlinkNext2.link_Clicked += new EventHandler(this.hyperlinkNext_link_Clicked);
			this.hyperlinkNext3.link_Clicked += new EventHandler(this.hyperlinkNext_link_Clicked);

            this.NextButton.Click += new EventHandler(this.nextButton_Click);

            this.feedbackJourneyConfirmationList.SelectedIndexChanged 
                += new EventHandler(this.feedbackJourneyConfirmationList_SelectedIndexChanged);
            this.feedbackResultsConfirmationList.SelectedIndexChanged
                += new EventHandler(this.feedbackResultsConfirmationList_SelectedIndexChanged);
            this.feedbackOtherProblemList.SelectedIndexChanged
                += new EventHandler(this.feedbackOtherProblemList_SelectedIndexChanged);
 		}
		#endregion
	}
}
