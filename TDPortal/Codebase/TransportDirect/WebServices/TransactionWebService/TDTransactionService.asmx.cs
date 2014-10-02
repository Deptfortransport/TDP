// *********************************************** 
// NAME			: TDTransactionService.asmx.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Used to submit transactions to the 
// TD Portal.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/TransactionWebServices/TDTransactionService.asmx.cs-arc  $
//
//   Rev 1.5   Jan 25 2010 14:39:34   MTurner
//Updated so that Gaz lookups are performed for Coach only journeys even if IsTrunkRequest is set to true
//Resolution for 5382: KPI03 does not perform Gaz check
//
//   Rev 1.4   Mar 16 2009 12:24:08   build
//Automatically merged from branch for stream5215
//
//   Rev 1.3.1.7   Feb 24 2009 12:27:58   mturner
//Removed redundant if statement
//
//   Rev 1.3.1.6   Jan 30 2009 16:37:02   mturner
//Added code to pass sequence in for EES Synchronous Journeys.
//
//   Rev 1.3.1.5   Jan 26 2009 11:43:44   mturner
//Added logic to use DateTimes supplied by the TI for EES Journey Transactions
//
//   Rev 1.3.1.4   Jan 23 2009 10:35:10   mturner
//Added synchronousJourneyPlanner functionallity and made changes to RTTI and FInd Nearest to being them much more int line with EES.
//
//   Rev 1.3.1.3   Jan 20 2009 12:17:48   mturner
//Implemented logic for CityToCity transaction
//
//   Rev 1.3.1.2   Jan 19 2009 14:52:52   mturner
//Implemented logic for the EESFindNearest transaction
//
//   Rev 1.3.1.1   Jan 16 2009 16:58:44   mturner
//Implemented TravelNewsTransaction logic
//
//   Rev 1.3.1.0   Jan 14 2009 17:47:02   mturner
//Tech refresh updates
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.3   Oct 27 2008 14:06:02   mturner
//Removed xslt transform from method call for CyclePlanning.  This is becuase the underlying code has been changed to not perform this transform when journey requests are actually injected transactions.
//
//   Rev 1.2   Oct 24 2008 15:35:20   mturner
//Changes for cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Oct 14 2008 11:10:28   build
//Merge for stream 5014
//
//   Rev 1.0.1.1   Sep 09 2008 10:59:20   mturner
//Change call to the CallCyclePlanner method as the method signature was changed.
//
//   Rev 1.0.1.0   Aug 04 2008 16:56:12   mturner
//Added support for Cycle Injections
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 13:55:26   mturner
//Initial revision.
//
//   Rev 1.33   Oct 16 2007 14:06:46   mmodi
//Amended to pass a request ID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.32   Feb 23 2006 19:17:36   build
//Automatically merged from branch for stream3129
//
//   Rev 1.31.1.0   Jan 24 2006 13:08:32   RWilby
//Updated RequestGisQuery method to perform a transaction againsted the GisQuery.FindExchangePointsInRadius method
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.31   Nov 09 2005 12:23:56   build
//Automatically merged from branch for stream2818
//
//   Rev 1.30.1.3   Nov 02 2005 16:46:24   RPhilpott
//Added CJSessionInfo to PricePricingUnit() method.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.30.1.2   Oct 29 2005 12:25:14   RPhilpott
//Define dummy Discounts for pricing call.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.30.1.1   Oct 29 2005 10:56:48   RPhilpott
//Use TimeBasedFareSupplier directly, instead of trying to go via TimeBasedPriceSupplierCaller ...
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.30.1.0   Oct 28 2005 18:29:20   RPhilpott
//Use TimeBasedPriceSupplier to price Itinerary.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.30   Sep 27 2005 17:57:04   kjosling
//Merged stream 2625 with trunk. 
//Resolution for 2625: DEL 8 Stream: TrainTaxi Amendments
//
//   Rev 1.29.1.1   Aug 25 2005 11:32:32   kjosling
//Updated RequestStationInfo method
//
//   Rev 1.29.1.0   Aug 15 2005 10:07:08   kjosling
//Updated RequestStationInfo web method create a StopTaxiInformation object. Resolution for 2625: DEL 8 Stream: TrainTaxi Amendments
//
//   Rev 1.29   Apr 09 2005 15:03:54   schand
//Added RequestRTTIInfo
//
//   Rev 1.28   Mar 01 2005 16:53:16   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.27   Nov 23 2004 16:14:58   rhopkins
//Change RequestJourneyUsingGazeteer so that it uses the correct gazeteer for the specified SearchType.
//
//   Rev 1.26   Nov 04 2004 11:10:04   JHaydock
//Performance enhancement - use of BLOBs for road route journeys
//
//   Rev 1.25   Sep 22 2004 10:12:30   COwczarek
//Update to CallCJP method call to pass new parameter to
//indicate whether request is for a journey extension.
//Resolution for 1263: Unhelpful user friendly error message for extend results
//
//   Rev 1.24   Jul 02 2004 13:42:46   jgeorge
//Changes for user type
//
//   Rev 1.23   Jun 10 2004 17:09:16   passuied
//Addition of 2 new WebMethods
//RequestJourneyUsingGazetteer
//RequestJourneySleep
//
//   Rev 1.22   Feb 09 2004 18:22:34   geaton
//IncidentID 637: Pass valid session id when making journey requests.
//
//   Rev 1.21   Jan 09 2004 09:51:42   PNorell
//Changed the picklist alternative meaning.
//
//   Rev 1.20   Jan 08 2004 19:42:28   PNorell
//Added new transactions to inject.
//
//   Rev 1.19   Nov 13 2003 11:01:52   geaton
//Added relative datetime support to pricing transactions.
//
//   Rev 1.18   Nov 13 2003 08:36:46   geaton
//Corrected pricing results output.
//
//   Rev 1.17   Nov 12 2003 20:27:50   geaton
//Pass pricing data as a string rather than complex type - this was not possible due to serialization issues with TD types.
//
//   Rev 1.16   Nov 12 2003 16:16:28   geaton
//Added web methods to support pricing and station info transactions.

