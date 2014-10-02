//*********************************** 
// NAME			: FormattedJourneySummaryLines.cs
// AUTHOR		: James Haydock
// DATE CREATED	: 17.05.2004
// DESCRIPTION	: 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FormattedJourneySummaryLines.cs-arc  $
//
//   Rev 1.5   Dec 06 2010 12:54:56   apatel
//Code updated to implement show all show 10 feature for journey results and to remove anytime option from the input page.
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.4   Jul 03 2008 11:33:46   mmodi
//Updated to ensure car journey is always sorted to be last for Extend journey
//Resolution for 5037: Modify journey - car journey not shown at bottom
//
//   Rev 1.3   May 08 2008 14:17:38   dgath
//Fixed non-working sort-by-column functionality.
//Resolution for 4956: In FAT when viewing all results sort buttons do nothing.
//
//   Rev 1.2   Mar 31 2008 12:59:02   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory  Mar 08 2007 18:00:00   mmodi
//Fix applied when building array of summary lines, as requested by Apps Support
//
//   Rev 1.0   Nov 08 2007 13:11:22   mturner
//Initial revision.
//
//   Rev 1.15   Apr 28 2006 13:13:04   COwczarek
//Add functionaly to  support different sort order/default selection
//based on summary type (Find A, Extend, etc.)
//Resolution for 3970: DN068 Extend: Wrong choice of default journey in Extension options
//
//   Rev 1.14   Apr 04 2006 15:49:46   RGriffith
//IR3701 Fix: Addition of direction to FormattedJourneySummaryLine adapter to set correct origin/destination locations
//
//   Rev 1.13   Feb 23 2006 19:16:10   build
//Automatically merged from branch for stream3129
//
//   Rev 1.12.1.0   Jan 10 2006 15:17:38   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.12   Mar 07 2005 18:58:50   rhopkins
//If FindAMode is cost-based then force sorting to work as though the User had selected "leave after"
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.11   Sep 07 2004 15:01:08   jmorrissey
//IR1513 - changed how best match is set for arrive by journeys and don't try to set it if there are no journey summary lines to display
//
//   Rev 1.10   Sep 06 2004 16:13:20   jmorrissey
//IR 1513 and IR 1524
//
//   Rev 1.9   Sep 04 2004 10:37:02   passuied
//Changes so the results are not sorted by depart time by default when AnyTime selected
//Resolution for 1478: Find a choice arrive by journeys sorted incorrectly
//
//   Rev 1.8   Sep 03 2004 18:33:04   passuied
//Changes to be able to have default sorting as descending for Arrive column
//Resolution for 1471: Find a train arrive by results sorted in wrong order
//
//   Rev 1.7   Sep 02 2004 15:58:38   jmorrissey
//Fix for bug in FindA when no best match journeys found
//
//   Rev 1.6   Aug 24 2004 16:42:36   jgeorge
//IR1366
//
//   Rev 1.5   Aug 20 2004 12:13:20   jgeorge
//IR1338
//
//   Rev 1.4   Jul 19 2004 15:24:46   jgeorge
//Del 6.1 updates

using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using System.ComponentModel;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Web.Support;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.SessionManager;

namespace TransportDirect.UserPortal.Web.Adapters
{

    /// <summary>
    /// Type that determines what function the summary is for
    /// </summary>
    public enum FormattedSummaryType {FindA, Extend, Replan, Adjust}

    /// <summary>
	/// Summary description for FormattedJourneySummaryLines.
	/// </summary>
	[Serializable]
	public class FormattedJourneySummaryLines : IListSource, IEnumerable, IEnumerator
	{
		#region Private variables

		// The current array of lines and the options used to sort them
		private FormattedJourneySummaryLine[] formattedLines;
		private FormattedJourneySummaryLineSortOption[] gridSortOptions;
		
		// The index of the closest match
		private int bestMatch = 0;
		private int maxNumberOfResults = 0;
		private int journeyReferenceNumber = 0;

