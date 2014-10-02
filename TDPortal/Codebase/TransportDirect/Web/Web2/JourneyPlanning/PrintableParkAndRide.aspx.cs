// ***********************************************
// NAME 		: PrintableParkAndRide.aspx.cs
// AUTHOR 		: Neil Moorhouse
// DATE CREATED : 03/08/2005
// DESCRIPTION 	: Printable version of the ParkAndRide web page
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableParkAndRide.aspx.cs-arc  $
//
//   Rev 1.3   Dec 17 2008 11:27:56   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:28   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:56   mturner
//Initial revision.
//
//   Rev 1.4   Apr 11 2006 16:23:38   rgreenwood
//IR3789: Corrected setup of regiontext value via dataservices
//Resolution for 3789: DN058 Park and Ride Phase 2 - Printer friendly page region display
//
//   Rev 1.3   Feb 23 2006 18:01:16   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.2   Nov 18 2005 16:46:24   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.1   Oct 10 2005 12:14:46   schand
//Fix for IR2850. Removed 'Specific park and ride schemes' label. Also, adjusted the header and table to align left.
//Resolution for 2850: DN58 - Park and Ride  - layout issues with printer friendly page
//
//   Rev 1.0   Aug 12 2005 13:19:38   NMoorhouse
//Initial revision.
//
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for PrintableParkAndRide.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PrintableParkAndRide : TDPrintablePage, INewWindowPage
	{
		#region Instance variables
		protected TransportDirect.UserPortal.Web.Controls.ParkAndRideTableControl parkAndRideTable;
		ParkAndRideInfo[] parkAndRideData;
		IParkAndRideCatalogue parkAndRideLocations = (IParkAndRideCatalogue) TDServiceDiscovery.Current[ServiceDiscoveryKey.ParkAndRideCatalogue];
		#endregion

		#region Const declarations
		private const string RES_PAGEHEADING = "ParkAndRide.labelParkAndRideHeading.Text";		
		private const string RES_PRINTFRIENDLY = "StaticPrinterFriendly.labelPrinterFriendly";
		private const string RES_PRINTINSTRUC = "StaticPrinterFriendly.labelInstructions";
		private const string RES_REGIONDROPTEXT = "PrintableParkAndRide.labelRegionDrop.Text";
		private const string RES_ALLREGIONID = "0";
		#endregion
	
		#region Constructor
		/// <summary>
		/// Default constructor. Sets the pageId
		/// </summary>
		public PrintableParkAndRide() : base()
		{
			pageId = PageId.PrintableParkAndRide;
		}
		#endregion

		#region Page event handlers
		/// <summary>
		/// Handler for the load event. Sets up the page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//set page attributes
			labelParkAndRideHeading.Text = GetResource(RES_PAGEHEADING);
			labelPrinterFriendly.Text = GetResource(RES_PRINTFRIENDLY);
			labelInstructions.Text = GetResource(RES_PRINTINSTRUC);
			labelRegionDrop.Text = GetResource(RES_REGIONDROPTEXT);

			//Tell user control that it's on a printerfriendly page
			parkAndRideTable.PrinterFriendly = true;

			SetRegionElements();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Method used to set up the Regional specifics for the page
		/// </summary>
		private void SetRegionElements()
		{
			if (Request["region"] != null)
			{
				string regionId = Request["region"];

				//Get Park And Rides for selected Region
				parkAndRideData = parkAndRideLocations.GetRegion(regionId);

				//Get Region Text Value selected from the drop down list and display on the read only page.
				IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				string regionText = ds.GetResourceId(DataServiceType.NewsRegionDrop, regionId);
				labelRegionDropValue.Text = regionText;

			}
			else
			{
				//Get Park And Rides for all Regions
				parkAndRideData = parkAndRideLocations.GetAll();

				//Get Text Value for drop down option "All" and then dislay on the read only page.
				IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				string regionText = ds.GetText(DataServiceType.NewsRegionDrop, RES_ALLREGIONID);
				labelRegionDropValue.Text = regionText;
			}

			parkAndRideTable.Data = parkAndRideData;

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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
