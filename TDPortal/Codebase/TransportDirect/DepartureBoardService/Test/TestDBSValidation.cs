// *********************************************** 
// NAME                 : TestDBSValidation.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 14/01/2005
// DESCRIPTION  : Unit tests for DBSValidation class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/Test/TestDBSValidation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:48   mturner
//Initial revision.
//
//   Rev 1.1   Mar 31 2005 19:14:38   schand
//Fixed test script for new the changes (DBSValidation). Fix for 4.4, 4.5
//
//   Rev 1.0   Feb 28 2005 17:17:50   passuied
//Initial revision.
//
//   Rev 1.8   Feb 16 2005 16:21:30   passuied
//changes for new past time window functionality
//
//   Rev 1.7   Feb 16 2005 14:54:24   passuied
//Change in interface and behaviour of time Request
//
//possibility to plan in the past within configurable time window
//
//   Rev 1.6   Feb 11 2005 14:39:12   passuied
//fixed test script
//
//   Rev 1.5   Feb 08 2005 14:38:00   RScott
//Assertions changed to Asserts
//
//   Rev 1.4   Jan 19 2005 14:01:50   passuied
//added more validation + changed UT to allow destination to be optional
//
//   Rev 1.3   Jan 18 2005 17:36:26   passuied
//changed after update of CjpInterface
//
//   Rev 1.2   Jan 17 2005 14:49:20   passuied
//Unit tests OK!
//
//   Rev 1.1   Jan 14 2005 20:59:40   passuied
//back up of unit test. under construction
//
//   Rev 1.0   Jan 14 2005 18:44:30   passuied
//Initial revision.

using System;
using System.Collections;

using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;

namespace TransportDirect.UserPortal.DepartureBoardService.Test
{
	/// <summary>
	/// Unit tests for DBSValidation class
	/// </summary>
	[TestFixture]
	public class TestDBSValidation
	{
		public TestDBSValidation()
		{
		}

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init( new MockServiceInitialisation());
		}

		
		[Test]
		public void TestValidateLocationRequest()
		{
			DBSLocation stop = new DBSLocation();
			ArrayList errors = new ArrayList();
			DBSLocation[] stops = new DBSLocation[]{stop};

			
			// valid CRS stop with all requested info
			stop.Code = "EUS";
			stop.Type = TDCodeType.CRS;
			stop.NaptanIds = new string[]{"9100LDNEUS1", "9100LDNEUS2"};
			stop.Valid = true;

			Assert.AreEqual( true, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.AreEqual( 0, errors.Count);


			// valid SMS stop with all requested info
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "WSXXXXXXX";
			stop.Type = TDCodeType.SMS;
			stop.NaptanIds = new string[]{"9100LDNEUS1", "9100LDNEUS2"};
			stop.Locality = "LOCALITY";
			stop.Valid = true;

			Assert.AreEqual( true, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.AreEqual( 0, errors.Count);

			// Rejected IATA
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};

			stop.Code = "LGW";
			stop.Type = TDCodeType.IATA;
			stop.NaptanIds = new string[]{"9100LDNEUS1", "9100LDNEUS2"};
			stop.Locality = "LOCALITY";
			stop.Valid = true;
			
			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue( errors.Count != 0);


			// Rejected Postcode
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};

			stop.Code = "NW11 8TJ";
			stop.Type = TDCodeType.Postcode;
			stop.NaptanIds = new string[]{"9100LDNEUS1", "9100LDNEUS2"};
			stop.Locality = "LOCALITY";
			stop.Valid = true;

			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue( errors.Count != 0);

			// Empty location array
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[0];

			bool caught = false;
			try
			{
				DBSValidation.ValidateLocationRequest(ref stops, errors);
			}
			catch (TDException tde)
			{
				caught = true;
				Assert.AreEqual(TDExceptionIdentifier.DBSEmptyLocationArray, tde.Identifier);
			}
			Assert.AreEqual(true, caught);
			

			// location in array null
			stop = null;
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue( errors.Count != 0);


			// location marked as invalid, Empty code
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};

			stop.Code = string.Empty;
			stop.Type = TDCodeType.CRS;
			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue( errors.Count != 0);

			stop.Code = string.Empty;
			stop.Type = TDCodeType.SMS;
			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue( errors.Count != 0);