		private FindAMode findAMode = FindAMode.None;
        private FormattedSummaryType summaryType;
        private TDDateTime dateTime;
        private bool anyTime;
		private bool leaveAfter;
		private int requestedDay;
		private double conversionFactor;
		private string defaultOrigin;
		private string defaultDestination;
        private JourneySummaryColumn sortedColumn;
        private bool sortedAscending;
        private bool outward;
        private ModeType[] modeTypes;

		//Used to keep track of current collection enumerator location
		private int pos = -1;
		
		#endregion

		#region Constructor

		/// <summary>
		/// Constructor that can be called to create an empty FormattedJourneySummaryLines object.
		/// </summary>
		public FormattedJourneySummaryLines()
		{
			formattedLines = new FormattedJourneySummaryLine[0];
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="journeySummaryLines">The JourneySummaryLine objects to format</param>
		/// <param name="dateTime">The date/time selected for the specified Journey</param>
		/// <param name="anyTime">If True, dateTime will be ignored.</param>
		/// <param name="leaveAfter">True indicates that Leave After was selected, False indicates Arrive Before.</param>
		/// <param name="requestedDay"></param>
		/// <param name="conversionFactor"></param>
		/// <param name="defaultOrigin"></param>
		/// <param name="defaultDestination"></param>
		/// <param name="maxNumberOfResults"></param>
		/// <param name="outward">Bool to determine direction of FormattedJourneySummaryLine</param>
		/// <param name="summaryType">The type of summary</param>
		public FormattedJourneySummaryLines(
            int journeyReferenceNumber, 
            JourneySummaryLine[] journeySummaryLines, 
            TDDateTime dateTime, 
            bool anyTime, 
            bool leaveAfter, 
            int requestedDay, 
            double conversionFactor,
            string defaultOrigin,
            string defaultDestination,
            int maxNumberOfResults,
            JourneySummaryColumn sortedColumn,
            bool sortedAscending,
            FindAMode findAMode,
            bool outward,
            FormattedSummaryType summaryType,
            ModeType[] modeTypes)
		{
			this.journeyReferenceNumber = journeyReferenceNumber;
			this.maxNumberOfResults = maxNumberOfResults;
			this.requestedDay = requestedDay;
			this.conversionFactor = conversionFactor;
			this.defaultOrigin = defaultOrigin;
			this.defaultDestination = defaultDestination;
			this.findAMode = findAMode;
            this.summaryType = summaryType;
            this.anyTime = anyTime;
            this.dateTime = dateTime;
            this.sortedAscending = sortedAscending;
            this.sortedColumn = sortedColumn;
            this.outward = outward;
            this.modeTypes = modeTypes;

			// For a cost-based search, always treat as a "leave after" request (used for sorting)
			if (FindInputAdapter.IsCostBasedSearchMode(findAMode))
			{
				this.leaveAfter = true;
			}
			else
			{
				this.leaveAfter = leaveAfter;
			}

			// Build the array of formatted lines. At the same time, locate the best match
			formattedLines = new FormattedJourneySummaryLine[journeySummaryLines.Length];
	
            for (int i = 0; i < journeySummaryLines.Length; i++)
			{
				formattedLines[i] = new FormattedJourneySummaryLine(journeySummaryLines[i], i,  requestedDay, conversionFactor, defaultOrigin, defaultDestination, outward);
			}

            // Perform the sort after we've created the new array
            sortSummaryLines();

            int bestMatchIndex = 0;

            //Don't try and set best match if there are no journeys
            if (formattedLines.Length > 0)
			{
				bestMatchIndex = determineBestMatchIndex();
                bestMatch = formattedLines[bestMatchIndex].JourneyIndex;
            }

            // If it is necessary to limit the number of results, do so now
            if (summaryType == FormattedSummaryType.FindA) 
            {
                if ( (maxNumberOfResults > 0) && (maxNumberOfResults < formattedLines.Length) )
                {
                    // Work out the start index.
                    int startIndex = bestMatchIndex - ( leaveAfter ? 4 : 6 ) ;
                    // If there are fewer than the required number of items before the best match,
                    // ensure we don't have an illegal start value
                    if ( startIndex < 0 )
                        startIndex = 0;

                    // If there are fewer than the required number of items after the best match,
                    // ensure we don't have an illegal start value
                    if ( startIndex > ( formattedLines.Length - maxNumberOfResults ) )
                        startIndex = formattedLines.Length - maxNumberOfResults;

                    // Copy the data into a temp array
                    FormattedJourneySummaryLine[] tempLines = new FormattedJourneySummaryLine[maxNumberOfResults];
                    Array.Copy(formattedLines, startIndex, tempLines, 0, maxNumberOfResults);

                    // Copy the temp array over the old one
                    formattedLines = tempLines;

                    if (SetUserSortOption(sortedColumn, sortedAscending))
                        Array.Sort(formattedLines, new FormattedJourneySummaryLineComparer( gridSortOptions ));
                }
            }

		}


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="journeySummaryLines">The JourneySummaryLine objects to format</param>
        /// <param name="dateTime">The date/time selected for the specified Journey</param>
        /// <param name="anyTime">If True, dateTime will be ignored.</param>
        /// <param name="leaveAfter">True indicates that Leave After was selected, False indicates Arrive Before.</param>
        /// <param name="requestedDay"></param>
        /// <param name="conversionFactor"></param>
        /// <param name="defaultOrigin"></param>
        /// <param name="defaultDestination"></param>
        /// <param name="maxNumberOfResults"></param>
        /// <param name="outward">Bool to determine direction of FormattedJourneySummaryLine</param>
        /// <param name="summaryType">The type of summary</param>
        public FormattedJourneySummaryLines(
            int journeyReferenceNumber,
            JourneySummaryLine[] journeySummaryLines,
            TDDateTime dateTime,
            bool anyTime,
            bool leaveAfter,
            int requestedDay,
            double conversionFactor,
            string defaultOrigin,
            string defaultDestination,
            int maxNumberOfResults,
            JourneySummaryColumn sortedColumn,
            bool sortedAscending,
            FindAMode findAMode,
            bool outward,
            FormattedSummaryType summaryType,
            ModeType[] modeTypes,
            int selectedJourneyIndex)
        {
            this.journeyReferenceNumber = journeyReferenceNumber;
            this.maxNumberOfResults = maxNumberOfResults;
            this.requestedDay = requestedDay;
            this.conversionFactor = conversionFactor;
            this.defaultOrigin = defaultOrigin;
            this.defaultDestination = defaultDestination;
            this.findAMode = findAMode;
            this.summaryType = summaryType;
            this.anyTime = anyTime;
            this.dateTime = dateTime;
            this.sortedAscending = sortedAscending;
            this.sortedColumn = sortedColumn;
            this.outward = outward;
            this.modeTypes = modeTypes;

            // For a cost-based search, always treat as a "leave after" request (used for sorting)
            if (FindInputAdapter.IsCostBasedSearchMode(findAMode))
            {
                this.leaveAfter = true;
            }
            else
            {
                this.leaveAfter = leaveAfter;
            }

            // Build the array of formatted lines. At the same time, locate the best match
            formattedLines = new FormattedJourneySummaryLine[journeySummaryLines.Length];

            for (int i = 0; i < journeySummaryLines.Length; i++)
            {
                formattedLines[i] = new FormattedJourneySummaryLine(journeySummaryLines[i], i, requestedDay, conversionFactor, defaultOrigin, defaultDestination, outward);
            }

            // Perform the sort after we've created the new array
            sortSummaryLines();

            int bestMatchIndex = 0;

            if (selectedJourneyIndex >= 0)
            {
                bestMatchIndex = IndexFromJourneyIndex(selectedJourneyIndex);
                bestMatch = selectedJourneyIndex;
            }
            else if (formattedLines.Length > 0) //Don't try and set best match if there are no journeys
            {
                bestMatchIndex = determineBestMatchIndex();
                bestMatch = formattedLines[bestMatchIndex].JourneyIndex;
            }
            

            // If it is necessary to limit the number of results, do so now
            if (summaryType == FormattedSummaryType.FindA)
            {
                if ((maxNumberOfResults > 0) && (maxNumberOfResults < formattedLines.Length))
                {
                    // Work out the start index.
                    int startIndex = bestMatchIndex - (leaveAfter ? 4 : 6);
                    // If there are fewer than the required number of items before the best match,
                    // ensure we don't have an illegal start value
                    if (startIndex < 0)
                        startIndex = 0;

                    // If there are fewer than the required number of items after the best match,
                    // ensure we don't have an illegal start value
                    if (startIndex > (formattedLines.Length - maxNumberOfResults))
                        startIndex = formattedLines.Length - maxNumberOfResults;

                    // Copy the data into a temp array
                    FormattedJourneySummaryLine[] tempLines = new FormattedJourneySummaryLine[maxNumberOfResults];
                    Array.Copy(formattedLines, startIndex, tempLines, 0, maxNumberOfResults);

                    // Copy the temp array over the old one
                    formattedLines = tempLines;

                    if (SetUserSortOption(sortedColumn, sortedAscending))
                        Array.Sort(formattedLines, new FormattedJourneySummaryLineComparer(gridSortOptions));
                }
            }

        }


        /// <summary>
        /// Sorts summary lines. Sort criteria depends on summary type
        /// </summary>
        private void sortSummaryLines() 
        {
            if (summaryType == FormattedSummaryType.Extend) 
            {
                sortExtendSummaryLines();
                setLastJourneyCar();
            } 
            else if (summaryType == FormattedSummaryType.FindA)
            {
                sortFindASummaryLines();
            } 
        }

        /// <summary>
        /// Sorts summary lines for extend summary
        /// </summary>
        private void sortExtendSummaryLines() 
        {

            gridSortOptions = new FormattedJourneySummaryLineSortOption[2];

            FormattedJourneySummaryLineSortOption timeSort = null;
            FormattedJourneySummaryLineSortOption durationSort = null;

            if (leaveAfter) 
            {
                // Extension from destination

                if (outward) 
                {
                    timeSort = new FormattedJourneySummaryLineSortOption(JourneySummaryColumn.ArrivalTime, true);
                } 
                else 
                {
                    timeSort = new FormattedJourneySummaryLineSortOption(JourneySummaryColumn.DepartureTime, false);
                }
            } 
            else 
            {
                
                // Extension to origin

                if (outward) 
                {
                    timeSort = new FormattedJourneySummaryLineSortOption(JourneySummaryColumn.DepartureTime, false);
                } 
                else 
                {
                    timeSort = new FormattedJourneySummaryLineSortOption(JourneySummaryColumn.ArrivalTime, true);
                }
            }

            gridSortOptions[0] = timeSort;
            gridSortOptions[1] = durationSort;

            // Sort the list
            Array.Sort(formattedLines, new FormattedJourneySummaryLineComparer( gridSortOptions ));

        }

        /// <summary>
        /// Sorts summary lines for Find A summary
        /// </summary>
        private void sortFindASummaryLines() 
        {

            gridSortOptions = new FormattedJourneySummaryLineSortOption[3];

            // Work out how the results will be sorted
            FormattedJourneySummaryLineSortOption timeSort = null;
            FormattedJourneySummaryLineSortOption durationSort = null;

            if (leaveAfter)
                // Sorting by departure time ascending
                timeSort = new FormattedJourneySummaryLineSortOption(JourneySummaryColumn.DepartureTime, true);
            else
                // Sorting by arrival time descending!
                timeSort = new FormattedJourneySummaryLineSortOption(JourneySummaryColumn.ArrivalTime, false);
			
            durationSort = new FormattedJourneySummaryLineSortOption(JourneySummaryColumn.Duration, true);

            gridSortOptions[1] = timeSort;
            gridSortOptions[2] = durationSort;

            // If we have to limit the number of results, we need to first sort without the 
            // user specified column. Otherwise we can do it now.
            if ( (maxNumberOfResults > 0) && (maxNumberOfResults < formattedLines.Length) )
                gridSortOptions[0] = null;
            else
                SetUserSortOption(sortedColumn, sortedAscending);
			
            // Sort the list
            Array.Sort(formattedLines, new FormattedJourneySummaryLineComparer( gridSortOptions ));
        
        }

        /// <summary>
        /// Method which places the car journey to be last in the sorted list.
        /// This method should only be called after a sort has been done, as it does not change the
        /// order of Public journeys
        /// </summary>
        private void setLastJourneyCar()
        {
            // Create the temp array
            FormattedJourneySummaryLine[] tempLines = new FormattedJourneySummaryLine[formattedLines.Length];

            // Counter position used to place in temp array
            int x = 0;

            for (int i = 0; i < formattedLines.Length; i++)
            {
                if (formattedLines[i].Type != TDJourneyType.RoadCongested)
                {
                    // Not a car journey, so copy in to the temp array
                    tempLines[x] = formattedLines[i];
                    x++;
                }
                else
                {   // Its a car journey, so place at the end of the temp array
                    tempLines[formattedLines.Length - 1] = formattedLines[i];
                }
            }

            // Copy the temp array over the old one
            formattedLines = tempLines;
        }

        /// <summary>
        /// Determines the default selection for the summary. The logic to determine default selection
        /// will vary based on the purpose of the summary (Find A, Extend, etc.)
        /// </summary>
        /// <returns>The row index of the default selection</returns>
        private int determineBestMatchIndex() 
        {
            if (summaryType == FormattedSummaryType.Extend) 
            {
                return determineExtendBestMatchIndex();
            } 
            else if (summaryType == FormattedSummaryType.FindA)
            {
                return determineFindABestMatchIndex(modeTypes);
            } 
            else 
            {
                return 0;
            }

        }

        /// <summary>
        /// Determines the default selection for an extend summary.
        /// If the extension is to the origin of the orignal journey then the best journey is
        /// that which departs the latest.
        /// If the extension is from the destination of the	orignal journey then the best journey is
        /// that which arrives the earliest.
        /// A road journey is always excluded unless it is the only journey in which case it becomes
        /// the default selection.
        /// </summary>
        /// <returns>The row index of the default selection</returns>
        private int determineExtendBestMatchIndex() 
        {
            int bestMatchIndex = 0;

            if (leaveAfter)
            {
                TDDateTime earliestArriveTime = new TDDateTime(DateTime.MaxValue);

                for (int i = 0; i < formattedLines.Length; i++)
                {
                    TDDateTime arriveTime = formattedLines[i].ArrivalDateTime.GetDateTime();
                    if (arriveTime < earliestArriveTime && formattedLines[i].Type != TDJourneyType.RoadCongested) 
                    {
                        earliestArriveTime = arriveTime;
                        bestMatchIndex = i;
                    }
                }
            }
            else
            {

                TDDateTime latestDepartTime = new TDDateTime(DateTime.MinValue);

                for (int i = 0; i < formattedLines.Length; i++)
                {
                    TDDateTime departTime = formattedLines[i].DepartureDateTime.GetDateTime();
                    if (departTime > latestDepartTime && formattedLines[i].Type != TDJourneyType.RoadCongested) 
                    {
                        latestDepartTime = departTime;
                        bestMatchIndex = i;
                    }
                }

            }

            return bestMatchIndex;
        }

        /// <summary>
        /// Determines the default selection for a Find A summary.
        /// If the user specified that they wish to leave after a time, the best match is the
        /// first match with a departure time after that time, or if none are then the last entry
        /// in the list.
        /// If the user specified that they wish to arrive before a time, the best match is the
        /// last match with an arrival time before that time, or if none are then the last
        /// entry in the list		
        /// </summary>
        /// <returns>The row index of the default selection</returns>
        private int determineFindABestMatchIndex(ModeType[] modeTypes) 
        {
            int bestMatchIndex = 0;

            if (!anyTime) 
            {
                TimeSpan thisInterval = TimeSpan.MaxValue;

                if (leaveAfter)
                {
                    #region Leave After
                    for (bestMatchIndex = 0; bestMatchIndex < formattedLines.Length; bestMatchIndex++)
                    {
                        // If modeTypes has been passed then need to ensure the best match journey
                        // is for that modeType
                        if ((modeTypes != null) && (modeTypes.Length > 0))
                        {
                             // Is this formatted lines modes in the requested modeTypes
                            ModeType[] summaryLineModeTypes = formattedLines[bestMatchIndex].ModeTypes;
                            if (TDJourneyResult.UseJourneyHelper(summaryLineModeTypes, modeTypes))
                            {
                                thisInterval = formattedLines[bestMatchIndex].DepartureDateTime.GetDateTime() - dateTime.GetDateTime();
                            }
                        }
                        else
                            thisInterval = formattedLines[bestMatchIndex].DepartureDateTime.GetDateTime() - dateTime.GetDateTime();

                        // If the value is 0 or positive, then we have found the best match. Otherwise continue
                        if ( thisInterval.CompareTo(TimeSpan.Zero) >= 0 )
                            break;
                    }
                    #endregion
                }
                else
                {
                    thisInterval = TimeSpan.MinValue;

                    #region Arrive Before
                    //this is now handled in a similar way to leaveafter, because the journey summary lines
                    //are already sorted by arrival time
                    for (bestMatchIndex = 0; bestMatchIndex < formattedLines.Length; bestMatchIndex++)
                    {
                        // If modeTypes has been passed then need to ensure the best match journey
                        // is for that modeType
                        if ((modeTypes != null) && (modeTypes.Length > 0))
                        {
                            // Is this formatted lines modes in the requested modeTypes
                            ModeType[] summaryLineModeTypes = formattedLines[bestMatchIndex].ModeTypes;
                            if (TDJourneyResult.UseJourneyHelper(summaryLineModeTypes, modeTypes))
                            {
                                thisInterval = formattedLines[bestMatchIndex].ArrivalDateTime.GetDateTime() - dateTime.GetDateTime();
                            }
                        }
                        else
                            thisInterval = formattedLines[bestMatchIndex].ArrivalDateTime.GetDateTime() - dateTime.GetDateTime();
                        // If the value is less than 0, then we have found the best match. Otherwise continue
                        if ( thisInterval.CompareTo(TimeSpan.Zero) <= 0 )													
                            break;
                    }
                    #endregion
                }

                //if the bestMatchIndex is still greater than the length of 
                //the number of journeys, then bestMatchIndex to the last journey
                if (bestMatchIndex >= formattedLines.Length)
                {
                    bestMatchIndex = (formattedLines.Length - 1);
                }
                    //if there are no journeys, then set the bestMatchIndex to zero
                else if (bestMatchIndex < 0)
                {
                    bestMatchIndex = 0;
                }

            }
                       
            return bestMatchIndex;
        }

		/// <summary>
		/// Constructor used to recreate the object from stored data
		/// </summary>
		/// <param name="savedData">The object containing saved data about the formatted lines</param>
		/// <param name="sourceLines">The original source lines used to recreate the object</param>
		public FormattedJourneySummaryLines(FindJourneySummaryData savedData, JourneySummaryLine[] sourceLines)
		{
			maxNumberOfResults = savedData.MaxNumberOfResults;
			bestMatch = savedData.BestMatch;
			maxNumberOfResults = savedData.MaxNumberOfResults;
			journeyReferenceNumber = savedData.JourneyReferenceNumber;

			requestedDay = savedData.RequestedDay;
			conversionFactor = savedData.ConversionFactor;
			defaultOrigin = savedData.DefaultOrigin;
			defaultDestination = savedData.DefaultDestination;

			int[] lineIndexes = savedData.SummaryLineIndexes;

            // Build the array of formatted lines.
            formattedLines = new FormattedJourneySummaryLine[lineIndexes.Length];
            for (int i = 0; i < lineIndexes.Length; i++)
                formattedLines[i] = new FormattedJourneySummaryLine(sourceLines[lineIndexes[i]], lineIndexes[i], requestedDay, conversionFactor, defaultOrigin, defaultDestination, true);

            gridSortOptions = new FormattedJourneySummaryLineSortOption[3];

			if (savedData.GridColumn != JourneySummaryColumn.None)
				gridSortOptions[0] = new FormattedJourneySummaryLineSortOption(savedData.GridColumn, savedData.GridColumnAscending);
			else
				gridSortOptions[0] = null;

			gridSortOptions[1] = new FormattedJourneySummaryLineSortOption(savedData.TimeColumnUsed, savedData.GridColumnAscending);
			gridSortOptions[2] = new FormattedJourneySummaryLineSortOption(JourneySummaryColumn.Duration, true);
		}

		#endregion

		#region Indexer

		/// <summary>
		/// Returns a FormattedJourneySummaryLine by it's index
		/// </summary>
		public FormattedJourneySummaryLine this[int index]
		{
			get { return formattedLines[index]; }
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Returns the index of the item which is the closest match to the
		/// options given
		/// </summary>
		public int BestMatch
		{
			get { return bestMatch; }
		}

		/// <summary>
		/// Returns the value that was used to restrict the number of results
		/// </summary>
		public int MaxNumberOfResults
		{
			get { return maxNumberOfResults; }
		}

		/// <summary>
		/// Returns the current option being used to sort the grid.
		/// </summary>
		private FormattedJourneySummaryLineSortOption CurrentSortOption
		{
			get 
			{
				if (gridSortOptions[0] == null)
					return gridSortOptions[1];
				else
					return gridSortOptions[0];
			}
		}

		/// <summary>
		/// Returns the current column being used to sort the grid
		/// </summary>
		public JourneySummaryColumn CurrentSortColumn
		{
			get { return CurrentSortOption.Column; }
		}

		/// <summary>
		/// Returns the current sort order of the column being used to sort the grid
		/// </summary>
		public bool CurrentSortColumnAscending
		{
			get { return CurrentSortOption.Ascending; }
		}

		/// <summary>
		/// Returns whether the collection is a collection of IList objects
		/// </summary>
		//IListSource interface implementation
		public bool ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Returns the number of FormattedJourneySummaryLine objects in the colletion
		/// </summary>
		public int Count
		{
			get { return formattedLines.Length; }
		}

		/// <summary>
		/// Returns the current enumerated item in the collection.
		/// </summary>
		//IEnumerator interface implementation
		public object Current
		{
			get { return (object)formattedLines[pos]; }
		}

		/// <summary>
		/// Returns the collection's foreach enumerator interface.
		/// </summary>
		/// <returns></returns>
		//IEnumerable interface implementation
		public IEnumerator GetEnumerator()
		{
			pos = -1;
			return (IEnumerator)this;
		}

		/// <summary>
		/// Returns collection's array list
		/// </summary>
		/// <returns></returns>
		//IListSource interface implementation
		public IList GetList()
		{
			return (IList)formattedLines;
		}

		/// <summary>
		/// Returns the JourneyIndex given a FormattedJourneySummaryLines index position
		/// </summary>
		/// <param name="journeyIndex"></param>
		/// <returns></returns>
		public int IndexFromJourneyIndex(int journeyIndex)
		{
			for (int index = 0; index < formattedLines.Length; index++)
			{
				if ( formattedLines[index].JourneyIndex == journeyIndex )
					return index;
			}

			return -1;
		}

		/// <summary>
		/// Increments the collection's enumerator counter.
		/// </summary>
		/// <returns></returns>
		//IEnumerator interface implementation
		public bool MoveNext()
		{
			if (pos < formattedLines.Length - 1)
			{
				pos++;
				return true;
			}

			return false;
		}

		/// <summary>
		/// Resets the collection's enumerator counter.
		/// </summary>
		//IEnumerator interface implementation
		public void Reset()
		{
			pos = -1;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Sorts the data by the specified column
		/// </summary>
		public void Sort(JourneySummaryColumn column, bool ascending)
		{
			// timeSort and durationSort are created in the constructor, so we can reuse them here
			if (SetUserSortOption(column, ascending))
				Array.Sort(formattedLines, new FormattedJourneySummaryLineComparer( gridSortOptions ));
		}

		/// <summary>
		/// Returns an array of integers which are indexes into the original
		/// array of JourneySummaryLines. This can be passed back into the 
		/// object to reconstruct the array without having to do the sorting
		/// and "find best" done in the original constructor
		/// </summary>
		/// <returns></returns>
		private int[] JourneySummaryLines()
		{
			int[] results = new int[formattedLines.Length];
			for (int index = 0; index < results.Length; index++)
				results[index] = formattedLines[index].OriginalArrayIndex;
			return results;
		}

		/// <summary>
		/// Method which returns the default sorting for option 1.
		/// This options is the Leave after/Arrive by option.
		/// Leave After returns true (ascending). Arrive by returns false (descending)
		/// </summary>
		public bool GetDefaultOption1Sorting(JourneySummaryColumn column)
		{
			if( column == JourneySummaryColumn.ArrivalTime)
				return false;
			else
				return true;
		
		}
		public FindJourneySummaryData GetSavedSummaryData()
		{
			if (formattedLines.Length == 0)
			{
				return null;
			}
			else
			{
				return new FindJourneySummaryData(journeyReferenceNumber, JourneySummaryLines(), bestMatch, requestedDay, conversionFactor, defaultOrigin, defaultDestination, maxNumberOfResults, gridSortOptions[1].Column, gridSortOptions[0] == null ? JourneySummaryColumn.None : gridSortOptions[0].Column, gridSortOptions[0] == null ? gridSortOptions[1].Ascending : gridSortOptions[0].Ascending);
			}
		}

        /// <summary>
        /// Method which reselects the best match journey
        /// </summary>
        /// <param name="modeTypes">Supply modeTypes if the selected journey should be based on this</param>
        public void ResetBestMatch(TDDateTime dateTime, bool anyTime, bool leaveAfter, ModeType[] modeTypes)
        {
            this.dateTime = dateTime;
            this.anyTime = anyTime;
            this.leaveAfter = leaveAfter;
            this.modeTypes = modeTypes;

            int bestMatchIndex = 0;

            if (formattedLines.Length > 0)
            {
                bestMatchIndex = determineBestMatchIndex();
                bestMatch = formattedLines[bestMatchIndex].JourneyIndex;
            }
        }

        

		#endregion

		#region Helper methods

		/// <summary>
		/// Sets the first element in the array of sort options.
		/// </summary>
		/// <param name="column"></param>
		/// <param name="ascending"></param>
		/// <returns>true if the sort options were changed</returns>
		private bool SetUserSortOption(JourneySummaryColumn column, bool ascending)
		{
			bool changed = false;

			if ((gridSortOptions[1].Column == column) || (column == JourneySummaryColumn.None))
				// The user has specified that the default sort order be used
			{
				if ((gridSortOptions[0] != null) || (gridSortOptions[1].Ascending != ascending))
				{
					gridSortOptions[0] = null;
					// IR 1471 : This is a non performant code change because we don't want to break the whole system.
					// Theoretically, if the column parameter is none, it means the user hasn't chosen any column yet
					// and therefore we wouldn't need to change the Ascending value because it was set right before...
					// However, it might happen for another reason, that we ignore at this very moment.
					// Thus, it is safer to check if the column stored in option 1 is Arrive by,
					// and then force it to be descending (Only required case so far).
					// Good luck with this explanation!
					if (column == JourneySummaryColumn.None)
					{
						gridSortOptions[1].Ascending = GetDefaultOption1Sorting(gridSortOptions[1].Column);

					}
					else
					{
						gridSortOptions[1].Ascending = ascending;
					}
					changed = true;
				}
			}
			else
				// A non-standard sort order has been requested
			{
				if ((gridSortOptions[0] == null) || (gridSortOptions[0].Column != column) || (gridSortOptions[0].Ascending != ascending) || (gridSortOptions[1].Ascending != GetDefaultOption1Sorting(gridSortOptions[1].Column)))
				{
					gridSortOptions[0] = new FormattedJourneySummaryLineSortOption(column, ascending);
					gridSortOptions[1].Ascending = GetDefaultOption1Sorting(gridSortOptions[1].Column);
					changed = true;
				}
			}
			return changed;
		}

		#endregion
	}
}
