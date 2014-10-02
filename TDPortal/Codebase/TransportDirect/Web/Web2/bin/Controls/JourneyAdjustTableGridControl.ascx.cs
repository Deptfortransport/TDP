// *********************************************** 
// NAME                 : JourneyAdjustTableGridControl.ascx.cs 
// AUTHOR               : Paul Cross
// DATE CREATED         : 14/02/2006
// DESCRIPTION          : A user control to display details of a given journey that can be adjusted
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyAdjustTableGridControl.ascx.cs-arc  $ 
//
//   Rev 1.5   Mar 21 2013 10:13:04   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.4   Jan 13 2009 11:40:46   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Oct 13 2008 16:41:42   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Sep 26 2008 13:42:10   jfrank
//Amended to make XHTML transitional
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:21:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:08   mturner
//Initial revision.
//
//   Rev 1.6   Mar 20 2006 18:06:14   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Mar 14 2006 13:20:26   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Mar 08 2006 16:02:54   RGriffith
//FxCop Suggested Changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Mar 02 2006 14:59:44   pcross
//Added 'please select from dropdown' type entry for location and timings dropdowns
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 24 2006 10:40:36   pcross
//Removed network map links control.  Added tick in selected column
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 16 2006 10:25:02   pcross
//Interim check in
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 10 2006 10:41:42   pcross
//Initial revision.

