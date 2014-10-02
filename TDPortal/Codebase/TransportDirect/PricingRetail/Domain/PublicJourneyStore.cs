//********************************************************************************
//NAME         : PublicJourneyStore.cs
//AUTHOR       : James Broome
//DATE CREATED : 23/04/2005
//DESCRIPTION  : Implementation of PublicJourneyStore class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/PublicJourneyStore.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:54   mturner
//Initial revision.
//
//   Rev 1.1   Apr 25 2005 11:56:40   jbroome
//Added GetOutwardJourneys and GetReturnJourneys methods.
//
//   Rev 1.0   Apr 25 2005 10:08:44   jbroome
//Initial revision.

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Class holds collections of PublicJourney objects, grouped by outward 
	/// and return dates. Used to hold the PublicJourneys from  a CJP result 
	/// in a logical structure until they can be saved to the session.
	/// </summary>
	public class PublicJourneyStore
	{
	
		# region Private members

		private ArrayList outwardDates = new ArrayList();
		private ArrayList returnDates = new ArrayList();
		private ArrayList outwardJourneyArrays = new ArrayList();
		private ArrayList returnJourneyArrays = new ArrayList();

		# endregion
		
		#region Constructor

		/// <summary>
		/// Constructor accepts two ArrayLists of PublicJourneys.
		/// These are processed and the PublicJourneys
		/// are grouped into the relevant collections.
		/// </summary>
		public PublicJourneyStore(ArrayList outwardJourneys, ArrayList returnJourneys)
		{
			// Create hashtables to hold journeys:
			// Key = JourneyDate
			// Value = ArrayList of PublicJourneys
			Hashtable outJourneyGroups = new Hashtable();
			Hashtable retJourneyGroups = new Hashtable();

			// Process outward journeys
			foreach (PublicJourney journey in outwardJourneys)
			{
				if (outJourneyGroups.ContainsKey(journey.JourneyDate))
				{
					// This date already exists in hashtable, so 
					// add journey to the existing ArrayList
					ArrayList al = (ArrayList)outJourneyGroups[journey.JourneyDate];
					al.Add(journey);
					outJourneyGroups[journey.JourneyDate] = al;
				}
				else
				{
					// No journeys exist for this date yet
					ArrayList al = new ArrayList();
					al.Add(journey);
					outJourneyGroups.Add(journey.JourneyDate, al);
				}
			}

			// Process return journeys
			foreach (PublicJourney journey in returnJourneys)
			{
				if (retJourneyGroups.ContainsKey(journey.JourneyDate))
				{
					// This date already exists in hashtable, so 
					// add journey to the existing ArrayList
					ArrayList al = (ArrayList)retJourneyGroups[journey.JourneyDate];
					al.Add(journey);
					retJourneyGroups[journey.JourneyDate] = al;
				}
				else
				{
					// No journeys exist for this date yet
					ArrayList al = new ArrayList();
					al.Add(journey);
					retJourneyGroups.Add(journey.JourneyDate, al);
				}
			}

			// Now process outward hashtable:
			foreach (TDDateTime dateKey in outJourneyGroups.Keys)
			{
				ArrayList al = (ArrayList)outJourneyGroups[dateKey];
				PublicJourney[] journeyArray = (PublicJourney[])al.ToArray(typeof(PublicJourney));
				outwardJourneyArrays.Add(journeyArray);
				outwardDates.Add(dateKey);
			}

			// Now process return hashtable:
			foreach (TDDateTime dateKey in retJourneyGroups.Keys)
			{
				ArrayList al = (ArrayList)retJourneyGroups[dateKey];
				PublicJourney[] journeyArray = (PublicJourney[])al.ToArray(typeof(PublicJourney));
				returnJourneyArrays.Add(journeyArray);
				returnDates.Add(dateKey);
			}

		}

		#endregion
        		
		# region Public properties and methods

		/// <summary>
		/// Read only TDDateTime[] property
		/// Collection of distinct journey dates from journeys 
		/// </summary>
		public TDDateTime[] OutwardDates
		{
			get {return (TDDateTime[])outwardDates.ToArray(typeof(TDDateTime));}
		}

		/// <summary>
		/// Read only TDDateTime[] property
		/// Collection of distinct journey dates from journeys 
		/// </summary>
		public TDDateTime[] ReturnDates
		{
			get {return (TDDateTime[])returnDates.ToArray(typeof(TDDateTime));}
		}

		/// <summary>
		/// Method returns a two dimensional 'jagged' array 
		/// of PublicJourney objects of the Outward Journeys 
		/// grouped by date
		/// </summary>
		/// <returns>Two dimensional array of PublicJourneys</returns>
		public PublicJourney[][] GetOutwardJourneys()
		{
			PublicJourney[][] journeys = new PublicJourney[outwardDates.Count][];
			for (int i=0; i<journeys.Length; i++)
			{
				journeys[i] = GetOutwardJourneysForDate(i);			
			}
			return journeys;
		}


		/// <summary>
		/// Method returns a two dimensional 'jagged' array 
		/// of PublicJourney objects of the Return Journeys 
		/// grouped by date
		/// </summary>
		/// <returns>Two dimensional array of PublicJourneys</returns>
		public PublicJourney[][] GetReturnJourneys()
		{
			PublicJourney[][] journeys = new PublicJourney[returnDates.Count][];
			for (int i=0; i<journeys.Length; i++)
			{
				journeys[i] = GetReturnJourneysForDate(i);			
			}
			return journeys;
		}
		
		/// <summary>
		/// Method returns the index of a TDDateTime in
		/// the collection of distinct dates from journeys
		/// </summary>
		/// <param name="date">TDDateTime</param>
		/// <param name="outward">bool outward</param>
		/// <returns>int index of date</returns>
		public int GetDateIndex(TDDateTime date, bool outward)
		{
			if (outward)
			{
				return outwardDates.IndexOf(date);	
			}
			else
			{
				return returnDates.IndexOf(date);
			}
		}

		/// <summary>
		/// Method returns an array of PublicJourneys for 
		/// the date specified by the index
		/// </summary>
		/// <param name="dateIndex">int index of date</param>
		/// <returns>Array of PublicJourneys</returns>
		public PublicJourney[] GetOutwardJourneysForDate(int dateIndex)
		{
			return (PublicJourney[])outwardJourneyArrays[dateIndex];
		}

		/// <summary>
		/// Method returns an array of PublicJourneys for 
		/// the date specified by the index
		/// </summary>
		/// <param name="dateIndex">int index of date</param>
		/// <returns>Array of PublicJourneys</returns>
		public PublicJourney[] GetReturnJourneysForDate(int dateIndex)
		{
			return (PublicJourney[])returnJourneyArrays[dateIndex];
		}

		/// <summary>
		/// Method returns the index of a specified journey within the
		/// collection for a specified outward or return date.
		/// </summary>
		/// <param name="dateIndex">Index of date</param>
		/// <param name="outward">bool outward</param>
		/// <param name="journey">Journey to find index of</param>
		/// <returns>int index of journey in collection</returns>
		public int GetIndexForJourney(int dateIndex, bool outward, PublicJourney journey)
		{
			int result = -1;

			if (outward)
			{
				PublicJourney[] journeys = (PublicJourney[])outwardJourneyArrays[dateIndex];
				for (int i=0; i<journeys.Length; i++)
				{
					if (journey == journeys[i])
					{
						result = i;
						break;
					}
				}
			}
			else
			{
				PublicJourney[] journeys = (PublicJourney[])returnJourneyArrays[dateIndex];
				for (int i=0; i<journeys.Length; i++)
				{
					if (journey == journeys[i])
					{
						result = i;
						break;
					}
				}
			}

			return result;

		}

		# endregion

	}
}
