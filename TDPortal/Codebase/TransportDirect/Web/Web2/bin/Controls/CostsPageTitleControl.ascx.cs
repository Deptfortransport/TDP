// *********************************************** 
// NAME                 : CostsPageTitleControl.ascx.cs 
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 21/12/2003
// DESCRIPTION          : Header for CarJourneyItemisedCostsControl
//
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CostsPageTitleControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Apr 04 2008 18:25:44   mmodi
//Removed help button
//
//   Rev 1.2   Mar 31 2008 13:19:48   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:56   mturner
//Initial revision.
//
//   Rev 1.15   Apr 03 2006 16:21:18   AViitanen
//Moved setting OK button out of post back check.
//Resolution for 3746: DN068 Extend: Faulty position of Help button in login/register control when on Tickets/costs page
//
//   Rev 1.14   Mar 17 2006 15:56:22   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.13   Mar 16 2006 12:01:26   RGriffith
//Fix to display locations labels on PrintableRefineTickets page
//
//   Rev 1.12   Mar 14 2006 10:30:14   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.11   Feb 23 2006 19:16:28   build
//Automatically merged from branch for stream3129
//
//   Rev 1.10.2.1   Mar 13 2006 12:00:38   RGriffith
//Changes to make Car Fuel Costs drop down printer friendly
//
//   Rev 1.10.2.0   Mar 01 2006 13:13:46   RGriffith
//Changes for use with Tickets and Costs page
//
//   Rev 1.10   Nov 03 2005 17:05:48   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.10.1.0   Jan 10 2006 15:23:56   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9.1.0   Oct 18 2005 17:46:24   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.9   Apr 04 2005 17:03:56   esevern
//amendments to comply with FXCop
//
//   Rev 1.8   Mar 30 2005 11:43:22   esevern
//added setting of ok button alt text
//
//   Rev 1.7   Mar 23 2005 16:10:18   esevern
//check page id - if printable, hide drop down/buttons etc
//
//   Rev 1.6   Mar 14 2005 12:38:28   esevern
//moved setting of ok button image url to code behind page (obtained from lang strings)
//
//   Rev 1.5   Mar 07 2005 17:53:10   jgeorge
//Checked in other users changes to allow code to build successfully.
//
//   Rev 1.4   Feb 11 2005 15:52:08   rgreenwood
//Exposed ShowCostTypeControl to provide access to its events
//
//   Rev 1.3   Jan 14 2005 14:06:08   rgreenwood
//Work in progress
//
//   Rev 1.2   Jan 11 2005 10:26:36   rgreenwood
//Work in Progress
//
//   Rev 1.1   Jan 06 2005 13:13:26   rgreenwood
//Initial revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common;
	using TransportDirect.Common.ResourceManager;

	/// <summary>
	///	Control providing choice of fuel types via a drop down list and
	///	access to car costing help
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class CostsPageTitleControl : TDUserControl
	{

		// Controls & UserControls		
		protected TransportDirect.UserPortal.Web.Controls.ShowCostTypeControl costTypeControl;

		// Private members
		private bool hideHelpButton;
		private string startLocation;
		private string endLocation;

		/// <summary>
		/// Sets up button image and visibility of controls - if this is the
		/// printable journey page, the help and ok buttons should be hidden
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// ok button alt text
			buttonOK.Text = GetResource("CostsPageTitleControl.buttonOK.Text");

			if ((((TDPage)Page).PageId == PageId.PrintableJourneyFares)
				|| (((TDPage)Page).PageId == PageId.PrintableRefineTickets))
			{
				// hide the okbutton 
				buttonOK.Visible = false;
				
				if (((TDPage)Page).PageId == PageId.PrintableRefineTickets)
				{
					labelLocations.Visible = true;
					labelLocations.Text = startLocation + GetResource("RefineTickets.LocationSeperator.Text") + endLocation;
				}
				else
				{
					labelLocations.Visible = false;
				}
			}
			else 
			{
				if (!hideHelpButton)
				{
					labelLocations.Visible = false;
				}
				else
				{
					labelLocations.Visible = true;
					labelLocations.Text = startLocation + GetResource("RefineTickets.LocationSeperator.Text") + endLocation;
				}
			}

			if(IsPostBack)
			{
				if (hideHelpButton)
				{
					labelLocations.Visible = true;
					labelLocations.Text = startLocation + GetResource("RefineTickets.LocationSeperator.Text") + endLocation;
				}
			}
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


		#region properties

		/// <summary>
		///	Read only property returning the selected index of
		///	the fuel type drop down list in the ShowCostTypeControl 
		/// </summary>
		public int ShowSelectedCost
		{
			get { return (int)costTypeControl.ShowSelectedCost; }
		}

		/// <summary>
		/// Read only property returning reference to the ShowCostTypeControl
		/// </summary>
		public ShowCostTypeControl ShowCostTypeControl
		{
			get
			{
				return (costTypeControl);
			}
		}
		
		/// <summary>
		/// Read only property returning CostPageTitle label
		/// </summary>
		public Label Title 
		{
			get 
			{
				return labelCostPageTitle;
			}
		}

		/// <summary>
		/// Read only property returning the ok image button reference
		/// </summary>
		public TransportDirect.UserPortal.Web.Controls.TDButton OKButton 
		{
			get {	return buttonOK;  }
		}

		/// <summary>
		/// Read/write property to hide the help button and initialise appropriate labels in the CostsPageTitle
		/// </summary>
		public bool HideHelpButton
		{
			get { return hideHelpButton; }
			set { hideHelpButton = value; }
		}

		/// <summary>
		/// Read/write property to set the start location label of the control
		/// </summary>
		public string StartLocation
		{
			get {return startLocation;}
			set {startLocation = value;}
		}

		/// <summary>
		/// Read/write property to set the end location label of the control
		/// </summary>
		public string EndLocation
		{
			get {return endLocation;}
			set {endLocation = value;}
		}
		#endregion

	}
}
