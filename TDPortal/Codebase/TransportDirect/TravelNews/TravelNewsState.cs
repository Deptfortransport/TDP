// ***********************************************
// NAME 		: TravelNewsData.cs
// AUTHOR 		: Joe Morrissey
// DATE CREATED : 21/10/2003
// DESCRIPTION 	: 
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNews/TravelNewsState.cs-arc  $
//
//   Rev 1.2   Mar 10 2008 15:28:26   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:50:30   mturner
//Initial revision.
//
//   Rev Devfactory Jan 07 2008 13:00:09 apatel
//   CCN 0421 Added properties to store previous filter selections
//
//   Rev 1.6   Jan 19 2007 13:41:30   build
//Automatically merged from branch for stream4329
//
//   Rev 1.5.1.0   Jan 12 2007 13:48:30   tmollart
//Added properties for travel news search text and search tokens.
//Resolution for 4329: Travel News Updates and Search
//
//   Rev 1.5   Aug 09 2006 11:48:44   mmodi
//Added properties to save page language and incident selected
//Resolution for 4146: Selecting Welsh link on Travel news map reverts view back to table
//
//   Rev 1.4   Mar 28 2006 11:09:00   build
//Automatically merged from branch for stream0024
//
//   Rev 1.3.1.0   Mar 03 2006 16:17:06   AViitanen
//SelectedSeverityFilter as part of Travel News Updates. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.3   Aug 18 2005 11:29:52   jgeorge
//Automatically merged from branch for stream2558
//
//   Rev 1.2.1.1   Jul 08 2005 14:42:54   jbroome
//Removed unecessary SelectedDay and SelectedMonthYear properties
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.2.1.0   Jul 01 2005 11:23:36   jmorrissey
//Added selected date info and SelectedViewType
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.2   Dec 16 2004 15:27:24   passuied
//Refactoring the TravelNews component
//
//   Rev 1.0   Sep 07 2004 09:36:20   JHaydock
//Initial revision.
//
//   Rev 1.3   May 18 2004 13:32:00   jbroome
//IR864 Retaining values when Help is displayed.
//
//   Rev 1.2   Oct 22 2003 12:18:06   JMorrissey
//updated comments
//
//   Rev 1.1   Oct 22 2003 09:45:40   JMorrissey
//added [Serializable()] attribute to class
//
//   Rev 1.0   Oct 21 2003 17:33:56   JMorrissey
//Initial Revision

using System;
using System.Data;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.UserPortal.TravelNewsInterface;

namespace TransportDirect.UserPortal.TravelNews
{
	/// <summary>
	/// TravelNewsState is used to hold session data for the displayed data on
	/// TravelNews.aspx. This session data can then be accessed by other forms,
	/// in particular PrintableTravelNews.aspx.
	/// TravelNewsState is exposed as property 'TravelNewsData'on TDSessionManager
	/// </summary>
	[CLSCompliant(false)]
	[Serializable()]
	public class TravelNewsState
	{
		//private properties
		private TransportType ttSelectedTransport = TransportType.All;
		private string sSelectedRegion = "All";
		private DelayType detSelectedDelays = DelayType.Major;
		private DisplayType ditSelectedDetails = DisplayType.Full;
		private string dataDateTime = string.Empty;
		private bool helpDisplayed = false;
		private TDDateTime selectedDateTime = new TDDateTime();
		private TravelNewsViewType selectedViewType;	
		private SeverityFilter selectedSeverityFilter = SeverityFilter.Default;
		private string searchPhrase = string.Empty;
		private ArrayList searchTokens;
        private IncidentType selectedIncidentType = IncidentType.All;


        #region Previous filter selection properties CCN 0421
        private TransportType ttLastSelectedTransport = TransportType.All;
        private string sLastSelectedRegion = "All";
        private DelayType detLastSelectedDelays = DelayType.Major;
        private TDDateTime lastSelectedDateTime = new TDDateTime();
        private string lastSearchPhrase = string.Empty;
        private string lastSelectedIncident = string.Empty;
        private IncidentType lastSelectedIncidentType = IncidentType.All;
        private TravelNewsViewType lastSelectedViewType;
        #endregion

        // to track when page language is changed to prevent view from being reset
		private string pageLanguage = string.Empty;
		private string selectedIncident = string.Empty;

		public TravelNewsState()
		{
		}

		public void SetDefaultState()
		{
			ttSelectedTransport = TransportType.All;
			sSelectedRegion = "All";
			detSelectedDelays = DelayType.Major;
			ditSelectedDetails = DisplayType.Full;
			dataDateTime = string.Empty;
			helpDisplayed = false;		
			selectedDateTime = TDDateTime.Now;
			selectedViewType = TravelNewsViewType.Details;
			selectedSeverityFilter = SeverityFilter.Default;

            //CCN 0421 - Default values for variables storing last selected values
            ttLastSelectedTransport = TransportType.All;
            sLastSelectedRegion = "All";
            detLastSelectedDelays = DelayType.Major;
            lastSelectedDateTime = new TDDateTime();
            lastSelectedViewType = TravelNewsViewType.Details;

            // CCN 0421 - Default values for variables storing last and current 
            // selected incident type
            selectedIncidentType = IncidentType.All;
            lastSelectedIncidentType = IncidentType.All;

		}

		/// <summary>
		/// Read/Write property SelectedTransport.Holds value 
		/// selected from transport drop down on ShowNewsControl.
		/// </summary>
		public TransportType SelectedTransport
		{
			get { return ttSelectedTransport; }
			set { ttSelectedTransport = value; }
		}

