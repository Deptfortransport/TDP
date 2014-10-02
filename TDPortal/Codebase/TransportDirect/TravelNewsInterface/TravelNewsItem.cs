// *********************************************** 
// NAME                 : TravelNewsItem.cs
// AUTHOR               : Atos
// DATE CREATED         : 06/09/2011
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNewsInterface/TravelNewsItem.cs-arc  $ 
//
//   Rev 1.5   Sep 06 2011 11:20:40   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//

using System;
using System.Data;
using System.Collections.Generic;


namespace TransportDirect.UserPortal.TravelNewsInterface
{
	/// <summary>
	/// Summary description for TravelNewsItem.
	/// </summary>
	[Serializable]
	public class TravelNewsItem
	{
		private string sUid = string.Empty;
		private SeverityLevel slSeverityLevel;
		private string sSeverityDescription = string.Empty;
		private string sPublicTransportOperator = string.Empty;
		private string sOperator = string.Empty;
		private string sModeOfTransport = string.Empty;
		private string sRegions = string.Empty;
		private string sLocation = string.Empty;
		private string sRegionsLocation = string.Empty;
		private string sIncidentType = string.Empty;
		private string sHeadlineText = string.Empty;
		private string sDetailText = string.Empty;
		private string sIncidentStatus = string.Empty;
		private int iEasting = -1;
		private int iNorthing = -1;
		private DateTime dtReportedDateTime = DateTime.MinValue;
		private DateTime dtStartDateTime  = DateTime.MinValue;
		private int iStartToNowMinDiff = 0;
		private DateTime dtLastModifiedDateTime = DateTime.MinValue;
		private DateTime dtClearedDateTime = DateTime.MinValue;
		private DateTime dtExpiryDateTime = DateTime.MinValue;
        private bool sPlannedIncident = false; // Added  for PlannedIncident column in travelnews datatable

        //new members for travel news hierarchy, times of roadworks & rss functionality
        private string sIncidentParent = string.Empty;
        private string sCarriagewayDirection = string.Empty;
        private string sRoadNumber = string.Empty;
        //Default daymask to all days, tDailyStartTime to min and tDailyEndTime to max value respectively so
        //that any incidents with no values will just show according to previous criteria of expiry date etc.
        private string sDayMask = "SuMoTuWeThFrSa";
        private TimeSpan tDailyStartTime = TimeSpan.MinValue;
        private TimeSpan tDailyEndTime = TimeSpan.MaxValue;
        private string sItemChangeStatus = string.Empty;
        private IncidentActiveStatus iasIncidentActiveStatus;

        // Field to determine travel news affected toids
        private string[] affectedToids = null;
        // Determines if the closure/blockage is happen at the toids represented by travel news item
        private bool isClosure = false;

        // Determines when the travel news item is active 
        private TravelNewsEventDateTime[] travelNewsCalendar = null;
    		

		public TravelNewsItem ()
		{
		}

		public TravelNewsItem(DataRow travelNewsRow)
		{
		}

		public string Uid
		{
			get
			{
				return sUid;
			}
			set
			{
				sUid = value;
			}
		}

		public SeverityLevel SeverityLevel
		{
			get
			{
				return slSeverityLevel;
			}
			set
			{
				slSeverityLevel = value;
			}
		}

		public string SeverityDescription
		{
			get
			{
				return sSeverityDescription;
			}
			set
			{
				sSeverityDescription = value;
			}
		}

		public string PublicTransportOperator
		{
			get
			{
				return sPublicTransportOperator;
			}
			set
			{
				sPublicTransportOperator = value;
			}
		}

		public string Operator
		{
			get
			{
				return sOperator;

			}
			set
			{
				sOperator = value;
			}
		}

		public string ModeOfTransport
		{
			get
			{
				return sModeOfTransport;
			}
			set
			{
				sModeOfTransport = value;
			}
		}

		public string Regions
		{
			get
			{
				return sRegions;
			}
			set
			{
				sRegions = value;
			}
		}

		public string Location
		{
			get
			{
				return sLocation;
			}
			set
			{
				sLocation = value;
			}
		}

		public string RegionsLocation
		{
			get
			{
				return sRegionsLocation;
			}
			set
			{
				sRegionsLocation = value;
			}
		}

		public string IncidentType
		{
			get
			{
				return sIncidentType;
			}
			set
			{
				sIncidentType = value;
			}
		}

		public string HeadlineText
		{
			get
			{
				return sHeadlineText;
			}
			set
			{
				sHeadlineText = value;
			}
		}

