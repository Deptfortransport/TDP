// *********************************************** 
// NAME			: TestRequiredTicketTableLine.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 16/03/05
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestRequiredTicketTableLine.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:04   mturner
//Initial revision.
//
//   Rev 1.0   Mar 16 2005 11:57:14   jgeorge
//Initial revision.

using System;
using NUnit.Framework;

using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Tests the RequiredTicketTableLine class
	/// </summary>
	[TestFixture, CLSCompliant(false)]
	public class TestRequiredTicketTableLine
	{

		#region Setup/teardown

		#endregion


		#region Tests

		/// <summary>
		/// Tests creating a line for non discounted adult fare
		/// </summary>
		[Test]
		public void TestCreateAdultFull()
		{
			Ticket ticket = GetTicket();
			RequiredTicketTableLine line = new RequiredTicketTableLine(ticket, 5, false, false);

			Assert.AreEqual( ticket.AdultFare, line.AdultFare, "Adult fare not as expected" );
			Assert.IsTrue( float.IsNaN( line.ChildFare ), "Child fare not as expected" );
			Assert.IsFalse( line.Discounted, "Discounted not as expected");
			Assert.AreEqual( 5, line.NoPeople, "Number of people not as expected");
			Assert.AreEqual( ticket.Code, line.TicketName, "Ticket name not as expected");
			Assert.AreEqual( ticket.AdultFare * 5, line.Total, "Total cost not as expected");
		}

		/// <summary>
		/// Tests creating a line for discounted adult fare
		/// </summary>
		[Test]
		public void TestCreateAdultDiscounted()
		{
			Ticket ticket = GetTicket();
			RequiredTicketTableLine line = new RequiredTicketTableLine(ticket, 5, false, true);

			Assert.AreEqual( ticket.DiscountedAdultFare, line.AdultFare, "Adult fare not as expected" );
			Assert.IsTrue( float.IsNaN( line.ChildFare ), "Child fare not as expected" );
			Assert.IsTrue( line.Discounted, "Discounted not as expected");
			Assert.AreEqual( 5, line.NoPeople, "Number of people not as expected");
			Assert.AreEqual( ticket.Code, line.TicketName, "Ticket name not as expected");
			Assert.AreEqual( ticket.DiscountedAdultFare * 5, line.Total, "Total cost not as expected");
		}

		/// <summary>
		/// Tests creating a line for non discounted child fare
		/// </summary>
		[Test]
		public void TestCreateChildFull()
		{
			Ticket ticket = GetTicket();
			RequiredTicketTableLine line = new RequiredTicketTableLine(ticket, 5, true, false);

			Assert.AreEqual( ticket.ChildFare, line.ChildFare, "Child fare not as expected" );
			Assert.IsTrue( float.IsNaN( line.AdultFare ), "Adult fare not as expected" );
			Assert.IsFalse( line.Discounted, "Discounted not as expected");
			Assert.AreEqual( 5, line.NoPeople, "Number of people not as expected");
			Assert.AreEqual( ticket.Code, line.TicketName, "Ticket name not as expected");
			Assert.AreEqual( ticket.ChildFare * 5, line.Total, "Total cost not as expected");
		}

		/// <summary>
		/// Tests creating a line for discounted child fare
		/// </summary>
		[Test]
		public void TestCreateChildDiscounted()
		{
			Ticket ticket = GetTicket();
			RequiredTicketTableLine line = new RequiredTicketTableLine(ticket, 5, true, true);

			Assert.AreEqual( ticket.DiscountedChildFare, line.ChildFare, "Child fare not as expected" );
			Assert.IsTrue( float.IsNaN( line.AdultFare ), "Adult fare not as expected" );
			Assert.IsTrue( line.Discounted, "Discounted not as expected");
			Assert.AreEqual( 5, line.NoPeople, "Number of people not as expected");
			Assert.AreEqual( ticket.Code, line.TicketName, "Ticket name not as expected");
			Assert.AreEqual( ticket.DiscountedChildFare * 5, line.Total, "Total cost not as expected");
		}
		#endregion

		#region Supporting methods

		public Ticket GetTicket()
		{
			return new Ticket("XXXX", Flexibility.FullyFlexible, "XX", (float)20.0, (float)10.0, (float)13.33, (float)6.66, (uint)5, (uint)15);
		}

		#endregion

	}
}