using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using CJPI = TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.ReportDataProvider.TransactionHelper;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.TravelNewsInterface; 

using Logger = System.Diagnostics.Trace;
using TDPProperties = TransportDirect.Common.PropertyService.Properties;
using TDPEESFindNearestService = TransportDirect.ReportDataProvider.TransactionWebService.TDP.EES.FindNearestService;
using TDPEESJourneyPlannerSynchronousService = TransportDirect.ReportDataProvider.TransactionWebService.TDP.EES.JourneyPlannerSynchronousService;
using TravelNewsInterface = TransportDirect.UserPortal.TravelNewsInterface;
using CommonV1 = TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.FindNearest.V1;
using TransportDirect.ReportDataProvider.TransactionWebService.TDP.EES.DepartureBoardService;
using TransportDirect.ReportDataProvider.TransactionWebService.TDP.EES.FindNearestService;
using TransportDirect.ReportDataProvider.TransactionWebService.TDP.EES.JourneyPlannerSynchronousService;

namespace TransportDirect.ReportDataProvider.TransactionWebService
{
	/// <summary>
	/// Submits transactions of varying types to the Transport Direct Portal.
	/// </summary>
	[WebService(Namespace="urn:TransportDirect.ReportDataProvider.TransactionWebService")]
	public class TDTransactionService : System.Web.Services.WebService
    {
        #region Constructor/Dispose/Designer

        /// <summary>
		/// Default constructor.
		/// </summary>
		public TDTransactionService()
		{		
			InitializeComponent();
		}

