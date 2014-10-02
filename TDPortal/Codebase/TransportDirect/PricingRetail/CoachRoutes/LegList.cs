// ************************************************************** 
// NAME			: LegList.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Strong typed collection for Leg objects
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/LegList.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:42   mturner
//Initial revision.
//
//   Rev 1.1   Jan 17 2006 17:44:56   RPhilpott
//Code review updates.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.0   Oct 26 2005 09:55:46   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price

using System;
using System.Collections;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// Leg collection
	/// </summary>
	public class LegList : CollectionBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public LegList()
		{}

		/// <summary>
		/// Adds a Leg object to the end of the CollectionBase.
		/// </summary>
		/// <param name="leg"></param>
		/// <returns></returns>
		public int Add(Leg leg)
		{
			return List.Add(leg);
		}

		/// <summary>
		/// Determines whether the collection contains a specific Leg object
		/// </summary>
		/// <param name="leg"></param>
		/// <returns></returns>
		public bool Contains(Leg leg)
		{
			return List.Contains(leg);
		}

		/// <summary>
		/// Searches for the specified Leg object and returns the zero-based index of the first occurrence within the entire collection
		/// </summary>
		/// <param name="leg"></param>
		/// <returns></returns>
		public int IndexOf(Leg leg)
		{
			return List.IndexOf(leg);
		}

		/// <summary>
		/// Read-only collection indexer
		/// </summary>
		public Leg this[int index]
		{
			get { return (Leg)List[index]; }
		}

		/// <summary>
		/// Provides type specific validation when using the collection
		/// </summary>
		/// <param name="item"></param>
		protected override void OnValidate(object item)
		{
			if (!(item is Leg))
			{
				throw new ArgumentException(
				"This collection only accepts the Leg type or types that derive from Leg");
			}
		}

	}
}
