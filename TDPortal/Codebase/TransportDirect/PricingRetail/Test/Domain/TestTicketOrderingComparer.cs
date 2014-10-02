//********************************************************************************
//NAME         : TestTicketOrderingComparer.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 28/10/2003
//DESCRIPTION  : Implementation of TestTicketOrderingComparer class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/Domain/TestTicketOrderingComparer.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:28   mturner
//Initial revision.
//
//   Rev 1.4   Feb 14 2005 09:35:30   RScott
//warning "e declared but never used" resolved
//
//   Rev 1.3   Feb 07 2005 16:39:58   RScott
//Assertion changed to assert
//
//   Rev 1.2   Nov 07 2003 17:14:56   acaunt
//Flag included in Merge calls to indicate if we have the last journeydetail or not
//
//   Rev 1.1   Nov 07 2003 16:54:12   COwczarek
//FxCop fixes
//
//   Rev 1.0   Oct 28 2003 12:07:46   acaunt
//Initial Revision
using System;
using NUnit.Framework;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for TestTicketOrderingComparer.
	/// </summary>
	[TestFixture]
	public class TestTicketOrderingComparer
	{
		// Sample data
		Ticket ticketA = new Ticket("aadvark", Flexibility.FullyFlexible);
		Ticket ticketAA = new Ticket("aadvark", Flexibility.FullyFlexible);
		Ticket ticketB = new Ticket("Badger", Flexibility.FullyFlexible);

		// Our comparer
		TicketOrderingComparer comparer = new TicketOrderingComparer();


		public TestTicketOrderingComparer()
		{
		}

		[SetUp]
		public void Init()
		{					   
		}

		[TearDown]
		public void CleanUp()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestInvalidTickets()
		{
			string aString = "I am not a ticket";
			try 
			{
				comparer.Compare(aString, ticketA);
				// This should fail
				Assert.Fail("Comparer did not fail when passed a non-Ticket object");
			} 
			catch (ArgumentException e)
			{
				// Expected
				string temp = e.Message;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestTicketsDifferentAdultFares()
		{
			//  Setup two tickets with  different adult fares
			ticketA.AdultFare = 9f;
			ticketB.AdultFare = 10f;
			// Confirm that ticketA preceeds ticketB
			Assert.AreEqual(-1, comparer.Compare(ticketA, ticketB), "Incorrect ordering for two tickets with different adult fares");			
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestTicketsDifferentChildFares()
		{
			// Setup two tickets with idenfical adult fares but different child fares
			ticketA.AdultFare = 20;
			ticketB.AdultFare = 20;
			ticketA.ChildFare = 9f;
			ticketB.ChildFare = 10f;
			// Confirm that ticketA still preceeds ticketB
			Assert.AreEqual(-1, comparer.Compare(ticketA, ticketB), "Incorrect ordering for two tickets with identical adult but different child fares");		
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestTicketDifferentNames()
		{
			// Setup two tickets with idenfical adult fares and child fares, but different names
			ticketA.AdultFare = 20;
			ticketB.AdultFare = 20;
			ticketA.ChildFare = 10f;
			ticketB.ChildFare = 10f;
			// Confirm that ticketA still preceeds ticketB
			Assert.AreEqual(-1, comparer.Compare(ticketA, ticketB), "Incorrect ordering for two tickets with identical fares, but different names");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestIdenticalTickets()
		{
			// Setup two identical tickets
			ticketA.AdultFare = 20;
			ticketAA.AdultFare = 20;
			ticketA.ChildFare = 10f;
			ticketAA.ChildFare = 10f;
			// Confirm that ticketA still preceeds ticketB
			Assert.AreEqual(0,  comparer.Compare(ticketA, ticketAA), "Incorrect ordering for two identical tickets");
		}
	}
}
