// *********************************************** 
// NAME                 : TestTravelNewsService.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 12/01/2006 
// DESCRIPTION  		: Test class for travel news service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/TravelNews/V1/Test/TestTravelNewsService.cs-arc  $ 
//
//   Rev 1.1   Dec 13 2007 10:22:04   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:30   mturner
//Initial revision.
//
//   Rev 1.5   Apr 05 2006 15:59:44   mtillett
//Fix up unit tests to include new SeverityLevel for Critical and Serious
//Resolution for 3810: Mobile: Travel News Service for South East errors
//
//   Rev 1.4   Feb 21 2006 10:16:26   mguney
//The tests are changed to use the methods of the web service class directly when needed.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.3   Jan 30 2006 14:40:50   schand
//added test script to update the expiry date
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 23 2006 19:33:38   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 19:43:58   schand
//Added test for language and transaction ids
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 13 2006 15:40:40   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111

using System;
using NUnit.Framework;
using System.Web.Services.Protocols;
using System.Xml;
using System.Text; 
using TransportDirect.Common; 
using TransportDirect.Common.Logging;     
using Logger = System.Diagnostics.Trace;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using WebserviceTest = TransportDirect.EnhancedExposedServices.Test;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.Common.DatabaseInfrastructure;

using TNV1 = TransportDirect.EnhancedExposedServices.TravelNews.V1;
using TNDTV1 = TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1;

namespace TransportDirect.EnhancedExposedServices.TravelNews.V1.Test
{
	/// <summary>
	/// Test class for travel news service
	/// </summary>
	[TestFixture]
	public class TestTravelNewsService
	{
		//for using the webservice class directly
		private TNV1.TravelNewsService serviceProxy;
		//for using the webservice as a web service through proxy
		private TestTravelNewsServiceWse realServiceProxy;
		private string transactionId = "TestTransactionId";
		private string language = "en-GB";

		#region NUnit Members
		[SetUp]
		public void Init() 
		{						
			// Initialise the service discovery
			TDServiceDiscovery.Init(new TestInitialisation());
							
			serviceProxy = new TNV1.TravelNewsService();


			//username token
			UsernameToken token = new UsernameToken(WebserviceTest.TestInitialisation.UsernamePassword, WebserviceTest.TestInitialisation.UsernamePassword, PasswordOption.SendHashed);

			//web service proxy class
			realServiceProxy = new TestTravelNewsServiceWse();
            realServiceProxy.RequestSoapContext.Security.Tokens.Add(token);
        }

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() 
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			serviceProxy = null;
		}

		#endregion

		#region TestMethods
		/// <summary>
		/// Test for Travel News GetTravelNewsDetails method
		/// </summary>
		[Test]
		public void TestGetTravelNewsDetails()
		{
			// creating the request object 			
			TNDTV1.TravelNewsServiceRequest request = new TNDTV1.TravelNewsServiceRequest();
			request.DelayType = TNDTV1.TravelNewsServiceDelayType.All;			
			request.TransportType = TNDTV1.TravelNewsServiceTransportType.All;
			
			// calling the service via proxy 
			TNDTV1.TravelNewsServiceNewsItem[] travelNewsServiceNewsItems = serviceProxy.GetTravelNewsDetails(transactionId, language, request);
			
			Assert.IsTrue(travelNewsServiceNewsItems.Length > 0, "There should always be some news item"); 
		    
			Console.WriteLine(string.Format("Total number of new item returned {0}", travelNewsServiceNewsItems.Length.ToString()));
			StringBuilder outstring = new StringBuilder();
 
			foreach(TNDTV1.TravelNewsServiceNewsItem item in travelNewsServiceNewsItems)
			{
				Assert.IsTrue(item.HeadlineText.Length > 0, "Headline text should be present");
				outstring.Append("Headline Text -->"  + item.HeadlineText);   
				outstring.Append("Report Time -->"  + item.ReportedDateTime.ToString());
				outstring.Append(System.Environment.NewLine);   
   
			}

			Console.WriteLine(outstring);

		}


