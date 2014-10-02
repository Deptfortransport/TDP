// ************************************************************** 
// NAME			: InvalidExchangeOperatorList.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Strong typed collection for InvalidExchangeOperator objects
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/InvalidExchangeOperatorList.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:40   mturner
//Initial revision.
//
//   Rev 1.1   Jan 17 2006 17:44:56   RPhilpott
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
	/// InvalidExchangeOperator collection
	/// </summary>
	public class InvalidExchangeOperatorList : CollectionBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public InvalidExchangeOperatorList()
		{}

		/// <summary>
		/// Adds a InvalidExchangeOperator object to the end of the CollectionBase.
		/// </summary>
		/// <param name="invalidExchangeOperator"></param>
		/// <returns></returns>
		public int Add(InvalidExchangeOperator invalidExchangeOperator)
		{
			return List.Add(invalidExchangeOperator);
		}

		/// <summary>
		/// Determines whether the collection contains a specific InvalidExchangeOperator object
		/// </summary>
		/// <param name="invalidExchangeOperator"></param>
		/// <returns></returns>
		public bool Contains(InvalidExchangeOperator invalidExchangeOperator)
		{
			foreach(InvalidExchangeOperator child in List)
				if(child.CoachOperatorCode.Equals(invalidExchangeOperator.CoachOperatorCode))
					return true;
			return false;
		}

		/// <summary>
		/// Searches for the specified InvalidExchangeOperator object and returns the zero-based index of the first occurrence within the entire collection
		/// </summary>
		/// <param name="invalidExchangeOperator"></param>
		/// <returns></returns>
		public int IndexOf(InvalidExchangeOperator invalidExchangeOperator)
		{
			return List.IndexOf(invalidExchangeOperator);
		}

		/// <summary>
		/// Read-only collection Indexer
		/// </summary>
		public InvalidExchangeOperator this[int index]
		{
			get { return (InvalidExchangeOperator)List[index]; }
		}

		/// <summary>
		/// Provides type specific validation when using the collection
		/// </summary>
		/// <param name="item"></param>
		protected override void OnValidate(object item)
		{
			if (!(item is InvalidExchangeOperator))
			{
				throw new ArgumentException(
				"This collection only accepts the InvalidExchangeOperator type or types that derive from InvalidExchangeOperator");
			}
		}


		/// <summary>
		/// Checks if operator if an InvalidOperator for specific Coach Operator NaPTAN
		/// </summary>
		/// <param name="OperatorCode"></param>
		/// <returns></returns>
		public bool IsInvalidOperator(string coachOperatorCode)
		{
			InvalidExchangeOperator invalidExchangeOperator = new InvalidExchangeOperator();
			invalidExchangeOperator.CoachOperatorCode = coachOperatorCode;

			if(this.Contains(invalidExchangeOperator))
			{
				return true;
			}

			return false;
		
		}

		/// <summary>
		/// Method to return a data-populated InvalidExchangeOperatorList object
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="NaPTAN"></param>
		/// <returns>InvalidExchangeOperatorList</returns>
		public static InvalidExchangeOperatorList Fetch(DataSet ds,string NaPTAN)
		{
			InvalidExchangeOperatorList invalidExchangeOperatorList = new InvalidExchangeOperatorList();

			foreach (DataRow dr in ds.Tables[3].Rows)
			{
				//If Exchange naptan is for the location naptan
				if ( dr[0].ToString().Equals(NaPTAN))
				{
					//Populate the InvalidExchangeOperator and add it to the collection
					InvalidExchangeOperator invalidExchangeOperator = new InvalidExchangeOperator();
					invalidExchangeOperator.CoachOperatorCode = dr[1].ToString();
					invalidExchangeOperatorList.Add(invalidExchangeOperator);
				}
			}

			return invalidExchangeOperatorList;
		}

	}
}
