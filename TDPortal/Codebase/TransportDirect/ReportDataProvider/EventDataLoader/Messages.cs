// *********************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 01/07/2004
// DESCRIPTION  : Messages used by the EventDataLoader
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventDataLoader/Messages.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:38:26   mturner
//Initial revision.
//
//   Rev 1.2   Jul 02 2004 10:07:10   jgeorge
//Updated to fix bugs highlighted by unit testing
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
	/// Summary description for Messages.
	/// </summary>
	public sealed class Messages
	{
		static Messages() { }

		public const string InvalidEventCategory = "The value [{0}] supplied for event category could not be parsed into a TDEventCategory value.";
		public const string InvalidTraceLevel = "The value [{0}] supplied for trace level could not be parsed into a TDTraceLevel value.";
	}
}