		public string DetailText
		{
			get
			{
				return sDetailText;
			}
			set
			{
				sDetailText = value;
			}
		}

		public string IncidentStatus
		{
			get
			{
				return sIncidentStatus;
			}
			set
			{
				sIncidentStatus = value;
			}
		}

		public int Easting
		{
			get
			{
				return iEasting;
			}
			set
			{
				iEasting = value;
			}
		}

		public int Northing
		{
			get
			{
				return iNorthing;
			}
			set
			{
				iNorthing = value;
			}
		}

		public DateTime ReportedDateTime
		{
			get
			{
				return dtReportedDateTime;
			}
			set
			{
				dtReportedDateTime = value;
			}
		}

		public DateTime StartDateTime
		{
			get
			{
				return dtStartDateTime;
			}
			set
			{
				dtStartDateTime = value;
			}
		}

		public int StartToNowMinDiff
		{
			get
			{
				return iStartToNowMinDiff;
			}
			set
			{
				iStartToNowMinDiff = value;
			}
		}

		public	DateTime LastModifiedDateTime
		{
			get
			{
				return dtLastModifiedDateTime;
			}
			set
			{
				dtLastModifiedDateTime = value;
			}
		}

		public DateTime ClearedDateTime
		{
			get
			{
				return dtClearedDateTime;
			}
			set
			{
				dtClearedDateTime = value;
			}
		}

		public DateTime ExpiryDateTime
		{
			get
			{
				return dtExpiryDateTime;
			}
			set
			{
				dtExpiryDateTime = value;
			}
		}

        /// <summary>
        /// PlannedIncident to store Planned/Unplanned status of the news incident
        /// </summary>
        public bool PlannedIncident
        {
            get
            {
                return sPlannedIncident;
            }
            set
            {
                sPlannedIncident = value;
            }
        }

        /// <summary>
        /// Incident parent for hierarchy functionality
        /// </summary>
        public string IncidentParent
        {
            get
            {
                return sIncidentParent;
            }
            set
            {
                sIncidentParent = value;
            }
        }

        public string CarriagewayDirection
        {
            get
            {
                return sCarriagewayDirection;
            }
            set
            {
                sCarriagewayDirection = value;
            }
        }

        public string RoadNumber
        {
            get
            {
                return sRoadNumber;
            }
            set
            {
                sRoadNumber = value;
            }
        }

        /// <summary>
        /// Daymask for times of roadworks functionality
        /// </summary>
        public string DayMask
        {
            get
            {
                return sDayMask;
            }
            set
            {
                sDayMask = value;
            }
        }

        /// <summary>
        /// Daily start time for times of roadworks functionality
        /// </summary>
        public TimeSpan DailyStartTime
        {
            get
            {
                return tDailyStartTime;
            }
            set
            {
                tDailyStartTime = value;
            }
        }

        /// <summary>
        /// Daily end time for times of roadworks functionality
        /// </summary>
        public TimeSpan DailyEndTime
        {
            get
            {
                return tDailyEndTime;
            }
            set
            {
                tDailyEndTime = value;
            }
        }

        /// <summary>
        /// Change status for rss functionality
        /// </summary>
        public string ItemChangeStatus
        {
            get
            {
                return sItemChangeStatus;
            }
            set
            {
                sItemChangeStatus = value;
            }
        }

        /// <summary>
        /// incident active status for times of roadworks functionality
        /// </summary>
        public IncidentActiveStatus IncidentActiveStatus
        {
            get
            {
                return iasIncidentActiveStatus;
            }
            set
            {
                iasIncidentActiveStatus = value;
            }
        }

        /// <summary>
        /// Toids affected by the travel news
        /// </summary>
        public string[] AffectedToids
        {
            get
            {
                return affectedToids;
            }
            set
            {
                affectedToids = value;
            }
        }

        /// <summary>
        /// Determines if closure/blockage occured at the toids represented by the travel news
        /// </summary>
        public bool IsClosure
        {
            get
            {
                return isClosure;
            }
            set
            {
                isClosure = value; 
            }
        }

        /// <summary>
        /// Gets/Sets the travel news calender
        /// The calendar gives a daily start and end datetime for each day travel news incident is active
        /// </summary>
        public TravelNewsEventDateTime[] TravelNewsCalendar
        {
            get
            {
                return travelNewsCalendar;
            }
            set
            {
                travelNewsCalendar = value;
            }
        }

        
    }

}
