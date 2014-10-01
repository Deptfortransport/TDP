// *********************************************** 
// NAME             : GradientProfileRequestHelper.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: This class represents the functionality required 
//                  : by Gradient Profile synchronous service
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerService/GradientProfileRequestHelper.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:46:00   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1;
using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.JourneyPlannerService
{
    /// <summary>
    /// This class represents the functionality required by Gradient Profile synchronous service
    /// </summary>
	public class GradientProfileRequestHelper
	{
         #region Private members

        private ExposedServiceContext context;
        private GradientProfileRequest dtoGradientProfileRequest;
        private ITDGradientProfileRequest request;		

        #endregion

        #region constructor
        /// <summary>
        /// Constructor for Gradient Profile Request helper
        /// </summary>
        /// <param name="context">The exposed service context</param>
        /// <param name="request">The actual request</param>
        /// <param name="dtoRequestJourney">The data transformation request for the cycle journey</param>
        public GradientProfileRequestHelper(ExposedServiceContext context,
                                   ITDGradientProfileRequest request,
                                   GradientProfileRequest dtoGradientProfileRequest)
        {
            this.context = context;
            this.request = request;
            this.dtoGradientProfileRequest = dtoGradientProfileRequest;
        }

		#endregion

        #region properties
        /// <summary>
        /// Read-only property to write cleaner code.
        /// </summary>
        public TDResourceManager ResourceManager
        {
            get
            {
                return TDResourceManager.GetResourceManagerFromCache(TDResourceManager.JOURNEY_PLANNER_SERVICE_RM);
            }
        }

        #endregion

		

		#region public methods
		/// <summary>
		/// This method performs validation on the supplied request object in addition to the schema 
		/// validation already performed on the request
		/// </summary>
		public void Validate()
		{
            bool polylineFound = false;

            foreach (PolylineGroup group in dtoGradientProfileRequest.PolylineGroups)
            {
                if (group.Polylines.Length > 0)
                {
                    foreach (Polyline polyline in group.Polylines)
                    {
                        if (!string.IsNullOrEmpty(polyline.PolylineGridReferences.Trim()))
                        {
                            polylineFound = true;
                        }
                    }
                }
            }

            //validate if the request contains at least one polylinegroup and at least one polyline
            if (dtoGradientProfileRequest.PolylineGroups.Length == 0 || !polylineFound)
            {
                string validationError = "At least one polyline must be specified to plan gradient profile";

                ThrowError(validationError,
                           string.Empty,
                           TDExceptionIdentifier.EESGradientProfileNoPolylineSepcified);
            }

		}

		/// <summary>
		/// This method submits a request to the Cycle planner through the CyclePlannerControl 
		/// component to plan gradient profile
		/// </summary>
		/// <returns>ITDJourneyresult from the CJP</returns>
		public ITDGradientProfileResult CallGradientProfiler()
		{
			// Get a CJP Manager from the service discovery
            IGradientProfilerManager gradientProfileManager = (IGradientProfilerManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.GradientProfilerManager];

            ITDGradientProfileResult tdGradientProfileResult = gradientProfileManager.CallGradientProfiler(request,
                                                                        context.InternalTransactionId,
                                                                        (int)TDUserType.Standard,
                                                                        false,
                                                                        false,
                                                                        context.Language);

            if( TDTraceSwitch.TraceVerbose )
			{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"GradientProfileManager has returned - Internal Transaction ID = " + context.InternalTransactionId));
			}

            return tdGradientProfileResult;

		}

	

		#region throw errors and events
        /// <summary>
        /// Throws an TD exception error an logs an operational event
        /// </summary>
        /// <param name="errorMessage">Message to log</param>
        /// <param name="parameters">the innerparameter to fill into the message</param>
        /// <param name="identifier">unique identifier to this error</param>
        public void ThrowError(string errorMessage,
                               string parameter,
                               TDExceptionIdentifier identifier)
        {
            string[] parameters = new string[1];
            parameters[0] = parameter;

            ThrowError(errorMessage, parameters, identifier);
        }

		/// <summary>
		/// Throws an TD exception error an logs an operational event
		/// </summary>
		/// <param name="errorMessage">Message to log</param>
		/// <param name="parameters">the innerparameters to fill into the message</param>
		/// <param name="identifier">unique identifier to this error</param>
		public void ThrowError(string errorMessage,
							   string[] parameters,
							   TDExceptionIdentifier identifier)
		{												
			ThrowError(errorMessage, parameters, identifier, null);
		}

		/// <summary>
		/// Throws an TD exception error an logs an operational event
		/// </summary>
		/// <param name="errorMessage">Message to log</param>
		/// <param name="parameters">the innerparameters to fill into the message</param>
		/// <param name="identifier">unique identifier to this error</param>
		/// <param name="cjpMessages">message coming from the cjp</param>
		public void ThrowError(string errorMessage,
							   string[] parameters,
							   TDExceptionIdentifier identifier,
							   CJPMessage[] cjpMessages)
		{												
			//replace the number of parameters
			if (parameters != null && parameters.Length > 0)
			{
				for(int i=0; i<parameters.Length; i++)			
					errorMessage = errorMessage.Replace("{" + i + "}", parameters[i]);			
			}

			Logger.Write(new OperationalEvent(TDEventCategory.Business, 
											  TDTraceLevel.Error, 
											  errorMessage, 
										      this,
											  context.InternalTransactionId));

			if (cjpMessages != null)
				throw new TDException(errorMessage, false, identifier, cjpMessages);
			else
				throw new TDException(errorMessage, false, identifier);

		}		

		/// <summary>
		/// public method to log the end of the request
		/// </summary>
		/// <param name="enhanceExpServiceType">Enhanced Exposed Service Type Request</param>
		/// <param name="externalTransactionId">Reference transaction Id provided by client</param>
		/// <param name="callSucessful">Indicates whether call was sucessfull or not</param>
		public void LogFinishEvent(bool callSuccessful)
		{
			if (context != null)
			{
				Logger.Write(new EnhancedExposedServiceFinishEvent(callSuccessful, context));						
			}
		}
		#endregion

		#endregion

		
	}
}
