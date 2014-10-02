// *********************************************** 
// NAME                 : JourneysSearchFor.ascx.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 05/09/2003
// DESCRIPTION			: A custom control to display
// details of the journey search for.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneysSearchedForControl.ascx.cs-arc  $
//
//   Rev 1.9   Oct 01 2009 16:25:10   apatel
//Updates for Social Bookmark links
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.8   Sep 24 2009 16:35:02   mmodi
//Display via location for EBC
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.7   Sep 21 2009 14:57:26   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.6   Aug 07 2009 13:49:48   jfrank
//Changes for Content for vias CCN.
//If there is a via location show it in the header for certain planners.
//Resolution for 5306: Content Changes for Vias
//
//   Rev 1.5   Jan 15 2009 13:22:28   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   Oct 13 2008 16:44:18   build
//Automatically merged from branch for stream5014
//
//   Rev 1.3.1.1   Sep 15 2008 11:05:12   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.0   Jun 20 2008 14:32:38   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   May 07 2008 14:50:22   apatel
//made tiltle "Journey Found For" to look in table view when findamode is flight.
//
//   Rev 1.2   Mar 31 2008 13:21:40   mturner
//Drop3 from Dev Factory
//
//  Rev Feb 05 2008 13:12:12 dsawe
//  White labelling screen change on journey summary & details headers
//  Removed the table approach to display the header & replaced with plain text
//
//   Rev 1.0   Nov 08 2007 13:15:50   mturner
//Initial revision.
//
//   Rev 1.24   Oct 06 2006 15:34:16   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.23.1.1   Aug 17 2006 15:05:28   esevern
//corrected setting of title if the origin is a car park
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.23.1.0   Aug 04 2006 10:35:16   esevern
//added check for find car park and set control title accordingly
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.23   Feb 23 2006 19:16:54   build
//Automatically merged from branch for stream3129
//
//   Rev 1.22.1.0   Jan 10 2006 15:25:54   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.22   Mar 17 2005 13:09:48   pcross
//No change.
//
//   Rev 1.21   Mar 08 2005 14:01:26   jmorrissey
//Removed repeated 'using' statement
//
//   Rev 1.20   Mar 08 2005 12:26:58   COwczarek
//Display location names for cost based searches
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.19   Sep 30 2004 13:13:08   jbroome
//Extend Journey additional changes (DD02101d)
//
//   Rev 1.18   Sep 17 2004 12:25:54   jbroome
//Added missing comments.
//
//   Rev 1.17   Sep 17 2004 12:11:28   jbroome
//IR1591 - Extend Journey Usability Changes
//
//   Rev 1.16   Sep 03 2004 14:58:40   RHopkins
//IR1387 Change to display fixed-end location description from Itinerary rather than current Request, so that the text is the same in both "journeys found" panels.  Added date to journey start/end times.
//
//   Rev 1.15   Aug 17 2004 09:14:44   COwczarek
//Remove redundant instance variable
//
//   Rev 1.14   Aug 09 2004 16:15:08   jbroome
//IR 1258 - Ensure consistency of rounded time values.
//
//   Rev 1.13   Jul 12 2004 15:12:02   jbroome
//Actioned Extend Journey code review comments.
//
//   Rev 1.12   Jun 24 2004 16:34:06   esevern
//corrected itinerary display to include depart/arrive times
//
//   Rev 1.11   Jun 07 2004 17:42:34   RHopkins
//Modified behaviour when displaying values for Itinerary
//
//   Rev 1.10   May 25 2004 16:57:54   ESevern
//now obtains origin/destination details from itinerary manager if the itinerary length greater than zero
//
//   Rev 1.9   Oct 20 2003 09:48:46   kcheung
//Tidied up code for FXCop
//
//   Rev 1.8   Oct 17 2003 15:38:22   kcheung
//Fixed to conform to FXCOP rules
//
//   Rev 1.7   Oct 01 2003 16:07:16   kcheung
//Fixed header using literal
//
//   Rev 1.6   Oct 01 2003 15:56:00   kcheung
//Reverted page bind.. 
//
//   Rev 1.5   Oct 01 2003 15:02:42   kcheung
//Added Page.DataBind and removed Repeater.DataBind - this ensures the <% %> tag to get the header is executed.
//
//   Rev 1.4   Sep 29 2003 16:48:50   kcheung
//Update to read from description and not locality
//
//   Rev 1.3   Sep 29 2003 12:23:04   kcheung
//Added comments, removed redundant variables
//
//   Rev 1.2   Sep 23 2003 19:09:04   kcheung
//Integrated with css
//
//   Rev 1.1   Sep 05 2003 14:46:40   kcheung
//Working version
//
//   Rev 1.0   Sep 05 2003 13:50:16   kcheung
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
    using System.Text;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
    using TransportDirect.UserPortal.Web.Adapters;
    using TransportDirect.UserPortal.LocationService;
    using TransportDirect.UserPortal.CyclePlannerControl;    

	/// <summary>
	///	A custom control to display details of the journey search for.
	/// </summary>
	public partial  class JourneysSearchedForControl : TDUserControl
	{
		
		#region Declaration
		




		public const int MINUTES_OR_HOURS = 10;
        private bool isTableView = false;
        private bool useRouteFoundForHeading = false;

		#endregion

		#region Page Load / Pre Render
	
		/// <summary>.
		/// OnPreRender Method
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			if (showControl) 
			{
				// Make sure outer div is visible
				outerDiv.Visible = true;
				setUpControls();
			}
			else
			{
				// Not showing control - hide outer div
				outerDiv.Visible = false;
			}
			base.OnPreRender(e);
		}

		/// <summary>
		/// Page Load Method.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{

		}

		#endregion

        /// <summary>
        /// Overridden ToString method
        /// </summary>
        /// <returns>returns the title</returns>
        public override string ToString()
        {
            setUpControls();
            return literalTitle.Text;
        }

		#region Private properties / methods
        //  White labelling screen change -- Removed the table approach
        /// <summary>
        /// Method which sets visibilities and text of all table cells.
        /// </summary>
        private void setUpControls()
        {
            StringBuilder sbJourneyHeader = new StringBuilder();

           
            if (TDItineraryManager.Current.Length > 0)
            {
                sbJourneyHeader.Append(GetResource("JourneysSearchedForControl.headerExistingJourney"));
                if (!IsTableView)
                {
                    sbJourneyHeader.Append(" ");
                    sbJourneyHeader.Append(TDItineraryManager.Current.OutwardDepartLocation().Description);
                    sbJourneyHeader.Append(" ");
                    sbJourneyHeader.Append(GetResource("JourneysSearchedForControl.Seperator"));
                    sbJourneyHeader.Append(" ");
                    sbJourneyHeader.Append(TDItineraryManager.Current.OutwardArriveLocation().Description);
                }
                literalTitle.Text = sbJourneyHeader.ToString();
                
                // setting data for table view
                cellDepartLocation.Text = TDItineraryManager.Current.OutwardDepartLocation().Description;
                cellTo.Text = GetResource("JourneysSearchedForControl.Seperator");
                cellArriveLocation.Text = TDItineraryManager.Current.OutwardArriveLocation().Description;
                cellDepartTime.Text = AssembleTimeString(TDItineraryManager.Current.OutwardDepartDateTime(), true);
                cellArriveTime.Text = AssembleTimeString(TDItineraryManager.Current.OutwardArriveDateTime(), false);

                if (TDItineraryManager.Current.ReturnLength > 0)
                {
                    cellTitle.Text = GetResource("JourneysSearchedForControl.labelOutward");
                    cellReturnTitle.Text = GetResource("JourneysSearchedForControl.labelReturn");
                    cellReturnDepartLocation.Text = TDItineraryManager.Current.ReturnDepartLocation().Description;
                    cellReturnTo.Text = GetResource("JourneysSearchedForControl.Seperator");
                    cellReturnArriveLocation.Text = TDItineraryManager.Current.ReturnArriveLocation().Description;
                    cellReturnDepartTime.Text = AssembleTimeString(TDItineraryManager.Current.ReturnDepartDateTime(), true);
                    cellReturnArriveTime.Text = AssembleTimeString(TDItineraryManager.Current.ReturnArriveDateTime(), false);
                }
                else
                {
                    // No return journey info
                    cellTitle.Visible = false;
                    cellBlank.Visible = false;
                    rowReturnLocation.Visible = false;
                    rowReturnTime.Visible = false;
                }
            }
            else
            {
                // Hide unnecessary columns and rows
                tableOutward.Visible = IsTableView;
                rowReturnLocation.Visible = false;
                rowReturnTime.Visible = false;
                rowOutwardTime.Visible = false;
                cellTitle.Visible = IsTableView;
                cellBlank.Visible = IsTableView;
                rowOutwardLocation.Visible = IsTableView;               
                cellTo.Text = GetResource("JourneysSearchedForControl.Seperator");
                cellTo.Visible = IsTableView;

                if (FindInputAdapter.IsCostBasedSearchMode(TDSessionManager.Current.FindAMode))
                {
                    // Displaying locations for a cost based search
                    sbJourneyHeader.Append(GetResource("JourneysSearchedForControl.headerFaresSearchedFor"));
                    TDJourneyParameters searchParams = TDSessionManager.Current.JourneyParameters;
                        
                    if (!IsTableView)
                    {
                        sbJourneyHeader.Append(" ");
                        sbJourneyHeader.Append(searchParams.OriginLocation.Description);
                        sbJourneyHeader.Append(" ");
                        sbJourneyHeader.Append(GetResource("JourneysSearchedForControl.Seperator"));
                        sbJourneyHeader.Append(" ");
                        sbJourneyHeader.Append(searchParams.DestinationLocation.Description);
                    }
                    cellDepartLocation.Text = searchParams.OriginLocation.Description;
                    cellArriveLocation.Text = searchParams.DestinationLocation.Description;
                    literalTitle.Text = sbJourneyHeader.ToString();
                }
                else
                {
                    // Displaying locations for a time based search

                    //if its a car park journey, set suitable title
                    if ((TDSessionManager.Current.JourneyRequest.OriginLocation != null)
                        &&
                        (TDSessionManager.Current.JourneyRequest.OriginLocation.CarParking != null))
                    {
                        sbJourneyHeader.Append(GetResource("JourneysSearchedForControl.headerCarParksSearchedFor"));
                    }
                    else
                    {
                        if (useRouteFoundForHeading)
                        {
                            sbJourneyHeader.Append(GetResource("JourneysSearchedForControl.headerRouteFoundFor"));
                        }
                        else
                        {
                            sbJourneyHeader.Append(GetResource("JourneysSearchedForControl.headerJourneysSearchedFor"));
                        }
                    }
                    sbJourneyHeader.Append(" ");

                    #region Get locations
                    TDLocation originLocation;
                    TDLocation destinationLocation;
                    TDLocation viaLocation = null;

                    if ((TDSessionManager.Current.FindAMode == FindAMode.Cycle)
                        &&
                        (TDSessionManager.Current.CycleRequest != null))
                    {
                        ITDCyclePlannerRequest cycleRequest = TDSessionManager.Current.CycleRequest;
                        originLocation = cycleRequest.OriginLocation;
                        destinationLocation = cycleRequest.DestinationLocation;

                        if (cycleRequest.CycleViaLocations.Length == 1 && cycleRequest.CycleViaLocations[0].Status == TDLocationStatus.Valid)
                        {
                            viaLocation = cycleRequest.CycleViaLocations[0];
                        }
                    }
                    else
                    {
                        ITDJourneyRequest journeyRequest = TDSessionManager.Current.JourneyRequest;
                        originLocation = journeyRequest.OriginLocation;
                        destinationLocation = journeyRequest.DestinationLocation;

                        if (TDSessionManager.Current.FindAMode == FindAMode.Train || 
                            TDSessionManager.Current.FindAMode == FindAMode.Coach ||
                            TDSessionManager.Current.FindAMode == FindAMode.Bus)
                        {
                            if (journeyRequest.PublicViaLocations.Length == 1 && journeyRequest.PublicViaLocations[0].Status == TDLocationStatus.Valid)
                            {
                                viaLocation = journeyRequest.PublicViaLocations[0];
                            }
                        }
                        else if (TDSessionManager.Current.FindAMode == FindAMode.Car ||
                                 TDSessionManager.Current.FindAMode == FindAMode.EnvironmentalBenefits)
                        {
                            if (journeyRequest.PrivateViaLocation != null && journeyRequest.PrivateViaLocation.Status == TDLocationStatus.Valid)
                            {
                                viaLocation = journeyRequest.PrivateViaLocation;
                            }
                        }
                            
                   
                    }
                    #endregion

                    cellDepartLocation.Text = originLocation.Description;
                    cellArriveLocation.Text = destinationLocation.Description;
                    if (!IsTableView)
                    {
                        sbJourneyHeader.Append(originLocation.Description);
                        sbJourneyHeader.Append(" ");
                        sbJourneyHeader.Append(GetResource("JourneysSearchedForControl.Seperator"));
                        sbJourneyHeader.Append(" ");
                        sbJourneyHeader.Append(destinationLocation.Description);

                        if (viaLocation != null)
                        {
                            sbJourneyHeader.Append(" ");
                            sbJourneyHeader.Append(GetResource("JourneysSearchedForControl.Via"));
                            sbJourneyHeader.Append(" ");
                            sbJourneyHeader.Append(viaLocation.Description);
                        }
                    }
                    literalTitle.Text = sbJourneyHeader.ToString();
                }
            }
        }

		/// <summary>
		/// Read only property which is used to determine whether the control will be 
		/// displayed on the screen. This is governed by the state of the Itinerary manager.
		/// </summary>
		private bool showControl
		{
			get 
			{
				TDItineraryManager itineraryManager = TDItineraryManager.Current;
				if ((itineraryManager.Length > 0) && (!itineraryManager.ExtendInProgress))
					return false;
				else
					return true;
			}																									
		}

        /// <summary>
        /// Read/Write property which is used to determine the layout of the title.
        /// </summary>
        public bool IsTableView
        {
            get { return isTableView; }
            set { isTableView = value; }
        }

        /// <summary>
        /// Read/Write property which is used to set the control heading to display "Route found for..."
        /// </summary>
        public bool UseRouteFoundForHeading
        {
            get { return useRouteFoundForHeading; }
            set { useRouteFoundForHeading = value; }
        }

		/// <summary>
		/// Formats a specified time in HH:mm with text from
		/// langStrings for display on the control.
		/// </summary>
		/// <param name="time"></param>
		/// <param name="depart"></param>
		/// <returns>formatted time string </returns>
		private string AssembleTimeString(TDDateTime time, bool depart)
		{
			string theTime = string.Empty;

			// Round up if necessary for consistency.
			if(time.Second >= 30)
				time = time.AddMinutes(1);

			// construct the leaving time string
			string formattedHoursMins = time.ToString("HH:mm");
			string formattedDate = time.ToString("dd/MM");

			if(depart)
				theTime += GetResource("JourneySearchedForControl.DepartureTime");
			else
				theTime += GetResource("JourneySearchedForControl.ArrivalTime");

			return string.Format(theTime, formattedHoursMins, formattedDate);
		}

		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// OnInit Method
		/// </summary>
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	
	}
}
