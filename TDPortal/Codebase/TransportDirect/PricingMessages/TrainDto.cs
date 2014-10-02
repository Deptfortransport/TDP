//********************************************************************************
//NAME         : TrainDto.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Data Transfer Object for trains
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/TrainDto.cs-arc  $
//
//   Rev 1.2   Jun 04 2010 11:02:04   mmodi
//New property to indicate if this train is for a Walk leg
//Resolution for 5547: Fares - Balaston to Wedgewood rail replacement bus journey shows no fares
//
//   Rev 1.1   Feb 18 2009 18:14:32   mmodi
//Hold the fare route codes
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:35:56   mturner
//Initial revision.
//
//   Rev 1.8   Aug 25 2005 14:41:16   RPhilpott
//Pass Retail Train Id to RVBO in place of UID.
//Resolution for 2710: NRS interface -- retail train id needed
//
//   Rev 1.7   Mar 22 2005 19:06:34   RPhilpott
//Relax over-zealous validation.
//
//   Rev 1.6   Mar 22 2005 16:08:36   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.5   Mar 01 2005 18:43:00   RPhilpott
//Cost Search Back End for Del 7 - work in progress
//
//   Rev 1.4   Oct 19 2003 14:09:18   acaunt
//constructor modified
//
//   Rev 1.3   Oct 15 2003 11:34:04   CHosegood
//Added ReturnIndicator and intermediateStops is now an array of locationDtos
//
//   Rev 1.2   Oct 13 2003 13:20:52   CHosegood
//Apply basic checks of instance members
//
//   Rev 1.1   Oct 08 2003 11:41:00   CHosegood
//Changed access modifiers
//
//   Rev 1.0   Oct 08 2003 11:34:24   CHosegood
//Initial Revision