			// location marked as valid, empty code
			stop.Code = string.Empty;
			stop.Type = TDCodeType.CRS;
			stop.Valid = true;
			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue( errors.Count != 0);

			stop.Code = string.Empty;
			stop.Type = TDCodeType.SMS;
			stop.Valid = true;
			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue( errors.Count != 0);


			// -----------------------------------------
			// CRS marked as valid, but invalid. fetching info, successful result
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockCrsCodeGazetteerValid());
			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "EUS";
			stop.Type = TDCodeType.CRS;
			stop.NaptanIds = new string[0];
			stop.Valid = true;

			Assert.AreEqual( true, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count != 0);
			Assert.AreEqual( "EUS", stops[0].Code);
			Assert.IsTrue( stops[0].NaptanIds.Length == 1);
			

			// CRS marked as valid, but invalid. fetching info, successful result but more than 1 code with same CRS returned
			// Aggregation!!!
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockMultipleCrsCodeGazetteerValid());
			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "EUS";
			stop.Type = TDCodeType.CRS;
			stop.NaptanIds = new string[0];
			stop.Valid = true;

			Assert.AreEqual( true, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count != 0);
			Assert.AreEqual( 1, stops.Length);
			Assert.AreEqual( "EUS", stops[0].Code);
			Assert.AreEqual( 3, stops[0].NaptanIds.Length);



			// CRS marked as valid, but invalid. Fetching info, unsuccessful result
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockCodeGazetteerEmpty());
			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "EUS";
			stop.Type = TDCodeType.CRS;
			stop.NaptanIds = new string[0];
			stop.Valid = true;

			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count != 0);
			

			// CRS marked as invalid. Fetching info, successful result
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockMultipleCrsCodeGazetteerValid());
			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "EUS";
			stop.Type = TDCodeType.CRS;
			stop.NaptanIds = new string[0];
			stop.Valid = false;

			Assert.AreEqual( true, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count == 0); // no error as service knows location invalid!
			Assert.AreEqual( 1, stops.Length);
			Assert.AreEqual( "EUS", stops[0].Code);
			Assert.AreEqual( 3, stops[0].NaptanIds.Length);

			// CRS marked as invalid. Fetching info, unsucessful result
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockCodeGazetteerEmpty());
			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "EUS";
			stop.Type = TDCodeType.CRS;
			stop.NaptanIds = new string[0];
			stop.Valid = false;

			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count != 0);

			// -----------------------------------------

			// SMS marked as valid, but invalid (missing Naptans). fetching info, successful result
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockSmsCodeGazetteerValid());			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "WSXWMW12";
			stop.Type = TDCodeType.SMS;
			stop.NaptanIds = new string[0];
			stop.Locality = "LOCALITY";
			stop.Valid = true;

			Assert.AreEqual( true, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count != 0);
			Assert.AreEqual("WSXWMW12", stops[0].Code);
			Assert.AreEqual(1, stops[0].NaptanIds.Length);
			Assert.AreEqual("9000LEUS", stops[0].NaptanIds[0]);
			Assert.AreEqual("XYZ", stops[0].Locality);

			// SMS marked as valid, but invalid (missing Locality). fetching info, successful result
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockSmsCodeGazetteerValid());			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "WSXWMW12";
			stop.Type = TDCodeType.SMS;
			stop.NaptanIds = new string[]{"fjlsjfd", "fdsljl"};
			stop.Locality = string.Empty;
			stop.Valid = true;

			Assert.AreEqual( true, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count != 0);
			Assert.AreEqual("WSXWMW12", stops[0].Code);
			Assert.AreEqual(1, stops[0].NaptanIds.Length);
			Assert.AreEqual("9000LEUS", stops[0].NaptanIds[0]);
			Assert.AreEqual("XYZ", stops[0].Locality);


			// SMS marked as valid, but invalid. Fetching info, unsuccessful result
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockCodeGazetteerEmpty());			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "WSXWMW12";
			stop.Type = TDCodeType.SMS;
			stop.NaptanIds = new string[0];
			stop.Locality = "LOCALITY";
			stop.Valid = true;

			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count != 0);
			

			// SMS makred as invalid. Fetching info, successful result
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockSmsCodeGazetteerValid());			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "WSXWMW12";
			stop.Type = TDCodeType.SMS;
			stop.NaptanIds = new string[0];
			stop.Locality = "LOCALITY";
			stop.Valid = false;

			Assert.AreEqual( true, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count == 0); // no error as service knows location invalid!
			Assert.AreEqual("WSXWMW12", stops[0].Code);
			Assert.AreEqual(1, stops[0].NaptanIds.Length);
			Assert.AreEqual("9000LEUS", stops[0].NaptanIds[0]);
			Assert.AreEqual("XYZ", stops[0].Locality);

			// SMS marked as invalid. Fetching info, unsucessful result
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockCodeGazetteerEmpty());			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "WSXWMW12";
			stop.Type = TDCodeType.SMS;
			stop.NaptanIds = new string[0];
			stop.Locality = "LOCALITY";
			stop.Valid = true;

			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count != 0);
			// --------------------------------------------

			// CRS marked valid but invalid. Fetching info, get IATA + CRS back.
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockMultipleCrsIATACodeGazetteer());
			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "LGW";
			stop.Type = TDCodeType.CRS;
			stop.NaptanIds = new string[0];
			stop.Valid = true;

			Assert.AreEqual( true, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count != 0);
			Assert.AreEqual( 1, stops.Length); // return 1 because IATA rejected (in properties)
			Assert.AreEqual( "LGW", stops[0].Code);
			Assert.AreEqual( 3, stops[0].NaptanIds.Length);
			Assert.AreEqual( TDCodeType.CRS, stops[0].Type);


			// CRS marked invalid. Fetching info, get IATA + CRS back.
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockMultipleCrsIATACodeGazetteer());
			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "LGW";
			stop.Type = TDCodeType.CRS;
			stop.NaptanIds = new string[0];
			stop.Valid = false;

			Assert.AreEqual( true, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count == 0); // no error because was marked as invalid!
			Assert.AreEqual( 1, stops.Length); // return 1 because IATA rejected (in properties)
			Assert.AreEqual( "LGW", stops[0].Code);
			Assert.AreEqual( 3, stops[0].NaptanIds.Length);
			Assert.AreEqual( TDCodeType.CRS, stops[0].Type);


			// CRS invalid. Fetching info but IATA back.
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockIATAonlyCodeGazetteer());
			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "LGW";
			stop.Type = TDCodeType.CRS;
			stop.NaptanIds = new string[0];
			stop.Valid = false;

			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count != 0); 
			Assert.AreEqual( 0, stops.Length); // return 0 because IATA rejected (in properties)

			// CRS marked as valid but invalid. Fetching info but IATA back.
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockIATAonlyCodeGazetteer());
			
			stop = new DBSLocation();
			errors = new ArrayList();
			stops = new DBSLocation[]{stop};
			
			stop.Code = "LGW";
			stop.Type = TDCodeType.CRS;
			stop.NaptanIds = new string[0];
			stop.Valid = true;

			Assert.AreEqual( false, DBSValidation.ValidateLocationRequest(ref stops, errors));
			Assert.IsTrue(  errors.Count != 0); 
			Assert.AreEqual( 0, stops.Length); // return 0 because IATA rejected (in properties)
	
		}

		[Test]
		public void TestValidateLocationRequestOverloaded()
		{
			DBSLocation origin = null;
			DBSLocation dest = null;
			DBSLocation[] origins;
			DBSLocation[] dests;
			DBSValidationType validationType = DBSValidationType.Undefined;
			ArrayList errors = null;

			// test 2 valid locations CRS type
			errors = new ArrayList();

			origin = new DBSLocation();
			origin.Code = "EUS";
			origin.Type = TDCodeType.CRS;
			origin.Locality = "LOC";
			origin.NaptanIds = new string[]{"9100EUS1", "9100EUS2"};
			origin.Valid = true;
			origins = new DBSLocation[]{origin};

			dest = new DBSLocation();
			dest.Code = "KGX";
			dest.Type = TDCodeType.CRS;
			dest.Locality = "LOC2";
			dest.NaptanIds = new string[]{"9100KGX1", "9100KGX2"};
			dest.Valid = true;
			dests = new DBSLocation[]{dest};

			Assert.AreEqual(true, DBSValidation.ValidateLocationRequest(ref origins, errors, ref dests, ref validationType));
			Assert.IsTrue(errors.Count == 0);


			Assert.AreEqual(true, DBSValidation.ValidateLocationRequest(ref dests, errors, ref origins, ref validationType));
			Assert.IsTrue(errors.Count == 0);


			// test 1 valid, 1invalid location fetching info, then valid CRS 
			errors = new ArrayList();

			origin = new DBSLocation();
			origin.Code = "EUS";
			origin.Type = TDCodeType.CRS;
			origin.Locality = "LOC";
			origin.NaptanIds = new string[]{"9100EUS1", "9100EUS2"};
			origin.Valid = true;
			origins = new DBSLocation[]{origin};

			dest = new DBSLocation();
			dest.Code = "KGX";
			dest.Type = TDCodeType.CRS;
			dest.NaptanIds = new string[0];
			dest.Valid = true;
			dests = new DBSLocation[]{dest};

			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockMultipleCrsCodeGazetteerValid());

			Assert.AreEqual(true, DBSValidation.ValidateLocationRequest(ref origins, errors, ref dests, ref validationType));
			Assert.IsTrue(errors.Count != 0);


			Assert.AreEqual(true, DBSValidation.ValidateLocationRequest(ref dests, errors, ref origins,  ref validationType));
			Assert.IsTrue(errors.Count == 0);

			// test 2 valid locations SMS type
			errors = new ArrayList();

			origin = new DBSLocation();
			origin.Code = "WSXJLJHK";
			origin.Type = TDCodeType.SMS;
			origin.Locality = "LOC";
			origin.NaptanIds = new string[]{"9000EUS1", "9000EUS2"};
			origin.Valid = true;
			origins = new DBSLocation[]{origin};

			dest = new DBSLocation();
			dest.Code = "LJFLJLGJ";
			dest.Type = TDCodeType.SMS;
			dest.Locality = "LOC2";
			dest.NaptanIds = new string[]{"9000KGX1", "9000KGX2"};
			dest.Valid = true;
			dests = new DBSLocation[]{dest};

			Assert.AreEqual(true, DBSValidation.ValidateLocationRequest(ref origins, errors, ref dests, ref validationType));
			Assert.IsTrue(errors.Count == 0);

			Assert.AreEqual(true, DBSValidation.ValidateLocationRequest(ref dests, errors, ref origins, ref validationType));
			Assert.IsTrue(errors.Count == 0);


			// test 2 valid locations after fetch CRS/IATA ==> IATA will be excluded
			errors = new ArrayList();

			origin = new DBSLocation();
			origin.Code = "EUS";
			origin.Type = TDCodeType.CRS;
			origin.NaptanIds = new string[0];
			origin.Valid = false;
			origins = new DBSLocation[]{origin};

			dest = new DBSLocation();
			dest.Code = "EUS";
			dest.Type = TDCodeType.CRS;
			dest.NaptanIds = new string[0];
			dest.Valid = false;
			dests = new DBSLocation[]{dest};

			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockMultipleCrsIATACodeGazetteer());

			Assert.AreEqual(true, DBSValidation.ValidateLocationRequest(ref origins, errors, ref dests, ref validationType));
			Assert.IsTrue(errors.Count == 0);


			Assert.AreEqual(true, DBSValidation.ValidateLocationRequest(ref dests, errors, ref origins, ref validationType));
			Assert.IsTrue(errors.Count == 0);

			// test 2 valid locations 1 CRS, 1 SMS ==> error message
			errors = new ArrayList();

			origin = new DBSLocation();
			origin.Code = "EUS";
			origin.Type = TDCodeType.CRS;
			origin.NaptanIds = new string[0];
			origin.Valid = false;
			origins = new DBSLocation[]{origin};

			dest = new DBSLocation();
			dest.Code = "ADSLKJFH";
			dest.Type = TDCodeType.SMS;
			dest.NaptanIds = new string[0];
			dest.Valid = false;
			dests = new DBSLocation[]{dest};

			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockCrsOrSmsCodeGazetteer());

			Assert.AreEqual(false, DBSValidation.ValidateLocationRequest(ref origins, errors, ref dests, ref validationType));
			Assert.IsTrue(errors.Count != 0);
			Assert.AreEqual(0, origins.Length);
			Assert.AreEqual(0, dests.Length);

			// test 2 valid locations 1 CMS, 1 CRS ==> error message
			origin = new DBSLocation();
			origin.Code = "EUS";
			origin.Type = TDCodeType.CRS;
			origin.NaptanIds = new string[0];
			origin.Valid = false;
			origins = new DBSLocation[]{origin};

			dest = new DBSLocation();
			dest.Code = "ADSLKJFH";
			dest.Type = TDCodeType.SMS;
			dest.NaptanIds = new string[0];
			dest.Valid = false;
			dests = new DBSLocation[]{dest};

			Assert.AreEqual(false, DBSValidation.ValidateLocationRequest(ref dests, errors, ref origins, ref validationType));
			Assert.IsTrue(errors.Count != 0);
			Assert.AreEqual(0, origins.Length);
			Assert.AreEqual(0, dests.Length);


			// test 1 valid location CRS, 1 invalid location ==> error message
			errors = new ArrayList();

			origin = new DBSLocation();
			origin.Code = "EUS";
			origin.Type = TDCodeType.CRS;
			origin.NaptanIds = new string[]{"9000EUS1", "9000EUS2"};
			origin.Valid = true;
			origins = new DBSLocation[]{origin};

			dest = new DBSLocation();
			dest.Code = "KGX";
			dest.Type = TDCodeType.CRS;
			dest.NaptanIds = new string[0];
			dest.Valid = false;
			dests = new DBSLocation[]{dest};

			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockCodeGazetteerEmpty());

			Assert.AreEqual(false, DBSValidation.ValidateLocationRequest(ref origins, errors, ref dests, ref validationType));
			Assert.IsTrue(errors.Count != 0);


			Assert.AreEqual(false, DBSValidation.ValidateLocationRequest(ref dests, errors, ref origins, ref validationType));
			Assert.IsTrue(errors.Count != 0);

			// test 1 invalid location, 1 valid location SMS ==> error message
			errors = new ArrayList();

			origin = new DBSLocation();
			origin.Code = "AGSDFHGD";
			origin.Type = TDCodeType.SMS;
			origin.NaptanIds = new string[]{"9000EUS1", "9000EUS2"};
			origin.Locality = "LFH";
			origin.Valid = true;
			origins = new DBSLocation[]{origin};

			dest = new DBSLocation();
			dest.Code = "KGXAMFHG";
			dest.Type = TDCodeType.SMS;
			dest.NaptanIds = new string[0];
			dest.Valid = false;
			dests = new DBSLocation[]{dest};

			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockCodeGazetteerEmpty());

			Assert.AreEqual(false, DBSValidation.ValidateLocationRequest(ref origins, errors, ref dests, ref validationType));
			Assert.IsTrue(errors.Count != 0);


			Assert.AreEqual(false, DBSValidation.ValidateLocationRequest(ref dests, errors, ref origins, ref validationType));
			Assert.IsTrue(errors.Count != 0);

			// test origin valid, destination null ==> valid as destination optional
			errors = new ArrayList();

			origin = new DBSLocation();
			origin.Code = "EUS";
			origin.Type = TDCodeType.CRS;
			origin.Locality = "LOC";
			origin.NaptanIds = new string[]{"9100EUS1", "9100EUS2"};
			origin.Valid = true;
			origins = new DBSLocation[]{origin};

			dest = null;
			dests = new DBSLocation[]{dest};


			Assert.AreEqual(true, DBSValidation.ValidateLocationRequest(ref origins, errors, ref dests, ref validationType));
			Assert.IsTrue(errors.Count == 0);

			// test origin null ==> invalid as origin required
			errors = new ArrayList();

			origin = null;
			origins = new DBSLocation[]{origin};


			dest = new DBSLocation();
			dest.Code = "KGX";
			dest.Type = TDCodeType.CRS;
			dest.Locality = "LOC2";
			dest.NaptanIds = new string[]{"9100KGX1", "9100KGX2"};
			dest.Valid = true;
			dests = new DBSLocation[]{dest};

			Assert.AreEqual(false, DBSValidation.ValidateLocationRequest(ref origins, errors, ref dests, ref validationType));
			Assert.IsTrue(errors.Count != 0);
		}

		[Test]
		public void TestValidateTimeRequest()
		{
			ArrayList errors = new ArrayList();

			// test null time object
			Assert.AreEqual(false, DBSValidation.ValidateTimeRequest(null, errors));
			Assert.IsTrue(errors.Count != 0);

			// -------------------------

			// test valid time object
			
			// default time request 
			DBSTimeRequest time = new DBSTimeRequest();
			errors = new ArrayList();
			Assert.AreEqual(true, DBSValidation.ValidateTimeRequest(time, errors));
			Assert.IsTrue(errors.Count == 0);

			// time request First
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.First;
			errors = new ArrayList();
			Assert.AreEqual(true, DBSValidation.ValidateTimeRequest(time, errors));
			Assert.IsTrue(errors.Count == 0);

			// time request Last
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.Last;
			errors = new ArrayList();
			Assert.AreEqual(true, DBSValidation.ValidateTimeRequest(time, errors));
			Assert.IsTrue(errors.Count == 0);

			// time request Time
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.TimeToday;
			errors = new ArrayList();
			Assert.AreEqual(true, DBSValidation.ValidateTimeRequest(time, errors));
			Assert.IsTrue(errors.Count == 0);

			// time request Time = 23:59
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.TimeToday;
			time.Hour = 23;
			time.Minute = 59;
			errors = new ArrayList();
			Assert.AreEqual(true, DBSValidation.ValidateTimeRequest(time, errors));
			Assert.IsTrue(errors.Count == 0);

			// -----------------------

			// test invalid hour
			
			// time request Time = 24:59
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.TimeToday;
			time.Hour = 24;
			time.Minute = 59;
			errors = new ArrayList();
			Assert.AreEqual(false, DBSValidation.ValidateTimeRequest(time, errors));
			Assert.IsTrue(errors.Count != 0);

			// time request Time = -1:59
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.TimeToday;
			time.Hour = -1;
			time.Minute = 59;
			errors = new ArrayList();
			Assert.AreEqual(false, DBSValidation.ValidateTimeRequest(time, errors));
			Assert.IsTrue(errors.Count != 0);

			// test invalid minute
			// time request Time = 23:60
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.TimeToday;
			time.Hour = 23;
			time.Minute = 60;
			errors = new ArrayList();
			Assert.AreEqual(false, DBSValidation.ValidateTimeRequest(time, errors));
			Assert.IsTrue(errors.Count != 0);

			// time request Time = 23:-1
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.TimeToday;
			time.Hour = 23;
			time.Minute = -1;
			errors = new ArrayList();
			Assert.AreEqual(false, DBSValidation.ValidateTimeRequest(time, errors));
			Assert.IsTrue(errors.Count != 0);


			// -------------------------

			// Extra test for new Past time window functionality
			// note test time window set to 120 Min
			
			// time in the past but within 120 (extreme: 119 min) ==> TimeRequest should stay today
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.TimeToday;
			DateTime dtRequested = DateTime.Now.Add(new TimeSpan(0,-119, 0));
			time.Hour = dtRequested.Hour;
			time.Minute = dtRequested.Minute;
			
			Assert.IsTrue(DBSValidation.ValidateTimeRequest(time, errors));
			Assert.AreEqual(TimeRequestType.TimeToday, time.Type );

			// time in the past but within 120 (extreme: 1 min) ==> TimeRequest should stay today
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.TimeToday;
			dtRequested = DateTime.Now.Add(new TimeSpan(0,-1, 0));
			time.Hour = dtRequested.Hour;
			time.Minute = dtRequested.Minute;

			Assert.IsTrue(DBSValidation.ValidateTimeRequest(time, errors));
			Assert.AreEqual(TimeRequestType.TimeToday, time.Type );


			// time in the past but outside 120 (extreme: 121 min) ==> TimeRequest should change to tomorrow
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.TimeToday;
			dtRequested = DateTime.Now.Add(new TimeSpan(0,-121,0));
			time.Hour = dtRequested.Hour;
			time.Minute = dtRequested.Minute;

			Assert.IsTrue(DBSValidation.ValidateTimeRequest(time, errors));
			Assert.AreEqual(TimeRequestType.TimeTomorrow, time.Type );





		}
	}
}
