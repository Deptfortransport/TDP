// *********************************************** 
// NAME                 : LandingPageHelper.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 23/11/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/LandingPageHelper.cs-arc  $ 
//
//   Rev 1.9   Mar 22 2013 10:48:56   dlane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.8   Aug 17 2012 13:19:02   DLane
//Cycle walk links - making plan cycle do both outward and return
//Resolution for 5827: CCN Cycle Walk links
//
//   Rev 1.7   Aug 02 2011 15:46:44   mmodi
//Corrected month options to be xhtml valid
//Resolution for 5719: Business link templates are not XHTML valid
//
//   Rev 1.6   Jul 13 2010 13:22:10   mmodi
//Check for invalid naptan before deciding to populate url data with naptan or coordinates
//Resolution for 5573: Landing page link from email issue when not NaPTAN
//
//   Rev 1.5   Oct 06 2009 14:38:26   apatel
//Social bookmarking changes
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.4   Oct 01 2009 15:20:08   apatel
//parameters added for Find Nearest Car Park Landing url
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.3   Oct 01 2009 10:53:54   pghumra
//Applied changes for cycle planner landing page, latitude longitude coordinates in landing page and find nearest car park functionality
//Resolution for 5316: CCN537 Cycle Planning Page Landing
//Resolution for 5317: CCNxxx Lat Long Coordinates in Page Landing
//
//   Rev 1.2   Mar 31 2008 12:59:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:24   mturner
//Initial revision.
//
//   Rev 1.10   Jun 02 2006 14:00:20   CRees
//Removed entry from GetLandingPageUrl that hard-coded english channel. 
//Resolution for 4110: Client links for Welsh journeys
//
//   Rev 1.9   Apr 21 2006 11:40:30   jbroome
//Updated for Find a Bus bookmark link
//Resolution for 3939: DN093 Find A Bus: Recalling bookmark lands user on door to door results page displaying train journeys
//
//   Rev 1.8   Mar 22 2006 17:29:56   halkatib
//Changes due to Merge of stream3152 Landing Page phase 3
//
//   Rev 1.7   Feb 24 2006 14:46:30   RWilby
//Fix for merge stream3129.
//
//   Rev 1.6   Feb 24 2006 12:26:06   RWilby
//Fix for merge stream3129. Added using reference to TransportDirect.Common.ResourceManager namespace.
//
//   Rev 1.5   Jan 18 2006 14:36:22   jbroome
//Incorrect month-year format for Landing Page
//
//   Rev 1.4   Dec 14 2005 16:39:06   jbroome
//Updated GetLandingPageUrl() so that Url always points to english channel
//Resolution for 3368: Client Links: server error when recalling a Welsh bookmark
//
//   Rev 1.3   Nov 30 2005 17:18:18   jbroome
//Added missing substitution code replacement.
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.2   Nov 29 2005 20:53:28   asinclair
//Added code for BusinessLinks page
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.1   Nov 23 2005 18:13:58   jgeorge
//Use a different parameter code for hashed location data.
//Resolution for 3144: DEL 8 stream: Client Links Development
//
//   Rev 1.0   Nov 23 2005 11:18:06   jgeorge
//Initial revision.

