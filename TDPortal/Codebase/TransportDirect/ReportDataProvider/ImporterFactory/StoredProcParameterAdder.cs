// *********************************************** 
// NAME                 : StoredProcParameterAdder.cs 
// AUTHOR               : Andy Lole
// DATE CREATED         : 01/10/2003 
// DESCRIPTION  : Static Class to add parameters for specific events to their respective Import Stored Procs.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ImporterFactory/StoredProcParameterAdder.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:48   mturner
//Initial revision.
//
//   Rev 1.1   Jul 15 2004 18:04:02   acaunt
//TransferJourneyProcessingEvents now longer requires the time window passed in as a parameters
//
//   Rev 1.0   Nov 26 2003 11:00:38   geaton
//Initial Revision

using System;
using System.Collections;

namespace TransportDirect.ReportDataProvider.ImporterFactory
{
	/// <summary>
	/// Class that provides methods to return paramaters for input to stored procedures.
	/// </summary>
	public class StoredProcParameterAdder
	{
		/// <summary>
		/// Standard parameter adder for use by majority of import stored procedures.
		/// </summary>
		public static Hashtable StandardParamAdder(string dateValue, int CJPWebRequestWindow)
		{
			Hashtable parameters = new Hashtable(1);
			parameters.Add( "@Date", dateValue );

			return parameters;
		}
	}
}
