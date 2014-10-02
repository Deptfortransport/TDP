// *********************************************** 
// NAME         : CostSearchParams.cs
// AUTHOR       : Tim Mollart
// DATE CREATED : 22/12/2004
// DESCRIPTION  : Class for cost based search parameters
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/CostSearchParams.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:20   mturner
//Initial revision.
//
//   Rev 1.9   Aug 19 2005 14:06:48   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.8.1.0   Jul 27 2005 18:11:44   asinclair
//Check in to fix build errors.  Work in progress
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.8   Mar 29 2005 11:55:40   tmollart
//Removed initialisation of parameters. Now in FindFareInputAdapter.
//
//   Rev 1.7   Mar 01 2005 18:18:16   tmollart
//Modified so that Initialise method sets date flexibility back to default values.
//
//   Rev 1.6   Feb 28 2005 11:33:44   jmorrissey
//Updated Initialise method. Removed GetDefaultSearchType method which was a duplication of the one in the base class.
//
//   Rev 1.5   Feb 16 2005 14:09:10   tmollart
//Work in progress. Added Initialise method.
//
//   Rev 1.4   Jan 31 2005 16:56:14   tmollart
//Work in progress for Del 7 Find A Fare.
//
//   Rev 1.3   Jan 17 2005 15:52:48   tmollart
//Added [Serializabe] directive.
//
//   Rev 1.2   Jan 14 2005 15:18:52   jmorrissey
//OutwardDate and ReturnDate now held as strings rather than TDDateTime objects
//
//   Rev 1.1   Jan 07 2005 16:06:02   jmorrissey
//Corrected EndLocation property
//
//   Rev 1.0   Dec 22 2004 15:29:24   tmollart
//Initial revision.

using System;
using System.Collections;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Class for cost based search parameters
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class CostSearchParams : TDJourneyParameters
	{

		#region Private Member Declarations
	
		private int outwardFlexibilityDays;
		private int inwardFlexibilityDays;
		private TicketTravelMode [] travelModesParams;
		private string railDiscountedCard;
		private string coachDiscountedCard;
		
		#endregion

		#region Constructor

		public CostSearchParams()
		{
		}

		#endregion Constructor

		#region Public Properties

		/// <summary>
		/// Read/Write property. Gets/Sets outward flexibility in days
		/// </summary>
		public int OutwardFlexibilityDays
		{
			get {return outwardFlexibilityDays;}
			set {outwardFlexibilityDays = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets inward flexibility in days
		/// </summary>
		public int InwardFlexibilityDays
		{
			get {return inwardFlexibilityDays;}
			set {inwardFlexibilityDays = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets travel mode property
		/// </summary>
		public TicketTravelMode [] TravelModesParams
		{
			get {return travelModesParams;}
			set {travelModesParams = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets rail discounted card
		/// </summary>
		public string RailDiscountedCard
		{
			get {return railDiscountedCard;}
			set {railDiscountedCard = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets coach discounted card
		/// </summary>
		public string CoachDiscountedCard
		{
			get {return coachDiscountedCard;}
			set {coachDiscountedCard = value;}
		}
		
		#endregion Public Properties

		#region Public Methods

		/// <summary>
		/// Initialises cost based properties of the class. Journey specifics
		/// handled by call to Initialise on the base class.
		/// </summary>
		public override void Initialise()
		{	
			//Initialise inherited properties.
			base.Initialise ();
		}	
	
		public override string InputSummary()
		{
			return null;
		}

		#endregion Public Methods

	}
}