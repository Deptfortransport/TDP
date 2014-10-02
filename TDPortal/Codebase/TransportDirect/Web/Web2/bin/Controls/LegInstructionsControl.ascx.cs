// *********************************************** 
// NAME                 : LegInstructionsControl.ascx.cs
// AUTHOR               : Paul Cross
// DATE CREATED         : 18/07/2005
// DESCRIPTION			: Displays the instructions and operator links for each service
//						  within a leg of a journey.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/LegInstructionsControl.ascx.cs-arc  $
//
//   Rev 1.27   Jan 29 2013 14:12:56   DLane
//Update to interchange presentation
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.26   Dec 10 2012 12:11:08   mmodi
//Display service detail cjp info
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.25   Dec 05 2012 13:44:06   mmodi
//Display walk interchange text, and improve layout for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.24   Aug 10 2011 15:15:04   mmodi
//Updated to not display the service number when the leg mode is rail
//Resolution for 5723: Service detail number shown for Rail service
//
//   Rev 1.23   Apr 07 2010 13:31:54   pghumra
//Various updates for accessibility purposes
//Resolution for 5491: RS69801 CCN0551 Accessibility - Spaces TfL Map Note London Bus
//
//   Rev 1.22   Feb 26 2010 15:53:40   mmodi
//Corrected setting of Air operator link for international journeys
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.21   Feb 26 2010 13:24:20   mmodi
//Show international Accessability link in new window
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.20   Feb 26 2010 11:43:40   mmodi
//Only prefix "Central" to an international city location if it doesnt already have it
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.19   Feb 25 2010 15:56:04   apatel
//Updated for not to show printer friendly links, to show international city links correctly when transfer is at the end of the journey and correct to GB door to door link when there is invalid naptan for GB location
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.18   Feb 24 2010 12:11:12   apatel
//Updated to display Modal popup correctly and removed the javascript error on printer friendly page
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.17   Feb 23 2010 12:26:36   apatel
//Changes made to make ModalPopupMessage control work correctly
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.16   Feb 22 2010 07:25:12   apatel
//updated message popup control buttons text
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.15   Feb 21 2010 23:22:54   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.14   Feb 18 2010 15:54:46   mmodi
//Updated Plan door to door journey link for International journeys
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.13   Feb 17 2010 15:13:38   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.12   Feb 16 2010 11:16:02   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.11   Feb 12 2010 11:51:46   apatel
//updated to set international service detail
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.10   Feb 12 2010 11:13:58   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Apr 28 2009 13:10:56   mmodi
//Updated for display of accessibility link on printer friendly page
//Resolution for 5282: Printer Friendly Accessibilty link active in door to door
//
//   Rev 1.8   Jul 24 2008 13:45:46   apatel
//External links added text "(opens new window)"
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.7   Jul 11 2008 14:09:48   apatel
//updated to html decode accessibility link description
//Resolution for 5057: Zonal Accessibility links showing the same text in diagram
//
//   Rev 1.6   Jul 11 2008 13:31:26   apatel
//updated for CCN 464 to customize the url description
//Resolution for 5057: Zonal Accessibility links showing the same text in diagram
//
//   Rev 1.5   Jul 08 2008 09:25:44   apatel
//Accessibility link CCN 458 updates
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.4   Jul 03 2008 13:26:50   apatel
//change the namespage zonalaccessibility to zonalservices
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.3   Jun 27 2008 09:41:12   apatel
//CCN - 458 Accessibility Updates - Improved linking
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.2   Mar 31 2008 13:21:44   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:56   mturner
//Initial revision.
//
//   Rev 1.10   Oct 06 2006 15:36:38   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.9.1.1   Sep 27 2006 11:03:12   esevern
//Check for setting of opening times label now done in parent control (JourneyDetailsTableGridControl)
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4164: Car Parking: Car park note is not displayed in Details view
//
//   Rev 1.9.1.0   Sep 26 2006 11:56:12   esevern
//Added opening times note for car parking
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4164: Car Parking: Car park note is not displayed in Details view
//
//   Rev 1.9   Mar 20 2006 14:47:02   NMoorhouse
//Updated following review comments
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.8   Mar 13 2006 16:58:06   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6.2.0   Jan 26 2006 20:43:58   rhopkins
//Changed to use JourneyLeg instead of PublicJourneyDetails.  This allows proper mode-agnostic handling.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Feb 23 2006 19:16:54   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.0   Jan 10 2006 15:25:58   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Jul 26 2005 18:17:48   pcross
//Minor corrections from unit test run
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.5   Jul 25 2005 21:02:14   pcross
//FxCop updates
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.4   Jul 21 2005 18:37:38   pcross
//Added printer friendliness
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3   Jul 21 2005 10:13:58   RPhilpott
//Allow for null destination.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.2   Jul 20 2005 18:11:12   pcross
//Now handling road journeys too (for journey extensions)
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 19 2005 18:44:24   pcross
//Working version
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 19 2005 10:53:36   pcross
//Initial revision.
//

