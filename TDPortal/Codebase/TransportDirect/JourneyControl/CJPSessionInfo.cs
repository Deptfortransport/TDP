// *********************************************************** 
// NAME			: CJPSessionInfo.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 10/03/2005
// DESCRIPTION	: Definition of the CJPSessionInfo class - 
// moved from the CostSearch project
// *********************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/CJPSessionInfo.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:40   mturner
//Initial revision.
//
//   Rev 1.1   Jul 05 2005 13:52:36   asinclair
//Merge for stream2557
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.0.1.0   Jun 17 2005 09:51:56   jgeorge
//Added OriginAppDomainFriendlyName property
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.0   Mar 10 2005 16:55:18   jmorrissey
//Initial revision.

using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for CJPSessionInfo.
	/// </summary>
	[Serializable]
	public class CJPSessionInfo
	{
		private string sessionIdentifier;
		private bool isUserLoggedOn;
		private int thisUserType;
		private string lang;
		private string originAppDomainFriendlyName;

		public CJPSessionInfo()
		{
			//
			//
		}

		/// <summary>
		/// Holds sessionId value
		/// </summary>
		public string SessionId
		{
			get 
			{
				return sessionIdentifier;
			}
			set
			{
				sessionIdentifier = value;
			}
		}

		/// <summary>
		/// Is user logged on to the portal?
		/// </summary>
		public bool IsLoggedOn
		{
			get 
			{
				return isUserLoggedOn;
			}		
			set
			{
				isUserLoggedOn = value;
			}
		}

		/// <summary>
		/// The type of user
		/// 0 - No special privileges
		/// 1 - CJP will force all operational events to be logge 
		/// </summary>
		public int UserType
		{
			get 
			{
				return thisUserType;
			}
			set
			{
				thisUserType = value;
			}
		}

		/// <summary>
		/// Current language
		/// </summary>
		public string Language
		{
			get 
			{
				return lang;
			}
			set
			{
				lang = value;
			}
		}

		/// <summary>
		/// The AppDomain.FriendlyName property from the AppDomain where the value
		/// in the SessionId property originated.
		/// </summary>
		public string OriginAppDomainFriendlyName
		{
			get 
			{
				return originAppDomainFriendlyName; 
			}
			set 
			{ 
				originAppDomainFriendlyName = value; 
			}
		}
	}
}
