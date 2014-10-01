// ***********************************************
// NAME 		: TestDataChangeNotificationGroup.cs
// AUTHOR 		: Rob Greenwood
// DATE CREATED : 15/06/2004
// DESCRIPTION 	: Test functionality of the TestDataChangeNotificationGroup class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/Test/TestDataChangeNotificationGroup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:58   mturner
//Initial revision.
//
//   Rev 1.0   Jun 15 2004 14:57:20   CHosegood
//Initial revision.

using System;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Test functionality of the TestDataChangeNotificationGroup class.
	/// </summary>
	public class TestDataChangeNotificationGroup
	{

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void init() 
        {
            TDServiceDiscovery.Init(new TestInitialization());
        }

        /// <summary>
        /// 
        /// </summary>
		public TestDataChangeNotificationGroup()
		{ }
	}
}
