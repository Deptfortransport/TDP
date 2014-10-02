//********************************************************************************
//NAME         : TestPricingRequestDto.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : NUnit test script for PricingRequestDto
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/Test/TestPricingRequestDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:00   mturner
//Initial revision.
//
//   Rev 1.4   Apr 14 2005 21:02:38   RPhilpott
//Unit test changes
//
//   Rev 1.3   Feb 07 2005 15:17:28   RScott
//Assertion changed to Assert
//
//   Rev 1.2   Oct 15 2003 20:03:04   CHosegood
//Changed all occurences of DateTime to TDDateTime
//
//   Rev 1.1   Oct 13 2003 13:28:46   CHosegood
//Added/changed tests
//
//   Rev 1.0   Oct 08 2003 11:34:18   CHosegood
//Initial Revision

using NUnit.Framework;

using System;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// NUnit test script for PricingRequestDto.
	/// </summary>
	[TestFixture]
	public class TestPricingRequestDto
	{

        /// <summary>
        /// Default constructor
        /// </summary>
		public TestPricingRequestDto() {}

        /// <summary>
        /// nUnit initialisation
        /// </summary>
        [SetUp] public void SetUp()
        {
        }

        /// <summary>
        /// NUnit Tear down
        /// </summary>
        [TearDown] public void TearDown()
        {
        }

        /// <summary>
        /// Test that the outward date may be set to a valid value
        /// </summary>
        [Test] public void TestValidOutwardDate() 
        {
            PricingRequestDto request = new PricingRequestDto();
            TDDateTime now = TDDateTime.Now;

            //Test that the outward date can be correctly set if the
            //Returndate has not yet been initialised.
            request.ReturnDate = null;
            request.OutwardDate = TDDateTime.Now;

            //Test that the Outward date can be correctly set if the
            //ReturnDate has been initialised.
            request.ReturnDate = now.Add( new TimeSpan(1,0,0) );
            request.OutwardDate = now;
        }


        /// <summary>
        /// Test that the return date may be set to a valid value
        /// </summary>
        [Test] public void TestValidReturnDate() 
        {
            PricingRequestDto request = new PricingRequestDto();
            TDDateTime now = TDDateTime.Now;

            //Test that the outward date can be correctly set if the
            //Returndate has not yet been initialised.
            request.OutwardDate = null;
            request.ReturnDate = now;

            //Reset the dates
            request.OutwardDate = null;
            request.ReturnDate = null;

            //Test that the Outward date can be correctly set if the
            //ReturnDate has been initialised.
            request.OutwardDate = now;
            request.ReturnDate = now.Add( new TimeSpan(1,0,0) );

        }

        /// <summary>
        /// Test that the number of adults may be set correctly
        /// </summary>
        [Test]
        public void TestValidNumberOfAdults() 
        {
            PricingRequestDto request = new PricingRequestDto();
            int adults = 1;
            request.NumberOfAdults = adults;
            Assert.AreEqual( adults, request.NumberOfAdults, "Checking that the number of adults is " + adults );

            adults = 0;
            request.NumberOfAdults = adults;
            Assert.AreEqual( adults, request.NumberOfAdults, "Checking that the number of adults is " + adults );

            adults = 10;
            request.NumberOfAdults = adults;
            Assert.AreEqual( adults, request.NumberOfAdults, "Checking that the number of adults is " + adults );
        }

        /// <summary>
        /// Test that the expected exception is thrown if an attempt to set
        /// the number of adults to an invalid value is made
        /// </summary>
        [Test]
        [ExpectedException(typeof(TDException)) ]
        public void TestInvalidNumberOfAdults() 
        {
            PricingRequestDto request = new PricingRequestDto();
            int adults = -1;
            request.NumberOfAdults = adults;
            Assert.AreEqual( adults, request.NumberOfAdults, "Checking that the number of adults is " + adults );
        }

        /// <summary>
        /// Test that the number of children may be set correctly
        /// </summary>
        [Test]
        public void TestValidNumberOfChildren() 
        {
            PricingRequestDto request = new PricingRequestDto();
            int children = 1;
            request.NumberOfChildren = children;
            Assert.AreEqual( children, request.NumberOfChildren, "Checking that the number of children is " + children );

            children = 0;
            request.NumberOfChildren = children;
            Assert.AreEqual( children, children, request.NumberOfChildren, "Checking that the number of children is " + children );

            children = 10;
            request.NumberOfChildren = children;
            Assert.AreEqual( children, request.NumberOfChildren, "Checking that the number of children is " + children );

        }

        /// <summary>
        /// Test that the expected exception is thrown if an attempt to set
        /// the number of children to an invalid value is made
        /// </summary>
        [Test]
        [ExpectedException(typeof(TDException)) ]
        public void TestInvalidNumberOfChildren() 
        {
            PricingRequestDto request = new PricingRequestDto();
            int children = -1;
            request.NumberOfChildren = children;
            Assert.AreEqual( children, request.NumberOfChildren, "Checking that the number of children is " + children );
        }

        /// <summary>
        /// Test that the railcard may be set correctly
        /// </summary>
        [Test]
        public void TestValidRailcard() 
        {
            PricingRequestDto request = new PricingRequestDto();
            string railcard = "ABC";
            request.Railcard = railcard;
            Assert.AreEqual( railcard, request.Railcard, "Checking that the railcard is " + railcard );

            railcard = "   ";
            request.Railcard = railcard;
            Assert.AreEqual( railcard, request.Railcard, "Checking that the railcard is " + railcard );

            railcard = "123";
            request.Railcard = railcard;
            Assert.AreEqual( railcard, request.Railcard, "Checking that the railcard is " + railcard );
        }

        /// <summary>
        /// Test that the expected exception is thrown if an attempt to set
        /// the railcard to a value that is too large
        /// </summary>
        [Test]
        [ExpectedException(typeof(TDException)) ]
        public void TestLargeInvalidRailcard() 
        {
            PricingRequestDto request = new PricingRequestDto();
            string railcard = "1234";
            request.Railcard = railcard;
            Assert.AreEqual( railcard, request.Railcard, "Checking that the railcard is " + railcard );
        }

		/// <summary>
		/// Test that the expected exception is thrown if an attempt to set
		/// the railcard to a value that is too small
		/// </summary>
		[Test]
		[ExpectedException(typeof(TDException)) ]
		public void TestSmallInvalidRailcard() 
		{
			PricingRequestDto request = new PricingRequestDto();
			string railcard = "12";
			request.Railcard = railcard;
			Assert.AreEqual( railcard, request.Railcard, "Checking that the railcard is " + railcard );
		}


		/// <summary>
		/// Test that the sorting of the PricingRequestDto is correct 
		/// (ie, that an ArrayList is sorted in order:
		///		origin
		///		destination
		///		outward date
		///		return date
		/// </summary>
		[Test]
		public void TestSort() 
		{
			ArrayList requests = new ArrayList();
			
			LocationDto origin1 = new LocationDto("AAA", "1111");
			LocationDto origin2 = new LocationDto("BBB", "2222");
			LocationDto origin3 = new LocationDto("CCC", "3333");

			LocationDto dest1 = new LocationDto("AAA", "1111");
			LocationDto dest2 = new LocationDto("BBB", "2222");
			LocationDto dest3 = new LocationDto("CCC", "3333");

			TDDateTime outDate1 = new TDDateTime(2005, 04, 01);
			TDDateTime outDate2 = new TDDateTime(2005, 04, 02);

			TDDateTime returnDate1 = new TDDateTime(2005, 04, 10);
			TDDateTime returnDate2 = new TDDateTime(2005, 04, 20);

			PricingRequestDto request;

			request = new PricingRequestDto(9, 1, 0, string.Empty, outDate1, returnDate1, origin2, dest3, JourneyType.Return);
			requests.Add(request);			
			
			request = new PricingRequestDto(9, 1, 0, string.Empty, outDate1, returnDate1, origin1, dest2, JourneyType.Return);
			requests.Add(request);			

			request = new PricingRequestDto(9, 1, 0, string.Empty, outDate2, null, origin3, dest2, JourneyType.Return);
			requests.Add(request);			
			
			request = new PricingRequestDto(9, 1, 0, string.Empty, outDate1, null, origin3, dest2, JourneyType.Return);
			requests.Add(request);			

			request = new PricingRequestDto(9, 1, 0, string.Empty, outDate1, returnDate1, origin1, dest1, JourneyType.Return);
			requests.Add(request);			
			
			requests.Sort();
			
			Assert.AreEqual(((PricingRequestDto)requests[0]).Origin.Crs, origin1.Crs, "Item 1");
			Assert.AreEqual(((PricingRequestDto)requests[0]).Destination.Crs, dest1.Crs, "Item 1");

			Assert.AreEqual(((PricingRequestDto)requests[1]).Origin.Crs, origin1.Crs, "Item 2");
			Assert.AreEqual(((PricingRequestDto)requests[1]).Destination.Crs, dest2.Crs, "Item 2");

			Assert.AreEqual(((PricingRequestDto)requests[2]).Origin.Crs, origin2.Crs, "Item 3");
			Assert.AreEqual(((PricingRequestDto)requests[2]).Destination.Crs, dest3.Crs, "Item 3");

			Assert.AreEqual(((PricingRequestDto)requests[3]).Origin.Crs, origin3.Crs, "Item 4");
			Assert.AreEqual(((PricingRequestDto)requests[3]).Destination.Crs, dest2.Crs, "Item 4");
			Assert.AreEqual(((PricingRequestDto)requests[3]).OutwardDate, outDate1, "Item 4");

			Assert.AreEqual(((PricingRequestDto)requests[4]).Origin.Crs, origin3.Crs, "Item 5");
			Assert.AreEqual(((PricingRequestDto)requests[4]).Destination.Crs, dest2.Crs, "Item 5");
			Assert.AreEqual(((PricingRequestDto)requests[4]).OutwardDate, outDate2, "Item 5");

		}

	}
}
