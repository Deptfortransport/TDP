// ***********************************************
// NAME 		: TestDummySession.cs
// AUTHOR 		: Andrew Toner
// DATE CREATED : 26/09/2003
// DESCRIPTION 	: 
// ************************************************
//$log$
using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for TestDummySession.
	/// </summary>
	public class TestDummySession : ITDSession
	{
		private TypeSafeDictionary Dict = new TypeSafeDictionary();
		private string sessionID = string.Empty;		
		protected TDSessionPartition currentPartition;

		public TestDummySession( string SessionID )
		{
			sessionID = SessionID;
		}

		#region ITDSession interface

		/// <summary>
		/// This is a read/write property that adds data of type int32 to
		/// the ASP session data.
		/// </summary>
		/// <value>Int32</value>
		public int this[IntKey key]
		{
			get { return Dict[key]; }
			set { Dict[key] = value; }
		}
		
		/// <summary>
		/// This is a read/write property that adds data of type string to
		/// the ASP session data.
		/// </summary>
		/// <value>String</value>
		public string this[StringKey key]
		{
			get { return Dict[key]; }
			set { Dict[key] = value; }
		}
		
		/// <summary>
		/// This is a read/write property that adds data of type Double to
		/// the ASP session data.
		/// </summary>
		/// <value>double</value>
		public double this[DoubleKey key]
		{
			get { return Dict[key]; }
			set { Dict[key] = value; }
		}

		/// <summary>
		/// This is a read/write property that adds data of type DateTime to
		/// the ASP session data.
		/// </summary>
		/// <value>DateTime</value>
		public DateTime this[DateKey key]
		{
			get { return Dict[key]; }
			set { Dict[key] = value; }
		}

		/// <summary>
		/// This is a read/write property that adds data of type bool to
		/// the ASP session data.
		/// </summary>
		/// <value>bool</value>
		public bool this[BoolKey key]
		{
			get { return Dict[key]; }
			set { Dict[key] = value; }
		}

		/// <summary>
		/// This is a read/write property that adds data of type PageID to
		/// the ASP session data.
		/// </summary>
		/// <value>PageId</value>
		public PageId this[PageIdKey key]
		{
			get { return (PageId)Dict[key]; }
			set { Dict[key] = value; }
		}

		public string SessionID
		{
			get { return sessionID; }
			set { sessionID = value; }
		}

		public TDSessionPartition Partition
		{
			get 
			{
				return currentPartition; 
			}
			set 
			{ 
				currentPartition = value; 				
			}
		}
		#endregion

	}
}