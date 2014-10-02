// *********************************************** 
// NAME                 : TestTaxiInformationService.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : //2006 
// DESCRIPTION  		: Test class for Taxi Information Web Service 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/TaxiInformation/V1/Test/TestTaxiInformationService.cs-arc  $ 
//
//   Rev 1.1   Dec 13 2007 10:18:16   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:22   mturner
//Initial revision.
//
//   Rev 1.2   Jan 23 2006 19:33:36   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 19:43:58   schand
//Added test for language and transaction ids
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 18 2006 13:52:54   schand
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
using TransportDirect.EnhancedExposedServices.Test;
using TransportDirect.EnhancedExposedServices.Helpers;


namespace TransportDirect.EnhancedExposedServices.TaxiInformation.V1.Test
{
	/// <summary>
	/// Test class for Taxi Information Web Service 
	/// </summary>
	[TestFixture]
	public class TestTaxiInformationService
	{
		private TestTaxiInformationServiceWse   serviceProxy;
		private string transactionId = "TestTransactionId";
		private string language = "en-GB";		

		#region NUnit Members
		[SetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.Init(new TestInitialisation());

			//username token
			UsernameToken token = new UsernameToken(TestInitialisation.UsernamePassword, TestInitialisation.UsernamePassword, PasswordOption.SendHashed);

			//web service proxy class
			serviceProxy = new TestTaxiInformationServiceWse();
            serviceProxy.RequestSoapContext.Security.Tokens.Add(token);
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
		/// Test for Taxi Information GetTaxiInfo method
		/// </summary>
		[Test]
		public void TestGetTaxiInfo()
		{
			string naptanId = "9100EUSTON";

			TaxiInformationStopDetail taxiInformationStopDetail = serviceProxy.GetTaxiInfo(transactionId, language, naptanId);   
  
			Assert.IsTrue(taxiInformationStopDetail!=null, "Taxi information should not be null");
 
			// now printing the result 
			StringBuilder outstring = new StringBuilder();	
			
			PrintTaxiInformationStopDetail(outstring, taxiInformationStopDetail);


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
				serviceProxy.GetTaxiInfo(null, "en-GB", null);

			}
			catch(SoapException sEx)
			{
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) >= 0); 
			}

		}

		/// <summary>
		/// Test for missing language Id
		/// </summary>
		[Test]
		public void MissingLanguage()
		{
			try
			{
				serviceProxy.GetTaxiInfo("A", null, null);

			}
			catch(SoapException sEx)
			{
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) >= 0); 
			}

		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Method to print the details of Taxi Information Stop details
		/// </summary>
		/// <param name="outstring">output string</param>
		/// <param name="taxiInformationStopDetail">Taxi Information stop detail object</param>
		private void PrintTaxiInformationStopDetail(StringBuilder outstring, TaxiInformationStopDetail taxiInformationStopDetail)
		{
			outstring.Append("AccessibleOperatorPresent -->"  + taxiInformationStopDetail.AccessibleOperatorPresent.ToString());   
			outstring.Append("\t AccessibleText -->"  + taxiInformationStopDetail.AccessibleText);
			outstring.Append("\t Comment -->"  + taxiInformationStopDetail.Comment);
			outstring.Append("\t InformationAvailable -->"  + taxiInformationStopDetail.InformationAvailable.ToString());				
			outstring.Append("\t StopName -->"  + taxiInformationStopDetail.StopName.ToString());				
			outstring.Append("InformationAvailable -->"  + taxiInformationStopDetail.StopNaptan.ToString());				
			outstring.Append(System.Environment.NewLine);   

			if (taxiInformationStopDetail.Operators.Length > 0)
			{
				for (int i=0; i< taxiInformationStopDetail.Operators.Length; i++)
				{
					outstring.Append("\t Operators Accessible -->"  + taxiInformationStopDetail.Operators[i].Accessible.ToString());				
					outstring.Append("\t Operators Name -->"  + taxiInformationStopDetail.Operators[i].Name.ToString());				
					outstring.Append("\t Operators PhoneNumber -->"  + taxiInformationStopDetail.Operators[i].PhoneNumber.ToString());				
					outstring.Append(System.Environment.NewLine);   
				}

			}
			outstring.Append(System.Environment.NewLine);   

			if (taxiInformationStopDetail.AlternativeStops.Length > 0)
			{   				
				for (int j=0; j< taxiInformationStopDetail.AlternativeStops.Length; j++)
				{    					 
					PrintTaxiInformationStopDetail(outstring, taxiInformationStopDetail.AlternativeStops[j]);
				}
			}
		}

		#endregion

	}
}
