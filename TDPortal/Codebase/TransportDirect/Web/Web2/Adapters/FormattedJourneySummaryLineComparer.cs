//*********************************** 
// NAME			: FormattedJourneySummaryLine.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 15/07/2004
// DESCRIPTION	: IComparer implementation for FormattedJourneySummaryLine
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FormattedJourneySummaryLineComparer.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:59:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:22   mturner
//Initial revision.
//
//   Rev 1.5   Mar 13 2006 15:28:56   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3.2.1   Feb 24 2006 10:28:38   pcross
//Added ability to compare by journey index. Helps with ordering of itinerary summary display
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3.2.0   Feb 10 2006 10:32:40   pcross
//Added ability to sort on DisplayNumber
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Feb 23 2006 19:16:10   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.0   Jan 10 2006 15:17:38   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Aug 16 2004 15:02:14   jgeorge
//IR1303
//
//   Rev 1.2   Jul 22 2004 17:10:00   jgeorge
//Bug fix
//
//   Rev 1.1   Jul 21 2004 18:21:12   RPhilpott
//Get rid of unreachable code warnings.
//
//   Rev 1.0   Jul 19 2004 15:22:12   jgeorge
//Initial revision.

using System;
using TransportDirect.Common.ResourceManager;
using System.Collections;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Class which represents a sort option for a FormattedJourneySummaryLine.
	/// It contains a column to sort by and a bool indicating ascending order 
	/// (true) or descending (false)
	/// </summary>
	[Serializable]
	public class FormattedJourneySummaryLineSortOption
	{
		private readonly JourneySummaryColumn column;
		private bool ascending;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="column"></param>
		/// <param name="ascending"></param>
		public FormattedJourneySummaryLineSortOption(JourneySummaryColumn column, bool ascending)
		{
			this.column = column;
			this.ascending = ascending;
		}

		/// <summary>
		/// Constructor. The column specified will be sorted in ascending order
		/// </summary>
		/// <param name="column"></param>
		public FormattedJourneySummaryLineSortOption(JourneySummaryColumn column) : this(column, true)
		{ }

		/// <summary>
		/// Returns the column to sort
		/// </summary>
		public JourneySummaryColumn Column
		{
			get { return column; }
		}

		/// <summary>
		/// Returns true if Column should be sorted in ascending order, otherwise
		/// returns false.
		/// </summary>
		public bool Ascending
		{
			get { return ascending; }
			set { ascending = value; }
		}

	}


	/// <summary>
	/// IComparer implementation for FormattedJourneySummaryLine
	/// </summary>
	[Serializable]
	public class FormattedJourneySummaryLineComparer : IComparer
	{
		private FormattedJourneySummaryLineSortOption[] sortSpecification;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="options">An array of FormattedJourneySummaryLineSortOption which indicates how to compare two items</param>
		public FormattedJourneySummaryLineComparer(FormattedJourneySummaryLineSortOption[] sortSpecification)
		{
			this.sortSpecification = (FormattedJourneySummaryLineSortOption[])sortSpecification.Clone();
		}

		/// <summary>
		/// Implementation of IComparer.Compare
		/// </summary>
		/// <param name="x">Must be a FormattedJourneySummaryLine object</param>
		/// <param name="y">Must be a FormattedJourneySummaryLine object</param>
		/// <returns>Less than zero - x is less than y. Zero - x equals y. Greater than zero - x is greater than y.</returns>
		/// <exception cref="ArgumentException"></exception>
		public int Compare(object x, object y)
		{
			int currResult = 0;

			// Allow comparing to a null - null is considered "smaller" than anything else
			if (x == null && y == null)
				return 0;
			else if (x == null)
				return -1;
			else if (y == null)
				return 1;

			// Cast both arguments
			FormattedJourneySummaryLine cX = x as FormattedJourneySummaryLine;
			FormattedJourneySummaryLine cY = y as FormattedJourneySummaryLine;

			// Raise an error if either is null
			if (cX == null || cY == null)
				throw new ArgumentException("Both parameters must be instances of FormattedJourneySummaryLine");

			if (sortSpecification.Length == 0)
				return 0;

			for (int index = 0; index < sortSpecification.Length; index++)
			{
				// Ignore the entry if it is not set
				if (sortSpecification[index] != null)
				{
					// Compare the two columns
					currResult = CompareColumn(sortSpecification[index].Column, cX, cY);

					// If we have a result, exit now
					if (currResult != 0)
					{
						// There is a difference at this level, so we need go no further
						if (sortSpecification[index].Ascending)
							return currResult;
						else
							return (currResult * -1);
					}
				}
			}
			// If we reach this point, the items are exactly the same as far as the current
			// sort specification is concerned. To ensure that sort order always remains the
			// same, we will now sort based on the unique id of the item
			return cX.JourneyIndex.CompareTo(cY.JourneyIndex);
		}

		/// <summary>
		/// Compares the specified column of the two FormattedJourneySummaryLines
		/// </summary>
		/// <param name="column"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private int CompareColumn(JourneySummaryColumn column, FormattedJourneySummaryLine x, FormattedJourneySummaryLine y)
		{
			switch (column)
			{
				case JourneySummaryColumn.JourneyIndex:
					return x.JourneyIndex.CompareTo(y.JourneyIndex);
					
				case JourneySummaryColumn.Origin:
					return x.OriginDescription.CompareTo(y.OriginDescription);
				
				case JourneySummaryColumn.Destination:
					return x.DestinationDescription.CompareTo(y.DestinationDescription);

				case JourneySummaryColumn.JourneyType:
					return x.Type.ToString().CompareTo(y.Type.ToString());

				case JourneySummaryColumn.Mode:
					return x.GetTransportModes().CompareTo(y.GetTransportModes());

				case JourneySummaryColumn.InterchangeCount:
					return x.InterchangeCount.CompareTo(y.InterchangeCount);

				case JourneySummaryColumn.DepartureTime:
					return x.DepartureDateTime.CompareTo(y.DepartureDateTime);

				case JourneySummaryColumn.ArrivalTime:
					return x.ArrivalDateTime.CompareTo(y.ArrivalDateTime);

				case JourneySummaryColumn.RoadMiles:
					return x.RoadMiles.CompareTo(y.RoadMiles);

				case JourneySummaryColumn.DisplayNumber:
					return x.DisplayNumber.ToString().CompareTo(y.DisplayNumber.ToString());

				case JourneySummaryColumn.Duration:
					return x.Duration.CompareTo(y.Duration);

				case JourneySummaryColumn.OperatorName:
					return String.Compare(String.Join(" ", x.OperatorNames), String.Join(" ", y.OperatorNames), true);
			}
			return 0;
		}

	}
}
