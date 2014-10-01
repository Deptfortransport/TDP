// *********************************************** 
// NAME         : TestMockAsyncCallState.cs
// AUTHOR       : Jonathan George
// DATE CREATED : 10/10/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/TestMockAsyncCallState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:19:08   mturner
//Initial revision.
//
//   Rev 1.1   Apr 05 2006 15:43:04   build
//Automatically merged from branch for stream0030
//
//   Rev 1.0.1.0   Mar 29 2006 11:10:34   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.0   Oct 21 2005 18:29:30   jgeorge
//Initial revision.

using System;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Concrete subclass of AsyncCallState that can be used for testing purposes
	/// in the Sessionmanager assembly.
	/// </summary>
	public class TestMockAsyncCallState : AsyncCallState
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestMockAsyncCallState() : base(30)
		{
		}

		public override PageId ProcessState()
		{
			switch (this.Status)
			{
				case AsyncCallStatus.None:
				case AsyncCallStatus.TimedOut:
				case AsyncCallStatus.NoResults:		
					return this.ErrorPage;
				case AsyncCallStatus.ValidationError:
					return this.AmbiguityPage;
				case AsyncCallStatus.CompletedOK:
					return this.DestinationPage;
				case AsyncCallStatus.InProgress:
				default:
					return PageId.WaitPage;
			}
		}
	}
}
