// *********************************************** 
// NAME			: EmailCarJourneyDetailFormatter.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 21.01.04
// DESCRIPTION	: Provides implementation for formatting
// car journey details into an email.
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/EmailCarJourneyDetailFormatter.cs-arc  $
//
//   Rev 1.5   Mar 15 2011 11:14:56   rphilpott
//Fix email formatting bug
//
//   Rev 1.4   Mar 14 2011 15:12:02   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.3   Nov 29 2009 12:35:12   mmodi
//Constructor to pass in a road journey
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Mar 31 2008 12:58:54   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:14   mturner
//Initial revision.
//
//   Rev 1.19   Oct 17 2007 13:30:52   pscott
//UK:1361875  IR 4515  change instances of Km to km
//
//   Rev 1.18   Apr 07 2006 17:12:24   mguney
//Update continue method.
//Resolution for 3849: Car Journey Instructions Update after stream0030
//
//   Rev 1.17   Feb 23 2006 19:16:06   build
//Automatically merged from branch for stream3129
//
//   Rev 1.16.1.1   Jan 30 2006 12:15:20   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.16.1.0   Jan 10 2006 15:17:32   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.16   Aug 03 2005 21:06:40   asinclair
//fix for 2639
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Carge for return journeys
//
//   Rev 1.15   Aug 02 2005 12:49:52   jmorrissey
//Fix for Vantive 3868786
//
//   Rev 1.14   May 19 2005 10:31:58   rscott
//IR2499 - changes to charge message in detail descriptions for Ferries, Tolls and Congestion Charges.
//
//   Rev 1.13   May 12 2005 17:34:02   asinclair
//Added a print bool to constructor
//
//   Rev 1.12   May 03 2005 15:32:22   jbroome
//Modified congestion charge instruction when no charge is applicable.
//Resolution for 2395: DEL 7 Car Costing - Send to a firend
//
//   Rev 1.11   Apr 23 2005 19:30:00   asinclair
//Further temp check in
//
//   Rev 1.10   Apr 23 2005 14:11:36   asinclair
//Temp check in to fix build errors
//
//   Rev 1.9   Mar 18 2005 16:25:14   asinclair
//Changed string[] to object[]
//
//   Rev 1.8   Mar 01 2005 15:44:32   asinclair
//Updated for Del 7 Car Costing
//
//   Rev 1.7   Jan 20 2005 15:09:42   asinclair
//Updated for Del 7 Car Costing
//
//   Rev 1.6   Nov 26 2004 14:56:02   jmorrissey
//CCN156 Motorway junction changes
//
//   Rev 1.5   Oct 19 2004 13:36:12   jgeorge
//Added road distance column back in. 
//Resolution for 1719: Road distance should be added back into car journey details
//
//   Rev 1.4   Oct 06 2004 17:12:00   jgeorge
//Modifications to text for car journey details. 
//Resolution for 1594: Road distance &duration should be removed from car journey details
//
//   Rev 1.3   Sep 30 2004 11:57:52   rhopkins
//IR1648 Use ReturnOriginLocation and ReturnDestinationLocation when outputting Car segments of Extended Journeys
//
//   Rev 1.2   Feb 04 2004 16:58:32   COwczarek
//Complete formatting required for DEL 5.2
//Resolution for 613: Refactoring of code that displays car journey details

