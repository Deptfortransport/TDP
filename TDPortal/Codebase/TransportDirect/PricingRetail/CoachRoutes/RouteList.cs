// ************************************************************** 
// NAME			: RouteList.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Strong typed collection for Route objects
//				  - The foundation of the CoachRoutesQuotaFares object graph
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/RouteList.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:44   mturner
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

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// Route collection
	/// </summary>
	public class RouteList : CollectionBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public RouteList()
		{}
		
		/// <summary>
		/// Adds a Route object to the end of the CollectionBase.
		/// </summary>
		/// <param name="route"></param>
		/// <returns></returns>
		public int Add(Route route)
		{
			return List.Add(route);
		}
		
		/// <summary>
		/// Determines whether the collection contains a specific route object
		/// </summary>
		/// <param name="route"></param>
		/// <returns></returns>
		public bool Contains(Route route)
		{
			return List.Contains(route);
		}

		/// <summary>
		/// Searches for the specified Route object and returns the zero-based index of the first occurrence within the entire collection
		/// </summary>
		/// <param name="route">Route</param>
		/// <returns></returns>
		public int IndexOf(Route route)
		{
			return List.IndexOf(route);
		}

		/// <summary>
		/// Read-only collection indexer
		/// </summary>
		public Route this[int index]
		{
			get { return (Route)List[index]; }
		}
		
		/// <summary>
		/// Provides type specific validation when using the collection
		/// </summary>
		/// <param name="item"></param>
		protected override void OnValidate(object item)
		{
			if (!(item is Route))
			{
				throw new ArgumentException(
				"This collection only accepts the Route type or types that derive from Route");
			}
		}

	}
}
