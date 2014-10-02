//********************************************************************************
//NAME         : TestLocationDto
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : NUnit test script for LocationDto
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/Test/TestLocationDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:00   mturner
//Initial revision.
//
//   Rev 1.3   Apr 14 2005 21:02:38   RPhilpott
//Unit test changes
//
//   Rev 1.2   Feb 07 2005 15:17:28   RScott
//Assertion changed to Assert
//
//   Rev 1.1   Mar 19 2004 09:44:18   CHosegood
//Can now set the nlc, crs to an empty string.  Modified tests to test this
//
//   Rev 1.0   Oct 13 2003 13:26:54   CHosegood
//Initial Revision

using NUnit.Framework;

using System;
using System.Diagnostics;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingMessages
{
    /// <summary>
    /// NUnit test script for LocationDto.
    /// </summary>
    [TestFixture]
    public class TestLocationDto
    {

        /// <summary>
        /// NUnit test script for LocationDto
        /// </summary>
        public TestLocationDto() { }

        /// <summary>
        /// NUnit initialisation
        /// </summary>
        [SetUp] public void SetUp() { }

        /// <summary>
        /// NUnit Tear down
        /// </summary>
        [TearDown] public void TearDown() { }

        /// <summary>
        /// Test that the LocationDto CRS may be set correctly
        /// </summary>
        [Test]
        public void TestValidCrs() 
        {
            string crs = "NOT";
            LocationDto destination = new LocationDto( crs,"18260" );     //nottingham
            Assert.AreEqual(destination.Crs.PadRight(3,' '), destination.Crs, "Correctly set LocationDto CRS");

            crs = "LEI";
            destination = new LocationDto( crs,"19470" );     //leicester
            Assert.AreEqual( destination.Crs.PadRight(3,' '), destination.Crs, "Correctly set LocationDto CRS");
        }

        /// <summary>
        /// Test that the LocationDto CRS may be set correctly
        /// </summary>
        [Test]
        public void TestNullCrs() 
        {
            string nlc = "19470";
            string crs = null;
            LocationDto destination = new LocationDto( crs,nlc );     //leicester
            Assert.AreEqual(string.Empty.PadRight(3,' '), destination.Crs, "Correctly set LocationDto CRS");
        }

        /// <summary>
        /// Test that the LocationDto NLC may be set correctly
        /// </summary>
        [Test]
        public void TestValidNlc() 
        {
            string crs = "NOT";
            string nlc = "18260";
            LocationDto destination = new LocationDto( crs, nlc );     //nottingham
            Assert.AreEqual(nlc.Substring(0,4), destination.Nlc, "Correctly set LocationDto NLC");

            crs = "LEI";
            nlc = "19470";
            destination = new LocationDto( crs, nlc );     //leicester
            Assert.AreEqual(nlc.Substring(0,4), destination.Nlc, "Correctly set LocationDto NLC");

            nlc = "1947";
            destination = new LocationDto( crs, nlc );     //leicester
            Assert.AreEqual(nlc.Substring(0,4), destination.Nlc, "Correctly set LocationDto NLC");

            nlc = "194700";
            destination = new LocationDto( crs, nlc );     //leicester
            Assert.AreEqual(nlc.Substring(0,4), destination.Nlc, "Correctly set LocationDto NLC");
        }

        /// <summary>
        /// Test that the LocationDto NLC may be set correctly
        /// </summary>
        [Test]
        public void TestNullNlc() 
        {
            string crs = "LEI";
            string nlc = null;
            LocationDto destination = new LocationDto( crs,nlc );     //leicester
            Assert.AreEqual( string.Empty.PadRight(4,' '), destination.Nlc, "Correctly set LocationDto NLC");
        }

        /// <summary>
        /// Test that the expected exception is thrown if an attempt to set
        /// the LocationDto CRS to an invalid value is made
        /// </summary>
        [Test]
        [ExpectedException(typeof(TDException)) ]
        public void TestInvalidCrs() 
        {
            string crs = "NO";
            string nlc = "18260";
            LocationDto destination = new LocationDto( crs,nlc );     //nottingham
            Assert.AreEqual( crs.PadRight(3,' ' ), destination.Crs, "Correctly set LocationDto CRS" );
        }

        /// <summary>
        /// Test that an attempt to set the LocationDto CRS to an empty string
        /// results in a 3 spaces
        /// </summary>
        [Test]
        public void TestEmptyCrs() 
        {
            string crs = string.Empty;
            string nlc = "19470";
            LocationDto destination = new LocationDto( crs,nlc );     //leicester
            Assert.AreEqual( string.Empty.PadRight(3,' '), destination.Crs, "Correctly set LocationDto CRS" );
        }

        /// <summary>
        /// Test that the expected exception is thrown if an attempt to set
        /// the LocationDto NLC to an invalid value is made
        /// </summary>
        [Test]
        [ExpectedException(typeof(TDException)) ]
        public void TestInvalidNlc() 
        {
            string crs = "NOT";
            string nlc = "182";
            LocationDto destination = new LocationDto( crs, nlc);     //nottingham
            Assert.AreEqual( nlc.Substring(0,4), destination.Nlc, "Correctly set LocationDto NLC" );
        }

        /// <summary>
        /// Test that an attempt to set the LocationDto NLC to an empty string
        /// results in a 4 spaces
        /// </summary>
        [Test]
        public void TestEmptyNlc() 
        {
            string crs = "NOT";
            string nlc = string.Empty;
            LocationDto destination = new LocationDto( crs, nlc);     //nottingham
            Assert.AreEqual( string.Empty.PadRight(4,' '), destination.Nlc, "Correctly set LocationDto NLC" );
        }

	}
}
