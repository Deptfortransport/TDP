// *********************************************** 
// NAME                 : MockCodeGazetteers.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 14/01/2005
// DESCRIPTION  : Mock code gazetteer classes for DBS unit tests
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/Test/MockCodeGazetteers.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:46   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 17:17:50   passuied
//Initial revision.
//
//   Rev 1.3   Jan 18 2005 17:36:14   passuied
//changed after update of CjpInterface
//
//   Rev 1.2   Jan 17 2005 14:49:04   passuied
//Unit Tests OK!
//
//   Rev 1.1   Jan 14 2005 20:59:44   passuied
//back up of unit test. under construction
//
//   Rev 1.0   Jan 14 2005 18:44:30   passuied
//Initial revision.

using System;

using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.DepartureBoardService.Test
{
	
	public class MockCrsCodeGazetteerValid : ITDCodeGazetteer, IServiceFactory
	{
		public MockCrsCodeGazetteerValid()
		{
		}

		/// <summary>
		/// Method that identifies a given code as a valid code or not. 
		/// </summary>
		/// <param name="code"> code to identify.</param>
		/// <returns>If valid, all associated codes will be returned in a an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty</returns>
		public TDCodeDetail[] FindCode( 
			string code)
		{
			TDCodeDetail[] results = new TDCodeDetail[1];

			results[0] = new TDCodeDetail();
			results[0].Code = "EUS";
			results[0].CodeType = TDCodeType.CRS;
			results[0].Description = "London Euston Train station";
			results[0].NaptanId = "9100LEUS";
			results[0].Locality = "XYZ";
			results[0].Region = "London";
			results[0].Easting = 123456;
			results[0].Northing = 654321;
			results[0].ModeType = TDModeType.Rail;

			return results;
		}

		/// <summary>
		/// method that takes a given text entry and searches for associated codes.
		/// </summary>
		/// <param name="text">text to find</param>
		/// <param name="fuzzy">indicate if user is unsure of spelling</param>
		/// <param name="modeTypes">gives requested mode types-associated codes to be returned.</param>
		/// <returns>If codes are found, all matching ones will be returned in an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty.</returns>
		public TDCodeDetail[] FindText(
			string text,
			bool fuzzy,
			TDModeType[] modeTypes)
		{
			return new TDCodeDetail[0];
		}

		public object Get()
		{
			return this;
		}
	}

	public class MockMultipleCrsCodeGazetteerValid : ITDCodeGazetteer, IServiceFactory
	{
		public MockMultipleCrsCodeGazetteerValid()
		{
		}

		/// <summary>
		/// Method that identifies a given code as a valid code or not. 
		/// </summary>
		/// <param name="code"> code to identify.</param>
		/// <returns>If valid, all associated codes will be returned in a an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty</returns>
		public TDCodeDetail[] FindCode( 
			string code)
		{
			TDCodeDetail[] results = new TDCodeDetail[3];

			results[0] = new TDCodeDetail();
			results[0].Code = "EUS";
			results[0].CodeType = TDCodeType.CRS;
			results[0].Description = "London Euston Train station";
			results[0].NaptanId = "9100LEUS1";
			results[0].Locality = "XYZ";
			results[0].Region = "London";
			results[0].Easting = 123456;
			results[0].Northing = 654321;
			results[0].ModeType = TDModeType.Rail;

			results[1] = new TDCodeDetail();
			results[1].Code = "EUS";
			results[1].CodeType = TDCodeType.CRS;
			results[1].Description = "London Euston Train station";
			results[1].NaptanId = "9100LEUS2";
			results[1].Locality = "XYZ";
			results[1].Region = "London";
			results[1].Easting = 123456;
			results[1].Northing = 654321;
			results[1].ModeType = TDModeType.Rail;

			results[2] = new TDCodeDetail();
			results[2].Code = "EUS";
			results[2].CodeType = TDCodeType.CRS;
			results[2].Description = "London Euston Train station";
			results[2].NaptanId = "9100LEUS3";
			results[2].Locality = "XYZ";
			results[2].Region = "London";
			results[2].Easting = 123456;
			results[2].Northing = 654321;
			results[2].ModeType = TDModeType.Rail;

			return results;
		}

		/// <summary>
		/// method that takes a given text entry and searches for associated codes.
		/// </summary>
		/// <param name="text">text to find</param>
		/// <param name="fuzzy">indicate if user is unsure of spelling</param>
		/// <param name="modeTypes">gives requested mode types-associated codes to be returned.</param>
		/// <returns>If codes are found, all matching ones will be returned in an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty.</returns>
		public TDCodeDetail[] FindText(
			string text,
			bool fuzzy,
			TDModeType[] modeTypes)
		{
			return new TDCodeDetail[0];
		}

		public object Get()
		{
			return this;
		}
	}

	public class MockMultipleCrsIATACodeGazetteer : ITDCodeGazetteer, IServiceFactory
	{
		public MockMultipleCrsIATACodeGazetteer()
		{
		}

		/// <summary>
		/// Method that identifies a given code as a valid code or not. 
		/// </summary>
		/// <param name="code"> code to identify.</param>
		/// <returns>If valid, all associated codes will be returned in a an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty</returns>
		public TDCodeDetail[] FindCode( 
			string code)
		{
			TDCodeDetail[] results = new TDCodeDetail[4];

			results[0] = new TDCodeDetail();
			results[0].Code = "LGW";
			results[0].CodeType = TDCodeType.CRS;
			results[0].Description = "Langwathby Train station";
			results[0].NaptanId = "9100LGWB1";
			results[0].Locality = "XYZ";
			results[0].Region = "Wales";
			results[0].Easting = 123456;
			results[0].Northing = 654321;
			results[0].ModeType = TDModeType.Rail;

			results[1] = new TDCodeDetail();
			results[1].Code = "LGW";
			results[1].CodeType = TDCodeType.CRS;
			results[1].Description = "Langwathby Train station";
			results[1].NaptanId = "9100LGWB2";
			results[1].Locality = "XYZ";
			results[1].Region = "Wales";
			results[1].Easting = 123456;
			results[1].Northing = 654321;
			results[1].ModeType = TDModeType.Rail;

			results[2] = new TDCodeDetail();
			results[2].Code = "LGW";
			results[2].CodeType = TDCodeType.IATA;
			results[2].Description = "London Gatwick Airport";
			results[2].NaptanId = "9200LGW3";
			results[2].Locality = "ZYX";
			results[2].Region = "London";
			results[2].Easting = 6789;
			results[2].Northing = 9876;
			results[2].ModeType = TDModeType.Air;

			results[3] = new TDCodeDetail();
			results[3].Code = "LGW";
			results[3].CodeType = TDCodeType.CRS;
			results[3].Description = "Langwathby Train station";
			results[3].NaptanId = "9100LGWB3";
			results[3].Locality = "XYZ";
			results[3].Region = "Wales";
			results[3].Easting = 123456;
			results[3].Northing = 654321;
			results[3].ModeType = TDModeType.Rail;

			return results;
		}

		/// <summary>
		/// method that takes a given text entry and searches for associated codes.
		/// </summary>
		/// <param name="text">text to find</param>
		/// <param name="fuzzy">indicate if user is unsure of spelling</param>
		/// <param name="modeTypes">gives requested mode types-associated codes to be returned.</param>
		/// <returns>If codes are found, all matching ones will be returned in an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty.</returns>
		public TDCodeDetail[] FindText(
			string text,
			bool fuzzy,
			TDModeType[] modeTypes)
		{
			return new TDCodeDetail[0];
		}

		public object Get()
		{
			return this;
		}
	}

	public class MockIATAonlyCodeGazetteer : ITDCodeGazetteer, IServiceFactory
	{
		public MockIATAonlyCodeGazetteer()
		{
		}

		/// <summary>
		/// Method that identifies a given code as a valid code or not. 
		/// </summary>
		/// <param name="code"> code to identify.</param>
		/// <returns>If valid, all associated codes will be returned in a an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty</returns>
		public TDCodeDetail[] FindCode( 
			string code)
		{
			TDCodeDetail[] results = new TDCodeDetail[1];

			

			results[0] = new TDCodeDetail();
			results[0].Code = "LGW";
			results[0].CodeType = TDCodeType.IATA;
			results[0].Description = "London Gatwick Airport";
			results[0].NaptanId = "9200LGW3";
			results[0].Locality = "ZYX";
			results[0].Region = "London";
			results[0].Easting = 6789;
			results[0].Northing = 9876;
			results[0].ModeType = TDModeType.Air;


			return results;
		}

		/// <summary>
		/// method that takes a given text entry and searches for associated codes.
		/// </summary>
		/// <param name="text">text to find</param>
		/// <param name="fuzzy">indicate if user is unsure of spelling</param>
		/// <param name="modeTypes">gives requested mode types-associated codes to be returned.</param>
		/// <returns>If codes are found, all matching ones will be returned in an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty.</returns>
		public TDCodeDetail[] FindText(
			string text,
			bool fuzzy,
			TDModeType[] modeTypes)
		{
			return new TDCodeDetail[0];
		}

		public object Get()
		{
			return this;
		}
	}

	public class MockSmsCodeGazetteerValid : ITDCodeGazetteer, IServiceFactory
	{
		public MockSmsCodeGazetteerValid()
		{
		}

		/// <summary>
		/// Method that identifies a given code as a valid code or not. 
		/// </summary>
		/// <param name="code"> code to identify.</param>
		/// <returns>If valid, all associated codes will be returned in a an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty</returns>
		public TDCodeDetail[] FindCode( 
			string code)
		{
			TDCodeDetail[] results = new TDCodeDetail[1];

			results[0] = new TDCodeDetail();
			results[0].Code = "WSXWMW12";
			results[0].CodeType = TDCodeType.SMS;
			results[0].Description = "London Euston bus station";
			results[0].NaptanId = "9000LEUS";
			results[0].Locality = "XYZ";
			results[0].Region = "London";
			results[0].Easting = 123456;
			results[0].Northing = 654321;
			results[0].ModeType = TDModeType.Bus;

			return results;
		}

		/// <summary>
		/// method that takes a given text entry and searches for associated codes.
		/// </summary>
		/// <param name="text">text to find</param>
		/// <param name="fuzzy">indicate if user is unsure of spelling</param>
		/// <param name="modeTypes">gives requested mode types-associated codes to be returned.</param>
		/// <returns>If codes are found, all matching ones will be returned in an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty.</returns>
		public TDCodeDetail[] FindText(
			string text,
			bool fuzzy,
			TDModeType[] modeTypes)
		{
			return new TDCodeDetail[0];
		}

		public object Get()
		{
			return this;
		}
	}



	public class MockCodeGazetteerEmpty : ITDCodeGazetteer, IServiceFactory
	{
		public MockCodeGazetteerEmpty()
		{
		}

		/// <summary>
		/// Method that identifies a given code as a valid code or not. 
		/// </summary>
		/// <param name="code"> code to identify.</param>
		/// <returns>If valid, all associated codes will be returned in a an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty</returns>
		public TDCodeDetail[] FindCode( 
			string code)
		{
			return new TDCodeDetail[0];
		}

		/// <summary>
		/// method that takes a given text entry and searches for associated codes.
		/// </summary>
		/// <param name="text">text to find</param>
		/// <param name="fuzzy">indicate if user is unsure of spelling</param>
		/// <param name="modeTypes">gives requested mode types-associated codes to be returned.</param>
		/// <returns>If codes are found, all matching ones will be returned in an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty.</returns>
		public TDCodeDetail[] FindText(
			string text,
			bool fuzzy,
			TDModeType[] modeTypes)
		{
			return new TDCodeDetail[0];
		}

		public object Get()
		{
			return this;
		}

	}
	public class MockCrsOrSmsCodeGazetteer : ITDCodeGazetteer, IServiceFactory
	{
		public MockCrsOrSmsCodeGazetteer()
		{
		}

		/// <summary>
		/// Method that identifies a given code as a valid code or not. 
		/// </summary>
		/// <param name="code"> code to identify.</param>
		/// <returns>If valid, all associated codes will be returned in a an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty</returns>
		public TDCodeDetail[] FindCode( 
			string code)
		{
			if ( code.Length == 3)
			{
				TDCodeDetail[] results = new TDCodeDetail[3];

				results[0] = new TDCodeDetail();
				results[0].Code = "EUS";
				results[0].CodeType = TDCodeType.CRS;
				results[0].Description = "London Euston Train station";
				results[0].NaptanId = "9100LEUS1";
				results[0].Locality = "XYZ";
				results[0].Region = "London";
				results[0].Easting = 123456;
				results[0].Northing = 654321;
				results[0].ModeType = TDModeType.Rail;

				results[1] = new TDCodeDetail();
				results[1].Code = "EUS";
				results[1].CodeType = TDCodeType.CRS;
				results[1].Description = "London Euston Train station";
				results[1].NaptanId = "9100LEUS2";
				results[1].Locality = "XYZ";
				results[1].Region = "London";
				results[1].Easting = 123456;
				results[1].Northing = 654321;
				results[1].ModeType = TDModeType.Rail;

				results[2] = new TDCodeDetail();
				results[2].Code = "EUS";
				results[2].CodeType = TDCodeType.CRS;
				results[2].Description = "London Euston Train station";
				results[2].NaptanId = "9100LEUS3";
				results[2].Locality = "XYZ";
				results[2].Region = "London";
				results[2].Easting = 123456;
				results[2].Northing = 654321;
				results[2].ModeType = TDModeType.Rail;

				return results;
			}
			else
			{
				TDCodeDetail[] results = new TDCodeDetail[1];

				results[0] = new TDCodeDetail();
				results[0].Code = "WSXWMW12";
				results[0].CodeType = TDCodeType.SMS;
				results[0].Description = "London Euston bus station";
				results[0].NaptanId = "9000LEUS";
				results[0].Locality = "XYZ";
				results[0].Region = "London";
				results[0].Easting = 123456;
				results[0].Northing = 654321;
				results[0].ModeType = TDModeType.Bus;

				return results;
			}

			
		}

		/// <summary>
		/// method that takes a given text entry and searches for associated codes.
		/// </summary>
		/// <param name="text">text to find</param>
		/// <param name="fuzzy">indicate if user is unsure of spelling</param>
		/// <param name="modeTypes">gives requested mode types-associated codes to be returned.</param>
		/// <returns>If codes are found, all matching ones will be returned in an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty.</returns>
		public TDCodeDetail[] FindText(
			string text,
			bool fuzzy,
			TDModeType[] modeTypes)
		{
			return new TDCodeDetail[0];
		}

		public object Get()
		{
			return this;
		}
	}
	
}
