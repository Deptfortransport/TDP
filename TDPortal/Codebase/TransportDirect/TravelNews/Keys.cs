using System;

namespace TransportDirect.UserPortal.TravelNews
{
	/// <summary>
	/// Summary description for Keys.
	/// </summary>
	internal class Keys
	{
		/// <summary>
		/// Constructor. Does nothing.
		/// </summary>
		private Keys(){}

		// stored procedures
		public const string travelNewsHeadlinesProc = "TravelNewsHeadlines";
		public const string travelNewsAllProc = "TravelNewsAll";

		// columns
		public const string uidColumn = "UID";
		public const string severityLevelColumn = "SeverityLevel";
		public const string severityDescriptionColumn = "SeverityDescription";
		public const string publicTransportOperatorColumn = "PublicTransportOperator";
		public const string operatorColumn = "Operator";
		public const string modeOfTransportColumn = "ModeOfTransport";
		public const string regionsColumn = "Regions";
		public const string locationColumn = "Location";
		public const string regionsLocationColumn = "RegionsLocation";
		public const string incidentTypeColumn = "IncidentType";
		public const string headlineTextColumn = "HeadlineText";
		public const string detailTextColumn = "DetailText";
		public const string incidentStatusColumn = "IncidentStatus";
		public const string eastingColumn = "Easting";
		public const string northingColumn = "Northing";
		public const string reportedDateTimeColumn = "ReportedDateTime";
		public const string startDateTimeColumn = "StartDateTime";
		public const string startToNowMinDiffColumn = "StartToNowMinDiff";
		public const string lastModifiedDateTimeColumn = "LastModifiedDateTime";
		public const string clearedDateTimeColumn = "ClearedDateTime";
		public const string expiryDateTimeColumn = "ExpiryDateTime";

        //new key added for planned incidents CCN 0421
        public const string plannedIncidentColumn = "PlannedIncident";

        //new key added for road type CCN 0421
        public const string roadTypeColumn = "RoadType";

        //new keys for travel news hierarchy, times of roadworks & rss functionality
        public const string IncidentParentColumn = "IncidentParent";
        public const string CarriagewayDirectionColumn = "CarriagewayDirection";
        public const string RoadNumberColumn = "RoadNumber";
        public const string DayMaskColumn = "DayMask";
        public const string DailyStartTimeColumn = "DailyStartTime";
        public const string DailyEndTimeColumn = "DailyEndTime";
        public const string ItemChangeStatusColumn = "ItemChangeStatus";
        public const string IncidentActiveStatusColumn = "IncidentActiveStatus";
        // New key to identify the affected toids column
        public const string AffectedToidsColumn = "AffectedToids";
        // New key to identify closure or blockages
        public const string IsClosureColumn = "IsClosure";
	}

}
