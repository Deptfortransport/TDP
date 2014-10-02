// *********************************************** 
// NAME                 : FeedbackTypeControl.aspx.cs 
// AUTHOR               : Halim Ahad
// DATE CREATED         : 15/07/2003 
// DESCRIPTION			: A custom user control to 
// display a list of feedback types 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FeedbackTypeControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:20:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:54   mturner
//Initial revision.
//
//   Rev 1.23   Feb 23 2006 19:16:32   build
//Automatically merged from branch for stream3129
//
//   Rev 1.22.1.1   Jan 30 2006 14:41:04   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.22.1.0   Jan 10 2006 15:24:26   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.22   Aug 20 2003 12:41:30   JMorrissey
//Removed align="left" tags which are not required
//
//   Rev 1.21   Aug 19 2003 11:23:44   hahad
//Old comments removed
//
//   Rev 1.20   Aug 12 2003 17:55:46   hahad
//Anti hacking Validation Added
//
//   Rev 1.19   Aug 11 2003 11:21:12   hahad
//Checking in from Friday's Power Failure 08/08/03
//
//   Rev 1.18   Aug 07 2003 13:03:04   hahad
//Documentation Comments added
//
//   Rev 1.17   Jul 29 2003 14:59:10   hahad
//Autopostback set to false
//
//   Rev 1.16   Jul 29 2003 12:15:04   hahad
//Comments Updated
//
//   Rev 1.15   Jul 28 2003 15:29:54   hahad
//Control now Validates when slected item is changed
//
//   Rev 1.14   Jul 28 2003 13:02:10   hahad
//feedbackTypeValidator now looks at the index value for validation of the control
//
//   Rev 1.13   Jul 28 2003 11:32:32   hahad
//Removed bulid errors (unused variables0
//
//   Rev 1.12   Jul 25 2003 16:17:04   hahad
//protected text fields changed to private as recommended by FXcop
//
//   Rev 1.11   Jul 25 2003 16:13:52   hahad
//No changes
//
//   Rev 1.10   Jul 25 2003 15:46:36   hahad
//Hard Coded DropdownList Values and added language control
//
//   Rev 1.9   Jul 25 2003 11:34:08   hahad
//Comments Updated
//
//   Rev 1.8   Jul 24 2003 16:50:52   hahad
//FXcop changes unchanged
//
//   Rev 1.7   Jul 24 2003 16:04:00   hahad
//reverted changes suggested by FXcop (private to protected)
//
//   Rev 1.6   Jul 24 2003 14:28:42   hahad
//Changed feedbackTypeLabel & dropDownFeedback from Protected to Private (FXcop)
//
//   Rev 1.5   Jul 23 2003 13:05:34   AWindley
//updated controls to be language sensitive
//
//   Rev 1.4   Jul 22 2003 09:08:48   AWindley
//Changed TDLanguageManager to tdResourceManager
//
//   Rev 1.3   Jul 17 2003 15:56:42   hahad
//Updated Custom Validation
//
//   Rev 1.2   Jul 17 2003 12:09:56   hahad
//Added Custom Validator
//
//   Rev 1.1   Jul 17 2003 10:06:20   hahad
//Attempting to check in yesterdays work after file were deleted
//
//   Rev 1.3   Jul 16 2003 10:45:22   hahad
//Updated with comments
//
//   Rev 1.2   Jul 16 2003 10:33:22   hahad
//Formatted control into a table
//
//   Rev 1.1   Jul 16 2003 10:01:58   hahad
//Added Default 'Please Select' in Dropdown selection
//
//   Rev 1.0   Jul 15 2003 15:48:18   hahad
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;


	/// <summary>
	///		Summary description for FeedbackTypeControl.
	/// </summary>
	public partial  class FeedbackTypeControl : TDUserControl
	{
		// Control Label
		// Control DropDownList

		
		/// <summary>
		/// Page Load event for FeedbackTypeControl, sets language context and Dropdown list items
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//Add dropDownFeedback attributes to the dropdownlist, these take in a string that has langauage control applied on it
			//This Populates the Control DropDownLsit
			dropDownFeedback.Items.Add(Global.tdResourceManager.GetString("FeedbackTypeControl.dropDownDefault", TDCultureInfo.CurrentUICulture));
			dropDownFeedback.Items.Add(Global.tdResourceManager.GetString("FeedbackTypeControl.dropDownTextOption1", TDCultureInfo.CurrentUICulture));
			dropDownFeedback.Items.Add(Global.tdResourceManager.GetString("FeedbackTypeControl.dropDownTextOption2", TDCultureInfo.CurrentUICulture));
			dropDownFeedback.Items.Add(Global.tdResourceManager.GetString("FeedbackTypeControl.dropDownTextOption3", TDCultureInfo.CurrentUICulture));
			dropDownFeedback.Items.Add(Global.tdResourceManager.GetString("FeedbackTypeControl.dropDownTextOption4", TDCultureInfo.CurrentUICulture));

			//Set language control for the feedbackTypeLabel
			feedbackTypeLabel.Text =Global.tdResourceManager.GetString("FeedbackTypeControl.feedbackTypeLabel", TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Get and Set properties of dropDownFeedback Text Values
		/// </summary>
		//The get and set properties for the dropdownlist (dropDownFeedback) Text values
		public string FeedBackText
		{
			get
			{
				return dropDownFeedback.SelectedItem.Text;
			}
			set
			{
				dropDownFeedback.SelectedItem.Text = value;
			}
		}
		
		/// <summary>
		/// Get and Set properties of dropDownFeedback Index Values
		/// </summary>
		//The get and set properties for the dropdownlist (dropDownFeedback) Index values
		public int FeedBackIndex
		{
			get
			{
				return dropDownFeedback.SelectedIndex;
			}
			set
			{
				dropDownFeedback.SelectedIndex = value;
			}
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

		}
		#endregion

	
		

		
	}
}
