//********************************************************************************
//NAME         : Fare.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Fare.cs-arc  $
//
//   Rev 1.2   Jun 03 2010 09:29:36   mmodi
//Retain the fare string used to build the fare, to allow subesequent calls to rebuild the fare if it leaves the domain without having to redo the fares request.
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.1   Jan 11 2009 17:00:10   mmodi
//Updated to hold specifc ZPBO values
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:04   mturner
//Initial revision.
//
//   Rev 1.9   Apr 26 2006 12:15:02   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.8.1.0   Apr 05 2006 17:12:08   RPhilpott
//Obtain names of fare locations from LBO.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.8   Nov 24 2005 18:22:50   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.7   Apr 29 2005 20:46:54   RPhilpott
//Correct handling of availability checking for return journeys from pricing time-based searches. 
//Resolution for 2342: Del 7 - PT - Door to Door planner does not respond to unavailable ticket as expected
//
//   Rev 1.6   Mar 22 2005 16:09:04   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.5   Apr 15 2004 17:04:46   CHosegood
//Added ToString() method
//Resolution for 663: Rail fares not being displayed
//
//   Rev 1.4   Oct 17 2003 10:09:28   CHosegood
//Added trace and removed total fare prices.  Journey type now a member of ticket
//
//   Rev 1.3   Oct 16 2003 15:20:34   CHosegood
//Sets a fare to Nan if one does not exist
//
//   Rev 1.2   Oct 15 2003 20:05:06   CHosegood
//Added QuotaControlled attribute
//
//   Rev 1.1   Oct 14 2003 12:23:20   CHosegood
//Ticket type is now a type of Ticket instead of a string
//
//   Rev 1.0   Oct 13 2003 15:25:38   CHosegood
//Initial Revision

