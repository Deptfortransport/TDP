// ***************************************************************
// NAME                 : SearchTypeControl.ascx
// AUTHOR               : 
// DATE CREATED         : 
// DESCRIPTION			: 
// ****************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/SearchTypeControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:58   mturner
//Drop3 from Dev Factory

//   Rev DevFactory   Feb 25 2008 17:00:00   mmodi
//Updated to display icons

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.SessionManager;

	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	///	SearchTypeControl allows the user to select either a time of cost
	///	based journey. Contains a simple dropdown list to enable selection
	///	and an OK button. Event SearchTypeChanged raised when list selection
	///	is changed. Properties include read/write CostSearch (bool) which
	///	allows/determines selection of drop down list.
	/// </summary>
	public partial  class SearchTypeControl : TDUserControl
	{
		#region Member declaration

		public event EventHandler SearchTypeChanged;

		//Constants for resource management.
		private const string instructionLabelKey = "SearchTypeControl.instructionLabel";
		private const string okButtonTextKey = "loginPanel.OkBtn.Text";
		
		// Page level variables
		private TDResourceManager rm = null;
		public const string FIND_A_FARE_RM = "FindAFare";
		#endregion

		#region Constructor
		public SearchTypeControl()
		{
			LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
		}
		#endregion

		#region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//Set up labels
			rm = Global.tdResourceManager;
			instructionLabel.Text = rm.GetString(instructionLabelKey, TDCultureInfo.CurrentUICulture);
			okButton.Text = rm.GetString(okButtonTextKey, TDCultureInfo.CurrentCulture);

			//Set up OK image button and JavaScript on radio buttons
			ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
			Page.ClientScript.RegisterStartupScript(typeof(SearchTypeControl), timeRadioButton.ScriptName, scriptRepository.GetScript(timeRadioButton.ScriptName, ((TDPage)Page).JavascriptDom));

			timeRadioButton.EnableClientScript = true;
			costRadioButton.EnableClientScript = true;

			// Attempt to hide OK button using clientside JavaScript
			JavaScriptAdapter.InitialiseControlVisibility(okButton, false);
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
			this.EnableViewState = false;

		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Read/Write. Gets or sets the value of the Search Type Drop list.
		/// Set to TRUE for cost search or FALSE for time search.
		/// </summary>
		public bool CostSearch
		{
			get
			{
				return costRadioButton.Checked;
			}
			set
			{
				costRadioButton.Checked = value;
				timeRadioButton.Checked = !value;
			}
		}
		#endregion

		#region Private Event Handling
		private void searchTypeRadioButtons_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//Raise event SearchTypeChanged
			//Check that a subscriber has been registered to the event. If
			//so raise the event passing this as the sender and an empty
			//system.eventargs
			if (SearchTypeChanged != null)
				SearchTypeChanged(this,System.EventArgs.Empty);
		}

        /// <summary>
        /// Event handler for when Time icon click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageTime_Click(object sender, EventArgs e)
        {
            this.CostSearch = false;

            if (SearchTypeChanged != null)
                SearchTypeChanged(this, System.EventArgs.Empty);
        }

        /// <summary>
        /// Event handler for Cost icon click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageCost_Click(object sender, EventArgs e)
        {
            this.CostSearch = true;

            if (SearchTypeChanged != null)
                SearchTypeChanged(this, System.EventArgs.Empty);
        }

		#endregion

		#region Page Pre Render
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			//Set the value of the search type radio buttons. This is dependant on
			//a number of factors.
			switch (TDSessionManager.Current.FindAMode)
			{
				case FindAMode.Fare :
				case FindAMode.RailCost :
					CostSearch = true;	//Theoretically this control wont be displayed in this mode;
					break;
				case FindAMode.TrunkCostBased :
					CostSearch = true;	//True will set radio button to "cost"
					break;
				case FindAMode.Trunk :
					CostSearch = false;	//False will set radio button to "time"
					break;
				default :
					CostSearch = false; //Default is to set this to "time".
					break;
			}

			//Set the labels for time/cost
			timeLabel.Text = GetResource("SearchTypeControl.RadioButtons.Time");
			costLabel.Text = GetResource("SearchTypeControl.RadioButtons.Cost");

            imageTime.ImageUrl = GetResource("SearchTypeControl.imageTime.URL");
            imageTime.AlternateText = GetResource("SearchTypeControl.imageTime.AlternateText");
            imageCost.ImageUrl = GetResource("SearchTypeControl.imageCost.URL");
            imageCost.AlternateText = GetResource("SearchTypeControl.imageCost.AlternateText");
		}
		#endregion

		protected void Page_Init(object sender, System.EventArgs e)
		{
			timeRadioButton.CheckedChanged += new System.EventHandler(this.searchTypeRadioButtons_SelectedIndexChanged);
			costRadioButton.CheckedChanged += new System.EventHandler(this.searchTypeRadioButtons_SelectedIndexChanged);
            imageCost.Click += new System.Web.UI.ImageClickEventHandler(imageCost_Click);
            imageTime.Click += new System.Web.UI.ImageClickEventHandler(imageTime_Click);
		}

	}
}
