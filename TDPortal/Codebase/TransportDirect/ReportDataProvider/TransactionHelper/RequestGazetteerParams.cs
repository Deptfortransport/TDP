// *********************************************** 
// NAME			: RequestGazetteerParams.cs
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
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionHelper/RequestGazetteerParams.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:42   mturner
//Initial revision.
//
//   Rev 1.0   Jan 08 2004 19:41:36   PNorell
//Initial Revision

using System;
using System.IO;

using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.ReportDataProvider.TransactionHelper
{
	/// <summary>
	/// Summary description for RequestGazetteerParams.
	/// </summary>
	[Serializable()]
	public struct RequestGazetteerParams
	{
		/// <summary>
		/// The search text to use
		/// </summary>
		public string searchText;

		/// <summary>
		/// If the search should be fuzzy or not
		/// </summary>
		public bool fuzzy; 
		/// <summary>
		/// What type of search should be used
		/// </summary>
		public SearchType searchType;

		/// <summary>
		/// If the result should contain a picklist greater than 1
		/// </summary>
		public bool picklist;

		/// <summary>
		/// If the result should be drilled down rather than get location details.
		/// </summary>
		public bool drilldown;
	}
}
