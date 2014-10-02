//********************************************************************************
//NAME         : TestMandatoryReservations.cs
//AUTHOR       : Russell Wilby
//DATE CREATED : 2005-11-08
//DESCRIPTION  :NUnit test class for MandatoryReservationsRequest and MandatoryReservations classes
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Test/TestMandatoryReservations.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:46   mturner
//Initial revision.
//
//   Rev 1.1   Nov 14 2005 12:06:30   RWilby
//Updated tests to fix date problem.
//Resolution for 3003: NRS enhancement for non-compulsory reservations
//
//   Rev 1.0   Nov 11 2005 17:47:46   RWilby
//Initial revision.
//Resolution for 3003: NRS enhancement for non-compulsory reservations
using System;
using NUnit.Framework;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.RetailBusinessObjects;

namespace TransportDirect.UserPortal.RetailBusinessObjects.Test
{
	/// <summary>
	/// NUnit test class for MandatoryReservationsRequest and MandatoryReservations classes
	/// </summary>
	[TestFixture]
	public class TestMandatoryReservations
	{
		/// <summary>
		/// Ticket Type code Input test data
		/// </summary>
		private string[] testTicketTypeCodeDuplicates = {"SOR","FOR","KOL","FOR"};
		private string[] testTicketTypeCode1 = {"SOR"};
		private string[] testTicketTypeCode6 = {"SOR","FOR","BTW","BLA","ETO","HOL"};

		/// <summary>
		/// MandatoryReservationsRequest.InputBody expexted results
		/// </summary>
		private  string testTicketTypeCode1Result = "HEADER  001SOR                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           " + DateTime.Now.ToString("yyyyMMdd");
		private  string testTicketTypeCode6Result = "HEADER  006SORFORBTWBLAETOHOL                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            " + DateTime.Now.ToString("yyyyMMdd");
		
		private const string testOutputBodyForTicketTypeCode1 = "TTYMANDRN                                                                                                                                                                                                                                                         ";
		private const string testOutputHeaderForTicketTypeCode1 ="STDOUT          02      025800010000000000000000000000000000000000000000000000000000                                                  ";
		private const string testOutputBodyForTicketTypeCode6 = "TTYMANDRNYOREX                                                                                                                                                                                                                                                    ";
		private const string testOutputHeaderForTicketTypeCode6 ="STDOUT          02      025800010000000000000000000000000000000000000000000000000000                                                  ";
		private const string testOutputHeaderWithErrorCode ="STDOUT          02S442  025800010000000000000000000000000000000000000000000000000000                                                  ";
		/// <summary>
		/// TestFixture setup method
		/// </summary>
		[SetUp]
		public void Init()
		{}

		/// <summary>
		/// TestFixture teardown method
		/// </summary>
		[TearDown]
		public void CleanUp()
		{}	

		/// <summary>
		/// Tests that the MandatoryReservationsRequest creates the input header request correctly.
		/// </summary>
		[Test]
		public void TestMandatoryReservationRequest()
		{
			
			MandatoryReservationsRequest mandatoryReservationsRequest = new MandatoryReservationsRequest("sboPool.InterfaceVersion",testTicketTypeCode1);
			Assert.AreEqual(testTicketTypeCode1Result, mandatoryReservationsRequest.InputBody,"TestTicketTypeCode1 Test Failed");
		}

		/// <summary>
		/// Tests that the MandatoryReservations processes the output header data correctly.
		/// </summary>
		[Test]
		public void TestMandatoryReservationRequestAndMandatoryReservation()
		{
			BusinessObjectOutput MandatoryReservationBusinessObjectOutput = new BusinessObjectOutput(testOutputHeaderForTicketTypeCode1,testOutputBodyForTicketTypeCode1);
			
			MandatoryReservationsRequest mandatoryReservationsRequest = new MandatoryReservationsRequest("sboPool.InterfaceVersion",testTicketTypeCode1);
			Assert.AreEqual(testTicketTypeCode1Result, mandatoryReservationsRequest.InputBody,"TestTicketTypeCode1 Test Failed");

			MandatoryReservations mandatoryReservations = new MandatoryReservations(MandatoryReservationBusinessObjectOutput,mandatoryReservationsRequest.TicketTypeCodes);
		
			Assert.AreEqual(false,mandatoryReservations.HasErrors,"MandatoryReservations returned errors");

			Assert.AreEqual(1,mandatoryReservations.Results.Count,"Incorrect result hashtable count");

			Assert.AreEqual(MandatoryReservationFlag.NotRequired, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[0]],"Incorrect MandatoryReservationFlag");
		}

