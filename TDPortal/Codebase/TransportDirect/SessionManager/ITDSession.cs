// ***********************************************
// NAME 		: ITDSession.cs
// AUTHOR 		: Mark Turner
// DATE CREATED : 02/07/2003
// DESCRIPTION 	: An interface detailing the indexers 
// that need to be provided by the TDSession class
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/ITDSession.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:30   mturner
//Initial revision.
//
//   Rev 1.4   Sep 11 2003 11:04:24   cshillan
//Included SessionID property with GET accessor
//
//   Rev 1.3   Jul 17 2003 13:44:54   kcheung
//Added BoolKey
//
//   Rev 1.2   Jul 17 2003 13:35:54   MTurner
//Added documentation comments after code review.
//
//   Rev 1.1   Jul 17 2003 13:27:58   kcheung
//Add PageId to the interface
//
//   Rev 1.0   Jul 03 2003 17:31:24   AWindley
//Initial Revision

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// The ITDSession interface is implemented by the TDSession class located
	/// in TDSession.cs
	/// </summary>
	
	public interface ITDSession 
	{
		/// <summary>
		/// Get/Set Session data.  Read/Write Access.
		/// </summary>
		/// <value>Int32</value>
		int this[IntKey key]{get;set;}
		
		/// <summary>
		/// Get/Set Session data.  Read/Write Access.
		/// </summary>
		/// <value>String</value>
		string this[StringKey key]{get;set;}
		
		/// <summary>
		/// Get/Set Session data.  Read/Write Access.
		/// </summary>
		/// <value>Double</value>
		double this[DoubleKey key]{get;set;}
		
		/// <summary>
		/// Get/Set Session data.  Read/Write Access.
		/// </summary>
		/// <value>DateTime</value>
		DateTime this[DateKey key]{get; set;}

		/// <summary>
		/// Get/Set Session data.  Read/Write Access.
		/// </summary>
		/// <value>bool</value>
		bool this[BoolKey key]{get; set;}

		/// <summary>
		/// Get/Set Session data.  Read/Write Access.
		/// </summary>
		/// <value>PageId</value>
		PageId this[PageIdKey key]{get; set;}

		/// <summary>
		/// Get Session ID.  Read Access.
		/// </summary>
		string SessionID { get; }
	}
}
