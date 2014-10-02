// *********************************************** 
// NAME                 : RetailerInformation.aspx.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 21/02/2005 
// DESCRIPTION			: Page displays information for a given retailer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/RetailerInformation.aspx.cs-arc  $
//
//   Rev 1.3   Jan 12 2009 09:41:18   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:44   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:18   mturner
//Initial revision.
//
//   Rev 1.7   Feb 23 2006 19:04:44   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.6   Feb 10 2006 15:09:24   build
//Automatically merged from branch for stream3180
//
//   Rev 1.5.1.1   Dec 07 2005 10:54:50   AViitanen
//Tidied up a commented out line
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.5.1.0   Dec 06 2005 14:42:40   AViitanen
//Changed to use HeaderControl and HeadElementControl as part of Homepage phase2. 
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.5   Nov 09 2005 15:02:28   ECHAN
//Fix for code review comments #4
//
//   Rev 1.4   Nov 03 2005 16:10:08   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.3.1.0   Oct 12 2005 11:37:52   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.4   Oct 11 2005 15:55:00   RGriffith
//Replacing the image button with HTML button
//
//   Rev 1.3   Apr 05 2005 14:03:34   rgeraghty
//Changes made post code review
//Resolution for 1948: DEV CODE REVIEW: FR Retailer Information
//
//   Rev 1.2   Mar 03 2005 18:04:42   rgeraghty
//FxCop changes made
//
//   Rev 1.1   Feb 24 2005 09:23:24   rgeraghty
//Comments added
//
//   Rev 1.0   Feb 22 2005 18:10:38   rgeraghty
//Initial revision.

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
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Page displays information about a Retailer.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class RetailerInformation : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.RetailerInformationControl retailerInformationControl;
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		

		/// <summary>
		/// Constructor
		/// </summary>
		public RetailerInformation() : base()
		{
			//set the page id
			pageId = PageId.RetailerInformation;

			//use the fares and tickets resource manager for this control
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraEventWireUp();
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
			buttonBack.Click += new EventHandler(this.buttonBack_Click);
		}
			
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    			

		}
		#endregion

		#region private methods

		/// <summary>
		/// Set the page's controls
		/// </summary>
		private void InititialiseStaticControls()
		{
			buttonBack.Text = GetResource("RetailerInformation.buttonBack.Text");	
		}

		/// <summary>
		/// Populates the retailer information control based on the retailer id passed 
		/// through the query string
		/// </summary>
		private void GetRetailerInfo()
		{
			RetailerCatalogue retailerCatalogue = (RetailerCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.RetailerCatalogue];

			// find the retailer to display from the querystring
			string retailerId = Server.UrlDecode(Request.QueryString["RetailerId"]);			
			retailerInformationControl.RetailerDetails = retailerCatalogue.FindRetailer(retailerId);			 
		}

		#endregion

		#region event handlers

		/// <summary>
		/// Used to redirect the page to the Ticket Retailers page
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void buttonBack_Click(object sender, EventArgs e)
		{
			//redirect page to TicketRetailers
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoTicketRetailers;
		}
			
		/// <summary>
		/// Page Load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InititialiseStaticControls();

			//Populate the page			
			GetRetailerInfo();

            ConfigureLeftMenu(expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextRetailerInformation);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		#endregion

	}
}
