//********************************************************************************
//NAME         : StopDto.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Data transfer object for a Stop.
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/StopDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:54   mturner
//Initial revision.
//
//   Rev 1.1   Oct 15 2003 20:02:58   CHosegood
//Changed all occurences of DateTime to TDDateTime
//
//   Rev 1.0   Oct 13 2003 13:27:12   CHosegood
//Initial Revision

using System;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Data transfer object for a Stop.
	/// </summary>
	[Serializable]
	public class StopDto
	{
        private LocationDto location;
        private TDDateTime arrival;
        private TDDateTime departure;

        /// <summary>
        /// The location of the stop
        /// </summary>
        public LocationDto Location 
        {
            get { return this.location; }
            set { this.location = value; }
        }

        /// <summary>
        /// The arrival date/time of the stop
        /// </summary>
        public TDDateTime Arrival 
        {
            get { return this.arrival; }
            set { this.arrival = value; }
        }

        /// <summary>
        /// The departure date/time of the stop
        /// </summary>
        public TDDateTime Departure 
        {
            get { return this.departure; }
            set { this.departure = value; }
        }

        /// <summary>
        /// Data transfer object for a Stop
        /// </summary>
        /// <param name="location">The location of the stop</param>
        public StopDto( LocationDto location )
		{
            this.Location = location;
		}
	}
}
