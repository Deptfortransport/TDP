// *********************************************** 
// NAME                 : IOperationalEventLoader.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 01/07/2004
// DESCRIPTION  : Interface for classes that load 
// operational events from sinks
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventDataLoader/IOperationalEventLoader.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:38:26   mturner
//Initial revision.
//
//   Rev 1.4   Jul 12 2004 10:34:10   jgeorge
//Modifications to the way filtering works
//
//   Rev 1.3   Jul 02 2004 10:07:08   jgeorge
//Updated to fix bugs highlighted by unit testing
//
//   Rev 1.2   Jul 01 2004 17:15:46   jgeorge
//Interim check-in
//
//   Rev 1.1   Jul 01 2004 15:58:02   jgeorge
//Work in progress
//
//   Rev 1.0   Jul 01 2004 14:58:56   jgeorge
//Initial revision.

using System;

namespace TransportDirect.ReportDataProvider.EventDataLoader
{

	/// <summary>
	/// Summary description for OperationalEventLoader.
	/// </summary>
	public interface IOperationalEventLoader
	{
		/// <summary>
		/// Returns all operational events in the source
		/// </summary>
		LoadedOperationalEvent[] GetEvents();

		/// <summary>
		/// Returns operational events from the source that meet the specified criteria
		/// </summary>
		LoadedOperationalEvent[] GetEvents(OperationalEventFilter filter);
	}
}
