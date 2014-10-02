// *********************************************** 
// NAME                 : FeedbackControl.aspx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 05/01/2007
// DESCRIPTION          : Control to hold the various feedback controls and perform the processing
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FeedbackControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:20:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:42   mturner
//Initial revision.
//
//   Rev 1.5   Mar 07 2007 12:15:42   mmodi
//Changed logic for when feedback fails to return control to initial state, but ensuring users comments are not lost.
//Resolution for 4366: Feedback records not being submitted
//
//   Rev 1.4   Jan 24 2007 16:48:36   mmodi
//Added journey ref number to user comments
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.3   Jan 18 2007 16:25:26   mmodi
//Amended so Journey is added regardless if it is a problem with it or not
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.2   Jan 17 2007 18:03:00   mmodi
//Added screen reader controls
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.1   Jan 12 2007 14:13:38   mmodi
//Updated code as part of development
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.0   Jan 08 2007 10:20:30   mmodi
//Initial revision.
//Resolution for 4332: Contact Us Improvements - workstream
//
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Web.Adapters;
	
	/// <summary>
	///		Summary description for FeedbackControl.
	/// </summary>
	public partial class FeedbackControl : TDUserControl
	{
		#region Controls

		protected TransportDirect.UserPortal.Web.Controls.FeedbackOptionControl feedbackOptionControl;
		protected TransportDirect.UserPortal.Web.Controls.FeedbackProblemControl feedbackProblemControl;
		protected TransportDirect.UserPortal.Web.Controls.FeedbackDetailsEmailControl feedbackDetailsEmailControl;




		#endregion

		#region Private variables

		private InputPageState inputPageState;

		#endregion

		#region Page_Load, Page_PreRender

		/// <summary>
		/// Page_Load
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SubmitButton.Text = GetResource("FeedbackControl.SubmitButton.Text");
			AnotherFeedbackButton.Text = GetResource("FeedbackControl.SubmitAnotherFeedback.Text");
			
			// Labels to be read by Screen Reader only
			labelSRFeedbackStart.Text = GetResource("FeedbackControl.FeedbackStart.Text");
			labelSRFeedbackEnd.Text = GetResource("FeedbackControl.FeedbackEnd.Text");

			inputPageState = TDSessionManager.Current.InputPageState;			
		}

		/// <summary>
		/// Page_PreRender
		/// </summary>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			SetControlVisibility();
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Sets the visibility of the Feedback controls
		/// </summary>
		private void SetControlVisibility()
		{
			if (feedbackOptionControl.FeedbackOptionSelected < 0)
			{
				inputPageState.FeedbackPageState = FeedbackState.Initial;
			}

			switch (inputPageState.FeedbackPageState)
			{
				case FeedbackState.Initial:
				{
					feedbackOptionControl.Visible = true;
					feedbackProblemControl.Visible = false;
					feedbackProblemControl.Initialise();
					feedbackDetailsEmailControl.Visible = false;
					feedbackDetailsEmailControl.Initialise();
					PanelSubmit.Visible = false;
				}
					break;
				case FeedbackState.Problem:
				{
					feedbackProblemControl.Visible = true;
					if ((feedbackProblemControl.FeedbackProblemWithResultsOptionSelected == -1)
						&&
						(feedbackProblemControl.FeedbackAnotherProblemSelected == -1))
					{
						feedbackDetailsEmailControl.Visible = false;
						PanelSubmit.Visible = false;
					}
					else
					{
						feedbackDetailsEmailControl.Visible = true;
						PanelSubmit.Visible = true;
					}
				}
					break;
				case FeedbackState.Suggestion:
				{
					feedbackDetailsEmailControl.Visible = true;
					PanelSubmit.Visible = true;
					feedbackProblemControl.Visible = false;
					// Reset the problem control in case user switches to Problem
					feedbackProblemControl.Initialise();
				}
					break;
				case FeedbackState.SubmittedSuccess:
				{
					feedbackConfirmationLabel.Text = GetResource("FeedbackControl.FeedbackConfirmationSuccess.Text");

					// If everything gone through ok, show confirmation
					ShowConfirmation();
				}
					break;
				case FeedbackState.SubmittedFail:
				{
					feedbackConfirmationLabel.Text = GetResource("FeedbackControl.FeedbackConfirmationFail.Text");

					AnotherFeedbackButton.Visible = false;
					PanelSubmit.Visible = false;
					PanelFeedbackConfirmation.Visible = true;
					feedbackOptionControl.Initialise();
					feedbackProblemControl.Visible = false;
					feedbackProblemControl.Initialise();
					feedbackDetailsEmailControl.Visible = false;
					PanelSubmit.Visible = false;
				}
					break;
			}
			   
		}

		/// <summary>
		/// Displays the Feedback sent confirmation message
		/// </summary>
		private void ShowConfirmation()
		{
			feedbackOptionControl.Visible = false;
			feedbackProblemControl.Visible = false;
			feedbackDetailsEmailControl.Visible = false;
			PanelSubmit.Visible = false;
			AnotherFeedbackButton.Visible = true;

			feedbackProblemControl.Initialise();

			PanelFeedbackConfirmation.Visible = true;
		}

		/// <summary>
		/// Creates an ArrayList of Options (Items) user has selected on the Feedback page
		/// </summary>
		/// <returns>ArrayList of Option Items</returns>
		private ArrayList GetUserOptionsItems()
		{
			ArrayList userOptionsItems = new ArrayList();

			// Feedback type selected
			userOptionsItems.Add (feedbackOptionControl.FeedbackOptionSelectedItem);

			// Problem, otherwise its a suggestion
			if (feedbackOptionControl.FeedbackOptionSelected == 0)
			{
				if (feedbackProblemControl.FeedbackProblemWithJourneyOptionSelected >= 0)
				{
					userOptionsItems.Add(feedbackProblemControl.FeedbackProblemWithJourneyOptionSelectedItem);
				}

				if (feedbackProblemControl.FeedbackProblemWithResultsOptionSelected >= 0)
				{
					userOptionsItems.Add(feedbackProblemControl.FeedbackProblemWithResultsOptionSelectedItem);
				}

				if (feedbackProblemControl.FeedbackAnotherProblemSelected >= 0)
				{
					userOptionsItems.Add(feedbackProblemControl.FeedbackAnotherProblemSelectedItem);
				}
			}
    
			return userOptionsItems;
		}

		/// <summary>
		/// Creates an ArrayList of Options (Values) user has selected on the Feedback page
		/// </summary>
		/// <returns>ArrayList of Option Values</returns>
		private ArrayList GetUserOptionsValues()
		{
			ArrayList userOptionsValues = new ArrayList();

			// Feedback type selected
			userOptionsValues.Add(feedbackOptionControl.FeedbackOptionSelectedValue);

			// Problem, otherwise its a suggestion
			if (feedbackOptionControl.FeedbackOptionSelected == 0)
			{
				if (feedbackProblemControl.FeedbackProblemWithResultsOptionSelected >= 0)
				{
					userOptionsValues.Add(feedbackProblemControl.FeedbackProblemWithResultsOptionSelectedValue);
				}

				if (feedbackProblemControl.FeedbackAnotherProblemSelected >= 0)
				{
					userOptionsValues.Add(feedbackProblemControl.FeedbackAnotherProblemSelectedValue);
				}
			}
    
			return userOptionsValues;
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Event Handler for Submit Button Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SubmitButton_Click(object sender, EventArgs e)
		{
			// If user selected "I didnt get any results" or "Another journey", then they do not
			// need to enter any details in the Comments Text box
			if( (feedbackDetailsEmailControl.UserComments.Length == 0)
					 &&
						((feedbackProblemControl.FeedbackProblemWithResultsOptionSelected != 0)
						&&
						(feedbackProblemControl.FeedbackAnotherProblemSelected != 2))
					 )
			{
				feedbackDetailsEmailControl.UserCommentsErrorVisible = true;
			}
			else 
			{
				if(Page.IsValid) 
				{
					ArrayList userOptionsItems;
					ArrayList userOptionsValues;
					int feedbackType;
					string userEmailAddress = string.Empty;
					string userComments = string.Empty;


                    // Get the user feedback options selected
					userOptionsItems = GetUserOptionsItems();
					userOptionsValues = GetUserOptionsValues();
                    
					feedbackType = feedbackOptionControl.FeedbackOptionSelected;
						
					// Get the User Comments and Email Address from the web page				
					userEmailAddress = feedbackDetailsEmailControl.EmailAddress.ToString(TDCultureInfo.CurrentUICulture);
					userComments = feedbackDetailsEmailControl.UserComments.ToString(TDCultureInfo.CurrentUICulture);

					// To identify user did not enter any comments
					if (userComments.Length == 0)
					{
						userComments = GetResource("FeedbackControl.NoComments.Text");
					}
					
					if (feedbackProblemControl.FeedbackAnotherProblemSelected == 2)
					{
						// Another journey option was entered, so add the details entered
						userComments = userComments + "\r\n\r\n" + feedbackProblemControl.GetJourneyDetailsEntered();
					}
					else if (feedbackProblemControl.ShowJourneyControl())
					{
						// if there is a journey, then add journey input details regardless if they
						// selected there was a problem with a journey or not
						userComments = userComments + "\r\n" + feedbackProblemControl.GetJourneyInputSummary() + 
							"\r\n" + "Journey reference number: " + feedbackProblemControl.GetJourneyRefNumber();
					}

					FeedbackHelper fbHelper = new FeedbackHelper();

					// Save to database and email
					if (fbHelper.SubmitFeedback(userEmailAddress, userComments, userOptionsItems, userOptionsValues, feedbackType))
					{
						inputPageState.FeedbackPageState = FeedbackState.SubmittedSuccess;
					}
					else
					{
						inputPageState.FeedbackPageState = FeedbackState.SubmittedFail;
					}

					// Save inputPageState back to the session
					TDSessionManager.Current.InputPageState = inputPageState;
				}
			}
		}

		/// <summary>
		/// Event Handler for Another Feedback Button Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AnotherFeedbackButton_Click(object sender, EventArgs e)
		{
			inputPageState.FeedbackPageState = FeedbackState.Initial;
            feedbackOptionControl.Initialise();
			feedbackDetailsEmailControl.Initialise();

			//Reload the Feedback page
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.GoFeedback;
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
			this.SubmitButton.Click += new EventHandler(this.SubmitButton_Click);
			this.AnotherFeedbackButton.Click += new EventHandler(this.AnotherFeedbackButton_Click);
		}
		#endregion
	}
}
