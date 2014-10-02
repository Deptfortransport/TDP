// *********************************************** 
// NAME                 : TestMockTDSession.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 21/07/2003 
// DESCRIPTION  : Mock TDSession used for NUnit
// Testing.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Test/TestMockTDSession.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:47:54   mturner
//Initial revision.
//
//   Rev 1.4   Sep 12 2003 10:06:20   kcheung
//Updated to make it compile!
//
//   Rev 1.3   Sep 11 2003 12:17:54   passuied
//update to make it compile
//
//   Rev 1.2   Sep 11 2003 12:13:38   passuied
//update to implement the interface members
//
//   Rev 1.1   Jul 23 2003 13:28:32   kcheung
//Changed $log to $Log

using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.UserPortal.Resource;


namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Dummy TDSession class.  The "real" TDSession that exists
	/// in SessionManager is not used becuase it requires a web
	/// context.  This will mean that it makes it very difficult
	/// to unit test td.ScreenFlow independently.
	/// This will behave exactly like the real TDSession
	/// except that a hashtable replaces the Session object.
	/// </summary>
	public class TestMockTDSession : ITDSession
	{
		Hashtable session = new Hashtable();
		
		/// Get/Set Session data
		/// </summary>
		/// <value>Int32</value>
		public int this[IntKey key]
		{
			get
			{
				return (int)session[key.ID];
			}
			set
			{
				session[key.ID] = value;
			}
		}
		
		/// <summary>
		/// Get/Set Session data
		/// </summary>
		/// <value>String</value>
		public string this[StringKey key]
		{
			get
			{
				return (string)session[key.ID];
			}
			set
			{
				session[key.ID] = value;
			}
		}
		
		/// <summary>
		/// Get/Set Session data
		/// </summary>
		/// <value>double</value>
		public double this[DoubleKey key]
		{
			get
			{
				return (double)session[key.ID];
			}
			set
			{
				session[key.ID] = value;
			}
		}

		/// <summary>
		/// Get/Set Session data
		/// </summary>
		/// <value>DateTime</value>
		public DateTime this[DateKey key]
		{
			get
			{
				return (DateTime)session[key.ID];
			}
			set
			{
				session[key.ID] = value;
			}
		}

		/// <summary>
		/// Get/Set Session data
		/// </summary>
		/// <value>bool</value>
		public bool this[BoolKey key]
		{
			get
			{
				return (bool)session[key.ID];
			}
			set
			{
				session[key.ID] = value;
			}
		}

		/// <summary>
		/// Get/Set Session data
		/// </summary>
		/// <value>PageId</value>
		public PageId this[PageIdKey key]
		{
			get
			{
				return (PageId)session[key.ID];
			}
			set
			{
				session[key.ID] = value;
			}
		}
		/// <summary>
		/// This is the string representation of the Session ID
		/// </summary>
		public string SessionID
		{
			get { return "Hello I am a session ID"; }
		}


	}
}
