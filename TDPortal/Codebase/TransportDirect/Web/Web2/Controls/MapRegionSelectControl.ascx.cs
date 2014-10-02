// *********************************************** 
// NAME                 : MapRegionSelectControl.ascx.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 13/07/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapRegionSelectControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:34   mturner
//Initial revision.
//
//   Rev Devfactory Jan 07 2008 12:15:15 apatel
//   Added new back button, event handler BackButtonClicked and IsBackButtonVisible public property
//
//   Rev 1.8   Feb 23 2006 16:12:56   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.7   Dec 20 2005 11:12:24   jgeorge
//Removed page landing functionality (copied to helper class)
//Resolution for 3320: DN077 - Travel News Table Map not shown on first landing on page
//
//   Rev 1.6   Nov 15 2005 14:13:06   RGriffith
//MapRegionSelectControl.regionsListOK.Text
//
//   Rev 1.5   Nov 04 2005 11:17:52   ralonso
//Manual Merge for stream2816
//
//   Rev 1.4   Nov 02 2005 18:33:08   kjosling
//Automatically merged from branch for stream2877
//
//   Rev 1.3.2.1   Nov 02 2005 16:12:10   jmcallister
//Code review comments enacted
//
//   Rev 1.3.2.0   Nov 01 2005 17:16:40   jmcallister
//Landing Page Functionality
//
//   Rev 1.3   Sep 27 2005 11:03:44   asinclair
//Merge of 2596
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.2.1.0   Aug 22 2005 16:30:58   NMoorhouse
//Persist the SelectedRegionId when re-selecting the same region from the drop down list
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.2   Aug 18 2005 14:44:26   jgeorge
//Removed commented out code
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.1   Aug 03 2005 17:34:48   jgeorge
//FxCop changes and fix width of region dropdown
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.0   Jul 28 2005 12:01:42   jgeorge
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;	
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;

	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Web.Support;

	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
	///	Allows selection of a region of the UK using either a drop down list 
	///	or an image map.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class MapRegionSelectControl : TDUserControl
	{
		protected System.Web.UI.WebControls.Panel regionsPanel;

		private string selectedRegionId;
		private GenericDisplayMode displayMode;
		private bool allowAllUKOption = true;
        
        //private bool isBackButtonVisible = true;
		

		/// <summary>
		/// Event raised when the selected region is changed using either the dropdown or the map
		/// </summary>
		public event EventHandler SelectedRegionChanged;
        /// <summary>
        /// CCN 0421 new back button event
        /// </summary>
        //public event EventHandler BackButtonClicked;

		#region Page event handlers

		/// <summary>
		/// Handler for the load event. Sets up the controls.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitialiseControls();
		}

		/// <summary>
		/// Handler for the Init event. Wires up event handlers
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, EventArgs e)
		{
			regionsList.SelectedIndexChanged +=new EventHandler(regionsList_SelectedIndexChanged);
			imageMap1.RegionClicked += new EventHandler(imageMap1_RegionClicked);
            //backButton.Click += new EventHandler(backButton_Click);
		}

       

		/// <summary>
		/// Handler for the PreRender event. Ensures that both dropdown and imagemap are
		/// displaying the correct region
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			if (allowAllUKOption)
				AddAllUKItem();
			else
				RemoveAllUKItem();

            // CCN 0421 setting backbutton visible property 
            //backButton.Visible = IsBackButtonVisible;

			IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			ds.Select(regionsList, ds.GetResourceId(DataServiceType.NewsRegionDrop, selectedRegionId));
			imageMap1.SelectedRegionId = selectedRegionId;
			imageMap1.DisplayMode = displayMode;

			switch (displayMode)
			{
				case GenericDisplayMode.Normal:
					regionsCell.Attributes.Remove("class");
					break;
				case GenericDisplayMode.Ambiguity:
					regionsCell.Attributes.Add("class", "alerterror");
					break;
				case GenericDisplayMode.ReadOnly:
					regionsCell.Attributes.Remove("class");
					break;
			}
		}

		#endregion

		#region Control event handlers
        /// <summary>
        /// handles the onclick event for the back button and raises BackButtonClicked event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void backButton_Click(object sender, EventArgs e)
        //{
        //    OnBackButtonClicked();
        //}

		/// <summary>
		/// Handles the SelectedIndexChanged event for the regions list. The new selected region id
		/// is stored in the selectedRegionId variable, and the SelectedRegionChanged event is raised.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void regionsList_SelectedIndexChanged(object sender, EventArgs e)
		{
			ActionSelectedRegionChanged();
		}

		/// <summary>		
		/// Commit a change to the Region value, and force appropriate change in appearance by raising changed event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActionSelectedRegionChanged()
		{
			IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			selectedRegionId = ds.GetValue(DataServiceType.NewsRegionDrop, regionsList.SelectedValue);
			OnSelectedRegionChanged();
		}
		/// <summary>
		/// Handles the RegionClicked event of the image map. The new selected region id is stored 
		/// in the selectedRegionId variable, and the SelectedRegionChanged event is raised.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void imageMap1_RegionClicked(object sender, EventArgs e)
		{
			selectedRegionId = imageMap1.SelectedRegion.Id;
			OnSelectedRegionChanged();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Read/write property holding the ID of the selected region
		/// </summary>
		public string SelectedRegionId
		{
			get 
			{
				if (selectedRegionId == null)
				{
					IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
					selectedRegionId = ds.GetValue(DataServiceType.NewsRegionDrop, regionsList.SelectedValue);
				} 
				return selectedRegionId; 
			}
			set { selectedRegionId = value; }
		}

		/// <summary>
		/// Read/write property controlling how the dropdown and imagemap are displayed
		/// </summary>
		public GenericDisplayMode DisplayMode
		{
			get { return displayMode; }
			set { displayMode = value; }
		}

		/// <summary>
		/// Read/write property controlling whether or not the "All UK" option is available in the 
		/// dropdown
		/// </summary>
		public bool AllowAllUKOption
		{
			get { return allowAllUKOption; }
			set { allowAllUKOption = value; }
		}

        //public bool IsBackButtonVisible
        //{
        //    get { return isBackButtonVisible; }
        //    set { isBackButtonVisible = value; }
        
        //}
		#endregion

		#region Private/protected methods

        //protected void OnBackButtonClicked()
        //{
        //    EventHandler h = BackButtonClicked;
        //    if (h != null)
        //        h(this, EventArgs.Empty);
        //}

		/// <summary>
		/// Raises the SelectedRegionChanged event
		/// </summary>
		protected void OnSelectedRegionChanged()
		{
			EventHandler h = SelectedRegionChanged;
			if (h != null)
				h(this, EventArgs.Empty);
		}

		/// <summary>
		/// Loads static text into the labels, and populates the list of regions
		/// </summary>
		private void InitialiseControls()
		{
            int indexRegionsList = regionsList.SelectedIndex;

			headingRegion.Text = GetResource("MapRegionSelectControl.headingRegion");
			headingMap.Text = GetResource("TravelNews.lblCurrentViewMap");
			headingClickToSelect.Text = GetResource("MapRegionSelectControl.headingClickToSelect");
			
			regionsListOK.Text = GetResource("MapRegionSelectControl.regionsListOK.Text");
            //backButton.Text = GetResource("MapRegionSelectControl.backButton.Text");
			// Load dropdown
			
				IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				ds.LoadListControl(DataServiceType.NewsRegionDrop, regionsList);
                regionsList.SelectedIndex = indexRegionsList;
			// Initialise ImageMap
			imageMap1.InitialiseFromProperties("UKRegionImageMap", Global.tdResourceManager);
		}

		/// <summary>
		/// Adds the "All UK" item to the list
		/// </summary>
		private void AddAllUKItem()
		{
			DSDropItem allUKItem = GetDataServicesAllUKItem();

			// Verify that the dropdown list contains this option
			ListItem all = regionsList.Items.FindByValue(allUKItem.ResourceID);
			if (all == null)
				regionsList.Items.Insert(0, new ListItem(GetResource(allUKItem.ResourceID), allUKItem.ResourceID));
		}

		/// <summary>
		/// Removes the "All UK" item from the list
		/// </summary>
		private void RemoveAllUKItem()
		{
			DSDropItem allUKItem = GetDataServicesAllUKItem();

			// Verify that the dropdown list contains this option
			ListItem all = regionsList.Items.FindByValue(allUKItem.ResourceID);
			if (all != null)
			{
				if (all.Selected == true)
				{
					// Change the selected item to the first region in the list
					IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
					selectedRegionId = ds.GetValue(DataServiceType.NewsRegionDrop, regionsList.Items[1].Value);
				}
				regionsList.Items.Remove(all);
			}
		}

		/// <summary>
		/// Retrieves the "All UK" item from Data Services
		/// </summary>
		/// <returns></returns>
		private DSDropItem GetDataServicesAllUKItem()
		{
			IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			ArrayList items = ds.GetList(DataServiceType.NewsRegionDrop);
			foreach (DSDropItem current in items)
			{
				if (current.ItemValue == "0")
					return current;
			}
			return null;
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
