//********************************************************************************
//NAME         : CoachFareRemoteForRoute.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of CoachFareRemoteForRoute class. Used for storing coach fare information (IF098).
//				 This class is used in the web service to return fares. When it is returned, it is transformed
//				 into CoachFareForRoute objects.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/CoachFareRemoteForRoute.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:26   mturner
//Initial revision.
//
//   Rev 1.7   Dec 06 2005 11:20:50   mguney
//AvailabilityScore is changed with Probability.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.6   Nov 26 2005 13:12:16   mguney
//FareType property is included.
//Resolution for 3213: IF098 Interface Stub: Issues with displaying fares from IF098 stub
//
//   Rev 1.5   Oct 20 2005 18:38:34   mguney
//ChildAgeMin and ChildAgeMax moved from CoachFareForRoute to CoachFare.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.4   Oct 13 2005 14:52:48   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Oct 12 2005 14:42:58   mguney
//XMLSerialisation attribute added.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 12 2005 14:38:28   mguney
//XMLSerialisation attribute added.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:12   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:00:26   mguney
//Initial revision.

using System;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{

	/// <summary>
	/// Used for storing coach fare information (IF098).
	/// </summary>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info")]
	public class CoachFareRemoteForRoute
	{

		#region Private variables
		
		private string originNaPTAN;
		private string destinationNaPTAN;
		private DateTime fareStartDate;
		private DateTime fareEndDate;
		private DayRestriction[] dayRestrictions;
		
		private string[] changeNaPTANs;
		private TimeRestriction[] timeRestrictions;
		private string[] restrictedServices;
		private int advancePurchaseDays;
		private bool isDayReturn;
		private Probability availability;
		private string[] restrictedOperatorCodes;
		private bool isConcessionaryFare;
		private Flexibility flexibility;
		private bool isAdult;
		private bool isSingle;
		private int fare;
		private string passengerType;
		private string passengerTypeConditions;
		private string discountCardType;
		private int childAgeMin;
		private int childAgeMax;
		private string fareType;
		#endregion

		#region Public properties

		/// <summary>
		/// Origin naptan. [rw]
		/// </summary>
		public string OriginNaPTAN
		{
			get {return originNaPTAN;}
			set {originNaPTAN = value;}
		}

		/// <summary>
		/// Destination naptan. [rw]
		/// </summary>
		public string DestinationNaPTAN
		{
			get {return destinationNaPTAN;}
			set {destinationNaPTAN = value;}
		}

		/// <summary>
		/// Start date for the fare. [rw]
		/// </summary>
		public DateTime FareStartDate
		{
			get {return fareStartDate;}
			set {fareStartDate = value;}
		}

		/// <summary>
		/// End date for the fare. [rw]
		/// </summary>
		public DateTime FareEndDate
		{
			get {return fareEndDate;}
			set {fareEndDate = value;}
		}

		/// <summary>
		/// Array of restricted days. [rw]
		/// </summary>
		public DayRestriction[] DayRestrictions
		{
			get {return dayRestrictions;}
			set {dayRestrictions = value;}
		}

		/// <summary>
		/// Flexibility type for the fare. [rw]
		/// </summary>
		public Flexibility Flexibility
		{
			get {return flexibility;}
			set {flexibility = value;}
		}

		/// <summary>
		/// Array of naptans for change. [rw]
		/// </summary>
		public string[] ChangeNaPTANs
		{
			get {return changeNaPTANs;}
			set {changeNaPTANs = value;}
		}

		/// <summary>
		/// Array of time restrictions. [rw]
		/// </summary>
		public TimeRestriction[] TimeRestrictions
		{
			get {return timeRestrictions;}
			set {timeRestrictions = value;}
		}
		
		/// <summary>
		/// Array of restricted services. [rw]
		/// </summary>
		public string[] RestrictedServices
		{
			get {return restrictedServices;}
			set {restrictedServices = value;}
		}
		
		/// <summary>
		/// Number of days for advance purchasing.
		/// </summary>
		public int AdvancePurchaseDays
		{
			get {return advancePurchaseDays;}
			set {advancePurchaseDays = value;}
		}

		/// <summary>
		/// IsDayReturn bool. [rw]
		/// </summary>
		public bool IsDayReturn
		{
			get {return isDayReturn;}
			set {isDayReturn = value;}
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
		/// Array of restricted operator codes. [rw]
		/// </summary>
		public string[] RestrictedOperatorCodes
		{
			get {return restrictedOperatorCodes;}
			set {restrictedOperatorCodes = value;}
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
		/// Passenger type conditions. [rw]
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
		/// Concessionary fare or not. [rw]
		/// </summary>
		public bool IsConcessionaryFare
		{
			get {return isConcessionaryFare;}
			set {isConcessionaryFare = value;}
		}

		/// <summary>
		/// Min child age. [rw]
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

		/// <summary>
		/// Fare type. [rw]
		/// </summary>
		public string FareType
		{
			get {return fareType;}
			set {fareType = value;}
		}
		

		#endregion

	}
}