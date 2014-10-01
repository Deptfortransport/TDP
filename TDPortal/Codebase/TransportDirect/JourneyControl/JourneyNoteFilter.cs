// *********************************************** 
// NAME                 : JourneyNoteFilter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/03/2013 
// DESCRIPTION          : JourneyNoteFilter data class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyNoteFilter.cs-arc  $ 
//
//   Rev 1.0   Mar 21 2013 10:14:52   mmodi
//Initial revision.
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// JourneyNoteFilter data class
    /// </summary>
    public class JourneyNoteFilter
    {
        #region Private Members
		
		private const string CONST_ALL = "ALL";

		/// <summary>
		/// Structure used to define key for accessing data cached in journey note filter cache
		/// </summary>
		private struct FilterKey
		{
            public string mode, regionId;

			public FilterKey(string mode, string regionId)
			{
				this.mode = mode;
				this.regionId = regionId;
            }
		}

        /// <summary>
        /// Structure used to define journey note filter
        /// </summary>
        private struct FilterNote
        {
            public string filterText;
            public bool accessibleOnly;

            public FilterNote(string filterText, bool accessibleOnly)
            {
                this.filterText = filterText;
                this.accessibleOnly = accessibleOnly;
            }
        }

		/// <summary> Dictionary for storing journey note filters</summary>
		private Dictionary<FilterKey, List<FilterNote>> journeyNotesFilterCache = new Dictionary<FilterKey,List<FilterNote>>();

		#endregion

		#region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public JourneyNoteFilter()
		{
			Load();
		}

		#endregion

        #region Public Static Methods

        /// <summary>
        /// Read only property. Returns the current instance from ServiceDiscovery
        /// </summary>
        public static JourneyNoteFilter Current
        {
            get
            {
                return (JourneyNoteFilter)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyNoteFilter];
            }
        }

        #endregion

		#region Public methods

		/// <summary>
		/// Given a transport mode, region, and accessible journey flag, returns whether the supplied journey note 
        /// should be displayed or not
		/// </summary>
		public bool DisplayNote(ModeType modeType, string regionId, bool accessibleJourney, string journeyNote)
		{
            // Assume journey note is ok
			bool result = true;
            
            // Ignore case for journey note matching
            // and no point checking if journey note was not supplied
            if (!string.IsNullOrEmpty(journeyNote))
                journeyNote = journeyNote.ToUpper();
            else 
                return result;

			if (!string.IsNullOrEmpty(regionId))
                regionId = regionId.ToUpper();

            string mode = modeType.ToString().ToUpper();

            #region Find filter notes

            // Build up a list of filter note strings from the cache
            List<FilterNote> filterNotes = new List<FilterNote>();

			// Default mode "all" and region "all" key
            // This filter applies to all journey notes
            FilterKey filterKey = new FilterKey(CONST_ALL, CONST_ALL);

            if (journeyNotesFilterCache.ContainsKey(filterKey))
                filterNotes.AddRange(journeyNotesFilterCache[filterKey]);

            // Mode specific and region "all"
            filterKey = new FilterKey(mode, CONST_ALL);

            if (journeyNotesFilterCache.ContainsKey(filterKey))
                filterNotes.AddRange(journeyNotesFilterCache[filterKey]);

            // Mode "all" and region specific
            filterKey = new FilterKey(CONST_ALL, regionId);

            if (journeyNotesFilterCache.ContainsKey(filterKey))
                filterNotes.AddRange(journeyNotesFilterCache[filterKey]);

            // Mode specific and region specific
            filterKey = new FilterKey(mode, regionId);

            if (journeyNotesFilterCache.ContainsKey(filterKey))
                filterNotes.AddRange(journeyNotesFilterCache[filterKey]);

            #endregion

            // Now test the journey note supplied with the filters found
            foreach (FilterNote fn in filterNotes)
            {
                if (fn.accessibleOnly)
                {
                    // Only test "accessibleOnly" filter against an accessible journey,
                    // otherwise ignore and move to the next
                    if (accessibleJourney && journeyNote.Contains(fn.filterText))
                    {
                        // Journey note contains the filter text, do not display
                        result = false;
                        break;
                    }
                    continue;
                }
                else if (journeyNote.Contains(fn.filterText))
                {
                    // Journey note contains the filter text, do not display
                    result = false;
                    break;
                }
            }

            return result;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Loads all the available operator links data from the database into a hashtable
		/// </summary>
        private void Load()
        {
            try
            {
                using (SqlHelper helper = new SqlHelper())
                {
                    // Open connection
                    helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

                    #region Load JourneyNotesFilter data

                    // JourneyNotesFilter data
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, "Loading JourneyNotesFilter data started"));
                    }

                    Dictionary<FilterKey, List<FilterNote>> tmpJNFCache = new Dictionary<FilterKey, List<FilterNote>>();
                    
                    // Get data
                    SqlDataReader reader = helper.GetReader("GetJourneyNoteFilter");

                    string mode = string.Empty;
                    string region = string.Empty;
                    bool accessibleOnly = false;
                    string filterText = string.Empty;

                    // Set up column ordinals
                    int accColumnMode = reader.GetOrdinal("Mode");
                    int accColumnRegion = reader.GetOrdinal("Region");
                    int accColumnAccessibleOnly = reader.GetOrdinal("AccessibleOnly");
                    int accColumnFilterText = reader.GetOrdinal("FilterText");

                    // Read values and create key for caching
                    while (reader.Read())
                    {
                        mode = reader.IsDBNull(accColumnMode) ? string.Empty : reader.GetString(accColumnMode);
                        region = reader.IsDBNull(accColumnRegion) ? string.Empty : reader.GetString(accColumnRegion);
                        accessibleOnly = reader.GetBoolean(accColumnAccessibleOnly);
                        filterText = reader.IsDBNull(accColumnFilterText) ? string.Empty : reader.GetString(accColumnFilterText);

                        // Validate data and ignore case
                        if (string.IsNullOrEmpty(mode))
                            mode = CONST_ALL;
                        else
                            mode = mode.Trim().ToUpper();

                        if (string.IsNullOrEmpty(region))
                            region = CONST_ALL;
                        else
                            region = region.Trim().ToUpper();

                        if (!string.IsNullOrEmpty(filterText))
                        {
                            filterText = filterText.ToUpper();

                            // Region could be a list, so split and create a note for each
                            List<string> regions = new List<string>(region.Split('|'));

                            foreach (string r in regions)
                            {
                                FilterKey key = new FilterKey(mode, r);
                                FilterNote note = new FilterNote(filterText, accessibleOnly);

                                if (!tmpJNFCache.ContainsKey(key))
                                    tmpJNFCache.Add(key, new List<FilterNote>());

                                tmpJNFCache[key].Add(note);
                            }
                        }
                    }

                    reader.Close();

                    Dictionary<FilterKey, List<FilterNote>> oldJNFCache = new Dictionary<FilterKey, List<FilterNote>>();

                    // Create reference to the existing dictionary prior to replacing it.
                    // This is for multi-threading purposes to ensure there is always data to reference
                    if (journeyNotesFilterCache != null)
                    {
                        oldJNFCache = journeyNotesFilterCache;
                    }

                    // Replace the current
                    journeyNotesFilterCache = tmpJNFCache;

                    // Clear the old
                    oldJNFCache.Clear();

                    // Log that data has been loaded
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, "Loading JourneyNotesFilter data completed"));
                    }
                    #endregion
                }
            }
            // As there is no serious drawback to this data being missing, just log that there was an execption.
            catch (SqlException sqle)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, "An SQL exception occurred whilst attempting to load the JourneyNotesFilter data.", sqle));
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, "An exception occurred whilst attempting to load the JourneyNotesFilter data.", ex));
            }
        }

		#endregion

	}
}
