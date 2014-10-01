// **************************************************** 
// NAME                 : TestRTTIManager.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 28/02/2005 
// DESCRIPTION  		: Nunit Test for RTTI Manager
// **************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/Test/TestRTTIManager.cs-arc  $ 
//
//   Rev 1.1   Feb 17 2010 16:42:30   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.0   Nov 08 2007 12:21:48   mturner
//Initial revision.
//
//   Rev 1.5   May 03 2006 18:10:34   kjosling
//Fixed unit tests
//
//   Rev 1.4   Feb 28 2006 09:58:30   tolomolaiye
//Added header information

using System;
using System.Xml ;
using System.Reflection; 
using TransportDirect.Common;
using System.Collections; 
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using TransportDirect.UserPortal.DepartureBoardService;   
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade ;
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager ;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.AdditionalDataModule ;  
using TransportDirect.UserPortal.DepartureBoardService.StopEventManager;
using TransportDirect.UserPortal.LocationService;

using TransportDirect.Common.DatabaseInfrastructure;

namespace TransportDirect.UserPortal.DepartureBoardService.Test
{
	public class TestInitialization : IServiceInitialisation
	{

		public void Populate(Hashtable serviceCache)
		{
			serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory());
			// Enable PropertyService
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			// adding additionalData				
			serviceCache.Add (ServiceDiscoveryKey.AdditionalData, new AdditionalDataFactory());

			// adding RTTILookupHandlerFactory
			serviceCache.Add (ServiceDiscoveryKey.RTTILookupHandler , new RTTILookupHandlerFactory());

			// adding RDHandlerFactory
			serviceCache.Add (ServiceDiscoveryKey.RTTIManager , new RDHandlerFactory());

			serviceCache.Add(ServiceDiscoveryKey.StopEventManager, new StopEventMockManager());

			serviceCache.Add( ServiceDiscoveryKey.Cache, new TestTDCache() );

