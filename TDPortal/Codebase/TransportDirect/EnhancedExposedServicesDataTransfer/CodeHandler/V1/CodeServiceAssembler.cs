// *********************************************** 
// NAME                 : CodeServiceAssembler.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 12/01/2006 
// DESCRIPTION  		: Code Service Assembler class for converting domain types to exposed types.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CodeHandler/V1/CodeServiceAssembler.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:14   mturner
//Initial revision.
//
//   Rev 1.2   Jan 20 2006 16:25:30   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 17 2006 14:52:20   schand
//Added some comments
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 13 2006 15:32:18   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111

using System;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;   
using TransportDirect.UserPortal.LocationService;
 

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CodeHandler.V1
{
	/// <summary>
	/// Code Service Assembler class for converting domain types to exposed types.
	/// </summary>
	public sealed class CodeServiceAssembler
	{
		#region Public Methods
		/// <summary>
		/// This method translate an array TDCodeDetail(domain object) to an array of CodeServiceCodeDetail (DTO object)
		/// </summary>
		/// <param name="tdCodeDetails"></param>
		/// <returns>An array CodeServiceCodeDetail objects</returns>
		public static CodeServiceCodeDetail[] CreateCodeServiceCodeDetailArrayDT(TDCodeDetail[] tdCodeDetails )
		{
			if (tdCodeDetails.Length ==0)
				return new CodeServiceCodeDetail[0];

			int objectCount=0;
			CodeServiceCodeDetail[] codeServiceCodeDetail = new CodeServiceCodeDetail[tdCodeDetails.Length];
			foreach (TDCodeDetail tdCodeDetail in tdCodeDetails)
			{	
				codeServiceCodeDetail[objectCount] = new CodeServiceCodeDetail(); 
				if (tdCodeDetail==null)
				{
					codeServiceCodeDetail[objectCount]  = null;					
				}
				else
				{

					codeServiceCodeDetail[objectCount].Code  = tdCodeDetail.Code;
					codeServiceCodeDetail[objectCount].CodeType   = CreateCodeServiceCodeTypeDT(tdCodeDetail.CodeType);
					codeServiceCodeDetail[objectCount].Description   = tdCodeDetail.Description;
					codeServiceCodeDetail[objectCount].GridReference  = CommonAssembler.CreateOSGridReferenceDT(tdCodeDetail.Easting, tdCodeDetail.Northing);
					codeServiceCodeDetail[objectCount].Locality = tdCodeDetail.Locality;
					codeServiceCodeDetail[objectCount].ModeType  = CreateCodeServiceModeTypeDT(tdCodeDetail.ModeType);
					codeServiceCodeDetail[objectCount].NaptanId  = tdCodeDetail.NaptanId;
					codeServiceCodeDetail[objectCount].Region   = tdCodeDetail.Region;
				}
				objectCount++;
			}
			return codeServiceCodeDetail;
		}

	
		/// <summary>
		/// This method translate an array of CodeServiceModeType (DTO) to an array TDModeType(domain object)(DTO object)
		/// </summary>
		/// <param name="codeServiceModeTypes"></param>
		/// <returns></returns>
		public static TDModeType[] CreateTDModeTypeArray(CodeServiceModeType[] codeServiceModeTypes)
		{
			if (codeServiceModeTypes.Length ==0)
				return new TDModeType[0];

			int objectCount=0;
			TDModeType[] tdModeTypes = new TDModeType[codeServiceModeTypes.Length];
			foreach (CodeServiceModeType codeServiceModeType in codeServiceModeTypes)
			{
				 tdModeTypes[objectCount] = (TDModeType) Enum.Parse(typeof(TDModeType), codeServiceModeType.ToString(),true);   
				 objectCount++;
			}
			return tdModeTypes;
		}
		
		/// <summary>
		/// This method translate TDCodeType(domain object) to CodeServiceCodeType (DTO object)
		/// </summary>
		/// <param name="tdCodeType">TDCodeType Domain object</param>
		/// <returns>CodeServiceCodeType DTO object</returns>
		public static CodeServiceCodeType CreateCodeServiceCodeTypeDT(TDCodeType tdCodeType)
		{
			return (CodeServiceCodeType) Enum.Parse(typeof(CodeServiceCodeType), tdCodeType.ToString(),true);   
		}

		/// <summary>
		/// This method translate TDModeType(domain object) to CodeServiceModeType (DTO object)		
		/// </summary>
		/// <param name="tdModeType"></param>
		/// <returns></returns>
		public static CodeServiceModeType CreateCodeServiceModeTypeDT(TDModeType tdModeType)
		{
			return (CodeServiceModeType) Enum.Parse(typeof(CodeServiceModeType), tdModeType.ToString(),true);   
		}
		#endregion
	}

}
