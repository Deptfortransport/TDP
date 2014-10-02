// *********************************************** 
// NAME			: DefaultCarJourneyDetailFormatter.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 21.01.04
// DESCRIPTION	: Provides a default implementation for formatting
// car journey details for output to a web page.
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/DefaultCarJourneyDetailFormatter.cs-arc  $
//
//   Rev 1.12   Sep 01 2011 10:44:38   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.11   Mar 14 2011 15:12:00   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.10   Nov 08 2010 09:35:12   apatel
//Added extra check for toid info
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.9   Oct 28 2010 11:18:08   apatel
//Updated to correct the pvc comment as files assigned to wrong IR 5622 instead of IR 5623
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.8   Oct 26 2010 14:37:36   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.7   May 04 2010 15:05:58   rhopkins
//Added Car Sharing to Car Results page
//Resolution for 5527: Add CarSharing link to FindACar
//
//   Rev 1.6   Sep 29 2009 15:24:30   nrankin
//Accessibility - opens in new window
//Resolution for 5320: Accessibility - Opens in new window
//
//   Rev 1.5   Sep 21 2009 14:57:06   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.4   Oct 20 2008 15:56:42   jfrank
//Updated for XHTML compliance
//Resolution for 5146: WAI AAA copmpliance work (CCN 474)
//
//   Rev 1.3   Oct 13 2008 16:41:26   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Sep 26 2008 13:46:44   jfrank
//Amended to make XHTML transitional
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 12:58:44   mturner
//Drop3 from Dev Factory
//
//  Rev devfactory Feb 14 2008 18:00:00   mmodi
//Set find car park text dependent on outward flag
//
//  Rev devfactory Feb 12 2008 12:30:00   mmodi
//Use property to also determing whether to return the Find car parks link in footer array
//
//   Rev DevFactory   Feb 06 2008 17:00:00   mmodi
//If first/last leg is a car park, then display car park name rather than location description
//
//   Rev 1.0   Nov 08 2007 13:11:14   mturner
//Initial revision.
//
//   Rev 1.42   Oct 17 2007 13:30:54   pscott
//UK:1361875  IR 4515  change instances of Km to km
//
//   Rev 1.41   Dec 07 2006 14:37:36   build
//Automatically merged from branch for stream4240
//
//   Rev 1.40.1.2   Nov 25 2006 16:54:52   dsawe
//changed the fuel cost (journey emission) logo link 
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.40.1.1   Nov 09 2006 15:14:32   ralonso
//Removed redundant line of code that was already in the GetFooterHeadings method
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.40   Nov 08 2006 10:45:30   mmodi
//Amended Car Park detail line
//Resolution for 4212: Car Parking: Printer friendly view of journey details displays car park hyperlink
//
//   Rev 1.39   Oct 30 2006 17:45:42   mmodi
//Added code to create Printerfriendly version of CarPark detail line
//Resolution for 4212: Car Parking: Printer friendly view of journey details displays car park hyperlink
//
//   Rev 1.38   Oct 06 2006 13:47:02   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.37.1.1   Sep 20 2006 16:56:12   esevern
//Corrected setting of car parking description details for first/last line of directions
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4163: Car Parking: Car park is not shown as a link in Details view
//
//   Rev 1.37.1.0   Aug 22 2006 15:52:50   esevern
//added car parking url to first/last line construction
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.37   May 05 2006 10:48:00   asinclair
//Changed to use the correct string for return Park and Ride journeys
//Resolution for 4075: DN058 Park and Ride Phase 2: Return journey instructions begin 'Arrive at [car park]'
//
//   Rev 1.36   May 03 2006 17:57:42   jbroome
//Ensured hyperlinks are not output on Printable page
//Resolution for 4044: DN058 Park and Ride Phase 2: Car park is a hyperlink on printer friendly page
//
//   Rev 1.35   Apr 28 2006 17:31:32   jbroome
//Fix for IR 4044
//Resolution for 4044: DN058 Park and Ride Phase 2: Car park is a hyperlink on printer friendly page
//
//   Rev 1.34   Apr 26 2006 16:50:18   esevern
//corrected car park url for start location return journey leg
//Resolution for 4008: DN058 Park and Ride Phase 2 - Car Park link open in same window
//
//   Rev 1.33   Apr 26 2006 16:02:20   esevern
//Corrected setting of car park url href to show car park site in new browser window. Corrected setting of car park description text, so that it is only done once whether park and ride scheme is present or not.
//Resolution for 4007: DN058 Park and Ride Phase 2 - Car park name is repeated in driving instruction text
//Resolution for 4008: DN058 Park and Ride Phase 2 - Car Park link open in same window
//
//   Rev 1.32   Apr 20 2006 13:31:36   esevern
//added park and ride direction text for first line of return journey
//Resolution for 3839: DN058 Park and Ride Phase 2 - Final instruction in car journey details is not the correct format
//
//   Rev 1.31   Apr 20 2006 10:58:40   esevern
//added setting of park and ride instructional text and use of park and ride scheme url if specified car park has no url of its own.
//Resolution for 3839: DN058 Park and Ride Phase 2 - Final instruction in car journey details is not the correct format
//
//   Rev 1.30   Apr 07 2006 18:27:28   mguney
//"Add "For" when continue at a mini-roundabaout and a placename present " section removed from continue method.
//Resolution for 3849: Car Journey Instructions Update after stream0030
//
//   Rev 1.29   Apr 07 2006 16:57:36   mguney
//Updated AddContinueFor method.
//Resolution for 3849: Car Journey Instructions Update after stream0030
//
//   Rev 1.28   Mar 23 2006 17:41:10   tmollart
//Manual merge of stream 0025.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.27   Mar 14 2006 10:09:54   NMoorhouse
//stream3353 manual merge from branch -> trunk
//
//   Rev 1.26   Feb 23 2006 19:16:04   build
//Automatically merged from branch for stream3129
//
//   Rev 1.25.2.0   Feb 23 2006 18:50:02   NMoorhouse
//Changes for extend, replan & adjust
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.25.1.1   Jan 30 2006 12:15:18   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.25.1.0   Jan 10 2006 15:17:30   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.25   Aug 02 2005 12:49:52   jmorrissey
//Fix for Vantive 3868786
//
//   Rev 1.24   May 12 2005 17:32:58   asinclair
//Added a print bool to the constructor
//
//   Rev 1.23   May 11 2005 17:38:58   tmollart
//Modified formatting for motorway so that it does not output an inline style but instead uses the style sheets.
//Resolution for 2480: Mway names on printer friendly pages should not have blue background
//
//   Rev 1.22   May 11 2005 15:11:22   asinclair
//Add bool from properties to set visability of Map buttons for car journey sections
//
//   Rev 1.21   May 05 2005 10:35:28   asinclair
//Map button not displayed if a Node TOID is now returned by the CJP
//
//   Rev 1.20   May 04 2005 15:12:26   asinclair
//Added Map buttons for StopoverSections
//
//   Rev 1.19   Apr 24 2005 12:24:12   rgeraghty
//Added null check to GetFooterHeadings
//Resolution for 2257: Del 7 - Car Costing - Server error when accessing printable page for extended journey details
//
//   Rev 1.18   Apr 23 2005 19:29:38   asinclair
//Fix for IR 1983
//
//   Rev 1.17   Apr 21 2005 09:44:20   rscott
//Fixes for IR2259 Ferry Problem
//
//   Rev 1.16   Apr 13 2005 14:42:28   asinclair
//Added fix for 2034
//
//   Rev 1.15   Mar 30 2005 17:02:14   asinclair
//Updated to use langstrings
//
//   Rev 1.14   Mar 18 2005 16:26:56   asinclair
//Changes for DEL 7 Car Costing Units switch
//
//   Rev 1.13   Mar 03 2005 14:13:10   rscott
//DEL 7 - updated spans for miles to include a name attribute - to enable getelementsByName within FireFox
//
//   Rev 1.12   Mar 01 2005 15:44:20   asinclair
//Updated For Del 7 Car Costing
//
//   Rev 1.11   Jan 20 2005 10:39:08   asinclair
//Work in progress - Del 7 Car Costing
//
//   Rev 1.10   Nov 26 2004 14:56:00   jmorrissey
//CCN156 Motorway junction changes
//
//   Rev 1.9   Oct 19 2004 13:36:06   jgeorge
//Added road distance column back in. 
//Resolution for 1719: Road distance should be added back into car journey details
//
//   Rev 1.8   Oct 06 2004 17:11:58   jgeorge
//Modifications to text for car journey details. 
//Resolution for 1594: Road distance &duration should be removed from car journey details
//
//   Rev 1.7   Sep 27 2004 16:07:32   passuied
//Used the JourneyRequest.ReturnOriginLocation and JourneyRequest.ReturnDestinationLocation to fill return locations.
//Resolution for 1648: Return Extended journey : Car directions don' show correct origin/destination location
//
//   Rev 1.6   Jun 16 2004 20:23:50   JHaydock
//Update for JourneyDetailsTableControl to use ItineraryManager
//
//   Rev 1.5   Feb 04 2004 16:58:30   COwczarek
//Complete formatting required for DEL 5.2
//Resolution for 613: Refactoring of code that displays car journey details

