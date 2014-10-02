// *********************************************** 
// NAME			: RetailerSortDecorator.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 06/11/03
// DESCRIPTION	: Implements a wrapper for the immutable Retailer class
//                that has a sort key which is assigned a random value.
//                This allows a collection of these objects to be sorted 
//                to obtain randomized ordered collections.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/RetailerSortDecorator.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:36:56   mturner
//Initial revision.
//
//   Rev 1.6   May 16 2005 15:43:00   jgeorge
//Ignore whether or not retailer is online or offline, as they are now sorted independently anyway.
//Resolution for 2519: Order of retailers list should be random
//
//   Rev 1.5   Mar 02 2005 16:14:44   jgeorge
//Added small icon url property to retailer
//
//   Rev 1.4   Dec 23 2004 11:57:20   jgeorge
//Modified to add new property to Retailer
//
//   Rev 1.3   Nov 18 2003 16:10:10   COwczarek
//SCR#247 :Add $Log: for PVCS history

using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
    /// Implements a wrapper for the immutable Retailer class
    ///  that has a sort key which is assigned a random value.
    ///  This allows a collection of these objects to be sorted 
    ///  to obtain randomized ordered collections.
	/// </summary>
	[Serializable]
	public class RetailerSortDecorator : Retailer
	{

        // The random key value of this instance assigned at object creation
        private readonly int sortkey;
        // Object used for generation of random numbers for all instances of this class
        private static Random randomKey = new Random();
        
        /// <summary>
        /// Creates an instance of RetailerSortDecorator and assigns a random sort key value.
        /// Retailers which do not support Internet handoff are assigned higher random values
        /// than those which do. This ensures that they occur after Internet retailers in a
        /// sorted list.
        /// </summary>
        /// <param name="retailer">The Retailer instance being wrapped</param>
        public RetailerSortDecorator(Retailer retailer) : 
            base(retailer.Id, retailer.Name,
                retailer.WebsiteUrl, retailer.HandoffUrl, retailer.DisplayUrl, 
                retailer.PhoneNumber, retailer.PhoneNumberDisplay,
                retailer.IconUrl, retailer.SmallIconUrl, retailer.AllowsMultipleTicketHandoff,
                retailer.ResourceKey)
        {
			// Assign a sortkey. This ignores whether or not the retailer is capable
			// of dealing with handoffs.
			sortkey = randomKey.Next(0,100);
		}
		
		/// <summary>
		/// Returns the key value for this instance.
		/// </summary>
		public int SortKey {
		    get {return sortkey;}
		}
		
	}
}