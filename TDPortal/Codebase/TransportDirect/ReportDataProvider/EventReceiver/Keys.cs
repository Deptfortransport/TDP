// *********************************************** 
// NAME                 : Keys.cs 
// AUTHOR               : Jatinder S. Toor
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  :  Holds Event Receiver specific key constants.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventReceiver/Keys.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:36   mturner
//Initial revision.
//
//   Rev 1.3   Jul 05 2004 11:20:46   passuied
//changes for EventReceiver Recovery
//Resolution for 1048: EventReceiver failure Del6
//
//   Rev 1.2   Nov 06 2003 19:54:12   geaton
//Removed redundant key.
//
//   Rev 1.1   Oct 10 2003 15:22:50   geaton
//Updated error handling and validation.
//
//   Rev 1.0   Aug 22 2003 11:49:40   jtoor
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.EventReceiver
{
	/// <summary>
	/// Container class used to hold Event Receiver specific key constants
	/// </summary>
	public class Keys
	{
		public const string ReceiverQueue		= "Receiver.Queue";
		public const string ReceiverQueuePath	= ReceiverQueue + ".{0}.Path";
		public const string TimeBeforeRecovery	= "EventReceiver.TimeBeforeRecovery";
	}
}
