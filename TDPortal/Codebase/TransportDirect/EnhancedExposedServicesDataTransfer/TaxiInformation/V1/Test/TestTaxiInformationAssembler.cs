// *********************************************** 
// NAME                 : TestTaxiInformationAssembler.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 16/01/2006 
// DESCRIPTION  	    : Test class for Taxi Information Service Assember
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/TaxiInformation/V1/Test/TestTaxiInformationAssembler.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:50   mturner
//Initial revision.
//
//   Rev 1.5   Mar 06 2006 09:45:28   RWilby
//Changed Fixture setup method to use TestFixtureSetUp attribute. Fix for test on build machine.
//
//   Rev 1.4   Jan 30 2006 15:47:50   schand
//Removed the unneccessary initialisation of crypto & property
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.3   Jan 25 2006 11:37:50   schand
//Code reviews
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 17 2006 17:42:08   schand
//Checking specific test data
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 17 2006 14:54:42   schand
//Correction to the test
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 17 2006 10:18:40   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111


using System;
using System.Text;
using NUnit.Framework;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;   
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.Common.ServiceDiscovery;   
using TransportDirect.EnhancedExposedServices.DataTransfer.TaxiInformation.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.Test;


namespace TransportDirect.EnhancedExposedServices.DataTransfer.TaxiInformation.V1.Test
{
	/// <summary>
	/// Taxi Information Service Assember
	/// </summary>
	[TestFixture]
	public class TestTaxiInformationAssembler
	{
		
		
		string stopNaptan = "9100HFX";
		string stopNaptan2 = "9100LEEDS";
		string stopName = "Halifax";
		string stopName2 = "Leeds";
		bool accessibleOperatorPresent = false;
		string accessibleText = string.Empty;
		string accessibleText2 = "Our records show this firm operates wheelchair accessible vehicles. Travellers with impaired mobility are advised to contact the operator to check availablaility and suitability prior to travelling. Tripscopw (08457 58 56 61) offers expert advice and information on overcoming travel difficulties and may be able to offer information about an alternative, should this journey be unsuitable to your needs";
		string comment = "Halifax is a minor station with taxis usually available on a rank. Advance booking is not normally necessary or even possible, unless arriving early in the morning or late at night. Operators who may accept bookings include: ";
		string comment2 = "Leeds is a major station with taxis usually available on a rank. Advance booking is not normally necessary or even possible, unless arriving early in the morning or late at night. Operators who may accept bookings include: ";

		bool informationAvailable = true;
		string operatorName = "Name";
		string operatorPhone = "Phone";
		bool operatoraccessible = true;
		string taxiOperatorName = "Fortran Cabbies";
		string taxiOperatorPhoneNumber = "01274 576332";
		string taxiOperatorName1 = "Ziggys Cabs";
		string taxiOperatorPhoneNumber1 = "01274 343532";
		string taxiOperatorName2 = "City";
		string taxiOperatorPhoneNumber2 = "01274 726095";


		#region NUnit Members
		[TestFixtureSetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestInitialisation());
		}
		#endregion


