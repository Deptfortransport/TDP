//********************************************************************************
//NAME         : TestGateway.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-04-02
//DESCRIPTION  : Test Harness to drive the Gateway class 
//				 and the underlying RetailBusinessObjects.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/RBOGateway/TestGateway.cs-arc  $
//
//   Rev 1.2   Dec 05 2012 14:01:04   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Jun 03 2010 09:28:14   mmodi
//Updated following changes to the fares contructor
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.0   Nov 08 2007 12:37:34   mturner
//Initial revision.
//
//   Rev 1.9   Dec 05 2005 18:26:44   RPhilpott
//Changes to ensure that RE GD call is made if connecting TOC's need to be checked post-timetable call.
//Resolution for 3308: DN040: (CG) Incorrect day/rate availability on weekender fare
//
//   Rev 1.8   Nov 24 2005 18:22:54   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.7   Nov 09 2005 12:31:44   build
//Automatically merged from branch for stream2818
//
//   Rev 1.6.1.1   Nov 02 2005 16:42:36   RPhilpott
//Added CJSessionInfo to PricePricingUnit() method.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.6.1.0   Oct 28 2005 16:28:56   RPhilpott
//Change PriceRoute() sig for new interface.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.6   Aug 24 2005 16:06:56   RPhilpott
//Make tests consistent with changed TDJourneyResult.AddResult() and PublicJourney ctor. 
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.5   Apr 25 2005 10:12:08   jbroome
//Updated after change to PriceRoute definition
//
//   Rev 1.4   Apr 17 2005 18:17:12   RPhilpott
//Del 7 unit testing.
//
//   Rev 1.3   Apr 07 2005 20:52:50   RPhilpott
//Work in progress
//
//   Rev 1.2   Apr 07 2005 19:05:18   RPhilpott
//Work in progress.
//
//   Rev 1.1   Apr 05 2005 11:22:42   RPhilpott
//Work in progress.
//
//   Rev 1.0   Apr 03 2005 18:23:06   RPhilpott
//Initial revision.
//

using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using System.Runtime.Remoting;
using System.Xml.Serialization;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.RBOGateway;
using TransportDirect.UserPortal.AdditionalDataModule;