		/// <summary>
		/// Tests All flag variations for mandatory reservations
		/// </summary>
		[Test]
		public void TestMandatoryReservationAllFlagVariations()
		{
			BusinessObjectOutput MandatoryReservationBusinessObjectOutput = new BusinessObjectOutput(testOutputHeaderForTicketTypeCode6,testOutputBodyForTicketTypeCode6);
			
			MandatoryReservationsRequest mandatoryReservationsRequest = new MandatoryReservationsRequest("sboPool.InterfaceVersion",testTicketTypeCode6);
			Assert.AreEqual(testTicketTypeCode6Result, mandatoryReservationsRequest.InputBody,"TestTicketTypeCode6 Test Failed");

			MandatoryReservations mandatoryReservations = new MandatoryReservations(MandatoryReservationBusinessObjectOutput,mandatoryReservationsRequest.TicketTypeCodes);
		
			Assert.AreEqual(false,mandatoryReservations.HasErrors,"MandatoryReservations returned errors");

			Assert.AreEqual(6,mandatoryReservations.Results.Count,"Incorrect result hashtable count");
			
			//Test all variations
			Assert.AreEqual(MandatoryReservationFlag.NotRequired, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[0]],"Incorrect MandatoryReservationFlag");
			Assert.AreEqual(MandatoryReservationFlag.Required, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[1]],"Incorrect MandatoryReservationFlag");
			Assert.AreEqual(MandatoryReservationFlag.OutwardOnly, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[2]],"Incorrect MandatoryReservationFlag");
			Assert.AreEqual(MandatoryReservationFlag.ReturnOnly, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[3]],"Incorrect MandatoryReservationFlag");
			Assert.AreEqual(MandatoryReservationFlag.Required, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[4]],"Incorrect MandatoryReservationFlag");
			Assert.AreEqual(MandatoryReservationFlag.NotRequired, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[5]],"Incorrect MandatoryReservationFlag");
		}

		/// <summary>
		/// Tests mandatory reservations with errors
		/// </summary>
		[Test]
		public void TestMandatoryReservationWithErrors()
		{
			BusinessObjectOutput MandatoryReservationBusinessObjectOutput = new BusinessObjectOutput(testOutputHeaderWithErrorCode,testOutputBodyForTicketTypeCode6);
			
			MandatoryReservationsRequest mandatoryReservationsRequest = new MandatoryReservationsRequest("sboPool.InterfaceVersion",testTicketTypeCode6);
			Assert.AreEqual(testTicketTypeCode6Result, mandatoryReservationsRequest.InputBody,"TestTicketTypeCode6 Test Failed");

			MandatoryReservations mandatoryReservations = new MandatoryReservations(MandatoryReservationBusinessObjectOutput,mandatoryReservationsRequest.TicketTypeCodes);
		
			Assert.AreEqual(true,mandatoryReservations.HasErrors,"MandatoryReservations returned no errors when errors were expected");

			Assert.AreEqual(6,mandatoryReservations.Results.Count,"Incorrect result hashtable count");
			
			//Test all variations
			Assert.AreEqual(MandatoryReservationFlag.NotRequired, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[0]],"Incorrect MandatoryReservationFlag");
			Assert.AreEqual(MandatoryReservationFlag.NotRequired, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[1]],"Incorrect MandatoryReservationFlag");
			Assert.AreEqual(MandatoryReservationFlag.NotRequired, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[2]],"Incorrect MandatoryReservationFlag");
			Assert.AreEqual(MandatoryReservationFlag.NotRequired, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[3]],"Incorrect MandatoryReservationFlag");
			Assert.AreEqual(MandatoryReservationFlag.NotRequired, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[4]],"Incorrect MandatoryReservationFlag");
			Assert.AreEqual(MandatoryReservationFlag.NotRequired, mandatoryReservations.Results[mandatoryReservationsRequest.TicketTypeCodes[5]],"Incorrect MandatoryReservationFlag");
		}
	}
}