		#region Test Methods
		/// <summary>
		/// Test for CreateTaxiInformationStopDetailDT
		/// </summary>
		[Test]
		public void TestCreateTaxiInformationStopDetailDT()
		{
				
			StopTaxiInformation stopTaxiInformation1 = new StopTaxiInformation();
			stopTaxiInformation1 = InitialiseStopTaxiInformation("1");

			TaxiInformationStopDetail taxiInformationStopDetail = TaxiInformationAssembler.CreateTaxiInformationStopDetailDT(stopTaxiInformation1);   

			StringBuilder output = new StringBuilder();
 
			Assert.IsTrue(taxiInformationStopDetail.StopName.Length > 0);
			Assert.IsTrue(taxiInformationStopDetail.StopNaptan ==  stopNaptan);
			Assert.IsTrue(taxiInformationStopDetail.StopName == stopName);
			Assert.IsTrue(taxiInformationStopDetail.AccessibleOperatorPresent  == accessibleOperatorPresent);
			Assert.IsTrue(taxiInformationStopDetail.AccessibleText  == accessibleText);
			Assert.IsTrue(taxiInformationStopDetail.Comment  == comment);
			Assert.IsTrue(taxiInformationStopDetail.InformationAvailable   == informationAvailable);
			Assert.IsTrue(taxiInformationStopDetail.Operators.Length  == 4);
			// printing the values	
			output.Append("AccessibleOperatorPresent "  + taxiInformationStopDetail.AccessibleOperatorPresent.ToString()); 
			output.Append("\t AccessibleText " + taxiInformationStopDetail.AccessibleText  == accessibleText); 
			output.Append("\t AlternativeStops.Length " + taxiInformationStopDetail.AlternativeStops.Length.ToString());
			output.Append("\t Comment" + taxiInformationStopDetail.Comment); 
			output.Append("\t InformationAvailable"  + taxiInformationStopDetail.InformationAvailable.ToString()); 
			output.Append("\t StopName" + taxiInformationStopDetail.StopName); 
			output.Append("\t StopNaptan" + taxiInformationStopDetail.StopNaptan); 
			output.Append(System.Environment.NewLine);
		}

		/// <summary>
		/// Test for CreateTaxiInformationStopDetailArrayDT
		/// </summary>
		[Test]
		public void TestCreateTaxiInformationStopDetailArrayDT()
		{
			string testSet1 = "1";
			StopTaxiInformation stopTaxiInformation1 = new StopTaxiInformation();
			stopTaxiInformation1 = InitialiseStopTaxiInformation(testSet1);

			string testSet2 = "2";
			StopTaxiInformation stopTaxiInformation2 = new StopTaxiInformation();
			stopTaxiInformation2 = InitialiseStopTaxiInformation(testSet2);

			StopTaxiInformation[] stopTaxiInformations = new StopTaxiInformation[2];
			stopTaxiInformations[0] =   stopTaxiInformation1;
			stopTaxiInformations[1] =   stopTaxiInformation2;


			TaxiInformationStopDetail[] taxiInformationStopDetails = TaxiInformationAssembler.CreateTaxiInformationStopDetailArrayDT(stopTaxiInformations);

			Assert.IsTrue(taxiInformationStopDetails.Length ==2);

			
			//test 			
			for (int i=0; i< taxiInformationStopDetails.Length; i++)
			{
				TaxiInformationStopDetail taxiInformationStopDetail = taxiInformationStopDetails[i];
				StringBuilder output = new StringBuilder();
 
				Assert.IsTrue(taxiInformationStopDetail.StopName.Length > 0);
				

				if (i==0)
				{
					Assert.IsTrue(taxiInformationStopDetail.StopNaptan == stopNaptan);
					Assert.IsTrue(taxiInformationStopDetail.StopName == stopName);					
					Assert.IsTrue(taxiInformationStopDetail.Comment  == comment);
					Assert.IsTrue(taxiInformationStopDetail.AccessibleText  == accessibleText);
					Assert.IsTrue(taxiInformationStopDetail.Operators.Length  > 1);
					TaxiInformationOperator taxiInformationOperator = taxiInformationStopDetail.Operators[0];
					taxiInformationOperator.Accessible = accessibleOperatorPresent;
					taxiInformationOperator.Name = taxiOperatorName;
					taxiInformationOperator.PhoneNumber = taxiOperatorPhoneNumber;
					taxiInformationOperator = taxiInformationStopDetail.Operators[1];
					taxiInformationOperator.Accessible = accessibleOperatorPresent;
					taxiInformationOperator.Name = taxiOperatorName1;
					taxiInformationOperator.PhoneNumber = taxiOperatorPhoneNumber1;
				}
				else
				{   
					Assert.IsTrue(taxiInformationStopDetail.StopNaptan == stopNaptan2);
					Assert.IsTrue(taxiInformationStopDetail.StopName == stopName2);
					Assert.IsTrue(taxiInformationStopDetail.Comment  == comment2);
					Assert.IsTrue(taxiInformationStopDetail.AccessibleText  == accessibleText2);
					Assert.IsTrue(taxiInformationStopDetail.Operators.Length  > 0);
					TaxiInformationOperator taxiInformationOperator = taxiInformationStopDetail.Operators[0];
					taxiInformationOperator.Accessible = accessibleOperatorPresent;
					taxiInformationOperator.Name = taxiOperatorName2;
					taxiInformationOperator.PhoneNumber = taxiOperatorPhoneNumber2;
				}

				Assert.IsTrue(taxiInformationStopDetail.AccessibleOperatorPresent  == accessibleOperatorPresent);
				Assert.IsTrue(taxiInformationStopDetail.InformationAvailable   == informationAvailable);


				output.Append("AccessibleOperatorPresent "  + taxiInformationStopDetail.AccessibleOperatorPresent.ToString()); 
				output.Append("\t AccessibleText " + taxiInformationStopDetail.AccessibleText  == accessibleText); 
				output.Append("\t AlternativeStops.Length " + taxiInformationStopDetail.AlternativeStops.Length.ToString());
				output.Append("\t Comment" + taxiInformationStopDetail.Comment); 
				output.Append("\t InformationAvailable"  + taxiInformationStopDetail.InformationAvailable.ToString()); 
				output.Append("\t StopName" + taxiInformationStopDetail.StopName); 
				output.Append("\t StopNaptan" + taxiInformationStopDetail.StopNaptan); 
				output.Append(System.Environment.NewLine);
				
				
				Console.WriteLine(output);  
			}	
		}

