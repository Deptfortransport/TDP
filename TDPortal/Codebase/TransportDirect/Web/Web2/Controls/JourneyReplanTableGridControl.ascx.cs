// *********************************************** 
// NAME                 : JourneyReplanTableGridControl.ascx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 11/01/2006
// DESCRIPTION          : A custom control to display
// details of an individual leg of a journey in a tabular 
// format so allowing the user to replan part of the 
// journey.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyReplanTableGridControl.ascx.cs-arc  $
//
//   Rev 1.5   Mar 21 2013 10:13:18   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.4   Sep 14 2009 11:17:06   apatel
//Stop Information related changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.3   Jan 13 2009 13:57:20   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:21:38   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:48   mturner
//Initial revision.
//
//   Rev 1.10   Mar 31 2006 10:37:16   NMoorhouse
//Add Initialise method for car journeys details incase it becomes back in scope.
//
//   Rev 1.9   Mar 24 2006 12:48:12   pcross
//Removed hyperlinks from the replan grid control for consistency with diagram view
//Resolution for 3682: Extend, Replan & Adjust: Table view when selecting to Replan legs displays links to Stations
//
//   Rev 1.8   Mar 20 2006 14:47:02   NMoorhouse
//Updated following review comments
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Mar 14 2006 11:41:14   NMoorhouse
//Post stream3353 merge: added using TransportDirect.Common.ResourceManager
//
//   Rev 1.6   Mar 10 2006 09:47:36   NMoorhouse
//Updates for FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Mar 08 2006 16:02:50   RGriffith
//FxCop Suggested Changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Feb 24 2006 10:44:10   pcross
//Removed network map link control
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 20 2006 12:14:42   pcross
//Correction to name of JavaScript
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 09 2006 17:56:24   RGriffith
//Changes for using the confirmation page on the Journey Replan Input page. Also added a property to disable links on the location labels
//
//   Rev 1.1   Jan 27 2006 15:52:10   rhopkins
//Changed to use JourneyLeg property of LegInstructionsControl
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Jan 19 2006 11:16:38   NMoorhouse
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Globalization;

