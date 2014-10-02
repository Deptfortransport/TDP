// ************************************************************** 
// NAME			: QuotaFareList.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Strong typed collection for OuotaFares objects
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/QuotaFareList.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:42   mturner
//Initial revision.
//
//   Rev 1.1   Jan 17 2006 17:44:56   RPhilpott
//Code review updates.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.0   Oct 26 2005 09:55:48   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//

using System;
using System.Collections;
using System.Data;
using System.Globalization;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// OuotaFares collection
	/// </summary>
	public class QuotaFareList : CollectionBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public QuotaFareList()
		{}

		/// <summary>
		/// Adds a QuotaFare object to the end of the CollectionBase.
		/// </summary>
		/// <param name="quotaFare"></param>
		/// <returns></returns>
		public int Add(QuotaFare quotaFare)
		{
			return List.Add(quotaFare);
		}

		/// <summary>
		/// Determines whether the collection contains a specific QuotaFare object
		/// </summary>
		/// <param name="quotaFare"></param>
		/// <returns></returns>
		public bool Contains(QuotaFare quotaFare)
		{
			return List.Contains(quotaFare);
		}

		/// <summary>
		/// Searches for the specified QuotaFare object and returns the zero-based index of the first occurrence within the entire collection
		/// </summary>
		/// <param name="quotaFare"></param>
		/// <returns></returns>
		public int IndexOf(QuotaFare quotaFare)
		{
			return List.IndexOf(quotaFare);
		}

		/// <summary>
		/// Read-only Collection indexer 
		/// </summary>
		public QuotaFare this[int index]
		{
			get { return (QuotaFare)List[index]; }
		}
		
		/// <summary>
		/// Returns quota fares for route leg
		/// </summary>
		/// <param name="originNaPTAN">Origin NaPTAN location</param>
		/// <param name="destinationNaPTAN">Destination NaPTAN location</param>
		/// <returns>QuotaFareList collection</returns>
		public QuotaFareList GetQuotaFares(string originNaPTAN, string destinationNaPTAN)
		{
			QuotaFareList quotaFareList = new QuotaFareList();
			
			//Check if quota fare(s) exist for specific route leg
			foreach (QuotaFare quotaFare in this)
			{
				if( quotaFare.OriginNaPTAN.Equals( originNaPTAN) &&
					quotaFare.DestinationNaPTAN.Equals(destinationNaPTAN))
				{
					quotaFareList.Add(quotaFare);	
				}
			}

			return quotaFareList;
		}

		/// <summary>
		/// Provides type specific validation when using the collection
		/// </summary>
		/// <param name="item"></param>
		protected override void OnValidate(object item)
		{
			if (!(item is QuotaFare))
			{
				throw new ArgumentException(
				"This collection only accepts the QuotaFare type or types that derive from QuotaFare");
			}
		}

		/// <summary>
		/// Method to return a data-populated QuotaFareList object
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public static QuotaFareList Fetch(DataSet ds)
		{
			QuotaFareList quotaFareList = new QuotaFareList();
			
			foreach (DataRow dr in ds.Tables[4].Rows)
			{
				//Populate the QuotaFare and add it to the collection
				QuotaFare quotaFare = new QuotaFare();
				quotaFare.OriginNaPTAN = dr[0].ToString();
				quotaFare.DestinationNaPTAN = dr[1].ToString();
				quotaFare.TicketType = dr[2].ToString();
				quotaFare.Fare = Convert.ToInt32(dr[3].ToString(),CultureInfo.CurrentCulture);
				quotaFareList.Add(quotaFare);
			}


			return quotaFareList;
		}

	}
}
