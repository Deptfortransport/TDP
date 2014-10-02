// *********************************************** 
// NAME                 : RetailerMatrixControl.ascx.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 07/01/2005
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/RetailerMatrixControl.ascx.cs-arc  $
//
//   Rev 1.4   Oct 28 2010 12:47:36   rbroddle
//Removed explicit wire up to Page_Init as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.3   Sep 17 2009 13:39:46   jfrank
//Fix for retailer handoff to incorrect retailer
//Resolution for 5303: Retail handoff to rail retailler for coach tickets
//
//   Rev 1.2   Mar 31 2008 13:22:50   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:30   mturner
//Initial revision.
//
//   Rev 1.6   Jun 21 2006 17:17:52   rphilpott
//Store retailer list in session InputPageState if the existing list is empty or null, even if we are in a PostBack.
//Resolution for 4123: Retailer Handoff -- selection of wrong retailer
//
//   Rev 1.5   Apr 29 2006 19:39:30   RPhilpott
//Store retailer list to avoid affects of randomisation.
//Resolution for 4036: DD075: Mismatch of retailer selected in Find Cheaper
//
//   Rev 1.4   Feb 23 2006 19:17:06   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.0   Jan 10 2006 15:27:08   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Mar 21 2005 17:02:44   jgeorge
//FxCop changes
//
//   Rev 1.2   Mar 04 2005 09:29:02   jgeorge
//Changes for revised structure
//
//   Rev 1.1   Feb 22 2005 17:31:34   jgeorge
//Interim check-in
//
//   Rev 1.0   Jan 18 2005 11:44:38   jgeorge
//Initial revision.

using System;using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Web.Support;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Displays one or two lists of retailers with "Buy" buttons.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class RetailerMatrixControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{
		#region Control declaration

		protected RetailerListControl onlineList;
		protected RetailerListControl offlineList;

		#endregion

		#region Event

		/// <summary>
		/// Event raised when the user clicks a "Buy" button.
		/// </summary>
		public event RetailerSelectedEventHandler RetailerSelected;

		#endregion

		#region Private members

		private RetailUnit retailUnit;
		private bool isReturn;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor. Sets up the LocalResourceManager
		/// </summary>
		public RetailerMatrixControl() : base()
		{
			LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Handler for the Init event of the page. Wires up additional events
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			onlineList.RetailerSelected += new RetailerSelectedEventHandler(retailerListControl_RetailerSelected);
			offlineList.RetailerSelected += new RetailerSelectedEventHandler(retailerListControl_RetailerSelected);
		}

		/// <summary>
		/// Handler for the Load event. Ensures that controls are repopulated with data to 
		/// allow events to be raised correctly.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			DisplayData();
		}


		/// <summary>
		/// Handler for the PreRender event. Loads static label text and updates the data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			PopulateLabels();
			DisplayData();
		}

		/// <summary>
		/// Handles the RetailerSelected event for both List controls and raises the event up to the
		/// container
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void retailerListControl_RetailerSelected(object sender, RetailerSelectedEventArgs e)
		{
			OnRetailerSelected(e);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The retail unit containing the data to be displayed by this control
		/// </summary>
		public RetailUnit RetailUnit
		{
			get { return retailUnit; }
			set { retailUnit = value; }
		}

		/// <summary>
		/// The retail unit containing the data to be displayed by this control
		/// </summary>
		public bool IsReturn
		{
			get { return isReturn; }
			set { isReturn = value; }
		}

		/// <summary>
		/// The URL of the handoff page - this is the page that will be opened when the user
		/// clicks the "Buy" button. Not required when being used on the Printable page.
		/// </summary>
		public string HandoffPageUrl
		{
			get { return onlineList.HandoffPageUrl; }
			set 
			{
				onlineList.HandoffPageUrl = value; 
				offlineList.HandoffPageUrl = value; 
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Raises the RetailerSelected event
		/// </summary>
		/// <param name="e"></param>
		private void OnRetailerSelected(RetailerSelectedEventArgs e)
		{
			RetailerSelectedEventHandler theDelegate = RetailerSelected;
			if (theDelegate != null)
				theDelegate(this, e);
		}

		/// <summary>
		/// Populates the labels with strings from the resource file
		/// </summary>
		private void PopulateLabels()
		{
			labelTicketRetailers.Text = GetResource( "RetailerMatrixControl.TicketRetailers" );
			labelOnline.Text = GetResource( "RetailerMatrixControl.BuyOnline" );
			labelOffline.Text = GetResource( "RetailerMatrixControl.BuyOffline" );
		}


		/// <summary>
		/// Shows/hides the correct panels and sets properties on the contained controls
		/// </summary>
		private void DisplayData()
		{
			InputPageState pageState = TDSessionManager.Current.InputPageState;

			if (retailUnit != null)
			{
				if ((retailUnit.OnlineRetailers != null) && (retailUnit.OnlineRetailers.Length > 0))
				{
					if	(isReturn)
					{
						pageState.ReturnOnlineRetailers = retailUnit.OnlineRetailers;
					}
					else 
					{
						pageState.OutwardOnlineRetailers = retailUnit.OnlineRetailers;
					}

					onlinePanel.Visible = true;
					onlineList.Retailers = retailUnit.OnlineRetailers;
					onlineList.IsReturn = isReturn;
					onlineList.IsOnline = true;
				}
				else
				{
					onlinePanel.Visible = false;
				}

				if ((retailUnit.OfflineRetailers != null) && (retailUnit.OfflineRetailers.Length > 0))
				{
					if	(isReturn)
					{
						pageState.ReturnOfflineRetailers = retailUnit.OfflineRetailers;
					}
					else
					{
						pageState.OutwardOfflineRetailers = retailUnit.OfflineRetailers;
    				}

					offlinePanel.Visible = true;
					offlineList.Retailers = retailUnit.OfflineRetailers;
					offlineList.IsReturn = isReturn;
					offlineList.IsOnline = false;
				}
				else
				{
					offlinePanel.Visible = false;
				}
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.EnableViewState = false;

		}

		#endregion


	}
}
