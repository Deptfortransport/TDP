// *********************************************** 
// NAME                 : MapRegionControl.ascx.cs 
// AUTHOR               : Amit Patel
// DATE CREATED         : 13/07/2005
// ************************************************ 
//
//   Rev 1.0   Nov 08 2007 13:16:34   mturner
//Initial revision.
// this is MapRegionSelect Control which had to be changed to remove region select control
// and back button. Also, width of the control made narrower.



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
    public partial class MapRegionControl : TDUserControl
    {
        protected System.Web.UI.WebControls.Panel regionsPanel;

        private string selectedRegionId;
        private GenericDisplayMode displayMode;
        private bool allowAllUKOption = true;




        /// <summary>
        /// Event raised when the selected region is changed using either the dropdown or the map
        /// </summary>
        public event EventHandler SelectedRegionChanged;

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
            imageMap1.RegionClicked += new EventHandler(imageMap1_RegionClicked);
            selectAllUk.Click += new EventHandler(selectAllUk_Click);
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



            //CCN 0427 redionList control removed
            //IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
            //ds.Select(regionsList, ds.GetResourceId(DataServiceType.NewsRegionDrop, selectedRegionId));
            imageMap1.SelectedRegionId = selectedRegionId;
            imageMap1.DisplayMode = displayMode;


        }

        #endregion

        #region Control event handlers


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
            // CCN 0427 Region List Removed
            //IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
            //selectedRegionId = ds.GetValue(DataServiceType.NewsRegionDrop, regionsList.SelectedValue);
            //OnSelectedRegionChanged();
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

        private void selectAllUk_Click(object sender, EventArgs e)
        {
            DSDropItem allUKItem = GetDataServicesAllUKItem();
            selectedRegionId = allUKItem.ResourceID;
            imageMap1.SelectedRegionId = selectedRegionId;
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
                    //IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
                    //selectedRegionId = ds.GetValue(DataServiceType.NewsRegionDrop, regionsList.SelectedValue);
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


        #endregion

        #region Private/protected methods



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
            headingRegion.Text = GetResource("MapRegionControl.headingRegion");
            headingMap.Text = GetResource("TravelNews.lblCurrentViewMap");
            headingClickToSelect.Text = GetResource("MapRegionSelectControl.headingClickToSelect");

            //regionsListOK.Text = GetResource("MapRegionSelectControl.regionsListOK.Text");

            // CCN 0427 regionslist removed
            // Load dropdown
            //IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
            //ds.LoadListControl(DataServiceType.NewsRegionDrop, regionsList);

            // Initialise ImageMap
            imageMap1.InitialiseFromProperties("UKRegionImageMap", Global.tdResourceManager);

            //CCN 0427 select All uk button to select all uk 
            selectAllUk.Text = GetResource("MapRegionSelectControl.selectAllUk.Text");
        }

        /// <summary>
        /// Adds the "All UK" item to the list
        /// </summary>
        private void AddAllUKItem()
        {
            // CCN 0427 regionsList removed
            /*DSDropItem allUKItem = GetDataServicesAllUKItem();

            // Verify that the dropdown list contains this option
            ListItem all = regionsList.Items.FindByValue(allUKItem.ResourceID);
            if (all == null)
                regionsList.Items.Insert(0, new ListItem(GetResource(allUKItem.ResourceID), allUKItem.ResourceID));
            */
        }

        /// <summary>
        /// Removes the "All UK" item from the list
        /// </summary>
        private void RemoveAllUKItem()
        {
            // CCN 0427 Region List control removed
            /*
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
             */
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
