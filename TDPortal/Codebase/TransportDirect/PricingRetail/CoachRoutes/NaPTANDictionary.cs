// ************************************************************** 
// NAME			: NaPTANDictionary.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Strong typed dictionary collection for NaPTAN objects
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/NaPTANDictionary.cs-arc  $
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
//

using System;
using System.Collections;
using System.Data;
using System.Globalization;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// NaPTANDictionary
	/// </summary>
	public class NaPTANDictionary :DictionaryBase
	{
		/// <summary>
		/// OperatorList collection
		/// </summary>
		public NaPTANDictionary()
		{
		}

		/// <summary>
		/// Read-only collection indexer
		/// </summary>
		public NaPTAN this[string naptanID]
		{
			get {return (NaPTAN) this.Dictionary[naptanID]; }

			set { this.Dictionary[naptanID] = value; } 
		}

		/// <summary>
		///  Adds a NaPTAN object to the end of the CollectionBase.
		/// </summary>
		/// <param name="naptanID"></param>
		/// <param name="naptan"></param>
		public void Add(string naptanID, NaPTAN naptan ) 
		{ 
			//Do not add the same naptan twice.
			//-It is possible that the database data contains duplicate naptans
			// because the data is based on city-to-city data.
			if (!this.Dictionary.Contains(naptanID))
				this.Dictionary.Add(naptanID, naptan); 
		} 

		/// <summary>
		/// Determines whether the collection contains a specific route object
		/// </summary>
		/// <param name="naptanID"></param>
		/// <returns></returns>
		public bool Contains(string naptanID)
		{
			return this.Dictionary.Contains(naptanID);
		}

		/// <summary>
		/// Provides type specific validation when using the collection
		/// </summary>
		/// <param name="key"></param>
		/// <param name="item"></param>
		protected override void OnValidate(object key,object item)  
		{
			if (!(item is NaPTAN))
			{
				throw new ArgumentException
					("This collection only accepts the NaPTAN type or types that derive from NaPTAN");
			}
		}
	
		/// <summary>
		/// Method to check if a NaPTAN is served by the parent CoachOperator
		/// </summary>
		/// <param name="NaPTANID">NaPTAN location</param>
		/// <returns>Boolean value to indicate if the NaPTAN is served by the parent CoachOperator</returns>
		public bool IsServed(string NaPTANId)
		{
			if	(this.Contains(NaPTANId))
			{
				return this[NaPTANId].IsServed;
			}

			return false;
		}


		/// <summary>
		/// Method to return a data-populated NaPTANDictionary obejct
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>NaPTANDictionary</returns>
		public static NaPTANDictionary Fetch(DataSet ds)
		{
			NaPTANDictionary naptanictionary = new NaPTANDictionary();
			
			//Populate the NaPTANDictionary from the DataSet
			foreach (DataRow dr in ds.Tables[1].Rows)
			{
				//Populate NaPTAN
				NaPTAN naptan = new NaPTAN();
				naptan.NaPTANID = dr[0].ToString();
				naptan.IsServed = string.Compare(dr[1].ToString(),"Y",true,CultureInfo.InvariantCulture) ==0? true : false;	
				
				//Populate ExchangeNaPTAN child collection for NaPTAN
				naptan.ExchangeNaPTANList = ExchangeNaPTANList.Fetch(ds,naptan.NaPTANID);	

				//Populate InvalidExchangeOperator child collection for NaPTAN
				naptan.InvalidExchangeOperatorList = InvalidExchangeOperatorList.Fetch(ds,naptan.NaPTANID);
				
				//Add NaPTAN to collection
				naptanictionary.Add(naptan.NaPTANID,naptan);
			}

			return naptanictionary;
		}
	}
}