		public new void Dispose()
		{
			GC.SuppressFinalize( this );
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

        #endregion

        /// <summary>
        /// Used by the Transaction Injector to test that the web service is running.
        /// </summary>
        /// <returns>True if the web service is running.</returns>
        [WebMethod]
        public bool TestActive()
        {
            return true;
        }

        /// <summary>
        /// Used to invoke a cycle journey request.
        /// </summary>
        /// <param name="requestData">
        /// Journey request data, as a string.
        /// </param>
        /// <param name="resultData">Result data.</param>
        /// <returns>
        /// Returns true if call to CJP returned the minimum expected results, 
        /// otherwise false.
        /// </returns>
        /// <remarks>
        /// 1) Request data is passed as a string rather than a class instance
        /// due to problems encountered when serialising complex TD types.
        /// 2) The session id passed in requestJourneyParams.sessionId is ***not*** used by the web method and is redundant - instead a valid session id is used (which is why this web method is session-enabled)
        /// </remarks>
        [WebMethod(EnableSession = true)]
        public bool RequestCycleJourney(string requestData, ref string resultData)
        {
            bool success = false;

            // Get a Cycle Planner Manager class to make request.
            ICyclePlannerManager cpMgr = (ICyclePlannerManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.CyclePlannerManager];
            
            // Call Cycle Planner.
            try
            {
                // Convert parameters to the format required by the CyclePlannerManager.
                RequestCycleJourneyParams requestCycleJourneyParams = RequestCycleJourneyParamsUtil.FromBase64Str(requestData);

                // Convert datetime to tddatetime (data transported as datetime due to serialisation problems with tddatetime)
                requestCycleJourneyParams.request.OutwardDateTime = new TDDateTime[1];
                requestCycleJourneyParams.request.OutwardDateTime[0] = new TDDateTime(requestCycleJourneyParams.dtOutwardDateTime);

                requestCycleJourneyParams.request.ReturnDateTime = new TDDateTime[1];
                requestCycleJourneyParams.request.ReturnDateTime[0] = new TDDateTime(requestCycleJourneyParams.dtReturnDateTime);

                // Call CJP.
                ITDCyclePlannerResult result = (TDCyclePlannerResult)cpMgr.CallCyclePlanner(requestCycleJourneyParams.request,requestCycleJourneyParams.sessionId,1,true,false,"en",string.Empty);

                // Determine if CJP has provided the minimum success result.
                if (result.OutwardCycleJourneyCount >= requestCycleJourneyParams.minNumberOutwardCycleJourneyCount
                    && result.ReturnCycleJourneyCount >= requestCycleJourneyParams.minNumberReturnCycleJourneyCount)
                {
                    success = true;
                }

                resultData = String.Format(Messages.CycleJourney_ResultData, requestCycleJourneyParams.sessionId, result.JourneyReferenceNumber, result.OutwardCycleJourneyCount, result.ReturnCycleJourneyCount);

            }
            catch (TDException tdException)
            {
                resultData = String.Format(Messages.CycleJourney_Failed, tdException.Message);
            }

            return success;
        }

		/// <summary>
		/// Used to invoke a journey request on the CJP calling the gazetteer/GIS beforehand.
		/// returned values from the gaz are not used for the journey request
		/// </summary>
		/// <param name="requestData">
		/// Journey request data, as a string.
		/// </param>
		/// <param name="resultData">Result data.</param>
		/// <returns>
		/// Returns true if call to CJP returned the minimum expected results, 
		/// otherwise false.
		/// </returns>
		/// <remarks>
		/// 1) Request data is passed as a string rather than a class instance
		/// due to problems encountered when serialising complex TD types.
		/// 2) The session id passed in requestJourneyParams.sessionId is ***not*** used by the web method and is redundant - instead a valid session id is used (which is why this web method is session-enabled)
		/// </remarks>
		[WebMethod (EnableSession=true)]
		public bool RequestJourney(string requestData, ref string resultData)
		{					
			bool success = false;

			// Get a CJP Manager class to make request.
			ICJPManager cjpMgr = (ICJPManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
	
			// Call CJP.
			try
			{
                // Convert parameters to the format required by the CJP Manager.
                RequestJourneyParams requestJourneyParams = RequestJourneyParamsUtil.FromBase64Str(requestData);

                if ((requestJourneyParams.request.IsTrunkRequest == false)
                    ||
                    ((requestJourneyParams.request.IsTrunkRequest == true) &&
                    (requestJourneyParams.request.Modes.Length == 1) &&
                    (requestJourneyParams.request.Modes[0] == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Coach))
                    )
                {
                    // Call Gaz and GISQuery
                    string resultGaz = string.Empty;
                    RequestGazetteerParams requestGazetteerParams = new RequestGazetteerParams();
                    requestGazetteerParams.fuzzy = false;
                    requestGazetteerParams.drilldown = false;
                    requestGazetteerParams.picklist = false;

                    requestGazetteerParams.searchText = requestJourneyParams.request.OriginLocation.Description;
                    requestGazetteerParams.searchType = requestJourneyParams.request.OriginLocation.SearchType;
                    RequestGazetteer(requestGazetteerParams, ref resultGaz);

                    requestGazetteerParams.searchText = requestJourneyParams.request.DestinationLocation.Description;
                    requestGazetteerParams.searchType = requestJourneyParams.request.DestinationLocation.SearchType;
                    RequestGazetteer(requestGazetteerParams, ref resultGaz);
                }
                else
                {
                    // No Gazeteer lookup required for City to City Journey or Air journeys.
                    // Both of these request types have TrunkRequest set to true.
                }
    			
			    // Convert datetime to tddatetime (data transported as datetime due to serialisation problems with tddatetime)
			    requestJourneyParams.request.OutwardDateTime = new TDDateTime[1];			
			    requestJourneyParams.request.OutwardDateTime[0] = new TDDateTime(requestJourneyParams.dtOutwardDateTime);				
    				
			    requestJourneyParams.request.ReturnDateTime  = new TDDateTime[1];			
			    requestJourneyParams.request.ReturnDateTime[0]  = new TDDateTime(requestJourneyParams.dtReturnDateTime);			

			    // Hardcode ExtraCheckinTime to be zero
			    requestJourneyParams.request.ExtraCheckinTime = new TDDateTime(1,1,1,0,0,0);

		    	// Call CJP.
			    ITDJourneyResult result = (TDJourneyResult)cjpMgr.CallCJP(requestJourneyParams.request, HttpContext.Current.Session.SessionID, 0, true, false, "en", false);

                if (requestJourneyParams.request.IsTrunkRequest == true && requestJourneyParams.request.Modes.Length > 1)
                {
                    bool railFound = false;
                    bool coachFound = false;

                    // CityToCity specific proccessing to check we get at least one train and one Coach result
                    TransportDirect.UserPortal.JourneyControl.PublicJourney[] pjArray = (TransportDirect.UserPortal.JourneyControl.PublicJourney[])result.OutwardPublicJourneys.ToArray(typeof (TransportDirect.UserPortal.JourneyControl.PublicJourney));
                    foreach (TransportDirect.UserPortal.JourneyControl.PublicJourney pj in pjArray)
                    {
                        if (pj.GetJourneyModeType() == CJPI.ModeType.Rail)
                        {
                            railFound = true;
                        }
                        else
                        {
                            if (pj.GetJourneyModeType() == CJPI.ModeType.Coach)
                            {
                                coachFound = true;
                            }
                        }
                    }
                    // Determine if CJP has provided the minimum success result.
                    success = (result.OutwardRoadJourneyCount >= requestJourneyParams.minNumberOutwardRoadJourneyCount) &&
                              (result.ReturnRoadJourneyCount >= requestJourneyParams.minNumberReturnRoadJourneyCount) &&
                              (result.OutwardPublicJourneyCount >= requestJourneyParams.minNumberOutwardPublicJourneyCount) &&
                              (result.ReturnPublicJourneyCount >= requestJourneyParams.minNumberReturnPublicJourneyCount) &&
                              railFound && coachFound;

                    resultData = String.Format(Messages.CtCJourney_ResultData, requestJourneyParams.sessionId, result.JourneyReferenceNumber, result.OutwardRoadJourneyCount, result.ReturnRoadJourneyCount, result.OutwardPublicJourneyCount, result.ReturnPublicJourneyCount, railFound, coachFound);
                }
                else
                {
                    // Determine if CJP has provided the minimum success result.
                    success = (result.OutwardRoadJourneyCount >= requestJourneyParams.minNumberOutwardRoadJourneyCount) &&
                              (result.ReturnRoadJourneyCount >= requestJourneyParams.minNumberReturnRoadJourneyCount) &&
                              (result.OutwardPublicJourneyCount >= requestJourneyParams.minNumberOutwardPublicJourneyCount) &&
                              (result.ReturnPublicJourneyCount >= requestJourneyParams.minNumberReturnPublicJourneyCount);

                    resultData = String.Format(Messages.Journey_ResultData, requestJourneyParams.sessionId, result.JourneyReferenceNumber, result.OutwardRoadJourneyCount, result.ReturnRoadJourneyCount, result.OutwardPublicJourneyCount, result.ReturnPublicJourneyCount);
                }
			}
            catch (TDException tdException)
			{
				resultData = String.Format(Messages.Journey_Failed, tdException.Message);
			}

			return success;
		}

		/// <summary>
		/// Request to have a map generated
		/// </summary>
		/// <param name="requestData">Request data used to invoke transaction.</param>
		/// <param name="resultData">Result data.</param>
		/// <returns>True on success, false on failure. </returns>
		[WebMethod]
		public bool RequestMap(RequestMapParams requestData , ref string resultData)
		{			
			bool success = false;
			try
			{
				char[] sep = { '|' };
				Random r = new Random();

				// Different types of map
				// 1 - Zoom to initial
				// 2 - Zoom to envelope
				// 3 - Add RD route and zoom to route


				Map map = new Map();
				// Give dummy size of map so the generated image is within bounds
				map.Height = new Unit(498);
				map.Width = new Unit(498);

                map.ServerName = TDPProperties.Properties.Current["InteractiveMapping.Map.ServerName"];
                map.ServiceName = TDPProperties.Properties.Current["InteractiveMapping.Map.ServiceName"];

				if( requestData.type == "ZoomFull" )
				{
					map.ZoomFull(); 
					success = true;
				}
				else if( requestData.type == "ZoomToPoint" )
				{
					// array of easting/northing/scale -> choose at random
					string[] northings = requestData.northing.Split(sep);
					string[] eastings = requestData.easting.Split(sep);
					string[] scales = requestData.scale.Split(sep);

					// Decide on indexes
					int pointIndex = r.Next(northings.Length);
					int scaleIndex = r.Next( scales.Length );

					// turn into doubles/ints 
					int north = Convert.ToInt32( northings[pointIndex] );
					int east = Convert.ToInt32( eastings[pointIndex] );
					int scale = Convert.ToInt32( scales[scaleIndex] );

					if( TDTraceSwitch.TraceVerbose )
					{
						Logger.Write( new OperationalEvent( TDEventCategory.Business, TDTraceLevel.Verbose, "Northing[" + north + "]: Easting[" + east + "]: Scale["+ scale +"]" ) );
					}
					// generate map
					map.ZoomToPoint( east , north , scale );
					success = true;
				}
				else if( requestData.type == "ZoomRoadRoute" )
				{
					string[] sessions = requestData.session.Split(sep);
					string session = sessions[r.Next(sessions.Length)];
					//Always use route number 1

					//Expand the routes for use
					SqlHelper sqlHelper = new SqlHelper();
					sqlHelper.ConnOpen(SqlHelperDatabase.EsriDB);
					Hashtable htParameters = new Hashtable(2);
					htParameters.Add("@SessionID", session);
					htParameters.Add("@RouteNum", 1);
					sqlHelper.Execute("usp_ExpandRoutes", htParameters);

					//Add and zoom to the route
					map.AddRoadRoute(session, 1);
					map.ZoomRoadRoute(session, 1);
					success = true;
				}
			}
			catch( Exception e )
			{
                resultData = String.Format(Messages.Map_Failed, e.Message);
			}
			return success;			
		}

		/// <summary>
		/// Request to have a gazetteer lookup certain adresses and naptans
		/// </summary>
		/// <param name="requestData">Request data used to invoke transaction.</param>
		/// <param name="resultData">Result data.</param>
		/// <returns>True on success, false on failure. </returns>
		[WebMethod]
		public bool RequestGazetteer( RequestGazetteerParams requestData , ref string resultData)
		{			
			bool success = false;

			// Different types of gazetter searches
			// 1 - All other gazetteer with given x search text (including wildcard and/or fuzzy) getting details
			// 2 - All other gazetteer (including wildcard and/or fuzzy) picklist and drilldown
	
			try
			{
				if( TDTraceSwitch.TraceVerbose )
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business, TDTraceLevel.Verbose, "SearchType[" + requestData.searchType + "]:SearchText[" + requestData.searchText + "]:Fuzzy["+ requestData.fuzzy +"]: Picklist["+requestData.picklist+"]" ) );
				}

				// Supports option
				LocationSearch ls = new LocationSearch();
				LocationChoiceList lcl = ls.StartSearch(requestData.searchText, requestData.searchType, requestData.fuzzy, 1000, "dummy", false );


				if( requestData.picklist && lcl.Count > 0 ) 
				{
					// This is a success - no reason to continue or drill down.					
					return true;
				}

				foreach( LocationChoice lc in lcl )
				{
					if( !lc.HasChilden && !requestData.drilldown )
					{
						
						// Found exact match -> retreive location details
						TDLocation loc = new TDLocation();

						ls.GetLocationDetails( ref loc, lc);
						success = loc.Status == TDLocationStatus.Valid;	
						break;
					}
					else if( lc.HasChilden && requestData.drilldown )
					{
						// Drill down exercise - successfull
						LocationChoiceList lcl2 = ls.DrillDown( 0, lc );
						success = lcl2 != null;
						break;
					}
				}
				if( !success )
				{
					resultData = "Found no picklist item alternative with "+ (requestData.drilldown ? "":"no ") + "children (should picklist be reversed)";
				}
			}
            catch (Exception e)
            {
                resultData = String.Format(Messages.Gaz_Failed, e.Message);
            }
			return success;					
		}

