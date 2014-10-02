// *********************************************** 
// NAME			: RequestMapParams.cs
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
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionHelper/RequestMapParams.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:44   mturner
//Initial revision.
//
//   Rev 1.0   Jan 08 2004 19:41:40   PNorell
//Initial Revision

using System;
using System.IO;

namespace TransportDirect.ReportDataProvider.TransactionHelper
{
	/// <summary>
	/// Summary description for RequestMapParams.
	/// </summary>
	[Serializable()]
	public struct RequestMapParams
	{
		/// <summary>
		/// The type of map request to do
		/// </summary>
		public string type;

		/// <summary>
		/// A list of northing to randomise between, the list is separated by | characters
		/// </summary>
		public string northing;
		/// <summary>
		/// A list of easting to randomise between, the list is separated by | characters
		/// </summary>
		public string easting;
		/// <summary>
		/// A list of scale to randomise between, the list is separated by | characters
		/// </summary>
		public string scale;

		/// <summary>
		/// The session id used for a road route.
		/// </summary>
		public string session;
	}
}
