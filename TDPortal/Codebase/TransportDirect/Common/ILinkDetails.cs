// *********************************************** 
// NAME                 : ILinkDetails.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 17/08/2005 
// DESCRIPTION			: Definition of the Link Details interface. Defines properties for all
//						  types of link (internal and external) used in the portal)
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/ILinkDetails.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:19:02   mturner
//Initial revision.
//
//   Rev 1.2   Feb 16 2006 15:54:32   build
//Automatically merged from branch for stream0002
//
//   Rev 1.1.1.0   Dec 16 2005 11:47:52   kjosling
//Modified interface so that properties are read-only
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.1   Aug 31 2005 14:26:42   kjosling
//Updated following code review
//
//   Rev 1.0   Aug 19 2005 16:26:12   kjosling
//Initial revision.

using System;

namespace TransportDirect.Common
{
	/// <summary>
	/// Specifies the interface properties and methods for an internal or external link
	/// </summary>
	public interface ILinkDetails
	{
		/// <summary>
		/// (Read-only) Returns the link URL
		/// </summary>
		string Url
		{
			get; 
		}

		/// <summary>
		/// (Read-only) Returns true if the link is valid, false if otherwise
		/// </summary>
		bool IsValid
		{
			get; 
		}
	}
}
