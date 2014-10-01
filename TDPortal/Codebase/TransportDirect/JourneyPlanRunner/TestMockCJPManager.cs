using System;
using System.Collections;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for TestMockCJPManager.
	/// </summary>
	public class TestMockCJPManager : ICJPManager, IServiceFactory
	{

		private ITDJourneyRequest request;
		
		public TestMockCJPManager()
		{
		}


		public ITDJourneyResult CallCJP( ITDJourneyRequest	request,
			string sessionId,
			int userType,
			bool referenceTransaction,
			bool loggedOn,
			string language,
            bool isExtension)
		{
			this.request = request;
			return new TDJourneyResult();
		}

		public ITDJourneyResult CallCJP( ITDJourneyRequest	request,
			string sessionId,
			int userType,
			bool referenceTransaction,
			int referenceNumber,
			int lastSequenceNumber,
			bool loggedOn,
			string language)
		{
			this.request = request;
			return new TDJourneyResult();
		}

		
		public ITDJourneyRequest Request 
		{
			get { return request; }
		}


		public object Get()
		{
			return this;
		}
	
	}
}


