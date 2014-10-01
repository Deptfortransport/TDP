// *********************************************** 
// NAME             : JourneyLocationsAdapter.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 17 May 2011
// DESCRIPTION  	: JourneyLocationsAdapter class providing helper methods for Journey Locations intermediate page
// ************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using TDP.Common;
using TDP.Common.LocationService;
using TDP.Common.ServiceDiscovery;
using TDP.Common.Web;
using TDP.UserPortal.JourneyControl;

namespace TDP.UserPortal.TDPWeb.Adapters
{
    /// <summary>
    /// JourneyLocationsAdapter class providing helper methods for Journey Locations intermediate page
    /// </summary>
    public class JourneyLocationsAdapter
    {
        #region Private members

        private SessionHelper sessionHelper;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public JourneyLocationsAdapter()
        {
            sessionHelper = new SessionHelper();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validates the parameters and populates a StopEvent TDPJourneyRequest object using 
        /// the existing TDPJourneyRequest in session, and adds to the session
        /// </summary>
        /// <returns>True if stop event request built and added to session</returns>
        public bool ValidateAndBuildTDPStopEventRequest(string venuePierNaPTAN, string remotePierNaPTAN)
        {
            bool valid = false;

            // Assume existing journey request to base the stop event request on is already valid
            if (!string.IsNullOrEmpty(venuePierNaPTAN) && !string.IsNullOrEmpty(remotePierNaPTAN))
            {
                // Retrieve the journey request 
                ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();

                // Build the stop event request to submit using the journey request
                ITDPJourneyRequest tdpStopEventRequest = BuildTDPStopEventRequest(tdpJourneyRequest, venuePierNaPTAN, remotePierNaPTAN);

                if (tdpStopEventRequest != null)
                {
                    // Commit stop event request to session
                    sessionHelper.UpdateSessionStopEvent(tdpStopEventRequest);

                    valid = true;
                }
            }

            return valid;
        }

        /// <summary>
        /// Populates a new StopEvent TDPJourneyRequest object using the supplied StopEvent TDPJourneyRequest, 
        /// with the supplied parameters, and adds to the session
        /// </summary>
        /// <returns></returns>
        public bool ValidateAndBuildTDPStopEventRequestForReplan(ITDPJourneyRequest tdpStopEventRequest,
            bool replanOutwardRequired, bool replanReturnRequired,
            DateTime replanOutwardDateTime, DateTime replanReturnDateTime,
            List<Journey> outwardJourneys, List<Journey> returnJourneys)
        {
            bool valid = false;

            if (tdpStopEventRequest != null)
            {
                // Create a new stop event request. 
                // Must create a new request as it's hash will be different, and should allow the 
                // "browser back" to still function, and support multi-tabbing (assumption!)
                ITDPJourneyRequest tdpJourneyRequestReplan = BuildTDPStopEventRequestForReplan(tdpStopEventRequest,
                    replanOutwardRequired, replanReturnRequired,
                    replanOutwardDateTime, replanReturnDateTime,
                    outwardJourneys, returnJourneys);

                // Commit stop event request to session
                sessionHelper.UpdateSessionStopEvent(tdpJourneyRequestReplan);

                valid = true;
            }

            return valid;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds the stop event request using journey request and selecter river service route
        /// </summary>
        /// <param name="venuePierNaPTAN">NaPTAN of venue pier</param>
        /// <param name="remotePierNaPTAN">NaPTAN of remote pier</param>
        /// <returns>TDPJourneyRequest object</returns>
        private ITDPJourneyRequest BuildTDPStopEventRequest(ITDPJourneyRequest tdpJourneyRequest, string venuePierNaPTAN, string remotePierNaPTAN)
        {
            if (tdpJourneyRequest != null)
            {
                TDPLocation venue = null;
                bool isOriginVenue = false;

                if (tdpJourneyRequest.Destination is TDPVenueLocation)
                {
                    venue = tdpJourneyRequest.Destination;
                    isOriginVenue = false;
                }
                else if (tdpJourneyRequest.Origin is TDPVenueLocation)
                {
                    venue = tdpJourneyRequest.Origin;
                    isOriginVenue = true;
                }

                if (venue != null)
                {
                    List<TDPVenueRiverService> riverServiceList;

                    // Get River Services
                    LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

                    // River services list
                    riverServiceList = locationService.GetTDPVenueRiverServices(venue.Naptan);

                    // Retrive the venue river service so it's details can be used for the locations in the request
                    TDPVenueRiverService venueRiverService = riverServiceList.Where(rs => rs.RemotePierNaPTAN == remotePierNaPTAN && rs.VenuePierNaPTAN == venuePierNaPTAN).FirstOrDefault();

                    if (venueRiverService != null)
                    {
                        ITDPJourneyRequest tdpStopEventRequest = new TDPJourneyRequest();

                        TDPLocation destination = null;
                        TDPLocation origin = null;

                        if (isOriginVenue)
                        {
                            // From Venue Pier to the Remote Pier
                            destination = new TDPLocation(venueRiverService.RemotePierName, TDPLocationType.Station, TDPLocationType.Unknown, venueRiverService.RemotePierNaPTAN);
                            origin = new TDPLocation(venueRiverService.VenuePierName, TDPLocationType.Station, TDPLocationType.Unknown, venueRiverService.VenuePierNaPTAN);
                            origin.Locality = venue.Locality; // Locality is needed by the CJP
                        }
                        else
                        {
                            // To Venue Pier from the Remote Pier
                            origin = new TDPLocation(venueRiverService.RemotePierName, TDPLocationType.Station, TDPLocationType.Unknown, venueRiverService.RemotePierNaPTAN);
                            destination = new TDPLocation(venueRiverService.VenuePierName, TDPLocationType.Station, TDPLocationType.Unknown, venueRiverService.VenuePierNaPTAN);
                            destination.Locality = venue.Locality; // Locality is needed by the CJP
                        }

                        tdpStopEventRequest.Origin = origin;
                        tdpStopEventRequest.Destination = destination;

                        // Adjust date times with Transit time to/from venue to venue pier
                        tdpStopEventRequest.OutwardDateTime = tdpJourneyRequest.OutwardDateTime.Subtract(GetTransitTime(venue, venuePierNaPTAN, true));
                        tdpStopEventRequest.OutwardArriveBefore = false; //tdpJourneyRequest.OutwardArriveBefore; Ignored for StopEvent requests
                        tdpStopEventRequest.ReturnDateTime = tdpJourneyRequest.ReturnDateTime.Add(GetTransitTime(venue, venuePierNaPTAN, false));
                        tdpStopEventRequest.ReturnArriveBefore = false; //tdpJourneyRequest.ReturnArriveBefore; Ignored for StopEvent requests
                        tdpStopEventRequest.IsOutwardRequired = tdpJourneyRequest.IsOutwardRequired;
                        tdpStopEventRequest.IsReturnRequired = tdpJourneyRequest.IsReturnRequired;
                        tdpStopEventRequest.IsReturnOnly = tdpJourneyRequest.IsReturnOnly;

                        tdpStopEventRequest.PlannerMode = tdpJourneyRequest.PlannerMode;

                        // Only in Ferry mode for the river stop event request
                        tdpStopEventRequest.Modes = new List<TDPModeType>() { TDPModeType.Ferry };

                        // All request values have been set, now update the journey request hash.
                        // This determines the uniqueness of this journey request for this users session
                        tdpStopEventRequest.JourneyRequestHash = tdpStopEventRequest.GetTDPHashCode().ToString();

                        return tdpStopEventRequest;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Populates a new Stop Event TDPJourneyRequest object using the supplied Stop Event TDPJourneyRequest, 
        /// with the supplied parameters
        /// </summary>
        private ITDPJourneyRequest BuildTDPStopEventRequestForReplan(ITDPJourneyRequest tdpStopEventRequest,
            bool replanOutwardRequired, bool replanReturnRequired,
            DateTime replanOutwardDateTime, DateTime replanReturnDateTime,
            List<Journey> outwardJourneys, List<Journey> returnJourneys)
        {
            if (tdpStopEventRequest != null)
            {
                ITDPJourneyRequest tdpStopEventRequestReplan = new TDPJourneyRequest();

                tdpStopEventRequestReplan.Origin = (TDPLocation)tdpStopEventRequest.Origin.Clone();
                tdpStopEventRequestReplan.Destination = (TDPLocation)tdpStopEventRequest.Destination.Clone();

                tdpStopEventRequestReplan.PlannerMode = tdpStopEventRequest.PlannerMode;
                
                // Only in Ferry mode for the river stop event request
                tdpStopEventRequestReplan.Modes = new List<TDPModeType>() { TDPModeType.Ferry };
                                
                tdpStopEventRequestReplan.OutwardDateTime = tdpStopEventRequest.OutwardDateTime;
                tdpStopEventRequestReplan.OutwardArriveBefore = tdpStopEventRequest.OutwardArriveBefore;
                tdpStopEventRequestReplan.ReturnDateTime = tdpStopEventRequest.ReturnDateTime;
                tdpStopEventRequestReplan.ReturnArriveBefore = tdpStopEventRequest.ReturnArriveBefore;
                tdpStopEventRequestReplan.IsOutwardRequired = tdpStopEventRequest.IsOutwardRequired;
                tdpStopEventRequestReplan.IsReturnRequired = tdpStopEventRequest.IsReturnRequired;
                tdpStopEventRequestReplan.IsReturnOnly = tdpStopEventRequest.IsReturnOnly;
                
                // Add the replan values
                tdpStopEventRequestReplan.IsReplan = true;
                tdpStopEventRequestReplan.ReplanIsOutwardRequired = replanOutwardRequired;
                tdpStopEventRequestReplan.ReplanIsReturnRequired = replanReturnRequired;
                tdpStopEventRequestReplan.ReplanOutwardDateTime = replanOutwardDateTime;
                tdpStopEventRequestReplan.ReplanReturnDateTime = replanReturnDateTime;
                tdpStopEventRequestReplan.ReplanOutwardJourneys = outwardJourneys;
                tdpStopEventRequestReplan.ReplanReturnJourneys = returnJourneys;

                if (!replanOutwardRequired && outwardJourneys.Count != 0)
                {
                    tdpStopEventRequestReplan.ReplanRetainOutwardJourneys = true;
                }
                if (!replanReturnRequired && returnJourneys.Count != 0)
                {
                    tdpStopEventRequestReplan.ReplanRetainReturnJourneys = true;
                }
                
                // All request values have been set, now update the journey request hash.
                // This determines the uniqueness of this journey request for this users session
                tdpStopEventRequestReplan.JourneyRequestHash = tdpStopEventRequestReplan.GetTDPHashCode().ToString();

                return tdpStopEventRequestReplan;
            }

            return null;
        }

        /// <summary>
        /// Returns the total transit time between the Venue pier and Venue
        /// </summary>
        /// <returns></returns>
        private TimeSpan GetTransitTime(TDPLocation venue, string venuePierNaPTAN, bool isToVenue)
        {
            TimeSpan time = new TimeSpan();

            if (venue != null)
            {
                LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

                string venueGateNaPTAN = string.Empty;

                #region Venue pier to/from the venue interchange

                TDPPierVenueNavigationPath navigationPath = locationService.GetTDPVenuePierNavigationPaths(venue.Naptan, venuePierNaPTAN, isToVenue);

                if (navigationPath != null)
                {
                    venueGateNaPTAN = navigationPath.ToNaPTAN;

                    time = time.Add(navigationPath.DefaultDuration);
                }

                #endregion

                #region Venue gate and check constraints

                // Get the venue gate and path details for cycle park
                TDPVenueGate gate = locationService.GetTDPVenueGate(venueGateNaPTAN);
                TDPVenueGateCheckConstraint gateCheckConstraint = locationService.GetTDPVenueGateCheckConstraints(gate, isToVenue);
                TDPVenueGateNavigationPath gateNavigationPath = locationService.GetTDPVenueGateNavigationPaths(venue, gate, isToVenue);

                if (gateCheckConstraint != null)
                {
                    time = time.Add(gateCheckConstraint.AverageDelay);
                }

                if (gateNavigationPath != null)
                {
                    time = time.Add(gateNavigationPath.TransferDuration);
                }

                #endregion
            }

            return time;
        }

        #endregion
    }
}