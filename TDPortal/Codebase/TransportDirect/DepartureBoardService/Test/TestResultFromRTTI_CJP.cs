// *********************************************** 
// NAME                 : TestResultFromRTTI_CJP.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 11/03/2005 
// DESCRIPTION  		: Nunit Test class to switch between RTTI and CJP
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/Test/TestResultFromRTTI_CJP.cs-arc  $ 
//
//   Rev 1.1   Feb 17 2010 16:42:28   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.0   Nov 08 2007 12:21:48   mturner
//Initial revision.
//
//   Rev 1.4   Feb 10 2006 09:20:52   kjosling
//Turned off unit test
//
//   Rev 1.3   Apr 25 2005 17:22:16   schand
//Added more code in test initialisation.
//
//   Rev 1.2   Mar 15 2005 13:54:10   schand
//Added more test to check the type as well
//
//   Rev 1.1   Mar 14 2005 16:59:44   schand
//Added ReStoreOriginalValue()
//
//   Rev 1.0   Mar 14 2005 15:17:04   schand
//Initial revision.



using System;
using System.Xml ;
using System.Reflection; 
using System.Data;
using TransportDirect.Common;
using System.Collections; 
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using TransportDirect.Common.Logging ;  
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;  
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.DepartureBoardService.StopEventManager;
using TransportDirect.JourneyPlanning.CJPInterface;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.DepartureBoardService;   
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager ;
using TransportDirect.UserPortal.AdditionalDataModule; 
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.DatabaseInfrastructure ;
   
   




namespace TransportDirect.UserPortal.DepartureBoardService.Test
{
	/// <summary>
	/// Summary description for TestResultFromRTTI_CJP.
	/// </summary>
	[TestFixture] 
	public class TestResultFromRTTI_CJP
	{	
		string token= string.Empty ;		
		DBSLocation originLocation = new DBSLocation() ;
		DBSLocation destinationLocation = new DBSLocation();
        string operatorCode = string.Empty;
        string serviceNumber = string.Empty ;
        DBSTimeRequest time = new DBSTimeRequest(); 
		DBSRangeType rangeType = DBSRangeType.Sequence ; 
		int range = 5;
		bool showDepartures = true;
		bool showCallingStops = true;
	    DBSResult dbsResult ; 
	    DepartureBoardService dbs = new DepartureBoardService();
		string originalGetTrainInfoFromCJP = string.Empty ;

		public TestResultFromRTTI_CJP()
		{
			time.Hour = DateTime.Today.Hour ;
			time.Minute = DateTime.Today.Minute ;
			time.Type = TimeRequestType.Now ;
			
		}

