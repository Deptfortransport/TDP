// *********************************************** 
// NAME                 : SummaryResultTableControl.ascx.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 06/08/2003 
// DESCRIPTION			: A custom user control to
// display summary journey results in a tabular format.
// The control allows the user to select any of the rows
// in the table. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/SummaryResultTableControl.ascx.cs-arc  $
//
//   Rev 1.5   Mar 04 2010 17:03:30   pghumra
//Updated tracking functionality
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.4   Nov 18 2008 15:31:04   jfrank
//Updated for after code review
//Resolution for 5146: WAI AAA compliance work (CCN 474)
//
//   Rev 1.3   Oct 17 2008 11:55:26   build
//Automatically merged from branch for stream0093
//
//   Rev 1.2.1.0   Oct 15 2008 13:04:12   jfrank
//Updated for XHTML compliance
//Resolution for 93: Stream IR for Del 10.4 maintenance fixes
//
//   Rev 1.2   Mar 31 2008 13:23:10   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:06   mturner
//Initial revision.
//
//   Rev 1.62   Feb 23 2006 19:16:52   build
//Automatically merged from branch for stream3129
//
//   Rev 1.61.1.0   Jan 10 2006 15:27:36   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.61   May 11 2005 14:37:10   jbroome
//Radio buttons and "Select" column heading not displayed when only one results row.
//Resolution for 2481: Remove "Select" button from journey summary tables when only one journey result displayed
//
//   Rev 1.60   Mar 18 2005 11:17:28   rscott
//Fixed bug outward only journey result processing
//
//   Rev 1.59   Mar 08 2005 11:30:36   rscott
//DEL7 - bug fixed with returnDateTime / outwardDateTime
//
//   Rev 1.58   Mar 01 2005 16:27:12   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.57   Sep 08 2004 12:14:02   passuied
//Added extra check to do initialisation if not in FindAMode
//Resolution for 1530: Find a choice - Details page does not match the journey
//
//   Rev 1.56   Aug 09 2004 18:32:22   RHopkins
//IR1141 Change to the way that the control's viewstate is populated into the SessionManager's JourneyViewState.  This population needs to be done during the LoadViewState stage so that the values are available to all of the other controls and the parent page.
//
//   Rev 1.55   Jun 23 2004 14:15:24   RHopkins
//Corrected styles used for Printable pages
//
//   Rev 1.54   Jun 16 2004 13:41:50   RHopkins
//Correction to currentRow handling
//
//   Rev 1.53   Jun 08 2004 16:03:56   RHopkins
//Corrected state-handling for change of Itinerary mode.
//
//   Rev 1.52   May 26 2004 11:21:02   jbroome
//Only reset JourneyMapState if selecting different journey, not everytime the control is loaded.
//
//   Rev 1.51   May 20 2004 14:32:06   rgreenwood
//Back Button Enhancement: Control now handles currentRow selection, and stores the value in ViewState object.
//Code from OnPageLoad moved into new SetJourneyViewState method.
//
//   Rev 1.50   May 13 2004 16:55:48   acaunt
//Re-added missing delegate
//
//   Rev 1.49   May 13 2004 15:11:46   acaunt
//Bug fix to stop null pointer exceptions when dealing with single journeys
//
//   Rev 1.48   May 13 2004 13:23:04   acaunt
//Moved JourneyLineSummary formatting logic into FormattedJourneyLineSummary. 
//Added an extra convenience method IsSelectedIndex
//Merged OnLoad and Page_Load methods
//
//   Rev 1.47   May 13 2004 11:30:08   rgreenwood
//Back Button Enhancements: Changed Initialize, LoadViewState & SaveViewState to handle current row selection. Moved code from RadioButtonClick event handler to new SetJourneyViewState method.
//
//   Rev 1.46   Mar 26 2004 10:32:12   AWindley
//DEL5.2 QA Resolution for 673
//
//   Rev 1.45   Mar 15 2004 18:16:18   CHosegood
//Del 5.2 Map Changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.44   Dec 05 2003 16:01:52   kcheung
//Updated to display hours and mins instead of "h" and "m"
//
//   Rev 1.43   Dec 05 2003 15:00:30   kcheung
//Del 5.1 - Blue Summary Updates.
//
//   Rev 1.42   Dec 04 2003 18:37:24   kcheung
//Del 5.1 updates
//
//   Rev 1.41   Nov 28 2003 12:18:04   kcheung
//Align = right for radio button.  Load new button from langstrings.
//Resolution for 384: QA: JP Results: The radio buttons are too far over to the left
//
//   Rev 1.40   Nov 26 2003 15:08:06   kcheung
//Corrected rounding error.
//
//   Rev 1.39   Nov 26 2003 11:52:02   kcheung
//Alt text for selected journey needs to be different now.
//
//   Rev 1.38   Nov 25 2003 13:40:30   kcheung
//Added date on arrival field if different from request date.
//Resolution for 371: date on Arrive
//
//   Rev 1.37   Nov 24 2003 16:05:48   kcheung
//Fixed so that if result is not valid then correctly handled.
//
//   Rev 1.36   Nov 20 2003 16:31:58   kcheung
//Fixed bug where duration is not correctly displayed if duration of the journey takes longer than 24 hours.
//
//   Rev 1.35   Nov 18 2003 10:02:30   kcheung
//Added date time formatter so that single digit days and/or months are preceded by a 0.
//
//   Rev 1.34   Nov 18 2003 09:17:44   kcheung
//Added date field to the summary if the depart date is not equal to the requested depart date.
//Resolution for 213: Date missing from journey returned
//
//   Rev 1.33   Nov 05 2003 12:25:30   kcheung
//Updated property headers
//
//   Rev 1.32   Nov 04 2003 10:46:10   kcheung
//Fixed 0 min bug for QA
//
//   Rev 1.31   Nov 03 2003 16:37:08   kcheung
//Updated so that selected journey index and journey type is initialised on !postback.
//
//   Rev 1.30   Oct 28 2003 13:37:54   kcheung
//Fixed for QA.  If there is only one result, then it is not highlighted.
//
//   Rev 1.29   Oct 28 2003 12:34:20   kcheung
//Fixed number for QA
//
//   Rev 1.28   Oct 24 2003 13:55:50   COwczarek
//Update session with selected outbound/return journey id
//
//   Rev 1.27   Oct 20 2003 10:01:48   kcheung
//Tidied up code for FXCOP
//
//   Rev 1.26   Oct 17 2003 16:00:10   kcheung
//Fixed resulting from FXCop comments
//
//   Rev 1.25   Oct 17 2003 15:46:38   kcheung
//Fixed to conform with FXCOP rules
//
//   Rev 1.24   Oct 17 2003 13:53:26   kcheung
//Removed commented out code
//
//   Rev 1.23   Oct 15 2003 13:49:08   kcheung
//Fixed HTML after comments from w3c validation
//
//   Rev 1.22   Oct 13 2003 13:14:50   kcheung
//Fixed alternate text
//
//   Rev 1.21   Oct 09 2003 10:50:40   kcheung
//Fixed to ensure that the summary tag for the table is read from langstrings
//
//   Rev 1.20   Oct 02 2003 18:11:22   passuied
//added strings for printablemapinput
//
//   Rev 1.19   Sep 29 2003 10:56:36   kcheung
//Fixed bug where data was only being loaded if the control was being rendered for non print mode.
//
//   Rev 1.18   Sep 26 2003 15:31:42   PNorell
//Minor html tweaks to make it conform to the web templates.
//
//   Rev 1.17   Sep 24 2003 16:15:54   PNorell
//Updated for html integration.
//
//   Rev 1.16   Sep 18 2003 11:03:52   PNorell
//Fixed reference to use interface instead of concrete implimentation.
//
//   Rev 1.15   Sep 16 2003 18:07:12   kcheung
//Updated
//
//   Rev 1.14   Sep 10 2003 15:24:48   kcheung
//Updated to make it work with TDPage... need to bind the data twice
//
//   Rev 1.13   Sep 08 2003 15:07:14   kcheung
//No update
//
//   Rev 1.12   Sep 03 2003 16:29:46   kcheung
//Removed object cast from class into HTML
//
//   Rev 1.11   Sep 03 2003 16:09:28   kcheung
//Updated mileage conversion from metres
//
//   Rev 1.10   Sep 02 2003 12:42:18   kcheung
//Updated 
//
//   Rev 1.9   Aug 28 2003 12:46:50   kcheung
//Updated - integration with Journey Summary Line complete
//
//   Rev 1.8   Aug 27 2003 17:57:40   kcheung
//Integrated with JourneySummaryLine - working version
//
//   Rev 1.7   Aug 21 2003 10:13:24   kcheung
//Updated SelectIndex property
//
//   Rev 1.6   Aug 15 2003 17:04:54   kcheung
//No update
//
//   Rev 1.5   Aug 14 2003 18:19:46   kcheung
//Updated
//
//   Rev 1.4   Aug 07 2003 14:56:38   kcheung
//Fixed weird buy where the design view would not open because a class was defined above the Control class.  Moved it to the bottom of the file and it worked.
//
//   Rev 1.3   Aug 07 2003 14:22:36   kcheung
//Updated $Log$

