//********************************************************************************
//NAME         : TestMockNatExCjp.cs
//AUTHOR       : James Broome
//DATE CREATED : 02/03/2005
//DESCRIPTION  : Implementation of MockNatExCjpFactory, MockNatExCjpManagerFactory and
//				 MockNatCjpExManager classes
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/NatExFares/TestMockNatExCjp.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:32   mturner
//Initial revision.
//
//   Rev 1.1   Aug 24 2005 16:06:56   RPhilpott
//Make tests consistent with changed TDJourneyResult.AddResult() and PublicJourney ctor. 
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.0   Mar 31 2005 09:52:36   jbroome
//Initial revision.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.1   Mar 30 2005 10:39:32   jbroome
//Minor update
//
//   Rev 1.0   Mar 23 2005 09:36:54   jbroome
//Initial revision.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier

using System;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{

	#region MockNatExCjpFactory
	/// <summary>
	/// Mock Factory used by Service Discovery to create a CJP Stub
	/// with dummy NatExFares data.
	/// </summary>
	public class MockNatExCjpFactory : IServiceFactory
	{
		private string propertiesFileName = string.Empty;

		/// <summary>
		/// Constructor. Filename parameter used to retreive the 
		/// properties file used in setting up the stub CJP.
		/// </summary>
		public MockNatExCjpFactory(string fileName)
		{
			this.propertiesFileName = fileName;	
		}
	
		/// <summary>
		///  Method used by the ServiceDiscovery to get an
		///  instance of an implementation of ICJP
		/// </summary>
		/// <returns>A new instance of a CJP.</returns>
		public Object Get()
		{
			return new CjpStub(propertiesFileName);
		}
	}

	#endregion
	
	#region MockNatExCjpManagerFactory

	/// <summary>
	/// Mock CjpManagerFactory used by Service Discovery 
	/// to create a dummy cjpManager
	/// </summary>
	public class MockNatExCjpManagerFactory : IServiceFactory
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public MockNatExCjpManagerFactory()
		{
		}

		/// <summary>
		///  Method used by the ServiceDiscovery to get an
		///  instance of the CJPManager class. 
		/// </summary>
		/// <returns>A new instance of a mock CJPManager.</returns>
		public Object Get()
		{
			return new MockNatCjpExManager();
		}

	}

	#endregion

	#region MockNatCjpExManager

	/// <summary>
	/// Mock CjpManager used by Service Discovery to create a 
	/// dummy Cjp Manager that will return dummy data.
	public class MockNatCjpExManager : ICJPManager
	{
		/// <summary>
		/// Implementation of ICJPManager.CallCJP
		/// Retrieves the 'next' journey result from the list of 
		/// results specified in the properties file.
		/// </summary>
		/// <param name="request">ITDJourneyRequest</param>
		/// <param name="sessionId">not used</param>
		/// <param name="userType">not used</param>
		/// <param name="referenceTransaction">not used</param>
		/// <param name="loggedOn">not used</param>
		/// <param name="language">not used</param>
		/// <param name="isExtension">not used</param>
		/// <returns>ITDJourneyResult</returns>
		public 	ITDJourneyResult CallCJP( ITDJourneyRequest	request,
			string sessionId,
			int userType,
			bool referenceTransaction,
			bool loggedOn,
			string language,
			bool isExtension)
		{
		
			TDJourneyResult result = new TDJourneyResult();
			// Return dummy CJP from Service Discovery
			ICJP cjp = (ICJP) TDServiceDiscovery.Current[ServiceDiscoveryKey.Cjp];
			// Dummy request used in JourneyPlan process
			JourneyRequest dummyRequest = new JourneyRequest();	
			JourneyResult cjpResult = new JourneyResult();

			// Get result from dummy CJP stub
			for (int i=0; i<request.OutwardDateTime.Length; i++)
			{
				cjpResult = (JourneyResult)cjp.JourneyPlan(dummyRequest);
				result.AddResult(cjpResult, true, null, null, null, "");
			}
			

			// Do we need a return journey?
			if (request.ReturnDateTime != null)
			{
				for (int i=0; i<request.ReturnDateTime.Length; i++)
				{
					cjpResult = (JourneyResult)cjp.JourneyPlan(dummyRequest);
					result.AddResult(cjpResult, false, null, null, null, "");
				}
			}

			ProcessErrors(result, (TDJourneyRequest)request);
		
			return result;

		}

		/// <summary>
		/// Dummy implementation of CallCjp from ICJPManager
		/// Not used.
		/// </summary>
		/// <param name="request"></param>
		/// <param name="sessionId"></param>
		/// <param name="userType"></param>
		/// <param name="referenceTransaction"></param>
		/// <param name="referenceNumber"></param>
		/// <param name="lastSequenceNumber"></param>
		/// <param name="loggedOn"></param>
		/// <param name="language"></param>
		/// <returns></returns>
		public 	ITDJourneyResult CallCJP( ITDJourneyRequest	request,
			string sessionId,
			int userType,
			bool referenceTransaction,
			int referenceNumber,
			int lastSequenceNumber,
			bool loggedOn,
			string language)
		{
			return null;
		}

		/// <summary>
		/// Method adds error messages from CJP journey result to the 
		/// CJPMessages array of the TDJourneyResult.
		/// </summary>
		/// <param name="result"></param>
		/// <param name="request"></param>
		private void ProcessErrors (TDJourneyResult result, TDJourneyRequest request)
		{
			if (result.OutwardPublicJourneyCount == 0)
			{
				if (request.IsReturnRequired)
				{
					if  (result.ReturnPublicJourneyCount == 0)
					{
						if  (result.CJPValidError)
						{
							if  (result.CJPMessages.Length == 0)
							{
								result.AddMessageToArray(string.Empty, JourneyControlConstants.GetJourneyWebNoResults(false), 0, 0);
							}
						}
					}
				}
				else
				{
					if  (result.CJPValidError)
					{
						if  (result.CJPMessages.Length == 0 )
						{
							result.AddMessageToArray(string.Empty, JourneyControlConstants.GetJourneyWebNoResults(false), 0, 0);
						}
					}
				}
			}
		}
		
	}
	#endregion

}