using System;
using System.Text;
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
	/// User control to select details of a public journey in a tabular format for Adjust.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class JourneyAdjustTableGridControl : TDUserControl
	{
		protected LegInstructionsControl legInstructionsControl;
		protected Label modeLinkLabel;
		protected Label startLocationLabelControl;
		protected Label endLocationLabelControl;
		protected VehicleFeaturesControl vehicleFeaturesControl;
		protected TDImage selectedTick;

		private JourneyControl.PublicJourney publicJourney;
		private PageId belongingPageId = PageId.Empty;
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
		
		private const string originNaptanString = "Origin";
		private const string destinationNaptanString = "Destination";
		private const string highlightedClass = "jdtrowhighlight";
		private const string UNREFINED_TEXT = "unrefined";

		/// <summary>
		/// Constructor
		/// </summary>
		public JourneyAdjustTableGridControl() : base()
		{
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		/// <summary>
		/// Page Load method - initialises this control.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Need to call UpdateData to populate the Repeater. This isn't required
			// when inheriting from Web.UI.Page (and not TDPage).
			UpdateData();

			AddClientForElementHighlighting();
		}

		/// <summary>
		/// Method to call to initialise this control for a public journey.
		/// </summary>
		/// <param name="publicJourney">Public journey to render details for</param>
		/// <param name="outward">Indicates if rendering for outward or return</param>
		/// <param name="belongingPageId">Parent page PageId</param>
		/// that the journey relates to - use -1 for unspecified</param>
		public void Initialise(JourneyControl.PublicJourney publicJourney, bool outward)
		{
			this.publicJourney = publicJourney;
			this.outward = outward;
			this.belongingPageId = this.PageId;

			Initialise();
		}

		/// <summary>
		/// Generic initialisation.
		/// </summary>
		private void Initialise()
		{
			minsText = GetResource("langStrings", "JourneyDetailsTableControl.minutesString");
			minText = GetResource("langStrings", "JourneyDetailsTableControl.minuteString");
			secondsText = GetResource("langStrings", "JourneyDetailsTableControl.secondsString");
			hoursText = GetResource("langStrings", "JourneyDetailsTableControl.hoursString");
			hourText = GetResource("langStrings", "JourneyDetailsTableControl.hourString");
			everyText = GetResource("langStrings", "JourneyDetailsControl.every");
			maxDurationText = GetResource("langStrings", "JourneyDetailsControl.maxDuration");
			typicalDurationText = GetResource("langStrings", "JourneyDetailsControl.typicalDuration");
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
				string javaScriptFileName = "JourneyAdjustElementSelection";
				string javaScriptDom = tdPage.JavascriptDom;
				ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
				// Output reference to necessary JavaScript file from the ScriptRepository
				this.Page.ClientScript.RegisterClientScriptBlock(typeof(JourneyAdjustTableGridControl), javaScriptFileName, scriptRepository.GetScript(javaScriptFileName, javaScriptDom));                				
			}
		}

		/// <summary>
		/// Sets the data for the Repeater.
		/// </summary>
		private void UpdateData()
		{
			flight = false;

			if (publicJourney == null)
			{
				detailsRepeater.DataSource = new object[0];
			}
			else
			{
				detailsRepeater.DataSource = publicJourney.Details;
				flight = ShowAirColumns();
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
					
					vehicleFeaturesControl = e.Item.FindControl("vehicleFeaturesControl") as VehicleFeaturesControl;
					vehicleFeaturesControl.Features = ((PublicJourneyDetail)e.Item.DataItem).GetVehicleFeatures();

					// Set up leg instructions control
					legInstructionsControl = (LegInstructionsControl)e.Item.FindControl("legInstructionsControl");
					
					if  (legInstructionsControl != null)
					{
						// Set properties for the control appropriate to the public journey data in the row
						legInstructionsControl.JourneyLeg = (PublicJourneyDetail)e.Item.DataItem;
						legInstructionsControl.PrinterFriendly = true;	// (we don't want any links to be live)
					}

					startLocationLabelControl = (Label)e.Item.FindControl("startLocationLabelControl");
					startLocationLabelControl.Text = GetFromLocation((PublicJourneyDetail)e.Item.DataItem);

					endLocationLabelControl = (Label)e.Item.FindControl("endLocationLabelControl");

					// Use start location of next journey unless there is not next journey
					// in which case use end location of current.
					// This ensures that we always have matching locations as descriptions of the same Naptan
					// can vary between legs.
					endLocationLabelControl.Text = GetToLocation(e.Item.ItemIndex);

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

					// Assign tick image url
					selectedTick = (TDImage)e.Item.FindControl("selectedTick");
					if (selectedTick != null)
					{
						selectedTick.ImageUrl = GetResource("JourneyAdjustTableGridControl.Tick.ImageURL");
						selectedTick.AlternateText = GetResource("JourneyAdjustTableGridControl.Tick.AlternateText");
						selectedTick.ToolTip = GetResource("JourneyAdjustTableGridControl.Tick.AlternateText");

					}

					break;

				default :
					break;
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

			string headerItemText = GetResource("JourneyAdjustTableGridControl.HeaderItemText" + column);

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

		/// <summary>
		/// Method to return HTML string which represents the start of a table data cell for the given column
		/// Handles visibility of column
		/// </summary>
		/// <param name="column">Column to get table data start tag for</param>
		/// <returns>TD open tag</returns>
		public string CellStart(int column)
		{
			return (ColumnVisible(column) ? "<td class=\"jdtbody" + column + "\" >" : String.Empty);
		}
        
		/// <summary>
		/// Method to return HTML string which represents the end of a table data cell for the given column
		/// Handles visibility of column
		/// </summary>
		/// <param name="column">Column to get table data end tag for</param>
		/// <returns>TD close tag</returns>
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
					detailsItemText = FormatModeDetails(publicJourney.Details[indexRow]);
					break;
				
				case 3:
					detailsItemText = GetCheckInTime(publicJourney.Details[indexRow]);
					break;

				case 4:
					detailsItemText = GetDepartTime(publicJourney.Details[indexRow], indexRow);
					break;


				case 6:
					detailsItemText = GetArriveTime(publicJourney.Details[indexRow], indexRow);
					break;
				
				case 7:
					detailsItemText = GetExitTime(publicJourney.Details[indexRow]);
					break;

				case 8:
					detailsItemText = GetDisplayNotes(publicJourney.Details[indexRow]);
					break;

				case 9:
					detailsItemText = FormatDurationDetails(publicJourney.Details[indexRow]);
					break;
			}

			return detailsItemText;
		}

		#region Highlighting selected part of journey to adjust

		/// <summary>
		/// Return a unique id for the row being rendered
		/// </summary>
		/// <param name="index">Current item being rendered.</param>
		/// <returns>Table Row Id</returns>
		public string GetHighlightRowId(int index)
		{
			string rowId = "highlightRow" + index;
			return rowId;
		}

		/// <summary>
		/// Returns Class style for the row being rendered
		/// </summary>
		/// <param name="index">Current item being rendered.</param>
		/// <returns>Class style for the table row</returns>
		public string GetHighlightRowClass(int index)
		{	
			if (IsIndexForAdjust(index))
				return highlightedClass;
			else
				return String.Empty;
		}

		/// <summary>
		/// Returns "true" if the line in the repeater is a user-selected line. Can thus be used to
		/// make items visible on a selected line.
		/// </summary>
		/// <param name="index">Current item being rendered.</param>
		/// <returns>"true" if the line in the repeater is a user-selected line.</returns>
		public string GetVisibility(int index)
		{	
			if (IsIndexForAdjust(index))
				return "display: inline";
			else
				return "display: none";
		}

		/// <summary>
		/// Returns whether current item being rendered is in the selected area
		/// </summary>
		/// <param name="index">Current item being rendered.</param>
		/// <returns>true or false</returns>
		private bool IsIndexForAdjust(int index)
		{
			bool isIndexForAdjust = false;

			TDCurrentAdjustState tsj = TDSessionManager.Current.CurrentAdjustState;
			if (tsj != null)
			{

				if (tsj.SelectedRouteNode > 0)
				{
					// Only mark as for adjust if there is a selection for both timings and locations
					if (tsj.SelectedAdjustLocationsDropdownValue.ToString() == UNREFINED_TEXT ||
						tsj.SelectedAdjustTimingsDropdownValue.ToString() == UNREFINED_TEXT)
					{
						return false;
					}

					if (tsj.SelectedRouteNodeSearchType == false)	// Leave after
					{
						// Is index within range from selected leg to end
						if (index >= tsj.SelectedRouteNode && index <= publicJourney.Details.Length - 1)
							isIndexForAdjust = true;
						else
							isIndexForAdjust = false;
					}
					else	// Arrive before
					{
						// Is index within range from start to selected leg
						if (index >= 0 && index <= tsj.SelectedRouteNode - 1)
							isIndexForAdjust = true;
						else
							isIndexForAdjust = false;
					}
				}
			}

			return isIndexForAdjust;
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
				output.Append("<br />" + GetServices(detail));
			}
			
			PublicJourneyFrequencyDetail publicJourneyFrequencyDetail = (detail as PublicJourneyFrequencyDetail);
			if (publicJourneyFrequencyDetail != null) 
			{
				if	(publicJourneyFrequencyDetail.MinFrequency == publicJourneyFrequencyDetail.MaxFrequency)
				{
					output.Append("<br />" + everyText + " " + publicJourneyFrequencyDetail.MinFrequency + minsText);
				}
				else
				{
					output.Append("<br />" + everyText + " " + publicJourneyFrequencyDetail.MinFrequency
						+ "-" + publicJourneyFrequencyDetail.MaxFrequency + minsText);
				}
			}
			
			return output.ToString();
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
		public string GetMode(PublicJourneyDetail publicJourneyDetail, bool lowerCase)
		{
			string resourceManagerKey = (lowerCase ? "TransportModeLowerCase." : "TransportMode.")
				+ publicJourneyDetail.Mode.ToString();
            
			return GetResource("langStrings", resourceManagerKey);

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
				return DisplayFormatAdapter.StandardTimeFormat(detail.CheckInTime);
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
				return DisplayFormatAdapter.StandardTimeFormat(detail.FlightDepartDateTime);
			}
			else
			{
				return DisplayFormatAdapter.StandardTimeFormat(detail.LegStart.DepartureDateTime);
			}
		}

		/// <summary>
		/// Returns the To Location string for a public journey.
		/// This is given by the Start location of the next leg unless it is the last leg, which means we use
		/// the end location of the current leg.
		/// Exception: if we are arriving at an interchange (short walk removed) we put the to location as
		/// the end location of the current leg. Thus we can see a difference between the end of the last leg
		/// and start of the new.
		/// </summary>
		/// <param name="itemIndex">Index of current item being rendered.</param>
		/// <returns>Formatted to location string.</returns>
		public string GetToLocation(int currentItemIndex)
		{
			// Get the index of the detail object
			if (currentItemIndex == publicJourney.Details.Length - 1)
			{
				return publicJourney.Details[currentItemIndex].LegEnd.Location.Description;
			}
			else
			{
				ResultsAdapter resultsAdapter = new ResultsAdapter();
				if (resultsAdapter.PreInterchange(publicJourney, currentItemIndex))
				{
					return publicJourney.Details[currentItemIndex].LegEnd.Location.Description;
				}
				else
				{
					return publicJourney.Details[currentItemIndex + 1].LegStart.Location.Description;
				}
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
				return DisplayFormatAdapter.StandardTimeFormat(detail.FlightArriveDateTime);
			}
			else 
			{
				// Round the arrival time
				return DisplayFormatAdapter.StandardTimeFormat(detail.LegEnd.ArrivalDateTime);
			}
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
				return DisplayFormatAdapter.StandardTimeFormat(detail.ExitTime);
			}
			else
			{
				return "&nbsp;";
			}
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
				return "<br />" + GetMaxJourneyDurationText(detail) +
					"<br />" + GetTypicalDurationText(detail);
			} 
			else
			{
				return GetDuration(detail.Duration);
			}
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
		public string GetTypicalDurationText(PublicJourneyDetail detail)
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
		/// Checks if current journey detail has additional service details to be displayed.
		/// Currently based on detail mode only - returning true for rail or rail replacement bus.
		/// </summary>
		private bool HasServiceDetails(PublicJourneyDetail detail)
		{
			return (detail.Mode == ModeType.Rail || detail.Mode == ModeType.RailReplacementBus);
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

		/// <summary>
		/// Read only property to get the summary text for the main table in this page
		/// </summary>
		protected string GetMainTableSummary
		{
			get { return GetResource("JourneyAdjustTableGridControl.MainTable.SummaryText"); }
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