			// Enable logging service.
			ArrayList errors = new ArrayList();
			try
			{    
				IEventPublisher[] customPublishers = new IEventPublisher[0];
				
				Logger.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException tdEx)
			{
				foreach(string error in errors)
				{
					Console.WriteLine(error);
				}
				throw tdEx;
			}
		}
	}

	/// <summary>
	/// Summary description for TestRTTIManager.
	/// </summary>
	[TestFixture] 
	public class TestRTTIManager
	{
		
		[Ignore("Manual setup")]
		public void ManualSetup()
		{
			// Make sure that RTTI Mock listener is running in the background. 
			// If its not running then most of the test method would simply fails.			
		}
		
		public TestRTTIManager()
		{
		}

		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[SetUp]
		public void Init()
		{
			try
			{

				TDServiceDiscovery.ResetServiceDiscoveryForTest();
				// Initialise services
				TDServiceDiscovery.Init( new  TestInitialization() );
				
			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message.ToString());    
			}
		}

		

		[TearDown]
		public void TearDown()
		{

		}



		[Test]
		public void TestXmlExtractionForStation()
		{
				RTTIXmlExtractor xmlExtractor = new RTTIXmlExtractor() ;
			string xmlFrag = "";
			string outstring = "";
			DBSResult dbresult = new DBSResult() ;  
			try
			{	
				xmlFrag= GetSampleResponse(TemplateRequestType.TripRequestByCRS); 
				
				
				if (xmlFrag == null || xmlFrag == "")
				{
					Assert.Fail("Please check the filepath .."); 
				}
				if (! xmlExtractor.ExtractData(TemplateRequestType.TripRequestByCRS  , xmlFrag,   dbresult))
				{
					Assert.Fail("Unable to extract data");  
				}
				else
				{
					
					DBSEvent dbsEvent = new DBSEvent(); 
					foreach (TrainStopEvent trainstopEvent in  dbresult.StopEvents)
					{
						dbsEvent = trainstopEvent.Arrival;

												outstring = "From: " +  trainstopEvent.Departure.Stop.Name  + " To: " + trainstopEvent.Arrival.Stop.Name.ToString()   + " Requested Stop: " + trainstopEvent.Stop.Stop.Name.ToString()   ;						
						outstring += " arrive: " +  trainstopEvent.Stop.ArriveTime.Hour.ToString() + ":" +  trainstopEvent.Stop.ArriveTime.Minute.ToString();      
						outstring += " depart: " +  trainstopEvent.Stop.DepartTime.Hour.ToString() + ":" +   trainstopEvent.Stop.DepartTime.Minute.ToString();

						outstring += " Service No: " + trainstopEvent.Service.ServiceNumber ;   


						Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					}
				}

			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);   
			}
		}


		
		[Test]
		public void TestXmlExtractionForTrain()
		{
				RTTIXmlExtractor xmlExtractor = new RTTIXmlExtractor() ;
			string xmlFrag = "";
			string outstring = "";
			DBSResult dbresult = new DBSResult() ;  
			try
			{	
				//xmlFrag= GetSampleResponse(TemplateRequestType.TripRequestByCRS); 
				xmlFrag= GetSampleResponse(TemplateRequestType.TrainRequestByRID); 
				
				if (xmlFrag == null || xmlFrag == "")
				{
					Assert.Fail("Please check the filepath .."); 
				}
				xmlExtractor.RequestedStop = "9100COKSBDG";
				xmlExtractor.ShowCallingStop = true; 

				if (! xmlExtractor.ExtractData(TemplateRequestType.TrainRequestByRID , xmlFrag,   dbresult))
				{
					Assert.Fail("Unable to extract data");  
				}
				else
				{
					
					DBSEvent dbsEvent = new DBSEvent(); 
					foreach (TrainStopEvent trainstopEvent in  dbresult.StopEvents)
					{
							dbsEvent = trainstopEvent.Arrival;

						outstring = "From: " +  trainstopEvent.Departure.Stop.Name  + " To: " + trainstopEvent.Arrival.Stop.Name.ToString()   + " Requested Stop: " + trainstopEvent.Stop.Stop.Name.ToString()   ;						
						outstring += " arrive: " +  trainstopEvent.Stop.ArriveTime.Hour.ToString() + ":" +  trainstopEvent.Stop.ArriveTime.Minute.ToString();      
						outstring += " depart: " +  trainstopEvent.Stop.DepartTime.Hour.ToString() + ":" +   trainstopEvent.Stop.DepartTime.Minute.ToString();

						outstring += " Service No: " + trainstopEvent.Service.ServiceNumber ;   


						Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					}
				}

			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);   
			}
		}

		

		[Test]
		public void TestErrorResponse()
		{
			RTTIXmlExtractor xmlExtractor = new RTTIXmlExtractor() ;
			string xmlFrag = "";
			string outstring = "";
			DBSResult dbresult = new DBSResult() ;  
			try
			{	
				//xmlFrag= GetSampleResponse(TemplateRequestType.TripRequestByCRS); 
				xmlFrag= GetSampleResponse(TemplateRequestType.ErrorResponse); 
				
				if (xmlFrag == null || xmlFrag == "")
				{
					Assert.Fail("Please check the filepath .."); 
				}
				xmlExtractor.RequestedStop = "9100COKSBDG";
				xmlExtractor.ShowCallingStop = true; 

				if (! xmlExtractor.ExtractData(TemplateRequestType.TrainRequestByRID , xmlFrag,   dbresult))
				{
					Assert.Fail("Unable to extract data");  
				}
				else
				{
					
					foreach (DBSMessage  dbsMessage in  dbresult.Messages)
					{
						
						outstring = " Error code: " + dbsMessage.Code.ToString() + " Error Description: "  + dbsMessage.Description ;     
						Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					}
				}

			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);   
			}
		}


		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestGetDepartureBoardStopForStation()
		{	RDHandler rdHandler=null;
			DBSResult dbresult = new DBSResult() ;  
			DBSLocation dbsLoc = new DBSLocation(); 
			try
			{	
				// start mock server
				//StartMockServer();

				// getting discovery data 
				rdHandler = (RDHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.RTTIManager];   
				
				dbsLoc.Code = "SYB";

                dbresult = rdHandler.GetDepartureBoardStop(dbsLoc, string.Empty, string.Empty, false, false);
				if (dbresult.Messages.Length > 0 )
				{	
					Assert.Fail("Unable to extract data");  
				}
				else
				{
					PrintResult(dbresult); 
				}
			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);
			}
			finally
			{
				rdHandler.Dispose();  
			}

		}
		
		
		[Test]
		public void TestGetDepartureBoardTripDeparture()
		{			
			DBSResult dbresult = new DBSResult() ;  
			DBSLocation origin = new DBSLocation(); 
			DBSLocation destination = new DBSLocation(); 
			string outstring = "";
			try
			{	
				
				origin.Code = "SYB";
				DBSTimeRequest dbsTimeReq = new DBSTimeRequest();
				dbsTimeReq.Type = TimeRequestType.TimeToday  ;
				dbsTimeReq.Hour = 20;
				dbsTimeReq.Minute = 10;
				
				bool showCallingPoints = true; 
				bool showDeparture = true;
				int range = 100; 
				string serviceNumber = string.Empty ;
				DBSRangeType dbsRangeType = DBSRangeType.Interval ;
				

						
				// With origin == null and destination == null		
				origin = null;
				destination = new DBSLocation();
				destination.Code = "SYB";

                dbresult = GetDepartureBoardTrip(origin, origin, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				
				if (dbresult.Messages.Length > 0 )
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== TestGetDepartureBoardTripDeparture With origin == null and destination == null  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}



				// With destination == null	and origin== blank							
				destination = null;
				origin = new DBSLocation();
                dbresult = GetDepartureBoardTrip(origin, destination, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				
				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== TestGetDepartureBoardTripDeparture With destination == null and origin== blank ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}
				
				
				// With destination == null	and origin.Code == SYB
				destination = null;
				origin = new DBSLocation(); 
				origin.Code = "SYB";

                dbresult = GetDepartureBoardTrip(origin, destination, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);

				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== TestGetDepartureBoardTripDeparture With destination == null and origin.Code == SYB ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}
			
				
				// With destination != null		
				destination = new DBSLocation();
 				destination.Code = "SYB";

                dbresult = GetDepartureBoardTrip(origin, origin, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				if (dbresult.Messages.Length > 0)
				{	
//					Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== TestGetDepartureBoardTripDeparture With destination != null  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}
				

				// With dbsRangeType = DBSRangeType.Sequence and range = -1
				dbsRangeType = DBSRangeType.Sequence ; 
				range = -1;

                dbresult = GetDepartureBoardTrip(origin, origin, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);

				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== TestGetDepartureBoardTripDeparture With dbsRangeType = DBSRangeType.Sequence and range = -1  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}
				
				//  With dbsRangeType = DBSRangeType.Sequence and range = 10000
				dbsRangeType = DBSRangeType.Sequence ; 
				range = 10000;

                dbresult = GetDepartureBoardTrip(origin, origin, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				if (dbresult.Messages.Length > 0)				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{	
					outstring = "====================TestGetDepartureBoardTripDeparture With dbsRangeType = DBSRangeType.Sequence and range = 10000  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}


			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);
			}

		}

		
		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestGetDepartureBoardTripArrival()
		{			
			DBSResult dbresult = new DBSResult() ;  
			DBSLocation origin = new DBSLocation(); 
			DBSLocation destination = new DBSLocation(); 
			string outstring = "";
			try
			{	
				
				origin.Code = "SYB";
				DBSTimeRequest dbsTimeReq = new DBSTimeRequest();
				dbsTimeReq.Type = TimeRequestType.TimeToday  ;
				dbsTimeReq.Hour = 20;
				dbsTimeReq.Minute = 10;
				
				bool showCallingPoints = true; 
				bool showDeparture = false;
				int range = 100; 
				string serviceNumber = string.Empty ;
				DBSRangeType dbsRangeType = DBSRangeType.Interval ;
				

						
				// With origin == null and destination == null		
				origin = null;
				destination = new DBSLocation();
				destination.Code = "SYB";

                dbresult = GetDepartureBoardTrip(origin, origin, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);    
				
				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With origin == null and destination == null  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}



				// With destination == null	and origin== blank							
				destination = null;
				origin = new DBSLocation();

                dbresult = GetDepartureBoardTrip(origin, destination, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With destination == null and origin== blank ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}
				
				
				// With destination == null	and origin.Code == SYB
				destination = null;
				origin = new DBSLocation(); 
				origin.Code = "SYB";

                dbresult = GetDepartureBoardTrip(origin, destination, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				if (dbresult.Messages.Length > 0 )
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With destination == null and origin.Code == SYB ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}
			
				
				// With destination != null		
				destination = new DBSLocation();
				destination.Code = "SYB";

                dbresult = GetDepartureBoardTrip(origin, origin, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);    
				if (dbresult.Messages.Length > 0 )
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With destination != null  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}
				

				// With dbsRangeType = DBSRangeType.Sequence and range = -1
				dbsRangeType = DBSRangeType.Sequence ; 
				range = -1;

                dbresult = GetDepartureBoardTrip(origin, origin, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				if (dbresult.Messages.Length > 0 )
				{	
					//Assert.Fail("Unable to extract data"); 
 					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With dbsRangeType = DBSRangeType.Sequence and range = -1  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}
				
				//  With dbsRangeType = DBSRangeType.Sequence and range = 10000
				dbsRangeType = DBSRangeType.Sequence ; 
				range = 10000;
                dbresult = GetDepartureBoardTrip(origin, origin, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				if (dbresult.Messages.Length > 0)
				{	
					Assert.Fail("Unable to extract data");  					
				}
				else
				{	
					outstring = "====================GetDepartureBoardTripArrival With dbsRangeType = DBSRangeType.Sequence and range = 10000  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}
			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);
			}

		}

		

		[Test]
		public void TestGetDepartureBoardTrip_PNZEDI()
		{			
			DBSResult dbresult = new DBSResult() ;  
			DBSLocation origin = new DBSLocation(); 
			DBSLocation destination = new DBSLocation(); 
			string outstring = "";
			try
			{	
				
				origin.Code = "PNZ";
				origin.NaptanIds  = new string[]{"9100PENZNCE"} ;
				DBSTimeRequest dbsTimeReq = new DBSTimeRequest();
				dbsTimeReq.Type = TimeRequestType.TimeToday  ;
				dbsTimeReq.Hour = 20;
				dbsTimeReq.Minute = 10;
				
				bool showCallingPoints = true; 
				bool showDeparture = false;
				int range = 100; 
				string serviceNumber = string.Empty ;
				DBSRangeType dbsRangeType = DBSRangeType.Interval ;
						
			
				destination = new DBSLocation();
				destination.Code = "EDI";
				destination.NaptanIds = new string[]{"9100EDINBUR"}; 
				// PNZ--> EDI  (Arrival)
				// for arrival
                dbresult = GetDepartureBoardTrip(origin, destination, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				
				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With origin == PNZ and destination == EDI and for arrival  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}

				// PNZ--> EDI  (departure)
				// for departure 
				showDeparture = true;

                dbresult = GetDepartureBoardTrip(origin, destination, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);    
				
				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With origin == PNZ and destination == EDI and for deparure  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}

				

				// PNZ--> BRI  (Arrival)
				// for arrival
				showDeparture = false;
				origin.Code = "PNZ";
				origin.NaptanIds = new string[]{"9100PENZNCE"};
				destination.Code = "BRI"; 
				destination.NaptanIds = new string[]{"9100BRSTLTM"};

                dbresult = GetDepartureBoardTrip(origin, destination, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				
				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With origin == PNZ and destination == BRI and for arrival  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}

				// PNZ--> BRI  (departure)
				// for departure 
				showDeparture = true;

                dbresult = GetDepartureBoardTrip(origin, destination, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With origin == PNZ and destination == BRI and for deparure  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}
				

				
				// BRI--> RUN  (Arrival)
				// for arrival
				showDeparture = false;
				origin.Code = "BRI";
				origin.NaptanIds = new string[]{"9100BRSTLTM"};
				destination.Code = "RUN"; 
				destination.NaptanIds = new string[]{"9100RUNCORN"};

                dbresult = GetDepartureBoardTrip(origin, destination, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With origin == BRI and destination == RUN and for arrival  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}

				// BRI--> RUN  (departure)
				// for departure 
				showDeparture = true;
                dbresult = GetDepartureBoardTrip(origin, destination, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With origin == BRI and destination == RUN and for deparure  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}


				
				
				// BRI--> EDI  (Arrival)
				// for arrival
				showDeparture = false;
				origin.Code = "BRI";
				origin.NaptanIds = new string[]{"9100BRSTLTM"};
				destination.Code = "EDI"; 
				destination.NaptanIds = new string[] {"9100EDINBUR"};
                dbresult = GetDepartureBoardTrip(origin, destination, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				
				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With origin == BRI and destination == EDI and for arrival  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}

				// BRI--> RUN  (departure)
				// for departure 
				showDeparture = true;
                dbresult = GetDepartureBoardTrip(origin, destination, string.Empty, string.Empty, dbsTimeReq, dbsRangeType, range, showDeparture, showCallingPoints);
				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  					
					PrintError(dbresult); 
				}
				else
				{
					outstring = "==================== GetDepartureBoardTripArrival With origin == BRI and destination == EDI and for deparure  ========================";
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					PrintResult(dbresult); 
				}

				

			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);
			}			

		}
		
		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestGetDepartureBoardFirstService()
		{
			RDHandler rdHandler=null;
			DBSResult dbresult = new DBSResult() ;  
			DBSLocation origin = new DBSLocation(); 
			
			try
			{	
				// start mock server
				//StartMockServer();

				// getting discovery data 
				rdHandler = (RDHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.RTTIManager];   
				
				origin.Code = "SYB";
				DBSTimeRequest dbsTimeReq = new DBSTimeRequest();
				dbsTimeReq.Type = TimeRequestType.First ;
				dbsTimeReq.Hour = 20;
				dbsTimeReq.Minute = 10;

                dbresult = rdHandler.GetDepartureBoardTrip(origin, origin, string.Empty, string.Empty, dbsTimeReq, DBSRangeType.Interval, 100, true, true);
				if (dbresult.Messages.Length > 0 )
				{	
					//PrintError(dbresult);  
					Assert.Fail("Unable to extract data");  
				}
				else
				{	Assert.IsTrue( dbresult.StopEvents.Length == 1); 
					PrintResult(dbresult); 
				}
			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);
			}
			finally
			{
				rdHandler.Dispose();  
			}

		}
		


		
		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestGetDepartureBoardLastService()
		{
			RDHandler rdHandler=null;
			DBSResult dbresult = new DBSResult() ;  
			DBSLocation origin = new DBSLocation(); 
			
			try
			{	
				// start mock server
				//StartMockServer();

				// getting discovery data 
				rdHandler = (RDHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.RTTIManager];   
				
				origin.Code = "SYB";
				DBSTimeRequest dbsTimeReq = new DBSTimeRequest();
				dbsTimeReq.Type = TimeRequestType.Last ;
				dbsTimeReq.Hour = 20;
				dbsTimeReq.Minute = 10;

                dbresult = rdHandler.GetDepartureBoardTrip(origin, origin, string.Empty, string.Empty, dbsTimeReq, DBSRangeType.Interval, 100, true, true);
				
				if (dbresult.Messages.Length > 0 )
				{	
					Assert.Fail("Unable to extract data");  
				}
				else
				{	Assert.IsTrue( dbresult.StopEvents.Length == 1);
					PrintResult(dbresult); 
				}
			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);
			}
			finally
			{
				rdHandler.Dispose();  
			}

		}
		


		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestGetDepartureBoardTrain()
		{
			RDHandler rdHandler=null;
			DBSResult dbresult = new DBSResult() ;  
			DBSLocation origin = new DBSLocation(); 
			DBSLocation dbsLoc = new DBSLocation(); 
			string outstring = "";
			try
			{	
				// start mock server
				//StartMockServer();

				// getting discovery data 
				rdHandler = (RDHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.RTTIManager];   
								
				string trainRID = "200411120961691";
				
				// With dbsLoc == null
				dbsLoc = null;
				outstring = "==================== TestGetDepartureBoardTrain dbsLoc == null ========================";
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
				dbresult = rdHandler.GetDepartureBoardStop(dbsLoc, string.Empty,trainRID, false, true);

				if (dbresult.Messages.Length > 0)
				{	
					//Assert.Fail("Unable to extract data");  
					PrintError(dbresult); 
				}
				else
				{	
					PrintResultForTrain(dbresult); 
				}

                
					
				// With Valid Naptan Id and Show calling == true
				dbsLoc = new DBSLocation() ;
				dbsLoc.Code = "SYB";
				dbsLoc.NaptanIds = new string[]{"9100COKSBDG"}; 

				outstring = "==================== TestGetDepartureBoardTrain With Valid Naptan Id and Show calling points == true ========================";
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));

                dbresult = rdHandler.GetDepartureBoardStop(dbsLoc, string.Empty, trainRID, false, true);
				if (dbresult.Messages.Length > 0)
				{	
					PrintError(dbresult); 
					//Assert.Fail("Unable to extract data");  
				}
				else
				{	
					PrintResultForTrain(dbresult); 
				}
				
					
				// With Valid Naptan Id and and Show calling == false
				dbsLoc = new DBSLocation() ;
				dbsLoc.Code = "SYB";
				dbsLoc.NaptanIds = new string[]{"9100COKSBDG"}; 
				outstring = "==================== TestGetDepartureBoardTrain With Valid Naptan Id and Show calling points == false ========================";
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));

                dbresult = rdHandler.GetDepartureBoardStop(dbsLoc, string.Empty, trainRID, false, false);
				if (dbresult.Messages.Length > 0)
				{	
					Assert.Fail("Unable to extract data");  
				}
				else
				{	
					PrintResultForTrain(dbresult); 
				}

				// With inValid TrainId
				trainRID = "200411120961691xx";				
				outstring = "==================== TestGetDepartureBoardTrain dbsLoc == null ========================";
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));

                dbresult = rdHandler.GetDepartureBoardStop(dbsLoc, string.Empty, trainRID, false, true);

				//if (dbresult.StopEvents.Length == 0 &&  dbresult.Messages.Length == 0 ) 
					  
				if (dbresult.Messages.Length > 0   )
				{	
					//Assert.Fail("Unable to extract data");  
					PrintError(dbresult); 
				}
				else
				{	
					PrintResultForTrain(dbresult); 
				}


			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);
			}
			finally
			{
				rdHandler.Dispose();  
			}

		}



		private DBSResult GetDepartureBoardTrip(DBSLocation origin, DBSLocation destination, string operatorCode, string serviceNumber ,DBSTimeRequest dbsTimeReq,  DBSRangeType dbsRangeType, int range, bool showDeparture, bool showCallingStop)
		{
			RDHandler rdHandler=null;			
			
			try
			{	
				
				// getting discovery data 
				rdHandler = (RDHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.RTTIManager];   			
				    
				return rdHandler.GetDepartureBoardTrip(origin, destination, operatorCode, serviceNumber, dbsTimeReq ,dbsRangeType, range, showDeparture ,showCallingStop);
				
			}			
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);
				return null;
			}
			finally
			{
				rdHandler.Dispose();  
			}
		}
		

		private string GetSampleResponse(TemplateRequestType reqType )
		{	
			string filepath = string.Empty;
			string replaceVal = @"file:\";
			string folderPath = string.Empty;
			string	responsePath = string.Empty; 
			folderPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
			folderPath = folderPath.Replace(replaceVal, "");
			//filepath = filepath.Substring(0, filepath.IndexOf("bin")) +  @"Test\RTTITestResponses\";
			responsePath = System.Configuration.ConfigurationManager.AppSettings["RTTITestResponsesFilesPath"];
			filepath = System.IO.Path.Combine(folderPath,  responsePath);	
			return RTTIUtilities.GetXmlStringForFile(reqType, filepath); 
			
		}


		

