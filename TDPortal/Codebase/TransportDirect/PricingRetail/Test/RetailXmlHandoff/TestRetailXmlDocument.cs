// *********************************************** 
// NAME			: TestRetailXmlDocument.cs
// AUTHOR		: A Toner
// DATE CREATED	: 23/10/03
// DESCRIPTION	: Tests for the RetailXmlDocument class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/RetailXmlHandoff/TestRetailXmlDocument.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:36   mturner
//Initial revision.
//
//   Rev 1.13   Aug 19 2005 14:06:38   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.12.1.0   Aug 16 2005 11:20:50   RPhilpott
//Get rid of warnings from deprecated methods.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.12   Mar 30 2005 15:39:58   jgeorge
//Changes to class being tested
//
//   Rev 1.11   Mar 17 2005 17:40:26   jgeorge
//Split the large single test out into multiple tests. Added ManualVerification element.

using System;
using System.Diagnostics;
using System.Xml;

using NUnit.Framework;

using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.PricingRetail.RetailXmlHandoff
{
    /// <summary>
    /// Tests for the RetailXmlDocument class
    /// </summary>
	[TestFixture]
	public class TestRetailXmlDocument
	{
		#region Private members

		TDLocation viaLocation;

		Discounts allDiscounts;
		Discounts noDiscounts;

		#endregion

		#region Setup and teardown

		[TestFixtureSetUp]
		public void Setup()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());		

			viaLocation = TestSampleJourneyData.TrainPJDStpNot.LegStart.Location;

			allDiscounts = new Discounts("NEW", "Family Save Coachcard", TicketClass.All);
			noDiscounts =  new Discounts("", "", TicketClass.All);
		}

		[TestFixtureTearDown]
		public void CleanUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		#endregion

		#region Tests

		[Test, Ignore("These tests verify that XML can be generated without error; they do not verify that the generated XML is correct. It will be written to StdOut stream, so can be checked there.")]
		public void ManualVerification()
		{
		}

        /// <summary>
        /// Tests generation for all of a return journey
        /// </summary>
		[Test]
		public void TestReturnJourneyReturnItinerary()
		{
			generateXML(new Itinerary( TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.TrainNotDov), ItineraryType.Return, true, 2, false, allDiscounts, viaLocation );
		}

		/// <summary>
		/// Tests generation for single outward part of return journey
		/// </summary>
		[Test]
		public void TestReturnJourneyReturnItineraryOverrideSingleOutbound()
		{
			generateXML( new Itinerary( TestSampleJourneyData.NatExDovNot, TestSampleJourneyData.NatExNotDov), ItineraryType.Single, false, 0, true, noDiscounts, viaLocation );
		}

		/// <summary>
		/// Tests generation for single inward part of return journey
		/// </summary>
		[Test]
		public void TestReturnJourneyReturnItineraryOverrideSingleInbound()
		{
			generateXML( new Itinerary( TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.TrainNotDov), ItineraryType.Single ,true, 3, false, allDiscounts, viaLocation );
		}

		/// <summary>
		/// Tests generation for single journey
		/// </summary>
		[Test]
		public void TestSingleJourneySingleItineraryOutbound()
		{
			// Single journey, default, outbound
			WriteLine("Single journey, default, outbound");
			generateXML( new Itinerary( TestSampleJourneyData.TrainDovNot, null), ItineraryType.Single,  false, 2, true, noDiscounts, viaLocation);
		}

		/// <summary>
		/// Tests generation for inbound part of return journey/single itinerary
		/// </summary>
		[Test]
		public void TestReturnJourneySingleItineraryInbound()
		{
			generateXML( new Itinerary( TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.NatExNotDov), ItineraryType.Single, true, 0, false, allDiscounts, viaLocation );
		}

		/// <summary>
		/// Tests generation for outbound part of return journey, single itinerary overridden to return
		/// </summary>
		[Test]
		public void TestReturnJourneySingleItineraryOverrideReturnOutbound()
		{
			generateXML( new Itinerary( TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.NatExNotDov), ItineraryType.Return, false, 3, true, noDiscounts, viaLocation );
		}

		/// <summary>
		/// Tests generation for inbound part of return journey, single itinerary overridden to return
		/// </summary>
		[Test]
		public void TestReturnJourneySingleItineraryOverrideReturnInbound()
		{
			generateXML( new Itinerary( TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.NatExNotDov), ItineraryType.Return,  false, 2, false, allDiscounts, viaLocation );
		}

		/// <summary>
		/// Tests generation for single walking journey
		/// </summary>
		[Test]
		public void TestSingleWalkingJourney()
		{
			generateXML( new Itinerary( TestSampleJourneyData.WalkNot, null), ItineraryType.Single,  false, 0, false, noDiscounts, null );

		}


		#endregion

		#region Helper methods

		private void generateXML(Itinerary itinerary, ItineraryType overrideType, bool isReturn, int speed, bool noChanges, Discounts discounts, TDLocation viaLocation)
		{
			PublicJourneyDetail[] outwardJourney = ((PublicJourney)itinerary.OutwardJourney).Details;
			PublicJourneyDetail[] inwardJourney = isReturn ? ((PublicJourney)itinerary.ReturnJourney).Details : null;

			XmlDocument xml = (XmlDocument)RetailXmlDocument.GenerateXml( outwardJourney, inwardJourney, itinerary.Type, overrideType,isReturn, speed, noChanges, discounts, viaLocation, 1, 0 );
			displayXML(xml);
		}

		private void displayXML(XmlDocument xml)
		{
			String xmlString = xml.OuterXml.ToString();
			WriteLine();
			string offset = "";
			for (int i=0; i< xmlString.Length; ++i)
			{
				if (xmlString[i] == '<') 
				{
					if (xmlString[i+1] == '/')
					{	
						offset = offset.Substring(0, offset.Length -2);
						WriteLine();
						Write(offset+"</");
						++i;					
					} 
					else 
					{					
						WriteLine();
						Write(offset+"<");
						offset = offset+"  ";
					}
				} 
				else if (xmlString[i] == '/')
				{
					if (xmlString[i+1] == '>')
					offset = offset.Substring(0, offset.Length -2);
					Write("/>");
					++i;
				} 
				else 
				{
					Write(xmlString[i]);
				}
			}
			WriteLine();

		}

		private void Write(string text)
		{
			Console.Write(text);
		}

		private void WriteLine(string text)
		{
			Write(text+"\n");
		}

		private void WriteLine()
		{
			WriteLine("");
		}

		private void Write(char text)
		{
			Write(text.ToString());
		}

		private void WriteLine(char text)
		{
			WriteLine(text.ToString());
		}

		#endregion
	}

}
