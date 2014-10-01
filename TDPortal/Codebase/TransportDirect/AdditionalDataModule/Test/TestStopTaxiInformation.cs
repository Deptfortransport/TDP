// ********************************************************************* 
// NAME                 : TestStopTaxiInformation.cs 
// AUTHOR               : Ken Josling
// DATE CREATED         : 08/08/2005
// DESCRIPTION			: Implementation of TestStopTaxiInformation
// ********************************************************************** 	
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/Test/TestStopTaxiInformation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 16:17:44   mturner
//Initial revision.
//
//   Rev 1.4   Sep 01 2005 11:35:32   kjosling
//Moved setup routine code to Init method of test fixture
//
//   Rev 1.3   Aug 12 2005 14:57:50   kjosling
//Added extra tests for serialization
//
//   Rev 1.2   Aug 12 2005 11:33:46   kjosling
//Updated unit tests
 
using System;
using NUnit.Framework;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.Common.ServiceDiscovery;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

namespace TransportDirect.UserPortal.AdditionalDataModule.TestStopTaxiInformation
{
	/// <summary>
	/// This class provides the unit test suite for StopTaxiInformation. It is dependant on the
	/// stub class TestStubAdditionalData to provide it with stop reference data. This class is
	/// supported by NUnit. 
	/// </summary>
	[TestFixture]
	public class TestStopTaxiInformation
	{
		public TestStopTaxiInformation()
		{
			
		}

		[SetUp]
		public void Init()
		{	
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestAdditionalDataInitialisation());

		}

		[TearDown]
		public void CleanUp()
		{
		}

		/// <summary>
		/// Tests the TaxiOperator class (basic test)
		/// </summary>
		[Test]
		public void TestTaxiOperator()
		{
			TaxiOperator t1 = new TaxiOperator("City Cars", "07891 203303", "");
			TaxiOperator t2 = new TaxiOperator("A1 Taxis", "07775 934234", "Y");

			Assert.IsTrue(t2.Accessible == true);
			Assert.IsTrue(t2.Name == "A1 Taxis");
			Assert.IsTrue(t1.PhoneNumber == "07891 203303");
			Assert.IsTrue(t1.Accessible == false);
		}

		/// <summary>
		/// Tests that the StopTaxiInformation class can detect an accessible operator
		/// </summary>
		[Test]
		public void TestforAccessibleOperator()
		{
			StopTaxiInformation s1 = new StopTaxiInformation("9100BRADIN", false);
			Assert.IsTrue(s1.AccessibleOperatorPresent == true);
			Assert.AreEqual(2, s1.Operators.Length, "Unexpected number of operators");
			Assert.IsFalse(s1.AccessibleText == "");
		}

		/// <summary>
		/// Tests that the StopTaxiInformation class behaves expectedly when no reference data is provided
		/// </summary>
		[Test]
		public void TestNoReferenceData()
		{
			StopTaxiInformation s1 = new StopTaxiInformation("RETURNSNULL", false);
			Assert.AreEqual(0, s1.Operators.Length, "Expected zero operators");
			Assert.IsTrue(s1.InformationAvailable == false);
		}

		/// <summary>
		/// Tests that the StopTaxiInformation class can detect an alternative stop and behave accordingly
		/// </summary>
		[Test]
		public void TestGoto()
		{
			StopTaxiInformation s1 = new StopTaxiInformation("9100FRNGDN", true);
			Assert.IsTrue(s1.AlternativeStops.Length == 2);
			Assert.IsTrue(s1.AccessibleOperatorPresent == true);
		}

		/// <summary>
		/// Tests that the StopTaxiInformation class does not process alternative stops when the functionality
		/// is disabled
		/// </summary>
		[Test]
		public void TestGotoDisabled()
		{
			StopTaxiInformation s1 = new StopTaxiInformation("9100FRNGDN", false);
			Assert.AreEqual(0, s1.AlternativeStops.Length, "Expected zero alternative operators");
		}

