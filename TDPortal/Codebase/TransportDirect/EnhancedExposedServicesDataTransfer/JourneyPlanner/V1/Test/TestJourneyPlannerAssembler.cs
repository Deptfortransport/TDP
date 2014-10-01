// ***********************************************
// NAME 		: TestJourneyPlannerAssembler.cs
// AUTHOR 		: Hassan Al-Katib
// DATE CREATED : 16/01/2006
// DESCRIPTION 	: testclass for JourneyPlannerAssembler.cs class
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/Test/TestJourneyPlannerAssembler.cs-arc  $  
//
//   Rev 1.0   Nov 08 2007 12:22:34   mturner
//Initial revision.
//
//   Rev 1.2   Jul 12 2006 16:23:34   rphilpott
//Select best journeys to return to client when number requestd from CJP is more than asked for by client.
//Resolution for 4126: Not returning best journey to Lauren
//
//   Rev 1.1   Feb 01 2006 16:25:34   halkatib
//Changes made as part of code review.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//

using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;

using NUnit.Framework;
using journeyPlannerDTO = TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using journeyControl = TransportDirect.UserPortal.JourneyControl;
using cjpInterface = TransportDirect.JourneyPlanning.CJPInterface;

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;


namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1.Test
{
	public class TestJourneyPlannerAssemblerInitialisation : IServiceInitialisation
	{		

		public void Populate(Hashtable serviceCache)
		{

			// nasty bodge to make date validation checks independent of the 
			//  date format of the user/machine the tests are run on ...
			CultureInfo ci = new CultureInfo("en-GB");
			DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
			dtfi.ShortDatePattern = "dd MM yyyy";
			ci.DateTimeFormat = dtfi;
			Thread.CurrentThread.CurrentCulture = ci;

			serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory() );
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			serviceCache.Add(ServiceDiscoveryKey.DataServices, new TestMockDataServicesFactory());	
			serviceCache.Add (ServiceDiscoveryKey.GisQuery, new TestMockGisQuery());
			serviceCache.Add( ServiceDiscoveryKey.Cache, new TDCache() );					
			serviceCache.Add(ServiceDiscoveryKey.ExternalLinkService, new DummyExternalLinkService());		
			serviceCache.Add(ServiceDiscoveryKey.TDMapHandoff, new TestStubTDMapHandoff());			
		}
	
	}
	
	
	/// <summary>
	/// Summary description for TestJourneyPlannerAssembler.
	/// </summary>
	[TestFixture]
	public class TestJourneyPlannerAssembler
	{
		private PublicJourneyRequest publicJourneyRequest;
		private PublicJourneyResult publicJourneyResult;
		//private JourneyRequestHelper helper;
		private TransportDirect.Common.ResourceManager.TDResourceManager rm;
		ITDJourneyResult result;	
		

		#region setup
		/// <summary>
		/// Method to initalise the simulated result
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			result = null;

			// Initialise the service discovery
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestJourneyPlannerAssemblerInitialisation( ));
			IPropertyProvider property = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];

			Trace.Listeners.Remove("TDTraceListener");
		
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
				
			try
			{
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException e)
			{
				Assert.Fail(e.Message);
			}		
			
			//intialise a dummy request object
			publicJourneyRequest = new PublicJourneyRequest();		
			
		}
		
		/// <summary>
		/// Method to initialise the journey result using the specified outward filename
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		private ITDJourneyResult InitialiseJourneyResult(string fileName)
		{
			TestMockCJPFromFile cjpManager = new TestMockCJPFromFile();

			cjpManager.FilenameOutward = AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName;	
			
			ITDJourneyRequest request =  new TDJourneyRequest();
			request.IsReturnRequired = false;
			ITDJourneyResult initialResult = null;
			initialResult = cjpManager.CallCJP(request);
			
			rm = TDResourceManager.GetResourceManagerFromCache(TDResourceManager.JOURNEY_PLANNER_SERVICE_RM);

			return initialResult;
		}

		/// <summary>
		/// Mehtod to initialise the journey result using the specified outward and return filename
		/// </summary>
		/// <param name="fileNameOut"></param>
		/// <param name="fileNameReturn"></param>
		/// <returns></returns>
		private ITDJourneyResult InitialiseReturnJourneyResult(string fileNameOut, string fileNameReturn)
		{
			TestMockCJPFromFile cjpManager = new TestMockCJPFromFile();

			cjpManager.FilenameOutward = AppDomain.CurrentDomain.BaseDirectory + "\\" + fileNameOut;
			cjpManager.FilenameReturn = AppDomain.CurrentDomain.BaseDirectory + "\\" + fileNameReturn;

			ITDJourneyRequest request =  new TDJourneyRequest();
			ITDJourneyResult initialResult = null;
			request.IsReturnRequired = true;
			initialResult = cjpManager.CallCJP(request);
			
			rm = TDResourceManager.GetResourceManagerFromCache(TDResourceManager.JOURNEY_PLANNER_SERVICE_RM);

			return initialResult;
		}
		#endregion 
		
		/// <summary>
		/// This tests if the Method converts an ITDourneyRequest object to a PublicJourneyResult object when supplied with a single journey result.
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyResultDT_Inverness_Folkestone()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");

			publicJourneyResult = JourneyPlannerAssembler.CreatePublicJourneyResultDT(result, rm, false,false, 3);
						
			Console.WriteLine(publicJourneyResult.OutwardPublicJourneys.Length.ToString());					

			Assert.AreEqual(publicJourneyResult.OutwardPublicJourneys.Length,3);
			Assert.AreEqual(publicJourneyResult.ReturnPublicJourneys.Length,0);
		}

		
		/// <summary>
		/// This tests if the Method converts an ITDourneyRequest object to a PublicJourneyResult object when supplied with a return journey result.
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyResultDT_Inverness_Folkestone_Return()
		{
			result = InitialiseReturnJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml","Inverness_Folkstone_Xml_Journey_Return_Result.xml");

			publicJourneyResult = JourneyPlannerAssembler.CreatePublicJourneyResultDT(result, rm, false,false,3);
						
			Console.WriteLine(publicJourneyResult.OutwardPublicJourneys.Length.ToString());					
			Console.WriteLine(publicJourneyResult.ReturnPublicJourneys.Length.ToString());

			Assert.AreEqual(publicJourneyResult.OutwardPublicJourneys.Length,3);
			Assert.AreEqual(publicJourneyResult.ReturnPublicJourneys.Length,3);
		}

		
		/// <summary>
		/// This tests if the Method converts an ITDourneyRequest object containing user warnings to a PublicJourneyResult and verifies that the warnings have been passed to the new object.
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyResultDT_Inverness_Folkestone_AddedWarning()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");			

			result.AddMessageToArray(rm.GetString("JourneyPlannerOutput.JourneyOverlap"),
				"JourneyPlannerOutput.JourneyOverlap",
				0,
				0, 
				ErrorsType.Warning);

			publicJourneyResult = JourneyPlannerAssembler.CreatePublicJourneyResultDT(result, rm, false,false,2);
						
			Console.WriteLine(publicJourneyResult.UserWarnings[0]);
			Assert.AreEqual(publicJourneyResult.UserWarnings[0], 
				"Certain combinations of outward and return journeys would result in you needing to leave your destination before arriving at it.");
		}

		
		/// <summary>
		/// This tests if the Method returns a null dto object when provided with a null domain object.
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyResultDT_null()
		{
			result = null;

			publicJourneyResult = JourneyPlannerAssembler.CreatePublicJourneyResultDT(result, rm, false,false,2);
						
			Assert.AreEqual(publicJourneyResult,null);			
		}
	
		
		/// <summary>
		/// This tests if the method creates a journeysummary dto using the supplied summary domain object provided by a single journey result.
		/// </summary>	
		[Test]
		public void CallCreateJourneySummaryDT_Inverness_Folkestone()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			JourneySummary journeySummary = new JourneySummary();
			
			journeySummary = JourneyPlannerAssembler.CreateJourneySummaryDT(result.OutwardJourneySummary(false)[0],rm);					

			Assert.AreEqual(journeySummary.OriginDescription, "Inverness Farraline Park Bus Station");
			Assert.AreEqual(journeySummary.DestinationDescription, "Folkestone Central");
			foreach (ModeType m in journeySummary.Modes)
			{
				Assert.IsTrue(
					(m == ModeType.Bus) || 
					(m == ModeType.Walk) || 
					(m == ModeType.Rail) || 
					(m == ModeType.Underground)
					);				
			}
			foreach(string s in journeySummary.ModesText)
			{
				Assert.IsTrue(
					(s=="Bus") || (s=="Walk") || (s=="Train") || (s=="Underground")
					);
			}
			Assert.AreEqual(journeySummary.InterchangeCount,5);		
	
			Assert.AreEqual(journeySummary.DepartureDateTime.Hour, 11);
			Assert.AreEqual(journeySummary.DepartureDateTime.Minute, 15);

			Assert.AreEqual(journeySummary.ArrivalDateTime.Hour, 23);
			Assert.AreEqual(journeySummary.ArrivalDateTime.Minute, 30);
		}

		
		/// <summary>
		/// This tests if the Method returns a null dto object when provided with a null domain object.
		/// </summary>
		[Test]
		public void CallCreateJourneySummaryDT_null()
		{
			JourneySummary journeySummary = new JourneySummary();			
			journeySummary = JourneyPlannerAssembler.CreateJourneySummaryDT(null,rm);					
			Assert.AreEqual(journeySummary, null);			
		}

		
		/// <summary>
		/// This tests if the method creates a serviceDetails dto using the supplied serviceDetails domain object provided by a single journey result.
		/// </summary>
		[Test]
		public void CallCreateServiceDetailsDT_Inverness_Folkestone()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			journeyPlannerDTO.ServiceDetails[] servicedetails 
				= new journeyPlannerDTO.ServiceDetails[JourneyPlannerAssembler.CreateServicesDetailsDT(result.OutwardPublicJourney(0).Details[0].Services).Length];			
			
			
			servicedetails = JourneyPlannerAssembler.CreateServicesDetailsDT(result.OutwardPublicJourney(1).Details[0].Services);
				

			foreach(journeyPlannerDTO.ServiceDetails sd in servicedetails)
			{
				
				Console.WriteLine(sd.OperatorCode);				
				Console.WriteLine(sd.OperatorName);
				Console.WriteLine(sd.ServiceNumber);
				Console.WriteLine(sd.DestinationBoard);
				Console.WriteLine();

				Assert.AreEqual(sd.OperatorCode, "MEG");
				Assert.AreEqual(sd.OperatorName, "Megabus");
				Assert.AreEqual(sd.ServiceNumber, "M90");
				Assert.AreEqual(sd.DestinationBoard, "Edinburgh");
			}	
		}

		
		/// <summary>
		/// This tests if the Method returns a null dto object when provided with a null domain object.
		/// </summary>
		[Test]
		public void CallCreateServiceDetailsDT_null()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			journeyPlannerDTO.ServiceDetails[] servicedetails 
				= new journeyPlannerDTO.ServiceDetails[JourneyPlannerAssembler.CreateServicesDetailsDT(result.OutwardPublicJourney(0).Details[0].Services).Length];					
			
			servicedetails = JourneyPlannerAssembler.CreateServicesDetailsDT(null);
			Assert.AreEqual(servicedetails, null);
		}

	
		/// <summary>
		/// This tests if the method creates a Response location dto using the supplied tdlocation domain object provided by a single journey result.
		/// </summary>
		[Test]
		public void CallCreateResponseLocationDT_Inverness_Folkestone()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			ResponseLocation responseLocation = new ResponseLocation();

			
			responseLocation = JourneyPlannerAssembler.CreateResponseLocationDT(result.OutwardPublicJourney(1).Details[0].LegStart.Location);		

			Console.WriteLine(responseLocation.GridReference.Easting.ToString());				
			Console.WriteLine(responseLocation.GridReference.Northing.ToString());
			Console.WriteLine(responseLocation.Naptan.NaptanId);
			Console.WriteLine(responseLocation.Description);
			Console.WriteLine(responseLocation.Locality);
			Console.WriteLine();			

			Assert.AreEqual(responseLocation.GridReference.Easting.ToString(),"266670");
			Assert.AreEqual(responseLocation.GridReference.Northing.ToString(),"845580");
			Assert.AreEqual(responseLocation.Naptan.NaptanId,"900090147");
			Assert.AreEqual(responseLocation.Description,"Inverness Farraline Park Bus Station");
			//locality??			
		}

		
		/// <summary>
		/// This tests if the Method returns a null dto object when provided with a null domain object.
		/// </summary>
		[Test]
		public void CallCreateResponseLocationDT_null()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			ResponseLocation responseLocation = new ResponseLocation();			
			responseLocation = JourneyPlannerAssembler.CreateResponseLocationDT(null);		
			Assert.AreEqual(responseLocation,null);
		}

	
		/// <summary>
		/// This tests if the method creates a Public Journey Calling Point dto using the supplied Public Journey Calling Point domain object provided by a single journey result.
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyCallingPointDT_Inverness_Folkestone()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			journeyPlannerDTO.PublicJourneyCallingPoint pjcpLegStart = new journeyPlannerDTO.PublicJourneyCallingPoint();
			journeyPlannerDTO.PublicJourneyCallingPoint pjcpLegEnd = new journeyPlannerDTO.PublicJourneyCallingPoint();
			journeyPlannerDTO.PublicJourneyCallingPoint pjcOrigin = new journeyPlannerDTO.PublicJourneyCallingPoint();
			journeyPlannerDTO.PublicJourneyCallingPoint pjcpDestination = new journeyPlannerDTO.PublicJourneyCallingPoint();
			journeyPlannerDTO.PublicJourneyCallingPoint[] pjcpIntBefore;
			journeyPlannerDTO.PublicJourneyCallingPoint[] pjcpIntLeg;
			journeyPlannerDTO.PublicJourneyCallingPoint[] pjcpIntAfter;

			
			pjcpLegStart = JourneyPlannerAssembler.CreatePublicJourneyCallingPointDT(result.OutwardPublicJourney(1).Details[2].LegStart);
			pjcpLegEnd= JourneyPlannerAssembler.CreatePublicJourneyCallingPointDT(result.OutwardPublicJourney(1).Details[2].LegEnd);
			pjcOrigin= JourneyPlannerAssembler.CreatePublicJourneyCallingPointDT(result.OutwardPublicJourney(1).Details[2].Origin);
			pjcpDestination = JourneyPlannerAssembler.CreatePublicJourneyCallingPointDT(result.OutwardPublicJourney(1).Details[2].Destination);
			pjcpIntBefore = JourneyPlannerAssembler.CreatePublicJourneyCallingPointDT(result.OutwardPublicJourney(1).Details[2].GetIntermediatesBefore());
			pjcpIntLeg = JourneyPlannerAssembler.CreatePublicJourneyCallingPointDT(result.OutwardPublicJourney(1).Details[2].GetIntermediatesLeg());
			pjcpIntAfter = JourneyPlannerAssembler.CreatePublicJourneyCallingPointDT(result.OutwardPublicJourney(1).Details[2].GetIntermediatesAfter());
		
			journeyPlannerDTO.PublicJourneyCallingPoint[] pjcpArray = new journeyPlannerDTO.PublicJourneyCallingPoint[]
				{
					pjcpLegStart,pjcpLegEnd, pjcOrigin, pjcpDestination
				};

			foreach (journeyPlannerDTO.PublicJourneyCallingPoint pjcp in pjcpArray)
			{
				Console.WriteLine("Leg------------------");
				Console.WriteLine(pjcp.Type.ToString());
				Console.WriteLine(pjcp.Location.Description);
				Console.WriteLine(pjcp.ArrivalDateTime.ToShortTimeString());
				Console.WriteLine(pjcp.DepartureDateTime.ToShortTimeString());
				Console.WriteLine();
			}	
			Console.WriteLine("Int Before---------------");
			foreach (journeyPlannerDTO.PublicJourneyCallingPoint pjcp in pjcpIntBefore)
			{
				Console.WriteLine(pjcp.Type.ToString());
				Console.WriteLine(pjcp.Location.Description);
				Console.WriteLine(pjcp.ArrivalDateTime.ToShortTimeString());
				Console.WriteLine(pjcp.DepartureDateTime.ToShortTimeString());
				Console.WriteLine();
			}
			Console.WriteLine("Int Leg------------------");
			foreach (journeyPlannerDTO.PublicJourneyCallingPoint pjcp in pjcpIntLeg)
			{
				Console.WriteLine(pjcp.Type.ToString());
				Console.WriteLine(pjcp.Location.Description);
				Console.WriteLine(pjcp.ArrivalDateTime.ToShortTimeString());
				Console.WriteLine(pjcp.DepartureDateTime.ToShortTimeString());
				Console.WriteLine();
			}
			Console.WriteLine("Int After------------------");
			foreach (journeyPlannerDTO.PublicJourneyCallingPoint pjcp in pjcpIntAfter)
			{
				Console.WriteLine(pjcp.Type.ToString());
				Console.WriteLine(pjcp.Location.Description);
				Console.WriteLine(pjcp.ArrivalDateTime.ToShortTimeString());
				Console.WriteLine(pjcp.DepartureDateTime.ToShortTimeString());
				Console.WriteLine();
			}

			//test leg Start
			Assert.AreEqual(pjcpArray[0].Type.ToString(), "OriginAndBoard");
			Assert.AreEqual(pjcpArray[0].Location.Description, "Edinburgh");
			Assert.AreEqual(pjcpArray[0].ArrivalDateTime.ToShortTimeString(), "15:05");
			Assert.AreEqual(pjcpArray[0].DepartureDateTime.ToShortTimeString(), "15:05");

			//test leg end
			Assert.AreEqual(pjcpArray[1].Type.ToString(), "Alight");
			Assert.AreEqual(pjcpArray[1].Location.Description, "Newcastle");
			Assert.AreEqual(pjcpArray[1].ArrivalDateTime.ToShortTimeString(), "16:36");
			Assert.AreEqual(pjcpArray[1].DepartureDateTime.ToShortTimeString(), "16:40");

			//test destination
			Assert.AreEqual(pjcpArray[3].Type.ToString(), "Destination");
			Assert.AreEqual(pjcpArray[3].Location.Description, "Southampton Central");
			Assert.AreEqual(pjcpArray[3].ArrivalDateTime.ToShortTimeString(), "22:47");
			//no departure time

			//intermediate Leg
			Assert.AreEqual(pjcpIntLeg[0].Type.ToString(), "CallingPoint");
			Assert.AreEqual(pjcpIntLeg[0].Location.Description, "Dunbar");
			Assert.AreEqual(pjcpIntLeg[0].ArrivalDateTime.ToShortTimeString(), "15:26");
			Assert.AreEqual(pjcpIntLeg[0].DepartureDateTime.ToShortTimeString(), "15:27");
			
			//intermediate After
			Assert.AreEqual(pjcpIntAfter[5].Type.ToString(), "PassingPoint");
			Assert.AreEqual(pjcpIntAfter[5].Location.Description, "Chesterfield");
			Assert.AreEqual(pjcpIntAfter[5].ArrivalDateTime.ToShortTimeString(), "00:00");
			Assert.AreEqual(pjcpIntAfter[5].DepartureDateTime.ToShortTimeString(), "19:05");			
		}
		
		
		/// <summary>
		/// This tests if the Method returns a null dto object when provided with a null domain object.
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyCallingPointDT_null()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			journeyPlannerDTO.PublicJourneyCallingPoint pjcpLegStart = new journeyPlannerDTO.PublicJourneyCallingPoint();
			journeyPlannerDTO.PublicJourneyCallingPoint[] pjcpIntBefore;
			
			journeyControl.PublicJourneyCallingPoint pjd = null;
			journeyControl.PublicJourneyCallingPoint[] pjdArray = null;
			pjcpLegStart = JourneyPlannerAssembler.CreatePublicJourneyCallingPointDT(pjd);
			pjcpIntBefore = JourneyPlannerAssembler.CreatePublicJourneyCallingPointDT(pjdArray);
			
			Assert.AreEqual(pjcpLegStart, null);
			Assert.AreEqual(pjcpIntBefore, null);			
		}


		/// <summary>
		/// This tests if the method creates a suitable leg instruction text using the supplied details from a journey result domain object provided by a single journey result.
		/// </summary>
		[Test]
		public void CallCreateInstructionText_Inverness_Folkestone()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			string instructiontext;	
			string[] compareStringArray = new String[]
				{
				"Take Megabus/M90 towards Edinburgh",
				"Walk to Edinburgh Waverley (trains)",
                "Take Virgin Trains towards Southampton Central",
				"Take Great North Eastern Railway Ltd towards London Kings Cross",
				"Take Underground",
				"Take South Eastern Trains towards Dover Priory",
				"Take South Eastern Trains towards Ashford (Kent)"
				};
			string[] instructionArray = new String[result.OutwardPublicJourney(1).Details.Length];

			int count = 0;
			foreach(journeyControl.PublicJourneyDetail pjd in result.OutwardPublicJourney(1).Details)
			{
				instructiontext = JourneyPlannerAssembler.CreateInstructionText(pjd, rm);
				instructionArray[count] = instructiontext;
				Console.WriteLine(instructiontext);								
				Console.WriteLine();
				Assert.AreEqual(instructiontext, compareStringArray[count]);
				count++;
			}		
		}

	
		/// <summary>
		/// This tests if the method creates a suitable leg instruction text using the supplied details from a journey result domain object provided by a single journey result. Here multiple service details are added for a single leg. This results in the code using a different pathway to create the text.
		/// </summary>
		[Test]
		public void CallCreateInstructionText_ExtraServices()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			string instructiontext;	
			
			journeyControl.PublicJourneyDetail pjd = result.OutwardPublicJourney(1).Details[0];			
			journeyControl.ServiceDetails[] extraServices = new journeyControl.ServiceDetails[]
				{pjd.Services[0], pjd.Services[0], pjd.Services[0]};
			pjd.Services = extraServices;
			instructiontext = JourneyPlannerAssembler.CreateInstructionText(pjd, rm);
			
			Console.WriteLine(instructiontext);								
				
			Assert.AreEqual(instructiontext,"Take Megabus/M90 towards Edinburgh or take Megabus/M90 towards Edinburgh or take Megabus/M90 towards Edinburgh");		
		}


		/// <summary>
		/// This tests if the method creates a suitable leg instruction text using the null service details. Here code adds the minimum information when there are not details provided. This results in the code using a different pathway to create the text.
		/// </summary>
		[Test]
		public void CallCreateInstructionText_Services_null()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			string instructiontext;	
			
			journeyControl.PublicJourneyDetail pjd = result.OutwardPublicJourney(1).Details[0];			
			pjd.Services = null;
			instructiontext = JourneyPlannerAssembler.CreateInstructionText(pjd, rm);
			
			Console.WriteLine(instructiontext);								
				
			Assert.AreEqual(instructiontext,"Take Bus");		
		}


		/// <summary>
		/// This tests if the method creates a suitable leg instruction text using the null service details. In this case the journey leg has a populated destination property. Here code adds the extra information for the destination. This results in the code using a different pathway to create the text.
		/// </summary>
		[Test]
		public void CallCreateInstructionText_Services_With_Destination()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			string instructiontext;	
			
			journeyControl.PublicJourneyDetail pjd = result.OutwardPublicJourney(1).Details[2];			
			pjd.Services = null;
			instructiontext = JourneyPlannerAssembler.CreateInstructionText(pjd, rm);
			
			Console.WriteLine(instructiontext);								
				
			Assert.AreEqual(instructiontext,"Take Rail towards Southampton Central");		
		}

	
		/// <summary>
		/// This tests if the method creates a suitable leg instruction text using a single null service detail. In this case the journey leg has a populated destination property. Here code adds the minimum information when there is no detail provided. This results in the code using a different pathway to create the text.
		/// </summary>
		[Test]
		public void CallCreateInstructionText_Service_Detail_null()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			string instructiontext;	
			
			journeyControl.PublicJourneyDetail pjd = result.OutwardPublicJourney(1).Details[0];			
			pjd.Services[0] = null;
			instructiontext = JourneyPlannerAssembler.CreateInstructionText(pjd, rm);
			
			Console.WriteLine(instructiontext);								
				
			Assert.AreEqual(instructiontext,"Take Bus");		
		}


		/// <summary>
		/// This tests if the method creates a suitable leg instruction text using a null service detail. In this case the journey leg has a populated destination property. Here code adds the extra information for the destination. This results in the code using a different pathway to create the text.
		/// </summary>	
		[Test]
		public void CallCreateInstructionText_Service_Detail_null_With_Destination()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			string instructiontext;	
			
			journeyControl.PublicJourneyDetail pjd = result.OutwardPublicJourney(1).Details[2];			
			pjd.Services[0] = null;
			instructiontext = JourneyPlannerAssembler.CreateInstructionText(pjd, rm);
			
			Console.WriteLine(instructiontext);								
				
			Assert.AreEqual(instructiontext,"Take Rail towards Southampton Central");		
		}
		

		/// <summary>
		/// This tests if the method creates an array of public journey detail using the supplied public journey detail domain object array provided by a single journey result.
		/// </summary>
		[Test]
		public void CallCreateInstructionText_null()
		{
			string instructiontext;	
			instructiontext = JourneyPlannerAssembler.CreateInstructionText(null, rm);
			Assert.AreEqual(instructiontext, "");					
		}

	
		/// <summary>
		/// This tests if the method creates an array of public journey detail using the supplied public journey detail domain object array provided by a single journey result.
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyDetailsDT_Inverness_Folkestone()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
							
			journeyPlannerDTO.PublicJourneyDetail[] pjdArray = new
				journeyPlannerDTO.PublicJourneyDetail[result.OutwardPublicJourney(1).Details.Length];			
		
			pjdArray = JourneyPlannerAssembler.CreatePublicJourneyDetailsDT(result.OutwardPublicJourney(1).Details,rm);
			
			Console.WriteLine(pjdArray[2].Type.ToString());
			Console.WriteLine(pjdArray[2].Mode.ToString());
			Console.WriteLine(pjdArray[2].ModeText);
			Console.WriteLine(pjdArray[2].Duration.ToString());
			Console.WriteLine(pjdArray[2].DurationText);
			Console.WriteLine(pjdArray[2].InstructionText);
			Console.WriteLine(pjdArray[2].Origin.Type.ToString());
			Console.WriteLine(pjdArray[2].Destination.Type.ToString());
			Console.WriteLine(pjdArray[2].LegStart.Type.ToString());
			Console.WriteLine(pjdArray[2].LegEnd.Type.ToString());
			Console.WriteLine(pjdArray[2].IntermediatesAfter.Length.ToString());
			Console.WriteLine(pjdArray[2].Services.Length.ToString());
			Console.WriteLine(pjdArray[2].VehicleFeatures.Length.ToString());
			Console.WriteLine(pjdArray[2].VehicleFeaturesText[0].ToString());				
			//Console.WriteLine(pjdArray[2].DisplayNotes.Length.ToString());

			Assert.AreEqual(pjdArray[2].Type.ToString(),"Timed");
			Assert.AreEqual(pjdArray[2].Mode.ToString(),"Rail");
			Assert.AreEqual(pjdArray[2].ModeText, "Train");
			Assert.AreEqual(pjdArray[2].Duration,5460);
			Assert.AreEqual(pjdArray[2].DurationText,"Duration: 1 hour 31 minutes");
			Assert.AreEqual(pjdArray[2].InstructionText,"Take Virgin Trains towards Southampton Central");
			Assert.AreEqual(pjdArray[2].Origin.Type.ToString(),"OriginAndBoard");
			Assert.AreEqual(pjdArray[2].Destination.Type.ToString(),"Destination");
			Assert.AreEqual(pjdArray[2].LegStart.Type.ToString(),"OriginAndBoard");
			Assert.AreEqual(pjdArray[2].LegEnd.Type.ToString(),"Alight");
			Assert.AreEqual(pjdArray[2].IntermediatesAfter.Length,20);
			Assert.AreEqual(pjdArray[2].Services.Length,1);
			Assert.AreEqual(pjdArray[2].VehicleFeatures.Length,3);
			Assert.AreEqual(pjdArray[2].VehicleFeaturesText[0],"Buffet service");				
			//Assert.AreEqual(pjdArray[2].DisplayNotes.Length,0);		
			Assert.IsNull(pjdArray[2].DisplayNotes);
		}

	
		/// <summary>
		/// This tests if the method creates an array of public journey detail using the supplied public journey detail domain object array provided by a single journey result. In this case the journey leg observed is a frequency based leg. This results in the code using a different pathway to create the text.
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyDetailsDT_ec1r3hn_paddington_BusOnly_FrequencyBetween()
		{
			result = InitialiseJourneyResult("ec1r3hn_paddington_BusOnly.xml");
							
			journeyPlannerDTO.PublicJourneyDetail[] pjdArray = new
				journeyPlannerDTO.PublicJourneyDetail[result.OutwardPublicJourney(0).Details.Length];	
		
			pjdArray = JourneyPlannerAssembler.CreatePublicJourneyDetailsDT(result.OutwardPublicJourney(0).Details,rm);
				
			Console.WriteLine(pjdArray[2].Duration.ToString());
			Console.WriteLine(pjdArray[2].DurationText);				
			Console.WriteLine(pjdArray[2].MinFrequency.ToString());
			Console.WriteLine(pjdArray[2].MaxFrequency.ToString());
			Console.WriteLine(pjdArray[2].FrequencyText);
			Console.WriteLine(pjdArray[2].MaxDuration.ToString());
			Console.WriteLine(pjdArray[2].MaxDurationText);				
			
			Assert.AreEqual(pjdArray[2].Duration,14);
			Assert.AreEqual(pjdArray[2].DurationText, "Typical Duration: 14 minutes");				
			Assert.AreEqual(pjdArray[2].MinFrequency,2);
			Assert.AreEqual(pjdArray[2].MaxFrequency,5);
			Assert.AreEqual(pjdArray[2].FrequencyText,"Frequency: Every 2 to 5 minutes");
			Assert.AreEqual(pjdArray[2].MaxDuration,17);
			Assert.AreEqual(pjdArray[2].MaxDurationText,"Maximum duration: 17 minutes");		
		}

	
		/// <summary>
		/// This tests if the method creates an array of public journey detail using the supplied public journey detail domain object array provided by a single journey result. In this case the journey leg observed is a fixed frequency based leg. This results in the code using a different pathway to create the text.
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyDetailsDT_ec1r3hn_paddington_BusOnly_FrequencyFixed()
		{
			result = InitialiseJourneyResult("ec1r3hn_paddington_BusOnly.xml");
							
			journeyPlannerDTO.PublicJourneyDetail[] pjdArray = new
				journeyPlannerDTO.PublicJourneyDetail[result.OutwardPublicJourney(0).Details.Length];			
		
			pjdArray = JourneyPlannerAssembler.CreatePublicJourneyDetailsDT(result.OutwardPublicJourney(0).Details,rm);
				
			Console.WriteLine(pjdArray[1].FrequencyText);				
					
			Assert.AreEqual(pjdArray[1].FrequencyText,"Frequency: Every 4 minutes");			
		}


		/// <summary>
		/// As above. In this case the duration of the journey leg observed set to 29 seconds in order to produce text displaying the <30 second label. This results in the code using a different pathway to create the text.
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyDetailsDT_Test_GetDuration_LessThan30()
		{
			result = InitialiseJourneyResult("ec1r3hn_paddington_BusOnly.xml");
							
			journeyPlannerDTO.PublicJourneyDetail[] pjdArray = new
				journeyPlannerDTO.PublicJourneyDetail[result.OutwardPublicJourney(0).Details.Length];			

			result.OutwardPublicJourney(0).Details[0].Duration = 29;	
		
			pjdArray = JourneyPlannerAssembler.CreatePublicJourneyDetailsDT(result.OutwardPublicJourney(0).Details,rm);
				
			Console.WriteLine(pjdArray[0].DurationText);				
					
			Assert.AreEqual(pjdArray[0].DurationText,"Duration: < 30 seconds");			
		}

	
		/// <summary>
		/// As above. In this case the duration of the journey leg observed set to 175 seconds in order to test the rounding up functionality of the code. This results in the code using a different pathway to create the text.
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyDetailsDT_Test_Round_175Seconds()
		{
			result = InitialiseJourneyResult("ec1r3hn_paddington_BusOnly.xml");
							
			journeyPlannerDTO.PublicJourneyDetail[] pjdArray = new
				journeyPlannerDTO.PublicJourneyDetail[result.OutwardPublicJourney(0).Details.Length];			

			result.OutwardPublicJourney(0).Details[0].Duration = 175;	
		
			pjdArray = JourneyPlannerAssembler.CreatePublicJourneyDetailsDT(result.OutwardPublicJourney(0).Details,rm);
				
			Console.WriteLine(pjdArray[0].DurationText);				
					
			Assert.AreEqual(pjdArray[0].DurationText,"Duration: 3 minutes");			
		}

	
		/// <summary>
		/// This tests if the Method returns a null dto object when provided with a null domain object.
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyDetailsDT_null()
		{
			result = InitialiseJourneyResult("ec1r3hn_paddington_BusOnly.xml");
							
			journeyPlannerDTO.PublicJourneyDetail[] pjdArray = new
				journeyPlannerDTO.PublicJourneyDetail[result.OutwardPublicJourney(0).Details.Length];					
			pjdArray = JourneyPlannerAssembler.CreatePublicJourneyDetailsDT(null,rm);				
			Assert.AreEqual(pjdArray,null);			
		}

	
		/// <summary>
		/// This tests if the method creates a public journey result dto using the supplied public journey result domain object provided by a single journey result. 
		/// </summary>
		[Test]
		public void CallCreatePublicJourneyDT_Inverness_Folkestone()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");
			
			journeyPlannerDTO.PublicJourney pj = new journeyPlannerDTO.PublicJourney();
			
			pj = JourneyPlannerAssembler.CreatePublicJourneyDT(result.OutwardPublicJourney(1),
				result.OutwardJourneySummary(false)[1],rm);

			Console.WriteLine(pj.Details.Length.ToString());
			Console.WriteLine(pj.Summary.OriginDescription);
			Console.WriteLine(pj.Summary.DestinationDescription);

			Assert.AreEqual(pj.Details.Length,7);
			Assert.AreEqual(pj.Summary.OriginDescription, "Inverness");
			Assert.AreEqual(pj.Summary.DestinationDescription, "Folkestone Central");				
		}

	
		/// <summary>
		/// This tests if the method creates a tdjourney request domain object using an instansiated journeyrequest dto.
		/// </summary>
		[Test]
		public void CallCreateTDJourneyRequest()
		{
			journeyPlannerDTO.PublicJourneyRequest pjrDTO = new PublicJourneyRequest();
			pjrDTO.IsReturnRequired = true;
			pjrDTO.OutwardArriveBefore = false;
			pjrDTO.ReturnArriveBefore = false;
			pjrDTO.OutwardDateTime = new DateTime(2006,03,01);
			pjrDTO.ReturnDateTime = new DateTime(2006,03,02);
			pjrDTO.InterchangeSpeed = 50;
			pjrDTO.WalkingSpeed = 5;
			pjrDTO.MaxWalkingTime = 15;
			pjrDTO.Sequence = 10;

			ITDJourneyRequest tdjr = new TDJourneyRequest();
            
			tdjr = JourneyPlannerAssembler.CreateTDJourneyRequest(pjrDTO);
			foreach (cjpInterface.ModeType mode in tdjr.Modes)
			{
				Assert.IsNotNull( (mode == cjpInterface.ModeType.Air) || (mode == cjpInterface.ModeType.Bus) || (mode == cjpInterface.ModeType.Coach) || (mode == cjpInterface.ModeType.Ferry) || (mode == cjpInterface.ModeType.Metro) || (mode == cjpInterface.ModeType.Rail) || (mode == cjpInterface.ModeType.Tram) || (mode == cjpInterface.ModeType.Underground) );				
			}

			TDDateTime[] testOut = new TDDateTime[]{new TDDateTime(2006,03,01)};
			TDDateTime[] testReturn = new TDDateTime[]{new TDDateTime(2006,03,02)};

			Assert.AreEqual(tdjr.IsReturnRequired,true);
			Assert.AreEqual(tdjr.OutwardArriveBefore,false);
			Assert.AreEqual(tdjr.ReturnArriveBefore,false);
			Assert.AreEqual(tdjr.OutwardDateTime,testOut);
			Assert.AreEqual(tdjr.ReturnDateTime,testReturn);
			Assert.AreEqual(tdjr.InterchangeSpeed,50);
			Assert.AreEqual(tdjr.WalkingSpeed,5);
			Assert.AreEqual(tdjr.MaxWalkingTime,15);
			Assert.AreEqual(tdjr.Sequence,10);
			Assert.AreEqual(tdjr.PrivateAlgorithm, cjpInterface.PrivateAlgorithmType.Cheapest);
			Assert.AreEqual(tdjr.PublicAlgorithm, cjpInterface.PublicAlgorithmType.Default);
			Assert.AreEqual(tdjr.VehicleType, cjpInterface.VehicleType.Bicycle);

			//chack the default set parameters
			Assert.AreEqual(tdjr.DrivingSpeed,0);
			Assert.AreEqual(tdjr.AvoidMotorways,false);
			Assert.AreEqual(tdjr.OriginLocation,null);
			Assert.AreEqual(tdjr.ReturnOriginLocation, null);
			Assert.AreEqual(tdjr.ReturnDestinationLocation,null);
			Assert.AreEqual(tdjr.DoNotUseMotorways,false);
			Assert.AreEqual(tdjr.TrainUidFilter,null);
			Assert.AreEqual(tdjr.TrainUidFilterIsInclude,false);
			Assert.AreEqual(tdjr.PublicViaLocations,null);
			Assert.AreEqual(tdjr.PublicSoftViaLocations,null);
			Assert.AreEqual(tdjr.PublicNotViaLocations,null);
			Assert.AreEqual(tdjr.RoutingPointNaptans,null);
			Assert.AreEqual(tdjr.PrivateViaLocation,null);
			Assert.AreEqual(tdjr.AvoidRoads,null);
			Assert.AreEqual(tdjr.AlternateLocations,null);
			Assert.AreEqual(tdjr.AlternateLocationsFrom,false);
			Assert.AreEqual(tdjr.UseOnlySpecifiedOperators,false);
			Assert.AreEqual(tdjr.SelectedOperators,null);
			Assert.AreEqual(tdjr.IsTrunkRequest,false);
			Assert.AreEqual(tdjr.OutwardAnyTime,false);
			Assert.AreEqual(tdjr.ReturnAnyTime,false);
			Assert.AreEqual(tdjr.ViaLocationOutwardStopoverTime,null);
			Assert.AreEqual(tdjr.ViaLocationReturnStopoverTime,null);
			Assert.AreEqual(tdjr.ExtraCheckinTime,null);
			Assert.AreEqual(tdjr.DirectFlightsOnly,false);
			Assert.AreEqual(tdjr.AvoidTolls,false);
			Assert.AreEqual(tdjr.FuelPrice,null);
			Assert.AreEqual(tdjr.AvoidFerries,false);
			Assert.AreEqual(tdjr.FuelConsumption,null);
			Assert.AreEqual(tdjr.IncludeRoads,null);
			Assert.AreEqual(tdjr.FuelType,null);
			Assert.AreEqual(tdjr.CarSize,null);
		}

	
		/// <summary>
		/// This tests if the Method returns a null domain object when provided with a null dto object.
		/// </summary>
		[Test]
		public void CallCreateTDJourneyRequest_null()
		{
			ITDJourneyRequest tdjr = new TDJourneyRequest();            
			tdjr = JourneyPlannerAssembler.CreateTDJourneyRequest(null);
			Assert.AreEqual(tdjr,null);
		}		
		
	}
}
