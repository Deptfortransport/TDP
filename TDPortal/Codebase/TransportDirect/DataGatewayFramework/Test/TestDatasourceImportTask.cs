// ***********************************************
// NAME 		: TestDatasourceImportTask.cs
// AUTHOR 		: Atos Origin
// DATE CREATED : 27/05/2004
// DESCRIPTION 	: Tests Datasource Import task class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/Test/TestDatasourceImportTask.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:20:22   mturner
//Initial revision.
//
//   Rev 1.2   Feb 01 2006 11:24:40   kjosling
//Fixed unit test
//
//   Rev 1.1   Feb 07 2005 10:43:58   bflenk
//Assertion changed to Assert
//
//   Rev 1.0   Jun 02 2004 11:23:18   CHosegood
//Initial revision.

using System;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Summary description for TestDatasourceImportTask.
	/// </summary>
	[TestFixture]
	public class TestDatasourceImportTask
	{
		public TestDatasourceImportTask()
		{
        }


        #region Setup/teardown
        /// <summary>
        /// Setup anything required by the test
        /// </summary>
        [SetUp]
        public void Init()
        {
            if ( !TestSetup.Initialised )
                TestSetup.Setup();
        }

        /// <summary>
        /// Cleans up after tests
        /// </summary>
        [TearDown]
        public void ClearUp()
        {
        }
		#endregion

        #region Tests
        /// <summary>
        /// 
        /// </summary>
        [Test]
		[Ignore("Test not implemented")]
        public void TestInvalidParameters() 
        {
   
        }

        #endregion
	}
}