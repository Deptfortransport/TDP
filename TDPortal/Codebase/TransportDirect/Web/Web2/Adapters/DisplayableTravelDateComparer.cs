//*********************************** 
// NAME			: DisplayableTravelDateComparer.cs
// AUTHOR		: Richard Hopkins
// DATE CREATED	: 11/02/2005
// DESCRIPTION	: IComparer implementation for DisplayableTravelDate
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/DisplayableTravelDateComparer.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:58:54   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:14   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 19:16:04   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.1   Jan 30 2006 12:15:18   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3.1.0   Jan 10 2006 15:17:32   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Nov 01 2005 15:11:42   build
//Automatically merged from branch for stream2638
//
//   Rev 1.2.1.0   Sep 19 2005 11:05:02   jbroome
//Updated for Visit Planner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Mar 23 2005 16:50:08   rhopkins
//Fixed FxCop warning
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.1   Feb 25 2005 13:19:24   rhopkins
//Corrections to sorting logic
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.0   Feb 15 2005 20:48:14   rhopkins
//Initial revision.
//

using System;
using System.Collections;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Web.Support;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// IComparer implementation for DisplayableTravelDate
	/// </summary>
	[Serializable]
	public class DisplayableTravelDateComparer : IComparer
	{
		private DisplayableTravelDateSortOption sortSpecification;
		private Hashtable travelModeTranslation = new Hashtable();

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="options">A single DisplayableTravelDateSortOption which indicates how to compare two items</param>
		public DisplayableTravelDateComparer(DisplayableTravelDateSortOption sortSpecification)
		{
			this.sortSpecification = sortSpecification;

			// Preload translations of all possible travel modes to avoid having to
			// repeatedly translate the Mode of each TravelDate during each comparison
			TDResourceManager resourceManager = TDResourceManager.GetResourceManagerFromCache(TDResourceManager.FIND_A_FARE_RM);
			string translatedTravelMode = String.Empty;

			foreach (string travelMode in Enum.GetNames(typeof(TicketTravelMode)))
			{
				translatedTravelMode = resourceManager.GetString("FindFare.TransportMode." + travelMode);
				if ( (translatedTravelMode == null) || (translatedTravelMode.Length == 0) )
				{
					travelModeTranslation.Add(travelMode, " ");
				}
				else
				{
					travelModeTranslation.Add(travelMode, translatedTravelMode);
				}
			}
		}

		/// <summary>
		/// Implementation of IComparer.Compare
		/// </summary>
		/// <param name="x">Must be a DisplayableTravelDate object</param>
		/// <param name="y">Must be a DisplayableTravelDate object</param>
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
			DisplayableTravelDate cX = x as DisplayableTravelDate;
			DisplayableTravelDate cY = y as DisplayableTravelDate;

			// Raise an error if either is null
			if (cX == null || cY == null)
				throw new ArgumentException("Both parameters must be instances of DisplayableTravelDate");

			// Compare the two columns
			currResult = CompareColumn(sortSpecification.Column, cX, cY);

			// If we have a result, exit now
			if (currResult != 0)
			{
				// If we are sorting into descending order then we need to invert the comparisson result
				if (sortSpecification.Ascending)
					return currResult;
				else
					return (currResult * -1);
			}
			else
			{
				// If we reach this point, the items are exactly the same as far as the current
				// sort specification is concerned. To ensure that sort order always remains the
				// same, we will now sort based on the unique id of the item
				return cX.Index.CompareTo(cY.Index);
			}
		}

		/// <summary>
		/// Compares the specified column of the two DisplayableTravelDates
		/// </summary>
		/// <param name="column"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private int CompareColumn(DisplayableTravelDateColumn column, DisplayableTravelDate x, DisplayableTravelDate y)
		{
			switch (column)
			{
				case DisplayableTravelDateColumn.OutwardDate:
					if ((x.OutwardDate == null) || (y.OutwardDate == null))
					{
						return 0;
					}
					else
					{
						return x.OutwardDate.CompareTo(y.OutwardDate);
					}

				case DisplayableTravelDateColumn.ReturnDate:
					if ((x.ReturnDate == null) || (y.ReturnDate == null))
					{
						return 0;
					}
					else
					{
						return x.ReturnDate.CompareTo(y.ReturnDate);
					}

				case DisplayableTravelDateColumn.TransportMode:
					return ((string)travelModeTranslation[x.TravelMode]).CompareTo((string)travelModeTranslation[y.TravelMode]);

				case DisplayableTravelDateColumn.LowestFare:
					return x.LowestProbableFareFloat.CompareTo(y.LowestProbableFareFloat);
			}
			return 0;
		}
	}
}
