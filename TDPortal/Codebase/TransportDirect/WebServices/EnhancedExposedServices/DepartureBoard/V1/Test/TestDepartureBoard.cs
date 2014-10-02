// *********************************************** 
// NAME                 : TestDepartureBoard.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 11/01/2006 
// DESCRIPTION  		: Nunit test class Departure Board Web Service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/DepartureBoard/V1/Test/TestDepartureBoard.cs-arc  $ 
//
//   Rev 1.1   Dec 12 2007 16:19:48   jfrank
//Updated for WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:51:56   mturner
//Initial revision.
//
//   Rev 1.4   Feb 13 2006 17:33:18   RWilby
//Updated to use DepartureBoardService stub class
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.3   Jan 30 2006 14:15:44   schand
//Added additional test methods for DepartureBoard Service.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 23 2006 19:33:34   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 19:43:54   schand
//Added test for language and transaction ids
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 13 2006 15:39:10   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111

using System;
using System.Collections;
using NUnit.Framework;
using System.Web.Services.Protocols;
using System.Xml;
using System.Text; 
using TransportDirect.Common; 
using TransportDirect.Common.Logging;     
using Logger = System.Diagnostics.Trace;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Test;
using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.Common.PropertyService.Properties;

using dtV1 =  TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1;
using dbV1 = TransportDirect.EnhancedExposedServices.DepartureBoard.V1;

namespace TransportDirect.EnhancedExposedServices.DepartureBoard.V1.Test
{
	/// <summary>
	/// Nunit test class Departure Board Web Service
	/// </summary>
	[TestFixture]
	public class TestDepartureBoard
	{
		private string transactionId = "TestTransactionId";
		private string language = "en-GB";
		private string railServiceNumber = String.Empty;
		private dbV1.DepartureBoardService departureBoardService;

