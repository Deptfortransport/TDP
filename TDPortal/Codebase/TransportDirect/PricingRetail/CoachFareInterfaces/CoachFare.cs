//********************************************************************************
//NAME         : CoachFare.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of CoachFare class. Abstract class for handling coach fares.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/CoachFare.cs-arc  $
//
//   Rev 1.1   Feb 03 2012 16:01:36   mturner
//Changed default min and max ages for child fares and made these values configurable.
//Resolution for 5788: Change default age range for child coach fares
//
//   Rev 1.0   Nov 08 2007 12:36:26   mturner
//Initial revision.
//
//   Rev 1.9   Jan 18 2006 18:16:28   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.8   Dec 06 2005 11:20:26   mguney
//AvailabilityScore is changed with Probability.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.7   Nov 02 2005 11:09:08   mguney
//Age Constants changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.6   Oct 29 2005 17:02:28   mguney
//Changed according to the new Atkins dll namespace: FaresProviderInterface
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Oct 22 2005 12:14:14   mguney
//New constructor included.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.4   Oct 20 2005 18:38:48   mguney
//ChildAgeMin and ChildAgeMax moved from CoachFareForRoute to CoachFare.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Oct 19 2005 15:19:58   mguney
//FareType moved from CoachFareForJourney to CoachFare.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 13 2005 14:52:28   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:06   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:00:20   mguney
//Initial revision.

using System;
using System.Globalization;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.JourneyPlanning.FaresProviderInterface;


namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{

	/// <summary>
	/// Abstract class for handling coach fares.
	/// </summary>
	public class CoachFare
	{
		// Default values of valid child ages
		public const int ChildAgeDefaultMin = 3;
		public const int ChildAgeDefaultMax = 15;

		#region Constructors
		/// <summary>
		/// Constructor.
		/// </summary>
		public CoachFare()
        {
            if (!int.TryParse(Properties.Current["PricingRetail.CoachFares.ChildAgeMin"], out childAgeMin))
            {
                childAgeMin = ChildAgeDefaultMin;
            }
            if (!int.TryParse(Properties.Current["PricingRetail.CoachFares.ChildAgeMax"], out childAgeMax))
            {
                childAgeMax = ChildAgeDefaultMax;
            }
        }		

		/// <summary>
		/// Constructor using cjp fare.
		/// </summary>
		/// <param name="cjpFare"></param>
		public CoachFare(Fare cjpFare) : this()
		{
			
			fareType = cjpFare.fareType;
			fare = cjpFare.fare;
			isAdult = cjpFare.adult;
			isSingle = cjpFare.single;
			discountCardType = cjpFare.discountCardType;
			passengerType = cjpFare.passengerType;
			passengerTypeConditions = cjpFare.passengerTypeConditions;
			availability = Probability.High;

			switch (cjpFare.fareRestrictionType)
			{
				case TransportDirect.JourneyPlanning.FaresProviderInterface.FareType.Flexible: 
					flexibility = Flexibility.FullyFlexible;
					break;
				case TransportDirect.JourneyPlanning.FaresProviderInterface.FareType.LimitedFlexibility: 
					flexibility = Flexibility.LimitedFlexibility;
					break;
				case TransportDirect.JourneyPlanning.FaresProviderInterface.FareType.NotFlexible: 
					flexibility = Flexibility.NoFlexibility;
					break;
				default: flexibility = Flexibility.Unknown;
					break;
			}
			//set child age range properties
			string[] ages = cjpFare.childAgeRange.Split(new char[] {'-'});
			if (ages.Length == 2)
			{				
				try
				{
					childAgeMin = Convert.ToInt32(ages[0],CultureInfo.InvariantCulture);
					childAgeMax = Convert.ToInt32(ages[1],CultureInfo.InvariantCulture);
				}
				//Catch the format exception but do nothing.
				catch(FormatException) {}
			}
					
		}
		#endregion

		#region Private variables
		private TDNaptan originNaPTAN;
		private TDNaptan destinationNaPTAN;
		private Flexibility flexibility;
		private Probability availability;
		private bool isAdult;
		private bool isSingle;
		private int fare;
		private string passengerType;
		private string passengerTypeConditions;
		private string discountCardType;
		private string fareType;
		private int childAgeMin;
		private int childAgeMax;
		#endregion

		#region Public properties

		/// <summary>
		/// Origin NaPTAN property. [rw]
		/// </summary>
		public TDNaptan OriginNaPTAN
		{
			get {return originNaPTAN;}
			set {originNaPTAN = value;}
		}
		
		/// <summary>
		/// Destination NaPTAN property. [rw]
		/// </summary>
		public TDNaptan DestinationNaPTAN
		{
			get {return destinationNaPTAN;}
			set {destinationNaPTAN = value;}
		}

		/// <summary>
		/// Flexibility type. [rw]
		/// </summary>
		public Flexibility Flexibility
		{
			get {return flexibility;}
			set {flexibility = value;}
		}

		/// <summary>
		/// Availability score. [rw]
		/// </summary>
		public Probability Availability
		{
			get {return availability;}
			set {availability = value;}
		}

		/// <summary>
		/// Adult or not. [rw]
		/// </summary>
		public bool IsAdult
		{
			get {return isAdult;}
			set {isAdult = value;}
		}
		
		/// <summary>
		/// Single or not. [rw]
		/// </summary>
		public bool IsSingle
		{
			get {return isSingle;}
			set {isSingle = value;}
		}
		
		/// <summary>
		/// Fare in pences. [rw]
		/// </summary>
		public int Fare
		{
			get {return fare;}
			set {fare = value;}
		}
				
		/// <summary>
		/// Passenger type. [rw]
		/// </summary>
		public string PassengerType		
		{
			get {return passengerType;}	
			set {passengerType = value;}			
		}
		
		/// <summary>
		/// Passenger type conditions in string. [rw]
		/// </summary>
		public string PassengerTypeConditions
		{
			get {return passengerTypeConditions;}
			set {passengerTypeConditions = value;}
		}
		
		/// <summary>
		/// Discount card type. [rw]
		/// </summary>
		public string DiscountCardType
		{
			get {return discountCardType;}
			set {discountCardType = value;}
		}

		/// <summary>
		/// Fare type represented with a string. [rw]
		/// </summary>
		public string FareType
		{
			get {return fareType;}
			set {fareType = value;}
		}

		/// <summary>
		/// Minimum child age. [rw]
		/// </summary>
		public int ChildAgeMin
		{
			get {return childAgeMin;}
			set {childAgeMin = value;}
		}
		
		/// <summary>
		/// Max child age. [rw]
		/// </summary>
		public int ChildAgeMax
		{
			get {return childAgeMax;}
			set {childAgeMax = value;}
		}
				
		#endregion

	}
}