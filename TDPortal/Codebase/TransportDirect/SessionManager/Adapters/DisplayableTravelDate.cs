// *********************************************** 
// NAME         : DisplayableTravelDate.cs
// AUTHOR       : Richard Hopkins
// DATE CREATED : 07/01/2005
// DESCRIPTION  : Represents a row of table data displayed by the FindFares...TravelDatesControl
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/Adapters/DisplayableTravelDate.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:50   mturner
//Initial revision.
//
//   Rev 1.6   Apr 29 2005 20:46:30   rhopkins
//Changed formatting of monetary columns
//Resolution for 2109: PT - Find a fare layout - Select date/transport option page
//Resolution for 2141: PT - Incorrect text on Date/Transport screen for Cheaper Fares
//Resolution for 2329: PT - Find Fare Date Selection column widths
//
//   Rev 1.5   Apr 21 2005 13:27:48   rhopkins
//Tidied up way that DisplayableTravelDates are created for child/adult.
//Resolution for 2287: FindAFare date selection is not displaying errors correctly
//
//   Rev 1.4   Mar 14 2005 16:08:32   COwczarek
//Add TravelDate and PartialResults properties
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.3   Mar 09 2005 16:39:42   rhopkins
//Fixed some FXCop errors
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.2   Feb 25 2005 13:16:40   rhopkins
//Improve resilience to missing date data
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.1   Feb 15 2005 20:32:30   rhopkins
//Work in progress
//
//   Rev 1.0   Feb 10 2005 11:29:50   rhopkins
//Initial revision.
//

using System;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Represents a row of table data displayed by the FindFares...TravelDatesControl.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class DisplayableTravelDate
	{
		#region Private Member Declaration

		private const string POUND_STRING = "<span class=\"pound\">&pound;</span>";
		private int poundStringLength = POUND_STRING.Length;

		private int index;
		private TravelDate travelDate;
		private float lowestProbableFareFloat;

		private string outwardDayMonth = String.Empty;
		private string returnDayMonth = String.Empty;
		private string travelMode = String.Empty;
		private string fareRange = String.Empty;
		private string lowestProbableFare = String.Empty;

		#endregion Private Member Declaration

		#region Constructor

		public DisplayableTravelDate(TravelDate travelDate, bool showChild, int index)
		{
			this.travelDate = travelDate;
			this.index = index;
			FormatData(showChild, 0, 0, 0);
		}

		public DisplayableTravelDate(TravelDate travelDate, int index)
		{
			this.travelDate = travelDate;
			this.index = index;
		}

		#endregion Constructor

		#region Public Methods

		public void FormatData(bool showChild, int lengthMaximumMinimum, int lengthMaximumMaximum, int lengthMaximumLowest)
		{
			if (travelDate != null)
			{
				if (travelDate.OutwardDate != null)
				{
					outwardDayMonth = travelDate.OutwardDate.ToString("dd") + "/" + travelDate.OutwardDate.ToString("MM");
				}
				else
				{
					outwardDayMonth = String.Empty;
				}

				if (travelDate.ReturnDate != null)
				{
					returnDayMonth = travelDate.ReturnDate.ToString("dd") + "/" + travelDate.ReturnDate.ToString("MM");
				}
				else
				{
					returnDayMonth = String.Empty;
				}

				travelMode = travelDate.TravelMode.ToString();

				// Set the Min/Max fares according to whether we are viewing child or adult fares
				if (showChild)
				{
					lowestProbableFareFloat = travelDate.LowestProbableChildFare;
					lowestProbableFare = FormatPrice(lowestProbableFareFloat, lengthMaximumLowest) + ((travelDate.UnlikelyToBeAvailable) ? "&nbsp;*" : "&nbsp;&nbsp;");
					fareRange = FormatPrice(travelDate.MinChildFare, lengthMaximumMinimum) + "&nbsp;-&nbsp;" + FormatPrice(travelDate.MaxChildFare, lengthMaximumMaximum);
				}
				else
				{
					lowestProbableFareFloat = travelDate.LowestProbableAdultFare;
					lowestProbableFare = FormatPrice(lowestProbableFareFloat, lengthMaximumLowest) + ((travelDate.UnlikelyToBeAvailable) ? "&nbsp;*" : "&nbsp;&nbsp;");
					fareRange = FormatPrice(travelDate.MinAdultFare, lengthMaximumMinimum) + "&nbsp;-&nbsp;" + FormatPrice(travelDate.MaxAdultFare, lengthMaximumMaximum);
				}
			}
		}

		#endregion Public Methods

		#region Public Properties

		/// <summary>
		/// Gets the index of the DisplayableTravelDate
		/// </summary>
		public int Index
		{
			get { return index; }
		}

		/// <summary>
		/// Gets the Outward Date in unformated form (used for sorting)
		/// </summary>
		public TDDateTime OutwardDate
		{
			get { return travelDate.OutwardDate; }
		}

		/// <summary>
		/// Gets the Day Name of the Outward TravelDate
		/// </summary>
		public string OutwardDayName
		{
			get
			{
				if (travelDate.OutwardDate != null)
				{
					return travelDate.OutwardDate.ToString("ddd");
				}
				else
				{
					return String.Empty;
				}
			}
		}

		/// <summary>
		/// Gets the Date/Month of the Outward TravelDate
		/// </summary>
		public string OutwardDayMonth
		{
			get { return outwardDayMonth; }
		}

		/// <summary>
		/// Gets the Return Date in unformated form (used for sorting)
		/// </summary>
		public TDDateTime ReturnDate
		{
			get { return travelDate.ReturnDate; }
		}

		/// <summary>
		/// Gets the Day Name of the Return TravelDate
		/// </summary>
		public string ReturnDayName
		{
			get
			{
				if (travelDate.ReturnDate != null)
				{
					return travelDate.ReturnDate.ToString("ddd");
				}
				else
				{
					return String.Empty;
				}
			}
		}

		/// <summary>
		/// Gets the Date/Month of the Return TravelDate
		/// </summary>
		public string ReturnDayMonth
		{
			get { return returnDayMonth; }
		}

		/// <summary>
		/// Gets the partial resource ID for the travel mode for the TravelDate
		/// </summary>
		public string TravelMode
		{
			get { return travelMode; }
		}

		/// <summary>
		/// Gets the lowest probable fare in unformated form (used for sorting)
		/// </summary>
		public float LowestProbableFareFloat
		{
			get { return lowestProbableFareFloat; }
		}

		/// <summary>
		/// Gets the lowest probable fare for the TravelDate
		/// </summary>
		public string LowestProbableFare
		{
			get { return lowestProbableFare; }
		}

		/// <summary>
		/// Gets the min/max range of fares
		/// </summary>
		public string FareRange
		{
			get {return fareRange;}
		}

        /// <summary>
        /// Gets the TravelDate object from which this displayable version is derived
        /// </summary>
        public TravelDate TravelDate 
        {
            get {return travelDate;}
        }

        /// <summary>
        /// Read only property that returns true if the associated TravelDate object indicates
        /// not all results could be obtained during the cost search.
        /// </summary>
        public bool PartialResults 
        {
            get 
            {
                return travelDate.ErrorForInward || travelDate.ErrorForOutward;
            }
        }

		#endregion Public Properties

		#region Private Methods

		private string FormatPrice(float price, int length)
		{
			string formattedPrice = POUND_STRING + price.ToString("0.00", CultureInfo.CurrentCulture);
			formattedPrice = formattedPrice.PadLeft(length + poundStringLength, '|');
			return formattedPrice.Replace("|", "&nbsp;");
		}

		#endregion Private Methods
	}
}
