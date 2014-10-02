// *********************************************** 
// NAME                 : JourneyAdjustSegmentsControl.ascx.cs 
// AUTHOR               : Paul Cross
// DATE CREATED         : 06/02/2006
// DESCRIPTION          : A user control to display details of a given journey that can be adjusted
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyAdjustSegmentControl.ascx.cs-arc  $ 
//
//   Rev 1.5   Jan 20 2013 16:26:50   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.4   Jan 13 2009 11:40:44   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Oct 13 2008 16:41:40   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Sep 26 2008 13:42:12   jfrank
//Amended to make XHTML transitional
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:21:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:06   mturner
//Initial revision.
//
//   Rev 1.10   Mar 20 2006 20:11:04   pcross
//Typo
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.9   Mar 20 2006 18:06:00   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.8   Mar 17 2006 12:38:12   pcross
//ArriveBefore indicator no longer needed on JourneyAdjustSegmentControl
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Mar 17 2006 11:40:40   pcross
//Changed highlighting for interchanges
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Mar 14 2006 13:20:32   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Mar 08 2006 20:29:16   pcross
//FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Mar 08 2006 16:02:54   RGriffith
//FxCop Suggested Changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Mar 02 2006 14:59:22   pcross
//Added 'please select from dropdown' type entry for location and timings dropdowns
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 20 2006 12:10:44   pcross
//Some tidying
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 16 2006 10:23:48   pcross
//Interim check in
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 10 2006 10:41:42   pcross
//Initial revision.

#region Using Statements

using System;
using System.Data;
using System.Text;
using System.Globalization;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.ResourceManager;

#endregion

namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	/// A user control to display details of a given journey that can be adjusted
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class JourneyAdjustSegmentControl : TDUserControl
	{
		protected Label modeLinkLabel;
		protected Label locationInfoLinkLabel;
		protected Label locationLabelControl;
		protected Label alightLocationInfoLinkLabel;
		protected Label alightLocationLabelControl;
		protected Label endLocationInfoLinkLabel;
		protected Label endLocationLabelControl;
		protected HtmlGenericControl interchangeDiv;

		// Data for the Repeater.
		private JourneyControl.PublicJourney publicJourney;

		private bool firstJourney;	
		private bool lastJourney;
		
		/// <summary>
		/// True if the leg being rendered is at an interchange
		/// </summary>
		private bool atInterchange;
		
		/// <summary>
		/// Notes when the cell following an interchange needs to be given an interchange ID
		/// </summary>
		private bool nextBottomCellIsInterchange;			// for GetHighlightCellID
		private bool nextBottomCellIsInterchange_SetClass;	// for GetHighlightCellClass

		/// <summary>
		/// To help assign the right index values for the highlight cell IDs (as they are ordered out weirdly on the page)
		/// </summary>
		private int lastIndexId = -1;			// for GetHighlightCellID
		private int lastIndexId_SetClass = -1;	// for GetHighlightCellClass

		/// <summary>
		/// True if the location selected in the locations dropdown on the JourneyAdjust page covers an interchange
		/// </summary>
		private bool selectedLocationIsInterchange;

		// For seeing if there's information available for a particular location
		private const string originNaptanString = "Origin";
		private const string destinationNaptanString = "Destination";
		
		// Constants associated with highlighting
		private const string highlightedClass = "bglinedotted";
		private const string highlightedBottomClass = "bglinedottedbottom";
		private const string nonHighlightedBottomClass = "departline";
		private const string highlightCellIdPrefix = "highlightCell";
		private const string InterchangeIndicator = "_interchange";
		private const string UNREFINED_TEXT = "unrefined";


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
        private string imageTelecabineUrl = String.Empty;
        private string imageTramUrl = String.Empty;
		private string imageUndergroundUrl = String.Empty;
		private string imageWalkUrl = String.Empty;
		private string imageRailReplacementBusUrl = String.Empty;

		private string imageSpacerUrl = String.Empty;

		// Alternate Text for images and links
		private string alternateTextStartNode = String.Empty;
		private string alternateTextEndNode = String.Empty;
		private string alternateTextIntermediateNode = String.Empty;
		private string alternateTextNodeConnector = String.Empty;
		private string toolTipTextLocationLink = String.Empty;
		private string toolTipTextDetailsLink = String.Empty;
		private string toolTipTextInformationButton = String.Empty;

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
		private string startText = String.Empty;
		private string endText = String.Empty;
		private string hoursText = String.Empty;
		private string hourText = String.Empty;
		private string secondsText = String.Empty;
		private string driveToText = String.Empty;


		#endregion

		#region Initialisation and initialisation properties
		/// <summary>
		/// Initialises the control. This method must be called.
		/// </summary>
		/// <param name="publicJourney">Journey to render journey for.</param>
		public void Initialise(
			JourneyControl.PublicJourney publicJourney, bool firstJourney, bool lastJourney)
		{
			// All stored in internal viewstate so that initialise is called only once
			this.publicJourney = publicJourney;
			this.firstJourney = firstJourney;
			this.lastJourney = lastJourney;

			selectedLocationIsInterchange = SelectedLocationIsInterchange();

		}

		private PageId belongingPageId = PageId.Empty;
		/// <summary>
		/// Get/Set property - get or sets the page Id. This should be the page Id
		/// of the page that contains this control.
		/// </summary>
		public PageId MyPageId
		{
			get
			{
				return belongingPageId;
			}

			set
			{
				belongingPageId = value;
			}
		}

		#endregion

		#region Method to update the data

		/// <summary>
		/// Checks TDSessionManager to find the data that should be rendered,
		/// sets it as the datasource to the repeater and binds.
		/// </summary>
		private void UpdateData()
		{
			if( publicJourney == null )
			{
				journeySegmentsRepeater.DataSource = new Object[0];
				journeySegmentsRepeater.DataBind();
				return;
			}
			journeySegmentsRepeater.DataSource = publicJourney.Details;
			journeySegmentsRepeater.DataBind();
		}

		#endregion

		#region OnLoad / Page Load / OnPreRender Methods
            
		/// <summary>
		/// Sets-up event handlers for all the buttons
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			// Need to call UpdateData to populate the Repeater. This isn't required
			// when inheriting from Web.UI.Page (and not TDPage).
			UpdateData();
			base.OnLoad(e);
		}

		/// <summary>
		/// Page Load.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Add javascript to the client side
			lastIndexId = -1;

			#region Image translation and updating

			// Node and node connector image urls
			imageStartNodeUrl = GetResource("langStrings", "JourneyDetailsControl.imageStartNodeUrl");
			imageEndNodeUrl = GetResource("langStrings", "JourneyDetailsControl.imageEndNodeUrl");
			imageIntermediateNodeUrl = GetResource("langStrings", "JourneyDetailsControl.imageIntermediateNodeUrl");
			imageNodeConnectorUrl = GetResource("langStrings", "JourneyDetailsControl.imageNodeConnectorUrl");

			// Transport Mode Image Urls    
			imageCarUrl = GetResource("langStrings", "JourneyDetailsControl.imageCarUrl");
			imageAirUrl = GetResource("langStrings", "JourneyDetailsControl.imageAirUrl");
			imageBusUrl = GetResource("langStrings", "JourneyDetailsControl.imageBusUrl");
			imageCoachUrl = GetResource("langStrings", "JourneyDetailsControl.imageCoachUrl");
			imageCycleUrl = GetResource("langStrings", "JourneyDetailsControl.imageCycleUrl");
			imageDrtUrl = GetResource("langStrings", "JourneyDetailsControl.imageDrtUrl");
			imageFerryUrl = GetResource("langStrings", "JourneyDetailsControl.imageFerryUrl");
			imageMetroUrl = GetResource("langStrings", "JourneyDetailsControl.imageMetroUrl");
			imageRailUrl = GetResource("langStrings", "JourneyDetailsControl.imageRailUrl");
			imageTaxiUrl = GetResource("langStrings", "JourneyDetailsControl.imageTaxiUrl");
            imageTelecabineUrl = GetResource("langStrings", "JourneyDetailsControl.imageTelecabineUrl");
            imageTramUrl = GetResource("langStrings", "JourneyDetailsControl.imageTramUrl");
			imageUndergroundUrl = GetResource("langStrings", "JourneyDetailsControl.imageUndergroundUrl");
			imageWalkUrl = GetResource("langStrings", "JourneyDetailsControl.imageWalkUrl");
			imageRailReplacementBusUrl = GetResource("langStrings", "JourneyDetailsControl.imageRailReplacementBusUrl");
			imageSpacerUrl = GetResource("langStrings", "MapZoomControl.spacer.ImageUrl");

			// Alternate text strings
			alternateTextStartNode = GetResource("langStrings", "JourneyDetailsControl.alternateTextStartNode");
			alternateTextEndNode = GetResource("langStrings", "JourneyDetailsControl.alternateTextEndNode");
			alternateTextIntermediateNode = GetResource("langStrings", "JourneyDetailsControl.alternateTextIntermediateNode");
			alternateTextNodeConnector = GetResource("langStrings", "JourneyDetailsControl.alternateTextNodeConnector");
			toolTipTextInformationButton = GetResource("langStrings", "JourneyDetailsControl.InformationButton.ToolTipText");
			toolTipTextLocationLink = GetResource("langStrings", "JourneyDetailsControl.LocationLink.ToolTipText");
			toolTipTextDetailsLink = GetResource("langStrings", "JourneyDetailsControl.ServiceDetailsLink.ToolTipText");

			// Labels
			leaveText = GetResource("langStrings", "JourneyDetailsControl.Leave");
			arriveText = GetResource("langStrings", "JourneyDetailsControl.Arrive");
			departText = GetResource("langStrings", "JourneyDetailsControl.Depart");
			checkinText = GetResource("langStrings", "JourneyDetailsControl.Checkin");
			exitText = GetResource("langStrings", "JourneyDetailsControl.Exit");
			minsText = GetResource("langStrings", "JourneyDetailsTableControl.minutesString");
			minText = GetResource("langStrings", "JourneyDetailsTableControl.minuteString");
			everyText = GetResource("langStrings", "JourneyDetailsControl.every");
			maxDurationText = GetResource("langStrings", "JourneyDetailsControl.maxDuration");
			typicalDurationText = GetResource("langStrings", "JourneyDetailsControl.typicalDuration");
			startText = GetResource("langStrings", "JourneyDetailsControl.StartText");
			endText = GetResource("langStrings", "JourneyDetailsControl.EndText");
			secondsText = GetResource("langStrings", "JourneyDetailsTableControl.secondsString");
			hoursText = GetResource("langStrings", "JourneyDetailsTableControl.hoursString");
			hourText = GetResource("langStrings", "JourneyDetailsTableControl.hourString");

			#endregion			

		}
		
		#region Event support

		/// <summary>
		/// Set up user controls in the repeater
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void JourneySegmentsRepeaterItemCreated(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item :
				case ListItemType.AlternatingItem :
					
					// Find the checkbox and bind its checkedchanged event
					locationInfoLinkLabel = (Label)e.Item.FindControl("locationInfoLinkLabel");
					locationLabelControl = (Label)e.Item.FindControl("locationLabelControl");

					if	(InfoAvailable((PublicJourneyDetail)e.Item.DataItem))
					{
						locationInfoLinkLabel.Text = GetStartLocation((PublicJourneyDetail)e.Item.DataItem);
						locationInfoLinkLabel.Visible = true;
						locationLabelControl.Visible = false;
					}
					else
					{
						locationLabelControl.Text = GetStartLocation((PublicJourneyDetail)e.Item.DataItem);
						locationLabelControl.Visible = true;
						locationInfoLinkLabel.Visible = false;
					}


					ResultsAdapter resultsAdapter = new ResultsAdapter();
					atInterchange = resultsAdapter.AtInterchange(publicJourney, IndexOf((PublicJourneyDetail)e.Item.DataItem));

					alightLocationInfoLinkLabel = (Label)e.Item.FindControl("alightLocationInfoLinkLabel");
					alightLocationLabelControl = (Label)e.Item.FindControl("alightLocationLabelControl");

					if	(atInterchange)
					{

						if	(InfoAvailable((PublicJourneyDetail)e.Item.DataItem))
						{
							alightLocationInfoLinkLabel.Text = GetPreviousEndLocation(IndexOf((PublicJourneyDetail)e.Item.DataItem));
							alightLocationInfoLinkLabel.Visible = true;
							alightLocationLabelControl.Visible = false;
						}
						else
						{
							alightLocationLabelControl.Text = GetPreviousEndLocation(IndexOf((PublicJourneyDetail)e.Item.DataItem));
							alightLocationLabelControl.Visible = true;
							alightLocationInfoLinkLabel.Visible = false;
						}
					}
					else
					{
						alightLocationInfoLinkLabel.Visible = false;
						alightLocationLabelControl.Visible = false;
					}

					interchangeDiv = (HtmlGenericControl)e.Item.FindControl("interchangeDiv");
					interchangeDiv.Visible = atInterchange;

					modeLinkLabel = (Label)e.Item.FindControl("modeLinkLabel");

					if	(HasServiceDetails((PublicJourneyDetail)e.Item.DataItem))
					{
						modeLinkLabel.Text = GetMode((PublicJourneyDetail)e.Item.DataItem, false);
						modeLinkLabel.Visible = true;
					}
					else
					{
						modeLinkLabel.Visible = false;
					}

					break;

				case ListItemType.Footer :

					endLocationInfoLinkLabel = (Label)e.Item.FindControl("endLocationInfoLinkLabel");
					endLocationLabelControl = (Label)e.Item.FindControl("endLocationLabelControl");

					if	(InfoAvailableEndLocation())
					{
						endLocationInfoLinkLabel.Text = FooterEndLocation;
						endLocationInfoLinkLabel.Visible = true;
						endLocationLabelControl.Visible = false;
					}
					else
					{
						endLocationLabelControl.Text = FooterEndLocation;
						endLocationLabelControl.Visible = true;
						endLocationInfoLinkLabel.Visible = false;
					}

					break;
				
				default :
					break;
			}

		}

		#endregion

		/// <summary>
		/// Fetches the data to render from TDSessionManager (if any) and binds the data
		/// to the repeaters. The fetching of data is done at this stage
		/// (as opposed to Page Load) because OnPreRender will be executed after
		/// any event handlers and therefore when OnPreRender executes, the latest
		/// data will exist in TDSessionManager.
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			// UpdateData needs to be called again because a Button Event Handler
			// may have updated the data. (Can't get away with just having UpdateData
			// here (and not in OnLoad) because during OnLoad, the Repeater is empty
			// for some reason (this behaviour does not occur if the page that this
			// control is in is inherited from Web.UI.Page), therefore, the UpdateData
			// is required to populate the Repeater so that the event handlers can
			// be correctly registered.

			UpdateData();	
			base.OnPreRender(e);
		}
        
		#endregion

		#region Rendering Code

		/// <summary>
		/// Get Property StartText.
		/// Internationalised text for "start"
		/// </summary>
		public string StartText
		{
			get 
			{
				return startText;
			}
		}

		/// <summary>
		/// Internationalised text for "start"
		/// </summary>
		public string StartTextVisible()
		{
			if (this.firstJourney)
				return string.Empty;
			else
				return "visibility: hidden";
		}

		/// <summary>
		/// Get property EndText.
		/// Internationalised text for "end"
		/// </summary>
		public string EndText
		{
			get
			{
				if (this.lastJourney)
					return endText;
				else
					return "&nbsp;";
			}
		}

		/// <summary>
		/// Public journey object to give information about what legs on this journey
		/// </summary>
		public JourneyControl.PublicJourney PublicJourney
		{
			get { return publicJourney; }
		}

		/// <summary>
		/// Returns the url to the start node image if the current item being rendered
		/// is the first item otherwise returns the url to the intermediate node image.
		/// </summary>
		/// <param name="index">Index of current data item being rendered.</param>
		/// <returns>Url to the image.</returns>
		public string GetNodeImage(int index)
		{
			if( index == 0 ) 
			{
				return imageStartNodeUrl;
			} 
			else 
			{
				return imageIntermediateNodeUrl;
			}
		}

		/// <summary>
		/// Returns valign property of the table cell containing the node image.
		/// "top" if Start Location, "middle" if not.
		/// </summary>
		/// <param name="index">Index of current data item being rendered.</param>
		/// <returns>string of valign property</returns>
		public string GetNodeImageVAlign(int index)
		{
			if( index == 0 ) 
			{
				return "top";
			} 
			else 
			{
				return "middle";
			}
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
			if( index == 0) 
			{
				return alternateTextStartNode;
			} 
			else 
			{
				return alternateTextIntermediateNode;
			}
		}

		/// <summary>
		/// Get property - returns the end node image url.
		/// </summary>
		public string EndNodeImage
		{
			get
			{
				return imageEndNodeUrl;
			}
		}

		/// <summary>
		/// Get property EndNodeImageAlternateText.
		/// The alternate text for the end node image.
		/// </summary>
		public string EndNodeImageAlternateText
		{
			get
			{
				return alternateTextEndNode;
			}
		}

		/// <summary>
		/// Returns whether the leg start station name should be rendered as a hyperlink
		/// </summary>
		public bool InfoAvailable(PublicJourneyDetail publicJourneyDetail)
		{
			// The location name is rendered as a hyperlink if 
			// the page is not printable AND a naptan exists for 
			// the start location of leg being currently rendered.

			TDNaptan[] naptans = publicJourneyDetail.LegStart.Location.NaPTANs;
			
			bool naptanExists = false;
			
			if	(naptans != null && naptans.Length > 0  && naptans[0].Naptan != null && naptans[0].Naptan.Length > 0)
			{
				if	(!naptans[0].Naptan.Equals(originNaptanString) && !naptans[0].Naptan.Equals(destinationNaptanString)) 
				{
					naptanExists = true;
				}
			}

			return naptanExists;
		}

		/// <summary>
		/// Returns whether the leg end station name should be rendered as a hyperlink
		/// </summary>
		public bool InfoAvailableEndLocation()
		{
			// Check for null, as this could cause error on the initial databind.
			if	(publicJourney == null)
			{
				return false;
			}

			// The end location name is rendered as a hyperlink if 
			// the page is not printable AND a naptan exists for 
			// the end location of leg being currently rendered.

			// Get the details for the last leg.            
			PublicJourneyDetail publicJourneyDetail = publicJourney.Details[publicJourney.Details.Length - 1];

			TDNaptan[] naptans = publicJourneyDetail.LegEnd.Location.NaPTANs;
			
			bool naptanExists = false;
			
			if	(naptans != null && naptans.Length > 0  && naptans[0].Naptan != null && naptans[0].Naptan.Length > 0)
			{
				if	(!naptans[0].Naptan.Equals(originNaptanString) && !naptans[0].Naptan.Equals(destinationNaptanString)) 
				{
					naptanExists = true;
				}
			}
           
			return naptanExists;
		}

		/// <summary>
		/// Returns the formatted string representation of the depart time.
		/// A space is returned if the specified leg is either a frequency
		/// leg or a walking leg
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Depart time string or space (&nbsp;)</returns>
		public string GetDepartDateTime(int index)
		{
			PublicJourneyDetail detail = publicJourney.Details[index];

			// Return formatted time string.
			if (index != 0 && (detail is PublicJourneyFrequencyDetail || detail is PublicJourneyContinuousDetail)) 
			{
				return "&nbsp;";
			} 
			else if (detail.Mode == ModeType.Air)
			{
				return DisplayFormatAdapter.StandardTimeFormatWithRoundingUp(detail.FlightDepartDateTime);
			} 
			else 
			{
				return DisplayFormatAdapter.StandardTimeFormatWithRoundingUp(detail.LegStart.DepartureDateTime);
			}
		}

		/// <summary>
		/// Returns the arrival time of the previous leg.
		/// A space is returned if the leg previous to the one specified
		/// is either a frequency leg or a walking leg
		/// </summary>
		/// <param name="index">Index of current leg being rendered.</param>
		/// <param name="interchangeMode">Interchanges have a distinct display format.
		/// InterchangeMode indicates when the leg being rendered is from the interchange markup.</param>
		/// <returns>Arrival time of previous leg or space (&nbsp;)</returns>
		public string GetPreviousArrivalDateTime(int index, bool interchangeMode)
		{
			//check if first leg is a flight check in leg
			PublicJourneyDetail detail = publicJourney.Details[index];
			
			if (index == 0) 
			{
				if (detail.Mode == ModeType.Air && detail.CheckInTime != null)
				{
					return DisplayFormatAdapter.StandardTimeFormatWithRoundingUp(detail.CheckInTime);
				}
				else
				{

					return String.Empty;
				}
			}			
        
			ResultsAdapter resultsAdapter = new ResultsAdapter();
			if	(interchangeMode != resultsAdapter.AtInterchange(publicJourney, index))
			{
				return "&nbsp;";
			}

			detail = publicJourney.Details[index - 1];
			
			if (detail is PublicJourneyFrequencyDetail || detail is PublicJourneyContinuousDetail)
			{
				return "&nbsp;";
			} 
			else 
			{
				return DisplayFormatAdapter.StandardTimeFormatWithRoundingUp(detail.LegEnd.ArrivalDateTime);
			}
		}
        
		/// <summary>
		/// Returns the instruction text "Arrive", or "Check-in" for Air if the leg previous to the
		/// one specified is neither a frequency leg or walking leg, otherwise
		/// a space is returned.
		/// </summary>
		/// <param name="index">Index of current leg being rendered.</param>
		/// <param name="interchangeMode">Interchanges have a distinct display format.
		/// InterchangeMode indicates when the leg being rendered is from the interchange markup.</param>
		/// <returns>Instruction text or space (&nbsp;)</returns>
		public string GetPreviousInstruction(int index, bool interchangeMode) 
		{
			//if first journey detail is air then a 'Check-in' label should be displayed
			PublicJourneyDetail detail = publicJourney.Details[index];
			
			if (index == 0 && detail.Mode == ModeType.Air && detail.CheckInTime != null)
			{
				return checkinText;
			}
			else if ((index == 0) && (detail.Mode != ModeType.Air || detail.CheckInTime == null))
			{
				return "&nbsp;";
			}
			else
			{            
				
				ResultsAdapter resultsAdapter = new ResultsAdapter();
				if	(interchangeMode != resultsAdapter.AtInterchange(publicJourney, index))
				{
					return "&nbsp;";
				}
			
				detail = publicJourney.Details[index - 1];
				
				if (detail is PublicJourneyFrequencyDetail || detail is PublicJourneyContinuousDetail) 
				{
					return "&nbsp;";
				} 			
				else 
				{
					return arriveText;
				}
			}
            
		}

		/// <summary>
		/// Returns the instruction text "Leave" for the first leg
		/// (index is zero). Returns the instruction text "Depart" if the specified 
		/// leg is neither a frequency leg or walking leg, otherwise
		/// a space is returned.
		/// </summary>
		/// <param name="index">Index of current leg being rendered.</param>
		/// <returns>Instruction text or space (&nbsp;)</returns>
		public string GetCurrentInstruction(int index) 
		{			

			PublicJourneyDetail detail = publicJourney.Details[index];

			if ((index == 0) && (detail.Mode != ModeType.Air)) 
			{						  
				return leaveText;
			}						  
			else if (detail is PublicJourneyFrequencyDetail || detail is PublicJourneyContinuousDetail) 
			{
				return "&nbsp;";
			} 
			else 
			{
				return departText;
			}
		}
		
		/// <summary>
		/// Read only property. Returns the longest instruction text to be used by a dummy cell in the repeater table. This is only to ensure correct cell widths.
		/// </summary>
		public string GetLongestInstruction
		{
			get {return checkinText;}
		}

		/// <summary>
		/// Returns the text for the transport mode of the specified leg.
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Mode string formatted.</returns>
		public string GetMode(PublicJourneyDetail publicJourneyDetail, bool lowerCase)
		{
			string resourceManagerKey = (lowerCase ? "TransportModeLowerCase." : "TransportMode.")
				+ publicJourneyDetail.Mode.ToString();
            
			return Global.tdResourceManager.GetString(
				resourceManagerKey, TDCultureInfo.CurrentUICulture);

		}
		
		/// <summary>
		/// Returns the Duration string.
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted duration string.</returns>
		public string GetDuration( PublicJourneyDetail detail )
		{
			if (detail is PublicJourneyFrequencyDetail)
			{
				return String.Empty;
			} 
			else 
			{
				// Get the duration of the current leg (rounded to the nearest minute)
				long durationInSeconds = detail.Duration; // in seconds

				// Get the minutes
				double durationInMinutes = durationInSeconds / 60;

				// Check to see if seconds is less than 30 seconds.
				if( durationInSeconds < 31)
				{
					return "< 30 " + secondsText;
				}
				else
				{
					// Round to the nearest minute
					durationInMinutes = DisplayFormatAdapter.RoundNumber(durationInMinutes);

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
		/// If the supplied leg is a frequency leg, returns a formatted
		/// string containing frequency details, otherwise an empty
		/// string is returned.
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>frequency details or empty string</returns>
		public string GetFrequencyText( PublicJourneyDetail detail ) 
		{
			PublicJourneyFrequencyDetail publicJourneyFrequencyDetail = (detail as PublicJourneyFrequencyDetail);
			if (publicJourneyFrequencyDetail != null)
			{   
				if	(publicJourneyFrequencyDetail.MinFrequency == publicJourneyFrequencyDetail.MaxFrequency)
				{
					return everyText + " " + publicJourneyFrequencyDetail.MinFrequency + minsText;
				}
				else
				{
					return everyText + " " + publicJourneyFrequencyDetail.MinFrequency + "-" + 
						publicJourneyFrequencyDetail.MaxFrequency + minsText;
				}
			} 
			else 
			{
				return "&nbsp;";
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

			// if service details are available, mode will be rendered 
			//  as a hyperlink to ServiceDetails page, otherwise it is 
			//  to be included here as plain text ...

			if	(!HasServiceDetails(detail))    
			{
				output.Append("<span class=\"txteight\">" + GetMode(detail, false) + "</span>");
			}
			
			string services = GetServices(detail);

			if (services.Length > 0) 
			{
				output.Append("<span class=\"txtnineb\"> "+GetServices(detail)+"</span>");
			}
			
			if (detail is PublicJourneyFrequencyDetail) 
			{
				output.Append("<br /><span class=\"txteight\">"+GetMaxJourneyDurationText(detail)+"<br>");
				output.Append(GetTypicalDurationText(detail)+"</span>");
			} 
			else 
			{
				output.Append("<br /><span class=\"txteight\">"+GetDuration(detail)+"</span>");
			}
			
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
			PublicJourneyFrequencyDetail publicJourneyFrequencyDetail = (detail as PublicJourneyFrequencyDetail);
			if (publicJourneyFrequencyDetail != null) 
			{
				return maxDurationText + ": " + publicJourneyFrequencyDetail.MaxDuration +
					(publicJourneyFrequencyDetail.MaxDuration > 1 ? minsText : minText);
			} 
			else 
			{
				return string.Empty;
			}
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
			PublicJourneyFrequencyDetail publicJourneyFrequencyDetail = (detail as PublicJourneyFrequencyDetail);
			if (publicJourneyFrequencyDetail != null) 
			{
				return typicalDurationText + ": " + publicJourneyFrequencyDetail.Duration +
					(publicJourneyFrequencyDetail.Duration > 1 ? minsText : minText);
			} 
			else 
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// If the supplied leg is not a rail leg and services details are
		/// present, returns a string containing every service number
		/// delimited by commas. Otherwise an empty string is returned. 
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing service numbers or empty string</returns>
		public string GetServices(PublicJourneyDetail detail)
		{
			if (detail.Mode != ModeType.Rail && 
				detail.Services != null && 
				detail.Services.Length > 0)
			{
				StringBuilder serviceDetailsText = new StringBuilder();
				for (int count=0; count < detail.Services.Length; count++) 
				{
					serviceDetailsText.Append(detail.Services[count].ServiceNumber);
					if (count < detail.Services.Length -1)
					{
						serviceDetailsText.Append(",");
					}
				}
				return serviceDetailsText.ToString();         
			} 
			else 
			{
				return String.Empty;
			}
		}

		/// <summary>
		/// Returns the css class name "bgline" if the index value 
		/// supplied is non-zero, otherwise returns an empty string
		/// </summary>
		/// <param name="index">Current item being rendered</param>
		/// <returns>css class name "bgline" or empty string</returns>
		public string GetBackgroundLineClass(int index) 
		{
			if (index == 0) 
			{
				return String.Empty;
			} 
			else 
			{
				return "bgline";
			}
		}

		/// <summary>
		/// Get property.
		/// The end time for the journey.
		/// </summary>
		public string FooterEndDateTime
		{
			get 
			{
				if( publicJourney == null) 
				{
					return "&nbsp;";
				}
				if (publicJourney.Details[publicJourney.Details.Length-1].Mode == ModeType.Air)
				{
					return DisplayFormatAdapter.StandardTimeFormatWithRoundingUp(
						publicJourney.Details[publicJourney.Details.Length-1].FlightArriveDateTime);
				}
				else
				{
					return DisplayFormatAdapter.StandardTimeFormatWithRoundingUp(
						publicJourney.Details[publicJourney.Details.Length-1].LegEnd.ArrivalDateTime);
				}
			}
		}

		/// <summary>
		/// Get property.
		/// Returns the instruction text "exit" for a flight leg, otherwise
		/// a space is returned.
		/// </summary>
		public string FooterExitText
		{
			get 
			{
				if( publicJourney == null) 
				{
					return "&nbsp;";
				}
				if (publicJourney.Details[publicJourney.Details.Length - 1].Mode == ModeType.Air 
					&& publicJourney.Details[publicJourney.Details.Length - 1].ExitTime != null)
				{
					return exitText;
				}
				else
				{
					return "&nbsp;";
				}
			}
		}

		/// <summary>
		/// Get property.
		/// Returns the formatted string representation of the exit time.
		/// A space is returned if the specified leg is not a flight leg
		/// </summary>
		public string FooterExitDateTime
		{
			get 
			{
				if( publicJourney == null) 
				{
					return "&nbsp;";
				}
				if (publicJourney.Details[publicJourney.Details.Length-1].Mode == ModeType.Air
					&& publicJourney.Details[publicJourney.Details.Length-1].ExitTime != null)
				{
					return DisplayFormatAdapter.StandardTimeFormatWithRoundingUp(
						publicJourney.Details[publicJourney.Details.Length-1].ExitTime);
				}
				else
				{
					return "&nbsp;";
				}
			}
		}

		/// <summary>
		/// Returns the name of the end location of the previous leg
		/// </summary>
		/// <param name="index">Index data item currently being rendered.</param>
		/// <returns>Name of the end location</returns>
		public string GetPreviousEndLocation(int index)
		{
			if	(index == 0) 
			{
				return string.Empty;
			}
			else
			{
				PublicJourneyDetail detail = publicJourney.Details[index  - 1];
				return detail.LegEnd.Location.Description;
			}
		}

		/// <summary>
		/// Returns the naptan of the end location of the previous leg
		/// </summary>
		/// <param name="index">Index data item currently being rendered.</param>
		/// <returns>Naptan of the end location</returns>
		public string GetPreviousEndNaptan(int index)
		{
			
			PublicJourneyDetail detail = null;

			if	(index == 0) 
			{
				return string.Empty;
			}
			else
			{
				detail = publicJourney.Details[index  - 1];
			}

			TDNaptan[] naptans = detail.LegEnd.Location.NaPTANs;

			if  (naptans != null && naptans.Length > 0  && naptans[0].Naptan != null && naptans[0].Naptan.Length != 0)
			{
				return naptans[0].Naptan;
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Returns the name of the start location
		/// </summary>
		/// <param name="legDetails">Current data item being rendered.</param>
		/// <returns>Name of the start location</returns>
		public string GetStartLocation(PublicJourneyDetail publicJourneyDetail)
		{
			// Return the start location of the given leg
			return publicJourneyDetail.LegStart.Location.Description;
		}

		/// <summary>
		/// Get property FooterEndLocation. The end location of the journey.
		/// </summary>
		public string FooterEndLocation
		{
			get 
			{
				if( publicJourney == null ) 
				{
					return string.Empty;
				}
				return publicJourney.Details[publicJourney.Details.Length-1].LegEnd.Location.Description;
			}
		}
        
		/// <summary>
		/// Get property FooterEndInstruction. Internationalised text for "Arrive"
		/// </summary>
		public string FooterEndInstruction
		{
			get
			{
				return arriveText;
			}
		}

		/// <summary>
		/// Read only property. Gets image for spacing in diagram.
		/// </summary>
		public string GetSpacerImageUrl
		{
			get {return imageSpacerUrl;}
		}

		/// <summary>
		/// Returns the image url for the transport mode of the specified
		/// leg.
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Image Url</returns>
		public string GetModeImageUrl(PublicJourneyDetail detail)
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

                case ModeType.Telecabine:
                    return imageTelecabineUrl;

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
		/// Gets the alternate text for the transport mode 
		/// image for the given leg
		/// </summary>
		/// <param name="detail">Current leg being rendered.</param>
		/// <returns>Alternate text</returns>
		public string GetModeImageAlternateText(PublicJourneyDetail detail)
		{
			
			// Note: these are currently all set to empty strings in the resource file.
			//		 This is apparently so that the mode is not read out twice by screen readers 
			//		 (once from the alt text, then again from then label underneath).
			
			switch(detail.Mode)
			{
				case ModeType.Air :
					return GetResource("langStrings", "TransportMode.Air.ImageAlternateText");

				case ModeType.Bus :
					return GetResource("langStrings", "TransportMode.Bus.ImageAlternateText");

				case ModeType.Coach :
					return GetResource("langStrings", "TransportMode.Coach.ImageAlternateText");

				case ModeType.Cycle :
					return GetResource("langStrings", "TransportMode.Cycle.ImageAlternateText");

				case ModeType.Drt :
					return GetResource("langStrings", "TransportMode.Drt.ImageAlternateText");

				case ModeType.Ferry :
					return GetResource("langStrings", "TransportMode.Ferry.ImageAlternateText");

				case ModeType.Metro :
					return GetResource("langStrings", "TransportMode.Metro.ImageAlternateText");

				case ModeType.Rail :
					return GetResource("langStrings", "TransportMode.Rail.ImageAlternateText");

				case ModeType.Taxi :
					return GetResource("langStrings", "TransportMode.Taxi.ImageAlternateText");

                case ModeType.Telecabine:
                    return GetResource("langStrings", "TransportMode.Telecabine.ImageAlternateText");

				case ModeType.Tram :
					return GetResource("langStrings", "TransportMode.Tram.ImageAlternateText");

				case ModeType.Underground :
					return GetResource("langStrings", "TransportMode.Underground.ImageAlternateText");

				case ModeType.Walk :
					return GetResource("langStrings", "TransportMode.Walk.ImageAlternateText");

				case ModeType.RailReplacementBus :
					return GetResource("langStrings", "TransportMode.RailReplacementBus.ImageAlternateText");
			}

			return String.Empty;
		}


		/// <summary>
		/// Returns the index of the given leg details
		/// </summary>
		/// <param name="legDetails">Current data item being rendered.</param>
		/// <returns>Index of the data</returns>
		private int IndexOf(PublicJourneyDetail detail)
		{
			int index=0;

			for(index=0; index < publicJourney.Details.Length; index++)
			{
				if  (publicJourney.Details[index] == detail)
				{
					break;
				}
			}

			return index;
		}

		#endregion

		#region Highlighting selected part of journey to adjust

		/// <summary>
		/// Return a unique id for the cell being rendered
		/// </summary>
		/// <param name="index">Index of current item being rendered.</param>
		/// <param name="pos">Indicates whether cell is at the top, middle or bottom of the leg.</param>
		/// <param name="interchange">True if the cell being rendered is for an interchange</param>
		/// <returns>Table Cell Id</returns>
		public string GetHighlightCellId(int index, string pos, bool interchange)
		{
			string cellId = String.Empty;

			// Don't increment index on an interchange (has same index as current leg being processed)
			if (!interchange)
			{
				// If the cell is a bottom cell, we reduce the index (by using last value) as the repeater has actually moved onto
				// the next leg at this point
				if (pos == "Bottom")
				{
					if (nextBottomCellIsInterchange)	// Special processing for interchange - if last cell was bottom cell of interchange this will be true
					{
						// Suffix the ID with "_interchange"
						index = lastIndexId + 1;

						cellId = highlightCellIdPrefix + pos + index + InterchangeIndicator;
						nextBottomCellIsInterchange = false;
					}
					else
					{
						index = lastIndexId;
						cellId = highlightCellIdPrefix + pos + index;
					}
				}
				else
				{
					cellId = highlightCellIdPrefix + pos + index;
				}
				lastIndexId = index;
			}
			else
			{
				// At interchange, the bottom cell is visually the last cell of the previous leg, therfore use
				// previous index and don't append with "_interchange" BUT note that the next bottom WILL represent
				// interchange and must be appended so
				if (pos == "Bottom" && atInterchange)
				{
					index = lastIndexId;
					cellId = highlightCellIdPrefix + pos + index;
					nextBottomCellIsInterchange = true;
				}
				else
				{
					index = lastIndexId + 1;

					cellId = highlightCellIdPrefix + pos + index + InterchangeIndicator;
				}
			}

			return cellId;
		}

		/// <summary>
		/// Returns Class style for the cell being rendered
		/// </summary>
		/// <param name="index">Index of current item being rendered.</param>
		/// <param name="pos">Indicates whether cell is at the top, middle or bottom of the leg.</param>
		/// <param name="interchange">True if the cell being rendered is for an interchange</param>
		/// <returns>Class style for the table cell</returns>
		public string GetHighlightCellClass(int index, string pos, bool interchange)
		{
			// Set the index in the same way as when named in GetHighlightCellId
			if (!interchange)
			{
				if (pos == "Bottom")
				{
					if (nextBottomCellIsInterchange_SetClass)	// Special processing for interchange - if last cell was bottom cell of interchange this will be true
					{
						index = lastIndexId_SetClass + 1;
						interchange = true;
						nextBottomCellIsInterchange_SetClass = false;
					}
					else
					{
						index = lastIndexId_SetClass;
					}
				}
				lastIndexId_SetClass = index;
			}
			else
			{
				// At interchange, the bottom cell is visually the last cell of the previous leg, therfore use
				// previous index and don't append with "_interchange" BUT note that the next bottom WILL represent
				// interchange and must be appended so
				if (pos == "Bottom" && atInterchange)
				{
					index = lastIndexId_SetClass;
					interchange = false;
					nextBottomCellIsInterchange_SetClass = true;
				}
				else
				{
					index = lastIndexId_SetClass + 1;
				}
			}

			// Is the tag for highlighting?
			if (pos == "Bottom")
			{
				if (IsIndexForAdjust(index, interchange))
				{
					return highlightedBottomClass;
				}
				else
				{
					return nonHighlightedBottomClass;
				}
			}
			else
			{
				if (IsIndexForAdjust(index, interchange))
				{
					return highlightedClass;
				}
				else
				{
					return String.Empty;
				}
			}
		}

		/// <summary>
		/// Returns whether current item (leg) being rendered is in the selected area
		/// </summary>
		/// <param name="index">Current item being rendered.</param>
		/// <param name="interchange">True if the cell being rendered is for an interchange</param>
		/// <returns>true or false</returns>
		private bool IsIndexForAdjust(int index, bool interchange)
		{
			bool isIndexForAdjust = false;

			TDCurrentAdjustState tsj = TDSessionManager.Current.CurrentAdjustState;
			if (tsj != null)
			{
				if (index != -1 && tsj.SelectedRouteNode > 0)
				{

					// Only mark as for adjust if there is a selection for both timings and locations
					if (tsj.SelectedAdjustLocationsDropdownValue == UNREFINED_TEXT ||
						tsj.SelectedAdjustTimingsDropdownValue == UNREFINED_TEXT)
					{
						return false;
					}
					
					if (tsj.SelectedRouteNodeSearchType == false)	// Leave after
					{
						// Is index within range from selected leg to end
						if (index >= tsj.SelectedRouteNode && index <= publicJourney.Details.Length - 1)
						{
							if (interchange)
							{
								if (selectedLocationIsInterchange)
								{
									isIndexForAdjust = true;
								}
							}
							else
							{
								isIndexForAdjust = true;
							}
						}
					}
					else	// Arrive before
					{
						// Is index within range from start to selected leg
						// Plus highlight the selected interchange leg if last leg selected (has index out of range checked)
						if (index >= 0 && index <= tsj.SelectedRouteNode - 1)
							isIndexForAdjust = true;
						else if((index == tsj.SelectedRouteNode) && interchange && selectedLocationIsInterchange)
							isIndexForAdjust = true;
						else
							isIndexForAdjust = false;
					}
				}
			}

			return isIndexForAdjust;
		}

		/// <summary>
		/// Notes if the location selected by the user includes an interchange
		/// </summary>
		/// <returns>True if the location selected by the user includes an interchange</returns>
		private bool SelectedLocationIsInterchange()
		{
			bool locIsInterchange = false;

			TDCurrentAdjustState tsj = TDSessionManager.Current.CurrentAdjustState;
			if (tsj != null)
			{
				if (tsj.SelectedRouteNode > 0)
				{
					int interchangePos = tsj.SelectedAdjustLocationsDropdownValue.IndexOf(InterchangeIndicator);

					if (tsj.SelectedRouteNodeSearchType == false)	// Leave after
					{
						if (interchangePos > 0)
							locIsInterchange = true;
					}
					else	// Arrive before
					{
						if (interchangePos == -1 && IndexHasInterchange(Convert.ToInt32(tsj.SelectedRouteNode)))
							locIsInterchange = true;
					}
					
				}
			}
			return locIsInterchange;
		}

		/// <summary>
		/// For a given index, sees if there is an equivalent value in the dropdown with an interchange,
		/// ie, given "4", sees if there is a "4_interchange" value.
		/// </summary>
		/// <param name="index">leg index</param>
		/// <returns>True if the leg index has an interchange</returns>
		private bool IndexHasInterchange(int index)
		{

			bool hasInterchange = false;

			ResultsAdapter resultsAdapter = new ResultsAdapter();
			hasInterchange = resultsAdapter.AtInterchange(publicJourney, index);
			
			return hasInterchange;
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraWiringEvents();
			InitializeComponent();
			base.OnInit(e);
		}
        
		///     Required method for Designer support - do not modify
		///     the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

		/// <summary>
		/// Method for wiring up events
		/// </summary>
		private void ExtraWiringEvents()
		{
			this.journeySegmentsRepeater.ItemCreated += new RepeaterItemEventHandler(JourneySegmentsRepeaterItemCreated);
		}

		#region Journey Properties
		
		/// <summary>
		/// Checks if current journey detail has additional service details to be dispalyed.
		/// Currently based on detail mode only - returning true for rail or rail replacement bus.
		/// </summary>
		public bool HasServiceDetails(PublicJourneyDetail detail)
		{
			return (detail.Mode == ModeType.Rail || detail.Mode == ModeType.RailReplacementBus);
		}

		#endregion

		#region Rounding method

		#endregion

	}
}