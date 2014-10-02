// *********************************************** 
// NAME                 : AdjustOptionsControl.ascx.cs 
// AUTHOR               : Paul Cross
// DATE CREATED         : 01/02/2006
// DESCRIPTION			: A user control to offer options in adjusting the timings of an existing journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AdjustOptionsControl.ascx.cs-arc  $
//
//   Rev 1.4   Jan 13 2009 11:40:42   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 04 2008 10:49:56   apatel
//updated populate list method to retail the index of adjusttiming dropdown
//
//   Rev 1.2   Mar 31 2008 13:18:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:44   mturner
//Initial revision.
//
//Rev DevFactory Feb 13th 14:35:00 dgath
//Removed all references to cancelButton, as this has now been moved to JourneyAdjust.aspx page.
//
//   Rev 1.11   Apr 06 2006 16:49:16   asinclair
//Removed the label between the two dropdowns
//Resolution for 3822: DN068 Adjust: Changes requested by Ben
//
//   Rev 1.10   Mar 28 2006 12:57:32   pcross
//Reduced number of characters allowed in the locations dropdown to prevent wrapping onto next line
//Resolution for 3736: Location dropdown forced onto next line on Journey Adjust input
//
//   Rev 1.9   Mar 14 2006 19:49:56   NMoorhouse
//fix stream3353 merge issues
//
//   Rev 1.8   Mar 14 2006 13:20:44   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Mar 08 2006 18:15:44   pcross
//Reversed suggested fxCop change
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Mar 08 2006 18:06:20   pcross
//FxCop changes 
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Mar 08 2006 15:58:04   RGriffith
//FxCop Suggested Changes
//
//   Rev 1.4   Mar 02 2006 14:58:36   pcross
//Added 'please select from dropdown' type entry for location and timings dropdowns
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 24 2006 10:37:58   pcross
//Minor
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 16 2006 10:20:10   pcross
//Interim check in
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 10 2006 10:40:30   pcross
//Interim check-in
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 03 2006 14:46:22   pcross
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Globalization;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web.Adapters;

	/// <summary>
	///		Summary description for AdjustOptionsControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class AdjustOptionsControl : TDPrintableUserControl
	{

		#region Control Declarations


		#endregion

		#region Private Member Variables
		
		private IDataServices populator;
		private PublicJourney publicJourney;
		private FindAMode findMode;
		private bool arriveBefore;

		private const string InterchangeIndicator = "_interchange";
		private const int MaxLocationTextLength = 42;
		private const string UNREFINED_TEXT = "unrefined";

		#endregion

		#region Properties

		/// <summary>
		/// Read only property. Exposes the Next button to parent pages.
		/// </summary>
		public TDButton NextButton
		{
			get { return nextButton; }
		}

		/// <summary>
		/// Read only property. Exposes the adjust locations dropdown to parent pages.
		/// </summary>
		public ScriptableDropDownList AdjustLocations
		{
			get { return adjustLocations; }
		}

		/// <summary>
		/// Read only property. Exposes the adjust timings dropdown to parent pages.
		/// </summary>
		public ScriptableDropDownList AdjustTimings
		{
			get { return adjustTimings; }
		}
		
		/// <summary>
		/// Read/write property. The journey whose intermediate legs are to be displayed in the adjust locations dropdown.
		/// </summary>
		public PublicJourney PublicJourney
		{
			get
			{
				return this.publicJourney;
			}
			set
			{
				this.publicJourney = value;
			}
		}

		/// <summary>
		/// Read/write property. If single mode of transport is know (from FindA search) then set to that, else set to None.
		/// </summary>
		public FindAMode FindMode
		{
			get
			{
				return findMode;
			}
			set
			{
				findMode = value;
			}
		}

		/// <summary>
		/// Read/write property. True indicates the search was for journeys that arrive before given time. False indicates leave after.
		/// </summary>
		public bool ArriveBefore
		{
			get
			{
				return arriveBefore;
			}
			set
			{
				arriveBefore = value;
			}
		}

		#endregion

		#region Initialisation

		/// <summary>
		/// Contructor for control
		/// </summary>
		public AdjustOptionsControl()
		{
			// Set the resource file for the control
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#endregion

		#region Private methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Populate text in labels, etc
			PopulateStaticControls();

			// Populate the dropdownlists
			PopulateDropdowns();

			// Set up the script action on the location dropdown
			SetupDropdownsSelectionAction();

			// Add client side javascript to allow DHTML update of legs selected for adjust
			AddClientForElementHighlighting();

		}
		
		/// <summary>
		/// Registers the client side script.
		/// </summary>
		private void AddClientForElementHighlighting()
		{
			TDPage tdPage = (TDPage)this.Page;
			if (tdPage.IsJavascriptEnabled)
			{
				string javaScriptFileName = "JourneyAdjustElementSelection";
				string javaScriptDom = tdPage.JavascriptDom;
				ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
				// Output reference to necessary JavaScript file from the ScriptRepository
				this.Page.ClientScript.RegisterClientScriptBlock(typeof(AdjustOptionsControl), javaScriptFileName, scriptRepository.GetScript(javaScriptFileName, javaScriptDom));                				
			}
		}

		/// <summary>
		/// Sets up the script action on the location dropdown
		/// </summary>
		private void SetupDropdownsSelectionAction()
		{
			int legCount = publicJourney.JourneyLegs.Length;

			adjustLocations.Action = "HighlightLocationSelection('" + legCount.ToString(CultureInfo.InvariantCulture) + "', '" + arriveBefore.ToString(CultureInfo.InvariantCulture) + "')";
			adjustTimings.Action = "HighlightLocationSelection('" + legCount.ToString(CultureInfo.InvariantCulture) + "', '" + arriveBefore.ToString(CultureInfo.InvariantCulture) + "')";
		}

		/// <summary>
		/// Populates the text in the labels, etc using the resource manager
		/// </summary>
		private void PopulateStaticControls()
		{
			adjustPreText.Text = GetResource("AdjustOptionsControl.PreLabel.Text");
			//adjustMiddleText.Text = GetResource("AdjustOptionsControl.MiddleLabel.Text");
			nextButton.Text = GetResource("AdjustOptionsControl.NextButton.Text");
		}

		/// <summary>
		/// Populates the text in the labels, etc using the resource manager
		/// </summary>
		private void PopulateDropdowns()
		{
			PopulateAdjustTimingsDropdown();
			PopulateAdjustLocationsDropdown();
		}

		/// <summary>
		/// Populates the Adjust dropdowns list from the journey supplied
		/// </summary>
		private void PopulateAdjustTimingsDropdown()
		{
            int adjusttimingsindex = adjustTimings.SelectedIndex;
			// Update the adjust timings dropdown list with the data specified by the supplied list type
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			populator.LoadListControl(DataServiceType.JourneyAdjustmentTypes, adjustTimings, this.resourceManager);

			// We have the timings from the db but there are tailorable waiting options depending on the findamode
			// used to get the results:

			// Get the journey adjust increment from the database for the current transport mode
			string adjustIncrementType = String.Empty;
			if (findMode.ToString() == "None")
				adjustIncrementType = "Default";
			else
				adjustIncrementType = findMode.ToString();

			string incrementProperty = "JourneyAdjustIncrement." + adjustIncrementType;
			int journeyAdjustIncrement = Convert.ToInt32(Properties.Current[incrementProperty], CultureInfo.InvariantCulture);

			// The increment multiplier allows multiple durations based on a single seed duration
			int incrementMultiplier = 0;
			int adjustIncrement = 0;
			foreach (ListItem timingsItem in adjustTimings.Items)
			{
				// If this is a tailorable option then update with the appropriate duration
				if (timingsItem.Text.IndexOf("{0}", 0) > 0)
				{
					incrementMultiplier++;
					adjustIncrement = journeyAdjustIncrement*incrementMultiplier;
					timingsItem.Text = String.Format(TDCultureInfo.InvariantCulture, timingsItem.Text, adjustIncrement.ToString(CultureInfo.InvariantCulture));
					timingsItem.Value = adjustIncrement.ToString(CultureInfo.InvariantCulture);
				}
			}

			// Add the unselected instructional text as the 1st item
			string pleaseSelect = GetResource("AdjustOptionsControl.TimingsDropdown.itemPleaseSelect");
			ListItem itemPleaseSelect = new ListItem(pleaseSelect, UNREFINED_TEXT);
			adjustTimings.Items.Insert(0, itemPleaseSelect);

            adjustTimings.SelectedIndex = adjusttimingsindex;
		}

		/// <summary>
		/// Populates the Adjust dropdowns list from the journey supplied
		/// </summary>
		private void PopulateAdjustLocationsDropdown()
		{
			// Values in the adjustLocations dropdown from Journey property.
			
			// Only showing intermediate legs (otherwise would be adjusting whole journey - may as well start again).

			// The value for each location is the number of the leg where location is start of. It is zero based but start
			// location of 1st leg is excluded from dropdown.
			// Where value is suffixed by "_interchange" this location represents the start of an interchange (which doesn't
			// have a leg of its own)
			ResultsAdapter resultsAdapter = new ResultsAdapter();
			string locationDescription = String.Empty;
			for (int detailIndex = 0; detailIndex <= publicJourney.Details.Length - 1; detailIndex++)
			{
				if (detailIndex > 0)
				{
					// If preceded by interchange then get these details before getting current leg details
					if (resultsAdapter.AtInterchange(publicJourney, detailIndex))
					{
						locationDescription = TruncateString(publicJourney.Details[detailIndex - 1].LegEnd.Location.Description, MaxLocationTextLength);
						// Include location at the end of the previous leg
						adjustLocations.Items.Add(new ListItem(locationDescription,
							(detailIndex).ToString(CultureInfo.InvariantCulture) + InterchangeIndicator));
					}
					locationDescription = TruncateString(publicJourney.Details[detailIndex].LegStart.Location.Description, MaxLocationTextLength);
					adjustLocations.Items.Add(new ListItem(locationDescription,
						(detailIndex).ToString(CultureInfo.InvariantCulture)));
				}
			}

			// Add the unselected instructional text as the 1st item
			string pleaseSelect = GetResource("AdjustOptionsControl.LocationDropdown.itemPleaseSelect");
			ListItem itemPleaseSelect = new ListItem(pleaseSelect, UNREFINED_TEXT);
			adjustLocations.Items.Insert(0, itemPleaseSelect);

		}

		/// <summary>
		/// Truncates a string and adds "..."
		/// </summary>
		/// <param name="inputString">String to truncate</param>
		/// <param name="length">Number of characters required in the final string</param>
		/// <returns>Truncated string</returns>
		/// <remarks>Used to truncate location description to allow dropdown to fit on line</remarks>
		private string TruncateString(string inputString, int length)
		{
			if (inputString.Length > length)
				return inputString.Substring(0, (length-3)) + "...";
			else
				return inputString;
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