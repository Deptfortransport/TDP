//********************************************************************************
//NAME         : Fares.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Collection of Fares
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Fares.cs-arc  $
//
//   Rev 1.2   Sep 24 2009 08:42:10   apatel
//updated so when a station returned by the fares result is a group station and location look up query returns no destination only then adding a request location in to locationdto array so request for journey details for the fare ticket return journeys
//Resolution for 5325: Find Cheaper Rail Fares - Journeys to Manchester Victoria fail to return any trains
//
//   Rev 1.1   Jan 11 2009 17:02:04   mmodi
//Updated to pass ZPBO flag
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:06   mturner
//Initial revision.
//
//   Rev 1.9   Apr 23 2007 14:55:56   asinclair
//Added an empty constructor
//
//   Rev 1.8   Apr 26 2006 12:15:02   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.7.1.0   Apr 05 2006 17:12:06   RPhilpott
//Obtain names of fare locations from LBO.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.7   Nov 25 2005 17:14:28   RPhilpott
//Fix Find-A-Fare case where we find individual and group tickets from same station.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.6   Nov 24 2005 18:22:48   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.5   Apr 29 2005 20:46:54   RPhilpott
//Correct handling of availability checking for return journeys from pricing time-based searches. 
//Resolution for 2342: Del 7 - PT - Door to Door planner does not respond to unavailable ticket as expected
//
//   Rev 1.4   Apr 20 2005 10:32:12   RPhilpott
//Add more thorough and robust error handling.
//Resolution for 2247: PT: error handling by Retail Business Objects
//
//   Rev 1.3   Jan 09 2004 16:16:50   CHosegood
//Added fareOriginNlc and fareDestinationNlc attributes
//Resolution for 585: Pricing does not obtain all valid fares for stations within a group.

using System;
using System.Text;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Summary description for Fares.
	/// </summary>
	public class Fares
	{
        private string currency = string.Empty;
        private int minimumChildAge;
        private int maximumChildAge;
        private ArrayList fares = new ArrayList();
		private bool fatalError = false;
		private string fareOriginNlc = string.Empty;
		private string fareDestinationNlc = string.Empty;

		/// <summary>
		/// The origin NLC used in the original fares request for this collection.
		/// This might not be the same as the the origin supplied in the request
		/// e.g Requested origin = Birmingham New St, fare origin = Birmingham Stations.
		/// Individual fares added later may have different values (property on Fare).  
		/// </summary>
		public string FareOriginNlc 
		{
			get { return fareOriginNlc; }
		}

		/// <summary>
		/// The destination NLC used in the original fares request for this collection.
		/// This might not be the same as the the destination supplied in the request
		/// e.g Requested dest = Birmingham New St, fare dest = Birmingham Stations.
		/// Individual fares added later may have different values (property on Fare).   
		/// </summary>
		public string FareDestinationNlc
		{
			get { return fareDestinationNlc; }
		}

        /// <summary>
        /// The fare currency
        /// </summary>
        public string Currency
        {
            get { return currency; }
        }

        /// <summary>
        /// The minimum child age for child fares
        /// </summary>
        public int MinimumChildAge
        {
            get { return minimumChildAge; }
        }

        /// <summary>
        /// The maximum child age for child fares
        /// </summary>
        public int MaximumChildAge
        {
            get { return maximumChildAge; }
        }

        /// <summary>
        /// The collection of fares
        /// </summary>
        public ArrayList Item
        {
            get { return fares; }
            set { this.fares = value; }
        }

		/// <summary>
		/// Indicates Critical or Error level 
		///   error code was returned by FBO.
		/// </summary>
		public bool FatalError
		{
			set { fatalError = value; }
			get { return fatalError; }
		}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="output">Output of the FBO or ZPBO get fares call</param>
        /// <param name="useZPBO">Flag to indicate if this used a FBO or ZPBO fares call</param>
        public Fares (BusinessObjectOutput output, string originNlc, string destinationNlc, bool originIsGroup, bool destIsGroup, TDDateTime fareDate, bool useZPBO)
        {
			if	(output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
			{
				fatalError = true;
				return;
			}

			currency = output.OutputBody.Substring(8,3);

			if (output.OutputBody.Substring(11,2).Trim().Length != 0)
			{
				minimumChildAge = int.Parse(output.OutputBody.Substring(11,2));
			}

			if (output.OutputBody.Substring(13,2).Trim().Length != 0) 
			{
				maximumChildAge = int.Parse(output.OutputBody.Substring(13,2));
			}

			if (output.OutputBody.Substring(15,4).Trim().Length != 0) 
			{
				fareDestinationNlc = output.OutputBody.Substring(17,4).Trim();
			}

			if (output.OutputBody.Substring(21,4).Trim().Length != 0) 
			{
				fareOriginNlc = output.OutputBody.Substring(23,4).Trim();
			}

			LookupTransform lookup = new LookupTransform();
			LocationDto[] origins = null;
			LocationDto[] destinations = null;

			// if the FBO has returned different NLC codes from the ones 
			//  we provided, the fare is for a group station ...

			if	(originIsGroup || !fareOriginNlc.Equals(originNlc))
			{
				origins = lookup.GetStationsForFareGroup(fareOriginNlc, fareDate);
                if (origins.Length == 0)
                {
                    origins = new LocationDto[] { new LocationDto(null, originNlc) };
                }
			}
			else
			{
				origins = new LocationDto[] { new LocationDto(null, fareOriginNlc) }; 
			}

			if	(destIsGroup || !fareDestinationNlc.Equals(destinationNlc))
			{
				destinations = lookup.GetStationsForFareGroup(fareDestinationNlc, fareDate);
                if (destinations.Length == 0)
                {
                    destinations = new LocationDto[] { new LocationDto(null, destinationNlc) };
                }
			}
			else
			{
				destinations = new LocationDto[] { new LocationDto(null, fareDestinationNlc) }; 
			}

			string originName	   = lookup.LookupNameForNlc(fareOriginNlc,		 fareDate);
			string destinationName = lookup.LookupNameForNlc(fareDestinationNlc, fareDate);

            int offset = output.RecordDetails[0].RecordLength;
            int length = output.RecordDetails[1].RecordLength;
            int numberOfRecords = output.RecordDetails[1].RecordOutput;

            for (int i = 0; i < numberOfRecords; i++) 
            {
                if ( (offset + i*length + length) <= output.OutputBody.Length ) 
                {
                    fares.Add(new Fare(output.OutputBody.Substring(offset + (i * length), length), fareOriginNlc, fareDestinationNlc, origins, destinations, originName, destinationName, fareDate, useZPBO));
                }
            }
        }

		/// <summary>
		/// Empty Constructor
		/// </summary>
		/// <param name="output">output of the FBO GF call</param>
		public Fares ()
		{

		}
	}
}
