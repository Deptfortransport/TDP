// ***********************************************
// NAME 		: JourneyPlannerSynchronous.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 03/01/2006
// DESCRIPTION 	: This class is a concrete implementation of IJourneyPlannerSynchronous
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerService/JourneyPlannerSynchronous.cs-arc  $
//
//   Rev 1.3   Sep 29 2010 11:27:46   apatel
//EES Web Services for Cycle code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.2   Sep 08 2009 13:29:18   mmodi
//Updated for a max number of journeys in car request
//Resolution for 5318: Car exposed service - Multiple journey limit property
//
//   Rev 1.1   Aug 04 2009 14:04:14   mmodi
//Added PlanPrivateJourney method
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.0   Nov 08 2007 12:24:32   mturner
//Initial revision.
//
//   Rev 1.7   Jul 12 2006 16:21:42   rphilpott
//Ensure CJP sequence number is > 1 to avoid "force coah" problem, and then select best journey(s) to return to client.
//Resolution for 4126: Not returning best journey to Lauren
//
//   Rev 1.6   Feb 02 2006 14:43:22   mdambrine
//rework on the journey planner enhanced exposed services see CR053_IR_3407 Journey Planner Service Component.doc 
//
//   Rev 1.5   Jan 31 2006 16:10:10   mdambrine
//Fix fo the welsh language
//
//   Rev 1.4   Jan 20 2006 14:42:04   mdambrine
//changed the consumer to seperate project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Jan 19 2006 17:26:50   mdambrine
//Changes to the async process
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 17 2006 14:33:42   mdambrine
//Addition of the sendresult method + asynchronous journeyplanner functionality
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 13 2006 18:29:14   mdambrine
//In development
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 11 2006 13:39:04   mdambrine
//Initial revision.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1;
using TransportDirect.UserPortal.JourneyControl;

using CycleJPDTO = TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1;
using CyclePlannerControl = TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1;


namespace TransportDirect.UserPortal.JourneyPlannerService
{
	
	/// <summary>
	/// This class is a concrete implementation of IJourneyPlannerSynchronous
	/// </summary>
	public class JourneyPlannerSynchronous	: MarshalByRefObject, IJourneyPlannerSynchronous
	{

		/// <summary>
		/// This method implements the PlanPublicJourney method of IJourneyPlannerSynchronous. 
		/// It performs journey planning and returns a journey plan result
		/// </summary>
		/// <param name="context">The parameters in which the request is running in</param>
		/// <param name="request">The actual journeyplanning request</param>
		/// <returns></returns>
		public PublicJourneyResult PlanPublicJourney(ExposedServiceContext context,
											         PublicJourneyRequest request)
		{			
			//set the threads culture
			context.SetThreadCurrentCulture();

			//Translating the request from data transfer objects passed in by the External System to domain objects
			ITDJourneyRequest TDJourneyRequest = JourneyPlannerAssembler.CreateTDJourneyRequest(request);
			
			JourneyRequestHelper helper = new JourneyRequestHelper(context, TDJourneyRequest, request);

			//Validation and resolving of the locations
			helper.ResolveLocations();
			helper.Validate();
			helper.UpdateRequestSequence();

			//if validation was successful(no errors  call the CallCJP method of the JourneyRequestHelper instance. 
			//This commences journey planning			
			ITDJourneyResult journeyResult = helper.CallCJP();

			//If the CJPMessages property contains messages, a SoapException containing 
			//all messages should be thrown by calling ThrowSoapException
			bool errorOccured = false;
			if (journeyResult.CJPMessages != null && journeyResult.CJPMessages.Length > 0)
			{				
				for(int i = 0; i< journeyResult.CJPMessages.Length; i++)
				{					
					CJPMessage cjpMessage = (CJPMessage) journeyResult.CJPMessages[i];
					
					//get the message from the resourcefile
					cjpMessage.MessageText = helper.ResourceManager.GetString(cjpMessage.MessageResourceId);										

					//Gather the errorinformation when the message is of type "error"
					if (cjpMessage.Type == ErrorsType.Error)
					{						
						errorOccured = true;
					}						
				}

				if (errorOccured)
				{	
					helper.ThrowError("CJP errors occured",
						null,
						TDExceptionIdentifier.JPCJPErrorsOccured,
						journeyResult.CJPMessages);									  					
				}				
				
			}

			//translation from a domain object to a dto object
			PublicJourneyResult dtoJourneyResult =  JourneyPlannerAssembler.CreatePublicJourneyResultDT(journeyResult,
																										helper.ResourceManager,
																										request.OutwardArriveBefore,
																										request.ReturnArriveBefore,
																										helper.OriginalSequence);
			//obtain a status object that can be passed to SendResult
			CommonAssembler.CreateCompletionStatusDT(journeyResult.IsValid, 
													 journeyResult.CJPMessages, 
													 helper.ResourceManager);

			return dtoJourneyResult;
																
		}

