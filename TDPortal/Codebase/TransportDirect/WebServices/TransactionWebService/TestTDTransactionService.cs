// *********************************************** 
// NAME			: TestTDTransactionService.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 11/09/2003 
// DESCRIPTION	: Implementation of the TestTDTransactionService class.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/TransactionWebServices/TestTDTransactionService.cs-arc  $:
//
//   Rev 1.1   Mar 16 2009 12:24:08   build
//Automatically merged from branch for stream5215
//
//   Rev 1.0.1.0   Jan 14 2009 17:59:34   mturner
//Removed redundant methods as part of tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 13:55:28   mturner
//Initial revision.
//
//   Rev 1.22   Feb 15 2006 17:37:38   kjosling
//Disabled Unit Test
//
//   Rev 1.21   Feb 15 2006 15:09:30   kjosling
//Disabled unit test
//
//   Rev 1.20   Feb 09 2006 15:53:58   RWilby
//Updated for ProjectNewkirk
//
//   Rev 1.19   Feb 10 2005 12:20:26   asinclair
//Changed filepath to work on Build machine
//
//   Rev 1.18   Feb 08 2005 13:23:10   RScott
//Assertions changed to Asserts
//
//   Rev 1.17   Apr 06 2004 09:44:38   geaton
//Removed test journey request (since the web method that this tests uses the EnableSession attribute which does not have effect when calling via NUnit)
//
//   Rev 1.16   Nov 13 2003 11:02:20   geaton
//Added test data to support relative datetimes for pricing transactions.
//
//   Rev 1.15   Nov 12 2003 20:27:04   geaton
//Updated test following change in type passed to pricing web method.
//
//   Rev 1.14   Nov 12 2003 16:11:10   geaton
//Added tests for pricing and station info.
//
//   Rev 1.13   Nov 06 2003 17:07:48   geaton
//Refactored tests.
//
//   Rev 1.12   Nov 03 2003 19:38:16   geaton
//Call Web method directly to simulate proper use of web service.
//
//   Rev 1.11   Oct 29 2003 10:50:38   JTOOR
//Latest changes to work with CJP.
//
//   Rev 1.10   Oct 23 2003 11:52:42   JTOOR
//Changes made to support mock and real CJP.
//
//   Rev 1.9   Oct 07 2003 13:11:32   JTOOR
//Minor mod.
//
//   Rev 1.8   Oct 07 2003 11:42:28   JTOOR
//Modified to support RequestJourneyParam.
//
//   Rev 1.7   Oct 03 2003 12:28:04   JTOOR
//Modified method "RequestJourney" to handle two new DateTime arguments.
//1. OutwardDateTime
//2. ReturnDateTime
//
//
//   Rev 1.6   Sep 30 2003 14:38:14   JTOOR
//Included two new tests for the SoftContent.
//
//   Rev 1.5   Sep 23 2003 14:06:12   RPhilpott
//Changes to TDJourneyResult (ctor and referenceNumber)
//
//   Rev 1.4   Sep 23 2003 09:52:52   RPhilpott
//Add extra param to CJPMessage ctor

using System;
using System.Diagnostics;
using System.Collections;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Remoting;
using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.ReportDataProvider.TransactionHelper;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.ReportDataProvider.TransactionWebService
{
	/// <summary>
	/// Tests the TDTransactionService.
	/// Uses a mock implementation of the CjpManagerFactory and CJPManager.
	/// </summary>
	[TestFixture]
	public class TestTDTransactionService
	{
		// Used to ensure initialisation is only performed once.
		static private bool initialised = false;

		[SetUp]
		public void Init()
		{
			if (!initialised)
			{
				// Only necessary to initialise once for all tests in this suite.
				// When used in production, this intialisation will be performed in 
				// the methog Global.asax::Application_Start once on web service start up.
				TDServiceDiscovery.Init( new TransactionWebServiceInitialisation() );
				initialised = true;
			}
		}

		[TearDown]
		public void CleanUp()
		{

		} 
	}
}