using System;
using System.Web;
using System.Text;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;
using TransportDirect.CommonWeb.Helpers;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using System.Security.Cryptography;
using TD.ThemeInfrastructure;
using TransportDirect.UserPortal.CoordinateConvertorProvider;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using CCP = TransportDirect.UserPortal.CoordinateConvertorProvider.CoordinateConvertorWebService;
using System.Collections.Specialized;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Helper class for dealing with the landing page.
	/// </summary>
	public class LandingPageHelper
	{
		/// <summary>
		/// Default constructor. Does nothing.
		/// </summary>
		public LandingPageHelper()
		{	}

        public OSGridReference GetOSGRFromLatLong(LatitudeLongitude latlong)
        {
            CCP.OSGridReference ccposgridreference = new CCP.OSGridReference();
            OSGridReference osgridreference = new OSGridReference();
            try
            {
                ICoordinateConvertor coordinateConvertor = (ICoordinateConvertor)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoordinateConvertorFactory];

                CCP.LatitudeLongitude ccplatlong = new CCP.LatitudeLongitude();
                ccplatlong.Latitude = latlong.Latitude;
                ccplatlong.Longitude = latlong.Longitude;

                ccposgridreference = coordinateConvertor.GetOSGridReference(ccplatlong);
                osgridreference.Easting = ccposgridreference.Easting;
                osgridreference.Northing = ccposgridreference.Northing;
                
            }
            catch (Exception ex)
            {
                string message = "Populating the LatitudeLongitude coordinates using the CoordinateConvertor threw an exception: " + ex.Message;

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message, ex));
            }
            return osgridreference;
        }

		/// <summary>
		/// Method returns string of the HTML that creates 
		/// the associated template. The location object 
		/// is used in creating the HTML, along with
		/// other substituted values.
		/// </summary>
		/// <param name="location">Location object to use in HTML</param>
		/// <param name="templateType">Template object</param>
		/// <returns>HTML template string</returns>
		public string GetBusinessLinksHtml( TDLocation location, BusinessLinkTemplate templateType )
		{
			string locationType = string.Empty;
			string locationText = string.Empty;
			string locationData = string.Empty;

			GetLocationData(location, ref locationType, ref locationData, ref locationText);

			// Retreive the Business Link partner ID
			string partnerId = Properties.Current["BusinessLinks.PartnerId"];

			// Get base HTML from template object
			string journeyPlannerHTML = templateType.Html;

			// Replace the necessary substitution codes with the 'real' values
			journeyPlannerHTML = journeyPlannerHTML.Replace(LandingPageHelperConstants.TargetUrl, GetServerName());
            journeyPlannerHTML = journeyPlannerHTML.Replace(LandingPageHelperConstants.PartnerId, partnerId);
            journeyPlannerHTML = journeyPlannerHTML.Replace(LandingPageHelperConstants.DestinationName, location.Description);
            journeyPlannerHTML = journeyPlannerHTML.Replace(LandingPageHelperConstants.DestinationType, locationType);
            journeyPlannerHTML = journeyPlannerHTML.Replace(LandingPageHelperConstants.DestinationData, locationData);
            journeyPlannerHTML = journeyPlannerHTML.Replace(LandingPageHelperConstants.PartnerName, ThemeProvider.Instance.GetTheme().Name);

			// Replace month drop down values if necessary
            if (journeyPlannerHTML.IndexOf(LandingPageHelperConstants.MonthOptions) > 0)
			{
				// Get string of months, based on number of months
				int noOfMonths = Convert.ToInt32(Properties.Current["BusinessLinks.NoOfMonthOptions"], TDCultureInfo.CurrentUICulture.NumberFormat);
				string monthOptions = BuildMonthOptions(noOfMonths);
                journeyPlannerHTML = journeyPlannerHTML.Replace(LandingPageHelperConstants.MonthOptions, monthOptions);
			}
		
			return journeyPlannerHTML;

		}

		/// <summary>
		/// Method returns a string of HTML option 
		/// month values to go inside a drop down, 
		/// based on number of months
		/// </summary>
		/// <param name="noOfMonths">No. of month options to include</param>
		/// <returns>string of HTML options</returns>
		private string BuildMonthOptions(int noOfMonths)
		{
			const string tagOptionOpen = "<option {0} value=\"{1}\">";
			const string tagOptionClose = "</option>";

			StringBuilder optionsSB = new StringBuilder(200);

			// Add an option for each month
            for (int i=0; i<noOfMonths; i++)
			{
				StringBuilder sb = new StringBuilder(100);
				DateTime dt = new TDDateTime().GetDateTime().AddMonths(i);

				string optionText = dt.ToString( "MMM yyyy" );
				string optionValue = dt.ToString( "MMyyyy" );
				string selected = (i==0) ? "selected=\"selected\"" : string.Empty;

				// Build option string
				sb.Append(string.Format(TDCultureInfo.CurrentUICulture, tagOptionOpen, selected, optionValue));
				sb.Append(optionText);
				sb.Append(tagOptionClose);

				// Add to options collection string
				optionsSB.Append(sb.ToString());
			}

			return optionsSB.ToString();
   		}

		/// <summary>
		/// Builds the encrypted string for use with the landing page.
		/// </summary>
		/// <param name="partnerId">Partner Id to include in the encrypted string.</param>
		/// <param name="locationData">Location data to include in the encrypted string.</param>
		/// <param name="isOrigin">True if the locationData relates to the origin location, false if it relates to the destination.</param>
		/// <returns>The encrypted string for use with the enc parameter in the landing page URL.</returns>
		/// <remarks>It will normally be necessary to UrlEncode the string before using as part of a URL querystring.
		/// Note also that the location data is UrlEncoded before it is encrypted.</remarks>
		public string BuildEncryptedString(string partnerId, string locationData, bool isOrigin)
		{
			StringBuilder builder = new StringBuilder(100);

            builder.Append(LandingPageHelperConstants.ParameterPartnerId);
            builder.Append(LandingPageHelperConstants.EncryptedValueSeparator);
			builder.Append(partnerId);
			builder.Append("&");
			
			if (isOrigin)
                builder.Append(LandingPageHelperConstants.ParameterHashedOriginData);
			else
                builder.Append(LandingPageHelperConstants.ParameterHashedDestinationData);

            builder.Append(LandingPageHelperConstants.EncryptedValueSeparator);

			builder.Append(HttpUtility.UrlEncode(GetHash(locationData)));

			ITDCrypt crypto = (ITDCrypt)TDServiceDiscovery.Current[ ServiceDiscoveryKey.Crypto ];
			return crypto.AsymmetricEncrypt(builder.ToString());
		}

		/// <summary>
		/// Retrieves location data for use in a landing page URL, using the appropriate part of the supplied location.
		/// </summary>
		/// <param name="location">The location object containing data.</param>
		/// <param name="locationType">The value for use in the URL as the oo or do parameter.</param>
		/// <param name="locationData">The value for use in the URL as the o or d parameter.</param>
		/// <param name="locationText">The value for use in the URL as the on or dn parameter.</param>
		/// <remarks>All values should be UrlEncoded before they are added to a Url.</remarks>
		public void GetLocationData(TDLocation location, ref string locationType, ref string locationData, ref string locationText)
		{
			// If there are NaPTANS use them, otherwise use OSGR.
            if (!HasNaPTAN(location))
				GetOSGRLocationData(location, ref locationType, ref locationData, ref locationText);
			else
				GetNaPTANLocationData(location, ref locationType, ref locationData, ref locationText);
		}

        /// <summary>
        /// Returns true if valid NaPTANs exist
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private bool HasNaPTAN(TDLocation location)
        {
            bool naptanExists = false;

            if ((location != null) && (location.NaPTANs != null) && (location.NaPTANs.Length > 0))
            {
                // Check if an artificial NaPTAN exists (will be prefixed with "Origin" or "Destination"), 
                // because cannot create a NaPTAN based URL for non-existent NaPTANs
                if ((location.NaPTANs[0].Naptan != null) && (!string.IsNullOrEmpty(location.NaPTANs[0].Naptan)))
                {
                    if (!location.NaPTANs[0].Naptan.StartsWith(LandingPageHelperConstants.originNaptanString) && !location.NaPTANs[0].Naptan.StartsWith(LandingPageHelperConstants.destinationNaptanString))
                    {
                        naptanExists = true;
                    }
                }
            }

            return naptanExists;
        }

		/// <summary>
		/// Retrieves location data for use in a landing page URL, using the OSGR from the supplied location.
		/// </summary>
		/// <param name="location">The location object containing data.</param>
		/// <param name="locationType">The value for use in the URL as the oo or do parameter.</param>
		/// <param name="locationData">The value for use in the URL as the o or d parameter.</param>
		/// <param name="locationText">The value for use in the URL as the on or dn parameter.</param>
		private void GetOSGRLocationData(TDLocation location, ref string locationType, ref string locationData, ref string locationText)
		{
            locationType = LandingPageHelperConstants.ValueLocationTypeOSGR;
			locationText = location.Description;
			locationData = location.GridReference.Easting.ToString() + "," + location.GridReference.Northing.ToString();    
		}

		/// <summary>
		/// Retrieves location data for use in a landing page URL, using the naptans from the supplied location.
		/// </summary>
		/// <param name="location">The location object containing data.</param>
		/// <param name="locationType">The value for use in the URL as the oo or do parameter.</param>
		/// <param name="locationData">The value for use in the URL as the o or d parameter.</param>
		/// <param name="locationText">The value for use in the URL as the on or dn parameter.</param>
		private void GetNaPTANLocationData(TDLocation location, ref string locationType, ref string locationData, ref string locationText)
		{
            locationType = LandingPageHelperConstants.ValueLocationTypeNaPTAN;
			locationText = location.Description;
			string[] naptans = location.GetNaPTANIds();
			locationData = string.Join(",", naptans);
		}

		/// <summary>
		/// Calculates a hash value for the supplied string.
		/// </summary>
		/// <param name="data">The string to hash</param>
		/// <returns>String containing the hash code for the input string.</returns>
		/// <remarks>The string will be hashed with the default implementation of the MD5 algorithm.</remarks>
		public string GetHash(string data)
		{
			byte[] byteData = ASCIIEncoding.UTF8.GetBytes(data);
			MD5 hashAlgorithm = MD5.Create();
			byte[] resultData = hashAlgorithm.ComputeHash(byteData);
			return ASCIIEncoding.UTF8.GetString(resultData);
		}

		/// <summary>
		/// Method returns string of URL for specified 
		/// landing page based on current server
		/// e.g. http://www.transportdirect.info/transportdirect/en/journeyplanning/jplandingpage.aspx
		/// </summary>
		/// <param name="pageid">Page ID of Landing Page</param>
		/// <returns>URL string</returns>
		public string GetLandingPageUrl(PageId pageid)
		{
			// First step is to resolve the Url for the JP Landing page.
			IPageController controller = (IPageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			PageTransferDetails jpLandingPageTransferDetails = controller.GetPageTransferDetails(pageid);

			// Add the resolved Url to a string builder
			StringBuilder sb = new StringBuilder(GetServerName());
            sb.Append(TDPage.getBookmarkBaseChannelURL(TDPage.SessionChannelName));
			sb.Append(jpLandingPageTransferDetails.PageUrl);
			
			string targetURL = sb.ToString();
			
			// IR4110 - removed hack to prevent welsh channel from being used. 
			return targetURL;
			// end IR4110
		}

		/// <summary>
		/// Method returns string representing server name of site
		/// e.g. http://www.transportdirect.info
		/// </summary>
		/// <returns>ServerName URL string</returns>
		private string GetServerName()
		{
			return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
		}

		/// <summary>
		/// Method resets all session parameters set by the landing page. 
		/// </summary>
		public void ResetLandingPageSessionParameters()
		{
			ITDSessionManager currentSessionManager = TDSessionManager.Current;		
			
			currentSessionManager.Session[ SessionKey.LandingPageCheck ] = false;
			currentSessionManager.Session[ SessionKey.LandingPageAutoPlan ] = false;
			currentSessionManager.Session[ SessionKey.LandingPageOriginInputType ] = string.Empty;
			currentSessionManager.Session[ SessionKey.LandingPageDestinationInputType ] = string.Empty;
			currentSessionManager.Session[ SessionKey.LandingPageBothDataNotNull ] = false;			
		}

        /// <summary>
        /// Builds a the query string for a landing page for the params supplied, checks for known
        /// landing page params before adding.
        /// Can be used when redirecting a POST landing page request to a GET landing page handler, 
        /// e.g. redirecting to TDP Mobile 
        /// </summary>
        /// <param name="requestForm"></param>
        /// <returns></returns>
        public string BuildLandingPageQueryStringFromParams(NameValueCollection requestForm)
        {
            StringBuilder querystring = new StringBuilder();

            if (requestForm != null)
            {
                // Check each form value for known landing page keys and build query string
                foreach (string key in requestForm.Keys)
                {
                    switch (key)
                    {
                        case LandingPageHelperConstants.ParameterPartnerId:
                        case LandingPageHelperConstants.ParameterOriginType:
                        case LandingPageHelperConstants.ParameterHashedOriginData:
                        case LandingPageHelperConstants.ParameterOriginData:
                        case LandingPageHelperConstants.ParameterOriginText:
                        case LandingPageHelperConstants.ParameterDestinationType:
                        case LandingPageHelperConstants.ParameterHashedDestinationData:
                        case LandingPageHelperConstants.ParameterDestinationData:
                        case LandingPageHelperConstants.ParameterDestinationText:
                        case LandingPageHelperConstants.ParameterOutwardDate:
                        case LandingPageHelperConstants.ParameterOutwardTime:
                        case LandingPageHelperConstants.ParameterOutwardDepartArrive:
                        case LandingPageHelperConstants.ParameterReturnRequired:
                        case LandingPageHelperConstants.ParameterReturnDate:
                        case LandingPageHelperConstants.ParameterReturnTime:
                        case LandingPageHelperConstants.ParameterReturnDepartArrive:
                        case LandingPageHelperConstants.ParameterMode:
                        case LandingPageHelperConstants.ParameterCarDefault:
                        case LandingPageHelperConstants.ParameterAutoPlan:
                        case LandingPageHelperConstants.ParameterEncryptedData:
                        case LandingPageHelperConstants.ParameterExcludeModes:
                        case LandingPageHelperConstants.ParameterEntryType:
                        case LandingPageHelperConstants.ParameterFindNearestType:
                        case LandingPageHelperConstants.ParameterFindNearestPlace:
                        case LandingPageHelperConstants.ParameterFindNearestLocGaz:
                            if (querystring.Length > 0)
                                querystring.Append("&");

                            querystring.Append(string.Format("{0}={1}", key, requestForm[key]));
                            break;
                        default:
                            break;
                    }
                }
            }

            return querystring.ToString();
        }
	}
}
