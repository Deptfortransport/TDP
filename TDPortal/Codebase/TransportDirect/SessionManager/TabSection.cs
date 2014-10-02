// ***********************************************
// NAME         : TabSection.cs
// AUTHOR       : Tim Mollart
// DATE CREATED : 11/01/2006
// DESCRIPTION  : Contains enumeration for tab sections
//				  used within the portal. 
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TabSection.cs-arc  $
//
//   Rev 1.1   Mar 10 2008 15:27:12   mturner
//Initial Del10 Codebase from Dev Factory
//
//  Rev devfactory Feb 07 2008 12:12:13 dsawe
//  Chaged the home page buttons look after white labelling change & included tab LoginRegister
//
//   Rev 1.0   Nov 08 2007 12:48:38   mturner
//Initial revision.
//
//   Rev 1.1   Jan 17 2006 19:03:30   tmollart
//Updated with comments from code review. TabSection.None has now been removed.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.0   Jan 11 2006 13:47:18   tmollart
//Initial revision.

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Enum to keep track of tab-sections
	/// </summary>
	public enum TabSection
	{
		Home,
		PlanAJourney,
		FindAPlace,
		TravelInfo,
		TipsAndTools,
        LoginRegister
	}
}