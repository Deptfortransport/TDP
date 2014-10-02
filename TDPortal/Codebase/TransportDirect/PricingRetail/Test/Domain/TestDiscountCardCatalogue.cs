// *********************************************** 
// NAME			: TestDiscountCardCatalogue.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 27/01/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/Domain/TestDiscountCardCatalogue.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:24   mturner
//Initial revision.
//
//   Rev 1.1   Mar 10 2005 17:19:10   jgeorge
//Updated commenting

using System;
using NUnit.Framework;
using System.Data;
using System.Data.SqlClient;

using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Tests the DiscountCardCatalogue and DiscountCard classes.
	/// </summary>
	[TestFixture]
	public class TestDiscountCardCatalogue
	{
		private const string sqlGetAllCards = "SELECT Id, ItemValue, Mode, MaxAdults, MaxChildren FROM DiscountCards WHERE NOT Mode = 'All'";
		private const string sqlGetModeCards = "SELECT Id, ItemValue, Mode, MaxAdults, MaxChildren FROM DiscountCards WHERE Mode = '{0}'";
		private SqlHelperDatabase testDB = SqlHelperDatabase.DefaultDB;

		public TestDiscountCardCatalogue()
		{
		}

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());
		}

		[TearDown]
		public void CleanUp()
		{
		}

		/// <summary>
		/// Tests that the catalogue implements IServiveFactory as expected
		/// </summary>
		[Test]
		public void TestInterfaceConformance()
		{
			IServiceFactory catalogue = new DiscountCardCatalogue(SqlHelperDatabase.DefaultDB);
			
			object result = catalogue.Get();

			Assert.AreSame(catalogue, result, "The Get method of DiscountCardCatalogue did not return a reference to itself, as expected");
		}

		/// <summary>
		/// Tests retrieving cards using mode and code
		/// </summary>
		[Test]
		public void TestRetrieveByMode()
		{
			DiscountCardCatalogue catalogue = new DiscountCardCatalogue(SqlHelperDatabase.DefaultDB);

			ModeType[] relevantModes = new ModeType[] { ModeType.Rail, ModeType.Coach };	

			foreach (ModeType m in relevantModes)
			{
				// Get a datareader containing the items 
				SqlHelper helper = new SqlHelper();
				helper.ConnOpen(testDB);
				SqlDataReader reader = helper.GetReader( String.Format(sqlGetModeCards, m.ToString()) );

				int rowCount = 0;

				// For each row in the reader, check that there is a matching discount card in the
				// catalogue

				while (reader.Read())
				{
					int id = reader.GetInt32(0);
					string itemValue = reader.GetString(1);
					int maxAdults = reader.GetInt32(3);
					int maxChildren = reader.GetInt32(4);

					DiscountCard thisCard = catalogue.GetDiscountCard(m, itemValue);
				
					Assert.IsNotNull(thisCard, String.Format("Expected discount card with code [{0}] for mode [{1}] was not found in the catalogue", itemValue, m.ToString()) );

					// Check that the values are correct
					Assert.AreEqual(id, thisCard.Id, String.Format("ID not as expected for discount card with code [{0}] for mode [{1}]", itemValue, m.ToString()));
					Assert.AreEqual(maxAdults, thisCard.MaxAdults, String.Format("MaxAdults not as expected for discount card with code [{0}] for mode [{1}]", itemValue, m.ToString()));
					Assert.AreEqual(maxChildren, thisCard.MaxChildren, String.Format("MaxChildren not as expected for discount card with code [{0}] for mode [{1}]", itemValue, m.ToString()));
					rowCount++;
				}

				reader.Close();
				
				// Check that the number of items in the catalogue matches the number we had in the reader
				Assert.AreEqual(rowCount, catalogue.Count(m), String.Format("Catalogue has more cards than expected for mode [{0}]", m.ToString()));
			}
		}

		/// <summary>
		/// Tests retrieving the cards using IDs
		/// </summary>
		[Test]
		public void TestRetrieveAll()
		{
			DiscountCardCatalogue catalogue = new DiscountCardCatalogue(SqlHelperDatabase.DefaultDB);

			// Get a datareader containing the items 
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(testDB);
			SqlDataReader reader = helper.GetReader( sqlGetAllCards );

			int rowCount = 0;

			// For each row in the reader, check that there is a matching discount card in the
			// catalogue

			while (reader.Read())
			{
				int id = reader.GetInt32(0);
				string itemValue = reader.GetString(1);
				ModeType mode = (ModeType)Enum.Parse( typeof(ModeType), reader.GetString(2), true );
				int maxAdults = reader.GetInt32(3);
				int maxChildren = reader.GetInt32(4);

				DiscountCard thisCard = catalogue.GetDiscountCard(id);
				
				Assert.IsNotNull(thisCard, String.Format("Expected discount card with id [{0}] was not found in the catalogue", id) );

				// Check that the values are correct
				Assert.AreEqual(itemValue, thisCard.Code, String.Format("Code not as expected for discount card with id [{0}] was not found in the catalogue", id) );
				Assert.AreEqual(mode, thisCard.Mode, String.Format("Mode not as expected for discount card with id [{0}] was not found in the catalogue", id) );
				Assert.AreEqual(maxAdults, thisCard.MaxAdults, String.Format("MaxAdults not as expected for discount card with id [{0}] was not found in the catalogue", id) );
				Assert.AreEqual(maxChildren, thisCard.MaxChildren, String.Format("MaxChildren not as expected for discount card with id [{0}] was not found in the catalogue", id) );
				rowCount++;
			}

			reader.Close();
				
			// Check that the number of items in the catalogue matches the number we had in the reader
			Assert.AreEqual(rowCount, catalogue.Count(), "Catalogue has more cards than expected");
		}	

	}
}
