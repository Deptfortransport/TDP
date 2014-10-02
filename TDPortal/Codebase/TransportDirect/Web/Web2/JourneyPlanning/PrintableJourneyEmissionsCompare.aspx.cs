// *********************************************** 
// NAME                 : PrintableJourneyEmissionsCompare.aspx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 02/02/2007
// DESCRIPTION			: Printer friendly page to display the CO2 emissions for public transport modes 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableJourneyEmissionsCompare.aspx.cs-arc  $
//
//   Rev 1.3   Dec 19 2008 14:33:26   devfactory
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:48   mturner
//Initial revision.
//
//   Rev 1.1   Mar 05 2007 17:22:38   mmodi
//Set compare control mode
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.0   Feb 20 2007 17:31:34   mmodi
//Initial revision.
//Resolution for 4350: CO2 Public Transport
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
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for PrintableJourneyEmissionsCompare.
	/// </summary>
	public partial class PrintableJourneyEmissionsCompare : TDPage
	{
		#region Controls


		protected TransportDirect.UserPortal.Web.Controls.JourneyEmissionsCompareControl journeyEmissionsCompareControl;
		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public PrintableJourneyEmissionsCompare()
		{
			this.pageId = PageId.PrintableJourneyEmissionsCompare;
		}

		#endregion

		#region Page_Load

		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Initialise text and button properties
			PopulateControls();
			PopulateJourneyEmissionsCompareControl();			
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
			// Page title
			labelTitle.Text = GetResource("JourneyEmissionsCompare.Title");

			labelPrinterFriendly.Text = GetResource("StaticPrinterFriendly.labelPrinterFriendly");
			labelInstructions.Text = GetResource("StaticPrinterFriendly.labelInstructions");

			labelDateTime.Text = TDDateTime.Now.ToString("G");
			labelDateTimeTitle.Text = GetResource("PrintableJourneySummary.labelDateTitle");
			labelUsernameTitle.Text = GetResource("PrintableJourneyMapInput.labelUsernameTitle");

			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;
			labelUsername.Visible = TDSessionManager.Current.Authenticated;
			if( TDSessionManager.Current.Authenticated )
			{
				labelUsername.Text = TDSessionManager.Current.CurrentUser.Username;
			}
		}

		/// <summary>
		/// Populates the JourneyEmissionsCompareControl
		/// </summary>
		private void PopulateJourneyEmissionsCompareControl()
		{
			journeyEmissionsCompareControl.NonPrintable = false;

			// Set the control to Distance state so we show the compare emissions
			journeyEmissionsCompareControl.JourneyEmissionsCompareMode = JourneyEmissionsCompareMode.DistanceDefault;

			//Sets the Units for Road jounreys on the Printable page
			string UrlQueryString = string.Empty;
			//The Query params is set using javascript on the non-printable page
			UrlQueryString = Request.Params["Units"];
			if (UrlQueryString =="kms")
			{
				journeyEmissionsCompareControl.RoadUnits = RoadUnitsEnum.Kms;
			}
			else
			{
				journeyEmissionsCompareControl.RoadUnits = RoadUnitsEnum.Miles;
			}
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