		/// <summary>
		/// Read/Write property SelectedTransport.Holds value 
		/// selected from details drop down on ShowNewsControl.
		/// </summary>
		public DisplayType SelectedDetails
		{
			get { return ditSelectedDetails; }
			set { ditSelectedDetails = value; }
		}

		/// <summary>
		/// Read/Write property SelectedTransport.Holds value 
		/// selected from delays drop down on ShowNewsControl.
		/// </summary>
		public DelayType SelectedDelays
		{
			get { return detSelectedDelays; }
			set { detSelectedDelays = value; }
		}

		/// <summary>
		/// Read/Write property SelectedTransport.Holds value 
		/// selected from region drop down on ShowNewsControl.
		/// </summary>
		public string SelectedRegion
		{
			get { return sSelectedRegion; }
			set { sSelectedRegion = value; }
		}

		/// <summary>
		/// Read Write property HelpDisplayed. Used when showing the Help control
		/// to get round the ForceRedirect that occurs, losing viewstate.
		/// </summary>
		public bool HelpDisplayed
		{
			get { return helpDisplayed; }
			set { helpDisplayed = value; }
		}

		/// <summary>
		/// Read Write property SelectedDate. Holds the TDDateTime value of the date
		/// that the user has selected to view travel news data for
		/// </summary>
		public TDDateTime SelectedDate
		{
			get { return selectedDateTime; }
			set { selectedDateTime = value; }
		}

		/// <summary>
		/// Read Write property SelectedView. Holds the ViewType value that specifies whether
		/// to display travel news incidents in a details table or a map view
		/// </summary>
		public TravelNewsViewType SelectedView
		{
			get { return selectedViewType; }
			set { selectedViewType = value; }
		}

		/// <summary>
		/// Read Write property SelectedSeverityFilter. Holds incident severity level 
		/// that the user has selected to view travel news data for
		/// </summary>
		public SeverityFilter SelectedSeverityFilter 
		{
			get { return selectedSeverityFilter;}
			set { selectedSeverityFilter = value;}
		}
		
		/// <summary>
		/// Read Write property TravelNewsPageLanguage. Holds the page language
		/// used to prevent default state being intialised when switching language
		/// </summary>
		public string PageLanguage
		{
			get { return pageLanguage; }
			set { pageLanguage = value; }
		}

		/// <summary>
		/// Read Write property SelectedIncident. Holds the incident selected.
		/// Used to allow zoom in to the incident if it was displayed when switching language
		/// </summary>
		public string SelectedIncident
		{
			get { return selectedIncident; }
			set { selectedIncident = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string SearchPhrase
		{
			get { return searchPhrase; }
			set { searchPhrase = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public ArrayList SearchTokens
		{
			get { return searchTokens; }
			set { searchTokens = value; }
        }

        #region Previous values for the TravelNewsState filter
        /// <summary>
        /// Read/Write property LastSelectedTransport.Holds last value 
        /// selected from transport drop down on ShowNewsControl.
        /// </summary>
        public TransportType LastSelectedTransport
        {
            get { return ttLastSelectedTransport; }
            set { ttLastSelectedTransport = value; }
        }

        /// <summary>
        /// Read Write property LastSelectedDate. Holds the TDDateTime value of the date
        /// that the user has selected previously to view travel news data for
        /// </summary>
        public TDDateTime LastSelectedDate
        {
            get { return lastSelectedDateTime; }
            set { lastSelectedDateTime = value; }
        }

        /// <summary>
        /// Read/Write property LastSelectedTransport.Holds previous value 
        /// selected from region drop down on ShowNewsControl.
        /// </summary>
        public string LastSelectedRegion
        {
            get { return sLastSelectedRegion; }
            set { sLastSelectedRegion = value; }
        }

        /// <summary>
        /// Read/Write property LastSelectedTransport.Holds previous value 
        /// selected from delays drop down on ShowNewsControl.
        /// </summary>
        public DelayType LastSelectedDelays
        {
            get { return detLastSelectedDelays; }
            set { detLastSelectedDelays = value; }
        }

        /// <summary>
        /// Read/Write property LastSarchPhrase.Holds previous value 
        /// selected from search phrase on ShowNewsControl.
        /// </summary>
        public string LastSearchPhrase
        {
            get { return lastSearchPhrase; }
            set { lastSearchPhrase = value; }
        }

        /// <summary>
        /// Read Write property LastSelectedIncident. Holds the incident selected.
        /// previously Used to allow zoom in to the incident if it was displayed when switching language
        /// </summary>
        public string LastSelectedIncident
        {
            get { return lastSelectedIncident; }
            set { lastSelectedIncident = value; }
        }

        /// <summary>
        /// Read Write property SelectedIncidentType. Holds the IncidentType value
        /// selected from Incident Type dropdown on ShowNewsControl
        /// </summary>
        public IncidentType SelectedIncidentType
        {
            get { return selectedIncidentType; }
            set { selectedIncidentType = value; }
        }

        /// <summary>
        /// Read Write property LastSelectedIncidentType. Holds the previous IncidentType value
        /// selected from Incident Type dropdown on ShowNewsControl
        /// </summary>
        public IncidentType LastSelectedIncidentType
        {
            get { return lastSelectedIncidentType; }
            set { lastSelectedIncidentType = value; }
        }

        /// <summary>
        /// Read Write property LastSelectedView. Holds the previous ViewType value that specifies whether
        /// to display travel news incidents in a details table or a map view
        /// </summary>
        public TravelNewsViewType LastSelectedView
        {
            get { return lastSelectedViewType; }
            set { lastSelectedViewType = value; }
        }

        #endregion
    }
}
