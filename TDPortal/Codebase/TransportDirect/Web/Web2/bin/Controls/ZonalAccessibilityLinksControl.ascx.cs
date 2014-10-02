// *********************************************** 
// NAME                 : ZonalAccessibilityLinksControl.cs 
// AUTHOR               : Sanjeev Johal
// DATE CREATED         : 19/06/2008
// DESCRIPTION			: Displays zonal service links as a list of hyperlinks
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ZonalAccessibilityLinksControl.ascx.cs-arc  $
//
//   Rev 1.6   Sep 14 2009 10:55:28   apatel
//Stop Information page changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.5   Jul 24 2008 13:45:48   apatel
//External links added text "(opens new window)"
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.4   Jul 24 2008 10:44:28   apatel
//Removed External Links tooltip and added (opens new window) text to the links
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.3   Jul 17 2008 13:05:06   apatel
//Updated to show links based on naptan or admin-district area but not base on both.
//Resolution for 5071: Zonal accessibilty links do not display in the correct section of the screen
//
//   Rev 1.2   Jul 11 2008 14:09:50   apatel
//updated to html decode accessibility link description
//Resolution for 5057: Zonal Accessibility links showing the same text in diagram
//
//   Rev 1.1   Jul 03 2008 13:26:52   apatel
//change the namespage zonalaccessibility to zonalservices
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.0   Jun 27 2008 09:50:10   apatel
//Initial revision.
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.0   Jun 19 2008 14:20:40   sjohal
//Initial Creation


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Collections;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
    using TransportDirect.UserPortal.ZonalServices;
	using TransportDirect.UserPortal.ExternalLinkService;
	using TransportDirect.Common.ServiceDiscovery;
    using TransportDirect.UserPortal.LocationService;
    using TransportDirect.Presentation.InteractiveMapping;

	/// <summary>
    ///	Displays zonal Accessibility links
	/// </summary>
    public partial class ZonalAccessibilityLinksControl : TDUserControl
	{
		private bool isEmpty = true;										//Determines if any links are to be rendered
        //Used to list zonal accessibility links
		private string naptan = String.Empty;								//Stores the currently selected stop
		private ExternalLinkDetail[] links;

        private bool accessibilityByNaptan = false;

        /// <summary>
        /// (read-write) Gets or Sets the option to show the Accessibility links.
        /// if true shows accessibility links associated with Naptan
        /// if false shows accessibility links associated with area id and district id.
        /// </summary>
        public bool AccessibilityByNaptan
        {
            get { return accessibilityByNaptan; }
            set
            {
                accessibilityByNaptan = value;
            }
        }


		/// <summary>
		/// (read-write) Gets or Sets the naptan for this control
		/// </summary>
		public string Naptan
		{
			get{ return naptan;		}
			set
			{ 
				naptan = value;	
				GetLinksForStop();
			}
		}

		/// <summary>
		/// (read-only) Returns true if the control is to render links, false othewise
		/// </summary>
		public bool IsEmpty
		{
			get{ return isEmpty;		}
		}

		/// <summary>
		/// Called during pre-render. Retrieves the zonal service links prior to displaying the control on
		/// a client
		/// </summary>
        private void GetLinksForStop()
        {
            ZonalAccessibilityCatalogue refData = (ZonalAccessibilityCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.ZonalAccessibility];
            if (accessibilityByNaptan)
            {
                links = refData.GetZonalAccessibilityLinks(naptan);
            }
            else
            {
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
                NPTGInfo nptginfo = gisQuery.GetNPTGInfoForNaPTAN(naptan);
                if (nptginfo != null)
                    links = refData.GetAdminAreaDistrictLinks(nptginfo.AdminAreaID, nptginfo.DistrictID);
            }
            if (links != null)
            {
                isEmpty = false;
            }
        }


		/// <summary>
		/// Runs before the control is rendered to the client
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnPreRender(EventArgs e)
		{
			if(links != null)
			{
                zonalAccessibilityLinks.DataSource = links;
                zonalAccessibilityLinks.DataBind();
			}
			if(!isEmpty)
			{
				base.OnPreRender (e);
			}
		}

		/// <summary>
        /// Runs once for each zonal accessibility link. Configures the repreated hyperlink for each zonal
		/// service
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">RepeaterItemEventArgs</param>
        private void zonalAccessibilityLinks_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				ExternalLinkDetail externallink = (ExternalLinkDetail)e.Item.DataItem;
                HyperLink link = (HyperLink)e.Item.FindControl("zonalAccessibilityLink");
				link.Target = "_blank";
				link.NavigateUrl = externallink.Url;
                link.Text = string.Format("{0} {1}", Server.HtmlDecode(externallink.LinkText), GetResource("ExternalLinks.OpensNewWindowImage"));
                

			}
		}

		/// <summary>
		/// Runs when the control loads
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            zonalAccessibilityLinks.ItemDataBound += new RepeaterItemEventHandler(zonalAccessibilityLinks_ItemDataBound);
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