		/// <summary>
		/// Requests RTTI info for the given origin and destination or service numvber.
		/// </summary>
		/// <param name="requestData">Request data used to invoke transaction.</param>
		/// <param name="resultData">Result data.</param>
		/// <returns>True on success, false on failure. </returns>
        [WebMethod (EnableSession=true)]
        public bool RequestEESInfo(RequestEESInfoParams requestData, ref string resultData)
        {
            bool success = false;
            // Need to check which EES method is being called

            switch (requestData.method)
            {
                case "DepartureBoard":
                    {
                        #region Departure Board

                        // This is an RTTI Departure Board Request
                        try
                        {
                            // Set retry to false to allow one attempt only, 
                            // it is set to true below if a retry is necessary
                            bool retry = false;
                            bool retried = false;

                            // Initial locations to use
                            string origin = requestData.origin;
                            string destination = requestData.destination;

                            do
                            {
                                if (TDTraceSwitch.TraceVerbose)
                                {
                                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                        string.Format("RequestEESInfo - {0}DepartureBoard request with origin[{1}] destination[{2}]",
                                        retry ? "Retrying " : string.Empty,
                                        origin, destination)));
                                }

                                // Set up web service connection
                                DepartureBoardService DBS = new DepartureBoardService();
                                DBS.Url = TransportDirect.Common.PropertyService.Properties.Properties.Current["TransactionWebService.Services.DepartureBoardService.URL"];

                                #region Origin location

                                // Create a DBS location for our origin and populate from the data supplied by the TI
                                DepartureBoardServiceLocation originLocation = new DepartureBoardServiceLocation();
                                switch (requestData.searchType)
                                {
                                    case "NAPTAN":
                                        originLocation.Type = DepartureBoardServiceCodeType.NAPTAN;
                                        // As Location is a NapTAN use origin to populate the NapTANIds array instead of Code Property.
                                        originLocation.NaptanIds = new string[1] { origin };
                                        originLocation.Code = string.Empty;
                                        break;
                                    case "SMS":
                                        originLocation.Type = DepartureBoardServiceCodeType.SMS;
                                        originLocation.NaptanIds = new string[0];
                                        originLocation.Code = origin;
                                        break;
                                    case "Postcode":
                                        originLocation.Type = DepartureBoardServiceCodeType.Postcode;
                                        originLocation.NaptanIds = new string[0];
                                        originLocation.Code = origin;
                                        break;
                                    case "IATA":
                                        // IATA departure boards have never been implemented but included here for completeness
                                        originLocation.Type = DepartureBoardServiceCodeType.IATA;
                                        originLocation.NaptanIds = new string[0];
                                        originLocation.Code = origin;
                                        break;
                                    case "CRS":
                                    default:
                                        originLocation.Type = DepartureBoardServiceCodeType.CRS;
                                        originLocation.NaptanIds = new string[0];
                                        originLocation.Code = origin;
                                        break;
                                }
                                originLocation.Locality = string.Empty;
                                originLocation.Valid = true;

                                #endregion

                                #region Destination location

                                // Create a DBS location for our destination and populate from the data supplied by the TI
                                DepartureBoardServiceLocation destinationLocation = new DepartureBoardServiceLocation();
                                switch (requestData.searchType)
                                {
                                    case "NAPTAN":
                                        destinationLocation.Type = DepartureBoardServiceCodeType.NAPTAN;
                                        // As Location is a NapTAN use destination to populate the NapTANIds array instead of Code Property.
                                        destinationLocation.NaptanIds = new string[1] { destination };
                                        destinationLocation.Code = string.Empty;
                                        break;
                                    case "SMS":
                                        destinationLocation.Type = DepartureBoardServiceCodeType.SMS;
                                        destinationLocation.NaptanIds = new string[0];
                                        destinationLocation.Code = destination;
                                        break;
                                    case "Postcode":
                                        destinationLocation.Type = DepartureBoardServiceCodeType.Postcode;
                                        destinationLocation.NaptanIds = new string[0];
                                        destinationLocation.Code = destination;
                                        break;
                                    case "IATA":
                                        // IATA departure boards have never been implemented but included here for completeness
                                        destinationLocation.Type = DepartureBoardServiceCodeType.IATA;
                                        destinationLocation.NaptanIds = new string[0];
                                        destinationLocation.Code = destination;
                                        break;
                                    case "CRS":
                                    default:
                                        destinationLocation.Type = DepartureBoardServiceCodeType.CRS;
                                        destinationLocation.NaptanIds = new string[0];
                                        destinationLocation.Code = destination;
                                        break;
                                }
                                destinationLocation.Locality = string.Empty;
                                destinationLocation.Valid = true;

                                #endregion

                                #region Time

                                // Set the time request type to control what type of search we will perform
                                DepartureBoardServiceTimeRequest time = new DepartureBoardServiceTimeRequest();
                                switch (requestData.day)
                                {
                                    case "FirstToday":
                                        time.Type = DepartureBoardServiceTimeRequestType.FirstToday;
                                        break;
                                    case "FirstTomorrow":
                                        time.Type = DepartureBoardServiceTimeRequestType.FirstTomorrow;
                                        break;
                                    case "LastToday":
                                        time.Type = DepartureBoardServiceTimeRequestType.LastToday;
                                        break;
                                    case "LastTomorrow":
                                        time.Type = DepartureBoardServiceTimeRequestType.LastTomorrow;
                                        break;
                                    case "TimeToday":
                                        time.Type = DepartureBoardServiceTimeRequestType.TimeToday;
                                        break;
                                    case "TimeTomorrow":
                                        time.Type = DepartureBoardServiceTimeRequestType.TimeTomorrow;
                                        break;
                                    case "First":
                                        time.Type = DepartureBoardServiceTimeRequestType.First;
                                        break;
                                    case "Last":
                                        time.Type = DepartureBoardServiceTimeRequestType.Last;
                                        break;
                                    case "Now":
                                        time.Type = DepartureBoardServiceTimeRequestType.Now;
                                        break;
                                    default:
                                        time.Type = DepartureBoardServiceTimeRequestType.FirstTomorrow;
                                        break;
                                }

                                #endregion

                                DepartureBoardServiceRequest DBR = new DepartureBoardServiceRequest();
                                DBR.OriginLocation = originLocation;
                                DBR.DestinationLocation = destinationLocation;
                                DBR.JourneyTimeInformation = time;
                                if (requestData.serviceNumber != null) DBR.ServiceNumber = requestData.serviceNumber;
                                DBR.ShowDepartures = requestData.showDepartures;
                                DBR.ShowCallingStops = requestData.showCallingPoints;
                                DBR.RangeType = DepartureBoardServiceRangeType.Sequence; // Sequence means ask for 'n' results rather than all results within a time span
                                DBR.Range = 1; //Number of results we are asking for.  As only checking whether service is working 1 will do 

                                DepartureBoardServiceStopInformation[] results = new DepartureBoardServiceStopInformation[] { };

                                // Send the Departure board request
                                results = DBS.GetDepartureBoard("TransactionInjector", "en-GB", DBR);

                                if (results.Length > 0)
                                {
                                    success = true;
                                    if (results[0].Stop.Stop.Name.ToString() == null)
                                        resultData = String.Format(Messages.EESRTTI_ResultData, results.Length, string.Empty);
                                    else
                                        resultData = String.Format(Messages.EESRTTI_ResultData, results.Length, results[0].Stop.Stop.Name.ToString());

                                    // Ensure retry is false to allow exit from loop if a retry occurred
                                    retry = false;
                                }
                                else
                                {
                                    success = false;
                                    resultData = String.Format(Messages.EESRTTI_Failed, "No services were returned");

                                    #region Check for retry using alternative locations

                                    if (retried)
                                    {
                                        // Do not retry again, as already used alternative locations
                                        retry = false;
                                    }
                                    // If no results are found, then check if retry is required,
                                    // true when alternative locations supplied
                                    else if (!retried
                                        && !string.IsNullOrEmpty(requestData.originAlternative)
                                        && !string.IsNullOrEmpty(requestData.destinationAlternative))
                                    {
                                        // Alternative origin and destination supplied, allow retry
                                        retry = true;

                                        // Only retry once
                                        retried = true;

                                        // Update to use the alternative locations in the retry attempt
                                        origin = requestData.originAlternative;
                                        destination = requestData.destinationAlternative;
                                    }

                                    #endregion
                                }

                            } while (retry); // If retry set, then attempt to submit request again (using updated locations)

                        }
                        catch (Exception exception)
                        {
                            success = false;
                            resultData = String.Format(Messages.EESRTTI_Failed, exception.Message);
                        }

                        #endregion
                    }
                    break;
                case "FindNearest":
                    {
                        #region Find Nearest

                        // We need to do a 'Find Nearest Stations' request
                        try
                        {
                            // Set up web service connection
                            FindNearest findNearest = new FindNearest();
                            findNearest.Url = TransportDirect.Common.PropertyService.Properties.Properties.Current["TransactionWebService.Services.FindNearestService.URL"];

                            // Find the OSGR for the supplied postcode
                            TDPEESFindNearestService.OSGridReference gridRef = findNearest.GetGridReference("TransactionInjector", "en-GB", requestData.origin);

                            // Request the nearest station to the OSGR we found
                            TDPEESFindNearestService.NaptanProximity[] resultsArray = findNearest.FindNearestStations("TransactionInjector", "en-GB", gridRef, 1);

                            if (resultsArray.Length > 0)
                            {
                                success = true;
                            }
                            else
                            {
                                success = false;
                                resultData = String.Format(Messages.EESFindNearest_Failed, "No stops were returned");
                            }
                        }
                        catch (Exception exception)
                        {
                            success = false;
                            resultData = String.Format(Messages.EESFindNearest_Failed, exception.Message);
                        }

                        #endregion
                    }
                    break;
                case "JourneyPlan":
                    {
                        #region Journey Plan

                        // We need to do a EES Synchronous Journey Plan request
                        try
                        {
                            TDPEESJourneyPlannerSynchronousService.JourneyPlannerSynchronousService JPSS = new TDPEESJourneyPlannerSynchronousService.JourneyPlannerSynchronousService();
                            JPSS.Url = TransportDirect.Common.PropertyService.Properties.Properties.Current["TransactionWebService.Services.JourneyPlannerSynchronousService.URL"];

                            TDPEESJourneyPlannerSynchronousService.PublicJourneyRequest PJRequest = new TDPEESJourneyPlannerSynchronousService.PublicJourneyRequest();
                            PJRequest.OriginLocation = new RequestLocation();
                            PJRequest.OriginLocation.Type = LocationType.Postcode;
                            PJRequest.OriginLocation.Postcode = requestData.origin;
                            PJRequest.DestinationLocation = new RequestLocation();
                            PJRequest.DestinationLocation.Type = LocationType.Postcode;
                            PJRequest.DestinationLocation.Postcode = requestData.destination;
                            PJRequest.IsReturnRequired = requestData.isReturnRequired;
                            PJRequest.OutwardArriveBefore = requestData.outwardArriveBefore;
                            PJRequest.ReturnArriveBefore = requestData.returnArriveBefore;
                            PJRequest.OutwardDateTime = new DateTime(requestData.outwardTime.Year, requestData.outwardTime.Month, requestData.outwardTime.Day, requestData.outwardTime.Hour, requestData.outwardTime.Minute, 0);
                            PJRequest.ReturnDateTime = new DateTime(requestData.returnTime.Year, requestData.returnTime.Month, requestData.returnTime.Day, requestData.returnTime.Hour, requestData.returnTime.Minute, 0);
                            PJRequest.Sequence = requestData.sequence;
                            PJRequest.WalkingSpeed = requestData.walkingSpeed;
                            PJRequest.InterchangeSpeed = requestData.interchangeSpeed;
                            PJRequest.MaxWalkingTime = requestData.maxWalkingTime;
                            PJRequest.Sequence = requestData.sequence;

                            PublicJourneyResult PJResult = JPSS.PlanPublicJourney("TransactionInjector", "en-GB", PJRequest);

                            if (PJResult.OutwardPublicJourneys.Length > 0)
                            {
                                if (PJRequest.IsReturnRequired)
                                {
                                    if (PJResult.ReturnPublicJourneys.Length > 0)
                                    {
                                        success = true;
                                    }
                                    else
                                    {
                                        success = false;
                                        resultData = String.Format(Messages.EESJourneyPlan_Failed, "No Return services returned");
                                    }
                                }
                                else
                                {
                                    success = true;
                                }
                            }
                            else
                            {
                                success = false;
                                resultData = String.Format(Messages.EESJourneyPlan_Failed, "No Outward services were returned");
                            }
                        }
                        catch (Exception exception)
                        {
                            success = false;
                            resultData = String.Format(Messages.EESJourneyPlan_Failed, exception.Message);
                        }

                        #endregion
                    }
                    break;
            }

            return success;
        }
	
        /// <summary>
		/// Requests Travel News info for the given region, modes and time.
		/// </summary>
		/// <param name="requestData">Request data used to invoke transaction.</param>
		/// <param name="resultData">Result data.</param>
		/// <returns>True on success, false on failure. </returns>
		[WebMethod]
		public bool RequestTravelNews(RequestTravelNewsParams requestData , ref string resultData)
		{
            bool success = false;

            try
            {
                TravelNewsState tnState = new TravelNewsState();

                // Set the the tnState values from the parameters passed in from the TI

                // Set Date to be today then add any days passed in in the Days attribute
                tnState.SelectedDate = new TDDateTime();
                tnState.SelectedDate = tnState.SelectedDate.AddDays(requestData.day);
                                
                tnState.SelectedRegion = requestData.region;

                switch (requestData.delays.Trim())
                {
                    case "Major":
                        tnState.SelectedDelays = DelayType.Major;
                        break;
                    case "All":
                        tnState.SelectedDelays = DelayType.All;
                        break;
                    case "Recent":
                        tnState.SelectedDelays = DelayType.Recent;
                        break;
                    default:
                        tnState.SelectedDelays = DelayType.All;
                        break;
                }

                switch (requestData.transport.Trim())
                {
                    case "All":
                        tnState.SelectedTransport = TravelNewsInterface.TransportType.All;
                        break;
                    case "Public":
                        tnState.SelectedTransport = TravelNewsInterface.TransportType.PublicTransport;
                        break;
                    case "Private":
                        tnState.SelectedTransport = TravelNewsInterface.TransportType.Road;
                        break;
                    default:
                        tnState.SelectedTransport = TravelNewsInterface.TransportType.All;
                        break;
                }

                switch (requestData.type.Trim())
                {
                    case "All":
                        tnState.SelectedIncidentType = IncidentType.All;
                        break;
                    case "Planned":
                        tnState.SelectedIncidentType = IncidentType.Planned;
                        break;
                    case "Unplanned":
                        tnState.SelectedIncidentType = IncidentType.Unplanned;
                        break;
                    default:
                        tnState.SelectedIncidentType = IncidentType.All;
                        break;
                }

                // Set up a travelNewsHandler and then call the GetDetails method to retrieve incidents
                // that match the filters specified in tnState.
                ITravelNewsHandler travelNewsHandler = (ITravelNewsHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.TravelNews];
                TravelNewsItem[] tnArray = travelNewsHandler.GetDetails(tnState);

                if (tnArray.Length > 0)
                {
                    // We have some incidents so everything must have worked
                    success = true;
                }
            }
            catch (Exception exception)
            {
                success = false;
                resultData = String.Format(Messages.TravelNews_Failed, exception.Message);
            }

            return success;
		}
	}	
}
