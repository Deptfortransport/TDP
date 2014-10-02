// *********************************************** 
// NAME                 : TestFindNearestService.cs
// AUTHOR               : Russell Wilby
// DATE CREATED         : 17/01/2006 
// DESCRIPTION  		: Nunit test class Find Nearest Web Service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/FindNearest/V1/Test/TestFindNearestService.cs-arc  $
//
//   Rev 1.1   Dec 13 2007 10:04:52   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:00   mturner
//Initial revision.
//
//   Rev 1.4   Mar 13 2006 13:54:28   RWilby
//FindNearestLocality web method
//
//   Rev 1.3   Feb 28 2006 12:19:48   RWilby
//Fixed unit tests for merge. Build machine was not catching SoapExceptions with Nunit ExpectedException attribute.
//
//   Rev 1.2   Jan 19 2006 16:58:24   RWilby
//Updated tests to not test against the live data return
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.1   Jan 19 2006 15:43:54   RWilby
//Added Tests
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.0   Jan 19 2006 10:41:20   RWilby
//Initial revision.
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110

using System;
using NUnit.Framework;
using System.Web.Services.Protocols;
using System.Xml;
using System.Text; 
using System.Collections;
using TransportDirect.Common; 
using TransportDirect.Common.Logging;   
using TransportDirect.UserPortal.LocationService;  
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Test;
using TransportDirect.EnhancedExposedServices.Helpers;
using FindNearestV1 = TransportDirect.EnhancedExposedServices.FindNearest.V1;
using DataTransferCommonV1 = TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;

namespace TransportDirect.EnhancedExposedServices.FindNearest.V1.Test
{
	/// <summary>
	/// Nunit test class for Find Nearest Web Service
	/// </summary>
	[TestFixture]
	public class TestFindNearestService
	{
		private TestFindNearestServiceProxyReference serviceProxy;
		private string transactionId = "TestTransactionId";
		private string language = "en-GB";

		public TestFindNearestService()
		{}

		[SetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestFindNearestServiceInitialisation());

			//username token
			UsernameToken token = new UsernameToken(TestFindNearestServiceInitialisation.UsernamePassword, TestFindNearestServiceInitialisation.UsernamePassword, PasswordOption.SendHashed);

			//web service proxy class
			serviceProxy = new TestFindNearestServiceProxyReference();
            serviceProxy.RequestSoapContext.Security.Tokens.Add(token);
        }

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() 
		{
			serviceProxy = null;
		}

		/// <summary>
		/// Tests the TestFindNearestLocality method on the web service against the MockGisQuery class
		/// </summary>
		[Test]
		public void TestFindNearestLocality()
		{
			DataTransferCommonV1.OSGridReference osGridReference;
			osGridReference = new DataTransferCommonV1.OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;

			FindNearestService findNearestService = new FindNearestService();

			string Locality = findNearestService.FindNearestLocality(transactionId,language,osGridReference);

			//MockGisQuery class is hard coded to return E0001659 
			Assert.AreEqual("E0001659",Locality,"Incorrect locality returned");
		}

		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestLocalityMissingTransactionId()
		{
			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;
			try 
			{
				string locality = serviceProxy.FindNearestLocality(null,language,osGridReference);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}  

		}
		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestLocalityInvalidLengthTransactionId()
		{
			string longTransactionId = String.Empty;
			longTransactionId.PadLeft(101, 'A');

			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;

			try 
			{
				string locality = serviceProxy.FindNearestLocality(longTransactionId,language,osGridReference);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}   
		}	

		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestLocalityMissingLanguage()
		{
			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;
			try 
			{
				string locality = serviceProxy.FindNearestLocality(transactionId,null,osGridReference);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}   
		}
		
		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestLocalityMissingOSGridReference()
		{
			try 
			{
				string locality =  serviceProxy.FindNearestLocality(transactionId,language,null);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}    
		}

