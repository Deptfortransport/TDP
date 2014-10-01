// *********************************************** 
// NAME			: InternationalPlannerRequestPopulator.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 08 Feb 2010
// DESCRIPTION	: Class which is responsible for populating International planner requests
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/InternationalPlannerRequestPopulator.cs-arc  $
//
//   Rev 1.1   Feb 11 2010 08:53:14   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 09 2010 09:33:54   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1


using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.InternationalPlanner;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common;
using System.Collections;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.InternationalPlannerControl
{
    /// <summary>
    /// Class to populate a TDJourneyRequest in to a InternationalPlannerRequest
    /// </summary>
    public class InternationalPlannerRequestPopulator
    {
        #region Private members

       
        private ITDJourneyRequest tdInternationalPlannerRequest;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new InternationalPlannerRequestPopulator
        /// </summary>
        /// <param name="request">Related ITDJourneyRequest</param>
        public InternationalPlannerRequestPopulator(ITDJourneyRequest request)
        {
            this.tdInternationalPlannerRequest = request;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Creates the InternationalPlannerRequest objects needed to call the International planner service for the current 
        /// ITDJourneyRequests, and returns them encapsulated in an array of InternationalPlannerCall objects.
        /// </summary>
        /// <param name="referenceNumber"></param>
        /// <param name="seqNo"></param>
        /// <param name="sessionId"></param>
        /// <param name="referenceTransaction"></param>
        /// <param name="userType"></param>
        /// <param name="language"></param>
        /// <returns>Array of InternationalPlannerCall objects</returns>
        public InternationalPlannerCall[] PopulateRequests(int referenceNumber,
                                                        int seqNo,
                                                        string sessionId,
                                                        bool referenceTransaction,
                                                        int userType,
                                                        string language)
        {
            // The planner will not plan outward and return journeys in a single enquiry, so there will be only one call
            InternationalPlannerCall[] ipCalls = new InternationalPlannerCall[1];

            InternationalPlannerRequest request = null;

            request = PopulateSingleIPRequest(TDInternationalPlannerRequest,
                                                referenceNumber, seqNo++,
                                                sessionId, referenceTransaction,
                                                userType, language);

            ipCalls[0] = new InternationalPlannerCall(request, false, referenceNumber, sessionId);
            

            return ipCalls;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Creates a single, fully populate InternationalPlannerRequest object
        /// </summary>
        /// <returns></returns>
        private InternationalPlannerRequest PopulateSingleIPRequest(
            ITDJourneyRequest tdInternationalPlannerRequest,
            int referenceNumber,
            int seqNo,
            string sessionId,
            bool referenceTransaction,
            int userType,
            string language)
        {
            InternationalPlannerRequest request = new InternationalPlannerRequest();

            #region Initialise the request

            request.RequestID = SqlHelper.FormatRef(referenceNumber) + FormatSeqNo(seqNo);
            request.SessionID = sessionId;
            request.UserType = userType;

            #endregion

            #region Set locations and date/times, plus any TOIDs
           
            
            // Origin/Destination location names
            request.OriginName = tdInternationalPlannerRequest.OriginLocation.Description;
            request.DestinationName = tdInternationalPlannerRequest.DestinationLocation.Description;

            // Origin/Destination location naptans
            request.OriginNaptans = GetNaPTANs(tdInternationalPlannerRequest.OriginLocation.NaPTANs);
            request.DestinationNaptans = GetNaPTANs(tdInternationalPlannerRequest.DestinationLocation.NaPTANs);

            //Origin/Destination City Id
            request.OriginCityID = tdInternationalPlannerRequest.OriginLocation.CityId;
            request.DestinationCityID = tdInternationalPlannerRequest.DestinationLocation.CityId;

            List<InternationalModeType> modes = new List<InternationalModeType>();

            foreach(ModeType mode in tdInternationalPlannerRequest.Modes)
            {
                switch(mode)
                {
                    case ModeType.Air:
                        modes.Add(InternationalModeType.Air);
                        break;
                    case ModeType.Rail:
                        modes.Add(InternationalModeType.Rail);
                        break;
                    case ModeType.Coach:
                        modes.Add(InternationalModeType.Coach);
                        break;
                    case ModeType.Car:
                        modes.Add(InternationalModeType.Car);
                        break;
                    default:
                        modes.Add(InternationalModeType.None);
                        break;
                }
            }

            request.ModeType = modes.ToArray();
            
            request.OutwardDateTime = tdInternationalPlannerRequest.OutwardDateTime[0].GetDateTime();
            

            #endregion
                       

            return request;
        }

        /// <summary>
        /// Converts TDNaptan object array into naptan string array
        /// </summary>
        /// <param name="tDNaptan">TDNaptan object array</param>
        /// <returns>string array</returns>
        private string[] GetNaPTANs(TDNaptan[] tDNaptan)
        {
            List<string> naptans = new List<string>();

            foreach (TDNaptan naptan in tDNaptan)
            {
                naptans.Add(naptan.Naptan);
            }

            return naptans.ToArray();
        }

        /// <summary>
        /// Formats a sequence number for use as a request-id
        /// </summary>
        /// <param name="seqNo"></param>
        /// <returns>Formatted string</returns>
        private string FormatSeqNo(int seqNo)
        {
            return seqNo.ToString("-0000");
        }

        
        #endregion

        #region Public properties

        /// <summary>
        /// Read/write-only - the TDInternationalPlannerRequest supllied by the caller
        /// </summary>
        protected ITDJourneyRequest TDInternationalPlannerRequest
        {
            get { return tdInternationalPlannerRequest; }
            set { tdInternationalPlannerRequest = value; }
        }

        #endregion
    }
}
