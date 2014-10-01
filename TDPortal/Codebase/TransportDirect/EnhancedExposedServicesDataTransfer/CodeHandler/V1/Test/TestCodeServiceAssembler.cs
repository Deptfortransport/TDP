// *********************************************** 
// NAME                 : TestCodeServiceAssembler.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 16/01/2006 
// DESCRIPTION  	    : Test class for Code Service Assember
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CodeHandler/V1/Test/TestCodeServiceAssembler.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:16   mturner
//Initial revision.
//
//   Rev 1.0   Jan 17 2006 10:16:48   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111


using System;
using NUnit.Framework;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;   
using TransportDirect.UserPortal.LocationService;
using TransportDirect.EnhancedExposedServices.DataTransfer.CodeHandler.V1;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CodeHandler.V1.Test
{
	/// <summary>
	/// Test class for Code Service Assember
	/// </summary>
	[TestFixture]
	public class TestCodeServiceAssembler
	{
		#region Test Methods
		/// <summary>
		/// Test for CreateCodeServiceCodeDetailArrayDT
		/// </summary>
		[Test]
		public void TestCreateCodeServiceCodeDetailArrayDT()
		{
			string code = "EUS";
			string description = "EUSTON STATION";
			string locatlity = "London";
			string region = locatlity; 
			TDCodeType codeType = TDCodeType.CRS;
			int easting = 11;
			int northing = 12;
			TDModeType modeType = TDModeType.Rail; 
			string naptanId = "9100EUSTON";

		   // creating the instance of domain object 
			TDCodeDetail[] tdCodeDetails = new TDCodeDetail[2];

			for (int i=0; i< tdCodeDetails.Length; i++ )
			{
				tdCodeDetails[i] = new TDCodeDetail(); 
				tdCodeDetails[i].Code = code;  			
				tdCodeDetails[i].CodeType = codeType;
				tdCodeDetails[i].Description  = description;
				tdCodeDetails[i].Easting = easting;
				tdCodeDetails[i].Locality  = locatlity;
				tdCodeDetails[i].ModeType  = modeType;
				tdCodeDetails[i].NaptanId  = naptanId;
				tdCodeDetails[i].Northing  = northing;
				tdCodeDetails[i].Region  = region;
			}

			// Getting DTO object from domain object
			CodeServiceCodeDetail[] codeServiceCodeDetails = CodeServiceAssembler.CreateCodeServiceCodeDetailArrayDT(tdCodeDetails);
  
			// now testing each params
			Assert.IsTrue(codeServiceCodeDetails.Length ==2);
			
			for (int j=0; j< codeServiceCodeDetails.Length; j++)
			{
				Assert.IsTrue(codeServiceCodeDetails[j].Code == code);
				Assert.IsTrue(codeServiceCodeDetails[j].CodeType  == CodeServiceCodeType.CRS);
				Assert.IsTrue(codeServiceCodeDetails[j].Description  == description);
				Assert.IsTrue(codeServiceCodeDetails[j].GridReference.Easting   == easting);
				Assert.IsTrue(codeServiceCodeDetails[j].GridReference.Northing   == northing);
				Assert.IsTrue(codeServiceCodeDetails[j].Locality == locatlity);
				Assert.IsTrue(codeServiceCodeDetails[j].ModeType  == CodeServiceModeType.Rail);
				Assert.IsTrue(codeServiceCodeDetails[j].NaptanId == naptanId);
				Assert.IsTrue(codeServiceCodeDetails[j].Region  == region);				

			}
		}

		/// <summary>
		/// Test for CreateTDModeTypeArray
		/// </summary>
		[Test]
		public void TestCreateTDModeTypeArray()
		{
		   // creating dto object 
			CodeServiceModeType[] codeServiceModeTypes = new CodeServiceModeType[3];
			codeServiceModeTypes[0] = CodeServiceModeType.Rail;
			codeServiceModeTypes[1] = CodeServiceModeType.Bus;
			codeServiceModeTypes[2] = CodeServiceModeType.Air;

			// creating domain object 
			TDModeType[] tdModeTypes = CodeServiceAssembler.CreateTDModeTypeArray(codeServiceModeTypes);
   
			Assert.IsTrue(tdModeTypes[0] == TDModeType.Rail);
			Assert.IsTrue(tdModeTypes[1] == TDModeType.Bus);
			Assert.IsTrue(tdModeTypes[2] == TDModeType.Air);
		}

		/// <summary>
		/// Test for CreateCodeServiceCodeTypeDT
		/// </summary>
		[Test]
		public void TestCreateCodeServiceCodeTypeDT()
		{
			// creating the instance of domain object 
			TDCodeType tdCodeType = TDCodeType.CRS;
			
			// creating domain object 
			CodeServiceCodeType codeServiceCodeType = CodeServiceAssembler.CreateCodeServiceCodeTypeDT(tdCodeType);   

			Assert.IsTrue(codeServiceCodeType == CodeServiceCodeType.CRS); 

		}


		/// <summary>
		/// Test for CreateCodeServiceModeTypeDT
		/// </summary>
		[Test]
		public void TestCreateCodeServiceModeTypeDT()
		{
			// creating domain object 
			TDModeType tdModeTypes = TDModeType.Rail;
						
			// creating domain object 
			CodeServiceModeType codeServiceModeType = CodeServiceAssembler.CreateCodeServiceModeTypeDT(tdModeTypes);
   
			Assert.IsTrue(codeServiceModeType == CodeServiceModeType.Rail);
			
		}
		#endregion
	}
}