namespace TransportDirect.UserPortal.Web.Controls
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    using TD.ThemeInfrastructure;
    using TransportDirect.Common;
    using TransportDirect.Common.ResourceManager;
    using TransportDirect.Common.ServiceDiscovery;
    using TransportDirect.JourneyPlanning.CJPInterface;
    using TransportDirect.UserPortal.AirDataProvider;
    using TransportDirect.UserPortal.DataServices;
    using TransportDirect.UserPortal.ExternalLinkService;
    using TransportDirect.UserPortal.InternationalPlannerControl;
    using TransportDirect.UserPortal.JourneyControl;
    using TransportDirect.UserPortal.LocationService;
    using TransportDirect.UserPortal.Resource;
    using TransportDirect.UserPortal.SessionManager;
    using TransportDirect.UserPortal.Web.Adapters;
    using TransportDirect.UserPortal.Web.Controls;
    using TransportDirect.UserPortal.ZonalServices;

    using Logger = System.Diagnostics.Trace;
    using TransportDirect.Common.PropertyService.Properties;
    
	[System.Runtime.InteropServices.ComVisible(false)]

	/// <summary>
	///		Displays the instructions and operator links for each service within a leg of a journey.
	/// </summary>
	public partial class LegInstructionsControl : TDPrintableUserControl
    {
        #region Private members

        private JourneyLeg journeyLeg;
		private PublicJourneyDetail publicJourneyDetail;
		private RoadJourney roadJourney;
        private FindAMode findAMode = FindAMode.None;

		private bool listRoadJourneyRoads;
		private bool showingInGrid;
        private bool useNameList;
        private bool useWalkInterchange;

		// Resource strings for services text (may be used several times per leg)
		private string upperTakeText;
		private string towardsText;
		private string orText;
		private string lowerTakeText;
        private string everyText;

        // If set, can be used within the instruction label where required
        private string durationMinsText;

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
		{

			// Make sure control has the information it needs to function
			if (roadJourney != null)
			{
				listRoadJourneyRoads = true;
				instructionRepeater.Visible = false;
				SetNoServicesLabel();
			}
			else if (journeyLeg == null)
			{
				this.Visible = false;
			}
			else
			{
				// Hide the unused control (if there are services then the repeater is used, else show label)
				// Set the datasource for the repeater if it will be used.
				switch (journeyLeg.Mode)
				{
					case ModeType.Cycle:
					case ModeType.Car:
					case ModeType.Walk:
						instructionRepeater.Visible = false;
						SetNoServicesLabel();
						break;
					default:
						noServicesInstructionLabel.Visible = false;
						publicJourneyDetail = (journeyLeg as PublicJourneyDetail);
						if (publicJourneyDetail != null)
						{
							SetServiceResourceStrings();
							instructionRepeater.DataSource = publicJourneyDetail.Services;
							instructionRepeater.DataBind();
						}
						else
						{
							instructionRepeater.Visible = false;
						}
						break;
				}
			}
		}

		#region Private Methods
		
		/// <summary>
		/// Checks the journey leg parameter with the initial journey leg, 
		/// returning true if the current journey leg is the start and
		/// the start location is a car park
		/// </summary>
		/// <param name="detail">JourneyLeg</param>
		/// <returns>bool</returns>
		private bool CurrentIsStartCarPark()
		{
			return journeyLeg.LegStart.Location.CarParking != null;
		}

		/// <summary>
		/// Checks the journey leg parameter with the final journey leg, 
		/// returning true if the current journey leg is the end and
		/// the end location is a car park
		/// </summary>
		/// <param name="detail">JourneyLeg</param>
		/// <returns>bool</returns>
		private bool CurrentIsEndCarPark()
		{
			return journeyLeg.LegEnd.Location.CarParking != null;
		}

		/// <summary>
		/// Wiring events to handlers
		/// </summary>
		private void AddEventHandlers()
		{
			instructionRepeater.ItemCreated += new RepeaterItemEventHandler(instructionRepeater_ItemCreated);
		}

		/// <summary>
		/// These resource strings are potentially used several times per leg.
		/// </summary>
		private void SetServiceResourceStrings()
		{
			// Get resource strings
			upperTakeText = GetResource("JourneyDetailsControl.labelUpperTakeString");
			towardsText = GetResource("JourneyDetailsControl.labelTowardsString");
			orText = GetResource("JourneyDetailsControl.labelOrString");
			lowerTakeText = GetResource("JourneyDetailsControl.labelLowerTakeString");
            everyText = GetResource("JourneyDetailsControl.every");
		}

		/// <summary>
		/// Sets the output for a leg that has no service information.
		/// </summary>
		private void SetNoServicesLabel()
		{
			string description = String.Empty;

			// See which mode it is and set description accordingly

			if (listRoadJourneyRoads)
			{
				// Output road journey description in the format of a list of roads that are en route (used in table control)
				description = GetRoadList();
			}
			else
			{
				// Currently do not have use cycle
				if( journeyLeg.Mode == ModeType.Cycle)
					description = String.Empty;

				// Check to see if walk leg
				if( journeyLeg.Mode == ModeType.Walk )
				{
                    if (useWalkInterchange)
                    {
                        description = string.Format(GetResource("JourneyDetailsTableControl.WalkInterchangeTo"),
                            journeyLeg.LegEnd.Location.Description);
                    }
                    else
                    {
                        // Get resource strings
                        string walkToText = GetResource("JourneyDetailsTableControl.WalkTo");

                        // Get the "Walk to" string from resourcing manager.
                        description += walkToText;

                        // Check if this location is going to a car park, and display the car park name if needed
                        if (CurrentIsEndCarPark() && journeyLeg.LegEnd.Location.SearchType == TransportDirect.UserPortal.LocationService.SearchType.CarPark)
                            description += " " + FindCarParkHelper.GetCarParkName(journeyLeg.LegEnd.Location.CarParking);
                        else
                            description += " " + journeyLeg.LegEnd.Location.Description;

                        // check whether the car parks opening times note should be visible
                        if (!ShowingInGrid)
                        {
                            HideOpenTimeLabel();
                        }
                    }
				}
			}

			// Now output the description to the noServicesInstructionLabel control
			noServicesInstructionLabel.Text = description;

		}

		/// <summary>
		/// Compiles a list of roads that are en route for a road journey
		/// </summary>
		/// <returns></returns>
		private string GetRoadList()
		{
			string description = "&nbsp;";	// Default vaue in case there are no roads listed

			// Get all the roads for the route
			ArrayList roads = new ArrayList();

			foreach (RoadJourneyDetail detail in roadJourney.Details)
			{
				string road = detail.RoadNumber;

				// Add the road only if is not empty and it hasn't already been added.
				if (road != null && road.Length != 0 && !roads.Contains(road))
				{
					roads.Add(road);
				}
			}

			if (roads.Count > 0)
			{
				// Construct the roads list
				StringBuilder buildDescription = new StringBuilder();

				int i;
				for (i = 0; i < roads.Count - 1; i++)
				{
					buildDescription.Append(roads[i] + ", ");
				}

				buildDescription.Append(roads[i]);

				// Get the sentence start
				upperTakeText = GetResource("JourneyDetailsControl.labelUpperTakeString");
				
				// Put the sentence together
				description = upperTakeText + ": " + buildDescription.ToString();
			}
			else if (journeyLeg != null)
			{
				description = SimpleDrivingInstructions();
			}

			return description;
		}

		/// <summary>
		/// Searches the instruction repeater for the opening times label,
		/// sets text and visibility.
		/// </summary>
		private void ShowRepeaterLabel()
		{
			Label openingTimesLabel = (Label)instructionRepeater.FindControl("openingTimesLabel");
			openingTimesLabel.Visible = true;
			openingTimesLabel.Text = GetResource("CarParkInformationControl.informationNote");
		}

		/// <summary>
		/// Searches the instruction repeater for the opening times label
		/// and sets its visibility to false.
		/// </summary>
		private void HideRepeaterLabel()
		{
			((Label)instructionRepeater.FindControl("openingTimesLabel")).Visible = false;
		}

		/// <summary>
		/// Sets the text and displays the no services opening times label
		/// for car parking
		/// </summary>
		private void ShowOpenTimeLabel()
		{
			noServiceOpenTimeLabel.Visible = true;
			noServiceOpenTimeLabel.Text = GetResource("CarParkInformationControl.informationNote");
		}

		/// <summary>
		/// Hides the no service opening times label for car parking
		/// </summary>
		private void HideOpenTimeLabel()
		{
			noServiceOpenTimeLabel.Visible = false;
		}

		/// <summary>
		/// Method to return the Simple Driving Instructions
		/// </summary>
		/// <returns>String containing the summary of where the drive is from and to</returns>
		private string SimpleDrivingInstructions()
		{
			// Get resource strings
			string driveFromText = GetResource("JourneyDetailsControl.DriveFrom");
			string towardsText = GetResource("JourneyDetailsControl.labelTowardsString");

			return driveFromText + " " + journeyLeg.LegStart.Location.Description
				+ " " + towardsText + " " + journeyLeg.LegEnd.Location.Description;
		}

		/// <summary>
		/// Sets a service information output for a leg.
		/// May actually have 0 to many services with information to output so this may be called several times.
		/// </summary>
		private void SetServiceOutput(object sender, RepeaterItemEventArgs e)
		{

			// initialise bit of text used in the last label in repeater row
			string destinationText = string.Empty;

			if (e.Item.DataItem != null) // null check (e.Item.DataItem is a service)
			{

				// Build the desription for the service
				
				// takeLabel
				if (e.Item.ItemIndex == 0)	// the description of the 1st service in a leg starts with a capital letter
					((Label)e.Item.FindControl("takeLabel")).Text = upperTakeText + " ";
				else
					((Label)e.Item.FindControl("takeLabel")).Text = " " + orText + " " + lowerTakeText + " ";	// subsequent service suggestions separated by "or"

				// operator link
				if ((ServiceDetails)e.Item.DataItem != null) // null check
				{
					// Set up the properties of the operator link control for this service and mode
					OperatorLinkControl operatorLinkControlRef = (OperatorLinkControl)e.Item.FindControl("operatorLinkControl");
                    Repeater accessibilityLinkRepeater = e.Item.FindControl("AccessibilityLinkRepeater") as Repeater;

                    ZonalAccessibilityCatalogue refData = (ZonalAccessibilityCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.ZonalAccessibility];

                    accessibilityLinkRepeater.ItemDataBound += new RepeaterItemEventHandler(AccessibilityLinkRepeater_ItemDataBound);


                    ExternalLinkDetail[] accessibilityLinkDetails = refData.GetRegionOperatorModeLinks(publicJourneyDetail.Region, ((ServiceDetails)e.Item.DataItem).OperatorCode, publicJourneyDetail.Mode.ToString());

                    accessibilityLinkRepeater.DataSource = accessibilityLinkDetails;
                    accessibilityLinkRepeater.DataBind();

					operatorLinkControlRef.OperatorCode = ((ServiceDetails)e.Item.DataItem).OperatorCode;
					operatorLinkControlRef.OperatorName = ((ServiceDetails)e.Item.DataItem).OperatorName;
                    operatorLinkControlRef.Region = publicJourneyDetail.Region;
					operatorLinkControlRef.TravelMode = publicJourneyDetail.Mode;
					operatorLinkControlRef.PrinterFriendly = this.PrinterFriendly;

					if(((ServiceDetails)e.Item.DataItem).ServiceNumber != null
						&& ((ServiceDetails)e.Item.DataItem).ServiceNumber.Length != 0)
					{
                        if (journeyLeg.Mode != ModeType.Rail)
                        {
                            operatorLinkControlRef.ExtraUrlText = "/" + ((ServiceDetails)e.Item.DataItem).ServiceNumber;
                        }
					}

					// destination text
					if(((ServiceDetails)e.Item.DataItem).DestinationBoard != null 
						&& ((ServiceDetails)e.Item.DataItem).DestinationBoard.Length != 0)
					{
						destinationText += " " + towardsText + " " +
							((ServiceDetails)e.Item.DataItem).DestinationBoard;
					}
					else if (publicJourneyDetail.Destination != null  
						&& publicJourneyDetail.Destination.Location != null  
						&& publicJourneyDetail.Destination.Location.Description != null
						&& publicJourneyDetail.Destination.Location.Description.Length != 0)
					{
						destinationText += " " + towardsText + " " +
							publicJourneyDetail.Destination.Location.Description;

                    }

                    #region CJP User Info

                    CJPUserInfoControl cjpUserInfoService = (CJPUserInfoControl)e.Item.FindControl("cjpUserInfoService");

                    CJPUserInfoHelper journeyInfoHelper = new CJPUserInfoHelper(((ServiceDetails)e.Item.DataItem));
                    cjpUserInfoService.Initialise(journeyInfoHelper);

                    #endregion
                }
				else
				{
					// Just set the travel mode - can do this in the headerInstructionLabel so we can
					// hide the operator link control
					e.Item.FindControl("operatorLinkControl").Visible = false;

					// destination text
					destinationText += " " + publicJourneyDetail.Mode.ToString();

					if( publicJourneyDetail.Destination.Location != null  
						&& publicJourneyDetail.Destination.Location.Description != null
						&& publicJourneyDetail.Destination.Location.Description.Length != 0)
					{
						destinationText += " " + towardsText + " " +
							publicJourneyDetail.Destination.Location.Description;

					}	
				}

                // Add frequency text for last service only where detail is a frequency detail
                if ((publicJourneyDetail != null) && (e.Item.ItemIndex == publicJourneyDetail.Services.Length - 1))
                {
                    string frequencyText = GetFrequencyText(publicJourneyDetail);
                    if (!string.IsNullOrEmpty(frequencyText))
                        destinationText = string.Format("{0}<br /><br />{1}", destinationText, frequencyText);
                }

                ((Label)e.Item.FindControl("itemInstructionLabel")).Text = destinationText;

				if(!ShowingInGrid)
				{
					HideOpenTimeLabel();
				}
			}
		}

        /// <summary>
        /// AccessibilityLinkRepeater item data bound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccessibilityLinkRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.DataItem != null) // null check (e.Item.DataItem is a service)
            {
                HyperLink accessibilityLinkRef = e.Item.FindControl("accessibilityLink") as HyperLink;
                Label accessibilityLabelRef = e.Item.FindControl("accessibilityLabel") as Label;

                ExternalLinkDetail accessibilityLinkDetail = e.Item.DataItem as ExternalLinkDetail;

                if (accessibilityLinkRef != null)
                {
                    if (!string.IsNullOrEmpty(accessibilityLinkDetail.Url))
                    {
                        // Check if link starts with http or https
                        string linkPrefix = Properties.Current["LegInstructionsControl.LondonBusAccessibilityLinkPrefix"];
                        string linkPrefixSecure = (!linkPrefix.Contains("https")) ? linkPrefix.Replace("http", "https") : linkPrefix;

                        // Don't display link on printer friendly version
                        if ((!this.PrinterFriendly) 
                            && (accessibilityLinkDetail.Url.StartsWith(linkPrefix)
                                || accessibilityLinkDetail.Url.StartsWith(linkPrefixSecure))
                            )
                        {
                            accessibilityLinkRef.Text = string.Format("{0} {1}", Server.HtmlDecode(string.IsNullOrEmpty(accessibilityLinkDetail.LinkText) ? GetResource("JourneyDetails.accessibilityLink.Text") : accessibilityLinkDetail.LinkText), GetResource("ExternalLinks.OpensNewWindowText"));
                            accessibilityLinkRef.Target = "_blank";
                            accessibilityLinkRef.NavigateUrl = accessibilityLinkDetail.Url;
                            accessibilityLinkRef.Visible = true;
                        }
                        else
                        {
                            accessibilityLabelRef.Text = Server.HtmlDecode(string.IsNullOrEmpty(accessibilityLinkDetail.LinkText) ? GetResource("JourneyDetails.accessibilityLink.Text") : accessibilityLinkDetail.LinkText);
                            accessibilityLabelRef.Visible = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets a service information output for an international journey leg.
        /// May actually have 0 to many services with information to output so this may be called several times.
        /// </summary>
        private void SetInternationalServiceOutput(object sender, RepeaterItemEventArgs e)
        {
            OperatorLinkControl operatorLinkControlRef =
                        (OperatorLinkControl)e.Item.FindControl("operatorLinkControl");

            // initialise bit of text used in the last label in repeater row
            string destinationText = string.Empty;

            if (e.Item.DataItem != null) // null check (e.Item.DataItem is a service)
            {
                #region initialiae operatorLinkControl
                string operatorCode = (((ServiceDetails)e.Item.DataItem).OperatorCode).Trim();

                operatorLinkControlRef.OperatorCode = operatorCode;
                operatorLinkControlRef.OperatorName = ((ServiceDetails)e.Item.DataItem).OperatorName;
                operatorLinkControlRef.Region = publicJourneyDetail.Region;
                operatorLinkControlRef.TravelMode = publicJourneyDetail.Mode;
                operatorLinkControlRef.PrinterFriendly = this.PrinterFriendly;
                
                #endregion

                if (journeyLeg.Mode == ModeType.Transfer)
                {
                    operatorLinkControlRef.Visible = false;

                    #region Set up journey link to Door to door (for international public journeys)

                    HyperLink planJourneyLink = e.Item.FindControl("planJourneyLink") as HyperLink;
                    HyperlinkPostbackControl planJourneyLinkControl = e.Item.FindControl("planJourneyLinkControl") as HyperlinkPostbackControl;
                    Label takeLabel = ((Label)e.Item.FindControl("takeLabel"));
                    string legTransferUrlText = GetResource("JourneyDetailControl.planJourneyLink.Text");

                    if (!string.IsNullOrEmpty(legTransferUrlText)) // Check for resource
                    {
                        string locationDescription = journeyLeg.LegEnd.Location.Description;

                        // Add on the "Central " prefix if the location is a city and hasn't already got the prefix
                        if ((!string.IsNullOrEmpty(journeyLeg.LegEnd.Location.CityId))
                            && (!locationDescription.StartsWith(GetResource("JourneyDetailControl.location.city.prefix"))))
                        {
                            locationDescription = GetResource("JourneyDetailControl.location.city.prefix") + locationDescription;
                        }

                        string legTransferText = string.Format(legTransferUrlText, locationDescription);

                        planJourneyLink.Text = legTransferText;
                        takeLabel.Text = legTransferText;

                        // Transfer is in GB so show journey planner landing link
                        if (journeyLeg.LegStart.Location.Country.CountryCode == "GB"
                                && journeyLeg.LegEnd.Location.Country.CountryCode == "GB")
                        {
                            planJourneyLinkControl.Text = legTransferText;
                            planJourneyLinkControl.link_Clicked += new EventHandler(planJourneyLinkControl_Clicked);
                            planJourneyLinkControl.Visible = false;
                            planJourneyLinkControl.PrinterFriendly = this.PrinterFriendly;
                            
                            // Determine if the plan journey link is To a station or From a station
                            
                            bool validNaptan = false;

                            // For an international journey, one side of the leg will have a location with a City id - don't show the link for that location.
                            // The other side location must be in the UK and therefore must have a Naptan.
                            // If the Naptan is not valid or location is in UK but not got Naptan for some reason,
                            // Check for valid OSGR and show the link 
                            if (string.IsNullOrEmpty(journeyLeg.LegEnd.Location.CityId))
                            {
                                validNaptan = false; // Make sure validNaptan is false at start
   
                                if (journeyLeg.LegEnd.Location.NaPTANs.Length > 0)
                                {
                                    validNaptan = IsValidNaptan(journeyLeg.LegEnd.Location.NaPTANs[0].Naptan, journeyLeg.LegEnd.Location.Description);
                                }

                                // Check if its a valid naptan before showing the link
                                if (validNaptan)
                                {
                                    // Link should plan a transfer To the location stop
                                    planJourneyLinkControl.CommandArgument = "LegEnd";
                                    planJourneyLinkControl.Visible = !this.PrinterFriendly;
                                }
                                // Check for valid OSGR to see if link should be still visible even if there is no Naptan or Naptan is not valid
                                else if (IsValidOSGR(journeyLeg.LegEnd.Location.GridReference))
                                {
                                    // Link should plan a transfer To the location stop with location replacetype set as coordinates
                                    planJourneyLinkControl.CommandArgument = "LegEndOSGR";
                                    planJourneyLinkControl.Visible = !this.PrinterFriendly;
                                }

                                
                            }
                            else if (string.IsNullOrEmpty(journeyLeg.LegStart.Location.CityId))
                            {
                                validNaptan = false; // Make sure validNaptan is false at start

                                if (journeyLeg.LegEnd.Location.NaPTANs.Length > 0)
                                {
                                    validNaptan = IsValidNaptan(journeyLeg.LegStart.Location.NaPTANs[0].Naptan, journeyLeg.LegStart.Location.Description);
                                }

                                // Check if its a valid naptan before showing the link
                                if (validNaptan)
                                {
                                    // Link should plan a transfer From the stop
                                    planJourneyLinkControl.CommandArgument = "LegStart";
                                    planJourneyLinkControl.Visible = !this.PrinterFriendly;
                                }
                                // Check for valid OSGR to see if link should be still visible even if there is no Naptan or Naptan is not valid
                                else if (IsValidOSGR(journeyLeg.LegStart.Location.GridReference))
                                {
                                    // Link should plan a transfer To the location stop with location replacetype set as coordinates
                                    planJourneyLinkControl.CommandArgument = "LegStartOSGR";
                                    planJourneyLinkControl.Visible = !this.PrinterFriendly;
                                }
                            }

                            if (planJourneyLinkControl.Visible)
                            {
                                ModalPopupMessage modalPopup = e.Item.FindControl("modalPopup") as ModalPopupMessage;
                                modalPopup.TargetControl = planJourneyLinkControl.LinkHyperLink;
                                modalPopup.OkButtonText = GetResource("ModalPopupMessage.OkButton.Text");
                                modalPopup.CancelButtonText = GetResource("ModalPopupMessage.CancelButton.Text");
                                modalPopup.PopupMessage = GetResource("JourneyDetailControl.DoortoDoorLink.Javascript.Warning");
                                modalPopup.PrinterFriendly = this.PrinterFriendly;
                                modalPopup.Visible = !this.PrinterFriendly;
                            }
                            takeLabel.Visible = false;

                        }
                        else // Transfer is not in GB
                        {
                            planJourneyLink.Text = string.Format("{0} {1}", legTransferText, GetResource("ExternalLinks.OpensNewWindowImage"));
                            takeLabel.Text = planJourneyLink.Text;
                            planJourneyLink.Target = "_blank";
                            
                            // if we got city id for leg start but not leg end transfer is at the start of the journey
                            // so use the city information url from start leg
                            if (!string.IsNullOrEmpty(journeyLeg.LegStart.Location.CityId) && string.IsNullOrEmpty(journeyLeg.LegEnd.Location.CityId))
                            {
                                if (!string.IsNullOrEmpty(journeyLeg.LegStart.CityInformationURL))
                                {
                                    planJourneyLink.NavigateUrl = journeyLeg.LegStart.CityInformationURL;
                                }
                            }
                            // if we got city id for leg end but not leg start transfer is at the end of the journey
                            // so use the city information url from end leg
                            else if (!string.IsNullOrEmpty(journeyLeg.LegEnd.Location.CityId) && string.IsNullOrEmpty(journeyLeg.LegStart.Location.CityId))
                            {
                                if (!string.IsNullOrEmpty(journeyLeg.LegEnd.CityInformationURL))
                                {
                                    planJourneyLink.NavigateUrl = journeyLeg.LegEnd.CityInformationURL;
                                }
                            }
                            

                            planJourneyLink.Visible = !string.IsNullOrEmpty(planJourneyLink.NavigateUrl) && !PrinterFriendly;
                            takeLabel.Visible = false; // PrinterFriendly && !string.IsNullOrEmpty(planJourneyLink.NavigateUrl);
                        }
                    }

                    #endregion
                }
                else
                {
                    #region Instruction "Take Eurostar/9108 towards London"

                    #region Take label "Take", "or take"

                    // Build the desription for the service
                    // takeLabel
                    if (e.Item.ItemIndex == 0)	// the description of the 1st service in a leg starts with a capital letter
                        ((Label)e.Item.FindControl("takeLabel")).Text = upperTakeText + " ";
                    else
                        ((Label)e.Item.FindControl("takeLabel")).Text = " " + orText + " " + lowerTakeText + " ";	// subsequent service suggestions separated by "or"

                    #endregion

                    // operator link
                    if ((ServiceDetails)e.Item.DataItem != null) // null check
                    {
                        #region Operator link and label "Eurostar/9108"

                        AirDataProvider airData = (AirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
                        
                        if (string.IsNullOrEmpty(((ServiceDetails)e.Item.DataItem).OperatorName))
                        {
                            if(publicJourneyDetail.Mode == ModeType.Air)
                            {
                                AirOperator airOperator = airData.GetOperator(operatorCode);
                                if (airOperator != null && !string.IsNullOrEmpty(airOperator.Name))
                                {
                                    operatorLinkControlRef.OperatorName = airOperator.Name;
                                }
                            }
                        }

                        if (((ServiceDetails)e.Item.DataItem).ServiceNumber != null
                            && ((ServiceDetails)e.Item.DataItem).ServiceNumber.Length != 0)
                        {
                            operatorLinkControlRef.ExtraUrlText = "/" + ((ServiceDetails)e.Item.DataItem).ServiceNumber.Trim();
                        }

                        #endregion

                        #region Instruction label "towards London"

                        // destination text
                        if (((ServiceDetails)e.Item.DataItem).DestinationBoard != null
                            && ((ServiceDetails)e.Item.DataItem).DestinationBoard.Length != 0)
                        {
                            destinationText += " " + towardsText + " " +
                                ((ServiceDetails)e.Item.DataItem).DestinationBoard;
                        }
                        else if (publicJourneyDetail.Destination != null
                            && publicJourneyDetail.Destination.Location != null
                            && publicJourneyDetail.Destination.Location.Description != null
                            && publicJourneyDetail.Destination.Location.Description.Length != 0)
                        {
                            destinationText += " " + towardsText + " " +
                                publicJourneyDetail.Destination.Location.Description;

                        }

                        ((Label)e.Item.FindControl("itemInstructionLabel")).Text = destinationText;

                        #endregion

                        #region Accssability link
                        
                        if (!this.PrinterFriendly
                            && !string.IsNullOrEmpty(publicJourneyDetail.LegStart.AccessibilityURL) 
                            && !string.IsNullOrEmpty(publicJourneyDetail.LegStart.AccessibilityInfo))
                        {
                            Repeater accessibilityLinkRepeater = e.Item.FindControl("AccessibilityLinkRepeater") as Repeater;
                            accessibilityLinkRepeater.ItemDataBound += new RepeaterItemEventHandler(AccessibilityLinkRepeater_ItemDataBound);

                            ExternalLinkDetail[] accessibilityLinkDetails = new ExternalLinkDetail[1];

                            accessibilityLinkDetails[0] = new ExternalLinkDetail(
                                publicJourneyDetail.LegStart.AccessibilityURL,
                                true,
                                TDDateTime.Now,
                                TDDateTime.Now.AddDays(1),
                                publicJourneyDetail.LegStart.AccessibilityInfo);

                            accessibilityLinkRepeater.DataSource = accessibilityLinkDetails;
                            accessibilityLinkRepeater.DataBind();
                        }
                       
                        #endregion
                    }
                    else
                    {
                        #region No service details, "coach towards London"

                        // Just set the travel mode - can do this in the headerInstructionLabel so we can
                        // hide the operator link control
                        e.Item.FindControl("operatorLinkControl").Visible = false;

                        // destination text
                        destinationText += " " + publicJourneyDetail.Mode.ToString();

                        if (publicJourneyDetail.Destination.Location != null
                            && publicJourneyDetail.Destination.Location.Description != null
                            && publicJourneyDetail.Destination.Location.Description.Length != 0)
                        {
                            destinationText += " " + towardsText + " " +
                                publicJourneyDetail.Destination.Location.Description;

                        }

                        ((Label)e.Item.FindControl("itemInstructionLabel")).Text = destinationText;

                        #endregion
                    }

                    #endregion
                }

                if (!ShowingInGrid)
                {
                    HideOpenTimeLabel();
                }
            }
        }

        /// <summary>
        /// Populate locality data into relevant object hierarchy
        /// </summary>
        /// <param name="osgr"></param>
        /// <returns></returns>
        private string PopulateLocality(OSGridReference osgr)
        {
            // GIS Query to get the locality
            IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

            return gisQuery.FindNearestLocality(osgr.Easting, osgr.Northing);
        }

        /// <summary>
        /// Checks the naptan cache to find the naptan, returns true if found
        /// </summary>
        /// <param name="naptan"></param>
        /// <returns></returns>
        private bool IsValidNaptan(string naptan, string description)
        {
            // Lookup naptan in cache and/or GIS query ...
            NaptanCacheEntry naptanCacheEntry = NaptanLookup.Get(naptan, description);

            return naptanCacheEntry.Found;
        }

        /// <summary>
        /// Validates the gridReferece for location
        /// </summary>
        /// <param name="gridReference"></param>
        /// <returns></returns>
        private bool IsValidOSGR(OSGridReference gridReference)
        {
            return gridReference != null && gridReference.IsValid;
        }

        /// <summary>
        /// If the supplied leg is a frequency leg, returns a formatted
        /// string containing frequency details, otherwise an empty
        /// string is returned.
        /// </summary>
        /// <param name="detail">Current item being rendered</param>
        /// <returns>frequency details or empty string</returns>
        private string GetFrequencyText(JourneyLeg detail)
        {
            if (detail is PublicJourneyFrequencyDetail)
            {
                PublicJourneyFrequencyDetail frequencyDetail = (PublicJourneyFrequencyDetail)detail;

                if (frequencyDetail.MinFrequency == frequencyDetail.MaxFrequency)
                {
                    return string.Format(
                        everyText,
                        frequencyDetail.MinFrequency,
                        GetResource("JourneyDetailsTableControl.minutesString.Long"));
                }
                else
                {
                    return string.Format(
                        everyText,
                        string.Format("{0}-{1}", frequencyDetail.MinFrequency, frequencyDetail.MaxFrequency),
                        GetResource("JourneyDetailsTableControl.minutesString.Long"));
                }
            }
            else
            {
                return string.Empty;
            }
        }

		#endregion
        
		#region Event Handlers


		/// <summary>
		/// Event fired on item creation.
		/// Initialise controls contained within repeater for that item.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void instructionRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
		{
            if (findAMode != FindAMode.International)
            {
                // Repeater is bound to services so we must have services to output
                SetServiceOutput(sender, e);
            }
            else
            {
                SetInternationalServiceOutput(sender, e);
            }

		}

        /// <summary>
        /// handles the door to door link click event rendered for Internataion journey detail
        /// Takes user to door to door page with the prefilled information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void planJourneyLinkControl_Clicked(object sender, EventArgs e)
        {
            #region Initialise session journey parameters

            ITDSessionManager sessionManager = TDSessionManager.Current;

            // Reset the journey parameters
            sessionManager.InitialiseJourneyParameters(FindAMode.None);
            
            TDJourneyParametersMulti journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;

            journeyParameters.PublicViaType = new TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType(TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType.Default);
            journeyParameters.PrivateViaType = new TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType(TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType.Default);

            #endregion

            HyperlinkPostbackControl control = (HyperlinkPostbackControl)sender;

            // Set the search and location objects
            LocationSearch search = new LocationSearch();
            TDLocation location = new TDLocation();

            // LegEndOSGR is the state when there is no valid NaPTAN but location got a valid GridReference
            if (control.CommandArgument == "LegEnd" || control.CommandArgument == "LegEndOSGR")
            {
                #region Setup plan To location

                search.InputText = journeyLeg.LegEnd.Location.Description;
                search.SearchType = SearchType.MainStationAirport;
                search.LocationFixed = true;
                
                location.Description = journeyLeg.LegEnd.Location.Description;
                location.GridReference = journeyLeg.LegEnd.Location.NaPTANs[0].GridReference;
                location.PopulateToids();
                location.Locality = PopulateLocality(location.GridReference);
                location.SearchType = SearchType.MainStationAirport;
                location.Status = TDLocationStatus.Valid;
                
                // In case of non-valid NaPTAN set locations request place type to be as coordinates
                if (control.CommandArgument == "LegEndOSGR")
                {
                    location.RequestPlaceType = RequestPlaceType.Coordinate;
                }
                else
                {
                    location.NaPTANs = journeyLeg.LegEnd.Location.NaPTANs;
                }
                                
                // Ensure search and location objects are populated correctly, as needed by the Door to door page
                locationSelect.Populate(DataServiceType.FromToDrop, CurrentLocationType.To, ref search, ref location, false, true, false);

                journeyParameters.Destination = search;
                journeyParameters.DestinationLocation = location;

                // Set the date and time based on the leg
                TDDateTime leaveDateTime = journeyLeg.EndTime;
                
                // Round down the minutes if required
                if (leaveDateTime.Minute % 5 != 0)
                    leaveDateTime = leaveDateTime.Subtract(new TimeSpan(0,(leaveDateTime.Minute % 5), 0));

                journeyParameters.OutwardArriveBefore = true;
                journeyParameters.OutwardHour = leaveDateTime.Hour.ToString(CultureInfo.CurrentCulture.NumberFormat);
                journeyParameters.OutwardMinute = leaveDateTime.Minute.ToString(CultureInfo.CurrentCulture.NumberFormat);
                journeyParameters.OutwardDayOfMonth = leaveDateTime.ToString("dd");
                journeyParameters.OutwardMonthYear = leaveDateTime.ToString("MM") + "/" + leaveDateTime.ToString("yyyy");

                #endregion
            }
            // LegStartOSGR is the state when there is no valid NaPTAN but location got a valid GridReference
            else if (control.CommandArgument == "LegStart" || control.CommandArgument == "LegStartOSGR")
            {
                #region Setup plan From location

                search.InputText = journeyLeg.LegStart.Location.Description;
                search.SearchType = SearchType.MainStationAirport;
                search.LocationFixed = true;

                location.Description = journeyLeg.LegStart.Location.Description;
                location.GridReference = journeyLeg.LegStart.Location.NaPTANs[0].GridReference;
                location.PopulateToids();
                location.Locality = PopulateLocality(location.GridReference);
                location.SearchType = SearchType.MainStationAirport;
                location.Status = TDLocationStatus.Valid;

                // In case of non-valid NaPTAN set locations request place type to be as coordinates
                if (control.CommandArgument == "LegStartOSGR")
                {
                    location.RequestPlaceType = RequestPlaceType.Coordinate;
                }
                else
                {
                    location.NaPTANs = journeyLeg.LegStart.Location.NaPTANs;
                }

                // Ensure search and location objects are populated correctly, as needed by the Door to door page
                locationSelect.Populate(DataServiceType.FromToDrop, CurrentLocationType.To, ref search, ref location, false, true, false);

                journeyParameters.Origin = search;
                journeyParameters.OriginLocation = location;

                // Set the date and time based on the leg
                TDDateTime leaveDateTime = journeyLeg.StartTime;

                // Round Up the minutes if required
                if (leaveDateTime.Minute % 5 != 0)
                    leaveDateTime = leaveDateTime.AddMinutes(-leaveDateTime.Minute % 5 + 5);

                journeyParameters.OutwardArriveBefore = false;
                journeyParameters.OutwardHour = leaveDateTime.Hour.ToString(CultureInfo.CurrentCulture.NumberFormat);
                journeyParameters.OutwardMinute = leaveDateTime.Minute.ToString(CultureInfo.CurrentCulture.NumberFormat);
                journeyParameters.OutwardDayOfMonth = leaveDateTime.ToString("dd");
                journeyParameters.OutwardMonthYear = leaveDateTime.ToString("MM") + "/" + leaveDateTime.ToString("yyyy");

                #endregion
            }

            // If there is a valid results set, reset the parameters and pagestate, and 
            // and invalidate the results.
            if (((TDItineraryManager.Current.JourneyResult != null) && (TDItineraryManager.Current.JourneyResult.IsValid))
                || (TDItineraryManager.Current.FullItinerarySelected))
            {
                TDItineraryManager.Current.ResetItinerary();
                sessionManager.InputPageState.Initialise();
                sessionManager.JourneyResult.IsValid = false;
            }

            // Log an MIS event indicating the user has selected to Plan the transfer in door to door
            InternationalPlannerEvent ipe = new InternationalPlannerEvent(InternationalPlannerType.ExtendInDoorToDoor, sessionManager.Authenticated, sessionManager.Session.SessionID);
            Logger.Write(ipe);

            // Navigate to the journey input page
            sessionManager.SetOneUseKey(SessionKey.InternationalPlannerInput, string.Empty);
            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;
        }

		#endregion

		#region Properties

		public JourneyLeg JourneyLeg
		{
			get {return journeyLeg;}
			set {journeyLeg = value;}
		}

		public RoadJourney RoadJourney
		{
			get {return roadJourney;}
			set {roadJourney = value;}
		}

		/// <summary>
		/// Read/write property. True if this control is being displayed 
		/// as part of the JourneyDetailsTableGrid control
		/// </summary>
		public bool ShowingInGrid
		{
			get { return showingInGrid; }
			set { showingInGrid = value; }
		}

		/// <summary>
		/// Read only property returning the car parking 
		/// no service opening times label
		/// </summary>
		public Label NoServiceOpenTimeLabel
		{
			get { return noServiceOpenTimeLabel; }
		}

		/// <summary>
		/// Read/write property returning true if control should use array list of
		/// unique car park references
		/// </summary>
		public bool UseNameList
		{
			get{ return useNameList; }
			set{ useNameList = value; }
		}

        /// <summary>
        /// Read/Write property if control should use the walk interchange instruction for a Walk leg
        /// </summary>
        public bool UseWalkInterchange
        {
            get { return useWalkInterchange; }
            set { useWalkInterchange = value; }
        }

        /// <summary>
        /// Read/Write property. If set, can be used within the instruction label where required
        /// </summary>
        public string DurationMinsText
        {
            get { return durationMinsText; }
            set { durationMinsText = value; }
        }

        /// <summary>
        /// Read/write property to determind FindAMode of the journey
        /// </summary>
        public FindAMode JournyModeType
        {
            get { return findAMode; }
            set { findAMode = value; }
        }

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();

			AddEventHandlers();

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
