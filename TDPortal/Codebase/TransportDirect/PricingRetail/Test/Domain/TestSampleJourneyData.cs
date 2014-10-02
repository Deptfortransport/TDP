
using System;
using System.Collections;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Sample data used for Pricing and Retail test harnessses
	/// </summary>
	public sealed class TestSampleJourneyData
	{
		#region class attributes
		// *** TDJourneyResults for initialising ItineraryAdapters
		// Train only JourneyResult
		private static TDJourneyResult trainResultDovNotDov;
		// NatEx only single JourneyResult
		private static TDJourneyResult natExResultDovNot;
		// NatEx only return JourneyResult
		private static TDJourneyResult natExResultDovNotDov;
		// Mixed JourneyResult
		private static TDJourneyResult mixedResultDovNotDov;
        // Train only JourneyResult (single)
        private static TDJourneyResult singleTrainResultDovNotDov;
        // Multiple journeys (return)
        private static TDJourneyResult multiJourney;
        // Multiple journeys (single)
        private static TDJourneyResult multiJourneySingle;
		// Walking JourneyResult
		private static TDJourneyResult walkJourney;
		// single Air JourneyResult
		private static TDJourneyResult singleAirTDJourneyResult;
		// return Air JourneyResult
		private static TDJourneyResult returnAirTDJourneyResult;

		// *** Journey objects for initialising Itineraries ***
		// train Journey
		private static Journey trainDovNot; 
		private static  Journey trainNotDov; 
		// NatEx Journey
		private static Journey natExDovNot; 
		private static  Journey natExNotDov; 
		// SCL Journey
		private static  Journey sclDovVic;
		private static  Journey sclStpDov; 
		// Mixed Journey
		private static  Journey mixedDovNot;
		private static Journey mixedNotDov;
		// Diff Journey
		private static  Journey diffDovNot;
		// Walking Journey
		private static Journey walkNot;
		// Air Journey
		private static Journey journeyAirLhrGla;
		// Air Journey
		private static Journey journeyAirGlaLhr;

		// *** JourneyDetail objects for initialising PricingUnits ***
		//Train PublicJourneyDetails
		private static PublicJourneyDetail  trainPJDDovVic; 
		private static PublicJourneyDetail  trainPJDVicDov;
		private static PublicJourneyDetail  trainPJDStpNot;
		private static PublicJourneyDetail  trainPJDNotStp;
		//NatEx PublicJourneyDetails
		private static PublicJourneyDetail  natExPJDDovVic;
		private static PublicJourneyDetail  natExPJDVicDov; 
		private static PublicJourneyDetail  natExPJDVicNot; 
		private static PublicJourneyDetail  natExPJDNotVic;
		private static PublicJourneyDetail  natExPJDNotStp;
		// SCL PublicJourneyDetails
		private static PublicJourneyDetail  sclPJDDovVic;
		private static PublicJourneyDetail  sclPJDStpDov; 
		// coach PublicJourneyDetail
		private static PublicJourneyDetail  coachPJDNotStp; 
		// underground PublicJourneyDetail
		private static PublicJourneyDetail  underPJDVicStp; 
		private static PublicJourneyDetail  underPJDStpVic;
		// other mode PublicJourneyDetail
		private static PublicJourneyDetail  diffPJDDovNot; 
		// walking PublicJourneyDetail
		private static PublicJourneyDetail walkPJDNot;
        // rail replacement PublicJourneyDetail
        private static PublicJourneyDetail railReplacementPDJDovVic;
		// air PublicJourneyDetail
		private static PublicJourneyDetail journeyDetailAirLHRtoGLA;
		// air PublicJourneyDetail
		private static PublicJourneyDetail journeyDetailAirGLAtoLHR;

		// *** TicketDto objects for simulating RBO responses ***
		private static PricingResultDto pricingResultDto;
        private static PricingResult[] pricingResult;
		private static TicketDto singleUndiscountedStandard;
		private static TicketDto singleDiscountedStandard;
		private static TicketDto singleUndiscountedStandardCostSearch;
		private static TicketDto singleDiscountedStandardCostSearch;
		private static TicketDto returnUndiscountedStandard;
		private static TicketDto returnDiscountedStandard;
		private static TicketDto returnUndiscountedFirst;
		private static TicketDto returnDiscountedFirst;
		private static TicketDto returnFirstNoDiscounts;
		private static TicketDto returnUndiscountedNoChild;
		private static TicketDto returnNoFares;
		private static TicketDto anotherSingleUndiscountedStandard;

		// *** Sample coach data
		private static TransportDirect.JourneyPlanning.CJPInterface.PricingUnit natExFaresDovNot;
		private static TransportDirect.JourneyPlanning.CJPInterface.PricingUnit natExFaresNotDov;
		#endregion
		
		#region Private constructor
		private TestSampleJourneyData()
		{
		}
		#endregion

		#region Properties returning TDJourneyResults
		public static TDJourneyResult TrainResultDovNotDov
		{
			get {return trainResultDovNotDov;}
		}

		public static TDJourneyResult NatExResultDovNot
		{
			get {return natExResultDovNot;}
		}

		public static TDJourneyResult NatExResultDovNotDov
		{
			get {return natExResultDovNotDov;}
		}

		public static TDJourneyResult MixedResultDovNotDov
		{
			get {return mixedResultDovNotDov;}
		}

        public static TDJourneyResult SingleTrainResultDovNotDov
        {
            get {return singleTrainResultDovNotDov;}
        }

        public static TDJourneyResult MultiJourney
        {
            get {return multiJourney;}
        }

        public static TDJourneyResult MultiJourneySingle
        {
            get {return multiJourneySingle;}
        }
		public static TDJourneyResult SingleAirJourney
		{
			get {return singleAirTDJourneyResult;}
		}
		public static TDJourneyResult ReturnAirJourney
		{
			get {return returnAirTDJourneyResult;}
		}


        #endregion

		#region Properties returning PublicJourneys
			/**************
			Public Journeys
			These journeys  are created below:

			Dover	Ldn Victoria	Ldn St Pancras	Nottingham	Home Mancester
		
			--------------------------------------->
			trainDovNot (uses trainPJDDovVic, underPJDVicStp, trainPJDStpNot)
			<---------------------------------------
			trainNotDov (uses trainPJDNotStp, underPJDStpVic, trainPJDVicDov)
		
			--------------------------------------->
			natExDovNot (uses natExPJDDovVic,natExPJDVicNot)
			<---------------------------------------
			natExNotDov (uses natExPJDNotVic,  natExPJDVicDov)		
		
			------->
			sclDovVic(uses sclPJDDovVic)
			<-----------------------
			sclStpDov (uses sclPJDStpDov)	
		
			--------------------------------------->
			mixedDovNot (sclPJDDovVic, natExVicNot)
			<---------------------------------------
			mixedNotDov (uses coachPJDNotStp, underPJDStpVic,trainPJDVicDov)	
			--------------------------------------->
			
			--------------------------------------->
			diffPJDDovNot
									
			                                        ------------->
													walkNot

			**************/

			// Train Journey 
			public static Journey TrainDovNot
		{
			get {return trainDovNot;}
		}

		// Train Journey 
		public static Journey TrainNotDov
		{
			get {return trainNotDov;}
		}

		// NatEx Journey
		public static Journey NatExDovNot
		{
			get {return natExDovNot;}
		}

		// NatEx Journey
		public static Journey NatExNotDov
		{
			get {return natExNotDov;}
		}

		// SCL Journey
		public static Journey SCLDovVic
		{
			get {return sclDovVic;}
		}

		// SCL Journey
		public static Journey SCLStpDov
		{
			get {return sclStpDov;}
		}

		// Mixed (different Operators)
		public static Journey MixedDovNot
		{
			get {return mixedDovNot;}
		}

		//  Mixed (train and coach)
		public static Journey MixedNotDov
		{
			get {return mixedNotDov;}
		}

		//  Different mode Journey (you shouldn't be able to create a PricingUnit with this)
		public static Journey DiffDovNot
		{
			get {return diffDovNot;}
		}

		// Walking journey
		public static Journey WalkNot
		{
			get {return walkNot;}
		}

		//Air journey
		public static Journey JourneyAirLhrGla
		{
			get {return journeyAirLhrGla;}
		}

		//Air journey
		public static Journey JourneyAirGlaLhr
		{
			get {return journeyAirGlaLhr;}
		}
		#endregion

		#region Properties for JourneyDetails
		/**************
		Journeys Details
		These journey details are created below:

		Dover	Ldn Victoria	Ldn St Pancras	Nottingham	Mancester
		
		------->				--------------->
		trainPJDDovVic			trainPJDStpNot
		<-------				<---------------
		trainPJDVicDov			trainPJDNotStp
		
		------->------------------------------->
		natExPJDDovVic			natExPJDVicNot
		<-------<-------------------------------
		natExPJDVicDov			natExPJDNotVic
		
		------->
		sclPJDDovVic
		<-----------------------
		sclPJDStpDov	
		
				--------------->
				underVicStP
				<---------------
				underStpVic
				
								<---------------
								coachPJDNotStp
								<---------------
								natExPJDNotStp
		
		--------------------------------------->
		diffPJDDovNot
		
		**************/
		public static PublicJourneyDetail TrainPJDDovVic
		{
			get {return trainPJDDovVic;}
		}

		public static PublicJourneyDetail TrainPJDVicDov
		{
			get {return trainPJDVicDov;}		
		}

		public static PublicJourneyDetail TrainPJDStpNot
		{
			get {return trainPJDStpNot;}
		}

		public static PublicJourneyDetail TrainPJDNotStp
		{
			get {return trainPJDNotStp;}		
		}

		public static PublicJourneyDetail NatExPJDDovVic
		{
			get {return natExPJDDovVic;}		
		}

		public static PublicJourneyDetail NatExPJDVicDov
		{
			get {return natExPJDVicDov;}
		}

		public static PublicJourneyDetail NatExPJDVicNot
		{
			get {return natExPJDVicNot;}		
		}

		public static PublicJourneyDetail NatExPJDNotVic
		{
			get {return natExPJDNotVic;}
		}

		public static PublicJourneyDetail NatExPJDNotStp
		{
			get {return natExPJDNotStp;}
		}

		public static PublicJourneyDetail SCLPJDDovVic
		{
			get {return sclPJDDovVic;}
		}

		public static PublicJourneyDetail SCLPJDStpDov
		{
			get {return sclPJDStpDov;}
		}

		public static PublicJourneyDetail CoachPJDNotStp
		{
			get {return coachPJDNotStp;}
		}

		public static PublicJourneyDetail UnderPJDVicStp
		{
			get {return underPJDVicStp;}
		}
		public static PublicJourneyDetail UnderPJDStpVic
		{
			get {return underPJDStpVic;}
		}


		public static PublicJourneyDetail DiffPJDDovNot
		{
			get {return diffPJDDovNot;}
		}

        public static PublicJourneyDetail WalkPJDNot
        {
            get {return walkPJDNot;}
        }

        public static PublicJourneyDetail RailReplacementPDJDovVic
        {
            get { return railReplacementPDJDovVic; }
        }

		public static PublicJourneyDetail JourneyDetailAirLHRtoGLA
		{
			get { return journeyDetailAirLHRtoGLA; }
		}

		public static PublicJourneyDetail JourneyDetailAirGLAtoLHR
		{
			get { return journeyDetailAirGLAtoLHR; }
		}

        
		#endregion

		#region Properties for Sample Coach Fares
		public static TransportDirect.JourneyPlanning.CJPInterface.PricingUnit NatExFaresDovNot
		{
			get {return natExFaresDovNot;}
		}

		public static TransportDirect.JourneyPlanning.CJPInterface.PricingUnit NatExFaresNotDpv
		{
			get {return natExFaresNotDov;}
		}
		#endregion

		#region Properties for RBO Response
		// Sample objects to mimic what might be returned by the RBOs
        public static PricingResult[] PricingResult
        {
            get {return pricingResult;}
        }

        public static PricingResultDto PricingResultDto
		{
			get {return pricingResultDto;}
		}

		public static TicketDto SingleUndiscountedStandard
		{
			get {return singleUndiscountedStandard;}
		}

		public static TicketDto SingleDiscountedStandard
		{
			get {return singleDiscountedStandard;}
		}

		public static TicketDto SingleUndiscountedStandardCostSearch
		{
			get {return singleUndiscountedStandardCostSearch;}
		}

		public static TicketDto SingleDiscountedStandardCostSearch
		{
			get {return singleDiscountedStandardCostSearch;}
		}

		public static TicketDto ReturnUndiscountedStandard
		{
			get {return returnUndiscountedStandard;}
		}

		public static TicketDto ReturnDiscountedStandard
		{
			get {return returnDiscountedStandard;}
		}

		public static TicketDto ReturnUndiscountedFirst
		{
			get {return returnUndiscountedFirst;}
		}

		public static TicketDto ReturnDiscountedFirst
		{
			get {return returnDiscountedFirst;}
		}

		public static TicketDto ReturnFirstNoDiscounts
		{
			get {return returnFirstNoDiscounts;}
		}

		public static TicketDto ReturnUndiscountedNoChild
		{
			get {return returnUndiscountedNoChild;}
		}

		public static TicketDto ReturnNoFares
		{
			get {return returnNoFares;}
		}

		public static TicketDto AnotherSingleUndiscountedStandard
		{ 
			get {return anotherSingleUndiscountedStandard;} 
		}
		#endregion

 		#region Static initializer
		static TestSampleJourneyData()
		{
			// See notes above for summaries of these journeys
			// We first create all the Atkins objects, using these to populate the TDP ones
			// There is a 1:1 correspondnace betwen the Legs and PublicJourneyDetails 
			// and Atkins PublicJourneys and TDP PublicJourneys

			// Create all the Atkins objects required to populate the  journey details
			try 
			{			            
				/**** Atkins Event Data ****/
				// Outward Events
				// Event A Dover
				Event dovOut = CreateAtkinsEvent(DateTime.MinValue, DateTime.Parse("1/12/03 08:00",CultureInfo.InvariantCulture), "9100DOVERP", "Dover");
				// Event B1 Victoria (Central)
				Event viccOut = CreateAtkinsEvent( DateTime.Parse("1/12/03 10:00",CultureInfo.InvariantCulture), DateTime.Parse("1/12/03 10:30",CultureInfo.InvariantCulture), "9100VICTRIC", "London Victoria");
				// Event B2 Victoria (Central)
				Event viceOut = CreateAtkinsEvent(DateTime.Parse("1/12/03 10:00",CultureInfo.InvariantCulture), DateTime.Parse("1/12/03 10:30",CultureInfo.InvariantCulture), "9100VICTRIC",  "London Victoria");
				// Event C St Pancras
				Event stpanOut = CreateAtkinsEvent(DateTime.Parse("1/12/03 11:00",CultureInfo.InvariantCulture), DateTime.Parse("1/12/03 11:30",CultureInfo.InvariantCulture), "9100STPX", "London St Pancras");
				// Event D Manchester Pic
				Event manpicOut = CreateAtkinsEvent(DateTime.Parse("1/12/03 14:00",CultureInfo.InvariantCulture), DateTime.MinValue, "9100MCRPIC", "Manchester");
				// Event E Nottingham
				Event notOut = CreateAtkinsEvent(DateTime.Parse("1/12/03 13:30",CultureInfo.InvariantCulture), DateTime.Parse("1/12/03 13:40",CultureInfo.InvariantCulture), "9100NTNG", "Nottingham");
				
				// Return Events
				// Event A Dover
				Event dovRet = CreateAtkinsEvent(DateTime.Parse("3/12/03 21:30",CultureInfo.InvariantCulture), DateTime.MinValue, "9100DOVERP", "Dover");
				// Event B1 Victoria (Central)
				Event viccRet = CreateAtkinsEvent( DateTime.Parse("3/12/03 19:00",CultureInfo.InvariantCulture), DateTime.Parse("3/12/03 19:30",CultureInfo.InvariantCulture), "9100VICTRIC", "London Victoria");
				// Event B2 Victoria (Central)
				Event viceRet = CreateAtkinsEvent(DateTime.Parse("3/12/03 19:00",CultureInfo.InvariantCulture), DateTime.Parse("3/12/03 19:30",CultureInfo.InvariantCulture), "9100VICTRIC",  "London Victoria");
				// Ev	ent C St Pancras
				Event stpanRet = CreateAtkinsEvent(DateTime.Parse("3/12/03 18:00",CultureInfo.InvariantCulture), DateTime.Parse("3/12/03 18:30",CultureInfo.InvariantCulture), "9100STPX", "London St Pancras");
				// Event D Manchester Pic
				Event manpicRet = CreateAtkinsEvent(DateTime.MinValue,DateTime.Parse("3/12/03 16:00",CultureInfo.InvariantCulture) , "9100MCRPIC", "Manchester");
				// Event E Nottingham
				Event notRet = CreateAtkinsEvent(DateTime.MinValue, DateTime.Parse("3/12/03 16:30",CultureInfo.InvariantCulture), "9100NTNG", "Nottingham");

				Event houseOut = CreateAtkinsEvent(DateTime.Parse("1/12/03 15:00", CultureInfo.InvariantCulture), DateTime.MinValue, null, "A house");
				
				//Event Heathrow Airport
				Event CJPEventLhrOut = CreateAtkinsEvent(DateTime.Now.AddHours(1),DateTime.Now,"9200LHR", "Heathrow");
				//Event Glasgow Airport
				Event CJPEventGlaOut = CreateAtkinsEvent(DateTime.Now.AddDays(1).AddHours(1), DateTime.Now.AddDays(1),"9200GLA", "Glasgow");

				/**** Atkins Leg data ****/
				// 4 Train legs. Note matching journeys
				// Dover -> London Victoria
				Leg trainLegDovVic = CreateAtkinsLeg(dovOut, dovOut, viccOut, viccOut, "CX", "111111", ModeType.Rail);
				// London Victoria -> Dover, note: different Victoria naptan, but should be the same location
				Leg trainLegVicDov = CreateAtkinsLeg(viceRet, viceRet, dovRet, dovRet, "CX", "222222", ModeType.Rail);
				// London St Pancras -> Nottingham
				Leg trainLegStpNot = CreateAtkinsLeg(stpanOut, stpanOut, notOut, notOut, "ML", "333333", ModeType.Rail);
//				trainLegStpNot.alight.pass = true; // Set St Pancras as a requested via location
				// Nottingham > London St Pancras 
				Leg trainLegNotStp = CreateAtkinsLeg(notRet, notRet, stpanRet, stpanRet, "ML", "444444", ModeType.Rail);
				// 5 NatEx legs. Note matching journeys
				// Dover -> London Victoria
				Leg natExLegDovVic = CreateAtkinsLeg(dovOut, dovOut, viccOut, viccOut, "AB", "", ModeType.Coach, "207", "North");
				// London Victoria -> Dover note: different Victoria naptan, but should be the same location
				Leg natExLegVicDov = CreateAtkinsLeg(viceRet, viceRet, dovRet, dovRet, "AL", "", ModeType.Coach, "207", "South");
				// London Victoria -> Nottingham
				Leg natExLegVicNot = CreateAtkinsLeg(viccOut, viccOut, notOut, notOut, "AB", "", ModeType.Coach, "207", "North");
				// Nottingham -> London Victoria
				Leg natExLegNotVic = CreateAtkinsLeg(notRet, notRet, viceRet, viceRet, "AL", "", ModeType.Coach, "207", "South");
				// Nottingham -> London St Pancras
				Leg natExLegNotStp = CreateAtkinsLeg(notRet, notRet, stpanRet, stpanRet, "AB", "", ModeType.Coach, "207", "South");
				// 2 SCL legs. Note not matching journeys
				// Dover -> London Victoria
				Leg sclLegDovVic = CreateAtkinsLeg(dovOut, dovOut, viccOut, viccOut, "SC", "", ModeType.Coach, "E2", ""); //*SC
				// London St Pancras -> Dover
				Leg sclLegStpDov = CreateAtkinsLeg(stpanRet, stpanRet, dovRet, dovRet, "SC", "", ModeType.Coach, "E2", ""); //SC:
				// Other coach leg
				// Nottingham -> London St Pancras
				Leg coachLegNotStp = CreateAtkinsLeg(notRet, notRet, stpanRet, stpanRet, "ABC", "", ModeType.Coach, "N11", "");
				// 2 Underground legs
				// London Victoria -> London St Pancras
				Leg underLegVicStp = CreateAtkinsLeg(viccOut, viccOut, stpanOut, stpanOut, "UND", "", ModeType.Underground);
				// London St Pancras -> London Victoria 				
				Leg underLegStpVic = CreateAtkinsLeg(stpanRet, stpanRet, viceRet, viceRet, "UND", "", ModeType.Underground);
				// 1 Different leg
				// London Dover -> Nottingham
				Leg diffLegDovNot = CreateAtkinsLeg(dovOut, dovOut, notOut, notOut, "XYZ", "", ModeType.Car);
				// 1 walking leg
				Leg walkLegNot = CreateAtkinsLeg(notOut, notOut, houseOut, houseOut, null, null, ModeType.Walk);
                //1 rail replacement leg
				Leg railReplacementLegDovVic = CreateAtkinsLeg(dovOut, dovOut, viccOut, viccOut, "CX", "111111", ModeType.RailReplacementBus);

				//flight legs
				Leg cjpLegAirLhrGla = CreateAtkinsLeg(CJPEventLhrOut, CJPEventLhrOut, CJPEventGlaOut, CJPEventGlaOut, "BA", "123456",ModeType.Air);
				Leg cjpLegAirGlaLhr = CreateAtkinsLeg(CJPEventGlaOut, CJPEventGlaOut, CJPEventLhrOut, CJPEventLhrOut, "BA", "123456",ModeType.Air);

                // Create fares information for the coach journeys
				natExFaresDovNot = CreateAtkinsFares(3200, new int[]{0,1});
				natExFaresNotDov = CreateAtkinsFares(3200, new int[]{0,1});

                /**** Atkins PricingUnit data for routing guide compliant sections ****/
                TransportDirect.JourneyPlanning.CJPInterface.PricingUnit puTrainDovNot = CreateAtkinsPricingUnit(new int[2] { 0, 2 }, null, new string[1] { "00000" }, true);
                TransportDirect.JourneyPlanning.CJPInterface.PricingUnit puTrainNotDov = CreateAtkinsPricingUnit(new int[2] { 0, 2 }, null, new string[1] { "00000" }, true);
                

				/**** Atkins PublicJourney data ****/
				// Attach the legs to (Atkins)PublicJourneys
				TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpTrainDovNot = CreateAtkinsJourney(new Leg[]{trainLegDovVic, underLegVicStp, trainLegStpNot}, puTrainDovNot);
				TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpTrainNotDov = CreateAtkinsJourney(new Leg[]{trainLegNotStp, underLegStpVic, trainLegVicDov}, puTrainNotDov);

				TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpNatExDovNot = CreateAtkinsJourney(new Leg[]{natExLegDovVic, natExLegVicNot});
				cjpNatExDovNot.fares = new TransportDirect.JourneyPlanning.CJPInterface.PricingUnit[]{natExFaresDovNot};
				TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpNatExNotDov = CreateAtkinsJourney(new Leg[]{natExLegNotVic, natExLegVicDov});
				cjpNatExNotDov.fares = new TransportDirect.JourneyPlanning.CJPInterface.PricingUnit[]{natExFaresNotDov};

				TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpSclDovVic = CreateAtkinsJourney(new Leg[]{sclLegDovVic});
				TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpSclStpDov = CreateAtkinsJourney(new Leg[]{sclLegStpDov});

				TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpMixedDovNot = CreateAtkinsJourney(new Leg[]{sclLegDovVic, natExLegVicNot});
				TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpMixedNotDov = CreateAtkinsJourney(new Leg[]{coachLegNotStp, underLegStpVic, trainLegVicDov});

				TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpDiffDovNot = CreateAtkinsJourney(new Leg[]{diffLegDovNot});

				TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpWalkNot = CreateAtkinsJourney(new Leg[]{walkLegNot});

				TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpJourneyAirLhrGla = CreateAtkinsJourney(new Leg[]{cjpLegAirLhrGla});
				TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpJourneyAirGlaLhr = CreateAtkinsJourney(new Leg[]{cjpLegAirGlaLhr});

				/**** Atkins Journey Results ****/
				JourneyResult cjpResultTrainDovNot = CreateAtkinsJourneyResult(cjpTrainDovNot);
				JourneyResult cjpResultTrainNotDov = CreateAtkinsJourneyResult(cjpTrainNotDov);
				JourneyResult cjpResultNatExDovNot = CreateAtkinsJourneyResult(cjpNatExDovNot);
				JourneyResult cjpResultNatExNotDov = CreateAtkinsJourneyResult(cjpNatExNotDov);
				JourneyResult cjpResultMixedDovNot = CreateAtkinsJourneyResult(cjpMixedDovNot);
				JourneyResult cjpResultMixedNotDov = CreateAtkinsJourneyResult(cjpMixedNotDov);
				JourneyResult cjpResultMultiDovNot = CreateAtkinsJourneyResult(
                    new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney[]
                        {cjpSclDovVic,
                         cjpNatExDovNot,
                         cjpMixedDovNot
                        }
                    );
                JourneyResult cjpResultMultiNotDov = CreateAtkinsJourneyResult(
                    new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney[]
                        {
                            cjpSclStpDov,
                            cjpNatExNotDov,
                            cjpMixedNotDov
                        }
                    );
				JourneyResult cjpResultWalkNot = CreateAtkinsJourneyResult(cjpWalkNot);

				//air journey results
				JourneyResult cjpResultAirLhrGla = CreateAtkinsJourneyResult(cjpJourneyAirLhrGla);
				JourneyResult cjpResultAirGlaLhr = CreateAtkinsJourneyResult(cjpJourneyAirGlaLhr);

				// Now create TDP objects using the Atkins type ones

				/**** TDP JourneyDetail Data ****/
				// Setup the train PublicJourneyDetails
				trainPJDDovVic = CreateTDPJourneyDetail(trainLegDovVic);
				trainPJDVicDov = CreateTDPJourneyDetail(trainLegVicDov);
				trainPJDStpNot = CreateTDPJourneyDetail(trainLegStpNot);
				trainPJDNotStp = CreateTDPJourneyDetail(trainLegNotStp);
				// Setup the NatEx PublicJourneyDetails
				natExPJDDovVic = CreateTDPJourneyDetail(natExLegDovVic);
				natExPJDVicDov = CreateTDPJourneyDetail(natExLegVicDov);
				natExPJDVicNot = CreateTDPJourneyDetail(natExLegVicNot);
				natExPJDNotVic = CreateTDPJourneyDetail(natExLegNotVic);
				natExPJDNotStp = CreateTDPJourneyDetail(natExLegNotStp);
				// Setup the SCL PublicJourneyDetails
				sclPJDDovVic = CreateTDPJourneyDetail(sclLegDovVic);
				sclPJDStpDov = CreateTDPJourneyDetail(sclLegStpDov);
				// Setup the other coach PublicJourneyDetail
				coachPJDNotStp = CreateTDPJourneyDetail(coachLegNotStp);
				// Setup the underground PublicJourneyDetail
				underPJDVicStp = CreateTDPJourneyDetail(underLegVicStp);
				underPJDStpVic = CreateTDPJourneyDetail(underLegStpVic);
				// Setup the other mode PublicJourneyDetail
				diffPJDDovNot = CreateTDPJourneyDetail(diffLegDovNot);
				walkPJDNot = CreateTDPJourneyDetail(walkLegNot);
				// Setup the rail replacement mode PublicJourneyDetail
				railReplacementPDJDovVic = CreateTDPJourneyDetail(railReplacementLegDovVic);
				// Setup the air mode PublicJourneyDetails
				journeyDetailAirLHRtoGLA = CreateTDPJourneyDetail(cjpLegAirLhrGla);
				journeyDetailAirGLAtoLHR = CreateTDPJourneyDetail(cjpLegAirGlaLhr);

				/**** TDP PublicJourney Data ****/
				// Setup the train Journey A->B and return
				trainDovNot = new TransportDirect.UserPortal.JourneyControl.PublicJourney(1, cjpTrainDovNot, new TDLocation( stpanOut), null, null, TDJourneyType.PublicOriginal, false, 0);
                trainNotDov = new TransportDirect.UserPortal.JourneyControl.PublicJourney(1, cjpTrainNotDov, null, null, null, TDJourneyType.PublicOriginal, false, 0);
                natExDovNot = new TransportDirect.UserPortal.JourneyControl.PublicJourney(2, cjpNatExDovNot, null, null, null, TDJourneyType.PublicOriginal, false, 0);
                natExNotDov = new TransportDirect.UserPortal.JourneyControl.PublicJourney(2, cjpNatExNotDov, null, null, null, TDJourneyType.PublicOriginal, false, 0);
                sclDovVic = new TransportDirect.UserPortal.JourneyControl.PublicJourney(3, cjpSclDovVic, null, null, null, TDJourneyType.PublicOriginal, false, 0);
                sclStpDov = new TransportDirect.UserPortal.JourneyControl.PublicJourney(3, cjpSclStpDov, null, null, null, TDJourneyType.PublicOriginal, false, 0);
                mixedDovNot = new TransportDirect.UserPortal.JourneyControl.PublicJourney(4, cjpMixedDovNot, null, null, null, TDJourneyType.PublicOriginal, false, 0);
                mixedNotDov = new TransportDirect.UserPortal.JourneyControl.PublicJourney(4, cjpMixedNotDov, null, null, null, TDJourneyType.PublicOriginal, false, 0);
                diffDovNot = new TransportDirect.UserPortal.JourneyControl.PublicJourney(5, cjpDiffDovNot, null, null, null, TDJourneyType.PublicOriginal, false, 0);
                walkNot = new TransportDirect.UserPortal.JourneyControl.PublicJourney(6, cjpWalkNot, null, null, null, TDJourneyType.PublicOriginal, false, 0);
                journeyAirLhrGla = new TransportDirect.UserPortal.JourneyControl.PublicJourney(7, cjpJourneyAirLhrGla, null, null, null, TDJourneyType.PublicOriginal, false, 0);
                journeyAirGlaLhr = new TransportDirect.UserPortal.JourneyControl.PublicJourney(7, cjpJourneyAirGlaLhr, null, null, null, TDJourneyType.PublicOriginal, false, 0);
				
				/**** TDP TDJourneyResults ****/
				// Train only JourneyResult
				trainResultDovNotDov = CreateTDPJourneyResult(0, 0, cjpResultTrainDovNot,cjpResultTrainNotDov);
				// NatEx only return JourneyResult
				natExResultDovNotDov = CreateTDPJourneyResult(1, 1, cjpResultNatExDovNot, cjpResultNatExNotDov);
				// NatEx only single JourneyResult
				natExResultDovNot = CreateTDPJourneyResult(2, 2, cjpResultNatExDovNot, null);
				// Miaxed JourneyResult
				mixedResultDovNotDov = CreateTDPJourneyResult(3, 3, cjpResultMixedDovNot, cjpResultMixedNotDov);
                // Single train only JourneyResult
                singleTrainResultDovNotDov = CreateTDPJourneyResult(4, 4, cjpResultTrainDovNot, null);
                // Multi journey JourneyResult (return)
                multiJourney = CreateTDPJourneyResult(5,5, cjpResultMultiDovNot, cjpResultMultiNotDov);
                // Multi journey JourneyResult (single)
                multiJourneySingle = CreateTDPJourneyResult(6,6, cjpResultMultiDovNot, null);
				// Walking journey (single)
				walkJourney = CreateTDPJourneyResult(7,7, cjpResultWalkNot, null);
				// Air journey (single)
				singleAirTDJourneyResult = CreateTDPJourneyResult(8,8, cjpResultAirLhrGla, null);
				// Air journey (return)
				returnAirTDJourneyResult = CreateTDPJourneyResult(9,9, cjpResultAirLhrGla, cjpResultAirGlaLhr);

				/**** TDP RBO Response Data ****/
				// Dummy RBO responses
				singleUndiscountedStandard = new TicketDto("SAS", "00000", 20.00f, 12.00f, float.NaN, float.NaN, TransportDirect.UserPortal.PricingMessages.TicketClass.Standard, string.Empty, false, JourneyType.OutwardSingle, string.Empty, string.Empty);
				// Note discounted more than undisounted (this can happen);
                singleDiscountedStandard = new TicketDto("SAS", "00000", 30.00f, 18.00f, float.NaN, float.NaN, TransportDirect.UserPortal.PricingMessages.TicketClass.Standard, "NET", false, JourneyType.OutwardSingle, string.Empty, string.Empty);

                returnUndiscountedStandard = new TicketDto("SOR", "00000", 50.00f, 30.00f, float.NaN, float.NaN, TransportDirect.UserPortal.PricingMessages.TicketClass.Standard, string.Empty, false, JourneyType.Return, string.Empty, string.Empty);
                returnDiscountedStandard = new TicketDto("SOR", "00000", 25.00f, 15.00f, float.NaN, float.NaN, TransportDirect.UserPortal.PricingMessages.TicketClass.Standard, "NET", false, JourneyType.Return, string.Empty, string.Empty);
                returnUndiscountedFirst = new TicketDto("FOR", "00000", 70.00f, 42.00f, float.NaN, float.NaN, TransportDirect.UserPortal.PricingMessages.TicketClass.First, string.Empty, false, JourneyType.Return, string.Empty, string.Empty);
                returnDiscountedFirst = new TicketDto("FOR", "00000", 35.00f, 21.00f, float.NaN, float.NaN, TransportDirect.UserPortal.PricingMessages.TicketClass.First, "NET", false, JourneyType.Return, string.Empty, string.Empty);
				
				// More exotic ones
                returnFirstNoDiscounts = new TicketDto("FOR", "00000", float.NaN, float.NaN, float.NaN, float.NaN, TransportDirect.UserPortal.PricingMessages.TicketClass.First, "NET", false, JourneyType.Return, string.Empty, string.Empty);
                returnUndiscountedNoChild = new TicketDto("FOR", "00000", 70.00f, float.NaN, float.NaN, float.NaN, TransportDirect.UserPortal.PricingMessages.TicketClass.First, string.Empty, false, JourneyType.Return, string.Empty, string.Empty);
                returnNoFares = new TicketDto("FOR", "00000", float.NaN, float.NaN, float.NaN, float.NaN, TransportDirect.UserPortal.PricingMessages.TicketClass.First, string.Empty, false, JourneyType.Return, string.Empty, string.Empty);


				//  Another set of standard tickets for a different route code. The system should be able to handle both tickets together
                anotherSingleUndiscountedStandard = new TicketDto("SAS", "00001", 18.00f, 11.00f, float.NaN, float.NaN, TransportDirect.UserPortal.PricingMessages.TicketClass.Standard, string.Empty, false, JourneyType.OutwardSingle, string.Empty, string.Empty);


				// a pair of tickets to represent results from a cost-based search ... 
                singleUndiscountedStandardCostSearch = new TicketDto("SAS", "00000", 20.00f, 12.00f, float.NaN, float.NaN, TransportDirect.UserPortal.PricingMessages.TicketClass.Standard, string.Empty, false, JourneyType.OutwardSingle, "RC", "1072", "1234", new LocationDto[0], new LocationDto[0], "origin", "destn", "originNLCactual", "destnNLCactual", string.Empty);
				singleUndiscountedStandardCostSearch.IsFromCostSearch = true;

                singleDiscountedStandardCostSearch = new TicketDto("SAS", "00000", 12.50f, 6.25f, float.NaN, float.NaN, TransportDirect.UserPortal.PricingMessages.TicketClass.Standard, "NET", false, JourneyType.OutwardSingle, "RC", "1072", "1234", new LocationDto[0], new LocationDto[0], "origin", "destn", "originNLCactual", "destnNLCactual", string.Empty);
				singleDiscountedStandardCostSearch.IsFromCostSearch = true;

				ArrayList tickets = new ArrayList();
				tickets.Add(singleUndiscountedStandard);
				tickets.Add(singleDiscountedStandard);
				tickets.Add(returnUndiscountedStandard);
				tickets.Add(returnDiscountedStandard);
				tickets.Add(returnUndiscountedFirst);
				tickets.Add(returnDiscountedFirst);
				
				TDDateTime outwardDate = new TDDateTime(2005, 3, 23);
				TDDateTime returnDate  = new TDDateTime(2005, 3, 29);

				pricingResultDto = new PricingResultDto(outwardDate, returnDate, 5, 15, "GBP", tickets, JourneyType.Return, false, false, false);

                TransportDirect.UserPortal.PricingRetail.RBOGateway.GatewayTransform transform = new TransportDirect.UserPortal.PricingRetail.RBOGateway.GatewayTransform();
                pricingResult = transform.MapPriceUnitResponse(TestSampleJourneyData.PricingResultDto);
			} 
			catch (Exception e) 
			{
				Console.WriteLine("Error occured when initializing TestSampleJourneyData");
				Console.WriteLine("Reported error "+e.Message);
				Console.WriteLine(e.StackTrace);
			}
		}
		#endregion

		#region Private Methods
		private static Event CreateAtkinsEvent(DateTime arriveTime, DateTime departTime, string naptan, string name)
		{
			Event e = new Event();
			if (arriveTime != DateTime.MinValue)
				e.arriveTime = arriveTime;
			if (departTime != DateTime.MinValue)
				e.departTime = departTime;
			e.stop = new Stop();
			e.stop.NaPTANID = naptan;
			e.stop.name = name;
			return e;
		}
		
		/// <summary>
		/// Create a leg including coach specific details
		/// </summary>
		/// <param name="board"></param>
		/// <param name="alight"></param>
		/// <param name="destination"></param>
		/// <param name="operatorCode"></param>
		/// <param name="privateId"></param>
		/// <param name="modeType"></param>
		/// <param name="serviceNumber"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		private static Leg CreateAtkinsLeg(Event origin, Event board, Event alight, Event destination, string operatorCode, 
			string privateId, ModeType modeType, string serviceNumber, string direction)
		{
			Leg leg = new TimedLeg();
			leg.origin = origin;
			leg.board = board;
			leg.alight = alight;
			leg.destination = destination;
			// Only at a service with with have service details
			if (operatorCode != null && !operatorCode.Equals(string.Empty))
			{
				leg.services = new Service[1];
				leg.services[0] = new Service();
				leg.services[0].operatorCode=operatorCode;
				leg.services[0].privateID = privateId;
				leg.services[0].serviceNumber = serviceNumber;
				leg.services[0].direction = direction;
			}
			leg.mode =modeType;		
			return leg;



		}

		/// <summary>
		/// Create a leg without coach specific details
		/// </summary>
		/// <param name="board"></param>
		/// <param name="alight"></param>
		/// <param name="destination"></param>
		/// <param name="operatorCode"></param>
		/// <param name="privateId"></param>
		/// <param name="modeType"></param>
		/// <returns></returns>
		private static Leg CreateAtkinsLeg(Event origin, Event board, Event alight, Event destination, string operatorCode, string privateId, ModeType modeType)
		{		
			return CreateAtkinsLeg(origin, board, alight, destination, operatorCode, privateId, modeType, "", "");
		}

		private static TransportDirect.JourneyPlanning.CJPInterface.PricingUnit CreateAtkinsFares(int basePrice, int[] legs)
		{
			Fare adultSing = CreateAtkinsFare("Standard Single", basePrice, true, true, "", "Adult", "", FareType.Flexible);
			Fare childSing = CreateAtkinsFare("Standard Single", (basePrice)/2+100, false, true, "", "Child", "6-15", FareType.Flexible);
			Fare adultRet = CreateAtkinsFare("Standard Return", basePrice*2-100, true, false, "", "Adult", "", FareType.LimitedFlexibility);
			Fare childRet = CreateAtkinsFare("Standard Return", (basePrice*2 - 100)/2+100, false, false, "", "Child", "6-15",FareType.LimitedFlexibility );
			Fare adultAltRet = CreateAtkinsFare("Standard Return", (int)basePrice*3/2, true, false, "", "Disabled", "", FareType.Flexible);
			Fare adultDis1 = CreateAtkinsFare("Standard Return", (int)basePrice*3/2, true , false, "National Express Student Coachcard", "Student", "", FareType.LimitedFlexibility);
			Fare adultDis2 = CreateAtkinsFare("Standard Return", (int)basePrice*3/2, true, false, "Advantage50 Coachcard", "Advantage50", "", FareType.LimitedFlexibility);
			Fare childDis1 = CreateAtkinsFare("Standard Return", (int)basePrice/3+100, false, false, "Child Card", "Super Child", "6-15", FareType.LimitedFlexibility);
			Fare adultOpenRet = CreateAtkinsFare("Open Return", basePrice*3-100, true, false, "", "Adult", "", FareType.Flexible);
			Fare childOpenRet = CreateAtkinsFare("Open Return", (basePrice*3 - 100)/2+100, false, false, "", "Child", "6-15", FareType.Flexible);
			Fare adultApexRet = CreateAtkinsFare("Apex Return", basePrice*3/2, true, false, "", "Adult", "", FareType.NotFlexible);
			Fare adultApexDisRet = CreateAtkinsFare("Apex Return", basePrice, true, false, "National Express Student Coachcard", "Student", "", FareType.NotFlexible);

			TransportDirect.JourneyPlanning.CJPInterface.PricingUnit cjpUnit = new TransportDirect.JourneyPlanning.CJPInterface.PricingUnit();
			cjpUnit.legs = legs;
			cjpUnit.prices = new Fare[]{ adultSing, childSing, adultRet, childRet, adultAltRet, adultDis1, adultDis2, childDis1, adultOpenRet, childOpenRet, adultApexRet, adultApexDisRet};
			return cjpUnit;
		}

		private static Fare CreateAtkinsFare(string name, int price, bool isAdult, bool isSingle, string card, string passengerType, string ageRange, FareType flexibility)
		{
			Fare fare = new Fare();
			fare.fareType = name;
			fare.fare = price;
			fare.adult = isAdult;
			fare.single = isSingle;
			fare.discountCardType = card;
			fare.childAgeRange = ageRange;
			fare.fareRestrictionType = flexibility;
			fare.passengerType = passengerType;
			return fare;
		}

		private static  TransportDirect.JourneyPlanning.CJPInterface.PricingUnit CreateAtkinsPricingUnit(int[] legs, Fare[] fares, string[] fareCodes, bool routingGuideCompliant)
		{
			TransportDirect.JourneyPlanning.CJPInterface.PricingUnit cjpUnit = new TransportDirect.JourneyPlanning.CJPInterface.PricingUnit();
			cjpUnit.prices = fares;
			cjpUnit.legs = legs;
            cjpUnit.routingGuideCompliant = routingGuideCompliant;
            cjpUnit.fareCodes = fareCodes;
			return cjpUnit;
		}

        private static TransportDirect.JourneyPlanning.CJPInterface.PublicJourney CreateAtkinsJourney(Leg[] legs, TransportDirect.JourneyPlanning.CJPInterface.PricingUnit pricingUnit)
        {
            TransportDirect.JourneyPlanning.CJPInterface.PublicJourney journey = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
            journey.legs = legs;
            journey.fares = new TransportDirect.JourneyPlanning.CJPInterface.PricingUnit[] { pricingUnit };
            return journey;
        }

		private static TransportDirect.JourneyPlanning.CJPInterface.PublicJourney CreateAtkinsJourney(Leg[] legs)
		{
			TransportDirect.JourneyPlanning.CJPInterface.PublicJourney journey = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			journey.legs = legs;
			return journey;
		}

		private static JourneyResult CreateAtkinsJourneyResult(TransportDirect.JourneyPlanning.CJPInterface.PublicJourney journey)
		{
			JourneyResult result = new JourneyResult();
			result.publicJourneys = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney[]{journey};
			return result;
		}

        private static JourneyResult CreateAtkinsJourneyResult(TransportDirect.JourneyPlanning.CJPInterface.PublicJourney[] journey)
        {
            JourneyResult result = new JourneyResult();
            result.publicJourneys = journey;
            return result;
        }

        private static PublicJourneyDetail CreateTDPJourneyDetail(Leg leg)
		{
			PublicJourneyDetail detail = PublicJourneyDetail.Create(leg, null);
			detail.LegStart.Location.Description = leg.board.stop.name;
			detail.LegEnd.Location.Description = leg.alight.stop.name;
			return detail;
		}

		private static TDJourneyResult CreateTDPJourneyResult(int refNum, int routeNum, JourneyResult outbound, JourneyResult inbound)
		{
            TDJourneyResult result;
            if (inbound != null) 
            {
                result = new TDJourneyResult(refNum, routeNum, outbound.publicJourneys[0].legs[0].board.departTime, inbound.publicJourneys[0].legs[0].board.departTime, false, false, false);
                result.AddResult(outbound, true, null, null, null, null, "abcdefg", false, -1);
                result.AddResult(inbound, false, null, null, null, null, "abcdefg", false, -1);
            } 
            else {
                result = new TDJourneyResult(refNum, routeNum, outbound.publicJourneys[0].legs[0].board.departTime, null, false, false, false);
                result.AddResult(outbound, true, null, null, null, null, "abcdefg", false, -1);
            }

			return result;
		}

		#endregion
	
	}
}
