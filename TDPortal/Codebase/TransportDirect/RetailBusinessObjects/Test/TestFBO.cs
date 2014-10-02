//********************************************************************************
//NAME         : TestRBOPool.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : NUnit test script for RBOPool
//			   : ***** IMPORTANT ******
//				 The test data hardcoded into this class must be kept in step with the
//				 latest timetable data.
//				 As a minimum this usually requires changing any test dates to fall within the latest timetable season.
//				 Failure to do this may result in unit tests failing.
//				 Data which must be kept up-to-date has been highlighted with the comment ***USELATEST***
//				 The Railplanner application (with latest data) should be used to determine
//				 the test data used.
//				************************
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Test/TestFBO.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:46   mturner
//Initial revision.
//
//   Rev 1.16   Mar 01 2005 18:43:18   RPhilpott
//Cost Search Back End for Del 7 - work in progress
//
//   Rev 1.15   Feb 18 2005 16:37:34   rscott
//data updated to 2005 timetable
//
//   Rev 1.14   Feb 08 2005 09:39:56   RScott
//Assertion changed to Assert
//
//   Rev 1.13   Jun 09 2004 13:30:30   asinclair
//Updated Test Data
//
//   Rev 1.12   Feb 04 2004 17:39:08   geaton
//Updated timetable specific data so tests will work against the latest timetable data. Added comments to highlight that this will need to be done the next time that timetables change.
//
//   Rev 1.11   Nov 23 2003 19:54:52   CHosegood
//Added code doco
//
//   Rev 1.10   Oct 31 2003 10:54:50   CHosegood
//Renamed BusinessObjectTest to TestBusinessObject
//
//   Rev 1.9   Oct 28 2003 20:05:18   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.8   Oct 19 2003 14:08:54   acaunt
//Revised to use the new TrainDto constructor
//
//   Rev 1.7   Oct 17 2003 12:35:58   geaton
//Released instances back to pool at end of test. Added asserts to ensure this is performed.
//
//   Rev 1.6   Oct 17 2003 12:05:32   CHosegood
//Adde unit test trace
//
//   Rev 1.5   Oct 16 2003 15:23:30   CHosegood
//Moved Leicester to Nottingham into base class
//
//   Rev 1.4   Oct 16 2003 13:59:20   CHosegood
//Removed Console.WriteLine statements
//
//   Rev 1.3   Oct 16 2003 13:26:18   CHosegood
//Extends BusinessObjectTest
//
//   Rev 1.2   Oct 15 2003 20:10:52   CHosegood
//Added unit tests
//
//   Rev 1.1   Oct 14 2003 12:45:18   CHosegood
//No change.
//
//   Rev 1.0   Oct 14 2003 11:24:46   CHosegood
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
	/// Test harness for FBO.
	/// </summary>
	[TestFixture]
	public class TestFBO : TestBusinessObject
	{
        /// <summary>
        /// 
        /// </summary>
		public TestFBO() { }
        /// <summary>
        /// A simple single journey with intermediate stops from Nottingham to
        /// Dover using a Young Persons Railcard
        /// </summary>
        [Test] public void TestSingleRequest() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }

            FBOPool pool = FBOPool.GetFBOPool();
            BusinessObject fbo = pool.GetInstance();
            int outputLength = 8192;

            PricingRequestDto request = BuildSingleRequest();

            FareRequest fareRequest = new FareRequest( pool.InterfaceVersion, request, outputLength  );

            BusinessObjectOutput output = fbo.Process( fareRequest );

            pool.Release( ref fbo );

            Fares fares = new Fares( output );
			Assert.IsTrue(fares.Item.Count > 0,
				"Fares data not available to test BO - ensure that test data in class TestBusinessObject corresponds with the latest timetable data.");

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }

            return;
        }

        /// <summary>
        /// A simple return journey with intermediate stops from Nottingham to
        /// Dover using a disabled adult railcard.
        /// </summary>
        [Test] public void TestReturnRequest() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
            FBOPool pool = FBOPool.GetFBOPool();
            BusinessObject fbo = pool.GetInstance();
            int outputLength = 8192;

            PricingRequestDto request = BuildReturnRequest();

            FareRequest fareRequest = new FareRequest( pool.InterfaceVersion, request, outputLength  );

            BusinessObjectOutput output = fbo.Process( fareRequest );

            pool.Release( ref fbo );

            Fares fares = new Fares( output );
			
			Assert.IsTrue(fares.Item.Count > 0,
				"Fares data not available to test BO - ensure that test data in class TestBusinessObject corresponds with the latest timetable data.");

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }

            return;
        }

        /// <summary>
        /// Test a journey between Leicester and Nottingham with  intermediate
        /// points provided but no TrainUID.  This journey uses a Family
        /// railcard.
        /// </summary>
        [Test] public void TestGetLeicesterToNottinghamFare() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
            FBOPool pool = FBOPool.GetFBOPool();
            BusinessObject fbo = pool.GetInstance();
            int outputLength = 8192;

            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );

            PricingRequestDto request = BuildLeicesterToNottinghamPricingRequestDto();

            FareRequest fareRequest = new FareRequest( pool.InterfaceVersion, request, outputLength  );

            BusinessObjectOutput output = fbo.Process( fareRequest );

            pool.Release( ref fbo );

            Fares fares = new Fares( output );
			
			Assert.IsTrue(fares.Item.Count > 0,
				"Fares data not available to test BO - ensure that test data in class TestBusinessObject corresponds with the latest timetable data.");

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }

            return;
        }

        /// <summary>
        /// This journey has no intermediate points or railcard.
        /// </summary>
        [Test] public void TestGetYorkToDoverFare() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
            TocDto greatNorthEasternRailway = new TocDto("GR");
            TocDto connex = new TocDto("CX");

            LocationDto york = new LocationDto( "YRK", "82630");
            LocationDto kingsX = new LocationDto( "KGX", "61210");
            LocationDto victoria = new LocationDto( "VIC", "54260");
            LocationDto doverPriory = new LocationDto( "DVP", "50330");

            FBOPool pool = FBOPool.GetFBOPool();
            BusinessObject fbo = pool.GetInstance();
            int outputLength = 8192;

            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );

            PricingRequestDto request = new PricingRequestDto();

			// ***USELATEST***
            request.OutwardDate = new TDDateTime(2005,6,4,13,4,0);

            //york -> kingsX [kingsX] (GR)
            StopDto board = new StopDto( york );
			// ***USELATEST***
            board.Departure = new TDDateTime(2005,2,6,10,3,0);

            StopDto alight = new StopDto( kingsX );
			// ***USELATEST***
            alight.Arrival = new TDDateTime(2005,2,6,12,10,0);

            TocDto[] originTocs = new TocDto[] { greatNorthEasternRailway };
            TrainDto train1 = new TrainDto(null, originTocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, null );
            request.Trains.Add( train1 );

            //victoria -> dover priory [Dover Priory] (CX)
            StopDto board2 = new StopDto( victoria );
			// ***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,13,4,0);

            StopDto alight2 = new StopDto( doverPriory );
			// ***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,14,44,0);

            TocDto[] destinationTocs = new TocDto[] { connex };
            TrainDto train2 = new TrainDto(null, originTocs, null, null, null, null, ReturnIndicator.Outbound, board2, board2, alight2, alight2, null );
            request.Trains.Add( train2 );

            request.NumberOfAdults = 1;
            request.NumberOfChildren = 1;
            request.TicketClass = 9;

            FareRequest fareRequest = new FareRequest( pool.InterfaceVersion, request, outputLength  );

            BusinessObjectOutput output = fbo.Process( fareRequest );

            pool.Release( ref fbo );

            Fares fares = new Fares( output );
			
			Assert.IsTrue(fares.Item.Count > 0,
				"Fares data not available to test BO - ensure that test data in class TestBusinessObject corresponds with the latest timetable data.");

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }

            return;
        }

        /// <summary>
        /// This journey uses 3 trains and no railcard.
        /// </summary>
        [Test] public void TestWiganToEgtonRequest() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }

            FBOPool pool = FBOPool.GetFBOPool();
            BusinessObject fbo = pool.GetInstance();
            int outputLength = 8192;

            TocDto firstNorthWestern = new TocDto("NW");
            TocDto arrivaTrainsNorthern = new TocDto("AN");

            LocationDto wiganWallgate = new LocationDto( "WGW", "24060");       //9100WIGANWL
            LocationDto salfordCrescent = new LocationDto( "SLD", "27940");     //9100SLFDCT
            LocationDto manchesterPiccadilly = new LocationDto( "MAN", "29680");//9100MNCRPIC
            LocationDto darlington = new LocationDto( "DAR", "78770");          //9100DLTN
            LocationDto middlesbrough = new LocationDto( "MBR", "79290");       //9100MDLSBRO
            LocationDto egton = new LocationDto( "EGT", "79180");               //9100EGTON

            LocationDto rochdale = new LocationDto( "RCD", "29240");            //9100RCHDALE
            LocationDto manchesterAirport = new LocationDto( "MIA", "29610");   //9100MNCRIAP
            LocationDto newcastle = new LocationDto( "NCL", "77280");           //9100NWCSTLE
            LocationDto saltburn = new LocationDto( "SLB", "79510");            //9100SBRN
            LocationDto whitby = new LocationDto( "WTB", "81840");              //9100WTBY

            PricingRequestDto request = new PricingRequestDto();
			// ***USELATEST***
            request.OutwardDate = new TDDateTime(2005, 6, 4, 9, 11, 0);

            //Wigan Wallgate -> Salford Crescent
            StopDto board = new StopDto( wiganWallgate );
			// ***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,9,11,0);
            StopDto alight = new StopDto( salfordCrescent );
			// ***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,9,44,0);
            StopDto destination = new StopDto( rochdale );
			// ***USELATEST***
            destination.Arrival = new TDDateTime( 2005, 6, 4, 10, 27, 0);

            TocDto[] tocs = new TocDto[] { firstNorthWestern };
            LocationDto[] intermediates =  null;
            TrainDto train = new TrainDto("C71530",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //Salford Crescent -> Manchester Picadilly
            board = new StopDto( salfordCrescent );
			// ***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,10,7,0);
            alight = new StopDto( manchesterPiccadilly );
			// ***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,10,20,0);
            destination = new StopDto( manchesterAirport );
			// ***USELATEST***
            destination.Arrival = new TDDateTime( 2005, 6, 4, 10, 38, 0);

            tocs = new TocDto[] { firstNorthWestern };
            intermediates =  null;
            train = new TrainDto("C71467",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //Manchester Picadilly -> Darlington
            board = new StopDto( manchesterPiccadilly );
			// ***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,10,46,0);
            alight = new StopDto( darlington );
			// ***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,12,50,0);
            destination = new StopDto( newcastle );
			// ***USELATEST***
            destination.Arrival = new TDDateTime( 2005, 6, 4, 13, 33, 0);

            tocs = new TocDto[] { arrivaTrainsNorthern };
            intermediates =  null;
            train = new TrainDto("C70039",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //Darlington -> Middlesbrough
            board = new StopDto( darlington );
			// ***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,13,30,0);
            alight = new StopDto( middlesbrough );
			// ***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,24,13,55,0);
            destination = new StopDto( saltburn );
			// ***USELATEST***
            destination.Arrival = new TDDateTime( 2005, 6, 4, 14, 21, 0);

            tocs = new TocDto[] { arrivaTrainsNorthern };
            intermediates =  null;
            train = new TrainDto("Y50379",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //Middlesbrough -> Egton
            board = new StopDto( middlesbrough );
			// ***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,14,16,0);
            alight = new StopDto( egton );
			// ***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,15,18,0);
            destination = new StopDto( whitby );
			// ***USELATEST***
            destination.Arrival = new TDDateTime( 2005, 6, 4, 15, 43, 0);

            tocs = new TocDto[] { arrivaTrainsNorthern };
            intermediates =  null;
            train = new TrainDto("Y50382",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            request.NumberOfAdults = 1;
            request.NumberOfChildren = 1;
            request.TicketClass = 9;

            FareRequest fareRequest = new FareRequest( pool.InterfaceVersion, request, outputLength  );

            BusinessObjectOutput output = fbo.Process( fareRequest );

            pool.Release( ref fbo );

            Fares fares = new Fares( output );

			Assert.IsTrue(fares.Item.Count > 0,
				"Fares data not available to test BO - ensure that test data in class TestBusinessObject corresponds with the latest timetable data.");

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }

            return;
        }

	}
}
