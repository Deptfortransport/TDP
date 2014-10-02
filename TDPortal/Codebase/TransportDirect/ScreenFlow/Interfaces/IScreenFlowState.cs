// *********************************************** 
// NAME                 : IPageTransferDataCache.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : Interface that defines the
// DoTransition method.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Interfaces/IScreenFlowState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:47:50   mturner
//Initial revision.
//
//   Rev 1.3   Apr 23 2004 14:36:34   acaunt
//CSL Compliant attribute removed so that the Assembly can be marked as non CLS compliant. (All projects are to have an identical AssemblyInfo.cs file).
//
//   Rev 1.2   Aug 07 2003 13:54:10   kcheung
//Set CLSComplaint to true
//
//   Rev 1.1   Jul 23 2003 12:28:28   kcheung
//Updated after code review comments

using System;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.ScreenFlow.State
{
	/// This Interface provides the DoTransition of the PageId
	
	public interface IScreenFlowState
	{
		PageId DoTransition();
	}
}
