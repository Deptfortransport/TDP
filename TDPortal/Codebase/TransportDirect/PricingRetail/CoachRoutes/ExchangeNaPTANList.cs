// ************************************************************** 
// NAME			: ExchangeNaPTANList.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Strong typed collection for ExchnageNaPTAN objects
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/ExchangeNaPTANList.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:40   mturner
//Initial revision.
//
//   Rev 1.1   Jan 17 2006 17:44:54   RPhilpott
//Code review updates.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.0   Oct 26 2005 09:55:44   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//

using System;
using System.Collections;
using System.Data;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// ExchangeNaPTAN collection
	/// </summary>
	public class ExchangeNaPTANList : CollectionBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ExchangeNaPTANList()
		{}

		/// <summary>
		///  Adds a ExchangeNaPTAN object to the end of the CollectionBase.
		/// </summary>
		/// <param name="exchangeNaPTAN"></param>
		/// <returns></returns>
		public int Add(ExchangeNaPTAN exchangeNaPTAN)
		{
			return List.Add(exchangeNaPTAN);
		}
	
		/// <summary>
		/// Determines whether the collection contains a specific ExchangeNaPTAN object
		/// </summary>
		/// <param name="exchangeNaPTAN"></param>
		/// <returns></returns>
		public bool Contains(ExchangeNaPTAN exchangeNaPTAN)
		{
			return List.Contains(exchangeNaPTAN);
		}
	
		/// <summary>
		/// Searches for the specified ExchangeNaPTAN object and returns the zero-based index of the first occurrence within the entire collection
		/// </summary>
		/// <param name="exchangeNaPTAN"></param>
		/// <returns></returns>
		public int IndexOf(ExchangeNaPTAN exchangeNaPTAN)
		{
			return List.IndexOf(exchangeNaPTAN);
		}
		
		/// <summary>
		/// Read-only collection indexer.
		/// </summary>
		public ExchangeNaPTAN this[int index]
		{
			get { return (ExchangeNaPTAN)List[index]; }
		}

		/// <summary>
		/// Provides type specific validation when using the collection
		/// </summary>
		/// <param name="item"></param>
		protected override void OnValidate(object item)
		{
			if (!(item is ExchangeNaPTAN))
			{
				throw new ArgumentException(
				"This collection only accepts the ExchangeNaPTAN type or types that derive from ExchangeNaPTAN");
			}
		}

		/// <summary>
		/// Method to return a data-populated ExchangeNaPTANList obejct
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="NaPTAN"></param>
		/// <returns>ExchangeNaPTANList</returns>
		public static ExchangeNaPTANList Fetch(DataSet ds,string NaPTAN)
		{
			ExchangeNaPTANList exchangeNaPTANList = new ExchangeNaPTANList();

			foreach (DataRow dr in ds.Tables[2].Rows)
			{
				//If Exchange naptan is for the location naptan
				if ( dr[0].ToString().Equals(NaPTAN))
				{
					//Populate a exchangeNaPTAN and add it to the collection
					ExchangeNaPTAN exchangeNaPTAN = new ExchangeNaPTAN();
					exchangeNaPTAN.NaPTANID = dr[1].ToString();
					exchangeNaPTANList.Add(exchangeNaPTAN);
				}
			}
			return exchangeNaPTANList;
		}

	}
}