using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.ManualTestHarness
{
	
	/// <summary>
	/// Test Harness to drive the Gateway class and underlying RetailBusinessObjects.
	/// Note that this is NOT intended to be an automated test, but rather a harness 
	/// to allow manual testing without the need for the CostSearchFacade or UI.
	/// </summary>
	[TestFixture]
	public class TestGateway
	{
	
//		private static string RAIL_DISCOUNT = "SRN";
		private static TransportDirect.UserPortal.PricingRetail.Domain.TicketClass DEFAULT_CLASS  = TransportDirect.UserPortal.PricingRetail.Domain.TicketClass.All;
//		private static TransportDirect.UserPortal.PricingRetail.Domain.TicketClass STANDARD_CLASS = TransportDirect.UserPortal.PricingRetail.Domain.TicketClass.Second;

		public TestGateway()
		{
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.AdditionalData, new AdditionalDataFactory());
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.GisQuery, new GisQueryFactory());
		}

		[SetUp]
		public void Init()
		{
//			RemotingConfiguration.Configure("cjp.client.config");		// determines which RBO's are used
																		//  (locally, not via .NET remoting, 
																		//    if this line commented out)
			Directory.SetCurrentDirectory("c:\\rgp\\dlls");				// location of RBO DLL's

		}

		[TearDown]
		public void CleanUp()
		{
		}

		[Test]
		[Ignore("Not automatically run -- this class is a harness for manual tests")]
		public void TestPriceRoute()
		{
 			Gateway gateway = new Gateway();

			string[] naptans = new string[] { "900057366", "9100BLFR", "9100CANONST", "9100CHRX", "9100EUSTON", "9100FENCHRS", "9100KNGX", "9100KNSXMCL", "9100LIVST", "9100LNDNBDC", 
									"9100LNDNBDE", "9100MARYLBN", "9100MRGT", "9100PADTON", "9100STPX", "9100VICTRIC", "9100VICTRIE", "9100WATRLMN", "9100WLOE", "9200LHR", "9200LTN", "9200STN" };
			bool[] fares = new bool[] { true, true, true, false, false, false, false, false, false, false,  
								   false, false, true, false, false, false, false, false, true, true, true, true };

			TDLocation origin = CreateLocation("London", naptans, fares); 

			naptans = new string[] { "900010091", "9100LVRPLCH", "9100LVRPLSH", "9100LVRPLSL", "9200LPL", "9200MAN" }; 
			fares = new bool[] { true, true, false, true, true, true };

			TDLocation destination = CreateLocation("Liverpool", naptans, fares); 

			ArrayList dates = new ArrayList();

			TravelDate date = new TravelDate();
			date.OutwardDate = new TDDateTime(2005, 04, 24);
			date.ReturnDate  = new TDDateTime(2005, 04, 25);
			date.TravelMode = TicketTravelMode.Rail;
			dates.Add(date);


			Discounts discounts = new Discounts(string.Empty, string.Empty, DEFAULT_CLASS);

			string[] errors = gateway.PriceRoute(dates, origin, destination, discounts, null, string.Empty, 0, 0, null);
			
		}

		[Test]
		[Ignore("Not automatically run -- this class is a harness for manual tests")]
		public void TestServiceParametersForFare()
		{
			Gateway gateway = new Gateway();

			TDLocation origin		= CreateLocation("9100EUSTON", "London Euston", true); 
			TDLocation destination	= CreateLocation("9100MNCRPIC", "Manchester Piccadilly", true); 

			TDDateTime outwardDate = new TDDateTime(2005, 05, 02);
			TDDateTime returnDate  = new TDDateTime(2005, 05, 08);

			RailFareData outwardFareData = new RailFareData("WKR", "00000", "VE", "1072", "0438", null, false, new LocationDto[0], new LocationDto[0], string.Empty);
            RailFareData returnFareData = new RailFareData("WKR", "00000", "VE", "1072", "0438", null, false, new LocationDto[0], new LocationDto[0], string.Empty); 

			RailServiceParameters[] rsps = gateway.ServiceParametersForFare(outwardDate, returnDate,
																	outwardFareData, returnFareData);
		}


		[Test]
		[Ignore("Not automatically run -- this class is a harness for manual tests")]
		public void TestValidateServicesForFare()
		{
			Gateway gateway = new Gateway();

			TDLocation origin		= CreateLocation("9100EUSTON", "London Euston", true); 
			TDLocation destination	= CreateLocation("9100MNCRPIC", "Manchester Piccadilly", true); 

			TDDateTime outwardDate = new TDDateTime(2005, 05, 02);
			TDDateTime returnDate  = new TDDateTime(2005, 05, 08);

            RailFareData outwardFareData = new RailFareData("WKR", "00000", "VE", "1072", "0438", null, false, new LocationDto[0], new LocationDto[0], string.Empty);
            RailFareData returnFareData = new RailFareData("WKR", "00000", "VE", "1072", "0438", null, false, new LocationDto[0], new LocationDto[0], string.Empty);  

			ArrayList outwardJourneys = CreateJourneys(true);
			ArrayList returnJourneys  = CreateJourneys(false);

			string outwardRestrictions = string.Empty;
			string returnRestrictions  = string.Empty;

			RailServiceValidationResultsDto response = gateway.ValidateServicesForFare(origin, destination, outwardDate, returnDate,
				outwardFareData, returnFareData, outwardJourneys, returnJourneys, outwardRestrictions, returnRestrictions, false, false, 
                false, false, false, false, false, false, string.Empty, string.Empty);
		}


		private ArrayList CreateJourneys(bool outward)
		{

			ArrayList journeys = new ArrayList();

			string currentFile = (outward ? "c:\\rgp\\outward.xml" : "c:\\rgp\\return.xml");

			int index = 0;
			int routeNum = (outward ? 0 : 999);

			XmlSerializer xs = new XmlSerializer(typeof(JourneyResult));
				
			FileStream fileStream = new FileStream(currentFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			JourneyResult result = (JourneyResult)xs.Deserialize(fileStream);
			fileStream.Close();

			foreach (TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpJourney in result.publicJourneys)
			{
			
				TransportDirect.UserPortal.JourneyControl.PublicJourney pj
						= new TransportDirect.UserPortal.JourneyControl.PublicJourney(index++, cjpJourney, null, 
									null, null, TDJourneyType.PublicOriginal, false, routeNum++);

				journeys.Add(pj);

			}

			return journeys;
		}


		private TDLocation CreateLocation(string naptan, string description, bool useForFares)
		{
			TDLocation loc = new TDLocation();
			TDNaptan[] naptans = new TDNaptan[] { new TDNaptan(naptan, new OSGridReference(), description, useForFares) };
			loc.NaPTANs = naptans;
			loc.Description = description;
			return loc;
		}

		private TDLocation CreateLocation(string description, string[] naptans, bool[] useForFares)
		{
			TDLocation loc = new TDLocation();
			
			TDNaptan[] tdNaptans = new TDNaptan[naptans.Length];

			for (int i = 0; i < naptans.Length; i++)
			{
				tdNaptans[i] = new TDNaptan(naptans[i], new OSGridReference(), description, useForFares[i]);
			}
													   
			loc.NaPTANs = tdNaptans;
			loc.Description = description;
			return loc;
		}

	}
}