		/// <summary>
		/// Test for Travel News GetTravelNewsHeadlines method
		/// </summary>
		[Test]
		public void TestGetTravelNewsHeadlines()
		{
			// creating the request object 
			TNDTV1.TravelNewsServiceRequest request = new TNDTV1.TravelNewsServiceRequest();
			request.DelayType = TNDTV1.TravelNewsServiceDelayType.All;
			request.Region = "London";
			request.TransportType = TNDTV1.TravelNewsServiceTransportType.All;
			
			// calling the service via proxy 
			TNDTV1.TravelNewsServiceHeadlineItem[] travelNewsServiceHeadlineItems = serviceProxy.GetTravelNewsHeadlines(transactionId, language, request);
			
			Assert.IsTrue(travelNewsServiceHeadlineItems.Length > 0, "There should always be some headline item"); 
		    
			Console.WriteLine(string.Format("Total number of new item returned {0}", travelNewsServiceHeadlineItems.Length.ToString()));
			StringBuilder outstring = new StringBuilder();
 
			foreach(TNDTV1.TravelNewsServiceHeadlineItem item in travelNewsServiceHeadlineItems)
			{
				Assert.IsTrue(item.HeadlineText.Length > 0, "Headline text should be present");
				outstring.Append("Headline Text -->"  + item.HeadlineText);   
				outstring.Append("Regions -->"  + item.Regions);   
				outstring.Append(System.Environment.NewLine);   

			}
			Console.WriteLine(outstring);
		}


		/// <summary>
		/// Test for Travel News GetTravelNewsDetailsByUid method
		/// </summary>
		[Test]
		public void TestGetTravelNewsDetailsByUid()
		{
			// getting uid by calling another test method 
			TestGetTravelNewsHeadlines();

			// calling the service via proxy 
			TNDTV1.TravelNewsServiceNewsItem travelNewsServiceNewsItem = serviceProxy.GetTravelNewsDetailsByUid(transactionId, language, "RTM15160");
			
			Assert.IsTrue(travelNewsServiceNewsItem != null, "It should not be null"); 		    
		
			StringBuilder outstring = new StringBuilder();
 
			Assert.IsTrue(travelNewsServiceNewsItem.HeadlineText.Length > 0, "Headline text should be present");
			outstring.Append("Headline Text -->"  + travelNewsServiceNewsItem.HeadlineText);   
			outstring.Append("Report Time -->"  + travelNewsServiceNewsItem.ReportedDateTime.ToString());   
			outstring.Append(System.Environment.NewLine);   

			Console.WriteLine(outstring);
		}

		
		/// <summary>
		/// Test for Travel News GetTravelNewsAvailableRegions method
		/// </summary>
		[Test]
		public void TestGetTravelNewsAvailableRegions()
		{   			
			// calling the service via proxy 
			string[] regions = serviceProxy.GetTravelNewsAvailableRegions(transactionId, language);
			
			Assert.IsTrue(regions.Length > 0, "There should always be some region"); 
		
			StringBuilder outstring = new StringBuilder();
			foreach(string region in regions)
			{
				Assert.IsTrue(region.Length > 0, " region should be present");
				outstring.Append("Region -->"  + region);   				
				outstring.Append(System.Environment.NewLine);             				
			}
			Console.WriteLine(outstring);
		}

		
		/// <summary>
		/// Test for Travel News GetTravelNewsRegion method
		/// </summary>
		[Test]
		public void TestGetTravelNewsRegion()
		{
			string travelineNewsRegion = "London";
			// calling the service via proxy 
			string region = serviceProxy.GetTravelNewsRegion(transactionId , language, travelineNewsRegion );
			
			Assert.IsTrue(region !=null, " Traveline region must be returned"); 
		
			StringBuilder outstring = new StringBuilder();
			Assert.IsTrue(region.Length > 0, " region should be present");
			outstring.Append("Region -->"  + region);   				
			outstring.Append(System.Environment.NewLine);             				
		
			Console.WriteLine(outstring);
		}


		/// <summary>
		/// Test for missing transaction Id
		/// </summary>
		[Test]
		public void MissingTransactionId()
		{
			try
			{
				realServiceProxy.GetTravelNewsAvailableRegions(null, "en-GB");
			}
			catch(SoapException sEx)
			{
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) >= 0); 
			}
		}

		/// <summary>
		/// Test for missing language
		/// </summary>
		[Test]
		public void MissingLanguage()
		{
			try
			{
				realServiceProxy.GetTravelNewsAvailableRegions("A", null);				
			}
			catch(SoapException sEx)
			{
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) >= 0); 
			}
		}
		#endregion

		
	}


	
}