using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// User control to select details of a public journey in a tabular format for replan.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class JourneyReplanTableGridControl : TDUserControl
	{
		protected LegInstructionsControl legInstructionsControl;
		protected Label modeLinkLabel;
		protected Label startLocationLabelControl;
		protected Label endLocationLabelControl;
		protected HyperlinkPostbackControl endLocationInfoLinkControl;
		protected VehicleFeaturesControl vehicleFeaturesControl;

		private JourneyControl.PublicJourney publicJourney;
		private JourneyControl.RoadJourney roadJourney;
		private ITDJourneyRequest journeyRequest;
		private PageId belongingPageId = PageId.Empty;
		private int itinerarySegment = -1;
		private bool useRoadJourney;
		private bool outward;
		private bool flight;
		private string minsText = String.Empty;
		private string minText = String.Empty;
		private string maxDurationText = String.Empty;
		private string typicalDurationText = String.Empty;
		private string everyText = String.Empty;
		private string secondsText = String.Empty;
		private string hoursText = String.Empty;
		private string hourText = String.Empty;
		
		private string toolTipTextLocationLink = String.Empty;
		private string toolTipTextDetailsLink = String.Empty;

		private const string originNaptanString = "Origin";
		private const string destinationNaptanString = "Destination";

		private ArrayList eventsRaised = new ArrayList();
		private static readonly object JourneyElementSelectionChangedEventKey = new object();

		/// <summary>
		/// Event that will be raised when the journey element selection is changed. This 
		/// is deemed to have happenned when any of the check boxes are selected/deselected.
		/// </summary>
		public event EventHandler JourneyElementSelectionChanged
		{
			add { this.Events.AddHandler(JourneyElementSelectionChangedEventKey, value); }
			remove { this.Events.AddHandler(JourneyElementSelectionChangedEventKey, value); }
		}

		/// <summary>
		/// Sets-up event handlers for all the buttons, etc
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			// Need to call UpdateData to populate the Repeater. This isn't required
			// when inheriting from Web.UI.Page (and not TDPage).
			UpdateData();
			AddEventHandlers();
			base.OnLoad(e);
		}

		/// <summary>
		/// Page Load method - initialises this control.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AddClientForElementHighlighting();
		}

		/// <summary>
		/// Method to call to initialise this control for a public journey.
		/// </summary>
		/// <param name="publicJourney">Public journey to render details for</param>
		/// <param name="outward">Indicates if rendering for outward or return</param>
		/// <param name="belongingPageId">Parent page PageId</param>
		/// <param name="itinerarySegment">The itinerary segment
		/// that the journey relates to - use -1 for unspecified</param>
		public void Initialise(JourneyControl.PublicJourney publicJourney,
			bool outward, int itinerarySegment)
		{
			this.useRoadJourney = false;
			this.publicJourney = publicJourney;
			this.outward = outward;
			this.belongingPageId = this.PageId;
			this.itinerarySegment = itinerarySegment;

			Initialise();
		}

		/// <summary>
		/// Method to call to initialise this control for a road journey.
		/// </summary>
		/// <param name="roadJourney">Road journey to render details for</param>
		/// <param name="journeyRequest">The related original journey request</param>
		/// <param name="outward">Indicates if rendering for outward or return</param>
		/// <param name="belongingPageId">Parent page PageId</param>
		/// <param name="itinerarySegment">The itinerary segment
		/// that the journey relates to - use -1 for unspecified</param>
		public void Initialise(JourneyControl.RoadJourney roadJourney,
			ITDJourneyRequest journeyRequest,
			bool outward, int itinerarySegment)
		{
			this.useRoadJourney = true;
			this.roadJourney = roadJourney;
			this.outward = outward;
			this.journeyRequest = journeyRequest;
			this.belongingPageId = this.PageId;
			this.itinerarySegment = itinerarySegment;

			Initialise();
		}
 
		/// <summary>
		/// Generic initialisation.
		/// </summary>
		private void Initialise()
		{
			minsText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.minutesString", TDCultureInfo.CurrentUICulture);

			minText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.minuteString", TDCultureInfo.CurrentUICulture);

			secondsText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.secondsString", TDCultureInfo.CurrentUICulture);

			hoursText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.hoursString", TDCultureInfo.CurrentUICulture);

			hourText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.hourString", TDCultureInfo.CurrentUICulture);

			everyText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.every", TDCultureInfo.CurrentUICulture);

			maxDurationText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.maxDuration", TDCultureInfo.CurrentUICulture);
                
			typicalDurationText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.typicalDuration", TDCultureInfo.CurrentUICulture);

			toolTipTextLocationLink = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.LocationLink.ToolTipText", TDCultureInfo.CurrentUICulture);

			toolTipTextDetailsLink = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.ServiceDetailsLink.ToolTipText", TDCultureInfo.CurrentUICulture);

		}

		/// <summary>
		/// OnPreRender method.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			UpdateData();
			base.OnPreRender(e);

		}

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
				this.Page.ClientScript.RegisterClientScriptBlock(typeof(JourneyReplanTableGridControl), javaScriptFileName, scriptRepository.GetScript(javaScriptFileName, javaScriptDom));                				
			}
		}

		/// <summary>
		/// Sets the data for the Repeater.
		/// </summary>
		private void UpdateData()
		{
			flight = false;

			if (useRoadJourney)
			{
				if (roadJourney == null)
				{
					detailsRepeater.DataSource = new object[0];
				}
				else
				{
					detailsRepeater.DataSource = new object[1];
				}
			}
			else
			{
				if (publicJourney == null)
				{
					detailsRepeater.DataSource = new object[0];
				}
				else
				{
					detailsRepeater.DataSource = publicJourney.Details;
					flight = ShowAirColumns();
				}
			}

			detailsRepeater.DataBind();
		}

		/// <summary>
		/// returns a bool indicating whether to show the 'air' related columns
		/// this is done on basis of whether there are any air legs in the journey(s) being displayed
		/// </summary>
		private bool ShowAirColumns()
		{
			//return false if journey is null
			if (publicJourney == null)
			{
				return false;
			}

			//check all details to see if any air modes found
			for (int i = 0; i < publicJourney.Details.Length; i++)
			{
				//air columns should be visible if the journey includes a flight leg. 
				if (publicJourney.Details[i].Mode == ModeType.Air) 
				{					
					return true;
				}	
			}

			//if method has not returned by now, then no air legs have been found so return false
			return false;
		}

		/// <summary>
		/// Method to add the event handlers to add dynamically generated buttons
		/// </summary>
		private void AddEventHandlers()
		{
			for(int i = 0; i < detailsRepeater.Items.Count; i++)
			{

				if	((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("startLocationInfoLinkControl") != null)
				{
					// Add button event handler for the Information button
					((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("startLocationInfoLinkControl")).link_Clicked +=
						new System.EventHandler(this.InformationLinkClick);
				}						

				if	((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("endLocationInfoLinkControl") != null)
				{
					// Add button event handler for the Information button
					((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("endLocationInfoLinkControl")).link_Clicked +=
						new System.EventHandler(this.InformationLinkClick);
				}						

				if	((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("modeLinkControl") != null)
				{
					// Add button event handler for the Information button
					((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("modeLinkControl")).link_Clicked +=
						new System.EventHandler(this.ServiceDetailsClick);
				}						

			}
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
		/// Event Handler for the information hyperlink.
		/// </summary>
		public void InformationLinkClick(object sender, System.EventArgs e)
		{
			HyperlinkPostbackControl link = (HyperlinkPostbackControl)sender;

			string naptan = link.CommandName;

			if( naptan != null && naptan.Length > 0 )
			{
				TDSessionManager.Current.InputPageState.AdditionalDataLocation = naptan;
                SetStopInformation(naptan);
			}

			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( belongingPageId );

			// Show the information page for the selected location.
			// Write the Transition Event
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.StopInformation;
		}

		/// <summary>
		/// Event Handler for the information button.
		/// </summary>
		public void ServiceDetailsClick(object sender, System.EventArgs e)
		{
			HyperlinkPostbackControl link = (HyperlinkPostbackControl)sender;
			int selectedJourneyLeg = Int32.Parse(link.CommandName, CultureInfo.InvariantCulture);

			TDJourneyViewState journeyViewState = TDItineraryManager.Current.JourneyViewState;
			journeyViewState.SelectedJourneyLeg = selectedJourneyLeg;
			journeyViewState.SelectedIntermediateItinerarySegment = itinerarySegment;
			journeyViewState.CallingPageID = belongingPageId;
	
			TDSessionManager.Current.Session[SessionKey.JourneyDetailsOutward] = outward;
		
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( belongingPageId );

			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.ServiceDetails;
		}

		/// <summary>
		/// Set up user controls in the repeater
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void detailsRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item :
				case ListItemType.AlternatingItem :
					
					// Find the checkBox and bind its checkedchanged event
					ScriptableCheckBox s = (ScriptableCheckBox)e.Item.FindControl("checkJourneyElement");
					s.Enabled = !TDSessionManager.Current.JourneyViewState.ConfirmationMode;
					s.CheckedChanged += new EventHandler(OnJourneyElementSelectionChanged);

					vehicleFeaturesControl = e.Item.FindControl("vehicleFeaturesControl") as VehicleFeaturesControl;

					if	(!useRoadJourney)
					{
						vehicleFeaturesControl.Features = ((PublicJourneyDetail)e.Item.DataItem).GetVehicleFeatures();
					}
					else
					{
						vehicleFeaturesControl.Visible = false;
					}

					// Set up leg instructions control
					legInstructionsControl = (LegInstructionsControl)e.Item.FindControl("legInstructionsControl");
					
					if  (legInstructionsControl != null)
					{
						if (useRoadJourney)
						{
							legInstructionsControl.RoadJourney = roadJourney;
						}

						// Set properties for the control appropriate to the public journey data in the row
						legInstructionsControl.JourneyLeg = (PublicJourneyDetail)e.Item.DataItem;
						legInstructionsControl.PrinterFriendly = true;	// (we don't want any links to be live)
					}

					startLocationLabelControl = (Label)e.Item.FindControl("startLocationLabelControl");

					if	(useRoadJourney)
					{
						startLocationLabelControl.Text = GetFromLocation();
					}
					else
					{
						startLocationLabelControl.Text = GetFromLocation((PublicJourneyDetail)e.Item.DataItem);
					}

					endLocationLabelControl = (Label)e.Item.FindControl("endLocationLabelControl");

					if	(useRoadJourney)
					{
						endLocationLabelControl.Text = GetToLocation();
					}
					else
					{
						endLocationLabelControl.Text = GetToLocation((PublicJourneyDetail)e.Item.DataItem);
					}

					modeLinkLabel = (Label)e.Item.FindControl("modeLinkLabel");

					if	(!useRoadJourney)
					{

						if	(HasServiceDetails((PublicJourneyDetail)e.Item.DataItem))
						{
							modeLinkLabel.Text = GetMode((PublicJourneyDetail)e.Item.DataItem, false);
							modeLinkLabel.Visible = true;
						}
						else
						{
							modeLinkLabel.Visible = false;
						}
					}
					else
					{
						modeLinkLabel.Visible = false;
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
		
		/// <summary>
		/// Returns the html table cell entry for the given column
		/// </summary>
		public string HeaderItem(int column)
		{
			if (!ColumnVisible(column))
			{
				return String.Empty;
			}

			string headerItemText = Global.tdResourceManager.GetString("JourneyReplanTableGridControl.HeaderItemText" 
				+ column, TDCultureInfo.CurrentUICulture);

			return "<th class=\"jdtheader" + column.ToString(CultureInfo.InvariantCulture) + "\">" + headerItemText + "</th>";
		}

		/// <summary>
		/// Returns whether the cell should be visible
		/// </summary>
		public bool ColumnVisible(int column)
		{
			switch (column)
			{
				case 3:
				case 7:
					return flight;
				case 10:
					return true;
				default:
					return true;
			}
		}

		public string CellStart(int column)
		{
			return (ColumnVisible(column) ? "<td class=\"jdtbody" + column + "\" >" : String.Empty);
		}

		public string CellEnd(int column)
		{
			return (ColumnVisible(column) ? "</td>" : String.Empty);
		}

		/// <summary>
		/// Returns the details text for the specified row and column
		/// </summary>
		public string DetailsItem(int column, int indexRow)
		{
			if (!ColumnVisible(column))
			{
				return string.Empty;
			}

			string detailsItemText = string.Empty;

			switch (column)
			{
				case 1:
					if  (useRoadJourney)
					{
						detailsItemText = FormatModeDetails();
					}
					else
					{
						detailsItemText = FormatModeDetails(publicJourney.Details[indexRow]);
					}
					break;
				
				case 3:
					detailsItemText = GetCheckInTime(publicJourney.Details[indexRow]);
					break;

				case 4:
					if  (useRoadJourney)
					{
						detailsItemText = GetDepartTime();
					}
					else
					{
						detailsItemText = GetDepartTime(publicJourney.Details[indexRow], indexRow);
					}
					break;


				case 6:
					if  (useRoadJourney)
					{
						detailsItemText = GetArriveTime();
					}
					else
					{
						detailsItemText = GetArriveTime(publicJourney.Details[indexRow], indexRow);
					}
					break;
				
				case 7:
					detailsItemText = GetExitTime(publicJourney.Details[indexRow]);
					break;

				case 8:
					if (!useRoadJourney)
					{
						detailsItemText = GetDisplayNotes(publicJourney.Details[indexRow]);
					}
					break;

				case 9:
					if  (useRoadJourney)
					{
						detailsItemText = FormatDurationDetails();
					}
					else
					{
						detailsItemText = FormatDurationDetails(publicJourney.Details[indexRow]);
					}
					break;
			}

			return detailsItemText;
		}

		#region Rendering of checkBoxes for journey element selection and highlighting
		/// <summary>
		/// Return a unique id for specific checkBox being rendered
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>CheckBox Id</returns>
		public string GetCheckBoxId(int index)
		{
			string checkBoxId = "checkbox" + index;
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
		/// Return a unique id for the row being rendered
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Table Row Id</returns>
		public string GetHighlightRowId(int index)
		{

			string rowId = "highlightRow" + index;
			return rowId;
		}

		/// <summary>
		/// Returns Class style for the row being rendered
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Class style for the table row</returns>
		public string GetHighlightRowClass(int index)
		{	
			if (IsIndexForReplan(index))
			{
				return "jdtrowhighlight";
			}
			else
			{
				return "";
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
			if (index >= ((ReplanPageState)TDSessionManager.Current.InputPageState).ReplanStartJourneyDetailIndex && index <= ((ReplanPageState)TDSessionManager.Current.InputPageState).ReplanEndJourneyDetailIndex)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

		/// <summary>
		/// Returns a formatted string containing mode type, service details
		/// (if present) and frequency (if leg is a frequency leg)
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing mode, service and frequency details</returns>
		public string FormatModeDetails(PublicJourneyDetail detail)
		{
			StringBuilder output = new StringBuilder();
			
			// if service details are available, mode will be rendered 
			//  as a hyperlink to ServiceDetails page, otherwise it is 
			//  to be included here as plain text ...

			if	(!HasServiceDetails(detail))    
			{
				output.Append(GetMode(detail, false));
			}
			
			string services = GetServices(detail);
			
			if (services.Length > 0) 
			{
				output.Append("<br/>" + GetServices(detail));
			}
			
			PublicJourneyFrequencyDetail frequencyDetail = detail as PublicJourneyFrequencyDetail;
			if (frequencyDetail != null)
			{ 
				
				if	(frequencyDetail.MinFrequency == frequencyDetail.MaxFrequency)
				{
					output.Append("<br/>" + everyText + " " + frequencyDetail.MinFrequency + minsText);
				}
				else
				{
					output.Append("<br/>" + everyText + " " + frequencyDetail.MinFrequency
						+ "-" + frequencyDetail.MaxFrequency + minsText);
				}
			}
			
			return output.ToString();
		}

		/// <summary>
		/// Returns a formatted string containing road journey mode type
		/// </summary>
		/// <returns>Formatted string containing road journey mode type</returns>
		public string FormatModeDetails()
		{
			string resourceManagerKey = "TransportMode." + roadJourney.GetUsedModes()[0].ToString();
			return Global.tdResourceManager.GetString(resourceManagerKey, TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// If the supplied leg is not a rail leg and services details are
		/// present, returns a string containing every service number
		/// delimited by commas. Otherwise an empty string is returned. 
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing service numbers or empty string</returns>
		public string GetServices(PublicJourneyDetail publicJourneyDetail) 
		{
			if (publicJourneyDetail.Mode != ModeType.Rail &&
                publicJourneyDetail.Mode != ModeType.RailReplacementBus && 
				publicJourneyDetail.Services != null && 
				publicJourneyDetail.Services.Length > 0) 
			{
				StringBuilder serviceDetailsText = new StringBuilder();
				for (int count=0; count < publicJourneyDetail.Services.Length; count++) 
				{
					serviceDetailsText.Append(publicJourneyDetail.Services[count].ServiceNumber);
					if (count < publicJourneyDetail.Services.Length -1) 
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
		/// Returns the text for the transport mode of the specified leg.
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Mode string formatted.</returns>
		public string GetMode(PublicJourneyDetail publicJourneyDetail, bool lowercase)
		{
			string resourceManagerKey = (lowercase ? "TransportModeLowerCase." : "TransportMode.")
				+ publicJourneyDetail.Mode.ToString();
            
			return Global.tdResourceManager.GetString(
				resourceManagerKey, TDCultureInfo.CurrentUICulture);

		}

		public string GetNaptan(int index, bool startLocation)
		{
			if	(useRoadJourney)
			{
				return GetNaptan(null, startLocation);
			}
			else
			{
				return GetNaptan(publicJourney.Details[index], startLocation);
			}
		}


		/// <summary>
		/// Returns the naptan of the start location
		/// </summary>
		/// <param name="legDetails">Current data item being rendered.</param>
		/// <returns>Naptan of the start location</returns>
		public string GetNaptan(PublicJourneyDetail publicJourneyDetail, bool startLocation)
		{
			TDNaptan[] naptans = null;
			
			if	(useRoadJourney)
			{
				if	(startLocation)
				{
					if (outward)
					{
						if	(journeyRequest.OriginLocation.RequestPlaceType == RequestPlaceType.NaPTAN)
						{
							naptans = journeyRequest.OriginLocation.NaPTANs;
						}
					}
					else if	(journeyRequest.ReturnOriginLocation.RequestPlaceType == RequestPlaceType.NaPTAN)
					{
						naptans = journeyRequest.ReturnOriginLocation.NaPTANs;
					}
				}
				else
				{
					if (outward)
					{
						if	(journeyRequest.DestinationLocation.RequestPlaceType == RequestPlaceType.NaPTAN)
						{
							naptans = journeyRequest.DestinationLocation.NaPTANs;
						}
					}
					else if	(journeyRequest.ReturnDestinationLocation.RequestPlaceType == RequestPlaceType.NaPTAN)
					{
						naptans = journeyRequest.ReturnDestinationLocation.NaPTANs;
					}
				}
			}
			else
			{
				if	(startLocation)
				{
					naptans = publicJourneyDetail.LegStart.Location.NaPTANs;
				}
				else
				{
					naptans = publicJourneyDetail.LegEnd.Location.NaPTANs;
				}
			}

			string firstNaptan = string.Empty; 

			if  (naptans != null && naptans.Length > 0  && naptans[0].Naptan != null && naptans[0].Naptan.Length > 0)
			{
				if	(!naptans[0].Naptan.Equals(originNaptanString) && !naptans[0].Naptan.Equals(destinationNaptanString)) 
				{
					firstNaptan = naptans[0].Naptan;
				}
			}

			return firstNaptan;
		}

		/// <summary>
		/// Returns the From Location string for a public journey
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted from location string</returns>
		public string GetFromLocation(PublicJourneyDetail detail)
		{
			return detail.LegStart.Location.Description;
		}

		/// <summary>
		/// Returns the Checkin Time string for a public journey
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted depart time string.</returns>
		public string GetCheckInTime(PublicJourneyDetail detail)
		{
			if (detail.Mode == ModeType.Air && detail.CheckInTime != null)
			{
				return detail.CheckInTime.ToString("HH:mm");
			}
			else
			{
				return "&nbsp;";
			}
		}

		/// <summary>
		/// Returns the Depart Time string for a public journey
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted depart time string.</returns>
		public string GetDepartTime (PublicJourneyDetail detail, int indexRow)
		{	
			
			// Don't display depart time for Walk or other continuous leg
			// or for frequency based services, unless it is the first leg.
			if  ( indexRow != 0 && (detail is PublicJourneyContinuousDetail || detail is PublicJourneyFrequencyDetail ))
			{
				return "&nbsp;";
			}
			else if (detail.Mode == ModeType.Air)
			{
				return detail.FlightDepartDateTime.ToString("HH:mm");
			}
			else
			{
				return detail.LegStart.DepartureDateTime.ToString("HH:mm");
			}
		}


		/// <summary>
		/// Returns the Depart Time string for a road journey
		/// </summary>
		/// <returns>Formatted depart time string.</returns>
		public string GetDepartTime()
		{
			bool arriveBefore;
			TDDateTime time;

			if (outward)
			{
				arriveBefore = journeyRequest.OutwardArriveBefore;
				time = journeyRequest.OutwardDateTime[0];
			}
			else
			{
				arriveBefore = journeyRequest.ReturnArriveBefore;
				time = journeyRequest.ReturnDateTime[0];
			}

			TimeSpan duration = new TimeSpan(0, 0, (int)roadJourney.TotalDuration);
			time = arriveBefore ? time.Subtract(duration) : time;

			return time.ToString("HH:mm");
		}

		/// <summary>
		/// Returns the To Location string for a public journey
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted to location string.</returns>
		public string GetToLocation(PublicJourneyDetail detail)
		{
			return detail.LegEnd.Location.Description;
		}

		/// <summary>
		/// Returns the To Location string for a road journey
		/// </summary>
		/// <returns>Formatted to location string.</returns>
		public string GetToLocation()
		{
			string description = String.Empty;

			if (outward)
			{
				description = journeyRequest.DestinationLocation.Description;
			}
			else
			{
				description = journeyRequest.ReturnDestinationLocation.Description;
			}

			if (description.Length > 27)
			{
				return description.Substring(0, 25) + "...";
			}
			else
			{
				return description;
			}
		}

		/// <summary>
		/// Returns the To Location string for a road journey
		/// </summary>
		/// <returns>Formatted to location string.</returns>
		public string GetFromLocation()
		{
			string description = String.Empty;

			if (outward)
			{
				description = journeyRequest.OriginLocation.Description;
			}
			else
			{
				description = journeyRequest.ReturnOriginLocation.Description;
			}

			if (description.Length > 27)
			{
				return description.Substring(0, 25) + "...";
			}
			else
			{
				return description;
			}
		}

		/// <summary>
		/// Returns the arrive time string for a public journey
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted from arrive timestring.</returns>
		public string GetArriveTime(PublicJourneyDetail detail, int indexRow)
		{
			
			// Don't display arrive time for Walk leg
			if (indexRow != publicJourney.Details.Length -1 && ( detail is PublicJourneyContinuousDetail  || detail is PublicJourneyFrequencyDetail))
			{
				return "&nbsp;";
			}
			else if (detail.Mode == ModeType.Air)
			{
				return detail.FlightArriveDateTime.ToString("HH:mm");
			}
			else 
			{
				// Round the arrival time
				return detail.LegEnd.ArrivalDateTime.ToString("HH:mm");
			}
		}

		/// <summary>
		/// Returns the arrive time string for a road journey
		/// </summary>
		/// <returns>Formatted from arrive timestring.</returns>
		public string GetArriveTime()
		{
			bool arriveBefore;
			TDDateTime time;

			if (outward)
			{
				arriveBefore = journeyRequest.OutwardArriveBefore;
				time = journeyRequest.OutwardDateTime[0];
			}
			else
			{
				arriveBefore = journeyRequest.ReturnArriveBefore;
				time = journeyRequest.ReturnDateTime[0];
			}

			TimeSpan duration = new TimeSpan(0, 0, (int)roadJourney.TotalDuration);
			time = arriveBefore ? time : time.Add(duration);

			return time.ToString("HH:mm");
		}

		/// <summary>
		/// Returns the Exit Time string for a public journey
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted depart time string.</returns>
		public string GetExitTime(PublicJourneyDetail detail)
		{
			if (detail.Mode == ModeType.Air && detail.ExitTime != null)
			{
				return detail.ExitTime.ToString("HH:mm");
			}
			else
			{
				return "&nbsp;";
			}
		}

		/// <summary>
		/// Returns the Exit Time string for a road journey
		/// </summary>
		/// <returns>Formatted depart time string.</returns>
		public string GetExitTime()
		{
			return "&nbsp;";
		}

		/// <summary>
		/// If the specified leg is a frequency leg, returns a formatted 
		/// string containing maximum and typical duration times. Otherwise
		/// returns the duration time for the leg.
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing duration details</returns>
		public string FormatDurationDetails(PublicJourneyDetail detail) 
		{
			if (detail is PublicJourneyFrequencyDetail) 
			{
				return "<br/>" + GetMaxJourneyDurationText(detail) +
					"<br/>" + GetTypicalDurationText(detail);
			} 
			else
			{
				return GetDuration(detail.Duration);
			}
		}

		/// <summary>
		/// Returns the Duration string for a road journey
		/// </summary>
		/// <returns>Formatted string containing duration details</returns>
		public string FormatDurationDetails() 
		{
			return GetDuration(roadJourney.TotalDuration);
		}

		/// <summary>
		/// Returns formatted string containing the maximum duration of
		/// a supplied frequency leg, or empty string if supplied leg is
		/// not a frequency leg.
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing the maximum duration or empty string</returns>
		public string GetMaxJourneyDurationText(PublicJourneyDetail detail)
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
		public string GetTypicalDurationText(PublicJourneyDetail detail)
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
		/// Returns the Duration string
		/// </summary>
		/// <param name="durationInSeconds">Total duration in seconds</param>
		/// <returns>Formatted duration string</returns>
		public string GetDuration(long durationInSeconds)
		{
			// Get the minutes
			double durationInMinutes = durationInSeconds / 60;

			// Check to see if seconds is less than 30 seconds.
			if( durationInMinutes / 60.0  < 1.00 &&
				durationInMinutes % 60.0 < 0.5 )
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
				string hourString = hours > 1 ? hoursText : hourText;

				// If greater than 1 minute - retrive "minutes", if 1 or less, retrieve "minute"
				string minuteString = minutes > 1 ? minsText : minText;
        
				string formattedString = string.Empty;

				if(hours > 0)
					formattedString += hours + " " + hourString + " ";

				formattedString += minutes + " " + minuteString;

				return formattedString;
			}
		}

		/// <summary>
		/// Checks if current journey detail has additional service details to be dispalyed.
		/// Currently based on detail mode only - returning true for rail or rail replacement bus.
		/// </summary>
		private bool HasServiceDetails(PublicJourneyDetail detail)
		{
			return (detail.Mode == ModeType.Rail || detail.Mode == ModeType.RailReplacementBus);
		}


		/// <summary>
		/// Returns whether the leg start station name should be rendered as a hyperlink
		/// </summary>
		public bool StartLocationInfoAvailable(PublicJourneyDetail detail)
		{
			// The location name is rendered as a hyperlink if 
			// the page is not printable AND a naptan exists for 
			// the start location of leg being currently rendered.

			return (GetNaptan(detail, true).Length > 0); 
		}

		/// <summary>
		/// Returns whether the end start station name should be rendered as a hyperlink
		/// </summary>
		public bool EndLocationInfoAvailable(PublicJourneyDetail detail)
		{
			// The location name is rendered as a hyperlink if 
			// the page is not printable AND a naptan exists for 
			// the start location of leg being currently rendered.

			return (GetNaptan(detail, false).Length > 0); 
		}

		/// <summary>
		/// Returns the command name that should be associated with the map button.
		/// </summary>
		/// <param name="publicJourneyDetail">Current item being rendered.</param>
		/// <returns>Returns the command name.</returns>
		//public string GetCommandName(PublicJourneyDetail publicJourneyDetail)
		//{
		//	return IndexOf(publicJourneyDetail).ToString(TDCultureInfo.CurrentUICulture);
		//}
		public string GetCommandName(int index)
		{
			return index.ToString(TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Gets Display Notes
		/// </summary>
		/// <param name="detail">Public Journey Detail</param>
		/// <returns>HTML Formatted Notes</returns>
		public string GetDisplayNotes(PublicJourneyDetail detail)
		{
			NotesDisplayAdapter notesDisplayAdapter = new NotesDisplayAdapter();

			return	notesDisplayAdapter.GetDisplayableNotes(publicJourney, detail); 
		}

		/// <summary>
		/// read only property
		/// Gets the selected journey indexes for the element to be replanned
		/// </summary>
		public ArrayList SelectedJourneyElementValues
		{
			get 
			{
				ArrayList results = new ArrayList();

				foreach (RepeaterItem ri in detailsRepeater.Items)
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
		/// Get property - Public journey object to give information about what legs on this journey
		/// </summary>
		public JourneyControl.PublicJourney PublicJourney
		{
			get { return publicJourney; }
		}

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

		/// <summary>
		/// Method for wiring up events
		/// </summary>
		private void ExtraWiringEvents()
		{
			detailsRepeater.ItemCreated += new RepeaterItemEventHandler(detailsRepeater_ItemCreated);
		}
		#endregion
	}
}

