// *********************************************** 
// NAME                 : ZonalServiceLinksControl.cs 
// AUTHOR               : Ken Josling
// DATE CREATED         : 15/12/2005
// DESCRIPTION			: Displays zonal service links as a list of hyperlinks
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ZonalServiceLinksControl.ascx.cs-arc  $
//
//   Rev 1.4   Sep 23 2009 10:33:14   nrankin
//Opens in new window
//Resolution for 5320: Accessibility - Opens in new window
//
//   Rev 1.3   Jun 27 2008 09:41:18   apatel
//CCN - 458 Accessibility Updates - Improved linking
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.2   Mar 31 2008 13:23:44   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:50   mturner
//Initial revision.
//
//   Rev 1.1   Dec 19 2005 16:26:32   kjosling
//Updated following integration testing
//
//   Rev 1.0   Dec 19 2005 11:46:02   kjosling
//Initial revision.

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

	/// <summary>
	///	Displays zonal service links
	/// </summary>
	public partial class ZonalServiceLinksControl : TDUserControl
	{
		private bool isEmpty = true;										//Determines if any links are to be rendered
		//Used to list zonal service links
		private string naptan = String.Empty;								//Stores the currently selected stop
		private ExternalLinkDetail[] links;
		
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
			ZonalServiceCatalogue refData = (ZonalServiceCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.ZonalServices];
			links = refData.GetZonalServiceLinks(naptan);
			if(links != null)
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
				zonalServiceLinks.DataSource = links;
				zonalServiceLinks.DataBind();
			}
			if(!isEmpty)
			{
				base.OnPreRender (e);
			}
		}

		/// <summary>
		/// Runs once for each zonal service link. Configures the repreated hyperlink for each zonal
		/// service
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">RepeaterItemEventArgs</param>
		private void zonalServiceLinks_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
                ExternalLinkDetail externallink = (ExternalLinkDetail)e.Item.DataItem;
                string openNewWindowImageUrl = GetResource("langStrings", "ExternalLinks.OpensNewWindowImage");

                HyperLink link = (HyperLink)e.Item.FindControl("zonalServiceLink");
                link.Target = "_blank";
                link.NavigateUrl = externallink.Url;
                link.Text = externallink.LinkText + " " + openNewWindowImageUrl;
                link.ToolTip = GetResource("ParkAndRideTableControl.ParkAndRideHyperlink.ToolTip");
			}
		}

		/// <summary>
		/// Runs when the control loads
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			zonalServiceLinks.ItemDataBound +=new RepeaterItemEventHandler(zonalServiceLinks_ItemDataBound);
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
