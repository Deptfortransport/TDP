// *********************************************** 
// NAME			: LocationInformation.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 18/10/07
// DESCRIPTION	: Class to hold Location Information detail
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationInformationService/LocationInformation.cs-arc  $
//
//   Rev 1.1   Jun 27 2008 09:40:54   apatel
//CCN - 458 Accessibility Updates - Improved linking
//
//   Rev 1.0   Nov 28 2007 14:56:42   mturner
//Initial revision.
//
//   Rev 1.0   Oct 25 2007 15:38:32   mmodi
//Initial revision.
//Resolution for 4518: Del 9.8 - Air Departure Boards
//

using System;

using TransportDirect.UserPortal.ExternalLinkService;

namespace TransportDirect.UserPortal.LocationInformationService
{
	public enum LocationInformationLinkType
	{
		Departure,
		Arrival,
		Information,
        Accessibility,
		None
	}

	/// <summary>
	/// Summary description for LocationInformation.
	/// </summary>
	public class LocationInformation
	{
		#region Private members
		private string departureLinkID;
		private string arrivalLinkID;
		private string informationLinkID;
        private string accessibilityLinkID;

		private ExternalLinkDetail departureLink;
		private ExternalLinkDetail arrivalLink;
		private ExternalLinkDetail informationLink;
        private ExternalLinkDetail accessibilityLink;
		#endregion

		#region Constructor
		/// <summary>
		/// Default constructor
		/// </summary>
		public LocationInformation()
		{
			departureLinkID = string.Empty;
			arrivalLinkID = string.Empty;
			informationLinkID = string.Empty;
            accessibilityLinkID = string.Empty;
		}

        public LocationInformation(string departureLink, string arrivalLink, string informationLink, string accessibilityLink)
		{
			this.departureLinkID = departureLink;
			this.arrivalLinkID = arrivalLink;
			this.informationLinkID = informationLink;
            this.accessibilityLinkID = accessibilityLink;
		}
		#endregion

		#region Public properties
		public string DepartureLinkID
		{
			get { return departureLinkID; }
			set { departureLinkID = value; }
		}

		public string ArrivalLinkID
		{
			get { return arrivalLinkID; }
			set { arrivalLinkID = value; }
		}

		public string InformationLinkID
		{
			get { return informationLinkID; }
			set { informationLinkID = value; }
		}

        public string AccessibilityLinkID
        {
            get { return accessibilityLinkID; }
            set { accessibilityLinkID = value; }
        }

		public ExternalLinkDetail DepartureLink
		{
			get { return departureLink; }
			set { departureLink = value; }
		}

		public ExternalLinkDetail ArrivalLink
		{
			get { return arrivalLink; }
			set { arrivalLink = value; }
		}

		public ExternalLinkDetail InformationLink
		{
			get { return informationLink; }
			set { informationLink = value; }
		}

        public ExternalLinkDetail AccessibilityLink
        {
            get { return accessibilityLink; }
            set { accessibilityLink = value; }
        }
		#endregion

		#region Public methods
		public void AddLinkID(string linkID, LocationInformationLinkType linkType)
		{
			switch (linkType)
			{
				case LocationInformationLinkType.Departure:
					this.departureLinkID = linkID;
					break;
				case LocationInformationLinkType.Arrival:
					this.arrivalLinkID = linkID;
					break;
				case LocationInformationLinkType.Information:
					this.informationLinkID = linkID;
					break;
                case LocationInformationLinkType.Accessibility:
                    this.accessibilityLinkID = linkID;
                    break;
			}
		}
		#endregion
	}
}