		/// <summary>
		/// Test that the GetGridReference method provides OS grid reference co-ordinates for full UK postcode
		/// </summary>
		[Test]
		public void TestGetGridReference()
		{
			//Test1: Test method provides OS grid reference co-ordinates for full UK postcode
			//This test is performed against live data therefore we don't test the actual returned data

			string postcode1 = "EC1R 3HN";//AO office Farringdon Road,London 
			OSGridReference osGridReference =  serviceProxy.GetGridReference(transactionId,language, postcode1);

			//Test2: Test method throws soap exception with correct message for invalid full UK postcode
			string partPostCode = "EC1R";
			try
			{
				OSGridReference osGridReferenceTest2 =  serviceProxy.GetGridReference(transactionId,language, partPostCode);
			}
			catch (SoapException soapExeption)
			{
				//create namespace manager to allow XPath query of soap detail node
				XmlNamespaceManager nsmgr = new XmlNamespaceManager(new NameTable());
				nsmgr.AddNamespace("td", "TransportDirect.EnhancedExposedServices.SoapFault.V1");
				XmlNode detail = soapExeption.Detail;
				XmlNodeList messagelist = detail.SelectNodes("/td:Details/td:Messages/td:Message", nsmgr);
				//check that there is only 1 message
				Assert.IsTrue(messagelist.Count == 1, "Only 1 message expected");
				XmlNode message = messagelist.Item(0);
				XmlNode code = message.SelectSingleNode("td:Code", nsmgr);
				XmlNode description = message.SelectSingleNode("td:Description", nsmgr);
				//test that the correct code and description are included in the message
				Assert.IsTrue(code.InnerText == "1131");
				Assert.IsTrue(description.InnerText == "Invalid Full UK Postcode","Incorrect error message");
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}
		}

		/// <summary>
		/// Tests the TestFindNearestAirports method on the web service against live data.
		/// </summary>
		[Test]
		public void TestFindNearestAirports()
		{
			//This test is performed against live data therefore we don't test the actual returned data

			OSGridReference osGridReference;
			NaptanProximity[] naptanProximityArray;

			//Test1: Test the method on the web service against live data.
			//Create OSGridReference for EC1R 3HN postcode (AO office Farringdon Road, London) 
			osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;

			naptanProximityArray = serviceProxy.FindNearestAirports(transactionId,language,osGridReference,5);
		}

		/// <summary>
		/// Tests the TestFindNearestBusStops method on the web service against live data.
		/// </summary>
		[Test]
		public void TestFindNearestBusStops()
		{
			//This test is performed against live data therefore we don't test the actual returned data

			OSGridReference osGridReference;
			NaptanProximity[] naptanProximityArray;

			//Test1: Test the method on the web service against live data.
			//Create OSGridReference for EC1R 3HN postcode (AO office Farringdon Road, London) 
			osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;

			naptanProximityArray = serviceProxy.FindNearestBusStops(transactionId,language,osGridReference,5);
		}

