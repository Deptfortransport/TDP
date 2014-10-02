// *********************************************** 
// NAME                 : AmendCostSearchDateControl.ascx.cs 
// AUTHOR               : Richard Hopkins
// DATE CREATED         : 02/03/2005
// DESCRIPTION			: Cost Search Date control pane for the AmendSaveSend control.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendCostSearchDateControl.ascx.cs-arc  $
//
//   Rev 1.5   Oct 26 2010 14:20:06   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.4   Jan 26 2009 12:55:18   apatel
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 07 2008 15:27:06   scraddock
//See main comment.
//Resolution for 4839: Amend date not working for cheap rail fare from Nottingham to Beeston when no fares found.
//
// Rev DevFactory Apr 7 sbarker
// Fixing a bug where amend date hits object reference not set when revising date
// for single train journey. Bug number 4839.
//
//   Rev 1.2   Mar 31 2008 13:19:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:00   mturner
//Initial revision.
//
//   Rev 1.13   Apr 24 2006 17:23:28   esevern
//added check for 'open return' selected for journey request - if so don't add 'no return' to the drop down list of return options
//Resolution for 3897: DN062 Amend Tool: 'No return' shown on Amend tool when open return fares displayed in Find A Fare
//
//   Rev 1.12   Apr 12 2006 17:18:00   kjosling
//'Open Return' on the Amend Toolbar is default if TicketType is 'Open Return'
//Resolution for 3897: DN062 Amend Tool: 'No return' shown on Amend tool when open return fares displayed in Find A Fare
//
//   Rev 1.11   Mar 24 2006 17:16:16   kjosling
//Automatically merged from branch for stream0023
//
//   Rev 1.10.1.0   Mar 14 2006 12:01:30   kjosling
//Added support for single to return journey conversions
//Resolution for 23: DEL 8.1 Workstream - Journey Results - Phase 1
//
//   Rev 1.10   Feb 23 2006 19:16:18   build
//Automatically merged from branch for stream3129
//
//   Rev 1.9.1.1   Jan 30 2006 14:40:58   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9.1.0   Jan 10 2006 15:23:16   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9   Nov 14 2005 18:31:38   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.8   Nov 03 2005 16:10:32   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.7.1.0   Oct 20 2005 19:32:42   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.7   Apr 11 2005 16:08:32   COwczarek
//Remove redundant code. Cast page state to correct class.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.6   Apr 09 2005 15:54:44   tmollart
//Removed code to set up controls from page load as this was causing controls to be overwritten before event handlers fired.
//Modified code in PreRender so date controls are repopulated from searchParams.
//Removed date population code from SetupControls as it was overwriting the controls from values from searchParams before event handlers could fire to populate searchParams!
//
//   Rev 1.5   Apr 08 2005 11:46:50   tmollart
//Updated SetUpControls method so that date flexibility is also populated.
//
//   Rev 1.4   Mar 30 2005 16:33:34   rhopkins
//Ensure that child controls are always populated.
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.3   Mar 17 2005 14:39:14   rhopkins
//Get ...DateTime from SearchRequest rather than OriginalJourneyRequest
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.2   Mar 14 2005 18:54:38   rhopkins
//Expose the embeded date controls to allow values to be used for new search
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.1   Mar 08 2005 12:16:00   COwczarek
//Only do work if control is visible
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.0   Mar 07 2005 12:12:00   rhopkins
//Initial revision.
//

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.HtmlControls;
	using System.Web.UI.WebControls;

	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.PricingRetail.Domain;

	/// <summary>
	/// Displays date fields for outward and inward (if required) journeys.
	/// Only the date can be specified, with a flexibility of +/- a number of days.
	/// </summary>
	public partial class AmendCostSearchDateControl : TDUserControl, ILanguageHandlerIndependent
	{
		protected DateSelectControl selectControlOutward;
		protected DateSelectControl selectControlInward;

		private FindCostBasedPageState pageState;

		/// <summary>
		/// Constructor
		/// </summary>
		public AmendCostSearchDateControl()
		{
			
		}

		/// <summary>
		/// Page Load method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// PreRender method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			if (this.Visible)
			{
				//If controls not previously set up then set them up now.
    			SetUpControls();

				pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

				//Populate values from outward/return dates from search params.
				selectControlOutward.Current = pageState.SearchRequest.OutwardDateTime;
				selectControlOutward.Flexibility = pageState.SearchRequest.OutwardFlexibilityDays;

				if (pageState.SearchRequest.ReturnDateTime != null)
				{
					selectControlInward.Current = pageState.SearchRequest.ReturnDateTime;
					selectControlInward.Flexibility = pageState.SearchRequest.InwardFlexibilityDays;
				}
			}
		}

		private void SetUpControls()
		{
			ITDSessionManager tdSessionManager = TDSessionManager.Current;

			pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;

			instructionLabel.Text = GetResource("AmendSaveSendCostSearchDate.InstructionLabel");

			// Initialise the OK button
			buttonOK.Text = resourceManager.GetString("AmendSaveSendCostSearchDate.buttonOK.Text", TDCultureInfo.CurrentUICulture);

			// Set up date control for Outward journey
			selectControlOutward.ControlDateType.Text = GetResource("outwardSelect.labelDateType");
			selectControlOutward.FlexibilityControlsVisible = true;
			selectControlOutward.TimeControlsVisible = false;
			selectControlOutward.ControlCalendar.Visible = false;
			selectControlOutward.Populate();

			dateControlInward.Visible = true;
			selectControlInward.ControlDateType.Text = GetResource("returnSelect.labelDateType");
			selectControlInward.FlexibilityControlsVisible = true;
			selectControlInward.TimeControlsVisible = false;
			selectControlInward.ControlCalendar.Visible = false;
			selectControlInward.IsReturnControl = true;
			selectControlInward.Populate();

			if(pageState.SearchRequest.ReturnDateTime == null)
			{
				AddDefaultReturnOptions();
			}
		}

		/// <summary>
		/// Adds default return journey options to the control 
		/// </summary>
		private void AddDefaultReturnOptions()
		{
			if(dateControlInward.Visible == false || selectControlInward.ControlMonths.Items.Count > 3) return;

			ListItem item = new ListItem("-",string.Empty);
			selectControlInward.ControlHours.Items.Insert(0, item);
			
			selectControlInward.ControlDays.Items.Add(item);
			selectControlInward.ControlDays.SelectedIndex = selectControlInward.ControlDays.Items.Count - 1;		
			
			selectControlInward.ControlMinutes.Items.Insert(0, item);

			item = new ListItem(GetResource("DataServices.ReturnMonthYearDrop.OpenReturn"), "OpenReturn");
			selectControlInward.ControlMonths.Items.Add(item);

			//Add 'open return' option
			pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

			// if its an open return, set this as the default selected value and don't
			// add the 'no return' option
            //Steve Barker: Note that the first part of this if condition is 
            //needed to avoid an object reference error if SelectedTravelDate is null:
			if(pageState.SelectedTravelDate != null && pageState.SelectedTravelDate.TravelDate.TicketType == TicketType.OpenReturn)
			{
				selectControlInward.ControlMonths.SelectedIndex = selectControlInward.ControlMonths.Items.Count - 1;
			}
			else 
			{
				// add 'no return' option
				item = new ListItem(GetResource("DataServices.ReturnMonthYearDrop.NoReturn"), "NoReturn");
				selectControlInward.ControlMonths.Items.Insert(0, item);				
			}
		}

		/// <summary>
		/// Read only property returning the nested DateSelectControl for the outward journey
		/// </summary>
		public DateSelectControl SelectControlOutward
		{
			get { return this.selectControlOutward; }
		}

		/// <summary>
		/// Read only property returning the nested DateSelectControl for the outward journey
		/// </summary>
		public DateSelectControl SelectControlInward
		{
			get { return this.selectControlInward; }
		}

		/// <summary>
		/// Read only property returning the nested OKButton
		/// </summary>
		public TDButton OkButton
		{
			get{ return this.buttonOK;}
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
