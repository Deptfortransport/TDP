// ********************************************************************* 
// NAME                 : IAdditionalData.cs 
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 16/10/2003
// DESCRIPTION			: Implementation of IAdditionalData
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/IAdditionalData.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:18:08   mturner
//Initial revision.
//
//   Rev 1.5   Jan 12 2005 16:58:26   RScott
//updated to accomodate new method LookupStationNameForCRS()
//
//   Rev 1.4   Jan 11 2005 15:21:38   RScott
//includes new method LookupNaptanForCode
//
//   Rev 1.3   Nov 07 2003 16:29:28   RPhilpott
//Changes to accomodate removal of station name lookup by Atkins.
//
//   Rev 1.2   Nov 07 2003 14:07:40   RPhilpott
//Add TrainTaxiLink support
//
//   Rev 1.1   Nov 05 2003 19:16:16   RPhilpott
//Add CRS and Station Name convenience methods
//
//   Rev 1.0   Oct 16 2003 20:52:38   acaunt
//Initial Revision
using System;
using System.Data;

namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Summary description for IAdditionalData.
	/// </summary>
	public interface IAdditionalData
	{

		/// <summary>
		/// Performs a lookup against the additional data module using a NaPTAN and type key
		/// to return the single corresponding value
		/// </summary>
		/// <param name="type"></param>
		/// <param name="naptan"></param>
		/// <returns></returns>
		string LookupFromNaPTAN(LookupType type, String naptan);

		/// <summary>
		/// Performs a lookup against the additional data module using a NaPTAN.
		/// This results all the values associated with the NaPTAN
		/// </summary>
		LookupResult[] LookupFromNaPTAN(String naptan);

		/// <summary>
		/// Convenience method to get CRS code corresponding to specified NAPTAN
		/// </summary>
		/// <param name="naptan">NAPTAN</param>
		/// <returns>CRS code</returns>
		string LookupCrsForNaptan(String naptan);

		/// <summary>
		/// Convenience method to get NLC code corresponding to specified NAPTAN
		/// </summary>
		/// <param name="naptan">NAPTAN</param>
		/// <returns>NLC code</returns>
		string LookupNlcForNaptan(String naptan);

		/// <summary>
		/// Convenience method to get NAPTAN code corresponding to specified NLC or CRS code
		/// </summary>
		/// <param name="naptan">NAPTAN</param>
		/// <returns>NAPTAN codes string array</returns>
		string[] LookupNaptanForCode(String code, LookupType type);

		/// <summary>
		/// Get station name corresponding to specified NAPTAN
		/// </summary>
		/// <param name="naptan">NAPTAN</param>
		/// <returns>station name</returns>
		string LookupStationNameForNaptan(String naptan);

		/// <summary>
		/// Get station name corresponding to specified CRS Code
		/// </summary>
		/// <param name="code">CRS Code</param>
		/// <returns>station name</returns>
		string LookupStationNameForCRS(String code);

		/// <summary>
		/// Retrieve DataSet of TrainTaxi information for specified NAPTAN
		/// </summary>
		/// <param name="naptan">NAPTAN</param>
		/// <returns>DataSet of TrainTaxi records</returns>
		DataSet RetrieveTrainTaxiInfoForNaptan(String naptan);

	}
}
