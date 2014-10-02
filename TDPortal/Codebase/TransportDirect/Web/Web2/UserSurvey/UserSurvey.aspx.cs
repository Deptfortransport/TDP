// *********************************************** 
// NAME                 : UserSurvey.aspx.cs
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 05/09/2003
// DESCRIPTION			: User Survey page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/UserSurvey/UserSurvey.aspx.cs-arc  $
//
//   Rev 1.5   Mar 04 2010 17:03:32   pghumra
//Updated tracking functionality
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.4   Feb 19 2010 14:14:06   pghumra
//Fixed issue of losing selections in multiple choice questions across postbacks.
//Resolution for 5405: Problem with user survey questionnaire issue
//
//   Rev 1.3   Jan 20 2009 11:11:38   devfactory
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:27:20   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:58   mturner
//Initial revision.
//
//   Rev 1.33   Feb 24 2006 10:17:28   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.32   Feb 10 2006 15:09:34   build
//Automatically merged from branch for stream3180
//
//   Rev 1.31.1.0   Dec 06 2005 17:41:24   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.31   Nov 18 2005 16:48:48   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.30   Nov 09 2005 16:01:36   ECHAN
//updates for code review
//
//   Rev 1.29   Nov 09 2005 15:13:42   ECHAN
//Fix for code review comments #4
//
//   Rev 1.28   Nov 03 2005 16:10:36   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.27.1.0   Oct 14 2005 14:31:04   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.27   Feb 18 2005 14:06:34   rscott
//Updated - GetSurveyAnswers() method altered for Q8 to fix VANTIVE 3590507
//
//   Rev 1.26   Feb 10 2005 09:25:40   COwczarek
//Changes to use new mechanism for selecting an alternate resource manager
//Resolution for 1921: DEL 7 Development : Find A Fare Ticket Selection
//
//   Rev 1.25   Nov 25 2004 09:57:58   rgreenwood
//IR1767 - fixed Q7 and ContactEmailRadio postback events so they no longer trip validation on postback.
//Resolution for 1767: User survey -  Question 7 options 2 and 3 proceeds to 'Correct the section(s) marked in red. Then click 'Continue'.'
//
//   Rev 1.24   Nov 19 2004 09:11:04   rgreenwood
//IR1761 Changed GetSurveyAnswers() for Q16 to correctly point to Q16DropDown.
//
//   Rev 1.23   Nov 18 2004 15:02:56   rgreenwood
//IR 1749 & 1750. Added Page.Validate() in page_load to correct validation behaviour on Postback, and set email textboxes to default disabled.
//Resolution for 1749: User survey email address entry enabled on survey display
//
//   Rev 1.22   Nov 12 2004 13:53:46   rgreenwood
//FX Cop changes to ToString() methods
//
//   Rev 1.21   Nov 11 2004 18:45:20   rgreenwood
//General code tidy up, comments and summaries, and enhanced email textbox disbling to change textbox bkg colour
//
//   Rev 1.20   Nov 11 2004 16:25:26   rgreenwood
//Improved EMail validation mechanism
//
//   Rev 1.19   Nov 11 2004 12:13:56   rgreenwood
//Fixed Q6 Validation and made Email Validation mechanism more robust
//
//   Rev 1.18   Nov 10 2004 16:28:00   rgreenwood
//Fixed Q7 & Q8 interactions
//
//   Rev 1.17   Nov 10 2004 14:29:56   rgreenwood
//Some code tidy up
//
//   Rev 1.16   Nov 09 2004 18:17:46   rgreenwood
//Outstanding validation logic completed
//
//   Rev 1.15   Nov 05 2004 15:56:06   rgreenwood
//Added Email Validation
//
//   Rev 1.14   Nov 04 2004 14:14:50   rgreenwood
//Q9RadioMatrix Validation tests and Thank You panel
//
//   Rev 1.13   Oct 29 2004 15:07:28   jmorrissey
//Updated after integration with Rob
//
//   Rev 1.12   Oct 28 2004 16:45:36   rgreenwood
//Checked in to fix daily build
//
//   Rev 1.11   Oct 27 2004 11:04:24   jmorrissey
//Added new question requested by the dft
//
//   Rev 1.10   Oct 26 2004 18:28:30   jmorrissey
//Updated GetSurveyAnswers method
//
//   Rev 1.9   Oct 26 2004 12:18:24   rgreenwood
//Updated Answer Capture methods - in progress
//
//   Rev 1.8   Oct 25 2004 10:14:52   jmorrissey
//Updated declaration of UserSurveyRadioMaatrix control to use fully qualified namespace
//
//   Rev 1.7   Oct 22 2004 17:43:56   jmorrissey
//Removed show printer friendly page option
//
//   Rev 1.6   Oct 22 2004 11:53:10   rgreenwood
//Added Q9RadioMatrix control to page and started validation
//
//   Rev 1.5   Oct 21 2004 11:41:34   rgreenwood
//Latest version - all questions available
//
//   Rev 1.4   Oct 14 2004 20:14:00   rgreenwood
//Question labels and drop down controls
//
//   Rev 1.3   Oct 14 2004 12:42:46   jmorrissey
//Added question labels
//
//   Rev 1.2   Oct 11 2004 13:50:42   jmorrissey
//Completed main screenflow.
//
//   Rev 1.1   Oct 08 2004 12:38:24   jmorrissey
//Updated version. Still in progress.


#region System namespaces

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading;

#endregion

#region TD namespaces

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;

using Logger = System.Diagnostics.Trace; 

