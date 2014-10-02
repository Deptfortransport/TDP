// *********************************************** 
// NAME                 : PageTransferDataCache.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : Defines an exception
// (inherits from TDException) that is thrown
// if any exceptions occur in ScreenFlow. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Classes/ScreenFlowException.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:47:48   mturner
//Initial revision.
//
//   Rev 1.4   Apr 23 2004 14:36:36   acaunt
//CSL Compliant attribute removed so that the Assembly can be marked as non CLS compliant. (All projects are to have an identical AssemblyInfo.cs file).
//
//   Rev 1.3   Oct 03 2003 13:38:50   PNorell
//Updated the new exception identifier.
//
//   Rev 1.2   Aug 07 2003 13:53:56   kcheung
//Set CLSComplaint to true
//
//   Rev 1.1   Jul 23 2003 12:28:06   kcheung
//Updated after code review comments

using System;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.ScreenFlow
{
	/// <summary>
	/// Defines an exception
	/// (inherits from TDException) that is thrown
	/// if any exceptions occur in ScreenFlow.
	/// </summary>
	[Serializable]
	public class ScreenFlowException : TDException
	{
		public ScreenFlowException(string message, bool logged, TDExceptionIdentifier id)
			: base(message, null, logged, id)
		{
		}

		public ScreenFlowException
			(string message, Exception innerException, bool logged, TDExceptionIdentifier id)
			: base(message, innerException, logged, id)
		{
		}
	}
}
