//********************************************************************************
//NAME         : LocationDto.cs
//AUTHOR       : Chris Hosegood
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Data Transfer Object for a location.
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/LocationDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:50   mturner
//Initial revision.
//
//   Rev 1.4   Apr 14 2005 21:03:48   RPhilpott
//Unit test changes
//
//   Rev 1.3   Mar 22 2005 16:08:36   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.2   Mar 01 2005 18:42:48   RPhilpott
//Cost Search Back End for Del 7 - work in progress
//
//   Rev 1.1   Jan 12 2004 14:23:50   CHosegood
//Added ERROR trace to CRS & NLC properties of LocationDto.
//Resolution for 585: Pricing does not obtain all valid fares for stations within a group.
//
//   Rev 1.0   Oct 13 2003 13:27:10   CHosegood
//Initial Revision

using System;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Data Transfer Object for a location.
	/// </summary>
	[Serializable]
	public class LocationDto : IComparable
	{
		private const int CRS_LENGTH = 3;
		private const int NLC_LENGTH = 4;
		private const int MINIMUM_NAPTAN_LENGTH = 5;
		
		private string nlc	  = new String(' ', NLC_LENGTH);
		private string crs	  = new String(' ', CRS_LENGTH);
		private string naptan = string.Empty;

        /// <summary>
        /// The CRS code that identifies the Location
        /// This must be either 3 characters long or null.
        /// </summary>
        public string Crs 
        {
            get { return this.crs; }
            set 
            {
                if (value != null) 
                {
                    if ( (value.Length != CRS_LENGTH) ) 
                    {
                        if (!value.Equals( string.Empty ) ) 
                        {
                            TDException tde = new TDException("Invalid CRS code " + value, false, TDExceptionIdentifier.PRHInvalidPricingRequest );
                            Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, "Invalid CRS code", tde ) ); 
                            throw tde;
                        }
                    }
                } 
                else 
                {
                    value = string.Empty;
                }

                this.crs = value.PadLeft(CRS_LENGTH,' ');
            }
        }

		/// <summary>
		/// The NLC code that identifies the Location
		/// This may be set to either 4 or null
		/// Only the first 4 characters of the NLC are maintained
		/// </summary>
		public string Nlc 
		{
			get { return this.nlc; }
			set 
			{
				if (value != null) 
				{
					if ( (value.Length < NLC_LENGTH) )
					{
						if ( !value.Equals( string.Empty ) )
						{
							TDException tde = new TDException("Invalid NLC code " + value, false, TDExceptionIdentifier.PRHInvalidPricingRequest );
							Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, "Invalid NLC code", tde ) ); 
							throw tde;
						}
					}
				} 
				else 
				{
					value = string.Empty;
				}

				this.nlc = value.PadLeft(NLC_LENGTH,' ').Substring(0, 4);
			}
		}

		/// <summary>
		/// The Naptan code that identifies the Location
		/// </summary>
		public string Naptan 
		{
			get { return this.naptan; }
			set 
			{
				if  (value != null) 
				{
					if  ((value.Length < MINIMUM_NAPTAN_LENGTH) )
					{
						if ( !value.Equals(string.Empty))
						{
							TDException tde = new TDException("Invalid naptan code " + value, false, TDExceptionIdentifier.PRHInvalidPricingRequest );
							Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, "Invalid naptan code", tde ) ); 
							throw tde;
						}
					}
				} 
				else 
				{
					value = string.Empty;
				}

				this.naptan = value;
			}
		}

		/// <summary>
		/// Data Transfer Object for a location
		/// </summary>
		/// <param name="crs">The CRS code for the location</param>
		/// <param name="nlc">The NLC code for the location</param>
		public LocationDto(string crs, string nlc)
		{
			this.Crs = crs;
			this.Nlc = nlc;
		}

		/// <summary>
		/// Data Transfer Object for a location
		/// </summary>
		/// <param name="crs">The CRS code for the location</param>
		/// <param name="nlc">The NLC code for the location</param>
		/// <param name="nlc">The Naptan code for the location</param>
		public LocationDto(string crs, string nlc, string naptan)
		{
			this.Crs = crs;
			this.Nlc = nlc;
			this.Naptan = naptan; 
		}
	
		public override string ToString()
		{
			return this.Crs + ":" + this.Nlc + ":" + this.Naptan;
		}

		public int CompareTo(object obj)
		{
			if	(obj == null) 
			{
				return 1;
			}

			if	(!(obj is LocationDto))
			{
				throw new ArgumentException("Parameter not another LocationDto");
			}

			LocationDto otherDto = (LocationDto) obj;
		
			// simple string comparison works becuase both Crs and Nlc are fixed-lenth strings ...  
			return (this.ToString().CompareTo(otherDto.ToString()));			
		}

	}
}