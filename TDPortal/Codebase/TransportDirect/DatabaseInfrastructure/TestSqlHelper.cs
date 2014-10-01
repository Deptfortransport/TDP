// ***********************************************
// NAME 		: SqlHelperTest.cs
// AUTHOR 		: Tushar Karsan
// DATE CREATED : 18-Jul-2003
// DESCRIPTION 	: Core DB Infrastructure tester.
// See _ReadMe.txt for more information.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DatabaseInfrastructure/TestSqlHelper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:56   mturner
//Initial revision.
//
//   Rev 1.10   Mar 31 2005 09:08:40   jgeorge
//Moved code from constructor to TestFixtureSetup
//
//   Rev 1.9   Feb 07 2005 16:09:50   bflenk
//Changed Assertion to Assert
//
//   Rev 1.8   Jun 16 2004 14:43:46   CHosegood
//Changed namespace to TransportDirect.Common.DatabaseInfrastructure
//
//   Rev 1.7   May 27 2004 18:41:10   CHosegood
//Added AirRouteMatrixDB
//
//   Rev 1.6   Mar 17 2004 18:15:20   CHosegood
//Added trace
//
//   Rev 1.5   Oct 06 2003 13:45:02   TKarsan
//Using a standard database
//
//   Rev 1.4   Aug 11 2003 18:24:50   TKarsan
//Inserted Reference Number methods
//
//   Rev 1.3   Jul 25 2003 16:00:18   TKarsan
//Changes after 2nd code review
//
//   Rev 1.2   Jul 24 2003 16:00:18   TKarsan
//Update after review.
//
//   Rev 1.1   Jul 23 2003 17:22:12   TKarsan
//After project output name change.
//
//   Rev 1.0   Jul 23 2003 10:51:12   MTurner
//Initial revision.
//
//   Rev 1.4   Jul 21 2003 16:04:54   TKarsan
//FxCop fixes
//
//   Rev 1.3   Jul 18 2003 10:39:28   TKarsan
//Initial realease with comments on top of the file. The properties file is upated to use local server with trusted connection so that it can be checked out and run without additional set-up as long as Northwind DB exists.

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using NUnit.Framework;

namespace TransportDirect.Common.DatabaseInfrastructure
{
	/// <summary>
	/// This is required by the property service for this test module. It is instantiated once in the test constructor.
	/// </summary>
	public class TestInitialization : IServiceInitialisation
	{
		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

		}
	}

	/// <summary>
	/// The main module for the test application.
	/// </summary>
    [TestFixture]
    public class TestSqlHelper
    {
        /// <summary>
        /// Used by the test app, connection in opend on init and closed on cleanup.
        /// </summary>
        private SqlHelper sql = new SqlHelper();

        /// <summary>
        /// Used to test RefNumber functions of SqlHelper.
        /// </summary>
        private static string qryRefSelect = "SELECT MAX(RefID) AS MaxRefID FROM ReferenceNum";

		/// <summary>
		/// Set up servicediscovery for the tests
		/// </summary>
		[TestFixtureSetUp]
		public void FixtureInit()
		{
			TDServiceDiscovery.Init(new TestInitialization());
		}

        /// <summary>
        /// Opens the private instance of a connection at start.
        /// </summary>
        [SetUp]
        public void Init()
        {
            try 
            {
                sql.ConnOpen(SqlHelperDatabase.DefaultDB);
            } 
            catch ( SqlException e ) 
            {
                Trace.Write ( e.Message );
            }
        }

        /// <summary>
        /// Closes the private instance of the connection at end.
        /// </summary>
        [TearDown]
        public void CleanUp()
        {
            try 
            {
                sql.ConnClose();
            } 
            catch ( SqlException e ) 
            {
                Trace.Write ( e.Message );
            }
        }

        /// <summary>
        /// Test all the database connections in the SqlHelperDatabase enum.
        /// </summary>
        [Test]
        public void TestDBConnections()
        {
            System.Array array = Enum.GetValues( typeof( SqlHelperDatabase ) );
            SqlHelperDatabase currentDatabase = SqlHelperDatabase.SqlHelperDatabaseEnd;

            try 
            {
                foreach ( SqlHelperDatabase db in array ) 
                {
                    currentDatabase = db;
                    if ( !currentDatabase.Equals( SqlHelperDatabase.SqlHelperDatabaseEnd ) ) 
                    {
                        if ( sql.ConnIsOpen ) 
                        {
                            sql.ConnClose();
                        }
                        sql.ConnOpen( currentDatabase );
                        Assert.AreEqual(true, sql.ConnIsOpen, "Database connection to [] failed to open", "Database connection to [] failed to open");
                    }
                }
            } 
            catch ( Exception e ) 
            {
                Assert.Fail("Database connection to (" + currentDatabase.ToString() +") failed to open : [" + e.Message + "]" );
            } 

            if ( sql.ConnIsOpen ) 
            {
                sql.ConnClose();
            }
        }

        /// <summary>
        /// Tests TestGetReference() that should return the next in sequence
        /// number. It should not return 0 or -1.
        /// </summary>
        [Test]
        public void TestGetReference()
        {
            int refOld, refNew, refNo1, refNo2;
			
            refOld = TestGetReferenceGetRegister();
            refNo1 = SqlHelper.GetRefNumInt();
            refNew = TestGetReferenceGetRegister();

            // On first execution, reference register should increament.
            Assert.IsTrue((refNo1 != 0) && (refNo1 != -1));
            Assert.IsTrue(refNew == refOld + 100 );

            // Reference register should not increament the 2nd time.
            refNo2 = SqlHelper.GetRefNumInt(); // Get 2nd reference.
            Assert.IsTrue(refNew == TestGetReferenceGetRegister());
            Assert.IsTrue(refNo2 == refNo1 + 1);

            // Check number format function is working.
            Assert.IsTrue(SqlHelper.FormatRef(        1) == "0000-0000-0000-0001");
            Assert.IsTrue(SqlHelper.FormatRef(    12345) == "0000-0000-0001-2345");
            Assert.IsTrue(SqlHelper.FormatRef(123456789) == "0000-0001-2345-6789");
        }

        /// <summary>
        /// Called by TestGetReference() to red register the the database.
        /// </summary>
        /// <returns></returns>
        private int TestGetReferenceGetRegister()
        {
            return  (int) sql.GetScalar(qryRefSelect);
        }
    }
}