		/// <summary>
		/// Tests that the StopTaxiInformation class does not recursively process alternative stops (i.e. a 
		/// GOTO within a GOTO)
		/// </summary>
		[Test]
		public void TestGotoRecursive()
		{
			StopTaxiInformation s1 = new StopTaxiInformation("9100NTWCH", true);	
			Assert.IsTrue(s1.AlternativeStops.Length == 2);
			StopTaxiInformation s2 = (StopTaxiInformation)s1.AlternativeStops[0];
			Assert.IsTrue(s2.AlternativeStops.Length == 0);
			StopTaxiInformation s3 = (StopTaxiInformation)s1.AlternativeStops[1];
			Assert.IsTrue(s2.AlternativeStops.Length == 0);
		}

		/// <summary>
		/// Tests that the StopTaxiInformation class supports four operators at a given stop
		/// </summary>
		[Test]
		public void Test4Operators()
		{
			StopTaxiInformation s1 = new StopTaxiInformation("9100HFX", false);
			Assert.IsTrue(s1.Operators.Length == 4);
			Assert.IsTrue(s1.InformationAvailable == true);
		}

		/// <summary>
		/// Tests that the StopTaxiInformation class can support stops that have both native and alternative 
		/// stops
		/// </summary>
		[Test]
		public void TestGotoOperatorMix()
		{
			StopTaxiInformation s1 = new StopTaxiInformation("9100LEEDS", true);
			Assert.IsTrue(s1.Operators.Length == 1);
			Assert.IsTrue(s1.AlternativeStops.Length == 1);
			StopTaxiInformation s2 = (StopTaxiInformation)s1.AlternativeStops[0];
			Assert.IsTrue(s2.Operators.Length == 3);
		}

		/// <summary>
		/// Tests that the object can be serialized and deserialized correctly using a SOAPFormatter
		/// </summary>
		[Test]
		public void TestSerialization1()
		{
			//Serialize it

			StopTaxiInformation s1 = new StopTaxiInformation("9100NTWCH", true);
			SoapFormatter formatter = new SoapFormatter();
			Stream saveLocation = File.OpenWrite("C:\\temp\\test.xml");
			formatter.Serialize(saveLocation, s1);
			saveLocation.Close();

			//Now Deserialize it
			FileStream file = new FileStream("C:\\temp\\test.xml", FileMode.Open);
			
			StopTaxiInformation s2 = formatter.Deserialize(file) as StopTaxiInformation;
			file.Close();

			Assert.IsTrue(s2.AlternativeStops.Length == 2);
			Assert.IsTrue(s2.StopName.Length !=0);
			StopTaxiInformation s3 = (StopTaxiInformation)s1.AlternativeStops[0];
			Assert.IsTrue(s3.AlternativeStops.Length == 0);
	
		}

		/// <summary>
		/// Tests that the object can be serialized and deserialized correctly using a SOAPFormatter
		/// </summary>
		[Test]
		public void TestSerialization2()
		{
			//Serialize it

			StopTaxiInformation s1 = new StopTaxiInformation("9100BRADIN", true);
			SoapFormatter formatter = new SoapFormatter();
			Stream saveLocation = File.OpenWrite("C:\\temp\\test2.xml");
			formatter.Serialize(saveLocation, s1);
			saveLocation.Close();

			//Now Deserialize it
			FileStream file = new FileStream("C:\\temp\\test2.xml", FileMode.Open);
			
			StopTaxiInformation s2 = formatter.Deserialize(file) as StopTaxiInformation;
			file.Close();
			Assert.IsTrue(s2.StopName == "Bradford Interchange");
			Assert.IsTrue(s2.Operators.Length == 2);
			TaxiOperator t1 = s2.Operators[0];
			Assert.IsTrue(t1.Name == "City");
	
		}
	}
}
