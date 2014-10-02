// *********************************************** 
// NAME                 : FindFareSingleTravelDatesControl.ascx.cs 
// AUTHOR               : Richard Hopkins
// DATE CREATED         : 20/01/2005
// DESCRIPTION			: A custom control to display a list of 
//						  possible fares for a range of dates and travel modes
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindFareSingleTravelDatesControl.ascx.cs-arc  $
//
//   Rev 1.5   Oct 27 2010 13:50:26   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.4   Jan 08 2009 10:49:50   devfactory
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Dec 17 2008 13:08:58   devfactory
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:20:38   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:14   mturner
//Initial revision.
//
//   Rev 1.10   Jul 16 2007 18:21:46   asinclair
//Removed code setting text for labels
//Resolution for 4452: 9.6 - WAI Improvements to Find a Fare Tables
//
//   Rev 1.9   Jul 10 2007 12:22:54   asinclair
//Changed method called for setting screenreader label
//Resolution for 4452: 9.6 - WAI Improvements to Find a Fare Tables
//
//   Rev 1.8   Jun 26 2007 11:36:48   asinclair
//Added screenreader labels and replaced imagebuttons with TDButtons
//
//   Rev 1.7   Feb 23 2006 19:16:36   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.0   Jan 10 2006 15:24:42   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Apr 29 2005 21:06:46   rhopkins
//Changes to column styles, and some columns combined together
//Resolution for 2109: PT - Find a fare layout - Select date/transport option page
//Resolution for 2141: PT - Incorrect text on Date/Transport screen for Cheaper Fares
//Resolution for 2329: PT - Find Fare Date Selection column widths
//
//   Rev 1.5   Apr 08 2005 20:02:44   rhopkins
//Suppress header for "Select" column when in printer-friendly mode
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.4   Mar 30 2005 16:22:28   rhopkins
//Correct event handling for TravelDate table
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.3   Mar 29 2005 14:51:28   rhopkins
//Changed to use ResultsTableTitleControl
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.2   Mar 17 2005 14:42:06   rhopkins
//Change the way that the sort click events are handled
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.1   Feb 25 2005 12:42:42   rhopkins
//Modified to use base class FindFareTravelDatesBaseControl so that FindFareReturnTravelDatesControl can be created.
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.0   Feb 15 2005 16:35:06   rhopkins
//Initial revision.
//

namespace TransportDirect.UserPortal.Web.Controls
{

	using System;using TransportDirect.Common.ResourceManager;
	using System.Web.UI.WebControls;

	/// <summary>
	///	Summary description for FindFareSingleTravelDatesControl.
	/// </summary>
	public partial class FindFareSingleTravelDatesControl : FindFareTravelDatesBaseControl
	{

		protected System.Web.UI.WebControls.ImageButton HeaderImageSingleDate;
		protected System.Web.UI.WebControls.ImageButton HeaderImageTransportMode;
		protected System.Web.UI.WebControls.ImageButton HeaderImageLowestFare;


		protected ResultsTableTitleControl resultsTableTitleControlOutward;

		
		protected System.Web.UI.WebControls.Label labelSRDate;
		new protected System.Web.UI.WebControls.Label labelSRTransport;
		new protected System.Web.UI.WebControls.Label labelSRFareRange;
		new protected System.Web.UI.WebControls.Label labelSRSelect;
		new protected System.Web.UI.WebControls.Label labelSRAvailable;

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

		#region Private Methods

		/// <summary>
		/// Initialise the control
		/// </summary>
		protected override void Initialise()
		{
			base.travelDateRepeater = travelDateRepeaterSingle;
			base.resultsTableTitleControl = resultsTableTitleControlOutward;

			OutwardDayNameCssClass = "fftdbdysingleoutdateday";
			OutwardDayMonthCssClass = "fftdbdysingleoutdatedaymonth";
			TravelModeCssClass = "fftdbdysinglemode";
			FareRangeFareRangeCssClass = "fftdbdysinglefarerange";
			LowestProbableFareCssClass = "fftdbdysinglelowestfare";
			RadioButtonCssClassNoScroll = "fftdbdysingleselectnoscroll";
			RadioButtonCssClassScroll = "fftdbdysingleselectscroll";

			showReturnTickets = false;

			base.Initialise();

			AddEventHandlers();
		}

		/// <summary>
		/// Adds the event handlers to the sort buttons in the table header
		/// </summary>
		private void AddEventHandlers()
		{
			// Sorting is only required if this is a non-printable page and there is more than one row
			if (!this.PrinterFriendly && (displayableTravelDates.Length > 1))
			{
				tdButtonSingleDate.Click += new EventHandler(HeaderButtonSingleDate_Click);
				tdButtonTransport.Click  += new EventHandler(HeaderButtonTransportMode_Click);
				tdLinkButtonLowestFare.Click += new  EventHandler(HeaderLinkLowestFare_Click);
			}
		}

		/// <summary>
		/// Apply the images and text to the table headers
		/// </summary>
		private void SetupControls()
		{
			// Sortable columns
			ColumnHeader("SingleDate", HeaderTextSingleDate, IconSingleDate);
			ColumnHeader("TransportMode", HeaderTextTransportMode, IconTransportMode);
			ColumnHeader("LowestFare", HeaderTextLowestFare, IconLowestFare);

			// Non-sortable columns
			HeaderTextFareRange.Text = HeaderFareRangeText();

			if (!this.PrinterFriendly)
			{
				fftdhdrsingleselect.Visible = true;
				HeaderTextSelect.Text = HeaderText("Select");
			}
			else
			{
				fftdhdrsingleselect.Visible = false;
			}
			
//			labelSRDate.Text = GetResource("FindFareTravelDatesControl.labelSRDate");
//			labelSRTransport.Text = GetResource("FindFareTravelDatesControl.labelSRTransport");
//			labelSRFareRange.Text = HeaderFareRangeText();
//			labelSRAvailable.Text = GetResource("FindFareTravelDatesControl.labelSRAvailable");
//			labelSRSelect.Text = GetResource("FindFareTravelDatesControl.labelSRSelect.Text");

			if(ShowSortableLinks)
			{
				PopulateTDButtons();
			}

		}

		/// <summary>
		/// Populates the TDButton controls with text, cssClass, and CssClassMouseOver properties.
		/// </summary>
		private void PopulateTDButtons()
		{
			tdButtonSingleDate.Visible = true;
			tdButtonSingleDate.Text = GetResource("FindFareTravelDatesControl.labelDate");
			tdButtonSingleDate.CssClass = "TDHyperLinkStyleButton";
			tdButtonSingleDate.CssClassMouseOver = "TDHyperLinkStyleButtonMouseOver";

			tdButtonTransport.Visible = true;
			tdButtonTransport.Text = GetResource("FindFareTravelDatesControl.labelTransport");
			tdButtonTransport.CssClass  = "TDHyperLinkStyleButton";
			tdButtonTransport.CssClassMouseOver  = "TDHyperLinkStyleButtonMouseOver";

			tdLinkButtonLowestFare.Text = GetResource("FindFareTravelDatesControl.linkAvailableText");
			tdLinkButtonLowestFare.Visible = true;

		}

		#endregion Private Methods

		#region EventHandlers

		/// <summary>
		/// Page Load event code
		/// </summary>
		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.Visible)
			{
				Initialise();
			}
		}

		/// <summary>
		/// PreRender method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void Page_PreRender(object sender, EventArgs e)
		{
			if (this.Visible)
			{
				SetupControls();
			}

			base.Page_PreRender (sender, e);
		}

		#endregion
	}
}
