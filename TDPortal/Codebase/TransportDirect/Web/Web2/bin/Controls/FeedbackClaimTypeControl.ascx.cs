// *********************************************** 
// NAME                 : FeedbackClaimTypeControl.aspx.cs 
// AUTHOR               : Halim Ahad
// DATE CREATED         : 22/09/2003 
// DESCRIPTION          : Control to allow user to
//                        submit feedback type on the TDPortal  
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FeedbackClaimTypeControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:20:10   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:38   mturner
//Initial revision.
//
//   Rev 1.14   Feb 23 2006 19:16:32   build
//Automatically merged from branch for stream3129
//
//   Rev 1.13.1.1   Jan 30 2006 14:41:04   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.13.1.0   Jan 10 2006 15:24:22   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.13   Jul 23 2004 11:48:24   passuied
//Changes to add GetResource Method in TDPage and TDUserControl to ease access to resources. Also removal of local GetResouce in controls and pages
//
//   Rev 1.12   Mar 31 2004 09:54:28   AWindley
//DEL5.2 QA Changes: Resolution for 684
//
//   Rev 1.11   Mar 09 2004 12:44:18   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.10   Jan 05 2004 09:17:22   asinclair
//Updated to fix IR553
//
//   Rev 1.9   Nov 21 2003 12:40:32   hahad
//Fixed Java Script problem
//
//   Rev 1.8   Oct 28 2003 11:38:22   asinclair
//Updated control
//
//   Rev 1.7   Oct 17 2003 15:46:52   asinclair
//Using DataServices to populate drop downs
//
//   Rev 1.6   Oct 15 2003 14:19:56   asinclair
//Changed dropdown .visible to .enabled
//
//   Rev 1.5   Oct 01 2003 15:58:28   hahad
//Comments Added
//
//   Rev 1.4   Oct 01 2003 13:41:22   hahad
//Updated Dropdown Item to conform with new spec updates
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	///		User Selection Control that gains feedback via 3 radio button category headings then 
	///		a Drop down list related to the categories.  
	/// </summary>
	public partial  class FeedbackClaimTypeControl : TDUserControl
	{		
		
		private const string ICON_UNCHECKED_RB = "Feedbackpage.imageRadioButtonUnchecked";
		private const string ICON_CHECKED_RB = "Feedbackpage.imageRadioButtonChecked";
		//Controls used in the CustomControl
		private System.Web.UI.WebControls.ImageButton[] imageButtonArray;
		private System.Web.UI.WebControls.Label[] labelArray;
		private System.Web.UI.WebControls.DropDownList[] dropdownArray;
		public string selectedFeedBackCategory;
		
		/// <summary>
		/// Page load event for the FeedbackClaimTypeControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			imageButtonArray = new System.Web.UI.WebControls.ImageButton[3];
			imageButtonArray[0] = imageButtonProblemUnselected;
			imageButtonArray[1] = imageButtonInformationUnselected;
			imageButtonArray[2] = imageButtonSuggestionUnselected;

			labelArray = new System.Web.UI.WebControls.Label[3];
			labelArray[0] = labelProblem;
			labelArray[1] = labelInformation;
			labelArray[2] = labelSuggestion;

			dropdownArray = new System.Web.UI.WebControls.DropDownList[3];
			dropdownArray[0] = ProblemDropDownList;
			dropdownArray[1] = InformationDropDownList;
			dropdownArray[2] = SuggestionDropDownList;
		
			imageButtonArray[0].CommandArgument = "0";
			
			imageButtonProblemUnselected.ImageUrl = GetResource(ICON_CHECKED_RB);
			imageButtonInformationUnselected.ImageUrl = GetResource(ICON_UNCHECKED_RB);
			imageButtonSuggestionUnselected.ImageUrl = GetResource(ICON_UNCHECKED_RB);

			labelProblem.Text = Global.tdResourceManager.GetString("FeedbackClaimTypeControl.labelProblem", TDCultureInfo.CurrentUICulture);
			labelInformation.Text = Global.tdResourceManager.GetString("FeedbackClaimTypeControl.labelInformation", TDCultureInfo.CurrentUICulture);
			labelSuggestion.Text = Global.tdResourceManager.GetString("FeedbackClaimTypeControl.labelSuggestion", TDCultureInfo.CurrentUICulture);

			imageButtonProblemUnselected.AlternateText = labelProblem.Text;
			imageButtonInformationUnselected.AlternateText = labelInformation.Text;
			imageButtonSuggestionUnselected.AlternateText = labelSuggestion.Text;

			// Labels to be read by Screen Reader only
			labelSRProblem.Text = Global.tdResourceManager.GetString("FeedbackClaimTypeControl.labelSRProblem", TDCultureInfo.CurrentUICulture);
			labelSRInformation.Text = Global.tdResourceManager.GetString("FeedbackClaimTypeControl.labelSRInformation", TDCultureInfo.CurrentUICulture);
			labelSRSuggestion.Text = Global.tdResourceManager.GetString("FeedbackClaimTypeControl.labelSRSuggestion", TDCultureInfo.CurrentUICulture);

			InformationDropDownList.Enabled = false;
			SuggestionDropDownList.Enabled = false;
						
			// Populate the drop down lists from DataServices
            if (!IsPostBack)
            {
                DataServices.DataServices populator = (DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
                populator.LoadListControl(DataServices.DataServiceType.ProblemDropDownList, ProblemDropDownList);
                populator.LoadListControl(DataServices.DataServiceType.InformationDropDownList, InformationDropDownList);
                populator.LoadListControl(DataServices.DataServiceType.SuggestionDropDownList, SuggestionDropDownList);
            }
			// Populate the RadioButtonList
//				CategoryRadioButtonList.Items.Add(Global.tdResourceManager.GetString("FeedbackClaimTypeControl1.CategoryRadioButtonListItem1", TDCultureInfo.CurrentUICulture));
//				CategoryRadioButtonList.Items.Add(Global.tdResourceManager.GetString("FeedbackClaimTypeControl1.CategoryRadioButtonListItem2", TDCultureInfo.CurrentUICulture));
//				CategoryRadioButtonList.Items.Add(Global.tdResourceManager.GetString("FeedbackClaimTypeControl1.CategoryRadioButtonListItem3", TDCultureInfo.CurrentUICulture));
			
			//Set the RadioButtonList on the first item
			//CategoryRadioButtonList.SelectedIndex = 0;
			

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.imageButtonProblemUnselected.Click += new System.Web.UI.ImageClickEventHandler(this.imageButtonProblemUnselected_Click);
			this.imageButtonInformationUnselected.Click += new System.Web.UI.ImageClickEventHandler(this.imageButtonInformationUnselected_Click);
			this.imageButtonSuggestionUnselected.Click += new System.Web.UI.ImageClickEventHandler(this.imageButtonSuggestionUnselected_Click);

		}
		#endregion

		/// <summary>
		/// CategoryRadioList selected Changed Event - Fires when CategoryRadioList is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		
		private void SwitchFeedbackCategory(int toCategory)
		{
			
			int fromCategory = Convert.ToInt32(imageButtonArray[0].CommandArgument);

			if (toCategory != fromCategory)
			{
				imageButtonArray[fromCategory].ImageUrl = GetResource(ICON_UNCHECKED_RB);
				dropdownArray[fromCategory].Enabled = false;
				imageButtonArray[toCategory].ImageUrl = GetResource(ICON_CHECKED_RB);
				dropdownArray[toCategory].Enabled = true;
				imageButtonArray[0].CommandArgument = toCategory.ToString();
			}
		}
				

		
		//Click Events for the image button clicks
		private void imageButtonProblemUnselected_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SwitchFeedbackCategory(0);
			selectedFeedBackCategory ="1";
		}

		private void imageButtonInformationUnselected_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SwitchFeedbackCategory(1);
			selectedFeedBackCategory ="2";
		}

		private void imageButtonSuggestionUnselected_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SwitchFeedbackCategory(2);
			selectedFeedBackCategory ="3";
		}

		/// <summary>
		/// Returns the string value of the relevent dropdownlist.  Used when sending feedback form e-mail.
		/// </summary>
		public string SelectedCategoryFeedback
		{		
			get
			{
				int SelectedDropdownInt = Convert.ToInt32(imageButtonArray[0].CommandArgument);
				return dropdownArray[SelectedDropdownInt].SelectedItem.Text;
			}
		}
		

	
		/// <summary>
		/// Get Properties of the ProblemRadioButton.  Used when sending feedback form e-mail.
		/// </summary>
		public String CategoryRadioButtonString
		{
			get
			{
				int RadioButtonInt = Convert.ToInt32(imageButtonArray[0].CommandArgument);
				return labelArray[RadioButtonInt].Text;
			}
		}

		/// Get the int of the RadioButton (image button) that is selected.  Used to send e-mail to DfT.
		/// </summary>
		public int CategoryRadioButtonInt
		{
			get
			{
				int RadioButtonInt = Convert.ToInt32(imageButtonArray[0].CommandArgument);
				return RadioButtonInt;
			}
		}

	}
}
