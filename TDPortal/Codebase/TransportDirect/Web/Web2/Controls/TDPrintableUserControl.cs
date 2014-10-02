// *********************************************** 
// NAME                 : TDPrintableUserControl.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 06/01/2005
// DESCRIPTION			: Base class for controls which can display in a 
//                        format suitable for use on printable pages.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TDPrintableUserControl.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:10   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:17:08   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.0   Jan 10 2006 15:27:40   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Mar 21 2005 17:03:32   jgeorge
//FxCop changes
//
//   Rev 1.0   Jan 18 2005 11:44:40   jgeorge
//Initial revision.

using System;using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.Web;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Base class for controls which can display in a format suitable for use on printable pages.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class TDPrintableUserControl : TDUserControl, IPrintable
	{
		private bool printerFriendly;

		/// <summary>
		/// Default constructor. Does nothing extra.
		/// </summary>
		public TDPrintableUserControl() : base()
		{
		}

		/// <summary>
		/// Whether or not the control should display in Printer Friendly mode.
		/// </summary>
		public bool PrinterFriendly 
		{ 
			get { return printerFriendly; }
			set { printerFriendly = value; } 
		}


	}
}
