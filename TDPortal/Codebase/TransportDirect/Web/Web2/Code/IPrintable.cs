// *********************************************** 
// NAME                 : IPrintable.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 06/01/2005
// DESCRIPTION			: Interface implemented by any control that can be used on a printable page
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Code/IPrintable.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:18:48   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:10:54   mturner
//Initial revision.
//
//   Rev 1.1   Feb 23 2006 19:15:58   build
//Automatically merged from branch for stream3129
//
//   Rev 1.0.1.0   Jan 10 2006 15:53:36   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 18 2005 11:40:44   jgeorge
//Initial revision.

using System;using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Interface implemented by any control that can be used on a printable page
	/// </summary>
	public interface IPrintable
	{
		/// <summary>
		/// Whether or not the control should display in Printer Friendly mode.
		/// </summary>
		bool PrinterFriendly { get; set; }
	}
}