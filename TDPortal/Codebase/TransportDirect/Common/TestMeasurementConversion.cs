//***********************************************
//NAME			: TestMeasurementConversion.cs
//AUTHOR		: Reza Bamshad
//DATE CREATED	: 25/01/2005
//DESCRIPTION	: NUnit test class for MeasurementConversion
//***********************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/TestMeasurementConversion.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:08   mturner
//Initial revision.
//
//   Rev 1.1   Feb 09 2005 09:06:34   RScott
//Updated tests
//
//   Rev 1.0   Jan 26 2005 15:52:26   ralavi
//Initial revision.

using System;
using System.Collections;
using System.Globalization;
using NUnit.Framework;



namespace TransportDirect.Common
{
	/// <summary>
	/// Summary description for TestTDDateTime.
	/// </summary>
	[TestFixture]
	public class TestMeasurementConversion
	{
		
		//private static double result;
					
		/// <summary>
		/// Tests the Constructor
		/// </summary>
		[Test]
		public void TestConvertionMilesToKilometres()
		{
			
			string result = MeasurementConversion.Convert(120.50, ConversionType.MilesToKilometres);
			Assert.AreEqual("193.92", result, "Error");
			
		}

		[Test]
		public void TestConvertionKilometresToMiles()
		{
			
			string result = MeasurementConversion.Convert(120.50, ConversionType.KilometresToMiles);
			Assert.AreEqual("74.89", result, "Error");
			
		}

		[Test]
		public void TestConvertionGallonsToLitres()
		{
			
			string result = MeasurementConversion.Convert(120.50, ConversionType.GallonsToLitres);
			Assert.AreEqual("547.73", result, "Error");
			
		}

		[Test]
		public void TestConvertionLitresToGallons()
		{
			
			string result = MeasurementConversion.Convert(120.50, ConversionType.LitresToGallons);
			Assert.AreEqual("26.51", result, "Error");
			
		}

		[Test]
		public void TestConvertionMetresToMileage()
		{
			
			string result = MeasurementConversion.Convert(120.50, ConversionType.MetresToMileage);
			Assert.AreEqual(".07", result, "Error");
			
		}

		[Test]
		public void TestConvertionMileageToMetres()
		{
			
			string result = MeasurementConversion.Convert(120.50, ConversionType.MileageToMetres);
			Assert.AreEqual("193,916.96", result, "Error");
			
		}
	}
}
