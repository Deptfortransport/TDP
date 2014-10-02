// *********************************************** 
// NAME                 : RetailerListControl.ascx.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 07/01/2005
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/RetailerListControl.ascx.cs-arc  $
//
//   Rev 1.4   Oct 13 2008 16:44:24   build
//Automatically merged from branch for stream5014
//
//   Rev 1.3.1.0   Sep 25 2008 09:45:18   jfrank
//Fix to make page XHTML transitional
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Apr 21 2008 10:52:54   mmodi
//Updated to ensure correct retailer selected in handoff
//Resolution for 4876: Order of retail handoffs inconsistent with row clicked
//
//   Rev 1.2   Mar 31 2008 13:22:48   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:28   mturner
//Initial revision.
//
//   Rev 1.5   Apr 29 2006 19:39:28   RPhilpott
//Store retailer list to avoid affects of randomisation.
//Resolution for 4036: DD075: Mismatch of retailer selected in Find Cheaper
//
//   Rev 1.4   Feb 23 2006 19:17:04   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.0   Jan 10 2006 15:27:06   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Nov 03 2005 17:06:48   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.2.1.0   Oct 20 2005 18:23:42   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.2   Mar 21 2005 17:02:44   jgeorge
//FxCop changes
//
//   Rev 1.1   Mar 08 2005 16:23:34   jgeorge
//Added code for Printer Friendly mode
//
//   Rev 1.0   Mar 04 2005 11:51:22   jgeorge
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.ScreenFlow;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.ServiceDiscovery;

	/// <summary>
	///	Displays a list of retailers with "buy" buttons.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class RetailerListControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{

		#region Private members

		private string resourceBuyText;
		private string handoffPageUrl;
		private bool isReturn;
		private bool isOnline;
		private Retailer[] retailers;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor. Sets the local resource manager
		/// </summary>
		public RetailerListControl() : base()
		{
			LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#endregion

		#region Event

		/// <summary>
		/// Event raised when the user clicks a "Buy" button.
		/// </summary>
		public event RetailerSelectedEventHandler RetailerSelected;

		#endregion

		#region Event handlers

		/// <summary>
		/// Wires additional events
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Handles the load event of the control. Loads resources and rebinds the
		/// repeater to ensure that events are raised correctly.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Load resources
			LoadResources();

			// Rebind the data to ensure that events are raised correctly
			DisplayData();
		}

		/// <summary>
		/// Handles the PreRender event of the control. Rebinds the repeater
		/// to reflect any changes to the data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			DisplayData();
		}

		/// <summary>
		/// Sets properties of the controls in an item of the repeater
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void retailersList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				HyperLink linkRetailerName = (HyperLink)e.Item.FindControl("linkRetailerName");
				Label labelRetailerName = (Label)e.Item.FindControl("labelRetailerName");
				TDButton buyButton = (TDButton)e.Item.FindControl("buyButton");

				Retailer retailer = (Retailer)e.Item.DataItem;

				string retailerLink = GetRetailerInformationLink(retailer);

				if ((retailer.SmallIconUrl != null) && (retailer.SmallIconUrl.Length != 0))
				{
					PlaceHolder logoPlaceholder = (PlaceHolder)e.Item.FindControl("logoPlaceholder");
					HtmlImage logo = new HtmlImage();
					logo.Src = retailer.SmallIconUrl;
					logo.Alt = retailer.Name;
					logo.Align = "middle";

					if (PrinterFriendly)
						logoPlaceholder.Controls.Add(logo);
					else
					{
						HyperLink imageLink = new HyperLink();
						imageLink.NavigateUrl = retailerLink;
						imageLink.Controls.Add(logo);

						logoPlaceholder.Controls.Add(imageLink);
					}
				}

				if (PrinterFriendly)
				{
					linkRetailerName.Visible = false;
					labelRetailerName.Visible = true;
					labelRetailerName.Text = retailer.Name;
				}
				else
				{
					linkRetailerName.Visible = true;
					labelRetailerName.Visible = false;
					linkRetailerName.Text = retailer.Name;
					linkRetailerName.NavigateUrl = retailerLink;
				}
				
				buyButton.Visible = !PrinterFriendly;
				
				if (!PrinterFriendly)
				{
					buyButton.Text = resourceBuyText;

                    // assign the retailer id to the button so we know which retailer user has clicked
                    buyButton.CommandArgument = retailer.Id;
				}
			}
		}

		/// <summary>
		/// Handles the user clicking a Buy button and raises the appropriate event
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void retailersList_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			Retailer[] savedRetailers = null;
            Retailer selectedRetailer = null;

			InputPageState pageState = TDSessionManager.Current.InputPageState;

			if	(isReturn)
			{
				savedRetailers = (isOnline ? pageState.ReturnOnlineRetailers : pageState.ReturnOfflineRetailers);
			}
			else
			{
				savedRetailers = (isOnline ? pageState.OutwardOnlineRetailers : pageState.OutwardOfflineRetailers);
			}

            // Get the selected retailer id
            string retailerIdArg = (string)e.CommandArgument;

            // Find and set the retailer
            foreach (Retailer retailer in savedRetailers)
            {
                if (retailer.Id == retailerIdArg)
                {
                    selectedRetailer = retailer;
                    break;
                }
            }

            // Ensure we always return a retailer, even if it might be the wrong one!
            if (selectedRetailer == null)
            {
                selectedRetailer = savedRetailers[e.Item.ItemIndex];
            }

			OnRetailerSelected( new RetailerSelectedEventArgs( selectedRetailer ) );
		}


		#endregion

		#region Public properties

		/// <summary>
		/// The list of retailers to be displayed by the control
		/// </summary>
		public Retailer[] Retailers
		{
			get { return retailers; }
			set { retailers = value; }
		}

		public bool IsOnline
		{
			get { return isOnline; }
			set { isOnline = value; }
		}

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
			get { return handoffPageUrl; }
			set { handoffPageUrl = value; }
		}

		/// <summary>
		/// The summary for the table. Used for databinding
		/// </summary>
		public string ListTableSummary
		{
			get { return GetResource( "RetailerListControl.TableSummary" ); }
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
		/// Rebinds the repeater
		/// </summary>
		private void DisplayData()
		{
			if ((retailers != null) && (retailers.Length != 0))
			{
				retailersList.DataSource = retailers;
				retailersList.DataBind();
			}
		}

		/// <summary>
		/// Loads resources from the resource manager into local strings
		/// </summary>
		private void LoadResources()
		{
			resourceBuyText = GetResource( "RetailerListControl.Buy.Text" );
		}

		/// <summary>
		/// Returns the link used to navigate to the retailer information page. The URL
		/// is obtained from the PageController in ServiceDiscovery
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		private string GetRetailerInformationLink(Retailer r)
		{
			IPageController pageController = (IPageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(TransportDirect.Common.PageId.RetailerInformation);

			string url = pageTransferDetails.PageUrl;
			if (TDPage.SessionChannelName !=  null )
				url = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + url; 

			if (url.IndexOf("?") == -1)
				url += "?";
			else
				url += "&";
			url += "RetailerId=" + Server.UrlEncode(r.Id);

			return url;
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
			this.retailersList.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.retailersList_ItemDataBound);
			this.retailersList.ItemCommand += new RepeaterCommandEventHandler(retailersList_ItemCommand);

		}
		#endregion
	}

	/// <summary>
	/// EventArgs class used for retailer selection event
	/// </summary>
	public class RetailerSelectedEventArgs : EventArgs
	{
        private readonly Retailer selectedRetailer;

		/// <summary>
		/// Constructor. The retailer that was selected must be specified
		/// </summary>
		/// <param name="selectedRetailer"></param>
		public RetailerSelectedEventArgs(Retailer selectedRetailer)
		{
			this.selectedRetailer = selectedRetailer;
		}

		/// <summary>
		/// Read only property allowing access to the selected retailer
		/// </summary>
		public Retailer SelectedRetailer
		{
			get { return selectedRetailer; }
		}
	}

	/// <summary>
	/// Delegate type for events raised by the RetailerListControl and RetailerMatrixControl
	/// </summary>
	public delegate void RetailerSelectedEventHandler(object sender, RetailerSelectedEventArgs e);

}
