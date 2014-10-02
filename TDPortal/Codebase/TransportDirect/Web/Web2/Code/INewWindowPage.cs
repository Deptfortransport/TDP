// *********************************************** 
// NAME                 : INewWindowPage.cs
// AUTHOR               : Hassan Al-Katib
// DATE CREATED         : 18/11/2005
// DESCRIPTION			: Interface implemented by pages that open in a new window.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Code/INewWindowPage.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:18:48   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:10:54   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 16:18:10   RWilby
//Merged stream3129
//
//   Rev 1.1   Nov 18 2005 16:52:40   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface.
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//


using System;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Marker interface to identify whether the page is openned as a new window from the 
	/// TDportal application.
	/// </summary>
	public interface INewWindowPage
	{		
	}
}
