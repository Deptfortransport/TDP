// *********************************************** 
// NAME                 : LandingPageHelperConstants.cs
// AUTHOR               : David Lane
// DATE CREATED         : 05/02/2013
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CommonWeb/Helpers/LandingPageHelperConstants.cs-arc  $ 
//
//   Rev 1.0   Mar 22 2013 10:52:14   dlane
//Initial revision.
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//

using System;
using System.Web;
using System.Text;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using System.Security.Cryptography;
using TD.ThemeInfrastructure;
using TransportDirect.UserPortal.CoordinateConvertorProvider;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using CCP = TransportDirect.UserPortal.CoordinateConvertorProvider.CoordinateConvertorWebService;

namespace TransportDirect.CommonWeb.Helpers
{
	/// <summary>
	/// Helper class for dealing with the landing page.
	/// </summary>
	public class LandingPageHelperConstants
	{
		#region Parameter codes

		// Constants for parameters used in landing page URL
		public const string ParameterPartnerId = "id";
		public const string ParameterOriginType = "oo";
		public const string ParameterHashedOriginData = "oh";
		public const string ParameterOriginData = "o";
		public const string ParameterOriginText = "on";
		public const string ParameterDestinationType = "do";
		public const string ParameterHashedDestinationData = "dh";
		public const string ParameterDestinationData = "d";
		public const string ParameterDestinationText = "dn";
		public const string ParameterOutwardDate = "dt";
        public const string ParameterOutwardTime = "t";
        public const string ParameterOutwardDepartArrive = "da";
        public const string ParameterReturnRequired = "r";
        public const string ParameterReturnDate = "rdt";
        public const string ParameterReturnTime = "rt";
        public const string ParameterReturnDepartArrive = "rda";
        public const string ParameterMode = "m";
		public const string ParameterCarDefault = "c";
		public const string ParameterAutoPlan = "p";
		public const string ParameterEncryptedData = "enc";
		public const string ParameterExcludeModes = "ex";
        public const string ParameterEntryType = "et";
        public const string ParameterFindNearestType = "ft";
        public const string ParameterFindNearestPlace = "pn";
        public const string ParameterFindNearestLocGaz = "lg";

		#endregion

		#region Possible values for different types of parameter

		// Possible value for outward or return date
		public const string ValueDateNow = "now";

		// Possible values for the mode parameter
		public const string ValueModeMultimodal = "m";
		public const string ValueModeRoad = "r";
		public const string ValueModeTrain = "t";
		public const string ValueModeCoach = "c";
		public const string ValueModeAir = "a";
        public const string ValueModeCycle = "b";

		// Modes to exclude for a Find a Bus journey request
		public const string ValueModesFindABus = "r,u,t,f,p";

		// Possible values for departure and arrival time type
		public const string ValueDateTypeDepartAt = "d";
		public const string ValueDateTypeArriveBy = "a";

		// Possible values for location type
		public const string ValueLocationTypeOSGR = "en";
		public const string ValueLocationTypeNaPTAN = "n";
		public const string ValueLocationTypePostcode = "p";
		public const string ValueLocationTypeLongLat = "l";
		public const string ValueLocationTypeLocality = "lc";

		// Possible values for the autoplan flag
		public const string ValueAutoPlanOn = "1";
		public const string ValueAutoPlanOff = "0";

		//Values using for BusinessLinks HTML
		public const string PartnerId = "{PARTNER_ID}";
		public const string DestinationName = "{DESTINATION_NAME}";
		public const string DestinationType = "{DESTINATION_TYPE}";
		public const string DestinationData = "{DESTINATION_DATA}";
		public const string MonthOptions = "{MONTH_OPTIONS}";
		public const string TargetUrl = "{TARGET_URL}";
        public const string PartnerName = "{PARTNER_NAME}";
        	
		#endregion

		#region Other useful constants

		/// <summary>
		/// Separator character for parameters included in the encrypted section of the URL.
		/// </summary>
		public const string EncryptedValueSeparator = ":";

		/// <summary>
		/// Format string for dates used in landing page URLs
		/// </summary>
		public const string FullDateFormat = "ddMMyyyy";

		/// <summary>
		/// Format string for times used in landing page URLs
		/// </summary>
		public const string TimeFormat = "hhmm";

        // Used when checking for Valid NaPTANs in building a landing page url
        public const string originNaptanString = "Origin";
        public const string destinationNaptanString = "Destination";

		#endregion

		/// <summary>
		/// Default constructor. Does nothing.
		/// </summary>
		public LandingPageHelperConstants()
		{	}

	}
}
