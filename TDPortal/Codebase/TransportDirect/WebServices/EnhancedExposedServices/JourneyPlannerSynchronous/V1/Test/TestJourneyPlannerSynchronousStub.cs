// ***********************************************
// NAME 		: TestJourneyPlannerSynchronousStub.cs
// AUTHOR 		: C.M. Owczarek
// DATE CREATED : 26/01/2006
// DESCRIPTION 	: Test IJourneyPlannerSynchronous implementation for NUnit testing of 
//                JourneyPlannerSynchronousService web service
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/JourneyPlannerSynchronous/V1/Test/TestJourneyPlannerSynchronousStub.cs-arc  $
//
//   Rev 1.3   Sep 29 2010 11:27:56   apatel
//EES Web Services for Cycle code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.2   Sep 08 2009 13:26:00   mmodi
//Updated Car journey planner following interface change
//Resolution for 5318: Car exposed service - Multiple journey limit property
//
//   Rev 1.1   Aug 04 2009 14:30:52   mmodi
//Updated for tests to the Car journey planner exposed service
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.0   Nov 08 2007 13:52:08   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:31:44   COwczarek
//Initial revision.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using Logger = System.Diagnostics.Trace;
using System.Runtime.Remoting;
using System.Diagnostics;
using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.UserPortal.JourneyPlannerService;

using dataTransfer = TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using CarJPDTO = TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1;

namespace TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1.Test
{

	public class TestJourneyPlannerSynchronousFactory : IServiceFactory
	{

        private TestJourneyPlannerSynchronous journeyPlanner;

		/// <summary>
		/// </summary>
		public TestJourneyPlannerSynchronousFactory()
		{
            journeyPlanner = new TestJourneyPlannerSynchronous();
		}

		#region IServiceFactory Members

		public object Get()
		{
			try
			{
				return journeyPlanner;
			}
			catch (RemotingException e)
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e));
			}
			catch (Exception e)
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e));
			}
			return null;
		}

		#endregion
	}


    public class TestJourneyPlannerSynchronous : IJourneyPlannerSynchronous

    {
        private int testNumber;

        public dataTransfer.PublicJourneyResult PlanPublicJourney(ExposedServiceContext context,
            dataTransfer.PublicJourneyRequest request)
        {
            switch (testNumber) 
            {
                case 0:
                    // throw a TDException
                    throw new TDException("Postcode not found, postcode=xyz",false,TDExceptionIdentifier.JPResolvePostcodeFailed);
                case 1:
                    // throw system exception
                    string s = null;
                    s.ToString();
                    return null;
                default:
                    return new dataTransfer.PublicJourneyResult();
            }
        }

        /// <summary>
        /// Test method for JourneyPlannerSynchronouse.PlanPrivateJourney(). 
        /// Returns various responses based on TestNumber property
        /// </summary>
        public CarJPDTO.CarJourneyResult PlanPrivateJourney(ExposedServiceContext context, CarJPDTO.CarJourneyRequest request, int maxNumberOfJourneys)
        {
            switch (testNumber)
            {
                case 0:
                    // throw a TDException
                    throw new TDException("No journey requests were found in the CarJourneyRequest.", false, TDExceptionIdentifier.JPMissingJourneyInRequest);
                case 1:
                    // throw system exception
                    string s = null;
                    s.ToString();
                    return null;
                default:
                    return new CarJPDTO.CarJourneyResult();
            }
        }

        /// <summary>
        /// Test number value controlling reponse from Plan journey methods
        /// </summary>
        public int TestNumber
        {
            set { testNumber = value; }
            get { return testNumber; }
        }

        #region IJourneyPlannerSynchronous Members


        public CycleJourneyResult PlanCycleJourney(ExposedServiceContext context, TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1.CycleJourneyRequest request)
        {
            switch (testNumber)
            {
                case 0:
                    // throw a TDException
                    throw new TDException("No journey requests were found in the CycleJourneyRequest.", false, TDExceptionIdentifier.JPMissingJourneyInRequest);
                case 1:
                    // throw system exception
                    string s = null;
                    s.ToString();
                    return null;
                default:
                    return new CycleJourneyResult();
            }
        }

        public GradientProfileResult GetGradientProfile(ExposedServiceContext context, TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1.GradientProfileRequest request)
        {
            switch (testNumber)
            {
                case 0:
                    // throw a TDException
                    throw new TDException("No requests were found in the GradientProfileRequest.", false, TDExceptionIdentifier.JPMissingJourneyInRequest);
                case 1:
                    // throw system exception
                    string s = null;
                    s.ToString();
                    return null;
                default:
                    return new GradientProfileResult();
            }
        }

        #endregion
    }

}