        /// <summary>
        /// This method implements the PlanPrivateJourney method of IJourneyPlannerSynchronous. 
        /// It performs journey planning and returns a car journey plan result
        /// </summary>
        /// <param name="context">The parameters in which the request is running in</param>
        /// <param name="request">The actual car journeyplanning request</param>
        /// <param name="maxNumberOfJourneys">The max number of journeys allowed, -1 indicates there is no maximum</param>
        public CarJourneyResult PlanPrivateJourney(ExposedServiceContext context, CarJourneyRequest request, int maxNumberOfJourneys)
        {
            // Set the threads culture
            context.SetThreadCurrentCulture();

            // Arraylist to hold the journey results DT objects sent to user
            ArrayList journeyResults = new ArrayList();
            
            // Default helper to aid with throwing errors
            JourneyRequestHelper defaultHelper = new JourneyRequestHelper(context, null, new JourneyRequest());

            // Determine if we should limit number of car journeys to plan
            bool limitNumberOfJourneys = (maxNumberOfJourneys >= 0);

            // Check there is at least one journey request, otherwise throw error
            if ((request != null) && (request.JourneyRequests != null) && (request.JourneyRequests.Length > 0))
            {
                
                for (int i = 0; i < request.JourneyRequests.Length; i++)
                {
                    JourneyRequest jr = request.JourneyRequests[i];

                    // Check if exceeded the allowed number of journeys to plan
                    if ((limitNumberOfJourneys) && (i >= maxNumberOfJourneys))
                    {
                        // Exceeded allowed number of journeys to plan
                        string message = "Exceeded maximum number of journeys allowed in a request. This journey has not been planned";

                        journeyResults.Add(
                            CarJourneyPlannerAssembler.CreateJourneyResultDT(jr.JourneyRequestId, 
                                null, null, null, 
                                defaultHelper.ResourceManager, false, false,
                                message, TDExceptionIdentifier.JPExceededNumberOfJourneysInRequest));
                    }
                    else
                    {
                        #region Create TD request and Plan journey

                        // Translate the request to a domain object
                        ITDJourneyRequest tdJourneyRequest = CarJourneyPlannerAssembler.CreateTDJourneyRequest(request, jr);

                        if (tdJourneyRequest != null)
                        {
                            JourneyRequestHelper helper = new JourneyRequestHelper(context, tdJourneyRequest, jr);

                            try
                            {
                                // Validate and resolve locations
                                helper.ResolveLocations();
                                helper.Validate();
                                helper.UpdateRequestSequence();

                                // Plan the journey
                                ITDJourneyResult tdJourneyResult = helper.CallCJP();

                                // If the TDJourneyResult contains any error messages, these will be packaged in 
                                // to the result object sent back. 
                                // Don't throw an exception, we want to continue planning the remaining car journeys

                                // Create the journey result DT object, and add to the result array
                                journeyResults.Add(
                                    CarJourneyPlannerAssembler.CreateJourneyResultDT(jr.JourneyRequestId,
                                        tdJourneyRequest, tdJourneyResult, request.ResultSettings,
                                        helper.ResourceManager, jr.OutwardArriveBefore, jr.ReturnArriveBefore,
                                        string.Empty, TDExceptionIdentifier.Undefined));

                            }
                            catch (TDException tdEx)
                            {
                                // Flag this journey as not planned
                                journeyResults.Add(
                                    CarJourneyPlannerAssembler.CreateJourneyResultDT(jr.JourneyRequestId,
                                        null, null, null,
                                        helper.ResourceManager, false, false,
                                        tdEx.Message, tdEx.Identifier));
                            }
                        }
                        else
                        {
                            // Could not parse into a valid TDJourneyRequest, flag this journey as not planned because of parsing request
                            string message = "Failed to create a TDJourneyRequest from the CarJourneyRequest provided.";

                            journeyResults.Add(
                                CarJourneyPlannerAssembler.CreateJourneyResultDT(jr.JourneyRequestId,
                                    null, null, null,
                                    defaultHelper.ResourceManager, false, false,
                                    message, TDExceptionIdentifier.JPJourneyParametersParseError));
                        }

                        #endregion
                    }
                }

            }
            else
            {
                // No journeys found in the request, throw error
                defaultHelper.ThrowError("No journey requests were found in the CarJourneyRequest", string.Empty, TDExceptionIdentifier.JPMissingJourneyInRequest);
            }



            // Wrap all journeys in to the CarJourneyResult dto object returned
            CarJourneyResult dtoCarJourneyResult = CarJourneyPlannerAssembler.CreateCarJourneyResultDT(
                (JourneyResult[])journeyResults.ToArray(typeof(JourneyResult)));

            return dtoCarJourneyResult;
        }

