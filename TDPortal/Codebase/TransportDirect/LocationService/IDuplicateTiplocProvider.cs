// ***********************************************
// NAME 		: IDuplicateTiplocProvider.cs
// AUTHOR 		: Amit Patel
// DATE CREATED : 30-Jun-2010
// DESCRIPTION 	: DuplicateTiplocProvider interface
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/IDuplicateTiplocProvider.cs-arc  $
//
//   Rev 1.0   Jul 01 2010 12:49:04   apatel
//Initial revision.
//Resolution for 5565: Departure board stop service page fails for stations with 2 Tiplocs or 2 CRS code

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// Interface for DuplicateTipLocProvider
    /// </summary>
    public interface IDuplicateTiplocProvider
    {
        /// <summary>
        /// Checks if the naptan specified is in each duplicate tip loc comma delimited string
        /// if the matching string found the method splits the strings into an array an returns as
        /// out parameter and returns true.
        /// if matching string not exist returs false
        /// </summary>
        /// <param name="naptan">Naptan of the tiploc for which duplicate tiploc needs checking</param>
        /// <param name="duplicateTiplocsData">Out parameter returning duplicate tiploc data in the event of naptan specified matches</param>
        /// <returns>True if specified naptan exist in one of the duplicate tiploc data</returns>
        bool HasDuplicateTiploc(string naptan, out string[] duplicatTiplocs);
    }
}
