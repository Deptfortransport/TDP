// ***********************************************
// NAME 		: JourneyPlanner.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 03/01/2006
// DESCRIPTION 	: This class is a concrete implementation of IJourneyPlanner
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerService/JourneyPlanner.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:30   mturner
//Initial revision.
//
//   Rev 1.8   Jul 12 2006 16:21:42   rphilpott
//Ensure CJP sequence number is > 1 to avoid "force coah" problem, and then select best journey(s) to return to client.
//Resolution for 4126: Not returning best journey to Lauren
//
//   Rev 1.7   Mar 02 2006 16:49:50   mdambrine
//added proxy for the consumerservice + more logging
//
//   Rev 1.6   Mar 01 2006 15:43:52   mdambrine
//Added extra logging for errors
//
//   Rev 1.5   Feb 02 2006 14:43:22   mdambrine
//rework on the journey planner enhanced exposed services see CR053_IR_3407 Journey Planner Service Component.doc 
//
//   Rev 1.4   Jan 31 2006 16:10:10   mdambrine
//Fix fo the welsh language
//
//   Rev 1.3   Jan 20 2006 14:42:04   mdambrine
//changed the consumer to seperate project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 19 2006 17:26:50   mdambrine
//Changes to the async process
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 17 2006 14:33:44   mdambrine
//Addition of the sendresult method + asynchronous journeyplanner functionality
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 11 2006 13:39:02   mdambrine
//Initial revision.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
using System;
using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.Common;

using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.JourneyPlannerService
{

	/// <summary>
	/// This class is a concrete implementation of IJourneyPlanner
	/// </summary>
	public class JourneyPlanner	: MarshalByRefObject,
				IJourneyPlanner
	{

		private delegate void AsyncPlanRunner(ExposedServiceContext context, PublicJourneyRequest request);

		/// <summary>
		/// This method implements the PlanPublicJourney method of IJourneyPlanner. 
		/// It commences journey planning and returns immediately not waiting for a journey 
		/// plan result
		/// </summary>
		/// <param name="context"></param>
		/// <param name="request"></param>
		public void PlanPublicJourney(ExposedServiceContext context,
									  PublicJourneyRequest request)
		{					
			AsyncPlanRunner theDelegate = new AsyncPlanRunner(InvokePlanPublicJourney);

			theDelegate.BeginInvoke(context, request, null, null);			
		}

		/// <summary>
		/// This method controls the journey planning process
		/// </summary>
		/// <param name="context"></param>
		/// <param name="request"></param>
		private void InvokePlanPublicJourney(ExposedServiceContext context,
			 						         PublicJourneyRequest request)
		{
			
			try
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
																
				//translation from a domain object to a dto object
				PublicJourneyResult dtoJourneyResult =  JourneyPlannerAssembler.CreatePublicJourneyResultDT(journeyResult,
																											helper.ResourceManager,
																											request.OutwardArriveBefore,
																											request.ReturnArriveBefore,
																											helper.OriginalSequence);

				//obtain a status object that can be passed to SendResult
				CompletionStatus status = CommonAssembler.CreateCompletionStatusDT(journeyResult.IsValid, 
																				   journeyResult.CJPMessages, 
																				   helper.ResourceManager);

				//log a finish event
				helper.LogFinishEvent(true);

				//send the result to the clients consumer webservice
				helper.SendResult(status, dtoJourneyResult);				

			}
			catch(Exception ex)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, 
							 TDTraceLevel.Error, 
							 ex.Message.ToString(), 
							 this,
							 context.InternalTransactionId));	

				JourneyRequestHelper helper = new JourneyRequestHelper(context, null, request);

				CompletionStatus status = CommonAssembler.CreateCompletionStatusDT(ex);

				helper.SendResult(status, null);
			
						
			}

		}

	}

} 