#endregion

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// The UserSurvey page is triggered on a 1 in X chance when the user
	/// opens the print-friendly page (where X is a trigger value in the
	/// properties DB Table. The User Survey window is launched
	/// as a popup behind the print-friendly page. It will only appear if
	/// the user has javascript enabled on their browser. It is blocked by the
	/// Google Toolbar pop-up blocker and similar.
	/// The page consists of header areas, and four main panels (representing
	/// separate pages). The panels are shown & hidden in order from 1 to 4.
	/// On each panel, there are a variety of questions with associated validation
	/// controls (mixture of .NET validators, custom validators and bespoke
	/// validation methods(!)). The user cannot progress to the next page (panel)
	/// until the current page is successfully validated. Once complete, the results
	/// are passed to the UserSurvey table in the PermanentPortal database.
	/// </summary>
	public partial class UserSurvey : TDPrintablePage, INewWindowPage
	{
		#region Controls and variables

		//populator to load the strings for the check box list
		private DataServices.DataServices populator;

		//hashtable to store the survey results
		private Hashtable answers;

        private TrackingControlHelper trackingHelper;
		
		//labels
		protected System.Web.UI.WebControls.Label TDSurveyCompleteLabel1;
		protected System.Web.UI.WebControls.Label TDSurveyCompleteLabel2;
		protected System.Web.UI.WebControls.Label TDSurveyCompleteLabel3;
		protected System.Web.UI.WebControls.Label TDSurveyCompleteLabel4;
		protected System.Web.UI.WebControls.Label TDSurveyCompleteLabel5;
		protected System.Web.UI.WebControls.Label Label10TickAny;


		protected System.Web.UI.WebControls.Label Section1HeaderLabel;	
		protected System.Web.UI.WebControls.Label Section2HeaderLabel;
		protected System.Web.UI.WebControls.Label Section3HeaderLabel;




		//User Input Controls
		//Panel 1 Controls
		protected System.Web.UI.WebControls.DropDownList DropDownList1;	
		protected TransportDirect.UserPortal.Web.Controls.CheckBoxListRequiredFieldValidator UserSurveyQ3CheckValidator;

		//Panel 2 Controls
		protected TransportDirect.UserPortal.Web.Controls.UserSurveyRadioMatrix UserSurveyQ9RadioMatrix;

		//Panel 3 Controls
		protected System.Web.UI.WebControls.TextBox TextBoxQ14;	

		#endregion
	
		#region constructor
		/// <summary>
		/// Constructor - sets the page id
		/// </summary>
		public UserSurvey() : base() 
		{	
			//assign page ID
			pageId = PageId.UserSurvey;
			LocalResourceManager = TDResourceManager.USER_SURVEY_RM;
		}
		#endregion
	
		#region Page_Load
		/// <summary>
		/// Sets up resources and initial page display
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{

			UserSurveyQ9RadioMatrix.ResourceManager = resourceManager;

            

            int q1Drop = UserSurveyQ1Drop.SelectedIndex;
            int q2Radio = UserSurveyQ2Radio.SelectedIndex;
            bool[] q3Check = GetCheckBoxListSelection(UserSurveyQ3Check);
            bool[] q4Check = GetCheckBoxListSelection(UserSurveyQ4Check);
            int q5Radio = UserSurveyQ5Radio.SelectedIndex;
            int q6Drop1 = UserSurveyQ6Drop1.SelectedIndex;
            int q6Drop2 = UserSurveyQ6Drop2.SelectedIndex;
            int q7Radio = UserSurveyQ7Radio.SelectedIndex;
            bool[] q8Check = GetCheckBoxListSelection(UserSurveyQ8Check);

            //Panel 2 Controls - Q9 is set by itself, hence not in this list
            bool[] q10Check = GetCheckBoxListSelection(UserSurveyQ10Check);
            bool[] q11Check = GetCheckBoxListSelection(UserSurveyQ11Check);
            int q12Radio = UserSurveyQ12Radio.SelectedIndex;
            int q13Radio = UserSurveyQ13Radio.SelectedIndex;
            int q14Radio = UserSurveyQ14Radio.SelectedIndex;
            int q15Radio = UserSurveyQ15Radio.SelectedIndex;
            int q16Drop = UserSurveyQ16Drop.SelectedIndex;
            int contactRadio = UserSurveyContactRadio.SelectedIndex;

            //set up resources for page
            SetUpResources();

            UserSurveyQ1Drop.SelectedIndex = q1Drop ;
            UserSurveyQ2Radio.SelectedIndex = q2Radio ;
            SetCheckBoxListSelection(ref UserSurveyQ3Check, q3Check);
            SetCheckBoxListSelection(ref UserSurveyQ4Check, q4Check);
            UserSurveyQ5Radio.SelectedIndex = q5Radio;
            UserSurveyQ6Drop1.SelectedIndex = q6Drop1;
            UserSurveyQ6Drop2.SelectedIndex = q6Drop2;
            UserSurveyQ7Radio.SelectedIndex = q7Radio;
            SetCheckBoxListSelection(ref UserSurveyQ8Check, q8Check);

            //Panel 2 Controls - Q9 is set by itself, hence not in this list
            SetCheckBoxListSelection(ref UserSurveyQ10Check, q10Check);
            SetCheckBoxListSelection(ref UserSurveyQ11Check, q11Check);
            UserSurveyQ12Radio.SelectedIndex = q12Radio;
            UserSurveyQ13Radio.SelectedIndex = q13Radio ;
            UserSurveyQ14Radio.SelectedIndex = q14Radio;
            UserSurveyQ15Radio.SelectedIndex = q15Radio;
            UserSurveyQ16Drop.SelectedIndex = q16Drop;
            UserSurveyContactRadio.SelectedIndex = contactRadio;

			if (!IsPostBack)
			{
				//set flag to indicate that the surevy has now been shown in this session
				TDSessionManager.Current.UserSurveyAlreadyShown = true;

				//display section 1
				CurrentSectionNumber = 1;


                trackingHelper.AddTrackingParameter("UserSurvey", "UserSurvey", TrackingControlHelper.SHOW);

				
			}
			else
			{
				if (CurrentSectionNumber != 3)
				{

					//Reset state of validators on the page
					Page.Validate();

				}
				else if (CurrentSectionNumber == 3)
				{

                
					//If this is a postback triggered by the ContactRadio button
					//enable validation for email1 textbox
					if (UserSurveyContactRadio.SelectedIndex == 0)
					{
						TextUserSurveyEmail1.Enabled = true;
						TextUserSurveyEmail1.BackColor = System.Drawing.Color.White;
						TextUserSurveyEmail2.Enabled = true;
						TextUserSurveyEmail2.BackColor = System.Drawing.Color.White;

					}

					Page.Validate();
				}

			}
			//set up resources for page
			SetSectionVisibility();

		}
		#endregion

		#region methods

        /// <summary>
        /// Obtain the selection from a specified checkboxlist control
        /// </summary>
        /// <param name="checkboxlist">The checkboxbox list control whose selection is to be obtained</param>
        /// <returns>An array of boolean values representing the checkbox selection</returns>
        private bool[] GetCheckBoxListSelection(CheckBoxList checkboxlist)
        {
            bool[] selection = new bool[checkboxlist.Items.Count];

            for (int i = 0; i < checkboxlist.Items.Count; i++)
            {
                selection[i] = checkboxlist.Items[i].Selected;
            }

            return selection;
        }

        /// <summary>
        /// Set the selection of a specified checkboxlist
        /// </summary>
        /// <param name="checkboxlist">The checkboxlist control whose selection is to be set</param>
        /// <param name="selection">The selection to set</param>
        private void SetCheckBoxListSelection(ref CheckBoxList checkboxlist, bool[] selection)
        {
            for (int i = 0; i < selection.Length; i++)
            {
                checkboxlist.Items[i].Selected = selection[i];
            }
        }

        /// <summary>
        /// method that determines which sections should be shown
        /// </summary>
        private void SetSectionVisibility()
		{
			//evaluate the current section number and set the visibility accordingly
			switch (CurrentSectionNumber)
			{

					//show section 1
				case 1 :

					PanelSection1.Visible = true;					
					PanelSection2.Visible = false;
					PanelSection3.Visible = false;
					PanelSection4.Visible = false;

					//Show blue arrow for current page, others are grey
					ImageSection1Arrow1.Visible = true;
					ImageSection1Arrow1Grey.Visible = false;
					ImageSection1Arrow2.Visible = false;
					ImageSection1Arrow2Grey.Visible = true;
					ImageSection1Arrow3.Visible = false;
					ImageSection1Arrow3Grey.Visible = true;

					this.LabelSurveyHeader.Visible = true;
					this.LabelMainInstruction.Visible = true;

					this.LabelQ6Error1.Visible = false;
					this.LabelQ6Error2.Visible = false;

					//display section 1
					CurrentSectionNumber = 1;

					break;

					//show section 2
				case 2 :

					PanelSection1.Visible = false;
					PanelSection2.Visible = true;
					PanelSection3.Visible = false;
					PanelSection4.Visible = false;

					//Show blue arrow for section two, others are grey
					ImageSection1Arrow1.Visible = false;
					ImageSection1Arrow1Grey.Visible = true;
					ImageSection1Arrow2.Visible = true;
					ImageSection1Arrow2Grey.Visible = false;
					ImageSection1Arrow3.Visible = false;
					ImageSection1Arrow3Grey.Visible = true;

					//Hide the header labels
					this.LabelSurveyHeader.Visible = false;					
					this.LabelMainInstruction.Visible = false;
					
					//display section 2
					CurrentSectionNumber = 2;

					break;

					//show section 3
				case 3 :

					//Show Panel 3
					PanelSection1.Visible = false;
					PanelSection2.Visible = false;
					PanelSection3.Visible = true;
					PanelSection4.Visible = false;

					//Show blue arrow for panel 3, others are grey
					ImageSection1Arrow1.Visible = false;
					ImageSection1Arrow1Grey.Visible = true;
					ImageSection1Arrow2.Visible = false;
					ImageSection1Arrow2Grey.Visible = true;
					ImageSection1Arrow3.Visible = true;
					ImageSection1Arrow3Grey.Visible = false;

					//Hide headers and page-level error messages
					this.LabelSurveyHeader.Visible = false;					
					this.LabelMainInstruction.Visible = false;
					this.LabelUserSurveyPageErrorContinue.Visible = false;
					this.LabelUserSurveyPageErrorSubmit.Visible = false;

					//display section 3
					CurrentSectionNumber = 3;

					break;
					
					//Show section 4
				case 4 :
					//Show Panel 3
					PanelSection1.Visible = false;
					PanelSection2.Visible = false;
					PanelSection3.Visible = false;
					PanelSection4.Visible = true;
					PanelHeaderImages.Visible = false;

                    //Hide all arrows
					ImageSection1Arrow1.Visible = false;
					ImageSection1Arrow1Grey.Visible = false;
					ImageSection1Arrow2.Visible = false;
					ImageSection1Arrow2Grey.Visible = false;
					ImageSection1Arrow3.Visible = false;
					ImageSection1Arrow3Grey.Visible = false;

					//Hide headers
					this.LabelSurveyHeader.Visible = false;					
					this.LabelMainInstruction.Visible = false;


					//display section 4
					CurrentSectionNumber = 4;

					break;
					
					//by default show section 1
				default :

					//Show Panel 1
					PanelSection1.Visible = true;
					PanelSection2.Visible = false;
					PanelSection3.Visible = false;
					PanelSection4.Visible = false;

					//Set arrows for panel 1
					ImageSection1Arrow1.Visible = true;
					ImageSection1Arrow1Grey.Visible = false;
					ImageSection1Arrow2.Visible = false;
					ImageSection1Arrow2Grey.Visible = true;
					ImageSection1Arrow3.Visible = false;
					ImageSection1Arrow3Grey.Visible = true;

					//set section number currently visible
					CurrentSectionNumber = 1;
					break;
			}
		}


		/// <summary>
		/// Set up any resources that are not retrieved automatically by SetUpPageLanguage
		/// </summary>
		private void SetUpResources()
		{

			LabelUserSurveyPageErrorContinue.Text = resourceManager.GetString("UserSurvey.LabelUserSurveyPageErrorContinue", 
																						TDCultureInfo.CurrentUICulture);
			LabelUserSurveyPageErrorSubmit.Text = resourceManager.GetString("UserSurvey.LabelUserSurveyPageErrorSubmit", 
																						TDCultureInfo.CurrentUICulture);
			//Populates the drop down lists with the allowed values from DataServices
			populator = (DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			//Panel 1 Controls
			populator.LoadListControl(DataServiceType.UserSurveyQ1Drop,UserSurveyQ1Drop,resourceManager);
			populator.LoadListControl(DataServiceType.UserSurveyQ2Radio,UserSurveyQ2Radio,resourceManager);
			populator.LoadListControl(DataServiceType.UserSurveyQ3Check,UserSurveyQ3Check,resourceManager);
			populator.LoadListControl(DataServiceType.UserSurveyQ4Check,UserSurveyQ4Check,resourceManager);
			populator.LoadListControl(DataServiceType.UserSurveyQ5Radio,UserSurveyQ5Radio,resourceManager);
			populator.LoadListControl(DataServiceType.UserSurveyQ6Drop1,UserSurveyQ6Drop1,resourceManager);			
			populator.LoadListControl(DataServiceType.UserSurveyQ6Drop2,UserSurveyQ6Drop2,resourceManager);			
			populator.LoadListControl(DataServiceType.UserSurveyQ7Radio,UserSurveyQ7Radio,resourceManager);			
			populator.LoadListControl(DataServiceType.UserSurveyQ8Check,UserSurveyQ8Check,resourceManager);			
			
			//Panel 2 Controls - Q9 is set by itself, hence not in this list
			populator.LoadListControl(DataServiceType.UserSurveyQ10Check,UserSurveyQ10Check,resourceManager);			
			populator.LoadListControl(DataServiceType.UserSurveyQ11Check,UserSurveyQ11Check,resourceManager);			
			populator.LoadListControl(DataServiceType.UserSurveyQ12Radio,UserSurveyQ12Radio,resourceManager);
			populator.LoadListControl(DataServiceType.UserSurveyQ13Radio,UserSurveyQ13Radio,resourceManager);
			populator.LoadListControl(DataServiceType.UserSurveyQ14Radio,UserSurveyQ14Radio,resourceManager);
			populator.LoadListControl(DataServiceType.UserSurveyQ15Radio,UserSurveyQ15Radio,resourceManager);
			populator.LoadListControl(DataServiceType.UserSurveyQ16Drop,UserSurveyQ16Drop,resourceManager);
			populator.LoadListControl(DataServiceType.UserSurveyContactRadio,UserSurveyContactRadio,resourceManager);

			//Get text labels for TDButtons
			TDResourceManager langstrings = TDResourceManager.GetResourceManagerFromCache(TDResourceManager.LANG_STRINGS);
			ButtonContinuePage1.Text = langstrings.GetString("UserSurvey.ButtonContinuePage.Text");
			ButtonContinuePage2.Text = langstrings.GetString("UserSurvey.ButtonContinuePage.Text");
			ButtonSubmit.Text = langstrings.GetString("UserSurvey.ButtonSubmit.Text");	

			PageTitle = langstrings.GetString("UserSurvey.PageTitle");
		}

		/// <summary>
		/// Gets the numeric values (ItemValue) of the user responses to each question and
		/// stores them in the UserSurvey table in PermanentPortal, for batch processing.
		/// For RadioBoxLists or CheckBoxLists, the code will iterate through the list and
		/// get the ItemValue for the response as stored in the DropDownLists table in
		/// PermanentPortal.
		/// Responses for each question are stored as comma separated values in a string,
		/// which then passed into the Database via the AddUserSurvey stored procedure.
		/// </summary>
		private void GetSurveyAnswers()
		{
			answers = new Hashtable();			

			//counters 
			int i= 0, j = 0;

			//quotes
			string quotes = "\"";	

			//Strings to hold responses to each question
			string Q1Answers = string.Empty;
			string Q2Answers = string.Empty;
			string Q4Answers = string.Empty;
			string Q5Answers = string.Empty;	
			string Q6Answers = string.Empty;		
			string Q7Answers = string.Empty;
			string Q8Answers = string.Empty;
			string Q10Answers = string.Empty;
			string Q11Answers = string.Empty;
			string Q12Answers = string.Empty;
			string Q13Answers = string.Empty;
			string Q14Answers = string.Empty;
			string Q15Answers = string.Empty;
			string Q16Answers = string.Empty;


			//Local populator object to get ItemValues from DB for each response
			populator = (DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			#region Question 1 Answers

			//Question 1 DropDownList
			for ( i=0; i< UserSurveyQ1Drop.Items.Count; i++)
			{
				if (UserSurveyQ1Drop.Items[i].Selected)
				{
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ1Drop, 
						UserSurveyQ1Drop.SelectedItem.Value), TDCultureInfo.CurrentCulture.NumberFormat);
					Q1Answers += val;
				}
			}
			answers.Add("Q1",Q1Answers);		
		
			#endregion

			#region Question 2 Answers
			//Question 2 RadioButtonList
			for (i=0; i< UserSurveyQ2Radio.Items.Count; i++)
			{
				if (UserSurveyQ2Radio.Items[i].Selected)
				{
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ2Radio, 
						UserSurveyQ2Radio.Items[i].Value), TDCultureInfo.CurrentCulture.NumberFormat);
					Q2Answers += val;					
				}
			}
			answers.Add("Q2",Q2Answers);

			#endregion

			#region Question 3 Answers
			// Question 3 CheckBoxList - checked answers
			for (i= 0, j = 1; i< UserSurveyQ3Check.Items.Count; i++)
			{
				string Q3AnswersKey = "Q3_";

				if (UserSurveyQ3Check.Items[i].Selected)
				{					
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ3Check, 
						UserSurveyQ3Check.Items[i].Value), TDCultureInfo.CurrentCulture.NumberFormat);
										
					Q3AnswersKey += String.Format("{0}", j.ToString(TDCultureInfo.CurrentCulture));				
					answers.Add(Q3AnswersKey,val);
					j++;					
				}				
			}
			// Question 3 CheckBoxList - unchecked answers
			for (i= 0; i< UserSurveyQ3Check.Items.Count; i++)
			{
				string Q3AnswersKey = "Q3_";

				if (!UserSurveyQ3Check.Items[i].Selected)
				{					
										
					Q3AnswersKey += String.Format("{0}", j.ToString(TDCultureInfo.CurrentCulture));				
					answers.Add(Q3AnswersKey,string.Empty);
					j++;
				}
			}		
	
			#endregion

			#region Question 4 Answers
			// Question 4 CheckBoxList - checked answers
			for (i= 0, j = 1; i< UserSurveyQ4Check.Items.Count; i++)
			{
				string Q4AnswersKey = "Q4_";

				if (UserSurveyQ4Check.Items[i].Selected)
				{					
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ4Check, 
						UserSurveyQ4Check.Items[i].Value), TDCultureInfo.CurrentCulture.NumberFormat);
										
					Q4AnswersKey += String.Format("{0}", j.ToString(TDCultureInfo.CurrentCulture));				
					answers.Add(Q4AnswersKey,val);
					j++;					
				}				
			}
			// Question 4 CheckBoxList - unchecked answers
			for (i= 0; i< UserSurveyQ4Check.Items.Count; i++)
			{
				string Q4AnswersKey = "Q4_";

				if (!UserSurveyQ4Check.Items[i].Selected)
				{					
										
					Q4AnswersKey += String.Format("{0}", j.ToString(TDCultureInfo.CurrentCulture));				
					answers.Add(Q4AnswersKey,string.Empty);
					j++;
				}
			}					
			#endregion

			#region Question 5 Answers
			//Question 5 RadioButtonList
			for (i=0; i< UserSurveyQ5Radio.Items.Count; i++)
			{
				if (UserSurveyQ5Radio.Items[i].Selected)
				{
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ5Radio, 
						UserSurveyQ5Radio.Items[i].Value), TDCultureInfo.CurrentCulture.NumberFormat);
					Q5Answers += val;
				}
			}
			answers.Add("Q5",Q5Answers);
			#endregion

			#region Question 6 CheckBoxes & DropDown Answers

			if (UserSurveyQ6Check1.Checked)
			{
				Q6Answers = string.Empty;
				for ( i=0; i< UserSurveyQ6Drop1.Items.Count; i++)
				{
					if (UserSurveyQ6Drop1.Items[i].Selected)
					{
						string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ6Drop1, 
							UserSurveyQ6Drop1.SelectedItem.Value), TDCultureInfo.CurrentCulture.NumberFormat);
						Q6Answers += val;
					}
				}
				answers.Add("Q6_1",Q6Answers);
			}
			else
			{
				answers.Add("Q6_1",string.Empty);

			}

			if (UserSurveyQ6Check2.Checked)
			{
				answers.Add("Q6_2",2);
			}
			else
			{
				answers.Add("Q6_2",string.Empty);

			}

			if (UserSurveyQ6Check3.Checked)
			{
				Q6Answers = string.Empty;
				for ( i=0; i< UserSurveyQ6Drop2.Items.Count; i++)
				{
					if (UserSurveyQ6Drop2.Items[i].Selected)
					{
						string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ6Drop2, 
							UserSurveyQ6Drop2.SelectedItem.Value), TDCultureInfo.CurrentCulture.NumberFormat);
						Q6Answers += val;
					}
				}
				answers.Add("Q6_3",Q6Answers);
			}
			else
			{
				answers.Add("Q6_3",string.Empty);

			}

			#endregion
			
			#region Question 7 Answers
			//Question 7 RadioButtonList
			for (i = 0; i< UserSurveyQ7Radio.Items.Count; i++)
			{
				if (UserSurveyQ7Radio.Items[i].Selected)
				{
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ7Radio, 
						UserSurveyQ7Radio.Items[i].Value), TDCultureInfo.CurrentCulture.NumberFormat);
					Q7Answers += val;					
				}
			}
			answers.Add("Q7",Q7Answers);
			#endregion

			#region Question 8 Answers
			// Question 8 CheckBoxList - checked answers if Q7 'Yes' Checked
			if (UserSurveyQ7Radio.SelectedIndex == 0
				|| UserSurveyQ7Radio.SelectedIndex == 1
				|| UserSurveyQ7Radio.SelectedIndex == 2)
			{
				for (i= 0, j = 1; i< UserSurveyQ8Check.Items.Count; i++)
				{
					string Q8AnswersKey = "Q8_";

					if (UserSurveyQ8Check.Items[i].Selected)
					{					
						string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ8Check, 
							UserSurveyQ8Check.Items[i].Value), TDCultureInfo.CurrentCulture.NumberFormat);
										
						Q8AnswersKey += String.Format("{0}", j.ToString(TDCultureInfo.CurrentCulture));				
						answers.Add(Q8AnswersKey,val);
						j++;					
					}	
				}	
				
				// Question 8 CheckBoxList - unchecked answers
				for (i= 0; i< UserSurveyQ8Check.Items.Count; i++)
				{
					string Q8AnswersKey = "Q8_";
									
					if (!UserSurveyQ8Check.Items[i].Selected)
					{
												
						Q8AnswersKey += String.Format("{0}", j.ToString(TDCultureInfo.CurrentCulture));				
						answers.Add(Q8AnswersKey,string.Empty);
						j++;
					}
				}
			}
			else
			{
				// Question 8 CheckBoxList - unchecked answers
				for (i = 0, j = 1; i< UserSurveyQ8Check.Items.Count; i++)
				{
					string Q8AnswersKey = "Q8_";
									
					//Removed to fix vantive 3590507
					//if (!UserSurveyQ8Check.Items[i].Selected)
					//{						
						Q8AnswersKey += String.Format("{0}", j.ToString(TDCultureInfo.CurrentCulture));				
						answers.Add(Q8AnswersKey,string.Empty);
						j++;
					//}
				}
			}			

			#endregion

			#region Q9 answers
			
			Hashtable localQ9Answers = Q9Answers;

			foreach (object key in localQ9Answers.Keys)
				answers.Add(key, localQ9Answers[key]);

			#endregion

			#region Question 10 Answers

			// Question 10 CheckBoxList - checked answers
			for (i= 0, j = 1; i< UserSurveyQ10Check.Items.Count; i++)
			{
				string Q10AnswersKey = "Q10_";

				if (UserSurveyQ10Check.Items[i].Selected)
				{					
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ10Check, 
						UserSurveyQ10Check.Items[i].Value), TDCultureInfo.CurrentCulture.NumberFormat);
										
					Q10AnswersKey += String.Format("{0}", j.ToString(TDCultureInfo.CurrentCulture));				
					answers.Add(Q10AnswersKey,val);
					j++;					
				}				
			}
			// Question 10 CheckBoxList - unchecked answers
			for (i= 0; i< UserSurveyQ10Check.Items.Count; i++)
			{
				string Q10AnswersKey = "Q10_";

				if (!UserSurveyQ10Check.Items[i].Selected)
				{					
										
					Q10AnswersKey += String.Format("{0}", j.ToString(TDCultureInfo.CurrentCulture));				
					answers.Add(Q10AnswersKey,string.Empty);
					j++;
				}
			}
			#endregion
			
			#region Question 11 Answers
			// Question 11 CheckBoxList - checked answers
			for (i= 0, j = 1; i< UserSurveyQ11Check.Items.Count; i++)
			{
				string Q11AnswersKey = "Q11_";

				if (UserSurveyQ11Check.Items[i].Selected)
				{					
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ11Check, 
						UserSurveyQ11Check.Items[i].Value), TDCultureInfo.CurrentCulture.NumberFormat);
										
					Q11AnswersKey += String.Format("{0}", j.ToString(TDCultureInfo.CurrentCulture));				
					answers.Add(Q11AnswersKey,val);
					j++;					
				}				
			}
			// Question 11 CheckBoxList - unchecked answers
			for (i= 0; i< UserSurveyQ11Check.Items.Count; i++)
			{
				string Q11AnswersKey = "Q11_";

				if (!UserSurveyQ11Check.Items[i].Selected)
				{					
										
					Q11AnswersKey += String.Format("{0}", j.ToString(TDCultureInfo.CurrentCulture));				
					answers.Add(Q11AnswersKey,string.Empty);
					j++;
				}
			}
			#endregion

			#region Question 12 Answers
			//Question 12 RadioButtonList
			for (i = 0; i< UserSurveyQ12Radio.Items.Count; i++)
			{
				if (UserSurveyQ12Radio.Items[i].Selected)
				{
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ12Radio, 
						UserSurveyQ12Radio.Items[i].Value), TDCultureInfo.CurrentCulture.NumberFormat);
					Q12Answers += val;
				}
			}
			answers.Add("Q12",Q12Answers);
			#endregion

			#region Question 13 Answers
			//Question 13 RadioButtonList
			for (i = 0; i< UserSurveyQ13Radio.Items.Count; i++)
			{
				if (UserSurveyQ13Radio.Items[i].Selected)
				{
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ13Radio, 
						UserSurveyQ13Radio.Items[i].Value), TDCultureInfo.CurrentCulture.NumberFormat);
					Q13Answers += val;
				}
			}
			answers.Add("Q13",Q13Answers);
			#endregion

			#region Question 14 Answers
			//Question 14 RadioButtonList
			for (i = 0; i< UserSurveyQ14Radio.Items.Count; i++)
			{
				if (UserSurveyQ14Radio.Items[i].Selected)
				{
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ14Radio, 
						UserSurveyQ14Radio.Items[i].Value), TDCultureInfo.CurrentCulture.NumberFormat);
					Q14Answers += val;
				}
			}
			answers.Add("Q14",Q14Answers);
			#endregion

			#region Question 15 Answers
			//Question 15 RadioButtonList
			for (i = 0; i< UserSurveyQ15Radio.Items.Count; i++)
			{
				if (UserSurveyQ15Radio.Items[i].Selected)
				{
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ15Radio, 
						UserSurveyQ15Radio.Items[i].Value), TDCultureInfo.CurrentCulture.NumberFormat);
					Q15Answers += val;
				}
			}
			answers.Add("Q15",Q15Answers);
			#endregion

			#region Question 16 Answers

			//Question 16 DropDownList
			for ( i=0; i< UserSurveyQ16Drop.Items.Count; i++)
			{
				if (UserSurveyQ16Drop.Items[i].Selected)
				{
					string val = Convert.ToString(populator.GetValue(DataServiceType.UserSurveyQ16Drop, 
						UserSurveyQ16Drop.SelectedItem.Value), TDCultureInfo.CurrentCulture.NumberFormat);
					Q16Answers += val;
				}
			}
			if (TextBoxQ16.Text != null)
			{
				answers.Add("Q16",Q16Answers + quotes + TextBoxQ16.Text + quotes);
			}
			else
			{
				answers.Add("Q16",Q16Answers);

			}
			
			#endregion

			#region Email

			if (TextUserSurveyEmail1 != null)
			{
				answers.Add("ContactEmail",quotes + TextUserSurveyEmail1.Text + quotes);
			}
			
			else
			{
			answers.Add("ContactEmail",string.Empty);
			}


			#endregion 
		}

		
		/// <summary>
		/// Checks overall page validation. Tests whether any validation 
		/// errors are present on the panel.
		/// If at least one question-level validator returns a validation error,
		/// the currently viewed panel is not valid.
		/// </summary>
		/// <returns></returns>
		private bool ValidateOK(int sectionNumber)
		{
			
			bool IsValid = false;

			//Test each question's validation control
			//depending upon which panel the user is viewing.
			//If ALL questions have been validly answered, the panel is valid.
			switch (sectionNumber)
			{
					#region validate section 1
					//Validate section 1
				case 1 :

					bool q6IsValid = false;

					q6IsValid = Q6Validation();

					if (UserSurveyQ7Radio.SelectedItem == null)
					{
						UserSurveyQ8CheckRFValidator.Enabled = true;
					}
					else if (UserSurveyQ7Radio.SelectedIndex < 3)
					{
						UserSurveyQ8CheckRFValidator.Enabled = true;
					}
					else 
					{
						UserSurveyQ8CheckRFValidator.Enabled = false;
					}

					IsValid = ( UserSurveyQ2RadioRFValidator.IsValid								
								&& UserSurveyQ3CheckRFValidator.IsValid
								&& UserSurveyQ4CheckRFValidator.IsValid
								&& UserSurveyQ5RadioRFValidator.IsValid
								&& q6IsValid
								&& UserSurveyQ7RadioRFValidator.IsValid
								&& (!UserSurveyQ8CheckRFValidator.Enabled || UserSurveyQ8CheckRFValidator.IsValid) );
					break;
					#endregion

					#region Validate Section 2
					//Validate section 2
				case 2 :
					IsValid = ( UserSurveyQ9RadioMatrix.ValidateMatrix()
								&& UserSurveyQ10CheckRFValidator.IsValid
								&& UserSurveyQ11CheckRFValidator.IsValid
								&& UserSurveyQ12RadioRFValidator.IsValid
								&& UserSurveyQ13RadioRFValidator.IsValid );
				
					//add Q9 answers now
					if (UserSurveyQ9RadioMatrix.ValidateMatrix())
					{
						Q9Answers = UserSurveyQ9RadioMatrix.GetAnswers();						
					}

					break;
					#endregion

					#region Validate section 3
					//Validate section 3
				case 3:
					
					IsValid = ValidatePage3();

					if (!IsValid)
					{
						//Page not valid. Show header error message
						LabelUserSurveyPageErrorSubmit.Visible = true;
					}
					else
					{
						LabelUserSurveyPageErrorSubmit.Visible = false;
					}
					
					break;
					#endregion
			}
			return IsValid;			
		}


		private bool Q6Validation()
		{
			bool isValid = false;
			bool check1Valid;
			bool check3Valid;
			
			if ( UserSurveyQ2RadioRFValidator.Enabled
				&& UserSurveyQ3CheckRFValidator.Enabled
				&& UserSurveyQ4CheckRFValidator.Enabled
				&& UserSurveyQ5RadioRFValidator.Enabled
				&& UserSurveyQ7RadioRFValidator.Enabled )

			{
				//Validate first checkbox and dropdown
				if (UserSurveyQ6Check1.Checked)
					check1Valid = (UserSurveyQ6Drop1.SelectedIndex != 0);
				else
					check1Valid = true;

				//Validate last checkbox and dropdown
				if (UserSurveyQ6Check3.Checked)
					check3Valid = (UserSurveyQ6Drop2.SelectedIndex != 0);
				else
					check3Valid = true;

				//Validate overall question
				isValid = ((UserSurveyQ6Check1.Checked || UserSurveyQ6Check2.Checked || UserSurveyQ6Check3.Checked) && check1Valid && check3Valid);
			

				//Set visibility of error message labels
				if (!isValid)
					//Entire question is not valid - user has not checked any checkboxes
					LabelQ6Error1.Visible = true;

				if ((UserSurveyQ6Check1.Checked && !check1Valid) || (UserSurveyQ6Check3.Checked && !check3Valid))
				{
					//User has checked either checkbox 1 or 3 without selecting a dropdown response
					LabelQ6Error1.Visible = false;
					LabelQ6Error2.Visible = true;
				}
			
				return isValid;
			}
			else
			{
				return true;
			}
		}

		
		private bool ValidatePage3()
		{
			bool isValid = false;
			//Validate question 16
			UserSurveyQ16DropRFValidator.IsValid = ( UserSurveyQ16Drop.SelectedIndex != 0 );
			
			//Validate the ContactRadio control, then validate email addresses accordingly
			if (UserSurveyContactRadio.SelectedItem != null)
			{
				if (UserSurveyContactRadio.SelectedIndex == 0)
				{
					//User has ticked 'Yes' to contact
					//so enable validation'
					UserSurveyEmailRFValidator.Enabled = true;
					UserSurveyEmailRegExpValidator.Enabled = true;

					//If user has entered a second email, enable compare validator
					if (TextUserSurveyEmail2.Text.Length != 0)
					{
						UserSurveyEmailCompareValidator.Visible = true;
						UserSurveyEmailCompareValidator.Enabled = true;

						isValid = ( UserSurveyQ14RadioRFValidator.IsValid
							&& UserSurveyQ15RadioRFValidator.IsValid
							&& UserSurveyQ16DropRFValidator.IsValid
							&& UserSurveyContactRadioRFValidator.IsValid
							&& UserSurveyEmailRFValidator.IsValid
							&& UserSurveyEmailRegExpValidator.IsValid
							&& UserSurveyEmailCompareValidator.IsValid
							);
					}
					else
					{
						//User has not entered an email2
						//so do not validate its content
						UserSurveyEmailCompareValidator.Enabled = false;

						isValid = ( UserSurveyQ14RadioRFValidator.IsValid
							&& UserSurveyQ15RadioRFValidator.IsValid
							&& UserSurveyQ16DropRFValidator.IsValid
							&& UserSurveyContactRadioRFValidator.IsValid
							&& UserSurveyEmailRFValidator.IsValid
							&& UserSurveyEmailRegExpValidator.IsValid
							);
					}
				}
				else
				{
					//User has selected No, so don't validate email
					UserSurveyEmailRFValidator.Enabled = false;
					UserSurveyEmailRegExpValidator.Enabled = false;
					UserSurveyEmailCompareValidator.Enabled = false;

					isValid = ( UserSurveyQ14RadioRFValidator.IsValid
						&& UserSurveyQ15RadioRFValidator.IsValid
						&& UserSurveyQ16DropRFValidator.IsValid
						&& UserSurveyContactRadioRFValidator.IsValid
						);

				}
			}
			else
			{
				//User hasn't selected a Yes/No radio button for Contact
				UserSurveyEmailRFValidator.Enabled = false;
				UserSurveyEmailRegExpValidator.Enabled = false;
				UserSurveyEmailCompareValidator.Enabled = false;

				isValid = ( UserSurveyQ14RadioRFValidator.IsValid
					&& UserSurveyQ15RadioRFValidator.IsValid
					&& UserSurveyQ16DropRFValidator.IsValid
					&& UserSurveyContactRadioRFValidator.IsValid
					);
			}

			return isValid;
		}


		#endregion
	
		#region Event Handlers

		/// <summary>
		/// Event handler for user submitting the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonSubmit_Click(object sender, EventArgs e)
		{
			//IR1767 Enable all validation controls during this event
			UserSurveyQ14RadioRFValidator.Enabled = true;
			UserSurveyQ15RadioRFValidator.Enabled = true;
			UserSurveyQ16DropRFValidator.Enabled = true;
			UserSurveyContactRadioRFValidator.Enabled = true;
			UserSurveyEmailRegExpValidator.Enabled = true;
			UserSurveyEmailRFValidator.Enabled = true;
			UserSurveyEmailCompareValidator.Enabled = true;

			LabelUserSurveyPageErrorContinue.Enabled = true;
			LabelUserSurveyPageErrorSubmit.Enabled = true;

			Page.Validate();

			//Validate the panel, then submit answers if the panel is valid.			
			if (ValidateOK(CurrentSectionNumber))
			{
				//gets the user's survey answers 
				GetSurveyAnswers();

				//if survey is submitted successfully then show the final panel
				if (UserSurveyHelper.SubmitSurvey(answers))
				{
					
					//increment section number indicator
					if (CurrentSectionNumber < 5)
					{
                        trackingHelper.AddTrackingParameter("UserSurvey", "ButtonSubmit", TrackingControlHelper.TRUE);
						CurrentSectionNumber ++;

						//show correct section
						SetSectionVisibility();
					}
					
					//Ensure all error messages are hidden.
					LabelUserSurveyPageErrorContinue.Visible = false;
					LabelUserSurveyPageErrorSubmit.Visible = false;
				}
			}

		}

		/// <summary>
		/// Move user to next section of the User Survey form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonContinue_Click(object sender, EventArgs e)
		{
			//IR1767 Enable all validation controls during this event
			UserSurveyQ2RadioRFValidator.Enabled = true;
			UserSurveyQ3CheckRFValidator.Enabled = true;
			UserSurveyQ4CheckRFValidator.Enabled = true;
			UserSurveyQ5RadioRFValidator.Enabled = true;
			UserSurveyQ7RadioRFValidator.Enabled = true;
			
			if (UserSurveyQ7Radio.SelectedIndex <= 2)
			{
				//User selected a Yes response for Q7, enable Q8 Validation
				UserSurveyQ8CheckRFValidator.Enabled = true;
			}
			else
			{
				UserSurveyQ8CheckRFValidator.Enabled = false;
			}

			LabelUserSurveyPageErrorContinue.Enabled = true;
			LabelUserSurveyPageErrorSubmit.Enabled = true;

			Page.Validate();

			//Check whether any validation errors are present on the panel by testing
			//the ValidateOK return value. If true, navigate to the next panel.
			//If false, stay on this panel until the user corrects validation errors.

			if (ValidateOK(CurrentSectionNumber))
			{
				//increment section number indicator
				if (CurrentSectionNumber < 5)
				{
                    trackingHelper.AddTrackingParameter("UserSurvey", "ButtonContinuePage" + CurrentSectionNumber.ToString(), TrackingControlHelper.TRUE);
					CurrentSectionNumber ++;

					//show correct section
					SetSectionVisibility();
				}
			
				LabelUserSurveyPageErrorContinue.Visible = false;
				LabelUserSurveyPageErrorSubmit.Visible = false;
			}
			else
			{
				if (CurrentSectionNumber != 3)
				{
					LabelUserSurveyPageErrorContinue.Visible = true;
					LabelUserSurveyPageErrorSubmit.Visible = false;
				}
			}
		}


		/// <summary>
		/// Event that enables/disables the email1 and email2 texboxes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void UserSurveyContactRadio_SelectedIndexChanged(object sender, System.EventArgs e)
		{

			//IR1767 Disable all validation controls during this event
			UserSurveyQ14RadioRFValidator.Enabled = false;
			UserSurveyQ15RadioRFValidator.Enabled = false;
			UserSurveyQ16DropRFValidator.Enabled = false;
			UserSurveyContactRadioRFValidator.Enabled = false;
			UserSurveyEmailRegExpValidator.Enabled = false;
			UserSurveyEmailRFValidator.Enabled = false;
			UserSurveyEmailCompareValidator.Enabled = false;

			LabelUserSurveyPageErrorContinue.Enabled = false;
			LabelUserSurveyPageErrorSubmit.Enabled = false;

			//User clicks yes. Enable textboxes
			if (UserSurveyContactRadio.SelectedIndex == 0)
			{
				TextUserSurveyEmail1.Enabled = true;
				TextUserSurveyEmail2.Enabled = true;
				TextUserSurveyEmail1.BackColor = System.Drawing.Color.White;
				TextUserSurveyEmail2.BackColor = System.Drawing.Color.White;
				UserSurveyEmailRegExpValidator.Enabled = true;
				UserSurveyEmailRFValidator.Enabled = true;
				UserSurveyEmailCompareValidator.Enabled = true;
			}
			else //User clicked no. Disable textboxes
			{
				//if user has selected that they don not want to be contacted then disable the email controls
				TextUserSurveyEmail1.Text = string.Empty;
				TextUserSurveyEmail2.Text = string.Empty;
				TextUserSurveyEmail1.BackColor = System.Drawing.Color.Silver;
				TextUserSurveyEmail2.BackColor = System.Drawing.Color.Silver;
				TextUserSurveyEmail1.Enabled = false;
				TextUserSurveyEmail2.Enabled = false;
			}
		}


		/// <summary>
		/// Event that enables/disables Q8 Checkbox list and changes colour.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void UserSurveyQ7Radio_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//User selected a yes response. Enable Q8
			if (UserSurveyQ7Radio.SelectedIndex <= 2)
			{
				UserSurveyQ8Check.Enabled = true;

				//IR1769 Disable all validators (re-enabled on Continue button click)
				UserSurveyQ2RadioRFValidator.Enabled = false;
				UserSurveyQ3CheckRFValidator.Enabled = false;
				UserSurveyQ4CheckRFValidator.Enabled = false;
				UserSurveyQ5RadioRFValidator.Enabled = false;
				UserSurveyQ7RadioRFValidator.Enabled = false;
				UserSurveyQ8CheckRFValidator.Enabled = false;

				LabelUserSurveyPageErrorContinue.Visible = false;
				LabelUserSurveyPageErrorSubmit.Visible = false;

				Page.Validate();

			}
			else //User has selected a no response, disable Q8
			{
				//IR1769 Disable all validators (re-enabled on Continue button click)
				UserSurveyQ2RadioRFValidator.Enabled = false;
				UserSurveyQ3CheckRFValidator.Enabled = false;
				UserSurveyQ4CheckRFValidator.Enabled = false;
				UserSurveyQ5RadioRFValidator.Enabled = false;
				UserSurveyQ7RadioRFValidator.Enabled = false;
				UserSurveyQ8CheckRFValidator.Enabled = false;

				LabelUserSurveyPageErrorContinue.Visible = false;
				LabelUserSurveyPageErrorSubmit.Visible = false;
				
				//if user has selected no to Q7, diable Q8
				UserSurveyQ8Check.ClearSelection();
				UserSurveyQ8Check.Enabled = false;
				UserSurveyQ8CheckRFValidator.Enabled = false;
				
				Page.Validate();
			}

		}
				

		#endregion

		

		#region properties

		/// <summary>
		/// Holds the number of the section currently being displayed
		/// </summary>		
		public int CurrentSectionNumber
		{
			get
			{
				return (int)ViewState["currentSectionNumber"];
			}
			set
			{
				ViewState["currentSectionNumber"] = value; 
			}
		}

		private Hashtable Q9Answers
		{
			get { return (Hashtable)ViewState["q9Answers"]; }
			set { ViewState["q9Answers"] = value; }
		}


		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraEventWireUp();
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
			ButtonContinuePage1.Click += new EventHandler(this.ButtonContinue_Click);
			ButtonContinuePage2.Click += new EventHandler(this.ButtonContinue_Click);
			ButtonSubmit.Click += new EventHandler(this.ButtonSubmit_Click);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ID = "UserSurveyQ2RadioValidator";
            UserSurveyQ7Radio.SelectedIndexChanged += new EventHandler(UserSurveyQ7Radio_SelectedIndexChanged);
            UserSurveyContactRadio.SelectedIndexChanged += new EventHandler(UserSurveyContactRadio_SelectedIndexChanged);
            trackingHelper = new TrackingControlHelper();
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
	}
}
