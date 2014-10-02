// ********************************************* 
// NAME			: CostSearchOperation.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 19/01/2005
// DESCRIPTION	: CostSearchOperation enumerator
// *********************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/CostSearchOperation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:20   mturner
//Initial revision.
//
//   Rev 1.0   Jan 19 2005 11:51:16   jmorrissey
//Initial revision.

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Enumeration for CostSearchOperation that indicates which method of 
	/// CostSearchRunner has been called
	/// </summary>
	public enum CostSearchOperation
	{	
		Fares,
		Services	
	}
}
