//********************************************************************************
//NAME         : CoachFareForJourney.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of CoachFareForJourney class. Stores coach fares for IF031/32.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/CoachFareForJourney.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:26   mturner
//Initial revision.
//
//   Rev 1.9   Jan 03 2006 12:42:56   mguney
//Default constructor included.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.8   Oct 29 2005 17:02:28   mguney
//Changed according to the new Atkins dll namespace: FaresProviderInterface
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7   Oct 25 2005 15:01:44   mguney
//ChildAgeRange property removed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.6   Oct 22 2005 12:14:42   mguney
//Constructor changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Oct 20 2005 18:38:40   mguney
//ChildAgeMin and ChildAgeMax moved from CoachFareForRoute to CoachFare.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.4   Oct 19 2005 15:37:02   mguney
//FareType accessibility changed.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Oct 19 2005 15:19:50   mguney
//FareType moved from CoachFareForJourney to CoachFare.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 13 2005 14:52:36   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:08   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:00:22   mguney
//Initial revision.

using System;
using System.Globalization;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.FaresProviderInterface;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Stores coach fares for IF031/32.
	/// </summary>
	public class CoachFareForJourney : CoachFare
	{		
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CoachFareForJourney()
		{}

		/// <summary>
		/// Constructor for instantiating a CoachFareIF3132 instance using a CJPInterface::Fare object.
		/// </summary>
		/// <param name="CJPFare">CJPInterface::Fare to be used to instantiate.</param>
		public CoachFareForJourney(Fare cjpFare,TDNaptan originNaPTAN,TDNaptan destinationNaPTAN) : base(cjpFare)
		{
			//set own attributes
			this.additionalFees = cjpFare.additionalFees;
			this.additionalFeesDescription = cjpFare.additionalFeesDescription;
			this.bookingFee = cjpFare.bookingFee;			
			this.fareTypeConditions = cjpFare.fareTypeConditions;
			this.taxes = cjpFare.taxes;
			//set inherited attributes
			this.OriginNaPTAN = originNaPTAN;
			this.DestinationNaPTAN = destinationNaPTAN;															
		}

		#endregion

		#region Private variables
		private int additionalFees;
		private string additionalFeesDescription;
		private int bookingFee;
		private string fareTypeConditions;
		private int taxes;
		#endregion

		#region Public properties
		/// <summary>
		/// Additional fees for a coach fare. [rw]
		/// </summary>
		public int AdditionalFees
		{
			get {return additionalFees;}
			set {additionalFees = value;}
		}

		/// <summary>
		/// Additional fees description. [rw]
		/// </summary>
		public string AdditionalFeesDescription
		{
			get {return additionalFeesDescription;}
			set {additionalFeesDescription = value;}
		}
		
		/// <summary>
		/// Booking fee. [rw]
		/// </summary>
		public int BookingFee
		{
			get {return bookingFee;}
			set {bookingFee = value;}
		}									
		
		/// <summary>
		/// Fare type conditions. [rw]
		/// </summary>
		public string FareTypeConditions
		{
			get {return fareTypeConditions;}
			set {fareTypeConditions = value;}
		}
		
		/// <summary>
		/// Taxes in pences. [rw]
		/// </summary>
		public int Taxes
		{
			get {return taxes;}
			set {taxes = value;}
		}
		#endregion

	}
}