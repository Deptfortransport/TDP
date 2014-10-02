//********************************************************************************
//NAME         : TestTTBOParametersDto
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-04-13
//DESCRIPTION  : NUnit test script for TTBOParametersDto
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/Test/TestTTBOParametersDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:02   mturner
//Initial revision.
//
//   Rev 1.0   Apr 14 2005 21:03:16   RPhilpott
//Initial revision.
//

using NUnit.Framework;

using System;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// NUnit test script for TTBOParametersDto.
	/// </summary>
	[TestFixture]

	public class TestTTBOParametersDto
	{
		public TestTTBOParametersDto()
		{
		}


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
		public void Test1() 
		{

			TTBOParametersDto dto = new TTBOParametersDto();

			string gcString = "0000  N0000                                                                                                                                                                                                                                                                                                                                                                        0001U2                                      AW MSW MWE M                                                                                                                                                    ";

			string gnString = "WFJEWIJESWIEHRHETBDEGLDESALI            AW MBSW MBWE MB                                                                                                                                                                                         ";

			TDDateTime dateTime = new TDDateTime(2005, 04, 16, 0, 0, 0);

			dto.PopulateFromGCOutput(gcString, dateTime);

			dto.PopulateFromGNOutput(gnString);

			Assert.AreEqual(dto.ExcludeTocs.Length, 0);
			Assert.AreEqual(dto.IncludeTocs.Length, 3);
			Assert.AreEqual(dto.IncludeTocs[0].Code, "AW");
			Assert.AreEqual(dto.IncludeTocs[1].Code, "SW");
			Assert.AreEqual(dto.IncludeTocs[2].Code, "WE");
			Assert.AreEqual(dto.IncludeCrsLocations.Length, 1);
			Assert.AreEqual(dto.IncludeCrsLocations[0], "SAL");
			Assert.AreEqual(dto.ExcludeCrsLocations.Length, 6);
			Assert.AreEqual(dto.ExcludeCrsLocations[0], "WFJ");
			Assert.AreEqual(dto.ExcludeCrsLocations[5], "GLD");

		}

	}
}