        /// <summary>
        /// This method implements the PlanCycleJourney method of IJourneyPlannerSynchronous.
        /// It performs cycle journey planning and returns a cycle trip plan result.
        /// The method only plan journey in one direction. For return journey a separate call will required.
        /// </summary>
        /// <param name="context">The parameters in which the request is running in</param>
        /// <param name="request">The actual cycle journeyplanning request</param>
        /// <returns></returns>
        public CycleJPDTO.CycleJourneyResult PlanCycleJourney(ExposedServiceContext context, CycleJPDTO.CycleJourneyRequest request)
        {
            // Set the threads culture
            context.SetThreadCurrentCulture();

            CyclePlannerControl.ITDCyclePlannerResult tdCycleJourneyResult = null;

            CycleJPDTO.JourneyResult journeyResult = null;

            CycleJPDTO.ResultSettings resultSetting = new CycleJPDTO.ResultSettings();

            if (request.ResultSettings == null)
            {
                request.ResultSettings = CycleJPDTO.CycleJourneyPlannerAssembler.GetDefaultResultSettings();
            }

            // Default helper to aid with throwing errors
            CycleJourneyRequestHelper defaultHelper = new CycleJourneyRequestHelper(context, null, new CycleJPDTO.JourneyRequest());

            // Check there is at least one journey request, otherwise throw error
            if ((request != null) && (request.JourneyRequest != null))
            {
                
                #region Create TD Cycle request and Plan cycle journey

                        // Translate the request to a domain object
                CyclePlannerControl.ITDCyclePlannerRequest tdCycleJourneyRequest = CycleJPDTO.CycleJourneyPlannerAssembler.CreateTDCycleJourneyRequest(request);

                        if (tdCycleJourneyRequest != null)
                        {
                            CycleJourneyRequestHelper helper = new CycleJourneyRequestHelper(context, tdCycleJourneyRequest, request.JourneyRequest);

                            try
                            {
                                // Validate and resolve locations
                                helper.ResolveLocations();
                                helper.Validate();
                                
                                // Plan the journey
                                tdCycleJourneyResult = helper.CallCyclePlanner();

                               
                                // If the TDCycleJourneyResult contains any error messages, these will be packaged in 
                                // to the result object sent back. 
                               
                                // Create the cycle journey result DT object, and add to the result array

                                journeyResult = CycleJPDTO.CycleJourneyPlannerAssembler.CreateJourneyResultDT(request.JourneyRequest.JourneyRequestId,
                                        tdCycleJourneyRequest, tdCycleJourneyResult, request.ResultSettings,
                                        helper.ResourceManager, request.JourneyRequest.OutwardArriveBefore,
                                        string.Empty, TDExceptionIdentifier.Undefined);

                            }
                            catch (TDException tdEx)
                            {
                                // Flag this journey as not planned
                                journeyResult = CycleJPDTO.CycleJourneyPlannerAssembler.CreateJourneyResultDT(request.JourneyRequest.JourneyRequestId,
                                        null, null, null,
                                        helper.ResourceManager, false, 
                                        tdEx.Message, tdEx.Identifier);
                            }
                        }
                        else
                        {
                            // Could not parse into a valid TDCyclePlannerRequest, flag this journey as not planned because of parsing request
                            string message = "Failed to create a TDCyclePlannerRequest from the CycleJourneyRequest provided.";

                            journeyResult = CycleJPDTO.CycleJourneyPlannerAssembler.CreateJourneyResultDT(request.JourneyRequest.JourneyRequestId,
                                    null, null, null,
                                    defaultHelper.ResourceManager, false, 
                                    message, TDExceptionIdentifier.JPJourneyParametersParseError);
                        }

                        #endregion
                
            }
            else
            {
                // No journeys found in the request, throw error
                defaultHelper.ThrowError("No journey requests were found in the CycleJourneyRequest", string.Empty, TDExceptionIdentifier.JPMissingJourneyInRequest);
            }



            // Wrap all journeys in to the CycleJourneyResult dto object returned
            CycleJPDTO.CycleJourneyResult dtoCycleJourneyResult = CycleJPDTO.CycleJourneyPlannerAssembler.CreateCycleJourneyResultDT(journeyResult);

            return dtoCycleJourneyResult;
        }

