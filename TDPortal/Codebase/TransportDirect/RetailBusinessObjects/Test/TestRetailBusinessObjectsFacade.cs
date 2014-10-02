//********************************************************************************
//NAME         : TestRetailBusinessObjectsFacade.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : NUnit test script for RetailBusinessObjectsFacade
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Test/TestRetailBusinessObjectsFacade.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:48   mturner
//Initial revision.
//
//   Rev 1.11   Mar 03 2005 19:50:16   RPhilpott
//Work in progress
//
//   Rev 1.10   Mar 01 2005 18:43:18   RPhilpott
//Cost Search Back End for Del 7 - work in progress
//
//   Rev 1.9   Jun 17 2004 13:33:14   passuied
//changes for del6:
//Inserted calls to GL and GM functions to restrict fares.
//Changes in RestrictFares design to respect Open-Close Principle
//
//   Rev 1.8   Jun 09 2004 13:31:04   asinclair
//Updated Test Data
//
//   Rev 1.7   Oct 23 2003 10:16:14   CHosegood
//TicketCode instead of TicketName
//
//   Rev 1.6   Oct 19 2003 14:08:56   acaunt
//Revised to use the new TrainDto constructor
//
//   Rev 1.5   Oct 19 2003 14:02:16   CHosegood
//intermediates is now an empty array instead of null
//
//   Rev 1.4   Oct 17 2003 12:05:30   CHosegood
//Adde unit test trace
//
//   Rev 1.3   Oct 17 2003 10:23:16   CHosegood
//Added Trace
//
//   Rev 1.2   Oct 16 2003 15:22:52   CHosegood
//Added Leicester to Nottingham to test Nan for child tickets
//
//   Rev 1.1   Oct 16 2003 12:45:02   CHosegood
//Added railcard to test data
//
//   Rev 1.0   Oct 15 2003 20:11:00   CHosegood
//Initial Revision

using NUnit.Framework;