		#region NUnit Members
		[SetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.Init(new TestDepartureBoardInitialisation());

			departureBoardService = new dbV1.DepartureBoardService();
  
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() 
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		#endregion
		
		#region TestMethods
		///<summary>
		/// Test Departure Board GetDepartureBoard method for Rail
		/// </summary>
		[Test]
		public void TestGetDepartureBoardForRail()
		{	
			dtV1.DepartureBoardServiceStopInformation[] departureBoardServiceStopInformation; 
			dtV1.DepartureBoardServiceRequest departureBoardServiceRequest = new dtV1.DepartureBoardServiceRequest(); 
				
			dtV1.DepartureBoardServiceLocation originLocation = new dtV1.DepartureBoardServiceLocation(); 
				originLocation.Code = "EUS";
				originLocation.Valid = false;

			dtV1.DepartureBoardServiceLocation destinationLocation = new dtV1.DepartureBoardServiceLocation(); 
			destinationLocation.Code = "MAN";
			destinationLocation.Valid = false;

			int range = 5;
			dtV1.DepartureBoardServiceTimeRequest timeRequest = new dtV1.DepartureBoardServiceTimeRequest();
			timeRequest.Hour = 20;
			timeRequest.Minute = 10;
			timeRequest.Type = dtV1.DepartureBoardServiceTimeRequestType.Now; 

			departureBoardServiceRequest.OriginLocation = originLocation;
			departureBoardServiceRequest.DestinationLocation = destinationLocation;
			departureBoardServiceRequest.JourneyTimeInformation = timeRequest;
			departureBoardServiceRequest.Range = range;
			departureBoardServiceRequest.RangeType = dtV1.DepartureBoardServiceRangeType.Sequence;
			departureBoardServiceRequest.ServiceNumber = "";
			departureBoardServiceRequest.ShowCallingStops = false;
			departureBoardServiceRequest.ShowDepartures = true;
			
			departureBoardServiceStopInformation =departureBoardService.GetDepartureBoard(transactionId, language, departureBoardServiceRequest);   				
			Assert.IsTrue(departureBoardServiceStopInformation.Length > 0);   
			railServiceNumber =	 departureBoardServiceStopInformation[0].Service.ServiceNumber.ToString();   
			PrintDepartureBoardServiceResult(departureBoardServiceStopInformation);			
		}

		/// <summary>
		/// Test Departure Board GetDepartureBoard method for Rail for Origin location only
		/// </summary>
		[Test]
		public void TestGetDepartureBoardForRailOriginOnly()
		{
			dtV1.DepartureBoardServiceStopInformation[] departureBoardServiceStopInformation; 
			dtV1.DepartureBoardServiceRequest departureBoardServiceRequest = new dtV1.DepartureBoardServiceRequest(); 
				
			dtV1.DepartureBoardServiceLocation originLocation = new dtV1.DepartureBoardServiceLocation(); 
			originLocation.Code = "EUS";
			originLocation.Valid = false;
			

			int range = 5;
			dtV1.DepartureBoardServiceTimeRequest timeRequest = new dtV1.DepartureBoardServiceTimeRequest();
			timeRequest.Hour = 20;
			timeRequest.Minute = 10;
			timeRequest.Type = dtV1.DepartureBoardServiceTimeRequestType.Now; 

			departureBoardServiceRequest.OriginLocation = originLocation;
			departureBoardServiceRequest.DestinationLocation = null;
			departureBoardServiceRequest.JourneyTimeInformation = timeRequest;
			departureBoardServiceRequest.Range = range;
			departureBoardServiceRequest.RangeType = dtV1.DepartureBoardServiceRangeType.Sequence;
			departureBoardServiceRequest.ServiceNumber = "";
			departureBoardServiceRequest.ShowCallingStops = false;
			departureBoardServiceRequest.ShowDepartures = true;
			 
			departureBoardServiceStopInformation = departureBoardService.GetDepartureBoard(transactionId, language, departureBoardServiceRequest);   				
			Assert.IsTrue(departureBoardServiceStopInformation.Length > 0);   
			PrintDepartureBoardServiceResult(departureBoardServiceStopInformation);			
		}


		/// <summary>
		/// Test Departure Board GetDepartureBoard method for Rail for Origin location only
		/// </summary>
		[Test]
		public void TestGetDepartureBoardForRailServiceNumber()
		{
			dtV1.DepartureBoardServiceStopInformation[] departureBoardServiceStopInformation; 
			dtV1.DepartureBoardServiceRequest departureBoardServiceRequest = new dtV1.DepartureBoardServiceRequest(); 
			dtV1.DepartureBoardServiceLocation originLocation = new dtV1.DepartureBoardServiceLocation(); 
			originLocation.Code = "EUS";
			originLocation.Valid = false;

			// getting service number
			TestGetDepartureBoardForRail();

			int range = 5;
			dtV1.DepartureBoardServiceTimeRequest timeRequest = new dtV1.DepartureBoardServiceTimeRequest();
			timeRequest.Hour = 20;
			timeRequest.Minute = 10;
			timeRequest.Type = dtV1.DepartureBoardServiceTimeRequestType.Now; 

			departureBoardServiceRequest.OriginLocation = originLocation;
			departureBoardServiceRequest.DestinationLocation = null;
			departureBoardServiceRequest.JourneyTimeInformation = timeRequest;
			departureBoardServiceRequest.Range = range;
			departureBoardServiceRequest.RangeType = dtV1.DepartureBoardServiceRangeType.Sequence;
			departureBoardServiceRequest.ServiceNumber = railServiceNumber;
			departureBoardServiceRequest.ShowCallingStops = true;
			departureBoardServiceRequest.ShowDepartures = true;
			 
			departureBoardServiceStopInformation = departureBoardService.GetDepartureBoard(transactionId, language, departureBoardServiceRequest);   				
			Assert.IsTrue(departureBoardServiceStopInformation.Length > 0);   
			PrintDepartureBoardServiceResult(departureBoardServiceStopInformation);			
		}


		/// <summary>
		/// Test Departure Board GetDepartureBoard method for Bus 
		/// </summary>
		[Test]
		public void TestGetDepartureBoardForBus()
		{
			dtV1.DepartureBoardServiceStopInformation[] departureBoardServiceStopInformation; 
			dtV1.DepartureBoardServiceRequest departureBoardServiceRequest = new dtV1.DepartureBoardServiceRequest(); 
				
			dtV1.DepartureBoardServiceLocation originLocation = new dtV1.DepartureBoardServiceLocation(); 
			dtV1.DepartureBoardServiceLocation destinationLocation = new dtV1.DepartureBoardServiceLocation(); 
			originLocation.Code = "hamdmdwg"; // Headley, The Harrow
			originLocation.Valid = false;

			int range = 5;
			dtV1.DepartureBoardServiceTimeRequest timeRequest = new dtV1.DepartureBoardServiceTimeRequest();
			timeRequest.Hour = 20;
			timeRequest.Minute = 10;
			timeRequest.Type = dtV1.DepartureBoardServiceTimeRequestType.Now; 

			departureBoardServiceRequest.OriginLocation = originLocation;
			departureBoardServiceRequest.DestinationLocation = null;
			departureBoardServiceRequest.JourneyTimeInformation = timeRequest;
			departureBoardServiceRequest.Range = range;
			departureBoardServiceRequest.RangeType = dtV1.DepartureBoardServiceRangeType.Sequence;
			departureBoardServiceRequest.ServiceNumber = "";
			departureBoardServiceRequest.ShowCallingStops = false;
			departureBoardServiceRequest.ShowDepartures = true;
			 
			
			
			departureBoardServiceStopInformation = departureBoardService.GetDepartureBoard(transactionId, language, departureBoardServiceRequest);   				
			Assert.IsTrue(departureBoardServiceStopInformation.Length > 0);   
			PrintDepartureBoardServiceResult(departureBoardServiceStopInformation);			
		}


	
		#endregion

		#region Private Methods
		private static void PrintDepartureBoardServiceResult(dtV1.DepartureBoardServiceStopInformation[] departureBoardServiceStopInformation)
		{
			StringBuilder outstring = new StringBuilder();			
			int iResultCount = 0;
			foreach (dtV1.DepartureBoardServiceStopInformation info in departureBoardServiceStopInformation)
			{
				if (info == null)
					return;

				dtV1.DepartureBoardServiceInformation arrival = new dtV1.DepartureBoardServiceInformation();
				dtV1.DepartureBoardServiceInformation departure = new dtV1.DepartureBoardServiceInformation();
				dtV1.DepartureBoardServiceInformation stop = new dtV1.DepartureBoardServiceInformation();
				dtV1.DepartureBoardServiceInformation[] previous;
				dtV1.DepartureBoardServiceInformation[] next;

				arrival = info.Arrival;
				departure = info.Departure; 
				stop = info.Stop;
				previous = info.PreviousIntermediates; 
				next = info.OnwardIntermediates;
 

				if (arrival == null)
				{
					arrival = stop;
				}

				if (departure == null)
				{
					departure = stop;
				}


				outstring.Append("From: " +  departure.Stop.Name.ToString() + "\t" + " To: " + arrival.Stop.Name.ToString()  + "\t") ;
					
				if (stop!= null)
				{
					outstring.Append(" Stop: " + stop.Stop.Name.ToString()  + "\t");	
					if (stop.ArriveTime != DateTime.MinValue)
					{
						outstring.Append(" arrive: " +  stop.ArriveTime.Hour.ToString() +  ":" +  stop.ArriveTime.Minute.ToString() + "\t") ;      
					}
					if (stop.DepartTime != DateTime.MinValue)
					{
						outstring.Append(" depart: " +  stop.DepartTime.Hour.ToString() +  ":" +   stop.DepartTime.Minute.ToString() + "\t");
					}					
				}

				dtV1.DepartureBoardServiceInformation pStop ;

				for(int i= 0; i < previous.Length ; i++ )
				{
					pStop = previous[i];

					if (pStop != null )
					{
						outstring.Append("pStop:" + pStop.Stop.Name.ToString()) ; 
						if (pStop.ArriveTime != DateTime.MinValue)
						{
							outstring.Append( " arrive: " +  pStop.ArriveTime.Hour.ToString() + ":" +  pStop.ArriveTime.Minute.ToString() + "\t");
						}

						if (pStop.DepartTime != DateTime.MinValue)
						{
							outstring.Append( " depart: " +  pStop.DepartTime.Hour.ToString() + ":" +  pStop.DepartTime.Minute.ToString() + "\t");
						}


					}
				}

				dtV1.DepartureBoardServiceInformation nStop ;

				for(int i= 0; i < next.Length ; i++ )
				{
					nStop = next[i];

					if (nStop != null )
					{
						outstring.Append("nStop:" + nStop.Stop.Name.ToString() ); 
						if (nStop.DepartTime  != DateTime.MinValue)
						{
							outstring.Append(  " arrive: " +  nStop.ArriveTime.Hour.ToString() +  ":" +  nStop.ArriveTime.Minute.ToString() + "\t");
						}

						if (nStop.DepartTime != DateTime.MinValue)
						{
							outstring.Append( " depart: " + nStop.DepartTime.Hour.ToString() + ":" +  nStop.DepartTime.Minute.ToString() + "\t");
						}
					}
				}

				outstring.Append(" Service No: " + info.Service.ServiceNumber);   

				// now checking whether it has trains specific details
				if (info.HasTrainDetails)
				{
					outstring.Append(" Circular Route " + info.CircularRoute.ToString()+ "\t"); 
					outstring.Append(" False Destination " + info.FalseDestination.ToString()+ "\t"); 
					outstring.Append(" Late Reason " + info.LateReason.ToString()+ "\t"); 
					outstring.Append(" Cancelled " + info.Cancelled.ToString()+ "\t"); 
					outstring.Append("CancellationReason " + info.CancellationReason.ToString()+ "\t"); 
				}

				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring.ToString()));
				Console.WriteLine(outstring.ToString());
				iResultCount++;
			}

			outstring.Append("Actual result: " + iResultCount.ToString()  + " result printed:  " +  iResultCount.ToString());  
			Console.WriteLine(outstring); 
			Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure , TDTraceLevel.Verbose,outstring.ToString()));
			
            			
		}
		#endregion
	
	}

	/// <summary>
	/// DepartureBoard Test Initialisation class
	/// </summary>
	public class TestDepartureBoardInitialisation: IServiceInitialisation  
	{
		/// <summary>
		/// Blank contructor 
		/// </summary>
		public TestDepartureBoardInitialisation()
		{
		}
		public const string UsernamePassword = "EnhancedExposedWebServiceTest";

		/// <summary>
		/// Implementation of Populate method for unit testing
		/// </summary>
		/// <param name="serviceCache"></param>
		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService					
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());	
			
			// Add cryptographic scheme
			serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );	
	
			//Add DepartureBoardService
			serviceCache.Add(ServiceDiscoveryKey.DepartureBoardService, new TestDepartureBoardServiceFactory());
		}
	}

}
