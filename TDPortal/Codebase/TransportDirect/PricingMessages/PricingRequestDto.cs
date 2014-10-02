//********************************************************************************
//NAME         : PricingRequestDto.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Data Transfer Object for Pricing Request
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/PricingRequestDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:52   mturner
//Initial revision.
//
//   Rev 1.8   Apr 03 2005 17:59:26   RPhilpott
//Allow for null return date in sort comparer method.
//
//   Rev 1.7   Mar 22 2005 16:08:36   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.6   Sep 10 2004 10:37:06   passuied
//removed check in OutwardDate and ReturnDate
//Resolution for 1550: Compare city-to-city - show train fares - crash message displayed
//
//   Rev 1.5   Jan 09 2004 16:47:42   CHosegood
//  Now includes overriden origin and location attributes.  This is for when the origion/destionation is desired to be the station group and not the station.
//Resolution for 585: Pricing does not obtain all valid fares for stations within a group.
//
//   Rev 1.4   Nov 11 2003 11:50:34   CHosegood
//Changed for fxCop
//
//   Rev 1.3   Oct 15 2003 20:03:02   CHosegood
//Changed all occurences of DateTime to TDDateTime
//
//   Rev 1.2   Oct 15 2003 11:36:10   CHosegood
//Origin now returns the Origin instead of the Destination
//
//   Rev 1.1   Oct 13 2003 13:26:36   CHosegood
//Added comments and origin/destination now obtained from the trains list.
//
//   Rev 1.0   Oct 08 2003 11:34:20   CHosegood
//Initial Revision