using System;using TransportDirect.Common.ResourceManager;
using System.Globalization;
using TransportDirect.Web.Support;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.SessionManager;
using System.Text;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Provides implementation for formatting
	/// car journey details into an email.
	/// </summary>
	public class EmailCarJourneyDetailFormatter : CarJourneyDetailFormatter
	{
		//string HighTraffic = Global.tdResourceManager.GetString("RouteText.HighTraffic", CultureInfo.CurrentUICulture);
		//string PlanStop = Global.tdResourceManager.GetString("RouteText.PlanStop", CultureInfo.CurrentUICulture);

		#region Constructors

		/// <summary>
		/// Constructs a formatter.
		/// </summary>
		/// <param name="journey">Selected journey details</param>
		/// <param name="journeyViewState">Options pertaining to journey</param>
		/// <param name="outward">True if journey details are for outward journey, false if return</param>
		/// <param name="currentCulture">Culture for returned text</param>
		public EmailCarJourneyDetailFormatter(
			ITDJourneyResult journey, 
			TDJourneyViewState journeyViewState,
			bool outward,
			CultureInfo currentCulture,
			RoadUnitsEnum roadUnits, bool print
			) : base(journey, journeyViewState, outward, currentCulture, roadUnits, print)
		{
		}

        /// <summary>
        /// Constructs a formatter.
        /// </summary>
        /// <param name="journey">Selected journey param>
        /// <param name="journeyViewState">Options pertaining to journey</param>
        /// <param name="outward">True if journey details are for outward journey, false if return</param>
        /// <param name="currentCulture">Culture for returned text</param>
        public EmailCarJourneyDetailFormatter(
            RoadJourney journey,
            TDJourneyViewState journeyViewState,
            bool outward,
            CultureInfo currentCulture,
            RoadUnitsEnum roadUnits, bool print
            )
            : base(journey, journeyViewState, outward, currentCulture, roadUnits, print)
        {
        }
		
		#endregion Constructors
        
		#region Public methods



		//protected override string AddContinueFor(RoadJourneyDetail detail, string routeText)
		protected override string AddContinueFor(RoadJourneyDetail detail,
			bool nextDetailHasJunctionExitJunction, string routeText)
		{
			//convert metres to miles
			//string distanceInMiles = ConvertMetresToMileage(detail.Distance);
			string milesDistance = ConvertMetresToMileage(detail.Distance);
			
			string distanceInKm = ConvertMetresToKm(detail.Distance);

			string distanceInMiles = string.Empty;
			string kmDistance = string.Empty;
			string FerryCrossing = Global.tdResourceManager.GetString("RouteText.FerryCrossing",TDCultureInfo.CurrentUICulture);
			
			//string kmDistance = "<span id=\"miles\">" + distanceInKm + "</span>";

			if (roadUnits == RoadUnitsEnum.Miles)
			{
				distanceInMiles = milesDistance + space + miles;
				
			}
			else
			{
				kmDistance = distanceInKm + space + "km"; //need to add to langstrings
			}

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
				//add "For" when continue at a mini-roundabaout and a placename present  
			else if ((detail.Direction == TurnDirection.MiniRoundaboutContinue)  && 
				(detail.PlaceName != null && detail.PlaceName.Length == 0)) 
			{
				//adds "For..." to the end of the instruction
				//routeText = routeText + forText + space + distanceInMiles + space + miles;
				routeText = routeText + forText + space + kmDistance + distanceInMiles;
			}
			else
			{
				//in all other cases add "continues for..." to the end of the instruction 
				//routeText = routeText + continueFor + space + distanceInMiles + space + miles;
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
                routeText += ". " + detail.LimitedAccessText;
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
		
			string[] details = new string[3];

			string MilesHeading = Global.tdResourceManager.GetString("CarJourneyDetailsTableControl.headerAccumulatedDistance",TDCultureInfo.CurrentUICulture);
			string KmHeading = "Trip km";

			//Sets either kms or miles heading depending on selection.
			if (roadUnits == RoadUnitsEnum.Miles)
			{
				details[0] = MilesHeading;
			}
			else
			{
				details[0] = KmHeading;
			}

			details[1] = Global.tdResourceManager.GetString(
				"AmendSaveSendControl.headerDirections", TDCultureInfo.CurrentUICulture);			

			details[2] = Global.tdResourceManager.GetString(
				"AmendSaveSendControl.headerArrivalTime", TDCultureInfo.CurrentUICulture);

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
		
			object[] details = new object[3];

			string MilesHeading = Global.tdResourceManager.GetString("CarJourneyDetailsTableControl.headerAccumulatedDistance",TDCultureInfo.CurrentUICulture);

			//Sets either kms or miles heading depending on selection.
			//if (roadUnits == RoadUnitsEnum.Miles)
			//{
			details[0] = MilesHeading;
			//}
			//else
			//{
			//	details[0] = KmHeading;
			//}

			details[1] = Global.tdResourceManager.GetString(
				"AmendSaveSendControl.headerDirections", TDCultureInfo.CurrentUICulture);			

			details[2] = Global.tdResourceManager.GetString(
				"AmendSaveSendControl.headerArrivalTime", TDCultureInfo.CurrentUICulture);

			return details;
		    
		}
        
		#endregion Public methods
        
		#region Protected methods

		/// <summary>
		/// A hook method called by processRoadJourney to process each road journey
		/// instruction. The returned string array contains formatted details for
		/// total distance, directions and arrival time in that order.
		/// </summary>
		/// <param name="journeyDetailIndex">Array index into roadJourney.Details</param>
		/// <returns>details for each instruction</returns>
		protected override object[] processRoadJourneyDetail(int journeyDetailIndex, bool sameDayReturn) 
		{		
			object[] details = new object[3];
		    
			// Note that the Accumulated Distance is updated in GetDirections, therefore the TotalDistance
			// displayed is the total prior to the current instruction rather than after it			
			details[0] = TotalDistance;
			details[1] = GetDirections(journeyDetailIndex, sameDayReturn);			
			details[2] = GetArrivalTime(journeyDetailIndex);
			

			return details;		    
		}

		/// <summary>
		/// A hook method called by processRoadJourney to add a toll entry and charge to the
		/// ordered list of journey instructions, formatted for email.
		/// </summary>
		/// <returns>Toll company name and charge</returns>
		protected override string TollEntry(RoadJourneyDetail detail)
		{
			StringBuilder routeText = new StringBuilder(string.Empty);

			//Convert TollCost value to pounds & pence
			decimal pence = (Convert.ToDecimal(detail.TollCost))/100;
			string cost = string.Format("{0:C}", pence);


			//add "Toll:" to start of instruction			
			if (pence < 0)
			{
				//IR2499 added in message when charge is unknown "Charge: Not Available"
				routeText.Append(toll);
				routeText.Append(space);
				routeText.Append(detail.TollEntry);
				routeText.Append(space);
				routeText.Append(charge);
				routeText.Append(notAvailable);
			}
			else if (pence == 0)
			{
				routeText.Append(toll);
				routeText.Append(space);
				routeText.Append(detail.TollEntry);
				routeText.Append(space);
				routeText.Append(charge);
				routeText.Append("£0.00");
			}
			else
			{
				routeText.Append(toll);
				routeText.Append(space);
				routeText.Append(detail.TollEntry);
				routeText.Append(space);
				routeText.Append(charge);
				routeText.Append(cost);
			}
			return routeText.ToString();
		}

		/// <summary>
		/// A hook method called by processRoadJourney to add a congestion charge entry and cost to the
		/// ordered list of journey instructions, formatted for email
		/// </summary>
		/// <returns>Congestion Zone name and cost</returns>
		protected override string CongestionEntry(RoadJourneyDetail detail, bool showCongestionCharge)
		{
			StringBuilder routeText = new StringBuilder(string.Empty);
			string cost = String.Empty;
			decimal pence = (Convert.ToDecimal(detail.TollCost))/100;
			cost = string.Format("{0:C}", pence);
			
			// If there is a toll cost
			if (detail.TollCost > 0) 
			{	
				if(!showCongestionCharge)
				{
					routeText.Append(enter); 
					routeText.Append(space);
					routeText.Append(detail.CongestionZoneEntry);
					routeText.Append(fullstop);
					routeText.Append(space);
					routeText.Append(charge);
					routeText.Append("£0.00");
					routeText.Append(space); 
					routeText.Append(certainTimesNoCharge);


				}
			
				else
				{
					routeText.Append(enter);
					routeText.Append(space);
					routeText.Append(detail.CongestionZoneEntry);
					routeText.Append(space);
					routeText.Append(charge);
					routeText.Append(cost);
					routeText.Append(space);
					routeText.Append(certainTimes);

					//Vantive3868786
					journeyViewState.CongestionChargeAdded = true;
				}
			}
			else if ((detail.TollCost == 0) || (!showCongestionCharge))
			{
				routeText.Append(enter);
				routeText.Append(space);
				routeText.Append(detail.CongestionZoneEntry);
				routeText.Append(fullstop);
				routeText.Append(space);
				routeText.Append(charge);
				routeText.Append("£0.00");
				routeText.Append(space);
				routeText.Append(certainTimes);
			}
			else
			{
				routeText.Append(enter); 
				routeText.Append(space);
				routeText.Append(detail.CongestionZoneEntry);
				routeText.Append(fullstop);
				routeText.Append(space);
				routeText.Append(charge);
				routeText.Append(notAvailable);
				routeText.Append(space); 
				routeText.Append(certainTimesNoCharge);
			}
			return routeText.ToString();
		}

		/// <summary>
		/// A hook method called by processRoadJourney to add a ferry entry and cost to the
		/// ordered list of journey instructions, formatted for email
		/// </summary>
		/// <returns>Ferry company name and ferry cost</returns>
		protected override string FerryEntry(RoadJourneyDetail detail, RoadJourneyDetail PreviousDetail)
		{
			bool previousInstruction = PreviousDetail.undefindedWait;
			StringBuilder routeText = new StringBuilder(string.Empty);
			TimeSpan span = new TimeSpan(0, 0, (int)detail.Duration);
			TDDateTime arrivalTime = currentArrivalTime;
			currentArrivalTime = currentArrivalTime.Add(span);
			string time = FormatDateTime(currentArrivalTime);
			
			//string CostPence = string.Format("{0}", detail.TollCost);
			//cost = CostPence.Insert( CostPence.Length - 2, ".");	

			decimal pence = (Convert.ToDecimal(detail.TollCost))/100;
			string cost = string.Format("{0:C}", pence);

			//add "Board:" to start of instruction			
			//If previous instruction was an undefinedWait we don't want to display a time(as we don't know it)
			if (previousInstruction)
			{
				routeText.Append(board);
				routeText.Append(space);
				routeText.Append(detail.FerryCheckIn);
			}
			else
			{
				routeText.Append(board);
				routeText.Append(space);
				routeText.Append(detail.FerryCheckIn);
				routeText.Append(space);
				routeText.Append(departingAt);
				routeText.Append(space);
				routeText.Append(time);	
			}

            if (pence < 0)
			{
				routeText.Append(";");
				routeText.Append(space);
				routeText.Append(charge);
				routeText.Append(notAvailable);
			}
			else if (pence == 0)
			{
				routeText.Append(";");
				routeText.Append(space);
				routeText.Append(charge);
				routeText.Append("£0.00");
			}
			else
			{
				routeText.Append(";");
				routeText.Append(space);
				routeText.Append(charge);
				routeText.Append(cost);
			}
			return routeText.ToString();
		}


		/// <summary>
		/// A hook method called by processRoadJourney to add the first element to the 
		/// ordered list of journey instructions. The returned string array contains a "Leave from ..." 
		/// instruction and "not applicable" indicators for accumulated distance and arrival time.
		/// </summary>
		/// <returns>details for first instruction</returns>
		protected override object[] addFirstDetailLine() 
		{
			object[] details = new object[3];
		    
			//accumulated distance
			details[0] = string.Empty;

			//instruction
			if	(outward)
			{
				details[1] = leaveFrom + " " + journeyViewState.OriginalJourneyRequest.OriginLocation.Description;
			}
			else
			{
				details[1] = leaveFrom + " " + journeyViewState.OriginalJourneyRequest.ReturnOriginLocation.Description;
			}

			//arrival time
			details[2] = string.Empty;
			
		    
			return details;		    

		}
		
		/// <summary>
		/// A hook method called by processRoadJourney to add the last element to the 
		/// ordered list of journey instructions. The returned string array
		/// contains formatted details for arrival time and 
		/// "Arrive at ..." and empty strings for road distance, total distance and 
		/// duration (in that order).
		/// </summary>
		/// <returns>details for last instruction</returns>
		protected override object[] addLastDetailLine() 
		{

			object[] details = new object[3];

			int journeyDetailIndex = roadJourney.Details.Length - 1;

			//accumulated distance
			details[0] = TotalDistance;
            
			//instruction
			if (outward)
			{
				details[1] = arriveAt + " " + journeyViewState.OriginalJourneyRequest.DestinationLocation.Description;
			} 
			else 
			{
				details[1] = arriveAt + " " + journeyViewState.OriginalJourneyRequest.ReturnDestinationLocation.Description;
			}

			//arrival time
			details[2] = GetArrivalTime(journeyDetailIndex);
            		    
			return details;		    

		}

		/// <summary>
		/// A hook method called by processRoadJourney to add the 'Plan to stop' message on journeys over two 
		/// hours, formatted for email
		/// </summary>
		/// <returns>String containing 'Plan to stop' message</returns>
		protected override object[]addThinkSymbol()
		{

			object[] details = new object[4];

			details[0] = string.Empty;
			details[1] = string.Empty;
			details[2] = PlanStop;
			details[3] = string.Empty;

			return details;
		}

		/// <summary>
		/// A method called by processRoadJourney if the journey contained drive legs with high
		/// congestion levels. Displays the message at the end of the instructions, 
		/// formatted for email.
		/// </summary>
		/// <returns>Message indicating that a * is displayed to show high traffic levels</returns>
		protected override object[]addTrafficWarning()
		{

			object[] details = new object[4];
			
			details[0] = string.Empty;
			details[1] = "*"; 
			details[2] = HighTraffic;
			details[3] = string.Empty;

			return details;
		}

		/// <summary>
		/// A method called by processRoadJourney if the congestion levels for a certain 
		/// road direction are high.  Displays a * by the leg instruction. 
		/// Formatted for email.
		/// </summary>
		/// <returns>adds * to route direction</returns>
		protected override string AddCongestionSymbol (string routeText)
		{
			routeText += " *";
			return routeText;
		}

		/// <summary>
		/// checks if a place name exists and formats the instruction accordingly
		/// </summary>
		/// <param name="detail">the RoadJourneyDetail being formatted </param>
		/// <param name="routeText">the existing instruction text </param>
		/// <returns>updated formatted string of the directions</returns>
		protected override string AddPlaceName(RoadJourneyDetail detail, string routeText)
		{
			//add "towards {placename}" to the end of the instruction 
			routeText = routeText + space + towards + space + detail.PlaceName;

			return routeText;
		}

		/// <summary>
		/// processes the junction number and junction action values
		/// </summary>
		protected override string FormatMotorwayJunction(RoadJourneyDetail detail, string routeText)
		{
			string junctionText = String.Empty;

			//apply the junction number rules 
			switch (detail.JunctionAction)
			{

				case JunctionType.Entry :

					//add a join motorway message to the instructional text
					routeText = routeText + space + atJunctionJoin + space 
						+ detail.JunctionNumber;
										
					break;

				case JunctionType.Exit :

					//replace the normal instructional text with a leave motorway message
					routeText = atJunctionLeave + space + detail.JunctionNumber + space + leaveMotorway;					

					break;

				case JunctionType.Merge :

					//replace the normal instructional text with a leave motorway message
					routeText = atJunctionLeave + space + detail.JunctionNumber + space + leaveMotorway;
					
					break;

				default :

					//no action required
					return routeText;					
			}			
			
			//return amended route text
			return routeText;

		}
		
		/// <summary>
		/// Checks the subsequent RoadJourneyDetail
		/// </summary>
		/// <param name="detail"></param>
		/// <param name="routeText"></param>
		/// <returns></returns>
		protected  override string CheckNextDetail(RoadJourneyDetail nextDetail, string routeText)
		{
			//add "until junction number {no}" to end of current instruction
			if (nextDetail.JunctionAction == JunctionType.Exit)
			{
				routeText = routeText + space + untilJunction + space + nextDetail.JunctionNumber;
			}
			return routeText;
		}
	}
}
        
        #endregion Protected methods