//********************************************************************************
//NAME         : TestTrainDto.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : NUnit test script for TrainDto
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/Test/TestTrainDto.cs-arc  $
//
//   Rev 1.1   Feb 18 2009 18:15:02   mmodi
//Updated following change to TrainDto
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:36:02   mturner
//Initial revision.
//
//   Rev 1.8   Aug 25 2005 14:41:18   RPhilpott
//Pass Retail Train Id to RVBO in place of UID.
//Resolution for 2710: NRS interface -- retail train id needed
//
//   Rev 1.7   Apr 14 2005 21:02:38   RPhilpott
//Unit test changes
//
//   Rev 1.6   Mar 01 2005 18:43:12   RPhilpott
//Cost Search Back End for Del 7 - work in progress
//
//   Rev 1.5   Feb 07 2005 15:17:26   RScott
//Assertion changed to Assert
//
//   Rev 1.4   Nov 24 2003 15:04:40   CHosegood
//Added XML doco
//
//   Rev 1.3   Nov 12 2003 10:25:20   CHosegood
//Changed for fxCop
//
//   Rev 1.2   Oct 19 2003 14:47:58   acaunt
//Makes use of updated TrainDto constructor
//
//   Rev 1.1   Oct 15 2003 11:35:26   CHosegood
//Intermediate stops is now a collection of LocationDtos
//
//   Rev 1.0   Oct 13 2003 13:26:58   CHosegood
//Initial Revision

using NUnit.Framework;

using System;
using System.Diagnostics;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingMessages
{
    /// <summary>
    /// NUnit test script for TrainDto.
    /// </summary>
    [TestFixture]
	public class TestTrainDto
	{

        /// <summary>
        /// NUnit test script for TrainDto
        /// </summary>
		public TestTrainDto() { }

        /// <summary>
        /// NUnit initialisation
        /// </summary>
        [SetUp] public void SetUp() { }
 
        /// <summary>
        /// NUnit Tear down
        /// </summary>
        [TearDown] public void TearDown() { }

        /// <summary>
        /// Test that the TOCS may be set correctly
        /// </summary>
        [Test]
        public void TestTocs() 
        {
			string uid = string.Empty;
			string retailId = string.Empty;
			string sleeper = null;
			string catering = null;
			string reservability = null;
			string trainClass = null;
			StopDto origin = null;
			StopDto alight = null;
			StopDto board = null;
            StopDto destination = null;
            LocationDto[] intermediateStops = null;
            TocDto[] tocs = null;
            TocDto[] emptyTocs = {new TocDto(string.Empty), new TocDto(string.Empty) };
            TocDto[] fullTocs =  {new TocDto("VT"), new TocDto("AW") };
            string[] routeCodes = new string[0];

            TrainDto train = new TrainDto(uid, retailId, tocs, trainClass, sleeper, reservability, catering, ReturnIndicator.Outbound, origin, board, alight, destination, intermediateStops, routeCodes );
            Assert.AreEqual(emptyTocs[0].Code , train.Tocs[0].Code, "Tocs is correctly not set to null");
            Assert.AreEqual(emptyTocs[1].Code , train.Tocs[1].Code, "Tocs is correctly not set to null");

			train = new TrainDto(uid, retailId, tocs, trainClass, sleeper, reservability, catering, ReturnIndicator.Outbound, origin, board, alight, destination, intermediateStops, routeCodes );
			Assert.AreEqual(emptyTocs[0].Code , train.Tocs[0].Code, "Tocs is correctly not set to null");
            Assert.AreEqual(emptyTocs[1].Code , train.Tocs[1].Code, "Tocs is correctly not set to null");

			train = new TrainDto(uid, retailId, fullTocs, trainClass, sleeper, reservability, catering, ReturnIndicator.Outbound, origin, board, alight, destination, intermediateStops, routeCodes );
			Assert.AreEqual(fullTocs[0].Code , train.Tocs[0].Code, "Tocs is correctly not set to null");
            Assert.AreEqual(fullTocs[1].Code , train.Tocs[1].Code, "Tocs is correctly not set to null");
        }

        /// <summary>
        /// Test that the sleeper may be set correctly
        /// </summary>
        [Test]
        public void TestSleeper() 
        {
			string uid = string.Empty;
			string retailId = string.Empty;
			string sleeper = null;
			string catering = null;
			string reservability = null;
			string trainClass = null;
			StopDto origin = null;
			StopDto alight = null;
			StopDto board = null;
			StopDto destination = null;
			LocationDto[] intermediateStops = null;
            TocDto[] tocs = new TocDto[2];
            string[] routeCodes = new string[0];

			TrainDto train = new TrainDto(uid, retailId, tocs, trainClass, sleeper, reservability, catering, ReturnIndicator.Outbound, origin, board, alight, destination, intermediateStops, routeCodes );
            Assert.AreEqual(string.Empty.PadLeft(1, ' '), train.Sleeper, "Correctly set sleeper" );

            sleeper = "a";
            train.Sleeper = sleeper;
            Assert.AreEqual( sleeper.PadLeft(1, ' '), train.Sleeper, "Correctly set sleeper"  );
        }

        /// <summary>
        /// Test that the train UID may be set correctly
        /// </summary>
        [Test]
        public void TestUid() 
        {
            string uid = string.Empty;
			string retailId = string.Empty;
			string sleeper = null;
			string catering = null;
			string reservability = null;
			string trainClass = null;
			StopDto origin = null;
			StopDto alight = null;
			StopDto board = null;
            StopDto destination = null;
            LocationDto[] intermediateStops = null;
            TocDto[] tocs = new TocDto[2];
            string[] routeCodes = new string[0];

			TrainDto train = new TrainDto(uid, retailId, tocs, trainClass, sleeper, reservability, catering, ReturnIndicator.Outbound, origin, board, alight, destination, intermediateStops, routeCodes );
			Assert.AreEqual(string.Empty.PadLeft(6, ' '), train.Uid, "Correctly set train UID" );

            uid = "123";
            train.Uid = uid;
            Assert.AreEqual(uid.PadLeft(6, ' '), train.Uid, "Correctly set train UID" );

            uid = "123456";
            train.Uid = uid;
            Assert.AreEqual(uid.PadLeft(6, ' '), train.Uid, "Correctly set train UID" );

            uid = null;
            train.Uid = uid;
            Assert.AreEqual(string.Empty.PadLeft(6, ' '), train.Uid, "Correctly set train UID" );
        }

        /// <summary>
        /// Test that the expected exception is thrown if an attempt to set
        /// the train UID to an invalid value is made
        /// </summary>
        [Test]
        [ExpectedException( typeof(TDException) )]
        public void TestInvalidUid() 
        {
			string uid = "1234567";
			string retailId = string.Empty;
			string sleeper = null;
			string catering = null;
			string reservability = null;
			string trainClass = null;
			StopDto origin = null;
			StopDto alight = null;
			StopDto board = null;
			StopDto destination = null;
			LocationDto[] intermediateStops = null;
            TocDto[] tocs = new TocDto[2];
            string[] routeCodes = new string[0];

			TrainDto train = new TrainDto(uid, retailId, tocs, trainClass, sleeper, reservability, catering, ReturnIndicator.Outbound, origin, board, alight, destination, intermediateStops, routeCodes );
			Assert.AreEqual(string.Empty.PadLeft(6, ' '), train.Uid, "Correctly set train UID" );
        }
	}
}
