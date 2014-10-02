//********************************************************************************
//NAME         : CoachFareData.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 25/10/2005
//DESCRIPTION  : Implementation of CoachFareData class.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/CoachFareData.cs-arc  $
//
//   Rev 1.1   May 14 2009 17:11:20   jfrank
//CCN0503 to append route 60 to the start of national express Route 60 fares.
//
//   Rev 1.0   Nov 08 2007 12:36:44   mturner
//Initial revision.
//
//   Rev 1.3   Dec 06 2005 12:19:56   mguney
//Probability included.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.2   Nov 03 2005 17:37:54   RWilby
//Added Serializable attribute to class
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 26 2005 15:55:34   mguney
//Associated IR
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 26 2005 15:54:22   mguney
//Initial revision.

using System;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// CoachFareData.
	/// </summary>
	[Serializable]
	public class CoachFareData
	{
		public CoachFareData()
		{
			
		}

		#region Private members
		
		private string operatorCode;
		private string originNaptan;
		private string destinationNaptan;
		private bool isQuotaFare;
		private TimeRestriction[] timeRestrictions;
		private string[] restrictedServices;
		private string[] restrictedOperatorCodes;
		private string[] changeNaptans;
		private Probability probability;
        private string passengerType;

		#endregion

		#region Public properties

		/// <summary>
		/// CJP Operator code. [rw]
		/// </summary>
		public string OperatorCode
		{
			get {return operatorCode;}
			set {operatorCode = value;}
		}
		
		/// <summary>
		/// Origin naptan. [rw]
		/// </summary>
		public string OriginNaptan
		{
			get {return originNaptan;}
			set {originNaptan = value;}
		}
		
		/// <summary>
		/// Destination naptan. [rw]
		/// </summary>
		public string DestinationNaptan
		{
			get {return destinationNaptan;}
			set {destinationNaptan = value;}
		}

		/// <summary>
		/// Is Quata Fare. [rw]
		/// </summary>
		public bool IsQuotaFare
		{
			get {return isQuotaFare;}
			set {isQuotaFare = value;}
		}

		/// <summary>
		/// Time restriction. [rw]
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
		/// Array of restricted operator codes. [rw]
		/// </summary>
		public string[] RestrictedOperatorCodes
		{
			get {return restrictedOperatorCodes;}
			set {restrictedOperatorCodes = value;}
		}

		/// <summary>
		/// Array of change naptans. [rw]
		/// </summary>
		public string[] ChangeNaptans
		{
			get {return changeNaptans;}
			set {changeNaptans = value;}
		}

		/// <summary>
		/// Probability. [rw]
		/// </summary>
		public Probability Probability
		{
			get {return probability;}
			set {probability = value;}
		}

        /// <summary>
        /// Passenger Type. [rw]
        /// </summary>
        public string PassengerType
        {
            get { return passengerType; }
            set { passengerType = value; }
        }

		#endregion
	}
}
