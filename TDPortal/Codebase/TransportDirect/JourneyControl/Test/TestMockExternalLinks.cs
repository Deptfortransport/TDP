// *********************************************** 
// NAME			: TestMockAirDataProvider
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2006-02-20
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestMockExternalLinks.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:16   mturner
//Initial revision.
//
//   Rev 1.1   Feb 27 2006 12:20:08   RPhilpott
//Assign to IR 0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.0   Feb 27 2006 12:19:28   RPhilpott
//Initial revision.
//

using System;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Testing stub for ExternalLinks.
	/// </summary>
	public class TestMockExternalLinks : IServiceFactory, IExternalLinks
	{
		public TestMockExternalLinks()
		{
		}

		public ExternalLinkDetail this[string key]
		{
			get
			{
				return null;
			}
		}

		public ExternalLinkDetail FindUrl(string url)
		{
			return null;
		}

		public object Get()
		{
			return new TestMockExternalLinks();
		}
	}
}
