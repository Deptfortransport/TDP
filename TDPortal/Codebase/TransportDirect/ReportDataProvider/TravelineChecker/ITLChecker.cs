// *********************************************** 
// NAME                 : ITLChecker.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 15/11/2004 
// DESCRIPTION  : Interface for TLChecker classes. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TravelineChecker/ITLChecker.cs-arc  $ 
//
//   Rev 1.1   Mar 16 2009 12:24:06   build
//Automatically merged from branch for stream5215
//
//   Rev 1.0.1.0   Feb 19 2009 15:43:10   mturner
//Changes to implement via proxy server functionality.
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:40:44   mturner
//Initial revision.
//
//   Rev 1.1   Nov 17 2004 11:26:22   passuied
//First working version
//
//   Rev 1.0   Nov 15 2004 17:32:44   passuied
//Initial revision.


using System;

using TransportDirect.ReportDataProvider.TransactionHelper;

namespace TransportDirect.ReportDataProvider.TravelineChecker
{
	/// <summary>
	/// Interface for TLChecker classes. 
	/// </summary>
	public interface ITLChecker
	{

		/// <summary>
		/// Read-Write Property.JourneyWeb request to use for traveline checking
		/// </summary>
		TLJourneyWebRequest JourneyWebRequest { get; set;}
		
		/// <summary>
		/// Read-Write Property. Url of the traveline to test.
		/// </summary>
		string TravelineUrl{ get; set;}

        /// <summary>
        /// Read-Write Property. Url of the proxy server to be used if UseProxy is set to true.
        /// </summary>
        string  ProxyUrl{ get; set;}

        /// <summary>
        /// Read-Write Property. Flag to indicate whether to use proxy server.
        /// </summary>
        bool  UseProxy{ get; set;}
		
		/// <summary>
		/// Check traveline and returns number of returned journeys
		/// </summary>
		/// <returns>returns the number of returned journeys</returns>
		int Check();
	}
}
