// *********************************************** 
// NAME                 : TestMockTDSessionManager.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 18/07/2003 
// DESCRIPTION  : Mock TDSessionManager object
// used for NUnit testing.  A mock object is used
// because it allows the ScreenFlow component
// to be tested without an actual web session in
// place.  This means that NUnit tests can be
// carried out.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Test/TestMockTDSessionManager.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:47:54   mturner
//Initial revision.
//
//   Rev 1.10   May 23 2005 10:03:02   rscott
//Updated for NUnit Tests
//
//   Rev 1.9   Sep 17 2003 16:18:42   PNorell
//Updated for easier maintenance.
//Now extends the TestMockSessionManager in SessionManager. This way it does not need updating as soon as SessionManager interface changes.
//
//   Rev 1.8   Sep 17 2003 11:38:24   cshillan
//First implementation of JourneyPlanRunner
//Design doc ref: DV/DD014 Data Capture - Validate and Run
//
//   Rev 1.7   Sep 12 2003 10:06:16   kcheung
//Updated to make it compile!
//
//   Rev 1.6   Sep 11 2003 12:09:34   cshillan
//Addition of ValidationError array
//
//   Rev 1.5   Sep 09 2003 14:15:56   passuied
//make the solution compile!
//
//   Rev 1.4   Aug 27 2003 16:53:18   kcheung
//Added ViewState to comply with the updated interface.
//
//   Rev 1.3   Aug 26 2003 10:03:26   passuied
//update
//
//   Rev 1.2   Jul 23 2003 13:28:34   kcheung
//Changed $log to $Log

using System;
using System.Collections;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.JourneyControl;


namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Mock TDSessionManager used for NUnit testing.
	/// </summary>
	[CLSCompliant(false)]		
	public class TestMockTDSessionManager : TransportDirect.UserPortal.SessionManager.TestMockSessionManager
	{
		public TestMockTDSessionManager() : base( false, "Hello I am a Session ID", true)
		{
		}

		public new string SessionID
		{
			get
			{
				return "Hello I am a Session ID";
			}
		}
	}
}