using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
    /// <summary>
    /// Summary description for TestRBO.
    /// </summary>
    [TestFixture]
	[CLSCompliant(false)]
    public class TestRetailBusinessObjectsFacade : RetailBusinessObjectsFacade
    {

        private PricingRequestDto BuildLeicesterToNottinghamPricingRequestDto() 
        {
            PricingRequestDto request = new PricingRequestDto();

			//Where we get on the train
            LocationDto boardLocation = new LocationDto( "LEI", "19470" );
            StopDto board = new StopDto( boardLocation );
            board.Departure = new TDDateTime(2004,12,9,8,50,0);

			//Where we get off the train
            LocationDto alightLocation = new LocationDto( "NOT", "18260" );
            StopDto alight = new StopDto( alightLocation );
            alight.Arrival = new TDDateTime(2004,12,09,09,25,0);

			//Where the train goes to
            LocationDto destinationLocation = new LocationDto( "LCN", "63400" );
            StopDto destination = new StopDto( destinationLocation );
            destination.Arrival = new TDDateTime( 2004,12,9,10,15,0);

			//Loughborough is between Leicester and Nottingham (intermediate stop)
			LocationDto loughborough = new LocationDto( "LBO", "    ");

            request.OutwardDate = new TDDateTime(2004, 12, 9, 8, 50, 0);

			//The operator of the train is Central Trains Ltd.
            TocDto[] tocs = new TocDto[1];
            tocs[0] = new TocDto("CT");

			//Add intermediate stops
            LocationDto[] intermediates= new LocationDto[1];
            intermediates[0] = loughborough;

			//Create the train
            TrainDto train = new TrainDto( string.Empty,tocs," "," ", null, null, ReturnIndicator.Outbound, board, board, alight,destination, intermediates );
            request.Trains.Add ( train );

			//Journey for 1 adult and 1 child using a young persons railcard
            request.NumberOfAdults = 1;
            request.NumberOfChildren = 1;
            request.TicketClass = 9;
            request.Railcard = "YNG";

            return request;
        }

		private PricingRequestDto BuildEalingToSwindonPricingRequestDto() 
		{
			PricingRequestDto request = new PricingRequestDto();

			//Where we get on the train
			LocationDto boardLocation = new LocationDto( "EAL", "319000" );
			StopDto board = new StopDto( boardLocation );
			board.Departure = new TDDateTime(2004,12,9,11,05,0);

			//Where we get off the train
			LocationDto alightLocation = new LocationDto( "SWI", "333300" );
			StopDto alight = new StopDto( alightLocation );
			alight.Arrival = new TDDateTime(2004,12,09,12,30,0);

			//Where the train goes to
			LocationDto destinationLocation = new LocationDto( "SWI", "333300" );
			StopDto destination = new StopDto( destinationLocation );
			destination.Arrival = new TDDateTime( 2004,12,9,12,30,0);

			
			request.OutwardDate = new TDDateTime(2004, 12, 9, 11, 05, 0);

			//The operator of the train is Central Trains Ltd.
			TocDto[] tocs = new TocDto[1];
			tocs[0] = new TocDto("TT");

			

			//Create the train
			TrainDto train = new TrainDto( string.Empty,tocs, " ", " ", null, null, ReturnIndicator.Outbound, board, board, alight,destination, null);
			request.Trains.Add ( train );

			//Journey for 1 adult and 1 child using a young persons railcard
			request.NumberOfAdults = 1;
			request.NumberOfChildren = 1;
			request.TicketClass = 9;
			request.Railcard = "YNG";

			return request;
		}

		private PricingRequestDto BuildEalingToSwindonViaPaddingtonPricingRequestDto() 
		{
			PricingRequestDto request = new PricingRequestDto();

			//Where we get on the train
			LocationDto boardLocation = new LocationDto( "EAL", "319000" );
			StopDto board = new StopDto( boardLocation );
			board.Departure = new TDDateTime(2004,12,9,11,01,0);

			//Where we get off the train
			LocationDto alightLocation = new LocationDto( "PAD", "308700" );
			StopDto alight = new StopDto( alightLocation );
			alight.Arrival = new TDDateTime(2004,12,09,11,08,0);

			//Where the train goes to
			LocationDto destinationLocation = new LocationDto( "PAD", "308700" );
			StopDto destination = new StopDto( destinationLocation );
			destination.Arrival = new TDDateTime( 2004,12,9,11,08,0);

			
			request.OutwardDate = new TDDateTime(2004, 12, 9, 11, 01, 0);

			//The operator of the train is Central Trains Ltd.
			TocDto[] tocs = new TocDto[1];
			tocs[0] = new TocDto("TT");

			//Create the train
			TrainDto train = new TrainDto( string.Empty, tocs, " ", " ", null, null, ReturnIndicator.Outbound, board, board, alight,destination, null );
			request.Trains.Add ( train );

			//------------

			//Where we get on the train
			boardLocation = new LocationDto( "PAD", "308700" );
			board = new StopDto( boardLocation );
			board.Departure = new TDDateTime(2004,12,9,11,15,0);

			//Where we get off the train
			alightLocation = new LocationDto( "SWI", "333300" );
			alight = new StopDto( alightLocation );
			alight.Arrival = new TDDateTime(2004,12,09,12,15,0);

			//Where the train goes to
			destinationLocation = new LocationDto( "BRI", "323100" );
			destination = new StopDto( destinationLocation );
			destination.Arrival = new TDDateTime( 2004,12,9,12,15,0);

			

			
			tocs = new TocDto[1];
			tocs[0] = new TocDto("GW");

			

			//Create the train
			train = new TrainDto( string.Empty, tocs, " ", " ", null, null, ReturnIndicator.Outbound, board, board, alight,destination, null );
			request.Trains.Add ( train );




			//Journey for 1 adult and 1 child using a young persons railcard
			request.NumberOfAdults = 1;
			request.NumberOfChildren = 1;
			request.TicketClass = 9;
			request.Railcard = "YNG";

			return request;
		}
		private PricingRequestDto BuildGatwickToReadingPricingRequestDto() 
		{
			PricingRequestDto request = new PricingRequestDto();

			//Where we get on the train
			LocationDto boardLocation = new LocationDto ( "GTW", "541600" );
			StopDto board = new StopDto( boardLocation );
			board.Departure = new TDDateTime(2004,12,9,11,03,0);

			//Where we get off the train
			LocationDto alightLocation = new LocationDto( "RDG", "314900" );
			StopDto alight = new StopDto( alightLocation );
			alight.Arrival = new TDDateTime(2004,12,09,12,17,0);

			//Where the train goes to
			LocationDto destinationLocation = new LocationDto( "RDG", "314900" );
			StopDto destination = new StopDto( destinationLocation );
			destination.Arrival = new TDDateTime( 2004,12,9,12,17,0);

			//Waterloo is between Gatwick and Reading (intermediate stop)
			LocationDto redHill = new LocationDto( "RDH", "547800");
			LocationDto reigate = new LocationDto( "REI", "548000");
			LocationDto dorking = new LocationDto( "DPD", "541200");
			LocationDto guilford = new LocationDto( "GLD", "563100");
			LocationDto northCamp = new LocationDto( "NCM", "563600");
			LocationDto blackwater = new LocationDto( "BAW", "562500");		
			LocationDto wokingham = new LocationDto( "WKM", "569600");


		

			request.OutwardDate = new TDDateTime(2004, 12, 9, 11, 03, 0);

			//The operator of the train is Central Trains Ltd.
			TocDto[] tocs = new TocDto[1];
			tocs[0] = new TocDto("TT");

			//Add intermediate stops
			LocationDto[] intermediates= new LocationDto[7];
			intermediates[0] = redHill;
			intermediates[1] = reigate;
			intermediates[2] = dorking;
			intermediates[3] = guilford;
			intermediates[4] = northCamp;
			intermediates[5] = blackwater;
			intermediates[6] = wokingham;

			//Create the train
			TrainDto train = new TrainDto( string.Empty, tocs, " ", " ", null, null, ReturnIndicator.Outbound, board, board, alight,destination, intermediates );
			request.Trains.Add ( train );

			//Journey for 1 adult and 1 child using a young persons railcard
			request.NumberOfAdults = 1;
			request.NumberOfChildren = 1;
			request.TicketClass = 9;
			request.Railcard = "YNG";

			return request;
		}

		private PricingRequestDto BuildGatwickToFarringdonPricingRequestDto() 
		{
			PricingRequestDto request = new PricingRequestDto();

			//Where we get on the train
			LocationDto boardLocation = new LocationDto ( "GTW", "541600" );
			StopDto board = new StopDto( boardLocation );
			board.Departure = new TDDateTime(2004,12,9,11,16,0);

			//Where we get off the train
			LocationDto alightLocation = new LocationDto( "ZFD", "057700" );
			StopDto alight = new StopDto( alightLocation );
			alight.Arrival = new TDDateTime(2004,12,09,11,57,0);

			//Where the train goes to
			LocationDto destinationLocation = new LocationDto( "KGX", "612100" );
			StopDto destination = new StopDto( destinationLocation );
			destination.Arrival = new TDDateTime( 2004,12,9,12,02,0);

			//Waterloo is between Gatwick and Reading (intermediate stop)
			LocationDto eastCroydon = new LocationDto( "ECR", "535500");
			LocationDto londonBridge = new LocationDto( "LBG", "514800");
			LocationDto blackfriars = new LocationDto( "BFR", "511200");
			LocationDto city = new LocationDto( "CTK", "512100");
			
		

			request.OutwardDate = new TDDateTime(2004, 12, 9, 11, 16, 0);

			//The operator of the train is Central Trains Ltd.
			TocDto[] tocs = new TocDto[1];
			tocs[0] = new TocDto("TT");

			//Add intermediate stops
			LocationDto[] intermediates= new LocationDto[4];
			intermediates[0] = eastCroydon;
			intermediates[1] = londonBridge;
			intermediates[2] = blackfriars;
			intermediates[3] = city;
			

			//Create the train
			TrainDto train = new TrainDto( string.Empty, tocs, " ", " ", null, null, ReturnIndicator.Outbound, board, board, alight,destination, intermediates );
			request.Trains.Add ( train );

			//Journey for 1 adult and 1 child using a young persons railcard
			request.NumberOfAdults = 1;
			request.NumberOfChildren = 1;
			request.TicketClass = 9;
			request.Railcard = "YNG";

			return request;
		}
        private PricingRequestDto BuildSingleRequest() 
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
            request.OutwardDate = new TDDateTime(2004, 6, 27, 8, 30, 0);

            //Nottingham -> St Pancras
            StopDto board = new StopDto( nottingham );
            board.Departure = new TDDateTime(2004,6,27,8,30,0);
            StopDto alight = new StopDto( londonStPancras );
            alight.Arrival = new TDDateTime(2004,6,27,10,19,0);

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
            board.Departure = new TDDateTime(2004,6,27,11,34,0);
            alight = new StopDto( doverPriory );
            alight.Arrival = new TDDateTime(2004,6,27,13,27,0);

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

        private PricingRequestDto BuildReturnRequest() 
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
            request.OutwardDate = new TDDateTime(2004, 6, 27, 8, 30, 0);
            request.ReturnDate = new TDDateTime(2004, 6, 29, 11, 47, 0 );

            //OUTBOUND JOURNEY
            //Nottingham -> St Pancras
            StopDto board = new StopDto( nottingham );
            board.Departure = new TDDateTime(2004,6,27,8,30,0);
            StopDto alight = new StopDto( londonStPancras );
            alight.Arrival = new TDDateTime(2004,6,27,10,19,0);

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
            board.Departure = new TDDateTime(2004,6,27,11,34,0);
            alight = new StopDto( doverPriory );
            alight.Arrival = new TDDateTime(2004,6,27,13,27,0);

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
            board.Departure = new TDDateTime(2004,6,29,10,4,0);
            alight = new StopDto( victoria );
            alight.Arrival = new TDDateTime(2004,6,29,11,47,0);

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
            board.Departure = new TDDateTime(2004,6,29,12,55,0);
            alight = new StopDto( nottingham );
            alight.Arrival = new TDDateTime(2004,6,29,14,36,0);

            intermediates = new LocationDto[2];
            intermediates[0] = bedford;
            intermediates[1] = leicester;
            tocs = new TocDto[] { midlandMainLine };
            train = new TrainDto("C53495", tocs, null, null, null, null, ReturnIndicator.Return, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            request.NumberOfAdults = 1;
            request.NumberOfChildren = 1;
            request.TicketClass = 9;
            request.Railcard = "YNG";

            return request;
        }

        private PricingRequestDto BuildWiganToEgtonRequest() 
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
            request.OutwardDate = new TDDateTime(2004, 6, 20, 9, 11, 0);

            //Wigan Wallgate -> Salford Crescent
            StopDto board = new StopDto( wiganWallgate );
            board.Departure = new TDDateTime(2004,6,20,9,11,0);
            StopDto alight = new StopDto( salfordCrescent );
            alight.Arrival = new TDDateTime(2004,6,20,9,44,0);
            StopDto destination = new StopDto( rochdale );
            destination.Arrival = new TDDateTime( 2004, 6, 20, 10, 27, 0);

            TocDto[] tocs = new TocDto[] { firstNorthWestern };
            LocationDto[] intermediates =  null;
            TrainDto train = new TrainDto("C71530",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //Salford Crescent -> Manchester Picadilly
            board = new StopDto( salfordCrescent );
            board.Departure = new TDDateTime(2004,6,20,10,7,0);
            alight = new StopDto( manchesterPiccadilly );
            alight.Arrival = new TDDateTime(2004,6,20,10,20,0);
            destination = new StopDto( manchesterAirport );
            destination.Arrival = new TDDateTime( 2004, 6, 20, 10, 38, 0);

            tocs = new TocDto[] { firstNorthWestern };
            intermediates =  new LocationDto[0];
            train = new TrainDto("C71467",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //Manchester Picadilly -> Darlington
            board = new StopDto( manchesterPiccadilly );
            board.Departure = new TDDateTime(2004,6,20,10,46,0);
            alight = new StopDto( darlington );
            alight.Arrival = new TDDateTime(2004,6,20,12,50,0);
            destination = new StopDto( newcastle );
            destination.Arrival = new TDDateTime( 2004, 6, 20, 13, 33, 0);

            tocs = new TocDto[] { arrivaTrainsNorthern };
            intermediates =  new LocationDto[0];
            train = new TrainDto("C70039",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //Darlington -> Middlesbrough
            board = new StopDto( darlington );
            board.Departure = new TDDateTime(2004,6,20,13,30,0);
            alight = new StopDto( middlesbrough );
            alight.Arrival = new TDDateTime(2004,6,20,13,55,0);
            destination = new StopDto( saltburn );
            destination.Arrival = new TDDateTime( 2004, 6, 20, 14, 21, 0);

            tocs = new TocDto[] { arrivaTrainsNorthern };
            intermediates =  new LocationDto[0];
            train = new TrainDto("Y50379",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
            request.Trains.Add( train );

            //Middlesbrough -> Egton
            board = new StopDto( middlesbrough );
            board.Departure = new TDDateTime(2004,6,20,14,16,0);
            alight = new StopDto( egton );
            alight.Arrival = new TDDateTime(2004,6,20,15,18,0);
            destination = new StopDto( whitby );
            destination.Arrival = new TDDateTime( 2004, 6, 20, 15, 43, 0);

            tocs = new TocDto[] { arrivaTrainsNorthern };
            intermediates =  new LocationDto[0];
            train = new TrainDto("Y50382",  tocs, null, null, null, null, ReturnIndicator.Outbound, board, board, alight, alight, intermediates );
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
        public TestRetailBusinessObjectsFacade() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        private void PrintResult( PricingResultDto result ) 
        {
            foreach (TicketDto ticket in result.Tickets) 
            {
                string message = string.Format( Messages.BOTicketDetails, ticket.TicketCode, ticket.TicketClass, ticket.Railcard, ticket.AdultFare.ToString().PadLeft(6,' '), ticket.ChildFare.ToString().PadLeft(6,' '), ticket.QuotaControlled );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, message) ); 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Test] public void TestSingleFare() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }
            PricingResultDto result = GetFaresForSingleJourney( BuildSingleRequest() );
            if (TDTraceSwitch.TraceVerbose) 
            {   
                PrintResult( result );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Test] public void TestReturnFare() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }
            PricingResultDto result = GetFaresForSingleJourney( BuildReturnRequest() );
            if (TDTraceSwitch.TraceVerbose) 
            {   
                PrintResult( result );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Test] public void TestWiganToEgton() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }
            PricingResultDto result = GetFaresForSingleJourney( BuildWiganToEgtonRequest() );
            if (TDTraceSwitch.TraceVerbose) 
            {   
                PrintResult( result );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Test] public void TestLeicesterToNottingham() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }
            PricingResultDto result = GetFaresForSingleJourney( BuildLeicesterToNottinghamPricingRequestDto() );
            if (TDTraceSwitch.TraceVerbose) 
            {   
                PrintResult( result );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
        }

		/// <summary>
		/// 
		/// </summary>
		[Test] public void TestGatwickToReading() 
		{
			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
			}
			PricingResultDto result = GetFaresForSingleJourney( BuildGatwickToReadingPricingRequestDto() );
			if (TDTraceSwitch.TraceVerbose) 
			{   
				PrintResult( result );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Test] public void TestGatwickToFarringdon() 
		{
			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
			}
			PricingResultDto result = GetFaresForSingleJourney( BuildGatwickToFarringdonPricingRequestDto() );
			if (TDTraceSwitch.TraceVerbose) 
			{   
				PrintResult( result );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Test] public void TestEalingToSwindon() 
		{
			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
			}
			PricingResultDto result = GetFaresForSingleJourney( BuildEalingToSwindonPricingRequestDto() );
			if (TDTraceSwitch.TraceVerbose) 
			{   
				PrintResult( result );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Test] public void TestEalingToSwindonViaPaddington() 
		{
			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
			}
			PricingResultDto result = GetFaresForSingleJourney( BuildEalingToSwindonViaPaddingtonPricingRequestDto() );
			if (TDTraceSwitch.TraceVerbose) 
			{   
				PrintResult( result );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
			}
		}
    }
}