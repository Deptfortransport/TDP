// *********************************************** 
// NAME         : DisplayableTravelDateSortOption.cs
// AUTHOR       : Richard Hopkins
// DATE CREATED : 14/02/2005
// DESCRIPTION  : Represents the columns that are used to sort the table
//				of data displayed by the FindFares...TravelDatesControl
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/Adapters/DisplayableTravelDateSortOption.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:50   mturner
//Initial revision.
//
//   Rev 1.0   Feb 15 2005 20:37:54   rhopkins
//Initial revision.
//

using System;
using System.Collections;
using System.ComponentModel;

namespace TransportDirect.UserPortal.SessionManager
{

	/// <summary>
	/// Specifies a sortable data column used by DisplayableTravelDateComparer to sort DisplayableTravelDates
	/// </summary>
	public enum DisplayableTravelDateColumn
	{
		None,
		OutwardDate,
		ReturnDate,
		TransportMode,
		LowestFare
	}

	/// <summary>
	/// Class which represents a sort option for a DisplayableTravelDate.
	/// It contains a column to sort by and a bool indicating ascending order 
	/// (true) or descending (false)
	/// </summary>
	[CLSCompliant(false), Serializable]
	public class DisplayableTravelDateSortOption
	{
		private readonly DisplayableTravelDateColumn column;
		private bool ascending;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="column"></param>
		/// <param name="ascending"></param>
		public DisplayableTravelDateSortOption(DisplayableTravelDateColumn column, bool ascending)
		{
			this.column = column;
			this.ascending = ascending;
		}

		/// <summary>
		/// Constructor. The column specified will be sorted in ascending order
		/// </summary>
		/// <param name="column"></param>
		public DisplayableTravelDateSortOption(DisplayableTravelDateColumn column) : this(column, true)
		{ }

		/// <summary>
		/// Returns the column to sort
		/// </summary>
		public DisplayableTravelDateColumn Column
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
}
