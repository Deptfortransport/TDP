// ***********************************************
// NAME 		: TDSession.cs
// AUTHOR 		: Andrew Windley
// DATE CREATED : 02/07/2003
// DESCRIPTION 	: A number of indexers that ensure any 
// data saved to the session is done so in a type safe manner.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDSession.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:42   mturner
//Initial revision.
//
//   Rev 1.10   Sep 29 2005 17:39:00   build
//Automatically merged from branch for stream2610
//
//   Rev 1.9.1.0   Sep 14 2005 09:29:20   halkatib
//Bug fix suggested by Peter Norell to allow Landing Page to work.
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.9   Apr 06 2005 16:32:18   PNorell
//Updated for FX Cops.
//
//   Rev 1.8   Jan 31 2005 16:50:18   PNorell
//Changes for SessionManager to include support for getting a sessionmanager for opposing partition.
//Also updated cost based check.
//
//   Rev 1.7   Jan 26 2005 15:53:10   PNorell
//Support for partitioning the session information.
//
//   Rev 1.6   Mar 10 2004 15:53:16   PNorell
//Updated for IR498 to be included in 5.2
//
//   Rev 1.5   Sep 11 2003 11:04:24   cshillan
//Included SessionID property with GET accessor
//
//   Rev 1.4   Jul 17 2003 15:35:46   mturner
//Added documentation comments after code review
//
//   Rev 1.3   Jul 17 2003 13:45:18   kcheung
//Added BoolKey
//
//   Rev 1.2   Jul 17 2003 13:28:26   kcheung
//Added PageId
//
//   Rev 1.1   Jul 07 2003 17:29:28   mturner
//Changed the indexers so that they use key.ID rather than key 
//
//   Rev 1.0   Jul 03 2003 17:31:26   AWindley
//Initial Revision

using System;
using System.Web;
using System.Web.SessionState;
using TransportDirect.Common;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.SessionManager
{	
	/// <summary>
	/// TDSession forwards all calls to System.Web.Session
	/// </summary>
	public class TDSession : ITDSession
	{
		#region Public constants/readonly definitions
		private const string PARTITIONID = ":partition:";

		#endregion
		
		#region Private variables
		#endregion

		#region Partition details and helper methods
		public virtual TDSessionPartition Partition
		{
			get {
				TDSessionPartition currentPartition = TDSessionPartition.TimeBased;
				object partition = HttpContext.Current.Session[PARTITIONID];
				if( partition != null )
				{
					currentPartition = (TDSessionPartition)partition;
				}

				return currentPartition; 
			}
			set 
			{ 
				TDSessionPartition currentPartition = value; 
				if( currentPartition == TDSessionPartition.TimeBased )
				{
					HttpContext.Current.Session.Remove(PARTITIONID);
				}
				else
				{
					HttpContext.Current.Session[PARTITIONID] = value;
				}
			}
		}

		private string GetFullID(IKey key)
		{
			if( key is ITDSessionPartionable )
			{
				return ((int)Partition) + key.ID;
			}
			else
			{
				return key.ID;
			}
		}
		#endregion

		#region ITDSession definitions
		// The following get methods take an instance of the appropriate Key,
		// they then attempt to find this key's ID property in the System.Web.Session data. 
		// A succesfully matched entry's value is returned to the calling class.
		// The set methods place a value into the session data as a string, along with the 
		// ID property of the given key. 
		
		/// <summary>
		/// This is a read/write property that adds data of type int32 to
		/// the ASP session data.
		/// </summary>
		/// <value>Int32</value>
 		public int this[IntKey key]
		{
			get
			{
				return (int)HttpContext.Current.Session[GetFullID(key)];
			}
			set
			{
				HttpContext.Current.Session[GetFullID(key)] = value;
			}
		}
		
		/// <summary>
		/// This is a read/write property that adds data of type string to
		/// the ASP session data.
		/// </summary>
		/// <value>String</value>
		public string this[StringKey key]
		{
			get
			{
				return (string)HttpContext.Current.Session[GetFullID(key)];
			}
			set
			{
				HttpContext.Current.Session[GetFullID(key)] = value;
			}
		}
		
		/// <summary>
		/// This is a read/write property that adds data of type Double to
		/// the ASP session data.
		/// </summary>
		/// <value>double</value>
		public double this[DoubleKey key]
		{
			get
			{
				return (double)HttpContext.Current.Session[GetFullID(key)];
			}
			set
			{
				HttpContext.Current.Session[GetFullID(key)] = value;
			}
		}

		/// <summary>
		/// This is a read/write property that adds data of type DateTime to
		/// the ASP session data.
		/// </summary>
		/// <value>DateTime</value>
		public DateTime this[DateKey key]
		{
			get
			{
				return (DateTime)HttpContext.Current.Session[GetFullID(key)];
			}
			set
			{
				HttpContext.Current.Session[GetFullID(key)] = value;
			}
		}

		/// <summary>
		/// This is a read/write property that adds data of type bool to
		/// the ASP session data.
		/// </summary>
		/// <value>bool</value>
		public bool this[BoolKey key]
		{
			get
			{
				if( HttpContext.Current.Session[GetFullID(key)] == null )
				{
					return false;
				}
				return (bool)HttpContext.Current.Session[GetFullID(key)];
			}
			set
			{
				HttpContext.Current.Session[GetFullID(key)] = value;
			}
		}

		/// <summary>
		/// This is a read/write property that adds data of type PageID to
		/// the ASP session data.
		/// </summary>
		/// <value>PageId</value>
		public PageId this[PageIdKey key]
		{
			get
			{
				return (PageId)HttpContext.Current.Session[GetFullID(key)];
			}
			set
			{
				HttpContext.Current.Session[GetFullID(key)] = value;
			}
		}



		/// <summary>
		/// This is the string representation of the Session ID
		/// </summary>
		public string SessionID
		{
			get { return HttpContext.Current.Session.SessionID; }
		}

		#endregion


	}


	public class TDSessionLockedPartition : TDSession
	{
		private TDSessionPartition currentPartition = TDSessionPartition.TimeBased;

		public TDSessionLockedPartition( TDSessionPartition partition )
		{
			currentPartition = partition;
		}

		public override TDSessionPartition Partition
		{
			get
			{
				return currentPartition;
			}
			set
			{
				// Do nothing
			}
		}
	}
}