//		private void StartMockServer()
//		{	
//			string filepath = string.Empty;
//			string replaceVal = @"file:\";
//			filepath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
//			filepath = filepath.Replace(replaceVal, "");
//			filepath = filepath.Substring(0, filepath.IndexOf("bin")) +  @"Test\MockListener\SocketListner.exe";
//
//			// Start Mock TCP/IP server 
//			System.Diagnostics.Process.Start(filepath);
//			
//			//System.Diagnostics.Process.Start(@"C:\Sanjeev\SocketListner\bin\Debug\SocketListner.exe");			
//		}



		
		private void PrintError(DBSResult dbsresult)
		{	string outstring = string.Empty ;
			try
			{	Assert.IsTrue(dbsresult.Messages.Length == 1 );
				foreach (DBSMessage  dbsMessage in  dbsresult.Messages)
				{
						
					outstring = " Printing DBSMessage error code: " + dbsMessage.Code.ToString() + " Error Description: "  + dbsMessage.Description ;     
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
				}
			}
			catch(Exception ex)
			{
				outstring = "No result found" + ex.Message ;
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
			}
		}


		private void PrintResult(DBSResult dbsresult )
		{	string outstring = string.Empty ;
			int iResultCount = 0;
			try
			{
				//DBSEvent dbsEvent = new DBSEvent(); 
				Assert.IsTrue(dbsresult.StopEvents.Length > 0 );
				 	
				foreach (TrainStopEvent trainstopEvent in  dbsresult.StopEvents)
				{
					

					DBSEvent arrival = new DBSEvent();
					DBSEvent stop = new DBSEvent();
					DBSEvent departure = new DBSEvent();

					arrival = trainstopEvent.Arrival;
					stop =   trainstopEvent.Stop;
					departure = trainstopEvent.Departure ;
  
					if (arrival == null)
					{
						arrival = stop;
					}

					if (departure == null)
					{
						departure = stop;
					}

					outstring =  "From: " +  departure.Stop.Name.ToString() + "\t" + " To: " + arrival.Stop.Name.ToString()  + "\t" + " Stop: " + stop.Stop.Name.ToString()  + "\t"  ;						
					if (stop.ArriveTime != DateTime.MinValue)
					{
						outstring += " arrive: " +  stop.ArriveTime.Hour.ToString() +  ":" +  stop.ArriveTime.Minute.ToString() + "\t" ;      
					}
					if (stop.DepartTime != DateTime.MinValue)
					{
						outstring += " depart: " +  stop.DepartTime.Hour.ToString() +  ":" +   stop.DepartTime.Minute.ToString() + "\t";
					}
				
					outstring += " Service No: " + trainstopEvent.Service.ServiceNumber ;   
				

					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					iResultCount++;
				}
				//outstring = "Actual result: " + dbsresult.StopEvents.Length.ToString() + " result printed:  " +  iResultCount.ToString();  
				//Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
			}
			catch(Exception ex)
			{
				outstring = "No error message found";
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
				Assert.Fail("Unable to print result" + ex.Message);  
			}
		}





		private void PrintResultForTrain(DBSResult dbsresult )
		{
				string outstring = string.Empty ;
			int iResultCount = 0;
			try
			{
				//DBSEvent dbsEvent = new DBSEvent(); 
				 	
				foreach (TrainStopEvent trainstopEvent in  dbsresult.StopEvents)
				{
					

					DBSEvent arrival = new DBSEvent();
					DBSEvent stop = new DBSEvent();
					DBSEvent departure = new DBSEvent();
					DBSEvent[] previous ;
					DBSEvent[] next  ;

					arrival = trainstopEvent.Arrival;
					stop =   trainstopEvent.Stop;
					departure = trainstopEvent.Departure ;
					previous  = trainstopEvent.PreviousIntermediates ;
					next = trainstopEvent.OnwardIntermediates;
 
  
					if (arrival == null)
					{
						arrival = stop;
					}

					if (departure == null)
					{
						departure = stop;
					}
					



					outstring =  "From: " +  departure.Stop.Name.ToString() + "\t" + " To: " + arrival.Stop.Name.ToString()  + "\t" ;
					
					if (stop!= null)
					{
						outstring+= " Stop: " + stop.Stop.Name.ToString()  + "\t"  ;	
						if (stop.ArriveTime != DateTime.MinValue)
						{
							outstring += " arrive: " +  stop.ArriveTime.Hour.ToString() +  ":" +  stop.ArriveTime.Minute.ToString() + "\t" ;      
						}
						if (stop.DepartTime != DateTime.MinValue)
						{
							outstring += " depart: " +  stop.DepartTime.Hour.ToString() +  ":" +   stop.DepartTime.Minute.ToString() + "\t";
						}
					}

					DBSEvent pStop ;

					for(int i= 0; i < previous.Length ; i++ )
					{
						pStop = previous[i];

						if (pStop != null )
						{
							outstring += "pStop:" + pStop.Stop.Name.ToString() ; 
							if (pStop.ArriveTime != DateTime.MinValue)
							{
								outstring +=  " arrive: " +  pStop.ArriveTime.Hour.ToString() + ":" +  pStop.ArriveTime.Minute.ToString() + "\t";
							}

							if (pStop.DepartTime != DateTime.MinValue)
							{
								outstring +=  " depart: " +  pStop.DepartTime.Hour.ToString() + ":" +  pStop.DepartTime.Minute.ToString() + "\t";
							}


						}
					}

					DBSEvent nStop ;

					for(int i= 0; i < next.Length ; i++ )
					{
						nStop = next[i];

						if (nStop != null )
						{
							outstring += "nStop:" + nStop.Stop.Name.ToString() ; 
							if (nStop.DepartTime  != DateTime.MinValue)
							{
								outstring +=  " arrive: " +  nStop.ArriveTime.Hour.ToString() +  ":" +  nStop.ArriveTime.Minute.ToString() + "\t";
							}

							if (nStop.DepartTime != DateTime.MinValue)
							{
								outstring +=  " depart: " + nStop.DepartTime.Hour.ToString() + ":" +  nStop.DepartTime.Minute.ToString() + "\t";
							}
						}
					}


				
				
					outstring += " Service No: " + trainstopEvent.Service.ServiceNumber ;   
				

					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					iResultCount++;
				}
				outstring = "Actual result: " + dbsresult.StopEvents.Length.ToString() + " result printed:  " +  iResultCount.ToString();  
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
			}
			catch(Exception ex)
			{
				outstring = "No error message found";
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
				Assert.Fail("Unable to print result" + ex.Message);  
			}
		}



	}
}