using System;
using System.Globalization;
using System.Collections;
using System.Text;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.JourneyControl;
using System.Text.RegularExpressions;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web.Controls;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Provides a default implementation for formatting
	/// car journey details for output to a web page.
	/// </summary>
	public class DefaultCarJourneyDetailFormatter : CarJourneyDetailFormatter
	{

		private const string DISPLAY_MAP_BUTTONS = "DisplayMapButtons";
		bool MapButtonDisplay = Convert.ToBoolean(Properties.Current[DISPLAY_MAP_BUTTONS]);
        
		#region Constructors
		
		/// <summary>
		/// Constructs a formatter.
		/// </summary>
		/// <param name="journeyResult">Selected journey details</param>
		/// <param name="journeyViewState">Options pertaining to journey</param>
		/// <param name="outward">True if journey details are for outward journey, false if return</param>
		/// <param name="currentCulture">Culture for returned text</param>
		public DefaultCarJourneyDetailFormatter(
			ITDJourneyResult journeyResult, 
			TDJourneyViewState journeyViewState,
			bool outward,
			CultureInfo currentCulture,
			RoadUnitsEnum roadUnits, bool print
			) : base(journeyResult, journeyViewState, outward, currentCulture, roadUnits, print)
		{
            displayCongestionSymbol = false;
		}

		/// <summary>
		/// Constructs a formatter.
		/// </summary>
		/// <param name="roadJourney">The specific road journey to display</param>
		/// <param name="journeyViewState">The related journey view state</param>
		/// <param name="outward">Whether the journey is an outward one</param>
		/// <param name="currentCulture">Culture for returned text</param>
		public DefaultCarJourneyDetailFormatter(
			RoadJourney roadJourney, 
			TDJourneyViewState journeyViewState,
			bool outward,
			CultureInfo currentCulture,
			RoadUnitsEnum roadUnits, bool print
			) : base(roadJourney, journeyViewState, outward, currentCulture, roadUnits, print)
		{
            displayCongestionSymbol = false;
		}

		#endregion Constructors
		
		#region Public methods

		protected override string AddContinueFor(RoadJourneyDetail detail,
			bool nextDetailHasJunctionExitJunction, string routeText)
		{
			//convert metres to miles
			string milesDistance = ConvertMetresToMileage(detail.Distance);		
			string distanceInKm = ConvertMetresToKm(detail.Distance);
			string distanceInMiles = string.Empty;
			string kmDistance = string.Empty;
			string FerryCrossing = Global.tdResourceManager.GetString("RouteText.FerryCrossing",TDCultureInfo.CurrentUICulture); 
			
			//Switches the default display of either Miles or Kms depending on roadUnits
			if (roadUnits == RoadUnitsEnum.Miles)
			{
				distanceInMiles = "<span class=\"milesshow\">" + milesDistance + space + miles + "</span>";
				kmDistance = "<span class=\"kmshide\">" + distanceInKm + space + "km" + "</span>";
			}
			else
			{
				distanceInMiles = "<span class=\"mileshide\">" + milesDistance + space + miles + "</span>";
				kmDistance = "<span class=\"kmsshow\">" + distanceInKm + space + "km" + "</span>";
			}

			//check if this text should be added
			if (detail.IsFerry)
			{
				//no need to add the "continues for..." message in these situations
				routeText = FerryCrossing;
				return routeText;
			}
			else if (
				(detail.Distance <= immediateTurnDistance)  &&
				
				!((detail.JunctionAction == JunctionType.Entry) &&					
				detail.IsJunctionSection && detail.Roundabout &&
				(detail.PlaceName != null && detail.PlaceName.Length > 0) &&
				nextDetailHasJunctionExitJunction) &&
				
				!(detail.IsJunctionSection &&					
				((detail.JunctionAction == JunctionType.Exit) || 
				(detail.JunctionAction == JunctionType.Merge)))									
				)
			{
				//no need to add the "continues for..." message in these situations
				return routeText;

			}

			else if ((detail.IsSlipRoad) && (detail.Distance < slipRoadDistance) &&
				
				!((detail.JunctionAction == JunctionType.Entry) &&				
				detail.IsJunctionSection && detail.Roundabout &&
				(detail.PlaceName != null && detail.PlaceName.Length > 0) &&
				nextDetailHasJunctionExitJunction) &&
				
				!(detail.IsJunctionSection &&				
				((detail.JunctionAction == JunctionType.Exit) || 
				(detail.JunctionAction == JunctionType.Merge)) &&
				((detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))))
			{				
				//no need to add the "continues for..." message in these situations
				return routeText;
			}							
			else
			{
				//in all other cases add "continues for..." to the end of the instruction 
				routeText = routeText + continueFor + space + kmDistance + distanceInMiles;
			}

			return routeText;
		}

        /// <summary>
        /// Appends the limited access text, if any, to the existing instruction text 
        /// </summary>
        /// <param name="detail">The detail</param>
        /// <param name="routeText">Existing instruction text</param>
        /// <returns>Text with limited access text appended</returns>
        protected override string AddLimitedAccessText(RoadJourneyDetail detail, string routeText)
        {
            if (!string.IsNullOrEmpty(detail.LimitedAccessText))
            {
                routeText += "<br/>" + detail.LimitedAccessText;
            }
            return routeText;
        }
	

		/// <summary>
		/// Returns culture specific strings that are used to identify
		/// the elements of the string arrays returned by GetJourneyDetails
		/// The array has strings identifying accumulated distance, directions and arrival time
		/// </summary>
		/// <returns>Heading labels identifying elements of the string arrays
		/// returned by GetJourneyDetails</returns>
		public override string[] GetDetailHeadings() 
		{
		
			string[] details = new string[4];

			string MilesHeading = Global.tdResourceManager.GetString("CarJourneyDetailsTableControl.headerAccumulatedDistance",TDCultureInfo.CurrentUICulture);
			string KmHeading = "Trip km";

			//Switches the Km/Miles heading 
			if (roadUnits == RoadUnitsEnum.Miles)
			{
				details[0] = "<span class=\"milesshow\">" + MilesHeading + "</span>" + "<span class=\"kmshide\">" + KmHeading + "</span>";
			}
			else
			{
				details[0] = "<span class=\"mileshide\">" + MilesHeading + "</span>" + "<span class=\"kmsshow\">" + KmHeading + "</span>";
			}

			details[1] = Global.tdResourceManager.GetString(
				"CarJourneyDetailsTableControl.headerDirections", TDCultureInfo.CurrentUICulture);
			
			details[2] = Global.tdResourceManager.GetString(
				"CarJourneyDetailsTableControl.headerArrivalTime", TDCultureInfo.CurrentUICulture);

            // Show direction distance instead of arrival time
            string forMilesHeading = Global.tdResourceManager.GetString("CarJourneyDetailsTableControl.headerForDistance.Miles", TDCultureInfo.CurrentUICulture);
            string forKmHeading = Global.tdResourceManager.GetString("CarJourneyDetailsTableControl.headerForDistance.Km", TDCultureInfo.CurrentUICulture); ;

            //Switches the Km/Miles heading 
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                details[3] = "<span class=\"milesshow\">" + forMilesHeading + "</span>" + "<span class=\"kmshide\">" + forKmHeading + "</span>";
            }
            else
            {
                details[3] = "<span class=\"mileshide\">" + forMilesHeading + "</span>" + "<span class=\"kmsshow\">" + forKmHeading + "</span>";
            }

			return details;
		}

		/// <summary>
		/// Returns culture specific strings that are used to identify
		/// the elements of the string arrays returned by GetJourneyDetails
		/// The array has strings identifying accumulated distance, directions and arrival time
		/// </summary>
		/// <returns>Heading labels identifying elements of the string arrays
		/// returned by GetJourneyDetails</returns>
		public override object[] GetFooterHeadings() 
		{
			
			object[] details = new object[16];

            #region Add Find car parks link
            //if destination and origin are not a car park
            if ((this.ShowFindNearestCarParksLink)
                && (roadJourney != null)
                && (roadJourney.DestinationLocation.CarParking == null)
                && (roadJourney.OriginLocation.CarParking == null))
            {
                string findCarParkCaption = Global.tdResourceManager.GetString(
                    "CarCostingDetails.findNearestCarParkCaption", TDCultureInfo.CurrentUICulture);

                string findCarParkURL = Global.tdResourceManager.GetString(
                    "CarCostingDetails.findNearestCarParkUrl", TDCultureInfo.CurrentUICulture);

                string findCarParkLogo = Global.tdResourceManager.GetString(
                    "CarCostingDetails.findNearestCarParkLogo", TDCultureInfo.CurrentUICulture);

                // Set display text to be of the destination location in journey
                if (outward)
                    findCarParkCaption = string.Format(findCarParkCaption, roadJourney.DestinationLocation.Description);
                else
                    findCarParkCaption = string.Format(findCarParkCaption, roadJourney.OriginLocation.Description);

                details[9] = findCarParkCaption;
                details[10] = findCarParkLogo;
                details[11] = true;
            }
            else
            {
                details[9] = string.Empty;
                details[10] = string.Empty;
                details[11] = false;
            }
            #endregion

            if (roadJourney !=null && roadJourney.TotalDuration >= 7200 )
			{
				string planStop = Global.tdResourceManager.GetString(
					"CarCostingDetails.planStop", TDCultureInfo.CurrentUICulture);
				string thinkUrl = Global.tdResourceManager.GetString(
					"CarCostingDetails.thinkUrl", TDCultureInfo.CurrentUICulture);
				string thinkLogo = Global.tdResourceManager.GetString(
					"CarCostingDetails.thinkLogo", TDCultureInfo.CurrentUICulture);

				details[0] = thinkUrl + thinkLogo + "</a>";
				details[1] = planStop;
				details[4] = true;
			}
			else 
			{
				details[0] = string.Empty;
				details[1] = string.Empty;
				details[4] = false;

			}

			if (highTrafficLevel == true )
			{
				string highTrafficSymbol = Global.tdResourceManager.GetString(
					"CarCostingDetails.highTrafficSymbol", TDCultureInfo.CurrentUICulture);
				string highTrafficText = Global.tdResourceManager.GetString(
					"CarCostingDetails.highTrafficText", TDCultureInfo.CurrentUICulture);
			
				details[2] = highTrafficSymbol;
				details[3] = highTrafficText;
				details[5] = true;
			}
			else
			{
				details[2] = string.Empty;
				details[3] = string.Empty;
				details[5] = false;
			}
			
			if (roadJourney!= null)
			{
				string fuelEfficientLogo = Global.tdResourceManager.GetString(
					"CarFuelEfficient.PetrolLogo", TDCultureInfo.CurrentUICulture);
				string fuelEfficientLink = Global.tdResourceManager.GetString(
					"CarFuelEfficient.PetrolLink", TDCultureInfo.CurrentUICulture);
			
				details[6] = fuelEfficientLogo;
				details[7] = fuelEfficientLink;
				details[8] = true;
			}
			else
			{
				details[6] = string.Empty;
				details[7] = string.Empty;
				details[8] = false;
			}

            if (roadJourney != null)
            {
                string carSharingLogo = Global.tdResourceManager.GetString(
                    "CarSharing.CarSharingLogo", TDCultureInfo.CurrentUICulture);
                string carSharingLink = Global.tdResourceManager.GetString(
                    "CarSharing.CarSharingLink", TDCultureInfo.CurrentUICulture);
                string carSharingLinkText = Global.tdResourceManager.GetString(
                    "CarSharing.CarSharingLink.Text", TDCultureInfo.CurrentUICulture);

                details[12] = true;
                details[13] = carSharingLogo;
                details[14] = carSharingLinkText;
                details[15] = carSharingLink;
            }
            else
            {
                details[12] = false;
                details[13] = string.Empty;
                details[14] = string.Empty;
                details[15] = string.Empty;
            }

            
            return details;
		}

		#endregion Public methods
		
		#region Protected methods
		
		/// <summary>
		/// A hook method called by processRoadJourney to process each road journey
		/// instruction. The returned string array contains formatted details for
		/// step number, total distance, directions and arrival time in that order
		/// </summary>
		/// <param name="journeyDetailIndex">Array index into roadJourney.Details</param>
		/// <returns>details for each instruction</returns>
		protected override object[] processRoadJourneyDetail(int journeyDetailIndex, bool showCongestionCharge)
		{		
			object[] details = new object[11];
	
			// Note that the Accumulated Distance is updated in GetDirections, therefore the TotalDistance
			// displayed is the total prior to the current instruction rather than after it	
			RoadJourneyDetail roaddetail = roadJourney.Details[journeyDetailIndex];	
			RoadJourneyDetail previousroaddetail;
			
			if (journeyDetailIndex >0)
			{
				previousroaddetail = roadJourney.Details[journeyDetailIndex-1];
			}
			else
			{
				previousroaddetail = roadJourney.Details[journeyDetailIndex];
			}
            
			details[0] = GetCurrentStepNumber();

			if (roaddetail.viaLocation == true || roaddetail.wait == true)
			{
				if (roadUnits == RoadUnitsEnum.Miles)
				{
					details[1] = "<span class=\"milesshow\">" + TotalDistance + "</span>" + "<span class=\"kmshide\">" + TotalKmDistance + "</span>";
				}
				else
				{
					details[1] = "<span class=\"mileshide\">" + TotalDistance + "</span>" + "<span class=\"kmsshow\">" + TotalKmDistance + "</span>";
				}

			}
			else if (roaddetail.IsStopOver)
			{
				details[1] = "-";
			}
			else
			{
				if (roadUnits == RoadUnitsEnum.Miles)
				{
					details[1] = "<span class=\"milesshow\">" + TotalDistance + "</span>" + "<span class=\"kmshide\">" + TotalKmDistance + "</span>";
				}
				else
				{
					details[1] = "<span class=\"mileshide\">" + TotalDistance + "</span>" + "<span class=\"kmsshow\">" + TotalKmDistance + "</span>";
				}
			}
			
			//Vantive 3868786
			//is the journey a return and was congestion charge shown for the outward already?
			details[2] = GetDirections(journeyDetailIndex, showCongestionCharge);	
			details[3] = GetArrivalTime(journeyDetailIndex);
			details[4] = GetMapInfo(journeyDetailIndex);

			if (roaddetail.IsFerry)
			{
				details[5] = MapButtonDisplay;
			}

			else if(roaddetail.IsStopOver &&  roaddetail.nodeToid.Length == 0)
			{
				details[5] = false;
			}
		
			else
			{
				details[5] = MapButtonDisplay;
			}

            //direction distance
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                details[6] = "<span class=\"milesshow\">" + GetDistance(journeyDetailIndex, RoadUnitsEnum.Miles) + "</span>" + "<span class=\"kmshide\">" + GetDistance(journeyDetailIndex, RoadUnitsEnum.Kms) + "</span>";
                details[7] = "<span class=\"milesshow\">" + GetSpeed(journeyDetailIndex, RoadUnitsEnum.Miles) + "</span>" + "<span class=\"kmshide\">" + GetSpeed(journeyDetailIndex, RoadUnitsEnum.Kms) + "</span>";
            }
            else
            {
                details[6] = "<span class=\"mileshide\">" + GetDistance(journeyDetailIndex, RoadUnitsEnum.Miles) + "</span>" + "<span class=\"kmsshow\">" + GetDistance(journeyDetailIndex, RoadUnitsEnum.Kms) + "</span>";
                details[7] = "<span class=\"mileshide\">" + GetSpeed(journeyDetailIndex, RoadUnitsEnum.Miles) + "</span>" + "<span class=\"kmsshow\">" + GetSpeed(journeyDetailIndex, RoadUnitsEnum.Kms) + "</span>";
            }

            StringBuilder toidBuilder = new StringBuilder();
            try
            {
                foreach (string toid in roaddetail.Toid)
                {
                    toidBuilder.AppendFormat("{0} ", toid);
                }

                details[8] = toidBuilder.ToString().Trim();
            }
            catch
            {
                details[8] = string.Empty;
            }

            // Matching travel news incidents for the current journey detail
            details[9] = GetMatchingTravelNewsIncidents(journeyDetailIndex);

            // Bool value to determine if high traffic symbols needs displaying
            details[10] = (roaddetail.IsStopOver == false) && (roaddetail.CongestionLevel == true);
			
			return details;			
		}

		/// <summary>
		/// A hook method called by processRoadJourney to add the first element to the  ordered list 
		/// of journey instructions. The returned string array contains step number, a "Leave from ..." 
		/// instruction and "not applicable" indicators for accumulated distance and arrival time.
		/// </summary>
		/// <returns>details for first instruction</returns>
		protected override object[] addFirstDetailLine() 
		{
			StringBuilder stringDetails = new StringBuilder();
			object[] details = new object[11];
			
			//step number
			details[0] = GetCurrentStepNumber();

			CarPark carParking = null; // car park as part of Del9 Car Parking

			//accumulated distance
			details[1] = string.Empty;

			// if this is a park and ride location, should read 'Arrive at ...' 
			if (roadJourney.OriginLocation.ParkAndRideScheme != null)
			{
				CarParkInfo carPark = null;
				stringDetails.Append(leaveFrom);
				stringDetails.Append(" ");
				
				//create a link using car park if it is not null
				if (roadJourney.OriginLocation.CarPark == null)
				{
					roadJourney.OriginLocation.CarPark = 
						roadJourney.OriginLocation.ParkAndRideScheme.MatchCarPark(
						roadJourney.Details[roadJourney.Details.Length - 1].Toid);
				}

				if (roadJourney.OriginLocation.ParkAndRideScheme.Location != null) 
				{
					if ((roadJourney.OriginLocation.ParkAndRideScheme.UrlLink != null) && (print))
					{
						stringDetails.Append("<a href=\"");
						stringDetails.Append(roadJourney.OriginLocation.ParkAndRideScheme.UrlLink);
						stringDetails.Append("\" target=\"_blank\">");
						stringDetails.Append(roadJourney.OriginLocation.ParkAndRideScheme.Location);
						stringDetails.Append(parkAndRide);
                        stringDetails.Append(space);
                        stringDetails.Append(openNewWindowImageUrl);
						stringDetails.Append("</a>");
					}
					else
					{
						stringDetails.Append("<b>");
						stringDetails.Append(roadJourney.OriginLocation.ParkAndRideScheme.Location);
						stringDetails.Append(parkAndRide);
						stringDetails.Append("</b>");
					}

					// car park as part of a park and ride scheme
					if (roadJourney.OriginLocation.CarPark != null)
					{
						carPark = roadJourney.OriginLocation.CarPark;

						if ((carPark.UrlLink != null) && (print))
						{
							stringDetails.Append(", ");
							stringDetails.Append("<a href=\"");
							stringDetails.Append(carPark.UrlLink);
							stringDetails.Append("\" target=\"_blank\">");
							stringDetails.Append(carPark.CarParkName);
							stringDetails.Append(carParkText);
                            stringDetails.Append(space);
                            stringDetails.Append(openNewWindowImageUrl);
							stringDetails.Append("</a>");
							stringDetails.Append(" &nbsp; ");
						}
					}
				}
				else 
				{
					// car park destination used to plan standard car journey (not a park and ride)
					if (roadJourney.DestinationLocation.CarPark != null)
					{
						carPark = roadJourney.DestinationLocation.CarPark;

						//car park property is not null - create a link
						if ((carPark.UrlLink != null) && (print))
						{
							stringDetails.Append("<a href=\"");
							stringDetails.Append(carPark.UrlLink);
							stringDetails.Append("\" target=\"_blank\">");
							stringDetails.Append(carPark.CarParkName);
							stringDetails.Append(carParkText);
                            stringDetails.Append(space);
                            stringDetails.Append(openNewWindowImageUrl);
							stringDetails.Append("</a>");
							stringDetails.Append(" &nbsp; ");
						}
						else
						{
							stringDetails.Append("<b>");
							stringDetails.Append(carPark.CarParkName);
							stringDetails.Append(carParkText);
							stringDetails.Append("</b>");
							stringDetails.Append(" &nbsp; ");
						}
					}

				}

				details[2] = stringDetails.ToString();
			}
			else if(roadJourney.OriginLocation.CarParking != null)
			{
				carParking = roadJourney.OriginLocation.CarParking;

				stringDetails.Append(leaveFrom);
				string SEPARATOR = "$";

				stringDetails.Append(SEPARATOR);
				stringDetails.Append("<b>");
                stringDetails.Append(FindCarParkHelper.GetCarParkName(carParking));
				stringDetails.Append("</b>");
				stringDetails.Append(SEPARATOR);
				stringDetails.Append(carParking.Name);
				stringDetails.Append(SEPARATOR);
				stringDetails.Append(carParking.CarParkReference);

				details[2] = stringDetails.ToString();
			}
			else 
			{
				details[2] = leaveFrom + " <b>" + roadJourney.OriginLocation.Description + "</b>";
			}
	
			//arrival time
			details[3] = string.Empty;
			details[4] = string.Empty;
			details[5] = MapButtonDisplay;

            //distance
            details[6] = string.Empty;
            //speed
            details[7] = string.Empty;
            //Link Toids
            StringBuilder toidBuilder = new StringBuilder();

            if (roadJourney != null && roadJourney.OriginLocation != null)
            {
                foreach (string toid in roadJourney.OriginLocation.Toid)
                {
                    toidBuilder.AppendFormat("{0} ", toid);
                }
            }

            details[8] = toidBuilder.ToString().Trim();

            // Matching travel news incidents for the current journey detail
            details[9] = new string[0];

            // Bool value to determine if high traffic symbols needs displaying
            details[10] = false;

			return details;			

		}
		
		/// <summary>
		/// A hook method called by processRoadJourney to add the last element to the 
		/// ordered list of journey instructions. The returned string array contains step number,
		/// an "arrive at ..." instruction and arrival time, and an empty string for accumulated distance
		/// </summary>
		/// <returns>details for last instruction</returns>
		protected override object[] addLastDetailLine() 
		{
			StringBuilder stringDetails = new StringBuilder();
			object[] details = new object[11];
			CarParkInfo carPark = null; //car park in park and ride scheme
			CarPark carParking = null; // car park as part of Del9 Car Parking
			int journeyDetailIndex = roadJourney.Details.Length - 1;

			//step number
			details[0] = GetCurrentStepNumber();

			//accumulated distance
			if (roadUnits == RoadUnitsEnum.Miles)
			{
				details[1] = "<span class=\"milesshow\">" + TotalDistance + "</span>" + "<span class=\"kmshide\">" + TotalKmDistance + "</span>";
			}
			else
			{
				details[1] = "<span class=\"mileshide\">" + TotalDistance + "</span>" + "<span class=\"kmsshow\">" + TotalKmDistance + "</span>";
			}
			
			//instruction

			/* The park and ride scheme may not have a car park specified.  
			 * User may have planned a standard car journey to a car park
			 */
			if (roadJourney.DestinationLocation.ParkAndRideScheme != null)
			{
				stringDetails.Append(arriveAt);
				
				//create a link using car park if it is not null
				if (roadJourney.DestinationLocation.CarPark == null)
				{
					roadJourney.DestinationLocation.CarPark = 
						roadJourney.DestinationLocation.ParkAndRideScheme.MatchCarPark(
						roadJourney.Details[roadJourney.Details.Length - 1].Toid);
				}

				//create a link based on the values of location and url link
				if (roadJourney.DestinationLocation.ParkAndRideScheme.Location != null) 
				{
					if ((roadJourney.DestinationLocation.ParkAndRideScheme.UrlLink != null) && (print))
					{
						stringDetails.Append("<a href=\"");
						stringDetails.Append(roadJourney.DestinationLocation.ParkAndRideScheme.UrlLink);
						stringDetails.Append("\" target=\"_blank\">");
						stringDetails.Append(roadJourney.DestinationLocation.ParkAndRideScheme.Location);
						stringDetails.Append(parkAndRide);
                        stringDetails.Append(space);
                        stringDetails.Append(openNewWindowImageUrl);
						stringDetails.Append("</a>");
					}
					else
					{
						stringDetails.Append("<b>");
						stringDetails.Append(roadJourney.DestinationLocation.ParkAndRideScheme.Location);
						stringDetails.Append(parkAndRide);
						stringDetails.Append("</b>");
					}

					// car park as part of a park and ride scheme
					if (roadJourney.DestinationLocation.CarPark != null)
					{
						carPark = roadJourney.DestinationLocation.CarPark;

						if ((carPark.UrlLink != null) && (print))
						{
							stringDetails.Append(", ");// add comma as this follow park and ride text
							stringDetails.Append("<a href=\"");
							stringDetails.Append(carPark.UrlLink);
							stringDetails.Append("\" target=\"_blank\">");
							stringDetails.Append(carPark.CarParkName);
							stringDetails.Append(carParkText);
                            stringDetails.Append(space);
                            stringDetails.Append(openNewWindowImageUrl);
							stringDetails.Append("</a>");
							stringDetails.Append(" &nbsp; ");
						}
						else
						{
							// DN58: if there is no car park url, use the park and ride url
							if((roadJourney.DestinationLocation.ParkAndRideScheme.UrlLink != null) && (print))
							{
								stringDetails.Append(", ");// add comma as this follow park and ride text
								stringDetails.Append("<a href=\"");
								stringDetails.Append(roadJourney.DestinationLocation.ParkAndRideScheme.UrlLink);
								stringDetails.Append("\" target=\"_blank\">");
								stringDetails.Append(carPark.CarParkName);
								stringDetails.Append(carParkText);
                                stringDetails.Append(space);
                                stringDetails.Append(openNewWindowImageUrl);
								stringDetails.Append("</a>");
								stringDetails.Append(" &nbsp; ");
							}
							else 
							{
								stringDetails.Append(", ");
								stringDetails.Append("<b>");
								stringDetails.Append(carPark.CarParkName);
								stringDetails.Append(carParkText);
								stringDetails.Append("</b>");
								stringDetails.Append(" &nbsp; ");
							}
						}
					}
				}	
				else 
				{   

					// car park destination used to plan standard car journey (not a park and ride)
					if (roadJourney.DestinationLocation.CarPark != null)
					{
						carPark = roadJourney.DestinationLocation.CarPark;

						//car park property is not null - create a link
						if ((carPark.UrlLink != null) && (print))
						{
							stringDetails.Append("<a href=\"");
							stringDetails.Append(carPark.UrlLink);
							stringDetails.Append("\" target=\"_blank\">");
							stringDetails.Append(carPark.CarParkName);
							stringDetails.Append(carParkText);
                            stringDetails.Append(space);
                            stringDetails.Append(openNewWindowImageUrl);
							stringDetails.Append("</a>");
							stringDetails.Append(" &nbsp; ");
						}
						else
						{
							stringDetails.Append("<b>");
							stringDetails.Append(carPark.CarParkName);
							stringDetails.Append(carParkText);
							stringDetails.Append("</b>");
							stringDetails.Append(" &nbsp; ");
						}
					}
					

				}
			}

			else if(roadJourney.DestinationLocation.CarParking != null)
			{
				carParking = roadJourney.DestinationLocation.CarParking;

				stringDetails.Append(arriveAt);
				string SEPARATOR = "$";
				
				stringDetails.Append(SEPARATOR);
				stringDetails.Append("<b>");
                stringDetails.Append(FindCarParkHelper.GetCarParkName(carParking));
				stringDetails.Append("</b>");
				stringDetails.Append(SEPARATOR);
				stringDetails.Append(carParking.Name);
				stringDetails.Append(SEPARATOR);
				stringDetails.Append(carParking.CarParkReference);				
			}
			else
			{
				stringDetails.Append(arriveAt);
				stringDetails.Append("<b>");
				stringDetails.Append(roadJourney.DestinationLocation.Description);
				stringDetails.Append("</b>");
				stringDetails.Append(" &nbsp; ");
			}

			details[2] = stringDetails.ToString();

			//arrival time
			details[3] = GetArrivalTime(journeyDetailIndex);

			details[4] = string.Empty;
			details[5] = MapButtonDisplay;

            //direction distance
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                details[6] = "<span class=\"milesshow\">" + GetDistance(journeyDetailIndex, RoadUnitsEnum.Miles) + "</span>" + "<span class=\"kmshide\">" + GetDistance(journeyDetailIndex, RoadUnitsEnum.Kms) + "</span>";
                details[7] = "<span class=\"milesshow\">" + GetSpeed(journeyDetailIndex, RoadUnitsEnum.Miles) + "</span>" + "<span class=\"kmshide\">" + GetSpeed(journeyDetailIndex, RoadUnitsEnum.Kms) + "</span>";
            }
            else
            {
                details[6] = "<span class=\"mileshide\">" + GetDistance(journeyDetailIndex, RoadUnitsEnum.Miles) + "</span>" + "<span class=\"kmsshow\">" + GetDistance(journeyDetailIndex, RoadUnitsEnum.Kms) + "</span>";
                details[7] = "<span class=\"mileshide\">" + GetSpeed(journeyDetailIndex, RoadUnitsEnum.Miles) + "</span>" + "<span class=\"kmsshow\">" + GetSpeed(journeyDetailIndex, RoadUnitsEnum.Kms) + "</span>";
            }

            StringBuilder toidBuilder = new StringBuilder();

            if (roadJourney != null && roadJourney.DestinationLocation != null)
            {
                foreach (string toid in roadJourney.DestinationLocation.Toid)
                {
                    toidBuilder.AppendFormat("{0} ", toid);
                }
            }

            details[8] = toidBuilder.ToString().Trim();

            // Matching travel news incidents for the current journey detail
            details[9] = new string[0];

            // Bool value to determine if high traffic symbols needs displaying
            details[10] = false;

            return details;			

		}



		/// <summary>
		/// A hook method called by processRoadJourney to add the 'Think' symbol, link and message to the end of
		/// the ordered list of journey instructions.
		/// </summary>
		/// <returns>Image, message and link for 'Think' campaign</returns>
		protected override object[]addThinkSymbol()
		{
			string planStop = Global.tdResourceManager.GetString(
				"CarCostingDetails.planStop", TDCultureInfo.CurrentUICulture);
			string thinkUrl = Global.tdResourceManager.GetString(
				"CarCostingDetails.thinkUrl", TDCultureInfo.CurrentUICulture);
			string thinkLogo = Global.tdResourceManager.GetString(
				"CarCostingDetails.thinkLogo", TDCultureInfo.CurrentUICulture);

			object[] details = new object[2];

			details[0] = thinkUrl + thinkLogo + "</a>";
			details[1] = planStop;

			return details;
		}

		/// <summary>
		/// A hook method called by processRoadJourney to add the Traffic warning symbol and message to the end of
		/// the ordered list of journey instructions.
		/// </summary>
		/// <returns>Traffic warning image and message</returns>
		protected override object[]addTrafficWarning()
		{

			string highTrafficSymbol = Global.tdResourceManager.GetString(
				"CarCostingDetails.highTrafficSymbol", TDCultureInfo.CurrentUICulture);
			string highTrafficText = Global.tdResourceManager.GetString(
				"CarCostingDetails.highTrafficText", TDCultureInfo.CurrentUICulture);

			object[] details = new object[2];
		
			details[0] = highTrafficSymbol;

			details[1] = highTrafficText;

			return details;
		}

		
		/// <summary>
		/// Returns formatted string of the road name for the supplied
		/// instruction. Overrides default implmentation to enclose road
		/// name in HTML bold element.
		/// </summary>
		/// <param name="detail">Details of journey instruction</param>
		/// <returns>The road name</returns>
		protected override string FormatRoadName(RoadJourneyDetail detail)
		{
			//Regex regexMotorway = new Regex(motorwaynumber);
			
			//Check for a motorway
			if (detail.RoadNumber != null)
			{

				if ((detail.RoadNumber.StartsWith("M") | (detail.RoadNumber.EndsWith("(M)")))  && (detail.RoadName != string.Empty))
				{
					return "<span class=\"mwayLabel\">" + detail.RoadNumber +"</span>" + "<b>" + " (" + detail.RoadName + ")" + "</b> ";
				}
				else if ((detail.RoadNumber.StartsWith("M"))| (detail.RoadNumber.EndsWith("(M)")))
				{				
					return "<span class=\"mwayLabel\">" + detail.RoadNumber +"</span>";
				}
				else
				{
					return "<b>" + base.FormatRoadName(detail) +"</b>";
				}
			}
			else
			{
				return " ";
			}
		}
		
		#endregion Protected methods
    }	

}
