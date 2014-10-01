// *********************************************** 
// NAME         : PublicJourneyCallingPoint.cs
// AUTHOR       : Richard Philpott
// DATE CREATED : 2005-07-07
// DESCRIPTION  : Class to encapsulate attributes of a 
//                single point on a public transport schedule
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/PublicJourneyCallingPoint.cs-arc  $
//
//   Rev 1.2   Feb 15 2010 12:06:34   apatel
//Added arrival information properties
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 09 2010 09:45:12   apatel
//Updated for TD International planner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Nov 08 2007 12:23:56   mturner
//Initial revision.
//
//   Rev 1.1   Jul 13 2005 16:54:14   RPhilpott
//Added Serializable attribute.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 12 2005 10:23:26   RPhilpott
//Initial revision.
//

using System;

using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Class to encapsulate attributes of a 
	/// single point on a public transport schedule	
	/// </summary>
	/// 
	[Serializable()]
	public class PublicJourneyCallingPoint
	{

		private TDLocation location;
		private TDDateTime arrivalDateTime;
		private TDDateTime departureDateTime;
		private PublicJourneyCallingPointType type;

        // Fields added during TDExtra CCN
        private string information;
        private string informationURL;
        private string accessibilityInfo;
        private string accessibiltiyURL;
        private string departureInfo;
        private string departureURL;
        private string cityInformationURL;
        private string arrivalInfo;
        private string arrivalURL;

		public PublicJourneyCallingPoint()
		{
		}


		public PublicJourneyCallingPoint(TDLocation location, 
			TDDateTime arrivalDateTime, 
			TDDateTime departureDateTime, 
			PublicJourneyCallingPointType type)
		{
			this.location = location;
			this.arrivalDateTime = arrivalDateTime;
			this.departureDateTime = departureDateTime;
			this.type = type;
		}

		public TDLocation Location
		{
			get { return location; }
			set { location = value; }
		}

		public TDDateTime ArrivalDateTime
		{
			get { return arrivalDateTime; }
			set { arrivalDateTime = value; }
		}

		public TDDateTime DepartureDateTime
		{
			get { return departureDateTime; }
			set { departureDateTime = value; }
		}

		public PublicJourneyCallingPointType Type
		{
			get { return type; }
			set { type = value; }
		}

        /// <summary>
        /// Read/write property - Information url description for the calling point
        /// </summary>
        public string Information
        {
            get { return information; }
            set { information = value; }
        }

        /// <summary>
        /// Read/write property - Information url for the calling point
        /// </summary>
        public string InformationURL
        {
            get { return informationURL; }
            set { informationURL = value; }
        }

        /// <summary>
        /// Read/write property - Accessibility url description for the calling point
        /// </summary>
        public string AccessibilityInfo
        {
            get { return accessibilityInfo; }
            set { accessibilityInfo = value; }
        }

        /// <summary>
        /// Read/write property - Accessibility url for the calling point
        /// </summary>
        public string AccessibilityURL
        {
            get { return accessibiltiyURL; }
            set { accessibiltiyURL = value; }
        }

        /// <summary>
        /// Read/write property - Live departure url description for the calling point
        /// </summary>
        public string DepartureInfo
        {
            get { return departureInfo; }
            set { departureInfo = value; }
        }

        /// <summary>
        /// Read/write property - Live departure url for the calling point
        /// </summary>
        public string DepartureURL
        {
            get { return departureURL; }
            set { departureURL = value; }
        }

        /// <summary>
        /// Read/write property - Url providing infromation url for the calling point city
        /// </summary>
        public string CityInformationURL
        {
            get { return cityInformationURL; }
            set { cityInformationURL = value; }
        }

        /// <summary>
        /// Read/write property - Live arrival url description for the calling point
        /// </summary>
        public string ArrivalInfo
        {
            get { return arrivalInfo; }
            set { arrivalInfo = value; }
        }

        /// <summary>
        /// Read/write property - Live arrival url for the calling point
        /// </summary>
        public string ArrivalURL
        {
            get { return arrivalURL; }
            set { arrivalURL = value; }
        }
	}


	public enum PublicJourneyCallingPointType
	{
		Origin,
		Destination,
		Board,
		Alight,
		CallingPoint,
		PassingPoint,
		OriginAndBoard,
		DestinationAndAlight
	}
}