		/// <summary>
		/// Tests the TestFindNearestStations method on the web service against live data.
		/// </summary>
		[Test]
		public void TestFindNearestStations()
		{
			OSGridReference osGridReference;
			NaptanProximity[] naptanProximityArray;

			//Test1: Test the method on the web service against live data.
			//Create OSGridReference for EC1R 3HN postcode (AO office Farringdon Road, London) 
			osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;

			naptanProximityArray = serviceProxy.FindNearestStations(transactionId,language,osGridReference,5);
		}
		
		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void GetGridReferenceMissingTransactionId()
		{
			string postcode1 = "EC1R 3HN";//AO office Farringdon Road,London 
			
			try 
			{
				OSGridReference osGridReference =  serviceProxy.GetGridReference(null,language, postcode1);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			} 
		}

		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void GetGridReferenceInvalidLengthTransactionId()
		{
			string longTransactionId = String.Empty;
			longTransactionId.PadLeft(101, 'A');
			string postcode1 = "EC1R 3HN";//AO office Farringdon Road,London 
		
			try 
			{
				OSGridReference osGridReference =  serviceProxy.GetGridReference(longTransactionId,language, postcode1);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			} 
		}	

		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void GetGridReferenceMissingLanguage()
		{
			string postcode1 = "EC1R 3HN";//AO office Farringdon Road,London 
		
			try 
			{
				OSGridReference osGridReference =  serviceProxy.GetGridReference(transactionId,null, postcode1);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			} 
		}
		
		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void GetGridReferenceMissingPostcode()
		{
			try 
			{
				OSGridReference osGridReference =  serviceProxy.GetGridReference(transactionId,language, null);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			} 
		}


		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestAirportsMissingTransactionId()
		{
			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;

			try 
			{
				NaptanProximity[] naptanProximityArray = serviceProxy.FindNearestAirports(null,language,osGridReference,5);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			} 
		}

		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestAirportsInvalidLengthTransactionId()
		{
			string longTransactionId = String.Empty;
			longTransactionId.PadLeft(101, 'A');

			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;
			try 
			{
				NaptanProximity[] naptanProximityArray = serviceProxy.FindNearestAirports(longTransactionId,language,osGridReference,5);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			} 
		}	

		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestAirportsMissingLanguage()
		{
			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;
			try 
			{
				NaptanProximity[] naptanProximityArray = serviceProxy.FindNearestAirports(transactionId,null,osGridReference,5);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			} 
		}
		
		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestAirportsMissingOSGridReference()
		{

			try 
			{
				NaptanProximity[] naptanProximityArray = serviceProxy.FindNearestAirports(null,language,null,5);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			} 
		}


		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestBusStopsMissingTransactionId()
		{
			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;

			try 
			{
				NaptanProximity[] naptanProximityArray = serviceProxy.FindNearestBusStops(null,language,osGridReference,5);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			} 
		}
		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestBusStopsInvalidLengthTransactionId()
		{
			string longTransactionId = String.Empty;
			longTransactionId.PadLeft(101, 'A');

			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;

			try 
			{
				NaptanProximity[] naptanProximityArray = serviceProxy.FindNearestBusStops(longTransactionId,language,osGridReference,5);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			} 
		}	

		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestBusStopsMissingLanguage()
		{
			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;

			try 
			{
				NaptanProximity[] naptanProximityArray = serviceProxy.FindNearestBusStops(transactionId,null,osGridReference,5);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			} 
		}
		
		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestBusStopsMissingOSGridReference()
		{
			try 
			{
				NaptanProximity[] naptanProximityArray = serviceProxy.FindNearestBusStops(transactionId,language,null,5);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}  
		}

		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestStationsMissingTransactionId()
		{
			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;
			try 
			{
				NaptanProximity[] naptanProximityArray = serviceProxy.FindNearestStations(null,language,osGridReference,5);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}  

		}
		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestStationsInvalidLengthTransactionId()
		{
			string longTransactionId = String.Empty;
			longTransactionId.PadLeft(101, 'A');

			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;

			try 
			{
				NaptanProximity[] naptanProximityArray = serviceProxy.FindNearestStations(longTransactionId,language,osGridReference,5);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}   
		}	

		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestStationsMissingLanguage()
		{
			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = 531261;
			osGridReference.Northing = 182231;
			try 
			{
				NaptanProximity[] naptanProximityArray = serviceProxy.FindNearestStations(transactionId,null,osGridReference,5);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}   
		}
		
		/// <summary>
		/// Tests that WSDL validation occurs and that the correct SOAP exception is generated.
		/// </summary>
		[Test]
		public void FindNearestStationsMissingOSGridReference()
		{
			try 
			{
				NaptanProximity[] naptanProximityArray = serviceProxy.FindNearestStations(transactionId,language,null,5);
				Assert.Fail("Expected exception not thrown");
			} 
			catch (SoapException soapEx)
			{
				Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}    
		}
	}

	public class TestFindNearestServiceInitialisation: IServiceInitialisation  
	{
		/// <summary>
		/// Blank contructor 
		/// </summary>
		public TestFindNearestServiceInitialisation()
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

			// Add MockGisQuery service 
			serviceCache.Add( ServiceDiscoveryKey.GisQuery,  new TestMockGisQuery() );	
		}
	}
}