        /// <summary>
        /// This method implements GetGradientProfile method of IJourneyPlannerSynchronous.
        /// It performs gradient profile planning and returns a gradient profile result
        /// </summary>
        /// <param name="context">The parameters in which the request is running in</param>
        /// <param name="request">The actual gradient profile plan request</param>
        /// <returns></returns>
        public GradientProfileResult GetGradientProfile(ExposedServiceContext context, GradientProfileRequest request)
        {
            // Set the threads culture
            context.SetThreadCurrentCulture();

            CyclePlannerControl.ITDGradientProfileResult tdGradientProfileResult = null;

            GradientProfileResult dtoGradientProfileResult = null;

            // Default helper to aid with throwing errors
            GradientProfileRequestHelper defaultHelper = new GradientProfileRequestHelper(context, null, new GradientProfileRequest());

            // Check there is at least one gradient profile request, otherwise throw error
            if ((request != null))
            {

                #region Create TD Gradient Profile request and result

                // Translate the request to a domain object
                CyclePlannerControl.ITDGradientProfileRequest tdGradientProfileRequest = GradientProfileAssembler.CreateTDGradientProfileRequest(request);

                if (tdGradientProfileRequest != null)
                {
                    GradientProfileRequestHelper helper = new GradientProfileRequestHelper(context, tdGradientProfileRequest, request);

                    try
                    {
                        // Validate
                        helper.Validate();

                        // Plan the gradient profile
                        tdGradientProfileResult = helper.CallGradientProfiler();

                        // If the TDGradientProfileResult contains any error messages, these will be packaged in 
                        // to the result object sent back. 

                        // Create the gradient profile result DT object, and add to the result array

                        dtoGradientProfileResult = GradientProfileAssembler.CreateGradientProfileResultDT(
                                tdGradientProfileRequest, tdGradientProfileResult,
                                helper.ResourceManager, string.Empty, TDExceptionIdentifier.Undefined);

                    }
                    catch (TDException tdEx)
                    {
                        // Flag this gradient profile as not planned
                        dtoGradientProfileResult = GradientProfileAssembler.CreateGradientProfileResultDT(
                                null, null,
                                helper.ResourceManager, tdEx.Message, tdEx.Identifier);

                    }
                }
                else
                {
                    // Could not parse into a valid TDGradientProfileRequest, flag this request as errored due to parsing error
                    string message = "Failed to create a TDGradientProfileRequest from the GradientProfileRequest provided.";

                    dtoGradientProfileResult = GradientProfileAssembler.CreateGradientProfileResultDT(
                                tdGradientProfileRequest, tdGradientProfileResult,
                                defaultHelper.ResourceManager, message, TDExceptionIdentifier.Undefined);

                }

                #endregion

            }
            else
            {
                // No polylines found in the request
                defaultHelper.ThrowError("No polyline group requests were found in the GradientProfileRequest", string.Empty, TDExceptionIdentifier.JPMissingJourneyInRequest);
            }

            return dtoGradientProfileResult;
        }
	}

}
