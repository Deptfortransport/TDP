//********************************************************************************
//NAME         : CoachFareForRoute.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of CoachFareForRoute class. Stores coach fares for IF098.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/CoachFareForRoute.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:26   mturner
//Initial revision.
//
//   Rev 1.5   Dec 23 2005 15:46:02   mguney
//Default constructor added.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.4   Nov 26 2005 11:52:48   mguney
//FareType property is set in the constructor.
//Resolution for 3213: IF098 Interface Stub: Issues with displaying fares from IF098 stub
//
//   Rev 1.3   Oct 20 2005 18:38:26   mguney
//ChildAgeMin and ChildAgeMax moved from CoachFareForRoute to CoachFare.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 13 2005 14:52:42   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:10   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:00:24   mguney
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Stores coach fares for IF098.
	/// </summary>
	public class CoachFareForRoute	: CoachFare
	{		

		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CoachFareForRoute()
		{}

		/// <summary>
		/// This is going to be used to transfer a CoachFareRemoteForRoute object into a CoachFareForRoute
		/// object after the web service returns the results.
		/// </summary>
		/// <param name="remoteCoachFare">CoachFareRemoteForRoute</param>
		public CoachFareForRoute(CoachFareRemoteForRoute remoteCoachFare)
		{
			this.advancePurchaseDays = remoteCoachFare.AdvancePurchaseDays;
			this.Availability = remoteCoachFare.Availability;			
			this.ChildAgeMax = remoteCoachFare.ChildAgeMax;
			this.ChildAgeMin = remoteCoachFare.ChildAgeMin;
			
			this.DestinationNaPTAN = new TDNaptan(remoteCoachFare.DestinationNaPTAN,null);
			this.DiscountCardType = remoteCoachFare.DiscountCardType;
			this.Fare = remoteCoachFare.Fare;
			this.fareEndDate = new TDDateTime(remoteCoachFare.FareEndDate);				
			this.fareStartDate = new TDDateTime(remoteCoachFare.FareStartDate);
			this.Flexibility = remoteCoachFare.Flexibility;
			this.IsAdult = remoteCoachFare.IsAdult;
			this.isConcessionaryFare = remoteCoachFare.IsConcessionaryFare;
			this.isDayReturn = remoteCoachFare.IsDayReturn;
			this.IsSingle = remoteCoachFare.IsSingle;
			this.OriginNaPTAN = new TDNaptan(remoteCoachFare.OriginNaPTAN,null);
			this.PassengerType = remoteCoachFare.PassengerType;
			this.PassengerTypeConditions = remoteCoachFare.PassengerTypeConditions;
			this.FareType = remoteCoachFare.FareType;
			
			if (remoteCoachFare.RestrictedOperatorCodes != null)
			{
				this.restrictedOperatorCodes = new string [remoteCoachFare.RestrictedOperatorCodes.Length];
				for (int i=0;i < remoteCoachFare.RestrictedOperatorCodes.Length;i++)
					this.restrictedOperatorCodes[i] = remoteCoachFare.RestrictedOperatorCodes[i];
			}
			
			if (remoteCoachFare.RestrictedServices != null)
			{
				this.restrictedServices = new string[remoteCoachFare.RestrictedServices.Length];
				for (int i=0;i < remoteCoachFare.RestrictedServices.Length;i++)
					this.restrictedServices[i] = remoteCoachFare.RestrictedServices[i];			
			}
			
			if (remoteCoachFare.TimeRestrictions != null)
			{
				this.timeRestrictions = new TimeRestriction[remoteCoachFare.TimeRestrictions.Length];
				for (int i=0;i < remoteCoachFare.TimeRestrictions.Length;i++)
					this.timeRestrictions[i] = new TimeRestriction(remoteCoachFare.TimeRestrictions[i].StartTime,
						remoteCoachFare.TimeRestrictions[i].EndTime);
			}

			if (remoteCoachFare.DayRestrictions != null)
			{
				this.dayRestrictions = new DayRestriction[remoteCoachFare.DayRestrictions.Length];
				for (int i=0;i < remoteCoachFare.DayRestrictions.Length;i++)
					this.dayRestrictions[i] = remoteCoachFare.DayRestrictions[i];
			}

			if (remoteCoachFare.ChangeNaPTANs != null)
			{
				this.changeNaPTANs = new TDNaptan[remoteCoachFare.ChangeNaPTANs.Length]; 
				for (int i=0;i < remoteCoachFare.ChangeNaPTANs.Length;i++)
					this.changeNaPTANs[i] = new TDNaptan(remoteCoachFare.ChangeNaPTANs[i],null);
			}
			
		}
		#endregion

		#region Private variables
				
		private TDDateTime fareStartDate;
		private TDDateTime fareEndDate;
		private DayRestriction[] dayRestrictions;
		private TDNaptan[] changeNaPTANs;
		private TimeRestriction[] timeRestrictions;
		private string[] restrictedServices;
		private int advancePurchaseDays;
		private bool isDayReturn;		
		private string[] restrictedOperatorCodes;
		private bool isConcessionaryFare;				

		#endregion

		#region Public properties						

		/// <summary>
		/// Start date for the fare. [rw]
		/// </summary>
		public TDDateTime FareStartDate
		{
			get {return fareStartDate;}
			set {fareStartDate = value;}
		}

		/// <summary>
		/// Fare end date. [rw]
		/// </summary>
		public TDDateTime FareEndDate
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
		/// Array of change naptans. [rw]
		/// </summary>
		public TDNaptan[] ChangeNaPTANs
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
		/// Number of advance purchase days. [rw]
		/// </summary>
		public int AdvancePurchaseDays
		{
			get {return advancePurchaseDays;}
			set {advancePurchaseDays = value;}
		}

		/// <summary>
		/// Day return boolean. [rw]
		/// </summary>
		public bool IsDayReturn
		{
			get {return isDayReturn;}
			set {isDayReturn = value;}
		}				

		/// <summary>
		/// Array of restricted operators. [rw]
		/// </summary>
		public string[] RestrictedOperatorCodes
		{
			get {return restrictedOperatorCodes;}
			set {restrictedOperatorCodes = value;}
		}

		/// <summary>
		/// Concessionary fare bool. [rw]
		/// </summary>
		public bool IsConcessionaryFare
		{
			get {return isConcessionaryFare;}
			set {isConcessionaryFare = value;}
		}		

		#endregion

	}
}