// *********************************************** 
// NAME			: RequestGisQueryParams.cs
// AUTHOR		: Peter Norell
// DATE CREATED	: 07/01/2004 
// DESCRIPTION	: Provides conversion methods to
// convert Transaction Injector parameters between
// string format and binary format. This is necessary
// in order to ensure parameters are passed to 
// TD Transaction Web Service correctly.
// (Data loss was encountered when passing the 
// TDJourneyRequest class as XML to web service.)
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionHelper/RequestGisQueryParams.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:42   mturner
//Initial revision.
//
//   Rev 1.0   Jan 08 2004 19:41:38   PNorell
//Initial Revision

using System;
using System.IO;


namespace TransportDirect.ReportDataProvider.TransactionHelper
{
	/// <summary>
	/// Summary description for RequestGisQueryParams.
	/// </summary>
	[Serializable()]
	public struct RequestGisQueryParams		
	{
		/// <summary>
		/// The type of GIS query to do
		/// </summary>
		public string type;

		/// <summary>
		/// A list of northings to randomise between, the list is separated by | characters
		/// </summary>
		public string northing;

		/// <summary>
		/// A list of easting to randomise between, the list is separated by | characters
		/// </summary>
		public string easting;
	}
}
