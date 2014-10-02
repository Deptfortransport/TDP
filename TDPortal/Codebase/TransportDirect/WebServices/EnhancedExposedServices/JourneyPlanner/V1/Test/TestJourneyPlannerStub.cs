// ***********************************************
// NAME 		: TestJourneyPlannerStub.cs
// AUTHOR 		: C.M. Owczarek
// DATE CREATED : 26/01/2006
// DESCRIPTION 	: Test IJourneyPlanner implementation for NUnit testing of JourneyPlannerService
//                web service.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/JourneyPlanner/V1/Test/TestJourneyPlannerStub.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 13:52:06   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:31:08   COwczarek
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

namespace TransportDirect.EnhancedExposedServices.JourneyPlanner.V1.Test
{

	public class TestJourneyPlannerFactory	: IServiceFactory
	{

        private TestJourneyPlanner journeyPlanner;

		/// <summary>
		/// </summary>
		public TestJourneyPlannerFactory()
		{
            journeyPlanner = new TestJourneyPlanner();
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


    public class TestJourneyPlanner : IJourneyPlanner

    {
        private int testNumber;

        public void PlanPublicJourney(ExposedServiceContext context,
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
                    break;
                case 2:
                    // successful
                    break;
            }
        }

        public int TestNumber
        {
            set {testNumber = value;}
            get {return testNumber;}
        }
    }

}