using System;
using System.Collections;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Data Transfer Object for Pricing Request.
	/// </summary>
	[Serializable]
	public class PricingRequestDto : IComparable
	{
        private int ticketClass;
        private string numberOfAdults = string.Empty;
        private string numberOfChildren = string.Empty;
        private string railcard = string.Empty;
        private TDDateTime outwardDate = null;
        private TDDateTime returnDate = null;
		private ArrayList trains = null;
		private JourneyType journeyType = JourneyType.Return;
		private int journeyIndex = 0;

        private LocationDto overriddenOrigin = null;
        private LocationDto overriddenDestination = null;

        /// <summary>
        /// The journey origin
        /// </summary>
        public LocationDto Origin
        {
            get 
            {
                LocationDto org = null;
				
				if  (overriddenOrigin == null && trains.Count > 0)
				{
					org = ((TrainDto)trains[0]).Board.Location;
				}
				else
				{
					org = overriddenOrigin;
				}
                return org;
            }

			set { this.overriddenOrigin = value; }
        }

        /// <summary>
        ///  The journey destination
        /// </summary>
        public LocationDto Destination
        {
            get 
            {
                //Return the destination of the last outbound train
                LocationDto dest = null;

                if	(overriddenDestination == null && trains.Count > 0) 
                {
                    for (int i = 0; i < trains.Count; i++) 
                    {
                        if  (((TrainDto)trains[i]).ReturnIndicator.Equals(ReturnIndicator.Outbound)) 
                        {
                            dest = ((TrainDto)trains[i]).Alight.Location;
                        }
                    }
                } 
                else 
                {
                    dest = overriddenDestination;
                }
                return dest;
            }

            set { this.overriddenDestination = value;}
        }

        /// <summary>
        /// The class of the ticket
        /// </summary>
        public int TicketClass 
        {
            get { return this.ticketClass; }
            set { this.ticketClass = value; }
        }

        /// <summary>
        /// The number of adult passengers
        /// </summary>
        public int NumberOfAdults 
        {
            get 
            {
                if  ((numberOfAdults == null) || (numberOfAdults.Trim().Length == 0))
                    return 0;
                return int.Parse( numberOfAdults ); 
            }
            
			set 
            {
				if (value < 0)
				{		
					throw new TDException("Number of adults must be greater than or equal to 0", false, TDExceptionIdentifier.PRHInvalidPricingRequest);
				}
				else if (value == 0)
				{
					this.numberOfAdults = string.Empty.PadLeft(3, ' ');
				}
				else
				{
					this.numberOfAdults= value.ToString().PadLeft(3, ' ');
				}
            }
        }


        /// <summary>
        /// The number of children passengers
        /// </summary>
        public int NumberOfChildren 
        {
            get 
            {
				if  ((numberOfChildren == null) || (numberOfChildren.Trim().Length == 0))
				{
					return 0;
				}
				else
				{
					return int.Parse( numberOfChildren ); 
				}
            }
            
			set 
            {
				if (value < 0)
				{
					throw new TDException("Number of children must be greater than or equal to 0", false, TDExceptionIdentifier.PRHInvalidPricingRequest);
				}
				else if (value == 0)
				{
					this.numberOfChildren = string.Empty.PadLeft(3, ' ');
				}
				else
				{
					this.numberOfChildren= value.ToString().PadLeft(3,' ');
				}
            }
        }


        /// <summary>
        /// The railcard to apply discounts for
        /// </summary>
        public string Railcard
        {
            get { return railcard; }
        
			set 
            {
				if (value.Length != 3)
				{
					throw new TDException("Railcard must be 3 characters", false, TDExceptionIdentifier.PRHInvalidPricingRequest );
				}
                
				railcard= value.ToString().PadRight(3,' ');
            }
        }

        /// <summary>
        /// The outbound date time of the journey
        /// </summary>
        public TDDateTime OutwardDate 
        {
            get { return this.outwardDate; }
            
			set 
            {
                this.outwardDate = value; 
            }
        }

		/// <summary>
		/// The return date of the journey if known
		/// </summary>
		public TDDateTime ReturnDate 
		{
			get { return this.returnDate; }

			set 
			{
				this.returnDate = value; 
			}
		}

		/// <summary>
		/// Journey type  required 
		///  (note that Return implies OutwardSingles, too, here) 
		/// </summary>
		public JourneyType JourneyType 
		{
			get { return this.journeyType; }
			set { this.journeyType = value; }
		}

		/// <summary>
		/// JourneyIndex of the associated public journey (where relevant)  
		/// </summary>
		public int JourneyIndex 
		{
			get { return this.journeyIndex; }
			set { this.journeyIndex = value; }
		}

		/// <summary>
        /// The collection of trains involved in the journey.  This is an
        /// ordered list with the journey origin as the frist element and the
        /// journey destination as the last
        /// </summary>
        public ArrayList Trains
        {
            get { return this.trains; }
            set { this.trains = value; }
        }

        /// <summary>
        /// Data Transfer Object for Pricing Request
        /// </summary>
        public PricingRequestDto() 
        {
            this.trains = new ArrayList();
        }

        /// <summary>
        /// Data Transfer Object for Pricing Request
        /// </summary>
        /// <param name="ticketClass">The class of the ticket</param>
        /// <param name="numberOfAdults">The number of adults</param>
        /// <param name="numberOfChildren">The number of children</param>
        /// <param name="railcard">The railcard to apply discounts for</param>
        /// <param name="outwardDate">The outward date</param>
        /// <param name="returnDate">The return date if known</param>
        /// <param name="trains">The collection of trains involved in the
        /// journey.  This is an ordered list with the journey origin as the
        /// frist element and the journey destination as the last</param>
        public PricingRequestDto( int ticketClass, int numberOfAdults, int numberOfChildren,
            string railcard, TDDateTime outwardDate, TDDateTime returnDate,
            ArrayList trains)
        {
            this.ticketClass = ticketClass;
            this.NumberOfAdults = numberOfAdults;
            this.NumberOfChildren = numberOfChildren;
            this.railcard = railcard;
            this.outwardDate = outwardDate; 
            this.returnDate = returnDate;
            this.trains = trains;
        }


		/// <summary>
		/// Data Transfer Object for Pricing Request
		/// </summary>
		/// <param name="ticketClass">The class of the ticket</param>
		/// <param name="numberOfAdults">The number of adults</param>
		/// <param name="numberOfChildren">The number of children</param>
		/// <param name="railcard">The railcard to apply discounts for</param>
		/// <param name="outwardDate">The outward date</param>
		/// <param name="returnDate">The return date if known</param>
		/// <param name="trains">The collection of trains involved in the
		/// journey.  This is an ordered list with the journey origin as the
		/// frist element and the journey destination as the last</param>
		public PricingRequestDto(int ticketClass, int numberOfAdults, int numberOfChildren,
			string railcard, TDDateTime outwardDate, TDDateTime returnDate,
			LocationDto origin, LocationDto destination, JourneyType journeyType)
		{
			this.ticketClass = ticketClass;
			this.NumberOfAdults = numberOfAdults;
			this.NumberOfChildren = numberOfChildren;
			this.railcard = railcard;
			this.outwardDate = outwardDate; 
			this.returnDate = returnDate;
			this.overriddenOrigin = origin;
			this.overriddenDestination = destination;
			this.trains = new ArrayList(0);
			this.journeyType = journeyType;
		}

		public int CompareTo(object obj)
		{
			
			if	(obj == null) 
			{
				return 1;
			}

			if	(!(obj is PricingRequestDto))
			{
				throw new ArgumentException("Parameter not another PricingRequestDto");
			}

			PricingRequestDto otherDto = (PricingRequestDto) obj;

			if	(this.Origin.CompareTo(otherDto.Origin) < 0) 
			{
				return -1; 
			}
			else if (this.Origin.CompareTo(otherDto.Origin) > 0) 
			{
				return 1;
			}

			// origins are equal ...
		
			if	(this.Destination.CompareTo(otherDto.Destination) < 0) 
			{
				return -1; 
			}
			else if (this.Destination.CompareTo(otherDto.Destination) > 0) 
			{
				return 1;
			}

			// origins and destinations are equal ...

			if	(this.OutwardDate < otherDto.OutwardDate)
			{
				return -1; 
			}
			else if (this.OutwardDate > otherDto.OutwardDate) 
			{
				return 1;
			}

			// origins and destinations and outward dates are equal ...

			if	(this.ReturnDate == null && otherDto.ReturnDate != null) 
			{
				return -1;
			}
			else if (this.ReturnDate != null && otherDto.ReturnDate == null) 
			{
				return 1;
			}
			else if (this.ReturnDate == null && otherDto.ReturnDate == null) 
			{
				return 0;
			}

			if	(this.ReturnDate < otherDto.ReturnDate)
			{
				return -1; 
			}
			else if (this.ReturnDate > otherDto.ReturnDate) 
			{
				return 1;
			}

			// origins and destinations and outward and return dates are equal ...

			return 0;
		}

	}
}
