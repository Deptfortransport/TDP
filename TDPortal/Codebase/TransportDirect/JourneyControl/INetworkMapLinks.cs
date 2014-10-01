// *********************************************** 
// NAME			: INetworkMapLinks.cs
// AUTHOR		: Paul Cross
// DATE CREATED	: 08/07/2005
// DESCRIPTION	: Interface definition for the NetworkMapLinks class
//				  which wraps access to network links information.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/INetworkMapLinks.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:42   mturner
//Initial revision.
//
//   Rev 1.0   Jul 12 2005 10:45:46   pcross
//Initial revision.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results

using System;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Interface for objects handling network map links.
	/// </summary>
	public interface INetworkMapLinks
	{
		string GetURL(ModeType mode, string operatorCode);
	}
}