		[SetUp]
		public void Init()
		{
			try
			{

				TDServiceDiscovery.ResetServiceDiscoveryForTest();
				// Initialise services
				TDServiceDiscovery.Init( new  LocalTestInitialization() );				
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
		public void TestDepartureBoardTripFromCJP()
		{
			originLocation = new DBSLocation();
 			destinationLocation = new DBSLocation();
			dbsResult = new DBSResult();  
			originLocation.Code = "EUS";
			originLocation.NaptanIds = new string[2]{"9100EUS1", "9100EUS1"};
			originLocation.Valid = true;
			destinationLocation = null;
			showDepartures = true;
			
			// store original value
			originalGetTrainInfoFromCJP = GetTrainResultFromCJP().ToString().Trim().ToLower();    

			// Getting data from CJP
			// Make sure properties should be set to get the value 
			if (!DoSetUp(true))
			{
				Assert.Fail("Unable to prepare set up for this test.. " );
			}
			Assert.IsTrue(GetTrainResultFromCJP(), "DepartureBoardService.GetTrainInfoFromCJP key should have value 'true' for this test. Restart the test again." );
            
			
			dbsResult = dbs.GetDepartureBoardTrip(token, originLocation, destinationLocation , 
										operatorCode, serviceNumber , time , rangeType , range, 
										showDepartures, showCallingStops);  

			// restore original val
			ReStoreOriginalValue(); 

			if (dbsResult.Messages.Length > 0)
			{	PrintError(dbsResult);  
				Assert.Fail("Unable to extract data");  			
			}
			else
			{
				Assert.IsTrue( dbsResult.StopEvents.Length >= 1); 
				Assert.IsTrue((!(dbsResult.StopEvents[0] is TrainStopEvent)) , "The result should not be of type TrainStopEvent" ) ;
				PrintResult(dbsResult); 
			}


		}

		
		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestDepartureBoardTripFromRTTI()
		{
			originLocation = new DBSLocation();
			destinationLocation = new DBSLocation();
			dbsResult = new DBSResult();  
			originLocation.Code = "EUS";
			originLocation.NaptanIds = new string[2]{"9100EUS1", "9100EUS1"};
			originLocation.Valid = true;
			destinationLocation = null;
			showDepartures = true;
			
			// store original value
			originalGetTrainInfoFromCJP = GetTrainResultFromCJP().ToString().Trim().ToLower();    

			// Getting data from CJP
			// Make sure properties should be set to get the value 
			if (!DoSetUp(false))
			{
				Assert.Fail("Unable to prepare set up for this test.. " );
			}
			Assert.IsTrue((!GetTrainResultFromCJP()), "DepartureBoardService.GetTrainInfoFromCJP key should have value 'false' for this test. Restart the test again." );

			dbsResult = dbs.GetDepartureBoardTrip(token, originLocation, destinationLocation , 
				operatorCode, serviceNumber , time , rangeType , range, 
				showDepartures, showCallingStops);  

			// restore original val
			ReStoreOriginalValue(); 

			if (dbsResult.Messages.Length > 0 )
			{
				PrintError(dbsResult);  
				Assert.Fail("Unable to extract RTTI data, please make sure that RTTI server is available.");  			
			}
			else
			{
				Assert.IsTrue( dbsResult.StopEvents.Length >= 1); 
				Assert.IsTrue(dbsResult.StopEvents[0] is TrainStopEvent , "The result is not of type TrainStopEvent" ) ;
				PrintResult(dbsResult); 
			}


		}

		
		#region TestHelper
		private void PrintResult(DBSResult dbsresult )
		{
			string outstring = string.Empty ;
			int iResultCount = 0;
			StopEvent sv = new StopEvent();
			TrainStopEvent tv = new TrainStopEvent();
 
			try
			{
				//DBSEvent dbsEvent = new DBSEvent(); 
				Assert.IsTrue(dbsresult.StopEvents.Length > 0 );
				
				

				foreach (DepartureBoardFacade.DBSStopEvent dbsStopEvent in  dbsresult.StopEvents)
				{   				  
					DBSEvent arrival = new DBSEvent();
					DBSEvent stop = new DBSEvent();
					DBSEvent departure = new DBSEvent();				

					arrival = dbsStopEvent.Arrival;
					stop =   dbsStopEvent.Stop;
					departure = dbsStopEvent.Departure ;
  
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
				
					outstring += " Service No: " + dbsStopEvent.Service.ServiceNumber ;   
				

					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring));
					iResultCount++;
				}				
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
		
		private void PrintError(DBSResult dbsresult)
		{
				string outstring = string.Empty ;
			try
			{
				Assert.IsTrue(dbsresult.Messages.Length >= 1 );
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

		private bool DoSetUp(bool getTrainInfoFromCJP)
		{	string getResultFromCJP = "false";
			SqlHelper sqlHelper = new SqlHelper();

			try
			{
						
				if (getTrainInfoFromCJP)
				{
					getResultFromCJP = "true";
				}
				// check if user wants any data at this point 
				//open connection to TransientPortalDB
				sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);
				sqlHelper.Execute("Update properties SET pvalue='" + getResultFromCJP + "' where pName='DepartureBoardService.GetTrainInfoFromCJP' and AID='ExposedServices' and GID='UserPortal'"); 				
				// Initialise services
				Init(); 

				return true;
			}			
			finally
			{
				sqlHelper.ConnClose()  ;
			}
		}

		private bool GetTrainResultFromCJP()
		{
			try
			{
				return  Convert.ToBoolean(Properties.Current[Keys.GetTrainInfoFromCJP].ToString())  ;				
			}
			catch(FormatException)
			{
				return false;
			} 
		}    
		  
		private void ReStoreOriginalValue()
		{
			SqlHelper sqlHelper = new SqlHelper();

			try
			{
						
				if (originalGetTrainInfoFromCJP.Length == 0)
				{
					return;
				}
				
				if (originalGetTrainInfoFromCJP == "true" || originalGetTrainInfoFromCJP == "false")
				{
					sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);
					sqlHelper.Execute("Update properties SET pvalue='" + originalGetTrainInfoFromCJP + "' where pName='DepartureBoardService.GetTrainInfoFromCJP' and AID='ExposedServices' and GID='UserPortal'"); 								
				}
			}			
			finally
			{
				sqlHelper.ConnClose()  ;
			}
		}
		#endregion
	}


	public class LocalTestInitialization : IServiceInitialisation
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

			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockCodeGazetteerEmpty());  
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
}