using System;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Data Transfer Object for trains.
	/// </summary>
	[Serializable]
	public class TrainDto
	{
		private string uid = String.Empty;
		private string retailId = String.Empty;
		private ReturnIndicator returnIndicator;
        private TocDto[] toc = new TocDto[2];
		private StopDto origin;
		private StopDto board;
		private StopDto alight;
        private StopDto destination;
        private LocationDto[] intermediateStops;
        private string trainClass = String.Empty;
        private string sleeper =  String.Empty;
		private string catering = String.Empty;
		private string reservability = String.Empty;
		private bool invalidFare = false;
		private bool quotaControlledFare = false;
        private bool isForWalk = false;
        private string[] fareRouteCodes = new string[0];

		private const int MAX_CATERING_CODES = 5;

        #region Public properties

        /// <summary>
        /// The unique identifier for the train.
        /// This must be either null or less than or equal to 6 characters
        /// </summary>
        public string Uid 
        {
            get { return this.uid; }
            set {
                if (value != null) 
                {
                    if ( value.Length > 6 ) 
                        throw new TDException( "Invalid UID: " + uid, false, TDExceptionIdentifier.PRHInvalidPricingRequest );
                } 
                else 
                {
                    value = string.Empty;
                }

                this.uid = value.PadLeft(6, ' ');
            }
        }

		/// <summary>
		/// Gets/Sets the retail train id (used by NRS)
		/// </summary>
		public string RetailId 
		{
			get { return this.retailId; }
			set { this.retailId = value; }
		}

        /// <summary>
        /// 
        /// </summary>
        public ReturnIndicator ReturnIndicator 
        {
            get { return this.returnIndicator; }
            set { this.returnIndicator = value; }
        }

        /// <summary>
        /// TOC code of train operator
        /// </summary>
        public TocDto[] Tocs
        {
            get { return this.toc; }
            set 
            {
                if ( ( value == null ) || ( value.Length == 0 ) )
                {
                    this.toc[0] = new TocDto( string.Empty );
                    this.toc[1] = new TocDto( string.Empty );
                }
                else if (value.Length == 1) 
                {
                    this.toc[0] = value[0];
                    this.toc[1] = new TocDto( String.Empty );
                } 
                else 
                {
                    this.toc = value;
                }

            }
        }

		/// <summary>
		/// The boarding location for this train
		/// </summary>
		public StopDto Origin         
		{
			get { return this.origin; }
			set { this.origin = value; }
		}

		/// <summary>
		/// The boarding location for this train
		/// </summary>
		public StopDto Board         
		{
			get { return this.board; }
			set { this.board = value; }
		}

        /// <summary>
        /// The alight location for this train
        /// </summary>
        public StopDto Alight
        {
            get { return this.alight; }
            set { this.alight = value; }
        }

        /// <summary>
        /// The destination of the train
        /// </summary>
        public StopDto Destination
        {
            get { return this.destination; }
            set { this.destination = value; }
        }

        /// <summary>
        /// Intermediate stops between alight and board
        /// </summary>
        public LocationDto[] IntermediateStops
        {
            get { return this.intermediateStops; }
            set { this.intermediateStops = value; }
        }

        /// <summary>
        /// The class of the train
        /// </summary>
        public string TrainClass
        {
            get { return this.trainClass; }
            set 
            {
                if (value != null) 
                {
                    if ( value.Length != 1 ) 
                        throw new TDException( "Invalid train class: " + sleeper, false, TDExceptionIdentifier.PRHInvalidPricingRequest );
                } 
                else 
                {
                    value = string.Empty;
                }
                this.trainClass = value.PadLeft(1, ' ').Substring(0,1);
            }
        }

		/// <summary>
		/// Sleeper accommodation code for train
		/// </summary>
		public string Sleeper 
		{
			get { return this.sleeper; }
			set 
			{
				if (value != null) 
				{
					if ( value.Length != 1 ) 
						throw new TDException( "Invalid Sleeper: " + sleeper, false, TDExceptionIdentifier.PRHInvalidPricingRequest );
				} 
				else 
				{
					value = string.Empty;
				}
				this.sleeper = value.PadLeft(1, ' ').Substring(0,1);
			}
		}

		/// <summary>
		/// Reservability code for train
		/// </summary>
		public string Reservability 
		{
			get { return this.reservability; }
			set 
			{
				if (value != null) 
				{
					if ( value.Length > 1 ) 
						throw new TDException( "Invalid Reservability: " + reservability, false, TDExceptionIdentifier.PRHInvalidPricingRequest );
				} 
				else 
				{
					value = string.Empty;
				}
				this.reservability = value.PadLeft(1, ' ').Substring(0,1);
			}
		}

		/// <summary>
		/// Catering code(s) for train
		/// </summary>
		public string Catering 
		{
			get { return this.catering; }
			set 
			{
				if (value != null) 
				{
					if (value.Length > MAX_CATERING_CODES ) 
						throw new TDException( "Invalid Catering: " + catering, false, TDExceptionIdentifier.PRHInvalidPricingRequest );
				} 
				else 
				{
					value = string.Empty;
				}
				this.catering = value;
			}
		}

		/// <summary>
		/// Indicates if the current fare is quota-controlled on this train
		/// </summary>
		public bool QuotaControlledFare
		{
			get { return quotaControlledFare; }
			set { quotaControlledFare = value; }
		}
		
		/// <summary>
		/// Indicates if the current fare is invalid on this train
		/// </summary>
		public bool InvalidFare
		{
			get { return invalidFare; }
			set { invalidFare = value; }
        }

        /// <summary>
        /// Indicates if the train represents a Walk leg
        /// </summary>
        public bool IsForWalk
        {
            get { return isForWalk; }
            set { isForWalk = value; }
        }

        /// <summary>
        /// Gets/sets the fare route codes applicable to this train
        /// </summary>
        public string[] FareRouteCodes
        {
            get { return fareRouteCodes; }
            set { fareRouteCodes = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
		/// Data Transfer Object for trains
		/// </summary>
		/// <param name="uid">Train Unique Identifier</param>
		/// <param name="retailId">Retail Train Number</param>
		/// <param name="toc">Toc operator[s] of train</param>
		/// <param name="sleeper">Sleeper accomodation</param>
		/// <param name="reservability">Reservability of seats on this service</param>
		/// <param name="catering">Catering provision on this train service</param>
		/// <param name="board">Board Stop</param>
		/// <param name="alight">Alight stop</param>
		/// <param name="destination">Train Destination</param>
		/// <param name="intermediateStops">intermediate stops</param>
		public TrainDto(string uid, string retailId, TocDto[] toc, string trainClass, string sleeper, string reservability, string catering, ReturnIndicator returnIndicator, 
			StopDto origin, StopDto board, StopDto alight, StopDto destination, LocationDto[] intermediateStops, 
            string[] fareRouteCodes)
		{
			this.Uid = uid;
			this.retailId = retailId;
			this.Tocs = toc;
			this.TrainClass = trainClass;
			this.Sleeper = sleeper;
			this.ReturnIndicator = returnIndicator;
			this.Origin = origin;
			this.Board = board;
			this.Alight = alight;
			this.Destination = destination;
			this.IntermediateStops = intermediateStops;
			this.Catering = catering;
			this.Reservability = reservability;
            this.FareRouteCodes = fareRouteCodes;
        }

        #endregion
    }
}
