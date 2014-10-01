// *********************************************** 
// NAME                 : CodeServiceCodeType.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/01/2006 
// DESCRIPTION  		: DTO implementation of TDCodeType enum. For Information refer to TDCodeType.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CodeHandler/V1/CodeServiceCodeType.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:14   mturner
//Initial revision.
//
//   Rev 1.2   May 14 2007 09:39:16   mturner
//NAPTAN added to list of code types
//
//   Rev 1.1   Jan 20 2006 16:25:30   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CodeHandler.V1
{
	/// <summary>
	/// DTO implementation of TDCodeType enum. For Information refer to TDCodeType.
	/// </summary>
	[System.Serializable]
	public enum CodeServiceCodeType
	{
		CRS,
		SMS,
		IATA,
		Postcode,
		NAPTAN
	}
}
