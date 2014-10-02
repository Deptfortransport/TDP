// *********************************************** 
// NAME                 : AmendFaresControl.ascx.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 04/01/2005
// DESCRIPTION			: Fares control pane for the AmendSaveSend control.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendFaresControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:19:10   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:02   mturner
//Initial revision.
//
//   Rev 1.15   Nov 14 2006 09:58:08   rbroddle
//Merge for stream4220
//
//   Rev 1.14.1.0   Nov 12 2006 15:34:28   tmollart
//Added property to allow changing of coach options visibility.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.15   Nov 12 2006 15:29:56   tmollart
//Added property to allow changing of coach options visibility.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.14   May 04 2006 12:02:56   RPhilpott
//Moved prerender actions into manualPrerender() method so they can be invoked earlier by parent page.
//Resolution for 4071: Del 8.1: Return fares displayed on costs page but Amend fares tab shows single
//
//   Rev 1.13   May 04 2006 10:52:16   rgreenwood
//IR4029: added base.OnPreRender() call
//Resolution for 4029: DN058 Park and Ride Phase 2 - Landmark data format includes park and ride text
//
//   Rev 1.12   May 03 2006 12:01:32   rgreenwood
//IR4026: Moved populateDropDowns() method call into OnPreRender
//Resolution for 4026: Homepage Phase 2: Fare details not reset if plan new journey using tab at top
//
//   Rev 1.11   Feb 24 2006 17:23:18   build
//Removed duplicate namespace reference
//
//   Rev 1.10   Feb 23 2006 19:16:20   build
//Automatically merged from branch for stream3129
//
//   Rev 1.9.1.1   Jan 30 2006 14:41:00   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9.1.0   Jan 10 2006 15:23:18   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9   Nov 09 2005 16:58:46   jgeorge
//Manual merge for stream2818 (Search by Price)
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7.2.0   Nov 08 2005 20:18:16   RPhilpott
//Use no ReturnsOnly dropdown list if no "Two Singles" available.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.8   Nov 03 2005 17:05:44   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.7.1.0   Oct 17 2005 17:22:16   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.7   Apr 14 2005 17:59:32   rhopkins
//Test SelectedTicketType to determine which TicketType dropdown list to display.
//Resolution for 2189: FindFare: Requesting Open Return, Date Selection shows Return/Singles in Amend Fare
//
//   Rev 1.6   Apr 08 2005 14:07:10   rgeraghty
//fxcop changes
//
//   Rev 1.5   Mar 31 2005 11:45:52   rgeraghty
//Fx Cop changes applied
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.4   Mar 30 2005 11:37:36   rgeraghty
//Amended population of dropdowns including ShowchildFares
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.3   Mar 14 2005 19:07:36   rhopkins
//Exposed embeded dropdowns to facilitate the addition of event handlers.  Also allow ItineraryType dropdown to vary, depending upon the Mode of the results.
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.2   Feb 24 2005 14:10:50   rgeraghty
//Separate fares and tickets resource file referenced
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.1   Jan 18 2005 17:20:42   rgeraghty
//Added ILanguageHandlerIndependent
//
//   Rev 1.0   Jan 10 2005 17:22:52   rgeraghty
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ResourceManager;

	
	/// <summary>
	///	Displays fare options.  A user may select the type of rail or coach discount card 
	///	filtered by adult or child fares and itinerary type.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class AmendFaresControl : TDUserControl, ILanguageHandlerIndependent
	{

		private bool preRenderDone ;  

		#region constants

		//Note that constant strings are used here which match the database resourceId entries. These are 
		//needed to enable a language independent way of identifying whether adult or child fares have been selected
		private const string adultString ="Adult";
		private const string childString ="Child";

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public AmendFaresControl()
		{
			//use the fares and tickets resource manager for this control
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}


		#region Event Handlers
		/// <summary>
		/// Page load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			initialiseStaticControls();
	
		}

		protected override void OnPreRender(System.EventArgs e)
		{
			// DO NOT put state changing logic in here - put it in the manualPreRender()

			if (!preRenderDone )
			{
				manualPreRender();
			}

			base.OnPreRender(e);
		}

		/// <summary>
		/// Allows the OnPreRender() of the parent page/control to invoke the prerender logic
		/// of this control earlier than normal, so that the resulting state of the control
		/// can be interrogated in the parent's OnPreRender() method.
		/// </summary>
		public void manualPreRender()
		{
            //removing the ISPostBack condition to facilitate change of language
            //if	(!IsPostBack) 
            //{				
				//Populate drop downs from DataServices
				populateDropDowns();
            //}
			preRenderDone = true;
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

		#region private methods

		/// <summary>
		/// Populates the drop down lists with the allowed values from DataServices
		/// </summary>
		private void populateDropDowns()
		{
            int indexdropDownListCoachCardSelect = dropDownListCoachCardSelect.SelectedIndex;
            int indexdropDownListRailcardSelect = dropDownListRailcardSelect.SelectedIndex;
            int indexdropDownListAgeSelect = dropDownListAgeSelect.SelectedIndex;
            int indexdropDownListItineraryTypeSelect = dropDownListItineraryTypeSelect.SelectedIndex;

			ITDSessionManager sessionManager = TDSessionManager.Current;
			IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			populator.LoadListControl(DataServiceType.DiscountCoachCardDrop,dropDownListCoachCardSelect);
			populator.LoadListControl(DataServiceType.DiscountRailCardDrop,dropDownListRailcardSelect);
			populator.LoadListControl(DataServiceType.AdultChildDrop,dropDownListAgeSelect);

			// Populate ItineraryType with different values depending upon Mode of results
			if (sessionManager.IsFindAMode && FindFareInputAdapter.IsCostBasedSearchMode(sessionManager.FindAMode))
			{
				FindFareCostFacadeAdapter facadeAdapter = new FindFareCostFacadeAdapter((FindCostBasedPageState)sessionManager.FindPageState);
				populator.LoadListControl(facadeAdapter.CurrentTicketTypeDropDownList, dropDownListItineraryTypeSelect);
			}
			else
			{
				populator.LoadListControl(DataServiceType.SingleReturnDrop,dropDownListItineraryTypeSelect);
			}

            //restoring the selected index for postback condition
            dropDownListCoachCardSelect.SelectedIndex = indexdropDownListCoachCardSelect;
            dropDownListRailcardSelect.SelectedIndex = indexdropDownListRailcardSelect;
            dropDownListAgeSelect.SelectedIndex = indexdropDownListAgeSelect;
            dropDownListItineraryTypeSelect.SelectedIndex = indexdropDownListItineraryTypeSelect;
		}

		/// <summary>
		/// Initialise the static page controls
		/// </summary>
		private void initialiseStaticControls() 
		{
			buttonOK.Text = GetResource("AmendFaresControl.buttonOK.Text" );

			//set the label texts 
			labelShowType.Text =GetResource("AmendFaresControl.labelShowType");

			labelShowAge.Text =GetResource("AmendFaresControl.labelShowAge");

			labelRailcard.Text =GetResource("AmendFaresControl.labelRailCard");

			labelCoachCard.Text =GetResource("AmendFaresControl.labelCoachCard");

			labelInfo.Text =GetResource("AmendFaresControl.labelInfo");

			labelFaresAge.Text =GetResource("AmendFaresControl.labelFaresAge");

			labelFaresItinerary.Text =GetResource("AmendFaresControl.labelFaresItinerary");
								
		}

		#endregion

		#region properties

		/// <summary>
		/// Gets the selected item in the Itinerary drop down list for Time-Based planning
		/// </summary>	
		public ItineraryType ShowItineraryType
		{
			get 
			{ 				
				if (dropDownListItineraryTypeSelect.SelectedItem.Value.Equals(ItineraryType.Single.ToString()))
					return ItineraryType.Single;
				else
					return ItineraryType.Return;
			}

			set
			{
				//select the appropriate item in the dropdown
				ListItem itemItinerary;
				itemItinerary = dropDownListItineraryTypeSelect.Items.FindByValue(value.ToString());
				dropDownListItineraryTypeSelect.SelectedIndex = dropDownListItineraryTypeSelect.Items.IndexOf(itemItinerary);
			}
		}

		/// <summary>
		/// Gets the selected item in the Itinerary drop down list for Cost-Based planning
		/// </summary>
		public TicketType ShowCostBasedTicketType
		{
			get 
			{
				return (TicketType)Enum.Parse(typeof(TicketType), dropDownListItineraryTypeSelect.SelectedItem.Value, true);
			}

			set
			{
				//select the appropriate item in the dropdown
				ListItem ticketType;
				ticketType = dropDownListItineraryTypeSelect.Items.FindByValue(value.ToString());
				dropDownListItineraryTypeSelect.SelectedIndex = dropDownListItineraryTypeSelect.Items.IndexOf( ticketType );
			}
		}

		/// <summary>
		/// Gets/sets whether child fares is selected. 
		/// </summary>	
		public bool ShowChildFares 
		{

			get
			{
				//Return true if the selected items value is the same as child string.
				return (dropDownListAgeSelect.SelectedItem.Value == childString);				
			}

			set
			{
				//If value is true select item identified by item child
				//otherwise use item identified by adult.
				if (value == true)
				{					
					IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
					populator.Select(dropDownListAgeSelect, childString);					
				}
				else
				{
					IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
					populator.Select(dropDownListAgeSelect, adultString);

				}
			}
		}

		/// <summary>
		/// Gets the selected item in the Coach Discount drop down list
		/// </summary>		
		public string CoachCard
		{
			get
			{
				IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];				
				return populator.GetValue(DataServiceType.DiscountCoachCardDrop, dropDownListCoachCardSelect.SelectedItem.Value);
			}
			set 
			{
				//select the appropriate item in the dropdown				
				IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				populator.Select(dropDownListCoachCardSelect, populator.GetResourceId(DataServiceType.DiscountCoachCardDrop, value));
			}
		}

		/// <summary>
		/// Gets the selected item in the Rail Discount drop down list
		/// </summary>		
		public string RailCard
		{
			get
			{
				IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				return populator.GetValue(DataServiceType.DiscountRailCardDrop, dropDownListRailcardSelect.SelectedItem.Value);
			}

			set 
			{				
				//select the appropriate item in the dropdown				
				IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				populator.Select(dropDownListRailcardSelect, populator.GetResourceId(DataServiceType.DiscountRailCardDrop, value));			
				
			}
		}

		/// <summary>
		/// Exposes the ItineraryType dropdown list so that the page containing the control can handle its events
		/// </summary>
		/// <returns>The control's ItineraryType dropdown list</returns>
		public DropDownList DropDownListItineraryType
		{
			get { return this.dropDownListItineraryTypeSelect; }
		}

		/// <summary>
		/// Exposes the Age dropdown list so that the page containing the control can handle its events
		/// </summary>
		/// <returns>The control's Age dropdown list</returns>
		public DropDownList DropDownListAge
		{
			get { return this.dropDownListAgeSelect; }
		}

		/// <summary>
		/// Exposes the Railcard dropdown list so that the page containing the control can handle its events
		/// </summary>
		/// <returns>The control's Railcard dropdown list</returns>
		public DropDownList DropDownListRailCard
		{
			get { return this.dropDownListRailcardSelect; }
		}

		/// <summary>
		/// Exposes the CoachCard dropdown list so that the page containing the control can handle its events
		/// </summary>
		/// <returns>The control's CoachCard dropdown list</returns>
		public DropDownList DropDownListCoachCard
		{
			get { return this.dropDownListCoachCardSelect; }
		}

		/// <summary>
		/// Exposes the OK image button so that the page containing the control can handle its events
		/// </summary>
		/// <returns>The control's OK image button</returns>
		public TDButton OKButton
		{
			get{ return this.buttonOK;}
		}

		/// <summary>
		/// Write Only. Sets the visibility of the coach card discount label
		/// and drop down.
		/// </summary>
		public bool CoachCardOptionsVisible
		{
			set 
			{
				labelCoachCard.Visible = value;
				dropDownListCoachCardSelect.Visible = value;
			}
		}

		#endregion

	}
}
