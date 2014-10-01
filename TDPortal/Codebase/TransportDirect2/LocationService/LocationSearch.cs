// *********************************************** 
// NAME             : LocationSearch.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Class for the location search values passed to the LocationService component. 
//                  : It delegates the actual searching functions to the relevant Location Cache or TDPGazetteer classes
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.LocationService.Gazetteer;

namespace TDP.Common.LocationService
{
    /// <summary>
    /// Class for the location search values passed to the LocationService component
    /// </summary>
    [Serializable()]
    public class LocationSearch
    {
        #region Private members

        // Search inputs 
        private string searchText = string.Empty;
        private string searchId = string.Empty;
        private TDPLocationType searchType = TDPLocationType.Unknown;
        private OSGridReference osgr = null;
        private bool javascriptEnabled = false;

        // Search outputs
        private List<LocationQueryResult> locationQueryResults = new List<LocationQueryResult>(); // From Gazetteer
        private List<TDPLocation> locationCacheResults = new List<TDPLocation>(); // From Location cache, used for ambiguity
        private bool isVague = false;
        private bool isSingleWord = false;
        private bool isHierarchic = false;

        // Search selection (for ambiguity resolution)
        private LocationChoice locationChoiceSelected = null;
        private bool locationChoiceDrillDown = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LocationSearch()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LocationSearch(string searchText, string searchId, TDPLocationType searchType, bool javascriptEnabled)
        {
            if (!string.IsNullOrEmpty(searchText))
                this.searchText = searchText.Trim();
            
            if (!string.IsNullOrEmpty(searchId))
                this.searchId = searchId.Trim();

            this.searchType = searchType;
            this.javascriptEnabled = javascriptEnabled;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Input text of the search entered by user, or the 
        /// location text populated by the javascript "auto-suggest" selected location
        /// </summary>
        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; }
        }

        /// <summary>
        /// Read/Write. Search Id for the location, typically populated 
        /// by the javascript "auto-suggest" selected location
        /// </summary>
        public string SearchId
        {
            get { return searchId; }
            set { searchId = value; }
        }

        /// <summary>
        /// Read/Write. Returns the type of the search, typically populated 
        /// by the javascript "auto-suggest" selected location
        /// </summary>
        public TDPLocationType SearchType
        {
            get { return searchType; }
            set { searchType = value; }
        }

        /// <summary>
        /// Read/Write. OS grid reference to use for location, typically populated 
        /// when search type is coordinate to resolve a coordinate based location (e.g. for a "My location" coordinate location)
        /// </summary>
        public OSGridReference GridReference
        {
            get { return osgr; }
            set { osgr = value; }
        }

        /// <summary>
        /// Read/Write. Indicates if javascript is enabled on the location control,
        /// default is false
        /// </summary>
        public bool JavascriptEnabled
        {
            get { return javascriptEnabled; }
            set { javascriptEnabled = value; }
        }

        /// <summary>
        /// Read-only. Returns true if Location Query results exist at the current level
        /// </summary>
        public bool LocationQueryResultsExist
        {
            get
            {
                if (CurrentLevel() >= 0)
                {
                    LocationChoiceList locationChoices = GetLocationChoices(CurrentLevel());

                    return (locationChoices != null && locationChoices.Count > 0);
                }
                return false;
            }
        }

        /// <summary>
        /// Read-only. Returns true if Location Cache results exist
        /// </summary>
        public bool LocationCacheResultsExist
        {
            get
            {
                IList<TDPLocation> locations = GetLocationCacheResult();
                
                if (locations != null && locations.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Read/Write. List containing location query gazeteer results.
        /// Each LocationQueryResult contains locations for successive queries and drilldowns
        /// </summary>
        public List<LocationQueryResult> LocationQueryResults
        {
            get { return locationQueryResults; }
            set { locationQueryResults = value; }
        }

        /// <summary>
        /// Read/Write. List containing location ambiguity results from the cache.
        /// </summary>
        public List<TDPLocation> LocationCacheResults
        {
            get { return locationCacheResults; }
            set { locationCacheResults = value; }
        }

        /// <summary>
        /// Read/Write. Returns if the Search is too vague
        /// </summary>
        public bool VagueSearch
        {
            get { return isVague; }
            set { isVague = value; }
        }

        /// <summary>
        /// Read/Write. Returns if the Search is a single word address (and therefore too vague)
        /// </summary>
        public bool SingleWord
        {
            get { return isSingleWord; }
            set { isSingleWord = value; }
        }

        /// <summary>
        /// Read/Write. Indicates if the gazetteer used for the search supports hiearchic locations (drill down)
        /// </summary>
        public bool SupportHierarchic
        {
            get { return isHierarchic; }
            set { isHierarchic = value; }
        }

        /// <summary>
        /// Read/Write. Selected LocationChoice from the LocationQueryResult 
        /// for ambiguity resolution
        /// </summary>
        public LocationChoice LocationChoiceSelected
        {
            get { return locationChoiceSelected; }
            set { locationChoiceSelected = value; }
        }

        /// <summary>
        /// Read/Write. Indicates if the selected LocationChoice should be drilled down
        /// for ambiguity resolution
        /// </summary>
        public bool LocationChoiceDrillDown
        {
            get { return locationChoiceDrillDown; }
            set { locationChoiceDrillDown = value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// The current level number for the LocationQueryResult hierarchical search (zero based).
        /// A value of -1 indicates no query result has yet been associated with this search
        /// </summary>
        public int CurrentLevel()
        {
            return locationQueryResults.Count - 1;
        }

        /// <summary>
        /// Returns the LocationQueryResult for the given level
        /// </summary>
        public LocationQueryResult GetLocationQueryResult(int level)
        {
            if (locationQueryResults != null && level >= 0 && level < locationQueryResults.Count)
                return locationQueryResults[level];
            else
                return null;
        }

        /// <summary>
        /// Returns from the LocationQueryResult the list of LocationChoices for the given level,
        /// or null if none exist
        /// </summary>
        public LocationChoiceList GetLocationChoices(int level)
        {
            if (locationQueryResults != null && level >= 0 && level < locationQueryResults.Count && locationQueryResults[level] != null)
                return locationQueryResults[level].LocationChoiceList;
            else
                return null;
        }

        /// <summary>
        /// Returns a readonly list of Locations found from the "auto-suggest" ambiguous search
        /// using the location cache,
        /// or null if none exist
        /// </summary>
        /// <returns></returns>
        public IList<TDPLocation> GetLocationCacheResult()
        {
            if (locationCacheResults != null)
                return locationCacheResults.AsReadOnly();
            else
                return null;
        }

        /// <summary>
        /// Overridden ToString() method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Text[{0}] Value[{1}] Type[{2}] {3}", 
                        searchText, 
                        searchId, 
                        searchType,
                        (osgr != null) ? string.Format("Coordinate[{0}]", osgr) : string.Empty);
        }

        #endregion

    }
}
