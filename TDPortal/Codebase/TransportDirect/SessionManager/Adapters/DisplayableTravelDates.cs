// *********************************************** 
// NAME         : DisplayableTravelDates.cs
// AUTHOR       : Richard Hopkins
// DATE CREATED : 25/01/2005
// DESCRIPTION  : Represents a table of data displayed by the FindFares...TravelDatesControl
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/Adapters/DisplayableTravelDates.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:50   mturner
//Initial revision.
//
//   Rev 1.10   Apr 29 2005 20:49:38   rhopkins
//Changed formatting of monetary columns
//Resolution for 2109: PT - Find a fare layout - Select date/transport option page
//Resolution for 2141: PT - Incorrect text on Date/Transport screen for Cheaper Fares
//Resolution for 2329: PT - Find Fare Date Selection column widths
//
//   Rev 1.9   Apr 27 2005 16:57:42   rscott
//Fix for IR2331
//
//   Rev 1.8   Apr 21 2005 13:29:54   rhopkins
//DisplayableTravelDates now exposes whether any of the TravelDates that it contains have error for Outward or Inward journeys.  This is used for determining what error messages to display.
//Also tidied up way that DisplayableTravelDates are created for child/adult.
//Resolution for 2287: FindAFare date selection is not displaying errors correctly
//
//   Rev 1.7   Apr 20 2005 12:25:16   rhopkins
//Added constructor with no parameters, for creating empty DisplayableTravelDates dataset.
//Resolution for 2100: PT - Coach - Find a Fare not correctly handling missing fare information
//
//   Rev 1.6   Apr 15 2005 19:51:16   rhopkins
//Improve resilience of getting TravelDate or TravelDate index
//
//   Rev 1.5   Mar 24 2005 16:27:02   COwczarek
//Modify GetTravelDateIndex method.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.4   Mar 14 2005 16:07:40   COwczarek
//Add GetTravelDateIndex
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.3   Mar 08 2005 14:25:40   rhopkins
//Correct Child fare handling and FXCop errors
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.2   Feb 25 2005 13:18:04   rhopkins
//Corrections to sorting and child fares
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.1   Feb 15 2005 20:32:30   rhopkins
//Work in progress
//
//   Rev 1.0   Feb 10 2005 11:29:50   rhopkins
//Initial revision.
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.CostSearch;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Represents a row of table data displayed by the FindFares...TravelDatesControl.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class DisplayableTravelDates : IListSource, IEnumerable, IEnumerator
	{
		#region Private Member Declaration

		private bool errorForOutward = false;
		private bool errorForInward = false;
		private bool showChild;
		private TicketType selectedTicketType;
		private DisplayableTravelDate[] travelDateTable;
		private DisplayableTravelDateColumn sortColumn;
		private bool sortOrderAscending;

		private float maximumMinimum = float.MinValue;
		private float maximumMaximum = float.MinValue;
		private float maximumLowest = float.MinValue;
		private int lengthMaximumMinimum;
		private int lengthMaximumMaximum;
		private int lengthMaximumLowest;

		//Used to keep track of current collection enumerator location
		private int pos = -1;

		#endregion Private Member Declaration

		#region Constructor

		/// <summary>
		/// Constructor for empty DisplayableTravelDates
		/// </summary>
		public DisplayableTravelDates()
		{
			travelDateTable = new DisplayableTravelDate[0];
		}

		/// <summary>
		/// Constructor for DisplayableTravelDates
		/// </summary>
		/// <param name="travelDates">Array of TravelDate items to be displayed</param>
		/// <param name="newShowChild">If true then Child fares are used, else Adult fares are used</param>
		public DisplayableTravelDates(TravelDate[] travelDates, bool newShowChild)
		{
			int travelDatesLength = travelDates.Length;

			travelDateTable = new DisplayableTravelDate[travelDatesLength];

			for (int i=0; i<travelDatesLength; i++)
			{
				travelDateTable[i] = new DisplayableTravelDate(travelDates[i], i);

				if (travelDates[i].ErrorForOutward)
				{
					errorForOutward = true;
				}
				if (travelDates[i].ErrorForInward)
				{
					errorForInward = true;
				}

				if (newShowChild)
				{
					if ( travelDates[i].MinChildFare > maximumMinimum )
					{
						maximumMinimum = travelDates[i].MinChildFare;
					}
					if ( travelDates[i].MaxChildFare > maximumMaximum )
					{
						maximumMaximum = travelDates[i].MaxChildFare;
					}
					if ( travelDates[i].LowestProbableChildFare > maximumLowest )
					{
						maximumLowest = travelDates[i].LowestProbableChildFare;
					}
				}
				else
				{
					if ( travelDates[i].MinAdultFare > maximumMinimum )
					{
						maximumMinimum = travelDates[i].MinAdultFare;
					}
					if ( travelDates[i].MaxAdultFare > maximumMaximum )
					{
						maximumMaximum = travelDates[i].MaxAdultFare;
					}
					if ( travelDates[i].LowestProbableAdultFare > maximumLowest )
					{
						maximumLowest = travelDates[i].LowestProbableAdultFare;
					}
				}
			}

			lengthMaximumMinimum = (maximumMinimum.ToString("0.00", CultureInfo.CurrentCulture)).Length;
			lengthMaximumMaximum = (maximumMaximum.ToString("0.00", CultureInfo.CurrentCulture)).Length;
			lengthMaximumLowest = (maximumLowest.ToString("0.00", CultureInfo.CurrentCulture)).Length;

			for (int i=0; i<travelDatesLength; i++)
			{
				travelDateTable[i].FormatData(newShowChild, lengthMaximumMinimum, lengthMaximumMaximum, lengthMaximumLowest);
			}
			showChild = newShowChild;

		}

		#endregion Constructor

		#region Public Methods

		/// <summary>
		/// Update the data to show child or adult fares.  If the requested fares are already shown then no change will be made.
		/// </summary>
		/// <param name="showChild">If true then Child fares are used, else Adult fares are used</param>
		public void ShowChild(bool newShowChild)
		{
			if (newShowChild != showChild)
			{
				int travelDatesLength = travelDateTable.Length;

				for (int i=0; i<travelDatesLength; i++)
				{
					travelDateTable[i].FormatData(newShowChild, lengthMaximumMinimum, lengthMaximumMaximum, lengthMaximumLowest);
				}
				showChild = newShowChild;
			}
		}

		/// <summary>
		/// Sorts the data by the specified column
		/// </summary>
		public void Sort(IComparer sortComparer)
		{
			Array.Sort(travelDateTable, sortComparer);
			int i = 1;
			i++;
		}

		/// <summary>
		/// Returns collection's array list
		/// IListSource interface implementation
		/// </summary>
		/// <returns></returns>
		public IList GetList()
		{
			return (IList)travelDateTable;
		}

		/// <summary>
		/// Returns the collection's foreach enumerator interface.
		/// IEnumerable interface implementation
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator()
		{
			pos = -1;
			return (IEnumerator)this;
		}

		/// <summary>
		/// Increments the collection's enumerator counter.
		/// IEnumerator interface implementation
		/// </summary>
		/// <returns></returns>
		public bool MoveNext()
		{
			if (pos < travelDateTable.Length - 1)
			{
				pos++;
				return true;
			}

			return false;
		}

		/// <summary>
		/// Resets the collection's enumerator counter.
		/// IEnumerator interface implementation
		/// </summary>
		public void Reset()
		{
			pos = -1;
		}

        /// <summary>
        /// For the supplied travelDate, returns the corresponding index of the 
        /// element in the list of DisplayableTravelDate objects held by this instance.
        /// If the supplied travelDate is for singles and the corresponding entry cannot
        /// be found in the list, a search is performed (on the supplied searchResult object) for
        /// a return travel date for the same date and mode. This is in case the travel date has
        /// been switched from return to singles by the user.
        /// </summary>
        /// <param name="travelDate">The travelDate to find in the list</param>
        /// <param name="searchResult">The search result containing all travel dates</param>
        /// <returns>index of the element in the list of DisplayableTravelDate objects held by this instance.</returns>
        public int GetTravelDateIndex(TravelDate travelDate, ICostSearchResult searchResult) 
        {
			TravelDate thisTravelDate;

			for (int i=0; i < travelDateTable.Length; i++) 
            {
				thisTravelDate = travelDateTable[i].TravelDate;

				if (thisTravelDate == travelDate) 
                {
                    return i;
                }

				// If could not find a matching singles travel date, is this a matching return travel date instead?
                if ( (thisTravelDate.TicketType == TicketType.Return) && (travelDate.TicketType == TicketType.Singles)
					&& (thisTravelDate.OutwardDate == travelDate.OutwardDate) && (thisTravelDate.ReturnDate == travelDate.ReturnDate)
					&& (thisTravelDate.TravelMode == travelDate.TravelMode) )
				{
					return i;
				}

				//Introduced for IR2331
				if ( (thisTravelDate.TicketType == TicketType.Singles) && (travelDate.TicketType == TicketType.Return)
					&& (thisTravelDate.OutwardDate == travelDate.OutwardDate) && (thisTravelDate.ReturnDate == travelDate.ReturnDate)
					&& (thisTravelDate.TravelMode == travelDate.TravelMode) )
				{
					return i;
				}

			}
            return -1;
        }

		#endregion Public Methods

		#region Public Properties

		/// <summary>
		/// Gets/sets the column that is used for sorting
		/// </summary>
		public DisplayableTravelDateColumn SortColumn
		{
			get { return sortColumn; }
			set { sortColumn = value; }
		}

		/// <summary>
		/// Gets/sets whether the data should be sorted into ascending (true) or descending (false) order, using the SortColumn
		/// </summary>
		public bool SortOrderAscending
		{
			get { return sortOrderAscending; }
			set { sortOrderAscending = value; }
		}

		/// <summary>
		/// Returns the current enumerated item in the collection.
		/// Implements the IEnumerator interface member explicitly
		/// IEnumerator interface implementation
		/// </summary>
		object IEnumerator.Current
		{
			get { return (object)travelDateTable[pos]; }
		}

		/// <summary>
		/// Returns the current enumerated item in the collection.
		/// Implements the strongly typed member.
		/// IEnumerator interface implementation
		/// </summary>
		public DisplayableTravelDate Current
		{
			get { return travelDateTable[pos]; }
		}

		/// <summary>
		/// Returns whether the collection is a collection of IList objects
		/// IListSource interface implementation
		/// </summary>
		public bool ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Returns a DisplayableTravelDate by it's index
		/// </summary>
		public DisplayableTravelDate this[int index]
		{
			get
			{
				if ( (index < 0) || (index >= travelDateTable.Length) )
				{
					return null;
				}
				else
				{
					return travelDateTable[index];
				}
			}
		}

		/// <summary>
		/// Length of the TravelDates dataset
		/// </summary>
		public int Length
		{
			get { return travelDateTable.Length; }
		}

		/// <summary>
		/// The TicketType that the User selected to view.
		/// Note: This may differ from the TicketType of the current SelectedTravelDate
		/// </summary>
		public TicketType SelectedTicketType
		{
			get {return selectedTicketType;}
			set {selectedTicketType = value;}
		}

		/// <summary>
		/// read/write for errorForOutward
		/// </summary>
		public bool ErrorForOutward
		{
			get { return errorForOutward; }
			set { errorForOutward = value; }
		}

		/// <summary>
		/// read/write for errorForOutward
		/// </summary>
		public bool ErrorForInward
		{
			get { return errorForInward; }
			set { errorForInward = value; }
		}

		#endregion Public Properties
	}
}
