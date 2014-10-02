// *********************************************** 
// NAME                 : JourneyReplanSegmentsControl.ascx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 04/01/2006
// DESCRIPTION          : A user control to display
// details of a given journey that can be replanned
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyReplanSegmentControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Sep 14 2009 11:17:04   apatel
//Stop Information related changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.3   Jan 13 2009 13:57:16   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:21:38   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:46   mturner
//Initial revision.
//
//   Rev 1.12   Apr 26 2006 10:09:10   mdambrine
//the population of the text box "Tick to replace with a car journey " is done now for all legs, not only the train journey ones
//Resolution for 3985: DN068 Replan: 'Tick to replace...' text not displayed for certain legs
//
//   Rev 1.11   Apr 06 2006 15:41:36   RGriffith
//IR3732 Fix: Movement of Hardcoded text to langstring files & resolution of Mac style issues
//
//   Rev 1.10   Mar 20 2006 14:47:00   NMoorhouse
//Updated following review comments
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.9   Mar 14 2006 11:41:14   NMoorhouse
//Post stream3353 merge: added using TransportDirect.Common.ResourceManager
//
//   Rev 1.8   Mar 10 2006 09:45:54   NMoorhouse
//Updates for FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Mar 08 2006 16:02:52   RGriffith
//FxCop Suggested Changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Mar 01 2006 14:51:42   pcross
//Correction to adjust segment highlighting to interchanges are always selected on either side of a selected leg.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Feb 24 2006 10:42:40   pcross
//Replaced controls with labels
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Feb 20 2006 12:13:34   pcross
//Updated to allow highlighting of interchange legs
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 09 2006 17:56:46   RGriffith
//Changes for using the confirmation page on the Journey Replan Input page. Also added a property to disable links on the location labels
//
//   Rev 1.2   Feb 07 2006 19:50:50   tmollart
//Modfied to use ReplanPageState where required.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Jan 20 2006 10:36:18   NMoorhouse
//Updated for CUT
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Jan 19 2006 11:16:36   NMoorhouse
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//

#region Using Statements

using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using Logger = System.Diagnostics.Trace;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Web.Support;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.JourneyPlanRunner;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.Web.Adapters;
using System.Web.UI;

#endregion

namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	///		Summary description for JourneyReplanSegmentControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class JourneyReplanSegmentControl : TDUserControl
	{
		protected Label modeLinkLabel;
		protected Label locationInfoLinkLabel;
		protected Label locationLabelControl;
		protected Label alightLocationInfoLinkLabel;
		protected Label alightLocationLabelControl;
		protected Label endLocationInfoLinkLabel;
		protected Label endLocationLabelControl;
		protected Label labelTickBoxInputMessage;
		protected HtmlGenericControl interchangeDiv;

		// Data for the Repeater.
		private JourneyControl.PublicJourney publicJourney;

		/// <summary>
		/// True if the leg being rendered is at an interchange
		/// </summary>
		private bool atInterchange;

		/// <summary>
		/// Notes when the cell following an interchange needs to be given an interchange ID
		/// </summary>
		private bool nextBottomCellIsInterchange;

		private bool outward = true;

		private bool compareMode;
		private bool adjustable;
		private bool stationInfo;
		private bool firstJourney;	
		private bool lastJourney;
		private int journeyItineraryIndex = -1;

		// For seeing if there's information available for a particular location
		private const string originNaptanString = "Origin";
		private const string destinationNaptanString = "Destination";

		// Constants associated with highlighting
		private const string highlightedClass = "bglinedotted";
		private const string highlightedBottomClass = "bglinedottedbottom";
		private const string nonHighlightedBottomClass = "departline";
		private const string highlightCellIdPrefix = "highlightCell";
		private const string checkBoxIdPrefix = "highlightCell";
		private const string InterchangeIndicator = "_interchange";

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

		private int lastIndexId = -1;
		private ArrayList eventsRaised = new ArrayList();
		private static readonly object JourneyElementSelectionChangedEventKey = new object();

		#endregion

		/// <summary>
		/// Event that will be raised when the journey element selection is changed. This 
		/// is deemed to have happenned when any of the check boxes are selected/deselected.
		/// </summary>
		public event EventHandler JourneyElementSelectionChanged
		{
			add { this.Events.AddHandler(JourneyElementSelectionChangedEventKey, value); }
			remove { this.Events.AddHandler(JourneyElementSelectionChangedEventKey, value); }
		}

		#region Initialisation and initialisation properties
		/// <summary>
		/// Initialises the control. This method must be called.
		/// </summary>
		/// <param name="adjustable">Indicates if the control is being rendered in adjust mode.</param>
		/// <param name="compareMode">Indicates if the control is being rendered in compare mode.</param>
		/// <param name="outward">Indicates if the control is being rendered for outward or return journey.</param>
		/// <param name="publicJourney">Journey to render journey for.</param>
		/// <param name="stationInfo">Indicates if station info button should be displayed.</param>
		public void Initialise(
			JourneyControl.PublicJourney publicJourney, bool outward, bool compareMode, bool adjustable, bool stationInfo, bool firstJourney, bool lastJourney)
		{
			// All stored in internal viewstate so that initialise is called only once
			this.publicJourney = publicJourney;
			this.outward = outward;
			this.compareMode = compareMode;
			this.adjustable = adjustable;
			this.stationInfo = stationInfo;
			this.firstJourney = firstJourney;
			this.lastJourney = lastJourney;
			this.journeyItineraryIndex = TDItineraryManager.Current.SelectedItinerarySegment;			
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

		#region Page Load / OnPreRender Methods
            
		/// <summary>
		/// Page Load.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			// Need to call UpdateData to populate the Repeater. This isn't required
			// when inheriting from Web.UI.Page (and not TDPage).
			UpdateData();
			// Add javascript to the client side
			AddClientForElementHighlighting();
			lastIndexId = -1;

			#region Image translation and updating

			// Node and node connector image urls
			imageStartNodeUrl = GetResource("JourneyDetailsControl.imageStartNodeUrl");

			imageEndNodeUrl = GetResource(
				"JourneyDetailsControl.imageEndNodeUrl");
    
			imageIntermediateNodeUrl =  GetResource(
				"JourneyDetailsControl.imageIntermediateNodeUrl");
            
			imageNodeConnectorUrl = GetResource(
				"JourneyDetailsControl.imageNodeConnectorUrl");

			// Transport Mode Image Urls    
			imageCarUrl = GetResource(
				"JourneyDetailsControl.imageCarUrl");

			imageAirUrl = GetResource(
				"JourneyDetailsControl.imageAirUrl");

			imageBusUrl = GetResource(
				"JourneyDetailsControl.imageBusUrl");

			imageCoachUrl = GetResource(
				"JourneyDetailsControl.imageCoachUrl");

			imageCycleUrl = GetResource(
				"JourneyDetailsControl.imageCycleUrl");

			imageDrtUrl = GetResource(
				"JourneyDetailsControl.imageDrtUrl");
            
			imageFerryUrl = GetResource(
				"JourneyDetailsControl.imageFerryUrl");
            
			imageMetroUrl = GetResource(
				"JourneyDetailsControl.imageMetroUrl");
            
			imageRailUrl = GetResource(
				"JourneyDetailsControl.imageRailUrl");
            
			imageTaxiUrl = GetResource(
				"JourneyDetailsControl.imageTaxiUrl");
            
			imageTramUrl = GetResource(
				"JourneyDetailsControl.imageTramUrl");
            
			imageUndergroundUrl = GetResource(
				"JourneyDetailsControl.imageUndergroundUrl");
            
			imageWalkUrl = GetResource(
				"JourneyDetailsControl.imageWalkUrl");

			imageRailReplacementBusUrl = GetResource(
				"JourneyDetailsControl.imageRailReplacementBusUrl");

			imageSpacerUrl = GetResource(
				"MapZoomControl.spacer.ImageUrl");

			// Alternate text strings
			alternateTextStartNode = GetResource(
				"JourneyDetailsControl.alternateTextStartNode");

			alternateTextEndNode = GetResource(
				"JourneyDetailsControl.alternateTextEndNode");
            
			alternateTextIntermediateNode = GetResource(
				"JourneyDetailsControl.alternateTextIntermediateNode");
            
			alternateTextNodeConnector = GetResource(
				"JourneyDetailsControl.alternateTextNodeConnector");

			toolTipTextInformationButton = GetResource(
				"JourneyDetailsControl.InformationButton.ToolTipText");

			toolTipTextLocationLink = GetResource(
				"JourneyDetailsControl.LocationLink.ToolTipText");

			toolTipTextDetailsLink = GetResource(
				"JourneyDetailsControl.ServiceDetailsLink.ToolTipText");

			// Labels
			leaveText = GetResource(
				"JourneyDetailsControl.Leave");

			arriveText = GetResource(
				"JourneyDetailsControl.Arrive");

			departText = GetResource(
				"JourneyDetailsControl.Depart");

			checkinText = GetResource(
				"JourneyDetailsControl.Checkin");

			exitText = GetResource(
				"JourneyDetailsControl.Exit");

			minsText = GetResource(
				"JourneyDetailsTableControl.minutesString");
                
			minText = GetResource(
				"JourneyDetailsTableControl.minuteString");
                
			everyText = GetResource(
				"JourneyDetailsControl.every");

			maxDurationText = GetResource(
				"JourneyDetailsControl.maxDuration");
                
			typicalDurationText = GetResource(
				"JourneyDetailsControl.typicalDuration");

			startText = GetResource(
				"JourneyDetailsControl.StartText");

			endText = GetResource(
				"JourneyDetailsControl.EndText");

			secondsText = GetResource(
				"JourneyDetailsTableControl.secondsString");

			hoursText = GetResource(
				"JourneyDetailsTableControl.hoursString");

			hourText = GetResource(
				"JourneyDetailsTableControl.hourString");

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
					
					// Find the checkBox and bind its checkedchanged event
					ScriptableCheckBox s = (ScriptableCheckBox)e.Item.FindControl("checkJourneyElement");
					s.Enabled = !TDSessionManager.Current.JourneyViewState.ConfirmationMode;
					s.CheckedChanged += new EventHandler(OnJourneyElementSelectionChanged);
					
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
					labelTickBoxInputMessage = (Label)e.Item.FindControl("labelTickBoxInputMessage");

					if	(HasServiceDetails((PublicJourneyDetail)e.Item.DataItem))
					{
						modeLinkLabel.Text = GetMode((PublicJourneyDetail)e.Item.DataItem, false);
						modeLinkLabel.Visible = true;																		
					}
					else
					{
						modeLinkLabel.Visible = false;						
					}

					labelTickBoxInputMessage.Text = GetResource("JourneyReplanSegmentControl.labelTickBoxInputMessage.Text");

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

		/// <summary>
		/// Event handler for all of the checkBoxes.
		/// Raises the JourneyElementSelectionChanged event. The eventsRaised check is used to 
		/// ensure that the event is only raised once for each Postback, as the method
		/// will be called for each checkBox that was checked/unchecked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnJourneyElementSelectionChanged(object sender, EventArgs e)
		{

			if (!eventsRaised.Contains(JourneyElementSelectionChangedEventKey))
			{
				EventHandler theDelegate = (EventHandler)this.Events[JourneyElementSelectionChangedEventKey];
				if (theDelegate != null)
					theDelegate(this, e);
				eventsRaised.Add(JourneyElementSelectionChangedEventKey);
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
			// here (and not in PageLoad) because during PageLoad, the Repeater is empty
			// for some reason (this behaviour does not occur if the page that this
			// control is in is inherited from Web.UI.Page), therefore, the UpdateData
			// is required to populate the Repeater so that the event handlers can
			// be correctly registered.

			UpdateData();	
			base.OnPreRender(e);
		}
        
		#endregion

		/// <summary>
		/// Registers the client side script.
		/// </summary>
		private void AddClientForElementHighlighting()
		{
			TDPage tdPage = (TDPage)this.Page;
			if (tdPage.IsJavascriptEnabled)
			{
				string javaScriptFileName = "JourneyReplanElementSelection";
				string javaScriptDom = tdPage.JavascriptDom;
				ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
				// Output reference to necessary JavaScript file from the ScriptRepository
				this.Page.ClientScript.RegisterClientScriptBlock(typeof(JourneyReplanSegmentControl), javaScriptFileName, scriptRepository.GetScript(javaScriptFileName, javaScriptDom));                				
			}
		}

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
		/// Returns the text for the transport mode of the specified leg.
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Mode string formatted.</returns>
		public string GetMode(PublicJourneyDetail publicJourneyDetail, bool lowercase)
		{
			string resourceManagerKey = (lowercase ? "TransportModeLowerCase." : "TransportMode.")
				+ publicJourneyDetail.Mode.ToString();
            
			return GetResource(
				resourceManagerKey);

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
			PublicJourneyFrequencyDetail frequencyDetail = detail as PublicJourneyFrequencyDetail;
			if (frequencyDetail != null)
			{   		
				if	(frequencyDetail.MinFrequency == frequencyDetail.MaxFrequency)
				{
					return everyText + " " + frequencyDetail.MinFrequency + minsText;
				}
				else
				{
					return everyText + " " + frequencyDetail.MinFrequency + "-" + 
						frequencyDetail.MaxFrequency + minsText;
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
				output.Append("<label class=\"txtnine\">" + GetMode(detail, false) + "</label>");
			}
			
			string services = GetServices(detail);

			if (services.Length > 0) 
			{
				output.Append("<label class=\"txtnineb\"> "+GetServices(detail)+"</label>");
			}
			
			if (detail is PublicJourneyFrequencyDetail) 
			{
				output.Append("<br/><label class=\"txtnine\">"+GetMaxJourneyDurationText(detail)+"<br/>");
				output.Append(GetTypicalDurationText(detail)+"</label>");
			} 
			else 
			{
				output.Append("<br/><label class=\"txtnine\">"+GetDuration(detail)+"</label>");
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
			PublicJourneyFrequencyDetail frequencyDetail = detail as PublicJourneyFrequencyDetail;
			if (frequencyDetail != null)
			{   
				return maxDurationText + ": " + frequencyDetail.MaxDuration +
					(frequencyDetail.MaxDuration > 1 ? minsText : minText);
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
			PublicJourneyFrequencyDetail frequencyDetail = detail as PublicJourneyFrequencyDetail;
			if (frequencyDetail != null)
			{   	
				return typicalDurationText + ": " + frequencyDetail.Duration +
					(frequencyDetail.Duration > 1 ? minsText : minText);
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
		/// Read only property
		/// Returns the longest instruction text to be used by a dummy cell in the 
		/// repeater table. This is only to ensure correct cell widths.
		/// </summary>
		public string GetLongestInstruction 
		{
			get{ return checkinText;}
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
		/// Returns the naptan of the start location
		/// </summary>
		/// <param name="legDetails">Current data item being rendered.</param>
		/// <returns>Naptan of the start location</returns>
		public string GetStartNaptan(PublicJourneyDetail publicJourneyDetail)
		{
			
			TDNaptan[] naptans = publicJourneyDetail.LegStart.Location.NaPTANs;

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
		/// Read only property
		/// Gets the selected journey indexes for the element to be replanned
		/// </summary>
		public ArrayList SelectedJourneyElementValues
		{
			get 
			{
				ArrayList results = new ArrayList();

				foreach (RepeaterItem ri in journeySegmentsRepeater.Items)
				{
					switch (ri.ItemType)
					{
						case ListItemType.Item :
						case ListItemType.AlternatingItem : 
						
							ScriptableCheckBox checkCurr;

							checkCurr = (ScriptableCheckBox)ri.FindControl("checkJourneyElement");
					
							if (checkCurr.Checked)
								results.Add(int.Parse(checkCurr.Value, TDCultureInfo.InvariantCulture));
						
							break;

						default :
							break;
					}
				}

				return results;
			}

		}

		/// <summary>
		/// Read only property
		/// Returns the Spacer image url
		/// </summary>
		public string SpacerImageUrl
		{
			get{return imageSpacerUrl;}
		}

		/// <summary>
		/// Returns the command name that should be associated with the map button.
		/// </summary>
		/// <param name="publicJourneyDetail">Current item being rendered.</param>
		/// <returns>Returns the command name.</returns>
		public string GetCommandName(PublicJourneyDetail publicJourneyDetail)
		{
			return IndexOf(publicJourneyDetail).ToString(TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Returns the command name that should be associated with the leave later button.
		/// </summary>
		/// <param name="publicJourneyDetail">Current item being rendered.</param>
		/// <returns>Returns the command name.</returns>
		public string GetLeaveLaterCommandName(PublicJourneyDetail publicJourneyDetail)
		{
			return IndexOf(publicJourneyDetail).ToString(TDCultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Returns the command name that should be associated with the Find Transport To button.
		/// </summary>
		/// <param name="publicJourneyDetail">Current item being rendered.</param>
		/// <returns>Returns the command name.</returns>
		public string GetFindTransportToCommandName(PublicJourneyDetail publicJourneyDetail)
		{
			return IndexOf(publicJourneyDetail).ToString(TDCultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Returns the command name that should be associated with the arrive earlier button.
		/// </summary>
		/// <param name="publicJourneyDetail">Current item being rendered.</param>
		/// <returns>Returns the command name.</returns>
		public string GetInfoCommandName(PublicJourneyDetail publicJourneyDetail)
		{
			return IndexOf(publicJourneyDetail).ToString(TDCultureInfo.CurrentCulture);
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
					return GetResource(
						"TransportMode.Air.ImageAlternateText");

				case ModeType.Bus :
					return GetResource(
						"TransportMode.Bus.ImageAlternateText");

				case ModeType.Coach :
					return GetResource(
						"TransportMode.Coach.ImageAlternateText");

				case ModeType.Cycle :
					return GetResource(
						"TransportMode.Cycle.ImageAlternateText");

				case ModeType.Drt :
					return GetResource(
						"TransportMode.Drt.ImageAlternateText");

				case ModeType.Ferry :
					return GetResource(
						"TransportMode.Ferry.ImageAlternateText");

				case ModeType.Metro :
					return GetResource(
						"TransportMode.Metro.ImageAlternateText");

				case ModeType.Rail :
					return GetResource(
						"TransportMode.Rail.ImageAlternateText");

				case ModeType.Taxi :
					return GetResource(
						"TransportMode.Taxi.ImageAlternateText");

				case ModeType.Tram :
					return GetResource(
						"TransportMode.Tram.ImageAlternateText");

				case ModeType.Underground :
					return GetResource(
						"TransportMode.Underground.ImageAlternateText");

				case ModeType.Walk :
					return GetResource(
						"TransportMode.Walk.ImageAlternateText");

				case ModeType.RailReplacementBus :
					return GetResource(
						"TransportMode.RailReplacementBus.ImageAlternateText");
			}

			return String.Empty;
		}


		/// <summary>
		/// Gets the alternate text for the transport mode 
		/// image/text where this is a button or link 
		/// to the service details page.
		/// </summary>
		/// <param name="detail">Current leg being rendered.</param>
		/// <returns>Alternate text</returns>
		public string GetModeLinkText(PublicJourneyDetail detail)
		{
			return string.Format(CultureInfo.InvariantCulture, toolTipTextDetailsLink, GetMode(detail, true));
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

		#region Rendering of checkBoxes for journey element selection and highlighting
		/// <summary>
		/// Return a unique id for specific checkBox being rendered
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>CheckBox Id</returns>
		public string GetCheckBoxId(int index)
		{
			string checkBoxId = checkBoxIdPrefix + index;
			return checkBoxId;
		}

		/// <summary>
		/// Returns the index of the current item being rendered
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Journey Index</returns>
		public string GetItemIndex(int index)
		{
			return index.ToString(TDCultureInfo.CurrentUICulture);
		}

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
						index = lastIndexId;
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
					index = lastIndexId;
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
			/// Don't increment index on an interchange (has same index as current leg being processed)
			if (!interchange)
			{
				if (pos == "Bottom")
				{
					index = lastIndexId;
				}
				lastIndexId = index;
			}

			// Is the tag for highlighting?
			if (pos == "Bottom")
			{
				if (IsIndexForReplan(index))
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
				if (IsIndexForReplan(index))
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
		/// Returns whether current checkBox being rendered should be checked
		/// </summary>
		/// <param name="index">Current item being rendered.</param>
		/// <returns>true or false</returns>
		public bool GetCheckedStatus(int index)
		{
			return IsIndexForReplan(index);
		}

		/// <summary>
		/// Returns whether current item being rendered is in the selected area
		/// </summary>
		/// <param name="index">Current item being rendered.</param>
		/// <returns>true or false</returns>
		private bool IsIndexForReplan(int index)
		{
			if (index != -1 && index >= ((ReplanPageState)TDSessionManager.Current.InputPageState).ReplanStartJourneyDetailIndex && index <= ((ReplanPageState)TDSessionManager.Current.InputPageState).ReplanEndJourneyDetailIndex)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

		#region Button Click Handlers

		/// <summary>
		/// Event Handler for the information hyperlink.
		/// </summary>
		public void InformationLinkClick(object sender, System.EventArgs e)
		{
			// User has clicked the information hyperlink for a particular location.

			HyperlinkPostbackControl link = (HyperlinkPostbackControl)sender;

			string naptan =  link.CommandName;

			if( naptan != null && naptan.Length > 0 )
			{
				TDSessionManager.Current.InputPageState.AdditionalDataLocation = naptan;
                SetStopInformation(naptan);
			}

			// This is how we 'return'
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( MyPageId );


			// Show the information page for the selected location.
			// Write the Transition Event
            // CCN 526 Changes
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.StopInformation;
		}

        

		/// <summary>
		/// Event Handler for the information button.
		/// </summary>
		public void InformationLinkEndClick(object sender, System.EventArgs e)
		{
			// Get naptan for the end node
			int selectedRouteNode = publicJourney.Details.Length - 1;

			TDNaptan[] naptan = publicJourney.Details[selectedRouteNode].LegEnd.Location.NaPTANs;

			if( naptan != null && naptan.Length > 0 )
			{
				// Which naptan?
				TDSessionManager.Current.InputPageState.AdditionalDataLocation = naptan[0].Naptan;
                SetStopInformation(naptan[0].Naptan);
			}

			// This is how we 'return'
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( MyPageId );


			// Show the information page for the selected location.
			// Write the Transition Event
            // CCN526 changes
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.StopInformation;
		}


		/// <summary>
		/// Event Handler for the information button.
		/// </summary>
		public void ServiceDetailsClick(object sender, System.EventArgs e)
		{
			HyperlinkPostbackControl link = (HyperlinkPostbackControl)sender;
			DisplayServiceDetails(Int32.Parse(link.CommandName, TDCultureInfo.CurrentCulture.NumberFormat));
		}

		/// <summary>
		/// Event Handler for the mode imagebutton (linking to ServiceDetails page).
		/// </summary>
		public void ServiceDetailsButtonClick(object sender, ImageClickEventArgs e)
		{
			ImageButton button = (ImageButton)sender;
			DisplayServiceDetails(Int32.Parse(button.CommandName, TDCultureInfo.CurrentCulture.NumberFormat));
		}

        /// <summary>
        /// Seting the naptan information for Stop Information page
        /// </summary>
        /// <param name="naptan"></param>
        private void SetStopInformation(string naptan)
        {
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;

            inputPageState.StopCode = naptan;
            inputPageState.StopCodeType = TDCodeType.NAPTAN;
            inputPageState.ShowStopInformationPlanJourneyControl = false;
        }


		/// <summary>
		/// Perform a transition to the ServiceDetails page for the selected leg.
		/// </summary>
		private void DisplayServiceDetails(int selectedJourneyLeg)
		{
			// Write the selected journey leg to TDViewState
			TDJourneyViewState journeyViewState = TDItineraryManager.Current.JourneyViewState;
			journeyViewState.SelectedJourneyLeg = selectedJourneyLeg;
			journeyViewState.SelectedIntermediateItinerarySegment = journeyItineraryIndex;
			journeyViewState.CallingPageID = belongingPageId;
	
			TDSessionManager.Current.Session[SessionKey.JourneyDetailsOutward] = outward;
		
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( MyPageId );

			// Show the information page for the selected location.
			// Write the Transition Event
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.ServiceDetails;
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

		#region Properties: Alternate Text for buttons


		/// <summary>
		/// Get property InformationButtonAlternateText. Alternate text for the information button.
		/// </summary>
		public string InformationButtonToolTipText
		{
			get
			{
				return toolTipTextInformationButton;
			}
		}

		#endregion

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

	}
}