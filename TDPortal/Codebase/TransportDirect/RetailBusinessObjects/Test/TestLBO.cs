//********************************************************************************
//NAME         : TestRBOPool.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : NUnit test script for RBOPool
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Test/TestLBO.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:46   mturner
//Initial revision.
//
//   Rev 1.18   Apr 25 2005 21:14:00   RPhilpott
//Chnages to LBO calls, and removal of rendundant code.
//Resolution for 2328: PT - fares between Three Bridges and Victoria
//
//   Rev 1.17   Mar 24 2005 15:39:50   rscott
//Code Review Ammendments
//
//   Rev 1.16   Mar 22 2005 16:09:20   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.15   Feb 14 2005 09:27:40   RScott
//warning "tde declared but never used" resolved
//
//   Rev 1.14   Feb 08 2005 09:39:56   RScott
//Assertion changed to Assert
//
//   Rev 1.13   Feb 04 2004 17:39:10   geaton
//Updated timetable specific data so tests will work against the latest timetable data. Added comments to highlight that this will need to be done the next time that timetables change.
//
//   Rev 1.12   Jan 13 2004 13:15:44   CHosegood
//Added tests for station group looking (LBO)
//Resolution for 585: Pricing does not obtain all valid fares for stations within a group.
//Resolution for 594: BO request do not check for a negative length output buffer.
//
//   Rev 1.11   Nov 23 2003 19:54:52   CHosegood
//Added code doco
//
//   Rev 1.10   Oct 31 2003 10:54:46   CHosegood
//Renamed BusinessObjectTest to TestBusinessObject
//
//   Rev 1.9   Oct 28 2003 20:05:16   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.8   Oct 17 2003 12:50:00   geaton
//Use correct Interface property.
//
//   Rev 1.7   Oct 17 2003 12:35:54   geaton
//Released instances back to pool at end of test. Added asserts to ensure this is performed.
//
//   Rev 1.6   Oct 17 2003 12:05:26   CHosegood
//Adde unit test trace
//
//   Rev 1.5   Oct 17 2003 10:22:42   CHosegood
//TicketType is now a JourneyType
//
//   Rev 1.4   Oct 16 2003 13:59:16   CHosegood
//Removed Console.WriteLine statements
//
//   Rev 1.3   Oct 16 2003 13:26:04   CHosegood
//Extends BusinessObjectTest
//
//   Rev 1.2   Oct 15 2003 20:10:50   CHosegood
//Added unit tests
//
//   Rev 1.1   Oct 14 2003 12:45:10   CHosegood
//No change.
//
//   Rev 1.0   Oct 14 2003 11:24:48   CHosegood
//Initial Revision

using NUnit.Framework;

using System;
using System.Diagnostics;
using System.Reflection;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Test harness for the LBO.
	/// </summary>
	[TestFixture]
	public class TestLBO : TestBusinessObject
	{
        /// <summary>
        /// Test harness for LBO.
        /// </summary>
		public TestLBO() { }

		/// <summary>
        /// Test the LU method of the LBO for locations
        /// </summary>
        [Test] 
		public void TestValidLookupLocationGroupRequest() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }

            string validNlc = "1444";

            //Test valid call
			LookupTransform transform = new LookupTransform();
			LocationDto[] locations = transform.LookupFareGroups(validNlc, TDDateTime.Now );
            Assert.IsTrue(locations.Length > 0, "Should have found at least one fare group for Euston(1444)" );

        }

        /// <summary>
        /// Test the LU method of the LBO for locations
        /// This Test sends a non existant CRS to the LBO
        /// </summary>
        [Test] 
		public void TestNonExistantCrsLookupLocationGroupRequest() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }

            string nonExistentNlc = "9999";

            LookupTransform transform = new LookupTransform();
            LookupLocationGroupRequest groupLookupRequest;
            //Test nonExistant crs
            LocationDto[] locations = transform.LookupFareGroups( nonExistentNlc, TDDateTime.Now );
            Assert.IsTrue(locations.Length == 0, "Non Existent CRS Location has fare groups" );
       }

        /// <summary>
        /// Test the LU method of the LBO for locations
        /// This Test sends a date 10 years in the future to the LBO
        /// </summary>
        [Test] 
		public void TestDateLookupLocationGroupRequest() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }

            string validNlc= "1444";

            LookupTransform transform = new LookupTransform();
            //Test date 10 years in future
            LocationDto[] locations = transform.LookupFareGroups(validNlc, TDDateTime.Now.Add( new TimeSpan(356*10, 0, 0, 0)) );
            Assert.IsTrue(locations.Length == 0, "Groups returned unexpectedly");

        }
    }
}
