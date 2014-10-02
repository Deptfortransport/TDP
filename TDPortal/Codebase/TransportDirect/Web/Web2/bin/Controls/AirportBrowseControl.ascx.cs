// *********************************************** 
// NAME                 : AirportBrowseControl.ascx.cs 
// AUTHOR               : Jonathan George 
// DATE CREATED         : 07/05/2004
// DESCRIPTION			: Allows user to select airports
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AirportBrowseControl.ascx.cs-arc  $
//
//   Rev 1.4   May 01 2008 17:20:26   mmodi
//No change.
//Resolution for 4925: Control alignments: Find a flight
//
//   Rev 1.3   May 01 2008 17:10:28   mmodi
//Formatting improved.
//Resolution for 4925: Control alignments: Find a flight
//
//   Rev 1.2   Mar 31 2008 13:18:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:46   mturner
//Initial revision.
//
//   Rev 1.12   Feb 23 2006 16:10:08   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.11   Jan 11 2006 17:11:00   mguney
//JavaScript check is omitted in UseJavaScript property, because  the jscript functionality is needed immediately. Can't wait for the post-back.
//Resolution for 3096: UEE - Find a Flight - tick boxes for airports in a region removed
//
//   Rev 1.10   Nov 14 2005 18:30:40   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.9   Nov 03 2005 16:16:46   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.8.1.1   Oct 19 2005 11:15:06   rgreenwood
//TD089 ES020 Removed commented out code
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.8.1.0   Oct 12 2005 20:55:36   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.8   Oct 21 2004 17:57:18   esevern
//amended to use TDCultureInfo.CurrentUICulture
//
//   Rev 1.7   Sep 19 2004 14:35:30   jgeorge
//Restructured HTML to increase space for airport list
//Resolution for 1551: Find a flight - South east as origin or destination
//
//   Rev 1.6   Sep 05 2004 14:12:42   esevern
//'alertbox' style added for ambiguous airport selection display
//
//   Rev 1.5   Aug 17 2004 13:34:00   passuied
//Forced the selection changed event to be raised only when the control is visible
//Resolution for 1295: Find a flight does not display airports when using find nearest function
//
//   Rev 1.4   Jun 28 2004 14:41:10   jgeorge
//Updated for label text
//
//   Rev 1.3   Jun 21 2004 12:15:44   jgeorge
//Added extra check to ensure scripts are present before enabling JavaScript.
//
//   Rev 1.2   Jun 09 2004 16:30:32   jgeorge
//Find a flight

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Collections;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.AirDataProvider;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	///	Allows the user to select airports for the Find Flight page
	/// </summary>
	public partial  class AirportBrowseControl : TDUserControl
	{
		#region Controls


		#endregion

		#region Internal variables

		private IAirDataProvider adp;

		private ArrayList restrictAirports = null;
		private ArrayList restrictRegions = null;

		private AirRegion selectedRegion = null;
		private Airport[] selectedAirports = new Airport[0];

		private ArrayList eventsRaised = new ArrayList();
		private string targetControlName = string.Empty;
		private string operatorSelectionControlName = string.Empty;
		private bool useJavaScript = false;
		private bool updateTargetControl = false;

		#endregion

		#region Public events

		// Keys
		private static readonly object AirportSelectionChangedEventKey = new object();
		private static readonly object FindNearestClickEventKey = new object();

		/// <summary>
		/// Event that will be raised when the airport selection is changed. This 
		/// is deemed to have happenned when either the dropdown selection is changed
		/// or any of the check boxes are selected/deselected.
		/// </summary>
		public event EventHandler AirportSelectionChanged
		{
			add { this.Events.AddHandler(AirportSelectionChangedEventKey, value); }
			remove { this.Events.AddHandler(AirportSelectionChangedEventKey, value); }
		}

		/// <summary>
		/// Event that will be raised when the "Find Nearest" button is clicked.
		/// </summary>
		public event EventHandler FindNearestClick
		{
			add { this.Events.AddHandler(FindNearestClickEventKey, value); }
			remove { this.Events.AddHandler(FindNearestClickEventKey, value); }
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Handles the page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			adp = (AirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
			ArrayList allRegions = adp.GetRegions();
			ArrayList allAirports = adp.GetAirports();

			// Populate the dropdown list
			TDResourceManager rm = Global.tdResourceManager;

			// Get FindNearest Button text label
			buttonFindNearest.Text = rm.GetString(this.ID + ".buttonFindNearest.Text", TDCultureInfo.CurrentUICulture);

			// Preserve the current selection, if present
			string oldSelected = "";
			if (dropMain.SelectedIndex != -1)
				oldSelected = dropMain.SelectedItem.Value;
			
			dropMain.Items.Clear();

			// First entry is the "Please select..." one
			dropMain.Items.Add(new ListItem(rm.GetString("AirportBrowseControl.Dropdown.Default", TDCultureInfo.CurrentUICulture), String.Empty));

			// Second is the "Regions" heading
			dropMain.Items.Add(new ListItem(rm.GetString("AirportBrowseControl.Dropdown.RegionsTitle", TDCultureInfo.CurrentUICulture), String.Empty));

			foreach (AirRegion r in allRegions)
				dropMain.Items.Add(new ListItem(r.Name, r.Code.ToString()));

			dropMain.Items.Add(new ListItem(rm.GetString("AirportBrowseControl.Dropdown.Separator", TDCultureInfo.CurrentUICulture), String.Empty));
			dropMain.Items.Add(new ListItem(rm.GetString("AirportBrowseControl.Dropdown.AirportsTitle", TDCultureInfo.CurrentUICulture), String.Empty));
			
			foreach (Airport a in allAirports)
				dropMain.Items.Add(new ListItem(a.Name, a.IATACode));

			if (oldSelected.Length == 0)
				dropMain.SelectedIndex = 0;
			else
			{
				ListItem toSelect = dropMain.Items.FindByValue(oldSelected);
				if (toSelect == null)
					dropMain.SelectedIndex = 0;
				else
					dropMain.SelectedIndex = dropMain.Items.IndexOf(toSelect);
			}

			// Now bind the allRegions to the repeater
			rptRegionPanels.DataSource = allRegions;
			rptRegionPanels.DataBind();

			// Finally set up label text for labels which aren't populated automatically in TDPage
			labelSRdropMain.Text = rm.GetString(this.ID + ".labelSRdropMain", TDCultureInfo.CurrentUICulture);
			labelSRdropMainAmbiguous.Text = rm.GetString(this.ID + ".labelSRdropMainAmbiguous", TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Handles the page prerender event. This performs any last-minute updates to the controls
		/// that are displayed to the user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			if ((restrictAirports != null) && (restrictAirports.Count != 0))
			{
				Airport currAirport;
				AirRegion currRegion;
				ListItem li;
				// Now remove specified regions and airports from the list
				int listIndex = dropMain.Items.Count - 1;
				for ( ; listIndex > 0; listIndex--)
				{
					li = dropMain.Items[listIndex];
					if (li.Value.Length != 0)
					{
						// Try and see if it's an airport first
						currAirport = adp.GetAirport(li.Value);
						if (currAirport == null)
						{
							// It's a region
							currRegion = adp.GetRegion(Convert.ToInt32(li.Value));
							if ((currRegion != null) && (!restrictRegions.Contains(currRegion)))
								dropMain.Items.Remove(li);
						}
						else if (!restrictAirports.Contains(currAirport))
							dropMain.Items.Remove(li);
					}
				}

				// Now update the repeater
				RepeaterItem item;
				foreach (AirRegion r in restrictRegions)
				{
					item =  GetRegionRepeaterItem(r);
					if (item != null)
					{
						// Go through each checkbox seeing whether it should be enabled on the client
						// or not
						DataList checkBoxes = (DataList)item.FindControl("dlistAirports");
						ScriptableCheckBox checkCurr;
						foreach (DataListItem dli in checkBoxes.Items)
						{
							checkCurr = (ScriptableCheckBox)dli.FindControl("checkAirport");
							checkCurr.Enabled = restrictAirports.Contains(adp.GetAirport(checkCurr.Value));
							if (!checkCurr.Enabled)
								checkCurr.Checked = false;
						}

					}

				}
			}

			/* Implement the selection
			 * Scenarios:
			 * 1. No region, single airport - select the airport in the dropdown, ensure all enabled 
			 *    checkboxes are ticked. If airport not present in dropdown, clear it.
			 * 2. No region, no airport - clear dropdown, tick all enabled checkboxes.
			 * 3. Region, no airports - select region in dropdown, tick all enabled checkboxes
			 * 4. Region, airports - select region in dropdown, tick only airports specified that are
			 *    enabled. In this case, the airports in the list will be the ones for the specified
			 *    region. Airports for all other regions should be checked.
			 */
			ArrayList airportsToTick;

			if ( selectedRegion != null )
			{
				// Select region in dropdown
				SelectItemInDropDown(selectedRegion.Code.ToString());
				airportsToTick = adp.GetAirports();

				// Remove airports for the current region that aren't in the selection
				ArrayList selectedRegionAirports = adp.GetRegionAirports(selectedRegion.Code);
				ArrayList selectedAirportsList = new ArrayList(selectedAirports);
				foreach (Airport a in selectedRegionAirports)
					if (!selectedAirportsList.Contains(a))
						airportsToTick.Remove(a);

			}
			else if ( selectedAirports.Length != 0 )
			{
				// Select airport in dropdown
				SelectItemInDropDown(selectedAirports[0].IATACode);
				airportsToTick = adp.GetAirports();
			}
			else
			{
				dropMain.SelectedIndex = 0;
				airportsToTick = adp.GetAirports();
			}

			// Now select/deselect the relevent check boxes
			foreach (RepeaterItem item in rptRegionPanels.Items)
			{
				// Go through each checkbox seeing whether it should be enabled on the client
				// or not
				DataList checkBoxes = (DataList)item.FindControl("dlistAirports");
				ScriptableCheckBox checkCurr;
				foreach (DataListItem dli in checkBoxes.Items)
				{
					checkCurr = (ScriptableCheckBox)dli.FindControl("checkAirport");
					if (checkCurr.Enabled)
						checkCurr.Checked = (airportsToTick.Contains(adp.GetAirport(checkCurr.Value)));

				}
			}

		}

		/// <summary>
		/// Event handler for the repeater.
		/// When called, the method must bind the list of airports for this region to the 
		/// dlistAirports data bound list and set the ItemDataBound event handler for that.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rptRegionPanels_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			AirRegion r = (AirRegion)e.Item.DataItem;
			DataList d = (DataList)e.Item.FindControl("dlistAirports");
			d.ItemCreated += new DataListItemEventHandler(dlistAirports_ItemCreated);
			d.DataSource = adp.GetRegionAirports(r.Code);
			d.DataBind();
		}

		/// <summary>
		/// Handler for the datalists.
		/// Sets the event handler for each of the checkboxes so that an event can be raised when
		/// the selection is modified
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dlistAirports_ItemCreated(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{
			// Find the checkbox and bind its checkedchanged event
			ScriptableCheckBox s = (ScriptableCheckBox)e.Item.FindControl("checkAirport");
			s.CheckedChanged += new EventHandler(OnAirportSelectionChanged);
		}

		/// <summary>
		/// Event handler for the dropdown and all of the checkboxes.
		/// Raises the AirportSelectionChanged event. The eventsRaised check is used to 
		/// ensure that the event is only raised once for each Postback, as the method
		/// will be called for each checkbox that was checked/unchecked, as well as the
		/// dropdown if the value was changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnAirportSelectionChanged(object sender, EventArgs e)
		{
			if (Visible)
			{
				if (!eventsRaised.Contains(AirportSelectionChangedEventKey))
				{
					EventHandler theDelegate = (EventHandler)this.Events[AirportSelectionChangedEventKey];
					if (theDelegate != null)
						theDelegate(this, e);
					eventsRaised.Add(AirportSelectionChangedEventKey);
				}
			}
		}
		
		/// <summary>
		/// Raises the FindNearestClick event. Event handler for the image button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFindNearestClick(object sender, EventArgs e)
		{
			EventHandler theDelegate = (EventHandler)this.Events[FindNearestClickEventKey];
			if (theDelegate != null)
				theDelegate(this, e);
			eventsRaised.Add(FindNearestClickEventKey);
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Removes all previously applied restrictions
		/// </summary>
		public void ClearRestrictions()
		{
			restrictAirports = null;
			restrictRegions = null;
		}

		/// <summary>
		/// Applies a restriction to the data shown in the list. Only airports contained
		/// in the parameter are used. Regions with no airports are removed
		/// </summary>
		/// <param name="airports"></param>
		public void Restrict(ArrayList airports)
		{
			restrictAirports = airports;
			if ((restrictAirports != null) && (restrictAirports.Count != 0))
			{
				restrictRegions = new ArrayList();
				ArrayList currRegions;
				// Build region list for the supplied airports
				foreach (Airport a in restrictAirports)
				{
					currRegions = adp.GetAirportRegions(a.IATACode);
					foreach (AirRegion r in currRegions)
						if (!restrictRegions.Contains(r))
							restrictRegions.Add(r);
				}
			}
			else
				restrictRegions = null;
		}

		/// <summary>
		/// Sets the current selection.
		/// </summary>
		/// <param name="selectedAirport"></param>
		public void SetData(Airport selectedAirport)
		{
			SetData(null, new Airport[] { selectedAirport } );
		}
		
		/// <summary>
		/// Sets the current selection of region/airport.
		/// </summary>
		/// <param name="selectedRegion"></param>
		/// <param name="selectedAirports"></param>
		public void SetData(AirRegion selectedRegion, Airport[] selectedAirports)
		{
			this.selectedRegion = selectedRegion;
			if (selectedAirports == null)
				this.selectedAirports = new Airport[0];
			else
				this.selectedAirports = selectedAirports;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns true if the checkbox for the specified airport is disabled.
		/// Used for data binding purposes.
		/// </summary>
		/// <param name="current"></param>
		/// <returns></returns>
		public bool GetClientDisabledValue(Airport current)
		{
			if (restrictAirports == null || restrictAirports.Count == 0)
				return false;
			else
                return !restrictAirports.Contains(current);
		}

		/// <summary>
		/// Returns the name of the panel containing checkboxes for the given region.
		/// Used for data binding in the aspx page.
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		public string GetRegionPanelName(AirRegion region)
		{
			return dropMain.ClientID + "_region_" + region.Code;
		}

		/// <summary>
		/// Allows specification of a control that will be treated as the "other end"
		/// of the route started or ended by this control. If UpdateTargetControl is set
		/// to true, the TargetControl will be updated with valid destinations whenever 
		/// the value in this control is changed.
		/// <seealso cref="UpdateTargetControl"/>
		/// </summary>
		public string TargetControlName
		{
			get { return targetControlName; }
			set { targetControlName = value; }
		}

		/// <summary>
		/// Allows specification of a control that will be treated as the operator selection
		/// control
		/// </summary>
		public string OperatorSelectionControlName
		{
			get { return operatorSelectionControlName; }
			set { operatorSelectionControlName = value; }
		}

		/// <summary>
		/// Whether or not to update the control specified by TargetControlName when this
		/// control changes.
		/// <seealso cref="TargetControlName"/>
		/// </summary>
		public bool UpdateTargetControl
		{
			get { return updateTargetControl; }
			set { updateTargetControl = value; }
		}

		/// <summary>
		/// Returns the full id of the dropdown list, as it will be written out in the
		/// client HTML.
		/// </summary>
		public string DropDownClientID
		{
			get { return dropMain.ClientID; }
		}

		/// <summary>
		/// Gets/sets whether or not to use JavaScript. The Get method ORs the specified value with 
		/// the boolean value representing whether or not JavaScript is enabled on the client.
		/// </summary>
		public bool UseJavaScript
		{
			get 
			{ 
				//jsSupport needs to be true, because the jscript functionality is needed immediately.
				//Can't wait for the post-back.
				bool jsSupport = true;
				return 
					useJavaScript && 
					((AirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider]).ScriptGenerated &&
					jsSupport; 
			}
			set { useJavaScript = value; }
		}

		/// <summary>
		/// Boolean indicating whether or not the "Find nearest" button should be visible.
		/// </summary>
		public bool ShowFindButton
		{
			get { return buttonFindNearest.Visible; }
			set { buttonFindNearest.Visible = value; }
		}

        public string labelText
        {
            get
            {
                return labelFrom.Text;
            }
            set
            {
                labelFrom.Text = value;
            }
        }

		/// <summary>
		/// Returns the region selected in the dropdown. If no region is selected, null is returned
		/// </summary>
		public AirRegion SelectedRegion
		{
			get
			{
				string selected = dropMain.SelectedItem.Value;
				if (selected.Length == 0)
					return null;
				else if (!IsInt32(selected))
					return null;
				else
					return adp.GetRegion(Int32.Parse(selected));
			}
		}

		/// <summary>
		/// Returns an array of selected airports. If an airport was selected from
		/// the dropdown, this is the only element. If a region was selected then
		/// this will be the specified airports for that region. If JavaScript is
		/// not enabled, or all airports are unchecked, then all airports for the
		/// region will be selected.
		/// If no airports are selected (ie title or separator row selected) then 
		/// null will be returned.
		/// </summary>
		public Airport[] SelectedAirports
		{
			get
			{
				string selected = dropMain.SelectedItem.Value;
				AirRegion selectedRegion = this.SelectedRegion;
				if (selected.Length == 0)
					return null;
				else if (selectedRegion == null)
				{
					Airport a = adp.GetAirport(selected);
					if (a == null)
						return null;
					else
						return new Airport[] { a };
				} 
				else
				{
					// A region has been selected, so we need to return the list of
					// selected airports
					Airport[] selection = GetRegionSelectedAirportCodes(selectedRegion);
					if (selection.Length == 0)
						return (Airport[])adp.GetRegionAirports(selectedRegion.Code).ToArray(typeof(Airport));
					else
						return selection;
							
				}
				
			}
		}

		/// <summary>
		/// If set to true, the dropdown and checkboxes are displayed with a yellow background,
		/// and the message (if present) is displayed. Otherwise, the message is hidden, and the
		/// background as not set.
		/// </summary>
		public bool DisplayAsAmbiguity
		{
			get { return highlightPanel.CssClass == "alertwarning"; }
			set 
			{
				if ( value )
					highlightPanel.CssClass = "alertwarning";
				else
					highlightPanel.CssClass = "";
				labelAmbiguityMessage.Visible = value;
				labelSRdropMain.Visible = !value;
				labelSRdropMainAmbiguous.Visible = value;
			}
		}

		/// <summary>
		/// Sets the error message that will be displayed above the dropdown. This is only
		/// visible if the DisplayAsAmbiguity property is set to true.
		/// </summary>
		public string AmbiguityMessage
		{
			get { return labelAmbiguityMessage.Text; }
			set { labelAmbiguityMessage.Text = value; }
		}


		#endregion

		#region Private methods

		/// <summary>
		/// Selects the item with the given value in the dropdown
		/// </summary>
		/// <param name="itemValue"></param>
		private void SelectItemInDropDown(string itemValue)
		{
			ListItem li = dropMain.Items.FindByValue(itemValue);
			if (li == null)
				dropMain.SelectedIndex = 0;
			else
				dropMain.SelectedIndex = dropMain.Items.IndexOf(li);
		}

		/// <summary>
		/// Gets the item in the repeater corresponding to the specified region
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		private RepeaterItem GetRegionRepeaterItem(AirRegion region)
		{
			// Find the entry in the repeater corresponding to the region
			ArrayList allRegions = adp.GetRegions();
			int i = allRegions.IndexOf(region);
			if ( i == -1 )
				return null;
			else
				return rptRegionPanels.Items[i];
		}

		/// <summary>
		/// Gets the selected airports for the given region
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		private Airport[] GetRegionSelectedAirportCodes(AirRegion region)
		{
			ArrayList results = new ArrayList();
			RepeaterItem ri = GetRegionRepeaterItem(region);
			if (ri == null)
			{
				// Couldn't find the region panel. Return all airports for
				// the specified region
				results = adp.GetRegionAirports(region.Code);
			}
			else
			{
				DataList checkBoxes = (DataList)ri.FindControl("dlistAirports");
				ScriptableCheckBox checkCurr;
				foreach (DataListItem dli in checkBoxes.Items)
				{
					checkCurr = (ScriptableCheckBox)dli.FindControl("checkAirport");
					if (checkCurr.Checked)
						results.Add(adp.GetAirport(checkCurr.Value));
				}
			}
			return (Airport[])results.ToArray(typeof(Airport));
		}

		/// <summary>
		/// Returns true if the string contains a valid integer
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		private bool IsInt32(string s)
		{
			try
			{
				int i = Int32.Parse(s);
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region Protected/overridden methods

		/// <summary>
		/// Implements the selection and performs any other updates requred
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			dropMain.EnableClientScript = UseJavaScript;
			panelRegions.EnableClientScript = UseJavaScript;

            
			if (UseJavaScript)
			{
				panelRegions.Visible = true;

				string javaScriptDom = (string) Session[((TDPage)Page).Javascript_Dom];

				// Set up javascript stuff
				dropMain.ScriptName = "AirportBrowseControl";
				dropMain.Action = String.Format("handleAirportSelectionChanged('{0}', '{1}', {2}, '{3}');", dropMain.ClientID, TargetControlName, UpdateTargetControl.ToString().ToLower(), OperatorSelectionControlName);
				
				panelRegions.ScriptName = "AirportBrowseControl";
				panelRegions.Action = String.Format("handleRegionClick(event, '{0}', '{1}', {2}, '{3}');", dropMain.ClientID, TargetControlName, UpdateTargetControl.ToString().ToLower(), OperatorSelectionControlName);

				// checking for Javascript support
				ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
				Page.ClientScript.RegisterStartupScript(typeof(AirportBrowseControl), AirDataProvider.ScriptName, scriptRepository.GetScript(AirDataProvider.ScriptName, javaScriptDom));
                Page.ClientScript.RegisterStartupScript(typeof(AirportBrowseControl), dropMain.ScriptName, scriptRepository.GetScript(dropMain.ScriptName, javaScriptDom));
			}
			else
				panelRegions.Visible = false;

			base.OnPreRender(e);
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.rptRegionPanels.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rptRegionPanels_ItemDataBound);
            this.buttonFindNearest.Click += new EventHandler(this.OnFindNearestClick);
            this.dropMain.SelectedIndexChanged += new EventHandler(this.OnAirportSelectionChanged);
        }
		#endregion

	}
}