		/// <summary>
		/// Test for CreateCodeServiceCodeTypeDT
		/// </summary>
		[Test]
		public void TestCreateTaxiInformationOperatorArrayDT()
		{
			string testSet = "1";

			// creating the instance of domain object 
			TaxiOperator[] taxiOperators = new TaxiOperator[2];
			TaxiOperator taxiOperator1 = new TaxiOperator(operatorName+testSet, operatorPhone+testSet, (operatoraccessible).ToString()); 

			testSet = "2";
			TaxiOperator taxiOperator2 = new TaxiOperator(operatorName+testSet, operatorPhone+testSet, ""); 
			taxiOperators[0] = taxiOperator1;
			taxiOperators[1] = taxiOperator2;
            
			// creating domain object 
			TaxiInformationOperator[] taxiInformationOperators = TaxiInformationAssembler.CreateTaxiInformationOperatorArrayDT(taxiOperators);   

			Assert.IsTrue(taxiInformationOperators.Length == 2);
			testSet = "1";
			Assert.IsTrue(taxiInformationOperators[0].Accessible   == true); 
			Assert.IsTrue(taxiInformationOperators[0].Name   == operatorName+testSet);
			Assert.IsTrue(taxiInformationOperators[0].PhoneNumber   == operatorPhone+testSet);

			testSet = "2";
			Assert.IsTrue(taxiInformationOperators[1].Accessible   == (!taxiInformationOperators[0].Accessible)); 
			Assert.IsTrue(taxiInformationOperators[1].Name   == operatorName+testSet);
			Assert.IsTrue(taxiInformationOperators[1].PhoneNumber   == operatorPhone+testSet);    
		}

        	
		
		#endregion

		#region Private Methods
		/// <summary>
		/// Helper method to create the instance of	 StopTaxiInformation
		/// </summary>
		/// <param name="stopTaxiInformation"></param>
		/// <param name="testSet"></param>
		/// <returns></returns>
		private StopTaxiInformation InitialiseStopTaxiInformation(string testSet)
		{
			StopTaxiInformation stopTaxiInformation;
 	
			// creating the instance of domain object 
			if (testSet == "1")
				stopTaxiInformation = new StopTaxiInformation(stopNaptan, true);			
			else
				stopTaxiInformation = new StopTaxiInformation(stopNaptan2, false);			

			return stopTaxiInformation;
		}
		#endregion
	}
}
