// *********************************************** 
// NAME                 : TestJourneyInitialisation.cs
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 15/10/2003
// DESCRIPTION			: Implementation of TestJourneyInitialisation class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TestStub/TestInitialisation.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:24:24   mturner
//Initial revision.
//
//   Rev 1.4   Mar 30 2006 13:34:26   mguney
//Manual merge for stream0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.3   Mar 03 2006 14:00:26   build
//Automatically merged from branch for stream0004
//
//   Rev 1.2.1.0   Dec 21 2005 15:58:52   mguney
//ExternalLinkService enabled.
//Resolution for 4: DN083: Car Journey Planning Enhancements - Ambiguity
//
//   Rev 1.2   Jul 28 2004 11:39:44   RPhilpott
//Changes to support rewrite of CJPManager for del 6.1 trunk planning.
//
//   Rev 1.1   Nov 06 2003 16:27:50   PNorell
//Initialisation for test includes caching.
//
//   Rev 1.0   Oct 15 2003 21:57:10   acaunt
//Initial Revision

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.ExternalLinkService;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Initialisation class to be included in the JourneyControl test harnesses
	/// </summary>
	public class TestJourneyInitialisation : IServiceInitialisation
	{
		public TestJourneyInitialisation()
		{
		}

		public void Populate(Hashtable serviceCache)
		{
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TestStubGisQuery());
			serviceCache.Add(ServiceDiscoveryKey.TDMapHandoff, new TestStubTDMapHandoff());
			serviceCache.Add( ServiceDiscoveryKey.Cache, new TestJourneyControlMockCache() );
			serviceCache.Add( ServiceDiscoveryKey.ExternalLinkService, new TestMockExternalLinks() );			
		}
	}
}