using System;using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	A custom user control to
	/// display summary journey results in a tabular format.
	/// The control allows the user to select any of the rows in the table. 
	/// </summary>
	public partial  class SummaryResultTableControl : TDUserControl
	{
		
		// Indicates if the table should be rendered in print mode or selectable mode.
		private bool printMode = false;
		
		// Indicates if this control should load data for the outward journey
		// or the return journey.
		private bool outward = false;

		// Indicates if results were generated using "arrive before" or "depart after"
		private bool arriveBefore = false;

		// Event to fire when the selection has changed
		public event SelectionChangedEventHandler SelectionChanged;

		private string uncheckedRadioButtonImageUrl = String.Empty;
		private string checkedRadioButtonImageUrl = String.Empty;

		private FormattedJourneySummaryLine[] journeySummaryLines = null;

        private TrackingControlHelper trackingHelper;

		/// <summary>
		/// Initialisation method for this control.
		/// </summary>
		/// <param name="printMode">Indicates whether the control should be rendered in print mode.</param>
		/// <param name="outward">Indicates whether the journey rendered should be outward or return.</param>
		/// <param name="arriveBefore">Indicates whether the journey time is "Arrive before" or "Leaving after".</param>
		public void Initialise(bool printMode, bool outward, bool arriveBefore)
		{
			// All of these values will be saved in the viewstate so that Initialise
			// needs to be called once only.
			this.printMode = printMode;
			this.outward = outward;
			this.arriveBefore = arriveBefore;
		}

		/// <summary>
		/// Page Load method
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!TDSessionManager.Current.IsFindAMode)
				SetupControls();
		}

		/// <summary>
		/// OnPreRender method.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			if (TDItineraryManager.Current.ItineraryManagerModeChanged)
			{
				SetupControls();
			}

			base.OnPreRender(e);
		}

		/// <summary>
		/// Establish the display properties and data sources for the controls
		/// </summary>
		private void SetupControls()
		{
			if (TDSessionManager.Current.JourneyResult == null || !TDSessionManager.Current.JourneyResult.IsValid)
				return;

			// Read the button urls from resourcing manager.
			uncheckedRadioButtonImageUrl = Global.tdResourceManager.GetString(
				"SummaryResultTableControl.imageButton.SummaryRadioButtonUnchecked", TDCultureInfo.CurrentUICulture);

			checkedRadioButtonImageUrl = Global.tdResourceManager.GetString(
				"SummaryResultTableControl.imageButton.SummaryRadioButtonChecked", TDCultureInfo.CurrentUICulture);

			// Set the datasource for the repeater.  The data for the summary
			// result table is the array of JourneySummaryLines retrieved from
			// the session.

			ITDJourneyResult result = TDSessionManager.Current.JourneyResult;

			if(outward) 
			{
				journeySummaryLines = CreateFormattedLines(result.OutwardJourneySummary(arriveBefore));
			}
			else 
			{
				journeySummaryLines = CreateFormattedLines(result.ReturnJourneySummary(arriveBefore));
			}

			summaryRepeater.DataSource = journeySummaryLines;
			summaryRepeater.DataBind();

			// Add handlers if appropriate
			if(!printMode)
			{
				// Add a handler for each row.  Find how many rows exist first

				for(int i=0; i<summaryRepeater.Items.Count; i++)
				{
					if((ImageButton)summaryRepeater.Items[i].FindControl("ImageButton") != null)
					{
						((ImageButton)summaryRepeater.Items[i].FindControl("ImageButton")).Click +=
							new System.Web.UI.ImageClickEventHandler(this.RadioButtonClick);
					}
	
				} // for
			} 

			// Set the journey type. This is required otherwise
			// when the Summary is loaded for the very first time, the selected
			// journey type in viewstate will not be set.
			
			TDJourneyViewState journeyViewState = TDSessionManager.Current.JourneyViewState;

			// Update the currently selected JourneyId and JourneyType based on the index of the selected journey
			int index = GetSelectedIndex();
				
			if(outward && journeySummaryLines.Length > 0) 
			{
				journeyViewState.SelectedOutwardJourneyID = journeySummaryLines[index].JourneyIndex;
				journeyViewState.SelectedOutwardJourneyType = journeySummaryLines[index].Type;
			} 
			else
			{
				if(journeySummaryLines.Length > 0)
				{
					journeyViewState.SelectedReturnJourneyID = journeySummaryLines[index].JourneyIndex;
					journeyViewState.SelectedReturnJourneyType = journeySummaryLines[index].Type;
				}
			}
		}

		#region ViewState Code
		/// <summary>
		/// Loads the ViewState
		/// </summary>
		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at SavedViewState.
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					printMode = (bool)myState[1];
				if (myState[2] != null)
					outward = (bool)myState[2];
				if (myState[3] != null)
					arriveBefore = (bool)myState[3];
				if(outward)
				{
					if (myState[4] != null)
					{
						TDSessionManager.Current.JourneyViewState.SelectedOutwardJourney = (int)myState[4];
					}
					if (myState[5] != null)
					{
						TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyID = (int)myState[5];
					}
					if (myState[6] != null)
					{
						TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyType = (TDJourneyType)myState[6];
					}
				}
				else 
				{
					if (myState[4] != null)
					{
						TDSessionManager.Current.JourneyViewState.SelectedReturnJourney = (int)myState[4];
					}
					if (myState[5] != null)
					{
						TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyID = (int)myState[5];
					}
					if (myState[6] != null)
					{
						TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyType = (TDJourneyType)myState[6];
					}
				}
			}
		}
	
		/// <summary>
		/// Overrides the base SaveViewState to customise viewstate behaviour.
		/// </summary>
		/// <returns>The ViewState object to be saved.</returns>
		protected override object SaveViewState()
		{ 
			// Save State as a cumulative array of objects.
			object baseState = base.SaveViewState();

			object[] allStates = new object[7];
			allStates[0] = baseState;
			allStates[1] = printMode;
			allStates[2] = outward;
			allStates[3] = arriveBefore;
			if(outward) 
			{
				allStates[4] = TDSessionManager.Current.JourneyViewState.SelectedOutwardJourney;
				allStates[5] = TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyID;
				allStates[6] = TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyType;
			}
			else 
			{
				allStates[4] = TDSessionManager.Current.JourneyViewState.SelectedReturnJourney;
				allStates[5] = TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyID;
				allStates[6] = TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyType;
			}

			return allStates;
		}
		#endregion

		/// <summary>
		/// Handler for the Radio Button Click.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void RadioButtonClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{	
			ImageButton sendingButton = (ImageButton)sender;

			// Find the index number of the button that was pressed
			int index;
			for (index=0;index<summaryRepeater.Items.Count;++index) 
			{
				ImageButton ib =
					((ImageButton)summaryRepeater.Items[index].FindControl("ImageButton"));
				if (ib == sendingButton)
				{
					break;
				}
			}

            // Note that SelectedOutwardJourney and SelectedReturnJourney properties of
            // TDJourneyViewState are obsolete. The following properties should be used
            // instead which give the ID of the selected journey. The ID can then be used
            // to retrieve the selected journey object (pass a parameter to OutwardPublicJourney
            // and ReturnPublicJourney on class TDJourneyResult)
			if (journeySummaryLines != null && journeySummaryLines.Length > 0)
			{
				if(outward) 
				{
					// Set the selected journey in TDJourneyViewState in TDSessionManager
					// SelectedOutwardJourney and SelectedReturnJourney properties of
					// TDJourneyViewState are obsolete.
					TDSessionManager.Current.JourneyViewState.SelectedOutwardJourney = index;

					TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyID = journeySummaryLines[index].JourneyIndex;
					TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyType = journeySummaryLines[index].Type;

				} 
				else 
				{
					// Set the selected journey in TDJourneyViewState in TDSessionManager
					// SelectedOutwardJourney and SelectedReturnJourney properties of
					// TDJourneyViewState are obsolete.
					TDSessionManager.Current.JourneyViewState.SelectedReturnJourney = index;

					TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyID = journeySummaryLines[index].JourneyIndex;
					TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyType = journeySummaryLines[index].Type;

				}
			}

			//Reset the journey map state
			if (outward) 
			{
				TDSessionManager.Current.JourneyMapState.Initialise();
			}
			else
			{
				TDSessionManager.Current.ReturnJourneyMapState.Initialise();
			}

			// Highligted row has changed so re-bind repeater
			summaryRepeater.DataBind();

			OnSelectionChanged(new EventArgs());
		}



		/// <summary>
		/// Method to fire off the Selection Changed Event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			if(SelectionChanged != null)
			{
				// Invoke the delegate
				SelectionChanged(this, e);
                int selectedIndex = GetSelectedIndex();
                trackingHelper.AddTrackingParameter(this, selectedIndex.ToString());
			}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// OnInit Method.
		/// </summary>
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            trackingHelper = new TrackingControlHelper();
		}
		#endregion
		
		#region Rendering code

		/// <summary>
		/// Returns the Css class that the row text should be rendered with.
		/// </summary>
		/// <param name="summary">Current item being rendered.</param>
		/// <returns>Css class string.</returns>
		public string GetTextCssClass(int index)
		{
			// If there is only one result then no rows should
			// be highlighed. Check to see if this is the case.
			if(journeySummaryLines.Length == 1)
				return "";

			if(IsSelectedIndex(index))
			{
				return "y";
			}
			return (index % 2) == 0 ? "g":"";
		}

		/// <summary>
		/// Returns the Url to the radio button image.
		/// </summary>
		/// <param name="summary">The index of the line being rendered</param>
		/// <returns>Url string to the radio button image.</returns>
		public string GetButtonImageUrl(int index)
		{
			if(IsSelectedIndex(index))
			{
				return checkedRadioButtonImageUrl;
			}
			else 
			{
				return uncheckedRadioButtonImageUrl;
			}
		}

		/// <summary>
		///Returns the alternate text for the radio button.
		/// </summary>
		public string AlternateText(int index)
		{
			if(IsSelectedIndex(index))
			{
				return Global.tdResourceManager.GetString(
					"SummaryResultTableControl.RadioButtonSelectedAlternateText", TDCultureInfo.CurrentUICulture);
			}
			else
			{
				return Global.tdResourceManager.GetString(
					"SummaryResultTableControl.RadioButtonAlternateText", TDCultureInfo.CurrentUICulture);
			}
		}

		#endregion

		#region index identifying convenience methods
		/// <summary>
		/// Finds the current selected index from TDSessionManager.
		/// </summary>
		/// <returns>The current selected index.</returns>
		private int GetSelectedIndex()
		{
			TDJourneyViewState journeyViewState = TDSessionManager.Current.JourneyViewState;
			return outward ? journeyViewState.SelectedOutwardJourney : journeyViewState.SelectedReturnJourney;
		}

		/// <summary>
		/// Determines if an index is the currently selected index specified by TDSessionManager
		/// </summary>
		/// <param name="index">The candidate index</param>
		/// <returns>true if the index is the selected index, false otherwise</returns>
		private bool IsSelectedIndex(int index)
		{
			return index == GetSelectedIndex();
		}
		#endregion
		#region Properties for the headers
		
		/// <summary>
		/// Get property - returns the Transport Header
		/// </summary>
		public string HeaderTransport
		{
			get 
			{
				return Global.tdResourceManager.GetString(
					"SummaryResultTable.headerTransport",
					TDCultureInfo.CurrentUICulture);
			}
		}

		/// <summary>
		/// Get property returns the Changes Header
		/// </summary>
		public string HeaderChanges
		{
			get 
			{
				return Global.tdResourceManager.GetString(
					"SummaryResultTable.headerChanges",
					TDCultureInfo.CurrentUICulture);
			}
		}

		/// <summary>
		/// Get property - returns the Leave Header
		/// </summary>
		public string HeaderLeave
		{
			get 
			{
				return Global.tdResourceManager.GetString(
					"SummaryResultTable.headerLeave",
					TDCultureInfo.CurrentUICulture);
			}
		}

		/// <summary>
		/// Get property - returns the Arrive Header
		/// </summary>
		public string HeaderArrive
		{
			get 
			{
				return Global.tdResourceManager.GetString(
					"SummaryResultTable.headerArrive",
					TDCultureInfo.CurrentUICulture);
			}
		}

		/// <summary>
		/// Get property - returns the Duration Header
		/// </summary>
		public string HeaderDuration
		{
			get 
			{
				return Global.tdResourceManager.GetString(
					"SummaryResultTable.headerDuration",
					TDCultureInfo.CurrentUICulture);
			}
		}

		/// <summary>
		/// Get property - returns the Option Header
		/// </summary>
		public string HeaderOption
		{
			get 
			{
				return Global.tdResourceManager.GetString(
					"SummaryResultTable.headerOption",
					TDCultureInfo.CurrentUICulture);
			}
		}

		/// <summary>
		/// Get property - returns the Select Header
		/// </summary>
		public string HeaderSelect
		{
			get 
			{
				if(!printMode && (journeySummaryLines.Length > 1))
				{
					return Global.tdResourceManager.GetString(
						"SummaryResultTable.headerSelect",
						TDCultureInfo.CurrentUICulture);
				}
				else
				{
					return "&nbsp;";
				}
			}
		}

		#endregion

		#region Other properties
		/// <summary>
		/// Determines if the radio button should be visible or not.
		/// </summary>
		/// <returns>True if the button should be rendered, false otherwise.</returns>
		public bool RadioButtonsVisible
		{
			get {return !printMode && (journeySummaryLines.Length > 1);}
		}
		#endregion

		/// <summary>
		/// Create a correctly constructed array of FormattedJourneySummaryLine objects
		/// from an array of JourneySummaryLine objects
		/// </summary>
		/// <param name="sourceLines">The source JourneySummaryLine array</param>
		/// <returns></returns>
		private FormattedJourneySummaryLine[] CreateFormattedLines(JourneySummaryLine[] sourceLines)
		{
			FormattedJourneySummaryLine[] generatedLines = new FormattedJourneySummaryLine[sourceLines.Length];

			if (!outward && 
				(TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime == null 
				|| TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime.Length == 0
				|| TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime[0] == null)) 
			{
				return new FormattedJourneySummaryLine[0];
			}
			
			int requestedDay;
			if(outward)
			{
				requestedDay = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.OutwardDateTime[0].Day;
			}
			else
			{
				requestedDay = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime[0].Day;
			}

			double conversionFactor =
				Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentCulture.NumberFormat);

			for (int i=0; i<generatedLines.Length;++i)
			{
				generatedLines[i] = new FormattedJourneySummaryLine(sourceLines[i],  requestedDay, conversionFactor);
			}
			return generatedLines;
		}



        /// <summary>
        /// The event handler for the summaryRepeater Item Data Bound event
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void summaryRepeater_ItemDatabound(object sender, RepeaterItemEventArgs e)
        {
            #region Populate the screen reader headers and table cell styles
            if ((e.Item.ItemType == ListItemType.Item)
                || (e.Item.ItemType == ListItemType.AlternatingItem)
                || (e.Item.ItemType == ListItemType.SelectedItem))
            {
                HtmlTableCell th = null;
                HtmlTableCell td = null;

                #region Column 1 - Journey number

                th = summaryRepeater.Controls[0].FindControl("screenreaderheader1") as HtmlTableCell;
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "jpnumbhd");

                    td = e.Item.FindControl("screenreaderchild1") as HtmlTableCell;
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "jpnumb" + GetTextCssClass(e.Item.ItemIndex));
                    }
                }

                #endregion

                #region Column 2 - Journey type

                th = summaryRepeater.Controls[0].FindControl("screenreaderheader2") as HtmlTableCell;
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "jptypehd");

                    td = e.Item.FindControl("screenreaderchild2") as HtmlTableCell;
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "jptype" + GetTextCssClass(e.Item.ItemIndex));
                    }
                }

                #endregion

                #region Column 3 - Changes

                th = summaryRepeater.Controls[0].FindControl("screenreaderheader3") as HtmlTableCell;
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "jpchanhd");

                    td = e.Item.FindControl("screenreaderchild3") as HtmlTableCell;
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "jpchan" + GetTextCssClass(e.Item.ItemIndex));
                    }
                }

                #endregion

                #region Column 4 - Leave at

                th = summaryRepeater.Controls[0].FindControl("screenreaderheader4") as HtmlTableCell;
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "jpleavhd");

                    td = e.Item.FindControl("screenreaderchild4") as HtmlTableCell;
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "jpleav" + GetTextCssClass(e.Item.ItemIndex));
                    }
                }

                #endregion

                #region Column 4 - Arrive at

                th = summaryRepeater.Controls[0].FindControl("screenreaderheader5") as HtmlTableCell;
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "jparrihd");

                    td = e.Item.FindControl("screenreaderchild5") as HtmlTableCell;
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "jparri" + GetTextCssClass(e.Item.ItemIndex));
                    }
                }

                #endregion

                #region Column 5 - Duration

                th = summaryRepeater.Controls[0].FindControl("screenreaderheader6") as HtmlTableCell;
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "jpdurahd");

                    td = e.Item.FindControl("screenreaderchild6") as HtmlTableCell;
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "jpdura" + GetTextCssClass(e.Item.ItemIndex));
                    }
                }

                #endregion
                
                 #region Column 6 - select journey

                th = summaryRepeater.Controls[0].FindControl("screenreaderheader7") as HtmlTableCell;
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "jpimgbhd");

                    td = e.Item.FindControl("screenreaderchild7") as HtmlTableCell;
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "jpimgb" + GetTextCssClass(e.Item.ItemIndex));
                    }
                }

                #endregion               

            }

            #endregion
        }



	}

    /// <summary>
	/// Delegate - Event Handler if selected index changes
	/// </summary>
	public delegate void SelectionChangedEventHandler(object sender, EventArgs e);
	
}

