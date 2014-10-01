// ****************************************************************** 
// NAME			: TestMockCostSearchFacade.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 04/01/2005 
// DESCRIPTION	: Definition of the TestMockCostSearchFacade class
// This is a mock class available for UI testing purposes.
// ****************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/Test/TestMockCostSearchFacade.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 16:21:56   mturner
//Initial revision.
//
//   Rev 1.18   Dec 21 2005 17:47:58   RWilby
//Updated to fix unit test code
//
//   Rev 1.17   Nov 09 2005 12:23:50   build
//Automatically merged from branch for stream2818
//
//   Rev 1.16.1.0   Oct 28 2005 15:25:22   RWilby
//Updated for refactored CostSearchFacade
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.16   Apr 27 2005 17:02:00   jmorrissey
//Update to assembleServices methods
//Resolution for 2323: PT: Singles tickets being displayed more than once if flexibility selected
//
//   Rev 1.15   Apr 25 2005 17:55:28   jmorrissey
//Updated after changes to CostSearchFacade AssembleFares method.
//Resolution for 2290: Session data for cost based searching - coach
//
//   Rev 1.14   Apr 22 2005 11:40:38   rgeraghty
//Additional Error test conditions added to AssembleServices to test IR2066
//Resolution for 2066: PT - Train - Fare Ticket Selection - text needs to change when no tickets available
//
//   Rev 1.13   Mar 30 2005 12:26:56   jgeorge
//Corrected invalid value in test data
//
//   Rev 1.12   Mar 29 2005 09:52:38   COwczarek
//Changes to ensure consistent update of
//FindCostBasedPageState object during
//serialization/deserialization of session
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.11   Mar 22 2005 17:54:38   jmorrissey
//After initial back end integration
//
//   Rev 1.10   Mar 22 2005 10:58:16   jmorrissey
//Added overload of AssembleServices method that takes an outward and inward ticket. Not coded yet but checked in because the interface is checked in.
//
//   Rev 1.9   Mar 21 2005 09:20:56   COwczarek
//Change AssembleServices to return the same object graph modified (not new object graph)
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.8   Mar 13 2005 16:31:02   jmorrissey
//New AddTicketAndTravelDateAssociation method to associate each Ticket with its TravelDate
//
//   Rev 1.7   Mar 13 2005 16:07:30   jmorrissey
//Updated to add more test coach and rail travel data with varied dates
//
//   Rev 1.6   Mar 08 2005 11:36:18   jmorrissey
//Completed testing and FxCop.
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.5   Mar 07 2005 18:30:28   jmorrissey
//Updated after after latest testing
//
//   Rev 1.4   Mar 07 2005 09:55:54   jmorrissey
//Updated version. Still being worked on.
//
//   Rev 1.3   Feb 25 2005 13:15:38   jmorrissey
//Updated for integartion testing
//
//   Rev 1.2   Feb 22 2005 16:43:26   jmorrissey
//Change to AssembleServices signature
//
//   Rev 1.1   Feb 07 2005 15:55:10   jmorrissey
//Updated after change to TravelDate class structure
//
//   Rev 1.0   Feb 01 2005 10:59:02   jmorrissey
//Initial revision.

using System;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;


namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// Summary description for CostSearchFacade
	/// </summary>
	public class TestMockCostSearchFacade : ICostSearchFacade
	{

		private CostSearchTicket[] singleCoachTickets;
		private CostSearchTicket[] singleCoachTickets2;
		private CostSearchTicket[] singleCoachTickets3;
		private CostSearchTicket[] singleCoachTickets4;
		private CostSearchTicket[] singleCoachTickets5;
		private CostSearchTicket[] singleCoachTickets6;
		private CostSearchTicket[] singleCoachTickets7;
		private CostSearchTicket[] returnCoachTickets;
		private CostSearchTicket[] returnCoachTickets2;
		private CostSearchTicket[] returnCoachTickets3;
		private CostSearchTicket[] returnCoachTickets4;
		private CostSearchTicket[] singleRailTickets;
		private CostSearchTicket[] singleRailTickets2;
		private CostSearchTicket[] singleRailTickets3;
		private CostSearchTicket[] singleRailTickets4;
		private CostSearchTicket[] singleRailTickets5;
		private CostSearchTicket[] singleRailTickets6;
		private CostSearchTicket[] singleRailTickets7;
		private CostSearchTicket[] returnRailTickets;
		private CostSearchTicket[] returnRailTickets2;
		private CostSearchTicket[] returnRailTickets3;
		private CostSearchTicket[] returnRailTickets4;
		private CostSearchTicket[] singleAirTickets;
		private CostSearchTicket[] returnAirTickets;
		
		public TestMockCostSearchFacade()
		{
		}


		#region public methods

		/// <summary>
		/// Returns a CostSearchResult populated with dummy TravelDates and dummy CostSearchTickets
		/// </summary>
		public ICostSearchResult AssembleFares(ICostSearchRequest request)
		{
			CostSearchResult costResult = new CostSearchResult();
			//assign test fares to the results set
			costResult.ResultId = request.RequestId;
			costResult.TravelDates = GetFares();

			//return the CostSearchResult, which contains fares and coach journeys
			return costResult;
		}
		
		/// <summary>
		/// Returns a CostSearchResult populated with dummy TravelDates and dummy CostSearchTickets AND dummy journeys
		/// </summary>
		public ICostSearchResult AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket selectedTicket)
		{
			return AssembleServices(request, existingResult, selectedTicket, null);
		}	

		/// <summary>
		/// Overloaded version of AssembleServices method that takes 2 Singles tickets 
		/// </summary>
		public ICostSearchResult AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket outwardTicket, CostSearchTicket inwardTicket)
		{

			TravelDate[] allTravelDates =  ((CostSearchResult)existingResult).GetAllTravelDates();
	
			//get the tickets from the CostSearchResult
			for (int i = 0; i < allTravelDates.Length; i++)
			{
				if (allTravelDates[i].TravelMode == TicketTravelMode.Rail )
				{
					//add journey results to tickets
					if (allTravelDates[i].TicketType == TicketType.Return)
					{
						GetRailJourneys(allTravelDates[i].ReturnTickets, false);
					}
					else if (allTravelDates[i].TicketType == TicketType.Singles && inwardTicket != null)
					{
						GetRailJourneys(allTravelDates[i].OutwardTickets, true);
						GetRailJourneys(allTravelDates[i].InwardTickets, true);
					}
					else 
					{
						GetRailJourneys(allTravelDates[i].OutwardTickets, true);
					}
				}

				if (allTravelDates[i].TravelMode == TicketTravelMode.Coach )
				{
					//add journey results to tickets
					if (allTravelDates[i].TicketType == TicketType.Return)
					{
						GetCoachJourneys(allTravelDates[i].ReturnTickets, false);
					}
					else if (allTravelDates[i].TicketType == TicketType.Singles && inwardTicket != null)
					{
						GetCoachJourneys(allTravelDates[i].OutwardTickets, true);
						GetCoachJourneys(allTravelDates[i].InwardTickets, true);
					}
					else 
					{
						GetCoachJourneys(allTravelDates[i].OutwardTickets, true);
					}
				}

				if (allTravelDates[i].TravelMode ==  TicketTravelMode.Air)
				{
					//add journey results
					if (allTravelDates[i].TicketType == TicketType.Return)
					{
						GetAirJourneys(allTravelDates[i].ReturnTickets, false);
					}
					else if (allTravelDates[i].TicketType == TicketType.Singles && inwardTicket != null)
					{
						GetAirJourneys(allTravelDates[i].OutwardTickets, true);
						GetAirJourneys(allTravelDates[i].InwardTickets, true);
					}
					else 
					{
						GetAirJourneys(allTravelDates[i].OutwardTickets, true);
					}
				}
			}				
			 
			// Simulate some error cases (as defined in DD/57)

			// Error cases 3,4,5,6 for services for fare processing
			// For tickets starting with NX, no journeys or error messages returned.
			// Ticket probability set to "None".

			if (outwardTicket.Code.StartsWith("NX")) 
			{
				TravelDate travelDate = outwardTicket.TravelDateForTicket;
				switch (travelDate.TicketType) 
				{
					case TicketType.Single:
						CostSearchTicket[] singleTickets = existingResult.GetSingleTickets(travelDate.OutwardDate, travelDate.TravelMode);
						foreach (CostSearchTicket ticket in singleTickets) 
						{
							if (outwardTicket.Code == ticket.Code) 
							{
								ticket.Probability = Probability.None;
								ticket.JourneysForTicket.CostJourneyResult = new TDJourneyResult();
							}
						}
						break;					

					case TicketType.Return:
						CostSearchTicket[] returnTickets = existingResult.GetReturnTickets(travelDate.OutwardDate,travelDate.ReturnDate,travelDate.TravelMode);
						foreach (CostSearchTicket ticket in returnTickets) 
						{
							if (outwardTicket.Code == ticket.Code) 
							{
								ticket.Probability = Probability.None;
								ticket.JourneysForTicket.CostJourneyResult = new TDJourneyResult();
							}
						}
						break;
						
					case TicketType.OpenReturn:
						CostSearchTicket[] openReturnTickets = existingResult.GetOpenReturnTickets(travelDate.OutwardDate, travelDate.TravelMode);
						foreach (CostSearchTicket ticket in openReturnTickets) 
						{
							if (outwardTicket.Code == ticket.Code) 
							{
								ticket.Probability = Probability.None;
								ticket.JourneysForTicket.CostJourneyResult = new TDJourneyResult();
							}
						}
						break;	
					default:
						break;
				}
			}

			if (outwardTicket.Code.StartsWith("NX") && inwardTicket.Code.StartsWith("NX")) 
			{
				TravelDate travelDateOutward = outwardTicket.TravelDateForTicket;
				TravelDate travelDateInward = inwardTicket.TravelDateForTicket;
								
				if (travelDateOutward.TicketType == TicketType.Singles)
				{
					CostSearchTicket[] singlesTicketsOutward = existingResult.GetOutwardTickets(travelDateOutward.OutwardDate, travelDateOutward.ReturnDate, travelDateOutward.TravelMode);
					foreach (CostSearchTicket ticket in singlesTicketsOutward) 
					{
						if (outwardTicket.Code == ticket.Code) 
						{
							ticket.Probability = Probability.None;
							ticket.JourneysForTicket.CostJourneyResult = new TDJourneyResult();
						}
					}
				}
				if (travelDateInward.TicketType == TicketType.Singles)
				{
					CostSearchTicket[] singlesTicketsInward = existingResult.GetInwardTickets(travelDateInward.OutwardDate, travelDateInward.ReturnDate,travelDateInward.TravelMode);
					foreach (CostSearchTicket ticket in singlesTicketsInward) 
					{
						if (inwardTicket.Code == ticket.Code) 
						{
							ticket.Probability = Probability.None;
							ticket.JourneysForTicket.CostJourneyResult = new TDJourneyResult();
						}
					}
				}			
			}

			//return the updated CostSearchResult, 
			//now complete with coach, rail and air CostBasedJourneys 
			return existingResult;
		}

		#endregion	
	
		#region private helper methods
		/// <summary>
		/// private helper method that creates and populates some mock travel dates and tickets		
		/// </summary>
		public TravelDate[] GetFares()
		{	
			//date object representing today's date
			TDDateTime todayDate = new TDDateTime(DateTime.Today);

			//mock versions of coach tickets
			singleCoachTickets = new CostSearchTicket[]
			{
				new	CostSearchTicket("NX Standard",Flexibility.FullyFlexible,"NXS",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16,Probability.High),
				new CostSearchTicket("NX Business",Flexibility.LimitedFlexibility,"NXB",200.00f,50.00f,float.NaN,float.NaN,150.00f,25.00f,0,16,Probability.Medium),
				new CostSearchTicket("NX Cheap day single",Flexibility.LimitedFlexibility,"NXC",5.00f,1.00f,float.NaN,float.NaN,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("NX first",Flexibility.FullyFlexible,"NXF",40.00f,20.00f,float.NaN,float.NaN,30.00f,10.00f,0,16, Probability.High),
				new CostSearchTicket("NX Bank Holiday Special",Flexibility.LimitedFlexibility,"NXC",5.00f,1.00f,float.NaN,float.NaN,2.00f,1.00f,0,16,Probability.Low),
				new CostSearchTicket("NX Sunday Single",Flexibility.FullyFlexible,"NXF",40.00f,20.00f,float.NaN,float.NaN,30.00f,10.00f,0,16, Probability.High)

			};			

			singleCoachTickets2 = new CostSearchTicket[]
			{
				new	CostSearchTicket("Greenline Standard",Flexibility.FullyFlexible,"GS",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16,Probability.High),
				new CostSearchTicket("Greenline Business",Flexibility.LimitedFlexibility,"GB",200.00f,50.00f,float.NaN,float.NaN,150.00f,25.00f,0,16,Probability.Medium),
				new CostSearchTicket("Greenline Cheap day single",Flexibility.LimitedFlexibility,"GC",5.00f,1.00f,float.NaN,float.NaN,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Greenline first",Flexibility.FullyFlexible,"GF",40.00f,20.00f,float.NaN,float.NaN,30.00f,10.00f,0,16, Probability.High),
			};	
		
			singleCoachTickets3 = new CostSearchTicket[]
			{
				new	CostSearchTicket("Easybus Standard",Flexibility.FullyFlexible,"EBS",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16,Probability.High),
				new CostSearchTicket("Easybus Business",Flexibility.LimitedFlexibility,"EBB",200.00f,float.NaN,float.NaN,50.00f,150.00f,25.00f,0,16,Probability.Medium),
				new CostSearchTicket("Easybus Cheap day single",Flexibility.LimitedFlexibility,"EBC",5.00f,1.00f,float.NaN,float.NaN,2.00f,1.00f,0,16,Probability.Medium),
			};	

			singleCoachTickets4 = new CostSearchTicket[]
			{
				new	CostSearchTicket("Arriva Standard",Flexibility.FullyFlexible,"AS",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16,Probability.High),
				new CostSearchTicket("Arriva Cheap day single",Flexibility.LimitedFlexibility,"AC",5.00f,1.00f,float.NaN,float.NaN,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Arriva first",Flexibility.FullyFlexible,"AF",40.00f,20.00f,float.NaN,float.NaN,30.00f,10.00f,0,16, Probability.High),
			};					

			singleCoachTickets5 = new CostSearchTicket[]
			{
				new	CostSearchTicket("Greenline Standard",Flexibility.FullyFlexible,"GS",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16,Probability.High),
				new CostSearchTicket("Greenline Business",Flexibility.LimitedFlexibility,"GB",200.00f,50.00f,float.NaN,float.NaN,150.00f,25.00f,0,16,Probability.Medium),
				new CostSearchTicket("Greenline Cheap day single",Flexibility.LimitedFlexibility,"GC",5.00f,1.00f,float.NaN,float.NaN,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Greenline first",Flexibility.FullyFlexible,"GF",40.00f,20.00f,float.NaN,float.NaN,30.00f,10.00f,0,16, Probability.High),
			};	
		
			singleCoachTickets6 = new CostSearchTicket[]
			{
				new	CostSearchTicket("Easybus Standard",Flexibility.FullyFlexible,"EBS",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16,Probability.High),
				new CostSearchTicket("Easybus Business",Flexibility.LimitedFlexibility,"EBB",200.00f,50.00f,float.NaN,float.NaN,150.00f,25.00f,0,16,Probability.Medium),
				new CostSearchTicket("Easybus Cheap day single",Flexibility.LimitedFlexibility,"EBC",5.00f,1.00f,float.NaN,float.NaN,2.00f,1.00f,0,16,Probability.Medium),
			};	

			singleCoachTickets7 = new CostSearchTicket[]
			{
				new	CostSearchTicket("Arriva Standard",Flexibility.FullyFlexible,"AS",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16,Probability.High),
				new CostSearchTicket("Arriva Cheap day single",Flexibility.LimitedFlexibility,"AC",5.00f,1.00f,float.NaN,float.NaN,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Arriva first",Flexibility.FullyFlexible,"AF",40.00f,20.00f,float.NaN,float.NaN,30.00f,10.00f,0,16, Probability.High),
			};					


			returnCoachTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("NX Standard Return",Flexibility.FullyFlexible,"NXSR",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16, Probability.Low),
				new CostSearchTicket("NX Business Return",Flexibility.LimitedFlexibility,"NXBR",200.00f,50.00f,float.NaN,float.NaN,150.00f,25.00f,0,16,Probability.Medium),
				new CostSearchTicket("NX Cheap day return",Flexibility.LimitedFlexibility,"NXCR",5.00f,1.00f,float.NaN,float.NaN,2.00f,1.00f,0,16, Probability.Medium),
				new CostSearchTicket("NX first return",Flexibility.FullyFlexible,"NXR",40.00f,20.00f,float.NaN,float.NaN,30.00f,10.00f,0,16,Probability.High)
			};

			returnCoachTickets2 = new CostSearchTicket[]
			{
				new CostSearchTicket("Greenline Standard Return",Flexibility.FullyFlexible,"GSR",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16, Probability.Low),
				new CostSearchTicket("Greenline Business Return",Flexibility.LimitedFlexibility,"GBR",200.00f,50.00f,float.NaN,float.NaN,150.00f,25.00f,0,16,Probability.Medium),
				new CostSearchTicket("Greenline Cheap day return",Flexibility.LimitedFlexibility,"GCR",5.00f,1.00f,float.NaN,float.NaN,2.00f,1.00f,0,16, Probability.Medium),
				new CostSearchTicket("Greenline first return",Flexibility.FullyFlexible,"GR",40.00f,20.00f,float.NaN,float.NaN,30.00f,10.00f,0,16,Probability.High)
			};


			returnCoachTickets3 = new CostSearchTicket[]
			{
				new CostSearchTicket("Easybus Standard Return",Flexibility.FullyFlexible,"EBSR",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16, Probability.Low),
				new CostSearchTicket("Easybus Business Return",Flexibility.LimitedFlexibility,"EBBR",200.00f,50.00f,float.NaN,float.NaN,150.00f,25.00f,0,16,Probability.Medium),
				new CostSearchTicket("Easybus Cheap day return",Flexibility.LimitedFlexibility,"EBCR",5.00f,1.00f,float.NaN,float.NaN,2.00f,1.00f,0,16, Probability.Medium),
			};

			returnCoachTickets4 = new CostSearchTicket[]
			{
				new CostSearchTicket("Arriva Standard Return",Flexibility.FullyFlexible,"NXSR",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16, Probability.Low),
				new CostSearchTicket("Arriva Cheap day return",Flexibility.LimitedFlexibility,"NXCR",5.00f,1.00f,float.NaN,float.NaN,2.00f,1.00f,0,16, Probability.Medium),
				new CostSearchTicket("Arriva first return",Flexibility.FullyFlexible,"NXR",40.00f,20.00f,float.NaN,float.NaN,30.00f,10.00f,0,16,Probability.High)
			};


			//for coach mode only, add the journeys (other modes get journeys added through AssembleServices) 
			GetCoachJourneys(singleCoachTickets, true);
			GetCoachJourneys(singleCoachTickets2, true);
			GetCoachJourneys(singleCoachTickets3, true);
			GetCoachJourneys(singleCoachTickets4, true);
			GetCoachJourneys(singleCoachTickets5, false);
			GetCoachJourneys(singleCoachTickets6, false);
			GetCoachJourneys(singleCoachTickets7, false);
			GetCoachJourneys(returnCoachTickets, false);
			GetCoachJourneys(returnCoachTickets2, false);
			GetCoachJourneys(returnCoachTickets3, false);
			GetCoachJourneys(returnCoachTickets4, false);

			//mock coach travel dates
			TravelDate coachDate1 = new TravelDate(1, todayDate,todayDate,TicketTravelMode.Coach,10.00f,50.00f,true);
			coachDate1.TicketType = TicketType.Single;
			coachDate1.OutwardTickets = singleCoachTickets;				

			TravelDate coachDate2 = new TravelDate(2, todayDate.AddDays(1),TicketTravelMode.Coach,10.00f,50.00f,true);
			coachDate2.TicketType = TicketType.Single;
			coachDate2.OutwardTickets = singleCoachTickets2;

			TravelDate coachDate3 = new TravelDate(3, todayDate.AddDays(2),TicketTravelMode.Coach,10.00f,50.00f,true);
			coachDate3.TicketType = TicketType.Single;
			coachDate3.OutwardTickets = singleCoachTickets3;

			TravelDate coachDate4 = new TravelDate(4, todayDate.AddDays(3),TicketTravelMode.Coach,10.00f,50.00f,true);
			coachDate4.TicketType = TicketType.Single;
			coachDate4.OutwardTickets = singleCoachTickets4;
		    
			TravelDate coachDate5 = new TravelDate(5, todayDate, todayDate.AddDays(2),TicketTravelMode.Coach,5.00f,10.00f,true);
			coachDate5.TicketType = TicketType.Singles;
			coachDate5.OutwardTickets = singleCoachTickets5;
			coachDate5.InwardTickets = singleCoachTickets6;

			TravelDate coachDate6 = new TravelDate(6, todayDate,todayDate,TicketTravelMode.Coach,10.00f,50.00f,true);
			coachDate6.TicketType = TicketType.OpenReturn;
			coachDate6.OutwardTickets = singleCoachTickets7;	
		
			TravelDate coachDate7 = new TravelDate(7, todayDate, todayDate.AddDays(2),TicketTravelMode.Coach,10.00f,50.00f,true);
			coachDate7.TicketType = TicketType.Return;
			coachDate7.ReturnTickets = returnCoachTickets;

			TravelDate coachDate8 = new TravelDate(8, todayDate.AddDays(1), todayDate.AddDays(3),TicketTravelMode.Coach,10.00f,50.00f,true);
			coachDate8.TicketType = TicketType.Return;
			coachDate8.ReturnTickets = returnCoachTickets2;

			TravelDate coachDate9 = new TravelDate(9, todayDate.AddDays(2), todayDate.AddDays(4),TicketTravelMode.Coach,10.00f,50.00f,true);
			coachDate9.TicketType = TicketType.Return;
			coachDate9.ReturnTickets = returnCoachTickets3;

			TravelDate coachDate10 = new TravelDate(10, todayDate.AddDays(3), todayDate.AddDays(5),TicketTravelMode.Coach,10.00f,50.00f,true);
			coachDate10.TicketType = TicketType.Return;
			coachDate10.ReturnTickets = returnCoachTickets4;

			//mock versions of rail tickets
			singleRailTickets = new CostSearchTicket[]
			{		
				new CostSearchTicket("Apex Single /any rail",Flexibility.FullyFlexible,"Apex",27.10f,15.00f, float.NaN, float.NaN, 40.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("7 day advance",Flexibility.NoFlexibility,"7day",20.00f,10.00f, float.NaN, float.NaN, 5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Cheap day single /any rail",Flexibility.LimitedFlexibility,"CDS",5.00f,1.00f, float.NaN, float.NaN, 2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Standard first /any rail",Flexibility.FullyFlexible,"STF",40.00f,20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16,Probability.High)
			};		

			singleRailTickets2 = new CostSearchTicket[]
			{		
				new CostSearchTicket("Midland Mainline Apex Single",Flexibility.NoFlexibility,"MMApex",27.10f,15.00f, float.NaN, float.NaN, 40.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Midland Mainline 7 day advance",Flexibility.NoFlexibility,"MM7day",20.00f,10.00f, float.NaN, float.NaN, 5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Midland Mainline Cheap day single",Flexibility.LimitedFlexibility,"MMCDS",5.00f,1.00f, float.NaN, float.NaN, 2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Midland Mainline Standard first",Flexibility.FullyFlexible,"MMSF",40.00f,20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16,Probability.High)
			};	

			singleRailTickets3 = new CostSearchTicket[]
			{		
				new CostSearchTicket("Virgin Apex Single",Flexibility.LimitedFlexibility,"VApex",27.10f,15.00f, float.NaN, float.NaN, 40.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Virgin 7 day advance",Flexibility.LimitedFlexibility,"V7day",20.00f,10.00f, float.NaN, float.NaN, 5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Virgin Cheap day single",Flexibility.LimitedFlexibility,"VCDS",5.00f,1.00f, float.NaN, float.NaN, 2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Virgin first",Flexibility.FullyFlexible,"VSF",40.00f,20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16,Probability.High)
			};	

			singleRailTickets4 = new CostSearchTicket[]
			{		
				new CostSearchTicket("GNER Single",Flexibility.FullyFlexible,"GNERApex",27.10f,15.00f, float.NaN, float.NaN, 40.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("GNER 7 day advance",Flexibility.NoFlexibility,"GNER7day",20.00f,10.00f,float.NaN, float.NaN, 5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("GNER Cheap day single",Flexibility.LimitedFlexibility,"GNERCDS",5.00f,1.00f,float.NaN, float.NaN, 2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("GNER Standard first",Flexibility.FullyFlexible,"GNERSTF",40.00f,20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16,Probability.High)
			};	

			singleRailTickets5 = new CostSearchTicket[]
			{		
				new CostSearchTicket("GNER Single",Flexibility.FullyFlexible,"GNERApex",27.10f,15.00f,float.NaN, float.NaN, 40.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("GNER 7 day advance",Flexibility.NoFlexibility,"GNER7day",20.00f,10.00f,float.NaN, float.NaN,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("GNER Cheap day single",Flexibility.LimitedFlexibility,"GNERCDS",5.00f,1.00f,float.NaN, float.NaN,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("GNER Standard first",Flexibility.FullyFlexible,"GNERSTF",40.00f,20.00f,float.NaN, float.NaN,30.00f,10.00f,0,16,Probability.High)
			};	

			singleRailTickets6 = new CostSearchTicket[]
			{		
				new CostSearchTicket("Virgin Apex Single",Flexibility.LimitedFlexibility,"VApex",27.10f,15.00f,float.NaN, float.NaN,40.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Virgin 7 day advance",Flexibility.LimitedFlexibility,"V7day",20.00f,10.00f,float.NaN, float.NaN,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Virgin Cheap day single",Flexibility.LimitedFlexibility,"VCDS",5.00f,1.00f,float.NaN, float.NaN,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Virgin first",Flexibility.FullyFlexible,"VSF",40.00f,20.00f,float.NaN, float.NaN,30.00f,10.00f,0,16,Probability.High)
			};	

			singleRailTickets7 = new CostSearchTicket[]
			{		
				new CostSearchTicket("GNER Single",Flexibility.FullyFlexible,"GNERApex",27.10f,15.00f,float.NaN, float.NaN,40.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("GNER 7 day advance",Flexibility.NoFlexibility,"GNER7day",20.00f,10.00f,float.NaN, float.NaN,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("GNER Cheap day single",Flexibility.LimitedFlexibility,"GNERCDS",5.00f,1.00f,float.NaN, float.NaN,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("GNER Standard first",Flexibility.FullyFlexible,"GNERSTF",40.00f,20.00f,float.NaN, float.NaN,30.00f,10.00f,0,16,Probability.High)
			};	

			returnRailTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("Apex Return /any rail",Flexibility.NoFlexibility,"Apex",50.00f,10.00f,float.NaN, float.NaN,25.00f,7.00f,0,16,Probability.Low),
				new CostSearchTicket("7 day advance return/ Virgin",Flexibility.NoFlexibility,"7day",20.00f,10.00f,float.NaN, float.NaN,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Cheap day return /any rail",Flexibility.LimitedFlexibility,"CDR",5.00f,1.00f,float.NaN, float.NaN,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Standard return /any rail",Flexibility.FullyFlexible,"SR",40.00f,20.00f,float.NaN, float.NaN,30.00f,10.00f,0,16,Probability.High)
			};

			returnRailTickets2 = new CostSearchTicket[]
			{
				new CostSearchTicket("Midland Mainline Apex Return",Flexibility.NoFlexibility,"Apex",50.00f,10.00f,float.NaN, float.NaN,25.00f,7.00f,0,16,Probability.Low),
				new CostSearchTicket("Midland Mainline 7 day advance return",Flexibility.NoFlexibility,"7day",20.00f,10.00f,float.NaN, float.NaN,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Midland Mainline Cheap day return",Flexibility.LimitedFlexibility,"CDR",5.00f,1.00f,float.NaN, float.NaN,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Midland Mainline Standard return",Flexibility.FullyFlexible,"SR",40.00f,20.00f,float.NaN, float.NaN,30.00f,10.00f,0,16,Probability.High)
			};

			returnRailTickets3 = new CostSearchTicket[]
			{
				new CostSearchTicket("Virgin Apex Return",Flexibility.NoFlexibility,"Apex",50.00f,10.00f,float.NaN, float.NaN,25.00f,7.00f,0,16,Probability.Low),
				new CostSearchTicket("Virgin 7 day advance return",Flexibility.NoFlexibility,"7day",20.00f,10.00f,float.NaN, float.NaN,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Virgin Cheap day return",Flexibility.LimitedFlexibility,"CDR",5.00f,1.00f,float.NaN, float.NaN,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Virgin Standard return",Flexibility.FullyFlexible,"SR",40.00f,20.00f,30.00f,float.NaN, float.NaN,10.00f,0,16,Probability.High)
			};

			returnRailTickets4 = new CostSearchTicket[]
			{
				new CostSearchTicket("GNER Apex Return",Flexibility.NoFlexibility,"Apex",50.00f,10.00f,float.NaN, float.NaN,25.00f,7.00f,0,16,Probability.Low),
				new CostSearchTicket("GNER 7 day advance return",Flexibility.NoFlexibility,"7day",20.00f,10.00f,float.NaN, float.NaN,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("GNER Cheap day return",Flexibility.LimitedFlexibility,"CDR",5.00f,1.00f,float.NaN, float.NaN,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("GNER Standard return",Flexibility.FullyFlexible,"SR",40.00f,20.00f,float.NaN, float.NaN,30.00f,10.00f,0,16,Probability.High)
			};

			//mock rail travel dates
			TravelDate railDate1 = new TravelDate(101, todayDate,todayDate,TicketTravelMode.Rail,10.00f,50.00f,true);
			railDate1.TicketType = TicketType.Single;
			railDate1.OutwardTickets = singleRailTickets;	
			
			TravelDate railDate2 = new TravelDate(102, todayDate.AddDays(1),TicketTravelMode.Rail,10.00f,50.00f,true);
			railDate2.TicketType = TicketType.Single;
			railDate2.OutwardTickets = singleRailTickets2;	

			TravelDate railDate3 = new TravelDate(103, todayDate.AddDays(2),TicketTravelMode.Rail,10.00f,50.00f,true);
			railDate3.TicketType = TicketType.Single;
			railDate3.OutwardTickets = singleRailTickets3;	

			TravelDate railDate4 = new TravelDate(104, todayDate.AddDays(3),TicketTravelMode.Rail,10.00f,50.00f,true);
			railDate4.TicketType = TicketType.Single;
			railDate4.OutwardTickets = singleRailTickets4;	

			TravelDate railDate5 = new TravelDate(105, todayDate.AddDays(1), todayDate.AddDays(2),TicketTravelMode.Rail,3.00f,10.00f,true);
			railDate5.TicketType = TicketType.Singles;
			railDate5.OutwardTickets = singleRailTickets5;
			railDate5.InwardTickets = singleRailTickets6;			

			TravelDate railDate6 = new TravelDate(106, todayDate,todayDate, TicketTravelMode.Rail,1.00f,5.00f,true);
			railDate6.TicketType = TicketType.OpenReturn;
			railDate6.OutwardTickets = singleRailTickets7;

			TravelDate railDate7 = new TravelDate(107, todayDate.AddDays(1), todayDate.AddDays(2),TicketTravelMode.Rail,1.00f,5.00f,true);
			railDate7.TicketType = TicketType.Return;
			railDate7.ReturnTickets = returnRailTickets;

			TravelDate railDate8 = new TravelDate(108, todayDate.AddDays(1), todayDate.AddDays(3),TicketTravelMode.Rail,1.00f,5.00f,true);
			railDate8.TicketType = TicketType.Return;
			railDate8.ReturnTickets = returnRailTickets2;

			TravelDate railDate9 = new TravelDate(109, todayDate.AddDays(1), todayDate.AddDays(4),TicketTravelMode.Rail,1.00f,5.00f,true);
			railDate9.TicketType = TicketType.Return;
			railDate9.ReturnTickets = returnRailTickets3;

			TravelDate railDate10 = new TravelDate(110, todayDate.AddDays(1), todayDate.AddDays(5),TicketTravelMode.Rail,1.00f,5.00f,true);
			railDate10.TicketType = TicketType.Return;
			railDate10.ReturnTickets = returnRailTickets4;


			//mock versions of air tickets
			singleAirTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("BA Economy",Flexibility.NoFlexibility,"Econ",50.00f,10.00f,float.NaN,float.NaN, 25.00f,7.00f,0,16,Probability.Low),
				new CostSearchTicket("BA Business",Flexibility.LimitedFlexibility,"Busi",200.00f,10.00f,float.NaN,float.NaN,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("BA First Class",Flexibility.FullyFlexible,"First",1000.00f,500.00f,float.NaN,float.NaN,400.00f,250.00f,0,16,Probability.Medium),
				new CostSearchTicket("BA Cattle Class",Flexibility.LimitedFlexibility,"Cattle",0.99f,0.49f,float.NaN,float.NaN,0.79f,0.09f,0,16,Probability.High)
			};

			returnAirTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("BA Economy Return",Flexibility.NoFlexibility,"Econ",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16,Probability.Low),
				new CostSearchTicket("BA Business Return",Flexibility.NoFlexibility,"Busi",200.00f,10.00f,float.NaN,float.NaN,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("BA First Class Return",Flexibility.NoFlexibility,"First",1000.00f,500.00f,float.NaN,float.NaN,400.00f,250.00f,0,16,Probability.Medium),
				new CostSearchTicket("Easyjet Cattle Class Return",Flexibility.NoFlexibility,"Cattle",0.99f,0.49f,float.NaN,float.NaN,0.79f,0.09f,0,16,Probability.High),
				new CostSearchTicket("Ryanair Luxury Class",Flexibility.LimitedFlexibility,"Silver Service",1000.00f,500.00f,float.NaN,float.NaN,100.00f,50.00f,0,16,Probability.Low)
			};		
	
			//mock air travel dates
			TravelDate airDate1 = new TravelDate( 201, todayDate, todayDate,TicketTravelMode.Air,10.00f,50.00f,true);
			airDate1.TicketType = TicketType.Single;
			airDate1.OutwardTickets = singleAirTickets;			

			TravelDate airDate2 = new TravelDate(202, todayDate,todayDate.AddDays(7),TicketTravelMode.Air,7.50f,10.00f,true);
			airDate2.TicketType = TicketType.Singles;
			airDate2.OutwardTickets = singleAirTickets;
			airDate2.InwardTickets = singleAirTickets;

			TravelDate airDate3 = new TravelDate(203, todayDate, todayDate.AddDays(2),TicketTravelMode.Air,100.00f,50.00f,true);
			airDate3.TicketType = TicketType.Return;
			airDate3.ReturnTickets = returnAirTickets;

			TravelDate airDate4 = new TravelDate(204, todayDate,todayDate,TicketTravelMode.Air,100.00f,50.00f,true);
			airDate4.TicketType = TicketType.OpenReturn;
			airDate4.OutwardTickets = returnAirTickets;
	
			//assign mock travel dates to results set
			TravelDate[] travelDates = new TravelDate[]
			{
				coachDate1, coachDate2, coachDate3, coachDate4,
				coachDate5, coachDate6, coachDate7, coachDate8,
				coachDate9, coachDate10, 
				railDate1, railDate2, railDate3, railDate4,
				railDate5, railDate6, railDate7, railDate8,
				railDate9, railDate10,
				airDate1, airDate2, airDate3, airDate4
			};	

			//add bidirectional assocation between ticket and travel date
			AddTicketAndTravelDateAssociation(travelDates);

			return travelDates;
		}

		/// <summary>
		/// helper method that populates coach journeys
		/// </summary>
		private void GetCoachJourneys(CostSearchTicket[] coachTickets, bool single)
		{			
			//for each coach ticket...
			for (int i = 0; i < coachTickets.Length; i++)
			{
				//assign CostBasedJourney	 
				coachTickets[i].JourneysForTicket = new CostBasedJourney();					
				
				if (single)
				{
					//populate journeyRequest
					coachTickets[i].JourneysForTicket.CostJourneyRequest = CreateJourneyRequest(TicketTravelMode.Coach, false);
					//dummy single result
					coachTickets[i].JourneysForTicket.CostJourneyResult = TestSampleJourneyData.NatExResultDovNot;
				}
				else
				{
					//populate journeyRequest
					coachTickets[i].JourneysForTicket.CostJourneyRequest = CreateJourneyRequest(TicketTravelMode.Coach, true);

					//dummy return result
					coachTickets[i].JourneysForTicket.CostJourneyResult = TestSampleJourneyData.NatExResultDovNotDov;
				}						
			}			
		}

		/// <summary>
		/// helper method that populates rail journeys
		/// </summary>
		private void GetRailJourneys(CostSearchTicket[] railTickets, bool single)
		{			
			//for each rail ticket...
			for (int i = 0; i < railTickets.Length; i++)
			{				
				//assign CostBasedJourney
				railTickets[i].JourneysForTicket = new CostBasedJourney();					

				if (single)
				{
					//populate journeyRequest
					railTickets[i].JourneysForTicket.CostJourneyRequest = CreateJourneyRequest(TicketTravelMode.Rail, false);

					//dummy single result
					railTickets[i].JourneysForTicket.CostJourneyResult = TestSampleJourneyData.SingleTrainResultDovNotDov;
				}
				else
				{
					//populate journeyRequest
					railTickets[i].JourneysForTicket.CostJourneyRequest = CreateJourneyRequest(TicketTravelMode.Rail, true);

					//dummy return result
					railTickets[i].JourneysForTicket.CostJourneyResult = TestSampleJourneyData.TrainResultDovNotDov;
				}
						
			}
		}

		/// <summary>
		/// helper method that populates air journeys
		/// </summary>
		private void GetAirJourneys(CostSearchTicket[] airTickets, bool single)
		{
			//for each air ticket...
			for (int i = 0; i < airTickets.Length; i ++)
			{				
				//assign CostBasedJourney
				airTickets[i].JourneysForTicket = new CostBasedJourney();					
				
				if (single)
				{
					//populate journeyRequest
					airTickets[i].JourneysForTicket.CostJourneyRequest = CreateJourneyRequest(TicketTravelMode.Air, false);

					//dummy single result
					airTickets[i].JourneysForTicket.CostJourneyResult = TestSampleJourneyData.SingleAirJourney;
				}
				else
				{
					//populate journeyRequest
					airTickets[i].JourneysForTicket.CostJourneyRequest = CreateJourneyRequest(TicketTravelMode.Air, false);

					//dummy return result
					airTickets[i].JourneysForTicket.CostJourneyResult = TestSampleJourneyData.ReturnAirJourney;
				}
						
			}
		}

		/// <summary>
		/// Adds an association between each ticket and its travel date
		/// </summary>
		/// <param name="travelDate"></param>
		private void AddTicketAndTravelDateAssociation(TravelDate[] allTravelDates)
		{

			//step through all travel dates
			foreach (TravelDate thisTravelDate in allTravelDates)
			{
				//link all outward tickets
				if (thisTravelDate.OutwardTickets != null)
				{
					foreach (CostSearchTicket ticket in thisTravelDate.OutwardTickets)
					{
						ticket.TravelDateForTicket = thisTravelDate;
					}
				}

				//link all outward tickets
				if (thisTravelDate.InwardTickets != null)
				{
					foreach (CostSearchTicket ticket in thisTravelDate.InwardTickets)
					{
						ticket.TravelDateForTicket = thisTravelDate;
					}
				}


				//link all outward tickets
				if (thisTravelDate.ReturnTickets != null)
				{
					foreach (CostSearchTicket ticket in thisTravelDate.ReturnTickets)
					{
						ticket.TravelDateForTicket = thisTravelDate;
					}
				}
			}
		}

		/// <summary>
		/// Creates and returns a dummy TDJourneyRequest, based on mode and if single/return
		/// </summary>
		/// <returns></returns>
		private TDJourneyRequest CreateJourneyRequest(TicketTravelMode mode, bool isReturn)
		{
			TDJourneyRequest request = new TDJourneyRequest();
			DateTime timeNow = new DateTime();			
			timeNow = DateTime.Now;
			
			request.OutwardArriveBefore = false;			
			request.OutwardDateTime = new TDDateTime[1];
			request.OutwardDateTime[0] = new TDDateTime( timeNow );			
			request.InterchangeSpeed = 0;
			request.WalkingSpeed = 2;
			request.MaxWalkingTime = 3;
			request.DrivingSpeed = 4;					
			
			//add specific info for return requests
			if (isReturn)
			{
				request.IsReturnRequired = true;
				request.ReturnDateTime = new TDDateTime[1];
				request.ReturnDateTime[0] = new TDDateTime( timeNow.AddDays(1) );
				request.ReturnArriveBefore = true;
			}

			//add mode specific info
			switch (mode)
			{
				case TicketTravelMode.Coach :

					request.Modes = new ModeType[1];
					request.Modes[0] = ModeType.Coach;	
			
					//set up origin location			
					request.OriginLocation = new TDLocation();
					request.OriginLocation.Status = TDLocationStatus.Valid;
					request.OriginLocation.NaPTANs = new TDNaptan[3];
					//add naptans
					request.OriginLocation.NaPTANs[0] = new TDNaptan("9000origin1", new OSGridReference(10,10));
					request.OriginLocation.NaPTANs[1] = new TDNaptan("9000origin2", new OSGridReference(100,100));
					request.OriginLocation.NaPTANs[2] = new TDNaptan("9000origin3", new OSGridReference(1000,1000));
					request.OriginLocation.Locality = "locality";			
			
					//set up destination location
					request.DestinationLocation = new TDLocation();
					request.DestinationLocation.Status = TDLocationStatus.Valid;
					request.DestinationLocation.NaPTANs = new TDNaptan[3];
					//add naptans
					request.DestinationLocation.NaPTANs[0] = new TDNaptan("9000dest1", new OSGridReference(20,20));
					request.DestinationLocation.NaPTANs[1] = new TDNaptan("9100dest2", new OSGridReference(200,200));
					request.DestinationLocation.NaPTANs[2] = new TDNaptan("9200dest3", new OSGridReference(2000,2000));
					request.DestinationLocation.Locality = "locality";	
		
					break;


				case TicketTravelMode.Rail :

					request.Modes = new ModeType[1];
					request.Modes[0] = ModeType.Rail;	

					//set up origin location			
					request.OriginLocation = new TDLocation();
					request.OriginLocation.Status = TDLocationStatus.Valid;
					request.OriginLocation.NaPTANs = new TDNaptan[3];
					//add naptans
					request.OriginLocation.NaPTANs[0] = new TDNaptan("9100origin1", new OSGridReference(10,10));
					request.OriginLocation.NaPTANs[1] = new TDNaptan("9100origin2", new OSGridReference(100,100));
					request.OriginLocation.NaPTANs[2] = new TDNaptan("9100origin3", new OSGridReference(1000,1000));
					request.OriginLocation.Locality = "locality";			
			
					//set up destination location
					request.DestinationLocation = new TDLocation();
					request.DestinationLocation.Status = TDLocationStatus.Valid;
					request.DestinationLocation.NaPTANs = new TDNaptan[3];
					//add naptans
					request.DestinationLocation.NaPTANs[0] = new TDNaptan("9100dest1", new OSGridReference(20,20));
					request.DestinationLocation.NaPTANs[1] = new TDNaptan("9100dest2", new OSGridReference(200,200));
					request.DestinationLocation.NaPTANs[2] = new TDNaptan("9100dest3", new OSGridReference(2000,2000));
					request.DestinationLocation.Locality = "locality";	
		
					break;

				case TicketTravelMode.Air :

					request.Modes = new ModeType[1];
					request.Modes[0] = ModeType.Air;	

					//set up origin location			
					request.OriginLocation = new TDLocation();
					request.OriginLocation.Status = TDLocationStatus.Valid;
					request.OriginLocation.NaPTANs = new TDNaptan[3];
					//add naptans
					request.OriginLocation.NaPTANs[0] = new TDNaptan("9200origin1", new OSGridReference(10,10));
					request.OriginLocation.NaPTANs[1] = new TDNaptan("9200origin2", new OSGridReference(100,100));
					request.OriginLocation.NaPTANs[2] = new TDNaptan("9200origin3", new OSGridReference(1000,1000));
					request.OriginLocation.Locality = "locality";			
			
					//set up destination location
					request.DestinationLocation = new TDLocation();
					request.DestinationLocation.Status = TDLocationStatus.Valid;
					request.DestinationLocation.NaPTANs = new TDNaptan[3];
					//add naptans
					request.DestinationLocation.NaPTANs[0] = new TDNaptan("9200dest1", new OSGridReference(20,20));
					request.DestinationLocation.NaPTANs[1] = new TDNaptan("9200dest2", new OSGridReference(200,200));
					request.DestinationLocation.NaPTANs[2] = new TDNaptan("9200dest3", new OSGridReference(2000,2000));
					request.DestinationLocation.Locality = "locality";		

					break;

				default:

					//set up origin location			
					request.OriginLocation = new TDLocation();
					request.OriginLocation.Status = TDLocationStatus.Valid;
					request.OriginLocation.NaPTANs = new TDNaptan[3];
					//coach naptan
					request.OriginLocation.NaPTANs[0] = new TDNaptan("9000origin", new OSGridReference(10,10));
					//rail naptan
					request.OriginLocation.NaPTANs[1] = new TDNaptan("9100origin", new OSGridReference(100,100));
					//air naptan
					request.OriginLocation.NaPTANs[2] = new TDNaptan("9200origin", new OSGridReference(1000,1000));
					request.OriginLocation.Locality = "locality";			
			
					//set up destination location
					request.DestinationLocation = new TDLocation();
					request.DestinationLocation.Status = TDLocationStatus.Valid;
					request.DestinationLocation.NaPTANs = new TDNaptan[3];
					//coach naptan
					request.DestinationLocation.NaPTANs[0] = new TDNaptan("9000dest", new OSGridReference(20,20));
					//rail naptan
					request.DestinationLocation.NaPTANs[1] = new TDNaptan("9100dest", new OSGridReference(200,200));
					//air naptan
					request.DestinationLocation.NaPTANs[2] = new TDNaptan("9200dest", new OSGridReference(2000,2000));
					request.DestinationLocation.Locality = "locality";	
		
					break;
			}

			//return the dummy request
			return request;

		}

		#endregion
		
	}
}