using System;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
    /// <summary>
    /// Summary description for Fares.
    /// </summary>
    public class Fare
    {
        #region Private variables

        private Ticket ticketType;
		private LocationDto[] fareOrigins;
		private LocationDto[] fareDestinations;
		private string originNlc;
		private string destinationNlc;
        private string originNlcActual;          // Used by ZPBO to identify actual origin of fare (could be a group station)
        private string destinationNlcActual;    // Used by ZPBO to identify actual destination of fare (could be a group station)
		private string originName;
		private string destinationName;
        private string originNameActual;
        private string destinationNameActual;
		private Route route;
        private string restrictionCode;
        private string ticketValidityCode;
        private string railcardCode;
        private float adultFare;
        private string adultFareError;
        private float adultFareMinimum;
        private float childFare;
        private string childFareError;
        private float childFareMinimum;
        private string tocCode;
		private bool quotaControlled;
		private bool invalidForTrains;
        private string rawFareString;

        #endregion

        #region Public properties

        /// <summary>
        /// 
        /// </summary>
        public Ticket TicketType
        {
            get { return this.ticketType; }
            set { this.ticketType = value; }
        }

		/// <summary>
		/// The origin station(s) from which the fare applies.
		/// If more than one, these are the members of the fare 
		/// group to which the origin in the request applies (r/o).
		/// </summary>
		public LocationDto[] Origins 
		{
			get { return fareOrigins; }
		}

		/// <summary>
		/// The destination station(s) to which the fare applies.
		/// If more than one, these are the members of the fare 
		/// group to which the destination in the request applies (r/o).
		/// </summary>
		public LocationDto[] Destinations
		{
			get { return fareDestinations; }
		}

		/// <summary>
		/// NLC code of fare origin - may be code for an individual station or a group (r/o).
		/// </summary>
		public string FareOriginNlc 
		{
			get { return originNlc; }
		}

		/// <summary>
		/// NLC code of fare destination - may be code for an individual station or a group (r/o).
		/// </summary>
		public string FareDestinationNlc 
		{
			get { return destinationNlc; }
		}

        /// <summary>
        /// NLC code of fare origin - may be code for an individual station or a group/zone (r/o).
        /// This is set only by a ZPBO call. 
        /// This value represents the actual origin NLC this fare is valid for
        /// and can be different to the FareOriginNLC but is still valid for this journey.
        /// </summary>
        public string FareOriginNlcActual
        {
            get { return originNlcActual; }
        }

        /// <summary>
        /// NLC code of fare destination - may be code for an individual station or a group/zone (r/o).
        /// This is set only by a ZPBO call. 
        /// This value represents the actual destination NLC this fare is valid for
        /// and can be different to the FareDestinationNLC but is still valid for this journey.
        /// </summary>
        public string FareDestinationNlcActual
        {
            get { return destinationNlcActual; }
        }

		/// <summary>
		/// Name of fare origin - may be of an individual station or a group (r/o).
		/// </summary>
		public string OriginName 
		{
			get { return originName; }
		}

		/// <summary>
		/// Name of fare destination - may be of an individual station or a group (r/o).
		/// </summary>
		public string DestinationName 
		{
			get { return destinationName; }
		}

		/// <summary>
		/// Fare route code (r/o).
		/// </summary>
		public Route Route
        {
            get { return this.route; }
        }

        /// <summary>
		/// Fare restriction code (r/o).
		/// </summary>
        public string RestrictionCode
        {
            get { return this.restrictionCode; }
        }

        /// <summary>
		/// Fare ticket validity code (r/o).
		/// </summary>
        public string TicketValidityCode
        {
            get { return this.ticketValidityCode; }
        }

        /// <summary>
		/// Discount card used (r/o).
		/// </summary>
        public string RailcardCode
        {
            get { return this.railcardCode; }
        }

        /// <summary>
		/// Adult fare, in pounds (r/w)
		/// </summary>
        public float AdultFare
        {
            get { return this.adultFare; }
            set { this.adultFare = value; }
        }

        /// <summary>
        /// Fare error message (r/o)
        /// </summary>
        public string AdultFareError
        {
            get { return this.adultFareError.Trim(); }
        }

        /// <summary>
		/// Minimum adult fare, in pounds (r/o)
		/// </summary>
        public float AdultFareMinimum
        {
            get { return this.adultFareMinimum; }
        }

        /// <summary>
		/// Child fare, in pounds (r/w)
		/// </summary>
        public float ChildFare
        {
            get { return this.childFare; }
            set { this.childFare = value; }
        }

        /// <summary>
		/// Fare error message (r/o)
		/// </summary>
        public string ChildFareError
        {
            get { return this.childFareError.Trim(); }
        }

        /// <summary>
		/// Minimum adult fare, in pounds (r/o)
		/// </summary>
        public float ChildFareMinimum
        {
            get { return this.childFareMinimum; }
        }

        /// <summary>
		/// Train operator code (r/o)
		/// </summary>
        public string TocCode
        {
            get { return this.tocCode; }
        }

		/// <summary>
		/// True if fare quota controlled (r/w)
		/// </summary>
		public bool QuotaControlled 
		{
			get { return this.quotaControlled; }
			set { this.quotaControlled = value; }
		}

		/// <summary>
		/// True if validity of this fare has been checked
		/// against train services and it is not valid. (r/w)
		/// </summary>
		public bool InvalidForTrains
		{
			get { return this.invalidForTrains; }
			set { this.invalidForTrains = value; }
        }

        /// <summary>
        /// Read only. The raw fare string used to create this Fare object
        /// </summary>
        public string RawFareString
        {
            get { return this.rawFareString; }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Return the price of a given fare.  If the fare
        /// is equal to 99999999 then returns NaN.
        /// </summary>
        /// <param name="fare">The 8 characters to convert to the fare</param>
        /// <returns></returns>
        private float calculateFare(string fare) 
        {
            float price = float.NaN;
            if (!fare.Equals("99999999")) 
            {
                //If the value is not 99999999 the parse the fare
                price = float.Parse( fare.Insert(6,".") );
            }
            return price;
        }

        #endregion

        #region Constructor

        public Fare(string fare, string fareOriginNlc, string fareDestinationNlc, LocationDto[] origins, LocationDto[] destinations, string fareOriginName, string fareDestinationName, TDDateTime fareDate, bool useZPBO)
        {
            int current = 0;
            string temp = string.Empty;

            rawFareString = fare;

			fareOrigins = origins;
			fareDestinations = destinations;
			
			originNlc = fareOriginNlc;
			destinationNlc = fareDestinationNlc;
			originName = fareOriginName;
			destinationName = fareDestinationName;

            ticketType = new Ticket( fare.Substring(current, 3) );
            current += 3;
            
            string routeCode = fare.Substring(current, 5);
            current += 5;
            
            string crossLondon = fare.Substring(current, 1);
            current += 1;
            route = new Route( routeCode, crossLondon );
            
            restrictionCode = fare.Substring(current, 2);
            current += 2;
          
            if ( fare.Substring(current, 1).Equals("S") )
            {
                TicketType.Type = PricingMessages.JourneyType.OutwardSingle;
            }
            else if ( fare.Substring(current, 1).Equals("R") ) 
            {
                TicketType.Type = PricingMessages.JourneyType.Return;
            }
            current += 1;

            ticketValidityCode = fare.Substring(current, 2);
            current += 2;

            railcardCode = fare.Substring(current, 3);
            current += 3;

            temp = fare.Substring(current, 8);
            current += 8;
            adultFare = calculateFare( temp );

            adultFareError = fare.Substring(current, 5);
            current += 5;

            temp = fare.Substring(current, 8);
            current += 8;
            adultFareMinimum = calculateFare( temp );

            temp = fare.Substring(current, 8);
            current += 8;
            childFare = calculateFare( temp );

            childFareError = fare.Substring(current, 5);
            current += 5;

            temp = fare.Substring(current, 8);
            current += 8;
            childFareMinimum = calculateFare( temp );

            ticketType.TicketClass = (TicketClass) Enum.Parse( typeof(TicketClass),fare.Substring(current, 1), true );
            current += 1;

            tocCode = fare.Substring(current, 3);
            current += 3;

            // Only setup the actual fare nlc if ZPBO request (FBO request will not include this info)
            if (useZPBO)
            {
                originNlcActual = fare.Substring(current + 2, 4);
                current += 6;

                destinationNlcActual = fare.Substring(current + 2, 4);
                current += 6;

                LookupTransform lookup = new LookupTransform();

                originNameActual = lookup.LookupNameForNlc(originNlcActual, fareDate);
                destinationNameActual = lookup.LookupNameForNlc(destinationNlcActual, fareDate);
            }
        }

        #endregion

        #region Public methods

        public override string ToString() 
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbTemp = new StringBuilder(); // used to insert some padding to align the output

            sb.Append("TicketName[").Append(TicketType.Code).Append("] ");

            sbTemp.Append("TicketType[").Append(TicketType.Type).Append("] ");
            sb.Append(sbTemp.ToString().PadRight(26, ' '));
            sbTemp = new StringBuilder();

            sb.Append("Route[").Append(route.Code).Append("] ");
			sb.Append("Origin[").Append(originNlc).Append(" ").Append(originName).Append("] ");
			sb.Append("Destination[").Append(destinationNlc).Append(" ").Append(destinationName).Append("] ");
            sb.Append("OriginActual[").Append(originNlcActual).Append(" ").Append(originNameActual).Append("] ");
            sb.Append("DestinationActual[").Append(destinationNlcActual).Append(" ").Append(destinationNameActual).Append("] ");
			sb.Append("CrossLondon[").Append(route.CrossLondon).Append("] ");
			sb.Append("RestrictionCode[").Append(RestrictionCode).Append("] ");
            sb.Append("ticketValidityCode[").Append(TicketValidityCode).Append("] ");
            sb.Append("railcardCode[").Append(RailcardCode).Append("] ");
            sb.Append("adultFare[").Append(AdultFare.ToString().PadLeft(6,' ')).Append("] ");
            sb.Append("adultFareError[").Append(AdultFareError).Append("] ");
            sb.Append("adultFareMinimum[").Append(AdultFareMinimum.ToString().PadLeft(6,' ')).Append("] ");
            sb.Append("childFare[").Append(ChildFare.ToString().PadLeft(6,' ')).Append("] ");
            sb.Append("childFareError[").Append(ChildFareError).Append("] ");
            sb.Append("childFareMinimum[").Append(ChildFareMinimum.ToString().PadLeft(6,' ')).Append("] ");
            
            sbTemp.Append("ticketClass[").Append(TicketType.TicketClass).Append("] ");
            sb.Append(sbTemp.ToString().PadRight(22, ' '));
            sbTemp = new StringBuilder();

            sb.Append("tocCode[").Append(TocCode).Append("] ");
			sb.Append("quotaControlled[").Append(QuotaControlled).Append("] ");
			sb.Append("invalidForTrains[").Append(InvalidForTrains).Append("] ");

            return sb.ToString();
        }

        #endregion
    }
}
