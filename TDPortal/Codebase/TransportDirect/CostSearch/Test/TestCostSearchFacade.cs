// ****************************************************************** 
// NAME			: TestCostSearchFacade.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 20/10/2005 
// DESCRIPTION	: Definition of the TestCostSearchFacade
// ****************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/Test/TestCostSearchFacade.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 16:21:54   mturner
//Initial revision.
//
//   Rev 1.7   Dec 21 2005 17:40:34   RWilby
//Fixed test code
using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.AdditionalDataModule;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// Summary description for TestCostSearchFacade
	/// </summary>
	[TestFixture]
	public class TestCostSearchFacade
	{
		//facade
		private CostSearchFacade costSearchFacade;

		//requests
		private ICostSearchRequest singleRailRequest;
		private ICostSearchRequest returnRailRequest;
		private ICostSearchRequest singleCoachRequest;
		private ICostSearchRequest returnCoachRequest;
		
		//results
		private CostSearchResult result;
		private CostSearchResult existingResult;

		//tickets
		private CostSearchTicket selectedOutwardTicket;
		private CostSearchTicket selectedInwardTicket;
		private CostSearchTicket selectedReturnTicket;

		//session info
		CJPSessionInfo sessionInfo;

		//locations
		TDLocation originLocation;
		TDLocation destinationLocation;

		/// <summary>
		/// Initialises some dummy CostSearchRequests and some class level objects
		/// </summary>
		[SetUp]
		public void Init()
		{
			//initialise services
			TDServiceDiscovery.Init(new TestCostSearchInitialisation());
	
			//initialise an instance of the classes needed for tests			
			//facade
			costSearchFacade = new CostSearchFacade();

			//requests
			singleRailRequest =  new CostSearchRequest();
			returnRailRequest =  new CostSearchRequest();
			singleCoachRequest =  new CostSearchRequest();
			returnCoachRequest =  new CostSearchRequest();
			
			//results
			result =  new CostSearchResult();
			existingResult =  new CostSearchResult();

			//tickets
			selectedOutwardTicket= new CostSearchTicket();
			selectedInwardTicket= new CostSearchTicket();
			selectedReturnTicket= new CostSearchTicket();

			//date used as outward date
			TDDateTime eighthJune = new TDDateTime(2006,6,08);	
	
			#region Test SingleRailRequest
			
			//singleRailRequest - single rail journey London - Cambridge going eighthJune with no flexibility
			
			//set up origin location - London
			originLocation = new TDLocation();
			originLocation.Description = "London";
			originLocation.Status = TDLocationStatus.Valid;
			originLocation.Locality = "N0060403";	

			//naptans
			originLocation.NaPTANs = new TDNaptan[19];			
			originLocation.NaPTANs[0] = new TDNaptan("900057366", new OSGridReference(528629,180914),"London", true);
			originLocation.NaPTANs[1] = new TDNaptan("9100BLFR", new OSGridReference(531714,178693),"London", true);
			originLocation.NaPTANs[2] = new TDNaptan("9100CANONST", new OSGridReference(532600,180800),"London", true);
			originLocation.NaPTANs[3] = new TDNaptan("9100CHRX", new OSGridReference(530284,180418),"London", false);
			originLocation.NaPTANs[4] = new TDNaptan("9100EUSTON", new OSGridReference(529500,182700),"London", false);
			originLocation.NaPTANs[5] = new TDNaptan("9100FENCHRS", new OSGridReference(533431,180932),"London", false);
			originLocation.NaPTANs[6] = new TDNaptan("9100KNGX", new OSGridReference(530300,183000),"London", false);
			originLocation.NaPTANs[7] = new TDNaptan("9100KNSXMCL", new OSGridReference(530600,182900),"London", false);
			originLocation.NaPTANs[8] = new TDNaptan("9100LIVST", new OSGridReference(533176,181643),"London", false);
			originLocation.NaPTANs[9] = new TDNaptan("9100LNDNBDC", new OSGridReference(533000,180200),"London", false);
			originLocation.NaPTANs[10] = new TDNaptan("9100LNDNBDE", new OSGridReference(533000,180200),"London", false);
			originLocation.NaPTANs[11] = new TDNaptan("9100MARYLBN", new OSGridReference(527500,182000),"London", false);
			originLocation.NaPTANs[12] = new TDNaptan("9100MRGT", new OSGridReference(532600,181700),"London", true);
			originLocation.NaPTANs[13] = new TDNaptan("9100PADTON", new OSGridReference(526600,181300),"London", false);
			originLocation.NaPTANs[14] = new TDNaptan("9100STPX", new OSGridReference(530000,183150),"London", false);
			originLocation.NaPTANs[15] = new TDNaptan("9100VICTRIC", new OSGridReference(528900,179000),"London", false);
			originLocation.NaPTANs[16] = new TDNaptan("9100VICTRIE", new OSGridReference(528900,179000),"London", false);
			originLocation.NaPTANs[17] = new TDNaptan("9100WATRLMN", new OSGridReference(531200,179800),"London", false);
			originLocation.NaPTANs[18] = new TDNaptan("9100WLOE", new OSGridReference(531350,180044),"London", true);


			//set up destination location - Cambridge
			destinationLocation = new TDLocation();
			destinationLocation.Description = "Cambridge";
			destinationLocation.Status = TDLocationStatus.Valid;
			destinationLocation.Locality = "E0055326";	

			//naptans
			destinationLocation.NaPTANs = new TDNaptan[2];
			destinationLocation.NaPTANs[0] = new TDNaptan("900052074", new OSGridReference(545301,258427),"Cambridge", true);
			destinationLocation.NaPTANs[1] = new TDNaptan("9100CAMBDGE", new OSGridReference(546200,257300),"Cambridge", true);

			singleRailRequest.OriginLocation = originLocation;
			singleRailRequest.DestinationLocation = destinationLocation;	

			//travel dates 	
			singleRailRequest.OutwardDateTime = eighthJune;
			singleRailRequest.SearchOutwardStartDate = eighthJune;
			singleRailRequest.SearchOutwardEndDate = eighthJune;

			//flexibility
			singleRailRequest.OutwardFlexibilityDays = 0;

			//discount cards
			singleRailRequest.CoachDiscountedCard = string.Empty;
			singleRailRequest.RailDiscountedCard = string.Empty;

			//requestid
			singleRailRequest.RequestId = Guid.NewGuid();

			//session info
			sessionInfo = new CJPSessionInfo();			
			sessionInfo.IsLoggedOn = false;
			sessionInfo.Language = "en-GB";
			sessionInfo.SessionId = "session1";
			sessionInfo.UserType = 0;
			singleRailRequest.SessionInfo = sessionInfo;
			#endregion

			#region Test SingleCoachRequest
			
			//singleCoachRequest - single coach journey London - Cambridge going 08/06/2005 with no flexibility
			
			//requestid
			singleCoachRequest.RequestId = Guid.NewGuid();

			//set up origin location - London
			originLocation = new TDLocation();
			originLocation.Description = "London";
			originLocation.Status = TDLocationStatus.Valid;
			originLocation.Locality = "E0057190";	
			//naptans
			originLocation.NaPTANs = new TDNaptan[1];			
			originLocation.NaPTANs[0] = new TDNaptan("900057366", new OSGridReference(528629,178693),"London", true);

			//set up destination location - Cambridge
			destinationLocation = new TDLocation();
			destinationLocation.Description = "Cambridge";
			destinationLocation.Status = TDLocationStatus.Valid;
			destinationLocation.Locality = "E0055326";	
			//naptans
			destinationLocation.NaPTANs = new TDNaptan[1];
			destinationLocation.NaPTANs[0] = new TDNaptan("900052074", new OSGridReference(545301,258427),"Cambridge", true);

			singleCoachRequest.OriginLocation = originLocation;
			singleCoachRequest.DestinationLocation = destinationLocation;			

			//travel dates 	
			singleCoachRequest.OutwardDateTime = eighthJune;
			singleCoachRequest.SearchOutwardStartDate = eighthJune;
			singleCoachRequest.SearchOutwardEndDate = eighthJune;

			//flexibility
			singleCoachRequest.OutwardFlexibilityDays = 0;

			//discount cards
			singleCoachRequest.CoachDiscountedCard = string.Empty;
			singleCoachRequest.RailDiscountedCard = string.Empty;

			//session info
			sessionInfo = new CJPSessionInfo();			
			sessionInfo.IsLoggedOn = false;
			sessionInfo.Language = "en-GB";
			sessionInfo.SessionId = "session1";
			sessionInfo.UserType = 0;
			singleCoachRequest.SessionInfo = sessionInfo;
			#endregion		
		}

		/// <summary>
		/// 
		/// </summary>
		[TearDown]
		public void CleanUp()
		{			
	
		}	

		public TestCostSearchFacade()
		{
		}

		/// <summary>
		/// Tests the AssembleFares method completes returns the expected number of TravelDates
		/// and correct ticket collections for a single rail request
		/// </summary>
		[Test]
		public void TestAssembleFaresRailSingle()
		{			
			
			//Add travel modes			
			singleRailRequest.TravelModes = new TicketTravelMode[1];
			singleRailRequest.TravelModes[0] = TicketTravelMode.Rail;			
			
			//call AssembleFares
			result = (CostSearchResult)costSearchFacade.AssembleFares(singleRailRequest);

			//check result is not null
			Assert.IsNotNull(result, "TestAssembleFaresRailSingle has unexpectedly returned a null CostSearchResult"); 

			//check result has no errors
			CostSearchError[] errors = result.GetErrors();
			Assert.IsTrue(errors.Length == 0, "TestAssembleFaresRailSingle has unexpectedly returned CostSearchErrors"); 

			//check result has 2 TravelDates - 1 OpenReturn and 1 Single 
			TravelDate[] travelDates = result.GetAllTravelDates();
			Assert.IsTrue(travelDates.Length == 2, "TestAssembleFaresRailSingle has not returned the number of TravelDates expected"); 
			Assert.IsTrue(travelDates[0].TicketType == TicketType.OpenReturn, "TestAssembleFaresRailSingle has returned a Travel Date with an unexpected TicketType"); 
			Assert.IsTrue(travelDates[1].TicketType == TicketType.Single, "TestAssembleFaresRailSingle has returned a Travel Date with an unexpected TicketType"); 

			//check that the TravelDates have tickets 
			Assert.IsTrue(travelDates[0].HasTickets == true, "TestAssembleFaresRailSingle has returned a Travel Date with no tickets"); 
			Assert.IsTrue(travelDates[1].HasTickets == true, "TestAssembleFaresRailSingle has returned a Travel Date with no tickets"); 

			//check that the tickets are only outward tickets
			Assert.IsTrue(travelDates[0].HasOutwardTickets == true, "TestAssembleFaresRailSingle has returned a Travel Date with no outward tickets"); 
			Assert.IsTrue(travelDates[0].HasInwardTickets == false, "TestAssembleFaresRailSingle has returned a Travel Date with unexpected inward tickets"); 
			Assert.IsTrue(travelDates[0].HasReturnTickets == false, "TestAssembleFaresRailSingle has returned a Travel Date with unexpected return tickets"); 
			Assert.IsTrue(travelDates[1].HasOutwardTickets == true, "TestAssembleFaresRailSingle has returned a Travel Date with no outward tickets"); 
			Assert.IsTrue(travelDates[1].HasInwardTickets == false, "TestAssembleFaresRailSingle has returned a Travel Date with unexpected inward tickets"); 
			Assert.IsTrue(travelDates[1].HasReturnTickets == false, "TestAssembleFaresRailSingle has returned a Travel Date with unexpected return tickets"); 
		}

		/// <summary>
		/// Tests the AssembleFares method completes returns the expected number of TravelDates
		/// and correct ticket collections for a single coach request
		/// </summary>
		[Test]
		public void TestAssembleFaresCoachSingle()
		{			
			
			//Add travel modes to base request			 
			singleCoachRequest.TravelModes = new TicketTravelMode[1];
			singleCoachRequest.TravelModes[0] = TicketTravelMode.Coach;

			//call AssembleFares
			result = (CostSearchResult)costSearchFacade.AssembleFares(singleCoachRequest);

			//check result is not null
			Assert.IsNotNull(result, "TestAssembleFaresCoachSingle has unexpectedly returned a null CostSearchResult"); 

			//Get travel dates
			TravelDate[] travelDates = result.GetAllTravelDates();

			Assert.AreEqual(2,travelDates.Length,"TestAssembleFaresCoachSingle expected 2 TravelDates");

			Assert.AreEqual(1,travelDates[0].OutwardTickets.Length,"TestAssembleFaresCoachSingle expected 1 outward ticket");
			
		}

		/// <summary>
		/// Tests the AssembleFares method for a multi mode (coach and rail) request
		/// </summary>
		[Test]
		public void TestAssembleFaresMultiMode()
		{			
			
			//Add travel modes to base request			 
			singleRailRequest.TravelModes = new TicketTravelMode[2];
			singleRailRequest.TravelModes[0] = TicketTravelMode.Coach;
			singleRailRequest.TravelModes[1] = TicketTravelMode.Rail;

			//call AssembleFares
			result = (CostSearchResult)costSearchFacade.AssembleFares(singleRailRequest);

			//check result is not null
			Assert.IsNotNull(result, "TestAssembleFaresMultiMode has unexpectedly returned a null CostSearchResult"); 

			//Get travel dates
			TravelDate[] travelDates = result.GetAllTravelDates();

			Assert.AreEqual(4,travelDates.Length,"TestAssembleFaresMultiMode expected 4 TravelDates");

			//Check multi mode results
			Assert.AreEqual(TicketTravelMode.Coach,travelDates[0].TravelMode,"TestAssembleFaresMultiMode expected Coach TravelMode for TravelDate[0]");
			Assert.AreEqual(TicketTravelMode.Coach,travelDates[1].TravelMode,"TestAssembleFaresMultiMode expected Coach TravelMode for TravelDate[1]");
			Assert.AreEqual(TicketTravelMode.Rail,travelDates[2].TravelMode,"TestAssembleFaresMultiMode expected Rail TravelMode for TravelDate[2]");
			Assert.AreEqual(TicketTravelMode.Rail,travelDates[3].TravelMode,"TestAssembleFaresMultiMode expected Rail TravelMode for TravelDate[3]");

		}

		/// <summary>
		/// Tests the AssembleServices method for a single rail ticket
		/// </summary>
		[Test]
		public void TestAssembleServicesRailSingle()
		{
			//Add travel modes to base request			 
			singleRailRequest.TravelModes = new TicketTravelMode[1];
			singleRailRequest.TravelModes[0] = TicketTravelMode.Rail;

			//call AssembleFares so that we have an existing result
			existingResult = (CostSearchResult)costSearchFacade.AssembleFares(singleRailRequest);		

			//pick a ticket
			TravelDate[] travelDates = existingResult.GetAllTravelDates();
			

			selectedOutwardTicket = travelDates[0].OutwardTickets[0];		
		
			selectedOutwardTicket.TicketRailFareData.Origin = singleRailRequest.OriginLocation;
			selectedOutwardTicket.TicketRailFareData.Destination = singleRailRequest.DestinationLocation;

			//call AssembleServices
			ICostSearchResult updatedResult = costSearchFacade.AssembleServices(singleRailRequest, existingResult, selectedOutwardTicket);

			//check result is not null
			Assert.IsNotNull(updatedResult, "TestAssembleServicesRailSingle has unexpectedly returned a null CostSearchResult"); 

			//check result has no errors
			CostSearchError[] errors = updatedResult.GetErrors();
			Assert.IsTrue(errors.Length == 0, "TestAssembleServicesRailSingle has unexpectedly returned CostSearchErrors"); 

			//check that a TDJourneyResult exists
			TDJourneyResult journeyResult = (TDJourneyResult)updatedResult.GetJourneyResultsForTicket(selectedOutwardTicket);
			Assert.IsNotNull(journeyResult, "TestAssembleServicesRailSingle has unexpectedly returned a null JourneyResult for the selected ticket"); 

			//check number of public journeys in the result
			Assert.IsTrue(journeyResult.OutwardPublicJourneyCount > 0, "TestAssembleServicesRailSingle has not returned any OutwardPublicJourneys for the selected ticket"); 
			
			//there are 111 OutwardPublicJourneys in the mock result (\RailLondonCambridge20060608.xml), 
			Assert.IsTrue(journeyResult.OutwardPublicJourneyCount == 111, "TestAssembleServicesRailSingle has not returned the expected number of OutwardPublicJourneys for the selected ticket"); 
			
			//as this is a single, there should be no return journeys in the result
			Assert.IsTrue(journeyResult.ReturnPublicJourneyCount == 0, "TestAssembleServicesRailSingle has unexpectedly returned some ReturnPublicJourneys for the selected ticket"); 

		}
		/// <summary>
		/// Tests the AssembleServices method for a single coach ticket
		/// </summary>
		[Test]
		public void TestAssembleServicesCoachSingle()
		{
			//Add travel modes to base request			 
			singleCoachRequest.TravelModes = new TicketTravelMode[1];
			singleCoachRequest.TravelModes[0] = TicketTravelMode.Coach;

			//call AssembleFares so that we have an existing result
			existingResult = (CostSearchResult)costSearchFacade.AssembleFares(singleCoachRequest);		

			//pick a ticket
			TravelDate[] travelDates = existingResult.GetAllTravelDates();
			
			selectedOutwardTicket = travelDates[1].OutwardTickets[0];				

			//call AssembleServices
			ICostSearchResult updatedResult = costSearchFacade.AssembleServices(singleCoachRequest, existingResult, selectedOutwardTicket);

			//check result is not null
			Assert.IsNotNull(updatedResult, "TestAssembleServicesCoachSingle has unexpectedly returned a null CostSearchResult"); 

			//check result has no errors
			CostSearchError[] errors = updatedResult.GetErrors();
			Assert.IsTrue(errors.Length == 0, "TestAssembleServicesCoachSingle has unexpectedly returned CostSearchErrors"); 

			//check that a TDJourneyResult exists
			TDJourneyResult journeyResult = (TDJourneyResult)updatedResult.GetJourneyResultsForTicket(selectedOutwardTicket);
			Assert.IsNotNull(journeyResult, "TestAssembleServicesCoachSingle has unexpectedly returned a null JourneyResult for the selected ticket"); 
		}

		/// <summary>
		/// Tests the AssembleServices method for a return coach ticket
		/// </summary>
		[Test]
		public void TestAssembleServicesCoachReturn()
		{
			//Add travel modes to base request			 
			singleCoachRequest.TravelModes = new TicketTravelMode[1];
			singleCoachRequest.TravelModes[0] = TicketTravelMode.Coach;

			//call AssembleFares so that we have an existing result
			existingResult = (CostSearchResult)costSearchFacade.AssembleFares(singleCoachRequest);		

			//pick a ticket
			TravelDate[] travelDates = existingResult.GetAllTravelDates();
			
			selectedOutwardTicket = travelDates[0].OutwardTickets[0];				

			//call AssembleServices
			ICostSearchResult updatedResult = costSearchFacade.AssembleServices(singleCoachRequest, existingResult, selectedOutwardTicket);

			//check result is not null
			Assert.IsNotNull(updatedResult, "TestAssembleServicesCoachReturn has unexpectedly returned a null CostSearchResult"); 

			//check result has no errors
			CostSearchError[] errors = updatedResult.GetErrors();
			Assert.IsTrue(errors.Length == 0, "TestAssembleServicesRailSingle has unexpectedly returned CostSearchErrors"); 

			//check that a TDJourneyResult exists
			TDJourneyResult journeyResult = (TDJourneyResult)updatedResult.GetJourneyResultsForTicket(selectedOutwardTicket);
			Assert.IsNotNull(journeyResult, "TestAssembleServicesCoachReturn has unexpectedly returned a null JourneyResult for the selected ticket"); 

			//check number of public journeys in the result
			Assert.IsTrue(journeyResult.OutwardPublicJourneyCount > 0, "TestAssembleServicesCoachReturn has not returned any OutwardPublicJourneys for the selected ticket"); 
			
			//there are 20 OutwardPublicJourneys in the mock result (\CoachLondonCambridge20060608.xml), 
			Assert.IsTrue(journeyResult.OutwardPublicJourneyCount == 20, "TestAssembleServicesRailSingle has not returned the expected number of OutwardPublicJourneys for the selected ticket"); 
			
			//as this is a single, there should be no return journeys in the result
			Assert.IsTrue(journeyResult.ReturnPublicJourneyCount == 0, "TestAssembleServicesCoachReturn has unexpectedly returned some ReturnPublicJourneys for the selected ticket"); 

		}

		
		/// <summary>
		/// Tests the AssembleServices method for a multi mode (coach and rail) request
		/// </summary>
		[Test]
		public void TestAssembleServicesMultiMode()
		{			
			
			//Add travel modes to base request			 
			singleCoachRequest.TravelModes = new TicketTravelMode[2];
			singleCoachRequest.TravelModes[0] = TicketTravelMode.Coach;
			singleCoachRequest.TravelModes[1] = TicketTravelMode.Rail;

			existingResult = (CostSearchResult)costSearchFacade.AssembleFares(singleCoachRequest);	

			//check result is not null
			Assert.IsNotNull(existingResult, "TestAssembleServicesMultiMode has unexpectedly returned a null CostSearchResult"); 

			//pick a ticket
			TravelDate[] travelDates = existingResult.GetAllTravelDates();
			
			selectedOutwardTicket = travelDates[0].OutwardTickets[0];				

			//call AssembleServices
			ICostSearchResult updatedResult = costSearchFacade.AssembleServices(singleCoachRequest, existingResult, selectedOutwardTicket);

			//check result is not null
			Assert.IsNotNull(updatedResult, "TestAssembleServicesMultiMode has unexpectedly returned a null CostSearchResult"); 

		}

	}
}
