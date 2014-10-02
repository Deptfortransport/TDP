// *********************************************** 
// NAME                 : FindJourneySummaryData.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 16/07/2004 
// DESCRIPTION  : Holds information that can be used to
// reconstruct a FormattedJourneySummaryLines object
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindJourneySummaryData.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:26   mturner
//Initial revision.
//
//   Rev 1.0   Jul 19 2004 15:26:50   jgeorge
//Initial revision.

using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Holds information that can be used to reconstruct a FormattedJourneySummaryLines object
	/// </summary>
	[CLSCompliant(false), Serializable]
	public class FindJourneySummaryData
	{
		private readonly int journeyReferenceNumber;
		private readonly int[] summaryLineIndexes;
		private readonly int bestMatch;
		private readonly int requestedDay;
		private readonly double conversionFactor;
		private readonly string defaultOrigin;
		private readonly string defaultDestination;
		private readonly int maxNumberOfResults;
		private readonly JourneySummaryColumn timeColumnUsed;
		private readonly JourneySummaryColumn gridColumn;
		private readonly bool gridColumnAscending;
        
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="journeyReferenceNumber"></param>
		/// <param name="summaryLineIndexes"></param>
		/// <param name="bestMatch"></param>
		/// <param name="requestedDay"></param>
		/// <param name="conversionFactor"></param>
		/// <param name="defaultOrigin"></param>
		/// <param name="defaultDestination"></param>
		/// <param name="maxNumberOfResults"></param>
		/// <param name="timeColumnUsed"></param>
		/// <param name="gridColumn"></param>
		/// <param name="gridColumnAscending"></param>
		public FindJourneySummaryData(int journeyReferenceNumber, int[] summaryLineIndexes, int bestMatch, int requestedDay, double conversionFactor, string defaultOrigin, string defaultDestination, int maxNumberOfResults, JourneySummaryColumn timeColumnUsed, JourneySummaryColumn gridColumn, bool gridColumnAscending)
		{
			this.journeyReferenceNumber = journeyReferenceNumber;
			this.summaryLineIndexes = (int[])summaryLineIndexes.Clone();
			this.bestMatch = bestMatch;
			this.requestedDay = requestedDay;
			this.conversionFactor = conversionFactor;
			this.defaultOrigin = defaultOrigin;
			this.defaultDestination = defaultDestination;
			this.maxNumberOfResults = maxNumberOfResults;
			this.timeColumnUsed = timeColumnUsed;
			this.gridColumn = gridColumn;
			this.gridColumnAscending = gridColumnAscending;
		}

		/// <summary>
		/// Reference number that these summary details apply to
		/// </summary>
		public int JourneyReferenceNumber
		{
			get { return journeyReferenceNumber; }
		}

		/// <summary>
		/// Indexes of the JourneySummaryLines used in the results, in order
		/// </summary>
		public int[] SummaryLineIndexes
		{
			get { return (int[])summaryLineIndexes.Clone(); }
		}

		/// <summary>
		/// Index of the most closely matching line
		/// </summary>
		public int BestMatch
		{
			get { return bestMatch; }
		}

		/// <summary>
		/// The RequestedDay
		/// </summary>
		public int RequestedDay
		{
			get { return requestedDay; }
		}

		/// <summary>
		/// Conversion factor used for milage
		/// </summary>
		public double ConversionFactor
		{
			get { return conversionFactor; }
		}

		/// <summary>
		/// Default origin for the summary display
		/// </summary>
		public string DefaultOrigin
		{
			get { return defaultOrigin; }
		}

		/// <summary>
		/// Default destination for the summary display
		/// </summary>
		public string DefaultDestination
		{
			get { return defaultDestination; }
		}

		/// <summary>
		/// Maximum number of results used to generate the results set
		/// </summary>
		public int MaxNumberOfResults
		{
			get { return maxNumberOfResults; }
		}

		/// <summary>
		/// The column used for the secondary sorting key (either Arrival or Departure time)
		/// </summary>
		public JourneySummaryColumn TimeColumnUsed
		{
			get { return timeColumnUsed; }
		}

		/// <summary>
		/// The column used to sort the grid
		/// </summary>
		public JourneySummaryColumn GridColumn
		{
			get { return gridColumn; }
		}

		/// <summary>
		/// The order in which the column used to sort the grid is sorted
		/// </summary>
		public bool GridColumnAscending
		{
			get { return gridColumnAscending; }
		}

	}
}
