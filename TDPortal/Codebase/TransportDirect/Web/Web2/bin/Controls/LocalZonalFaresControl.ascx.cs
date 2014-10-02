// *********************************************** 
// NAME                 : LocalZonalFaresControl.ascx.cs
// AUTHOR               : Andrew Sinclair
// DATE CREATED         : 08/03/2007
// DESCRIPTION			: Displays the external link for local fares operator
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/LocalZonalFaresControl.ascx.cs-arc  $
//
//   Rev 1.3   Oct 17 2008 11:55:24   build
//Automatically merged from branch for stream0093
//
//   Rev 1.2.1.0   Aug 06 2008 11:18:18   apatel
//added text "(opens new window)" to hyperlinks opening in new window.
//Resolution for 5096: ArrivalBoard and DepartureBoard , and related sites -  labels missing "Opens new window" text
//
//   Rev 1.2   Mar 31 2008 13:21:44   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:58   mturner
//Initial revision.
//
//   Rev 1.5   Apr 03 2007 11:15:54   dsawe
//updated for local zonal services
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.4   Mar 21 2007 15:15:28   dsawe
//added for printer friendly pages
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.3   Mar 19 2007 15:57:56   dsawe
//updated for local zonal services
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.2   Mar 14 2007 14:30:20   dsawe
//Updated for local zonal
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.1   Mar 13 2007 17:43:18   dsawe
//modified
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.0   Mar 09 2007 11:10:56   asinclair
//Initial revision.


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.ZonalServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.Presentation.InteractiveMapping;
	using System.Globalization;
	using TransportDirect.UserPortal.ExternalLinkService;
	
	/// <summary>
	///		Summary description for LocalZonalFaresControl.
	/// </summary>
	public partial class LocalZonalFaresControl : TDPrintableUserControl
	{
        private PublicJourneyDetail journeyDetail;
		//Determines if any links are to be rendered
		private bool isEmpty = true;
		private string faresUrlLink = string.Empty;

		/// <summary>
		/// Event handler for the prerender event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender( EventArgs e )
		{
            faresUrlHyperlink.Text = string.Format("{0} {1}", GetResource("LocalZonalFares.Hyperlink.Title"), GetResource("langStrings", "ExternalLinks.OpensNewWindowText")); 
			faresUrlHyperlink.ToolTip = GetResource("OperatorLinks.Hyperlink.Title");
			checkForLabel.Text = GetResource("LocalZonalFares.Label.CheckFor");

			// If in printer friendly mode then don't show as hyperlink
			if (!this.PrinterFriendly)
			{
				// Set the hyperlink control properties
				faresUrlHyperlink.NavigateUrl = faresUrlLink;
				faresUrlHyperlink.Target = "_blank";
				faresUrlHyperlink.Visible = true;
				checkForLabel.Visible = true;
			}
			else
			{
				// Set the label title text
				checkForLabel.Text += faresUrlHyperlink.Text;
				faresUrlHyperlink.Visible = false;
				checkForLabel.Visible = true;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Set the resource manager
			this.LocalResourceManager  = TDResourceManager.FARES_AND_TICKETS_RM;
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

		#region Properties

		/// <summary>
		/// Read/write property.  
		/// 
		/// </summary>
		public PublicJourneyDetail JourneyDetail
		{
			get {return journeyDetail;}
			set 
			{
				journeyDetail = value;
				BuildLocalFaresLink();
			}
		}

		/// <summary>
		/// (read-only) Returns true if the control is to render links, false othewise
		/// </summary>
		public bool IsEmpty
		{
			get{ return isEmpty; }
		}

		#endregion

		#region Private Methods

		private void BuildLocalFaresLink()
		{
			string originAdminDistrictUrl = string.Empty;
			string destinationAdminDistrictUrl = string.Empty;
			string originAdminUrl = string.Empty;
			string destinationAdminUrl = string.Empty;
			bool districtIsZero = false;

			ExternalLinkDetail faresLinkDetail = null;

			NPTGInfo originNPTGInfo = null;
			NPTGInfo destinationNPTGInfo =null;

			ZonalServiceCatalogue refData = (ZonalServiceCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.ZonalServices];
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

			originNPTGInfo = gisQuery.GetNPTGInfoForNaPTAN(journeyDetail.LegStart.Location.NaPTANs[0].Naptan);
			destinationNPTGInfo = gisQuery.GetNPTGInfoForNaPTAN(journeyDetail.LegEnd.Location.NaPTANs[0].Naptan);

			if((originNPTGInfo != null) && (destinationNPTGInfo != null))
			{
				originAdminDistrictUrl = refData.GetAdminAreaDistrictLinks(originNPTGInfo.AdminAreaID, destinationNPTGInfo.DistrictID);
				destinationAdminDistrictUrl = refData.GetAdminAreaDistrictLinks(destinationNPTGInfo.AdminAreaID, destinationNPTGInfo.DistrictID);
			}
			if((originAdminDistrictUrl != string.Empty) && (destinationAdminDistrictUrl != string.Empty) && (originAdminDistrictUrl == destinationAdminDistrictUrl))
				isEmpty = false;
			
			if(isEmpty)
			{
				//By passing DistrictId= “0” to the method we can get links for the whole Admin Area  
				if((originNPTGInfo != null) && (destinationNPTGInfo != null))
				{
					originAdminUrl = refData.GetAdminAreaDistrictLinks(originNPTGInfo.AdminAreaID, "0");
					destinationAdminUrl = refData.GetAdminAreaDistrictLinks(destinationNPTGInfo.AdminAreaID, "0");
				}
				if((originAdminUrl != string.Empty) && (destinationAdminUrl != string.Empty) && (originAdminUrl == destinationAdminUrl))
				{
					isEmpty = false;
					districtIsZero = true;
				}

			}
			switch (districtIsZero)
			{
				case true:
					if (originAdminUrl!= string.Empty)
					{
						faresLinkDetail = (ExternalLinkDetail)ExternalLinks.Current[originAdminUrl];
						faresUrlLink = faresLinkDetail.Url;
					}
					break;
			
				case false:
					if(originAdminDistrictUrl!= string.Empty)
					{
						faresLinkDetail = (ExternalLinkDetail)ExternalLinks.Current[originAdminDistrictUrl];
						faresUrlLink = faresLinkDetail.Url;
					}
					break;
			}
		}

		#endregion
	}
}
