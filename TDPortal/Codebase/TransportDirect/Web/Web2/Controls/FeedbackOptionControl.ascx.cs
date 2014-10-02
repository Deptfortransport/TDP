// *********************************************** 
// NAME                 : FeedbackOptionsControl.aspx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 04/01/2007
// DESCRIPTION          : Control to allow user to choose type of feedback
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FeedbackOptionControl.ascx.cs-arc  $
//
//   Rev 1.3   Jan 16 2009 13:27:22   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:20:18   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:48   mturner
//Initial revision.
//
//   Rev 1.3   Jan 24 2007 12:47:04   mmodi
//Added button to provide alternative functionality when Javascript disabled
//Resolution for 4332: Contact Us Improvements - workstream
//Resolution for 4342: Contact Us: Does not work when Javascript disabled
//
//   Rev 1.2   Jan 17 2007 18:08:28   mmodi
//Added screen reader control
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.1   Jan 12 2007 14:13:56   mmodi
//Updated code as part of development
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.0   Jan 08 2007 10:24:26   mmodi
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
	///		Summary description for FeedbackOptionControl.
	/// </summary>
	public partial class FeedbackOptionControl : TDUserControl
	{
		#region Controls

		protected HyperlinkPostbackControl hyperlinkNext;

		#endregion

		#region Private variables

		private IDataServices populator;
		private InputPageState inputPageState;
		private int feedbackOptionSelected;

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
            if (!Page.IsPostBack)
            {
                // Only set the selected item first time page is loaded
                feedbackOptionSelected = -1;
                populator.LoadListControl(DataServiceType.UserFeedbackType, feedbackOptionsRadioButtonList);
            }
			
            feedbackOptionTitle.Text = GetResource("FeedbackOptionControl.Title.Text");
			
			// Labels to be read by Screen Reader only
			labelSRSelectOption.Text = GetResource("FeedbackControl.SelectOption.Text");
				
			inputPageState = TDSessionManager.Current.InputPageState;

			// Hyperlink button displayed when Javascript has been disabled on user browser
			hyperlinkNext.Text = GetResource("UserSurvey.ButtonSubmit.Text");
		
			if ((this.Page as TDPage).IsJavascriptEnabled)
			{
				hyperlinkNext.Visible = false;				
			}
			else
			{
				hyperlinkNext.Visible = true;
				hyperlinkNext.ShowLabelForNonJS = false;
			}
		}

		#endregion

		#region Private Event handlers

		/// <summary>
		/// Option type list selected index event handler
		/// </summary>
		protected void feedbackOptionsRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
		{
            feedbackOptionSelected = feedbackOptionsRadioButtonList.SelectedIndex;

			// Set the Feedback page state to session
			switch (feedbackOptionSelected)
			{
				case 0:
					inputPageState.FeedbackPageState = FeedbackState.Problem;
					break;
				case 1:
					inputPageState.FeedbackPageState = FeedbackState.Suggestion;
					break;
				default:
					inputPageState.FeedbackPageState = FeedbackState.Initial;
					break;
			}

			TDSessionManager.Current.InputPageState = inputPageState;
		}

		/// <summary>
		/// Method handles the click event from the hyperlinkNext
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hyperlinkNext_link_Clicked(object sender, EventArgs e)
		{
			// Do nothing - we just want the page to postback
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Read only property. Returns the Feedback type option selected - Selected index, as int
		/// </summary>
		public int FeedbackOptionSelected
		{
			get { return feedbackOptionsRadioButtonList.SelectedIndex; }
		}

		/// <summary>
		/// Read only property. Returns the Feedback type option selected - Selected item, as string
		/// </summary>
		public string FeedbackOptionSelectedItem
		{
			get { return populator.GetValue(DataServiceType.UserFeedbackType, feedbackOptionsRadioButtonList.SelectedItem.Value); } 
		}

		/// <summary>
		/// Read only property. Returns the Feedback type option selected - Selected value, as string
		/// </summary>
		public string FeedbackOptionSelectedValue
		{
			get { return feedbackOptionsRadioButtonList.SelectedValue; }
		}

		/// <summary>
		/// Resets all the options to their defaults
		/// </summary>
		public void Initialise()
		{
			feedbackOptionsRadioButtonList.SelectedIndex = -1;
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
			this.hyperlinkNext.link_Clicked += new EventHandler(this.hyperlinkNext_link_Clicked);
            this.feedbackOptionsRadioButtonList.SelectedIndexChanged += new EventHandler(feedbackOptionsRadioButtonList_SelectedIndexChanged);
        }
		#endregion
	}
}
