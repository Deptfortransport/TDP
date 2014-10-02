// ************************************************************** 
// NAME			: Route.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Represents a coach route
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/Route.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:44   mturner
//Initial revision.
//
//   Rev 1.0   Oct 26 2005 09:55:48   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 16:45:22   RWilby
//Added comments to properties
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 10 2005 17:04:32   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// Represents a specific coach route, eg London to Leeds
	/// </summary>
	public class Route
	{
		private string originNaPTAN;
		private string destinationNaPTAN;
		private LegList legList = new LegList();

		/// <summary>
		/// Constructor
		/// </summary>
		public Route()
		{
		}

		/// <summary>
		///Read/Write Property. Origin NaPTAN 
		/// </summary>
		public string OriginNaPTAN
		{
			get{return originNaPTAN;}
			set{originNaPTAN = value;}
		}
		
		/// <summary>
		/// Read/Write Property. Destination NaPTAN
		/// </summary>
		public string DestinationNaPTAN
		{
			get{return destinationNaPTAN;}
			set{destinationNaPTAN = value;}
		}
		
		/// <summary>
		/// Read/Write Property. Collection of route Legs.
		/// Each Route will contain One-to-Two Leg objects stored in the LegList collection.
		/// </summary>
		public LegList LegList
		{
			get{return legList;}
			set{legList = value;}
		}

	}

}
