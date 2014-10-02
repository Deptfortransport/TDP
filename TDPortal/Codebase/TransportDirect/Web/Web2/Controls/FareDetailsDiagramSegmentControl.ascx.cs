// *********************************************************************** 
// NAME                 : FareDetailsDiagramSegmentControl.ascx.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 19/01/2005
// DESCRIPTION			: Displays a section of a journey; either the 
//						  legs contained in a single PricingUnit or a 
//						  single unpriced leg.
// ************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FareDetailsDiagramSegmentControl.ascx.cs-arc  $
//
//   Rev 1.3   Oct 22 2008 16:01:02   jfrank
//Updated for XHTML compliance
//Resolution for 5146: WAI AAA copmpliance work (CCN 474)
//
//   Rev 1.2   Mar 31 2008 13:20:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:18   mturner
//Initial revision.
//
//   Rev 1.16   Jun 06 2007 15:59:54   asinclair
//Added returnCoachJourneyNewFares
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.15   Jun 06 2007 12:38:46   asinclair
//Added singleCoachJourneyNewFares
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.14   May 25 2007 16:22:18   build
//Automatically merged from branch for stream4401
//
//   Rev 1.13.1.0   May 22 2007 13:16:52   mmodi
//Added NewAndOldCoachFares flag for new NX fares
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.13   Apr 26 2007 15:32:12   asinclair
//Updated to check returnfaresincluded when setting legdetails text
//Resolution for 4395: 9.5 - Improved Rail / Local Zonal - Cosmetic Issues
//
//   Rev 1.12   Apr 23 2007 14:52:02   asinclair
//Added returnfaresincluded
//
//   Rev 1.11   Mar 21 2007 15:12:26   dsawe
//modified to add for printer friendly
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.10   Mar 06 2007 13:43:54   build
//Automatically merged from branch for stream4358
//
//   Rev 1.9.1.0   Mar 02 2007 11:42:34   asinclair
//Added OtherFaresButtonClick event handler
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.9   Feb 23 2006 19:16:30   build
//Automatically merged from branch for stream3129
//
//   Rev 1.8.1.1   Jan 30 2006 14:41:04   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.8.1.0   Jan 10 2006 15:24:06   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.8   Nov 14 2005 18:07:14   mguney
//null check is done for ReturnFares and SingleFares in fareDetailsSegmentItemCreated method.
//Resolution for 3015: DN040: Null reference error on SBT when multi operator journeys selected
//
//   Rev 1.7   Apr 05 2005 14:06:12   rgeraghty
//fx cop changes, plus commenting
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.6   Mar 14 2005 15:42:06   rgeraghty
//Added event to handle upgrade info button click
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.5   Mar 03 2005 17:57:40   rgeraghty
//FxCop changes made
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.4   Mar 01 2005 15:02:36   rgeraghty
//First version
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.3   Feb 11 2005 14:58:08   rgeraghty
//Work in progress
//
//   Rev 1.2   Feb 09 2005 10:16:42   rgeraghty
//Work in progress
//
//   Rev 1.1   Feb 04 2005 12:30:54   rgeraghty
//Checked in pre-DFT changes
//
//   Rev 1.0   Jan 24 2005 18:23:04   rgeraghty
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	#region .NET namespaces
	using System;
	using System.Data;
	using System.Drawing;
	using System.Text;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;

	using TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.Common;
	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;	
	using TransportDirect.Common.ResourceManager;	
	#endregion

	/// <summary>
	///	Displays a section of a journey; either the legs contained in a single PricingUnit or a 
	//  single unpriced leg.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class FareDetailsDiagramSegmentControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{

		#region controls

		
		#endregion

		#region constants

		private const string nonbreakingSpace = "&nbsp;";
		private const string alignTop = "top";
		private const string alignMiddle = "middle";

		#endregion
		
        #region instance members

		private FareDetailsTableSegmentControl fareDetailsTableSegmentControl;
		private PricedJourneySegment pricedSegment;		

		private bool newAndOldCoachFares;
		private bool showChildFares;
		private bool singleCoachJourneyNewFares;
		private bool returnCoachJourneyNewFares;
		private string railDiscount = string.Empty;
		private string coachDiscount = string.Empty;

		private PublicJourneyDetail firstLeg;
		private ItineraryType overrideItineraryType;
		private Hashtable selectedTicketsHash;
		
		// Data for the Repeater.
		private IList journeyDetailList; 	

		#endregion

		
		/// <summary>
		/// Event raised when the user clicks an "Info" button.
		/// </summary>
		public event EventHandler InfoButtonClicked;

		/// <summary>
		/// Event raised when the user clicks the otherFaresLinkControl HyperbackPostbackControl
		/// on the FareDetailsTableSegmentControl
		/// </summary>
		public event EventHandler OtherFaresClicked;


		#region constructor
		/// <summary>
		/// constructor
		/// </summary>
		public FareDetailsDiagramSegmentControl()
		{
			selectedTicketsHash = new Hashtable();
		}

		#endregion

		#region Web holders and image definitions

		private string imageEndNodeUrl = String.Empty;
		private string imageStartNodeUrl = String.Empty;
		private string imageIntermediateNodeUrl = String.Empty;
		private string imageNodeConnectorUrl = String.Empty;

		private string imageCarUrl = String.Empty;
		private string imageAirUrl = String.Empty;
		private string imageBusUrl = String.Empty;
		private string imageCoachUrl = String.Empty;
		private string imageCycleUrl = String.Empty;
		private string imageDrtUrl = String.Empty;
		private string imageFerryUrl = String.Empty;
		private string imageMetroUrl = String.Empty;
		private string imageRailUrl = String.Empty;
		private string imageTaxiUrl = String.Empty;
		private string imageTramUrl = String.Empty;
		private string imageUndergroundUrl = String.Empty;
		private string imageWalkUrl = String.Empty;
		private string imageRailReplacementBusUrl = String.Empty;

		// Alternate Text for image strings
		private string alternateTextStartNode = String.Empty;
		private string alternateTextEndNode = String.Empty;
		private string alternateTextIntermediateNode = String.Empty;
		private string alternateTextNodeConnector = String.Empty;

		private string noFareInformation = String.Empty;
		private string checkinText = String.Empty;
		private string exitText = String.Empty;
		private string leaveText = String.Empty;
		private string arriveText = String.Empty;
		private string departText = String.Empty;        
		private string minsText = String.Empty;
		private string minText = String.Empty;
		private string maxDurationText = String.Empty;
		private string typicalDurationText = String.Empty;
		private string everyText = String.Empty;
		private string hoursText = String.Empty;
		private string hourText = String.Empty;
		private string secondsText = String.Empty;
		private string upperTakeText = String.Empty;
		private string lowerTakeText = String.Empty;
		private string towardsText = String.Empty;
		private string orText = String.Empty;
		private string walkToText = String.Empty;
		private string driveFromText = String.Empty;
		private string driveToText = String.Empty;
		private string alternateTextInformationButton = String.Empty;	
		private bool returnfaresincluded;

		#endregion

		#region private methods

		/// <summary>
		/// Set the strings of the control
		/// </summary>
		private void InitialiseControlText()
		{			
			#region resource strings

			noFareInformation = GetResource("JourneyFares.NoFaresInformationAvailable.Text");

			// Node and node connector image urls

			imageStartNodeUrl = GetResource("JourneyDetailsControl.imageStartNodeUrl");

			imageEndNodeUrl = GetResource("JourneyDetailsControl.imageEndNodeUrl");
    
			imageIntermediateNodeUrl = GetResource("JourneyDetailsControl.imageIntermediateNodeUrl");
            
			imageNodeConnectorUrl = GetResource("JourneyDetailsControl.imageNodeConnectorUrl");

			// Transport Mode Image Urls    
			imageCarUrl = GetResource("JourneyDetailsControl.imageCarUrl");

			imageAirUrl = GetResource("JourneyDetailsControl.imageAirUrl");

			imageBusUrl = GetResource("JourneyDetailsControl.imageBusUrl");

			imageCoachUrl = GetResource("JourneyDetailsControl.imageCoachUrl");

			imageCycleUrl = GetResource("JourneyDetailsControl.imageCycleUrl");

			imageDrtUrl = GetResource("JourneyDetailsControl.imageDrtUrl");
            
			imageFerryUrl = GetResource("JourneyDetailsControl.imageFerryUrl");
            
			imageMetroUrl =GetResource("JourneyDetailsControl.imageMetroUrl");
            
			imageRailUrl = GetResource("JourneyDetailsControl.imageRailUrl");
            
			imageTaxiUrl = GetResource("JourneyDetailsControl.imageTaxiUrl");
            
			imageTramUrl = GetResource("JourneyDetailsControl.imageTramUrl");
            
			imageUndergroundUrl = GetResource("JourneyDetailsControl.imageUndergroundUrl");
            
			imageWalkUrl = GetResource("JourneyDetailsControl.imageWalkUrl");

			imageRailReplacementBusUrl = GetResource("JourneyDetailsControl.imageRailReplacementBusUrl");
			
			// Alternate text strings

			alternateTextStartNode = GetResource("JourneyDetailsControl.alternateTextStartNode");

			alternateTextEndNode = GetResource("JourneyDetailsControl.alternateTextEndNode");
            
			alternateTextIntermediateNode = GetResource("JourneyDetailsControl.alternateTextIntermediateNode");
            
			alternateTextNodeConnector = GetResource("JourneyDetailsControl.alternateTextNodeConnector");

			alternateTextInformationButton = GetResource("JourneyDetailsControl.InformationButton.AlternateText");
			

			// Labels

			leaveText = GetResource("JourneyDetailsControl.Leave");

			arriveText = GetResource("JourneyDetailsControl.Arrive");

			departText = GetResource("JourneyDetailsControl.Depart");

			checkinText = GetResource("JourneyDetailsControl.Checkin");

			exitText = GetResource("JourneyDetailsControl.Exit");

			minsText = GetResource("JourneyDetailsTableControl.minutesString");
                
			minText = GetResource("JourneyDetailsTableControl.minuteString");
                
			everyText = GetResource("JourneyDetailsControl.every");

			maxDurationText = GetResource("JourneyDetailsControl.maxDuration");
                
			typicalDurationText = GetResource("JourneyDetailsControl.typicalDuration");

			secondsText = GetResource("JourneyDetailsTableControl.secondsString");

			hoursText = GetResource("JourneyDetailsTableControl.hoursString");

			hourText = GetResource("JourneyDetailsTableControl.hourString");

			upperTakeText = GetResource("JourneyDetailsControl.labelUpperTakeString");

			lowerTakeText = GetResource("JourneyDetailsControl.labelLowerTakeString");

			towardsText = GetResource("JourneyDetailsControl.labelTowardsString");

			orText = GetResource("JourneyDetailsControl.labelOrString");

			walkToText = GetResource("JourneyDetailsTableControl.WalkTo");

			driveFromText = GetResource("JourneyDetailsControl.DriveFrom");

			#endregion
		}

		/// <summary>
		/// Formats the given TDDateTime for display.
		/// </summary>
		/// <param name="time">TDDateTime to format.</param>
		/// <returns>Formatted time string for display in this control.</returns>
		private static string FormatTDDateTime(TDDateTime time)
		{
			// Round up if necessary for consistency.
			if(time.Second >= 30)
				time = time.AddMinutes(1);

			return time.ToString("HH:mm");
		}

		/// <summary>
		/// Occurs when the user clicks the otherFaresLinkControl HyperbackPostbackControl
		/// on the FareDetailsTableSegmentControl
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void fareDetailsTableSegmentControl_OtherFaresButtonClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = OtherFaresClicked;
			if (eventHandler != null)
				eventHandler(this, e);
		
		}

		#endregion

		#region Method to bind the data

		/// <summary>		
		/// sets the datasource of the repeater and binds.
		/// </summary>
		private void GetData()
		{
			journeyDetailList = pricedSegment.OutboundLegs;							

			if ((journeyDetailList != null) && (journeyDetailList.Count !=0 ))
			{				
				firstLeg = (PublicJourneyDetail) journeyDetailList[0];
				fareDetailsSegmentRepeater.DataSource = journeyDetailList;
				fareDetailsSegmentRepeater.DataBind();
			}
			
			DataBind(); 
		}

		#endregion

		#region Event Handlers
            
		/// <summary>
		/// Page Load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			InitialiseControlText();
			GetData();
		}

		/// <summary>
		/// PreRender event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{		
			GetData();

			//need to set the selected tickets for each diagram segment control
			foreach(RepeaterItem item in fareDetailsSegmentRepeater.Items)
			{				
				PlaceHolder placeholder = item.FindControl("faresTablePlaceholder") as PlaceHolder;
				 
				if (placeholder != null && placeholder.Controls.Count > 0)
				{
					fareDetailsTableSegmentControl = ( FareDetailsTableSegmentControl ) placeholder.Controls[0];

					if(fareDetailsTableSegmentControl!= null)
					{
						//set the selected ticket on the table segment control
						if (selectedTicketsHash.ContainsKey(fareDetailsTableSegmentControl.PriceUnit))
							fareDetailsTableSegmentControl.SelectedTicket = (Ticket) selectedTicketsHash[fareDetailsTableSegmentControl.PriceUnit];										
					}
				}
			}

		}

		/// <summary>
		/// Event Handler for when info button of table segment control is clicked
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void fareDetailsTableSegmentControl_InfoButtonClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = InfoButtonClicked;
			if (eventHandler != null)
				eventHandler(this, e);
		}

		/// <summary>
		/// Adds a fareDetailsTableSegment control to the placeholder
		/// for the first leg of a priced journey segment and leg detail controls
		/// for the remaining journey legs
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void fareDetailsSegmentItemCreated(object sender, RepeaterItemEventArgs e)
		{						
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				PublicJourneyDetail journeyDetail = (PublicJourneyDetail) e.Item.DataItem;
				
				//evaluate whether there will be any fare information to display
				int ticketCount = 0;

				if (pricedSegment.UnitIsPriced)
				{
					if ( overrideItineraryType.Equals( ItineraryType.Return ))
					{
						if ((pricedSegment.PricingUnit.ReturnFares != null) &&
							(pricedSegment.PricingUnit.ReturnFares.Tickets != null))
							ticketCount = pricedSegment.PricingUnit.ReturnFares.Tickets.Count;
					}
					else 
						if ((pricedSegment.PricingUnit.SingleFares != null) &&
							(pricedSegment.PricingUnit.SingleFares.Tickets != null))
						ticketCount = pricedSegment.PricingUnit.SingleFares.Tickets.Count;
				}
				//check if the segment is a priced unit and that it is the first non-walk leg being dealt with 
				//as this is the only time a table segment should be added
				if (pricedSegment.UnitIsPriced && journeyDetail.Equals(firstLeg))
				{					
					//find the placeholder 
					PlaceHolder placeholder = e.Item.FindControl("faresTablePlaceholder") as PlaceHolder;
					fareDetailsTableSegmentControl = ( FareDetailsTableSegmentControl )this.LoadControl( "FareDetailsTableSegmentControl.ascx" );										
					fareDetailsTableSegmentControl.ShowChildFares = this.ShowChildFares;
					fareDetailsTableSegmentControl.RailDiscount = this.RailDiscount;
					fareDetailsTableSegmentControl.CoachDiscount = this.CoachDiscount;					
					fareDetailsTableSegmentControl.PriceUnit = pricedSegment.PricingUnit;
					fareDetailsTableSegmentControl.ReturnFaresIncluded = this.ReturnFaresIncluded;
					fareDetailsTableSegmentControl.NewAndOldCoachFares = this.NewAndOldCoachFares;
					fareDetailsTableSegmentControl.SingleCoachJourneyNewFares = this.singleCoachJourneyNewFares;
					fareDetailsTableSegmentControl.ReturnCoachJourneyNewFares = this.ReturnCoachJourneyNewFares;
					fareDetailsTableSegmentControl.OverrideItineraryType = this.OverrideItineraryType;
					fareDetailsTableSegmentControl.PrinterFriendly = this.PrinterFriendly;
					fareDetailsTableSegmentControl.InfoButtonClicked +=new EventHandler(fareDetailsTableSegmentControl_InfoButtonClicked);
					placeholder.Controls.Add(fareDetailsTableSegmentControl);
					fareDetailsTableSegmentControl.OtherFaresClicked +=new EventHandler(fareDetailsTableSegmentControl_OtherFaresButtonClicked);

					//only display the leg details if there is more than one leg and there is fare information to display
					if ((journeyDetailList.Count > 1) && (ticketCount != 0))					
					{
						//set the legdetails
						LegDetailsControl legDetailsControl = e.Item.FindControl("legDetails") as LegDetailsControl;
						legDetailsControl.IsPriced = pricedSegment.UnitIsPriced;
						legDetailsControl.LegDetail = journeyDetail;
						legDetailsControl.PrinterFriendly = this.PrinterFriendly;
					}
				}				
				else
				{
					//label used as a separator for the two tables - only visible if displaying both
					//a detailssegmentcontrol and a leg details control
					Label labelBreak = e.Item.FindControl("labelBreak") as Label;
					labelBreak.Visible = false;

					//set the legdetails
					LegDetailsControl legDetailsControl = e.Item.FindControl("legDetails") as LegDetailsControl;
					legDetailsControl.IsPriced = pricedSegment.UnitIsPriced;
					legDetailsControl.LegDetail = journeyDetail;
					legDetailsControl.PrinterFriendly = this.PrinterFriendly;

					//update fares included text to no fare information available if there is no ticket information 
					//for the priced segment
					if (ticketCount ==0  && !returnfaresincluded)
						legDetailsControl.FaresAboveText = noFareInformation;
				}	
			}
		}
        
		#endregion

		#region Rendering Code


		/// <summary>
		/// Returns css class based on whether the segment contains a priced unit
		/// </summary>
		/// <returns></returns>
		public string GetCssClass(string edge)
		{			
			if (pricedSegment.UnitIsPriced)
				return "PricingUnit" + edge;
			else
				return string.Empty;

		}


		/// <summary>
		/// Returns css class based on whether the segment contains a priced unit
		/// </summary>
		/// <returns></returns>
		public string GetEightBoldCssClass( string edge)
		{			
			if (pricedSegment.UnitIsPriced)
				return "PricingUnitEightb" + edge;
			else
				return "txteightb";
		}

		/// <summary>
		/// Returns css class based on whether the segment contains a priced unit
		/// </summary>
		/// <returns></returns>
		public string GetNineBoldCssClass( string edge)
		{			
			if (pricedSegment.UnitIsPriced)
				return "PricingUnitNineb" + edge;
			else
				return "txtnineb";

		}

		
		/// <summary>
		/// Returns the url to the start node image if the current item being rendered
		/// is the first item otherwise returns the url to the intermediate node image.
		/// </summary>
		/// <param name="index">Index of current data item being rendered.</param>
		/// <returns>Url to the image.</returns>
		public string GetNodeImage(int index)
		{
			if( index == 0 && pricedSegment.IsFirst) 
				return imageStartNodeUrl;
			else 
				return imageIntermediateNodeUrl;
		}

		/// <summary>
		/// Returns valign property of the table cell containing the node image.
		/// "top" if Start Location, "middle" if not.
		/// </summary>
		/// <param name="index">Index of current data item being rendered.</param>
		/// <returns>string of valign property</returns>
		public string GetNodeImageVAlign(int index)
		{
			if( index == 0  && pricedSegment.IsFirst) 
				return alignTop;
			else 
				return alignMiddle;
		}

		/// <summary>
		/// Returns the alternative text for start node image if the current item
		/// being rendered is the first item otherwise returns the alternative
		/// text for intermediate node.
		/// </summary>
		/// <param name="index">Index of current item being rendered.</param>
		/// <returns>Alternate Text</returns>
		public string GetNodeImageAlternateText(int index)
		{
			if( index == 0 && pricedSegment.IsFirst) 
				return alternateTextStartNode;
			else 
				return alternateTextIntermediateNode;
		}

               
		/// <summary>
		/// Returns the formatted string representation of the depart time dependant
		/// on the type of leg being rendered
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Depart time string or space (&nbsp;)</returns>
		public string GetDepartDateTime(int index)
		{
			PublicJourneyDetail detail = (PublicJourneyDetail) journeyDetailList[index];
			
			//if there is only one leg in the priced segment and it is a walk
			//or a frequency leg then if the pricedSegment is the first in the journey
			//or a frequency leg return the formatted departure date time. 
			//If the priced segment is not the first of the journey
			//return space
			if ((index==0) && (journeyDetailList.Count==1) && (detail is PublicJourneyContinuousDetail || detail is PublicJourneyFrequencyDetail))
			{
				if (pricedSegment.IsFirst || (detail is PublicJourneyFrequencyDetail))
					return FormatTDDateTime(detail.LegStart.DepartureDateTime);
				else 
					return nonbreakingSpace;		
			}
			//otherwise for non-first legs of type walk or frequency return a space
			else if (index != 0 && (detail is PublicJourneyFrequencyDetail || detail is PublicJourneyContinuousDetail)) 
				return nonbreakingSpace;

			//otherwise for flight legs return formatted Flight Departure Date and Time
			else if (detail.Mode == ModeType.Air)
				return FormatTDDateTime(detail.FlightDepartDateTime);

			//otherwise return formatted the departure date and time
			else 
				return FormatTDDateTime(detail.LegStart.DepartureDateTime);

		}

		/// <summary>
		/// Returns the arrival time of the previous leg.
		/// A space is returned if the leg previous to the one specified
		/// is either a frequency leg or a walking leg
		/// </summary>
		/// <param name="index">Index of current leg being rendered.</param>
		/// <returns>Arrival time of previous leg or space (&nbsp;)</returns>
		public string GetPreviousArrivalDateTime(int index)
		{
			//check if first leg is a flight check in leg
			PublicJourneyDetail detail = (PublicJourneyDetail) journeyDetailList[index];
			
			if (index == 0) 
			{
				if (detail.Mode == ModeType.Air && detail.CheckInTime != null)
					return FormatTDDateTime(detail.CheckInTime);
				else
					return String.Empty;
			}			
            			
			detail = (PublicJourneyDetail) journeyDetailList[index - 1];
			
			if (detail is PublicJourneyFrequencyDetail || detail is PublicJourneyContinuousDetail)
				return nonbreakingSpace;
			else 
				return FormatTDDateTime(detail.LegEnd.ArrivalDateTime);			
		}
        
		/// <summary>
		/// Returns the instruction text "Arrive", or "Check-in" for Air if the leg previous to the
		/// one specified is neither a frequency leg or walking leg, otherwise
		/// a space is returned.
		/// </summary>
		/// <param name="index">Index of current leg being rendered.</param>
		/// <returns>Instruction text or space (&nbsp;)</returns>
		public string GetPreviousInstruction(int index) 
		{

			//if first journey detail is air then a 'Check-in' label should be displayed			
			PublicJourneyDetail detail = (PublicJourneyDetail) journeyDetailList[index];
						
			if (index == 0 && detail.Mode == ModeType.Air && detail.CheckInTime != null)
				return checkinText;
			else if ((index == 0) && (detail.Mode != ModeType.Air || detail.CheckInTime == null))
				return nonbreakingSpace;
			else
			{            
				detail = (PublicJourneyDetail) journeyDetailList[index-1];
				
				if (detail is PublicJourneyFrequencyDetail || detail is PublicJourneyContinuousDetail) 
					return nonbreakingSpace;	
				else 
					return arriveText;
			}            
		}

		/// <summary>
		/// Returns the instruction text "Leave" or "Depart" dependant on 
		/// the leg detail being rendered
		/// </summary>
		/// <param name="index">Index of current leg being rendered.</param>
		/// <returns>Instruction text or space (&nbsp;)</returns>
		public string GetCurrentInstruction(int index) 
		{			
			
			PublicJourneyDetail detail = (PublicJourneyDetail) journeyDetailList[index];						  			
			
			//if there is only one leg in the priced segment and it is a walk
			//or a frequency leg then if the pricedSegment is the first in the journey
			//return "Leave". If the priced segment is not the first of the journey
			//return space for a walk leg, or "Leave" for a frequency leg
			if ((index==0) && (journeyDetailList.Count==1) && (detail is PublicJourneyContinuousDetail || detail is PublicJourneyFrequencyDetail))
			{
				if (pricedSegment.IsFirst)
					return leaveText;
				else if (detail is PublicJourneyContinuousDetail)
					return nonbreakingSpace;
				else
					return leaveText;
			}
			// otherwise for the first non-flight leg return "Leave"
			else if ((index == 0) && (detail.Mode != ModeType.Air))			  
				return leaveText;
						  
			//otherwise return space for walk and frequency legs
			else if (detail is PublicJourneyFrequencyDetail || detail is PublicJourneyContinuousDetail) 
				return nonbreakingSpace;
			//otherwise return "Depart"
			else 
				return departText;
			
		}
		

		/// <summary>
		/// Returns the text for the transport mode of the specified leg.
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Mode string formatted.</returns>
		public string GetMode(PublicJourneyDetail publicJourneyDetail)
		{
			string resourceManagerKey = "TransportMode." +
				publicJourneyDetail.Mode.ToString();
            
			return GetResource(resourceManagerKey);

		}

		/// <summary>
		/// Returns the Duration string.
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted duration string.</returns>
		public string GetDuration( PublicJourneyDetail detail )
		{
			if (detail is PublicJourneyFrequencyDetail)
				return String.Empty;
			else 
			{
                
				// Get the duration of the current leg (rounded to the nearest minute)
				long durationInSeconds = detail.Duration; // in seconds

				// Get the minutes
				double durationInMinutes = durationInSeconds / 60;

				// Check to see if seconds is less than 30 seconds.
				if( durationInSeconds < 31)
					return "< 30 " + secondsText;
				else
				{
					// Round to the nearest minute
					durationInMinutes = Round(durationInMinutes);

					// Calculate the number of hours in the minute
					int hours = (int)durationInMinutes / 60;

					// Get the minutes (afer the hours has been subracted so always < 60)
					int minutes = (int)durationInMinutes % 60;

					// If greater than 1 hour - retrieve "hours", if 1 or less, retrieve "hour"
					string hourString = hours > 1 ?
					hoursText : hourText;

					// If greater than 1 minute - retrive "minutes", if 1 or less, retrieve "minute"
					string minuteString = minutes > 1 ? minsText : minText;
            
					string formattedString = string.Empty;

					if(hours > 0)
						formattedString += hours + " " + hourString + " ";

					formattedString += minutes + " " + minuteString;

					return formattedString;
				}
			}
		}

		/// <summary>
		/// Returns a formatted string containing mode type, service details
		/// (if present) and duration (if leg is not a frequency leg)
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing mode, service and duration details</returns>
		public string FormatModeDetails(PublicJourneyDetail detail) 
		{
			StringBuilder output = new StringBuilder();
			output.Append("<span class=\"txteight\">"+GetMode(detail)+"</span>");

			string services = GetServices(detail);
			if (services.Length > 0) 
				output.Append("<span class=\"txtnineb\">&nbsp;"+GetServices(detail)+"</span>");
			
			if (detail is PublicJourneyFrequencyDetail) 
			{
				output.Append("<br /><span class=\"txteight\">"+GetMaxJourneyDurationText(detail)+"<br />");
				output.Append(GetTypicalDurationText(detail)+"</span>");
			} 
			else 
				output.Append("<br /><span class=\"txteight\">"+GetDuration(detail)+"</span>");

			return output.ToString();
		}

		/// <summary>
		/// Returns formatted string containing the maximum duration of
		/// a supplied frequency leg, or empty string if supplied leg is
		/// not a frequency leg.
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing the maximum duration or empty string</returns>
		public string GetMaxJourneyDurationText( PublicJourneyDetail detail ) 
		{

			if (detail is PublicJourneyFrequencyDetail) 
			{
				PublicJourneyFrequencyDetail frequencyDetail =
					(PublicJourneyFrequencyDetail)detail;

				return maxDurationText + ": " + frequencyDetail.MaxDuration +
					(frequencyDetail.MaxDuration > 1 ? minsText : minText);
			} 
			else 
				return string.Empty;
		}

		/// <summary>
		/// Returns formatted string containing the typical duration of
		/// a supplied frequency leg, or empty string if supplied leg is
		/// not a frequency leg.
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing the typical duration or empty string</returns>        
		public string GetTypicalDurationText( PublicJourneyDetail detail ) 
		{

			if (detail is PublicJourneyFrequencyDetail) 
			{
				PublicJourneyFrequencyDetail frequencyDetail =
					(PublicJourneyFrequencyDetail)detail;

				return typicalDurationText + ": " + frequencyDetail.Duration +
					(frequencyDetail.Duration > 1 ? minsText : minText);
			} 
			else 
				return string.Empty;
		}

		/// <summary>
		/// If the supplied leg is not a rail leg and services details are
		/// present, returns a string containing every service number
		/// delimited by commas. Otherwise an empty string is returned. 
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing service numbers or empty string</returns>
		private static string GetServices(PublicJourneyDetail detail)
		{
			if (detail.Mode != ModeType.Rail &&
                detail.Mode != ModeType.RailReplacementBus &&
                detail.Services != null && 
                detail.Services.Length > 0)
			{
				StringBuilder serviceDetailsText = new StringBuilder();
				for (int count=0; count < detail.Services.Length; count++) 
				{
					serviceDetailsText.Append(detail.Services[count].ServiceNumber);
					if (count < detail.Services.Length -1)
						serviceDetailsText.Append(",");
				}
				return serviceDetailsText.ToString();         
			} 
			else 
				return String.Empty;
		}

		/// <summary>
		/// Returns the css class name "bgline" if the index value 
		/// supplied is non-zero, otherwise returns an empty string
		/// </summary>
		/// <param name="index">Current item being rendered</param>
		/// <returns>css class name "bgline" or empty string</returns>
		public static string GetBackgroundLineClass(int index) 
		{
			if (index == 0) 
				return string.Empty;
			else 
				return "bgline";
		}


				
		/// <summary>
		/// Returns the image url for the transport mode of the specified
		/// leg.
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Image Url</returns>
		public string GetModeImagePath(PublicJourneyDetail detail)
		{
			switch(detail.Mode)
			{
				case ModeType.Car :
					return imageCarUrl;

				case ModeType.Air :
					return imageAirUrl;

				case ModeType.Bus :
					return imageBusUrl;

				case ModeType.Coach :
					return imageCoachUrl;

				case ModeType.Cycle :
					return imageCycleUrl;

				case ModeType.Drt :
					return imageDrtUrl;

				case ModeType.Ferry :
					return imageFerryUrl;

				case ModeType.Metro :
					return imageMetroUrl;

				case ModeType.Rail :
					return imageRailUrl;

				case ModeType.Taxi :
					return imageTaxiUrl;

				case ModeType.Tram :
					return imageTramUrl;

				case ModeType.Underground :
					return imageUndergroundUrl;

				case ModeType.Walk :
					return imageWalkUrl;

				case ModeType.RailReplacementBus :
					return imageRailReplacementBusUrl;
			}

			return String.Empty;
		}

		/// <summary>
		/// Gets the alternate text for the transport mode image for the
		/// given leg
		/// </summary>
		/// <param name="detail">Current leg being rendered.</param>
		/// <returns>Alternate text</returns>
		public string GetModeImageAlternateText(PublicJourneyDetail detail)
		{
			switch(detail.Mode)
			{
				case ModeType.Air :
					return GetResource("TransportMode.Air.ImageAlternateText");

				case ModeType.Bus :
					return GetResource("TransportMode.Bus.ImageAlternateText");

				case ModeType.Coach :
					return GetResource("TransportMode.Coach.ImageAlternateText");

				case ModeType.Cycle :
					return GetResource("TransportMode.Cycle.ImageAlternateText");

				case ModeType.Drt :
					return GetResource("TransportMode.Drt.ImageAlternateText");

				case ModeType.Ferry :
					return GetResource("TransportMode.Ferry.ImageAlternateText");

				case ModeType.Metro :
					return GetResource("TransportMode.Metro.ImageAlternateText");

				case ModeType.Rail :
					return GetResource("TransportMode.Rail.ImageAlternateText");

				case ModeType.Taxi :
					return GetResource("TransportMode.Taxi.ImageAlternateText");

				case ModeType.Tram :
					return GetResource("TransportMode.Tram.ImageAlternateText");

				case ModeType.Underground :
					return GetResource("TransportMode.Underground.ImageAlternateText");

				case ModeType.Walk :
					return GetResource("TransportMode.Walk.ImageAlternateText");

				case ModeType.RailReplacementBus :
					return GetResource("TransportMode.RailReplacementBus.ImageAlternateText");
			}

			return String.Empty;
		}


		#endregion

		#region public properties

		/// <summary>
		/// Read only property. - Returns the end time for the journey.
		/// </summary>
		public string FooterEndDateTime
		{
			get 
			{
				if (journeyDetailList == null)
					return nonbreakingSpace;

				PublicJourneyDetail detail = (PublicJourneyDetail) journeyDetailList[journeyDetailList.Count-1];

				//When the pricedSegment contains only one leg, if the leg is a walk or frequency leg
				//then if the pricedSegment is the last in the journey return the formatted arrival time,
				//otherwise return a space
				if (journeyDetailList.Count==1) 
				{					
					if (detail is PublicJourneyContinuousDetail || detail is PublicJourneyFrequencyDetail)
					{
						if (pricedSegment.IsLast)
							return FormatTDDateTime(detail.LegEnd.ArrivalDateTime);
						else
							return nonbreakingSpace;
					}


				}
				
				if (detail.Mode == ModeType.Air)		
					return FormatTDDateTime(detail.FlightArriveDateTime);
				else
					return FormatTDDateTime(detail.LegEnd.ArrivalDateTime);

			}
		}

		/// <summary>
		/// Read only property. Returns the instruction text "exit" for a flight leg, otherwise
		/// a space is returned.
		/// </summary>
		public string FooterExitText
		{
			get 
			{
				if (journeyDetailList == null)
					return nonbreakingSpace;

				PublicJourneyDetail detail = (PublicJourneyDetail) journeyDetailList[journeyDetailList.Count-1];

				if (detail.Mode == ModeType.Air && detail.ExitTime != null)
					return exitText;
				else
					return nonbreakingSpace;
			}
		}

		/// <summary>
		/// Read only property.  Returns the formatted string representation of the exit time.
		/// A space is returned if the specified leg is not a flight leg
		/// </summary>
		public string FooterExitDateTime
		{

			get 
			{
				if (journeyDetailList == null)
					return nonbreakingSpace;

				PublicJourneyDetail detail = (PublicJourneyDetail) journeyDetailList[journeyDetailList.Count-1];

				if (detail.Mode == ModeType.Air	&& detail.ExitTime != null)
					return FormatTDDateTime(detail.ExitTime);
				else
					return nonbreakingSpace;
			}
		}

       
		/// <summary>
		/// Read only property FooterEndInstruction. Internationalised text for "Arrive"
		/// </summary>
		public string FooterEndInstruction
		{
			get
			{
				//When the pricedSegment contains only one leg, if the leg is a walk or frequency leg
				// then if the pricedSegment is the last in the journey return "Arrive", otherwise return 
				//a space
				if (journeyDetailList.Count==1) 
				{
					PublicJourneyDetail detail = (PublicJourneyDetail) journeyDetailList[0];

					if (detail is PublicJourneyContinuousDetail || detail is PublicJourneyFrequencyDetail)
					{
						if (pricedSegment.IsLast)
							return arriveText;
						else
							return nonbreakingSpace;
					}
				}			
				//otherwise return "Arrive"
				return arriveText;
			}
		}

		/// <summary>
		/// Read only property - returns the end node image url.
		/// </summary>
		public string EndNodeImage
		{
			get
			{
				if(pricedSegment.IsLast) 
					return imageEndNodeUrl;
				else 
					return imageIntermediateNodeUrl;
			}
		}

		/// <summary>
		/// Read only property - returns the alternate text for the end node image.
		/// </summary>
		public string EndNodeImageAlternateText
		{
			get
			{
				if(pricedSegment.IsLast) 
					return alternateTextEndNode;
				else 
					return alternateTextIntermediateNode;
			}
		}
		
		/// <summary>
		/// Contains the selected ticket for a given pricing unit. 
		/// </summary>		
		public Hashtable SelectedTickets
		{
			get {return selectedTicketsHash;}
			set {selectedTicketsHash = value;}
		}


		/// <summary>
		/// Indicates whether control is to display rail discount fares
		/// </summary>
		public string RailDiscount
		{
			get {return railDiscount;}
			set {railDiscount=value;}
		}

		/// <summary>
		/// Indicates whether control is to display coach discount fares
		/// </summary>
		public string CoachDiscount
		{
			get {return coachDiscount;}
			set {coachDiscount=value;}
		}


		/// <summary>
		/// Gets/Sets the itinerarytype to ensure single/return fares are
		/// displayed
		/// </summary>
		public ItineraryType OverrideItineraryType
		{
			get {return overrideItineraryType;}
			set {overrideItineraryType = value;}
		}

		/// <summary>
		/// Indicates whether adult or child fares are visible 
		/// </summary>
		public bool ShowChildFares
		{
			get {return showChildFares;}
			set {showChildFares=value;}
		}

		/// <summary>
		/// The PricedJourneySegment object to be displayed
		/// </summary>
		public PricedJourneySegment PricedSegment
		{
			get {return pricedSegment;}
			set {pricedSegment = value;}			
		}

		/// <summary>
		/// Returns the FareDetailsTableSegmentControl for the segment
		/// </summary>
		public FareDetailsTableSegmentControl FaresTable
		{
			get
			{ 
				return fareDetailsTableSegmentControl; // will be null if the segment is for an unpriced leg
			}
		}

		public bool ReturnFaresIncluded
		{
			get {return returnfaresincluded;}
			set {returnfaresincluded = value;}
		}
		
		/// <summary>
		/// Read/Write property. Set to indicate that the outward leg of coach journey has
		/// been planned returning new fares, but that the return leg returns old fares, or that 
		/// this is a Single journey.
		/// In these cases we don't want to display return fares for this journey.
		/// </summary>
		public bool SingleCoachJourneyNewFares
		{
			get {return singleCoachJourneyNewFares;}
			set {singleCoachJourneyNewFares = value;}
		}

		/// <summary>
		/// Read/Write property. Set to indicate that the return leg of single coach journey has
		/// been planned returning new fares, but that the outward leg returns old fares.
		/// Therefore, we don't want to display return fares for this journey.
		/// </summary>
		public bool ReturnCoachJourneyNewFares
		{
			get {return returnCoachJourneyNewFares;}
			set {returnCoachJourneyNewFares = value;}
		}

		/// <summary>
		/// Read/Write property. Set to indicate the Outward and Return journeys have a mixed set of 
		/// new and old coach fares.
		/// </summary>
		public bool NewAndOldCoachFares
		{
			get {return newAndOldCoachFares;}
			set {newAndOldCoachFares = value;}
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.fareDetailsSegmentRepeater.ItemCreated += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.fareDetailsSegmentItemCreated);			
			this.PreRender += new System.EventHandler(this.Page_PreRender);
			base.OnInit(e);
		}
        
		///     Required method for Designer support - do not modify
		///     the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region Rounding method

		/// <summary>
		/// Rounds the given double to the nearest int.
		/// If double is 0.5, then rounds up.
		/// Using this instead of Math.Round because Math.Round
		/// ALWAYS returns the even number when rounding a .5 -
		/// this is not behaviour we want.
		/// </summary>
		/// <param name="valueToRound">Value to round.</param>
		/// <returns>Nearest integer</returns>
		private static int Round(double valueToRound)
		{
			// Get the decimal point
			double valueFloored = Math.Floor(valueToRound);
			double remain = valueToRound - valueFloored;

			if(remain >= 0.5)
				return (int)Math.Ceiling(valueToRound);
			else
				return (int)Math.Floor(valueToRound);
		}

		#endregion


	}
}
