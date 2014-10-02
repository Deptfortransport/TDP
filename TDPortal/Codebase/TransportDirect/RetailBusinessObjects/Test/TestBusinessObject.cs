//********************************************************************************
//NAME         : TestBusinessObject.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Test Data for business objects
//			   : ***** IMPORTANT ******
//				 The test data hardcoded into this class must be kept in step with the
//				 latest timetable data. 
//				 As a minimum this usually requires changing any test dates to fall within the latest timetable season.
//				 Failure to do this may result in unit tests failing.
//				 Data which must be kept up-to-date has been highlighted with the comment ***USELATEST***
//				 The Railplanner application (with latest data) should be used to determine
//				 the test data used.
//				************************
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Test/TestBusinessObject.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:44   mturner
//Initial revision.
//
//   Rev 1.6   Mar 01 2005 18:43:18   RPhilpott
//Cost Search Back End for Del 7 - work in progress
//
//   Rev 1.5   Feb 18 2005 16:37:30   rscott
//data updated to 2005 timetable
//
//   Rev 1.4   Jun 09 2004 13:31:06   asinclair
//Updated Test Data
//
//   Rev 1.3   Feb 04 2004 17:39:12   geaton
//Updated timetable specific data so tests will work against the latest timetable data. Added comments to highlight that this will need to be done the next time that timetables change.
//
//   Rev 1.2   Nov 23 2003 19:54:50   CHosegood
//Added code doco
//
//   Rev 1.1   Oct 31 2003 10:54:54   CHosegood
//Renamed BusinessObjectTest to TestBusinessObject
using NUnit.Framework;

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
    /// <summary>
    /// Summary description for BusinessObjectTest.
    /// </summary>
    public abstract class TestBusinessObject
    {
        public TestBusinessObject() { }
        /// <summary>
        /// 
        /// </summary>
        [SetUp] 
        public void SetUp() 
        {
            // Intialise property service and td logging service. In production this will be performed in global.asax.
            // NB RBOServiceInitialisation will only be called once per test suite run despite being in SetUp
            TDServiceDiscovery.Init(new RBOServiceInitialisation());
        }

        /// <summary>
        /// 
        /// </summary>
        [TearDown] public void TearDown() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PricingRequestDto BuildSingleRequest() 
        {
            TocDto midlandMainLine = new TocDto("ML");
            TocDto connex = new TocDto("CX");

            LocationDto nottingham = new LocationDto( "NOT", "18260");
            LocationDto londonStPancras = new LocationDto( "STP", "15550");
            LocationDto victoria = new LocationDto( "VIC", "54260");
            LocationDto doverPriory = new LocationDto( "DVP", "50330");

            //Intermediate Stops
            LocationDto loughborough = new LocationDto( "LBO", "    ");
            LocationDto leicester = new LocationDto( "LEI", "    ");
            LocationDto kettering = new LocationDto( "KET", "    ");
            LocationDto bedford = new LocationDto( "BDM", "    ");
            LocationDto bromleySouth = new LocationDto( "BMS", "    ");
            LocationDto rochester = new LocationDto( "RTR", "    ");
            LocationDto chatham = new LocationDto( "CTM", "    ");
            LocationDto gillingham = new LocationDto( "GLM", "    ");
            LocationDto rainham = new LocationDto( "RAI", "    ");
            LocationDto sittingbourne = new LocationDto( "SIT", "    ");
            LocationDto faversham = new LocationDto( "FAV", "    ");
            LocationDto canterburyEast = new LocationDto( "CBE", "    ");

            LocationDto selling = new LocationDto( "SEG", "    ");
            LocationDto bekesbourne = new LocationDto( "BKS", "    ");
            LocationDto adisham = new LocationDto( "ADM", "    ");
            LocationDto ayesham = new LocationDto( "AYH", "    ");
            LocationDto snowdown = new LocationDto( "SWO", "    ");
            LocationDto shephardsWell = new LocationDto( "SPH", "    ");
            LocationDto kearsney = new LocationDto( "KSN", "    ");

            PricingRequestDto request = new PricingRequestDto();
			//***USELATEST***
            request.OutwardDate = new TDDateTime(2005, 6, 4, 8, 30, 0);

            //Nottingham -> St Pancras
            StopDto board = new StopDto( nottingham );
			//***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,8,30,0);
            StopDto alight = new StopDto( londonStPancras );
			//***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,10,19,0);

            TocDto[] tocs = new TocDto[] { midlandMainLine };
            LocationDto[] intermediates = new LocationDto[4];
            intermediates[0] = loughborough;
            intermediates[1] = leicester;
            intermediates[2] = kettering;
            intermediates[3] = bedford;
            TrainDto train = new TrainDto("Y63502", tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //London Victoria -> Dover Priory(CX)
            board = new StopDto( victoria );
			//***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,11,34,0);
            alight = new StopDto( doverPriory );
			//***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,13,27,0);

            tocs = new TocDto[] { connex };
            intermediates = new LocationDto[15];
            intermediates[0] = bromleySouth;
            intermediates[1] = rochester;
            intermediates[2] = chatham;
            intermediates[3] = gillingham;
            intermediates[4] = rainham;
            intermediates[5] = sittingbourne;
            intermediates[6] = faversham;
            intermediates[7] = selling;
            intermediates[8] = canterburyEast;
            intermediates[9] = bekesbourne;
            intermediates[10] = adisham;
            intermediates[11] = ayesham;
            intermediates[12] = snowdown;
            intermediates[13] = shephardsWell;
            intermediates[14] = kearsney;
            train = new TrainDto("W03081",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            request.NumberOfAdults = 1;
            request.NumberOfChildren = 1;
            request.TicketClass = 9;
            request.Railcard = "YNG";

            return request;
        }
		

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PricingRequestDto BuildReturnRequest() 
        {
            TocDto midlandMainLine = new TocDto("ML");
            TocDto connex = new TocDto("CX");

            LocationDto nottingham = new LocationDto( "NOT", "18260");
            LocationDto londonStPancras = new LocationDto( "STP", "15550");
            LocationDto victoria = new LocationDto( "VIC", "54260");
            LocationDto doverPriory = new LocationDto( "DVP", "50330");

            //Intermediate Stops
            LocationDto loughborough = new LocationDto( "LBO", "    ");
            LocationDto leicester = new LocationDto( "LEI", "    ");
            LocationDto kettering = new LocationDto( "KET", "    ");
            LocationDto bedford = new LocationDto( "BDM", "    ");
            LocationDto bromleySouth = new LocationDto( "BMS", "    ");
            LocationDto rochester = new LocationDto( "RTR", "    ");
            LocationDto chatham = new LocationDto( "CTM", "    ");
            LocationDto gillingham = new LocationDto( "GLM", "    ");
            LocationDto rainham = new LocationDto( "RAI", "    ");
            LocationDto sittingbourne = new LocationDto( "SIT", "    ");
            LocationDto faversham = new LocationDto( "FAV", "    ");
            LocationDto canterburyEast = new LocationDto( "CBE", "    ");

            LocationDto selling = new LocationDto( "SEG", "    ");
            LocationDto bekesbourne = new LocationDto( "BKS", "    ");
            LocationDto adisham = new LocationDto( "ADM", "    ");
            LocationDto ayesham = new LocationDto( "AYH", "    ");
            LocationDto snowdown = new LocationDto( "SWO", "    ");
            LocationDto shephardsWell = new LocationDto( "SPH", "    ");
            LocationDto kearsney = new LocationDto( "KSN", "    ");

            PricingRequestDto request = new PricingRequestDto();

            request.OutwardDate = new TDDateTime(2005,6,4,8,30,0); //***USELATEST***
            request.ReturnDate = new TDDateTime(2005,6,6,11,47,0); //***USELATEST***
		

            //OUTBOUND JOURNEY
            //Nottingham -> St Pancras
            StopDto board = new StopDto( nottingham );
            board.Departure = new TDDateTime(2005,6,4,8,30,0);  //***USELATEST***
            StopDto alight = new StopDto( londonStPancras );     
            alight.Arrival = new TDDateTime(2005,6,4,10,19,0);  //***USELATEST***

            TocDto[] tocs = new TocDto[] { midlandMainLine };
            LocationDto[] intermediates = new LocationDto[4];
            intermediates[0] = loughborough;
            intermediates[1] = leicester;
            intermediates[2] = kettering;
            intermediates[3] = bedford;
            TrainDto train = new TrainDto("Y63502",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates ); 
            request.Trains.Add( train );

            //London Victoria -> Dover Priory(CX)
            board = new StopDto( victoria );
            board.Departure = new TDDateTime(2005,6,4,11,34,0); //***USELATEST***
            alight = new StopDto( doverPriory );
            alight.Arrival = new TDDateTime(2005,6,4,13,27,0); //***USELATEST***

            tocs = new TocDto[] { connex };
            intermediates = new LocationDto[15];
            intermediates[0] = bromleySouth;
            intermediates[1] = rochester;
            intermediates[2] = chatham;
            intermediates[3] = gillingham;
            intermediates[4] = rainham;
            intermediates[5] = sittingbourne;
            intermediates[6] = faversham;
            intermediates[7] = selling;
            intermediates[8] = canterburyEast;
            intermediates[9] = bekesbourne;
            intermediates[10] = adisham;
            intermediates[11] = ayesham;
            intermediates[12] = snowdown;
            intermediates[13] = shephardsWell;
            intermediates[14] = kearsney;
            train = new TrainDto("W03081",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //RETURN JOURNEY
            //Dover Priory -> London Victoria (CX)
            board = new StopDto( doverPriory );
            board.Departure = new TDDateTime(2005,6,6,10,4,0); //***USELATEST***
            alight = new StopDto( victoria );
            alight.Arrival = new TDDateTime(2005,6,6,11,47,0); //***USELATEST***

            tocs = new TocDto[] { connex };
            intermediates = new LocationDto[8];
            intermediates[0] = canterburyEast;
            intermediates[1] = faversham;
            intermediates[2] = sittingbourne;
            intermediates[3] = rainham;
            intermediates[4] = gillingham;
            intermediates[5] = chatham;
            intermediates[6] = rochester;
            intermediates[7] = bromleySouth;

            train = new TrainDto("W03054",  tocs, null, null, null, null, ReturnIndicator.Return, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //St Pancras -> Nottingham
            board = new StopDto( londonStPancras );
            board.Departure = new TDDateTime(2005,6,6,12,55,0); //***USELATEST***
            alight = new StopDto( nottingham );
            alight.Arrival = new TDDateTime(2005,6,6,14,36,0); //***USELATEST***

            intermediates = new LocationDto[2];
            intermediates[0] = bedford;
            intermediates[1] = leicester;
            tocs = new TocDto[] { midlandMainLine };
            train = new TrainDto("C53495", tocs, null, null, null, null, ReturnIndicator.Return, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            request.NumberOfAdults = 1;
            request.NumberOfChildren = 1;
            request.TicketClass = 9;
            request.Railcard = "DIS";

            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
  
        public PricingRequestDto BuildWiganToEgtonRequest() 
        {
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
			//***USELATEST***
            request.OutwardDate = new TDDateTime(2005, 6, 4, 9, 11, 0);

            //Wigan Wallgate -> Salford Crescent
            StopDto board = new StopDto( wiganWallgate );
			//***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,9,11,0);
            StopDto alight = new StopDto( salfordCrescent );
			//***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,9,44,0);
            StopDto destination = new StopDto( rochdale );
			//***USELATEST***
            destination.Arrival = new TDDateTime( 2005, 6, 4, 10, 27, 0);

            TocDto[] tocs = new TocDto[] { firstNorthWestern };
            LocationDto[] intermediates =  new LocationDto[0];
            TrainDto train = new TrainDto("C71530",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //Salford Crescent -> Manchester Picadilly
            board = new StopDto( salfordCrescent );
			//***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,10,7,0);
            alight = new StopDto( manchesterPiccadilly );
			//***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,10,20,0);
            destination = new StopDto( manchesterAirport );
			//***USELATEST***
            destination.Arrival = new TDDateTime( 2005, 6, 4, 10, 38, 0);

            tocs = new TocDto[] { firstNorthWestern };
            intermediates =  new LocationDto[0];
            train = new TrainDto("C71467",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //Manchester Picadilly -> Darlington
            board = new StopDto( manchesterPiccadilly );
			//***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,10,46,0);
            alight = new StopDto( darlington );
			//***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,12,50,0);
            destination = new StopDto( newcastle );
			//***USELATEST***
            destination.Arrival = new TDDateTime( 2005, 6, 4, 13, 33, 0);

            tocs = new TocDto[] { arrivaTrainsNorthern };
            intermediates = new LocationDto[0];
            train = new TrainDto("C70039",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //Darlington -> Middlesbrough
            board = new StopDto( darlington );
			//***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,13,30,0);
            alight = new StopDto( middlesbrough );
			//***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,13,55,0);
            destination = new StopDto( saltburn );
			//***USELATEST***
            destination.Arrival = new TDDateTime( 2005, 6, 4, 14, 21, 0);

            tocs = new TocDto[] { arrivaTrainsNorthern };
            intermediates =  new LocationDto[0];
            train = new TrainDto("Y50379",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //Middlesbrough -> Egton
            board = new StopDto( middlesbrough );
			//***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,14,16,0);
            alight = new StopDto( egton );
			//***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,15,18,0);
            destination = new StopDto( whitby );
			//***USELATEST***
            destination.Arrival = new TDDateTime( 2005, 6, 4, 15, 43, 0);

            tocs = new TocDto[] { arrivaTrainsNorthern };
            intermediates =  new LocationDto[0];
            train = new TrainDto("Y50382",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            request.NumberOfAdults = 1;
            request.NumberOfChildren = 1;
            request.TicketClass = 9;
            return request;
        }
		
		
        /// <summary>
        /// Return a pricing request from leicester to nottingham.  This fare
        /// only has 1 train and does not go via london
        /// </summary>
        /// <returns></returns>
        
        protected PricingRequestDto BuildLeicesterToNottinghamPricingRequestDto() 
        {
            PricingRequestDto request = new PricingRequestDto();

            LocationDto boardLocation = new LocationDto( "LEI", "19470" );
            StopDto board = new StopDto( boardLocation );
			//***USELATEST***
            board.Departure = new TDDateTime(2005,6,4,8,50,0);

            LocationDto alightLocation = new LocationDto( "NOT", "18260" );
            StopDto alight = new StopDto( alightLocation );
			//***USELATEST***
            alight.Arrival = new TDDateTime(2005,6,4,09,25,0);

            LocationDto destinationLocation = new LocationDto( "LCN", "63400" );
            StopDto destination = new StopDto( destinationLocation );
			//***USELATEST***
            destination.Arrival = new TDDateTime( 2005,6,4,10,12,0);
			
			//***USELATEST***
            request.OutwardDate = new DateTime(2005,6,4,8,50,0);
			//***USELATEST***
            DateTime date = new DateTime(2005,6,4,10,12,0);
            TocDto[] tocs = new TocDto[1];
            tocs[0] = new TocDto("CT");
            LocationDto[] intermediates= new LocationDto[1];
            LocationDto loughborough = new LocationDto( "LBO", "    ");
            intermediates[0] = loughborough;
            TrainDto train = new TrainDto( string.Empty, tocs, " ", " ", null, null, ReturnIndicator.Outbound, board, board, alight,destination, intermediates );
            request.Trains.Add ( train );

            request.NumberOfAdults = 1;
            request.NumberOfChildren = 1;
            request.TicketClass = 9;
            request.Railcard = "FAM";
            request.OutwardDate = TDDateTime.Now;

            return request;
        }
		
    }
}

